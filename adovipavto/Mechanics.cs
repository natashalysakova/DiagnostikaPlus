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
    public partial class Mechanics : Form
    {
        private readonly VipAvtoSet _set;

        readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        public Mechanics(VipAvtoSet set)
        {
            _set = set;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            InitializeComponent();
        }

        private void Mechanics_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _set.Mechanics;
            UpdateRoles();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;

            VipAvtoSet.MechanicsRow row = (VipAvtoSet.MechanicsRow) _set.GetRowById(Constants.MechanicsTableName, id);


            if (new EditMechanicForm(row, _set).ShowDialog() == DialogResult.OK)
                UpdateRoles();
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
            if (new AddMechanicForm(_set).ShowDialog() == DialogResult.OK)
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
                if (MessageBox.Show(_rm.GetString("lockmech"), _rm.GetString("warning"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) ==
                    DialogResult.Yes)
                {
                    var id = (int) dataGridView1.SelectedRows[0].Cells["mechanicIDDataGridViewTextBoxColumn"].Value;

                    _set.LockMechanic(id);
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
                row.Cells["StateString"].Value = Constants.GetEnumDescription((State) row.Cells["State"].Value);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Delete();
        }
    }
}