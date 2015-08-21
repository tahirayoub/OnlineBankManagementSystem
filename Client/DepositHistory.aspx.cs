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
using System.Globalization;
public partial class Client_DepositHistory : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, cid1, an1 = "";
    string bill = "";
    DateTime ed = DateTime.Now;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["c_id"] != null)
            {

                fn = Session["c_id"].ToString();


                cmd.Connection = cn;
                if (!Page.IsPostBack)
                {
                    //Fill Years
                    int e_year = ed.Year;
                    int e_years = ed.Year - 1;

                    for (int i = e_years; i <= e_year; i++)
                    {
                        ddlYear1.Items.Add(i.ToString());
                    }
                    ddlYear1.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;  //set current year as selected

                    //Fill Months
                    for (int i = 1; i <= 12; i++)
                    {
                        ddlMonth1.Items.Add(i.ToString());
                    }
                    ddlMonth1.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true; // Set current month as selected


                    for (int i = e_years; i <= e_year; i++)
                    {
                        ddlYear2.Items.Add(i.ToString());
                    }
                    ddlYear2.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;  //set current year as selected

                    //Fill Months
                    for (int i = 1; i <= 12; i++)
                    {
                        ddlMonth2.Items.Add(i.ToString());
                    }
                    ddlMonth2.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true; // Set current month as selected

                    //Fill days
                    FillDays();
                    FillDays2();
                }

            }
            else
                Response.Redirect("LoginClient.aspx");
        }
        catch
        { }
    }

    public void FillDays2()
    {
        try
        {
            ddlDay2.Items.Clear();
            //getting numbner of days in selected month & year
            int noofdays = DateTime.DaysInMonth(Convert.ToInt32(ddlYear2.SelectedValue), Convert.ToInt32(ddlMonth2.SelectedValue));

            //Fill days
            for (int i = 1; i <= noofdays; i++)
            {
                ddlDay2.Items.Add(i.ToString());
            }
            ddlDay2.Items.FindByValue(System.DateTime.Now.Day.ToString()).Selected = true;// Set current date as selected
        }
        catch
        { }
    }
    public void FillDays()
    {
        try
        {
            ddlDay1.Items.Clear();
            //getting numbner of days in selected month & year
            int noofdays = DateTime.DaysInMonth(Convert.ToInt32(ddlYear1.SelectedValue), Convert.ToInt32(ddlMonth1.SelectedValue));

            //Fill days
            for (int i = 1; i <= noofdays; i++)
            {
                ddlDay1.Items.Add(i.ToString());
            }
            ddlDay1.Items.FindByValue(System.DateTime.Now.Day.ToString()).Selected = true;// Set current date as selected
        }
        catch
        { }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {


            string sd1 = ddlDay1.Text + "/" + ddlMonth1.Text + "/" + ddlYear1.Text;
            string ed1 = ddlDay2.Text + "/" + ddlMonth2.Text + "/" + ddlYear2.Text;

            DateTimeFormatInfo StartDate = new DateTimeFormatInfo();
            StartDate.ShortDatePattern = "dd/MM/yyyy";
            StartDate.DateSeparator = "/";
            DateTime objDate = Convert.ToDateTime(sd1, StartDate);

            DateTimeFormatInfo EndDate = new DateTimeFormatInfo();
            EndDate.ShortDatePattern = "dd/MM/yyyy";
            EndDate.DateSeparator = "/";
            DateTime objDate2 = Convert.ToDateTime(ed1, StartDate);

            var da = new SqlDataAdapter();
            var ds = new DataSet();


            da = new SqlDataAdapter("SELECT Client.First_Name AS [Client Name], deposit.Amount, deposit.date,  deposit.Account_No FROM deposit INNER JOIN Client ON deposit.Client_Id = Client.Client_Id WHERE deposit.status = '1' AND Client.Client_Id = '" + fn + "' AND deposit.date BETWEEN '" + objDate + "' AND '" + objDate2 + "'", cn);
            ds = new DataSet();

            da.Fill(ds);
            GVClientBillList.DataSource = ds;
            GVClientBillList.DataBind();


            if (GVClientBillList.Rows.Count != 0)
            {
                ImageButton3.Visible = true;
                LblError.Visible = false;
            }
            else
            {
                LblError.Visible = true;
                LblError.Text = "No Record Found";
                ImageButton3.Visible = false;
            }


        }

        catch { }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
         try
        {
            //souce for reference INSE 6260 
            /////http://csharp-video-tutorials.blogspot.ca/2013/04/export-gridview-to-pdf-in-aspnet-part-58.html

            int columnsCount = GVClientBillList.HeaderRow.Cells.Count;

            PdfPTable pdfTable = new PdfPTable(columnsCount);


            foreach (TableCell gridViewHeaderCell in GVClientBillList.HeaderRow.Cells)
            {

                Font font = new Font();

                font.Color = new BaseColor(GVClientBillList.HeaderStyle.ForeColor);

                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));


                pdfCell.BackgroundColor = new BaseColor(GVClientBillList.HeaderStyle.BackColor);


                pdfTable.AddCell(pdfCell);
            }

            foreach (GridViewRow gridViewRow in GVClientBillList.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {

                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {
                        Font font = new Font();
                        font.Color = new BaseColor(GVClientBillList.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text, font));

                        pdfCell.BackgroundColor = new BaseColor(GVClientBillList.RowStyle.BackColor);

                        pdfTable.AddCell(pdfCell);
                    }
                }
            }
            Document pdfDocument = new Document(PageSize.A4, 10f, 10f, 10f, 10f);

            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            pdfDocument.Open();
            pdfDocument.Add(pdfTable);
            pdfDocument.Close();

            DateTime dt1 = DateTime.Now;
            string dt = dt1.ToShortDateString();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition", "attachment;filename=" + dt + "Statement.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            LblError.Visible = true;
            LblError.Text = ex.ToString();
        }
    
    }
}