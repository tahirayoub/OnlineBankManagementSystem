using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Collections;
using System.Net;
using System.Configuration;


public partial class Client_ClientCheckPassword : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);

    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["c_id"] != null)
            {

                fn = Session["c_id"].ToString();
                cmd.Connection = cn;
            }
            else
                Response.Redirect("LoginClient.aspx");
        }
        catch
        { }

    }
    protected void BtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        if (TxtPassword.Text != "")
        {
            if (TxtPassword.Text.Length < 3)
            {
                LblError.Visible = true;
                LblError.Text = "Please Enter atleast 3 character length Password";
                TxtPassword.Focus();
                return;
            }
            try
            {
                int i = 0;
                cn.Close();
                string sql22 = "UPDATE Client SET Client_Check=@cc,Password=@pwd where Client_Id=@cid";
                SqlCommand cmd22 = new SqlCommand();
                cmd22.Connection = cn;
                cmd22.CommandType = CommandType.Text;
                cmd22.CommandText = sql22;
                cmd22.Parameters.AddWithValue("cid", fn);
                cmd22.Parameters.AddWithValue("cc", i);
                cmd22.Parameters.AddWithValue("pwd", EncryptPasswrod(TxtPassword.Text));
                cn.Open();
                cmd22.ExecuteNonQuery();

                cmd22.Clone();
                cn.Close();
                Response.Redirect("~/Client/LoginClient.aspx", false);
            }
            catch
            { }

        }
        else
        {
            TxtPassword.Focus();
            LblError.Visible = true;
            LblError.Text = "Please enter the password";
        }
    }

    private string EncryptPasswrod(string password)
    {
        System.Security.Cryptography.SHA1 sha = System.Security.Cryptography.SHA1.Create();
        string hashed = System.Convert.ToBase64String(sha.ComputeHash(System.Text.UnicodeEncoding.Unicode.GetBytes(password)));
        return hashed.Length > 49 ? hashed.Substring(0, 49) : hashed;
    }
}