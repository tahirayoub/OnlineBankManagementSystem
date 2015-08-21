using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Manager_ManagerLogin : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string pass = "";
    int count = 1;
    string dc = "";
    string chk = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            cmd.Connection = cn;
        }
        catch
        { }
    }
    protected void BtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            cn.Open();

            if (TxtPassword.Text != "" && TxtUserName.Text != "")
            {

                string abc = EncryptPasswrod(TxtPassword.Text); 
                string sql = "SELECT UserName, Password,Manager_Id from Manager where UserName=@un and Password=@ps ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("un", TxtUserName.Text);
                cmd.Parameters.AddWithValue("ps", abc);

                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pass = dr["Password"].ToString();
                        dc   = dr["UserName"].ToString();
                        chk  = dr["Manager_Id"].ToString();

                    }
                }



                dr.Close();
                cn.Close();
                if (pass == abc && dc == TxtUserName.Text)
                {

                    Session["m_id"] = chk;
                    Response.Redirect("~/Manager/ManagerMenu.aspx", false);


                }

                else
                {

                    LblError.Visible = true;
                    LblError.Text = "Please enter Valid information";


                }


            }
            else
            {
                if (TxtPassword.Text == "")
                {
                    Response.Write("Please enter password");
                    TxtPassword.Focus();

                }
                if (TxtUserName.Text == "")
                {
                    Response.Write("Please enter username");
                    TxtUserName.Focus();
                }

                return;
            }


        }
        catch
        {


        }
        finally
        {
            cn.Close();

        }
    }
    private string EncryptPasswrod(string password)
    {
        System.Security.Cryptography.SHA1 sha = System.Security.Cryptography.SHA1.Create();
        string hashed = System.Convert.ToBase64String(sha.ComputeHash(System.Text.UnicodeEncoding.Unicode.GetBytes(password)));
        return hashed.Length > 49 ? hashed.Substring(0, 49) : hashed;
    }
}