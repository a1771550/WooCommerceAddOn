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
    public partial class frmLocation : Form
    {
        private DataTable dtLocation { get; set; }
        public string PriLocation { get; set; }
        public frmLocation(DataTable dtLocation, string prilocation)
        {
            InitializeComponent();
            this.dtLocation = dtLocation;
            PriLocation = prilocation;
        }

        private void btnConfirmLocation_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void frmLocation_Load(object sender, EventArgs e)
        {
            DataRow row = dtLocation.NewRow();
            row[0] = 0;
            row[1] = "-- Select --";
            dtLocation.Rows.InsertAt(row, 0);
            comboPrimaryLocation.DisplayMember = "LocationName";
            comboPrimaryLocation.ValueMember = "LocationIdentification";
            comboPrimaryLocation.DataSource = dtLocation;

            if (string.IsNullOrEmpty(PriLocation))
            {
                comboPrimaryLocation.SelectedIndex = 0;
            }
            else
            {
                int idx = 0;
                foreach(var item in comboPrimaryLocation.Items)
                {
                    DataRowView rowv = item as DataRowView;

                    if (rowv != null)
                    {
                        string location = Convert.ToString(rowv["LocationIdentification"]);
                        if (PriLocation == location)
                        {
                            comboPrimaryLocation.SelectedIndex = idx;
                            break;
                        }
                    }
                    idx++;
                }
            }
            
        }

        private void comboPrimaryLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPrimaryLocation.SelectedIndex > 0)
            {
                DataRowView vrow = (DataRowView)comboPrimaryLocation.SelectedItem;
                DataRow row = vrow.Row;
                PriLocation = Convert.ToString(row["LocationIdentification"]);
            }
        }
    }
}
