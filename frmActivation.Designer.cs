namespace WooCommerceAddOn
{
    partial class frmActivation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmActivation));
            this.label1 = new System.Windows.Forms.Label();
            this.txtComName = new System.Windows.Forms.TextBox();
            this.txtComPhone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtComEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboProtocols = new System.Windows.Forms.ComboBox();
            this.txtWcConsumerSecret = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWcConsumerKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWcUrl = new System.Windows.Forms.TextBox();
            this.txtAbssPass = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAbssID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoUS = new System.Windows.Forms.RadioButton();
            this.rdoEN = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.lblExeLocation = new System.Windows.Forms.Label();
            this.lblFileLocation = new System.Windows.Forms.Label();
            this.btnExeLocation = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnFileLocation = new System.Windows.Forms.Button();
            this.abssFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errProviderApp = new System.Windows.Forms.ErrorProvider(this.components);
            this.abssExeDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProviderApp)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // txtComName
            // 
            this.txtComName.Location = new System.Drawing.Point(88, 35);
            this.txtComName.Name = "txtComName";
            this.txtComName.Size = new System.Drawing.Size(537, 20);
            this.txtComName.TabIndex = 1;
            // 
            // txtComPhone
            // 
            this.txtComPhone.Location = new System.Drawing.Point(88, 73);
            this.txtComPhone.Name = "txtComPhone";
            this.txtComPhone.Size = new System.Drawing.Size(537, 20);
            this.txtComPhone.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Phone:";
            // 
            // txtComEmail
            // 
            this.txtComEmail.Location = new System.Drawing.Point(88, 111);
            this.txtComEmail.Name = "txtComEmail";
            this.txtComEmail.Size = new System.Drawing.Size(537, 20);
            this.txtComEmail.TabIndex = 5;
            this.txtComEmail.Leave += new System.EventHandler(this.txtComEmail_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Email:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "URL:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtComName);
            this.groupBox1.Controls.Add(this.txtComPhone);
            this.groupBox1.Controls.Add(this.txtComEmail);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(654, 163);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Company Information";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboProtocols);
            this.groupBox2.Controls.Add(this.txtWcConsumerSecret);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtWcConsumerKey);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtWcUrl);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(654, 209);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WooCommerce Information";
            // 
            // comboProtocols
            // 
            this.comboProtocols.FormattingEnabled = true;
            this.comboProtocols.Items.AddRange(new object[] {
            "https",
            "http"});
            this.comboProtocols.Location = new System.Drawing.Point(109, 36);
            this.comboProtocols.Name = "comboProtocols";
            this.comboProtocols.Size = new System.Drawing.Size(121, 21);
            this.comboProtocols.TabIndex = 12;
            // 
            // txtWcConsumerSecret
            // 
            this.txtWcConsumerSecret.Location = new System.Drawing.Point(109, 155);
            this.txtWcConsumerSecret.Name = "txtWcConsumerSecret";
            this.txtWcConsumerSecret.Size = new System.Drawing.Size(537, 20);
            this.txtWcConsumerSecret.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Consumer Secret:";
            // 
            // txtWcConsumerKey
            // 
            this.txtWcConsumerKey.Location = new System.Drawing.Point(109, 117);
            this.txtWcConsumerKey.Name = "txtWcConsumerKey";
            this.txtWcConsumerKey.Size = new System.Drawing.Size(537, 20);
            this.txtWcConsumerKey.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Consumer Key:";
            // 
            // txtWcUrl
            // 
            this.txtWcUrl.Location = new System.Drawing.Point(109, 74);
            this.txtWcUrl.Name = "txtWcUrl";
            this.txtWcUrl.Size = new System.Drawing.Size(537, 20);
            this.txtWcUrl.TabIndex = 7;
            // 
            // txtAbssPass
            // 
            this.txtAbssPass.Location = new System.Drawing.Point(88, 163);
            this.txtAbssPass.Name = "txtAbssPass";
            this.txtAbssPass.PasswordChar = '*';
            this.txtAbssPass.Size = new System.Drawing.Size(537, 20);
            this.txtAbssPass.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Password:";
            // 
            // txtAbssID
            // 
            this.txtAbssID.Location = new System.Drawing.Point(88, 121);
            this.txtAbssID.Name = "txtAbssID";
            this.txtAbssID.Size = new System.Drawing.Size(537, 20);
            this.txtAbssID.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "User ID:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "File Location:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoUS);
            this.groupBox3.Controls.Add(this.rdoEN);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.lblExeLocation);
            this.groupBox3.Controls.Add(this.lblFileLocation);
            this.groupBox3.Controls.Add(this.btnExeLocation);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtAbssPass);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.btnFileLocation);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtAbssID);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(12, 430);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(654, 251);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ABSS Information";
            // 
            // rdoUS
            // 
            this.rdoUS.AutoSize = true;
            this.rdoUS.Location = new System.Drawing.Point(204, 207);
            this.rdoUS.Name = "rdoUS";
            this.rdoUS.Size = new System.Drawing.Size(83, 17);
            this.rdoUS.TabIndex = 24;
            this.rdoUS.TabStop = true;
            this.rdoUS.Text = "US (M/D/Y)";
            this.rdoUS.UseVisualStyleBackColor = true;
            this.rdoUS.CheckedChanged += new System.EventHandler(this.rdoUS_CheckedChanged);
            // 
            // rdoEN
            // 
            this.rdoEN.AutoSize = true;
            this.rdoEN.Checked = true;
            this.rdoEN.Location = new System.Drawing.Point(88, 207);
            this.rdoEN.Name = "rdoEN";
            this.rdoEN.Size = new System.Drawing.Size(83, 17);
            this.rdoEN.TabIndex = 23;
            this.rdoEN.TabStop = true;
            this.rdoEN.Text = "EN (D/M/Y)";
            this.rdoEN.UseVisualStyleBackColor = true;
            this.rdoEN.CheckedChanged += new System.EventHandler(this.rdoEN_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 207);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Date Format:";
            // 
            // lblExeLocation
            // 
            this.lblExeLocation.AutoSize = true;
            this.lblExeLocation.Location = new System.Drawing.Point(170, 86);
            this.lblExeLocation.Name = "lblExeLocation";
            this.lblExeLocation.Size = new System.Drawing.Size(0, 13);
            this.lblExeLocation.TabIndex = 21;
            // 
            // lblFileLocation
            // 
            this.lblFileLocation.AutoSize = true;
            this.lblFileLocation.Location = new System.Drawing.Point(170, 44);
            this.lblFileLocation.Name = "lblFileLocation";
            this.lblFileLocation.Size = new System.Drawing.Size(0, 13);
            this.lblFileLocation.TabIndex = 20;
            // 
            // btnExeLocation
            // 
            this.btnExeLocation.Location = new System.Drawing.Point(88, 76);
            this.btnExeLocation.Name = "btnExeLocation";
            this.btnExeLocation.Size = new System.Drawing.Size(75, 23);
            this.btnExeLocation.TabIndex = 19;
            this.btnExeLocation.Text = "Open...";
            this.btnExeLocation.UseVisualStyleBackColor = true;
            this.btnExeLocation.Click += new System.EventHandler(this.btnExeLocation_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Exe Location:";
            // 
            // btnFileLocation
            // 
            this.btnFileLocation.Location = new System.Drawing.Point(88, 35);
            this.btnFileLocation.Name = "btnFileLocation";
            this.btnFileLocation.Size = new System.Drawing.Size(75, 23);
            this.btnFileLocation.TabIndex = 13;
            this.btnFileLocation.Text = "Open...";
            this.btnFileLocation.UseVisualStyleBackColor = true;
            this.btnFileLocation.Click += new System.EventHandler(this.btnFileLocation_Click);
            // 
            // abssFileDialog
            // 
            this.abssFileDialog.FileName = "openFileDialog1";
            this.abssFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.abssFileDialog_FileOk);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(16, 704);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(591, 704);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errProviderApp
            // 
            this.errProviderApp.ContainerControl = this;
            // 
            // abssExeDialog
            // 
            this.abssExeDialog.FileName = "openFileDialog1";
            this.abssExeDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.abssExeDialog_FileOk);
            // 
            // frmActivation
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 754);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmActivation";
            this.Text = "Activation";
            this.Load += new System.EventHandler(this.frmActivation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProviderApp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtComName;
        private System.Windows.Forms.TextBox txtComPhone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtComEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAbssPass;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAbssID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtWcConsumerSecret;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWcConsumerKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWcUrl;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnFileLocation;
        private System.Windows.Forms.OpenFileDialog abssFileDialog;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errProviderApp;
        private System.Windows.Forms.Button btnExeLocation;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.OpenFileDialog abssExeDialog;
        private System.Windows.Forms.ComboBox comboProtocols;
        private System.Windows.Forms.Label lblExeLocation;
        private System.Windows.Forms.Label lblFileLocation;
        private System.Windows.Forms.RadioButton rdoEN;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rdoUS;
    }
}