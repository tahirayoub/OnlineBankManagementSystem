﻿using System;
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

public partial class Manager_ManagerMenu : System.Web.UI.Page
{
    string fn = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["m_id"] != null)
            {
                fn = Session["m_id"].ToString();
             
            }
            else
                Response.Redirect("ManagerLogin.aspx");


        }
        catch
        { }
    }
}