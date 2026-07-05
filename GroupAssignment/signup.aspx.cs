using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GroupAssignment
{
    public partial class signup : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["success"] == "true")
            {
                lblSuccessMessage.Text = "✓ Account Created Successfully!";
                lblSuccessMessage.Visible = true;
                txtUsername.Text = "";
                txtEmail.Text = "";
                txtPassword1.Text = "";
                txtPassword2.Text = "";

                // Remove the query string from URL (optional - requires JavaScript)
                // ClientScript.RegisterStartupScript(this.GetType(), "removeQuery", 
                //    "window.history.replaceState({}, document.title, 'signup.aspx');", true);
            }
        }

        protected void UsernameExistsCV_Validation(object source, ServerValidateEventArgs args)
        {
            string username = args.Value.Trim();

            if (UsernameExists(username))
            {
                args.IsValid = false; // Username exists, validation fails
            }
            else
            {
                args.IsValid = true; // Username available, validation passes
            }
        }

        protected void EmailExistsCV_Validation(object source, ServerValidateEventArgs args)
        {
            string email = args.Value.Trim();
            
            if (EmailExists(email))
            {
                args.IsValid = false; // Username exists, validation fails
            }
            else
            {
                args.IsValid = true; // Username available, validation passes
            }
        }

        // Check if username exists in database
        private bool UsernameExists(string username)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM [userTable] WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error checking username: " + ex.Message);
                return false; // Assume username doesn't exist if error occurs
            }
        }

        // Check if email exists
        private bool EmailExists(string email)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM [userTable] WHERE email = @email";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@email", email);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error checking email: " + ex.Message);
                return false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Clear any previous error messages
            lblErrorMessage.Visible = false;
            lblSuccessMessage.Visible = false;
            lblErrorMessage.Text = "";
            lblSuccessMessage.Text = "";

            // Check if all validators pass (including custom validator)
            if (!Page.IsValid)
            {
                lblErrorMessage.Text = "Please correct the errors below.";
                lblErrorMessage.Visible = true;
                return;
            }

            // Get input values
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword1.Text;

            // Create user in database
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Insert query
                    string insertQuery = @"INSERT INTO [userTable] (username, password, email, role, dateRegistered, lastLogin) 
                                           VALUES (@username, @password, @email, @role, @dateRegistered, @lastLogin)";

                    SqlCommand cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@role", "Member");
                    cmd.Parameters.AddWithValue("@dateRegistered", DateTime.Now);
                    cmd.Parameters.AddWithValue("@lastLogin", DateTime.Now);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Response.Redirect("signup.aspx?success=true");
                    }
                    else
                    {
                        lblErrorMessage.Text = "Failed to create account. Please try again.";
                        lblErrorMessage.Visible = true;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                lblErrorMessage.Text = "Database error: " + sqlEx.Message;
                lblErrorMessage.Visible = true;
                System.Diagnostics.Debug.WriteLine("SQL Error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred: " + ex.Message;
                lblErrorMessage.Visible = true;
                System.Diagnostics.Debug.WriteLine("General Error: " + ex.Message);
            }
        }
    }
}