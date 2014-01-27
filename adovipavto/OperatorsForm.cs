using System;
using System.Data;
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

namespace adovipavto
{
    public partial class OperatorsForm : Form
    {
        ResourceManager rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        public OperatorsForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);

            InitializeComponent();
            dataGridView1.DataSource = Program.VipAvtoDataSet.Tables[Constants.OperatorsTableName];
            //dataGridView1.DataSource = (
                //from DataRow r in Program.VipAvtoDataSet.Tables[Constants.OperatorsTableName].Rows )
        }

        private void OperatorsForm_Load(object sender, System.EventArgs e)
        {
            UpdateRoles();
        }

        private void UpdateRoles()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["RightsString"].Value = Constants.GetEnumDescription((Rights)row.Cells["rightDataGridViewTextBoxColumn"].Value);
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
                if (MessageBox.Show(rm.GetString("lockOperator"), Properties.Resources.warning,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) ==
                    DialogResult.Yes)
                {
                    int id = (int) dataGridView1.SelectedRows[0].Cells["operatorIdDataGridViewTextBoxColumn"].Value;
                    if (AdministratorsCount() == 1 &&
                        (int) dataGridView1.SelectedRows[0].Cells["rightDataGridViewTextBoxColumn"].Value ==
                        (int) Rights.Administrator)
                    {
                        MessageBox.Show(rm.GetString("cantLock"), Properties.Resources.error, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        Program.VipAvtoDataSet.LockOperator(id);
                        UpdateRoles();
                    }
                }
            }
        }

        private int AdministratorsCount()
        {
            return
                Program.VipAvtoDataSet.Tables[Constants.OperatorsTableName].Select(string.Format("Right = {0}",
                    (int)Rights.Administrator)).Length;
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
            this.dataGridView1.FirstDisplayedCell = this.dataGridView1.CurrentCell;

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

                contextMenuStrip1.Show((Control)sender, r.Left + e.X, r.Top + e.Y);
            }

        }
    }
}