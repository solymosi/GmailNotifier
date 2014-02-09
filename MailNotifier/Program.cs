using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MailNotifier
{
    static class Program
    {
        static public Context Context;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Guardian.LaunchApplication();
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }

    static class Guardian
    {
        public static void LaunchApplication()
        {
            try
            {
                Program.Context = new Context();
                Application.Run(Program.Context);
            }
            catch (Exception Exception)
            {
                Crashed(Exception);
            }
        }

        public static void Crashed(Exception Exception)
        {
            Tools.ErrorMessage("Oops, Gmail Notifier has crashed.\r\nDetails for geeks:\r\n" + Exception.Message + "\r\n" + Exception.StackTrace);
            Program.Exit();
        }
    }
}
