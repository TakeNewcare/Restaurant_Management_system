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
    public partial class fkmStaffAdd : SampleAdd
    {
        public fkmStaffAdd()
        {
            InitializeComponent();
        }


        public int id = 0;

        // 직원 저장 버튼
        public override void btnSave_Click(object sender, EventArgs e)
        {
            // 데이터 저장용, 수정용 쿼리 
            string qry = "";
            if (id == 0)
            {
                qry = "insert into staff values (@Name, @phone, @role)";
            }
            else
            {
                qry = "update staff set sName = @Name, sPhone = @phone, sRole = @role where staffID = @id";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName1.Text);
            ht.Add("@phone", txtPhone.Text);
            ht.Add("@role", cbRole.Text);

            if (MainClass.SQL(qry, ht) > 0)
            {
                // 초기화
                guna2MessageDialog1.Show("저장 완료");
                id = 0;
                txtName1.Text = "";
                txtPhone.Text = "";
                cbRole.SelectedIndex = -1;
                txtName1.Focus();
            }


        }

    }
}
