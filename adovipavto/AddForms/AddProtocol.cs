using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Enums;

namespace adovipavto.AddForms
{
    public partial class AddProtocol : Form
    {
        private readonly MainForm _mainForm;



        private int _newProtocolId;
        private DataRow[] _normatives;
        private List<VisualRow> _rows;

        public AddProtocol(MainForm mainForm)
        {
            _mainForm = mainForm;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            InitializeComponent();


            _rows = new List<VisualRow>();
        }

        private void AddProtocol_Load(object sender, EventArgs e)
        {
            CleanFields();
            UpdateFormLables();

            string[] groups = (
                from DataRow item in Program.VipAvtoDataSet.Tables[Constants.GroupTableName].Rows
                select Program.VipAvtoDataSet.CreateGroupTitle((int) item["GroupID"])).ToArray();

            if (groups.Length == 0)
            {
                MessageBox.Show(StringResource.nogroup,
                    StringResource.warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }

// ReSharper disable once CoVariantArrayConversion
            comboBox1.Items.AddRange(groups);

            dateTimePicker1.Value = DateTime.Now;

            string[] mechanics = (
                from DataRow item in Program.VipAvtoDataSet.Tables[Constants.MechanicsTableName].Rows
                where (int) item["State"] != (int) State.Unemployed
                select
                    Program.VipAvtoDataSet.GetShortMechanicName((int) item["MechanicID"])
                ).ToArray();
            if (mechanics.Length == 0)
            {
                MessageBox.Show(StringResource.nomech,
                    StringResource.warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            comboBox2.DataSource = mechanics;
            comboBox2.Text = Settings.Instance.LastUsedMechanic;

            maskedTextBox1.SelectionStart = 6;
            maskedTextBox1.SelectionLength = 2;
        }

        private void UpdateFormLables()
        {
            brakesystem.Text = StringResource.brakeSystem;
            OUTSRTS.Text = StringResource.OUTSRTS;
            OUTSSTS.Text = StringResource.OUTSSTS;
            ORTS1.Text = StringResource.ORTS1;
            ORTS2.Text = StringResource.ORTS2;
            ORTS3.Text = StringResource.ORTS3;
            ORTSS.Text = StringResource.ORTSSS;
            MVSTS.Text = StringResource.MVSTS;
            KUNOU1.Text = StringResource.KUNOU1;


            lightSystem.Text = StringResource.lightSystem;
            SSFBS.Text = StringResource.SSFBS;
            SSFDS.Text = StringResource.SSFDS;
            SSPF.Text = StringResource.SSPF;
            CHPUP.Text = StringResource.CHPUP;

            engineAndItsSystem.Text = StringResource.engineAndItsSystem;
            SCOMCHV.Text = StringResource.SCOMCHV;
            SCOMACHV.Text = StringResource.SCOMACHV;
            SCHVCHV.Text = StringResource.SCHMCHV;
            SCHMACHV.Text = StringResource.SCHMACHV;
            DVRSUM.Text = StringResource.DVRSUM;
            DVRSUP.Text = StringResource.DVRSUP;
            //CHVNMO.Text = rm.GetString("CHVNMO");
            //CHVNPO.Text = rm.GetString("CHVNPO");

            GBO.Text = StringResource.GGBS;
            radioButton6.Text = StringResource.germ;
            radioButton7.Text = StringResource.nogerm;

            glass.Text = StringResource.glass;
            PVS.Text = StringResource.PVS;
            PPBS.Text = StringResource.PPBS;

            wheelSystem.Text = StringResource.wheelSystem;
            SL.Text = StringResource.SL;


            noise.Text = StringResource.noise;
            VSHA.Text = StringResource.VSHA;

            wheelAndTyres.Text = StringResource.wheelAndTyres;
            OVRP.Text = StringResource.OVRP;
            visualCheck.Text = StringResource.visualCheck;

            radioButton1.Text = StringResource.check;
            radioButton2.Text = StringResource.uncheck;


            label79.Text = StringResource.notAllFields;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnlockFields();
        }

        private void UnlockFields()
        {
            if (comboBox1.SelectedIndex == -1) return;
            if (!maskedTextBox1.MaskCompleted) return;

            panel1.Enabled = true;
            CleanFields();

            visualCheck.BackColor = SystemColors.Control;


            if (Program.VipAvtoDataSet.GroupWithGasEngine(comboBox1.SelectedItem.ToString()))
            {
                if (MessageBox.Show(StringResource.IsGBOActive, StringResource.warning, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    GBO.Enabled = true;
                    GBO.BackColor = Color.LightGoldenrodYellow;

                }
                else
                {
                    GBO.Enabled = false;
                    GBO.BackColor = SystemColors.Control;
                }
            }
            else
            {
                GBO.Enabled = false;
                GBO.BackColor = SystemColors.Control;
            }



            PreviewBtn.Enabled = true;
            SaveBtn.Enabled = true;
            PrintBtn.Enabled = true;


            _rows = new List<VisualRow>();



            int id = Program.VipAvtoDataSet.GetGroupId(comboBox1.SelectedItem.ToString());

            _normatives = (
                from DataRow item in Program.VipAvtoDataSet.Tables[Constants.NormativesTableName].Rows
                where (int) item["IDGroup"] == id
                select item
                ).ToArray();


            foreach (DataRow normative in _normatives)
            {
                var row = new VisualRow(normative);

                var lables = new List<Label>();


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

                            if (control is TextBox)
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

                _rows.Add(row);

                visualCheck.BackColor = Color.LightGoldenrodYellow;
                groupBox10.BackColor = Color.LightGoldenrodYellow;


                panel2.BackColor = Color.LightGoldenrodYellow;

            }


            if (Settings.Instance.AutoGeneratedData)
            {
                radioButton1.Checked = true;
                if (GBO.Enabled)
                {
                    radioButton6.Checked = true;
                }

            }



            timer1.Start();

        }

        private void CleanFields()
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is GroupBox)
                {
                    foreach (Control control2 in control.Controls)
                    {
                        if (control2 is TableLayoutPanel)
                        {
                            foreach (Control control3 in ((TableLayoutPanel) control2).Controls)
                            {
                                if (control3 is TextBox)
                                {
                                    control3.Text = "";
                                    control3.BackColor = Color.White;
                                    control3.Enabled = false;
                                }
                                if (control3 is Label && control3.Tag != null)
                                {
                                    control3.Text = @"0";
                                }
                            }
                        }
                    }
                }
            }

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            radioButton7.Checked = false;

            

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!SaveProtocolToDb())
            {
                MessageBox.Show(StringResource.fillFields, StringResource.error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (_newProtocolId != -1)
            {
                DataRow protocol = Program.VipAvtoDataSet.GetRowById(Constants.ProtocolsTableName, _newProtocolId);
                DataRow[] mesures = Program.VipAvtoDataSet.GetMesuresFromProtocol(protocol);

                new ProtocolReportForm(protocol, mesures, true).ShowDialog();

                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(StringResource.noprotocol, StringResource.error,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SaveProtocolToDb()
        {
            if (panel2.BackColor == Color.LightGoldenrodYellow)
                return false;


            Settings.Instance.LastUsedMechanic = comboBox2.SelectedItem.ToString();
            Settings.Instance.Save();


            string techpass = "";
            if (pictureBox28.Tag != null)
                techpass = pictureBox28.Tag.ToString();

            bool result;
            if (panel2.BackColor == Color.LightGreen)
                result = true;
            else
            {
                result = false;
            }

            DateTime nexDateTime;

            if (radioButton4.Checked)
                nexDateTime = dateTimePicker1.Value.AddMonths(6);
            else if (radioButton3.Checked)
                nexDateTime = dateTimePicker1.Value.AddYears(1);
            else
            {
                nexDateTime = dateTimePicker1.Value.AddYears(2);
            }

            int gbo;

            if (GBO.Enabled == false)
            {
                gbo = (int) Gbo.NotChecked;
            }
            else
            {
                if (radioButton6.Checked)
                    gbo = (int) Gbo.Germetical;
                else
                {
                    gbo = (int) Gbo.NotGermrtical;
                }
            }


            _newProtocolId = Program.VipAvtoDataSet.AddProtocol(label80.Text + maskedTextBox1.Text,
                comboBox2.SelectedItem.ToString(), dateTimePicker1.Value, techpass, comboBox1.SelectedItem.ToString(),
                result, nexDateTime, radioButton1.Checked, gbo);

            foreach (VisualRow row in _rows)
            {
                Program.VipAvtoDataSet.AddMesure(row.Id, row.Value, _newProtocolId);
            }


            return true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                visualCheck.BackColor = Color.LightGreen;
            else
            {
                visualCheck.BackColor = Color.LightPink;
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
                label79.Text = StringResource.notAllFields;
            }
            else if (AllValide())
            {
                panel2.BackColor = Color.LightGreen;
                string s = StringResource.sucess;
                if (s != null) label79.Text = s.ToUpper();
            }
            else
            {
                {
                    panel2.BackColor = Color.LightPink;
                    string s = StringResource.fail;
                    if (s != null) label79.Text = s.ToUpper();
                }
            }
        }

        private bool SomeRequred()
        {
            bool result = false;

            foreach (VisualRow row in _rows)
            {
                if (row.Requred())
                    result = true;
            }


            if (visualCheck.BackColor == Color.LightGoldenrodYellow)
                result = true;

            if (groupBox10.BackColor == Color.LightGoldenrodYellow)
                result = true;

            if (GBO.Enabled)
            {
                if (GBO.BackColor == Color.LightGoldenrodYellow)
                    result = true;
            }


            return result;
        }

        private bool AllValide()
        {
            bool result = true;

            foreach (VisualRow row in _rows)
            {
                if (!row.IsValid())
                    result = false;
            }

            if (visualCheck.BackColor == Color.LightPink)
                result = false;

            if (GBO.Enabled)
            {
                if (GBO.BackColor == Color.LightPink)
                    result = false;
            }

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
            if (!SaveProtocolToDb())
            {
                MessageBox.Show(StringResource.fillFields, StringResource.error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }


            _mainForm.UpdateRows();

            if (_newProtocolId != -1)
            {
                DataRow protocol = Program.VipAvtoDataSet.GetRowById(Constants.ProtocolsTableName, _newProtocolId);
                DataRow[] mesures = Program.VipAvtoDataSet.GetMesuresFromProtocol(protocol);

                new ProtocolReportForm(protocol, mesures).ShowDialog();
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(StringResource.noprotocol, StringResource.error,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!SaveProtocolToDb())
            {
                MessageBox.Show(StringResource.fillFields, StringResource.error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ((PictureBox) sender).Tag = openFileDialog1.FileName;
                ((PictureBox) sender).Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
                GBO.BackColor = Color.LightGreen;
            else
            {
                GBO.BackColor = Color.LightPink;
            }
        }
    }
}