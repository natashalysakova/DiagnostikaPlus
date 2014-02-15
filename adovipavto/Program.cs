using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto
{
    internal static class Program
    {
        public static VipAvtoSet VipAvtoDataSet;
        //public static Dictionary<Normatives, string> NormasTitles;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Settings.Instance.Load();

            var t = new Thread(SplashScreen);

            t.Start();

            VipAvtoDataSet = new VipAvtoSet();
            VipAvtoDataSet.LoadData();


            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            t.Join();
            t.Abort();

            Application.Run(new MainForm());
        }

        private static void SplashScreen()
        {
            Application.Run(new SplashScreen());
        }

    }
}