using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto.EditForms
{
    public partial class EditOperatorForm : Form
    {
        private readonly DataRow _selected;



        public EditOperatorForm(DataRow selected)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            _selected = selected;
            InitializeComponent();
        }

        private void AddOperatorForm_Load(object sender, EventArgs e)
        {
            nameTxtBx.Text = _selected["Name"].ToString();
            lnTxtBx.Text = _selected["LastName"].ToString();
            loginTxtBx.Text = _selected["Login"].ToString();
            passTxtBx.Text = _selected["Password"].ToString();

            ValidateChildren();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(nameTxtBx) == "" && errorProvider1.GetError(lnTxtBx) == "" &&
                errorProvider1.GetError(loginTxtBx) == "" && errorProvider1.GetError(passTxtBx) == "")
            {
                Program.VipAvtoDataSet.EditOperator((int) _selected["OperatorId"], nameTxtBx.Text, lnTxtBx.Text,
                    loginTxtBx.Text, passTxtBx.Text);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(StringResource.wrongData, StringResource.error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void nameTxtBx_TextChanged(object sender, EventArgs e)
        {
            ValidateChildren();
        }

        private void nameTxtBx_Validated(object sender, EventArgs e)
        {
            if (((TextBox) sender).Text == "")
                errorProvider1.SetError(((TextBox) sender), StringResource.wrongData);
            else
                errorProvider1.SetError(((TextBox) sender), null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}