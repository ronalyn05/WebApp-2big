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
using WRS2big_Web.Model;

namespace WRS2big_Web.Admin
{
    public partial class UsersLog : System.Web.UI.Page
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

                //connection to database 
                twoBigDB = new FireSharp.FirebaseClient(config);

                //WORKING BUT NEED TO BE MODIFIED

                string idno = (string)Session["idno"];
                    // int adminId = int.Parse(idno);

                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("USERSLOG/");
                Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();
                var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);

                // Create the DataTable to hold the orders
                //sa pag create sa table
                DataTable userLogTable = new DataTable();
                userLogTable.Columns.Add("LOG ID");
                userLogTable.Columns.Add("USER NAME");
                userLogTable.Columns.Add("LOGIN TIME");
                userLogTable.Columns.Add("EMPLOYEE");
                userLogTable.Columns.Add("PRODUCT REFILL OFFER");
                userLogTable.Columns.Add("OTHER PRODUCTS OFFER");
                userLogTable.Columns.Add("TANK SUPPLY");
                userLogTable.Columns.Add("ORDER TRANSACTION");
                userLogTable.Columns.Add("PAYMENT TRANSACTION");
                userLogTable.Columns.Add("LOGOUT TIME");

            if (response != null && response.ResultAs<UsersLogs>() != null)
                {
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    // Check if payment has been received for this entry
                    if (!string.IsNullOrEmpty(entry.datePaymentReceived.ToString()))
                    {
                        string employee = "Added employee with id number: " + entry.emp_id + " on " + entry.empDateAdded;
                        string tankSupply = "Added tank supply on: " + entry.tankSupplyDateAdded + " with tank id " + entry.tankId;
                        string productRefill = "Added product refill on: " + entry.productrefillDateAdded + " with product id " + entry.productRefillId;
                        string otherProduct = "Added other product offers on: " + entry.otherProductDateAdded + " with other product id " + entry.other_productId;
                        string order = "Accepted order from customer: " + entry.cusId + " with other order id " + entry.orderId + " on " + entry.dateOrderAccepted;
                        string payment = "Received payment on: " + entry.datePaymentReceived + " with order id " + entry.orderId;

                        userLogTable.Rows.Add(entry.logsId, entry.userFullname, entry.dateLogin, employee, productRefill, otherProduct,
                                              tankSupply, order, payment, entry.dateLogout);
                    }
                    else
                    {
                        string employee = "Added employee with id number: " + entry.emp_id + " on " + entry.empDateAdded;
                        string tankSupply = "Added tank supply on: " + entry.tankSupplyDateAdded + " with tank id " + entry.tankId;
                        string productRefill = "Added product refill on: " + entry.productrefillDateAdded + " with product id " + entry.productRefillId;
                        string otherProduct = "Added other product offers on: " + entry.otherProductDateAdded + " with other product id " + entry.other_productId;
                        string order = "Accepted order from customer: " + entry.cusId + " with other order id " + entry.orderId + " on " + entry.dateOrderAccepted;

                        userLogTable.Rows.Add(entry.logsId, entry.userFullname, entry.dateLogin, employee, productRefill, otherProduct,
                                              tankSupply, order, "", entry.dateLogout);
                    }
                }

            }
            else
            {
                // Handle null response or invalid selected value
                userLogTable.Rows.Add("No data found", "", "", "", "", "", "");
            }

                // Bind the DataTable to the GridView
                gridUserLog.DataSource = userLogTable;
                gridUserLog.DataBind();
            
        }
    }
}