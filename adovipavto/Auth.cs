using System;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace adovipavto
{
    public partial class Auth : Form
    {
        readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        public Auth()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = Program.VipAvtoDataSet.GetUserPasswors(textBox1.Text);

            if (password != "")
            {

                if (VipAvtoSet.GetHash(maskedTextBox1.Text) == password)
                {
                    Program.VipAvtoDataSet.SetCurrentOperator(textBox1.Text);

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
    }
}