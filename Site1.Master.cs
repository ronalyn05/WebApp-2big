using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using WRS2big_Web.Model;

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
                string stationName = (string)Session["stationName"];
                lblWRSname.Text = stationName;
            }
           
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            // Get the log ID from the session
            int logsId = (int)Session["logsId"];

            // Retrieve the existing Users log object from the database
            FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
            UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

            //Get the current date and time
            DateTimeOffset addedTime = DateTimeOffset.UtcNow;

            // Log user activity
            var log = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = logsId,
                userFullname = (string)Session["fullname"],
                emp_id = existingLog.emp_id,
                empFullname = existingLog.empFullname,
                empDateAdded = existingLog.empDateAdded,
                dateLogin = existingLog.dateLogin,
                deliveryDetailsId = existingLog.deliveryDetailsId,
                standardAdded = existingLog.standardAdded,
                reservationAdded = existingLog.reservationAdded,
                expressAdded = existingLog.expressAdded,
                productRefillId = existingLog.productRefillId,
                productrefillDateAdded = existingLog.productrefillDateAdded,
                other_productId = existingLog.other_productId,
                otherProductDateAdded = existingLog.otherProductDateAdded,
                tankId = existingLog.tankId,
                tankSupplyDateAdded = existingLog.tankSupplyDateAdded,
                dateLogout = addedTime
            };

            twoBigDB.Update("USERSLOG/" + log.logsId, log);


            Session.Abandon();
            Session.RemoveAll();
            Session["idno"] = null;
            Session["password"] = null;
            Session.Clear();
            Response.Redirect("/LandingPage/Account.aspx");
        }
    }
}