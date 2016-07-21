using KeePass.App;
using KeePass.UI;
using System;
using System.Diagnostics;
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

			var config = KeePassQuickUnlockExt.Host.CustomConfig;

			switch (config.GetEnum(QuickUnlockProvider.CfgMode, Mode.Default))
			{
				default:
				case Mode.Entry: modeEntryRadioButton.Checked = true; break;
				case Mode.EntryOrPartOf: modeEntryPartOfRadioButton.Checked = true; break;
			}

			validPeriodComboBox.SelectedIndex = PeriodToIndex(
				config.GetULong(
					QuickUnlockProvider.CfgValidPeriod,
					QuickUnlockProvider.VALID_DEFAULT
				)
			);

			switch (config.GetEnum(QuickUnlockProvider.CfgPartOfOrigin, PartOfOrigin.Default))
			{
				default:
				case PartOfOrigin.Front: originFrontRadioButton.Checked = true; break;
				case PartOfOrigin.End: originEndRadioButton.Checked = true; break;
			}

			lengthNumericUpDown.Value = config.GetULong(QuickUnlockProvider.CfgPartOfLength, QuickUnlockProvider.MinimumPartOfLength);
		}

		private ulong IndexToPeriod(int index)
		{
			switch (index)
			{
				case 0: return QuickUnlockProvider.VALID_UNLIMITED;
				case 1: return QuickUnlockProvider.VALID_1MINUTE;
				case 2: return QuickUnlockProvider.VALID_5MINUTES;
				case 3: return QuickUnlockProvider.VALID_10MINUTES;
				case 4: return QuickUnlockProvider.VALID_15MINUTES;
				case 5: return QuickUnlockProvider.VALID_30MINUTES;
				case 6: return QuickUnlockProvider.VALID_1HOUR;
				case 7: return QuickUnlockProvider.VALID_2HOURS;
				case 8: return QuickUnlockProvider.VALID_6HOURS;
				case 9: return QuickUnlockProvider.VALID_12HOURS;
				case 10: return QuickUnlockProvider.VALID_1DAY;
				default:return QuickUnlockProvider.VALID_DEFAULT;
			}
		}

		private int PeriodToIndex(ulong period)
		{
			switch (period)
			{
				case QuickUnlockProvider.VALID_UNLIMITED: return 0;
				case QuickUnlockProvider.VALID_1MINUTE: return 1;
				case QuickUnlockProvider.VALID_5MINUTES: return 2;
				case QuickUnlockProvider.VALID_10MINUTES: return 3;
				case QuickUnlockProvider.VALID_15MINUTES: return 4;
				case QuickUnlockProvider.VALID_30MINUTES: return 5;
				case QuickUnlockProvider.VALID_1HOUR: return 6;
				case QuickUnlockProvider.VALID_2HOURS: return 7;
				case QuickUnlockProvider.VALID_6HOURS: return 8;
				case QuickUnlockProvider.VALID_12HOURS: return 9;
				case QuickUnlockProvider.VALID_1DAY: return 10;
				default: return 3;
			}
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
						var config = KeePassQuickUnlockExt.Host.CustomConfig;
						
						config.SetEnum(
							QuickUnlockProvider.CfgMode,
							modeEntryRadioButton.Checked ? Mode.Entry : Mode.EntryOrPartOf
						);
						config.SetULong(
							QuickUnlockProvider.CfgValidPeriod,
							IndexToPeriod(validPeriodComboBox.SelectedIndex)
						);
						config.SetEnum(
							QuickUnlockProvider.CfgPartOfOrigin,
							originFrontRadioButton.Checked ? PartOfOrigin.Front : PartOfOrigin.End
						);
						config.SetULong(
							QuickUnlockProvider.CfgPartOfLength,
							(ulong)lengthNumericUpDown.Value
						);
					}
				};
			}
		}

		private void helpButton_Click(object sender, EventArgs e)
		{
			Process.Start("https://github.com/KN4CK3R/KeePassQuickUnlock/blob/master/README.md");
		}
	}
}
