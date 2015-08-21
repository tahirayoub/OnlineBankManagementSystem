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



public partial class Client_MainMenu : System.Web.UI.Page
{
    

    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
   
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn = "";




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

                acountgv();
                creditcc();
                summary1();
                summary2();
            }
        }
        catch
        { }




    }

    /////////////////////////////////////////


    void summary1()
    {

        try
        {
            cn.Open();




            string sql = "SELECT  AccountType.Type AS Type,  Account.Amount AS Amount FROM Account INNER JOIN AccountType ON Account.Account_Type_Id = AccountType.Account_Type_Id where Client_Id=@cid ";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("cid", fn);



            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //    LblACCAmount.Text = dr["Amount"].ToString();


                    Label dynamicLabel = new Label();
                    div1.Controls.Add(dynamicLabel);
                    dynamicLabel.Text = dr["Type"].ToString() + "<br/>";
                    dynamicLabel.Visible = true;


                    Label dynamicLabel2 = new Label();

                    div2.Controls.Add(dynamicLabel2);
                    dynamicLabel2.Text = dr["Amount"].ToString() + "<br/>";
                    dynamicLabel2.Visible = true;




                }
            }


            summery3();

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
    /// <summary>
    /// ///////////
    /// </summary>

    void summery3()
    {
        try
        {
            cn.Close();
            cn.Open();




            string sql = "SELECT  SUM(Amount) AS TotalAmount from Account where Client_Id=@cid ";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("cid", fn);



            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    LblAccountTotal.Text = dr["TotalAmount"].ToString();


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

    /// <summary>
    /// //////////////
    /// </summary>
    void summary2()
    {

       
    }


    /// <summary>
    /// ////////////////////////////////////
    /// </summary>
    void acountgv()
    {
        try
        {

            var da = new SqlDataAdapter();
            var ds = new DataSet();

            da = new SqlDataAdapter("SELECT AccountType.Type AS [Account Type], Account.Account_No AS [Account Number], Account.Bank_Transit_No AS [Transit Number], Account.Amount FROM Account INNER JOIN AccountType ON Account.Account_Type_Id = AccountType.Account_Type_Id where Client_Id = '" + fn + "'", cn);
            ds = new DataSet();

            da.Fill(ds);
            GVacct.DataSource = ds;
            GVacct.DataBind();
            GVacct.RowStyle.Height = 150;

        }

        catch { }
    }


    /// <summary>
    /// /////////////////////////////////////////////
    /// </summary>
    void creditcc()
    {

        try
        {

            var da1 = new SqlDataAdapter();
            var ds1 = new DataSet();

            da1 = new SqlDataAdapter("SELECT Card_No AS [Credit Card Number], Bank_Transit_No AS [Bank Transit Number], Limit as [Total Card Limit] from CreditAccount where Client_Id = '" + fn + "'", cn);
            ds1 = new DataSet();

            da1.Fill(ds1);

            GVCreditCard.DataSource = ds1;

            GVCreditCard.DataBind();
            if (GVCreditCard.Rows.Count != 0)
            {
                
                GVCreditCard.RowStyle.Height = 150;
            }
            else
            {
                LblCCmessage.Visible = true;
                LblCCmessage.Text = "No Credit Card information Available";

            }

        }

        catch { }
    }
    protected void GVAccount_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

  
    protected void Button1_Click(object sender, EventArgs e)
    {
      
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //souce for reference INSE 6260 
            /////http://csharp-video-tutorials.blogspot.ca/2013/04/export-gridview-to-pdf-in-aspnet-part-58.html

            int columnsCount = GVacct.HeaderRow.Cells.Count;

            PdfPTable pdfTable = new PdfPTable(columnsCount);


            foreach (TableCell gridViewHeaderCell in GVacct.HeaderRow.Cells)
            {

                Font font = new Font();

                font.Color = new BaseColor(GVacct.HeaderStyle.ForeColor);

                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));


                pdfCell.BackgroundColor = new BaseColor(GVacct.HeaderStyle.BackColor);


                pdfTable.AddCell(pdfCell);
            }

            foreach (GridViewRow gridViewRow in GVacct.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {

                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {
                        Font font = new Font();
                        font.Color = new BaseColor(GVacct.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text, font));

                        pdfCell.BackgroundColor = new BaseColor(GVacct.RowStyle.BackColor);

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
        catch
        { }
    }
    protected void GVacct_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GVCreditCard_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}