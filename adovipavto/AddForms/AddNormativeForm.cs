using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto.AddForms
{
    public partial class AddNormativeForm : Form
    {
        public AddNormativeForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);


            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(textBox1) == "" & errorProvider1.GetError(textBox2) == "")
            {
                if (checkedListBox1.CheckedItems.Count != 0)
                {
                    double min = Convert.ToDouble(textBox1.Text);
                    double max = Convert.ToDouble(textBox2.Text);

                    foreach (object item in checkedListBox1.CheckedItems)
                    {
                        Program.VipAvtoDataSet.AddNormative(item.ToString(), comboBox2.SelectedItem.ToString(), min, max);
                    }

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Должна быть выбрана как минимум одна группа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Неверное значение поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            try
            {
                double min = Convert.ToDouble(textBox1.Text);
                double max = Convert.ToDouble(textBox2.Text);

                if (min < max)
                {
                    errorProvider1.SetError(textBox1, null);
                    errorProvider1.SetError(textBox2, null);
                }
                else
                {
                    errorProvider1.SetError(textBox1, "Минимальное значение больше максимального");
                    errorProvider1.SetError(textBox2, "Минимальное значение больше максимального");

                }

            }
            catch (Exception)
            {
                errorProvider1.SetError(((TextBox)sender), "try again");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Validate();
        }

        private void NewNormative_Load(object sender, EventArgs e)
        {
            checkedListBox1.DataSource =
                (from DataRow item in Program.VipAvtoDataSet.Tables[Constants.GroupTableName].Rows
                select item["Title"].ToString()).ToList();


            //comboBox2.DataSource = Program.NormasTitles.Select(item => item.Value).ToList();
            comboBox2.DataSource = Constants.NormativesTitles;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }
    }
}