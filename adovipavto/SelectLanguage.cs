using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                Properties.Settings.Default.Language = "ru-RU";
            else
            {
                Properties.Settings.Default.Language = "uk-UA";
            }
            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }
    }
}
