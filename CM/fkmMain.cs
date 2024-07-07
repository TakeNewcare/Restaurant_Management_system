using CM.Model;
using CM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CM
{
    public partial class fkmMain : Form
    {
        public fkmMain()
        {
            InitializeComponent();
        }

        // 종료
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // 로그인을 통해 해당 폼이 호출되며, 메인클래스의 USER 프로퍼티를 통해 username을 받아온다
        private void fkmMain_Load(object sender, EventArgs e)
        {
            lblUser.Text = MainClass.USER;
            btnHome.Checked = true;
            AddControls(new fkmHome());

            _obj = this;   // instance 프로퍼티를 통해 get을 하여 fkmMain 폼을 불러온다.
        }

        static fkmMain _obj;

        public static fkmMain instance
        {
            get
            {
                if (_obj == null) { _obj = new fkmMain(); }
                return _obj;
            }
        }



        // ★ 항목 선택 시 해당 폼을 centerpanel에 보여준다
        public void AddControls(Form form)
        {
            CenterPanel.Controls.Clear();  // 일단, 비우기
            form.Dock = DockStyle.Fill;  // 패널 전체를 채우는 형식으로 설정
            form.TopLevel = false;  // 부모인 mainform에 속하게 만들기 위해
            CenterPanel.Controls.Add(form);
            form.Show();
        }

        // 홈 화면 호출
        private void fkmhome_Click(object sender, EventArgs e)
        {
            AddControls(new fkmHome());
        }

        // 카테고리 화면 호출
        private void fkmCategory_Click(object sender, EventArgs e)
        {
            AddControls(new fkmCategoryView());

        }

        // 테이블 화면 호출
        private void fkmTable_Click(object sender, EventArgs e)
        {
            AddControls(new fkmTableView());
        }

        // 직원 관리 화면 호출
        private void fkmStaff_Click(object sender, EventArgs e)
        {
            AddControls(new fkmStaffView());

        }

        // 제품 화면 호출
        private void fkmProduct_Click(object sender, EventArgs e)
        {
            AddControls(new fkmProductView());

        }



        private void fkmPos_Click(object sender, EventArgs e)
        {
            fkmPOS frm = new fkmPOS();
            frm.Show();
        }
        private void btnKichen_Click(object sender, EventArgs e)
        {
            AddControls(new fkmKitchenView());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            AddControls(new fkmReports());

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            AddControls(new fkmHome());
        }
    }
}
