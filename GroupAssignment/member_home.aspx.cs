using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GroupAssignment
{
    public partial class member_home : System.Web.UI.Page
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
        }
    }
}