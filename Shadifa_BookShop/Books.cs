using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Shadifa_BookShop
{
    public partial class Books : Form
    {
        public Books()
        {
            InitializeComponent();
            populate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-3O1GDNN\ADITYAASUS;Initial Catalog=Book_Shop_DB;Integrated Security=True");
        private void populate()
        {
            Con.Open();
            string query = " select * from Table_Book";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Filter()
        {
            Con.Open();
            string query = " select * from Table_Book where B_Kategori='" + katdb.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SimpanBTN_Click(object sender, EventArgs e)
        {
            if (BjdlTb.Text == "" || BpengTb.Text == "" || BkatTb.SelectedIndex == -1 || qtyTb.Text == "" || hargatb.Text == "")
            {
                MessageBox.Show("Informasi Tidak Tersedia");

            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into Table_Book values (' " + BjdlTb.Text + "','" + BpengTb.Text + "','" + BkatTb.SelectedItem.ToString() + "'," + qtyTb.Text + "," + hargatb.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil Menyimpan Buku");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void katdb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void katdb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
            katdb.SelectedIndex = -1;
        }

        private void Reset()
        {
            BjdlTb.Text = "";
            BpengTb.Text = "";
            BkatTb.SelectedIndex = -1;
            hargatb.Text = "";
            qtyTb.Text = "";
        }
        private void RstBTN_Click(object sender, EventArgs e)
        {
            Reset();

        }

        private int key = 0;
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {   
            BjdlTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            BpengTb.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString();
            BkatTb.SelectedItem = BookDGV.SelectedRows[0].Cells[3].Value.ToString();
            qtyTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            hargatb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (BjdlTb.Text == "")
            {
                key = 0;

            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Informasi Tidak Tersedia");

            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from  Table_Book where B_Id=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil Menghapus Buku");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void EditBTN_Click(object sender, EventArgs e)
        {
            if (BjdlTb.Text == "" || BpengTb.Text == "" || BkatTb.SelectedIndex == -1 || qtyTb.Text == "" || hargatb.Text == "")
            {
                MessageBox.Show("Informasi Tidak Tersedia");

            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update Table_Book set B_Judul='"+BjdlTb.Text+"',B_Pengarang='"+BpengTb.Text+"',B_Kategori='"+BkatTb.SelectedItem.ToString()+"',B_Jumlah='"+qtyTb.Text+"',B_Harga='"+hargatb.Text+"' where B_Id="+key+";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil Mengedit Buku");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

