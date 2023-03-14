namespace WooCommerceAddOn
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnGetOrder = new System.Windows.Forms.Button();
            this.btnGetItem = new System.Windows.Forms.Button();
            this.btnGetCustomer = new System.Windows.Forms.Button();
            this.btnEditRegisteredData = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLicense = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGetAbssProduct = new System.Windows.Forms.Button();
            this.lblSchedule = new System.Windows.Forms.Label();
            this.comIntervalDays = new System.Windows.Forms.ComboBox();
            this.comIntervalTimes = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSaveSchedule = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetOrder
            // 
            this.btnGetOrder.BackColor = System.Drawing.Color.Honeydew;
            this.btnGetOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetOrder.Location = new System.Drawing.Point(16, 14);
            this.btnGetOrder.Name = "btnGetOrder";
            this.btnGetOrder.Size = new System.Drawing.Size(181, 23);
            this.btnGetOrder.TabIndex = 0;
            this.btnGetOrder.Text = "Retrieve WooCommerce Order";
            this.btnGetOrder.UseVisualStyleBackColor = false;
            this.btnGetOrder.Click += new System.EventHandler(this.btnGetOrder_Click);
            // 
            // btnGetItem
            // 
            this.btnGetItem.BackColor = System.Drawing.Color.Honeydew;
            this.btnGetItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetItem.Location = new System.Drawing.Point(16, 58);
            this.btnGetItem.Name = "btnGetItem";
            this.btnGetItem.Size = new System.Drawing.Size(181, 23);
            this.btnGetItem.TabIndex = 2;
            this.btnGetItem.Text = "Retrieve WooCommerce Product";
            this.btnGetItem.UseVisualStyleBackColor = false;
            this.btnGetItem.Click += new System.EventHandler(this.btnGetItem_Click);
            // 
            // btnGetCustomer
            // 
            this.btnGetCustomer.BackColor = System.Drawing.Color.Honeydew;
            this.btnGetCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetCustomer.Location = new System.Drawing.Point(16, 102);
            this.btnGetCustomer.Name = "btnGetCustomer";
            this.btnGetCustomer.Size = new System.Drawing.Size(181, 23);
            this.btnGetCustomer.TabIndex = 3;
            this.btnGetCustomer.Text = "Retrieve WooCommerce Customer";
            this.btnGetCustomer.UseVisualStyleBackColor = false;
            this.btnGetCustomer.Click += new System.EventHandler(this.btnGetCustomer_Click);
            // 
            // btnEditRegisteredData
            // 
            this.btnEditRegisteredData.BackColor = System.Drawing.Color.Navy;
            this.btnEditRegisteredData.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEditRegisteredData.Location = new System.Drawing.Point(334, 177);
            this.btnEditRegisteredData.Name = "btnEditRegisteredData";
            this.btnEditRegisteredData.Size = new System.Drawing.Size(145, 23);
            this.btnEditRegisteredData.TabIndex = 4;
            this.btnEditRegisteredData.Text = "Edit Registered Data";
            this.btnEditRegisteredData.UseVisualStyleBackColor = false;
            this.btnEditRegisteredData.Click += new System.EventHandler(this.btnEditRegisteredData_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.IndianRed;
            this.btnExit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExit.Location = new System.Drawing.Point(404, 206);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLicense
            // 
            this.btnLicense.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnLicense.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLicense.Location = new System.Drawing.Point(253, 177);
            this.btnLicense.Name = "btnLicense";
            this.btnLicense.Size = new System.Drawing.Size(75, 23);
            this.btnLicense.TabIndex = 6;
            this.btnLicense.Text = "License";
            this.btnLicense.UseVisualStyleBackColor = false;
            this.btnLicense.Click += new System.EventHandler(this.btnLicense_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnGetOrder);
            this.panel1.Controls.Add(this.btnGetItem);
            this.panel1.Controls.Add(this.btnGetCustomer);
            this.panel1.Location = new System.Drawing.Point(11, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 146);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lavender;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnGetAbssProduct);
            this.panel2.Location = new System.Drawing.Point(12, 177);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(216, 55);
            this.panel2.TabIndex = 9;
            // 
            // btnGetAbssProduct
            // 
            this.btnGetAbssProduct.BackColor = System.Drawing.Color.SpringGreen;
            this.btnGetAbssProduct.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGetAbssProduct.Location = new System.Drawing.Point(18, 16);
            this.btnGetAbssProduct.Name = "btnGetAbssProduct";
            this.btnGetAbssProduct.Size = new System.Drawing.Size(181, 23);
            this.btnGetAbssProduct.TabIndex = 2;
            this.btnGetAbssProduct.Text = "Retrieve ABSS Product";
            this.btnGetAbssProduct.UseVisualStyleBackColor = false;
            this.btnGetAbssProduct.Click += new System.EventHandler(this.btnGetAbssProduct_Click);
            // 
            // lblSchedule
            // 
            this.lblSchedule.AutoSize = true;
            this.lblSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSchedule.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblSchedule.Location = new System.Drawing.Point(17, 14);
            this.lblSchedule.Name = "lblSchedule";
            this.lblSchedule.Size = new System.Drawing.Size(140, 13);
            this.lblSchedule.TabIndex = 11;
            this.lblSchedule.Text = "Data Update Schedule:";
            // 
            // comIntervalDays
            // 
            this.comIntervalDays.DropDownWidth = 80;
            this.comIntervalDays.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comIntervalDays.FormattingEnabled = true;
            this.comIntervalDays.Items.AddRange(new object[] {
            "Daily",
            "Weekly",
            "Monthly"});
            this.comIntervalDays.Location = new System.Drawing.Point(20, 52);
            this.comIntervalDays.Name = "comIntervalDays";
            this.comIntervalDays.Size = new System.Drawing.Size(79, 21);
            this.comIntervalDays.TabIndex = 12;
            // 
            // comIntervalTimes
            // 
            this.comIntervalTimes.DropDownWidth = 80;
            this.comIntervalTimes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comIntervalTimes.FormattingEnabled = true;
            this.comIntervalTimes.Location = new System.Drawing.Point(124, 52);
            this.comIntervalTimes.Name = "comIntervalTimes";
            this.comIntervalTimes.Size = new System.Drawing.Size(75, 21);
            this.comIntervalTimes.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnSaveSchedule);
            this.panel3.Controls.Add(this.lblSchedule);
            this.panel3.Controls.Add(this.comIntervalTimes);
            this.panel3.Controls.Add(this.comIntervalDays);
            this.panel3.Location = new System.Drawing.Point(253, 15);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(226, 146);
            this.panel3.TabIndex = 14;
            // 
            // btnSaveSchedule
            // 
            this.btnSaveSchedule.BackColor = System.Drawing.Color.PeachPuff;
            this.btnSaveSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSchedule.Location = new System.Drawing.Point(124, 102);
            this.btnSaveSchedule.Name = "btnSaveSchedule";
            this.btnSaveSchedule.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSchedule.TabIndex = 14;
            this.btnSaveSchedule.Text = "Save";
            this.btnSaveSchedule.UseVisualStyleBackColor = false;
            this.btnSaveSchedule.Click += new System.EventHandler(this.btnSaveSchedule_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(502, 244);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLicense);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnEditRegisteredData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetOrder;
        private System.Windows.Forms.Button btnGetItem;
        private System.Windows.Forms.Button btnGetCustomer;
        private System.Windows.Forms.Button btnEditRegisteredData;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLicense;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnGetAbssProduct;
        private System.Windows.Forms.Label lblSchedule;
        private System.Windows.Forms.ComboBox comIntervalDays;
        private System.Windows.Forms.ComboBox comIntervalTimes;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSaveSchedule;
    }
}

