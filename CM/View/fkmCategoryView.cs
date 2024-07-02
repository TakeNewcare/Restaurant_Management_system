using CM.Model;
using Guna.UI2.WinForms;
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
    public partial class fkmCategoryView : SampleView
    {
        public fkmCategoryView()
        {
            InitializeComponent();
        }

        // 화면 켜지면 데이터 불러오기
        private void fkmCategoryView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        // 추가 버튼 클릭 시 추가폼 불러오기
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new fkmCategoryAdd());
            GetData();   // 굳이?
        }

        // 텍스트가 변경될 때 마다 실행하여 데이터를 조회한다.
        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        // 데이터 불러오기
        public void GetData()
        {
            string qry = $"select * from category where catName like '%{txtSearch.Text}%'";  // 텍스트와 비슷한 값을 가지는 데이터 조회
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);            // 각 열의 이름(값 x)을 넘겨준다!
            lb.Items.Add(dgvName);          // 각 열의 이름을 넘겨준다!

            MainClass.LoadDate(qry, guna2DataGridView1, lb);  // 쿼리, 출력해서 보여줄 데이터그리드, 열이름이 담긴 리스트박스를 넘겨서
                                                              // 리스트박스의 이름들과 맵핑 후 데이터를 그리드뷰에 보여준다.
        }

        // 데이타그리드의 수정, 삭제 이미지 클릭 시 동작
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")   // DataGridViewColumn == OwningColumn 현재 컬럼 => 현재 컬럼의 이름이 dgvedit이면
            {
                // 카테고리 이름 수정
                fkmCategoryAdd frm = new fkmCategoryAdd();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);  // 데이터 그리드 클릭된 현재의 행의 dgvid 컬럼의 값을 전달
                frm.txtName.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);

                MainClass.BlurBackground(frm);  // 폼을 받아 배경 입히기
                GetData();      // frm 종료하면 데이터 불러오기
            }
            // 카테고리 삭제
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;

                if (guna2MessageDialog1.Show("정말 삭제 하시겠습니까?") == DialogResult.Yes)
                {
                    // 데이터 삭제
                    int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);

                    string qry = "delete from category where catID = " + id + "";

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
