using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Enums;

namespace adovipavto.AddForms
{
    public partial class AddGroupForm : Form
    {
        public AddGroupForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
        }


        private void NewGroupForm_Load(object sender, EventArgs e)
        {
            checkedListBox1.DataSource = Enum.GetValues(typeof(Category));
            checkedListBox2.DataSource = Enum.GetValues(typeof(Engine));
            for (int i = 1920; i <= DateTime.Now.Year; i++)
            {
                comboBox3.Items.Add(i);
            }

            comboBox3.SelectedItem = DateTime.Now.Year;
            //}
            //else
            //{
            //    DataRow dataRow = Program.VipAvtoDataSet.GetRowByIndex(Constants.GroupTableName, groupId);
            //    checkedListBox1.DataSource = Enum.GetValues(typeof(Category));
            //    checkedListBox1.SelectedItem = (Category)Enum.Parse(typeof(Category), dataRow["Category"].ToString(), true);
            //    checkedListBox2.DataSource = Enum.GetValues(typeof(Engine));
            //    checkedListBox2.SelectedItem = (Engine)Enum.Parse(typeof(Engine), dataRow["EngineType"].ToString(), true);
            //    for (int i = 1920; i <= DateTime.Now.Year; i++)
            //    {
            //        comboBox3.Items.Add(i);
            //    }
            //    comboBox3.SelectedItem = dataRow["Year"];

            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count != 0)
            {
                if (checkedListBox2.CheckedItems.Count != 0)
                {
                    foreach (object item in checkedListBox1.CheckedItems)
                    {
                        foreach (object item2 in checkedListBox2.CheckedItems)
                        {
                            Program.VipAvtoDataSet.AddGroup(Convert.ToInt32(comboBox3.SelectedItem.ToString()),
                                item.ToString(),
                                item2.ToString(), radioButton1.Checked);
                        }

                        DialogResult = DialogResult.OK;

                    }
                }
                else
                {
                    MessageBox.Show("Должен быть выбран как минимум один вид двигателя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Должна быть выбрана как минимум одна группа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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