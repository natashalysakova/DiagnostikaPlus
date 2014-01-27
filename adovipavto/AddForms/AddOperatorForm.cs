using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Enums;

namespace adovipavto.AddForms
{
    public partial class AddOperatorForm : Form
    {
        public AddOperatorForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
        }

        private void AddOperatorForm_Load(object sender, EventArgs e)
        {
            List<string> arr = new List<string>();
            foreach (Rights item in Enum.GetValues(typeof (Rights)))
            {
                arr.Add(Constants.GetEnumDescription(item));
            }

            roleCmbBx.DataSource = arr;
            roleCmbBx.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(nameTxtBx) == "" && errorProvider1.GetError(lnTxtBx) == "" && errorProvider1.GetError(loginTxtBx) == "" && errorProvider1.GetError(passTxtBx) == "")
            {

                Program.VipAvtoDataSet.AddOperator(nameTxtBx.Text, lnTxtBx.Text, loginTxtBx.Text, passTxtBx.Text,
                    roleCmbBx.SelectedItem.ToString());
                DialogResult = DialogResult.OK;
                ;
            }
            else
            {
                MessageBox.Show("Неверно введены данные");
            }

        }

        private void nameTxtBx_TextChanged(object sender, EventArgs e)
        {
            Validate();
        }

        private void nameTxtBx_Validated(object sender, EventArgs e)
        {
            if (((TextBox) sender).Text == "")
                errorProvider1.SetError(((TextBox) sender), "try again");
            else
                errorProvider1.SetError(((TextBox) sender), null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            
        }
    }
}
