﻿namespace adovipavto
{
    partial class Search
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Search));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label80 = new System.Windows.Forms.Label();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idProtocolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blankNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.protocolsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.newVipAvtoSet = new adovipavto.NewVipAvtoSet();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.secondDate = new System.Windows.Forms.DateTimePicker();
            this.firstDate = new System.Windows.Forms.DateTimePicker();
            this.printPreviewControl2 = new System.Windows.Forms.PrintPreviewControl();
            this.protocolsTableAdapter = new adovipavto.NewVipAvtoSetTableAdapters.ProtocolsTableAdapter();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.protocolsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newVipAvtoSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label80);
            this.tabPage1.Controls.Add(this.maskedTextBox1);
            this.tabPage1.Controls.Add(this.printPreviewControl1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label80
            // 
            resources.ApplyResources(this.label80, "label80");
            this.label80.Name = "label80";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            resources.ApplyResources(this.maskedTextBox1, "maskedTextBox1");
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.TextChanged += new System.EventHandler(this.maskedTextBox1_TextChanged);
            // 
            // printPreviewControl1
            // 
            resources.ApplyResources(this.printPreviewControl1, "printPreviewControl1");
            this.printPreviewControl1.Name = "printPreviewControl1";
            this.printPreviewControl1.UseAntiAlias = true;
            this.printPreviewControl1.Click += new System.EventHandler(this.printPreviewControl1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.radioButton5);
            this.tabPage2.Controls.Add(this.radioButton4);
            this.tabPage2.Controls.Add(this.radioButton3);
            this.tabPage2.Controls.Add(this.radioButton2);
            this.tabPage2.Controls.Add(this.radioButton1);
            this.tabPage2.Controls.Add(this.secondDate);
            this.tabPage2.Controls.Add(this.firstDate);
            this.tabPage2.Controls.Add(this.printPreviewControl2);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idProtocolDataGridViewTextBoxColumn,
            this.blankNumberDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.protocolsBindingSource;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // idProtocolDataGridViewTextBoxColumn
            // 
            this.idProtocolDataGridViewTextBoxColumn.DataPropertyName = "IdProtocol";
            resources.ApplyResources(this.idProtocolDataGridViewTextBoxColumn, "idProtocolDataGridViewTextBoxColumn");
            this.idProtocolDataGridViewTextBoxColumn.Name = "idProtocolDataGridViewTextBoxColumn";
            this.idProtocolDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // blankNumberDataGridViewTextBoxColumn
            // 
            this.blankNumberDataGridViewTextBoxColumn.DataPropertyName = "BlankNumber";
            resources.ApplyResources(this.blankNumberDataGridViewTextBoxColumn, "blankNumberDataGridViewTextBoxColumn");
            this.blankNumberDataGridViewTextBoxColumn.Name = "blankNumberDataGridViewTextBoxColumn";
            this.blankNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            resources.ApplyResources(this.dateDataGridViewTextBoxColumn, "dateDataGridViewTextBoxColumn");
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // protocolsBindingSource
            // 
            this.protocolsBindingSource.DataMember = "Protocols";
            this.protocolsBindingSource.DataSource = this.newVipAvtoSet;
            // 
            // newVipAvtoSet
            // 
            this.newVipAvtoSet.DataSetName = "NewVipAvtoSet";
            this.newVipAvtoSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // radioButton5
            // 
            resources.ApplyResources(this.radioButton5, "radioButton5");
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton4
            // 
            resources.ApplyResources(this.radioButton4, "radioButton4");
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            resources.ApplyResources(this.radioButton3, "radioButton3");
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // secondDate
            // 
            resources.ApplyResources(this.secondDate, "secondDate");
            this.secondDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.secondDate.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.secondDate.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.secondDate.Name = "secondDate";
            this.secondDate.ValueChanged += new System.EventHandler(this.firstDate_ValueChanged);
            // 
            // firstDate
            // 
            resources.ApplyResources(this.firstDate, "firstDate");
            this.firstDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.firstDate.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.firstDate.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.firstDate.Name = "firstDate";
            this.firstDate.ValueChanged += new System.EventHandler(this.firstDate_ValueChanged);
            // 
            // printPreviewControl2
            // 
            resources.ApplyResources(this.printPreviewControl2, "printPreviewControl2");
            this.printPreviewControl2.Name = "printPreviewControl2";
            this.printPreviewControl2.UseAntiAlias = true;
            this.printPreviewControl2.Click += new System.EventHandler(this.printPreviewControl1_Click);
            // 
            // protocolsTableAdapter
            // 
            this.protocolsTableAdapter.ClearBeforeFill = true;
            // 
            // Search
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "Search";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Search_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.protocolsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newVipAvtoSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PrintPreviewControl printPreviewControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.DateTimePicker secondDate;
        private System.Windows.Forms.DateTimePicker firstDate;
        private System.Windows.Forms.PrintPreviewControl printPreviewControl2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private NewVipAvtoSet newVipAvtoSet;
        private System.Windows.Forms.BindingSource protocolsBindingSource;
        private NewVipAvtoSetTableAdapters.ProtocolsTableAdapter protocolsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProtocolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blankNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
    }
}