# <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=duckdb&logoColor=red"/> Minesweeper
   [![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2FTakeNewcare&count_bg=%23939DAE&title_bg=%2361ACCD&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)
   
<br>

<p align="center">
  <img src ="../main/image/%EC%8B%9C%EC%9E%91%ED%99%94%EB%A9%B4.png"  width="400" height="400">
  <img src ="../main/image/%ED%8F%AC%EC%8A%A4%EA%B8%B0.png"  width="400" height="400">
</p>





## <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=googledocs&logoColor=black"/> Project Info
이번 프로젝트는 winform 프로젝트 중 흔하게 접할 수 있는 레스토랑 관리 시스템을 작업하였습니다.<br>
이번 프로젝트를 통해 심도있는 winform 기능들과 guna.UI2<br>
그리고 mssql과 연동하여 실시간 서버와의 실시간 데이터 관리와 crystal report를 이용한 기본적인 영수증 form 작성을 학습할 수 있었습니다.

Reason for making: studying c# and winform,  <br>
Busan Polytechnic High-Tech Course <br>
Development period: 7 days <br>
<br>

## 개발팀 소개
<table>
  <tr>
    <th>정진영</th>
    <td  rowspan="3">

      내용적기  
    
    
    </td>
  </tr>
  <tr>
    <td> <img src ="https://github.com/TakeNewcare/Project/assets/163362484/c1ae717b-4156-4085-826f-7113ff18cf5d"  width="200" height="200"></td>
  </tr>
  <tr>
    <td align='center'>wlsdud1525@naver.com</td>
  </tr>
</table>
 

## Stacks
### Environment
<img src="https://img.shields.io/badge/visualstudio-5C2D91?style=flat-square&logo=visualstudio&logoColor=white"/><img src="https://img.shields.io/badge/github-181717?style=flat-square&logo=github&logoColor=white"/>

### Development
<img src="https://img.shields.io/badge/.NET-512BD4?style=flat-square&logo=.NET&logoColor=white"/> <img src="https://img.shields.io/badge/csharp-512BD4?style=flat-square&logo=csharp&logoColor=white"/> <img src="https://img.shields.io/badge/microsoftsqlserver-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white"/> 

### Communication
<img src="https://img.shields.io/badge/slack-4A154B?style=flat-square&logo=slack&logoColor=white"/>

### <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=airplayvideo&logoColor=black"/>Screen configuration
|Start|Win|Lose|
|:---:|:---:|:---:|
|<img src ="https://github.com/TakeNewcare/Project/assets/163362484/d01b57bc-6ecf-49df-90e1-6dbecaa22806"  width="250" height="300">|<img src ="https://github.com/TakeNewcare/Project/assets/163362484/4835c918-7354-409a-bb5a-031a4a94cab0"  width="250" height="300">|<img src ="https://github.com/TakeNewcare/Project/assets/163362484/43eee671-87e4-4f2d-b474-151f8441f9b7"  width="250" height="300">|
|**10x10**|**15x15**|**20x20**|
|<img src ="https://github.com/TakeNewcare/Project/assets/163362484/a5300c74-990c-4456-b741-94863e45d85b"  width="250" height="300">|<img src ="https://github.com/TakeNewcare/Project/assets/163362484/3573918e-6900-458c-b7a2-75090f4cadde"  width="250" height="300">|<img src ="https://github.com/TakeNewcare/Project/assets/163362484/ce9581cb-77e8-4fb3-9e81-00a57a254441"  width="250" height="300">|

## 

## main function
### 1. 안전지대 클릭 시 주변 오픈 기능
```
          // 클릭한 셀이 안전지대(.)이면 열고, 열린 지역도 안전지대이면 재귀함수로 한번 더 오픈
        public void OpenSurround(int x, int y)
        {
              // 클릭한 셀이 최소 / 최대 셀일 시 주변 8칸을 찾을 때 셀의 최소 인덱스, 최대 인덱스를 넘지 않게 찾기 위한 max, min 함수
              // 클릭한 셀이 0일 경우 0-1을 하면 -1이고 -1이란 셀이 없기 때문에 -1과 0 둘 중 최댓값 선택하기
            for (int i = Math.Max(x - 1, 0); i <= Math.Min(x + 1, current_row - 1); i++)
            {
                for (int j = Math.Max(y - 1, 0); j <= Math.Min(y + 1, current_col - 1); j++)
                {
                    if (cells[i, j].Text != "")   // 이미 오픈된 박스이면 패스
                        continue;

                    OpenCell(cells[i, j]);


                    if (cells[i, j].Text == ".")   // 만약 새로 오픈한 곳이 안전지대이면 해당 위치 기준으로 재귀함수 사용
                        OpenSurround(i, j);
                }
            }
        }

          // 경계라인 클릭 시 주변 범위 보여주기
        private void Surround_Sign(int x, int y)
        {
            for (int i = Math.Max(x - 1, 0); i <= Math.Min(x + 1, current_row); i++)
            {
                for (int j = Math.Max(y - 1, 0); j <= Math.Min(y + 1, current_col); j++)
                {
                    if (cells[i, j].Text != "")
                        continue;

                    cells[i, j].BackColor = Color.FromArgb(0, 153, 153);

                }
            }
        }
```

### 2. 플래그 기능
```
리드미 작성 방법 : https://velog.io/@luna7182/%EB%B0%B1%EC%97%94%EB%93%9C-%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8-README-%EC%93%B0%EB%8A%94-%EB%B2%95
