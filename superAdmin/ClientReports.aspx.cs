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
            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount wrsClients = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

            //creating the columns of the gridview
            DataTable plansTable = new DataTable();
            plansTable.Columns.Add("CLIENT ID");
            plansTable.Columns.Add("CLIENT NAME");
            plansTable.Columns.Add("EMAIL");
            plansTable.Columns.Add("DATE SUBSCRIBED");
            plansTable.Columns.Add("SUBSCRIBED PLAN");
            plansTable.Columns.Add("STATUS");

            foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
            {
                response = twoBigDB.Get("ADMIN/" + entry.Value.idno + "/Subscribed_Package");
                Model.Subscribed_Package packageDetails = response.ResultAs<Model.Subscribed_Package>();

                plansTable.Rows.Add(entry.Value.idno, entry.Value.fname + " " + entry.Value.lname + " ", entry.Value.email, packageDetails.dateSubscribed,packageDetails.packageName, entry.Value.status);

            }


            // Bind DataTable to GridView control
            ClientsGridview.DataSource = plansTable;
            ClientsGridview.DataBind();
        }

        


  
    }
}