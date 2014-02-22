using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Enums;

namespace adovipavto.AddForms
{
    public partial class AddGroupForm : Form
    {
        readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        public AddGroupForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);
            InitializeComponent();
        }


        private void NewGroupForm_Load(object sender, EventArgs e)
        {
            checkedListBox1.DataSource = Enum.GetValues(typeof (Category));
            checkedListBox2.DataSource = new Engines().GetAllEngines();
            for (int i = 1920; i <= DateTime.Now.Year; i++)
            {
                comboBox3.Items.Add(i);
            }

            comboBox3.SelectedItem = DateTime.Now.Year;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count != 0)
            {
                if (checkedListBox2.CheckedItems.Count != 0)
                {
                    var groupsList = new List<string>();


                    foreach (object item in checkedListBox1.CheckedItems)
                    {
                        foreach (object item2 in checkedListBox2.CheckedItems)
                        {
                            if (!Program.VipAvtoDataSet.GroupExist(Convert.ToInt32(comboBox3.SelectedItem.ToString()),
                                item.ToString(),
                                new Engines().GetEngineIndex(item2.ToString()), radioButton1.Checked))
                            {
                                Program.VipAvtoDataSet.AddGroup(Convert.ToInt32(comboBox3.SelectedItem.ToString()),
                                    item.ToString(),
                                    new Engines().GetEngineIndex(item2.ToString()), radioButton1.Checked);
                            }

                            else
                            {
                                groupsList.Add(item.ToString());
                            }
                        }
                    }

                    if (groupsList.Count != 0)
                    {
                        var sb = new StringBuilder();

                        foreach (string s in groupsList)
                        {
                            sb.Append(s + "\n");
                        }

                        MessageBox.Show(_rm.GetString("groupExist") + Environment.NewLine + sb,
                            _rm.GetString("error"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(_rm.GetString("oneEngine"), _rm.GetString("error"), MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(_rm.GetString("oneGroup"), _rm.GetString("error"), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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