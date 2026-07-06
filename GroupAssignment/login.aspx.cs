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
    public partial class login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["success"] == "true")
            {
                txtUsername.Text = "";
                txtPassword.Text = "";

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
            lblErrorMessage.Text = "";

            if (!Page.IsValid)
            {
                lblErrorMessage.Text = "Please enter both username and password.";
                lblErrorMessage.Visible = true;
                return;
            }

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (AuthenticateUser(username, password))
            {
                Session["Username"] = username;
                Session["UserID"] = GetUserID(username);
                Session["Role"] = GetUserRole(username);

                UpdateLastLogin(username);

                string role = Session["Role"]?.ToString();

                if (role == "Admin")
                {
                    Response.Redirect("admin_dashboard.aspx");
                }
                else if (role == "Member")
                {
                    Response.Redirect("member_dashboard.aspx");
                }
                else
                {
                    Response.Redirect("guest_home.aspx");
                }
            }
            else
            {
                lblErrorMessage.Text = "Invalid username or password. Please try again.";
                lblErrorMessage.Visible = true;
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM [userTable] WHERE username = @username AND password = @password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Login Error: " + ex.Message);
                return false;
            }
        }

        private int GetUserID(string username)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT userId FROM [userTable] WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        private string GetUserRole(string username)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT role FROM [userTable] WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);

                    object result = cmd.ExecuteScalar();


                    if (result != null && !string.IsNullOrEmpty(result.ToString()))
                    {
                        return result.ToString();
                    }

                    return "Guest";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GetUserRole Error: " + ex.Message);
                return "Guest";
            }
        }

        private void UpdateLastLogin(string username)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE [userTable] SET lastLogin = @lastLogin WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@lastLogin", DateTime.Now);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Update LastLogin Error: " + ex.Message);
            }
        }
    }
}