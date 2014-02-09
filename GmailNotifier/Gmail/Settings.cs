using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace GmailNotifier
{
    [Serializable()]
    public class Settings
    {
        public string User = "";
        public string EncryptedPassword = "";

        public string Password
        {
            get
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(EncryptedPassword));
            }
            set
            {
                EncryptedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
            }
        }

        public bool AutoStart
        {
            get
            {
                return (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Gmail", null) != null);
            }
            set
            {
                if (value) { Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Gmail", Application.ExecutablePath); }
                else
                {
                    try { Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true).DeleteValue("Gmail"); }
                    catch { }
                }
            }
        }

        public bool Present
        {
            get { return this.EncryptedPassword != "" && this.User != ""; }
        }
    }
}
