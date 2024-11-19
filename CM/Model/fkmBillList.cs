using CM.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CM.Model
{
    public partial class fkmBillList : Form
    {
        public fkmBillList()
        {
            InitializeComponent();
        }

        public int MainID = 0;
        public bool payment = true;

        private void fmBillList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string qry = @"select MainID, TableName, WaiterName, orderType, status, total from tblMain
                            where status <> '결제 완료' ";  // tblMain 테이블에서 status 열이 '결제완료'가 아닌 모든 레코드의 MainID, TableName, WaiterName, orderType, status, total 열을 선택하는 것

            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvtable);
            lb.Items.Add(dgvWaiter);
            lb.Items.Add(dgvType);
            lb.Items.Add(dgvStatus);
            lb.Items.Add(dgvTotal);

            MainClass.LoadDate(qry, guna2DataGridView1, lb);
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // for searil no

            int count = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")  // 편집 name 클릭
            {
                MainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);

                if (guna2DataGridView1.CurrentCell.OwningRow.Cells[7].Value.ToString() == "대기")
                {
                    payment = false;
                }
                else
                {
                    payment = true;
                }
                this.Close();

            }
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                MainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                string qry = @"select * from tblMain m inner join
                                tblDetails d on d.MainID = m.MainID
                                inner join products p on p.PID = d.ProID
                                where m.MainID =" + MainID + " ";
                SqlCommand cmd = new SqlCommand(qry, MainClass.con);
                MainClass.con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                MainClass.con.Close();
                fkmPrint frm = new fkmPrint();
                rptBill cr = new rptBill();

                cr.SetDatabaseLogon("sa", "1234");
                cr.SetDataSource(dt);
                frm.crystalReportViewer1.ReportSource = cr;
                frm.crystalReportViewer1.Refresh();
                frm.Show();
            }
        }

    }
}
