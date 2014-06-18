using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.AddForms;
using adovipavto.Classes;
using adovipavto.Enums;
using adovipavto.VipAvtoDBDataSetTableAdapters;
using adovipavto.Properties;

namespace adovipavto
{
    public partial class MainForm : Form
    {
        private readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());

        private int _selectedRow;


        public MainForm()
        {
            Application.EnableVisualStyles();

            if (Settings.Default.Language == "")
            {
                new SelectLanguage().ShowDialog();
            }
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                VipAvtoDBDataSet.LoadData();

            }
            catch
            {
                if (new ServerSetting().ShowDialog() == DialogResult.OK)
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
            do
            {
                if (new Auth(VipAvtoDBDataSet).ShowDialog() == DialogResult.Cancel)
                {
                    Close();
                    return;
                }
            } while (!SetUserRights());

            dataGridView1.DataSource = VipAvtoDBDataSet.Protocols;

            if (dateDataGridViewTextBoxColumn != null)
                dataGridView1.Sort(dateDataGridViewTextBoxColumn, ListSortDirection.Descending);

            timer1.Start();
        }

        private bool SetUserRights()
        {
            Rights right = VipAvtoDBDataSet.GetOperatorRight();
            if (right == Rights.Administrator)
            {
                toolStripSeparator3.Enabled = true;
                toolStripButton11.Enabled = true;
                toolStripButton12.Enabled = true;
                toolStripButton13.Enabled = true;
                toolStripButton14.Enabled = true;
                dataGridView1.Enabled = false;


                toolStripButton10.Enabled = false;
                cpyBtn.Enabled = false;
                srchBtn.Enabled = false;
                return true;
            }
            if (right == Rights.Operator)
            {
                toolStripSeparator3.Enabled = false;
                toolStripButton11.Enabled = false;
                toolStripButton12.Enabled = false;
                toolStripButton13.Enabled = false;
                toolStripButton14.Enabled = false;
                dataGridView1.Enabled = true;

                toolStripButton10.Enabled = true;
                cpyBtn.Enabled = true;
                srchBtn.Enabled = true;


                return true;
            }

            MessageBox.Show(_rm.GetString("noPermission"));
            return false;
        }


        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            new AddProtocol(VipAvtoDBDataSet).ShowDialog();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            new GroupsForm().ShowDialog();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            new NormativesForm().Show();
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            new Help().Show();
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
            new OperatorsForm().ShowDialog();
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            new Mechanics().ShowDialog();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (new SettingForm().ShowDialog() == DialogResult.OK)
            {
                if (Settings.Default.TmpLanguage != Settings.Default.Language)
                {
                    if (MessageBox.Show(_rm.GetString("lng"), _rm.GetString("warning"), MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                }
            }
        }


        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
        }

        private void cpyBtn_Click(object sender, EventArgs e)
        {
            new Report(VipAvtoDBDataSet).ShowDialog();
        }


        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;


                var newProtocolId =
                    (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                var protocol =
                    (VipAvtoDBDataSet.ProtocolsRow)VipAvtoDBDataSet.GetRowById(Constants.ProtocolsTableName, newProtocolId);
                VipAvtoDBDataSet.MesuresRow[] mesures = protocol.GetMesuresRows();

                new ProtocolReportForm(protocol, mesures, VipAvtoDBDataSet).ShowDialog();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Language = Settings.Default.TmpLanguage;
            Settings.Default.Save();
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
            var newProtocolId = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            var protocol =
                (VipAvtoDBDataSet.ProtocolsRow)VipAvtoDBDataSet.GetRowById(Constants.ProtocolsTableName, newProtocolId);
            VipAvtoDBDataSet.MesuresRow[] mesures = protocol.GetMesuresRows();

            new ProtocolReportForm(protocol, mesures, VipAvtoDBDataSet).ShowDialog();
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newProtocolId = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            var protocol =
                (VipAvtoDBDataSet.ProtocolsRow)VipAvtoDBDataSet.GetRowById(Constants.ProtocolsTableName, newProtocolId);
            VipAvtoDBDataSet.MesuresRow[] mesures = protocol.GetMesuresRows();

            new ProtocolReportForm(protocol, mesures, VipAvtoDBDataSet, true).ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            do
            {
                if (new Auth(VipAvtoDBDataSet).ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
            } while (!SetUserRights());
        }

        private void srchBtn_Click(object sender, EventArgs e)
        {
            new Search(VipAvtoDBDataSet).ShowDialog();
        }


        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] == groupIdDataGridViewTextBoxColumn)
            {
                if (e.Value.ToString() != String.Empty)
                {
                    e.Value = VipAvtoDBDataSet.GetGroupTitle((int)e.Value);
                }
                else
                {
                    e.Value = "NULL";
                }

            }
            else if (dataGridView1.Columns[e.ColumnIndex] == resultDataGridViewCheckBoxColumn)
            {
                if (e.Value.ToString() != String.Empty)
                {
                    if ((bool)e.Value)
                    {
                        e.Value = _rm.GetString("check");
                    }
                    else
                    {
                        e.Value = _rm.GetString("uncheck");
                    }
                }
                else
                {
                    e.Value = "NULL";
                }

            }
            else if (dataGridView1.Columns[e.ColumnIndex] == operatorIdDataGridViewTextBoxColumn)
            {
                if (e.Value.ToString() != String.Empty)
                {
                    e.Value = VipAvtoDBDataSet.GetShortOperatorName((int)e.Value);
                }
                else
                {
                    e.Value = "NULL";
                }

            }
            else if (dataGridView1.Columns[e.ColumnIndex] == mechanicIdDataGridViewTextBoxColumn)
            {

                if (e.Value.ToString() != String.Empty)
                {
                    try
                    {
                        e.Value = VipAvtoDBDataSet.GetShortMechanicName((int)e.Value);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                }
                else
                {
                    e.Value = "NULL";
                }
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if ((bool)dataGridView1[resultDataGridViewCheckBoxColumn.Index, e.RowIndex].Value)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            ProtocolsCountTableAdapter q = new ProtocolsCountTableAdapter();
            int count = q.GetData()[0].Column1;
            toolStripStatusLabel2.Text = count.ToString();

            if (count != VipAvtoDBDataSet.Protocols.Count)
            {
                int saveRow = 0;
                if (dataGridView1.Rows.Count > 0)
                    saveRow = dataGridView1.FirstDisplayedCell.RowIndex;

                if (dataGridView1.SelectedRows.Count != 0)
                    _selectedRow = (int)dataGridView1.SelectedRows[0].Cells[0].Value;



                VipAvtoDBDataSet.Mesures.Clear();
                var adapter = new MesuresTableAdapter();
                protocolsTableAdapter.Fill(VipAvtoDBDataSet.Protocols);
                adapter.Fill(VipAvtoDBDataSet.Mesures);

                int[] t =
    (from DataGridViewRow row in dataGridView1.Rows
     where (int)row.Cells[0].Value == _selectedRow
     select row.Index
        ).ToArray();
                if (t.Length != 0)
                    dataGridView1.Rows[t[0]].Selected = true;


                if (saveRow != 0 && saveRow < dataGridView1.Rows.Count)
                    dataGridView1.FirstDisplayedScrollingRowIndex = saveRow;

            }


        }

    }
}