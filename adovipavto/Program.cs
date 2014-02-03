using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows;
using adovipavto.Classes;
using adovipavto.Enums;
using adovipavto.Properties;

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
            Thread t = new Thread(SplashScreen);

            t.Start();

            VipAvtoDataSet = new VipAvtoSet();
            //NormasTitles = new Dictionary<Normatives, string>();
            LoadData();


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

        private static void LoadData()
        {
            string path = Constants.GetFullPath(Settings.Default.Operators);

            if (File.Exists(path))
                VipAvtoDataSet.Tables[Constants.OperatorsTableName].ReadXml(path);
            else
                VipAvtoDataSet.CreateAdministratorUser();


            path = Constants.GetFullPath(Settings.Default.Mechanics);

            if (File.Exists(path))
                VipAvtoDataSet.Tables[Constants.MechanicsTableName].ReadXml(path);
            else
                VipAvtoDataSet.Tables[Constants.MechanicsTableName].WriteXml(path);


            path = Constants.GetFullPath(Settings.Default.Groups);

            if (File.Exists(path))
                VipAvtoDataSet.Tables[Constants.GroupTableName].ReadXml(path);
            else
                VipAvtoDataSet.Tables[Constants.GroupTableName].WriteXml(path);


            path = Constants.GetFullPath(Settings.Default.Logs);

            if (File.Exists(path))
                VipAvtoDataSet.Tables[Constants.LogsTableName].ReadXml(path);
            else
                VipAvtoDataSet.Tables[Constants.LogsTableName].WriteXml(path);


            path = Constants.GetFullPath(Settings.Default.Normatives);

            if (File.Exists(path))
                VipAvtoDataSet.Tables[Constants.NormativesTableName].ReadXml(path);
            else
                VipAvtoDataSet.Tables[Constants.NormativesTableName].WriteXml(path);


            path = Constants.GetFullPath(Settings.Default.Protocols);

            if (File.Exists(path))
                VipAvtoDataSet.Tables[Constants.ProtocolsTableName].ReadXml(path);
            else
                VipAvtoDataSet.Tables[Constants.ProtocolsTableName].WriteXml(path);


            path = Constants.GetFullPath(Settings.Default.Mesure);

            if (File.Exists(path))
                VipAvtoDataSet.Tables[Constants.MesuresTableName].ReadXml(path);
            else
                VipAvtoDataSet.Tables[Constants.MesuresTableName].WriteXml(path);


            //string[] s = Properties.Resources.mesures.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //var n = new Normatives[Enum.GetValues(typeof(Normatives)).Length];
            //Enum.GetValues(typeof(Normatives)).CopyTo(n, 0);

            //for (int i = 0; i < n.Length; i++)
            //{
            //    NormasTitles.Add(n[i], s[i]);
            //}
        }
    }
}