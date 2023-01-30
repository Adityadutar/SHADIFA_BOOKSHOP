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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-3O1GDNN\ADITYAASUS;Initial Catalog=Book_Shop_DB;Integrated Security=True");
        public static string Username = "";
        private void button1_Click(object sender, EventArgs e)
        {
            
            string username, password;
            Username = Unametb.Text;
            username = Unametb.Text;
            password = UpassTb.Text;
            //try
            //{

                //string quarry = ("SELECT count(*) FROM UserTbl WHERE UNama= '" + Unametb.Text + "' AND   UPass= '" + UpassTb.Text + "'",Con);
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT count(*) FROM UserTbl WHERE UNama= '" + Unametb.Text + "' AND   UPass= '" + UpassTb.Text + "'", Con);
                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);
                if (Unametb.Text == "" || UpassTb.Text == "")
                {
                    MessageBox.Show("Silahkan Masukan Username dan Password!");
                }
                else
                {
                    if (dataTable.Rows[0][0].ToString() == "0")
                    {
                        
                        username = Unametb.Text;
                        password = UpassTb.Text;

                        Invoice panggil = new Invoice ();
                        panggil.Show();
                        this.Hide();
                        Con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username atau Password Salah. Silahkan Masukkan Kembali!","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Unametb.Clear();
                        UpassTb.Clear();

                        Unametb.Focus();
                    }

                    Con.Close();
                }

            //}
            //catch
            //{
                //MessageBox.Show("Error");
            //}
            //finally
            //{
                //Con.Close();
            //}
            Con.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Admin_Login obj = new Admin_Login();
            obj.Show();
            this.Hide();
        }
    }
}
