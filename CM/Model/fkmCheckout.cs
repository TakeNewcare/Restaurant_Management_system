﻿using Guna.UI2.WinForms;
using System;
using System.Collections;
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
    public partial class fkmCheckout : Form
    {
        public fkmCheckout()
        {
            InitializeComponent();
        }



        public double amt;
        public int MainID = 0;

        private void txtReceived_TextChanged(object sender, EventArgs e)
        {
            double amt = 0;
            double receipt = 0;
            double change = 0;

            double.TryParse(txtBillAmount.Text, out amt);  
            double.TryParse(txtReceived.Text, out receipt);


            if (amt > receipt)
            {
                txtChange.Text = "0";
                btnSave.Enabled = false;
                return;
            }
            btnSave.Enabled = true;

            change = Math.Abs(amt - receipt); 



            txtChange.Text = change.ToString();

        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string qry = @" Update tblMain set total = @total, received = @rec, change = @change,
                            status = '결제 완료' where MainID = @id";

            Hashtable ht = new Hashtable();
            ht.Add("@id", MainID);
            ht.Add("@total", txtBillAmount.Text);
            ht.Add("@rec", txtReceived.Text);
            ht.Add("@change", txtChange.Text);

            if (MainClass.SQL(qry, ht) > 0)
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Show("결제가 완료 되었습니다.");
                this.Close();

            }

        }

        private void fmCheckout_Load(object sender, EventArgs e)
        {
            txtBillAmount.Text = amt.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
