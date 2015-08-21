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
using System.Globalization;


public partial class Client_InvestmentProgress : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn = "";
    int i = 1;
    string latestamount, latestAcoountNo = "";

    DateTime dt = DateTime.Now.Date;
    DateTime ed = DateTime.Now;
  
    double amount,ac_amount = 0.0;
  
  
    string s1 = "";
    
    int randomNumber = 0;
    int c_yr  = 0;
    int c_mon = 0;
    int c_day = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            if (Session["c_id"] != null)
            {

                fn = Session["c_id"].ToString();
                cmd.Connection = cn;
                LblDate.Text = dt.ToString("dd/MM/yyyy");

                     c_yr = dt.Year;
                     c_mon = dt.Month % 100;
                     c_day = dt.Day % 100;

               
            }
            else
                Response.Redirect("LoginClient.aspx");
        }
        catch
        { }



        try
        {

            if (!IsPostBack)
            {

                investmentList();


            }
        }


        catch
        { }
    }

    void investmentList()
    {
        try
        {


            var da = new SqlDataAdapter();
            var ds = new DataSet();


            da = new SqlDataAdapter("SELECT Investment.Investment_id,InvestmentType.Type AS [Investment Type], AccountType.Type AS [Paid from Account Type], Investment.StartDate, Investment.EndDate, Investment.YearInvestment AS [Year Investment], Investment.Amount FROM Investment INNER JOIN InvestmentType ON Investment.Investment_Type_Id = InvestmentType.Investment_Type_Id INNER JOIN AccountType ON Investment.Account_Type_Id = AccountType.Account_Type_Id WHERE Investment.Client_Id = '" + fn + "'", cn);
            ds = new DataSet();

            da.Fill(ds);

            GVClientBillList.DataSource = ds;
            GVClientBillList.DataBind();


            if (GVClientBillList.Rows.Count != 0)
            {
            
                LblError.Visible = false;
            }
            else
            {
                LblError.Visible = true;
                LblError.Text = "No Investment Record Found";
             
            }


         // this.GVClientBillList.Columns[0].Visible = false;



        }

        catch { }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (LblInvType.Text == "Fixed (open Ended)")
            {

               

                string sd1 = c_day + "/" + c_mon + "/" + c_yr;

                DateTime dt_end = Convert.ToDateTime(LblEndDate.Text);    
                //string ed1 = ddlDay2.Text + "/" + ddlMonth2.Text + "/" + ddlYear2.Text;

                DateTimeFormatInfo CurrentDate = new DateTimeFormatInfo();
                CurrentDate.ShortDatePattern = "dd/MM/yyyy";
                CurrentDate.DateSeparator = "/";
                DateTime objDate = Convert.ToDateTime(sd1, CurrentDate);

            


                DateTimeFormatInfo EndDate = new DateTimeFormatInfo();
                EndDate.ShortDatePattern = "dd/MM/yyyy";
                EndDate.DateSeparator = "/";
                DateTime endDate = Convert.ToDateTime(dt_end, EndDate);

                int year = endDate.Year % 100;


                int month = endDate.Month % 100;

                int day = endDate.Day % 100;

                TimeSpan t = endDate - objDate;

                double NrOfDays = t.TotalDays;






                if (endDate.Date <= objDate.Date)
                {
                    if (LblYears.Text == "3")
                    {

                        int yr = int.Parse(LblYears.Text);

                        int get_days = yr * 365;

                        double db_amount = double.Parse(LblAmount.Text);


                        double aa = 1 + 0.2 / 12;

                        int yrs = yr * 12;

                        double bb = Math.Pow(aa, yrs);

                        amount = db_amount * bb;

                        LblTotalProfit.Visible = true;
                        LblTotalProfit.Text =Convert.ToString(amount);
                        LblError.Visible = false;
                        getaccountAmount();



                    }
                    if (LblYears.Text == "5")
                    {

                        int yr = int.Parse(LblYears.Text);

                        int get_days = yr * 365;

                        double db_amount = double.Parse(LblAmount.Text);

                        double aa = 1 + 0.2 / 12;

                        int yrs = yr * 12;

                        double bb = Math.Pow(aa, yrs);

                        amount = db_amount * bb;
                        LblTotalProfit.Visible = true;
                        LblTotalProfit.Text = "Total Profit " + Convert.ToString(amount);
                        LblError.Visible = false;
                        getaccountAmount();
                        

                    }
                }
                else
                    LblTotalProfit.Visible = false;
                if (endDate.Date > objDate.Date)
                {
                    //call update account update function and delete this investment
                    LblError.Visible = true;
                    LblError.Text = "Number of Days left for Investment Profit : " + NrOfDays.ToString() ;
                }
                else
                    LblError.Visible = false;

                string ii = Convert.ToString(month) + "/" + Convert.ToString(year);

            }

            if (LblInvType.Text == "Variable (Close Ended)")
            {
                try
                {

                    Random random = new Random();
                    
                    randomNumber = random.Next(-50, 50);

                    double db_amount = double.Parse(LblAmount.Text);
                  //  randomNumber = -55;

                    double rn = randomNumber * 0.01;

                    double dollar = db_amount * rn;

                    amount = db_amount + dollar;

                    updateget();

                }
                catch
                { }

            }
            
        }

        catch
        { }
    }

    void updateget()
    { 
    
        try
        {

            string sql2 = "UPDATE Investment SET Amount=@Amount where Client_Id=@cid and  Investment_Id = @Investment_Id";
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;
            cmd2.Parameters.AddWithValue("@Amount", amount);
            cmd2.Parameters.AddWithValue("@cid", fn);
            cmd2.Parameters.AddWithValue("@Investment_Id", LblId.Text);
            cn.Open();
            cmd2.ExecuteNonQuery();

            cmd2.Clone();
            cn.Close();
            investmentList();
            LblAmount.Text = amount.ToString();
            LblError.Visible = true;
            LblError.Text = "The Variable Investment of Amount " + LblAmount.Text  + " is updated please check you investment";
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
    void progess()
    {
        try
        {
            if (LblInvType.Text == "Fixed (open Ended)")
            {



                string sd1 = c_day + "/" + c_mon + "/" + c_yr;

                DateTime dt_end = Convert.ToDateTime(LblEndDate.Text);
                //string ed1 = ddlDay2.Text + "/" + ddlMonth2.Text + "/" + ddlYear2.Text;

                DateTimeFormatInfo CurrentDate = new DateTimeFormatInfo();
                CurrentDate.ShortDatePattern = "dd/MM/yyyy";
                CurrentDate.DateSeparator = "/";
                DateTime objDate = Convert.ToDateTime(sd1, CurrentDate);




                DateTimeFormatInfo EndDate = new DateTimeFormatInfo();
                EndDate.ShortDatePattern = "dd/MM/yyyy";
                EndDate.DateSeparator = "/";
                DateTime endDate = Convert.ToDateTime(dt_end, EndDate);

                int year = endDate.Year % 100;


                int month = endDate.Month % 100;

                int day = endDate.Day % 100;

                TimeSpan t = endDate - objDate;

                double NrOfDays = t.TotalDays;






                if (endDate.Date <= objDate.Date)
                {
                    if (LblYears.Text == "3")
                    {

                        int yr = int.Parse(LblYears.Text);

                        int get_days = yr * 365;

                        double db_amount = double.Parse(LblAmount.Text);


                        double aa = 1 + 0.2 / 12;

                        int yrs = yr * 12;

                        double bb = Math.Pow(aa, yrs);

                        amount = db_amount * bb;

                        LblTotalProfit.Visible = true;
                        LblTotalProfit.Text = "Total Profit " + Convert.ToString(amount);
                        LblError.Visible = false;



                    }
                    if (LblYears.Text == "5")
                    {

                        int yr = int.Parse(LblYears.Text);

                        int get_days = yr * 365;

                        double db_amount = double.Parse(LblAmount.Text);

                        double aa = 1 + 0.2 / 12;

                        int yrs = yr * 12;

                        double bb = Math.Pow(aa, yrs);

                        amount = db_amount * bb;
                        LblTotalProfit.Visible = true;
                        LblTotalProfit.Text = "Total Profit " + Convert.ToString(amount);
                        LblError.Visible = false;
                    }
                }
                else
                    LblTotalProfit.Visible = false;
                if (endDate.Date > objDate.Date)
                {
                    //call update account update function and delete this investment
                    LblError.Visible = true;
                    LblError.Text = "Number of Days left for Investment Profit : " + NrOfDays.ToString();
                }
                else
                    LblError.Visible = false;

                string ii = Convert.ToString(month) + "/" + Convert.ToString(year);

            }
            else
                LblTotalProfit.Visible = false;

        }
        catch
        { }
            
      
    }
    protected void GVClientBillList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var row = GVClientBillList.SelectedRow;
        LblId.Text = row.Cells[1].Text;
        LblInvType.Text = row.Cells[2].Text;
        LblAccountType.Text = row.Cells[3].Text;
        LblStartDate.Text = row.Cells[4].Text;
        LblEndDate.Text = row.Cells[5].Text;
        LblYears.Text = row.Cells[6].Text;
        LblAmount.Text = row.Cells[7].Text;
        
        LblInvType.Visible = true;
        LblAccountType.Visible = true;
        LblStartDate.Visible = true;
        LblEndDate.Visible = true;
        LblYears.Visible = true;
        LblAmount.Visible = true;

        LblMsg.Visible = true;
        LblMsg0.Visible = true;
        LblMsg1.Visible = true;

        ImageButton2.Visible = true;
        progess();
        
    }
    protected void GVClientBillList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GVClientBillList.PageIndex = e.NewPageIndex;
            investmentList();
        }
        catch
        { }
    }
    protected void GVClientBillList_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (GVClientBillList.Columns.Count > 0)
                GVClientBillList.Columns[1].Visible = false;
            else
            {
                GVClientBillList.HeaderRow.Cells[1].Visible = false;
                foreach (GridViewRow gvr in GVClientBillList.Rows)
                {

                    gvr.Cells[1].Visible = false;
                }
            }

        }
        catch
        { }
    }


    void  getaccountAmount()
    {
        try
        {
            cn.Close();
            cn.Open();

            string a1 = "Saving Account";
            string a2 = "Current Account";
        
          if (LblAccountType.Text == "Saving Account")
              i = 1;
          if (LblAccountType.Text == "Current Account")
              i = 2;

            string sql = "SELECT  SUM(Amount) AS TotalAmount  from Account where Client_Id=@cid and Account_Type_Id = @ATD GROUP BY Account_Type_Id";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@cid", fn);
            cmd.Parameters.AddWithValue("@ATD", i);



            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    s1 = dr["TotalAmount"].ToString();
                }
            }




            dr.Close();
            cn.Close();

            double d_dd = double.Parse(s1);



            ac_amount = d_dd + amount;
            
            updateamount();
            insertdata2();

            deleteamount();
        }




        catch
        {


        }
        finally
        {
            cn.Close();

        }
      
    }

   
    void updateamount()
    {

        try
        {

            string a1 = "Saving Account";
            string a2 = "Current Account";

            if (LblAccountType.Text == "Saving Account")
                i = 1;
            if (LblAccountType.Text == "Current Account")
                i = 2;

            string sql2 = "UPDATE Account SET Amount=@cc where Client_Id=@cid and  Account_Type_Id = @Account_Type_Id";
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;
            cmd2.Parameters.AddWithValue("cc", ac_amount);
            cmd2.Parameters.AddWithValue("cid", fn);
            cmd2.Parameters.AddWithValue("Account_Type_Id", i);
            cn.Open();
            cmd2.ExecuteNonQuery();

            cmd2.Clone();
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
    void deleteamount()
    {

        try
        {



            string sql2 = "Delete from Investment where Client_Id=@cid and  Investment_Id = @Investment_Id";
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;
            cmd2.Parameters.AddWithValue("cid", fn);
            cmd2.Parameters.AddWithValue("Investment_Id", LblId.Text);
            cn.Open();
            cmd2.ExecuteNonQuery();

            cmd2.Clone();
            cn.Close();

            investmentList();

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
    void insertdata2()
    {

        try
        {

            getaccountno();
            getlatestamount();
            DateTime statement_date = DateTime.Now;

            string title = "Investment Withdrawn: " ;
            double d = 0.0;
            cn.Open();
            SqlCommand cm = new SqlCommand(
            "INSERT INTO Statement (Date,Title, Account, credit,debit , Total, Client_Id) VALUES(@Date,@Title, @Account, @credit,@debit, @Total, @Client_Id)", cn);
            cm.Parameters.Add("@Date", statement_date);
            cm.Parameters.Add("@Title", title);
            cm.Parameters.Add("@Account", latestAcoountNo);
            cm.Parameters.Add("@credit", LblTotalProfit.Text);
            cm.Parameters.Add("@debit", d);
            cm.Parameters.Add("@Total", latestamount);
            cm.Parameters.Add("@Client_Id", fn);


            cm.ExecuteNonQuery();

            cm.Clone();

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
    void getaccountno()
    {
        try
        {
            cn.Close();
            cn.Open();



            string sql = "SELECT  Account.Account_No FROM AccountType INNER JOIN Account ON AccountType.Account_Type_Id = Account.Account_Type_Id WHERE (Account.Client_Id = @cid and AccountType.Type = @AT)";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@AT", LblAccountType.Text);
            cmd.Parameters.AddWithValue("@cid", fn);


            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    latestAcoountNo = dr["Account_No"].ToString();


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

    }
    void getlatestamount()
    {

        try
        {
            cn.Close();
            cn.Open();

            //   int i = int.Parse(LblAccountNo.Text);

            string sql = "Select sum(Amount) as Amount from Account where Account_No=@No";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("No", latestAcoountNo);


            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    latestamount = dr["Amount"].ToString();


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
    }
   
    }
