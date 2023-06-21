# FoodCourt

# C#를 이용한 푸드코트 키오스크 개인 프로젝트입니다.

![1](https://github.com/selloriwoo/FoodCourt/assets/39435633/2da42261-0aee-4ecd-b9c3-ee25a3f5ab81)

1. **C# Project**<br />
   + Windows Forms

2. **Tools**<br />
   + SQL Developer Data Modeler
   + SQL Developer

2. **DB**<br />
   +  OracleDB

C# Windows Forms에 오라클 DB를 연동하여 푸드코트의 키오스크를 구현해 보았습니다.

## 상세 구현 내용

<details>
<summary>접기/펼치기</summary>

## 주문 하기

![2](https://github.com/selloriwoo/FoodCourt/assets/39435633/e10c23ab-c00a-4257-9fd6-2745b6450d44)
<br />
<br />
**-타 코트 음식점 버튼 클릭시-** <br />
![2-1](https://github.com/selloriwoo/FoodCourt/assets/39435633/0d049266-b425-45dd-a2d7-95ab37fbc58a)


## 주문 완료

![3](https://github.com/selloriwoo/FoodCourt/assets/39435633/ab27f14d-10fb-4180-a58f-b4004c5a19c5)
<br />
<br />

## DataBase

## -DBModel-

![4](https://github.com/selloriwoo/FoodCourt/assets/39435633/c677d984-c63e-4459-8209-0ad1817d4b26)
+ 음식 코너에 음식점을 넣어주고 메뉴에 각 음식 코너의 메뉴를 넣어준다.
+ 프로그램에서 DB 메뉴들을 불러와 각 음식 이미지 클릭하여 메뉴를 담는다.
+ 메뉴를 선택하고 주문하기 누르면 주문내용에 DB에 입력된다.

## -음식코너-

![7](https://github.com/selloriwoo/FoodCourt/assets/39435633/6ee4b6ce-8e2d-4355-b627-8a490620ada3)
+ 4개의 코너번호를 등록.

## -메뉴-

![6](https://github.com/selloriwoo/FoodCourt/assets/39435633/cb0d629d-8de6-4db3-9250-c623ecec3f16)
+ 각 음식 코너마다 메뉴를 등록.

## -주문-

![5](https://github.com/selloriwoo/FoodCourt/assets/39435633/525436b5-93f2-44b5-bb83-15fcd8d5b45e)
+ 프로그램에서 음식을 주문하면 주문번호가 생기면서 하루마다 주문번호가 1로 초기화된다.

## -주문내용-

![8](https://github.com/selloriwoo/FoodCourt/assets/39435633/8f1b132b-739e-4cbe-a2a7-ce8dd1260472)
+ 주문이 완료 되면 선택한 메뉴들이 DB에 삽입된다.


</details>
