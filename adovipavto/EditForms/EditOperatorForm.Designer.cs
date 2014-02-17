namespace adovipavto.EditForms
{
    partial class EditOperatorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditOperatorForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lnTxtBx = new System.Windows.Forms.TextBox();
            this.loginTxtBx = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.passTxtBx = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.nameTxtBx = new System.Windows.Forms.TextBox();
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
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lnTxtBx
            // 
            this.errorProvider1.SetError(this.lnTxtBx, resources.GetString("lnTxtBx.Error"));
            resources.ApplyResources(this.lnTxtBx, "lnTxtBx");
            this.lnTxtBx.Name = "lnTxtBx";
            this.lnTxtBx.TextChanged += new System.EventHandler(this.nameTxtBx_TextChanged);
            this.lnTxtBx.Validated += new System.EventHandler(this.nameTxtBx_Validated);
            // 
            // loginTxtBx
            // 
            this.errorProvider1.SetError(this.loginTxtBx, resources.GetString("loginTxtBx.Error"));
            resources.ApplyResources(this.loginTxtBx, "loginTxtBx");
            this.loginTxtBx.Name = "loginTxtBx";
            this.loginTxtBx.TextChanged += new System.EventHandler(this.nameTxtBx_TextChanged);
            this.loginTxtBx.Validated += new System.EventHandler(this.nameTxtBx_Validated);
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
            // passTxtBx
            // 
            this.errorProvider1.SetError(this.passTxtBx, resources.GetString("passTxtBx.Error"));
            resources.ApplyResources(this.passTxtBx, "passTxtBx");
            this.passTxtBx.Name = "passTxtBx";
            this.passTxtBx.TextChanged += new System.EventHandler(this.nameTxtBx_TextChanged);
            this.passTxtBx.Validated += new System.EventHandler(this.nameTxtBx_Validated);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // nameTxtBx
            // 
            this.errorProvider1.SetError(this.nameTxtBx, resources.GetString("nameTxtBx.Error"));
            resources.ApplyResources(this.nameTxtBx, "nameTxtBx");
            this.nameTxtBx.Name = "nameTxtBx";
            this.nameTxtBx.TextChanged += new System.EventHandler(this.nameTxtBx_TextChanged);
            this.nameTxtBx.Validated += new System.EventHandler(this.nameTxtBx_Validated);
            // 
            // EditOperatorForm
            // 
            this.AcceptButton = this.button1;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.Controls.Add(this.passTxtBx);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.loginTxtBx);
            this.Controls.Add(this.lnTxtBx);
            this.Controls.Add(this.nameTxtBx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EditOperatorForm";
            this.Load += new System.EventHandler(this.AddOperatorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lnTxtBx;
        private System.Windows.Forms.TextBox loginTxtBx;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox passTxtBx;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox nameTxtBx;
    }
}