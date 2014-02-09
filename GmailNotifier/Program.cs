using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace GmailNotifier
{
    static class Program
    {
        static public Context Context;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //try
            //{
                bool Launch = false;

                using (Mutex Mutex = new Mutex(true, "GmailNotifier", out Launch))
                {
                    if (!Launch)
                    {
                        Tools.ErrorMessage("Gmail Notifier is already running. Check the notification area next to the clock to find its icon.");
                        Exit();
                    }

                    Context = new Context();
                    Application.Run(Program.Context);
                }
            //}
            //catch (Exception e)
            //{
            //    Tools.ErrorMessage("Aw, snap! Gmail Notifier has crashed :(\r\n\r\nThe error is caused by a " + e.GetType().FullName + (e.Message != null && e.Message != "" ? ": " + e.Message : "") + "\r\n" + e.StackTrace);
            //}
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
