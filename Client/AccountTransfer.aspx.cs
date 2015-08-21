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
public partial class Client_AccountTransfer : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn = "";
    DateTime dt = DateTime.Now;
  
    double account_saving = 0.0;
    double account_current = 0.0;
    string s1, s2, s3,s4 = "";
    double amount,amount2 = 0.0;
    double chk_amount = 0.0;
    double interest_amount = 0.0;
    string latestamount, latestAcoountNo, latestAcoountNo2 = "";

    double txtamt = 0.0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (Session["c_id"] != null)
            {

                fn = Session["c_id"].ToString();
                cmd.Connection = cn;
                LblDate.Text = dt.ToShortDateString();
            }
            else
                Response.Redirect("LoginClient.aspx");
        }
        catch
        { }


     
    }

    void getaccount()
    {
        try
        {
            cn.Close();
            cn.Open();


         //   int accont_no = int.Parse(DropDownList1.Text);
           // int accont_no2 = int.Parse(DropDownList1.Text);

            string sql = "SELECT  SUM(Amount) AS TotalAmount,SUM(Amount) AS TotalAmount2,Account_Type_Id  from Account where Client_Id=@cid and Account_Type_Id = @ATD GROUP BY Account_Type_Id";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("cid", fn);
            cmd.Parameters.AddWithValue("ATD", DropDownList1.SelectedValue);
           



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

            updatefun1();
        }




        catch
        {


        }
        finally
        {
            cn.Close();

        }
    }
    void updatefun1()
    {
        try
        {
            cn.Close();
            cn.Open();


        

            string sql = "SELECT  SUM(Amount) AS TotalAmount,SUM(Amount) AS TotalAmount2,Account_Type_Id  from Account where Client_Id=@cid and Account_Type_Id = @ATD GROUP BY Account_Type_Id";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("cid", fn);
            cmd.Parameters.AddWithValue("ATD", DropDownList2.SelectedValue);




            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    s3 = dr["TotalAmount"].ToString();
                    s4 = dr["Account_Type_Id"].ToString();

                }
            }




            dr.Close();
            cn.Close();

            if (s3 != null && s1!=null)
            {
                if (s4 != null && s2!=null)
                {
                    int a2 = int.Parse(s4);

                    int a1 = int.Parse(s2);

                    amount2 = double.Parse(s3);
                    amount = double.Parse(s1);


                    double txamount = double.Parse(TxtAmount.Text);
                    //int a2 = int.Parse(DropDownList2.Text);

                    if (a1 == a2)
                    {
                        LblError.Visible = true;
                        LblError.Text = "Cannot transfer in the same account type please select different accounts to Trasfer";
                        TxtAmount.Text = "";
                       
                    }

                    else if (a1 != a2)
                    {


                        if (txamount <= amount)
                        {
                            if (s2 == "1")
                            {

                                double a = (txamount / 100) * 2;
                                amount = amount - a - txamount;
                                //txtamt = a + double.Parse(TxtAmount.Text);
                                if (0.0 <=amount)
                                {
                                    LblError.Visible = false;
                                    txtamt = double.Parse(TxtAmount.Text)+a;
                                    update1();
                                  

                                }
                                else
                                {
                                    LblError.Visible = true;
                                    LblError.Text = "Insificient Balance in your Saving Account";
                                    return;
                                }
                              
                            }
                           else  if(s2 == "2")
                            {
                                amount = amount - txamount;
                                txtamt = double.Parse(TxtAmount.Text);
                                update1();
                              //  insertdata2();
                            }
                           

                            amount2 = amount2 + txamount;
                            update2();
                            insertdata2();
                            insertdata3();

                        }
                        else
                        {
                            LblError.Visible = true;
                            LblError.Text = "Not enough balance";
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

    void update1()
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
            getaccountno2();
            string title = "Transfer TO : " + latestAcoountNo2 ;
            double d = 0.0;
            cn.Open();
            SqlCommand cm = new SqlCommand(
            "INSERT INTO Statement (Date,Title, Account, credit,debit , Total, Client_Id) VALUES(@Date,@Title, @Account, @credit,@debit, @Total, @Client_Id)", cn);
            cm.Parameters.Add("@Date", statement_date);
            cm.Parameters.Add("@Title", title);
            cm.Parameters.Add("@Account", latestAcoountNo);
            cm.Parameters.Add("@credit", d);
            cm.Parameters.Add("@debit",txtamt);
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
    void insertdata3()
    {

        try
        {

            getaccountno3();
            getlatestamount2();
            DateTime statement_date = DateTime.Now;
            getaccountno4();
            string title = "Transfer from : " + latestAcoountNo2;
            double d = 0.0;
            cn.Open();
            SqlCommand cm = new SqlCommand(
            "INSERT INTO Statement (Date,Title, Account, credit,debit , Total, Client_Id) VALUES(@Date,@Title, @Account, @credit,@debit, @Total, @Client_Id)", cn);
            cm.Parameters.Add("@Date", statement_date);
            cm.Parameters.Add("@Title", title);
            cm.Parameters.Add("@Account", latestAcoountNo);
            cm.Parameters.Add("@credit", TxtAmount.Text);
            cm.Parameters.Add("@debit",d );
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
    void getaccountno3()
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
    void getaccountno4()
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

                    latestAcoountNo2 = dr["Account_No"].ToString();


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
    void getaccountno2()
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

                    latestAcoountNo2 = dr["Account_No"].ToString();


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
    void getlatestamount2()
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
    void update2()
    {
        try
        {



            string sql2 = "UPDATE Account SET Amount=@cc where Client_Id=@cid and  Account_Type_Id = @Account_Type_Id";
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;
            cmd2.Parameters.AddWithValue("cc", amount2);
            cmd2.Parameters.AddWithValue("cid", fn);
            cmd2.Parameters.AddWithValue("Account_Type_Id", DropDownList2.Text);
            cn.Open();
            cmd2.ExecuteNonQuery();

            cmd2.Clone();
            cn.Close();
            LblError.Visible = true;
            LblError.Text = "Successfully Transfered the amount";
          //  TxtAmount.Text = "";
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
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (TxtAmount.Text != "")
            {


                double t1 = double.Parse(TxtAmount.Text);
            if (t1 <= 0.0)
            {
                LblError.Visible = true;
                LblError.Text = "Please enter positive amount of money";
                TxtAmount.Focus();
                return;
            }
           else
           {
            getaccount();
           }
           }
            else

            {
                  LblError.Visible = true;
                LblError.Text = "Please Enter Valid Amount";
                TxtAmount.Focus();
                return;
            }
        }
        catch
        { }
        
    }
}