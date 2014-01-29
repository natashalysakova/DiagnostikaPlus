using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Enums;

namespace adovipavto.AddForms
{
    public partial class AddProtocol : Form
    {
        private List<VisualRow> rows;

        ResourceManager rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        public AddProtocol(MainForm mainForm)
        {
            _mainForm = mainForm;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);

            InitializeComponent();


            rows = new List<VisualRow>();
        }

        private void AddProtocol_Load(object sender, EventArgs e)
        {
            CleanFields();
            UpdateFormLables();

            object[] groups = (
                from DataRow item in Program.VipAvtoDataSet.Tables[Constants.GroupTableName].Rows
                select Program.VipAvtoDataSet.CreateGroupTitle((int)item["GroupID"])).ToArray();

            comboBox1.Items.AddRange(groups);

            dateTimePicker1.Value = DateTime.Now;

            var mechanics = (
                from DataRow item in Program.VipAvtoDataSet.Tables[Constants.MechanicsTableName].Rows
                where (int)item["State"] != (int)State.Unemployed
                select
                    Program.VipAvtoDataSet.GetShortMechanicName((int)item["MechanicID"])
                ).ToArray();
            if (mechanics.Length == 0)
            {
                MessageBox.Show(rm.GetString("nomech"),
                    rm.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            comboBox2.DataSource = mechanics;
            comboBox2.Text = Properties.Settings.Default.LastUsedMechanic;

            maskedTextBox1.SelectionStart = 6;
            maskedTextBox1.SelectionLength = 2;

        }

        private void UpdateFormLables()
        {
            brakesystem.Text = rm.GetString("brakeSystem");
            OUTSRTS.Text = rm.GetString("OUTSRTS");
            OUTSSTS.Text = rm.GetString("OUTSSTS");
            ORTS1.Text = rm.GetString("ORTS1");
            ORTS2.Text = rm.GetString("ORTS2");
            ORTS3.Text = rm.GetString("ORTS3");
            MVSTS.Text = rm.GetString("MVSTS");
            KUNOU1.Text = rm.GetString("KUNOU1");


            lightSystem.Text = rm.GetString("lightSystem");
            SSFBS.Text = rm.GetString("SSFBS");
            SSFDS.Text = rm.GetString("SSFDS");
            SSPF.Text = rm.GetString("SSPF");
            CHPUP.Text = rm.GetString("CHPUP");

            engineAndItsSystem.Text = rm.GetString("engineAndItsSystem");
            SCOMCHV.Text = rm.GetString("SCOMCHV");
            SCOMACHV.Text = rm.GetString("SCOMACHV");
            SCHVCHV.Text = rm.GetString("SCHMCHV");
            SCHMACHV.Text = rm.GetString("SCHMACHV");
            DVRSUM.Text = rm.GetString("DVRSUM");
            DVRSUP.Text = rm.GetString("DVRSUP");
            CHVNMO.Text = rm.GetString("CHVNMO");
            CHVNPO.Text = rm.GetString("CHVNPO");

            glass.Text = rm.GetString("glass");
            PVS.Text = rm.GetString("PVS");
            PPBS.Text = rm.GetString("PPBS");

            wheelSystem.Text = rm.GetString("wheelSystem");
            SL.Text = rm.GetString("SL");


            noise.Text = rm.GetString("noise");
            VSHA.Text = rm.GetString("VSHA");

            wheelAndTyres.Text = rm.GetString("wheelAndTyres");
            OVRP.Text = rm.GetString("OVRP");
            visualChrck.Text = rm.GetString("visualCheck");

            radioButton1.Text = rm.GetString("check");
            radioButton2.Text = rm.GetString("uncheck");

            label79.Text = rm.GetString("notAllFields");
        }

        private DataRow[] normatives;
        private int _newProtocolId;
        private MainForm _mainForm;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnlockFields();
        }

        private void UnlockFields()
        {
            if (comboBox1.SelectedIndex == -1) return;
            if (!maskedTextBox1.MaskCompleted) return;

            panel1.Enabled = true;
            rows = new List<VisualRow>();

            CleanFields();


            int id = Program.VipAvtoDataSet.GetGroupId(comboBox1.SelectedItem.ToString());

            normatives = (
                from DataRow item in Program.VipAvtoDataSet.Tables[Constants.NormativesTableName].Rows
                where (int)item["IDGroup"] == id
                select item
                ).ToArray();


            foreach (DataRow normative in normatives)
            {
                VisualRow row = new VisualRow(normative);

                List<Label> lables = new List<Label>();


                foreach (Control control2 in panel1.Controls)
                {
                    if (!(control2 is GroupBox)) continue;

                    foreach (Control control3 in control2.Controls)
                    {
                        if (!(control3 is TableLayoutPanel)) continue;

                        foreach (Control control in control3.Controls)
                        {
                            if (control.Tag == null) continue;

                            if (control.Tag.ToString() != row.Id.ToString()) continue;

                            if (control is PictureBox)
                            {
                                row.PictureBox = (control as PictureBox);
                            }
                            else if (control is TextBox)
                            {
                                row.TextBox = (control as TextBox);
                            }
                            else if (control is Label)
                            {
                                lables.Add((control as Label));
                            }
                        }
                    }
                }

                if (lables[0].Location.X > lables[1].Location.X)
                {
                    row.MaxLabel = lables[0];
                    row.MinLabel = lables[1];
                }
                else
                {
                    row.MaxLabel = lables[1];
                    row.MinLabel = lables[0];
                }

                rows.Add(row);

                visualChrck.BackColor = Color.LightGoldenrodYellow;
                groupBox10.BackColor = Color.LightGoldenrodYellow;

                panel2.BackColor = Color.LightGoldenrodYellow;

                timer1.Start();
            }
        }

        private void CleanFields()
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is GroupBox)
                {
                    foreach (Control control2 in ((GroupBox) control).Controls)
                    {
                        if (control2 is TableLayoutPanel)
                        {
                            foreach (Control control3 in ((TableLayoutPanel) control2).Controls)
                            {
                                if (control3 is TextBox)
                                {
                                    control3.Text = "";
                                    control3.BackColor = Color.White;
                                }
                                if (control3 is Label && control3.Tag != null)
                                {
                                    control3.Text = "0";
                                }
                            }
                        }
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!SaveProtocolToDB())
            {
                MessageBox.Show(rm.GetString("fillFields"), rm.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_newProtocolId != -1)
            {
                DataRow protocol = Program.VipAvtoDataSet.GetRowById(Constants.ProtocolsTableName, _newProtocolId);
                DataRow[] mesures = Program.VipAvtoDataSet.GetMesuresFromProtocol(_newProtocolId);

                new ProtocolReportForm(protocol, mesures, true).ShowDialog();

                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(rm.GetString("noprotocol"), rm.GetString("error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool SaveProtocolToDB()
        {
            if (panel2.BackColor == Color.LightGoldenrodYellow)
                return false;


            Properties.Settings.Default.LastUsedMechanic = comboBox2.SelectedItem.ToString();
            Properties.Settings.Default.Save();


            string photo = "", techpass = "";
            if (pictureBox27.Tag != null)
                photo = pictureBox27.Tag.ToString();
            if (pictureBox28.Tag != null)
                techpass = pictureBox28.Tag.ToString();

            bool result;
            if (panel2.BackColor == Color.LightGreen)
                result = true;
            else
            {
                result = false;
            }

            DateTime nexDateTime = dateTimePicker1.Value;

            if (radioButton4.Checked)
                nexDateTime = dateTimePicker1.Value.AddMonths(6);
            else if (radioButton3.Checked)
                nexDateTime = dateTimePicker1.Value.AddYears(1);
            else
            {
                nexDateTime = dateTimePicker1.Value.AddYears(2);
            }



            _newProtocolId = Program.VipAvtoDataSet.AddProtocol(label80.Text + maskedTextBox1.Text,
                comboBox2.SelectedItem.ToString(),
                dateTimePicker1.Value, photo, techpass, comboBox1.SelectedItem.ToString(), result, nexDateTime, radioButton1.Checked);

            foreach (VisualRow row in rows)
            {
                Program.VipAvtoDataSet.AddMesure(row.Id, row.Value, _newProtocolId);
            }

            return true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                visualChrck.BackColor = Color.LightGreen;
            else
            {
                visualChrck.BackColor = Color.LightPink;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
                groupBox10.BackColor = Color.PaleGreen;
            else if (radioButton3.Checked)
                groupBox10.BackColor = Color.GreenYellow;
            else
            {
                groupBox10.BackColor = Color.YellowGreen;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SomeRequred())
            {
                panel2.BackColor = Color.LightGoldenrodYellow;
                label79.Text = rm.GetString("notAllFields");
            }
            else if (AllValide())
            {
                panel2.BackColor = Color.LightGreen;
                string s = rm.GetString("sucess");
                if (s != null) label79.Text = s.ToUpper();
            }
            else
            {
                {
                    panel2.BackColor = Color.LightPink;
                    string s = rm.GetString("fail");
                    if (s != null) label79.Text = s.ToUpper();
                }
            }

        }

        private bool SomeRequred()
        {
            bool result = false;

            foreach (VisualRow row in rows)
            {
                if (row.Requred())
                    result = true;
            }


            if (visualChrck.BackColor == Color.LightGoldenrodYellow)
                result = true;

            if (groupBox10.BackColor == Color.LightGoldenrodYellow)
                result = true;

            return result;



        }

        private bool AllValide()
        {
            bool result = true;

            foreach (VisualRow row in rows)
            {
                if (!row.IsValid())
                    result = false;
            }

            if (visualChrck.BackColor == Color.LightPink)
                result = false;

            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.MaskCompleted)
            {
                maskedTextBox1.BackColor = Color.LightGreen;

                UnlockFields();

            }
            else
            {
                maskedTextBox1.BackColor = Color.LightGoldenrodYellow;
                panel1.Enabled = false;
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!SaveProtocolToDB())
            {
                MessageBox.Show(rm.GetString("fillFields"), rm.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            _mainForm.UpdateRows();

            if (_newProtocolId != -1)
            {
                DataRow protocol = Program.VipAvtoDataSet.GetRowById(Constants.ProtocolsTableName, _newProtocolId);
                DataRow[] mesures = Program.VipAvtoDataSet.GetMesuresFromProtocol(_newProtocolId);

                new ProtocolReportForm(protocol, mesures).ShowDialog();
                DialogResult = DialogResult.OK;

            }
            else
            {
                MessageBox.Show(rm.GetString("noprotocol"), rm.GetString("error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!SaveProtocolToDB())
            {
                MessageBox.Show(rm.GetString("fillFields"), rm.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;

        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ((PictureBox)sender).Tag = openFileDialog1.FileName;
                ((PictureBox)sender).Image = Image.FromFile(openFileDialog1.FileName);
            }
        }
    }
}
