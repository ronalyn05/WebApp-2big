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
            superLbl.Text = Session["name"].ToString();
        }
    }
}