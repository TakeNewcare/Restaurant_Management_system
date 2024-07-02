﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CM.Reports
{
    public partial class fkmSaleByCategory : Form
    {
        public fkmSaleByCategory()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            string qry = @"select * from tblMain m
                        inner join tblDetails d on m.MainID = d.MainID
                        inner join products p on p.pID = d.proID
                        inner join category c on c.catID = p.CategoryID
                        where m.aDate between @sdate and @edate and m.status = '결제 완료'";




            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            cmd.Parameters.AddWithValue("@sdate", Convert.ToDateTime(dateTimePicker1.Value).Date);
            cmd.Parameters.AddWithValue("@edate", Convert.ToDateTime(dateTimePicker2.Value).Date);
            MainClass.con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            MainClass.con.Close();
            fkmPrint frm = new fkmPrint();
            rptStateByCategory cr = new rptStateByCategory();

            cr.SetDatabaseLogon("sa", "1234");
            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }
    }
}