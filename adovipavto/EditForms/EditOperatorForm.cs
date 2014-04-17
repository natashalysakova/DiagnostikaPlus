using System;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto.EditForms
{
    public partial class EditOperatorForm : Form
    {
        private readonly NewVipAvtoSet.OperatorsRow _selected;
        private readonly NewVipAvtoSet _set;
        readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());



        public EditOperatorForm(NewVipAvtoSet.OperatorsRow selected, NewVipAvtoSet set)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            _selected = selected;
            _set = set;
            InitializeComponent();
        }

        private void AddOperatorForm_Load(object sender, EventArgs e)
        {
            nameTxtBx.Text = _selected.Name.ToString();
            lnTxtBx.Text = _selected.LastName.ToString();
            loginTxtBx.Text = _selected.Login.ToString();
            passTxtBx.Text = @"********";

            ValidateChildren();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(nameTxtBx) == "" && errorProvider1.GetError(lnTxtBx) == "" &&
                errorProvider1.GetError(loginTxtBx) == "" && errorProvider1.GetError(passTxtBx) == "")
            {
                _set.EditOperator(_selected.IdOperator, nameTxtBx.Text, lnTxtBx.Text,
                    loginTxtBx.Text, passTxtBx.Text);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(_rm.GetString("wrongData"), _rm.GetString("error"), MessageBoxButtons.OK,
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
                errorProvider1.SetError(((TextBox)sender), _rm.GetString("wrongData"));
            else
                errorProvider1.SetError(((TextBox) sender), null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}