namespace DB3_1FoodCourt
{
    public partial class Form1 : Form
    {
        OrderList orderList;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            orderList = new OrderList();
            orderList.Show();
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}