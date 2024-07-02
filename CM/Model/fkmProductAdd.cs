using System;
using System.Collections;
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
    public partial class fkmProductAdd : SampleAdd
    {
        public fkmProductAdd()
        {
            InitializeComponent();
        }



        // 이미지 선택 버튼
        string filePath;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg, .png)|* .png; *.jpg";        // 확장자 설정
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                txtImage.Image = new Bitmap(filePath);          // 파일 이름으로 이미지 참조
            }
        }

        // 제품 추가 화면 열릴 때 콤보박스 항목 선택
        public int cID = 0;
        public int id = 0;

        private void fkmProductAdd_Load(object sender, EventArgs e)
        {
            string qry = "select catid 'id', catName 'name' from category";

            MainClass.CBFill(qry, cbCat);       // 콤보박스에 채우기

            // 수정을 위한 것
            if (cID > 0)
            {
                cbCat.SelectedValue = cID;
            }

            if (id > 0)
            {
                ForUpdateLoadDate();
            }
        }

        // productview에서 수정버튼 클릭 시 선택 항목을 불러온다.
        Byte[] imageByteArray;
        private void ForUpdateLoadDate()
        {
            string qry = @"select * from products where pID = " + id + "";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtName1.Text = dt.Rows[0]["pName"].ToString();
                txtPrice.Text = dt.Rows[0]["pPrice"].ToString();

                Byte[] imageArray = (byte[])(dt.Rows[0]["pImage"]);  // pImage 열의 데이터를 받아 byte배열로 변환하여 저장한다.
                byte[] imageByteArray = imageArray;

                txtImage.Image = Image.FromStream(new MemoryStream(imageArray));
                // new MemoryStream()  : 바이트배열을 다루는 memorystram 객체 생성
                // Image.FromStream()  : 스트림에서 이미지를 로드한다
            }
        }


        // 저장버튼 클릭 시 해당 데이터 저장
        public override void btnSave_Click(object sender, EventArgs e)
        {
            // 데이터 저장용, 수정용 쿼리 
            string qry = "";
            if (id == 0)
            {
                qry = "insert into products values (@Name, @price, @cat, @img)";
            }
            else
            {
                qry = "Update products set pName = @Name, pPrice = @price, CategoryID = @cat, pImage = @img where pID = @id";
            }

            Image temp = new Bitmap(txtImage.Image);  // 선택한 이미지를 temp 변수에 저장
            MemoryStream ms = new MemoryStream();       // 이미지 저장을 위한 MemoryStream 객체
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);  // 선택한 이미지를 png 형식으로 변환 후 ms에 저장한다
            imageByteArray = ms.ToArray();              // 저장한 이미지를 바이트 배열 형식으로 변환

            Hashtable ht = new Hashtable(); // 키-값

            ht.Add("@id", id);
            ht.Add("@Name", txtName1.Text);
            ht.Add("@price", txtPrice.Text);
            ht.Add("@cat", Convert.ToInt32(cbCat.SelectedValue));
            ht.Add("@img", imageByteArray);

            if (MainClass.SQL(qry, ht) > 0)
            {
                // 저장 후 데이터 초기화
                guna2MessageDialog1.Show("저장 완료");
                id = 0;
                cID = 0;
                txtName1.Text = "";
                txtPrice.Text = "";
                cbCat.SelectedIndex = 0;
                cbCat.SelectedIndex = -1;
                txtImage.Image = CM.Properties.Resources.add_product;


                txtName1.Focus();
            }


        }
    }
}
