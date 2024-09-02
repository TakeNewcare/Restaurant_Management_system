# <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=duckdb&logoColor=red"/> Restaurant Management System
   [![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2FTakeNewcare&count_bg=%23939DAE&title_bg=%2361ACCD&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)
   
<br>

<p align="center">
  <img src ="../main/image/login.png"  width="200" height="300" align='left'>
  <img src ="../main/image/POS.png"  width="400" height="300">
</p>


## <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=googledocs&logoColor=black"/> Project Info
저의 두번째 프로젝트는 winform 프로젝트 중 흔하게 접할 수 있는 레스토랑 관리 시스템을 작업하였습니다.<br><br>
관리자가 로그인하여 제품의 카테고리 부터 테이블까지 설정할 수 있으며 포스기 화면을 통해 다양한 형태의 주문을 잡을 수 있습니다.<br>
모든 작업은 MSSQL을 바탕으로 제작되었습니다.<br>
<br><br>
Reason for making: studying c#, winform(Guna.UI2), crystal report, MSSQL <br>
Busan Polytechnic High-Tech Course
<br>
<br>

## 개발팀 소개
<table>
  <tr>
    <th>정진영</th>
    <td  rowspan="3">
    안녕하십니까!, 유통업의 물류팀에서 몸을 담고 있다가 IT 부서 분들과 친해져 IT분야에 대해 알게되어 늦은 나이에
    high-tech 과정을 통해 새로운 도전과 성장을 꿈꾸고 있습니다.
   <br>
   <br>
    처음 접해보는 분야라 많은 두려움이 있었지만,<br> 오류가 났을 때 스스로 해결해야 직성이 풀리는 저의 성격과 잘 맞아 
    꾸준히 성장하고 있습니다. <br>감사합니다
    </td>
  </tr>
  <tr>
    <td> <img src ="../main/image/me.JPG"  width="200" height="200"></td>
  </tr>
  <tr>
    <td align='center'>wlsdud1525@naver.com</td>
  </tr>
</table>
 
## Stacks
### Environment
<img src="https://img.shields.io/badge/visualstudio-5C2D91?style=flat-square&logo=visualstudio&logoColor=white"/> <img src="https://img.shields.io/badge/github-181717?style=flat-square&logo=github&logoColor=white"/>

### Development
<img src="https://img.shields.io/badge/-C%23-512BD4?logo=Csharp&style=flat&logo=.NET&logoColor=white"/> <img src="https://img.shields.io/badge/-Winform-FF0000?logo=Csharp&style=flat&logoColor=white"/> <img src="https://img.shields.io/badge/-MSSQL-4479A1?logo=Csharp&style=flat&logoColor=white"/> 
<img src ="../main/image/crystal%20report.jfif"  width="100" height="50">

### <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=airplayvideo&logoColor=black"/>Screen configuration and Description
|로그인|로그인 실패|메인화면|
|:---:|:---:|:---:|
|<img src ="../main/image/login.png"  width="200" height="300">|<img src ="../main/image/LoginFail2.png"  width="200" height="200">|<img src ="../main/image/main.png"  width="400" height="300">|
<br>

|카테고리 리스트 & 추가|제품 리스트 & 추가|테이블 리스트 & 추가|직원 리스트 & 추가|
|:---:|:---:|:---:|:---:|
|<img src ="../main/image/categoryList.png"  width="300" height="200">|<img src ="../main/image/productList3.png"  width="300" height="200">|<img src ="../main/image/table.png"  width="300" height="200">|<img src ="../main/image/staff.png"  width="300" height="200">|
<br>

|포스기|주문 수정|주방 화면|
|:---:|:---:|:---:|
|<img src ="../main/image/POS.png"  width="300" height="200">|<img src ="../main/image/bill.png"  width="300" height="200">|<img src ="../main/image/KI.png"  width="300" height="200">|<img src ="../main/image/staff.png"  width="300" height="200">|
<br>

|결산|메뉴판|주방 화면|주방 화면|
|:---:|:---:|:---:|:---:|
|<img src ="../main/image/result.png"  width="300" height="200">|<img src ="../main/image/result_menu.png"  width="150" height="300">|<img src ="../main/image/result_staff.png"   width="150" height="300">|<img src ="../main/image/result_sale.png"   width="150" height="300">|


## 

## main function
### 1. 데이터베이스 기초 지식
```
sql 흐름
    SqlConnection 클래스로 연결 => 한번만 연결하면 된다. 연결상태 확인용도 있으면 좋음
    SqlCommand, SqlDataAdapter, SqlDataReader 를 사용하여 읽고 조작한다.

    mssql 연결 객체 종류
    SqlConnection : 서버와 연결을 설정하는데 사용
    SqlCommand : 단순히 쿼리를 실행하고 그 결과를 받아오는데 사용
    SqlDataAdapter : cmd를 이용하여 직접적으로 데이터베이스와 상호작용한다.(데이터베이스 삽입, 업데이트, 삭제하는데 사용)
    SqlDataReader : 데이터베이스에서 데이터를 읽어오는데만 사용(데이터를 수정하거나 업데이트 하지 않는다.), 데이터를 순차적으로 읽어온다.

데이터 베이스 connection 쿼리
    Data Source = 데이터베이스 전체 이름(컴퓨터 이름이 아니다!!!);
    Initial Catalog= 데이터베이스명;
    TrustServerCertificate=True; // 신뢰성 확인
    Persist Security Info=True;   : 데이터베이스 연결 문자열에서 사용되는 옵션으로 true 이면 사용자가 연결 문자열에
                                    포함된 비밀번호를 볼 수 있게 된다
    User ID = mssql 아이디;
    Password= mssql 비밀번호;
```
### 2. 데이터 베이스 활용(보고있는 프로젝트 방법 보다 현재 새롭게 진행 중인 프로젝트의 방식이 더 간편한다)
```
// 카테고리 항목을 불러와서 버튼으로 만드는 코드
private void AddCategory()
{
    string qry = "select * from Category";                // Category 테이블의 모든 항목을 불러오는 쿼리
    SqlCommand cmd = new SqlCommand(qry, MainClass.con);  // MainClass에 정의된 con 객체와 쿼리를 연결한다
    SqlDataAdapter da = new SqlDataAdapter(cmd);          // 정의된 쿼리에 맞게 동작한다
    DataTable dt = new DataTable();                       
    da.Fill(dt);                                          // 불러온 Category 테이블의 모든 데이터를 dt에 넣는다.

    CategoryPanel.Controls.Clear();                       // 데이터를 넣기 전 초기화

    if (dt.Rows.Count > 0)
    {
        foreach (DataRow row in dt.Rows)                  // 한 행씩 불러와 row에 넣는다
        {
            Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();    
            b.FillColor = Color.FromArgb(50, 55, 89);
            b.Size = new Size(190, 80);
            b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            b.Text = row["catName"].ToString();           // 불러온 데이터 중 catName 열을 버튼 텍스트에 넣어준다

            // 카테고리 버튼 클릭
            b.Click += new EventHandler(b_Click);        

            CategoryPanel.Controls.Add(b);
        }
    }
}
```
```
// 이미지 선택
string filePath;
private void btnBrowse_Click(object sender, EventArgs e)
{
    OpenFileDialog ofd = new OpenFileDialog();              // OpenFileDialog 객체 (파일을 선택 대화상자) 선언
    ofd.Filter = "Images(.jpg, .png)|*.png; *.jpg";        // 확장자 설정(문자열 형태!!!)
                    // Images(.jpg, .png)" : 사용자에게 표시되는 파일 필터의 설명
                    // | : 구분자
                    // *.png; *.jpg; : 실제 파일 필터링 시 사용디는 확장자 패턴

    if (ofd.ShowDialog() == DialogResult.OK)               // 파일을 선택 대화상자에서 파일 선택하면
    {
        filePath = ofd.FileName;                           // 선택된 파일의 전체 경로(!!!)를 filePath 변수에 할당
        txtImage.Image = new Bitmap(filePath);             // txtImage픽쳐박스의 이미지 속성에 이미지를 그려준다
    }
}

```
```
// 데이터 저장
public override void btnSave_Click(object sender, EventArgs e)
{
    string qry = "";
    if (id == 0)   // productView 폼에서 추가 버튼 클릭 시 id 값이 0으로 설정
    {
        qry = "insert into products values (@Name, @price, @cat, @img)";
    }
    else   // productView 폼에서 데이터그리드뷰 안의 제품 행에서 수정 버튼 클릭 시 id != 0
    {
        qry = "Update products set pName = @Name, pPrice = @price, CategoryID = @cat, pImage = @img where pID = @id";
    }

    Image temp = new Bitmap(txtImage.Image);  // 픽쳐박스(txtImage)의 Image를 Bitmap 객체로 가져와 temp Image 변수에 저장한다.
    MemoryStream ms = new MemoryStream();       // 이미지 저장을 위한 MemoryStream 객체 생성
    temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);  // 선택한 이미지를 png 형식으로 변환 후 MemoryStream객체(ms)에 저장한다
    imageByteArray = ms.ToArray();              // 저장한 이미지를 바이트 배열 형식으로 변환

    Hashtable ht = new Hashtable(); // 키-값 형식의 hashtable 생성

    // hash 테이블에 데이터 그리드 뷰값을 저장한다.(키 값은 쿼리에서 지정한 형태와 같게한다) => Mainclass.Sql에서 각 값을 치환하여 쿼리를 실행한다.
    ht.Add("@id", id);         
    ht.Add("@Name", txtName1.Text);
    ht.Add("@price", txtPrice.Text);
    ht.Add("@cat", Convert.ToInt32(cbCat.SelectedValue));
    ht.Add("@img", imageByteArray);

// SQL 쿼리에 하드코딩된 값을 직접 전달하는 대신, 매개변수화된 쿼리를 사용하여 SQL Injection 공격을 방지, 코드의 재사용성과 유지보수성 향상

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
```


## <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=googledocs&logoColor=black"/> 새로 알게된 점과 느낀점
처음 작업해본 데이터베이스 연동 과정이 생각보다 오래 걸려 매일 새벽 4~5까지 작업한게 힘들었지만,
힘들게 얻어낸 지식들로 막힌 부분이 돌아가는 그 순간, 어릴 때 어려운 수학문제 혼자 고민하고 고민하다 해결했을 때의 기분을 다시 한번 느꼈고
그렇게 얻은 것들을 같이 배우는 사람에게 도움을 줄 수 있어서 좋았습니다.

이미지를 바이트 배열 형태로 다루는 부분과 데이터 베이스와 주고 받는 방식에 대해 새로 배울 수 있었습니다.<br>
해당 프로젝트에서는 이미지를 데이터베이스에만 저장하는 형식이지만 <br>현재 진행 중인 프로젝트에서는 이미지를 프로젝트 내부에 폴더를 생성하여 저장하는 방식이며,<br>
데이터 베이스 또한 Dataset이라는 객체와 인덱스 번호만을 가지고 이용하는 방식이라 다양한 경험이 필요하다고 느꼈습니다.
<br>

