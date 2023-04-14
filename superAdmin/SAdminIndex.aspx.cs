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
    public partial class SAdminIndex : System.Web.UI.Page
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

            // Retrieve all customer from the CUSTOMERS table
            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Dictionary<string, Customer> customerList = response.ResultAs<Dictionary<string, Customer>>();

            int count = customerList.Count;

            registeredCustomers.Text = count.ToString();

            response = twoBigDB.Get("ADMIN");
            Dictionary<string, AdminAccount> Clients = response.ResultAs<Dictionary<string, AdminAccount>>();

            int clientCount = 0;
            int pendingCount = 0;

            foreach (var admin in Clients)
            {
                if (admin.Value.subStatus == "Subscribed")
                {
                    clientCount++;
                }
            }
            subscribedClients.Text = clientCount.ToString();

            foreach (var pending in Clients)
            {
                if (pending.Value.status == "pending")
                {
                    pendingCount++;
                }
            }
            pendingClients.Text = pendingCount.ToString();

            response = twoBigDB.Get("SUPERADMIN/SUBSCRIBING_CLIENTS");
            Dictionary<string, superAdminClients> subscribed = response.ResultAs<Dictionary<string, superAdminClients>>();

            double totalSale = 0;
            foreach (var entry in subscribed)
            {
                superAdminClients client = entry.Value;
                totalSale += client.amount;
            }

            totalSubSale.Text = totalSale.ToString();


        }
    }
}