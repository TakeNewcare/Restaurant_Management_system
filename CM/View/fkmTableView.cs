using CM.Model;
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

namespace CM.View
{
    public partial class fkmTableView : SampleView
    {
        public fkmTableView()
        {
            InitializeComponent();
        }

        private void fkmTable_Load(object sender, EventArgs e)
        {
            GetData();

        }


        // 테이블 데이터 가져오기
        public void GetData()
        {
            string qry = $"select * from tables where tName like '%{txtSearch.Text}%'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);

            MainClass.LoadDate(qry, guna2DataGridView1, lb);
        }

        // 테이블 추가
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new fkmTableAdd());
            GetData();
        }

        // 검색 기능(글자가 변결될 때 마다 실행된다.)
        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        // 데이타그리드 안의 수정, 삭제 이미지 클릭 시 기능 
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 데이터 수정
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                fkmTableAdd frm = new fkmTableAdd();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtName1.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);

                 MainClass.BlurBackground(frm);
                GetData();
            }

            // 데이터 삭제
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;

                if (guna2MessageDialog1.Show("정말 삭제 하시겠습니까?") == DialogResult.Yes)
                {

                    int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);

                    string qry = "delete from tables where tID = " + id + "";

                    Hashtable ht = new Hashtable();
                    MainClass.SQL(qry, ht);

                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("삭제 완료");
                    GetData();
                }

            }

        }





    }
}
