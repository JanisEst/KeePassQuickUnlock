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
			this.numCharsLabel = new System.Windows.Forms.Label();
			this.numCharsNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.whereLabel = new System.Windows.Forms.Label();
			this.whereComboBox = new System.Windows.Forms.ComboBox();
			this.infoLabel = new System.Windows.Forms.Label();
			this.activeCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numCharsNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// numCharsLabel
			// 
			this.numCharsLabel.AutoSize = true;
			this.numCharsLabel.Location = new System.Drawing.Point(3, 72);
			this.numCharsLabel.Name = "numCharsLabel";
			this.numCharsLabel.Size = new System.Drawing.Size(137, 13);
			this.numCharsLabel.TabIndex = 0;
			this.numCharsLabel.Text = "Length of QuickUnlock key";
			// 
			// numCharsNumericUpDown
			// 
			this.numCharsNumericUpDown.Location = new System.Drawing.Point(150, 70);
			this.numCharsNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numCharsNumericUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.numCharsNumericUpDown.Name = "numCharsNumericUpDown";
			this.numCharsNumericUpDown.Size = new System.Drawing.Size(121, 20);
			this.numCharsNumericUpDown.TabIndex = 1;
			this.numCharsNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			// 
			// whereLabel
			// 
			this.whereLabel.AutoSize = true;
			this.whereLabel.Location = new System.Drawing.Point(3, 99);
			this.whereLabel.Name = "whereLabel";
			this.whereLabel.Size = new System.Drawing.Size(141, 13);
			this.whereLabel.TabIndex = 2;
			this.whereLabel.Text = "Position of QuickUnlock key";
			// 
			// whereComboBox
			// 
			this.whereComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.whereComboBox.FormattingEnabled = true;
			this.whereComboBox.Items.AddRange(new object[] {
            "Front",
            "Back"});
			this.whereComboBox.Location = new System.Drawing.Point(150, 96);
			this.whereComboBox.Name = "whereComboBox";
			this.whereComboBox.Size = new System.Drawing.Size(121, 21);
			this.whereComboBox.TabIndex = 3;
			// 
			// infoLabel
			// 
			this.infoLabel.AutoSize = true;
			this.infoLabel.Location = new System.Drawing.Point(3, 9);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new System.Drawing.Size(311, 26);
			this.infoLabel.TabIndex = 4;
			this.infoLabel.Text = "Unlock your database once with your full password, re-open it by\r\ntyping just a f" +
    "ew characters.";
			// 
			// activeCheckBox
			// 
			this.activeCheckBox.AutoSize = true;
			this.activeCheckBox.Location = new System.Drawing.Point(6, 45);
			this.activeCheckBox.Name = "activeCheckBox";
			this.activeCheckBox.Size = new System.Drawing.Size(130, 17);
			this.activeCheckBox.TabIndex = 5;
			this.activeCheckBox.Text = "Activate QuickUnlock";
			this.activeCheckBox.UseVisualStyleBackColor = true;
			// 
			// OptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.activeCheckBox);
			this.Controls.Add(this.infoLabel);
			this.Controls.Add(this.whereComboBox);
			this.Controls.Add(this.whereLabel);
			this.Controls.Add(this.numCharsNumericUpDown);
			this.Controls.Add(this.numCharsLabel);
			this.Name = "OptionsPanel";
			this.Size = new System.Drawing.Size(320, 131);
			((System.ComponentModel.ISupportInitialize)(this.numCharsNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label numCharsLabel;
		private System.Windows.Forms.NumericUpDown numCharsNumericUpDown;
		private System.Windows.Forms.Label whereLabel;
		private System.Windows.Forms.ComboBox whereComboBox;
		private System.Windows.Forms.Label infoLabel;
		private System.Windows.Forms.CheckBox activeCheckBox;
	}
}
