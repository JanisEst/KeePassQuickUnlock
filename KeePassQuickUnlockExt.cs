using KeePass.Forms;
using KeePass.Plugins;
using KeePass.UI;
using KeePassLib.Keys;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KeePassQuickUnlock
{
	public class KeePassQuickUnlockExt : Plugin
	{
		private static IPluginHost host = null;
		private static QuickUnlockProvider provider = null;

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

		public override bool Initialize(IPluginHost _host)
		{
			if (host != null) { Debug.Assert(false); Terminate(); }
			if (_host == null) { return false; }

			//Debugger.Launch();

			host = _host;

			provider = new QuickUnlockProvider();

			host.KeyProviderPool.Add(provider);

			host.MainWindow.FileClosingPre += OnFileClosingPre;

			GlobalWindowManager.WindowAdded += WindowAddedHandler;

			return true;
		}

		public override void Terminate()
		{
			if (host == null) { return; }

			GlobalWindowManager.WindowAdded -= WindowAddedHandler;

			host.MainWindow.FileClosingPre -= OnFileClosingPre;

			host.KeyProviderPool.Remove(provider);

			host = null;
		}

		/// <summary>
		/// Gets the masterkey before the database is closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFileClosingPre(object sender, FileClosingEventArgs e)
		{
			if (e == null) { Debug.Assert(false); return; }
			if (e.Cancel) { return; }

			if (Host.CustomConfig.GetBool(QuickUnlockProvider.CfgActive, false) == false)
			{
				return;
			}

			if (e.Database != null && e.Database.MasterKey != null)
			{
				var masterKey = e.Database.MasterKey;
				if (masterKey.UserKeyCount == 1) //we only support one key
				{
					var password = masterKey.GetUserKey(typeof(KcpPassword));
					if (password != null) //which must be a KcpPassword
					{
						provider.AddCachedKey(e.Database.IOConnectionInfo.Path, password as KcpPassword);
					}
				}
			}
		}

		/// <summary>
		/// Used to modify other form when they load.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WindowAddedHandler(object sender, GwmWindowEventArgs e)
		{
			//remove QuickUnlock auto select if there is no entry present
			var keyPromptForm = e.Form as KeyPromptForm;
			if (keyPromptForm != null)
			{
				keyPromptForm.Shown += delegate (object sender2, EventArgs e2)
				{
					var m_cmbKeyFile = keyPromptForm.Controls.Find("m_cmbKeyFile", false).FirstOrDefault() as ComboBox;
					if (m_cmbKeyFile != null)
					{
						if (m_cmbKeyFile.Text == ShortProductName)
						{
							if (!provider.HasCachedKeys)
							{
								var m_cbKeyFile = keyPromptForm.Controls.Find("m_cbKeyFile", false).FirstOrDefault() as CheckBox;
								if (m_cbKeyFile != null)
								{
									UIUtil.SetChecked(m_cbKeyFile, false);
								}
							}
						}
					}
				};
			}

			//add QuickUnlock options tab
			var optionsForm = e.Form as OptionsForm;
			if (optionsForm != null)
			{
				optionsForm.Shown += delegate(object sender2, EventArgs e2)
				{
					try
					{
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
