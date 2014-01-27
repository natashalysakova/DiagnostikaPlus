using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using adovipavto.AddForms;
using adovipavto.Classes;
using adovipavto.EditForms;

namespace adovipavto
{
    public partial class NormativesForm : Form
    {
        public NormativesForm() : this("")
        {
        }


        public NormativesForm(string selectedGroup)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);


            InitializeComponent();

            dataGridView1.DataSource = Program.VipAvtoDataSet.Tables[Constants.NormativesTableName];

            object[] groups =
                (from DataRow item in Program.VipAvtoDataSet.Tables[Constants.GroupTableName].Rows
                    select item["Title"]).ToArray();

            groupSelector.Items.AddRange(groups);

            if (selectedGroup != "" || groupSelector.Items.Count == 0)
                groupSelector.Text = selectedGroup;
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
                row.Cells["NormTitle"].Value = Constants.NormativesTitles[(int)row.Cells["titleDataGridViewTextBoxColumn"].Value];
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Удалить норматив?", "Внимание",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;
                Program.VipAvtoDataSet.RemoveRowById(Constants.NormativesTableName, id);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;

            DataRow row = Program.VipAvtoDataSet.GetRowById(Constants.NormativesTableName, id);
            if( new EditNormativeForm(row).ShowDialog() == DialogResult.OK)
                UpdateRows();
        }
    }
}