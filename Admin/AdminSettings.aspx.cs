using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

using System.Net.Sockets;
using System.IO;
using System.Globalization;


public partial class Admin_AdminSettings : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["a_id"] != null)
            {
                fn = Session["a_id"].ToString();
                cmd.Connection = cn;
                if (!Page.IsPostBack)
                { getadmininfo(); }
            }
            else
                Response.Redirect("AdminLogin.aspx");

        }
        catch
        { }
    }
    void getadmininfo()
    {
        try
        {
            cn.Open();
            string sql = "Select First_Name, Last_Name, Occupation, Designation, Monthly_Salary,Email, Address, Country, City, PostalCode, Phone_Number, Password, SQ1, Answer1, SQ2, Answer2, SQ3, Answer3 from Admin where Admin_Id=@a_id";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@a_id", fn);

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


                   
                    Lblpwd.Text = dr["Password"].ToString();

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
    protected void ImgSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (EncryptPasswrod(TxtOldPwd.Text) != "")
            {
                if (EncryptPasswrod(TxtOldPwd.Text) != Lblpwd.Text)
                {
                    LblMsgPwd.Visible = true;
                    LblMsgPwd.Text = "Please Enter right Password";
                    return;

                }
                else
                {
                    ImageButton1.Visible = true;
                    LblMsgPwd.Visible = true;
                    LblMsgPwd.Text = "Now You can Update Current Client Settings";

                }
            }
        }
        catch
        { }
    }
    private string EncryptPasswrod(string password)
    {
        System.Security.Cryptography.SHA1 sha = System.Security.Cryptography.SHA1.Create();
        string hashed = System.Convert.ToBase64String(sha.ComputeHash(System.Text.UnicodeEncoding.Unicode.GetBytes(password)));
        return hashed.Length > 49 ? hashed.Substring(0, 49) : hashed;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

           

            cn.Open();
            string sql = "UPDATE Admin SET First_Name=@First_Name, Last_Name=@Last_Name, Occupation=@Occupation, Designation=@Designation, Monthly_Salary=@Monthly_Salary,Email=@Email, Address=@Address, Country=@Country, City=@City, PostalCode=@PostalCode, Phone_Number=@Phone_Number, Password=@Password, SQ1=@SQ1, Answer1=@Answer1, SQ2=@SQ2, Answer2=@Answer2, SQ3=@SQ3, Answer3=@Answer3 where Admin_Id=@Aid";
            SqlCommand cm = new SqlCommand();
            cm.Connection = cn;
            cm.CommandType = CommandType.Text;
            cm.CommandText = sql;

            cm.Parameters.AddWithValue("@Aid", fn);

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

            cm.Parameters.Add("@Password", EncryptPasswrod(TxtPassword.Text));
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
            getadmininfo();
        }
        catch
        { }

        finally
        {
            cn.Close();
        }
    }
}