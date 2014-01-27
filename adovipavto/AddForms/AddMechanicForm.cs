using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adovipavto.AddForms
{
    public partial class AddMechanicForm : Form
    {
        public AddMechanicForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);

            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {

                Program.VipAvtoDataSet.AddMechanic(nameTxtBx.Text, lnTxtBx.Text, fnTxtBx.Text);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Неверно введены данные");
            }

        }

        private void nameTxtBx_Validated(object sender, EventArgs e)
        {
            if(((TextBox)sender).Text == "")
                errorProvider1.SetError((TextBox)sender, "EmptyField");
        }

        private void fnTxtBx_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != "")
                errorProvider1.SetError(((TextBox)sender), null);
        }

        private void AddMechanicForm_Load(object sender, EventArgs e)
        {
        }
    }
}
