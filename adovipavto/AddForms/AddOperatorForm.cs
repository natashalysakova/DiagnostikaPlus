﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Enums;

namespace adovipavto.AddForms
{
    public partial class AddOperatorForm : Form
    {


        public AddOperatorForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);
            InitializeComponent();
        }


        private void AddOperatorForm_Load(object sender, EventArgs e)
        {
            var arr = new List<string>();
            foreach (Rights item in Enum.GetValues(typeof (Rights)))
            {
                arr.Add(Constants.GetEnumDescription(item));
            }

            roleCmbBx.DataSource = arr;
            roleCmbBx.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(nameTxtBx) == "" && errorProvider1.GetError(lnTxtBx) == "" &&
                errorProvider1.GetError(loginTxtBx) == "" && errorProvider1.GetError(passTxtBx) == "")
            {
                Program.VipAvtoDataSet.AddOperator(nameTxtBx.Text, lnTxtBx.Text, loginTxtBx.Text, passTxtBx.Text,
                    roleCmbBx.SelectedItem.ToString());
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(StringResource.wrongData);
            }
        }

        private void nameTxtBx_TextChanged(object sender, EventArgs e)
        {
            Validate();
        }

        private void nameTxtBx_Validated(object sender, EventArgs e)
        {
            if (((TextBox) sender).Text == "")
                errorProvider1.SetError(((TextBox) sender), StringResource.wrongData);
            else
                errorProvider1.SetError(((TextBox) sender), null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void nameTxtBx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar) && !Char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void loginTxtBx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}