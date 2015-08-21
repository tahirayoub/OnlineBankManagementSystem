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

public partial class Client_withdrawCash : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, cid1, an1 = "";
    string bill = "";
    DateTime dt = DateTime.Now.Date;
    double t1 = 0.0;
    double amount = 0.0;
    double chk_amount = 0.0;
    double interest_amount = 0.0;
    string s1 = "";
    string s2 = "";
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
              
            }
            else
                Response.Redirect("LoginClient.aspx");
        }
        catch
        { }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            if (TxtAmount.Text != "" && TxtAmount2.Text != "")
            {

               
                    double t1 = double.Parse(TxtAmount.Text);

                    double t2 = double.Parse(TxtAmount2.Text);

                    

                    if (t1 <=500 && t2<=500)
                    {
                    if (t1 == t2)
                    {

                        if (t1 <= 0.0)
                        {
                            LblError.Visible = true;
                            LblError.Text = "Please enter positive amount of money";
                            TxtAmount.Focus();
                            return;
                        }
                        if (t2 <= 0.0)
                        {
                            LblError.Visible = true;
                            LblError.Text = "Please enter positive amount of money";
                            TxtAmount2.Focus();
                            return;
                        }

                        
                        else
                        {
                            LblError.Visible = false;
                            getaccountamount();
                        }
                    }
                    else
                    {
                        LblError.Visible = true;
                        LblError.Text = "Please enter same amount";
                    }
                }
                    else
                    {
                        LblError.Visible = true;
                        LblError.Text = "Please enter Amount less than or equal to 500$";
                    }
            }
           
            else
            {
                LblError.Visible = true;
                LblError.Text = "Please enter Amount";
            }
           
        }
        catch
        { }
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


            int accont_no = int.Parse(DropDownList1.Text);

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


                /////////////////////////

                SqlCommand cm = new SqlCommand(
                  "INSERT INTO withdraw (Client_Id, Account_Type_Id,Amount,Dates) VALUES(@Client_Id, @Account_Type_Id,@Amount,@Dates)", cn);
                cm.Parameters.Add("@Client_Id", fn);

                cm.Parameters.Add("@Account_Type_Id", DropDownList1.Text);
                cm.Parameters.Add("@Amount", amount);
                cm.Parameters.Add("@Dates", dt);
             


                cm.ExecuteNonQuery();

                cm.Clone();


                cn.Close();

                LblError.Visible = true;
                updateamount();
                LblError.Text = "Successfully Withdraw the amount";

           

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
            cmd2.Parameters.AddWithValue("Account_Type_Id", DropDownList1.Text);
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
    void insertdata2()
    {

        try
        {

            getaccountno();
            getlatestamount();
            DateTime statement_date = DateTime.Now;

            string title = "Cash Withdraw : " ;
            double d = 0.0;
            cn.Open();
            SqlCommand cm = new SqlCommand(
            "INSERT INTO Statement (Date,Title, Account, credit,debit , Total, Client_Id) VALUES(@Date,@Title, @Account, @credit,@debit, @Total, @Client_Id)", cn);
            cm.Parameters.Add("@Date", statement_date);
            cm.Parameters.Add("@Title", title);
            cm.Parameters.Add("@Account", latestAcoountNo);
            cm.Parameters.Add("@credit", d);
            cm.Parameters.Add("@debit", txtamt);
            cm.Parameters.Add("@Total", latestamount);
            cm.Parameters.Add("@Client_Id", fn);


            cm.ExecuteNonQuery();

            cm.Clone();

            cn.Close();

            TxtAmount.Text = "";
            TxtAmount2.Text = "";

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

            int i = int.Parse(DropDownList1.SelectedValue);

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