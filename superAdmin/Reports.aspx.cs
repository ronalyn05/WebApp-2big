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
    public partial class Reports : System.Web.UI.Page
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
                superAdminLog();
            }
        }
        private void superAdminLog()
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN_LOGS");
            Model.UsersLogs all = response.ResultAs<Model.UsersLogs>();
            var data = response.Body;
            Dictionary<string, Model.UsersLogs> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.UsersLogs>>(data);

            if (data != null)
            {
                //creating the columns of the gridview
                DataTable clientsTable = new DataTable();
                clientsTable.Columns.Add("LOG ID");
                clientsTable.Columns.Add("NAME");
                clientsTable.Columns.Add("ACTIVITY");
                clientsTable.Columns.Add("TIME");

                foreach (KeyValuePair<string, Model.UsersLogs> entry in subscribed)
                {

                    clientsTable.Rows.Add(entry.Value.logsId, entry.Value.userFullname, entry.Value.userActivity, entry.Value.activityTime);

                }
                // Bind DataTable to GridView control
                superAdminLogs.DataSource = clientsTable;
                superAdminLogs.DataBind();
            }
        }
    }
}