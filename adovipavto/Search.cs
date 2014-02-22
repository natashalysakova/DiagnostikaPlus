using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto
{
    public partial class Search : Form
    {
        private PrintProtocolDocument _document;
        private PrintProtocolDocument _document2;

        public Search()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text != @"     -")
            {
                string blank = label80.Text + maskedTextBox1.Text;


                DataRow row = Program.VipAvtoDataSet.GetProtocolByBlankId(blank);

                if (row != null)
                {
                    DataRow[] mesures = Program.VipAvtoDataSet.GetMesuresFromProtocol(row);
                    _document = new PrintProtocolDocument(row, mesures);
                    printPreviewControl1.Document = _document;

                    maskedTextBox1.BackColor = Color.LightGreen;
                    button1.Enabled = true;
                }
                else
                {
                    if (printPreviewControl1.Document != null)
                    {
                        _document = null;
                        printPreviewControl1.Document = null;
                    }
                    maskedTextBox1.BackColor = Color.LightPink;
                    button1.Enabled = false;
                }
            }
            else
            {
                maskedTextBox1.BackColor = Color.LightGoldenrodYellow;
                button1.Enabled = false;
            }
        }

        private void Search_Load(object sender, EventArgs e)
        {
            printPreviewControl1.MouseWheel += printPreviewControl1_MouseWheel;
            printPreviewControl2.MouseWheel += printPreviewControl1_MouseWheel;

            dataGridView1.DataSource = Program.VipAvtoDataSet.Tables[Constants.ProtocolsTableName].Copy();


            radioButton1.Checked = true;
            maskedTextBox1.Focus();
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

        private void printPreviewControl1_Click(object sender, EventArgs e)
        {
            ((PrintPreviewControl) sender).Focus();
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            DisableDatetimePickers();
            firstDate.Value = DateTime.Today;
            secondDate.Value = DateTime.Now;
        }

        private void firstDate_ValueChanged(object sender, EventArgs e)
        {
            ((DataTable) dataGridView1.DataSource).DefaultView.RowFilter =
                String.Format("Date > '{0}' AND Date <= '{1}'", firstDate.Value, secondDate.Value);

            var column = dataGridView1.Columns["protocolIDDataGridViewTextBoxColumn"];
            if (column != null)
                column.Visible = false;
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
        }

        private void DisableDatetimePickers()
        {
            firstDate.Enabled = false;
            secondDate.Enabled = false;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;


                var newProtocolId =
                    (int) dataGridView1.SelectedRows[0].Cells["protocolIDDataGridViewTextBoxColumn"].Value;
                DataRow protocol = Program.VipAvtoDataSet.GetRowById(Constants.ProtocolsTableName, newProtocolId);
                DataRow[] mesures = Program.VipAvtoDataSet.GetMesuresFromProtocol(protocol);

                _document2 = new PrintProtocolDocument(protocol, mesures);
                printPreviewControl2.Document = _document2;
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;


                var newProtocolId =
                    (int) dataGridView1.SelectedRows[0].Cells["protocolIDDataGridViewTextBoxColumn"].Value;
                DataRow protocol = Program.VipAvtoDataSet.GetRowById(Constants.ProtocolsTableName, newProtocolId);
                DataRow[] mesures = Program.VipAvtoDataSet.GetMesuresFromProtocol(protocol);

                new ProtocolReportForm(protocol, mesures).ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow protocol = Program.VipAvtoDataSet.GetProtocolByBlankId(label80.Text + maskedTextBox1.Text);
            DataRow[] mesures = Program.VipAvtoDataSet.GetMesuresFromProtocol(protocol);

            new ProtocolReportForm(protocol, mesures).ShowDialog();
        }
    }
}