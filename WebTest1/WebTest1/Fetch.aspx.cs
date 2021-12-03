using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Fetch : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string s = Request.QueryString["type"];
        string sr = s.Trim('\'');

        string[] ssizes = sr.Split(' ', '\t');

        string date = DateTime.Now.ToString("dd-MM-yyyy");
        string time = DateTime.Now.ToString("hh:mm:ss tt");

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Assistance"].ConnectionString);
        string sel = "Select * from DATA";
        SqlDataAdapter da = new SqlDataAdapter(sel, con);
        con.Open();
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        string count = ds.Tables[0].Rows.Count.ToString();
        if (ds.Tables[0].Rows.Count > 0)
        {
            string id = ds.Tables[0].Rows[0][0].ToString();
            string status = ds.Tables[0].Rows[0][4].ToString();
            string data = ds.Tables[0].Rows[0][1].ToString();
            if(status != "")
            {
                string ins = "update Data set data = '" + data + "  " + sr + "' where id = '" + id + "'";
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Assistance"].ConnectionString);
                SqlCommand cmd = new SqlCommand(ins, con1);
                con1.Open();
                cmd.ExecuteNonQuery();
                con1.Close();
                Session["action"] = "added";
                Response.Redirect("Test.aspx?status=done");
            }
            else
            {
                string del = "delete from Data";
                SqlCommand cmd = new SqlCommand(del, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                string ins = "insert into Data (data, date, time) values ('" + sr + "', '" + date + "', '" + time + "')";
                SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["Assistance"].ConnectionString);
                SqlCommand cmd1 = new SqlCommand(ins, con3);
                con3.Open();
                cmd1.ExecuteNonQuery();
                con3.Close();
                Session["action"] = "added";
                Response.Redirect("Test.aspx");
            }
        }
        else
        {
            if (ssizes[0] == "write" || ssizes[0] == "right")
            {
                string ins = "insert into Data (data, date, time, status) values ('" + sr + "', '" + date + "', '" + time + "', '1')";
                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Assistance"].ConnectionString);
                SqlCommand cmd = new SqlCommand(ins, con2);
                con2.Open();
                cmd.ExecuteNonQuery();
                con2.Close();
                Session["action"] = "added";
                Response.Redirect("Test.aspx?status=done");
            }
            else
            {
                string ins = "insert into Data (data, date, time) values ('" + sr + "', '" + date + "', '" + time + "')";
                SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["Assistance"].ConnectionString);
                SqlCommand cmd = new SqlCommand(ins, con3);
                con3.Open();
                cmd.ExecuteNonQuery();
                con3.Close();
                Session["action"] = "added";
               Response.Redirect("Test.aspx");
            }
           
        }
    }
}