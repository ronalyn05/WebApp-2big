using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Firebase.Storage;
using FireSharp.Response;

namespace WRS2big_Web
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient twoBigDB;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Connection to database
            twoBigDB = new FireSharp.FirebaseClient(config);


            //if (Session["username"] == null)
            //{
            //    //User not found
            //     Response.Write("<script>alert('User not found');</script>");
            //    //Response.Redirect("/LandingPage/Account.aspx");
            ////}
            if (Session["idno"] == null)
            {
                // User not found
                Response.Write("<script>alert('User not found');</script>");
                //Response.Redirect("/LandingPage/Account.aspx");
            }
            else
            {
                string WRSname = (string)Session["WRSname"];
                lblWRSname.Text = WRSname;
            }
           
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            //Session.Abandon();
            //Session.Clear();
            //if (Session["Email"] == null)
            //{
            //    Response.Redirect("/LandingPage/Index.aspx");
            //}

            Session.Abandon();
            Session.RemoveAll();
            Session["username"] = null;
            Session["password"] = null;
            Session.Clear();
            Response.Redirect("/LandingPage/Index.aspx");
        }
    }
}