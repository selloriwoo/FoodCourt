using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DB3_1FoodCourt
{
    public partial class OrderList : Form
    {
        
        Form1 form1 = new Form1();
        int[,] stock = new int[4,8]; //각각 음식 주문 갯수
        string[,] menu = new string[4,8]; // 고른 음식
        int ptr = -1; //선택한 음식 순서대로 넣은 위치값
        int foodCourtptr = 0; //선택한 음식점의 위치값
        int[,] pptr = new int[4,24]; // listbox의 index에 있는 값을 갱신하기 위해 위치값 저장
        int orderNum=0; //주문번호
        int sumPrice = 0; //합계
        
        PictureBox[] pictureBox =new PictureBox[8];
        Label[] labels = new Label[8];
        string query;
        DBConn conn;
        string nameRestaurant; //가게 이름 // 실제 구현시 가변 배열로 DB로 가게 수 count해서 음식점 바뀌면 수정 가능하게.
        public OrderList()
        {
            InitializeComponent();
            pictureBox[0] = pictureBox1;
            pictureBox[1] = pictureBox2;
            pictureBox[2] = pictureBox3;
            pictureBox[3] = pictureBox4;
            pictureBox[4] = pictureBox5;
            pictureBox[5] = pictureBox6;
            pictureBox[6] = pictureBox7;
            pictureBox[7] = pictureBox8;
            labels[0] = label1;
            labels[1] = label2;
            labels[2] = label3;
            labels[3] = label4;
            labels[4] = label5;
            labels[5] = label6;
            labels[6] = label7;
            labels[7] = label8;
            this.ControlBox = false;
            string constr = ""; // 여기에 DB 연결 정보 기입
            conn = new DBConn(constr);

            //첫 로딩시 default aa 코트 메뉴


            //모든 수량 0로 초기화
            for (int i = 0; i < stock.GetLength(0); i++)
            {
                for (int j = 0; j < stock.GetLength(1); j++)
                    stock[i, j] = 0;
            }

            
        }

        //nameRestaurant는 가게마다 들어 있는 사진 폴더 이름
        private void foodOfRestaurant(int index, string nameRestaurant)
        {
            for(int i = 0; i<pictureBox.GetLength(0); i++)
            {
                pictureBox[i].Image = null;
                labels[i].Text = null;
            }
            query = "select * from 메뉴 where 코너_코너번호="+(index+1);
            OracleDataReader rdr = conn.ExecuteReader(query);
            int foodLabelCount = 0;


            while (rdr.Read())
            {
                labels[foodLabelCount].Text = rdr["메뉴이름"].ToString();
                pictureBox[foodLabelCount].Load(@"Image\"+nameRestaurant+@"\" + rdr["메뉴번호"].ToString() + ".jpg");
                foodLabelCount++;
            }
        }

        private void aaBtn_Click(object sender, EventArgs e)
        {
            nameRestaurant = "deopbab";
            foodCourtptr = 0;
            foodOfRestaurant(foodCourtptr, nameRestaurant);
        }

        private void bbBtn_Click(object sender, EventArgs e)
        {
            nameRestaurant = "udon";
            foodCourtptr = 1;
            foodOfRestaurant(foodCourtptr, nameRestaurant);
        }

        private void ccBtn_Click(object sender, EventArgs e)
        {
            nameRestaurant = "sand";
            foodCourtptr = 2;
            foodOfRestaurant(foodCourtptr, nameRestaurant);
        }

        private void ddBtn_Click(object sender, EventArgs e)
        {
            nameRestaurant = "china";
            foodCourtptr = 3;
            foodOfRestaurant(foodCourtptr, nameRestaurant);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void OrderConfirm_Click(object sender, EventArgs e)
        {
            //오늘 첫 주문일경우 null값. 하루 중 주문번호 1부터 시작해서 DB에 있는 지금까지 주문번호를 받아옴
            query = "select MAX(주문번호) from 주문 where TO_CHAR(TO_DATE(날짜,'YYYY-MM-DD-HH24:MI:SS'),'YY/MM/DD')" +
                " = TO_CHAR(SYSDATE,'YY/MM/DD')";
            OracleDataReader rdr = conn.ExecuteReader(query);
            
            while (rdr.Read())
            {
                if (rdr.IsDBNull(0)) //오늘 첫 주문이면
                    orderNum = 1;
                else
                    orderNum = Int32.Parse(rdr[0].ToString()) + 1;
            }
            // 주문번호 삽입
            query = "insert into 주문 values("+ orderNum + ", TO_CHAR(SYSDATE,'YYYY-MM-DD-HH24:MI:SS'))";
            conn.ExecuteNonQuery(query);

            for (int i = 0; i < stock.GetLength(0); i++)
            {
                for (int j = 0; j < stock.GetLength(1); j++)
                {
                    if (stock[i, j] != 0)
                    {
                        query = "insert into 주문내용 values(" + orderNum + ", "
                            + stock[i, j] + ", " + (j+1) + ", " + (i+1) + ", TO_CHAR(SYSDATE,'YYYY-MM-DD-HH24:MI:SS'))"; //j는 메뉴번호 i는 코너 번호
                        conn.ExecuteNonQuery(query);
                    }
                }
            }
            string orderDate="";

            query = "select 날짜 from 주문 where 주문번호="+orderNum;
            rdr = conn.ExecuteReader(query);
            while (rdr.Read())
            {
                orderDate= rdr[0].ToString();
            }
            OrderResult orderResult = new OrderResult(form1,this);
            orderResult.passOrderNum = orderNum;
            orderResult.passOrderDate = orderDate;
            conn.close();
            orderResult.Show();
        }


        private void ClickMenu(int index)
        {
            index--;
            if (stock[foodCourtptr, index] != 0) //메뉴를 선택 한 이후 해당위치 아이템의 값만 증가시켜줌
            {
                stock[foodCourtptr, index]++;
                Orders.Items[pptr[foodCourtptr, index]] = labels[index].Text + " 개수:" + stock[foodCourtptr, index];
            }
            else //처음 클릭시 위치를 정해줌
            {
                stock[foodCourtptr, index]++;
                pptr[foodCourtptr, index] = ++ptr;
                Orders.Items.Add(labels[index].Text + " 개수:" + stock[foodCourtptr, index]); ;
                menu[foodCourtptr, index] = "";
            }
            query = "select " + stock[foodCourtptr, index] + "*" + "메뉴가격 from 메뉴 where 코너_코너번호=" + (foodCourtptr + 1) + " and 메뉴번호=" + ++index;
            OracleDataReader rdr = conn.ExecuteReader(query);
            while (rdr.Read())
            {
                sumPrice += Int32.Parse(rdr[0].ToString());
            }
            label9.Text = sumPrice.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ClickMenu(1);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClickMenu(2);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ClickMenu(3);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ClickMenu(4);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ClickMenu(5);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ClickMenu(6);
        }
        
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ClickMenu(7);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            ClickMenu(8);
        }

        private void OrderList_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.close();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            form1.Visible = true;
            this.Close();
        }
    }
}
