using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using adovipavto.AddForms;
using adovipavto.Classes;
using System.Drawing;

namespace adovipavto
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            if (Properties.Settings.Default.Language == "")
                new SelectLanguage().ShowDialog();

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (new Auth().ShowDialog() == DialogResult.Cancel)
                Close();
            else
            {
                dataGridView1.DataSource = Program.VipAvtoDataSet.Tables[Constants.ProtocolsTableName];
                dataGridView1.Sort(dataGridView1.Columns["Date"], ListSortDirection.Descending);

                UpdateRows();

            }
        }


        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (new AddProtocol(this).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (new GroupsForm().ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            new NormativesForm().Show();
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            if (new OperatorsForm().ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (new Mechanics().ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (new SettingForm().ShowDialog() == DialogResult.OK)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
                InitializeComponent();

                dataGridView1.DataSource = Program.VipAvtoDataSet.Tables[Constants.ProtocolsTableName];
                dataGridView1.Sort(dataGridView1.Columns["Date"], ListSortDirection.Descending);

                UpdateRows();

            }
        }

        public void UpdateRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["Group"].Value =
                    Program.VipAvtoDataSet.GetRowById(Constants.GroupTableName, (int)row.Cells["iDGroupDataGridViewTextBoxColumn"].Value)["Title"];


                row.Cells["Mechanic"].Value =
                    Program.VipAvtoDataSet.GetShortMechanicName(
                        (int)row.Cells["iDMechanicDataGridViewTextBoxColumn"].Value);



                row.Cells["Operator"].Value =
                    Program.VipAvtoDataSet.GetShortOperatorName(
                        (int)row.Cells["iDOperatorDataGridViewTextBoxColumn"].Value);

                if ((bool)row.Cells["Result"].Value)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.LightPink;
                }
            }

        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            UpdateRows();
        }

        private void cpyBtn_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Selected = true;

            int newProtocolId = (int)dataGridView1.SelectedRows[0].Cells["protocolIDDataGridViewTextBoxColumn"].Value;
            DataRow protocol = Program.VipAvtoDataSet.GetRowById(Constants.ProtocolsTableName, newProtocolId);
            DataRow[] mesures = Program.VipAvtoDataSet.GetMesuresFromProtocol(newProtocolId);

            new ProtocolReportForm(protocol, mesures).ShowDialog();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.VipAvtoDataSet.OperatorExit();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
            {
                Rectangle r = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                dataGridView1.Rows[e.RowIndex].Selected = true;

                contextMenuStrip1.Show((Control)sender, r.Left + e.X, r.Top + e.Y);
            }
        }

        private void просмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int newProtocolId = (int)dataGridView1.SelectedRows[0].Cells["protocolIDDataGridViewTextBoxColumn"].Value;
            DataRow protocol = Program.VipAvtoDataSet.GetRowById(Constants.ProtocolsTableName, newProtocolId);
            DataRow[] mesures = Program.VipAvtoDataSet.GetMesuresFromProtocol(newProtocolId);

            new ProtocolReportForm(protocol, mesures).ShowDialog();

        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int newProtocolId = (int)dataGridView1.SelectedRows[0].Cells["protocolIDDataGridViewTextBoxColumn"].Value;
            DataRow protocol = Program.VipAvtoDataSet.GetRowById(Constants.ProtocolsTableName, newProtocolId);
            DataRow[] mesures = Program.VipAvtoDataSet.GetMesuresFromProtocol(newProtocolId);

            new ProtocolReportForm(protocol, mesures, true).ShowDialog();

        }
    }
}