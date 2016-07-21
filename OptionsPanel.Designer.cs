namespace KeePassQuickUnlock
{
	partial class OptionsPanel
	{
		/// <summary> 
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Komponenten-Designer generierter Code

		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.infoLabel = new System.Windows.Forms.Label();
			this.validPeriodComboBox = new System.Windows.Forms.ComboBox();
			this.info2Label = new System.Windows.Forms.Label();
			this.modeGroupBox = new System.Windows.Forms.GroupBox();
			this.modeEntryPartOfRadioButton = new System.Windows.Forms.RadioButton();
			this.modeEntryRadioButton = new System.Windows.Forms.RadioButton();
			this.settingsGroupBox = new System.Windows.Forms.GroupBox();
			this.partOfGroupBox = new System.Windows.Forms.GroupBox();
			this.originLabel = new System.Windows.Forms.Label();
			this.lengthLabel = new System.Windows.Forms.Label();
			this.partOfInfo2Label = new System.Windows.Forms.Label();
			this.lengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.originEndRadioButton = new System.Windows.Forms.RadioButton();
			this.originFrontRadioButton = new System.Windows.Forms.RadioButton();
			this.partOfInfoLabel = new System.Windows.Forms.Label();
			this.helpButton = new System.Windows.Forms.Button();
			this.modeGroupBox.SuspendLayout();
			this.settingsGroupBox.SuspendLayout();
			this.partOfGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lengthNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// infoLabel
			// 
			this.infoLabel.AutoSize = true;
			this.infoLabel.Location = new System.Drawing.Point(6, 19);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new System.Drawing.Size(364, 13);
			this.infoLabel.TabIndex = 4;
			this.infoLabel.Text = "Select the time range after which a QuickUnlock password gets invalidated:";
			// 
			// validPeriodComboBox
			// 
			this.validPeriodComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.validPeriodComboBox.FormattingEnabled = true;
			this.validPeriodComboBox.Items.AddRange(new object[] {
            "Unlimited",
            "1 Minute",
            "5 Minutes",
            "10 Minutes",
            "15 Minutes",
            "30 Minutes",
            "1 Hour",
            "2 Hours",
            "6 Hours",
            "12 Hours",
            "1 Day"});
			this.validPeriodComboBox.Location = new System.Drawing.Point(9, 35);
			this.validPeriodComboBox.Name = "validPeriodComboBox";
			this.validPeriodComboBox.Size = new System.Drawing.Size(186, 21);
			this.validPeriodComboBox.TabIndex = 5;
			// 
			// info2Label
			// 
			this.info2Label.AutoSize = true;
			this.info2Label.Location = new System.Drawing.Point(6, 60);
			this.info2Label.Name = "info2Label";
			this.info2Label.Size = new System.Drawing.Size(454, 13);
			this.info2Label.TabIndex = 6;
			this.info2Label.Text = "If the password timed out you need to provide the full credentials to unlock the " +
    "database again.";
			// 
			// modeGroupBox
			// 
			this.modeGroupBox.Controls.Add(this.modeEntryPartOfRadioButton);
			this.modeGroupBox.Controls.Add(this.modeEntryRadioButton);
			this.modeGroupBox.Location = new System.Drawing.Point(3, 3);
			this.modeGroupBox.Name = "modeGroupBox";
			this.modeGroupBox.Size = new System.Drawing.Size(263, 68);
			this.modeGroupBox.TabIndex = 7;
			this.modeGroupBox.TabStop = false;
			this.modeGroupBox.Text = "QuickUnlock Mode";
			// 
			// modeEntryPartOfRadioButton
			// 
			this.modeEntryPartOfRadioButton.AutoSize = true;
			this.modeEntryPartOfRadioButton.Location = new System.Drawing.Point(9, 42);
			this.modeEntryPartOfRadioButton.Name = "modeEntryPartOfRadioButton";
			this.modeEntryPartOfRadioButton.Size = new System.Drawing.Size(244, 17);
			this.modeEntryPartOfRadioButton.TabIndex = 2;
			this.modeEntryPartOfRadioButton.TabStop = true;
			this.modeEntryPartOfRadioButton.Text = "\'QuickUnlock\' Entry / Part of Master Password";
			this.modeEntryPartOfRadioButton.UseVisualStyleBackColor = true;
			// 
			// modeEntryRadioButton
			// 
			this.modeEntryRadioButton.AutoSize = true;
			this.modeEntryRadioButton.Location = new System.Drawing.Point(9, 19);
			this.modeEntryRadioButton.Name = "modeEntryRadioButton";
			this.modeEntryRadioButton.Size = new System.Drawing.Size(140, 17);
			this.modeEntryRadioButton.TabIndex = 1;
			this.modeEntryRadioButton.TabStop = true;
			this.modeEntryRadioButton.Text = "\'QuickUnlock\' Entry only";
			this.modeEntryRadioButton.UseVisualStyleBackColor = true;
			// 
			// settingsGroupBox
			// 
			this.settingsGroupBox.Controls.Add(this.partOfGroupBox);
			this.settingsGroupBox.Controls.Add(this.infoLabel);
			this.settingsGroupBox.Controls.Add(this.validPeriodComboBox);
			this.settingsGroupBox.Controls.Add(this.info2Label);
			this.settingsGroupBox.Location = new System.Drawing.Point(3, 77);
			this.settingsGroupBox.Name = "settingsGroupBox";
			this.settingsGroupBox.Size = new System.Drawing.Size(540, 213);
			this.settingsGroupBox.TabIndex = 8;
			this.settingsGroupBox.TabStop = false;
			this.settingsGroupBox.Text = "QuickUnlock Settings";
			// 
			// partOfGroupBox
			// 
			this.partOfGroupBox.Controls.Add(this.originLabel);
			this.partOfGroupBox.Controls.Add(this.lengthLabel);
			this.partOfGroupBox.Controls.Add(this.partOfInfo2Label);
			this.partOfGroupBox.Controls.Add(this.lengthNumericUpDown);
			this.partOfGroupBox.Controls.Add(this.originEndRadioButton);
			this.partOfGroupBox.Controls.Add(this.originFrontRadioButton);
			this.partOfGroupBox.Controls.Add(this.partOfInfoLabel);
			this.partOfGroupBox.Location = new System.Drawing.Point(9, 80);
			this.partOfGroupBox.Name = "partOfGroupBox";
			this.partOfGroupBox.Size = new System.Drawing.Size(520, 124);
			this.partOfGroupBox.TabIndex = 7;
			this.partOfGroupBox.TabStop = false;
			this.partOfGroupBox.Text = "Part of Master Password";
			// 
			// originLabel
			// 
			this.originLabel.AutoSize = true;
			this.originLabel.Location = new System.Drawing.Point(6, 43);
			this.originLabel.Name = "originLabel";
			this.originLabel.Size = new System.Drawing.Size(37, 13);
			this.originLabel.TabIndex = 6;
			this.originLabel.Text = "Origin:";
			// 
			// lengthLabel
			// 
			this.lengthLabel.AutoSize = true;
			this.lengthLabel.Location = new System.Drawing.Point(6, 66);
			this.lengthLabel.Name = "lengthLabel";
			this.lengthLabel.Size = new System.Drawing.Size(43, 13);
			this.lengthLabel.TabIndex = 5;
			this.lengthLabel.Text = "Length:";
			// 
			// partOfInfo2Label
			// 
			this.partOfInfo2Label.AutoSize = true;
			this.partOfInfo2Label.Location = new System.Drawing.Point(6, 90);
			this.partOfInfo2Label.Name = "partOfInfo2Label";
			this.partOfInfo2Label.Size = new System.Drawing.Size(362, 26);
			this.partOfInfo2Label.TabIndex = 4;
			this.partOfInfo2Label.Text = "If the length of the master password is smaller than the QuickUnlock length,\r\nQui" +
    "ckUnlock will not be available.";
			// 
			// lengthNumericUpDown
			// 
			this.lengthNumericUpDown.Location = new System.Drawing.Point(56, 64);
			this.lengthNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.lengthNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.lengthNumericUpDown.Name = "lengthNumericUpDown";
			this.lengthNumericUpDown.Size = new System.Drawing.Size(130, 20);
			this.lengthNumericUpDown.TabIndex = 3;
			this.lengthNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			// 
			// originEndRadioButton
			// 
			this.originEndRadioButton.AutoSize = true;
			this.originEndRadioButton.Location = new System.Drawing.Point(111, 41);
			this.originEndRadioButton.Name = "originEndRadioButton";
			this.originEndRadioButton.Size = new System.Drawing.Size(44, 17);
			this.originEndRadioButton.TabIndex = 2;
			this.originEndRadioButton.TabStop = true;
			this.originEndRadioButton.Text = "End";
			this.originEndRadioButton.UseVisualStyleBackColor = true;
			// 
			// originFrontRadioButton
			// 
			this.originFrontRadioButton.AutoSize = true;
			this.originFrontRadioButton.Location = new System.Drawing.Point(56, 41);
			this.originFrontRadioButton.Name = "originFrontRadioButton";
			this.originFrontRadioButton.Size = new System.Drawing.Size(49, 17);
			this.originFrontRadioButton.TabIndex = 1;
			this.originFrontRadioButton.TabStop = true;
			this.originFrontRadioButton.Text = "Front";
			this.originFrontRadioButton.UseVisualStyleBackColor = true;
			// 
			// partOfInfoLabel
			// 
			this.partOfInfoLabel.AutoSize = true;
			this.partOfInfoLabel.Location = new System.Drawing.Point(6, 19);
			this.partOfInfoLabel.Name = "partOfInfoLabel";
			this.partOfInfoLabel.Size = new System.Drawing.Size(312, 13);
			this.partOfInfoLabel.TabIndex = 0;
			this.partOfInfoLabel.Text = "Select the origin and the length of the used part of the password.";
			// 
			// helpButton
			// 
			this.helpButton.Image = global::KeePassQuickUnlock.Properties.Resources.B16x16_Help;
			this.helpButton.Location = new System.Drawing.Point(520, 3);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(23, 23);
			this.helpButton.TabIndex = 3;
			this.helpButton.UseVisualStyleBackColor = true;
			this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
			// 
			// OptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.settingsGroupBox);
			this.Controls.Add(this.modeGroupBox);
			this.Name = "OptionsPanel";
			this.Size = new System.Drawing.Size(556, 298);
			this.modeGroupBox.ResumeLayout(false);
			this.modeGroupBox.PerformLayout();
			this.settingsGroupBox.ResumeLayout(false);
			this.settingsGroupBox.PerformLayout();
			this.partOfGroupBox.ResumeLayout(false);
			this.partOfGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lengthNumericUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label infoLabel;
		private System.Windows.Forms.ComboBox validPeriodComboBox;
		private System.Windows.Forms.Label info2Label;
		private System.Windows.Forms.GroupBox modeGroupBox;
		private System.Windows.Forms.RadioButton modeEntryPartOfRadioButton;
		private System.Windows.Forms.RadioButton modeEntryRadioButton;
		private System.Windows.Forms.GroupBox settingsGroupBox;
		private System.Windows.Forms.GroupBox partOfGroupBox;
		private System.Windows.Forms.RadioButton originFrontRadioButton;
		private System.Windows.Forms.Label partOfInfoLabel;
		private System.Windows.Forms.Label originLabel;
		private System.Windows.Forms.Label lengthLabel;
		private System.Windows.Forms.Label partOfInfo2Label;
		private System.Windows.Forms.NumericUpDown lengthNumericUpDown;
		private System.Windows.Forms.RadioButton originEndRadioButton;
		private System.Windows.Forms.Button helpButton;
	}
}
