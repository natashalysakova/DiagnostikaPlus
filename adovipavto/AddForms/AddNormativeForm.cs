﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
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
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);


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

                    var groupsList = new List<string>();
                    foreach (object item in checkedListBox1.CheckedItems)
                    {
                        if (!Program.VipAvtoDataSet.GroupContainsNormative(item.ToString(),
                            comboBox2.SelectedItem.ToString()))
                        {
                            Program.VipAvtoDataSet.AddNormative(item.ToString(), comboBox2.SelectedItem.ToString(), min,
                                max);
                        }
                        else
                        {
                            groupsList.Add(item.ToString());
                        }
                    }

                    if (groupsList.Count != 0)
                    {
                        var sb = new StringBuilder();

                        foreach (string s in groupsList)
                        {
                            sb.Append(s + "\n");
                        }

                        MessageBox.Show(StringResource.groupContaintsNormative + Environment.NewLine + sb,
                            StringResource.error,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(StringResource.oneGroup, StringResource.error, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(StringResource.wrongData, StringResource.error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
                    errorProvider1.SetError(textBox1, StringResource.minmax);
                    errorProvider1.SetError(textBox2, StringResource.minmax);
                }
            }
            catch (Exception)
            {
                errorProvider1.SetError(((TextBox) sender), StringResource.wrongData);
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
                    select Program.VipAvtoDataSet.CreateGroupTitle((int) item["GroupID"])).ToList();


            //comboBox2.DataSource = Program.NormasTitles.Select(item => item.Value).ToList();
            comboBox2.DataSource = new Normatives().GetAllNormatives();
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