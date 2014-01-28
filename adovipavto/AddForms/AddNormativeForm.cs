using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
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

        ResourceManager rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());


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

                    List<String> groupsList = new List<string>();
                    foreach (object item in checkedListBox1.CheckedItems)
                    {
                        if (!Program.VipAvtoDataSet.GroupContainsNormative(item.ToString(),
                                comboBox2.SelectedItem.ToString()))
                        {
                            Program.VipAvtoDataSet.AddNormative(item.ToString(), comboBox2.SelectedItem.ToString(), min, max);
                        }
                        else
                        {
                            groupsList.Add(item.ToString());
                        }
                    }

                    if (groupsList.Count != 0)
                    {
                        StringBuilder sb = new StringBuilder();

                        foreach (string s in groupsList)
                        {
                            sb.Append(s + "\n");
                        }

                        MessageBox.Show(rm.GetString("groupContaintsNormative") + "\n" + sb.ToString(), rm.GetString("error"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(rm.GetString("oneGroup"), rm.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(rm.GetString("wrongData"), rm.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    errorProvider1.SetError(textBox1, rm.GetString("minmax"));
                    errorProvider1.SetError(textBox2, rm.GetString("minmax"));

                }

            }
            catch (Exception)
            {
                errorProvider1.SetError(((TextBox)sender), rm.GetString("wrongData"));
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
            comboBox2.DataSource = new Normatives().NormativesTitle;
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