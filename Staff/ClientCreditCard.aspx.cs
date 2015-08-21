using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Sql;
using System.Data.SqlClient;

using System.Net;
using System.Net.Mail;
using System.Drawing;


public partial class Staff_ClientCreditCard : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, id, sum = "";
    Random random = new Random();
    int randomnumber = 0;
    int pin = 0;
    int cvv = 0;
    int pp = 0;
    int check = 0;
    double sm = 0.0;
    string p1 = "";
      DateTime ids = DateTime.Now;
    DateTime ed = DateTime.Now;

    Random randompin = new Random();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["s_id"] != null)
            {

                fn = Session["s_id"].ToString();
                cmd.Connection = cn;

                if (Session["s_c_id"] != null)
                {
                    LblClientId.Text = Session["s_c_id"].ToString();
                    TextBox t = (TextBox)Master.FindControl("TxtMasterbox");

                    t.Text = Session["s_debit_id"].ToString();
                    t.Enabled = false;
                }

            }
            else
                Response.Redirect("Staff_Login.aspx");
        }
        catch
        { }


        try
        {

            if (!IsPostBack)
            {

                depositchk();

            }
        }
        catch
        { }

    }


    void depositchk()
    {
        try
        {
            var da = new SqlDataAdapter();
            var ds = new DataSet();


            da = new SqlDataAdapter("Select Request_Id AS [Request Id],Client_Id AS [Client ID],Date from CreditRequest where status = '0'", cn);
            ds = new DataSet();

            da.Fill(ds);
            GVacct.DataSource = ds;
            GVacct.DataBind();


            GVacct.RowStyle.Height = 150;
            
            
            lblCid.Visible = false;
            LblMS.Visible = false;
            LblStat.Visible = false;
            LblAL.Visible = false;
        
            Label11.Visible = false;
          
            LblClientId.Visible = false;
            LblMonthlySalary.Visible = false;
         
          
            LblStatus.Visible = false;
       
            ImgDelete.Visible = false;
            ImgUpdate.Visible = false;
         
            LblStatus.Visible = false;
          
            LblEmail.Visible = false;
            LblFirstName.Visible = false;
            LblLastName.Visible = false;
            LblMonthlySalary.Visible = false;

            LblCN.Visible = false;
            LblBT.Visible = false;
            LblAL.Visible = false;
            Label13.Visible = false;
            Label14.Visible = false;

            LblCardNo.Visible = false;
            LblBankTransit.Visible = false;
            TxtAmountLimit.Visible = false;
            LblIssueDate.Visible = false;
            LblExpieryDate.Visible = false;

            LblFN.Visible = false;
            LblEM.Visible = false;
            LblLN.Visible = false;

        }

        catch { }

    }
    protected void GVacct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            var row = GVacct.SelectedRow;

            LblCN.Visible = false;
            LblBT.Visible = false;
            LblAL.Visible = false;
            Label13.Visible = false;
            Label14.Visible = false;

            LblCardNo.Visible = false;
            LblBankTransit.Visible = false;
            TxtAmountLimit.Visible = false;
            LblIssueDate.Visible = false;
            LblExpieryDate.Visible = false;
            LblId.Text = row.Cells[1].Text;

            id = LblId.Text;
            LblClientId.Text = row.Cells[2].Text;
            getclientinfo();

        }
        catch (Exception ex)
        {
          
        }
    }

    void getclientinfo()
    {
        try
        {
            cn.Open();



            string sql = "SELECT First_Name, Last_Name,Monthly_Salary,Email,Status from Client where Client_Id=@cid ";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@cid", LblClientId.Text);


            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {


                    LblFirstName.Text = dr["First_Name"].ToString();
                    LblLastName.Text = dr["Last_Name"].ToString();
                    LblMonthlySalary.Text = dr["Monthly_Salary"].ToString();
                    LblEmail.Text = dr["Email"].ToString();
                    LblStatus.Text = dr["Status"].ToString();
                    
                    


                }
            }

            lblCid.Visible = true;
            LblMS.Visible = true;
            LblStat.Visible = true;
           // LblAL.Visible = true;
        
            Label11.Visible = true;
           
            LblClientId.Visible = true;
            LblMonthlySalary.Visible = true;
        
           
            LblStatus.Visible = true;
           
            ImgDelete.Visible = true;
            ImgUpdate.Visible = true;
           
            LblFirstName.Visible = true;
            LblLastName.Visible = true;
            LblStatus.Visible = true;
            LblEmail.Visible = true;
            LblFN.Visible = true;
            LblLN.Visible = true;
            LblEM.Visible = true;


            if (LblStatus.Text == "0")
            {
                LblStatus.Text = "Pending";
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
    protected void GVacct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVacct.PageIndex = e.NewPageIndex;
        depositchk();
    }
    protected void ImgUpdate0_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            getcreditcardnumber();

            LblCardNo.Text = Convert.ToString(randomnumber);

            LblBankTransit.Text= "112233";
            string idy = Convert.ToString(ids);
            string ide = Convert.ToString(ed);


            int year = ids.Year % 100;


            int month = ids.Month % 100;



            string ii = Convert.ToString(month) + "/" + Convert.ToString(year);


            int e_year = ed.Year % 100;
            int e_month = ed.Month % 100;



            string ee = Convert.ToString(e_month) + "/" + Convert.ToString(e_year);


            LblExpieryDate.Text = ee;
            LblIssueDate.Text = ii;

            

            LblCN.Visible = true;
            LblBT.Visible = true;
            LblAL.Visible = true;
            Label13.Visible = true;
            Label14.Visible = true;
          
            LblCardNo.Visible = true;
            LblBankTransit.Visible = true;
            TxtAmountLimit.Visible = true;
            LblIssueDate.Visible = true;
            LblExpieryDate.Visible = true;

            ImageButton4.Visible = true;


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

    void insertdebit()
    {
        try
        {
            cn.Close();



            


            cn.Open();




            Random randompin = new Random();
            pin = randompin.Next(1000, 9999);


            Random randomcvv = new Random();
            cvv = randomcvv.Next(100, 999);

            if (TxtAmountLimit.Text == "")
            {

                TxtAmountLimit.Text = "500";

            }

            SqlCommand cm = new SqlCommand(
              "INSERT INTO CreditAccount (Client_Id, Bank_Transit_No, Issue_Date, Expiery_Date, Limit,Card_No,CCV_No,PIN) VALUES(@Client_Id, @Bank_Transit_No, @Issue_Date, @Expiery_Date, @Limit,@Card_No,@CCV_No,@PIN)", cn);
            
            cm.Parameters.Add("@Client_Id", LblClientId.Text);
            cm.Parameters.Add("@Bank_Transit_No", LblBankTransit.Text);
            cm.Parameters.Add("@Issue_Date", ids);
            cm.Parameters.Add("@Expiery_Date", ed);
            cm.Parameters.Add("@Limit", TxtAmountLimit.Text);
            cm.Parameters.Add("@Card_No", LblCardNo.Text);
            cm.Parameters.Add("@CCV_No", cvv);
            cm.Parameters.Add("@PIN", pin);



            cm.ExecuteNonQuery();

            cm.Clone();


            cn.Close();
            Response.Write("Contact Added Successfully!");

            TxtAmountLimit.Text = "";
         
            
            update();

           
            sndmail();

          



        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
        }
        finally
        {
            cn.Close();
            depositchk();
         
        }
    }

    void getcreditcardnumber()
    {
        try
        {
            cn.Close();
            check = 0;
            while (check == 0)
            {
                Random random = new Random();
                randomnumber = random.Next(1000000, 9999999);

                string sql = "Select Card_No from CreditAccount Where Card_No=@c_no";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@c_no", randomnumber);

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

                if (rn == randomnumber)
                    w1();

                if (pp == Convert.ToInt32(randomnumber))
                {
                    check = 0;
                }
                else
                {
                    check = 1;

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
   
   
    protected void ImgDelete_Click(object sender, ImageClickEventArgs e)
    {
        delte2();
    }

    void update()
    {
        try
        {

int aa =1;


            cn.Open();
            string sql = "UPDATE CreditRequest SET Status=@Status  where Client_Id=@cid";
            SqlCommand cm = new SqlCommand();
            cm.Connection = cn;
            cm.CommandType = CommandType.Text;
            cm.CommandText = sql;

            cm.Parameters.AddWithValue("@cid", LblClientId.Text);

            cm.Parameters.Add("@Status",aa );


            cm.ExecuteNonQuery();

            cm.Clone();
            cn.Close();

    



        }
        catch
        { }

        finally
        {
            cn.Close();
        }
    }
   void  delte2()
    {
    try
        {
            string sql2 = "Delete CreditRequest where Request_Id=@id";
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;

            cmd2.Parameters.AddWithValue("id", LblId.Text);
            cn.Open();
            cmd2.ExecuteNonQuery();

            cmd2.Clone();
            cn.Close();
            depositchk();
            Response.Redirect(Request.RawUrl);
        }
        catch
        {


        }
        finally
        {
            cn.Close();

        }
    }
    void sndmail()
    {

        
        try
        {
            string emailfrom = "bank6260@gmail.com";
            string pwd = "inse6260";
            string sb = "bank detail";

            string bd = "bank transit id : " + LblBankTransit.Text + " and Credit card number : " + LblCardNo.Text + " and your pin number is : " + pin + " and your cvv number is : " + cvv;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(emailfrom);

            msg.To.Add(LblEmail.Text);
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
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        insertdebit();
        depositchk();
        Response.Redirect(Request.RawUrl);
    }
}