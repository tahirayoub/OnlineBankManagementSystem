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

public partial class Manager_ManagerStaffUpdate2 : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string at_id = "";
    string cd = "";
    string pn, pn1 = "";
    DateTime myDateTime = DateTime.Now;
    DateTime myDateTime2 = DateTime.Now;
    DateTime ed = DateTime.Now;
    string bank_Id;

    string pwd = "";
    Random random = new Random();
    int randomNumber = 0;
    int pp = 0;
    int check = 0;
    string fn = "";

    string st = "";
    string dob = "";
    string s_Id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["m_id"] != null)
            {

                fn = Session["m_id"].ToString();

                s_Id = Session["manager_staff_sd"].ToString();
                cmd.Connection = cn;

                if (!Page.IsPostBack)
                { getManagerinfo(); }
              
            }
            else
                Response.Redirect("ManagerLogin.aspx");
        }
        catch
        { }
        try
        {

            if (!IsPostBack)
            {


                getManagerinfo();

            }
        }
        catch
        { }

    }
    void getManagerinfo()
    {
        try
        {
            cn.Open();
            string sql = "Select First_Name, Last_Name, Occupation, Designation, Monthly_Salary,Email, Address, Country, City, PostalCode, Phone_Number, SQ1, Answer1, SQ2, Answer2, SQ3, Answer3 from Staff where Staff_Id=@s_Id";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@s_Id", s_Id);

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    TxtFirstName.Text = dr["First_Name"].ToString();
                    TxtLastName.Text = dr["Last_Name"].ToString();



                    TxtOccupation.Text = dr["Occupation"].ToString();
                    TxtDesignation.Text = dr["Designation"].ToString();

                    TxtSalary.Text = dr["Monthly_Salary"].ToString();

                    TxtEmail.Text = dr["Email"].ToString();

                    TxtAddress.Text = dr["Address"].ToString();
                    TxtCountry.Text = dr["Country"].ToString();
                    TxtCity.Text = dr["City"].ToString();
                    TxtPostalCode.Text = dr["PostalCode"].ToString();

                    TxtPhoneNo.Text = dr["Phone_Number"].ToString();



                

                    TxtQ1.Text = dr["SQ1"].ToString();
                    TxtA1.Text = dr["Answer1"].ToString();

                    TxtQ2.Text = dr["SQ2"].ToString();
                    TxtA2.Text = dr["Answer2"].ToString();

                    TxtQ3.Text = dr["SQ3"].ToString();
                    TxtA3.Text = dr["Answer3"].ToString();


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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {


            cn.Open();
            string sql = "UPDATE Staff SET First_Name=@First_Name, Last_Name=@Last_Name, Occupation=@Occupation, Designation=@Designation, Monthly_Salary=@Monthly_Salary,Email=@Email, Address=@Address, Country=@Country, City=@City, PostalCode=@PostalCode, Phone_Number=@Phone_Number,SQ1=@SQ1, Answer1=@Answer1, SQ2=@SQ2, Answer2=@Answer2, SQ3=@SQ3, Answer3=@Answer3 where Staff_Id=@Mid";
            SqlCommand cm = new SqlCommand();
            cm.Connection = cn;
            cm.CommandType = CommandType.Text;
            cm.CommandText = sql;

            cm.Parameters.AddWithValue("@Mid", s_Id);

            cm.Parameters.Add("@First_Name", TxtFirstName.Text);
            cm.Parameters.Add("@Last_Name", TxtLastName.Text);

            cm.Parameters.Add("@Occupation", TxtOccupation.Text);
            cm.Parameters.Add("@Designation", TxtDesignation.Text);
            cm.Parameters.Add("@Monthly_Salary", TxtSalary.Text);
            cm.Parameters.Add("@Email", TxtEmail.Text);
            cm.Parameters.Add("@Address", TxtAddress.Text);
            cm.Parameters.Add("@Country", TxtCountry.Text);
            cm.Parameters.Add("@City", TxtCity.Text);
            cm.Parameters.Add("@PostalCode", TxtPostalCode.Text);
            cm.Parameters.Add("@Phone_Number", TxtPhoneNo.Text);

            
            cm.Parameters.Add("@SQ1", TxtQ1.Text);
            cm.Parameters.Add("@Answer1", TxtA1.Text);
            cm.Parameters.Add("@SQ2", TxtQ2.Text);
            cm.Parameters.Add("@Answer2", TxtA2.Text);
            cm.Parameters.Add("@SQ3", TxtQ3.Text);
            cm.Parameters.Add("@Answer3", TxtA3.Text);


            cm.ExecuteNonQuery();

            cm.Clone();
            cn.Close();

            LblError.Visible = true;
            LblError.Text = "Information Updated Successfully";
            getManagerinfo();
        }
        catch
        { }

        finally
        {
            cn.Close();
        }
    }
}