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
using System.Net;
using Newtonsoft.Json.Linq;

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

          
          

            if (!IsPostBack)
            {
                displayCustomers();
                displayApproved();
                displayDeclined();

                declinedCustomers.Visible = false;
                declinedLabel.Visible = false;
                approvedCustomers.Visible = false;
                approvedLabel.Visible = false;

            }

        }
        
     
        //APPROVED CUSTOMERS
        private void displayApproved()
        {
            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer plan = response.ResultAs<Model.Customer>();
            var customer = response.Body;
            Dictionary<string, Model.Customer> customers = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(customer);

            if (customers != null)
            {
                DataTable customerTable = new DataTable();
                customerTable.Columns.Add("USER ID");
                customerTable.Columns.Add("ROLE");
                customerTable.Columns.Add("STATUS");
                customerTable.Columns.Add("NAME");
                customerTable.Columns.Add("EMAIL");
                customerTable.Columns.Add("ADDRESS");
                customerTable.Columns.Add("PHONE #");
                customerTable.Columns.Add("DATE APPROVED");
                customerTable.Columns.Add("DATE REGISTERED");

                foreach (KeyValuePair<string, Model.Customer> entry in customers)
                {
                    string role = "Customer";
                    if (entry.Value.cus_status == "Approved")
                    {
                        customerTable.Rows.Add(
                           entry.Value.cusId,
                           role,
                           entry.Value.cus_status,
                           entry.Value.firstName + " " + entry.Value.lastName,
                           entry.Value.email,
                           entry.Value.address,
                           entry.Value.phoneNumber,
                           entry.Value.dateApproved,
                           entry.Value.dateRegistered);
                    }

                }
                if (customerTable.Rows.Count == 0)
                {
                    approvedLabel.Text = "NO 'APPROVED CUSTOMERS' FOUND";
                    approvedLabel.Style["font-size"] = "24px";
                    approvedLabel.Style["color"] = "red";
                }
                // Bind DataTable to GridView control
                approvedCustomers.DataSource = customerTable;
                approvedCustomers.DataBind();
            }
        }
        //DECLINED CUSTOMERS
        private void displayDeclined()
        {
            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer plan = response.ResultAs<Model.Customer>();
            var customer = response.Body;
            Dictionary<string, Model.Customer> customers = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(customer);

            if (customers != null)
            {
                DataTable customerTable = new DataTable();
                customerTable.Columns.Add("USER ID");
                customerTable.Columns.Add("STATUS");
                customerTable.Columns.Add("NAME");
                customerTable.Columns.Add("EMAIL");
                customerTable.Columns.Add("ADDRESS");
                customerTable.Columns.Add("PHONE #");
                customerTable.Columns.Add("DATE APPROVED");
                customerTable.Columns.Add("DATE REGISTERED");

                foreach (KeyValuePair<string, Model.Customer> entry in customers)
                {
                    if (entry.Value.cus_status == "Declined")
                    {
                        customerTable.Rows.Add(
                           entry.Value.cusId,
                           entry.Value.cus_status,
                           entry.Value.firstName + " " + entry.Value.lastName,
                           entry.Value.email,
                           entry.Value.address,
                           entry.Value.phoneNumber,
                           entry.Value.dateDeclined,
                           entry.Value.dateRegistered);
                    }

                }
                if (customerTable.Rows.Count == 0)
                {
                    declinedLabel.Text = "NO 'DECLINED CUSTOMERS' FOUND";
                    declinedLabel.Style["font-size"] = "24px";
                    declinedLabel.Style["color"] = "red";
                }
                // Bind DataTable to GridView control
                declinedCustomers.DataSource = customerTable;
                declinedCustomers.DataBind();
            }
        }
        //ALL CUSTOMERS 
        protected void displayCustomers()
        {

            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer plan = response.ResultAs<Model.Customer>();
            var customer = response.Body;
            Dictionary<string, Model.Customer> customers = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(customer);

            if (customers != null)
            {
                DataTable customerTable = new DataTable();
                customerTable.Columns.Add("USER ID");
                customerTable.Columns.Add("STATUS");
                customerTable.Columns.Add("NAME");
                customerTable.Columns.Add("EMAIL");
                customerTable.Columns.Add("ADDRESS");
                customerTable.Columns.Add("PHONE #");
                customerTable.Columns.Add("DATE APPROVED");
                customerTable.Columns.Add("DATE REGISTERED");

                foreach (KeyValuePair<string, Model.Customer> entry in customers)
                {
                    string role = "Customer";
                    if (entry.Value.cus_status != "Pending")
                    {
                        if (entry.Value.cus_status == "Approved")
                        {
                            customerTable.Rows.Add(
                               entry.Value.cusId,
                               entry.Value.cus_status,
                               entry.Value.firstName + " " + entry.Value.lastName,
                               entry.Value.email,
                               entry.Value.address,
                               entry.Value.phoneNumber,
                               entry.Value.dateApproved,
                               entry.Value.dateRegistered);
                        }
                        else
                        {
                            customerTable.Rows.Add(
                               entry.Value.cusId,
                               role,
                               entry.Value.cus_status,
                               entry.Value.firstName + " " + entry.Value.lastName,
                               entry.Value.email,
                               entry.Value.address,
                               entry.Value.phoneNumber,
                               entry.Value.dateDeclined,
                               entry.Value.dateRegistered);
                        }

                    }

                }
                if (customerTable.Rows.Count == 0)
                {
                    customersLabel.Text = "NO 'CUSTOMERS' FOUND";
                    customersLabel.Style["font-size"] = "24px";
                    customersLabel.Style["color"] = "red";
                }
                // Bind DataTable to GridView control
                allCustomers.DataSource = customerTable;
                allCustomers.DataBind();
            }
        }



        protected void searchButton_Click(object sender, EventArgs e)
        {
            string searched = search.Text;
            string selectedValue = sortDropdown.SelectedValue;

            //FETCH IN CUSTOMERS
            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer plan = response.ResultAs<Model.Customer>();
            var data = response.Body;
            Dictionary<string, Model.Customer> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(data);

           
            DataTable customerTable = new DataTable();
            customerTable.Columns.Add("USER ID");
            customerTable.Columns.Add("FIRST NAME");
            customerTable.Columns.Add("LAST NAME");
            customerTable.Columns.Add("EMAIL");
            customerTable.Columns.Add("ADDRESS");
            customerTable.Columns.Add("PHONE #");


            foreach (KeyValuePair<string, Model.Customer> entry in clients)
            {
                if ((selectedValue == "All" || selectedValue == entry.Value.cus_status) &&
                  (searched == entry.Value.firstName || searched == entry.Value.lastName || searched == entry.Value.email || searched == entry.Key))
                {

                    customerTable.Rows.Add(
                    //entry.Value.imageProof,
                    entry.Value.cusId,
                    entry.Value.firstName,
                    entry.Value.lastName,
                    entry.Value.email,
                    entry.Value.address,
                    entry.Value.phoneNumber);
                }

            }
            if (customerTable.Rows.Count == 0)
            {
                Response.Write("<script>alert ('Customer not found!'); window.location.href = '/superAdmin/CustomerReports.aspx'; </script>");

            }

            if (selectedValue == "All")
            {
                allCustomers.DataSource = customerTable;
                allCustomers.DataBind();
                allCustomers.Visible = true;


                customersLabel.Visible = true;
                declinedCustomers.Visible = false;
                declinedLabel.Visible = false;
                approvedCustomers.Visible = false;
                approvedLabel.Visible = false;
                //declinedGridView.Visible = false;
            }
            else if (selectedValue == "Declined")
            {
                declinedCustomers.DataSource = customerTable;
                declinedCustomers.DataBind();
                declinedCustomers.Visible = true;
                declinedLabel.Visible = true;

                approvedCustomers.Visible = false;
                approvedLabel.Visible = false;
                allCustomers.Visible = false;
                customersLabel.Visible = false;
            }
            else if (selectedValue == "Approved")
            {
                approvedCustomers.DataSource = customerTable;
                approvedCustomers.DataBind();
                approvedCustomers.Visible = true;

                approvedLabel.Visible = true;
                declinedCustomers.Visible = false;
                declinedLabel.Visible = false;
                allCustomers.Visible = false;
                customersLabel.Visible = false;
            }
        }
       
       

        protected void viewSorted_Click(object sender, EventArgs e)
        {
            string selectedValue = sortDropdown.SelectedValue;
          
            if (selectedValue == "All")
            {
                allCustomers.Visible = true;
                //clientReports.Visible = true;
                //clientsLabel.Visible = true;   
                customersLabel.Visible = true;

                approvedCustomers.Visible = false;
                approvedLabel.Visible = false;
                declinedCustomers.Visible = false;
                declinedLabel.Visible = false;


            }
            else if (selectedValue == "Approved")
            {
                approvedCustomers.Visible = true;
                approvedLabel.Visible = true;


                declinedCustomers.Visible = false;
                declinedLabel.Visible = false;
                allCustomers.Visible = false;
                customersLabel.Visible = false;
            }
         
            else if (selectedValue == "Declined")
            {
                approvedCustomers.Visible = false;
                allCustomers.Visible = false;
                customersLabel.Visible = false;
                approvedLabel.Visible = false;

                declinedCustomers.Visible = true;
                declinedLabel.Visible = true;
            }


        }

        protected void closeButton_Click(object sender, EventArgs e)
        {
            search.Text = "";
            string selectedValue = sortDropdown.SelectedValue;


            if (selectedValue == "Declined")
            {
                displayDeclined();

                allCustomers.Visible = false;
                customersLabel.Visible = false;

                declinedLabel.Visible = true;
                declinedCustomers.Visible = true;


            }
            else if (selectedValue == "Approved")
            {
                displayApproved();

                customersLabel.Visible = false;
                allCustomers.Visible = false;
                declinedCustomers.Visible = false;
                declinedLabel.Visible = false;
                approvedLabel.Visible = true;
                approvedCustomers.Visible = true;
            }
            else if (selectedValue == "All")
            {
                displayCustomers();
                approvedCustomers.Visible = false;
                approvedLabel.Visible = false;
                declinedCustomers.Visible = false;
                declinedLabel.Visible = false;
            }


        }
        protected void detailsButton_Click(object sender, EventArgs e)
        {

            //Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            //Get the order ID from the first cell in the row
            int customerID = int.Parse(row.Cells[1].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("CUSTOMER/" + customerID);
            Model.Customer customerDetails = response.ResultAs<Model.Customer>();

            int currentClient = customerDetails.cusId;
            Session["currentCustomer"] = currentClient;

            Response.Write("<script>window.location.href = '/superAdmin/customerDetails.aspx'; </script>");

        }
    }
}
