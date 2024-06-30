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
포스기를 통해 테이블과 직원을 선택하고 배달, 포장, 식사 등의 배송 형태 그리고 요리를 시작할지 말지를 결정하는 대기, 주문 등을 선택할 수 있습니다.<br>
또한, crystal report를 이용하여 영수증, 제품 리스트, 직원 리스트 등을 만들었으며,
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

### <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=airplayvideo&logoColor=black"/>Screen configuration
|Start|Win|End|
|:---:|:---:|:---:|
|<img src ="../main/Image/start.png"  width="250" height="300">|<img src ="../main/Image/win.png"  width="250" height="300">|<img src ="../main/Image/end.png"  width="250" height="300">|
|**10x10**|**15x15**|**20x20**|
|<img src ="../main/Image/10x10.png"  width="250" height="300">|<img src ="../main/Image/15x15.png"  width="250" height="300">|<img src ="../main/Image/20x20.png"  width="250" height="300">|

## 

## main function
### 1. 안전지대 클릭 시 주변 오픈 기능
```
// 클릭한 셀이 안전지대(.)이면 주변의 8칸도 열리게하는 코드
public void OpenSurround(int x, int y)
{
    // 사용자가 클릭한 셀이 최소 또는 최대 값을 갖는 셀의 위치이면 주변 8칸을 찾을 때 셀의 최소 또는 최대 위치를 넘지 않게 찾기 위한 max, min 함수
    //     ex) 10x10 게임 중 1행 1열 셀을 클릭 시 -1의 행과 열에 셀이 없기 때문에 Math의 Max와 Min 함수를 통해 0 또는 9를 최소/최대 값으로 설정해준다.
    for (int i = Math.Max(x - 1, 0); i <= Math.Min(x + 1, current_row - 1); i++)
    {
        for (int j = Math.Max(y - 1, 0); j <= Math.Min(y + 1, current_col - 1); j++)
        {
            if (cells[i, j].Text != "")   // 이미 오픈된 박스하면 패스
                continue;

            OpenCell(cells[i, j]);

            if (cells[i, j].Text == ".")   // 만약, 열린 곳이 또 안전 지대일 시 재귀한다.
                OpenSurround(i, j);
        }
    }
}


```

### 2. 플래그 기능
```
// 우클릭 시 깃발, 경계라인 클릭 시 범위를 나타내는 동작 => 플래그 꼽힌 셀은 클릭 막기
private void Flag_btn(object sender, MouseEventArgs e)
{
    Cell c = (Cell)sender;  // 이벤트 발생한 객체 찾기

    // 경계라인 클릭 시 범위 알려주는 기능 => 클릭 이벤트에 걸면 작동 x
    if (c.Text != "" && c.Text != "." && c.Text != "P")
        Surround_Sign(c.x, c.y);
    else
    {
    if (e.Button != MouseButtons.Right)
        return;

    if (game_start_true)   // 첫 클릭이 우클릭이라도 시작으로 간주하고 타이머 시작
    {
        game_start_true = false; // 첫 클릭인 true 값을 false로 돌려 다음 클릭은 타이머 다시 시작하는거 막기
        timer1.Start();
    }

    if (c.Text == "P")
    {
        c.Text = "";
        pan.L1.Text = (flag_count + 1).ToString();
        flag_count += 1;
    }
    else if (c.Text == "")
    {
        c.Text = "P";
        pan.L1.Text = (flag_count - 1).ToString();
        flag_count -= 1;
        c.ForeColor = System.Drawing.Color.Red;
    }
}
```

## <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=googledocs&logoColor=black"/> 새로 알게된 점과 느낀점
이번 프로젝트를 진행하면서 새로 알게된 부분은 label 컨트롤은 다른 컨트롤들과는 다르게 상속받아서 사용할 수 없다는 부분입니다.
<br><br>
지뢰를 만들 때 button 컨트롤을 상속 받는 클래스를 만들어 form에서 반복문을 통해 최대갯수로 인스턴스를 생성하여 배열에 담고  visible 속성을 통해 레벨에 따라 필요한 부분만 보이게 만들었지만,
<br><br>
전광판 객체를 생성할 때 label 컨트롤 상속을 시도하다 알게된 사실이며 label 뿐만 아니라, ListView와 ToolStrip 등 기본 컨트롤 중 몇가지 컨트롤이 상속할 수 없다는 점을 알게 되었습니다.

<br>

