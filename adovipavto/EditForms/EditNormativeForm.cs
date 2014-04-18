using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto.EditForms
{
    public partial class EditNormativeForm : Form
    {
        private readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());

        private readonly NewVipAvtoSet.NormativesRow _selected;
        private readonly NewVipAvtoSet _set;

        public EditNormativeForm(NewVipAvtoSet.NormativesRow selected, NewVipAvtoSet set)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            InitializeComponent();
            _selected = selected;
            _set = set;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(minTextBox) == "" & errorProvider1.GetError(maxTextBox) == "")
            {
                _set.EditNormative(_selected.IdNormative, groupTextBox.Text,
                    mesureTextBox.Text, Convert.ToDouble(minTextBox.Text), Convert.ToDouble(maxTextBox.Text));
                DialogResult = DialogResult.OK;
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            try
            {
                double min = Convert.ToDouble(minTextBox.Text);
                double max = Convert.ToDouble(maxTextBox.Text);

                if (min < max)
                {
                    errorProvider1.SetError(minTextBox, null);
                    errorProvider1.SetError(maxTextBox, null);
                }
                else
                {
                    errorProvider1.SetError(minTextBox, _rm.GetString("minmax"));
                    errorProvider1.SetError(maxTextBox, _rm.GetString("minmax"));
                }
            }
            catch (Exception)
            {
                errorProvider1.SetError(((TextBox) sender), _rm.GetString("wrongData"));
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Validate();
        }

        private void EditNormativeForm_Load(object sender, EventArgs e)
        {
            int id = _selected.IdNormative;

            foreach (NewVipAvtoSet.NormativesRow item in _set.Normatives.Rows)
            {
                if (item.IdNormative == id)
                {
                    minTextBox.Text = item.MinValue.ToString();
                    maxTextBox.Text = item.MaxValue.ToString();
                }
            }

            mesureTextBox.Text = new Normatives()[_selected.Tag];

            groupTextBox.Text = _set.GetGroupTitle(_selected.GroupId);
        }
    }
}