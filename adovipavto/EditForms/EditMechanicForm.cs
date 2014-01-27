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
using adovipavto.Classes;
using adovipavto.Enums;

namespace adovipavto.EditForms
{
    public partial class EditMechanicForm : Form
    {
        private DataRow selected;
        public EditMechanicForm(DataRow row)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);


            InitializeComponent();
            selected = row;
        }

        private void EditMechanicForm_Load(object sender, EventArgs e)
        {
            nameTxtBx.Text = selected["Name"].ToString();
            lnTxtBx.Text = selected["LastName"].ToString();
            fnTxtBx.Text = selected["FatherName"].ToString();

            Text = lnTxtBx.Text + " " + nameTxtBx.Text[0] + "." + fnTxtBx.Text[0] + ". - " +
                   Constants.GetEnumDescription((State)(int)selected["State"]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                Program.VipAvtoDataSet.EditMechanic((int)selected["MechanicID"], nameTxtBx.Text, lnTxtBx.Text, fnTxtBx.Text);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Неверно введены данные");
            }
        }

        private void nameTxtBx_TextChanged(object sender, EventArgs e)
        {
            if(((TextBox)sender).Text != "")
                errorProvider1.SetError(((TextBox)sender), null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
