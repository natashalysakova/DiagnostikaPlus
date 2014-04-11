using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.AddForms;
using adovipavto.Classes;
using adovipavto.Enums;


namespace adovipavto
{
    public partial class MainForm : Form
    {
        readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        private VipAvtoSet _set;

        public MainForm(VipAvtoSet set)
        {
            _set = set;
            Application.EnableVisualStyles();

            if (Settings.Instance.Language == "")
            {
                new SelectLanguage().ShowDialog();
            }
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            do
            {
                if (new Auth(_set).ShowDialog() == DialogResult.Cancel)
                {
                    Close();
                    return;
                }
            } while (!SetUserRights());

            dataGridView1.DataSource = _set.Tables[Constants.ProtocolsTableName];
            if (dataGridView1.Columns["Date"] != null)
                dataGridView1.Sort(dataGridView1.Columns["Date"], ListSortDirection.Descending);

            UpdateRows();
        }

        private bool SetUserRights()
        {
            Rights right = _set.GetOperatorRight();
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
            if (new AddProtocol(this, _set).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (new GroupsForm(_set).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            new NormativesForm(_set).Show();
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
            if (new OperatorsForm(_set).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (new Mechanics(_set).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (new SettingForm().ShowDialog() == DialogResult.OK)
            {
                if (Settings.Instance.TmpLanguage != Settings.Instance.Language)
                {
                    if (MessageBox.Show(_rm.GetString("lng"), _rm.GetString("warning"), MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                }
            }
        }

        public void UpdateRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["Group"].Value =
                    _set.GroupTitle((int) row.Cells["iDGroupDataGridViewTextBoxColumn"].Value);


                row.Cells["Mechanic"].Value =
                    _set.GetShortMechanicName(
                        (int) row.Cells["iDMechanicDataGridViewTextBoxColumn"].Value);


                row.Cells["Operator"].Value =
                    _set.GetShortOperatorName(
                        (int) row.Cells["iDOperatorDataGridViewTextBoxColumn"].Value);

                if ((bool) row.Cells["Result"].Value)
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
            new Report(_set).ShowDialog();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;


                var newProtocolId =
                    (int) dataGridView1.SelectedRows[0].Cells["protocolIDDataGridViewTextBoxColumn"].Value;
                VipAvtoSet.ProtocolsRow protocol = (VipAvtoSet.ProtocolsRow) _set.GetRowById(Constants.ProtocolsTableName, newProtocolId);
                VipAvtoSet.MesuresRow[] mesures = protocol.GetMesuresRows();

                new ProtocolReportForm(protocol, mesures, _set).ShowDialog();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Instance.Language = Settings.Instance.TmpLanguage;
            Settings.Instance.Save();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
            {
                Rectangle r = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                dataGridView1.Rows[e.RowIndex].Selected = true;

                contextMenuStrip1.Show((Control) sender, r.Left + e.X, r.Top + e.Y);
            }
        }

        private void просмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newProtocolId = (int) dataGridView1.SelectedRows[0].Cells["protocolIDDataGridViewTextBoxColumn"].Value;
            VipAvtoSet.ProtocolsRow protocol = (VipAvtoSet.ProtocolsRow) _set.GetRowById(Constants.ProtocolsTableName, newProtocolId);
            VipAvtoSet.MesuresRow[] mesures = protocol.GetMesuresRows();

            new ProtocolReportForm(protocol, mesures, _set).ShowDialog();
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newProtocolId = (int) dataGridView1.SelectedRows[0].Cells["protocolIDDataGridViewTextBoxColumn"].Value;
            VipAvtoSet.ProtocolsRow protocol = (VipAvtoSet.ProtocolsRow)_set.GetRowById(Constants.ProtocolsTableName, newProtocolId);
            VipAvtoSet.MesuresRow[] mesures = protocol.GetMesuresRows();

            new ProtocolReportForm(protocol, mesures,_set, true ).ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            do
            {
                if (new Auth(_set).ShowDialog() == DialogResult.Cancel)
                {
                    Close();
                    return;
                }
            } while (!SetUserRights());
            UpdateRows();
        }

        private void srchBtn_Click(object sender, EventArgs e)
        {
            new Search(_set).ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = _set.UpdateProtocolsAndMesures();
            UpdateRows();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = _set.UpdateProtocolsAndMesures();
            UpdateRows();
            //timer1.Start();
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            //timer1.Stop();
        }
    }
}