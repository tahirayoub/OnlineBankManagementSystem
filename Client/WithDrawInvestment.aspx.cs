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


public partial class Client_WithDrawInvestment : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn = "";
    int i = 1;

    DateTime dt = DateTime.Now.Date;
    DateTime ed = DateTime.Now;

    double amount, ac_amount, interest_amount = 0.0;

    string latestamount, latestAcoountNo = "";

    double txtamt = 0.0;


    string s1 = "";

    int randomNumber = 0;
    int c_yr = 0;
    int c_mon = 0;
    int c_day = 0;

    string amount_db = "";
    string inv_Type = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (Session["c_id"] != null)
            {

                fn = Session["c_id"].ToString();


                cmd.Connection = cn;
                LblDate.Text = dt.ToString("dd/MM/yyyy");

                c_yr = dt.Year % 100;
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



          //  this.GVClientBillList.Columns[0].Visible = false;



        }

        catch { }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            LblAmount.Visible = false;
            LblInterest.Visible = false;
            Label3.Visible = false;

            if(LblInvType.Text == "" || LblInvType.Text==null)
            {
                LblError.Visible = true;
                LblError.Text = "Please Select Investment from above table";
                return;
            }
            if (LblInvType.Text == "Fixed (open Ended)")
            {
                ac_amount = double.Parse(TxtAmount.Text);

                interest_amount = (ac_amount / 100) * 5;
                amount = ac_amount - interest_amount;

                if (0.0 <= amount)
                {
                    getaccountAmount();
                   updateInvamount();
                   LblError.Visible = true;

                   LblError.Text = "Successfully withdrawn money and 5% fee is paid";

                   txtamt = double.Parse(TxtAmount.Text) + interest_amount;
                   insertdata2();


                }
                else
                {
                    LblError.Visible = true;
                    LblError.Text = "Insificient Balance in your chequen Account";
                }

            }

            if (LblInvType.Text == "Variable (Close Ended)")
            {
                amount = double.Parse(TxtAmount.Text);
                getaccountAmount();
                updateInvamount();
                txtamt = double.Parse(LblAmount.Text);
                insertdata2();

              
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
    void getaccountAmount()
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
    void updateInvamount()
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
            LblError.Visible = true;
            LblError.Text = "Successfully withdrawn investment ";
           

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
    protected void GVClientBillList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var row = GVClientBillList.SelectedRow;
        LblId.Text = row.Cells[1].Text;
       LblInvType.Text = row.Cells[2].Text;
       LblAccountType.Text = row.Cells[3].Text;
       TxtAmount.Text = row.Cells[7].Text;
       LblError.Visible = false;
       if (LblInvType.Text == "Fixed (open Ended)")
       {
           double interests = 0.05;
           LblInterest.Text = "0.05 (5%)";
           if (TxtAmount.Text != null || TxtAmount.Text == "")
           {
               double amt = double.Parse(TxtAmount.Text) - (interests * (double.Parse(TxtAmount.Text)));
               LblAmount.Text = Convert.ToString(amt);



           }

       }
       if (LblInvType.Text == "Variable (Close Ended)")
       {
           
           LblInterest.Text = "0";
           if (TxtAmount.Text != null || TxtAmount.Text == "")
           {

               LblAmount.Text = TxtAmount.Text;

           }
       }
    }
    protected void TxtAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
        
        }
        catch
        { }
    }


    void insertdata2()
    {

        try
        {

            getaccountno();
            getlatestamount();
            DateTime statement_date = DateTime.Now;

            string title = "withdrawn : " + LblInvType.Text;
            double d = 0.0;
            cn.Open();
            SqlCommand cm = new SqlCommand(
            "INSERT INTO Statement (Date,Title, Account, credit,debit , Total, Client_Id) VALUES(@Date,@Title, @Account, @credit,@debit, @Total, @Client_Id)", cn);
            cm.Parameters.Add("@Date", statement_date);
            cm.Parameters.Add("@Title", title);
            cm.Parameters.Add("@Account", latestAcoountNo);
            cm.Parameters.Add("@credit", LblAmount.Text);
            cm.Parameters.Add("@debit", d);
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