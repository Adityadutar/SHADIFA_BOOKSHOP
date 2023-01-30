using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shadifa_BookShop
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Books obj = new Books();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-3O1GDNN\ADITYAASUS;Initial Catalog=Book_Shop_DB;Integrated Security=True");
        private void Dashboard_Load(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT SUM(B_Jumlah) FROM Table_Book", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            STBK.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT SUM(Amount) FROM Table_Bill", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            JMLTL.Text = dt1.Rows[0][0].ToString();
            SqlDataAdapter sda2 = new SqlDataAdapter("SELECT count (*) FROM Table_Bill", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            PGNA.Text = dt2.Rows[0][0].ToString();
            Con.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
