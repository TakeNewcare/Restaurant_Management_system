using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CM
{
    internal class MainClass
    {


        // 로그인한 사용자 닉네임을 가져온다.
        public static string user;

        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }

        // sql 흐름
        // SqlConnection 클래스로 한번 연결한 후,          (한번만 연결하면 된다. 연결상태 확인용도 있으면 좋음)
        // SqlCommand, SqlDataAdapter, SqlDataReader 를 사용하여 읽고 조작한다.

        // mssql 연결 객체 종류
        // SqlConnection : 서버와 연결을 설정하는데 사용

        // SqlCommand : 단순히 쿼리를 실행하고 그 결과를 받아오는데 사용
        // SqlDataAdapter : 데이터베이스에서 데이터를 읽고 쓰는데 사용된다!!(상호작용)(데이터베이스 삽입, 업데이트, 삭제하는데 사용)
        // SqlDataReader : 데이터베이스에서 데이터를 읽어오는데만 사용(데이터를 수정하거나 업데이트 하지 않는다.), 데이터를 순차적으로 읽어온다.


        // 데이터 베이스 connection 쿼리
        // Data Source = 데이터베이스 전체 이름(!!!);
        // Initial Catalog= 데이터베이스명;
        // Persist Security Info=True;   : 데이터베이스 연결 문자열에서 사용되는 옵션으로 true 이면 사용자가 연결 문자열에 포함된 비밀번호를 볼 수 있게 된다
        // User ID = mssql 아이디;
        // Password= mssql 비밀번호;
        public static readonly string con_string = "Data Source = proPC; Initial Catalog=CD;  TrustServerCertificate=True; Persist Security Info=False; User ID=sa; Password=1234;";
        public static SqlConnection con = new SqlConnection(con_string);  // 데이터 베이스와의 실제 연결을 담당한다.


        // 로그인 하려는 사람의 아이디와 비밀번호를 받는다.
        public static bool IsValidUser(string user, string pass)
        {
            bool isValid = false;

            string qry = $@"select * from users where username = '{user}' and userpass = {pass}";
            // @"Select * from users where username = '" + user + "' and userpass = '" + pass + "' ";

            // @ 리터럴 표기법 : 문자열 내에서 특수 기호를 문자 그대로 사용하기 위한 표기법

            SqlCommand cmd = new SqlCommand(qry, con);  // con으로 연결된 데이터 베이스에 qry 쿼리문 정의
            DataTable dt = new DataTable();   // 데이터를 저장하는 데 사용되는 테이블로 da.Fill(dt)에서 쿼리의 결과 데이터를 저장한다
            SqlDataAdapter da = new SqlDataAdapter(cmd);  // 정의한 쿼리문을 가져가서 데이터를 추출하여 가져온다
            da.Fill(dt);   // da가 가져온 데이터를 dt에 채운다.

            if (dt.Rows.Count > 0)
            {
                isValid = true;
                USER = dt.Rows[0]["uName"].ToString();

            }

            return isValid;
        }


        // 목록 관련 메서드
        // 쿼리문과 테이블을 받아서 실행하고 영향 받은 행의 수를 반환한다.
        public static int SQL(string qry, Hashtable ht)   // Hashtable : 데이터 구조 중 하나로 키와 값 쌍을 가진다.
        {

            int res = 0;

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;  // 쿼리 타입이 텍스트형식으로 되었다는 것을 명시하지만 기본값이다.

                foreach (DictionaryEntry item in ht)  // DictionaryEntry : 구조체로 Hashtable의 키-값 쌍을 나타낸다.
                {

                    // sqlcommand 객체의 parameters 속성을 이용해 sql 쿼리에 매개변수를 추가한다. => SQL 쿼리 문자열에서 '@매개변수이름' 형식으로 매개변수를 사용
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                res = cmd.ExecuteNonQuery();  // cms 객체에서 영향받은 행의 수를 반환한다.

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
                con.Close();

            }

            return res;
        }


        // Hashtable : 키와 값 형태의 쌍 데이터

        // DataGridView : 행과 열 형태의 테이블 형식으로 데이터를 편집, 정렬 등을 하여 사용자 인터페이스에 데이터를 표시(시각화!!!!!!)하는 곳에 사용된다..
        // DataTable : 행과 열 형식으로 각 행은 데이터를 의미한다.
        //              주로 데이터를 저장하고 관리하는 목적
        // ListBox : Windows Forms 애플리케이션에서 사용자에게 목록을 제공해준다.

        // => DataTable은 데이터를 추가, 수정, 검색하는 등의 데이터 관리 작업을 위해 사용 / DataGridView는 데이터를 시각적으로 표시하고 사용자가 데이터를 편집하거나 선택할 때 사용


        // 받아온 쿼리를 실행해서 화면으로 출력해준다.
        public static void LoadDate(string qry, DataGridView gv, ListBox lb)    // ListBox : 니가 아는 그 리스트박스
        {
            // 그리드뷰의 첫번째 항목인 항목별 번호 세팅
            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);

            // 열 매핑
            try
            {

                SqlCommand cmd = new SqlCommand(qry, con);          // con 객체를 이용하여 cmd를 생성 
                cmd.CommandType = CommandType.Text;                 // 
                SqlDataAdapter da = new SqlDataAdapter(cmd);        // cmd 객체를 이용하여 da를 생성 후 정의된 쿼리를 데이터 베이스로 전송하고 결과를 가져온다.
                DataTable dt = new DataTable();                     // 키-값쌍
                da.Fill(dt);                                        // 가져온 데이터를 dt에 채운다  => datatable은 column과 row 형태로 데이터 저장

                for (int i = 0; i < lb.Items.Count; i++)
                {
                    string colNam1 = ((DataGridViewColumn)lb.Items[i]).Name;            // 리스트박스 항목들(열 이름!!!)을 datagrid 형태로 변환 후 이름을 colNam1에 넣는다..
                    gv.Columns[colNam1].DataPropertyName = dt.Columns[i].ToString();      // i번째 열의 이름을 문자열로 가져와 데이터 그리드 뷰의 컬럼으로 사용한다
                }
                // =>  DataGridView의 colNam1이라는 이름의 열(Column)이 DataTable의 i번째 열(Column)과 데이터 바인딩되도록 설정하는 것


                gv.DataSource = dt;         // DataGridView gv의 데이터 소스를 DataTable dt로 설정하여 dt의 데이터를 gv에 표시하게 한다. 그래서 열 매핑 과정을 거친 것

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                con.Close();
            }
        }

        public static void BlurBackground(Form form)
        {
            Form Background = new Form();
            using (form)
            {
                Background.StartPosition = FormStartPosition.Manual;   // 개발자가 직접 지정
                Background.FormBorderStyle = FormBorderStyle.None;
                Background.Opacity = 0.5d;                   // 투명도
                Background.BackColor = Color.Black;
                Background.Size = fkmMain.instance.Size;        // 메인폼 사이즈 그대로 받아온다
                Background.Location = fkmMain.instance.Location;
                Background.ShowInTaskbar = false;               // 폼이 작업 표시줄(Taskbar)에 표시될지 여부를 설정
                Background.Show();
                form.Owner = Background;                // 다중 창 관리, 부모-자식 창 관계를 설정(폼을 생성 된 후에 설정이 가능하다)
                form.ShowDialog(Background);            // form이 Background 폼의 자식 으로 설정되면서 모달리스 형태로 나오게 된다.
                Background.Dispose();                   // 이미 윗줄에서 출력한 상태이기에 리소스 제거하면 실제로는 사라지지만 화면에는 나타난 상태가 유지된다.
            }
        }


        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI2.WinForms.Guna2DataGridView gv = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int count = 0;

            foreach (DataGridViewRow row in gv.Rows)        // 모든 열을 세면서 카운트 값을 넣어준다.
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        public static void CBFill(string qry, ComboBox cb)
        {
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cb.DisplayMember = "name";
            cb.ValueMember = "id";
            cb.DataSource = dt;
            cb.SelectedIndex = -1;


        }



    }
}
