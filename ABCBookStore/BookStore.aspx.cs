using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ABCBookStore
{
    public partial class BookStore : System.Web.UI.Page
    {
        String conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\BookStoreDB.mdf;Integrated Security=True";
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
                //open connection
                cmd.Connection = conn;

                //add values into database
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO BookInfo(Title,Author,ISBN,PublishDate,Publisher,Category,Pages,Price) Values (@title,@author,@ISBN,@PublishDate,@Publisher,@Category,@Pages,@Price)";
                
                //values
                cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@author", txtAuthor.Text);
                cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                cmd.Parameters.AddWithValue("@PublishDate", txtPublishDate.Text);
                cmd.Parameters.AddWithValue("@Publisher", ddPublisher.Text);
                cmd.Parameters.AddWithValue("@Category", ddCategory.Text);
                cmd.Parameters.AddWithValue("@Pages", txtPages.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);

                int numBookAdded = cmd.ExecuteNonQuery();

                if (numBookAdded == 1)
                {
                    //update table
                    GridView1.DataBind();   
                }
                else
                {
                    //Error notification
                }
            }
            conn.Close();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Refresh Table
            GridView1.DataSourceID = null;
            GridView1.DataSource = SqlDataSource1;
            GridView1.DataBind();
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            //empty fields
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //search 
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "searchLibrary";
                cmd.CommandType = CommandType.StoredProcedure;

                if (ddSearch.Text == "Title")
                {
                    if (txtSearch.Text != "")
                    {
                        SqlParameter param = new SqlParameter("@Title", txtSearch.Text);
                        cmd.Parameters.Add(param);
                    }          
                }

               if (ddSearch.Text == "Price")
                {
                    if (txtSearch.Text != "")
                    {
                        SqlParameter param = new SqlParameter("@Price", txtSearch.Text);
                        cmd.Parameters.Add(param);
                    }
                }
                if (ddSearch.Text == "Category")
                {
                    if (txtSearch.Text != "")
                    {
                        SqlParameter param = new SqlParameter("@Category", txtSearch.Text);
                        cmd.Parameters.Add(param);
                    }
                }

                SqlDataReader rdr = cmd.ExecuteReader();
                GridView1.DataSourceID = null;
                GridView1.DataSource = rdr;
                GridView1.DataBind();      
            }
            conn.Close();
        }

        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GridView1.EditIndex = 1;
        //}
    }
}