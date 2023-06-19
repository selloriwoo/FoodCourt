using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB3_1FoodCourt
{
    public partial class OrderResult : Form
    {
        Form1 form1;
        OrderList orderListForm;
        private int orderNum;
        private string orderDate;
        public int passOrderNum
        {
            get { return this.orderNum; }
            set { this.orderNum = value; }
        }
        public string passOrderDate
        {
            get { return this.orderDate; }
            set { this.orderDate = value; }
        }
        public OrderResult(Form1 form1, OrderList orderListForm)
        {
            InitializeComponent();
            this.form1 = form1;
            this.orderListForm = orderListForm;
        }

        private void OrderResult_Load(object sender, EventArgs e)
        {
            label2.Text= orderNum.ToString()+"번";
            label3.Text = "주문날짜: "+orderDate.ToString();
            orderListForm.Close();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            form1.Visible = true;
        }

        private void OrderResult_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
