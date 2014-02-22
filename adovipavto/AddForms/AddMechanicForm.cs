﻿using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto.AddForms
{
    public partial class AddMechanicForm : Form
    {
        readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        public AddMechanicForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(nameTxtBx) == "" && errorProvider1.GetError(lnTxtBx) == "" &&
                errorProvider1.GetError(fnTxtBx) == "")
            {
                Program.VipAvtoDataSet.AddMechanic(nameTxtBx.Text, lnTxtBx.Text, fnTxtBx.Text);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(_rm.GetString("wrongData"));
            }
        }

        private void fnTxtBx_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox) sender).Text == "")
                errorProvider1.SetError((TextBox)sender, _rm.GetString("wrongData"));
            else
            {
                errorProvider1.SetError((TextBox) sender, null);
            }
        }


        private void nameTxtBx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar) && !Char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }
    }
}