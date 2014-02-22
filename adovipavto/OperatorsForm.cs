using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.AddForms;
using adovipavto.Classes;
using adovipavto.EditForms;
using adovipavto.Enums;

namespace adovipavto
{
    public partial class OperatorsForm : Form
    {
        readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        public OperatorsForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            InitializeComponent();
            dataGridView1.DataSource = Program.VipAvtoDataSet.Tables[Constants.OperatorsTableName];
        }

        private void OperatorsForm_Load(object sender, EventArgs e)
        {
            UpdateRoles();
        }

        private void UpdateRoles()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["RightsString"].Value =
                    Constants.GetEnumDescription((Rights) row.Cells["rightDataGridViewTextBoxColumn"].Value);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (dataGridView1.SelectedRows[0] != null)
            {
                if (MessageBox.Show(_rm.GetString("lockOperator"), _rm.GetString("warning"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) ==
                    DialogResult.Yes)
                {
                    var id = (int) dataGridView1.SelectedRows[0].Cells["operatorIdDataGridViewTextBoxColumn"].Value;

                    if (AdministratorsCount() == 1 &&
                        (int) dataGridView1.SelectedRows[0].Cells["rightDataGridViewTextBoxColumn"].Value ==
                        (int) Rights.Administrator)
                    {
                        MessageBox.Show(_rm.GetString("cantLock"), _rm.GetString("error"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        Program.VipAvtoDataSet.LockOperator(id);
                        if (id == Program.VipAvtoDataSet.GetOperatorId())
                        {
                            MessageBox.Show(_rm.GetString("reboot"), _rm.GetString("warning"), MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            Application.Restart();
                        }
                        UpdateRoles();
                    }
                }
            }
        }

        private int AdministratorsCount()
        {
            return
                Program.VipAvtoDataSet.Tables[Constants.OperatorsTableName].Select(string.Format("Right = {0}",
                    (int) Rights.Administrator)).Length;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void Add()
        {
            if (new AddOperatorForm().ShowDialog() == DialogResult.OK)
                UpdateRoles();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;

            DataRow row = Program.VipAvtoDataSet.GetRowById(Constants.OperatorsTableName, id);


            if (new EditOperatorForm(row).ShowDialog() == DialogResult.OK)
                UpdateRoles();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            dataGridView1.FirstDisplayedCell = dataGridView1.CurrentCell;

            UpdateRoles();
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                Rectangle r = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                contextMenuStrip1.Show((Control) sender, r.Left + e.X, r.Top + e.Y);
            }
        }
    }
}