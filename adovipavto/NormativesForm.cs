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

        public NormativesForm() : this(0)
        {
        }

        public NormativesForm(int selectedGroup)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);


            InitializeComponent();

            dataGridView1.DataSource = Program.VipAvtoDataSet.Tables[Constants.NormativesTableName];

// ReSharper disable once CoVariantArrayConversion
            object[] groups =
                (from DataRow item in Program.VipAvtoDataSet.Tables[Constants.GroupTableName].Rows
                    select Program.VipAvtoDataSet.GroupTitle((int) item["GroupID"])).ToArray();

            groupSelector.Items.AddRange(groups);

            if (Program.VipAvtoDataSet.GroupTitle(selectedGroup) != "" || groupSelector.Items.Count == 0)
                groupSelector.Text = Program.VipAvtoDataSet.GroupTitle(selectedGroup);
            else
            {
                groupSelector.SelectedIndex = 0;
            }
        }

        private void NormativesForm_Load(object sender, EventArgs e)
        {
            int id = Program.VipAvtoDataSet.GetGroupId(groupSelector.Text);
            ((DataTable) dataGridView1.DataSource).DefaultView.RowFilter = string.Format("IDGroup = '{0}'", id);
            dataGridView1.Columns[0].Visible = false;
            UpdateRows();
        }


        private void groupSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Program.VipAvtoDataSet.GetGroupId(groupSelector.Text);
            ((DataTable) dataGridView1.DataSource).DefaultView.RowFilter = string.Format("IDGroup = '{0}'", id);
            UpdateRows();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (new AddNormativeForm().ShowDialog() == DialogResult.OK)
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
                Program.VipAvtoDataSet.RemoveRowById(Constants.NormativesTableName, id);
                UpdateRows();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;

            DataRow row = Program.VipAvtoDataSet.GetRowById(Constants.NormativesTableName, id);
            if (new EditNormativeForm(row).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Selected = true;

            var id = (int) dataGridView1.Rows[e.RowIndex].Cells[0].Value;

            DataRow row = Program.VipAvtoDataSet.GetRowById(Constants.NormativesTableName, id);
            if (new EditNormativeForm(row).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new AddNormativeForm().ShowDialog() == DialogResult.OK)
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

            DataRow row = Program.VipAvtoDataSet.GetRowById(Constants.NormativesTableName, id);
            if (new EditNormativeForm(row).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_rm.GetString("deleteNorm"), _rm.GetString("warning"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;
                Program.VipAvtoDataSet.RemoveRowById(Constants.NormativesTableName, id);
                UpdateRows();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_rm.GetString("deleteNorm"), _rm.GetString("warning"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

                var id = Program.VipAvtoDataSet.GetGroupId(groupSelector.SelectedItem.ToString());
                Program.VipAvtoDataSet.RemoveAllNormatives(id);
            }
        }
    }
}