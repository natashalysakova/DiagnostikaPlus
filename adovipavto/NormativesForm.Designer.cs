namespace adovipavto
{
    partial class NormativesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NormativesForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.normativeIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NormTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDGroupDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.normativesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vipAvtoSet = new adovipavto.VipAvtoSet();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.groupSelector = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normativesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vipAvtoSet)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dataGridView1);
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.normativeIDDataGridViewTextBoxColumn,
            this.NormTitle,
            this.minValueDataGridViewTextBoxColumn,
            this.maxValueDataGridViewTextBoxColumn,
            this.iDGroupDataGridViewTextBoxColumn,
            this.Tag});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView1.DataSource = this.normativesBindingSource;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // normativeIDDataGridViewTextBoxColumn
            // 
            this.normativeIDDataGridViewTextBoxColumn.DataPropertyName = "NormativeID";
            resources.ApplyResources(this.normativeIDDataGridViewTextBoxColumn, "normativeIDDataGridViewTextBoxColumn");
            this.normativeIDDataGridViewTextBoxColumn.Name = "normativeIDDataGridViewTextBoxColumn";
            this.normativeIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // NormTitle
            // 
            resources.ApplyResources(this.NormTitle, "NormTitle");
            this.NormTitle.Name = "NormTitle";
            this.NormTitle.ReadOnly = true;
            // 
            // minValueDataGridViewTextBoxColumn
            // 
            this.minValueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.minValueDataGridViewTextBoxColumn.DataPropertyName = "MinValue";
            resources.ApplyResources(this.minValueDataGridViewTextBoxColumn, "minValueDataGridViewTextBoxColumn");
            this.minValueDataGridViewTextBoxColumn.Name = "minValueDataGridViewTextBoxColumn";
            this.minValueDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // maxValueDataGridViewTextBoxColumn
            // 
            this.maxValueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.maxValueDataGridViewTextBoxColumn.DataPropertyName = "MaxValue";
            resources.ApplyResources(this.maxValueDataGridViewTextBoxColumn, "maxValueDataGridViewTextBoxColumn");
            this.maxValueDataGridViewTextBoxColumn.Name = "maxValueDataGridViewTextBoxColumn";
            this.maxValueDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDGroupDataGridViewTextBoxColumn
            // 
            this.iDGroupDataGridViewTextBoxColumn.DataPropertyName = "IDGroup";
            resources.ApplyResources(this.iDGroupDataGridViewTextBoxColumn, "iDGroupDataGridViewTextBoxColumn");
            this.iDGroupDataGridViewTextBoxColumn.Name = "iDGroupDataGridViewTextBoxColumn";
            this.iDGroupDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Tag
            // 
            this.Tag.DataPropertyName = "Tag";
            resources.ApplyResources(this.Tag, "Tag");
            this.Tag.Name = "Tag";
            this.Tag.ReadOnly = true;
            // 
            // normativesBindingSource
            // 
            this.normativesBindingSource.DataMember = "Normatives";
            this.normativesBindingSource.DataSource = this.vipAvtoSet;
            // 
            // vipAvtoSet
            // 
            this.vipAvtoSet.DataSetName = "VipAvtoSet";
            this.vipAvtoSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.groupSelector,
            this.toolStripSeparator2});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::adovipavto.Properties.Resources.plus;
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::adovipavto.Properties.Resources.edit;
            resources.ApplyResources(this.toolStripButton2, "toolStripButton2");
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::adovipavto.Properties.Resources.minus;
            resources.ApplyResources(this.toolStripButton3, "toolStripButton3");
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // groupSelector
            // 
            this.groupSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.groupSelector, "groupSelector");
            this.groupSelector.Name = "groupSelector";
            this.groupSelector.SelectedIndexChanged += new System.EventHandler(this.groupSelector_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.изменитьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            resources.ApplyResources(this.добавитьToolStripMenuItem, "добавитьToolStripMenuItem");
            this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click);
            // 
            // изменитьToolStripMenuItem
            // 
            resources.ApplyResources(this.изменитьToolStripMenuItem, "изменитьToolStripMenuItem");
            this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            this.изменитьToolStripMenuItem.Click += new System.EventHandler(this.изменитьToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            resources.ApplyResources(this.удалитьToolStripMenuItem, "удалитьToolStripMenuItem");
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // NormativesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "NormativesForm";
            this.Load += new System.EventHandler(this.NormativesForm_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normativesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vipAvtoSet)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripComboBox groupSelector;
        private System.Windows.Forms.BindingSource normativesBindingSource;
        private VipAvtoSet vipAvtoSet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn normativeIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NormTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn minValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDGroupDataGridViewTextBoxColumn;
        private new System.Windows.Forms.DataGridViewTextBoxColumn Tag;
    }
}