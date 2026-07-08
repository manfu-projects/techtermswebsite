using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace GroupAssignment
{
    public partial class guest_all_decks : System.Web.UI.Page
    {
        private string selectedCategory = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();

                string category = Request.QueryString["category"];
                if (!string.IsNullOrEmpty(category))
                {
                    ShowLockMessageOnly(category);
                }
                else
                {
                    LoadDecksWithLockMessage();
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

        // Show preview decks & lock message
        private void LoadDecksWithLockMessage()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT TOP 15 deckId, deckName, deckDesc FROM deckTable ORDER BY deckName";
                    SqlCommand cmd = new SqlCommand(query, con);

                    SqlDataReader reader = cmd.ExecuteReader();
                    rptDecks.DataSource = reader;
                    rptDecks.DataBind();

                    pnlLockedDecks.Visible = true;
                    pnlLockedDecks.CssClass = "locked-decks-banner";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading decks: " + ex.Message);
            }
        }

        private void ShowLockMessageOnly(string categoryName)
        {
            rptDecks.DataSource = null;
            rptDecks.DataBind();

            pnlLockedDecks.Visible = true;
            pnlLockedDecks.CssClass = "locked-decks-banner-full";
        }

        protected void rptAllDecks(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewDeck")
            {
                int deckId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"guest_flashcard.aspx?deckId={deckId}");
            }
        }
    }
}