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
using System.Web.Security;
public partial class ClientMasterPage : System.Web.UI.MasterPage
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, ln = "";
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
            try
            { 
                if (Session["c_id"] != null)
                {
                  

                    cmd.Connection = cn;
                }
                else
                {

                    FormsAuthentication.SignOut();
                    Response.Redirect("LoginClient.aspx");
                }
            }
            catch
            { }

            
    

        try
        {
            cn.Open();



                 string cid = Session["c_id"].ToString() ;
                string sql = "SELECT First_Name,Last_Name FROM Client where Client_Id=@id ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("id", cid);


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

                LblWelcome.Text  = "Welcome :" + fn + " " + ln;

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
            Session["c_id"] = null;
           
                HttpCookie myCookie = new HttpCookie("UserSettings");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
                       FormsAuthentication.SignOut();
            Response.Redirect("LoginClient.aspx");
        }
        catch
        { }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("ClientSettings.aspx");
        }
        catch
        { }
    }
}
