namespace WooCommerceAddOn
{
    partial class frmLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocation));
            this.comboPrimaryLocation = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnConfirmLocation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboPrimaryLocation
            // 
            this.comboPrimaryLocation.FormattingEnabled = true;
            this.comboPrimaryLocation.Location = new System.Drawing.Point(137, 9);
            this.comboPrimaryLocation.Name = "comboPrimaryLocation";
            this.comboPrimaryLocation.Size = new System.Drawing.Size(121, 21);
            this.comboPrimaryLocation.TabIndex = 25;
            this.comboPrimaryLocation.SelectedIndexChanged += new System.EventHandler(this.comboPrimaryLocation_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Primary Stock Location:";
            // 
            // btnConfirmLocation
            // 
            this.btnConfirmLocation.Location = new System.Drawing.Point(87, 44);
            this.btnConfirmLocation.Name = "btnConfirmLocation";
            this.btnConfirmLocation.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmLocation.TabIndex = 26;
            this.btnConfirmLocation.Text = "Confirm";
            this.btnConfirmLocation.UseVisualStyleBackColor = true;
            this.btnConfirmLocation.Click += new System.EventHandler(this.btnConfirmLocation_Click);
            // 
            // frmLocation
            // 
            this.AcceptButton = this.btnConfirmLocation;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 79);
            this.Controls.Add(this.btnConfirmLocation);
            this.Controls.Add(this.comboPrimaryLocation);
            this.Controls.Add(this.label11);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLocation";
            this.Text = "Primary Stock Location";
            this.Load += new System.EventHandler(this.frmLocation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboPrimaryLocation;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnConfirmLocation;
    }
}