using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CM.Model
{
    public partial class fkmPOS : Form
    {
        public fkmPOS()
        {
            InitializeComponent();
        }

        // 닫음 버튼
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 포스기 불러오기
        private void fkmPOS_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();

            ProductPanel.Controls.Clear();
            LoadProducts();
        }

        //카테고리 패널에 카테고리 항목을 추가한다.
        private void AddCategory()
        {
            string qry = "select * from Category";  // 모든 항목 불러오기
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            CategoryPanel.Controls.Clear();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                    b.FillColor = Color.FromArgb(50, 55, 89);
                    b.Size = new Size(190, 80);
                    b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    b.Text = row["catName"].ToString();

                    // 카테고리 버튼 클릭
                    b.Click += new EventHandler(b_Click);

                    CategoryPanel.Controls.Add(b);
                }

            }

        }

        // 클릭된 카테고리 항목만 보이기
        private void b_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;  // 클릭된 버튼 찾기

            if (b.Text == "전체 종류")
            {
                txtSearch.Text = "1";
                txtSearch.Text = "";
                return;
            }

            foreach (var item in ProductPanel.Controls)
            {
                var pro = (urProduct)item;
                pro.Visible = pro.PCategory.Contains(b.Text);   // 모든 항목의 visible 속성값을 true / false로 준다.
            }
        }



        // 판넬에 제품 넣기
        private void LoadProducts()
        {
            string qry = "Select * from products inner join category on catID = CategoryID"; // products id와 category id 를 연결 시켜 데이터 불러오기
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow item in dt.Rows)  // 조회된 결과값 만큼 반복하여 AddItems() 함수 실행 => ProductPanel에 항목 추가 작업
            {
                Byte[] imagearray = (byte[])item["pImage"];
                byte[] immagebytearray = imagearray;


                // 번호, 아이디, 프로덕트 이름, 카테고리 이름, 가격, 이미지
                AddItems("0", item["pID"].ToString(), item["pName"].ToString(), item["catName"].ToString(),
                    item["pPrice"].ToString(), Image.FromStream(new MemoryStream(immagebytearray)));
            }
        }


        // 검색창 기능
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in ProductPanel.Controls)  // 판넬 안의 항목만큼 반복하여 항목마다 visible 속성 적용
            {
                var pro = (urProduct)item;
                pro.Visible = pro.PName.Contains(txtSearch.Text.Trim());   // 모든 항목의 visible 속성값을 true / false로 준다.
            }
        }



        // product 판넬에 음식 항목 추가 시 음식 항목 생성
        private void AddItems(string id, string proID, string name, string cat, string price, Image Pimage)
        {

            var w = new urProduct()   // 받아온 데이터를 통해 인스턴스 생성
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = Pimage,
                id = Convert.ToInt32(proID)
            };

            ProductPanel.Controls.Add(w);       // 판넬에 추가한다.

            // 각 인스턴스의 함수 정의(이미 있는 항목 계산 또는 새로운 항목 추가)
            // 이미 있는 항목
            w.onSelect += (ss, ee) =>           // null로 정의된 델리게이트에 함수를 넣는다
            {
                var wdg = (urProduct)ss;

                foreach (DataGridViewRow item in guna2DataGridView1.Rows)   // 이미 추가된 동일 인스턴스가 있는지 찾기위해 모든 행을 반복하여 찾는다.
                {
                    if (Convert.ToInt32(item.Cells["dgvproID"].Value) == wdg.id)  // 만약,  클릭된 인스턴스의 id와 데이터 그리드에 추가된 항목의 id가 같으면!!!
                    {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;          // 개수 늘기기
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                                                        double.Parse(item.Cells["dgvPrice"].Value.ToString());      // 총액 변동

                        return;
                    }

                }
                // 새로운 항목 추가
                guna2DataGridView1.Rows.Add(new object[] { 0, 0, wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice });
                // 번호, 아이디, 프로덕트 아이디, 이름, 개수, 가격, 총액(첫 총액은 가격과 동일)


                GetTotal();
            };
        }

        // 데이터 그리드의 첫번째 열(항목 번호) 수정 - 각 셀이 그려지기 전에 이벤트가 발생한다
        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
            GetTotal();
        }

        // 전체 가격
        private void GetTotal()
        {
            double tot = 0;
            lblTotal.Text = "";
            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                tot += double.Parse(item.Cells["dgvPrice"].Value.ToString()) * double.Parse(item.Cells["dgvQty"].Value.ToString());
            }

            lblTotal.Text = tot.ToString("N2");
        }


        // 모든 내용 초기화
        public int MainID = 0;  // 생성, 수정 구분
        private void btnNew_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            guna2DataGridView1.Rows.Clear();
            MainID = 0;
            lblTotal.Text = "00";
        }

        // 오더타입 설정
        public string OrderType;



        // 오더타입 : 배달 
        public string customerName = "";
        public string customerPhone = "";

        // 배송 클릭
        private void btnDelivery_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "배달";

            fkmAddCustomer frm = new fkmAddCustomer();
            frm.mainID = MainID;
            frm.orderType = OrderType;
            MainClass.BlurBackground(frm);

            if (frm.txtName1.Text != "") // as take away did not have driver infro
            {
                driverID = frm.driverID;
                lblDriverName.Text = "고객명: " + frm.txtName1.Text + " 고객 번호: " + frm.txtPhone.Text + " 배달기사: " + frm.cbDriver.Text;
                lblDriverName.Visible = true;
                customerName = frm.txtName1.Text;
                customerPhone = frm.txtPhone.Text;
            }
        }


        public int driverID = 0;


        // 오더타입 : 포장
        private void btnTake_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "포장";

            fkmAddCustomer frm = new fkmAddCustomer();
            frm.mainID = MainID;
            frm.orderType = OrderType;
            MainClass.BlurBackground(frm);

            if (frm.txtName1.Text != "") // as take away did not have driver infro
            {
                driverID = frm.driverID;
                lblDriverName.Text = "고객명: " + frm.txtName1.Text + " 고객번호: " + frm.txtPhone.Text;
                lblDriverName.Visible = true;
                customerName = frm.txtName1.Text;
                customerPhone = frm.txtPhone.Text;
            }

        }

        // 식당 버튼 클릭 시
        // 테이블 선택 화면과 직원 선택 화면이 순차적으로 나온다(BlurBackground의 모달리스 효과)
        private void btnDin_Click(object sender, EventArgs e)
        {
            OrderType = "식당";
            lblDriverName.Visible = false;

            fkmTableSelect frm = new fkmTableSelect();
            MainClass.BlurBackground(frm);
            if (frm.TableName != "")
            {
                lblTable.Text = frm.TableName;              // 선택한 테이블과 직원을 pos 화면에 보여주기
                lblTable.Visible = true;
            }
            else
            {
                lblTable.Text = "";
                lblTable.Visible = false;
            }

            fkmWaiterSelect frm2 = new fkmWaiterSelect();
            MainClass.BlurBackground(frm2);
            if (frm2.waiterName != "")
            {
                lblWaiter.Text = frm2.waiterName;
                lblWaiter.Visible = true;

            }
            else
            {
                lblWaiter.Text = "";
                lblWaiter.Visible = false;
            }
        }

        // 테이블, 직원, 오더 타입 등 선택 후 클릭하면 예약 상태로 잡히는 버튼
        private void btnKot_Click(object sender, EventArgs e)
        {

            string qry1 = ""; //Main table
            string qry2 = ""; // Detail table

            int detailID = 0;

            if (MainID == 0) // 추가
            {
                // tblMain 테이블에 날짜, 시간, 테이블 번호, 웨이터 이름, 상태... 등을 추가한다.
                qry1 = @"insert into tblMain Values(@aDate, @aTime, @TableName, @waiterName, 
                            @status, @orderType, @total, @received, @change, @driverID, @CustName, @CustPhone);
                            select scope_identity()"; // 자동 증가 열을 가진 테이블에 데이터를 삽입한 후에 삽입된 행의 IDENTITY 값을 가져올 때 사용한다?
            }
            else // 수정
            {
                qry1 = @"update tblMain set status =  @status, total = @total, 
                        received = @received, change = @change where MainID = @ID";
            }

            SqlCommand cmd = new SqlCommand(qry1, MainClass.con);
            cmd.Parameters.AddWithValue("@ID", MainID);
            cmd.Parameters.AddWithValue("@aDate", Convert.ToDateTime(DateTime.Now.Date));  // 현재 날짜,  convert~~ : 형변환에 사용되는 클래스
            cmd.Parameters.AddWithValue("@aTime", DateTime.Now.ToShortTimeString());        // 현재 시간
            cmd.Parameters.AddWithValue("@TableName", lblTable.Text);
            cmd.Parameters.AddWithValue("@waiterName", lblWaiter.Text);
            cmd.Parameters.AddWithValue("@status", "주문 완료");
            cmd.Parameters.AddWithValue("@orderType", OrderType);
            cmd.Parameters.AddWithValue("@total", Convert.ToDouble(lblTotal.Text));  // 총액
            cmd.Parameters.AddWithValue("@received", Convert.ToDouble(0));      // 받은 돈 => 계산(체크아웃) 전이라 0으로 세팅
            cmd.Parameters.AddWithValue("@change", Convert.ToDouble(0));        // 잔돈  => 계산(체크아웃) 전이라 0으로 세팅
            cmd.Parameters.AddWithValue("@driverID", driverID);
            cmd.Parameters.AddWithValue("@CustName", customerName);
            cmd.Parameters.AddWithValue("@CustPhone", customerPhone);

            // 서버가 닫힌 상태면 연다 => 생성이면 쿼리 결과로 나온 첫번째 열의 첫번째 행의 값을 가져와 mainID에 넣는다.
            //                          => 수정이면 그냥 쿼리 실행으로 영향받은 행의 수 반환.
            if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }
            if (MainID == 0) { MainID = Convert.ToInt32(cmd.ExecuteScalar()); } else { cmd.ExecuteNonQuery(); }
            if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }


            foreach (DataGridViewRow row in guna2DataGridView1.Rows)  // 메뉴가 추가된 만큼 반복문을 돌려 tblDetails 테이블에 넣는다.
            {
                detailID = Convert.ToInt32(row.Cells["dgvid"].Value);

                if (detailID == 0)
                {
                    qry2 = @" insert into tblDetails Values(@MainID, @proID, @qty, @price, @amount)";
                }
                else
                {
                    qry2 = @" UPdate tblDetails Set proID = @proID, qty = @qty, price = @price, amount = @amount
                                where DetailID = @ID";
                }

                SqlCommand cmd2 = new SqlCommand(qry2, MainClass.con);          // 데이터 그리드 각 행의 각 열의 데이터값을 넣는다.
                cmd2.Parameters.AddWithValue("@ID", detailID);
                cmd2.Parameters.AddWithValue("@MainID", MainID);
                cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproID"].Value));
                cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvQty"].Value));
                cmd2.Parameters.AddWithValue("@price", Convert.ToDouble(row.Cells["dgvPrice"].Value));
                cmd2.Parameters.AddWithValue("@amount", Convert.ToDouble(row.Cells["dgvAmount"].Value));

                if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }
                cmd2.ExecuteNonQuery();
                if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }
            }

            guna2MessageDialog1.Show("주문 성공!");

            // 초기화
            guna2DataGridView1.Rows.Clear();
            MainID = 0;
            detailID = 0;
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            lblTotal.Text = "00";
            lblDriverName.Text = "";
            btnDin.Checked = false;
            btnDelivery.Checked = false;
            btnTake.Checked = false;
        }



        //// 영수증 확인  
        public int id = 0;
        // 예약 상태가 아닌 주문의 영수증을 보여준다.
        private void btnBill_Click(object sender, EventArgs e)
        {
            fkmBillList frm = new fkmBillList();
            MainClass.BlurBackground(frm);  // => frm에서 수정 작업을 하면 MainID값이 바뀌면서
            if (frm.MainID > 0)
            {
                id = frm.MainID;
                MainID = frm.MainID;
                LoadEntries();
            }

        }


        private void LoadEntries()
        {
            string qry = @"select * from tblMain m
                                     inner join tblDetails d on m.MainID = d.MainID
                                       inner join products p on p.pID = d.proID
                                               where m.MainID = " + id + "";


            SqlCommand cmd2 = new SqlCommand(qry, MainClass.con);
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);

            if (dt2.Rows[0]["orderType"].ToString() == "배달")
            {
                btnDelivery.Checked = true;
                lblWaiter.Visible = false;
                lblTable.Visible = false;
            }
            else if (dt2.Rows[0]["orderType"].ToString() == "포장")
            {
                btnTake.Checked = true;
                lblWaiter.Visible = false;
                lblTable.Visible = false;
            }
            else
            {
                btnDin.Checked = true;
                lblWaiter.Visible = true;
                lblTable.Visible = true;
            }

            guna2DataGridView1.Rows.Clear();

            foreach (DataRow item in dt2.Rows)
            {
                lblTable.Text = item["TableName"].ToString();
                lblWaiter.Text = item["waiterName"].ToString();

                string detailid = item["DetailID"].ToString();
                string proName = item["pName"].ToString();

                string proid = item["proID"].ToString();
                string qty = item["qty"].ToString();
                string price = item["price"].ToString();
                string amount = item["amount"].ToString();


                object[] obj = { 0, detailid, proid, proName, qty, price, amount };
                guna2DataGridView1.Rows.Add(obj);
            }
            GetTotal();
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {

            fkmCheckout frm = new fkmCheckout();
            frm.MainID = id;
            frm.amt = Convert.ToDouble(lblTotal.Text);
            MainClass.BlurBackground(frm);


            guna2DataGridView1.Rows.Clear();
            MainID = 0;
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            lblTotal.Text = "00";
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            string qry1 = "";
            string qry2 = "";
            int detailID = 0;

            if (OrderType == "")
            {
                guna2MessageDialog1.Show("배달 방식을 선택해 주세요");
                return;
            }

            if (MainID == 0)
            {
                qry1 = @"insert into tblMain Values(@aDate, @aTime, @TableName, @waiterName, 
                            @status, @orderType, @total, @received, @change, @driverID, @CustName, @CustPhone);
                            select scope_identity()";
            }
            else
            {
                qry1 = @"update tblMain set status =  @status, total = @total, 
                        received = @received, change = @change where MainID = @ID";
            }

            SqlCommand cmd = new SqlCommand(qry1, MainClass.con);
            cmd.Parameters.AddWithValue("@ID", MainID);
            cmd.Parameters.AddWithValue("@aDate", Convert.ToDateTime(DateTime.Now.Date));
            cmd.Parameters.AddWithValue("@aTime", DateTime.Now.ToShortTimeString());
            cmd.Parameters.AddWithValue("@TableName", lblTable.Text);
            cmd.Parameters.AddWithValue("@waiterName", lblWaiter.Text);
            cmd.Parameters.AddWithValue("@status", "대기");
            cmd.Parameters.AddWithValue("@orderType", OrderType);
            cmd.Parameters.AddWithValue("@total", Convert.ToDouble(lblTotal.Text));  // as we only saving data for kitchen value will update when payment received
            cmd.Parameters.AddWithValue("@received", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@change", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@driverID", driverID);
            cmd.Parameters.AddWithValue("@CustName", customerName);
            cmd.Parameters.AddWithValue("@CustPhone", customerPhone);

            if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }
            if (MainID == 0)
            {
                MainID = Convert.ToInt32(cmd.ExecuteScalar());

            }
            else { cmd.ExecuteNonQuery(); }
            if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }


            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                detailID = Convert.ToInt32(row.Cells["dgvid"].Value);

                if (detailID == 0)
                {
                    qry2 = @" insert into tblDetails Values(@MainID, @proID, @qty, @price, @amount)";
                }
                else
                {
                    qry2 = @" UPdate tblDetails Set proID = @proID, qty = @qty, price = @price, amount = @amount
                                where DetailID = @ID";
                }

                SqlCommand cmd2 = new SqlCommand(qry2, MainClass.con);
                cmd2.Parameters.AddWithValue("@ID", detailID);
                cmd2.Parameters.AddWithValue("@MainID", MainID);
                cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproID"].Value));
                cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvQty"].Value));
                cmd2.Parameters.AddWithValue("@price", Convert.ToDouble(row.Cells["dgvPrice"].Value));
                cmd2.Parameters.AddWithValue("@amount", Convert.ToDouble(row.Cells["dgvAmount"].Value));

                if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }
                cmd2.ExecuteNonQuery();
                if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }
            }

            guna2MessageDialog1.Show("대기 성공!");
            guna2DataGridView1.Rows.Clear();
            MainID = 0;
            detailID = 0;
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            lblTotal.Text = "00";
            lblDriverName.Text = "";
        }
    }
}
