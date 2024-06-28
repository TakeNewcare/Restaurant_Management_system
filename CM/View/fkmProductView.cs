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
    public partial class fkmProductView : SampleView
    {
        public fkmProductView()
        {
            InitializeComponent();
        }

        // 데이터 불러오기
        public void GetData()
        {
            // 각 항목별로 값을 가져와서 리스트 박스에 넣어 화면에 출력하는데 products의 category 이름은 category 테이블의 catID와 products 테이블의 CategoryID가 동일한 항목으로 한다.
            string qry = $"select pID, pName, pPrice, CategoryID, c.catName from products p inner join category c on c.catID = p.CategoryID where pName like '%" + txtSearch.Text + "%' ";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPrice);
            lb.Items.Add(dgvcatID);
            lb.Items.Add(dgvcat);

            MainClass.LoadDate(qry, guna2DataGridView1, lb);
        }

        // 테이블 추가
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new fkmProductAdd());
            GetData();
        }

        // 검색 기능(글자가 변경될 때 마다 실행된다.)
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
                fkmProductAdd frm = new fkmProductAdd();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.cID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvcatID"].Value);

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

                    string qry = "delete from products where pID = " + id + "";

                    Hashtable ht = new Hashtable();
                    MainClass.SQL(qry, ht);

                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("삭제 완료");
                    GetData();
                }

            }

        }

        private void fkmProductView_Load(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
