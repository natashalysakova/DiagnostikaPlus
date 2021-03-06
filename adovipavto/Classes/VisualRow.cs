﻿using System;
using System.Drawing;
using System.Windows.Forms;
using adovipavto.Properties;
using DRandomLib;

namespace adovipavto.Classes
{
    public class VisualRow
    {
        private readonly double _maxval;
        private readonly double _minval;
        private readonly DRandom _random;
        private Label _maxvluelabel;
        private Label _minvallable;
        private TextBox _textbox;

        public VisualRow(NewVipAvtoSet.NormativesRow norma, DRandom random)
        {
            Id = norma.Tag;
            _minval = norma.MinValue;
            _maxval = norma.MaxValue;
            _random = random;

        }

        void _textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (Char.IsDigit (e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsControl(e.KeyChar)) 
                return;

            e.Handled=true;
        }

        public int Id { private set; get; }

        //public PictureBox PictureBox { get; set; }

        public double Value { get; set; }

        public TextBox TextBox
        {
            get { return _textbox; }
            set
            {
                _textbox = value;
                _textbox.Enabled = true;
                SetYellow();
                _textbox.TextChanged += textbox_TextChanged;
                _textbox.KeyPress += _textbox_KeyPress;

                if (Settings.Default.AutoGeneratedData)
                {
                    double diff = (_maxval - _minval) * 0.2;

                    double val;


                    switch (Id)
                    {
                        case 22:
                            val = _random.NextDouble(_minval + (_maxval * 0.6), _maxval - (diff / 2));
                            break;
                        case 11:
                            val = _random.NextDouble(_minval + (diff / 2), _maxval - (_minval * 0.65));
                            break;
                        case 8:
                            val = _random.NextDouble(_minval + (_maxval * 0.75), _maxval - (diff / 2));
                            break;
                        case 9:
                            val = _random.NextDouble(_minval + (_maxval * 0.65), _maxval - (diff / 2));
                            break;
                        case 10:
                            val = _random.NextDouble(_minval + (_maxval * 0.65), _maxval - (diff / 2));
                            break;
                        default:
                            val = _random.NextDouble(_minval + diff, _maxval - diff);
                            break;
                    }


                    _textbox.Text =
                        Math.Round(val, new Normatives().DecimalPoints[Id])
                            .ToString();
                }

            }
        }

        public Label MinLabel
        {
            get { return _minvallable; }
            set
            {
                _minvallable = value;
                _minvallable.Text = _minval.ToString();
            }
        }

        public Label MaxLabel
        {
            get { return _maxvluelabel; }
            set
            {
                _maxvluelabel = value;
                _maxvluelabel.Text = _maxval.ToString();
            }
        }

        private void textbox_TextChanged(object sender, EventArgs e)
        {
            double value;
            bool val = double.TryParse(_textbox.Text, out value);

            if (val)
            {
                Value = value;
                if (_minval <= Value && Value <= _maxval)
                    SetGreen();
                else
                    SetRed();
            }
            else
            {
                if (_textbox.Text == "")
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


        public void SetMinMax()
        {
            _minvallable.Text = _minval.ToString();
            _maxvluelabel.Text = _maxval.ToString();
        }

        private void SetRed()
        {
            //picxtireBox.Image = Properties.Resources.red;
            _textbox.BackColor = Color.LightPink;
        }

        private void SetGreen()
        {
            //picxtireBox.Image = Properties.Resources.green;
            _textbox.BackColor = Color.LightGreen;
        }


        private void SetYellow()
        {
            //picxtireBox.Image = Properties.Resources.yellow;
            _textbox.BackColor = Color.Gold;
        }

        public bool IsValid()
        {
            if (_textbox.BackColor == Color.LightGreen)
                return true;
            return false;
        }

        public bool Requred()
        {
            if (_textbox.BackColor == Color.Gold)
                return true;
            return false;
        }

        public void PressKey(Keys keys)
        {
            _textbox_KeyPress(this._textbox, new KeyPressEventArgs((char) keys));
        }
    }
}