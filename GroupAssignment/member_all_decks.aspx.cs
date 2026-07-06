using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GroupAssignment
{
    public partial class member_all_decks : System.Web.UI.Page
    {
        protected Repeater rptCategories;
        protected Repeater rptDecks;
        private string selectedCategory = null;

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
                LoadCategories();
                string category = Request.QueryString["category"];
                if (!string.IsNullOrEmpty(category))
                {
                    LoadByCategory(category);
                }
                else
                {
                    LoadAllDecks();
                }
            }
        }

        private void LoadCategories()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT DISTINCT deckName FROM deckTable ORDER BY deckName";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    rptCategories.DataSource = reader;
                    rptCategories.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading categories: " + ex.Message);
            }
        }

        private void LoadAllDecks()
        {
            LoadByCategory(null);
        }

        private void LoadByCategory(string categoryName)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query;
                    SqlCommand cmd;

                    if (string.IsNullOrEmpty(categoryName))
                    {
                        query = "SELECT deckId, deckName, deckDesc FROM deckTable ORDER BY deckName";
                        cmd = new SqlCommand(query, con);
                    }
                    else
                    {
                        query = "SELECT deckId, deckName, deckDesc FROM deckTable WHERE deckName = @categoryName ORDER BY deckName";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@categoryName", categoryName);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    rptDecks.DataSource = reader;
                    rptDecks.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading filtered decks: " + ex.Message);
            }
        }

        protected void rptAllDecks(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewDeck")
            {
                int deckId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"member_flashcard.aspx?deckId={deckId}");
            }
        }
    }
}