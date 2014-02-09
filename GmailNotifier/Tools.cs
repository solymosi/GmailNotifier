using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Media;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GmailNotifier
{
    static class Tools
    {
        public static string SettingsDirectory
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GmailNotifier"; }
        }

        public static void ErrorMessage(string Message)
        {
            MessageBox.Show(Message, Application.ProductName, 0, MessageBoxIcon.Error);
        }

        public static void PlayNotificationSound()
        {
            try
            {
                string Path = (string)Registry.GetValue(@"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\MailBeep\.Default", null, "");
                new SoundPlayer(Path).Play();
            }
            catch { }
        }

        public static void Serialize<T>(T Object, string FileName)
        {
            CreateSettingsDirectory();

            using (Stream Stream = File.Open(SettingsDirectory + Path.DirectorySeparatorChar.ToString() + FileName, FileMode.Create))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                Formatter.Serialize(Stream, Object);
            }
        }

        public static T Deserialize<T>(string FileName)
        {
            CreateSettingsDirectory();

            using (Stream Stream = File.Open(SettingsDirectory + Path.DirectorySeparatorChar.ToString() + FileName, FileMode.Open))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                return (T)Formatter.Deserialize(Stream);
            }
        }

        public static void CreateSettingsDirectory()
        {
            if (!Directory.Exists(SettingsDirectory))
            {
                Directory.CreateDirectory(SettingsDirectory);
            }
        }

        public static string EscapeRtf(string Rtf)
        {
            StringBuilder Builder = new StringBuilder();

            foreach (char Character in Rtf)
            {
                if (Character == '\\' || Character == '{' || Character == '}')
                {
                    Builder.Append(@"\" + Character);
                }
                else if (Character <= 0x7f)
                {
                    Builder.Append(Character);
                }
                else
                {
                    Builder.Append("\\u" + Convert.ToUInt32(Character) + "?");
                }
            }

            return Builder.ToString();
        }

        public static string FormatDate(DateTime Date)
        {
            if ((Date.Year == DateTime.Now.Year && Date.Month == DateTime.Now.Month && Date.Day == DateTime.Now.Day) || (Date.Year == DateTime.Now.AddDays(-1).Year && Date.Month == DateTime.Now.AddDays(-1).Month && Date.Day == DateTime.Now.AddDays(-1).Day && (DateTime.Now - Date).TotalHours < 12))
            {
                return Date.Hour.ToString("D2") + ":" + Date.Minute.ToString("D2");
            }

            if (Date.Year == DateTime.Now.Year || (Date.Year == DateTime.Now.AddYears(-1).Year && (DateTime.Now - Date).TotalDays < 180))
            {
                return Date.ToString("MMM d");
            }

            return Date.ToString("MMM d, yyyy");
        }
    }
}
