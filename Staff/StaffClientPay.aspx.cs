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

public partial class Client_Pay : System.Web.UI.Page
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
         try
        {
           
            if (Session["s_id"] != null)
            {

                cmd.Connection = cn;
                fn = Session["s_id"].ToString();

                if (Session["s_c_id"] != null)
                {
                    LblClientId.Text = Session["s_c_id"].ToString();
                    TextBox t = (TextBox)Master.FindControl("TxtMasterbox");

                    t.Text = Session["s_debit_id"].ToString();
                    t.Enabled = false;

                    if (t.Text == "")
                    {
                        ImageButton2.Visible = false;
                        LblError.Visible = true;
                        LblError.Text = "Please Enter the Client Debit Card number to access the information";

                    }
                    else
                    {

                        LblError.Visible = false;
                        ImageButton2.Visible = true;
                    }
                }
                else
                {
                    ImageButton2.Visible = false;
                    LblError.Visible = true;
                    LblError.Text = "Please Enter the Client Debit Card number Perform Functions";

                }
              
                bill = Session["s_cd_bill"].ToString();
               
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

               
                getinfo();

            }
        }
        catch
        { }
    }

    void getinfo()
    {
        try
        {
            
            cn.Open();

            string sql = "Select Transit_Id, Account_Number, Name from ClientBill where Client_Id=@c_id and Client_Bill_Id=@Bid";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("c_id", LblClientId.Text);
            cmd.Parameters.AddWithValue("Bid", bill );

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    LblTransitId.Text = dr["Transit_Id"].ToString();
                    LblAccountNo.Text = dr["Account_Number"].ToString();
                    LblCompanyName.Text = dr["Name"].ToString();
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
           

            int accont_no = int.Parse(DropDownList1.Text);

            string sql = "SELECT  SUM(Amount) AS TotalAmount,Account_Type_Id  from Account where Client_Id=@cid and Account_Type_Id = @ATD GROUP BY Account_Type_Id";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("cid", LblClientId.Text);
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
                        double dbamount = double.Parse(s1);
                        chk_amount = double.Parse(TxtAmount.Text);
                        if (chk_amount <= dbamount)
                        {
                            LblError.Visible = false;
                            amount = amount - chk_amount;
                            insertbillamount();
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
                         double  dbamount = double.Parse(s1);
                         if (chk_amount <= dbamount)
                        {
                            interest_amount = (chk_amount / 100) * 2;
                            amount = amount - interest_amount - chk_amount;
                            //double ab = dbamount - amount;

                            if (0.0 <=amount)
                            {
                                LblError.Visible = false;
                                insertbillamount();

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
                  "INSERT INTO Bill (Client_Id, Client_Bill_Id, Amount,date,Account_Type_Id) VALUES(@Client_Id, @Client_Bill_Id, @Amount,@date,@Account_Type_Id)", cn);
                cm.Parameters.Add("@Client_Id", LblClientId.Text);

                cm.Parameters.Add("@Client_Bill_Id", bill);
                cm.Parameters.Add("@Amount", TxtAmount.Text);
                cm.Parameters.Add("@date", dt);
                cm.Parameters.Add("@Account_Type_Id", DropDownList1.Text); 


                cm.ExecuteNonQuery();

                cm.Clone();


                cn.Close();
              
                LblError.Visible = true;
                updateamount();
                Session["label"] = "Bill is Payed Successfully";
                LblError.Text = Session["label"].ToString();
                TxtAmount.Text = "";
               
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
            cmd2.Parameters.AddWithValue("cid", LblClientId.Text);
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
   

    

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("StaffClientBill.aspx", false);
        }
        catch
        { }
    }
}