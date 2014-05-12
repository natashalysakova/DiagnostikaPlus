using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Properties;

namespace adovipavto
{
    internal static class Program
    {
        private static readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());



        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;
            ;


            //if (!File.Exists("DRandom.dll"))
            //{
            //    MessageBox.Show(_rm.GetString("dllIsMissing"));
            //    Application.Exit();
            //}

            var t = new Thread(SplashScreen);

            t.Start();

            Application.SetCompatibleTextRenderingDefault(false);
            t.Join();
            t.Abort();
            Application.Run(new MainForm());
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            new SendBugReportForm(e.Exception).ShowDialog();
            Application.Exit();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            new SendBugReportForm((Exception)e.ExceptionObject).ShowDialog();
            Application.Exit();
        }

        private static void SplashScreen()
        {
            Application.Run(new SplashScreen());

        }
    }
}