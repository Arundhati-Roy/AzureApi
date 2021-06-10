using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace AzureApi
{
    public partial class Form1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            con.ConnectionString= "Data Source=(LocalDB)\\RoyDB;Initial Catalog=AzureApi  DB;Integrated Security=True";
            con.Open();
            if(!Page.IsPostBack)
            {
                DataShow();
            }
        }

        [Obsolete]
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            string securepass = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.ToString(), "MD5");
            cmd.CommandText = "Insert into AzureTable (Name,Email,Password)values('"+txtName.Text.ToString()+"','"+txtEmail.Text.ToString()+"','"+txtPassword.Text.ToString()+"')";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            
            DataShow();
        }
        public void DataShow()
        {
            ds = new DataSet();
            cmd.CommandText = " Select * From AzureTable";
            cmd.Connection = con;
            sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            cmd.ExecuteNonQuery();
            GridView1.DataSource = ds;
            GridView1.DataBind();
            con.Close();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            cmd.CommandText = "Update AzureTable set Password='" + txtPassword.Text.ToString() + "',Email='" + txtEmail.Text.ToString() + "'where Name='" + txtName.Text.ToString() + "'";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            DataShow();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            cmd.CommandText = "Delete from AzureTable where Name='" + txtName.Text.ToString() + "'";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            DataShow();
        }
    }
}