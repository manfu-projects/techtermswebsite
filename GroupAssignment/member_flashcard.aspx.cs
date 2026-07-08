using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GroupAssignment
{
    public partial class member_flashcard : System.Web.UI.Page
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
                int deckId = Convert.ToInt32(Request.QueryString["deckId"]);
                int cardIndex = string.IsNullOrEmpty(Request.QueryString["cardIndex"]) ? 0 : Convert.ToInt32(Request.QueryString["cardIndex"]);

                hdnDeckId.Value = deckId.ToString();
                Session["CurrentDeck"] = deckId;

                deckName.Text = GetScalar($"SELECT deckName FROM deckTable WHERE deckId = {deckId}");
                hdnTotalCards.Value = GetScalar($"SELECT COUNT(*) FROM flashcardTable WHERE deckId = {deckId}");

                ShowCard(cardIndex);
                LoadStudyLists();
            }
        }

        private string GetScalar(string query)
        {
            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                return cmd.ExecuteScalar().ToString();
            }
        }

        private void ShowCard(int index)
        {
            string query = $@"SELECT cardId, term, cardDefinition FROM flashcardTable WHERE deckId = {Session["CurrentDeck"]} ORDER BY cardId OFFSET {index} ROWS FETCH NEXT 1 ROWS ONLY";

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblTerm.Text = reader["term"].ToString();
                        lblDefinition.Text = reader["cardDefinition"].ToString();
                        hdnCurrentCardId.Value = reader["cardId"].ToString();
                        progress.Text = $"Card {index + 1} of {hdnTotalCards.Value}";
                        hdnCurrentIndex.Value = index.ToString();
                    }
                }
            }
        }

        private void LoadStudyLists()
        {
            string query = "SELECT DISTINCT listName FROM studyListTable WHERE userId = @userId ORDER BY listName";

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@userId", Session["UserId"]);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ddlStudyLists.DataSource = reader;
                    ddlStudyLists.DataTextField = "listName";
                    ddlStudyLists.DataValueField = "listName";
                    ddlStudyLists.DataBind();
                }
            }
            ddlStudyLists.Items.Insert(0, new ListItem("-- Select a list --", "0"));
        }

        private void SaveCardToList(string listName)
        {
            string deleteQuery = "DELETE FROM studyListTable WHERE listName = @listName AND cardId IS NULL";
            string saveQuery = "INSERT INTO studyListTable (userId, cardId, listName, dateAdded) VALUES (@userId, @cardId, @listName, GETDATE())";

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                using (SqlCommand deletecmd = new SqlCommand(deleteQuery, con))
                {
                    deletecmd.Parameters.AddWithValue("listName", listName);
                    deletecmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand(saveQuery, con))
                {
                    cmd.Parameters.AddWithValue("@userId", Session["UserId"]);
                    cmd.Parameters.AddWithValue("@cardId", hdnCurrentCardId.Value);
                    cmd.Parameters.AddWithValue("@listName", listName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void prevCardBtn(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(hdnCurrentIndex.Value);
            if (index > 0)
                Response.Redirect($"member_flashcard.aspx?deckId={hdnDeckId.Value}&cardIndex={index - 1}");
        }

        protected void nextCardBtn(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(hdnCurrentIndex.Value);
            if (index < Convert.ToInt32(hdnTotalCards.Value) - 1)
                Response.Redirect($"member_flashcard.aspx?deckId={hdnDeckId.Value}&cardIndex={index + 1}");
        }

        protected void saveToListBtn(object sender, EventArgs e)
        {
            LoadStudyLists();
            ScriptManager.RegisterStartupScript(this, GetType(), "openModal", "openSavePage();", true);
        }

        protected void createSaveBtn(object sender, EventArgs e)
        {
            string listName = ddlStudyLists.SelectedValue != "0" ? ddlStudyLists.SelectedValue : txtNewListName.Text.Trim();

            if (string.IsNullOrEmpty(listName))
            {
                return;
            }

            SaveCardToList(listName);

            // if a new list was created (selected value was "0"), reload the dropdown list to show the newly created list

            if (ddlStudyLists.SelectedValue == "0") LoadStudyLists();

            ScriptManager.RegisterStartupScript(this, GetType(), "close-save-page",
                "setTimeout(function() { closeSavePage(); }, 2000);", true);
        }
    }
}