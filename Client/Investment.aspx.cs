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

public partial class Client_Investment : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn  = "";
    
    DateTime dt = DateTime.Now.Date;
    DateTime ed = DateTime.Now;
    double t1 = 0.0;
    double amount = 0.0;
    double chk_amount = 0.0;
    double interest_amount = 0.0;
    string s1 = "";
    string s2 = "";
    int randomNumber = 0;

    string latestamount, latestAcoountNo = "";

    double txtamt = 0.0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (Session["c_id"] != null)
            {

                fn = Session["c_id"].ToString();
                cmd.Connection = cn;
                LblDate.Text = dt.ToString("dd/MM/yyyy");

                //int e_year = dt.Year % 100;
                //int e_month = dt.Month % 100;
                //int e_day = dt.Day % 100;


                //string ee = Convert.ToString(e_day) + "/" + Convert.ToString(e_month) + "/" + Convert.ToString(e_year);


                //LblDate.Text = ee;
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


            da = new SqlDataAdapter("Select Type AS [Investment Type],Description from InvestmentType", cn);
            ds = new DataSet();

            da.Fill(ds);
            GVClientBillList.DataSource = ds;
            GVClientBillList.DataBind();




        }

        catch { }
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            try
            {
                if (TxtAmount.Text == "")
                {
                    LblError.Visible = true;
                    LblError.Text = "Please enter amount in digits/Numbers";
                    TxtAmount.Focus();
                    return;
                }
                if (TxtAmount.Text != "")
                {
                    t1 = double.Parse(TxtAmount.Text);
                    if (t1 <= 0.0)
                    {
                        LblError.Visible = true;
                        LblError.Text = "Please enter positive amount of money";
                        TxtAmount.Focus();
                        return;
                    }
                }
            }
            catch
            {
                LblError.Visible = true;
                LblError.Text = "Please enter amount in digits/Numbers";
                return;
            }
            getaccountamount();
            TxtAmount.Text = "";

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

    /// <summary>
    /// check either selected account has amount or not;
    /// </summary>
    void getaccountamount()
    {
        try
        {
            cn.Close();
            cn.Open();


            int accont_no = int.Parse(DropDownList2.Text);

            string sql = "SELECT  SUM(Amount) AS TotalAmount,Account_Type_Id  from Account where Client_Id=@cid and Account_Type_Id = @ATD GROUP BY Account_Type_Id";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("cid", fn);
            cmd.Parameters.AddWithValue("ATD", accont_no);



            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    s1 = dr["TotalAmount"].ToString();
                    s2 = dr["Account_Type_Id"].ToString();
                }
            }




            dr.Close();
            cn.Close();
            if (s1 != null || s1 != "")
            {
                if (s2 != null || s2 != "")
                {
                    int a = int.Parse(s2);
                    amount = double.Parse(s1);
                    if (a == 2)
                    {

                        chk_amount = double.Parse(TxtAmount.Text);
                        if (chk_amount <= amount)
                        {
                            LblError.Visible = false;
                            amount = amount - chk_amount;
                            insertbillamount();
                            txtamt = double.Parse(TxtAmount.Text);
                            insertdata2();


                        }
                        else
                        {
                            LblError.Visible = true;
                            LblError.Text = "Insificient Balance in your current Account";
                        }


                    }
                    else if (a == 1)
                    {
                        chk_amount = double.Parse(TxtAmount.Text);
                        if (chk_amount <= amount)
                        {
                            interest_amount = (chk_amount / 100) * 2;
                            amount = amount - interest_amount - chk_amount;



                            if (0.0 <= amount)
                            {
                                LblError.Visible = false;

                             insertbillamount();
                             txtamt = double.Parse(TxtAmount.Text) + interest_amount;
                             insertdata2();


                            }
                            else
                            {
                                LblError.Visible = true;
                                LblError.Text = "Insificient Balance in your chequen Account";
                            }

                        }
                        else
                        {
                            LblError.Visible = true;
                            LblError.Text = "Insificient Balance in your Saving Account";
                        }

                    }
                    else
                    {
                        LblError.Visible = true;
                        LblError.Text = "Insificient Balance in your Account";
                    }

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
    void insertbillamount()
    {
        try
        {



            if (TxtAmount.Text != "")
            {

                cn.Close();
                cn.Open();
                if (DropDownList1.Text == "3")
                {
                    Random random = new Random();

                    randomNumber = random.Next(1, 100);
                }
                else
                {
                    randomNumber = 0;
                }
                int yr = int.Parse(DropDownList3.Text);
                ed = ed.AddYears(yr);
                /////////////////////////

                SqlCommand cm = new SqlCommand(
                  "INSERT INTO Investment (Investment_Type_Id, Client_Id,Amount,Account_Type_Id, StartDate,EndDate,Inv_Number,YearInvestment) VALUES(@Investment_Type_Id, @Client_Id,@Amount,@Account_Type_Id, @StartDate,@EndDate,@Inv_Number,@YearInvestment)", cn);

                cm.Parameters.Add("@Investment_Type_Id", DropDownList1.Text);
                cm.Parameters.Add("@Client_Id", fn);

               
                cm.Parameters.Add("@Amount", TxtAmount.Text);
                cm.Parameters.Add("@Account_Type_Id", DropDownList2.Text);
                
                cm.Parameters.Add("@StartDate", dt);
                cm.Parameters.Add("@EndDate", ed);
                cm.Parameters.Add("@Inv_Number", randomNumber);

                cm.Parameters.Add("@YearInvestment", DropDownList3.Text);


                cm.ExecuteNonQuery();

                cm.Clone();


                cn.Close();

                LblError.Visible = true;
                updateamount();
                LblError.Text = "Amount Successfully Invested";
                

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
    void updateamount()
    {

        try
        {



            string sql2 = "UPDATE Account SET Amount=@cc where Client_Id=@cid and  Account_Type_Id = @Account_Type_Id";
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;
            cmd2.Parameters.AddWithValue("cc", amount);
            cmd2.Parameters.AddWithValue("cid", fn);
            cmd2.Parameters.AddWithValue("Account_Type_Id", DropDownList2.Text);
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
/// <summary>
/// ////////
/// </summary>
    void insertdata2()
    {

        try
        {

            getaccountno();
            getlatestamount();
            DateTime statement_date = DateTime.Now;
            string abb = "";
            if(DropDownList1.Text == "1")
                abb = "Fixed (open Ended)";
            if (DropDownList1.Text == "2")
                abb = "Variable (Close Ended)";

            string title = "Investment of : " + abb;
            double d = 0.0;
            cn.Open();
            SqlCommand cm = new SqlCommand(
            "INSERT INTO Statement (Date,Title, Account, credit,debit , Total, Client_Id) VALUES(@Date,@Title, @Account, @credit,@debit, @Total, @Client_Id)", cn);
            cm.Parameters.Add("@Date", statement_date);
            cm.Parameters.Add("@Title", title);
            cm.Parameters.Add("@Account", latestAcoountNo);
            cm.Parameters.Add("@credit", d);
            cm.Parameters.Add("@debit",txtamt );
            cm.Parameters.Add("@Total", latestamount);
            cm.Parameters.Add("@Client_Id", fn);


            cm.ExecuteNonQuery();

            cm.Clone();

            cn.Close();

            TxtAmount.Text = "";

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

            int i = int.Parse(DropDownList2.SelectedValue);

            string sql = "Select Account_No from Account where Account_Type_Id=@ATD and client_Id=@cid";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@ATD", i);
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