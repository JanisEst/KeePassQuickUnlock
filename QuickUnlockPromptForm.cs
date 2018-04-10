using System;
using System.Diagnostics;
using System.Windows.Forms;
using KeePass.App;
using KeePass.UI;
using KeePassLib.Security;

namespace KeePassQuickUnlock
{
	public partial class QuickUnlockPromptForm : Form
	{
		private readonly bool initializing;

		private readonly Func<byte[]> getQuickUnlockKeyFn;
		private readonly Action<bool> enableProtectionFn;

		public byte[] QuickUnlockKey
		{
			get { return getQuickUnlockKeyFn(); }
		}

		public QuickUnlockPromptForm(bool isOnSecureDesktop)
		{
			InitializeComponent();

			initializing = true;

			GlobalWindowManager.AddWindow(this);

			Text = KeePassQuickUnlockExt.ShortProductName;
			BannerFactory.CreateBannerEx(this, bannerImagePictureBox, Properties.Resources.B48x48_TimeLock, KeePassQuickUnlockExt.ShortProductName, "Unlock using QuickUnlock.");

			// TODO: Replace with native code for 2.40
			// If KeePass < 2.39
			var secureTextBoxExType = Type.GetType("KeePass.UI.SecureTextBoxEx, KeePass");
			if (secureTextBoxExType == null)
			{
				var secureEditType = Type.GetType("KeePass.UI.SecureEdit, KeePass");
				Debug.Assert(secureEditType != null);

				var secureEdit = Activator.CreateInstance(secureEditType);

				var secureDesktopMode = secureEditType.GetProperty("SecureDesktopMode");
				Debug.Assert(secureDesktopMode != null);
				secureDesktopMode.SetValue(secureEdit, isOnSecureDesktop, null);

				var attach = secureEditType.GetMethod("Attach");
				Debug.Assert(attach != null);
				attach.Invoke(secureEdit, new object[] { keyTextBox, null, true });

				var toUtf8 = secureEditType.GetMethod("ToUtf8");
				Debug.Assert(toUtf8 != null);
				getQuickUnlockKeyFn = () => toUtf8.Invoke(secureEdit, null) as byte[];

				var enableProtection = secureEditType.GetMethod("EnableProtection");
				Debug.Assert(enableProtection != null);
				enableProtectionFn = hide => enableProtection.Invoke(secureEdit, new object[] { hide });
			}
			else
			{
				var secureTextBoxEx = Activator.CreateInstance(secureTextBoxExType) as TextBox;
				Debug.Assert(secureTextBoxEx != null);
				secureTextBoxEx.Location = keyTextBox.Location;
				secureTextBoxEx.Name = keyTextBox.Name;
				secureTextBoxEx.Size = keyTextBox.Size;
				secureTextBoxEx.TabIndex = keyTextBox.TabIndex;

				var initEx = secureTextBoxExType.GetMethod("InitEx");
				Debug.Assert(initEx != null);
				initEx.Invoke(null, new object[] { secureTextBoxEx });

				var textEx = secureTextBoxExType.GetProperty("TextEx");
				Debug.Assert(textEx != null);
				getQuickUnlockKeyFn = () => (textEx.GetValue(secureTextBoxEx, null) as ProtectedString).ReadUtf8();

				var enableProtection = secureTextBoxExType.GetMethod("EnableProtection");
				Debug.Assert(enableProtection != null);
				enableProtectionFn = hide => enableProtection.Invoke(secureTextBoxEx, new object[] { hide });

				Controls.Remove(keyTextBox);
				keyTextBox = secureTextBoxEx;
				Controls.Add(keyTextBox);
			}

			keyTextBox.Text = string.Empty;

			hideKeyCheckBox.Checked = true;
			OnCheckedHideKey(null, null);

			initializing = false;
		}

		private void OnFormShown(object sender, EventArgs e)
		{
			UIUtil.SetFocus(keyTextBox, this);
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			GlobalWindowManager.RemoveWindow(this);
		}

		private void OnCheckedHideKey(object sender, EventArgs e)
		{
			var hide = hideKeyCheckBox.Checked;
			if (!hide && !AppPolicy.Try(AppPolicyId.UnhidePasswords))
			{
				hideKeyCheckBox.Checked = true;
				return;
			}

			enableProtectionFn(hide);

			if (!initializing)
			{
				UIUtil.SetFocus(keyTextBox, this);
			}
		}
	}
}
