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
    public partial class guest_flashcard : System.Web.UI.Page
    {
        private string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["deckId"]))
                {
                    Response.Redirect("guest_all_decks.aspx");
                }

                int deckId = Convert.ToInt32(Request.QueryString["deckId"]);
                int cardIndex = string.IsNullOrEmpty(Request.QueryString["cardIndex"]) ? 0 : Convert.ToInt32(Request.QueryString["cardIndex"]);

                guestHdnDeckId.Value = deckId.ToString();
                Session["GuestCurrentDeck"] = deckId;

                guestDeckName.Text = GetScalar($"SELECT deckName FROM deckTable WHERE deckId = {deckId}");
                guestHdnTotalCards.Value = GetScalar($"SELECT COUNT(*) FROM flashcardTable WHERE deckId = {deckId}");

                ShowCard(cardIndex);
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
            string query = $@"SELECT cardId, term, cardDefinition FROM flashcardTable WHERE deckId = {Session["GuestCurrentDeck"]} ORDER BY cardId OFFSET {index} ROWS FETCH NEXT 1 ROWS ONLY";

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        guestLblTerm.Text = reader["term"].ToString();
                        guestLblDefinition.Text = reader["cardDefinition"].ToString();
                        guestHdnCurrentCardId.Value = reader["cardId"].ToString();
                        guestProgress.Text = $"Card {index + 1} of {guestHdnTotalCards.Value}";
                        guestHdnCurrentIndex.Value = index.ToString();
                    }
                }
            }
        }

        protected void guestPrevCardBtn(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(guestHdnCurrentIndex.Value);
            if (index > 0)
                Response.Redirect($"guest_flashcard.aspx?deckId={guestHdnDeckId.Value}&cardIndex={index - 1}");
        }

        protected void guestNextCardBtn(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(guestHdnCurrentIndex.Value);
            if (index < Convert.ToInt32(guestHdnTotalCards.Value) - 1)
                Response.Redirect($"guest_flashcard.aspx?deckId={guestHdnDeckId.Value}&cardIndex={index + 1}");
        }
    }
}