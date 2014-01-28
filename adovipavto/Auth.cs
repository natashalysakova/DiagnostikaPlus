using System;
using System.Reflection;
using System.Windows.Forms;
using System.Resources;

namespace adovipavto
{
    public partial class Auth : Form
    {

        private ResourceManager rm;
        public Auth()
        {
            InitializeComponent();
            rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());
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

                if (maskedTextBox1.Text == password)
                {
                    Program.VipAvtoDataSet.SetCurrentOperator(textBox1.Text);
                    timer1.Enabled = false;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(rm.GetString("wrongPassword"), rm.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(rm.GetString("isUser") + textBox1.Text + rm.GetString("notFound"), rm.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}