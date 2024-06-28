using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CM.Model
{
    public partial class fkmAddCustomer : Form
    {
        public fkmAddCustomer()
        {
            InitializeComponent();
        }


        public string orderType = "";
        public int driverID = 0;
        public int mainID = 0;
        public string cusName = "";



        private void fmAddCustomer_Load(object sender, EventArgs e)
        {
            if (orderType == "포장")
            {
                lblDriver.Visible = false;
                cbDriver.Visible = false;

            }
            string qry = "select staffID 'id', sName 'name' from staff where sRole Like '배달원'";
            MainClass.CBFill(qry, cbDriver);

            if (mainID > 0)
            {
                cbDriver.SelectedValue = driverID;
            }

        }

        private void cbDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            driverID = Convert.ToInt32(cbDriver.SelectedValue);
        }
    }
}
