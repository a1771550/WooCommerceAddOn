namespace WooCommerceAddOn
{
    partial class frmList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmList));
            this.dgList = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnNxtPage = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSaveDB = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnABSS = new System.Windows.Forms.Button();
            this.source = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.source)).BeginInit();
            this.SuspendLayout();
            // 
            // dgList
            // 
            this.dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgList.Location = new System.Drawing.Point(12, 12);
            this.dgList.Name = "dgList";
            this.dgList.Size = new System.Drawing.Size(977, 369);
            this.dgList.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Linen;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnPrevPage);
            this.panel1.Controls.Add(this.btnNxtPage);
            this.panel1.Location = new System.Drawing.Point(158, 398);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(311, 47);
            this.panel1.TabIndex = 19;
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPrevPage.Location = new System.Drawing.Point(206, 13);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(75, 23);
            this.btnPrevPage.TabIndex = 13;
            this.btnPrevPage.Text = "Previous Page";
            this.btnPrevPage.UseVisualStyleBackColor = false;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // btnNxtPage
            // 
            this.btnNxtPage.BackColor = System.Drawing.Color.Gainsboro;
            this.btnNxtPage.Location = new System.Drawing.Point(21, 12);
            this.btnNxtPage.Name = "btnNxtPage";
            this.btnNxtPage.Size = new System.Drawing.Size(75, 23);
            this.btnNxtPage.TabIndex = 11;
            this.btnNxtPage.Text = "Next page";
            this.btnNxtPage.UseVisualStyleBackColor = false;
            this.btnNxtPage.Click += new System.EventHandler(this.btnNxtPage_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.IndianRed;
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(911, 411);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSaveDB
            // 
            this.btnSaveDB.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnSaveDB.Location = new System.Drawing.Point(669, 412);
            this.btnSaveDB.Name = "btnSaveDB";
            this.btnSaveDB.Size = new System.Drawing.Size(75, 23);
            this.btnSaveDB.TabIndex = 20;
            this.btnSaveDB.Text = "Save to DB";
            this.btnSaveDB.UseVisualStyleBackColor = false;
            this.btnSaveDB.Click += new System.EventHandler(this.btnSaveDB_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 398);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 21;
            // 
            // btnABSS
            // 
            this.btnABSS.BackColor = System.Drawing.Color.LightGreen;
            this.btnABSS.Location = new System.Drawing.Point(780, 412);
            this.btnABSS.Name = "btnABSS";
            this.btnABSS.Size = new System.Drawing.Size(95, 23);
            this.btnABSS.TabIndex = 22;
            this.btnABSS.Text = "Upload to ABSS";
            this.btnABSS.UseVisualStyleBackColor = false;
            this.btnABSS.Click += new System.EventHandler(this.btnABSS_Click);
            // 
            // frmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(1000, 457);
            this.Controls.Add(this.btnABSS);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSaveDB);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmList";
            this.Text = "List";
            this.Load += new System.EventHandler(this.frmList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.source)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnNxtPage;
        private System.Windows.Forms.Button btnSaveDB;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnABSS;
        private System.Windows.Forms.BindingSource source;
    }
}