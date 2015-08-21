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


public partial class Client_BillList : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, cid1, an1 = "";

  
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



        try
        {

            if (!IsPostBack)
            {

                Billlist();
                ClientBillList();


            }
        }
        catch
        { }
    }

    void Billlist()
    {
        try
        {
            var da = new SqlDataAdapter();
            var ds = new DataSet();

           
            da = new SqlDataAdapter("SELECT Company_Transit_Id AS [Transit #],Company_Account_No AS [Account No],Company_Name AS [Company Name] from BillList", cn);
            ds = new DataSet();

            da.Fill(ds);
            GVBillList.DataSource = ds;
            GVBillList.DataBind();
          
        
        }

        catch { }
    }

    void ClientBillList()
    {
        try
        {
     

            var da = new SqlDataAdapter();
            var ds = new DataSet();


            da = new SqlDataAdapter("Select Client_Bill_Id AS [Select],Account_Number, Name  from ClientBill where Client_Id = '" + fn + "'", cn);
            ds = new DataSet();

            da.Fill(ds);
            GVClientBillList.DataSource = ds;
            GVClientBillList.DataBind();

            this.GVClientBillList.Columns[0].Visible = false;


        }

        catch { }
    }

    protected void GVBillList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GVBillList.PageIndex = e.NewPageIndex;
            Billlist();
        }
        catch
        { }
    }
    protected void ImgUpdate0_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            if (TxtAccountNo.Text == null)
            {
                LblError.Visible = true;
                LblError.Text = "Enter Account Number of the company ";
                return;
            }
            if (TxtTransitNo.Text == null)
            {
                LblError.Visible = true;
                LblError.Text = "Enter Transit Number of the company ";
                return;
            }
            if (TxtName.Text == null)
            {
                LblError.Visible = true;
                LblError.Text = "Enter Company Name of the company ";
                return;
            }
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

    void insertfun()
    {
        try
        {
            cn.Close();



            try
            {
                cn.Close();
                cn.Open();



                string sql = "Select Account_Number, Client_Id from ClientBill where Client_Id=@c_id and Account_Number=@AN";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("c_id", fn);
                cmd.Parameters.AddWithValue("AN", TxtAccountNo.Text);

                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        an1 = dr["Account_Number"].ToString();
                        cid1 = dr["Client_Id"].ToString();
                    }
                }


                dr.Close();
                cn.Close();

                if (an1 == TxtAccountNo.Text && cid1 == fn)
                {
                    LblError.Visible = true;
                    LblError.Text = "Company Info Already Exist";
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
            "INSERT INTO ClientBill (Transit_Id, Account_Number, Name, Client_Id) VALUES(@Transit_Id, @Account_Number, @Name, @Client_Id)", cn);
            cm.Parameters.Add("@Transit_Id", int.Parse(TxtTransitNo.Text));
            cm.Parameters.Add("@Account_Number", int.Parse(TxtAccountNo.Text));
            cm.Parameters.Add("@Name", TxtName.Text);
            cm.Parameters.Add("@Client_Id", fn);




            cm.ExecuteNonQuery();

            cm.Clone();


            cn.Close();
        //    Response.Write("Contact Added Successfully!");
            LblError.Visible = true;
            LblError.Text = "Contact added Successfully";


            Response.Redirect("BillList.aspx",false);
           // ClientBillList();


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
   
    protected void GVBillList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var row = GVBillList.SelectedRow;
            TxtTransitNo.Text = row.Cells[1].Text;
            TxtAccountNo.Text = row.Cells[2].Text;
            TxtName.Text = row.Cells[3].Text;
           // insertfun();
           
        }
        catch
        { }

    }
    protected void GVClientBillList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var row = GVClientBillList.SelectedRow;
            Session["cd_bill"] = row.Cells[1].Text;
            Response.Redirect("Pay.aspx");
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
}