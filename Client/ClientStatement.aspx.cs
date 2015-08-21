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

public partial class Client_ClientStatement : System.Web.UI.Page
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


            da = new SqlDataAdapter("Select StatementId AS Date,Date,Title, Account,Credit,Debit,Total  from Statement where Client_Id = '" + fn + "' and Account = '"+an1+"' order by StatementId desc", cn);
            ds = new DataSet();

            da.Fill(ds);
            GVClientBillList.DataSource = ds;
            GVClientBillList.DataBind();

            if (GVClientBillList.Rows.Count != 0)
            {
                ImageButton4.Visible = true;
                LblError.Visible = false;
            }
            else
            {
                LblError.Visible = true;
                LblError.Text = "No Investment Record Found";
                ImageButton4.Visible = false;
            }

            this.GVClientBillList.Columns[0].Visible = false;


        }

        catch { }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GVClientBillList.Visible = true;
            an1 = DropDownList1.Text;
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
                    gvr.Cells[0].Visible = false;
                }
            }
        }
        catch
        { }
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
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
    
}