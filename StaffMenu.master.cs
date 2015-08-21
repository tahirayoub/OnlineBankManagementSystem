using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
public partial class StaffMenu : System.Web.UI.MasterPage
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, ln = "";
    string debitcard = "";
    string client_Id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["s_id"] != null)
            {
                //   LblWelcome.Text += Session["c_id"].ToString();
                cmd.Connection = cn;
            }
            else
                Response.Redirect("Staff_Login.aspx");
        }
        catch
        { }




        try
        {
            cn.Open();



            string sid = Session["s_id"].ToString();

            string sql = "SELECT First_Name,Last_Name FROM Staff where Staff_Id=@id ";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("id", sid);


            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    fn = dr["First_Name"].ToString();
                    ln = dr["Last_Name"].ToString();


                }
            }



            dr.Close();
            cn.Close();

            LblWelcome.Text = "Welcome :" + fn + " " + ln;

        }
        catch
        {


        }
        finally
        {
            cn.Close();

        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["s_id"] = null;
            Response.Redirect("Staff_Login.aspx");
        }
        catch
        { }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           
            Response.Redirect("StaffSettings.aspx");
        }
        catch
        { }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            cn.Open();



            if (TxtMasterbox.Text != "")
            {

                string sql = "SELECT Client_Id,Debit_Card_No FROM Debit_Card where Debit_Card_No=@dcd ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("dcd", TxtMasterbox.Text);


                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                       
                        client_Id = dr["Client_Id"].ToString();
                        debitcard = dr["Debit_Card_No"].ToString();
                    }
                }



                dr.Close();
                cn.Close();

                if (debitcard == TxtMasterbox.Text)
                {
                    Session["s_c_id"] = client_Id;
                    Session["s_debit_id"] = debitcard;
                    LblMasterError.Visible = true;
                    LblMasterError.Text = "Client Id " + Session["s_c_id"].ToString();
                    TxtMasterbox.Enabled = false;
                    Response.Redirect(Request.RawUrl);
                
                }
                else
                {
                    LblMasterError.Visible = true;
                    LblMasterError.Text = "Please Enter the Valid Client Debit Card Number";
                }
            }
            else
            {
                LblMasterError.Visible = true;
                LblMasterError.Text = "Please Enter the Valid Client Debit Card Number";
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
    
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ImageButton3_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            TxtMasterbox.Enabled = true;
            TxtMasterbox.Text = "";
            Session["s_c_id"] = null;
            Session["s_debit_id"] = null;
            LblMasterError.Visible = false;
            Response.Redirect("StaffMenu.aspx", false);

        }
        catch
        { }
    }
    public TextBox TextMasterbox
    {

        get
        {
            TxtMasterbox.Enabled = false;
            return this.TxtMasterbox;
        }
    }
}
