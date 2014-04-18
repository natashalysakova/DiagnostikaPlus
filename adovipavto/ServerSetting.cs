using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using adovipavto.Classes;

namespace adovipavto
{
    public partial class ServerSetting : Form
    {
        private readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());
        FileInfo file = null;

        public ServerSetting()
        {
            Application.EnableVisualStyles();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);
            InitializeComponent();


            foreach (string var in Directory.GetFiles(Application.StartupPath))
            {
                if (var.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last() == "config")
                {
                    file = new FileInfo(var);
                    break;
                }
            }

            if (file == null)
            {
                MessageBox.Show("Файл конфигурации не найден. Обратитесь к системномму администратору!",
                    _rm.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void ServerSetting_Load(object sender, EventArgs e)
        {
            label1.Text = _rm.GetString("Server");
            label3.Text = _rm.GetString("Database");


            XmlDocument doc = new XmlDocument();
            doc.Load(file.FullName);

            XmlNode configuration = doc.ChildNodes[1]; // configuration
            XmlNode connectionString = configuration.ChildNodes[1];

            var v1 = connectionString.ChildNodes[0];

            if (v1 != null)
            {
                string connstr = v1.Attributes["connectionString"].InnerText;
                var v2 = connstr.Split(new char[] { '=', ';' }, StringSplitOptions.RemoveEmptyEntries);
                textBox1.Text = v2[1];
                textBox5.Text = v2[3];
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file.FullName);

            XmlNode configuration = doc.ChildNodes[1]; // configuration
            XmlNode connectionString = configuration.ChildNodes[1];

            var v1 = connectionString.ChildNodes[0];

            if (v1 != null)
            {
                string newstring = "Data Source="+ textBox1.Text + ";Initial Catalog=" + textBox5.Text + ";Integrated Security=True";
                v1.Attributes["connectionString"].InnerText = newstring;
            }

            doc.Save(file.FullName);
            Application.Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}