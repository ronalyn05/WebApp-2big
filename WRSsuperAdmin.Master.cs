using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WRS2big_Web
{
    public partial class WRSsuperAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = (string) Session["name"];
            if (name == null)
            {
                Response.Write("<script>alert('Please Login your account first'); window.location.href = '/superAdmin/Account.aspx'; </script>");
            }
            else
            {
                superLbl.Text = Session["name"].ToString();
            }
           
        }
    }
}