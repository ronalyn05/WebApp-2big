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

namespace WRS2big_Web.superAdmin
{
    public partial class clientSubscriptionHistory : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                loadClientSubHistory();
                
            }
        }
        private void loadClientSubHistory()
        {
            if (Session["currentClient"] != null)
            {
                int clientID = (int)Session["currentClient"];

                FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_LOGS");
                Model.subscriptionLogs all = response.ResultAs<Model.subscriptionLogs>();
                var data = response.Body;
                Dictionary<string, Model.subscriptionLogs> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.subscriptionLogs>>(data);

                decimal totalAmount = 0;
                if (data != null)
                {
                    //creating the columns of the gridview
                    DataTable clientsTable = new DataTable();
                    clientsTable.Columns.Add("LOG ID");
                    clientsTable.Columns.Add("NAME");
                    clientsTable.Columns.Add("ACTIVITY");
                    clientsTable.Columns.Add("TIME");
                    clientsTable.Columns.Add("TOTAL");
                    clientsTable.Columns.Add("STATUS");
                    clientsTable.Columns.Add("SUBSCRIPTION STATUS");

                    foreach (KeyValuePair<string, Model.subscriptionLogs> entry in subscribed)
                    {
                        if (entry.Value.userIdnum == clientID)
                        {
                            FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + clientID);
                            Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();

                            totalAmount += entry.Value.total;
                            clientsTable.Rows.Add(entry.Value.logsId, entry.Value.userFullname, entry.Value.userActivity, entry.Value.activityTime, totalAmount, admin.subStatus, admin.currentSubscription);

                        }

                    }
                    // Bind DataTable to GridView control
                    subscriptionHistory.DataSource = clientsTable;
                    subscriptionHistory.DataBind();
                }
            }
            else
            {
                Response.Write("<script> alert('Session Expired! Please login again');window.location.href = '/superAdmin/SuperAdminAccount.aspx';</script>");

            }

        }
    }
}