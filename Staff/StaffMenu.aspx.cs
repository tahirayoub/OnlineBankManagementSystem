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



public partial class Staff_StaffMenu : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, id,sum = "";

    double sm = 0.0;
    string latestamount = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (Session["s_id"] != null)
            {

                fn = Session["s_id"].ToString();
                cmd.Connection = cn;

                if (Session["s_c_id"] != null)
                {
                    LblClientId.Text = Session["s_c_id"].ToString();
                    TextBox t = (TextBox)Master.FindControl("TxtMasterbox");

                    t.Text = Session["s_debit_id"].ToString();
                    t.Enabled = false;

                 
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

                depositchk();

            }
        }
        catch
        { }

    }
    



    void depositchk()
    {
        try
        {
            var da = new SqlDataAdapter();
            var ds = new DataSet();

            
            da = new SqlDataAdapter("SELECT Id,cheque from deposit where status = '0'", cn);
            ds = new DataSet();

            da.Fill(ds);
            GVacct.DataSource = ds;
            GVacct.DataBind();
            GVacct.RowStyle.Height = 150;
            Label6.Visible = false;
            Label7.Visible = false;
            Label8.Visible = false;
            Label9.Visible = false;
            Label10.Visible = false;
            Label11.Visible = false;
            LblAccountNo.Visible = false;
            LblClientId.Visible = false;
            LblDate.Visible = false;
            LblTime.Visible = false;
            LblAccountNo.Visible = false;
            LblStatus.Visible = false;
            ImageButton2.Visible = false;
            ImgDelete.Visible = false;
            ImgUpdate.Visible = false;
            TxtAmount.Visible = false;

        }

        catch { }

    }
    protected void GVacct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            var row = GVacct.SelectedRow;

            LblId.Text = ((Label)row.Cells[1].Controls[1]).Text;

            id = LblId.Text;

            getclientinfo();
        }
        catch (Exception ex)
        {
          
        }
    }

    void getclientinfo()
    {
        try
        {
            cn.Open();



            string sql = "SELECT Client_Id,cheque,Amount,Date,time,Account_No, Status from deposit where Id=@id ";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("id", id);


            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    LblClientId.Text = dr["Client_Id"].ToString();
                    ImageButton2.ImageUrl = dr["cheque"].ToString();
                    TxtAmount.Text = dr["Amount"].ToString();
                    LblDate.Text = dr["date"].ToString();
                    LblTime.Text = dr["time"].ToString();
                    LblAccountNo.Text = dr["Account_No"].ToString();
                    LblStatus.Text = dr["status"].ToString();



                }
            }

            Label6.Visible = true;
            Label7.Visible = true;
            Label8.Visible = true;
            Label9.Visible = true;
            Label10.Visible = true;
            Label11.Visible = true;
            LblAccountNo.Visible = true;
            LblClientId.Visible = true;
            LblDate.Visible = true;
            LblTime.Visible = true;
            LblAccountNo.Visible = true;
            LblStatus.Visible = true;
            ImageButton2.Visible = true;
            ImgDelete.Visible = true;
            ImgUpdate.Visible = true;
            TxtAmount.Visible = true;

            if (LblStatus.Text == "0")
            {
                LblStatus.Text = "Pending";
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
    protected void GVacct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVacct.PageIndex = e.NewPageIndex;
        depositchk();
    }
    protected void ImgUpdate0_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            cn.Close();
            

            int i = 1;

            string sql2 = "UPDATE deposit SET Status=@cc where Id=@id";
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;
            cmd2.Parameters.AddWithValue("id", LblId.Text);
            cmd2.Parameters.AddWithValue("cc", i);
            cn.Open();
            cmd2.ExecuteNonQuery();

            cmd2.Clone();
            cn.Close();



            updateclientAccout();
            insertdata2();



        }
        catch
        {


        }
        finally
        {
            cn.Close();

        }
    }
    void updateclientAccout()
    {
        try
        {
            cn.Close();
            cn.Open();

            int i = int.Parse(LblAccountNo.Text);

            string sql = "Select sum(Amount) as Amount from Account where Account_No=@No";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("No", i);


            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    sum = dr["Amount"].ToString();


                }
            }
            dr.Close();
            cn.Close();
            Label6.Visible = true;
            Label7.Visible = true;
            Label8.Visible = true;
            Label9.Visible = true;
            Label10.Visible = true;
            Label11.Visible = true;
            LblAccountNo.Visible = true;
            LblClientId.Visible = true;
            LblDate.Visible = true;
            LblTime.Visible = true;
            LblAccountNo.Visible = true;
            LblStatus.Visible = true;
            ImageButton2.Visible = true;
            ImgDelete.Visible = true;
            ImgUpdate.Visible = true;
            TxtAmount.Visible = true;
          if(sum != null && TxtAmount.Text != null)
          {
              sm = double.Parse(sum) + double.Parse(TxtAmount.Text);

              updateaccountamount();

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
    void insertdata2()
    {

        try
        {


            getlatestamount();
            DateTime statement_date = DateTime.Now;

           string title =  "Deposite Cash";
           double d = 0.0;
            cn.Open();
            SqlCommand cm = new SqlCommand(
            "INSERT INTO Statement (Date,Title, Account, credit,debit , Total, Client_Id) VALUES(@Date,@Title, @Account, @credit,@debit, @Total, @Client_Id)", cn);
            cm.Parameters.Add("@Date", statement_date);
            cm.Parameters.Add("@Title", title);
            cm.Parameters.Add("@Account", LblAccountNo.Text);
            cm.Parameters.Add("@credit", TxtAmount.Text);
            cm.Parameters.Add("@debit", d);
            cm.Parameters.Add("@Total", latestamount);
            cm.Parameters.Add("@Client_Id", LblClientId.Text);


            cm.ExecuteNonQuery();

            cm.Clone();

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

    void getlatestamount()
    {

        try
        {
            cn.Close();
            cn.Open();

            int i = int.Parse(LblAccountNo.Text);

            string sql = "Select sum(Amount) as Amount from Account where Account_No=@No";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("No", i);


            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    latestamount = dr["Amount"].ToString();


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
    void updateaccountamount()
    {
        try
        {
            string sql2 = "UPDATE Account SET Amount=@amount where Account_No=@No";
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;
            cmd2.Parameters.AddWithValue("No", LblAccountNo.Text);
            cmd2.Parameters.AddWithValue("amount", sm);
            cn.Open();
            cmd2.ExecuteNonQuery();

            cmd2.Clone();
            cn.Close();
            depositchk();

        }
        catch
        {


        }
        finally
        {
            cn.Close();

        }
    }
    protected void ImgDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string sql2 = "Delete deposit where id=@id";
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;

            cmd2.Parameters.AddWithValue("id", LblId.Text);
            cn.Open();
            cmd2.ExecuteNonQuery();

            cmd2.Clone();
            cn.Close();
            depositchk();

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