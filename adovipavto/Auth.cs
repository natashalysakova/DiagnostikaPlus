using System;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using adovipavto.NewVipAvtoSetTableAdapters;

namespace adovipavto
{
    public partial class Auth : Form
    {
        private readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());

        private readonly NewVipAvtoSet _set;
        private readonly OperatorsTableAdapter adapter = new OperatorsTableAdapter();

        public Auth(NewVipAvtoSet set)
        {
            InitializeComponent();
            _set = set;
            adapter.Fill(_set.Operators);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = _set.GetUserPasswors(textBox1.Text);

            if (password != "")
            {
                if (NewVipAvtoSet.GetHash(maskedTextBox1.Text) == password)
                {
                    _set.SetCurrentOperator(textBox1.Text);

                    timer1.Enabled = false;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(_rm.GetString("wrongPassword"), _rm.GetString("error"), MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(_rm.GetString("isUser") + @" """ + textBox1.Text + @""" " + _rm.GetString("notFound"),
                    _rm.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void maskedTextBox1_Enter(object sender, EventArgs e)
        {
            maskedTextBox1.SelectAll();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }
    }
}