using System;
using System.Drawing;
using System.Windows.Forms;

namespace KeePassQuickUnlock
{
	public partial class OptionsPanel : UserControl
	{
		public OptionsPanel(KeePassQuickUnlockExt plugin)
		{
			InitializeComponent();

			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			BackColor = Color.Transparent;

			activeCheckBox.Checked = KeePassQuickUnlockExt.Host.CustomConfig.GetBool(QuickUnlockProvider.CfgActive, false);
			numCharsNumericUpDown.Value = KeePassQuickUnlockExt.Host.CustomConfig.GetULong(QuickUnlockProvider.CfgNumChars, 3);
			whereComboBox.SelectedIndex = Math.Max(0, Math.Min(1, (int)KeePassQuickUnlockExt.Host.CustomConfig.GetLong(QuickUnlockProvider.CfgWhere, 0)));
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (ParentForm != null)
			{
				ParentForm.FormClosing += delegate (object sender2, FormClosingEventArgs e2)
				{
					if (ParentForm.DialogResult == DialogResult.OK)
					{
						KeePassQuickUnlockExt.Host.CustomConfig.SetBool(QuickUnlockProvider.CfgActive, activeCheckBox.Checked);
						KeePassQuickUnlockExt.Host.CustomConfig.SetULong(QuickUnlockProvider.CfgNumChars, (ulong)numCharsNumericUpDown.Value);
						KeePassQuickUnlockExt.Host.CustomConfig.SetLong(QuickUnlockProvider.CfgWhere, whereComboBox.SelectedIndex);
					}
				};
			}
		}
	}
}
