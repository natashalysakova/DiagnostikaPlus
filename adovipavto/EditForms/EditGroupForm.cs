using System;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Enums;
using adovipavto.Properties;

namespace adovipavto.EditForms
{
    public partial class EditGroupForm : Form
    {
        private readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());

        private readonly DataRow _selectedRow;


        public EditGroupForm(DataRow select)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);


            InitializeComponent();
            _selectedRow = select;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void EditGroupForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Program.VipAvtoDataSet.CreateGroupTitle((int) _selectedRow["GroupID"]);


            categoryComboBox.DataSource = Enum.GetValues(typeof (Category));
            categoryComboBox.Text = _selectedRow["Category"].ToString();


            engineComboBox.DataSource = new Engines().GetAllEngines();
            engineComboBox.Text = new Engines()[(int) _selectedRow["EngineType"]];


            for (int i = 1920; i <= DateTime.Now.Year; i++)
            {
                yearComboBox.Items.Add(i);
            }
            yearComboBox.Text = _selectedRow["Year"].ToString();

            if ((bool) _selectedRow["Before"] == false)
                radioButton2.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Program.VipAvtoDataSet.GroupExist(Convert.ToInt32(yearComboBox.SelectedItem.ToString()),
                categoryComboBox.SelectedItem.ToString(),
                new Engines().GetEngineIndex(engineComboBox.SelectedItem.ToString()), radioButton1.Checked))
            {
                Program.VipAvtoDataSet.EditGroup(Convert.ToInt32(_selectedRow["GroupID"]),
                    Convert.ToInt32(yearComboBox.SelectedItem), categoryComboBox.SelectedItem.ToString(),
                    new Engines().GetEngineIndex(engineComboBox.SelectedItem.ToString()), radioButton1.Checked);

                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(_rm.GetString("groupExist2"),
                    _rm.GetString("error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}