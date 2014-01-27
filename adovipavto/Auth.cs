using System;
using System.Windows.Forms;

namespace adovipavto
{
    public partial class Auth : Form
    {
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

                if (maskedTextBox1.Text == password)
                {
                    Program.VipAvtoDataSet.SetCurrentOperator(textBox1.Text);
                    timer1.Enabled = false;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(@"Неверный пароль");
                }
            }
            else
            {
                MessageBox.Show(@"Пользователь " + textBox1.Text + @" не найден");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}