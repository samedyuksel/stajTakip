using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;

namespace kPanel
{
    public partial class login : System.Web.UI.Page
    {
        public static string sql = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\yukse\\Documents\\Staj.mdf;Integrated Security=True;Connect Timeout=30";
        public static int kID,actor;
        public static string ad, mail;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            SqlCommand command = new SqlCommand("SELECT * FROM Kullanici where uName='" + TextBox1.Text + "'and Pass='" + TextBox2.Text + "'", connection);
            //  string sql="SELECT * FROM Kullanicilar where uName=@U and Pass=@P";
            connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                actor = Convert.ToInt32(dr["Actor"]);
                kID= Convert.ToInt32(dr["ID"]);
                mail = dr["eMail"].ToString();
                SqlConnection connection2 = new SqlConnection(sql);
                SqlCommand cmd = new SqlCommand("Select * From Kurum where Id='" + kID + "'", connection2);
                connection2.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    ad = read["KurumAd"].ToString();
                   
                   
                }
                connection2.Close();

                Response.Redirect("KurumPaneli.aspx");

            }
            connection.Close();
        }
    }
}