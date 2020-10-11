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
    public partial class KurumPaneli : System.Web.UI.Page
    {
        public string sql = login.sql;
        string aliciMail;
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = login.ad;
            formListele();mesaj();
        }
        public void formListele()
        {
            try
            {
                SqlConnection con = new SqlConnection(sql);
                SqlCommand command = new SqlCommand("select S.oID,O.OgrenciAd,O.OgrenciTC,O.oNO,O.Bolum,S.Baslama,S.Bitis,F.FirmaAd,F.Adres,F.IsTanim,F.Tel from Firma F, Ogrenci O, StajForm S where S.KurumID='" + login.kID + "' and F.fID=S.FirmaID and S.oID=O.oID and S.Onay='0'",con);
                SqlDataReader read;
                con.Open();
                
                read = command.ExecuteReader();
                
                    DataList1.DataSource = read;
                    DataList1.DataBind();
                
                
                con.Close();
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            
            
        }
        public void mesaj()
        {
            SqlConnection con = new SqlConnection(sql);
            SqlCommand command = new SqlCommand("select * from Mesajlar where aliciMail='"+login.mail+"'",con);
            SqlDataReader read;
            
            con.Open();
            read = command.ExecuteReader();
            DataList2.DataSource = read;
            DataList2.DataBind();
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if(e.CommandName=="Onayla")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                SqlConnection con2 = new SqlConnection(sql);
                SqlCommand command = new SqlCommand("Select * from Kullanici where ID='"+id+"' and Actor='0'", con2);
                SqlDataReader read;

                con2.Open();
                read = command.ExecuteReader();
                if (read.Read())
                {
                    aliciMail = read["eMail"].ToString();
                }
                con2.Close();

                SqlConnection c = new SqlConnection(sql);
                string sorgu = "update StajForm set Onay='1' where oID='" + id + "'";
                c.Open();
                SqlCommand cmd = new SqlCommand(sorgu, c);
                cmd.ExecuteNonQuery();
                c.Close();

                SqlConnection con = new SqlConnection(sql);
                con.Open();
                SqlCommand cm = new SqlCommand("insert into Mesajlar Values(@gID,@aID,'Staj Formunuz onaylanmıştır.')",con);
                cm.Parameters.AddWithValue("@gID", login.mail);
                cm.Parameters.AddWithValue("@aID", aliciMail);
                cm.ExecuteNonQuery();
                con.Close();
            }
            if (e.CommandName == "Reddet")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                SqlConnection con2 = new SqlConnection(sql);
                SqlCommand command = new SqlCommand("Select * from Kullanici where ID='" + id + "' and Actor='0'", con2);
                SqlDataReader read;

                con2.Open();
                read = command.ExecuteReader();
                if (read.Read())
                {
                    aliciMail = read["eMail"].ToString();
                }
                con2.Close();

                SqlConnection c = new SqlConnection(sql);
                string sorgu = "update StajForm set Onay='2' where oID='" + id + "'";
                c.Open();
                SqlCommand cmd = new SqlCommand(sorgu, c);
                cmd.ExecuteNonQuery();
                c.Close();

                SqlConnection con = new SqlConnection(sql);
                con.Open();
                SqlCommand cm = new SqlCommand("insert into Mesajlar Values(@gID,@aID,'Staj Formunuz reddedilmiştir.')", con);
                cm.Parameters.AddWithValue("@gID", login.mail);
                cm.Parameters.AddWithValue("@aID", aliciMail);
                cm.ExecuteNonQuery();
                con.Close();

            }
            formListele();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("StajDefterleri.aspx");
        }
    }
}