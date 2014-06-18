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
using adovipavto.Properties;
using  DRandomLib;

namespace adovipavto.AddForms
{
    public partial class AddProtocol : Form
    {
        private readonly DRandom _random = new DRandom();

        private readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());

        private readonly VipAvtoDBDataSet _set;
        private GBOSTATE _gbo;


        private int _newProtocolId;
        private DataRow[] _normatives;
        private List<VisualRow> _rows;

        public AddProtocol(VipAvtoDBDataSet set)
        {
            _set = set;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);

            InitializeComponent();

            _gbo = GBOSTATE.None;
            _rows = new List<VisualRow>();
        }

        private void AddProtocol_Load(object sender, EventArgs e)
        {
            CleanFields();
            UpdateFormLables();

            string[] groups = (
                from VipAvtoDBDataSet.GroupsRow item in _set.Groups.Rows
                select item.Title).ToArray();

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
                from VipAvtoDBDataSet.MechanicsRow item in _set.Tables[Constants.MechanicsTableName].Rows
                where item.State != (int) State.Unemployed
                select
                    _set.GetShortMechanicName(item.IdMechanic)
                ).ToArray();
            if (mechanics.Length == 0)
            {
                MessageBox.Show(_rm.GetString("nomech"),
                    _rm.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            comboBox2.DataSource = mechanics;
            comboBox2.Text = Settings.Default.LastUsedMechanic;

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
            ORTS4.Text = _rm.GetString("ORTS4");

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
            _gbo = GBOSTATE.None;
            UnlockFields();
        }

        private void UnlockFields()
        {
            if (comboBox1.SelectedIndex == -1) return;
            if (!maskedTextBox1.MaskCompleted) return;

            panel1.Enabled = true;
            CleanFields();

            visualCheck.BackColor = SystemColors.Control;


            if (_set.GroupWithGasEngine(comboBox1.SelectedItem.ToString()))
            {
                switch (_gbo)
                {
                    case GBOSTATE.None:
                        if (MessageBox.Show(_rm.GetString("IsGBOActive"), _rm.GetString("warning"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            _gbo = GBOSTATE.Active;
                            GBO.Enabled = true;
                            GBO.BackColor = Color.Gold;
                        }
                        else
                        {
                            _gbo = GBOSTATE.NonActive;
                            GBO.Enabled = false;
                            GBO.BackColor = SystemColors.Control;
                        }
                        break;
                    case GBOSTATE.Active:
                        _gbo = GBOSTATE.Active;
                        GBO.Enabled = true;
                        GBO.BackColor = Color.Gold;
                        break;
                    default:
                        _gbo = GBOSTATE.NonActive;
                        GBO.Enabled = false;
                        GBO.BackColor = SystemColors.Control;
                        break;
                }
            }
            else
            {
                _gbo = GBOSTATE.NonActive;
                GBO.Enabled = false;
                GBO.BackColor = SystemColors.Control;
            }


            PreviewBtn.Enabled = true;
            SaveBtn.Enabled = true;
            PrintBtn.Enabled = true;


            _rows = new List<VisualRow>();


            _normatives = _set.GetNormativesFromGroup(comboBox1.SelectedItem.ToString());


            foreach (VipAvtoDBDataSet.NormativesRow normative in _normatives)
            {
                var row = new VisualRow(normative, _random);

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
                            if (tag == 23 &&
                                (numericUpDown1.Value == 1 || numericUpDown1.Value == 2 || numericUpDown1.Value == 3))
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


            if (Settings.Default.AutoGeneratedData)
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
                MessageBox.Show(_rm.GetString("fillFields"), _rm.GetString("error"), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (_newProtocolId != -1)
            {
                var protocol =
                    (VipAvtoDBDataSet.ProtocolsRow) _set.GetRowById(Constants.ProtocolsTableName, _newProtocolId);
                VipAvtoDBDataSet.MesuresRow[] mesures = protocol.GetMesuresRows();

                new ProtocolReportForm(protocol, mesures, _set, true).ShowDialog();

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


            Settings.Default.LastUsedMechanic = comboBox2.SelectedItem.ToString();
            Settings.Default.Save();



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

            byte gbo;

            if (GBO.Enabled == false)
            {
                gbo = (byte)Gbo.NotChecked;
            }
            else
            {
                if (radioButton6.Checked)
                    gbo = (byte) Gbo.Germetical;
                else
                {
                    gbo = (byte)Gbo.NotGermrtical;
                }
            }


            _newProtocolId = _set.AddProtocol(label80.Text + maskedTextBox1.Text,
                comboBox2.SelectedItem.ToString(), dateTimePicker1.Value, _set.GetGroupId(comboBox1.SelectedItem.ToString()),
                result, nexDateTime, radioButton1.Checked, gbo);


            if (pictureBox28.Tag != null)
            {
                _set.AddPhoto(pictureBox28.Image, _newProtocolId);
            }

            int groupid = _set.GetGroupId(comboBox1.SelectedItem.ToString());

            foreach (VisualRow row in _rows)
            {
                _set.AddMesure(row.Id, row.Value, _newProtocolId, groupid);
            }
            _set.Update(typeof (VipAvtoDBDataSet.MesuresRow));
            _set.AcceptChanges();

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
                if (_set.UniqProtocolNumber(label80.Text + maskedTextBox1.Text))
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


            if (_newProtocolId != -1)
            {
                var protocol =
                    (VipAvtoDBDataSet.ProtocolsRow) _set.GetRowById(Constants.ProtocolsTableName, _newProtocolId);
                VipAvtoDBDataSet.MesuresRow[] mesures = protocol.GetMesuresRows();

                new ProtocolReportForm(protocol, mesures, _set).ShowDialog();
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
                ((PictureBox) sender).Image = Image.FromFile(openFileDialog1.FileName);
                ((PictureBox) sender).Tag = openFileDialog1.FileName;
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

        private void button1_Click_2(object sender, EventArgs e)
        {
            pictureBox28.Image = Properties.Resources.openfoto;
            pictureBox28.Tag = null;
        }
    }

    internal enum GBOSTATE
    {
        None,
        Active,
        NonActive
    }
}