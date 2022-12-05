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
    public partial class frmDevice : Form
    {
        public string DeviceID { get; set; }   
        
        public DialogResult dialogResult { get; set; }
        public frmDevice()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.Cancel;
            Application.Exit();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DeviceID = txtDeviceID.Text;
            if (!string.IsNullOrEmpty(DeviceID))
            {
                dialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter your Device ID.","Device", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDeviceID.Focus();
            }
            
        }
    }
}
