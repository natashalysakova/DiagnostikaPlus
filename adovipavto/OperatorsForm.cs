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
using adovipavto.Enums;

namespace adovipavto
{
    public partial class OperatorsForm : Form
    {
        private readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());

        public OperatorsForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            InitializeComponent();
            dataGridView1.DataSource = newVipAvtoSet.Operators;
        }

        private void OperatorsForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "newVipAvtoSet.Operators". При необходимости она может быть перемещена или удалена.
            operatorsTableAdapter.Fill(newVipAvtoSet.Operators);
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (dataGridView1.SelectedRows[0] != null)
            {
                if (MessageBox.Show(_rm.GetString("lockOperator"), _rm.GetString("warning"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) ==
                    DialogResult.Yes)
                {
                    var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;

                    if (AdministratorsCount() == 1 &&
                        (int) dataGridView1.SelectedRows[0].Cells["rightDataGridViewTextBoxColumn"].Value ==
                        (int) Rights.Administrator)
                    {
                        MessageBox.Show(_rm.GetString("cantLock"), _rm.GetString("error"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        newVipAvtoSet.LockOperator(id);
                        if (id == newVipAvtoSet.GetOperatorId())
                        {
                            MessageBox.Show(_rm.GetString("reboot"), _rm.GetString("warning"), MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            Application.Restart();
                        }
                    }
                }
            }
        }

        private int AdministratorsCount()
        {
            return
                newVipAvtoSet.Operators.Select(string.Format("Right = {0}",
                    (int) Rights.Administrator)).Length;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void Add()
        {
            new AddOperatorForm(newVipAvtoSet).ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            var id = (int) dataGridView1.SelectedRows[0].Cells[0].Value;

            var row = (NewVipAvtoSet.OperatorsRow) newVipAvtoSet.GetRowById(Constants.OperatorsTableName, id);


            new EditOperatorForm(row, newVipAvtoSet).ShowDialog();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            dataGridView1.FirstDisplayedCell = dataGridView1.CurrentCell;
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add();
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == rightDataGridViewTextBoxColumn.Index)
            {
                if (e.Value != null)
                {
                    e.Value = Constants.GetEnumDescription((Rights) e.Value);
                }
            }
        }
    }
}