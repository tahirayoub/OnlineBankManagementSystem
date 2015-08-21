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


public partial class Client_ModifyBill : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, an1,an2 = "";
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


                ClientBillList();


            }
        }


        catch
        { }
    }
    void ClientBillList()
    {

        try
        {


            var da = new SqlDataAdapter();
            var ds = new DataSet();


            da = new SqlDataAdapter("Select Client_Bill_Id AS [Select],Transit_Id AS [Transit Id] ,Account_Number AS [Account Number], Name  from ClientBill where Client_Id = '" + LblClientId.Text + "'", cn);
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
        ClientBillList();
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
    try
    {
       
        var row = GVClientBillList.SelectedRow;
        Label13.Text = row.Cells[1].Text;
        TxtTransitNo.Text = row.Cells[2].Text;
        TxtAccountNo.Text = row.Cells[3].Text;
        TxtName.Text = row.Cells[4].Text;
        TxtAccountNo.Enabled = true;
        hello();

    }
    catch
    { }
}

void hello()
{
    try
    {
        cn.Close();
        cn.Open();



        string sql = "Select Company_Account_No from BillList where Company_Account_No=@AN";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;

        cmd.Parameters.AddWithValue("AN", TxtAccountNo.Text);

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                an1 = dr["Company_Account_No"].ToString();

            }
        }


        dr.Close();
        cn.Close();

        if (an1 == TxtAccountNo.Text)
        {
            TxtName.Text = "";
            TxtAccountNo.Text = "";
            TxtTransitNo.Text = "";
            LblError.Visible = true;
            ImgUpdate.Visible = false;
            LblError.Text = "Cannot Update this Account Information " + an1 + "  Please Contact Staff/Member Phone # 123456789";
            return;
        }
        else
        {
            LblError.Visible = false;
            ImgUpdate.Visible = true;
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
protected void ImgUpdate0_Click(object sender, ImageClickEventArgs e)
{
    try
    {

        if (TxtAccountNo.Text == "")
        {
            LblError.Visible = true;
            LblError.Text = "Enter Account Number of the company ";
            return;
        }
        if (TxtTransitNo.Text == "")
        {
            LblError.Visible = true;
            LblError.Text = "Enter Transit Number of the company ";
            return;
        }
        if (TxtName.Text == "")
        {
            LblError.Visible = true;
            LblError.Text = "Enter Company Name of the company ";
            return;
        }
        if (TxtAccountNo.Text != "")
        {
            try
            {
                if (TxtTransitNo.Text.Length != 6)
                {
                    LblError.Visible = true;
                    LblError.Text = "Please enter 6 digits Transit Id ";
                    return;
                }
                if (TxtAccountNo.Text.Length == 7)
                {
                    insertfun();
                }
                else
                {
                    LblError.Visible = true;
                    LblError.Text = "Please enter 7 digits account ";
                }
            }
            catch { }
        }

    }

    catch { }
}

void insertfun()
{
    try
    {





        cn.Close();
      
        try
        {



            string sql2 = "UPDATE ClientBill SET Transit_Id=@Tid, Account_Number = @AN, Name=@NAME where Client_Id=@cid and Client_Bill_Id = @ClientBillId";
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;
            cmd2.Parameters.AddWithValue("Tid", TxtTransitNo.Text);
            cmd2.Parameters.AddWithValue("AN", TxtAccountNo.Text);
            cmd2.Parameters.AddWithValue("Name", TxtName.Text);
            cmd2.Parameters.AddWithValue("cid",LblClientId.Text);
            cmd2.Parameters.AddWithValue("ClientBillId", Label13.Text);
            cn.Open();
            cmd2.ExecuteNonQuery();

            cmd2.Clone();
            cn.Close();
            LblError.Visible = true;
        
            LblError.Text = "Successfully Updated";
            ClientBillList();
        }


        catch (Exception ex)
        {

            Response.Write(ex.Message);
            LblError.Visible = true;
            LblError.Text = "This Account in not valid please enter a valid Account Number ";
           
        }
        finally
        {
            cn.Close();

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
