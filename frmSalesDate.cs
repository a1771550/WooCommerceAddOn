using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WooCommerceAddOn
{
    public partial class frmSalesDate : Form
    {
        public DialogResult dialogResult { get; set; }
        public DateTime frmDate { get; set; }
        public DateTime toDate { get; set; }
        public bool IncludeUploaded { get; set; }
        public frmSalesDate(string format)
        {
            InitializeComponent();         
            frmDate = DateTime.Now.Date;
            toDate = frmDate;
            dtTo.Format= dtFrm.Format = DateTimePickerFormat.Custom;
            dtTo.CustomFormat = dtFrm.CustomFormat = format == "E" ? "dd/MM/yyyy" : "MM/dd/yyyy";
            IncludeUploaded = false;
        }

        private void dtFrm_ValueChanged(object sender, EventArgs e)
        {
            frmDate = dtFrm.Value.Date;
        }

        private void dtTo_ValueChanged(object sender, EventArgs e)
        {
            toDate = dtTo.Value.Date;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkIncUploaded_CheckedChanged(object sender, EventArgs e)
        {
            IncludeUploaded = chkIncUploaded.Checked;
        }
    }
}
