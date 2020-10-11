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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

       public static string sql ="Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\yukse\\Documents\\Staj.mdf;Integrated Security=True;Connect Timeout=30";
       // SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\yukse\\Documents\\Staj.mdf;Integrated Security=True;Connect Timeout=30");
        //SqlConnection connection2 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\yukse\\Documents\\Staj.mdf;Integrated Security=True;Connect Timeout=30");
        
        public static string isim="";
        public static string TC ="";
        public static string No = "";
        public static string Bolum = "";
        public static int kID;
        public static int id;
        public static string FirmaAd, Adres, IsTanim, Tel, Mail;
        private void log_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(sql);
                SqlCommand command = new SqlCommand("SELECT * FROM Kullanici where uName='" + user.Text + "'and Pass='" + pass.Text + "'", connection);
                //  string sql="SELECT * FROM Kullanicilar where uName=@U and Pass=@P";
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                int actor;




               
                if (dr.Read())
                {
                    id = Convert.ToInt32(dr["ID"]);


                    actor = Convert.ToInt32(dr["Actor"]);
                    if (actor == 0)
                    {
                        Mail = dr["eMail"].ToString();
                        SqlConnection connection2 = new SqlConnection(sql);
                        SqlCommand cmd = new SqlCommand("Select * From Ogrenci where oID='"+id+"'", connection2);
                        connection2.Open();
                        SqlDataReader read = cmd.ExecuteReader();

                        if (read.Read())
                        {
                            isim = read["OgrenciAd"].ToString();
                            TC = read["OgrenciTC"].ToString();
                            No = read["oNO"].ToString();
                            Bolum = read["Bolum"].ToString();
                            kID = Convert.ToInt32(read["kID"]);
                           
                        }
                        connection2.Close();


                        Ogrenci ogr = new Ogrenci();
                        ogr.Show();

                        


                    }
                    if (actor == 1)
                    {
                        SqlConnection c = new SqlConnection(sql);
                        SqlCommand cm = new SqlCommand("select * From Firma where fID='" + id + "'", c);
                        c.Open();
                        SqlDataReader r = cm.ExecuteReader();

                        if (r.Read())
                        {
                            FirmaAd = r["FirmaAd"].ToString();
                            Adres = r["Adres"].ToString();
                            IsTanim = r["IsTanim"].ToString();
                            Tel = r["Tel"].ToString();
                            Mail = r["Mail"].ToString();
                        }
                        c.Close();
                        Firma firma = new Firma();
                        firma.Show();

                    }
                    if (actor == 2)
                        MessageBox.Show("Kurum girişleri için web sayfasını kullanınız.");



                }
                else { MessageBox.Show("Kullanıcı adı veya Şifre yanlış!"); }

                connection.Close();
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'stajDataSet.Users' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.usersTableAdapter.Fill(this.stajDataSet.Users);

        }
    }
}
