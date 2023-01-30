using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Shadifa_BookShop
{
    public partial class Invoice : Form
    {
        public Invoice()
        {
            InitializeComponent();
            populate();
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
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            usrnm.Text = Login.Username;
        }

        private void UpdateBuku()
        {
            int newqty = stok - Convert.ToInt32(qtyTb.Text);
            try
            {
                Con.Open();
                string query = "update Table_Book set B_Jumlah= " + newqty + " where B_Id=" + key + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                // MessageBox.Show("Berhasil Mengedit Buku");
                Con.Close();
                populate();
                //Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int n = 0, Ctotal = 0;
        private void SimpanBTN_Click(object sender, EventArgs e)
        {
            
            if(qtyTb.Text == ""|| Convert.ToInt32(qtyTb.Text)>stok)
            {
                MessageBox.Show("Tidak Cukup Stok");
            }
            else
            {
                int total = Convert.ToInt32(qtyTb.Text) * Convert.ToInt32(hargatb.Text);
                DataGridViewRow newRow= new DataGridViewRow();
                newRow.CreateCells(InvoiceDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = BjdlTb.Text;
                newRow.Cells[2].Value = hargatb.Text;
                newRow.Cells[3].Value = qtyTb.Text;            
                newRow.Cells[4].Value = total ;
                InvoiceDGV.Rows.Add(newRow);
                n++;
                UpdateBuku();
                Ctotal = Ctotal + total;
                Totalinv.Text = "Rp." + Ctotal;
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        int key = 0,stok = 0;
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BjdlTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            //qtyTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            hargatb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (BjdlTb.Text == "")
            {
                key = 0;
                stok = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
                stok = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[4].Value.ToString());
            }
        }

        private void InvoiceDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void EditBTN_Click(object sender, EventArgs e)
        {
       

            if (Namaklien.Text == "" || BjdlTb.Text == "")
            {
                MessageBox.Show("Select Nama Klien");

            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into Table_Bill values (' " + usrnm.Text + "','" + Namaklien.Text + "'," + Ctotal + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil Menyimpan Invoice");
                    Con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 350, 600);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
                


            }
        }

        int prodid, prodharga, prodqty, tottal, pos = 60;

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void usrnm_Click(object sender, EventArgs e)
        {
           
        }

        string prodnama;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {   
            e.Graphics.DrawString("  SHADIFA  ", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Maroon, new System.Drawing.Point(130));
            e.Graphics.DrawString("ID PRODUCT             PRICE    QTY    TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.DarkGreen, new System.Drawing.Point(26, 40));
            foreach (DataGridViewRow row in InvoiceDGV.Rows)
            {

                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodnama = "" + row.Cells["Column2"].Value;
                prodharga = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                tottal = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(26, pos));
                e.Graphics.DrawString("" + prodnama, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(40, pos));
                e.Graphics.DrawString("" + prodharga, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(160, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(215, pos));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(253, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Total Belanja : Rp " + Ctotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Maroon, new System.Drawing.Point(55, pos + 100));
            e.Graphics.DrawString("===========  TERIMA KASIH  ===========", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.DarkGreen, new System.Drawing.Point(28, pos + 135));
            e.Graphics.DrawString("===========   SHADIFA    ===========", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.DarkGreen, new System.Drawing.Point(40, pos + 170));
            InvoiceDGV.Rows.Clear();
            InvoiceDGV.Refresh();
            pos = 100;
            Ctotal = 0;
        }   

        private void Reset()
        {
            BjdlTb.Text = "";
            qtyTb.Text = "";
            hargatb.Text = "";
            Namaklien.Text = "";
                
        }
        private void RstBTN_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
