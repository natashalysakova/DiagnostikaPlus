using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto
{
    internal static class Program
    {
        static readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());


        public static VipAvtoSet VipAvtoDataSet;
        //public static Dictionary<Normatives, string> NormasTitles;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (!File.Exists("DRandom.dll"))
            {
                MessageBox.Show(_rm.GetString("dllIsMissing"));
                Application.Exit();
            }

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