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

public partial class Staff_StaffForget : System.Web.UI.Page
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

            if (TxtUserName.Text != "")
            {


                string sql = "SELECT  Staff_Id,Username as un, Email from Staff where UserName = @un";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("un", TxtUserName.Text);


                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        dc = dr["un"].ToString();
                        chk = dr["Staff_Id"].ToString();
                        eml = dr["Email"].ToString();

                    }
                }



                dr.Close();
                cn.Close();
                if (dc == TxtUserName.Text)
                {
                    Session["ccdd"] = chk;


                    LblError.Visible = true;
                    LblError.Text = "Staff User Name is Valid";
                    lbleml.Text = eml;
                    checkquestion();




                }

                else
                {

                    LblError.Visible = true;
                    LblError.Text = "Please enter Valid Staff User Name";
                    Session["ccdd"] = null;

                }


            }
            else
            {

                if (TxtUserName.Text == "")
                {
                    LblError.Visible = true;
                    LblError.Text = "Please enter Valid Staff User Name";
                    TxtUserName.Focus();
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

            if (TxtUserName.Text != "")
            {
                string chk2 = Session["ccdd"].ToString();
                string sql = "SELECT SQ1, Answer1,SQ2, Answer2,SQ3, Answer3 from Staff where Staff_Id=@sid ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("sid", chk2);


                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {


                        LblQ1.Text = dr["SQ1"].ToString();
                        LblQ2.Text = dr["SQ2"].ToString();
                        LblQ3.Text = dr["SQ3"].ToString();

                    }
                }



                dr.Close();
                cn.Close();

                 TxtUserName.Enabled = false;
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
            string chk2 = Session["ccdd"].ToString();
            if (TxtA1.Text != "" && TxtA2.Text != "" && TxtA3.Text != "")
            {

                string sql = "Select Answer1 ,Answer2 , Answer3  from Staff where Staff_Id=@sid and Answer1=@Answer1 and Answer2=@Answer2 and Answer3=@Answer3";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@sid", chk2);
                cmd.Parameters.AddWithValue("@Answer1", TxtA1.Text);
                cmd.Parameters.AddWithValue("@Answer2", TxtA2.Text);
                cmd.Parameters.AddWithValue("@Answer3", TxtA3.Text);


                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {


                        a1 = dr["Answer1"].ToString();
                        a2 = dr["Answer2"].ToString();
                        a3 = dr["Answer3"].ToString();

                    }
                }



                dr.Close();
                cn.Close();
                if (a1 == TxtA1.Text && a2 == TxtA2.Text && a3 == TxtA3.Text)
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

          
            if (TxtPassword.Text != "" && TxtUserName.Text != "")
            {


                    string chk2 = Session["ccdd"].ToString();

                   
                    LblError.Visible = true;
                    LblError.Text = "information matching";
                    string sql2 = "UPDATE Staff SET Password=@pwd where Staff_Id=@cid";
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
                    try
                    {
                        string emailfrom = "bank6260@gmail.com";
                        string pwd = "inse6260";
                        string sb = "bank detail";

                        string ee = lbleml.Text;
                        string pswd = TxtPassword.Text;
                        string bd = "Staff User Name :" + TxtUserName.Text + " and Password is :" + pswd;

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
                        Response.Redirect("~/Staff/Staff_Login.aspx", false);



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
                if (TxtUserName.Text == "")
                {
                    Response.Write("Please enter Passport Number");
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
