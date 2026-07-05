using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace GroupAssignment
{
    public partial class member_create_list : Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtListName.Text))
            {
                lblError.Text = "Please enter a list name.";
                lblError.Visible = true;
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            string listName = txtListName.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string insertQuery = @"INSERT INTO studyListTable (userId, cardId, listName) 
                                          VALUES (@userId, NULL, @listName)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@listName", listName);
                        cmd.ExecuteNonQuery();
                    }
                }

                lblMessage.Text = $"Study list '{listName}' created successfully!";
                lblMessage.Visible = true;
                lblError.Visible = false;
                txtListName.Text = "";
            }
            catch (Exception ex)
            {
                lblError.Text = "Error creating study list: " + ex.Message;
                lblError.Visible = true;
            }
        }
    }
}