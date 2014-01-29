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
using adovipavto.Properties;

namespace adovipavto
{
    public partial class GroupsForm : Form
    {
        private DataRow _selectedRow;

        private ResourceManager rm;

        public GroupsForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);

            rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());


            InitializeComponent();
            dataGridView1.DataSource = Program.VipAvtoDataSet.Tables[Constants.GroupTableName];
        }

        //readonly DataTable _table;


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                Rectangle r = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                _selectedRow = Program.VipAvtoDataSet.GetRowByIndex(Constants.GroupTableName, e.RowIndex);
                contextMenuStrip1.Show((Control) sender, r.Left + e.X, r.Top + e.Y);
            }
        }

        private void нормативыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NormativesForm((int)_selectedRow["GroupID"]).ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(rm.GetString("DeleteGroup"), rm.GetString("warning"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Program.VipAvtoDataSet.RemoveRow(Constants.GroupTableName, _selectedRow);
                Program.VipAvtoDataSet.Tables[Constants.GroupTableName].WriteXml(Constants.GetFullPath(Settings.Default.Groups));
                _selectedRow = null;
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Selected = true;
            _selectedRow = Program.VipAvtoDataSet.GetRowByIndex(Constants.GroupTableName, e.RowIndex);
            new NormativesForm((int)_selectedRow["GroupID"]).ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (new AddGroupForm().ShowDialog() == DialogResult.OK)
                UpdateRows();

        }

        private void UpdateRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["Title"].Value =
    Program.VipAvtoDataSet.CreateGroupTitle((int)row.Cells["groupIDDataGridViewTextBoxColumn"].Value);


            }
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _selectedRow = Program.VipAvtoDataSet.GetRowByIndex(Constants.GroupTableName,
                dataGridView1.SelectedRows[0].Index);

            if (_selectedRow != null)
            {
                if(new EditGroupForm(_selectedRow).ShowDialog() == DialogResult.OK)
                    UpdateRows();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _selectedRow = Program.VipAvtoDataSet.GetRowByIndex(Constants.GroupTableName,
                dataGridView1.SelectedRows[0].Index);

            if (_selectedRow != null)
            {
                if (MessageBox.Show(rm.GetString("DeleteGroup"), rm.GetString("warning"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) ==
                    DialogResult.Yes)
                {
                    Program.VipAvtoDataSet.RemoveRow(Constants.GroupTableName, _selectedRow);


                    Program.VipAvtoDataSet.Tables[Constants.NormativesTableName].AcceptChanges();
                    Program.VipAvtoDataSet.Tables[Constants.NormativesTableName].WriteXml(Constants.GetFullPath(Settings.Default.Normatives));
                    _selectedRow = null;

                    UpdateRows();
                }
            }
        }

        private void GroupsForm_Load(object sender, EventArgs e)
        {
            UpdateRows();
        }
    }
}