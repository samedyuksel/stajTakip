using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Staj
{
    public partial class Firma : Form
    {
        public Firma()
        {
            InitializeComponent();
        }
        string sql = Login.sql;
        int secili=0;
        int secili2 = 0;
        string icerik="";
        string aliciMail;
        private void Firma_Load(object sender, EventArgs e)
        {
            label1.Text = Login.FirmaAd;
            basvuruYenile();
            defterYenile();
            mesajYenile();
        }
        public void basvuruYenile()
        {
            SqlConnection connection = new SqlConnection(sql);
            SqlCommand cmd = new SqlCommand("Select S.oID,S.fID,O.OgrenciAd,O.Bolum,S.Onay From StajBasvuru S,Ogrenci O where S.fID='" + Login.id + "' and S.Onay='0' and S.oID=O.oID", connection);

            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }
        public void mesajYenile()
        {
            SqlConnection b = new SqlConnection(sql);
            b.Open();
            string k = "select Mesaj from Mesajlar where aliciMail='" + Login.Mail + "'";
            SqlCommand cm = new SqlCommand(k, b);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            b.Close();
        }

        public void defterYenile()
        {
            SqlConnection connection = new SqlConnection(sql);
            SqlCommand cmd = new SqlCommand("Select S.oID,S.fID,O.OgrenciAd,S.Tarih,S.Icerik From StajDefteri S,Ogrenci O where S.fID='" + Login.id + "' and S.fOnay='0' and S.oID=O.oID", connection);

            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(sql);
                string kayit = "Update StajBasvuru set Onay='1' where oID='"+secili+"'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(kayit, conn);
                
                cmd.ExecuteNonQuery();
                conn.Close();


                SqlConnection c = new SqlConnection(sql);
                c.Open();
                SqlCommand cm = new SqlCommand("insert into Mesajlar Values(@gID,@aID,'Firmamıza başvurunuz onaylanmıştır')", c);
                cm.Parameters.AddWithValue("@aID", aliciMail);
                cm.Parameters.AddWithValue("@gID", Login.Mail);
                cm.ExecuteNonQuery();
                c.Close();


                MessageBox.Show("Onayınız bildirildi.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            basvuruYenile();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            secili=Convert.ToInt32((dataGridView1.CurrentRow.Cells[0].Value));

            SqlConnection connection = new SqlConnection(sql);
            SqlCommand cmd = new SqlCommand("Select * From Kullanici where ID='" + secili + "' and Actor='0'", connection);

            connection.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                aliciMail = read["eMail"].ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection(sql);
                string kayit = "Update StajBasvuru set Onay='2' where oID='" + secili + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(kayit, conn);

                cmd.ExecuteNonQuery();
                conn.Close();


                SqlConnection c = new SqlConnection(sql);
                c.Open();
                SqlCommand cm = new SqlCommand("insert into Mesajlar Values(@gID,@aID,'Firmamıza başvurunuz reddedilmiştir')", c);
                cm.Parameters.AddWithValue("@aID", aliciMail);
                cm.Parameters.AddWithValue("@gID", Login.Mail);
                cm.ExecuteNonQuery();
                c.Close();


                MessageBox.Show("Öğrenci bildirildi.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            basvuruYenile();
        }

        private void defterOnay_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(sql);
                string kayit = "Update StajDefteri set fOnay='1' where oID='" + secili2 + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(kayit, conn);

                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            defterYenile();

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            secili2 = Convert.ToInt32((dataGridView2.CurrentRow.Cells[0].Value));
            icerik = (dataGridView2.CurrentRow.Cells[4].Value).ToString();

            SqlConnection connection = new SqlConnection(sql);
            SqlCommand cmd = new SqlCommand("Select * From Kullanici where ID='" + secili2 + "' and Actor='0'", connection);

            connection.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                aliciMail = read["eMail"].ToString();
            }
        }

        private void defterRet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(sql);
                string kayit = "Update StajDefteri set fOnay='2' where oID='" + secili2 + "' and Tarih='" + icerik + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(kayit, conn);

                cmd.ExecuteNonQuery();
                conn.Close();

                SqlConnection c = new SqlConnection(sql);
                c.Open();
                SqlCommand cm = new SqlCommand("insert into Mesajlar Values(@gID,@aID,'Defterinizde hatalı yerler bulunuyor.')", c);
                cm.Parameters.AddWithValue("@aID", secili);
                cm.Parameters.AddWithValue("@gID", Login.id);
                cm.ExecuteNonQuery();
                c.Close();


                MessageBox.Show("Öğrenci bildirildi.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            defterYenile();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            basvuruYenile();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mesajYenile();
        }
    }
   
}
