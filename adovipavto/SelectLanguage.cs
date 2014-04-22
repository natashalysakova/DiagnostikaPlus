using System;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Properties;

namespace adovipavto
{
    public partial class SelectLanguage : Form
    {
        public SelectLanguage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                Settings.Default.Language = "ru-RU";
            else
                Settings.Default.Language = "uk-UA";

            Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }
    }
}