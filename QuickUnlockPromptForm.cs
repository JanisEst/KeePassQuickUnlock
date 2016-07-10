using System;
using System.Windows.Forms;

using KeePass.UI;
using KeePass.App;

namespace KeePassQuickUnlock
{
	public partial class QuickUnlockPromptForm : Form
	{
		private bool m_bInitializing = true;

		public string QuickPassword
		{
			get { return passwordTextBox.Text; }
		}

		public QuickUnlockPromptForm()
		{
			InitializeComponent();

			m_bInitializing = true;

			GlobalWindowManager.AddWindow(this);

			string strTitle = KeePassQuickUnlockExt.ShortProductName;
			string strDesc = "Unlock using QuickUnlock.";

			Text = strTitle;
			BannerFactory.CreateBannerEx(this, bannerImagePictureBox, Properties.Resources.B48x48_TimeLock, strTitle, strDesc);

			hidePasswordCheckBox.Checked = true;

			passwordTextBox.Text = string.Empty;

			OnCheckedHidePassword(null, null);

			m_bInitializing = false;
		}

		private void OnFormShown(object sender, EventArgs e)
		{
			UIUtil.SetFocus(passwordTextBox, this);
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			GlobalWindowManager.RemoveWindow(this);
		}

		private void OnCheckedHidePassword(object sender, EventArgs e)
		{
			bool hide = hidePasswordCheckBox.Checked;
			if (!hide && !AppPolicy.Try(AppPolicyId.UnhidePasswords))
			{
				hidePasswordCheckBox.Checked = true;
				return;
			}

			passwordTextBox.UseSystemPasswordChar = hide;

			if (!m_bInitializing)
			{
				UIUtil.SetFocus(passwordTextBox, this);
			}
		}
	}
}
