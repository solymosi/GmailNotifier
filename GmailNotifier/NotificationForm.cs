using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GmailNotifier
{
    public partial class NotificationForm : Form
    {
        const int WS_EX_NOACTIVATE = 0x8000000;
        const int WS_EX_TOOLWINDOW = 0x00000080;

        public List<string> Messages = new List<string>();
        int CurrentMessage = 0;

        public NotificationForm()
        {
            InitializeComponent();
            this.Text = Application.ProductName;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams BaseParams = base.CreateParams;
                BaseParams.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);
                return BaseParams;
            }
        }

        private void NotifierForm_Load(object sender, EventArgs e)
        {
            DisplayTimer_Tick(this, new EventArgs());
        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {

            if (CurrentMessage == Messages.Count)
            {
                this.Close();
                return;
            }

            ShowMessage(Messages[CurrentMessage]);
            CurrentMessage++;
        }

        private void ShowMessage(string Message)
        {
            Content.Rtf = Message;
        }

        private void Content_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            Content.Height = e.NewRectangle.Height;
            Height = Content.Height + 18;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
        }
    }
}
