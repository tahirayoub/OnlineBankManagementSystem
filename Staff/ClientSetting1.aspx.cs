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

public partial class Staff_ClientSetting1 : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, id, sum,cid = "";

    double sm = 0.0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            if (Session["s_id"] != null)
            {

                fn = Session["s_id"].ToString();
                cmd.Connection = cn;
                LblClientId.Text = Session["s_c_id"].ToString();




            }
            else
                Response.Redirect("Staff_Login.aspx");
        }
        catch
        { }

    }
}