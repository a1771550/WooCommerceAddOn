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
            this.btnGetAbssOrder = new System.Windows.Forms.Button();
            this.btnGetAbssProduct = new System.Windows.Forms.Button();
            this.btnGetAbssCustomer = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.btnEditRegisteredData.Location = new System.Drawing.Point(188, 12);
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
            this.btnExit.Location = new System.Drawing.Point(424, 12);
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
            this.btnLicense.Location = new System.Drawing.Point(11, 12);
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
            this.panel1.Location = new System.Drawing.Point(11, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 146);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lavender;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnGetAbssOrder);
            this.panel2.Controls.Add(this.btnGetAbssProduct);
            this.panel2.Controls.Add(this.btnGetAbssCustomer);
            this.panel2.Location = new System.Drawing.Point(283, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(216, 146);
            this.panel2.TabIndex = 9;
            // 
            // btnGetAbssOrder
            // 
            this.btnGetAbssOrder.BackColor = System.Drawing.Color.SpringGreen;
            this.btnGetAbssOrder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGetAbssOrder.Location = new System.Drawing.Point(16, 14);
            this.btnGetAbssOrder.Name = "btnGetAbssOrder";
            this.btnGetAbssOrder.Size = new System.Drawing.Size(181, 23);
            this.btnGetAbssOrder.TabIndex = 0;
            this.btnGetAbssOrder.Text = "Retrieve ABSS Order";
            this.btnGetAbssOrder.UseVisualStyleBackColor = false;
            this.btnGetAbssOrder.Click += new System.EventHandler(this.btnGetAbssOrder_Click);
            // 
            // btnGetAbssProduct
            // 
            this.btnGetAbssProduct.BackColor = System.Drawing.Color.SpringGreen;
            this.btnGetAbssProduct.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGetAbssProduct.Location = new System.Drawing.Point(16, 58);
            this.btnGetAbssProduct.Name = "btnGetAbssProduct";
            this.btnGetAbssProduct.Size = new System.Drawing.Size(181, 23);
            this.btnGetAbssProduct.TabIndex = 2;
            this.btnGetAbssProduct.Text = "Retrieve ABSS Product";
            this.btnGetAbssProduct.UseVisualStyleBackColor = false;
            this.btnGetAbssProduct.Click += new System.EventHandler(this.btnGetAbssProduct_Click);
            // 
            // btnGetAbssCustomer
            // 
            this.btnGetAbssCustomer.BackColor = System.Drawing.Color.SpringGreen;
            this.btnGetAbssCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGetAbssCustomer.Location = new System.Drawing.Point(16, 102);
            this.btnGetAbssCustomer.Name = "btnGetAbssCustomer";
            this.btnGetAbssCustomer.Size = new System.Drawing.Size(181, 23);
            this.btnGetAbssCustomer.TabIndex = 3;
            this.btnGetAbssCustomer.Text = "Retrieve ABSS Customer";
            this.btnGetAbssCustomer.UseVisualStyleBackColor = false;
            this.btnGetAbssCustomer.Click += new System.EventHandler(this.btnGetAbssCustomer_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(511, 226);
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
        private System.Windows.Forms.Button btnGetAbssOrder;
        private System.Windows.Forms.Button btnGetAbssProduct;
        private System.Windows.Forms.Button btnGetAbssCustomer;
    }
}

