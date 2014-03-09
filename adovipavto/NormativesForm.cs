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
using adovipavto.EditForms;

namespace adovipavto
{
    public partial class NormativesForm : Form
    {
        readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        private VipAvtoSet _set;

        public NormativesForm(VipAvtoSet set) : this(0, set)
        {
        }

        public NormativesForm(int selectedGroup, VipAvtoSet set)
        {

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            _set = set;

            InitializeComponent();

            dataGridView1.DataSource = set.Normatives;

// ReSharper disable once CoVariantArrayConversion
            object[] groups =
                (from VipAvtoSet.GroupRow item in set.Group
                    select set.GroupTitle((int) item.GroupID)).ToArray();

            groupSelector.Items.AddRange(groups);

            if (set.GroupTitle(selectedGroup) != "" || groupSelector.Items.Count == 0)
                groupSelector.Text = set.GroupTitle(selectedGroup);
            else
            {
                groupSelector.SelectedIndex = 0;
            }
        }

        private void NormativesForm_Load(object sender, EventArgs e)
        {
            int id = _set.GetGroupId(groupSelector.Text);
            ((DataTable) dataGridView1.DataSource).DefaultView.RowFilter = string.Format("IDGroup = '{0}'", id);
            dataGridView1.Columns[0].Visible = false;
            UpdateRows();
        }


        private void groupSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = _set.GetGroupId(groupSelector.Text);
            ((DataTable) dataGridView1.DataSource).DefaultView.RowFilter = string.Format("IDGroup = '{0}'", id);
            UpdateRows();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (new AddNormativeForm(_set).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void UpdateRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["NormTitle"].Value = new Normatives()[(int) row.Cells["Tag"].Value];
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_rm.GetString("deleteNorm"), _rm.GetString("warning"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;
                _set.RemoveRowById(Constants.NormativesTableName, id);
                UpdateRows();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;

            VipAvtoSet.NormativesRow row = (VipAvtoSet.NormativesRow)_set.GetRowById(Constants.NormativesTableName, id);
            if (new EditNormativeForm(row, _set).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Selected = true;

            var id = (int) dataGridView1.Rows[e.RowIndex].Cells[0].Value;

            VipAvtoSet.NormativesRow row = (VipAvtoSet.NormativesRow)_set.GetRowById(Constants.NormativesTableName, id);
            if (new EditNormativeForm(row, _set).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new AddNormativeForm(_set).ShowDialog() == DialogResult.OK)
                UpdateRows();
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

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;

            VipAvtoSet.NormativesRow row = (VipAvtoSet.NormativesRow)_set.GetRowById(Constants.NormativesTableName, id);
            if (new EditNormativeForm(row, _set).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_rm.GetString("deleteNorm"), _rm.GetString("warning"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;
                _set.RemoveRowById(Constants.NormativesTableName, id);
                UpdateRows();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_rm.GetString("deleteNorm"), _rm.GetString("warning"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

                var id = _set.GetGroupId(groupSelector.SelectedItem.ToString());
                _set.RemoveAllNormatives(id);
            }
        }
    }
}