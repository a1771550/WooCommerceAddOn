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
            this.lblProdCat = new System.Windows.Forms.Label();
            this.comProdCat = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSaveCat = new System.Windows.Forms.Button();
            this.btnUpdateCat = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
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
            this.btnEditRegisteredData.Location = new System.Drawing.Point(253, 283);
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
            this.btnExit.Location = new System.Drawing.Point(404, 283);
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
            this.btnLicense.Location = new System.Drawing.Point(253, 254);
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
            this.panel2.Size = new System.Drawing.Size(216, 129);
            this.panel2.TabIndex = 9;
            // 
            // btnGetAbssProduct
            // 
            this.btnGetAbssProduct.BackColor = System.Drawing.Color.SpringGreen;
            this.btnGetAbssProduct.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGetAbssProduct.Location = new System.Drawing.Point(16, 49);
            this.btnGetAbssProduct.Name = "btnGetAbssProduct";
            this.btnGetAbssProduct.Size = new System.Drawing.Size(181, 23);
            this.btnGetAbssProduct.TabIndex = 2;
            this.btnGetAbssProduct.Text = "Retrieve ABSS Product";
            this.btnGetAbssProduct.UseVisualStyleBackColor = false;
            this.btnGetAbssProduct.Click += new System.EventHandler(this.btnGetAbssProduct_Click);
            // 
            // lblProdCat
            // 
            this.lblProdCat.AutoSize = true;
            this.lblProdCat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblProdCat.Location = new System.Drawing.Point(18, 13);
            this.lblProdCat.Name = "lblProdCat";
            this.lblProdCat.Size = new System.Drawing.Size(129, 13);
            this.lblProdCat.TabIndex = 3;
            this.lblProdCat.Text = "Default Product Category:";
            // 
            // comProdCat
            // 
            this.comProdCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comProdCat.FormattingEnabled = true;
            this.comProdCat.Location = new System.Drawing.Point(21, 41);
            this.comProdCat.Name = "comProdCat";
            this.comProdCat.Size = new System.Drawing.Size(192, 21);
            this.comProdCat.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnSaveCat);
            this.panel4.Controls.Add(this.btnUpdateCat);
            this.panel4.Controls.Add(this.comProdCat);
            this.panel4.Controls.Add(this.lblProdCat);
            this.panel4.Location = new System.Drawing.Point(253, 15);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(226, 146);
            this.panel4.TabIndex = 15;
            // 
            // btnSaveCat
            // 
            this.btnSaveCat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSaveCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveCat.Location = new System.Drawing.Point(138, 88);
            this.btnSaveCat.Name = "btnSaveCat";
            this.btnSaveCat.Size = new System.Drawing.Size(75, 23);
            this.btnSaveCat.TabIndex = 6;
            this.btnSaveCat.Text = "Confirm";
            this.btnSaveCat.UseVisualStyleBackColor = false;
            this.btnSaveCat.Click += new System.EventHandler(this.btnSaveCat_Click);
            // 
            // btnUpdateCat
            // 
            this.btnUpdateCat.BackColor = System.Drawing.Color.Ivory;
            this.btnUpdateCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateCat.Location = new System.Drawing.Point(21, 88);
            this.btnUpdateCat.Name = "btnUpdateCat";
            this.btnUpdateCat.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateCat.TabIndex = 5;
            this.btnUpdateCat.Text = "Update";
            this.btnUpdateCat.UseVisualStyleBackColor = false;
            this.btnUpdateCat.Click += new System.EventHandler(this.btnUpdateCat_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(502, 324);
            this.Controls.Add(this.panel4);
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
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
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
        private System.Windows.Forms.ComboBox comProdCat;
        private System.Windows.Forms.Label lblProdCat;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSaveCat;
        private System.Windows.Forms.Button btnUpdateCat;
    }
}

