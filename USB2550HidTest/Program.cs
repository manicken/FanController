using System;
using System.Windows.Forms;
using USB2550HidTest.Forms;

namespace USB2550HidTest
{
    static class Program
    {
        static int runCount = 0;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (args.Length == 1)
            {
                runCount = int.Parse(args[0]);

                if (runCount == 5) return;

                runCount++;
            }
            
            ApplicationRun();            
        }

        private static string GetDateTime_FileNameCompatible()
        {
            return DateTime.Now.ToString().Replace(':', '_').Replace('\\', '_').Replace('/', '_');
        }

        private static string GetErrorLogfileName()
        {
            return "Error - " + GetDateTime_FileNameCompatible() + ".txt";
        }

        public static void ApplicationRun()
        {
            Mainform mf = new Mainform();
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.Run(mf);
            
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try { System.IO.File.WriteAllText(GetErrorLogfileName(), e.ExceptionObject.ToString()); }
                catch { }
            RerunApplication();// rerun the app in case of exception
        }

        public static void RerunApplication()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(Application.ExecutablePath);
            psi.Arguments = runCount.ToString();
            p.StartInfo = psi;
            p.Start();
        }
    }
}
