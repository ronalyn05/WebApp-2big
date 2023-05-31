using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Storage;
using System.IO;
using WRS2big_Web.Model;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WRS2big_Web.LandingPage
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        //Initialize firebase client
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

        protected void checkKey_Click(object sender, EventArgs e)
        {
            string forgetEmail = enteredEmail.Text;
            string forgetPhone = enteredPhone.Text;

            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount all = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

            if (data != null && clients != null)
            {
                foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
                {
                    if (entry.Value.email == forgetEmail && entry.Value.phone == forgetPhone)
                    {
                        Session["currentAdmin"] = entry.Value.idno;
                        Response.Write("<script>alert ('We found your account! Change your password now!');window.location.href = '/LandingPage/ConfirmResetPass.aspx'; </script>");
                    }
                    else
                    {
                        Response.Write("<script>alert ('Account not found! Please input the correct credentials according to what you have used in your account registration');window.location.href = '/LandingPage/ResetPassword.aspx'; </script>");
                    }
                }
            }




            }
    }
}