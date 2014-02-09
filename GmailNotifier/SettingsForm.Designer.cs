namespace GmailNotifier
{
    partial class SettingsForm
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
            if (disposing && (components != null))
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
            this.TitleLabel = new System.Windows.Forms.Label();
            this.User = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.Save = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.AutoStart = new System.Windows.Forms.CheckBox();
            this.EmailLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.GetAppPass = new System.Windows.Forms.LinkLabel();
            this.AppPassLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TitleLabel.Location = new System.Drawing.Point(12, 17);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(206, 13);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Enter your Gmail credentials below:";
            // 
            // User
            // 
            this.User.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.User.Location = new System.Drawing.Point(75, 47);
            this.User.Name = "User";
            this.User.Size = new System.Drawing.Size(295, 21);
            this.User.TabIndex = 5;
            // 
            // Password
            // 
            this.Password.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Password.Location = new System.Drawing.Point(75, 75);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(295, 21);
            this.Password.TabIndex = 8;
            this.Password.UseSystemPasswordChar = true;
            // 
            // Save
            // 
            this.Save.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Save.Location = new System.Drawing.Point(180, 170);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(92, 25);
            this.Save.TabIndex = 9;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Cancel.Location = new System.Drawing.Point(278, 170);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(92, 25);
            this.Cancel.TabIndex = 10;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // AutoStart
            // 
            this.AutoStart.AutoSize = true;
            this.AutoStart.Location = new System.Drawing.Point(15, 135);
            this.AutoStart.Name = "AutoStart";
            this.AutoStart.Size = new System.Drawing.Size(224, 17);
            this.AutoStart.TabIndex = 11;
            this.AutoStart.Text = "Automatically start Gmail Notifier at logon";
            this.AutoStart.UseVisualStyleBackColor = true;
            // 
            // EmailLabel
            // 
            this.EmailLabel.AutoSize = true;
            this.EmailLabel.Location = new System.Drawing.Point(12, 50);
            this.EmailLabel.Name = "EmailLabel";
            this.EmailLabel.Size = new System.Drawing.Size(35, 13);
            this.EmailLabel.TabIndex = 12;
            this.EmailLabel.Text = "Email:";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(12, 78);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(57, 13);
            this.PasswordLabel.TabIndex = 13;
            this.PasswordLabel.Text = "Password:";
            // 
            // GetAppPass
            // 
            this.GetAppPass.AutoSize = true;
            this.GetAppPass.Location = new System.Drawing.Point(206, 104);
            this.GetAppPass.Name = "GetAppPass";
            this.GetAppPass.Size = new System.Drawing.Size(148, 13);
            this.GetAppPass.TabIndex = 14;
            this.GetAppPass.TabStop = true;
            this.GetAppPass.Text = "Get an app-specific password";
            this.GetAppPass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GetAppPass_LinkClicked);
            // 
            // AppPassLabel
            // 
            this.AppPassLabel.AutoSize = true;
            this.AppPassLabel.Location = new System.Drawing.Point(72, 104);
            this.AppPassLabel.Name = "AppPassLabel";
            this.AppPassLabel.Size = new System.Drawing.Size(128, 13);
            this.AppPassLabel.TabIndex = 15;
            this.AppPassLabel.Text = "Using 2-step verification?";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(382, 207);
            this.Controls.Add(this.AppPassLabel);
            this.Controls.Add(this.GetAppPass);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.EmailLabel);
            this.Controls.Add(this.AutoStart);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.User);
            this.Controls.Add(this.TitleLabel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gmail Notifier - Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Cancel;
        public System.Windows.Forms.TextBox User;
        public System.Windows.Forms.TextBox Password;
        public System.Windows.Forms.CheckBox AutoStart;
        private System.Windows.Forms.Label EmailLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.LinkLabel GetAppPass;
        private System.Windows.Forms.Label AppPassLabel;
    }
}