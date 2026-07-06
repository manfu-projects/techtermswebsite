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
    public partial class member_view_study_list : System.Web.UI.Page
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
                int userId = Convert.ToInt32(Session["UserId"]);
                string listName = Request.QueryString["listName"];
                int cardIndex = string.IsNullOrEmpty(Request.QueryString["cardIndex"]) ? 0 : Convert.ToInt32(Request.QueryString["cardIndex"]);

                hdnUserId.Value = userId.ToString();
                hdnListName.Value = listName;
                Session["CurrentUserId"] = userId;
                Session["CurrentListName"] = listName;

                if (!string.IsNullOrEmpty(listName))
                {
                    studyListName.Text = listName;
                }
                else
                {
                    studyListName.Text = "My Study Lists";
                }

                hdnTotalCards.Value = GetStudyListCardCount(listName, userId);

                ShowCard(cardIndex);
            }
        }

        private string GetStudyListCardCount(string listName, int userId)
        {
            string query = $@"SELECT COUNT(*) FROM studyListTable WHERE listName = '{listName}' AND userId = {userId} AND cardId IS NOT NULL";

            object result = GetScalar(query);
            return result?.ToString() ?? "0";
        }

        private object GetScalar(string query)
        {
            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                return cmd.ExecuteScalar();
            }
        }

        private void ShowCard(int index)
        {
            int userId = Convert.ToInt32(hdnUserId.Value);
            string listName = hdnListName.Value;
            int totalCards = Convert.ToInt32(hdnTotalCards.Value);

            if (totalCards == 0)
            {
                lblTerm.Text = "No cards in this study list";
                lblDefinition.Text = "Please add cards to this study list first";
                progress.Text = "0 of 0";
                return;
            }

            string query = $@"SELECT fc.cardId, fc.term, fc.cardDefinition, fc.deckId, sl.listId, sl.listName
                              FROM flashcardTable fc
                              INNER JOIN studyListTable sl ON fc.cardId = sl.cardId
                              WHERE sl.userId = {userId} 
                              AND sl.listName = '{listName}'
                              ORDER BY sl.listId, sl.cardId 
                              OFFSET {index} ROWS FETCH NEXT 1 ROWS ONLY";

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
                        studyListName.Text = reader["listName"].ToString();

                        progress.Text = $"Card {index + 1} of {totalCards}";
                        hdnCurrentIndex.Value = index.ToString();
                    }
                    else
                    {
                        lblTerm.Text = "No cards in this study list";
                        lblDefinition.Text = "Please add cards to this study list first";
                        progress.Text = "0 of 0";
                    }
                }
            }
        }

        protected void prevCardBtn(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(hdnCurrentIndex.Value);
            if (index > 0)
                Response.Redirect($"member_view_study_list.aspx?listName={hdnListName.Value}&cardIndex={index - 1}");
        }

        protected void nextCardBtn(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(hdnCurrentIndex.Value);
            if (index < Convert.ToInt32(hdnTotalCards.Value) - 1)
                Response.Redirect($"member_view_study_list.aspx?listName={hdnListName.Value}&cardIndex={index + 1}");
        }
    }
}