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

        readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        public Mechanics()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            InitializeComponent();
        }

        private void Mechanics_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "newVipAvtoSet.Mechanics". При необходимости она может быть перемещена или удалена.
            this.mechanicsTableAdapter.Fill(this.newVipAvtoSet.Mechanics);
            dataGridView1.DataSource = newVipAvtoSet.Mechanics;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;

            NewVipAvtoSet.MechanicsRow row = (NewVipAvtoSet.MechanicsRow)newVipAvtoSet.GetRowById(Constants.MechanicsTableName, id);


            new EditMechanicForm(row, newVipAvtoSet).ShowDialog();
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
            new AddMechanicForm(newVipAvtoSet).ShowDialog();
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
                    var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;

                    newVipAvtoSet.LockMechanic(id);
                }
            }
        }



        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == stateDataGridViewTextBoxColumn.Index)
            {
                if (e.Value != null)
                {
                    e.Value = Constants.GetEnumDescription((State)e.Value);
                }
            }
        }
    }
}