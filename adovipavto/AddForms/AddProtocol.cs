using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Enums;

namespace adovipavto.AddForms
{
    public partial class AddProtocol : Form
    {
        private GBOSTATE gbo;

        private readonly MainForm _mainForm;

        readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());


        private int _newProtocolId;
        private DataRow[] _normatives;
        private List<VisualRow> _rows;

        public AddProtocol(MainForm mainForm)
        {
            _mainForm = mainForm;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            InitializeComponent();

            gbo = GBOSTATE.None;
            _rows = new List<VisualRow>();
        }

        private void AddProtocol_Load(object sender, EventArgs e)
        {
            CleanFields();
            UpdateFormLables();

            string[] groups = (
                from DataRow item in Program.VipAvtoDataSet.Tables[Constants.GroupTableName].Rows
                select Program.VipAvtoDataSet.CreateGroupTitle((int)item["GroupID"])).ToArray();

            if (groups.Length == 0)
            {
                MessageBox.Show(_rm.GetString("nogroup"),
                    _rm.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }

            // ReSharper disable once CoVariantArrayConversion
            comboBox1.Items.AddRange(groups);

            dateTimePicker1.Value = DateTime.Now;

            string[] mechanics = (
                from DataRow item in Program.VipAvtoDataSet.Tables[Constants.MechanicsTableName].Rows
                where (int)item["State"] != (int)State.Unemployed
                select
                    Program.VipAvtoDataSet.GetShortMechanicName((int)item["MechanicID"])
                ).ToArray();
            if (mechanics.Length == 0)
            {
                MessageBox.Show(_rm.GetString("nomech"),
                    _rm.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            comboBox2.DataSource = mechanics;
            comboBox2.Text = Settings.Instance.LastUsedMechanic;

            maskedTextBox1.SelectionStart = 6;
            maskedTextBox1.SelectionLength = 2;
        }

        private void UpdateFormLables()
        {
            brakesystem.Text = _rm.GetString("brakeSystem");
            OUTSRTS.Text = _rm.GetString("OUTSRTS");
            OUTSSTS.Text = _rm.GetString("OUTSSTS");
            ORTS1.Text = _rm.GetString("ORTS1");
            ORTS2.Text = _rm.GetString("ORTS2");
            ORTS3.Text = _rm.GetString("ORTS3");
            ORTSS.Text = _rm.GetString("ORTSSS");
            MVSTS.Text = _rm.GetString("MVSTS");
            KUNOU1.Text = _rm.GetString("KUNOU1");


            lightSystem.Text = _rm.GetString("lightSystem");
            SSFBS.Text = _rm.GetString("SSFBS");
            SSFDS.Text = _rm.GetString("SSFDS");
            SSPF.Text = _rm.GetString("SSPF");
            CHPUP.Text = _rm.GetString("CHPUP");

            engineAndItsSystem.Text = _rm.GetString("engineAndItsSystem");
            SCOMCHV.Text = _rm.GetString("SCOMCHV");
            SCOMACHV.Text = _rm.GetString("SCOMACHV");
            SCHVCHV.Text = _rm.GetString("SCHMCHV");
            SCHMACHV.Text = _rm.GetString("SCHMACHV");
            DVRSUM.Text = _rm.GetString("DVRSUM");
            DVRSUP.Text = _rm.GetString("DVRSUP");
            //CHVNMO.Text = rm.GetString("CHVNMO");
            //CHVNPO.Text = rm.GetString("CHVNPO");

            GBO.Text = _rm.GetString("GGBS");
            radioButton6.Text = _rm.GetString("germ");
            radioButton7.Text = _rm.GetString("nogerm");

            glass.Text = _rm.GetString("glass");
            PVS.Text = _rm.GetString("PVS");
            PPBS.Text = _rm.GetString("PPBS");

            wheelSystem.Text = _rm.GetString("wheelSystem");
            SL.Text = _rm.GetString("SL");


            noise.Text = _rm.GetString("noise");
            VSHA.Text = _rm.GetString("VSHA");

            wheelAndTyres.Text = _rm.GetString("wheelAndTyres");
            OVRP.Text = _rm.GetString("OVRP");
            visualCheck.Text = _rm.GetString("visualCheck");

            radioButton1.Text = _rm.GetString("check");
            radioButton2.Text = _rm.GetString("uncheck");


            label79.Text = _rm.GetString("notAllFields");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gbo = GBOSTATE.None;
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
                switch (gbo)
                {
                    case GBOSTATE.None:
                        if (MessageBox.Show(_rm.GetString("IsGBOActive"), _rm.GetString("warning"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            gbo = GBOSTATE.Active;
                            GBO.Enabled = true;
                            GBO.BackColor = Color.Gold;

                        }
                        else
                        {
                            gbo = GBOSTATE.NonActive;
                            GBO.Enabled = false;
                            GBO.BackColor = SystemColors.Control;
                        }
                        break;
                    case GBOSTATE.Active:
                        gbo = GBOSTATE.Active;
                        GBO.Enabled = true;
                        GBO.BackColor = Color.Gold;
                        break;
                    default:
                        gbo = GBOSTATE.NonActive;
                        GBO.Enabled = false;
                        GBO.BackColor = SystemColors.Control;
                        break;
                }
            }




            PreviewBtn.Enabled = true;
            SaveBtn.Enabled = true;
            PrintBtn.Enabled = true;


            _rows = new List<VisualRow>();


            _normatives = Program.VipAvtoDataSet.GetNormativesFromGroup(comboBox1.SelectedItem.ToString());


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

                            int tag = Convert.ToInt32(control.Tag);


                            if (tag != row.Id) continue;

                            if (tag == 3 && numericUpDown1.Value == 1)
                                continue;
                            if (tag == 4 && (numericUpDown1.Value == 2 || numericUpDown1.Value == 1))
                                continue;


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

                if (lables.Count == 0)
                    continue;


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

                visualCheck.BackColor = Color.Gold;
                groupBox10.BackColor = Color.Gold;


                panel2.BackColor = Color.Gold;

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
                            foreach (Control control3 in ((TableLayoutPanel)control2).Controls)
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
                MessageBox.Show(_rm.GetString("fillFields"), _rm.GetString("error"), MessageBoxButtons.OK,
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
                MessageBox.Show(_rm.GetString("noprotocol"), _rm.GetString("error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SaveProtocolToDb()
        {
            if (panel2.BackColor == Color.Gold || panel2.BackColor == Color.DarkOrange)
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
                gbo = (int)Gbo.NotChecked;
            }
            else
            {
                if (radioButton6.Checked)
                    gbo = (int)Gbo.Germetical;
                else
                {
                    gbo = (int)Gbo.NotGermrtical;
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
            if (maskedTextBox1.BackColor == Color.DarkOrange)
            {
                panel2.BackColor = Color.DarkOrange;

                label79.Text = _rm.GetString("notUniqNumber");
            }
            else if (SomeRequred())
            {
                panel2.BackColor = Color.Gold;
                label79.Text = _rm.GetString("notAllFields");
            }
            else if (AllValide())
            {
                panel2.BackColor = Color.LightGreen;
                string s = _rm.GetString("sucess");
                if (s != null) label79.Text = s.ToUpper();
            }
            else
            {
                {
                    panel2.BackColor = Color.LightPink;
                    string s = _rm.GetString("fail");
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


            if (visualCheck.BackColor == Color.Gold)
                result = true;

            if (groupBox10.BackColor == Color.Gold)
                result = true;

            if (GBO.Enabled)
            {
                if (GBO.BackColor == Color.Gold)
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
                if (Program.VipAvtoDataSet.UniqProtocolNumber(label80.Text + maskedTextBox1.Text))
                {
                    maskedTextBox1.BackColor = Color.LightGreen;
                    UnlockFields();
                }
                else
                {
                    maskedTextBox1.BackColor = Color.DarkOrange;
                }

            }
            else
            {
                maskedTextBox1.BackColor = Color.Gold;
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
                MessageBox.Show(_rm.GetString("fillFields"), _rm.GetString("error"), MessageBoxButtons.OK,
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
                MessageBox.Show(_rm.GetString("noprotocol"), _rm.GetString("error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!SaveProtocolToDb())
            {
                MessageBox.Show(_rm.GetString("fillFields"), _rm.GetString("error"), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
                GBO.BackColor = Color.LightGreen;
            else
            {
                GBO.BackColor = Color.LightPink;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            UnlockFields();
        }
    }

    enum GBOSTATE
    {
        None,
        Active,
        NonActive
    }
}