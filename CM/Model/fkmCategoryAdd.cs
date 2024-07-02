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

namespace CM.Model
{
    public partial class fkmCategoryAdd : SampleAdd
    {
        public fkmCategoryAdd()
        {
            InitializeComponent();


        }
        // 데이터 저장, 수정
        public int id = 0;
        public override void btnSave_Click(object sender, EventArgs e)
        {
            // 화면 출력이 아니라 쿼리만 실행하기 때문에 sql 함수 사용

            // 데이터 저장용, 수정용 쿼리 
            string qry = "";
            if (id == 0)    // 데이터 저장용
            {
                qry = "insert into category values(@Name)";
            }
            else            // 수정용 쿼리
            {
                qry = "update category set catName = @Name where catID = @id";
            }

            Hashtable ht = new Hashtable();     // 그리드뷰의 데이터를 헤쉬테이블의 키-값 형태로 만들기
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);

            if (MainClass.SQL(qry, ht) > 0)  // 실행한 결과의 값
            {
                // 데이터 초기화
                guna2MessageDialog1.Show("저장 완료");
                id = 0;
                txtName.Text = "";
                txtName.Focus();
            }

        }
    }
}
