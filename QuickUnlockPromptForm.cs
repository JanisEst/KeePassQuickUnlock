using System;
using System.Windows.Forms;
using KeePass.App;
using KeePass.UI;

namespace KeePassQuickUnlock
{
	public partial class QuickUnlockPromptForm : Form
	{
		private readonly bool initializing;

		private readonly SecureEdit secureEdit = new SecureEdit();

		public byte[] QuickUnlockKey
		{
			get { return secureEdit.ToUtf8(); }
		}

		public QuickUnlockPromptForm(bool isOnSecureDesktop)
		{
			InitializeComponent();

			initializing = true;

			GlobalWindowManager.AddWindow(this);

			Text = KeePassQuickUnlockExt.ShortProductName;
			BannerFactory.CreateBannerEx(this, bannerImagePictureBox, Properties.Resources.B48x48_TimeLock, KeePassQuickUnlockExt.ShortProductName, "Unlock using QuickUnlock.");

			hideKeyCheckBox.Checked = true;

			keyTextBox.Text = string.Empty;

			secureEdit.SecureDesktopMode = isOnSecureDesktop;
			secureEdit.Attach(keyTextBox, null, true);

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

			secureEdit.EnableProtection(hide);

			if (!initializing)
			{
				UIUtil.SetFocus(keyTextBox, this);
			}
		}
	}
}
