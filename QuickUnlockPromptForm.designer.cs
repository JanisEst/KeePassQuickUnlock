namespace KeePassQuickUnlock
{
	partial class QuickUnlockPromptForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.bannerImagePictureBox = new System.Windows.Forms.PictureBox();
			this.infoLabel = new System.Windows.Forms.Label();
			this.keyTextBox = new KeePass.UI.SecureTextBoxEx();
			this.hideKeyCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.bannerImagePictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(121, 126);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "&OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(202, 126);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// bannerImagePictureBox
			// 
			this.bannerImagePictureBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.bannerImagePictureBox.Location = new System.Drawing.Point(0, 0);
			this.bannerImagePictureBox.Name = "bannerImagePictureBox";
			this.bannerImagePictureBox.Size = new System.Drawing.Size(289, 60);
			this.bannerImagePictureBox.TabIndex = 2;
			this.bannerImagePictureBox.TabStop = false;
			// 
			// infoLabel
			// 
			this.infoLabel.AutoSize = true;
			this.infoLabel.Location = new System.Drawing.Point(12, 77);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new System.Drawing.Size(250, 13);
			this.infoLabel.TabIndex = 3;
			this.infoLabel.Text = "Enter the QuickUnlock key to unlock the database:";
			// 
			// keyTextBox
			// 
			this.keyTextBox.Location = new System.Drawing.Point(15, 93);
			this.keyTextBox.Name = "keyTextBox";
			this.keyTextBox.Size = new System.Drawing.Size(224, 20);
			this.keyTextBox.TabIndex = 4;
			// 
			// hideKeyCheckBox
			// 
			this.hideKeyCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.hideKeyCheckBox.Location = new System.Drawing.Point(245, 91);
			this.hideKeyCheckBox.Name = "hideKeyCheckBox";
			this.hideKeyCheckBox.Size = new System.Drawing.Size(32, 23);
			this.hideKeyCheckBox.TabIndex = 5;
			this.hideKeyCheckBox.Text = "***";
			this.hideKeyCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.hideKeyCheckBox.UseVisualStyleBackColor = true;
			this.hideKeyCheckBox.CheckedChanged += new System.EventHandler(this.OnCheckedHideKey);
			// 
			// QuickUnlockPromptForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(289, 158);
			this.Controls.Add(this.hideKeyCheckBox);
			this.Controls.Add(this.keyTextBox);
			this.Controls.Add(this.infoLabel);
			this.Controls.Add(this.bannerImagePictureBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "QuickUnlockPromptForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "<>";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Shown += new System.EventHandler(this.OnFormShown);
			((System.ComponentModel.ISupportInitialize)(this.bannerImagePictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.PictureBox bannerImagePictureBox;
		private System.Windows.Forms.Label infoLabel;
		private KeePass.UI.SecureTextBoxEx keyTextBox;
		private System.Windows.Forms.CheckBox hideKeyCheckBox;
	}
}