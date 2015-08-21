using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Text;
public partial class Client_forgetpassword : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
  
   
    string dc, dc2 = "";
    string chk = "";
    string a1, a2, a3 = "";

    string eml = "";
    
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

            if (TxtDebitCard.Text != "")
            {


                string sql = "SELECT Debit_Card.Debit_Card_No AS card, Client.Email as Email, Debit_Card.Client_Id AS cid FROM Debit_Card INNER JOIN Client ON Debit_Card.Client_Id = Client.Client_Id where Debit_Card.Debit_Card_No=@card ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("card", TxtDebitCard.Text);


                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        dc = dr["card"].ToString();
                        chk = dr["cid"].ToString();
                        eml = dr["Email"].ToString();

                    }
                }



                dr.Close();
                cn.Close();
                if (dc == TxtDebitCard.Text)
                {
                    Session["ccdd"] = chk;


                    LblError.Visible = true;
                    LblError.Text = "Debit Card Number is Valid";
                    lbleml.Text = eml;
                    checkquestion();




                }

                else
                {

                    LblError.Visible = true;
                    LblError.Text = "Please enter Valid Debit Card Number";
                    Session["ccdd"] = null;

                }


            }
            else
            {

                if (TxtDebitCard.Text == "")
                {
                    LblError.Visible = true;
                    LblError.Text = "Please enter Valid Debit Card Number";
                    TxtDebitCard.Focus();
                }

                return;
            }


        }

        catch { }

        finally
        {
            cn.Close();

        }
    }
    void checkquestion()
    {
        try
        {
            cn.Open();

            if (TxtDebitCard.Text != "")
            {
               
                string sql = "SELECT Client.SQ1 AS Q1, Client.SQ2 AS Q2, Client.SQ3 AS Q3 FROM Debit_Card INNER JOIN   Client ON Debit_Card.Client_Id = Client.Client_Id where Debit_Card.Debit_Card_No=@card ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("card", TxtDebitCard.Text);


                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {


                        LblQ1.Text  = dr["Q1"].ToString();
                        LblQ2.Text   = dr["Q2"].ToString();
                        LblQ3.Text   = dr["Q3"].ToString();

                    }
                }



                dr.Close();
                cn.Close();

                TxtDebitCard.Enabled = false;
                BtnSubmit.Enabled = false;
                LblQ1.Visible = true;
                LblQ2.Visible = true;
                LblQ3.Visible = true;
                TxtA1.Visible = true;
                TxtA2.Visible = true;
                TxtA3.Visible = true;

                BtnCheckAnswer.Visible = true;

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
    protected void BtnCheckAnswer_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            cn.Open();
            if (TxtA1.Text == "")
            {
                LblAnswerError.Visible = true;
                LblAnswerError.Text = "Please Enter Security Answers A1";

                TxtA1.Focus();
                return;
            }

            if (TxtA2.Text == "")
            {
                LblAnswerError.Visible = true;
                LblAnswerError.Text = "Please Enter Security Answers A2";

                TxtA2.Focus();
                return;
            }
            if (TxtA3.Text == "")
            {
                LblAnswerError.Visible = true;
                LblAnswerError.Text = "Please Enter Security Answers A3";

                TxtA3.Focus();
                return;
            }
            if (TxtA1.Text != "" && TxtA2.Text !="" && TxtA3.Text !="")
            {

                string sql = "SELECT  Debit_Card.Debit_Card_No AS card,Client.Answer1 AS A1, Client.Answer2 AS A2, Client.Answer3 AS A3 FROM Debit_Card INNER JOIN   Client ON Debit_Card.Client_Id = Client.Client_Id where Debit_Card.Debit_Card_No=@card and Client.Answer1=@ans1 and Client.Answer2=@ans2 and Client.Answer3=@ans3 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("card", TxtDebitCard.Text);
                cmd.Parameters.AddWithValue("ans1", TxtA1.Text);
                cmd.Parameters.AddWithValue("ans2", TxtA2.Text);
                cmd.Parameters.AddWithValue("ans3", TxtA3.Text);


                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        dc2 = dr["card"].ToString();
                        a1 = dr["A1"].ToString();
                        a2 = dr["A2"].ToString();
                        a3 = dr["A3"].ToString();

                    }
                }



                dr.Close();
                cn.Close();
                if (dc2 == TxtDebitCard.Text && a1 == TxtA1.Text && a2 == TxtA2.Text && a3 == TxtA3.Text)
                {

                    BtnUpdate.Visible = true;
                    LblPassword.Visible = true;
                    TxtPassword.Visible = true;
                    BtnCheckAnswer.Enabled = false;
                    LblAnswerError.Visible = true;
                    LblAnswerError.Text = "Information Matched";
                    LblMessage.Visible = true;
                    LblMessage.Text = "Please Wait for a while after click update A confirmation mail will be send to you.";
                    


                }

                else
                {

                    LblAnswerError.Visible = true;
                    LblAnswerError.Text = "Please enter Valid Security Answers ";


                }


            }
            else
            {

                if (TxtA1.Text == "")
                {
                    Response.Write("Please enter Question 1 Answer ");
                    TxtA1.Focus();
                    return;
                }

                if (TxtA2.Text == "")
                {
                    Response.Write("Please enter Question 2 Answer ");
                    TxtA2.Focus();
                    return;
                }
                if (TxtA3.Text == "")
                {
                    Response.Write("Please enter Question 3 Answer ");
                    TxtA3.Focus();
                    return;
                }
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
    protected void BtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

          
            if (TxtPassword.Text != "" && TxtDebitCard.Text != "")
            {
                if (TxtPassword.Text.Length < 3)
                {
                    LblError.Visible = true;
                    LblError.Text = "Please Enter atleast 3 character length Password";
                    TxtPassword.Focus();
                    return;
                }

                    string chk2 = Session["ccdd"].ToString();

                   
                    LblError.Visible = true;
                    LblError.Text = "information matching";
                    string sql2 = "UPDATE Client SET Password=@pwd where Client_Id=@cid";
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = cn;
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = sql2;
                    cmd2.Parameters.AddWithValue("cid", chk2);
                    cmd2.Parameters.AddWithValue("pwd", EncryptPasswrod(TxtPassword.Text));
                    cn.Open();
                    cmd2.ExecuteNonQuery();

                    cmd2.Clone();
                    cn.Close();

                    LblAnswerError.Visible = true;
                    LblAnswerError.Text = "Password successfully Updated please";
                    LblAnswerError.Text = "Mail send";

                    int i = 1;
                    
                    try
                    {
                        cn.Close();
                        string sql22 = "UPDATE Client SET Client_Check=@cc where Client_Id=@cid";
                        SqlCommand cmd22 = new SqlCommand();
                        cmd22.Connection = cn;
                        cmd22.CommandType = CommandType.Text;
                        cmd22.CommandText = sql22;
                        cmd22.Parameters.AddWithValue("cid", chk2);
                        cmd22.Parameters.AddWithValue("cc", i);
                        cn.Open();
                        cmd22.ExecuteNonQuery();

                        cmd22.Clone();
                        cn.Close();
                    }
                    catch
                    { }
                    try
                    {
                        string emailfrom = "bank6260@gmail.com";
                        string pwd = "inse6260";
                        string sb = "bank detail";

                        string ee = lbleml.Text;
                        string pswd = TxtPassword.Text;
                        string bd = "debit card number :" + TxtDebitCard.Text + "and Password is :" + pswd;

                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress(emailfrom);

                        msg.To.Add(ee);
                        msg.Subject = sb;
                        msg.Body = bd;

                        SmtpClient sc = new SmtpClient("smtp.gmail.com");

                        sc.Port = 587;

                        sc.Credentials = new NetworkCredential(emailfrom, pwd);

                        sc.EnableSsl = true;

                        sc.Send(msg);

                        

                        System.Threading.Thread.Sleep(5000);
                        Response.Redirect("~/Client/LoginClient.aspx", false);



                    }
                    catch (Exception ex)
                    {
                      
                    }
               


            }
            else
            {
                if (TxtPassword.Text == "")
                {
                    Response.Write("Please enter passowrd");
                    TxtPassword.Focus();

                }
                if (TxtDebitCard.Text == "")
                {
                    Response.Write("Please enter Passport Number");
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