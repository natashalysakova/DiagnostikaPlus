﻿using System;
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
using adovipavto.VipAvtoDBDataSetTableAdapters;
using adovipavto.Properties;

namespace adovipavto
{
    public partial class NormativesForm : Form
    {
        private readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());

        private readonly int _selectedGroup;


        public NormativesForm() : this(0)
        {
        }

        public NormativesForm(int selectedGroup)
        {
            _selectedGroup = selectedGroup;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);

            InitializeComponent();
        }

        private void NormativesForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "VipAvtoDBDataSet.Normatives". При необходимости она может быть перемещена или удалена.
            normativesTableAdapter.Fill(VipAvtoDBDataSet.Normatives);

            var adapter = new GroupsTableAdapter();
            adapter.Fill(VipAvtoDBDataSet.Groups);
            adapter.Dispose();

            dataGridView1.DataSource = VipAvtoDBDataSet.Normatives;

            string[] groups =
                (from VipAvtoDBDataSet.GroupsRow item in VipAvtoDBDataSet.Groups
                    select item.Title).ToArray();

            groupSelector.Items.AddRange(groups);

            if (VipAvtoDBDataSet.GetGroupTitle(_selectedGroup) != null || groupSelector.Items.Count == 0)
            {
                groupSelector.Text = VipAvtoDBDataSet.GetGroupTitle(_selectedGroup);
            }
            else
            {
                groupSelector.SelectedIndex = 0;
            }


            int id = VipAvtoDBDataSet.GetGroupId(groupSelector.Text);
            ((DataTable) dataGridView1.DataSource).DefaultView.RowFilter = string.Format("GroupId = '{0}'", id);
        }


        private void groupSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = VipAvtoDBDataSet.GetGroupId(groupSelector.Text);
            ((DataTable) dataGridView1.DataSource).DefaultView.RowFilter = string.Format("GroupId = '{0}'", id);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new AddNormativeForm(VipAvtoDBDataSet).ShowDialog();
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_rm.GetString("deleteNorm"), _rm.GetString("warning"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;
                VipAvtoDBDataSet.RemoveRowById(Constants.NormativesTableName, id);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;

            var row = (VipAvtoDBDataSet.NormativesRow) VipAvtoDBDataSet.GetRowById(Constants.NormativesTableName, id);
            new EditNormativeForm(row, VipAvtoDBDataSet).ShowDialog();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Selected = true;

            var id = (int) dataGridView1.Rows[e.RowIndex].Cells[0].Value;

            var row = (VipAvtoDBDataSet.NormativesRow) VipAvtoDBDataSet.GetRowById(Constants.NormativesTableName, id);
            new EditNormativeForm(row, VipAvtoDBDataSet).ShowDialog();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddNormativeForm(VipAvtoDBDataSet).ShowDialog();
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

            var row = (VipAvtoDBDataSet.NormativesRow) VipAvtoDBDataSet.GetRowById(Constants.NormativesTableName, id);
            new EditNormativeForm(row, VipAvtoDBDataSet).ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_rm.GetString("deleteNorm"), _rm.GetString("warning"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;
                VipAvtoDBDataSet.RemoveRowById(Constants.NormativesTableName, id);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == tagDataGridViewTextBoxColumn.Index)
            {
                if (e.Value != null)
                {
                    e.Value = new Normatives()[(int) e.Value];
                }
            }
        }
    }
}