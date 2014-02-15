using System;
using System.Configuration;
using System.Windows.Forms;
using adovipavto.Classes;

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
                Settings.Instance.Language = "ru-RU";
            else
            {
                Settings.Instance.Language = "uk-UA";
            }
            Settings.Instance.Save();

            DialogResult = DialogResult.OK;
        }
    }
}