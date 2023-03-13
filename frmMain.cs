using MYOBLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WADAL;
using WALib.Models;

namespace WooCommerceAddOn
{
    public partial class frmMain : Form
    {
        public static string CacheKey { get { return "ComInfo"; } }
        public static string DeviceId { get; set; }
        public static string UserName { get; set; }
        private ComInfoModel comInfo { get; set; }
        public frmMain()
        {
            InitializeComponent();
        }


        private static ComInfoModel GetCachedComInfo(string username)
        {
            ObjectCache cache = MemoryCache.Default;
            if (cache.Contains(CacheKey))
            {
                return (ComInfoModel)cache.Get(CacheKey);
            }
            else
            {
                var combo = ComInfoEditModel.GetByName(username);
                // Store data in the cache    
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cache.Add(CacheKey, combo, cacheItemPolicy);
                return combo;
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            frmLogin frmlogin = new frmLogin();
            frmlogin.ShowDialog();
            if (frmlogin.dialogResult == DialogResult.OK)
            {
                UserName = frmlogin.UserName;
                comInfo = ComInfoEditModel.GetByName(UserName);
                //comInfo = GetCachedComInfo(UserName);

                if (comInfo == null)
                {
                    var result = MessageBox.Show("No User Information Found!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    if (result == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    if (comInfo.first_run)
                    {
                        frmReadMe frmReadMe = new frmReadMe();
                        frmReadMe.ShowDialog();
                    }
                    else
                    {
                        if (!comInfo.waLicenseActivated)
                        {
                            frmDevice frmDevice = new frmDevice();
                            frmDevice.ShowDialog();
                            if (frmDevice.dialogResult == DialogResult.OK)
                            {
                                DeviceId = frmDevice.DeviceID;
                                comInfo = ComInfoEditModel.Get(DeviceId);

                                if (comInfo == null)
                                {
                                    var result = MessageBox.Show("No Device Information Found!", "Device", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    if (result == DialogResult.OK)
                                    {
                                        Application.Exit();
                                    }
                                }
                                else
                                {
                                    frmActivation frmActivation = new frmActivation(comInfo);
                                    var result = frmActivation.ShowDialog();
                                    if (result == DialogResult.OK)
                                    {
                                        ActivationOK();
                                    }
                                    else
                                    {
                                        Application.Exit();
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (comInfo.waLicenseDateEnd <= CommonLib.Helpers.CommonHelper.GetDateTime())
                            {
                                var result = MessageBox.Show("Your License is end. Please contact us if you want to extend your license.", "License", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (result == DialogResult.OK)
                                {
                                    Application.Exit();
                                }
                            }
                        }
                    } 
                }
            }
        }

        private void ActivationOK(bool activation = true)
        {
            var msg = activation ? "Activation is done successfully." : "Registration Data is edited.";
            MessageBox.Show(msg, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGetOrder_Click(object sender, EventArgs e)
        {
            comInfo.dataType = DataType.Order;
            frmList frmList = new frmList(comInfo);
            frmList.ShowDialog();
        }

        private void btnGetItem_Click(object sender, EventArgs e)
        {
            comInfo.dataType = DataType.Product;
            frmList frmList = new frmList(comInfo);
            frmList.ShowDialog();
        }

        private void btnGetCustomer_Click(object sender, EventArgs e)
        {
            comInfo.dataType = DataType.Customer;
            frmList frmList = new frmList(comInfo);
            frmList.ShowDialog();
        }

        private void btnEditRegisteredData_Click(object sender, EventArgs e)
        {
            //comInfo = GetCachedComInfo(UserName);
            comInfo = ComInfoEditModel.GetByName(UserName);
            frmActivation frmActivation = new frmActivation(comInfo, false);
            frmActivation.Text = "Registration Data";
            var result = frmActivation.ShowDialog();
            if (result == DialogResult.OK)
            {
                ActivationOK(false);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            frmLicense frmLicense = new frmLicense(comInfo);
            frmLicense.ShowDialog();
        }

        private void btnGetAbssOrder_Click(object sender, EventArgs e)
        {
            comInfo.dataType = DataType.AbssOrder;
            frmList frmList = new(comInfo);
            frmList.ShowDialog();
        }

        private void btnGetAbssProduct_Click(object sender, EventArgs e)
        {
            comInfo.dataType = DataType.AbssProduct;
            frmList frmList = new(comInfo);
            frmList.ShowDialog();
        }

        private void btnGetAbssCustomer_Click(object sender, EventArgs e)
        {
            comInfo.dataType = DataType.AbssCustomer;
            frmList frmList = new(comInfo);
            frmList.ShowDialog();
        }
    }
}
