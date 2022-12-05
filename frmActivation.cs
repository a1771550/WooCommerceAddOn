using CommonLib.Helpers;
using CommonLib.Models.MYOB;
using MYOBLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WALib.Models;

namespace WooCommerceAddOn
{
    public partial class frmActivation : Form
    {
        private bool activation { get; set; }
        private ComInfoModel model;
        public DataTable dtLocation { get; set; }
        public frmActivation(ComInfoModel model, bool activation = true)
        {
            InitializeComponent();
            this.activation = activation;
            this.model = model;

            txtComName.Text = model.comName;
            txtComName.ReadOnly = true;

            if (!activation)
            {
                //txtComName.Text = model.comName;
                txtComPhone.Text = model.comPhone;
                txtComEmail.Text = model.comEmail;
                Uri uriAddress = new Uri(model.wcURL);
                int idx = 0;
                foreach (var item in comboProtocols.Items)
                {
                    if (item.ToString() == uriAddress.Scheme)
                    {
                        comboProtocols.SelectedIndex = idx;
                        break;
                    }
                    idx++;
                }
                txtWcUrl.Text = model.wcURL;
                txtWcConsumerKey.Text = model.wcConsumerKey;
                txtWcConsumerSecret.Text = model.wcConsumerSecret;
                lblFileLocation.Text = Path.Combine(model.abssFileLocation, model.abssFileName);
                lblExeLocation.Text = Path.Combine(model.abssExeLocation, model.abssExeName);
                txtAbssID.Text = model.abssUserId;
                txtAbssPass.Text = model.abssUserPass;
                if (model.abssDateFormat == "E")
                {
                    rdoEN.Checked = true;
                }
                else
                {
                    rdoUS.Checked = true;
                }
            }
        }

        private void btnFileLocation_Click(object sender, EventArgs e)
        {
            abssFileDialog.ShowDialog();
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (validForm())
            {
                WALib.Helpers.ModelHelper.GetAbssKeyInfo(ref model);
                string[] prefixes = { "https://", "http://" };
                bool result = prefixes.Any(prefix => model.wcURL.StartsWith(prefix));
                if (!result)
                {
                    model.wcURL = string.Concat(comboProtocols.SelectedItem.ToString(), "://", model.wcURL);
                }

                if (GetAbssStockLocations())
                {
                    if (dtLocation.Rows.Count == 1)
                    {
                        model.abssPrimaryLocation = dtLocation.Rows[0][2].ToString();
                        await HandleSubmit();
                    }
                    else
                    {
                        frmLocation frmLocation = new frmLocation(dtLocation, model.abssPrimaryLocation);
                        DialogResult dresult = frmLocation.ShowDialog();
                        if (dresult == DialogResult.OK)
                        {
                            model.abssPrimaryLocation = frmLocation.PriLocation;
                            await HandleSubmit();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ABSS Information is not correct. Please enter again.");
                }
            }
        }

        

        private async Task HandleSubmit()
        {
            try
            {
                if (await ComInfoEditModel.Save(model))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Something wrong in activation");
                }
            }
            catch(Exception ex)
            {
                if (DialogResult.OK == MessageBox.Show(string.Format("errmsg:{0}; innermsg:{1}",ex.Message,ex.InnerException), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error))
                {
                    Application.Exit();
                }
                //throw new Exception(ex.Message);
            }
            
        }

        private bool GetAbssStockLocations()
        {           
            ABSSModel abss = new ABSSModel(MyobHelper.LocationList_Active, model.abssDriver, model.abssUserId, model.abssUserPass, model.abssFileLocation, model.abssFileName, model.abssExeLocation, model.abssExeName);
            dtLocation = MyobHelper.GetLocationTable(abss);
            return dtLocation != null && dtLocation.Rows.Count > 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        private bool validForm()
        {
            StringBuilder ab = new StringBuilder();

            model.comName = txtComName.Text;
            if (string.IsNullOrEmpty(model.comName))
            {
                ab.Append("Please enter a company name.\r\n");
            }
            model.comPhone = txtComPhone.Text;

            model.comEmail = txtComEmail.Text;
            if (!string.IsNullOrEmpty(model.comEmail))
            {
                try
                {
                    MailAddress m = new MailAddress(model.comEmail);
                }
                catch (FormatException)
                {
                    ab.Append("Please enter a valid email address.\r\n");
                }
            }

            model.wcURL = txtWcUrl.Text;
            if (string.IsNullOrEmpty(model.wcURL))
            {
                ab.Append("Please enter a WooCommerce URL.\r\n");
            }
            else
            {

            }

            model.wcConsumerKey = txtWcConsumerKey.Text;
            if (string.IsNullOrEmpty(model.wcConsumerKey))
            {
                ab.Append("Please enter a WooCommerce Consumer Key.\r\n");
            }
            model.wcConsumerSecret = txtWcConsumerSecret.Text;
            if (string.IsNullOrEmpty(model.wcConsumerSecret))
            {
                ab.Append("Please enter a WooCommerce Consumer Secret.\r\n");
            }

            if (string.IsNullOrEmpty(model.abssFileLocation))
            {
                ab.Append("Please enter an ABSS file location.\r\n");
            }
            if (string.IsNullOrEmpty(model.abssFileName))
            {
                ab.Append("Please enter an ABSS file name.\r\n");
            }
            if (string.IsNullOrEmpty(model.abssExeLocation))
            {
                ab.Append("Please enter an ABSS Exe location.\r\n");
            }

            if (string.IsNullOrEmpty(model.abssExeName))
            {
                ab.Append("Please enter an ABSS Exe name.\r\n");
            }

            model.abssUserId = txtAbssID.Text;
            if (string.IsNullOrEmpty(model.abssUserId))
            {
                ab.Append("Please enter an ABSS User ID.\r\n");
            }
            model.abssUserPass = txtAbssPass.Text;
            if (string.IsNullOrEmpty(model.abssUserPass))
            {
                ab.Append("Please enter an ABSS User Password.\r\n");
            }

            if (ab.Length > 0)
            {
                MessageBox.Show(ab.ToString(), "Form Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ab.Length == 0;
        }

        private void txtComEmail_Leave(object sender, EventArgs e)
        {

        }

        private void btnExeLocation_Click(object sender, EventArgs e)
        {
            abssExeDialog.ShowDialog();
        }

        private void abssFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            lblFileLocation.Text = abssFileDialog.FileName;
            model.abssFileLocation = Path.GetDirectoryName(lblFileLocation.Text);
            model.abssFileName = Path.GetFileName(lblFileLocation.Text);
        }

        private void abssExeDialog_FileOk(object sender, CancelEventArgs e)
        {
            lblExeLocation.Text = abssExeDialog.FileName;
            model.abssExeLocation = Path.GetDirectoryName(lblExeLocation.Text);
            model.abssExeName = Path.GetFileName(lblExeLocation.Text);
            string drivermap = ConfigurationManager.AppSettings["DriverMap"];
            foreach (var dm in drivermap.Split(';'))
            {
                var version = dm.Split(':')[0].ToLower();
                var driver = dm.Split(':')[1];
                if (model.abssExeLocation.ToLower().EndsWith(version))
                {
                    model.abssDriver = string.Concat(ConfigurationManager.AppSettings["DriverPrefix"], driver);
                    break;
                }
            }
        }

        private void frmActivation_Load(object sender, EventArgs e)
        {
            if (activation)
            {
                comboProtocols.SelectedIndex = 0;
                model.abssDateFormat = "E";
            }
        }

        private void rdoEN_CheckedChanged(object sender, EventArgs e)
        {
            model.abssDateFormat = "E";
        }

        private void rdoUS_CheckedChanged(object sender, EventArgs e)
        {
            model.abssDateFormat = "U";
        }
    }
}
