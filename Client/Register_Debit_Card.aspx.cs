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

public partial class Client_Register_Debit_Card : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;

    string p1 = "";
    Random random = new Random();
    int randomNumber = 0;
    Random randomPin = new Random();
    int pin = 0;
    int pp = 0;
    int check = 0;
    string st = "";
    string band_id = "";
    DateTime id = DateTime.Now;
    DateTime ed = DateTime.Now;
    string eml,eml2 = "";

    string a1, a2, a3, a4, a5 = "";
  //  string count = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            cmd.Connection = cn;

            if (Session["cd"] != null)
            {
                LblClientId.Text = Session["cd"].ToString();

                //  LblClientId.Text = "47";
                //use the methods 
                ed = ed.AddYears(4);
                if (!IsPostBack)
                {
                    checkclient();

                }
            }
            else
                Response.Redirect("Registration.aspx");
         
        }
        catch
        { }
      
         
           
        
    }

    void checkclient()
    {

        try
        {
            cn.Close();
            cn.Open();



            string sql = "Select Client_Id, Bank_Transit_No, Issue_Date, Expiery_Date, Debit_Card_No from Debit_Card where Client_Id=@c_id";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("c_id", LblClientId.Text);

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    eml2 = dr["Client_Id"].ToString();
                    a1 = dr["Bank_Transit_No"].ToString();
                    a2 = dr["Issue_Date"].ToString();
                    a3 = dr["Expiery_Date"].ToString();
                    a4 = dr["Debit_Card_No"].ToString();


                }
            }

            if (eml2 != LblClientId.Text)
            {
                getranno();
                insertdebit();
            }
            else
            {
                LblDebitCard.Text = a4;
                LblTransiId.Text = a1;
                string idy = Convert.ToString(id);
                string ide = Convert.ToString(ed);


                int year = id.Year % 100;


                int month = id.Month % 100;



                string ii = Convert.ToString(month) + "/" + Convert.ToString(year);


                int e_year = ed.Year % 100;
                int e_month = ed.Month % 100;



                string ee = Convert.ToString(e_month) + "/" + Convert.ToString(e_year);

                
                LblExpieryDate.Text = ee;
                LblIssueDate.Text = ii;

            }

            dr.Close();
            cn.Close();




        }
        catch
        {

            
        }
        finally
        {
            cn.Close();

        }
    }
    void w1()
    {

        pp = random.Next(1000000, 9999999);


    }
    void getranno()
    {
        try
        {
            cn.Close();
            check = 0;
            while (check == 0)
            {
                Random random = new Random();
                randomNumber = random.Next(1000000, 9999999);

                string sql = "Select Debit_Card_No from Debit_Card Where Debit_Card_No=@d_id";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@d_id", randomNumber);

                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        p1 = dr["Debit_Card_No"].ToString();

                    }
                }


             
                dr.Close();
                cn.Close();

                int rn = int.Parse(p1);

                if (rn == randomNumber)
                    w1();

                if (pp == Convert.ToInt32(randomNumber))
                {
                    check = 0;
                }
                else
                { check = 1; }
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
    void getpinno()
    {
        try
        {
            cn.Close();
            check = 0;
            while (check == 0)
            {
                Random randompin = new Random();
                pin = randompin.Next(1000, 9999);



                string sql = "Select PIN from Debit_Card Where PIN=@pin_id";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@pin_id", pin);

                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        p1 = dr["PIN"].ToString();

                    }
                }


                //   string rn = Convert.ToString(randomNumber);


              
                dr.Close();
                cn.Close();

                int rn = int.Parse(p1);

                if (rn == pin)
                    w2();

                if (pp == Convert.ToInt16(pin))
                {
                    check = 0;
                }
                else
                { check = 1; }
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
    void w2()
    {

        pp = randomPin.Next(1000, 9999);


    }
    void getBanktransitNo()
    {
        try
        {
            cn.Close();

            cn.Open();

            cmd.CommandText = "Select Bank_Transit_No from Bank";
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();

            dr.Read();

            band_id = dr["Bank_Transit_No"].ToString();

            dr.Close();
            cn.Close();

        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
        }
        finally
        {
            cn.Close();

        }
    }

   
    void insertdebit()
    {
        try
        {
            cn.Close();



            getBanktransitNo();


            cn.Open();

            int cl_id = int.Parse(LblClientId.Text);
            int b_id = int.Parse(band_id);
            string rn = Convert.ToString(randomNumber);
            /////////////////////////
            Random randompin = new Random();
            pin = randompin.Next(1000, 9999);

            SqlCommand cm = new SqlCommand(
              "INSERT INTO Debit_Card (Client_Id, Bank_Transit_No, Issue_Date, Expiery_Date, Debit_Card_No,PIN) VALUES(@Client_Id, @Bank_Transit_No, @Issue_Date, @Expiery_Date, @Debit_Card_No,@PIN)", cn);
            cm.Parameters.Add("@Client_Id", cl_id);
            cm.Parameters.Add("@Bank_Transit_No", band_id);
            cm.Parameters.Add("@Issue_Date", id);
            cm.Parameters.Add("@Expiery_Date", ed);
            cm.Parameters.Add("@Debit_Card_No", rn);
            cm.Parameters.Add("@PIN", pin);
         


            cm.ExecuteNonQuery();

            cm.Clone();
           

            cn.Close();
            Response.Write("Contact Added Successfully!");
         
            sndmail();
            string idy = Convert.ToString(id);
            string ide = Convert.ToString(ed);


            int year = id.Year % 100;


            int month = id.Month % 100;



            string ii = Convert.ToString(month) + "/" + Convert.ToString(year);


            int e_year = ed.Year % 100;
            int e_month = ed.Month % 100;



            string ee = Convert.ToString(e_month) + "/" + Convert.ToString(e_year);

            LblDebitCard.Text = rn;
            LblExpieryDate.Text = ee;
            LblIssueDate.Text = ii;
            LblTransiId.Text = Convert.ToString(band_id);



        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
        }
        finally
        {
            cn.Close();
     //       Response.Redirect("~/Client/LoginClient.aspx");
        }
    }

    void sndmail()
    {

        try
        {
            cn.Close();
            cn.Open();



            string sql = "Select Email from Client where Client_Id=@c_id";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("c_id", LblClientId.Text);

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    eml = dr["Email"].ToString();

                }
            }



            dr.Close();
            cn.Close();




        }
        catch
        {


        }
        finally
        {
            cn.Close();

        }

        try
        {
            string emailfrom = "bank6260@gmail.com";
            string pwd = "inse6260";
            string sb = "bank detail";

            string bd = "bank transit id :" + band_id + " and debit card number :" + randomNumber +"and your pin number is :" + pin;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(emailfrom);

            msg.To.Add(eml);
            msg.Subject = sb;
            msg.Body = bd;

            SmtpClient sc = new SmtpClient("smtp.gmail.com");

            sc.Port = 587;

            sc.Credentials = new NetworkCredential(emailfrom, pwd);

            sc.EnableSsl = true;

            sc.Send(msg);

            Response.Write("mail send");

            
           
        }
        catch (Exception ex)
        {
          
        }
    }
}
