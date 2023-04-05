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
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;


namespace WRS2big_Web.superAdmin
{
    public partial class ClientReports : System.Web.UI.Page
    {
        //Initialize the FirebaseClient with the database URL and secret key.
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient twoBigDB;
        protected void Page_Load(object sender, EventArgs e)
        {

            twoBigDB = new FireSharp.FirebaseClient(config);

            if (!IsPostBack)
            {
                DisplayClients();
            }


        }

        private void DisplayClients()
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN/SUBSCRIBING_CLIENTS");
            Model.superAdminClients wrsClients = response.ResultAs<Model.superAdminClients>();
            var data = response.Body;
            Dictionary<string, Model.superAdminClients> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.superAdminClients>>(data);

            //creating the columns of the gridview
            DataTable plansTable = new DataTable();
            plansTable.Columns.Add("CLIENT ID");
            plansTable.Columns.Add("CLIENT NAME");
            plansTable.Columns.Add("EMAIL");
            plansTable.Columns.Add("DATE SUBSCRIBED");
            plansTable.Columns.Add("SUBSCRIBED PLAN");
            plansTable.Columns.Add("STATUS");

            foreach (KeyValuePair<string, Model.superAdminClients> entry in clients)
            {
                //FirebaseResponse clientResponse = twoBigDB.Get("SUPERADMIN/SUBSCRIBING_CLIENTS" + entry.Key);
                //Model.superAdminClients subscribingClients = clientResponse.ResultAs<Model.superAdminClients>();
                plansTable.Rows.Add(entry.Value.clientID, entry.Value.fullname, entry.Value.email, entry.Value.dateSubscribed, entry.Value.plan, entry.Value.status);

            }


            // Bind DataTable to GridView control
            ClientsGridview.DataSource = plansTable;
            ClientsGridview.DataBind();
        }

        


  
    }
}