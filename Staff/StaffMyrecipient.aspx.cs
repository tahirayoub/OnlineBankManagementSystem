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


public partial class Client_Myrecipient : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, cid1, an1,an2,id = "";
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
                        ImgUpdate.Visible = false;
                        LblError.Visible = true;
                        LblError.Text = "Please Enter the Client Debit Card number to access the information";

                    }
                    else
                    {

                        LblError.Visible = false;
                        ImgUpdate.Visible = false;
                    }
                }
                else
                {
                    ImgUpdate.Visible = false;
                    LblError.Visible = true;
                    LblError.Text = "Please Enter the Client Debit Card number Perform Functions";

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

                recipientList();
              

            }
        }
        catch
        { }
    }

    void recipientList()
    {
        try
        {


            var da = new SqlDataAdapter();
            var ds = new DataSet();


            da = new SqlDataAdapter("Select Recipient_Id AS [Select], Name,Account_No,Email  from Recipient where Client_Id = '" + LblClientId.Text + "'", cn);
            ds = new DataSet();

            da.Fill(ds);
            GVClientBillList.DataSource = ds;
            GVClientBillList.DataBind();

            this.GVClientBillList.Columns[0].Visible = false;


        }

        catch { }
    }
   
    protected void GVClientBillList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GVClientBillList.PageIndex = e.NewPageIndex;
            recipientList();
        }
        catch
        { }
    }
    protected void GVClientBillList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
                 var row = GVClientBillList.SelectedRow;
            
                   LblID.Text= row.Cells[1].Text;
                   TxtAccountNo.Enabled = true;
                   LblError.Visible = false;
                   ImgUpdate.Visible = true;

                   getRecipient();
           
        }
        catch
        { }
    }

    void getRecipient()
    {

        try
        {
            cn.Close();
            cn.Open();



            string sql = "Select Name, Account_No,Email from recipient where Recipient_Id=@R_id";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("R_id", LblID.Text);
           

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    TxtName.Text = dr["Name"].ToString();
                    TxtAccountNo.Text= dr["Account_No"].ToString();
                    TxtEmail.Text = dr["Email"].ToString();
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
    protected void ImgUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (TxtName.Text == "")
            {
                LblError.Visible = true;
                LblError.Text = "Enter Recipient Name ";
                return;
            }
            if (TxtName.Text == "")
            {
                LblError.Visible = true;
                LblError.Text = "Enter Recipient Id of the company ";

            }
            if (TxtAccountNo.Text == "")
            {
                LblError.Visible = true;
                LblError.Text = "Enter Account Number of the Recipient ";
                return;
            }
            else if (TxtAccountNo.Text != null)
            {
                try
                {
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

                    if (an2 == TxtAccountNo.Text)
                    {
                        LblError.Visible = true;
                        LblError.Text = "The account Number is Valid";
                        TxtAccountNo.Enabled = false;
                        updatefun();

                    }

                    else if (an2 != TxtAccountNo.Text)
                    {
                        LblError.Visible = true;
                        LblError.Text = "The account Number does not exist in our system";
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
            }
          

          
        }
        catch { }
    }
    void updatefun()
    {
        try
        {
            if (LblID.Text != null)
            {

                cn.Close();

                string sql2 = "UPDATE Recipient SET Name=@nn, Account_No=@an,Email=@em where Recipient_Id=@R_id ";
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = cn;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = sql2;
                cmd2.Parameters.AddWithValue("R_id", LblID.Text);
                cmd2.Parameters.AddWithValue("nn", TxtName.Text);
                cmd2.Parameters.AddWithValue("an", TxtAccountNo.Text);
                cmd2.Parameters.AddWithValue("em", TxtEmail.Text);

                cn.Open();
                cmd2.ExecuteNonQuery();

                cmd2.Clone();
                cn.Close();
              
                LblError.Visible = true;
                LblError.Text = "Recipient Account is Successfully updated";
                recipientList();
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