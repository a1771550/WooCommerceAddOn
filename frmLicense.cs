using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WALib.Models;

namespace WooCommerceAddOn
{
    public partial class frmLicense : Form
    {
        private ComInfoModel comInfo { get; set; }
        public DialogResult dialogresult { get; set; }
        public frmLicense(ComInfoModel comInfo)
        {
            InitializeComponent();
            this.comInfo = comInfo;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            dialogresult = DialogResult.OK;
            Close();
        }

        private void frmLicense_Load(object sender, EventArgs e)
        {
            string daystart = ((DateTime)comInfo.waLicenseDateStart).ToString("dddd, dd MMMM yyyy");
            string dayend = ((DateTime)comInfo.waLicenseDateEnd).ToString("dddd, dd MMMM yyyy");
            if (comInfo.abssDateFormat == "U")
            {
                daystart = ((DateTime)comInfo.waLicenseDateStart).ToString("dddd, MMMM dd yyyy");
                dayend = ((DateTime)comInfo.waLicenseDateEnd).ToString("dddd, MMMM dd yyyy");
            }

            lblLicStart.Text = daystart;
            lblLicEnd.Text = dayend;

            var days = (DateTime)comInfo.waLicenseDateEnd - DateTime.Now;
            lblDays.Text = days.Days.ToString();
        }
    }
}
