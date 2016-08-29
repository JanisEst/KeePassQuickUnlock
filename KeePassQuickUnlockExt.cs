using KeePass.Forms;
using KeePass.Plugins;
using KeePass.UI;
using KeePassLib;
using KeePassLib.Collections;
using KeePassLib.Keys;
using KeePassLib.Security;
using KeePassLib.Serialization;
using KeePassLib.Utility;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using STimers = System.Timers;

namespace KeePassQuickUnlock
{
	public class KeePassQuickUnlockExt : Plugin
	{
		private static IPluginHost host = null;
		private static QuickUnlockProvider provider = null;

		private STimers.Timer timer;

		public const string ShortProductName = "QuickUnlock";

		public static IPluginHost Host
		{
			get { return host; }
		}

		public override Image SmallIcon
		{
			get { return Properties.Resources.B16x16_TimeLock; }
		}

		public override string UpdateUrl
		{
			get { return "https://github.com/KN4CK3R/KeePassQuickUnlock/raw/master/keepass.version"; }
		}

		public static QuickUnlockProvider Provider
		{
			get { return provider; }
		}

		public override bool Initialize(IPluginHost _host)
		{
			if (host != null) { Debug.Assert(false); Terminate(); }
			if (_host == null) { return false; }

			//Debugger.Launch();

			host = _host;

			provider = new QuickUnlockProvider();

			host.KeyProviderPool.Add(provider);

			host.MainWindow.FileClosingPre += FileClosingPreHandler;

			GlobalWindowManager.WindowAdded += WindowAddedHandler;

			timer = new STimers.Timer(1000);
			timer.Elapsed += ElapsedHandler;
			timer.Start();

			return true;
		}

		public override void Terminate()
		{
			if (host == null) { return; }

			timer.Stop();
			timer.Elapsed -= ElapsedHandler;
			timer = null;

			GlobalWindowManager.WindowAdded -= WindowAddedHandler;

			host.MainWindow.FileClosingPre -= FileClosingPreHandler;

			host.KeyProviderPool.Remove(provider);

			host = null;
		}

		/// <summary>
		/// If the timer elapsed clear the expiered keys.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ElapsedHandler(object sender, STimers.ElapsedEventArgs e)
		{
			provider.ClearExpieredKeys();
		}

		/// <summary>
		/// Gets the masterkey before the database is closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FileClosingPreHandler(object sender, FileClosingEventArgs e)
		{
			if (e == null) { Debug.Assert(false); return; }
			if (e.Cancel) { return; }

			if (e.Database != null && e.Database.MasterKey != null)
			{
				var rootGroup = e.Database.RootGroup;
				if (rootGroup != null)
				{
					// Check if a QuickUnlock entry is available.
					var entry = rootGroup.GetEntries(true).Where(en => en.Strings.GetSafe(PwDefs.TitleField).ReadString() == ShortProductName).FirstOrDefault();
					if (entry != null)
					{
						var password = entry.Strings.GetSafe(PwDefs.PasswordField);
						if (password.IsEmpty == false)
						{
							provider.AddCachedKey(e.Database.IOConnectionInfo.Path, password, e.Database.MasterKey);

							return;
						}
					}
				}

				// Check if PartOfPassword is enabled...
				if (Host.CustomConfig.GetEnum(QuickUnlockProvider.CfgMode, Mode.Entry) == Mode.EntryOrPartOf)
				{
					// ...and there is a password available.
					var passwordKey = e.Database.MasterKey.UserKeys.Where(k => k is KcpPassword).FirstOrDefault() as KcpPassword;
					if (passwordKey != null)
					{
						var length = (int)Host.CustomConfig.GetLong(QuickUnlockProvider.CfgPartOfLength, QuickUnlockProvider.MinimumPartOfLength);
						if (passwordKey.Password.Length >= length)
						{
							var origin = Host.CustomConfig.GetEnum(QuickUnlockProvider.CfgPartOfOrigin, PartOfOrigin.Default);

							var chars = passwordKey.Password.GetChars();

							var startIndex = 0;
							if (origin == PartOfOrigin.End)
							{
								startIndex = chars.Length - length;
							}

							provider.AddCachedKey(e.Database.IOConnectionInfo.Path, chars.ToProtectedString(startIndex, length), e.Database.MasterKey);

							CharUtils.ZeroCharArray(chars);

							return;
						}
					}
				}

				// If no key is set, remove possible cached key.
				provider.RemoveCachedKey(e.Database.IOConnectionInfo.Path);
			}
		}

		/// <summary>
		/// Used to modify other form when they load.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WindowAddedHandler(object sender, GwmWindowEventArgs e)
		{
			var keyPromptForm = e.Form as KeyPromptForm;
			if (keyPromptForm != null)
			{
				keyPromptForm.Shown += delegate (object sender2, EventArgs e2)
				{
					// Warning: If one of the private fields get renamed this method will fail!
					var m_cmbKeyFile = keyPromptForm.Controls.Find("m_cmbKeyFile", false).FirstOrDefault() as ComboBox;
					if (m_cmbKeyFile != null)
					{
						var fieldInfo = keyPromptForm.GetType().GetField("m_ioInfo", BindingFlags.Instance | BindingFlags.NonPublic);
						if (fieldInfo != null)
						{
							var ioInfo = fieldInfo.GetValue(keyPromptForm) as IOConnectionInfo;
							if (ioInfo != null)
							{
								if (provider.IsCachedKey(ioInfo.Path))
								{
									var index = m_cmbKeyFile.Items.IndexOf(ShortProductName);
									if (index != -1)
									{
										m_cmbKeyFile.SelectedIndex = index;

										// If AutoPrompt is enabled click the Ok button.
										if (Host.CustomConfig.GetBool(QuickUnlockProvider.CfgAutoPrompt, true))
										{
											var m_btnOK = keyPromptForm.Controls.Find("m_btnOK", false).FirstOrDefault() as Button;
											if (m_btnOK != null)
											{
												m_btnOK.PerformClick();
											}
										}

										return;
									}
								}
							}
						}

						// If KeePass autoselected QuickUnlock but there isn't a key available just unselect it.
						if (m_cmbKeyFile.Text == ShortProductName)
						{
							var m_cbKeyFile = keyPromptForm.Controls.Find("m_cbKeyFile", false).FirstOrDefault() as CheckBox;
							if (m_cbKeyFile != null)
							{
								UIUtil.SetChecked(m_cbKeyFile, false);
							}
						}
					}
				};
			}

			var optionsForm = e.Form as OptionsForm;
			if (optionsForm != null)
			{
				optionsForm.Shown += delegate (object sender2, EventArgs e2)
				{
					try
					{
						// Add the QuickUnlock options tab.
						var m_tabMain = optionsForm.Controls.Find("m_tabMain", true).FirstOrDefault() as TabControl;
						if (m_tabMain != null)
						{
							if (m_tabMain.ImageList == null)
							{
								m_tabMain.ImageList = new ImageList();
							}
							var imageIndex = m_tabMain.ImageList.Images.Add(Properties.Resources.B16x16_TimeLock, Color.Transparent);

							var newTab = new TabPage(ShortProductName);
							newTab.UseVisualStyleBackColor = true;
							newTab.ImageIndex = imageIndex;

							var optionsPanel = new OptionsPanel(this);
							newTab.Controls.Add(optionsPanel);
							optionsPanel.Dock = DockStyle.Fill;

							m_tabMain.TabPages.Add(newTab);
						}
					}
					catch (Exception ex)
					{
						Debug.Fail(ex.ToString());
					}
				};
			}
		}
	}
}
