using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Properties;

namespace adovipavto
{
    public partial class ServerSetting : Form
    {
        private readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());

        public ServerSetting()
        {
            Application.EnableVisualStyles();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);
            InitializeComponent();
        }

        private void ServerSetting_Load(object sender, EventArgs e)
        {
            label1.Text = _rm.GetString("Server");
            label3.Text = _rm.GetString("Database");


            string userDocFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = userDocFolder + "\\" + "DiagnostikaPlus";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string connectionString;
            string filePath = folderPath + "\\" + "settings.ini";

            using (var sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
            {
                connectionString = sr.ReadLine();
            }


            if (connectionString != null)
            {
                var splitString = connectionString.Split(new[] { ';', '=' });
                textBox1.Text = splitString[1];
                textBox5.Text = splitString[3];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +
              "DiagnostikaPlus\\settings.ini";
            using (var sw = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)))
            {

                sw.WriteLine("Data Source=" + textBox1.Text + ";Initial Catalog=" + textBox5.Text + ";Persist Security Info=True;User ID=vipavto;Password=9194");
            }
            Application.Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +
                          "DiagnostikaPlus\\settings.ini";
            if (File.Exists(path))
                Process.Start(path);
        }
    }
}