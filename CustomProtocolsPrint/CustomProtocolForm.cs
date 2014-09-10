using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using adovipavto.Enums;
using CustomProtocolsPrint.Properties;

namespace CustomProtocolsPrint
{
    public partial class CustomProtocolForm : Form
    {
        private PrintReserveProtocolDocument _document = new PrintReserveProtocolDocument();

        public CustomProtocolForm()
        {
            InitializeComponent();
            //printPreviewControl1.MouseWheel += printPreviewControl1_MouseWheel;
            //printPreviewControl1.Document = _document;
        }

        private void CustomProtocolForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetValues(typeof(Category));
            comboBox1.SelectedIndex = 0;

            textBox9.Text = DateTime.Now.Year.ToString().Substring(2, 2);

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = sender as TextBox;
                if (txt.Text != String.Empty)
                {
                    txt.BackColor = Color.LightGreen;
                }
            }

            if (sender is MaskedTextBox)
            {
                MaskedTextBox txt = sender as MaskedTextBox;
                if (txt.Text.Length > 0 && Char.IsLetter(txt.Text[0]))
                {
                    txt.Mask = "LL00LL";
                }
                else if (txt.Text.Length > 0 && Char.IsDigit(txt.Text[0]))
                {
                    txt.Mask = "000-00LL";
                }
            }

            CreateProtocol();
        }

        private Protocol _protocol = new Protocol();

        private void CreateProtocol()
        {
            _protocol.BlankNumber = label2.Text + textBox1.Text + label3.Text + textBox9.Text;
            Category category = new Category();
            if (comboBox1.SelectedItem != null)
            {
                Category.TryParse(comboBox1.SelectedItem.ToString(), out category);
            }
            _protocol.Category = category;
            _protocol.DateTime = dateTimePicker1.Value;
            _protocol.DocNumber = richTextBox2.Text;
            _protocol.EcoLevel = textBox7.Text;
            _protocol.GosNumber = textBox5.Text;
            _protocol.Model = textBox4.Text;
            _protocol.NextDate = dateTimePicker2.Value;
            _protocol.Pereoborudovanie = textBox6.Text;
            _protocol.VIN = textBox3.Text;

            _document.Protocol = _protocol;


            Bitmap b = new Bitmap(808, 1152);
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(Properties.Resources.OBSZ, 0, 0, 808, 1152);

            int l = 10;

            Font font = new Font(FontFamily.GenericMonospace, 15, FontStyle.Regular);


            g.DrawString(_protocol.BlankNumber, font, Brushes.Black, 335, 192);
            DrawMultilineString(_protocol.DocNumber, 270, 67 * l, g, font);
            g.DrawString(_protocol.EcoLevel, font, Brushes.Black, 270, 85.5f * l);
            g.DrawString(_protocol.GosNumber, font, Brushes.Black, 270, 58 * l);
            DrawMultilineString(_protocol.Category.ToString() + ", " + _protocol.Model, 270, 49 * l, g, font);
            DrawMultilineString(_protocol.Pereoborudovanie, 270, 78 * l, g, font);

            g.DrawString(_protocol.VIN, font, Brushes.Black, 270, 44 * l);
            g.DrawString(_protocol.DateTime.ToShortDateString(), font, Brushes.Black, 270, 62 * l);
            g.DrawString(_protocol.NextDate.ToShortDateString(), font, Brushes.Black, 605, 92 * l);

            g.DrawString(DateTime.Now.ToShortDateString(), font, Brushes.Black, 270, 24 * l);
            DrawMultilineString(richTextBox1.Text, 270, 28 * l, g, font);
            DrawMultilineString(textBox2.Text, 270, 36 * l, g, font);

            //g.DrawString(richTextBox1.Text, font, Brushes.Black, new RectangleF(270,28*l,500, 60), new StringFormat(){ LineAlignment = StringAlignment.Near });
            //g.DrawString(textBox2.Text, font, Brushes.Black, 270, 36*l);

            g.DrawString("Лисаков В.А.", font, Brushes.Black, 550, 105 * l);




            pictureBox1.Image = b;
        }

        private void DrawMultilineString(string Text, float x, float y, Graphics g, Font font)
        {
            string txt = Text;
            SizeF fit = new SizeF(470, font.Height);
            StringFormat fmt = StringFormat.GenericTypographic;
            int spacing = (int)(1.45 * font.Height);
            int line = 0;
            for (int ix = 0; ix < txt.Length; )
            {
                int chars, lines;
                g.MeasureString(txt.Substring(ix), font, fit, fmt, out chars, out lines);
                g.DrawString(txt.Substring(ix, chars), font, Brushes.Black, x, y + (line * spacing));
                ++line;
                ix += chars;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateProtocol();
        }
    }
}
