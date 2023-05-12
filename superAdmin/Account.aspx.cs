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

                //Get the current date and time
                DateTime loginTime = DateTime.UtcNow;

                //generate a random number for users logged
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                //Store the login information in the USERLOG table
                var data = new UsersLogs
                {
                    logsId = idnum,
                    //userIdnum = int.Parse(idno),
                    userFullname = superAdmin,
                    userActivity = "LOGGED IN",
                    activityTime = loginTime
                };

                //Storing the  info
                FirebaseResponse response = twoBigDB.Set("SUPERADMIN_LOGS/" + data.logsId, data);//Storing data to the database
                UsersLogs res = response.ResultAs<UsersLogs>();//Database Result

                //Get the exsiting data in the database
                FirebaseResponse resLogs = twoBigDB.Get("SUPERADMIN_LOGS/" + data.logsId);
                UsersLogs existingLog = resLogs.ResultAs<UsersLogs>();

                if (existingLog != null)
                {
                    Session["logsId"] = existingLog.logsId;
                }


                Response.Write("<script>alert ('Login Successfull!'); window.location.href = '/superAdmin/SAdminIndex.aspx'; </script>");

                Session["name"] = superAdmin;
            }
            else
            {
                Response.Write("<script>alert ('Login Unsuccessful! Invalid credentials. Please Try again.'); location.reload(); window.location.href = '/superAdmin/Account.aspx'; </script>");
            }
        }
    }
}