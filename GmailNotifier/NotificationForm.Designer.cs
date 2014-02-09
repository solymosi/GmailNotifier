namespace GmailNotifier
{
    partial class NotificationForm
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
            this.components = new System.ComponentModel.Container();
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.Content = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayTimer
            // 
            this.DisplayTimer.Enabled = true;
            this.DisplayTimer.Interval = 5000;
            this.DisplayTimer.Tick += new System.EventHandler(this.DisplayTimer_Tick);
            // 
            // Content
            // 
            this.Content.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Content.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Content.DetectUrls = false;
            this.Content.Location = new System.Drawing.Point(50, 9);
            this.Content.Margin = new System.Windows.Forms.Padding(0);
            this.Content.Name = "Content";
            this.Content.ReadOnly = true;
            this.Content.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Content.Size = new System.Drawing.Size(299, 97);
            this.Content.TabIndex = 0;
            this.Content.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GmailNotifier.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(9, 9);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // NotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(358, 115);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Content);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotificationForm";
            this.Opacity = 0.8D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MaYoR értesítő";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NotifierForm_FormClosing);
            this.Load += new System.EventHandler(this.NotifierForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer DisplayTimer;
        private System.Windows.Forms.RichTextBox Content;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}