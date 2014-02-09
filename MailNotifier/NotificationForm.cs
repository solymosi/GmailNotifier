using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MailNotifier
{
    public partial class NotificationForm : Form
    {
        const int AW_SLIDE = 0X40000;
        const int AW_HIDE = 0X10000;
        const int AW_VER_POSITIVE = 0X4;
        const int AW_VER_NEGATIVE = 0X8;
        const int WS_EX_NOACTIVATE = 0x8000000;
        const int WS_EX_TOOLWINDOW = 0x00000080;

        public List<Email> Emails = new List<Email>();
        int CurrentNote = 0;

        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);

        public NotificationForm()
        {
            InitializeComponent();
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
            AnimateWindow(this.Handle, 500, AW_SLIDE | AW_VER_NEGATIVE);
        }

        private void NotifierForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimateWindow(this.Handle, 500, AW_SLIDE | AW_VER_POSITIVE | AW_HIDE);
        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {

            if (CurrentNote == Emails.Count)
            {
                this.Close();
                return;
            }
            ShowChange(Emails[CurrentNote]);
            CurrentNote++;
        }

        private void ShowChange(Email Email)
        {
            this.Height = 82;

            /*Note First = Email.Type == ChangeType.Added ? Email.New : Email.Old;
            Note Second = Email.Type == ChangeType.Modified ? Email.New : null;*/

            Title.Text = Email.Subject;
            Date.Text = Email.Date.ToShortDateString();
            /*OldName.Text = First.Name + " (" + First.FriendlyType + ")";
            OldNote.Text = First.Grade;
            if (Email.Type == ChangeType.Modified)
            {
                NewName.Text = Second.Name + " (" + Second.FriendlyType + ")";
                NewNote.Text = Second.Grade;
            }*/

            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
        }

    }
}
