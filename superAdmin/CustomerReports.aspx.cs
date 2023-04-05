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
    public partial class CustomerReports : System.Web.UI.Page
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

            displayCustomers();
        }

        protected void displayCustomers()
        {

            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer plan = response.ResultAs<Model.Customer>();
            var data = response.Body;
            Dictionary<string, Model.Customer> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(data);


            DataTable customerTable = new DataTable();
            customerTable.Columns.Add("CUSTOMER ID");
            customerTable.Columns.Add("FULL NAME");
            customerTable.Columns.Add("EMAIL");
            customerTable.Columns.Add("ADDRESS");
            customerTable.Columns.Add("PHONE #");



            foreach (KeyValuePair<string, Model.Customer> entry in clients)
            {
                customerTable.Rows.Add(entry.Value.cusId,
                    entry.Value.firstName + " " + entry.Value.lastName,
                    entry.Value.email,
                    entry.Value.address,
                    entry.Value.phoneNumber);
            }
            // Bind DataTable to GridView control
            customerReports.DataSource = customerTable;
            customerReports.DataBind();
        }

   }
}
