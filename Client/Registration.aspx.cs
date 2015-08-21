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
using System.Globalization;

public partial class Client_Registration : System.Web.UI.Page
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
    string bank_Id;

    string p1 = "";
    Random random = new Random();
    int randomNumber = 0;
    int pp = 0;
    int check = 0;

    string st = "";


    protected void Page_Load(object sender, EventArgs e)
    {

       
    
        try
        {
            cmd.Connection = cn;

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
        {}
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }
 



    /// <summary>
    /// ////////// to intert account type in account table w.r.t to client 
    /// </summary>
    void insertaccount()
    {
        try {
            for (int loop = 1; loop <= 2; loop++)
            {
                cn.Close();


                // getaccounttypeid();
                getBanktransitNo();

                getaccountnumber();
                int cl_Id = int.Parse(cd);
                double amount = 0.0;
                //string sd = DDLAccountType.SelectedValue;
                //    int ddl = int.Parse(st);
             
                cn.Open();

                SqlCommand cm = new SqlCommand(
                "INSERT INTO Account (Account_Type_Id, Client_Id, Account_No, Bank_Transit_No, Amount, Opening_Date) VALUES(@Account_Type_Id, @Client_Id,@Account_No, @Bank_Transit_No, @Amount, @Opening_Date)", cn);
                cm.Parameters.Add("@Account_Type_Id", loop);
                cm.Parameters.Add("@Client_Id", cl_Id);
                cm.Parameters.Add("@Account_No", randomNumber);
                cm.Parameters.Add("@Bank_Transit_No", bank_Id);
                cm.Parameters.Add("@Amount", amount);
                cm.Parameters.Add("@Opening_Date", myDateTime2);



                cm.ExecuteNonQuery();

                cm.Clone();
                //cmd.CommandText = "Insert INTO Account (Account_Type_Id, Client_Id, Account_No, Bank_Transit_No, Amount, Opening_Date) VALUES ('" + id + "','" + cl_Id + "','" + randomNumber + "','" + bank_Id + "','" + amount + "','" + myDateTime + "')";


                //cmd.ExecuteNonQuery();
                //cmd.Clone();


                Response.Write("Contact Added Successfully!");
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
    }

    //////////////////////////

    /////////////

    //////////////////
    void getaccountnumber()
    {
        try
        {
            cn.Close();
            check = 0;
            while (check == 0)
            {
                Random random = new Random();
                randomNumber = random.Next(1000000, 9999999);



                
           
                cn.Open();

              
                string sql = "Select Account_No from Account Where Account_No=@rn";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("rn", randomNumber);

                 dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    p1 = dr["Account_No"].ToString();
                }
                dr.Close();
                cn.Close();
                int rn = 0;
                if(p1 !="")
                    rn = int.Parse(p1);

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

    void w1()
    {

        pp = random.Next(1000000, 9999999);


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
            if (dr.HasRows)
            {
                bank_Id = dr["Bank_Transit_No"].ToString();

            }
          
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
    protected void Button2_Click(object sender, EventArgs e)
    {

    
        try
        {
           
            if(TxtPassportNo.Text != "")
            {

                int t1 = int.Parse(TxtPassportNo.Text);

                if (t1 <= 0)
                {
                    LblError.Visible = true;
                    LblError.Text = "Please enter Valid Passport Number";
                    TxtPassportNo.Focus();
                    return;
                }

                if (TxtPassportNo.Text.Length != 7)
                {
                    LblMsg.Visible = true;
                    LblMsg.Text = "Please Enter 7 digits passport number";
                    TxtPassportNo.Focus();
                    return;
                }
            cn.Open();
            string pas_Id = TxtPassportNo.Text;
            string sql = "Select PassportNo from Client where PassportNo=@pass_id";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("pass_id", pas_Id);

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    pn = dr["PassportNo"].ToString();

                }
            } 
         

            
            dr.Close();
            cn.Close();

            if (pn == TxtPassportNo.Text)
            {
                LblMsg.Visible = true;
                LblMsg.Text = "Passport Number Already exist";
                TxtPassportNo.Focus();

            }
            else
            {
                LblMsg.Visible = true;
                LblMsg.Text = "Passport Number is ok";
              

            }

           }
            else
            {
                LblMsg.Visible = true;
                LblMsg.Text = "Enter Passport number";
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


    protected void TxtPassportNo_TextChanged(object sender, EventArgs e)
    {
        try 
        {
            if (TxtPassportNo.Text.Length <= 8)
            {
                TxtPassportNo.Focus();
                return;
            }
        }
        catch
        { }
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            try
            {
                cn.Open();

                if (TxtPassportNo.Text != null)
                {
                    int t1 = int.Parse(TxtPassportNo.Text);

                    if (t1 <= 0)
                    {
                        LblError.Visible = true;
                        LblError.Text = "Please enter Valid Passport Number";
                        TxtPassportNo.Focus();
                        return;
                    }


                    if (TxtPassportNo.Text.Length != 7)
                    {
                        LblMsg.Visible = true;
                        LblMsg.Text = "Please Enter 7 digits passport number";
                        TxtPassportNo.Focus();
                        return;
                    }

                    if (TxtPassword.Text != "")
                    {
                        if (TxtPassword.Text.Length  < 3)
                        {
                            LblError.Visible = true;
                            LblError.Text = "Please Enter atleast 3 character length Password";
                            LblMsg.Visible = true;
                            LblMsg.Text = "Please Enter atleast 3 character length Password";
                            TxtPassword.Focus();
                            LblMsg.Visible = true;

                            return;
                        }


                    }
                    string pas_Id = TxtPassportNo.Text;
                    string sql = "Select PassportNo from Client where PassportNo=@pass_id";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("pass_id", pas_Id);

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            pn1 = dr["PassportNo"].ToString();

                        }
                    }



                    dr.Close();
                    cn.Close();

                    if (pn1 == TxtPassportNo.Text)
                    {
                        LblError.Visible = true;
                        LblError.Text = "Passport Number Already exist";
                        LblMsg.Visible = true;
                        LblMsg.Text = "Passport Number Already exist";
                        TxtPassportNo.Focus();
                        return;
                    }
                    else
                    {
                        LblMsg.Visible = false;
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


            if (TxtPassportNo.Text != "" && TxtPassword.Text != "" && TxtEmail.Text != "" && TxtFirstName.Text != "" && TxtLastName.Text != "")
            {
                //int chars = TxtPassportNo.Text.Length;  
                if (TxtPassportNo.Text.Length != 7)
                {
                    LblError.Visible = true;
                    LblMsg.Text = "Please Enter 7 digits passport number";
                    LblMsg.Visible = true;
                    LblMsg.Text = "Please Enter 7 digits passport number";
                    TxtPassportNo.Focus();
                    LblMsg.Visible = true;

                    return;
                }
                if (TxtA1.Text == "")
                {
                    LblError.Visible = true;
                    LblError.Text = "Please Enter Security Answers A1";

                    TxtA1.Focus();
                    return;
                }

                if (TxtA2.Text == "")
                {
                    LblError.Visible = true;
                    LblError.Text = "Please Enter Security Answers A2";

                    TxtA2.Focus();
                    return;
                }

                if (TxtA3.Text == "")
                {
                    LblError.Visible = true;
                    LblError.Text = "Please Enter Security Answers A3";

                    TxtA3.Focus();
                    return;
                }
                LblError.Visible = false;
                string dob = ddlDay.Text + "/" + ddlMonth.Text + "/" + ddlYear.Text;

                cn.Open();
                DateTimeFormatInfo StartDate = new DateTimeFormatInfo();
                StartDate.ShortDatePattern = "dd/MM/yyyy";
                StartDate.DateSeparator = "/";
                DateTime objDate = Convert.ToDateTime(dob, StartDate);

                int cnt = 0;

                SqlCommand cm = new SqlCommand(
              "INSERT INTO Client (First_Name, Last_Name, DOB, Occupation, Designation, Monthly_Salary,Email, Address, Country, City, PostalCode, Phone_Number, Status, Password, SQ1, Answer1, SQ2, Answer2, SQ3, Answer3, PassportNo, Client_Check) VALUES(@First_Name, @Last_Name, @DOB, @Occupation, @Designation, @Monthly_Salary,@Email, @Address, @Country, @City, @PostalCode, @Phone_Number, @Status, @Password, @SQ1, @Answer1, @SQ2, @Answer2, @SQ3, @Answer3, @PassportNo,@Client_Check)", cn);
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
                cm.Parameters.Add("@Password", EncryptPasswrod(TxtPassword.Text));
                cm.Parameters.Add("@SQ1", TxtQ1.Text);
                cm.Parameters.Add("@Answer1", TxtA1.Text);
                cm.Parameters.Add("@SQ2", TxtQ2.Text);
                cm.Parameters.Add("@Answer2", TxtA2.Text);
                cm.Parameters.Add("@SQ3", TxtQ3.Text);
                cm.Parameters.Add("@Answer3", TxtA3.Text);
                cm.Parameters.Add("@PassportNo", TxtPassportNo.Text);
                cm.Parameters.Add("@Client_Check", cnt);


                cm.ExecuteNonQuery();

                cm.Clone();
                //cmd.CommandText = "Insert INTO Client (First_Name, Last_Name, DOB, Occupation, Designation, Monthly_Salary, Address, Country, City, PostalCode, Phone_Number, Status, Password, SQ1, Answer1, SQ2, Answer2, SQ3, Answer3, PassportNo) VALUES ('" + TxtFirstName.Text + "','" + TxtLastName.Text + "','" + myDateTime + "','" + TxtOccupation.Text + "','" + TxtDesignation.Text + "','" + TxtSalary.Text + "','" + TxtAddress.Text + "','" + TxtCountry.Text + "','" + TxtCity.Text + "','" + TxtPostalCode.Text + "','" + TxtPhoneNo.Text + "','" + TxtStatus.Text + "','" + TxtPassword.Text + "','" + TxtQ1.Text + "','" + TxtA1.Text + "','" + TxtQ2.Text + "','" + TxtA2.Text + "','" + TxtQ3.Text + "','" + TxtA3.Text + "', '" + TxtPassportNo.Text + "')";

                //cmd.ExecuteNonQuery();
                //cmd.Clone();

                cn.Close();
                Response.Write("Contact Added Successfully!");



                try
                {
                    cn.Open();
                    string sql = "Select Client_Id from Client where PassportNo=@tp";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("tp", TxtPassportNo.Text);



                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cd = dr["Client_Id"].ToString();

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
                    insertaccount();

                    Session["cd"] = cd;
                    Response.Redirect("~/Client/Register_Debit_Card.aspx", false);

                }

                {
                    LblMsg.Visible = false;
                }

            }
            else
            {
                if (TxtPassword.Text == "")
                {
                    LblError.Visible = true;
                    LblError.Text = "Please enter passowrd";
                    TxtPassword.Focus();


                }
                if (TxtPassword.Text != "")
                {
                    if (TxtPassportNo.Text.Length != 3)
                    {
                        LblError.Visible = true;
                        LblMsg.Text = "Please Enter atleast 3 character length Password";
                        LblMsg.Visible = true;
                        LblMsg.Text = "Please Enter atleast 3 character length Password";
                        TxtPassword.Focus();
                        LblMsg.Visible = true;

                        return;
                    }


                }
                if (TxtPassportNo.Text == "")
                {
                    LblError.Visible = true;
                    LblError.Text = "Please enter Passport Number";
                    TxtPassportNo.Focus();
                }

                if (TxtEmail.Text == "")
                {
                    LblError.Visible = true;
                    LblError.Text = "Please Enter Email";
                    TxtEmail.Focus();
                }
                if (TxtFirstName.Text == "")
                {
                    LblError.Visible = true;
                    LblError.Text = "Please enter FirstName";
                    TxtFirstName.Focus();


                }
                if (TxtLastName.Text == "")
                {
                    LblError.Visible = true;
                    LblError.Text = "Please enter Last Name";
                    TxtLastName.Focus();


                }

                return;
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
    }
    private string EncryptPasswrod(string password)
    {
        System.Security.Cryptography.SHA1 sha = System.Security.Cryptography.SHA1.Create();
        string hashed = System.Convert.ToBase64String(sha.ComputeHash(System.Text.UnicodeEncoding.Unicode.GetBytes(password)));
        return hashed.Length > 49 ? hashed.Substring(0, 49) : hashed;
    }
}