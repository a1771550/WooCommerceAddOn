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
    public partial class frmLogin : Form
    {
        public string UserName { get; set; }
        public DialogResult dialogResult { get; set; }
        public frmLogin()
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
            UserName = txtUserName.Text;
            if (!string.IsNullOrEmpty(UserName))
            {
                dialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter your User Name.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUserName.Focus();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }       
    }
}
