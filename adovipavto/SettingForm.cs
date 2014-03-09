﻿using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto
{
    public partial class SettingForm : Form
    {
        public SettingForm()
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
            if (radioButton1.Checked)
                Settings.Instance.TmpLanguage = "ru-RU";
            else
                Settings.Instance.TmpLanguage = "uk-UA";

            Settings.Instance.AutoGeneratedData = radioButton3.Checked;

            string oldPath = Settings.Instance.FilesDirectory;
            if (oldPath != textBox1.Text)
            {
                if (oldPath == "")
                    oldPath = Directory.GetCurrentDirectory();

                foreach (string file in Directory.GetFiles(oldPath))
                {
                    if (file.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries).Last() == "xml")
                    {
                        string newFile = textBox1.Text +
                                         file.Split(new[] {'\\'}, StringSplitOptions.RemoveEmptyEntries).Last();
                        if (!File.Exists(newFile))
                            File.Move(file, newFile);
                    }
                }
            }


            Settings.Instance.FilesDirectory = textBox1.Text;

            Settings.Instance.BackupDirectory = textBox2.Text;

            Settings.Instance.Save();

            DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath.Last() == '\\')
                    textBox1.Text = folderBrowserDialog1.SelectedPath;
                else
                {
                    textBox1.Text = folderBrowserDialog1.SelectedPath + @"\";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox2.Text = folderBrowserDialog1.SelectedPath;
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            if (Settings.Instance.Language == "ru-RU")
                radioButton1.Checked = true;
            else
            {
                radioButton2.Checked = true;
            }

            if (Settings.Instance.AutoGeneratedData)
                radioButton3.Checked = true;
            else
                radioButton4.Checked = true;

            textBox1.Text = Settings.Instance.FilesDirectory;

            textBox2.Text = Settings.Instance.BackupDirectory;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new ServerSetting().ShowDialog();
        }
    }
}