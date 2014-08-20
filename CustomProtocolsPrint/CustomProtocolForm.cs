using System;
using System.Windows.Forms;
using adovipavto.Enums;
using CustomProtocolsPrint.Properties;

namespace CustomProtocolsPrint
{
    public partial class CustomProtocolForm : Form
    {
        public CustomProtocolForm()
        {
            InitializeComponent();
        }

        private void CustomProtocolForm_Load(object sender, EventArgs e)
        {
            textBox9.Text = DateTime.Now.Year.ToString().Substring(2, 2);
            textBox8.Text = DateTime.Today.AddYears(1).ToShortDateString();

            comboBox1.DataSource = Enum.GetValues(typeof(Category));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Enabled = !richTextBox1.Enabled;
            richTextBox1.ReadOnly = !richTextBox1.ReadOnly;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = !textBox2.Enabled;
            textBox2.ReadOnly = !textBox2.ReadOnly;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox9.Enabled = !textBox9.Enabled;
            textBox9.ReadOnly = !textBox9.ReadOnly;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = Resources.UnKnown;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox6.Text = Resources.UnKnown;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox7.Text = Resources.UnKnown;
        }
    }
}
