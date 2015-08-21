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


public partial class Client_ClientCreditCardRequest : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, cid1, an1 = "";
    string status = "";
    DateTime dt = DateTime.Now.Date;
    double t1 = 0.0;
    double amount = 0.0;
    double chk_amount = 0.0;
    double interest_amount = 0.0;
    string s1 = "";
    string s2 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["c_id"] != null)
            {

                fn = Session["c_id"].ToString();
                cmd.Connection = cn;
                LblDate.Text = dt.ToString("dd/MM/yyyy");
                try
                {

                    cn.Open();

                    string sql = "Select Status from CreditRequest where Client_Id=@c_id";
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = cn;
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = sql;
                    cmd2.Parameters.AddWithValue("c_id", fn);


                    dr = cmd2.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            status = dr["Status"].ToString();
                        }
                    }


                    dr.Close();
                    cn.Close();

                    if (status == "0")
                    {
                        LblError.Visible = true;
                        LblError.Text = "your Client Request is already enteres";
                        ImageButton2.Visible = false;
                        return;
                    }
                    else
                        ImageButton2.Visible = true;
                }
                catch
                {
                }
                finally
                {
                    cn.Close();

                }
            }
            else
                Response.Redirect("LoginClient.aspx");
        }
        catch
        { }

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {


        insertclientcredit();

          
    }

        void insertclientcredit()
        {
            try{
          if (TextBox1.Text != "")
            {

                cn.Close();
                cn.Open();


                /////////////////////////

                int a = 0;
                SqlCommand cm = new SqlCommand(
                  "INSERT INTO CreditRequest (Client_Id, Date,Status, Description) VALUES(@Client_Id,@Date,@Status,@Description)", cn);
                cm.Parameters.Add("@Client_Id", fn);
                
                cm.Parameters.Add("@Date", dt);
                
                cm.Parameters.Add("@Status", a);
                
                cm.Parameters.Add("@Description", TextBox1.Text);


                cm.ExecuteNonQuery();

                cm.Clone();


                cn.Close();

                LblError.Visible = true;
               // updateamount();
                LblError.Text = "Client credit card request send  Successfully";
                TextBox1.Text = "";

            }

        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
        }
        finally
        {
            cn.Close();

        }
        }
       
}