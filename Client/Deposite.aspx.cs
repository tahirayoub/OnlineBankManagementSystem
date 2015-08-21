using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Configuration;

public partial class Client_Deposite : System.Web.UI.Page
{
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bankDBConnectionString1"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string fn, ln = "";

    DateTime dat = DateTime.Now;
    TimeSpan tim = DateTime.Now.TimeOfDay;
    string name = "";

    Random random = new Random();
    int randomNumber = 0;
    int pp = 0;
    int check = 0;
    string p1 = "";
    
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
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                if (tbox1.Text != "" && tbox2.Text != "")
                {


                    double t1 = double.Parse(tbox1.Text);

                    double t2 = double.Parse(tbox2.Text);

                    if (t1 == t2)
                    {
                        if (t1 <= 0.0)
                        {
                            LblError.Visible = true;
                            LblError.Text = "Please enter positive amount of money";
                            tbox1.Focus();
                            return;
                        }
                        if (t2 <= 0.0)
                        {
                            LblError.Visible = true;
                            LblError.Text = "Please enter positive amount of money";
                            tbox2.Focus();
                            return;
                        }
                        else
                        {
                            LblError.Visible = false;
                            insertdata();
                           
                        }
                    }
                    else
                    {
                        LblError.Visible = true;
                        LblError.Text = "Please enter same amount";
                    }
                }
                else
                {
                    LblError.Visible = true;
                    LblError.Text = "Please enter Amount";
                }
            }
            else
            {
                LblError.Visible = true;
                LblError.Text = "Please upload cheque Image";
            }
        }
        catch
        { }
    }

    void insertdata()
    {

        try
        {


            fileupl();
            int accont_no = int.Parse(DropDownList1.Text);
            int status = 0;
            cn.Open();
            SqlCommand cm = new SqlCommand(
            "INSERT INTO deposit (Client_Id, Amount, date, time, Account_No, cheque,number,status ) VALUES(@Client_Id, @Amount, @date, @time,@Account_No, @cheque, @number,@status)", cn);
            cm.Parameters.Add("@Client_Id", fn);
            cm.Parameters.Add("@Amount", tbox1.Text);
            cm.Parameters.Add("@date", dat);
            cm.Parameters.Add("@time", tim);
            cm.Parameters.Add("@Account_No", accont_no);
            cm.Parameters.Add("@cheque", name);
            cm.Parameters.Add("@number", randomNumber);
            cm.Parameters.Add("@status", status);



            cm.ExecuteNonQuery();

            cm.Clone();
          
            cn.Close();
                   LblError.Visible = true;
                   LblError.Text ="Amount Deposit Request Sent Successfully!";
                   tbox1.Text = "";
                   tbox2.Text = "";


        }




        catch
        {


        }
        finally
        {
            cn.Close();

        }
    }

   

    void fileupl()
    {
        try
        {

            ranno();
            string path = Server.MapPath("../Thumbnail/");
            if (FileUpload1.HasFile)
            {
                string ext = Path.GetExtension(FileUpload1.FileName);

                string fname = randomNumber + FileUpload1.FileName;

                FileUpload1.SaveAs(path + fname);
                
                //System.Drawing.Image img1 = System.Drawing.Image.FromFile(MapPath("~/Thumbnail/") + fname);
                System.Drawing.Image img1 = System.Drawing.Image.FromFile(MapPath("~/Thumbnail/") + fname);
                System.Drawing.Image bmp2 = img1.GetThumbnailImage(500, 200, null, IntPtr.Zero);
                System.Drawing.Image bmp3 = img1.GetThumbnailImage(700, 350, null, IntPtr.Zero);

              

                bmp2.Save(MapPath("../TT/") + fname);
                bmp3.Save(MapPath("../ta/") + fname);
              
                name = "~/ta/" + fname;
            }
            else
            {
                return;
            }
        }
        catch
        { }
    }


    void ranno()
    { 
        try
        {
                   cn.Close();

                  while (check == 0)
                  {
                      Random random = new Random();
                      randomNumber = random.Next(10, 100000);

                   

                      cn.Open();
                      string sql = "Select number from deposit Where number = @rn";
                      SqlCommand cmd = new SqlCommand();
                      cmd.Connection = cn;
                      cmd.CommandType = CommandType.Text;
                      cmd.CommandText = sql;
                      cmd.Parameters.AddWithValue("rn", randomNumber);



                     dr = cmd.ExecuteReader();
                     if (dr.HasRows)
                     {
                         while (dr.Read())
                         {

                             p1 = dr["number"].ToString();
                         }
                     }
                      dr.Close();
                      cn.Close();

                      string rn = Convert.ToString(randomNumber);

                      if (p1 == rn)
                          w1();

                      if (pp == Convert.ToInt64(randomNumber))
                      {
                          check = 0;
                      }
                      else
                      { check = 1; }
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
    void w1()
    
    {

        pp = random.Next(10, 100000);
       
    }
}