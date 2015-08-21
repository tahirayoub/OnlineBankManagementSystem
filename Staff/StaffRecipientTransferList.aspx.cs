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

public partial class Client_RecipientTransferList : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, cid1, an1 = "";
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

                }
                cmd.Connection = cn;

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
            Session["Rec_Id"] = row.Cells[1].Text;
            Session["acc_no"] = row.Cells[3].Text;
            Response.Redirect("StaffRecipientTransfer.aspx");
        }
        catch
        { }
    }
}