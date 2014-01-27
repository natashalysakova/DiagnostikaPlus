using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using adovipavto.AddForms;
using adovipavto.Classes;
using adovipavto.EditForms;

namespace adovipavto
{
    public partial class Mechanics : Form
    {
        public Mechanics()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);

            InitializeComponent();
        }

        private void Mechanics_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Program.VipAvtoDataSet.Tables[Constants.MechanicsTableName];
            UpdateRoles();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            var id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;

            DataRow row = Program.VipAvtoDataSet.GetRowById(Constants.MechanicsTableName, id);


            if(new EditMechanicForm(row).ShowDialog() == DialogResult.OK)
                UpdateRoles();
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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void Add()
        {
            if (new AddMechanicForm().ShowDialog() == DialogResult.OK)
                UpdateRoles();
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (dataGridView1.SelectedRows[0] != null)
            {
                if (MessageBox.Show("Заблокировать механика?", "Внимание",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) ==
                    DialogResult.Yes)
                {
                    int id = (int)dataGridView1.SelectedRows[0].Cells["mechanicIDDataGridViewTextBoxColumn"].Value;

                    Program.VipAvtoDataSet.LockMechanic(id);
                    UpdateRoles();
                }
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            UpdateRoles();
        }


        private void UpdateRoles()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                
                row.Cells["StateString"].Value = Constants.GetEnumDescription((Enums.State)row.Cells["State"].Value);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Delete();
        }
    }
}
