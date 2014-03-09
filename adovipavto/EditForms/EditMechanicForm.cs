using System;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Enums;

namespace adovipavto.EditForms
{
    public partial class EditMechanicForm : Form
    {
        private readonly VipAvtoSet.MechanicsRow _selected;
        private readonly VipAvtoSet _set;

        public EditMechanicForm(VipAvtoSet.MechanicsRow row, VipAvtoSet set)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);


            InitializeComponent();
            _selected = row;
            _set = set;
        }

        private void EditMechanicForm_Load(object sender, EventArgs e)
        {
            nameTxtBx.Text = _selected.Name;
            lnTxtBx.Text = _selected.LastName;
            fnTxtBx.Text = _selected.FatherName;

            Text = lnTxtBx.Text + @" " + nameTxtBx.Text[0] + @"." + fnTxtBx.Text[0] + @". - " +
                   Constants.GetEnumDescription((State) (int) _selected.State);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                _set.EditMechanic((int) _selected.MechanicID, nameTxtBx.Text, lnTxtBx.Text, fnTxtBx.Text);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(
                    new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly()).GetString(
                        "wrongData"));
            }
        }

        private void nameTxtBx_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox) sender).Text != "")
                errorProvider1.SetError(((TextBox) sender), null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}