using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WRS2big_Web.Model;

namespace WRS2big_Web.superAdmin
{
    public partial class Account : System.Web.UI.Page
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient twoBigDB;

        protected void Page_Load(object sender, EventArgs e)
        {
            //connection to database 
            twoBigDB = new FireSharp.FirebaseClient(config);

        }
        protected void Login_Click(object sender, EventArgs e)
        {
            string email = "technique.services@gmail.com";
            string pass = "TechniqueServices";
            string uEmail = txt_idno.Text;
            string uPass = txt_password.Text;


            if (uEmail == email && uPass == pass)
            {
                string superAdmin = "Technique Services";

                Session["email"] = uEmail;
                Session["password"] = uPass;

                Response.Write("<script>alert ('Login Successfull!'); location.reload(); window.location.href = '/superAdmin/SAdminIndex.aspx'; </script>");

                Session["name"] = superAdmin;
            }
            else
            {
                Response.Write("<script>alert ('Login Unsuccessful! Invalid credentials. Please Try again.'); location.reload(); window.location.href = '/superAdmin/Account.aspx'; </script>");
            }
        }
    }
}