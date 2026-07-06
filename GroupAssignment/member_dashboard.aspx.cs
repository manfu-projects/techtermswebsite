using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GroupAssignment
{
    public partial class member_dashboard : System.Web.UI.Page
    {
        private string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

            string role = Session["Role"]?.ToString();

            if (role != "Member")
            {
                Response.Redirect("login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string username = Session["Username"]?.ToString() ?? "User";
                dashboard_username.Text = username;

                LoadDashboardStats();
            }
        }

        private void LoadDashboardStats()
        {
            try
            {
                int userId = Convert.ToInt32(Session["UserId"]);

                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();

                    // Get total distinct cards saved 

                    string totalCardsQuery = @"SELECT COUNT(DISTINCT cardId) 
                                              FROM studyListTable 
                                              WHERE userId = @userId 
                                              AND cardId IS NOT NULL";

                    using (SqlCommand cmd = new SqlCommand(totalCardsQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        int totalCards = Convert.ToInt32(cmd.ExecuteScalar());
                        cardsLearntValue.Text = totalCards.ToString();
                    }

                    // Get total study lists via unique list names

                    string totalListsQuery = @"SELECT COUNT(DISTINCT listName) FROM studyListTable WHERE userId = @userId";

                    using (SqlCommand cmd = new SqlCommand(totalListsQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        int totalLists = Convert.ToInt32(cmd.ExecuteScalar());
                        totalListsValue.Text = totalLists.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading dashboard stats: " + ex.Message);
            }
        }
    }
}