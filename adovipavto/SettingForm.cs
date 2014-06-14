using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Properties;

namespace adovipavto
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);

            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                Settings.Default.TmpLanguage = "ru-RU";
            else
                Settings.Default.TmpLanguage = "uk-UA";

            Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }


        private void SettingForm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.Language == "ru-RU")
                radioButton1.Checked = true;
            else
            {
                radioButton2.Checked = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new ServerSetting().ShowDialog();
        }
    }
}