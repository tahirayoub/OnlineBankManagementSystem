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

using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Globalization;


public partial class Admin_AddManager : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string at_id = "";
    string cd = "";
    string pn, pn1 = "";
    DateTime myDateTime = DateTime.Now;
    DateTime myDateTime2 = DateTime.Now;
    DateTime ed = DateTime.Now;
    string username,cname = "";

    string p1 = "";
    Random random = new Random();
    int randomNumber = 0;
    int pp = 0;
    int check = 0;

    string st = "";
    string fn = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            try
            {

                if (Session["m_id"] != null)
                {

                    fn = Session["m_id"].ToString();
                    cmd.Connection = cn;
                }
                else
                    Response.Redirect("ManagerLogin.aspx");
            }
            catch
            { }


            if (!Page.IsPostBack)
            {
                //Fill Years
                int e_year = ed.Year;


                for (int i = 1940; i <= e_year; i++)
                {
                    ddlYear.Items.Add(i.ToString());
                }
                ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;  //set current year as selected

                //Fill Months
                for (int i = 1; i <= 12; i++)
                {
                    ddlMonth.Items.Add(i.ToString());
                }
                ddlMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true; // Set current month as selected

                //Fill days
                FillDays();
            }
        }
        catch
        { }
    }
    public void FillDays()
    {
        try
        {
            ddlDay.Items.Clear();
            //getting numbner of days in selected month & year
            int noofdays = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue));

            //Fill days
            for (int i = 1; i <= noofdays; i++)
            {
                ddlDay.Items.Add(i.ToString());
            }
            ddlDay.Items.FindByValue(System.DateTime.Now.Day.ToString()).Selected = true;// Set current date as selected
        }
        catch
        { }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            cn.Close();

            cn.Open();

            string sql = "SELECT UserName from Staff where UserName=@un ";
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
                    username = dr["UserName"].ToString();
                }
            }

            dr.Close();
            cn.Close();

            if (username != null || username == "")
            {
                username = username.ToLower();
                TxtUserName.Text = TxtUserName.Text.ToLower();
                if (username == TxtUserName.Text)
                {
                    LblError.Visible = true;
                    LblError.Text = "The username '" + TxtUserName.Text + "' is Already Entered ";
                    return;
                }
                else
                {
                    LblError.Visible = false;
                }

            }
            else
            {
                LblError.Visible = false;
            }
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
        }
        finally
        {
            cn.Close();

        }
        try
        {
            if (TxtUserName.Text == "")
            {
                LblError.Visible = true;
                LblError.Text = "Please enter user name";
                TxtUserName.Focus();
                return;

            }
            if (TxtPassword.Text == "")
            {
                LblError.Visible = true;
                LblError.Text = "Please Password";
                TxtPassword.Focus();
                return;

            }
            if (TxtEmail.Text == "")
            {
                LblError.Visible = true;
                LblError.Text = "Please enter Email";
                TxtEmail.Focus();
                return;

            }
            if (TxtFirstName.Text == "")
            {
                LblError.Visible = true;
                LblError.Text = "Please enter First Name";
                TxtFirstName.Focus();
                return;

            }
            if (TxtLastName.Text == "")
            {
                LblError.Visible = true;
                LblError.Text = "Please enter Last Name";
                TxtLastName.Focus();
                return;

            }
            cn.Open();

            string dob = ddlDay.Text + "/" + ddlMonth.Text + "/" + ddlYear.Text;
            DateTimeFormatInfo StartDate = new DateTimeFormatInfo();
            StartDate.ShortDatePattern = "dd/MM/yyyy";
            StartDate.DateSeparator = "/";
            DateTime objDate = Convert.ToDateTime(dob, StartDate);
            DateTime jd = DateTime.Now;
            SqlCommand cm = new SqlCommand(
             "INSERT INTO Staff (First_Name, Last_Name, DOB, Occupation, Designation, Monthly_Salary, Email, Address, Country, City, PostalCode, Phone_Number, Status, Joining_Date, Password,Dept_Id, UserName, SQ1, Answer1, SQ2, Answer2, SQ3, Answer3) VALUES(@First_Name, @Last_Name, @DOB, @Occupation, @Designation, @Monthly_Salary, @Email, @Address, @Country, @City, @PostalCode, @Phone_Number, @Status, @Joining_Date, @Password,@Dept_Id, @User_Name, @SQ1, @Answer1, @SQ2, @Answer2, @SQ3, @Answer3)", cn);
            cm.Parameters.Add("@First_Name", TxtFirstName.Text);
            cm.Parameters.Add("@Last_Name", TxtLastName.Text);
            cm.Parameters.Add("@DOB", objDate);
            cm.Parameters.Add("@Occupation", TxtOccupation.Text);
            cm.Parameters.Add("@Designation", TxtDesignation.Text);
            cm.Parameters.Add("@Monthly_Salary", TxtSalary.Text);
            cm.Parameters.Add("@Email", TxtEmail.Text);
            cm.Parameters.Add("@Address", TxtAddress.Text);
            cm.Parameters.Add("@Country", TxtCountry.Text);
            cm.Parameters.Add("@City", TxtCity.Text);
            cm.Parameters.Add("@PostalCode", TxtPostalCode.Text);
            cm.Parameters.Add("@Phone_Number", TxtPhoneNo.Text);
            cm.Parameters.Add("@Status", TxtStatus.Text);
            cm.Parameters.Add("@Joining_Date", jd);
            cm.Parameters.Add("@Password", TxtPassword.Text);
            cm.Parameters.Add("@Dept_Id", DDLAccountType.Text);
            cm.Parameters.Add("@User_Name", TxtUserName.Text);
            cm.Parameters.Add("@SQ1", TxtQ1.Text);
            cm.Parameters.Add("@Answer1", TxtA1.Text);
            cm.Parameters.Add("@SQ2", TxtQ2.Text);
            cm.Parameters.Add("@Answer2", TxtA2.Text);
            cm.Parameters.Add("@SQ3", TxtQ3.Text);
            cm.Parameters.Add("@Answer3", TxtA3.Text);
         


            cm.ExecuteNonQuery();

            cm.Clone();
           

            cn.Close();
            LblError.Visible = true;
            LblError.Text = "An Email is sent to A Staff /n Account Added Successfully" ;
            Response.Redirect(Request.RawUrl, false);
     
         
            try
            {
                string emailfrom = "bank6260@gmail.com";
                string pwd = "inse6260";
                string sb = "Welcome to INSE 6260 Bank ";

                string bd = "Dear Staff! To Account Has been Created successfully /n : Your user name to Login is :" + TxtUserName.Text + " and Password is:" + TxtPassword.Text + " and Security Questions Answer is apple";

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(emailfrom);

                msg.To.Add(TxtEmail.Text);
                msg.Subject = sb;
                msg.Body = bd;

                SmtpClient sc = new SmtpClient("smtp.gmail.com");

                sc.Port = 587;

                sc.Credentials = new NetworkCredential(emailfrom, pwd);

                sc.EnableSsl = true;

                sc.Send(msg);

                Response.Write("mail send successfully");

             
               
                Response.Redirect("~/Manager/ManagerMenu.aspx", false);

                TxtFirstName.Text = "";
                TxtLastName.Text = "";
                TxtOccupation.Text = "";
                TxtDesignation.Text = "";
                TxtSalary.Text = "";
                TxtEmail.Text = "";
                TxtAddress.Text = "";
                TxtCity.Text = "";
                TxtPostalCode.Text = "";
                TxtPhoneNo.Text = "";
                TxtStatus.Text = "";
                TxtUserName.Text = "";
                TxtPassword.Text = "";

            }
            catch (Exception ex)
            {
              
            }

        }
        catch
        { }
    }
}