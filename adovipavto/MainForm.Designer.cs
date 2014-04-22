using System.Windows.Forms;

namespace adovipavto
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idProtocolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blankNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nextDataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mechanicIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operatorIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.visualCheckDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.gBODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.protocolsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.newVipAvtoSet = new adovipavto.NewVipAvtoSet();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.cpyBtn = new System.Windows.Forms.ToolStripButton();
            this.srchBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton18 = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.просмотрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.печатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.protocolsTableAdapter = new adovipavto.NewVipAvtoSetTableAdapters.ProtocolsTableAdapter();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.protocolsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newVipAvtoSet)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.BottomToolStripPanel, "toolStripContainer1.BottomToolStripPanel");
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dataGridView1);
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.LeftToolStripPanel, "toolStripContainer1.LeftToolStripPanel");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.RightToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.RightToolStripPanel, "toolStripContainer1.RightToolStripPanel");
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.TopToolStripPanel, "toolStripContainer1.TopToolStripPanel");
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            // 
            // dataGridView1
            // 
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idProtocolDataGridViewTextBoxColumn,
            this.blankNumberDataGridViewTextBoxColumn,
            this.groupIdDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.nextDataDataGridViewTextBoxColumn,
            this.mechanicIdDataGridViewTextBoxColumn,
            this.operatorIdDataGridViewTextBoxColumn,
            this.resultDataGridViewCheckBoxColumn,
            this.visualCheckDataGridViewCheckBoxColumn,
            this.gBODataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.protocolsBindingSource;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
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
            // groupIdDataGridViewTextBoxColumn
            // 
            this.groupIdDataGridViewTextBoxColumn.DataPropertyName = "GroupId";
            resources.ApplyResources(this.groupIdDataGridViewTextBoxColumn, "groupIdDataGridViewTextBoxColumn");
            this.groupIdDataGridViewTextBoxColumn.Name = "groupIdDataGridViewTextBoxColumn";
            this.groupIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            resources.ApplyResources(this.dateDataGridViewTextBoxColumn, "dateDataGridViewTextBoxColumn");
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nextDataDataGridViewTextBoxColumn
            // 
            this.nextDataDataGridViewTextBoxColumn.DataPropertyName = "NextData";
            resources.ApplyResources(this.nextDataDataGridViewTextBoxColumn, "nextDataDataGridViewTextBoxColumn");
            this.nextDataDataGridViewTextBoxColumn.Name = "nextDataDataGridViewTextBoxColumn";
            this.nextDataDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mechanicIdDataGridViewTextBoxColumn
            // 
            this.mechanicIdDataGridViewTextBoxColumn.DataPropertyName = "MechanicId";
            resources.ApplyResources(this.mechanicIdDataGridViewTextBoxColumn, "mechanicIdDataGridViewTextBoxColumn");
            this.mechanicIdDataGridViewTextBoxColumn.Name = "mechanicIdDataGridViewTextBoxColumn";
            this.mechanicIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // operatorIdDataGridViewTextBoxColumn
            // 
            this.operatorIdDataGridViewTextBoxColumn.DataPropertyName = "OperatorId";
            resources.ApplyResources(this.operatorIdDataGridViewTextBoxColumn, "operatorIdDataGridViewTextBoxColumn");
            this.operatorIdDataGridViewTextBoxColumn.Name = "operatorIdDataGridViewTextBoxColumn";
            this.operatorIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // resultDataGridViewCheckBoxColumn
            // 
            this.resultDataGridViewCheckBoxColumn.DataPropertyName = "Result";
            resources.ApplyResources(this.resultDataGridViewCheckBoxColumn, "resultDataGridViewCheckBoxColumn");
            this.resultDataGridViewCheckBoxColumn.Name = "resultDataGridViewCheckBoxColumn";
            this.resultDataGridViewCheckBoxColumn.ReadOnly = true;
            this.resultDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.resultDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // visualCheckDataGridViewCheckBoxColumn
            // 
            this.visualCheckDataGridViewCheckBoxColumn.DataPropertyName = "VisualCheck";
            resources.ApplyResources(this.visualCheckDataGridViewCheckBoxColumn, "visualCheckDataGridViewCheckBoxColumn");
            this.visualCheckDataGridViewCheckBoxColumn.Name = "visualCheckDataGridViewCheckBoxColumn";
            this.visualCheckDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // gBODataGridViewTextBoxColumn
            // 
            this.gBODataGridViewTextBoxColumn.DataPropertyName = "GBO";
            resources.ApplyResources(this.gBODataGridViewTextBoxColumn, "gBODataGridViewTextBoxColumn");
            this.gBODataGridViewTextBoxColumn.Name = "gBODataGridViewTextBoxColumn";
            this.gBODataGridViewTextBoxColumn.ReadOnly = true;
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
            // toolStrip2
            // 
            resources.ApplyResources(this.toolStrip2, "toolStrip2");
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton10,
            this.cpyBtn,
            this.srchBtn,
            this.toolStripSeparator3,
            this.toolStripButton11,
            this.toolStripButton12,
            this.toolStripButton13,
            this.toolStripButton14,
            this.toolStripSeparator4,
            this.toolStripButton15,
            this.toolStripButton16,
            this.toolStripButton17,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolStripButton18});
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Stretch = true;
            // 
            // toolStripButton10
            // 
            resources.ApplyResources(this.toolStripButton10, "toolStripButton10");
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = global::adovipavto.Properties.Resources.protocol1;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // cpyBtn
            // 
            resources.ApplyResources(this.cpyBtn, "cpyBtn");
            this.cpyBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cpyBtn.Image = global::adovipavto.Properties.Resources.copy;
            this.cpyBtn.Name = "cpyBtn";
            this.cpyBtn.Click += new System.EventHandler(this.cpyBtn_Click);
            // 
            // srchBtn
            // 
            resources.ApplyResources(this.srchBtn, "srchBtn");
            this.srchBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.srchBtn.Image = global::adovipavto.Properties.Resources.search;
            this.srchBtn.Name = "srchBtn";
            this.srchBtn.Click += new System.EventHandler(this.srchBtn_Click);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // toolStripButton11
            // 
            resources.ApplyResources(this.toolStripButton11, "toolStripButton11");
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = global::adovipavto.Properties.Resources.GROUP;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
            // 
            // toolStripButton12
            // 
            resources.ApplyResources(this.toolStripButton12, "toolStripButton12");
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = global::adovipavto.Properties.Resources.NORMARIVES;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // toolStripButton13
            // 
            resources.ApplyResources(this.toolStripButton13, "toolStripButton13");
            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton13.Image = global::adovipavto.Properties.Resources.oper;
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Click += new System.EventHandler(this.toolStripButton13_Click);
            // 
            // toolStripButton14
            // 
            resources.ApplyResources(this.toolStripButton14, "toolStripButton14");
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton14.Image = global::adovipavto.Properties.Resources.mech;
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Click += new System.EventHandler(this.toolStripButton14_Click);
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // toolStripButton15
            // 
            resources.ApplyResources(this.toolStripButton15, "toolStripButton15");
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton15.Image = global::adovipavto.Properties.Resources.setting;
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Click += new System.EventHandler(this.toolStripButton15_Click);
            // 
            // toolStripButton16
            // 
            resources.ApplyResources(this.toolStripButton16, "toolStripButton16");
            this.toolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton16.Image = global::adovipavto.Properties.Resources.help;
            this.toolStripButton16.Name = "toolStripButton16";
            this.toolStripButton16.Click += new System.EventHandler(this.toolStripButton16_Click);
            // 
            // toolStripButton17
            // 
            resources.ApplyResources(this.toolStripButton17, "toolStripButton17");
            this.toolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton17.Image = global::adovipavto.Properties.Resources.about;
            this.toolStripButton17.Name = "toolStripButton17";
            this.toolStripButton17.Click += new System.EventHandler(this.toolStripButton17_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripButton1
            // 
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::adovipavto.Properties.Resources.switchUserBtn;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton18
            // 
            resources.ApplyResources(this.toolStripButton18, "toolStripButton18");
            this.toolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton18.Image = global::adovipavto.Properties.Resources.exit;
            this.toolStripButton18.Name = "toolStripButton18";
            this.toolStripButton18.Click += new System.EventHandler(this.toolStripButton18_Click);
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.просмотрToolStripMenuItem,
            this.печатьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // просмотрToolStripMenuItem
            // 
            resources.ApplyResources(this.просмотрToolStripMenuItem, "просмотрToolStripMenuItem");
            this.просмотрToolStripMenuItem.Name = "просмотрToolStripMenuItem";
            this.просмотрToolStripMenuItem.Click += new System.EventHandler(this.просмотрToolStripMenuItem_Click);
            // 
            // печатьToolStripMenuItem
            // 
            resources.ApplyResources(this.печатьToolStripMenuItem, "печатьToolStripMenuItem");
            this.печатьToolStripMenuItem.Name = "печатьToolStripMenuItem";
            this.печатьToolStripMenuItem.Click += new System.EventHandler(this.печатьToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // protocolsTableAdapter
            // 
            this.protocolsTableAdapter.ClearBeforeFill = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.protocolsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newVipAvtoSet)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripButton toolStripButton13;
        private System.Windows.Forms.ToolStripButton toolStripButton14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton15;
        private System.Windows.Forms.ToolStripButton toolStripButton16;
        private System.Windows.Forms.ToolStripButton toolStripButton17;
        private System.Windows.Forms.ToolStripButton toolStripButton18;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ToolStripButton cpyBtn;
        private ToolStripButton srchBtn;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem просмотрToolStripMenuItem;
        private ToolStripMenuItem печатьToolStripMenuItem;
        private ToolStripButton toolStripButton1;
        private DataGridView dataGridView1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Timer timer1;
        private NewVipAvtoSet newVipAvtoSet;
        private BindingSource protocolsBindingSource;
        private NewVipAvtoSetTableAdapters.ProtocolsTableAdapter protocolsTableAdapter;
        private DataGridViewTextBoxColumn idProtocolDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn blankNumberDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn groupIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nextDataDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn mechanicIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn operatorIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn resultDataGridViewCheckBoxColumn;
        private DataGridViewCheckBoxColumn visualCheckDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn gBODataGridViewTextBoxColumn;
        private ToolStripStatusLabel toolStripStatusLabel2;
    }
}