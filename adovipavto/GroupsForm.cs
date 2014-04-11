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

namespace adovipavto
{
    public partial class GroupsForm : Form
    {
        private readonly VipAvtoSet _set;
        private VipAvtoSet.GroupRow _selectedRow;
        readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        public GroupsForm(VipAvtoSet set)
        {
            _set = set;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            InitializeComponent();
            dataGridView1.DataSource = set.Group;
        }

        //readonly DataTable _table;


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                Rectangle r = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                _selectedRow = (VipAvtoSet.GroupRow) _set.GetRowByIndex(Constants.GroupTableName, e.RowIndex);
                contextMenuStrip1.Show((Control) sender, r.Left + e.X, r.Top + e.Y);
            }
        }

        private void нормативыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NormativesForm((int) _selectedRow["GroupID"], _set).ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_rm.GetString("DeleteGroup"), _rm.GetString("warning"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _set.RemoveRow(Constants.GroupTableName, _selectedRow);
                _set.Update(Constants.GroupTableName);
                _selectedRow = null;
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Selected = true;
            _selectedRow = (VipAvtoSet.GroupRow) _set.GetRowByIndex(Constants.GroupTableName, e.RowIndex);
            new NormativesForm(_selectedRow.GroupID, _set).ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (new AddGroupForm(_set).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void UpdateRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["Title"].Value =
                    _set.GroupTitle((int) row.Cells["groupIDDataGridViewTextBoxColumn"].Value);
            }
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _selectedRow = (VipAvtoSet.GroupRow) _set.GetRowByIndex(Constants.GroupTableName,
                dataGridView1.SelectedRows[0].Index);

            if (_selectedRow != null)
            {
                if (new EditGroupForm(_selectedRow, _set).ShowDialog() == DialogResult.OK)
                    UpdateRows();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _selectedRow = (VipAvtoSet.GroupRow) _set.GetRowByIndex(Constants.GroupTableName,
                dataGridView1.SelectedRows[0].Index);

            if (_selectedRow != null)
            {
                if (MessageBox.Show(_rm.GetString("DeleteGroup"), _rm.GetString("warning"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) ==
                    DialogResult.Yes)
                {
                    _set.RemoveGroup(_selectedRow);
                    _selectedRow = null;
                    _set.Update(Constants.GroupTableName);

                    UpdateRows();
                }
            }
        }

        private void GroupsForm_Load(object sender, EventArgs e)
        {
            UpdateRows();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_rm.GetString("DeleteGroup"), _rm.GetString("warning"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) ==
                DialogResult.Yes)
            {

                _set.RemoveAllGroup();
            }
        }
    }
}