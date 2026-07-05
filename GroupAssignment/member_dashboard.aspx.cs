using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GroupAssignment
{
    public partial class member_dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                // Redirect to login if not logged in
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                dashboard_username.Text = Session["Username"].ToString();
            }
        }
    }
}