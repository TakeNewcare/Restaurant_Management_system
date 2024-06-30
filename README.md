# <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=duckdb&logoColor=red"/> Restaurant Management System
   [![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2FTakeNewcare&count_bg=%23939DAE&title_bg=%2361ACCD&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)
   
<br>

<p align="center">
  <img src ="../main/image/login.png"  width="200" height="300" align='left'>
  <img src ="../main/image/POS.png"  width="400" height="300">
</p>


## <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=googledocs&logoColor=black"/> Project Info
저의 두번째 프로젝트는 winform 프로젝트 중 흔하게 접할 수 있는 레스토랑 관리 시스템을 작업하였습니다.<br><br>

관리자가 로그인하여 제품의 카테고리 부터 제품, 직원 그리고 테이블까지 설정할 수 있으며,<br>
포스기를 통해 테이블과 직원을 선택하고 배달, 포장, 식사 등의 배송 형태 그리고 요리를 시작할지 말지를 결정하는 예약 상태와 주문 상태를 선택할 수 있습니다.<br>
또한, crystal report를 이용하여 영수증, 제품 리스트, 직원 리스트 등을 만들었으며,<br>
모든 작업은 MSSQL을 바탕으로 제작되었습니다.<br>
<br><br>
Reason for making: studying c#, winform(Guna.UI2), crystal report, MSSQL <br>
Development period: 7 days <br>
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
    처음 접해보는 분야라 많은 두려움이 있었지만,<br> 오류가 났을 때 몇 시간이 걸리더라도 해결해야 직성이 풀리는 저의 성격과 잘 맞아 
    꾸준히 성장하고 있습니다. <br>감사합니다.
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
<img src="https://img.shields.io/badge/.NET-512BD4?style=flat-square&logo=.NET&logoColor=white"/> <img src="https://img.shields.io/badge/csharp-512BD4?style=flat-square&logo=csharp&logoColor=white"/> <img src="https://img.shields.io/badge/csharp-512BD4?style=flat-square&logo=csharp&logoColor=white"/> <img src="https://img.shields.io/badge/csharp-512BD4?style=flat-square&logo=csharp&logoColor=white"/> 

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
    Persist Security Info=True;   : 데이터베이스 연결 문자열에서 사용되는 옵션으로 true 이면 사용자가 연결 문자열에 포함된 비밀번호를 볼 수 있게 된다
    User ID = mssql 아이디;
    Password= mssql 비밀번호;
```
데이터 베이스 활용
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
```
### 2. 플래그 기능
```


```

## <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=googledocs&logoColor=black"/> 새로 알게된 점과 느낀점
처음 작업해 보는 데이터베이스 연동 과정이 생각보다 오래 걸려 매일 새벽 4~5까지 작업한게 힘들었지만,
힘들게 얻어낸 지식들로 막힌 부분이 돌아가는 그 순간, 어릴 때 어려운 수학문제 혼자 고민하고 고민하다 해결했을 때의 기분을 다시 한번 느꼈고
그렇게 얻은 것들을 같이 배우는 사람에게 도움을 줄 수 있어서 좋았습니다.

<br>

