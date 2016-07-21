using System;
using System.Windows.Forms;

using KeePass.UI;
using KeePass.App;

namespace KeePassQuickUnlock
{
	public partial class QuickUnlockPromptForm : Form
	{
		private bool m_bInitializing = true;

		public string QuickUnlockKey
		{
			get { return keyTextBox.Text; }
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

			hideKeyCheckBox.Checked = true;

			keyTextBox.Text = string.Empty;

			OnCheckedHideKey(null, null);

			m_bInitializing = false;
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
			bool hide = hideKeyCheckBox.Checked;
			if (!hide && !AppPolicy.Try(AppPolicyId.UnhidePasswords))
			{
				hideKeyCheckBox.Checked = true;
				return;
			}

			keyTextBox.UseSystemPasswordChar = hide;

			if (!m_bInitializing)
			{
				UIUtil.SetFocus(keyTextBox, this);
			}
		}
	}
}
