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

		public byte[] QuickUnlockKey
		{
			get { return keyTextBox.TextEx.ReadUtf8(); }
		}

		public QuickUnlockPromptForm(bool isOnSecureDesktop)
		{
			InitializeComponent();

			initializing = true;

			GlobalWindowManager.AddWindow(this);

			Text = KeePassQuickUnlockExt.ShortProductName;
			BannerFactory.CreateBannerEx(this, bannerImagePictureBox, Properties.Resources.B48x48_TimeLock, KeePassQuickUnlockExt.ShortProductName, "Unlock using QuickUnlock.");

			SecureTextBoxEx.InitEx(ref keyTextBox);

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

			keyTextBox.EnableProtection(hide);

			if (!initializing)
			{
				UIUtil.SetFocus(keyTextBox, this);
			}
		}
	}
}
