using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.AddForms;
using adovipavto.Classes;
using adovipavto.EditForms;
using adovipavto.Properties;

namespace adovipavto
{
    public partial class GroupsForm : Form
    {
        private readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());

        private NewVipAvtoSet.GroupsRow _selectedRow;

        public GroupsForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);

            InitializeComponent();
            dataGridView1.DataSource = newVipAvtoSet.Groups;
        }

        //readonly DataTable _table;


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                Rectangle r = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                _selectedRow =
                    (NewVipAvtoSet.GroupsRow)
                        newVipAvtoSet.GetRowById(Constants.GroupTableName,
                            (int) dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                contextMenuStrip1.Show((Control) sender, r.Left + e.X, r.Top + e.Y);
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Left)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                _selectedRow =
                    (NewVipAvtoSet.GroupsRow)
                        newVipAvtoSet.GetRowById(Constants.GroupTableName,
                            (int) dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void нормативыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRow != null)
                new NormativesForm(_selectedRow.IdGroup).ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_rm.GetString("DeleteGroup"), _rm.GetString("warning"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                newVipAvtoSet.RemoveRow(_selectedRow);
                _selectedRow = null;
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Selected = true;
            _selectedRow =
                (NewVipAvtoSet.GroupsRow)
                    newVipAvtoSet.GetRowById(Constants.GroupTableName,
                        (int) dataGridView1.SelectedRows[0].Cells[0].Value);

            if (_selectedRow != null)
                new NormativesForm(_selectedRow.IdGroup).ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new AddGroupForm(newVipAvtoSet).ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _selectedRow = (NewVipAvtoSet.GroupsRow) newVipAvtoSet.GetRowById(Constants.GroupTableName,
                (int) dataGridView1.SelectedRows[0].Cells[0].Value);

            if (_selectedRow != null)
            {
                new EditGroupForm(_selectedRow, newVipAvtoSet).ShowDialog();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _selectedRow = (NewVipAvtoSet.GroupsRow) newVipAvtoSet.GetRowById(Constants.GroupTableName,
                (int) dataGridView1.SelectedRows[0].Cells[0].Value);

            if (_selectedRow != null)
            {
                if (MessageBox.Show(_rm.GetString("DeleteGroup"), _rm.GetString("warning"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) ==
                    DialogResult.Yes)
                {
                    newVipAvtoSet.RemoveRow(_selectedRow);
                    _selectedRow = null;
                }
            }
        }

        private void GroupsForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "newVipAvtoSet.Groups". При необходимости она может быть перемещена или удалена.
            this.groupsTableAdapter.Fill(this.newVipAvtoSet.Groups);
        }
    }
}