namespace adovipavto.AddForms
{
    partial class AddMechanicForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddMechanicForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nameTxtBx = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lnTxtBx = new System.Windows.Forms.TextBox();
            this.fnTxtBx = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // nameTxtBx
            // 
            this.errorProvider1.SetError(this.nameTxtBx, resources.GetString("nameTxtBx.Error"));
            resources.ApplyResources(this.nameTxtBx, "nameTxtBx");
            this.nameTxtBx.Name = "nameTxtBx";
            this.nameTxtBx.TextChanged += new System.EventHandler(this.fnTxtBx_TextChanged);
            this.nameTxtBx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameTxtBx_KeyPress);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // lnTxtBx
            // 
            this.errorProvider1.SetError(this.lnTxtBx, resources.GetString("lnTxtBx.Error"));
            resources.ApplyResources(this.lnTxtBx, "lnTxtBx");
            this.lnTxtBx.Name = "lnTxtBx";
            this.lnTxtBx.TextChanged += new System.EventHandler(this.fnTxtBx_TextChanged);
            this.lnTxtBx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameTxtBx_KeyPress);
            // 
            // fnTxtBx
            // 
            this.errorProvider1.SetError(this.fnTxtBx, resources.GetString("fnTxtBx.Error"));
            resources.ApplyResources(this.fnTxtBx, "fnTxtBx");
            this.fnTxtBx.Name = "fnTxtBx";
            this.fnTxtBx.TextChanged += new System.EventHandler(this.fnTxtBx_TextChanged);
            this.fnTxtBx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameTxtBx_KeyPress);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AddMechanicForm
            // 
            this.AcceptButton = this.button1;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fnTxtBx);
            this.Controls.Add(this.lnTxtBx);
            this.Controls.Add(this.nameTxtBx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddMechanicForm";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nameTxtBx;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox fnTxtBx;
        private System.Windows.Forms.TextBox lnTxtBx;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}