using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;

namespace GroupAssignment
{
    public partial class member_profile : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        int currentUserId = 0;
        string currentUsername = "";
        string currentEmail = "";
        string currentRole = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("login.aspx");
            }

            currentUserId = Convert.ToInt32(Session["UserId"]);

            if (!IsPostBack)
            {
                LoadUserData();
            }
        }

        private void LoadUserData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT username, email, role FROM userTable WHERE userId = @userId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", currentUserId);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            currentUsername = reader["username"].ToString();
                            currentEmail = reader["email"].ToString();
                            currentRole = reader["role"].ToString();

                            txtUsername.Text = currentUsername;
                            txtEmail.Text = currentEmail;
                            txtRole.Text = currentRole;
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error loading user data: " + ex.Message);
            }
        }

        protected void UsernameChange(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string newUsername = args.Value;

            // If username hasn't changed, it's valid
            if (newUsername == currentUsername)
            {
                args.IsValid = true;
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM userTable WHERE username = @username AND userId != @userId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", newUsername);
                        cmd.Parameters.AddWithValue("@userId", currentUserId);
                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        args.IsValid = (count == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                args.IsValid = false;
                ShowErrorMessage("Error checking username: " + ex.Message);
            }
        }

        protected void EmailChange(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string newEmail = args.Value;

            // If email hasn't changed, it's valid
            if (newEmail == currentEmail)
            {
                args.IsValid = true;
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM userTable WHERE email = @email AND userId != @userId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", newEmail);
                        cmd.Parameters.AddWithValue("@userId", currentUserId);
                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        args.IsValid = (count == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                args.IsValid = false;
                ShowErrorMessage("Error checking email: " + ex.Message);
            }
        }

        public void ProfileUpdateBtn(object sender, EventArgs e)
        {
            modifyErrorMsg.Visible = false;
            modifySuccessMsg.Visible = false;

            if (!Page.IsValid)
            {
                ShowErrorMessage("Please correct the errors below.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE userTable SET username = @username, email = @email";

                    // Only update password if provided
                    if (!string.IsNullOrWhiteSpace(confirmPWTxt1.Text))
                    {
                        query += ", password = @password";
                    }

                    query += " WHERE userId = @userId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@userId", currentUserId);

                        if (!string.IsNullOrWhiteSpace(confirmPWTxt1.Text))
                        {
                            cmd.Parameters.AddWithValue("@password", confirmPWTxt1.Text);
                        }

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            if (txtUsername.Text.Trim() != currentUsername)
                            {
                                Session["Username"] = txtUsername.Text.Trim();
                            }

                            confirmPWTxt1.Text = "";
                            confirmPWTxt2.Text = "";

                            currentUsername = txtUsername.Text.Trim();
                            currentEmail = txtEmail.Text.Trim();

                            ShowSuccessMessage("Profile updated successfully!");
                        }
                        else
                        {
                            ShowErrorMessage("No changes were made to your profile.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ShowErrorMessage("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred: " + ex.Message);
            }
        }

        public void ProfileSignoutBtn(object sender, EventArgs e)
        {
            Session["UserId"] = null;
            Session["UserName"] = null;
            Session["Role"] = null;
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        private void ShowErrorMessage(string message)
        {
            modifyErrorMsg.Text = message;
            modifyErrorMsg.Visible = true;
        }

        private void ShowSuccessMessage(string message)
        {
            modifySuccessMsg.Text = message;
            modifySuccessMsg.Visible = true;
        }
    }
}