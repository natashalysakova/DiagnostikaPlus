using System;
using System.Data;
using System.Drawing.Printing;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto
{
    public partial class Report : Form
    {
        private readonly NewVipAvtoSet _set;
        private PrintDocument _document;

        public Report(NewVipAvtoSet set)
        {
            _set = set;
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            printPreviewControl1.MouseWheel += printPreviewControl1_MouseWheel;
            radioButton1.Checked = true;
        }

        private void printPreviewControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                if (((PrintPreviewControl) sender).Zoom < 2.85)
                    ((PrintPreviewControl) sender).Zoom += 0.15;
                else
                {
                    ((PrintPreviewControl) sender).Zoom = 3;
                }
            else
            {
                if (((PrintPreviewControl) sender).Zoom > 0.15)
                    ((PrintPreviewControl) sender).Zoom -= 0.15;
                else
                {
                    ((PrintPreviewControl) sender).Zoom = 0.1;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _document.Print();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printDialog1.Document = _document;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                _document.Print();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            DisableDatetimePickers();
            firstDate.Value = DateTime.Today;
            secondDate.Value = DateTime.Now;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DisableDatetimePickers();
            firstDate.Value = DateTime.Now.AddDays(-7);
            secondDate.Value = DateTime.Now;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            DisableDatetimePickers();
            firstDate.Value = DateTime.Now.AddMonths(-1);
            secondDate.Value = DateTime.Now;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            DisableDatetimePickers();
            firstDate.Value = DateTime.Now.AddYears(-1);
            secondDate.Value = DateTime.Now;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            firstDate.Enabled = true;
            secondDate.Enabled = true;
            label1.Enabled = true;
            label2.Enabled = true;
        }

        private void DisableDatetimePickers()
        {
            firstDate.Enabled = false;
            secondDate.Enabled = false;
            label1.Enabled = false;
            label2.Enabled = false;
        }

        private void firstDate_ValueChanged(object sender, EventArgs e)
        {
            NewVipAvtoSet.ProtocolsRow[] rows = _set.GetProtocolsBetweenDates(firstDate.Value, secondDate.Value);

            _document = new PrintProtocolDocument(rows, firstDate.Value, secondDate.Value, _set);
            printPreviewControl1.Document = _document;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            printPreviewControl1.StartPage++;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (printPreviewControl1.StartPage > 0)
                printPreviewControl1.StartPage--;
        }

        private void printPreviewControl1_Click(object sender, EventArgs e)
        {
            ((PrintPreviewControl) sender).Focus();
        }
    }
}