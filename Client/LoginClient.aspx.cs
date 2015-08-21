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

public partial class Client_LoginClient : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
   
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string pass = "";
  
    string dc = "";
    string chk = "";
    string client_check = "0";

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

            if (TxtPassword.Text != "" && TxtDebitCard.Text != "")
            {
                if (TxtPassword.Text.Length < 3)
                {
                    LblError.Visible = true;
                    LblError.Text = "Please Enter atleast 3 character length Password";
                    TxtPassword.Focus();
                    return;
                }

             string sql = "SELECT Debit_Card.Debit_Card_No AS card, Debit_Card.Client_Id AS cid, Client.Password AS passd, Client_check FROM Debit_Card INNER JOIN Client ON Debit_Card.Client_Id = Client.Client_Id where Debit_Card.Debit_Card_No=@card and Client.Password=@ps ";
             SqlCommand cmd = new SqlCommand();
             cmd.Connection = cn;
             cmd.CommandType = CommandType.Text;
             cmd.CommandText = sql;
             cmd.Parameters.AddWithValue("card", TxtDebitCard.Text);
             cmd.Parameters.AddWithValue("ps", EncryptPasswrod(TxtPassword.Text));

             dr = cmd.ExecuteReader();
             if (dr.HasRows)
             {
                 while (dr.Read())
                 {
                     pass = dr["passd"].ToString();
                     dc = dr["card"].ToString();
                     chk = dr["cid"].ToString();
                     client_check =  dr["Client_Check"].ToString();
                 }
             }



             dr.Close();
             cn.Close();
             if (pass == EncryptPasswrod(TxtPassword.Text) && dc == TxtDebitCard.Text)
                {

                    if (client_check == "1")
                    {
                        Session["c_id"] = chk;
                        Response.Redirect("~/Client/ClientCheckPassword.aspx", false);
                    }
                    else
                    {
                        Session["c_id"] = chk;
                        Response.Redirect("~/Client/MainMenu.aspx", false);
                    }
                
                }

                else
                {

                    LblError.Visible = true;
                    LblError.Text = "Please enter right information";


                }

              
            }
            else
            {
                if (TxtPassword.Text == "")
                {
                    LblError.Visible = true;
                   LblError.Text ="Please enter passowrd";
                    TxtPassword.Focus();

                }
                if (TxtDebitCard.Text == "")
                {
                      LblError.Visible = true;
                     LblError.Text ="Please enter Debit Card Number";
                    TxtDebitCard.Focus();
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