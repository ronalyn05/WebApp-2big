using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using WRS2big_Web.Model;

namespace WRS2big_Web.Admin
{
    public partial class CustomerRecord : System.Web.UI.Page
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
                displaycustomerRecord();
            }

        }

        private void displaycustomerRecord()
        {
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Dictionary<string, Customer> customerlist = response.ResultAs<Dictionary<string, Customer>>();

            // Create the DataTable to hold the orders
            //sa pag create sa table
            DataTable customerTable = new DataTable();
            customerTable.Columns.Add("CUSTOMER ID");
            customerTable.Columns.Add("CUSTOMER NAME");
            customerTable.Columns.Add("CUSTOMER ADDRESS");
            customerTable.Columns.Add("CUSTOMER STATUS");
            customerTable.Columns.Add("CUSTOMER EMAIL");
            customerTable.Columns.Add("CUSTOMER CONTACT NUMBER");
            customerTable.Columns.Add("DATE REGISTERED");
           

            if (response != null && response.ResultAs<Customer>() != null)
            {
                //var filteredList = customerlist.Values.Where(d => d.adminId.ToString() == idno).OrderByDescending(d => d.dateAdded);

                // Loop through the orders and add them to the DataTable
               
                    foreach (var entry in customerlist)
                    {
                        //string dateRegistered = entry.dateRegistered == DateTime.MinValue ? "" : entry.dateRegistered.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        //customerTable.Rows.Add(entry.cusId, entry.firstName + " " + entry.middleName + " " + entry.lastName,
                        //    entry.address, entry.cus_status, entry.email, entry.phoneNumber, dateRegistered);

                        string dateRegistered = entry.Value.dateRegistered == DateTime.MinValue ? "" : entry.Value.dateRegistered.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        customerTable.Rows.Add(entry.Value.cusId, entry.Value.firstName + " " + entry.Value.middleName + " " + entry.Value.lastName,
                            entry.Value.address, entry.Value.cus_status, entry.Value.email, entry.Value.phoneNumber, dateRegistered);
                    }

                


            }
            else
            {
                // Handle null response or invalid selected value
                lblMessage.Text = " No data found";
            }

            // Bind the DataTable to the GridView
            gridCustomer_Details.DataSource = customerTable;
            gridCustomer_Details.DataBind();
        }
    }

}