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
public partial class AdminMenu : System.Web.UI.MasterPage
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, ln = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["a_id"] != null)
            {
                //   LblWelcome.Text += Session["c_id"].ToString();
                cmd.Connection = cn;
            }
            else
                Response.Redirect("AdminLogin.aspx");
        }
        catch
        { }




        try
        {
            cn.Open();



            string sid = Session["a_id"].ToString();

            string sql = "SELECT First_Name,Last_Name FROM Admin where Admin_Id=@id ";
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
            Session["a_id"] = null;
            Response.Redirect("AdminLogin.aspx");
        }
        catch
        { }
    }
    protected void ImgSettings_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            Response.Redirect("AdminSettings.aspx");
        }
        catch
        { }
    }
}
