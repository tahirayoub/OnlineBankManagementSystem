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
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Drawing;


public partial class Admin_AdminManagerUpdate : System.Web.UI.Page

{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, cid1, an1 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["a_id"] != null)
            {

                fn = Session["a_id"].ToString();
                cmd.Connection = cn;
            }
            else
                Response.Redirect("AdminLogin.aspx");
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


            da = new SqlDataAdapter("Select Manager_Id AS [Select],First_Name AS [First Name], Email  from Manager ", cn);
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
            Session["admin_Manager_sd"] = row.Cells[1].Text;
            Response.Redirect("~/Admin/AdminManagerUpdate2.aspx");
        }
        catch
        { }
    }
}