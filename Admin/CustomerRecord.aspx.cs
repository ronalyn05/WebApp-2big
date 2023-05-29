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

            // Retrieve all customers from the CUSTOMER table
            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            if (response != null && response.ResultAs<Dictionary<string, Customer>>() is Dictionary<string, Customer> customerList)
            {
                // Create the DataTable to hold the customer records
                DataTable customerTable = new DataTable();
                customerTable.Columns.Add("CUSTOMER ID");
                customerTable.Columns.Add("CUSTOMER NAME");
                customerTable.Columns.Add("CUSTOMER ADDRESS");
                customerTable.Columns.Add("CUSTOMER STATUS");
                customerTable.Columns.Add("CUSTOMER EMAIL");
                customerTable.Columns.Add("CUSTOMER CONTACT NUMBER");
                customerTable.Columns.Add("DATE REGISTERED");

                // Filter the customer records based on their access to the station
                var filteredList = customerList.Values.Where(c => c.orderedStore?.Any(os => os.adminId.ToString() == idno) ?? false).ToList();

                if(filteredList != null)
                {
                    // Loop through the filtered customer records and add them to the DataTable
                    foreach (var customer in filteredList)
                    {
                        string dateRegistered = customer.dateRegistered == DateTime.MinValue ? "" : customer.dateRegistered.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        customerTable.Rows.Add(
                            customer.cusId,
                            customer.firstName + " " + customer.middleName + " " + customer.lastName,
                            customer.address,
                            customer.cus_status,
                            customer.email,
                            customer.phoneNumber,
                            dateRegistered
                        );
                    }

                    // Bind the DataTable to the GridView
                    gridCustomer_Details.DataSource = customerTable;
                    gridCustomer_Details.DataBind();
                }
                else
                {
                    // Handle null response or invalid customer records
                    lblMessage.Text = "No customer record found";
                }

            }
            else
            {
                // Handle null response or invalid customer records
                lblMessage.Text = "No customer record found";
            }
        }


        //private void displaycustomerRecord()
        //{
        //    string idno = (string)Session["idno"];
        //    // int adminId = int.Parse(idno);

        //    // Retrieve all orders from the ORDERS table
        //    FirebaseResponse response = twoBigDB.Get("CUSTOMER");
        //    Dictionary<string, Customer> customerlist = response.ResultAs<Dictionary<string, Customer>>();

        //    // Create the DataTable to hold the orders
        //    //sa pag create sa table
        //    DataTable customerTable = new DataTable();
        //    customerTable.Columns.Add("CUSTOMER ID");
        //    customerTable.Columns.Add("CUSTOMER NAME");
        //    customerTable.Columns.Add("CUSTOMER ADDRESS");
        //    customerTable.Columns.Add("CUSTOMER STATUS");
        //    customerTable.Columns.Add("CUSTOMER EMAIL");
        //    customerTable.Columns.Add("CUSTOMER CONTACT NUMBER");
        //    customerTable.Columns.Add("DATE REGISTERED");


        //    if (response != null && response.ResultAs<Customer>() != null)
        //    {
        //        //var filteredList = customerlist.Values.Where(d => d.adminId.ToString() == idno).OrderByDescending(d => d.dateAdded);

        //        // Loop through the orders and add them to the DataTable

        //            foreach (var entry in customerlist)
        //            {
        //                //string dateRegistered = entry.dateRegistered == DateTime.MinValue ? "" : entry.dateRegistered.ToString("MMMM dd, yyyy hh:mm:ss tt");

        //                //customerTable.Rows.Add(entry.cusId, entry.firstName + " " + entry.middleName + " " + entry.lastName,
        //                //    entry.address, entry.cus_status, entry.email, entry.phoneNumber, dateRegistered);

        //                string dateRegistered = entry.Value.dateRegistered == DateTime.MinValue ? "" : entry.Value.dateRegistered.ToString("MMMM dd, yyyy hh:mm:ss tt");

        //                customerTable.Rows.Add(entry.Value.cusId, entry.Value.firstName + " " + entry.Value.middleName + " " + entry.Value.lastName,
        //                    entry.Value.address, entry.Value.cus_status, entry.Value.email, entry.Value.phoneNumber, dateRegistered);
        //            }




        //    }
        //    else
        //    {
        //        // Handle null response or invalid selected value
        //        lblMessage.Text = " No data found";
        //    }

        //    // Bind the DataTable to the GridView
        //    gridCustomer_Details.DataSource = customerTable;
        //    gridCustomer_Details.DataBind();
        //}
        //private void displaycustomerRecord()
        //{
        //    string idno = (string)Session["idno"];

        //    // Retrieve all customers from the CUSTOMER table
        //    FirebaseResponse response = twoBigDB.Get("CUSTOMER");
        //    Dictionary<string, Customer> customerList = response.ResultAs<Dictionary<string, Customer>>();

        //    // Create the DataTable to hold the customer records
        //    DataTable customerTable = new DataTable();
        //    customerTable.Columns.Add("CUSTOMER ID");
        //    customerTable.Columns.Add("CUSTOMER NAME");
        //    customerTable.Columns.Add("CUSTOMER ADDRESS");
        //    customerTable.Columns.Add("CUSTOMER STATUS");
        //    customerTable.Columns.Add("CUSTOMER EMAIL");
        //    customerTable.Columns.Add("CUSTOMER CONTACT NUMBER");
        //    customerTable.Columns.Add("DATE REGISTERED");

        //    if (response != null && customerList != null)
        //    {
        //        foreach (var entry in customerList)
        //        {
        //            Customer customer = entry.Value;

        //            // Check if the customer has access to the station
        //            if (HasAccessToStation(customer, idno))
        //            {
        //                string dateRegistered = customer.dateRegistered == DateTime.MinValue ? "" : customer.dateRegistered.ToString("MMMM dd, yyyy hh:mm:ss tt");

        //                customerTable.Rows.Add(
        //                    customer.cusId,
        //                    customer.firstName + " " + customer.middleName + " " + customer.lastName,
        //                    customer.address,
        //                    customer.cus_status,
        //                    customer.email,
        //                    customer.phoneNumber,
        //                    dateRegistered
        //                );
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Handle null response or invalid selected value
        //        lblMessage.Text = "No data found";
        //    }

        //    // Bind the DataTable to the GridView
        //    gridCustomer_Details.DataSource = customerTable;
        //    gridCustomer_Details.DataBind();
        //}

        //private bool HasAccessToStation(Customer customer, string stationId)
        //{
        //    // Check if the customer has access to the station
        //    if (customer.orderedStore != null)
        //    {
        //        foreach (var store in customer.orderedStore)
        //        {
        //            if (store.adminId.ToString() == stationId)
        //            {
        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //}

    }

}