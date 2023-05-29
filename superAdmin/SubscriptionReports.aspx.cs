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
                LoadSales();
            }
        }
        private void LoadSales()
        {
            FirebaseResponse response = twoBigDB.Get("SUBSCRIBED_CLIENTS");
            Model.superAdminClients all = response.ResultAs<Model.superAdminClients>();
            var data = response.Body;
            Dictionary<string, Model.superAdminClients> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superAdminClients>>(data);

            if (data != null && subscribed != null)
            {
                DataTable salesTable = new DataTable();
                salesTable.Columns.Add("SUBSCRIPTION ID");
                salesTable.Columns.Add("DATE / TIME");
                salesTable.Columns.Add("TRANSACTION TYPE");
                salesTable.Columns.Add("CLIENT NAME");
                salesTable.Columns.Add("AMOUNT");
                salesTable.Columns.Add("BALANCE");

                decimal totalAmount = 0;
                foreach (KeyValuePair<string, Model.superAdminClients> entry in subscribed)
                {
                    totalAmount += entry.Value.amount;
                    salesTable.Rows.Add(entry.Value.subscriptionID, entry.Value.dateSubscribed, entry.Value.type, entry.Value.fullname, entry.Value.amount, totalAmount);
                }
                // Bind DataTable to GridView control
                subscriptionSales.DataSource = salesTable;
                subscriptionSales.DataBind();
            }

        }
        private void loadSubscriptions()
        {
            FirebaseResponse response = twoBigDB.Get("SUBSCRIBED_CLIENTS");//CHANGE TO ADMIN TABLE
            Model.superAdminClients all = response.ResultAs<Model.superAdminClients>();
            var data = response.Body;
            Dictionary<string, Model.superAdminClients> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superAdminClients>>(data);
            
            if (data != null && subscribed != null)
            {
                //creating the columns of the gridview
                DataTable clientsTable = new DataTable();
                clientsTable.Columns.Add("ID");
                clientsTable.Columns.Add("DATE");
                clientsTable.Columns.Add("NAME");
                clientsTable.Columns.Add("AMOUNT");
                clientsTable.Columns.Add("PACKAGE");
                clientsTable.Columns.Add("STATUS");

                foreach (KeyValuePair<string, Model.superAdminClients> entry in subscribed)
                {

                    clientsTable.Rows.Add(entry.Value.clientID,entry.Value.dateSubscribed, entry.Value.fullname, entry.Value.amount, entry.Value.plan, entry.Value.currentSubStatus);

                }
                // Bind DataTable to GridView control
                subscriptionReport.DataSource = clientsTable;
                subscriptionReport.DataBind();
            }
           

        }

        protected void modalSearch_Click(object sender, EventArgs e)
        {

        }

        protected void viewSubscriptionHistory_Click(object sender, EventArgs e)
        {
            //Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            //Get the order ID from the first cell in the row
            int adminID = int.Parse(row.Cells[1].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ADMIN/" + adminID);
            Model.AdminAccount clientDetails = response.ResultAs<Model.AdminAccount>();

            int currentClient = clientDetails.idno;
            Session["currentClient"] = currentClient;

            Response.Write("<script>window.location.href = '/superAdmin/clientSubscriptionHistory.aspx'; </script>");
        }
    }
}