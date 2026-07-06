using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace GroupAssignment
{
    public partial class member_study_list : System.Web.UI.Page
    {

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
                showStudyLists();
            }
        }

        private void showStudyLists()
        {
            try
            {
                int userId = (int) Session["UserID"];
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = @"SELECT listName, 
                                     MIN(listId) as listId, 
                                     MIN(dateAdded) as dateAdded, 
                                     COUNT(cardId) as cardCount 
                                     FROM studyListTable 
                                     WHERE userId = @userId 
                                     GROUP BY listName 
                                     ORDER BY MIN(dateAdded) DESC";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    StudyListsSect.DataSource = reader;
                    StudyListsSect.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading study lists: " + ex.Message);
            }
        }
        protected void studyListsRpt(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewList")
            {
                string listName = e.CommandArgument.ToString();
                Response.Redirect($"member_view_study_list.aspx?listName={listName}");
            }
            else if (e.CommandName == "DeleteList")
            {
                string listName = e.CommandArgument.ToString();
                deleteStudyList(listName);
                showStudyLists();
            }
        }

        private void deleteStudyList(string listName)
        {
            try
            {
                int userId = (int) Session["UserID"];
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM studyListTable WHERE listName = @listName AND userId = @userId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@listName", listName);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error deleting study list: " + ex.Message);
            }
        }
    }
}