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
    public partial class SubscriptionReports : System.Web.UI.Page
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
                loadSubscriptions();
            }
        }

        private void loadSubscriptions()
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN/SUBSCRIBED_CLIENTS");
            Model.superAdminClients all = response.ResultAs<Model.superAdminClients>();
            var data = response.Body;
            Dictionary<string, Model.superAdminClients> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superAdminClients>>(data);
            
            if (data != null && subscribed != null)
            {
                //creating the columns of the gridview
                DataTable clientsTable = new DataTable();
                clientsTable.Columns.Add("DATE");
                clientsTable.Columns.Add("NAME");
                clientsTable.Columns.Add("AMOUNT");
                clientsTable.Columns.Add("PACKAGE");
                clientsTable.Columns.Add("STATUS");

                foreach (KeyValuePair<string, Model.superAdminClients> entry in subscribed)
                {

                    clientsTable.Rows.Add(entry.Value.dateSubscribed, entry.Value.fullname, entry.Value.amount, entry.Value.plan, entry.Value.currentSubStatus);

                }
                // Bind DataTable to GridView control
                subscriptionReport.DataSource = clientsTable;
                subscriptionReport.DataBind();
            }
           

        }
    }
}