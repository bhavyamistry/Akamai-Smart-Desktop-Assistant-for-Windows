using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["action"] != null)
        {    
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Assistance"].ConnectionString);
            Label1.Text = Session["action"].ToString();
            string Status = Request.QueryString["status"];
            if(Status != null)
            {
                status.Text = Status;
            }
            string sel = "Select * from DATA";
            SqlDataAdapter da = new SqlDataAdapter(sel, con);
            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);
            string count = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count == 0)
            {
                status.Text = "";
            }
        }
        else
        {
            Label1.Text = "";
        }
    }

}