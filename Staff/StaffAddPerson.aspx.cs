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


public partial class Client_AddPerson : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, cid1, an1,an2, bt = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (Session["s_id"] != null)
            {
                fn = Session["s_id"].ToString();
              

                if (Session["s_c_id"] != null)
                {
                    LblClientId.Text = Session["s_c_id"].ToString();
                    TextBox t = (TextBox)Master.FindControl("TxtMasterbox");

                    t.Text = Session["s_debit_id"].ToString();
                    t.Enabled = false;

                    if (t.Text == "")
                    {
                        ImgUpdate.Visible = false;
                        LblError.Visible = true;
                        LblError.Text = "Please Enter the Client Debit Card number to access the information";

                    }
                    else
                    {

                        LblError.Visible = false;
                        ImgUpdate.Visible = true;
                    }
                }
                else 
                {
                    ImgUpdate.Visible = false;
                    LblError.Visible = true;
                    LblError.Text = "Please Enter the Client Debit Card number Perform Functions";

                }
                cmd.Connection = cn;

            }
            else
                Response.Redirect("Staff_Login.aspx");


        }
        catch
        { }



         }
    protected void ImgUpdate0_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (TxtAccountNo.Text != null && TxtRecipientName.Text !=null)
            {
                try
                {

                    if (TxtAccountNo.Text.Length != 7)
                    {
                        LblError.Visible = true;
                        LblError.Text = "Please enter 7 digits of Account Number ";
                        return;
                    }
                    cn.Close();
                    cn.Open();



                    string sql = "Select Account_No from Account where Account_No=@AcNo";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("AcNo", TxtAccountNo.Text);

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            an2 = dr["Account_No"].ToString();
                         
                        }
                    }


                    dr.Close();
                    cn.Close();

                    if (an2 == TxtAccountNo.Text )
                    {
                        LblError.Visible = true;
                        LblError.Text = "The account Number is Valid";
                       // TxtAccountNo.Enabled = false;
                     
                    }

                    else if (an2 != TxtAccountNo.Text)
                    {
                        LblError.Visible = true;
                        LblError.Text = "The account Number does not exist";
                        return;

                    }




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
                    cn.Close();
                    cn.Open();



                    string sql = "Select Name, Account_No,Bank_Transit_No,Email, Client_Id from Recipient where Client_Id=@c_id and Bank_Transit_No=@BnkId and Account_No=@AcNo";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("c_id", LblClientId.Text);
                    cmd.Parameters.AddWithValue("BnkId", TxtTransitNumber.Text);
                    cmd.Parameters.AddWithValue("AcNo", TxtAccountNo.Text);

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            TxtRecipientName.Text = dr["Name"].ToString();
                            an1 = dr["Account_No"].ToString();
                            bt = dr["Bank_Transit_No"].ToString();
                            TxtEmail.Text = dr["Email"].ToString();
                            cid1 = dr["Client_Id"].ToString();
                        }
                    }


                    dr.Close();
                    cn.Close();

                    if (an1 == TxtAccountNo.Text && cid1 == LblClientId.Text && bt == TxtTransitNumber.Text)
                    {
                        LblError.Visible = true;
                        LblError.Text = "Recipient Already Exist Info Already Exist";
                        TxtTransitNumber.Text = bt;
                        TxtAccountNo.Text = an1;

                        return;
                    }




                }
                catch
                {


                }
                finally
                {
                    cn.Close();

                }


                cn.Open();



                SqlCommand cm = new SqlCommand(
                "INSERT INTO Recipient (Name,Account_No, Bank_Transit_No, Client_Id,Email) VALUES(@Name,@Account_No, @Bank_Transit_No, @Client_Id,@Email)", cn);
                cm.Parameters.Add("@Name", TxtRecipientName.Text);
                cm.Parameters.Add("@Account_No", TxtAccountNo.Text);
                cm.Parameters.Add("@Bank_Transit_No", TxtTransitNumber.Text);
                cm.Parameters.Add("@Client_Id", LblClientId.Text);
                cm.Parameters.Add("@Email", TxtEmail.Text);




                cm.ExecuteNonQuery();

                cm.Clone();


                cn.Close();
                //    Response.Write("Contact Added Successfully!");
                LblError.Visible = true;
                LblError.Text = "Recipient added Successfully";


                //  Response.Redirect("BillList.aspx", false);
                // ClientBillList();


            }
            else
            {
                LblError.Visible = true;
                LblError.Text = "Please Enter Mandatory Information";

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
}