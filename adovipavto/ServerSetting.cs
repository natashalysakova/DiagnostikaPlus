using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto
{
    public partial class ServerSetting : Form
    {            
        ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        public ServerSetting()
        {
            Application.EnableVisualStyles();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);
            InitializeComponent();
        }

        private void ServerSetting_Load(object sender, EventArgs e)
        {
            label1.Text = _rm.GetString("Server");
            label2.Text = _rm.GetString("Port");
            label3.Text = _rm.GetString("Database");
            label4.Text = _rm.GetString("Username");
            checkBox1.Text = _rm.GetString("Password");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ServerIp = textBox1.Text;
            Properties.Settings.Default.Port = textBox2.Text;
            Properties.Settings.Default.DataBase = textBox5.Text;
            Properties.Settings.Default.UserName = textBox3.Text;
            Properties.Settings.Default.Passwod = textBox4.Text;
            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
