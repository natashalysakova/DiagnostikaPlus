using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Enums;

namespace adovipavto.EditForms
{
    public partial class EditGroupForm : Form
    {
        private readonly DataRow selectedRow;

        public EditGroupForm(DataRow select)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);


            InitializeComponent();
            selectedRow = select;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void EditGroupForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Program.VipAvtoDataSet.CreateGroupTitle((int)selectedRow["GroupID"]);


            categoryComboBox.DataSource = Enum.GetValues(typeof (Category));
            categoryComboBox.Text = selectedRow["Category"].ToString();


            engineComboBox.DataSource = new Engines().EnginesTitle;
            engineComboBox.Text = new Engines().EnginesTitle[(int)selectedRow["EngineType"]];


            for (int i = 1920; i <= DateTime.Now.Year; i++)
            {
                yearComboBox.Items.Add(i);
            }
            yearComboBox.Text = selectedRow["Year"].ToString();

            if ((bool) selectedRow["Before"] == false)
                radioButton2.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.VipAvtoDataSet.EditGroup(Convert.ToInt32(selectedRow["GroupID"]),
                Convert.ToInt32(yearComboBox.SelectedItem), categoryComboBox.SelectedItem.ToString(),
                new Engines().GetEngineIndex(engineComboBox.SelectedItem.ToString()), radioButton1.Checked);

            DialogResult = DialogResult.OK;
        }
    }
}