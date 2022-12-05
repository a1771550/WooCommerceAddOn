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
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetOrder
            // 
            this.btnGetOrder.Location = new System.Drawing.Point(16, 14);
            this.btnGetOrder.Name = "btnGetOrder";
            this.btnGetOrder.Size = new System.Drawing.Size(181, 23);
            this.btnGetOrder.TabIndex = 0;
            this.btnGetOrder.Text = "Retrieve WooCommerce Order";
            this.btnGetOrder.UseVisualStyleBackColor = true;
            this.btnGetOrder.Click += new System.EventHandler(this.btnGetOrder_Click);
            // 
            // btnGetItem
            // 
            this.btnGetItem.Location = new System.Drawing.Point(16, 58);
            this.btnGetItem.Name = "btnGetItem";
            this.btnGetItem.Size = new System.Drawing.Size(181, 23);
            this.btnGetItem.TabIndex = 2;
            this.btnGetItem.Text = "Retrieve WooCommerce Product";
            this.btnGetItem.UseVisualStyleBackColor = true;
            this.btnGetItem.Click += new System.EventHandler(this.btnGetItem_Click);
            // 
            // btnGetCustomer
            // 
            this.btnGetCustomer.Location = new System.Drawing.Point(16, 102);
            this.btnGetCustomer.Name = "btnGetCustomer";
            this.btnGetCustomer.Size = new System.Drawing.Size(181, 23);
            this.btnGetCustomer.TabIndex = 3;
            this.btnGetCustomer.Text = "Retrieve WooCommerce Customer";
            this.btnGetCustomer.UseVisualStyleBackColor = true;
            this.btnGetCustomer.Click += new System.EventHandler(this.btnGetCustomer_Click);
            // 
            // btnEditRegisteredData
            // 
            this.btnEditRegisteredData.BackColor = System.Drawing.Color.LightCyan;
            this.btnEditRegisteredData.Location = new System.Drawing.Point(245, 135);
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
            this.btnExit.Location = new System.Drawing.Point(406, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLicense
            // 
            this.btnLicense.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLicense.Location = new System.Drawing.Point(406, 135);
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
            this.panel1.Location = new System.Drawing.Point(11, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 146);
            this.panel1.TabIndex = 8;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 185);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLicense);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnEditRegisteredData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
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
    }
}

