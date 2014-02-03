using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adovipavto.Classes
{
    class VisualRow
    {
        public int Id { private set; get; }

        //public PictureBox PictureBox { get; set; }

        public double Value { get; set; }

        public TextBox TextBox
        {
            get
            {
                return textbox;
            }
            set
            {
                textbox = value;
                textbox.Enabled = true;
                SetYellow();
                textbox.Validated += TextBox_Validated;
                textbox.TextChanged += textbox_TextChanged;
            }
        }

        void textbox_TextChanged(object sender, EventArgs e)
        {
            double value;
            bool val = double.TryParse(textbox.Text, out value);

            if (val)
            {
                    Value = value;
                    if (minval <= Value && Value < maxval)
                        SetGreen();
                    else
                        SetRed();
            }
            else
            {
                if (textbox.Text == "")
                {
                    SetYellow();
                    Value = 0;
                }
                else
                {

                    SetRed();
                }
            }


        }

        private Label minvallable;
        private Label maxvluelabel;
        private TextBox textbox;

        public Label MinLabel
        {
            get
            {
                return minvallable;
            }
            set
            {
                minvallable = value;
                minvallable.Text = minval.ToString();
            }
        }

        public Label MaxLabel
        {
            get
            {
                return maxvluelabel;

            }
            set
            {
                maxvluelabel = value;
                maxvluelabel.Text = maxval.ToString();
            }

        }

        private double minval;
        private double maxval;

        public VisualRow(DataRow norma)
        {
            Id = (int)norma["Tag"];
            minval = (double)norma["MinValue"];
            maxval = (double)norma["MaxValue"];

        }

        void TextBox_Validated(object sender, EventArgs e)
        {

        }


        public void SetMinMax()
        {
            minvallable.Text = minval.ToString();
            maxvluelabel.Text = maxval.ToString();
        }

        void SetRed()
        {
            //picxtireBox.Image = Properties.Resources.red;
            textbox.BackColor = Color.LightPink;
        }

        void SetGreen()
        {
            //picxtireBox.Image = Properties.Resources.green;
            textbox.BackColor = Color.LightGreen;
        }


        void SetYellow()
        {
            //picxtireBox.Image = Properties.Resources.yellow;
            textbox.BackColor = Color.LightGoldenrodYellow;
        }

        public bool IsValid()
        {
            if (textbox.BackColor == Color.LightGreen)
                return true;
            return false;
        }

        public bool Requred()
        {
            if (textbox.BackColor == Color.LightGoldenrodYellow)
                return true;
            return false;
        }

    }
}
