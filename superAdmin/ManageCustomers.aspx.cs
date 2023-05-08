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

namespace WRS2big_Web.superAdmin
{
    public partial class ManageCustomers : System.Web.UI.Page
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

            if (Session["email"] == null && Session["password"] == null)
            {
                Response.Write("<script>alert('Please login your account first'); window.location.href = '/superAdmin/Account.aspx'; </script>");
            }

            if (!IsPostBack)
            {
                DisplayAll();
                DisplayPending();
                DisplayApproved();
                DisplayDeclined();


                pendingGridView.Visible = false;
                approvedGridView.Visible = false;
                declinedGridView.Visible = false;

                selectAll.Visible = false;
                approveButton.Visible = false;
                declineButton.Visible = false;
                labelResponse.Visible = false;

                
            }
        }
        private void DisplayAll()
        {

            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer all = response.ResultAs<Model.Customer>();
            var data = response.Body;
            Dictionary<string, Model.Customer> Allclients = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(data);

            //creating the columns of the gridview
            DataTable clientsTable = new DataTable();
            clientsTable.Columns.Add("CUSTOMER ID");
            clientsTable.Columns.Add("CUSTOMER NAME");
            clientsTable.Columns.Add("EMAIL");
            clientsTable.Columns.Add("CONTACT #");
            clientsTable.Columns.Add("STATUS"); 

            if (Allclients == null)
            {
                labelResponse.Text = "No Registered Customers";
            }
            else
            {
                foreach (KeyValuePair<string, Model.Customer> entry in Allclients)
                {

                    clientsTable.Rows.Add(entry.Value.cusId, entry.Value.firstName + " " + entry.Value.lastName, entry.Value.email, entry.Value.phoneNumber, entry.Value.cus_status);

                }

            }


            // Bind DataTable to GridView control
            AllGridview.DataSource = clientsTable;
            AllGridview.DataBind();
            selectAll.Visible = false;
            approveButton.Visible = false;
            declineButton.Visible = false;
        }

        private void DisplayPending()
        {
            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer pending = response.ResultAs<Model.Customer>();
            var data = response.Body;
            Dictionary<string, Model.Customer> pendingCustomers = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(data);

            //creating the columns of the gridview
            DataTable pendingTable = new DataTable();
            pendingTable.Columns.Add("CUSTOMER ID");
            pendingTable.Columns.Add("CUSTOMER NAME");
            pendingTable.Columns.Add("EMAIL");
            pendingTable.Columns.Add("CONTACT #");
            pendingTable.Columns.Add("STATUS");

            foreach (KeyValuePair<string, Model.Customer> entry in pendingCustomers)
            {
                

                if (entry.Value.cus_status == "Pending")
                {
                    pendingTable.Rows.Add(entry.Value.cusId, entry.Value.firstName + " " + entry.Value.lastName, entry.Value.email, entry.Value.phoneNumber, entry.Value.cus_status);
                }
                else
                {

                    labelResponse.Text = "No 'PENDING' customers";
                    selectAll.Visible = false;
                    approveButton.Visible = false;
                    declineButton.Visible = false;
                }

            }
            // Bind DataTable to GridView control
            pendingGridView.DataSource = pendingTable;
            pendingGridView.DataBind();

            selectAll.Visible = true;
            approveButton.Visible = true;
            declineButton.Visible = true;
        }
        private void DisplayApproved()
        {
            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer pending = response.ResultAs<Model.Customer>();
            var data = response.Body;
            Dictionary<string, Model.Customer> pendingClients = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(data);

            //creating the columns of the gridview
            DataTable approvedTable = new DataTable();
            approvedTable.Columns.Add("CUSTOMER ID");
            approvedTable.Columns.Add("CUSTOMER NAME");
            approvedTable.Columns.Add("EMAIL");
            approvedTable.Columns.Add("CONTACT #");
            approvedTable.Columns.Add("STATUS");

            foreach (KeyValuePair<string, Model.Customer> entry in pendingClients)
            {
                
                if (entry.Value.cus_status != "Approved")
                {
                    labelResponse.Text = "No 'APPROVED' customers";
                }
                else if (entry.Value.cus_status == "Approved")
                {
                    approvedTable.Rows.Add(entry.Value.cusId, entry.Value.firstName + " " + entry.Value.lastName, entry.Value.email, entry.Value.phoneNumber, entry.Value.cus_status);

                }
                else
                {
                    labelResponse.Visible = false;
                }

            }
            // Bind DataTable to GridView control
            approvedGridView.DataSource = approvedTable;
            approvedGridView.DataBind();
            selectAll.Visible = false;
            approveButton.Visible = false;
            declineButton.Visible = false;
        }
        private void DisplayDeclined()
        {

            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer pending = response.ResultAs<Model.Customer>();
            var data = response.Body;
            Dictionary<string, Model.Customer> pendingClients = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(data);

            //creating the columns of the gridview
            DataTable approvedTable = new DataTable();
            approvedTable.Columns.Add("CUSTOMER ID");
            approvedTable.Columns.Add("CUSTOMER NAME");
            approvedTable.Columns.Add("EMAIL");
            approvedTable.Columns.Add("CONTACT #");
            approvedTable.Columns.Add("STATUS");

            foreach (KeyValuePair<string, Model.Customer> entry in pendingClients)
            {
                if (entry.Value.cus_status == "Declined")
                {
                    approvedTable.Rows.Add(entry.Value.cusId, entry.Value.firstName + " " + entry.Value.lastName, entry.Value.email, entry.Value.phoneNumber, entry.Value.cus_status);

                }
                else
                {
                    labelResponse.Text = "No 'DECLINED' customers";
                }

            }
            // Bind DataTable to GridView control
            declinedGridView.DataSource = approvedTable;
            declinedGridView.DataBind();
            selectAll.Visible = false;
            approveButton.Visible = false;
            declineButton.Visible = false;
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

        //multiple approve the customers
        protected void approveButton_Click(object sender, EventArgs e)
        {
            List<int> customerIDs = new List<int>();

            foreach (GridViewRow row in pendingGridView.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("select");
                if (chk != null && chk.Checked)
                {
                    int customerID = int.Parse(row.Cells[1].Text);
                    customerIDs.Add(customerID);
                }
            }

            if (customerIDs.Count == 0)
            {
                Response.Write("<script>alert ('Please select at least one customer to approve'); </script>");
                return;
            }

            foreach (int customerID in customerIDs)
            {
                FirebaseResponse response = twoBigDB.Get("CUSTOMER/" + customerID);
                Model.Customer customerDetails = response.ResultAs<Model.Customer>();


                customerDetails.cus_status = "Approved";
                response = twoBigDB.Update("CUSTOMER/" + customerID, customerDetails);

                ////SAVE TO SUPERADMIN 
                //var client = new Model.Customer
                //{
                //    cusId = customerDetails.cusId,
                //    firstName = customerDetails.firstName,
                //    lastName = customerDetails.lastName,
                //    phoneNumber = customerDetails.phoneNumber,
                //    email = customerDetails.email,
                //    dateApproved = DateTime.Now,
                //    dateRegistered = customerDetails.dateRegistered,
                //    userRole = customerDetails.userRole
                //};
                //SetResponse userResponse;
                //userResponse = twoBigDB.Set("SUPERADMIN/USERS/" + customerDetails.cusId, client);//Storing data to the database
                //Model.Customer user = userResponse.ResultAs<Model.Customer>();//Database Result


                Response.Write("<script>alert ('successfully approved!');  window.location.href = '/superAdmin/ManageCustomers.aspx'; </script>");
            }
        }
        protected void selectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selectAll = (CheckBox)sender;
            foreach (GridViewRow row in pendingGridView.Rows)
            {
                CheckBox select = (CheckBox)row.FindControl("select");
                select.Checked = selectAll.Checked;
            }
        }
        protected void declineButton_Click(object sender, EventArgs e)
        {
            List<int> customerIDs = new List<int>();

            foreach (GridViewRow row in pendingGridView.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("select");
                if (chk != null && chk.Checked)
                {
                    int customerID = int.Parse(row.Cells[1].Text);
                    customerIDs.Add(customerID);
                }
            }

            if (customerIDs.Count == 0)
            {
                Response.Write("<script>alert ('Please select at least one customer to approve'); </script>");
                return;
            }

            foreach (int customerID in customerIDs)
            {
                FirebaseResponse response = twoBigDB.Get("CUSTOMER/" + customerID);
                Model.Customer customerDetails = response.ResultAs<Model.Customer>();


                customerDetails.cus_status = "Declined";
                response = twoBigDB.Update("CUSTOMER/" + customerID, customerDetails);

                Response.Write("<script>alert ('successfully declined!');  window.location.href = '/superAdmin/ManageCustomers.aspx'; </script>");
            }
        }

        //protected void searchButton_Click(object sender, EventArgs e)
        //{
        //    // get the user input from search textbox
        //    string searched = search.Text;
        //    //get the selected value from the dropdown
        //    string selectedValue = sortDropdown.SelectedValue;

        //    FirebaseResponse response = twoBigDB.Get("CUSTOMER");
        //    Model.Customer all = response.ResultAs<Model.Customer>();
        //    var data = response.Body;
        //    Dictionary<string, Model.Customer> Allclients = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(data);

        //    //creating the columns of the gridview
        //    DataTable clientsTable = new DataTable();
        //    clientsTable.Columns.Add("CUSTOMER ID");
        //    clientsTable.Columns.Add("CUSTOMER NAME");
        //    clientsTable.Columns.Add("EMAIL");
        //    clientsTable.Columns.Add("CONTACT #");
        //    clientsTable.Columns.Add("STATUS");

        //    bool customerFound = false;
        //    foreach (KeyValuePair<string, Model.Customer> entry in Allclients)
        //    {
        //        if (selectedValue == "All" || entry.Value.cus_status == selectedValue && (searched == entry.Value.firstName || searched == entry.Value.lastName || searched == entry.Value.address || searched == entry.Value.email || searched == entry.Key))
        //        {
        //            clientsTable.Rows.Add(
        //                entry.Value.cusId,
        //                entry.Value.firstName + " " + entry.Value.lastName,
        //                entry.Value.email,
        //                entry.Value.phoneNumber,
        //                entry.Value.cus_status);

        //            customerFound = true;
        //        }
        //    }

        //    if (!customerFound)
        //    {
        //        Response.Write("<script>alert ('Customer not found!');  window.location.href = '/superAdmin/ManageCustomers.aspx'; </script>");
        //    }

        //    search.Text = "";

        //    if (selectedValue == "All")
        //    {
        //        AllGridview.DataSource = clientsTable;
        //        AllGridview.DataBind();
        //    }
        //    else if (selectedValue == "Pending")
        //    {
        //        pendingGridView.DataSource = clientsTable;
        //        pendingGridView.DataBind();
        //    }
        //    else if (selectedValue == "Approved")
        //    {
        //        approvedGridView.DataSource = clientsTable;
        //        approvedGridView.DataBind();
        //    }
        //    else if (selectedValue == "Declined")
        //    {
        //        declinedGridView.DataSource = clientsTable;
        //        declinedGridView.DataBind();
        //    }
        //}

        //protected void searchButton_Click(object sender, EventArgs e)
        //{
        //    string searched = search.Text;
        //    string selectedValue = sortDropdown.SelectedValue;

        //    FirebaseResponse response = twoBigDB.Get("CUSTOMER");
        //    Model.Customer all = response.ResultAs<Model.Customer>();
        //    var data = response.Body;
        //    Dictionary<string, Model.Customer> Allclients = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(data);

        //    //creating the columns of the gridview
        //    DataTable clientsTable = new DataTable();
        //    clientsTable.Columns.Add("CUSTOMER ID");
        //    clientsTable.Columns.Add("CUSTOMER NAME");
        //    clientsTable.Columns.Add("EMAIL");
        //    clientsTable.Columns.Add("CONTACT #");
        //    clientsTable.Columns.Add("STATUS");

        //    foreach (KeyValuePair<string, Model.Customer> entry in Allclients)
        //    {
        //        if ((selectedValue == "All" || selectedValue == entry.Value.cus_status) &&
        //            (searched == entry.Value.firstName ||
        //             searched == entry.Value.lastName ||
        //             searched == entry.Value.address ||
        //             searched == entry.Value.email ||
        //             searched == entry.Key))
        //        {
        //            clientsTable.Rows.Add(
        //                entry.Value.cusId,
        //                entry.Value.firstName + " " + entry.Value.lastName,
        //                entry.Value.email,
        //                entry.Value.phoneNumber,
        //                entry.Value.cus_status);
        //        }

        //    }

        //    if (clientsTable.Rows.Count == 0)
        //    {
        //        Response.Write("<script>alert ('Customer not found!'); window.location.href = '/superAdmin/ManageCustomers.aspx'; </script>");
        //    }
        //    else
        //    {
        //        search.Text = "";
        //        // Bind DataTable to GridView control
        //        AllGridview.DataSource = clientsTable;
        //        AllGridview.DataBind();

        //        // Show the appropriate gridview based on the selected value in the dropdown
        //        switch (selectedValue)
        //        {
        //            case "All":
        //                AllGridview.Visible = true;
        //                approvedGridView.Visible = false;
        //                pendingGridView.Visible = false;
        //                declinedGridView.Visible = false;
        //                break;
        //            case "Pending":
        //                AllGridview.Visible = false;
        //                approvedGridView.Visible = false;
        //                pendingGridView.Visible = true;
        //                declinedGridView.Visible = false;
        //                break;
        //            case "Approved":
        //                AllGridview.Visible = false;
        //                approvedGridView.Visible = true;
        //                pendingGridView.Visible = false;
        //                declinedGridView.Visible = false;
        //                break;
        //            case "Declined":
        //                AllGridview.Visible = false;
        //                approvedGridView.Visible = false;
        //                pendingGridView.Visible = false;
        //                declinedGridView.Visible = true;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}


        protected void searchButton_Click(object sender, EventArgs e)
        {
            string searched = search.Text;
            string selectedValue = sortDropdown.SelectedValue;

            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer all = response.ResultAs<Model.Customer>();
            var data = response.Body;
            Dictionary<string, Model.Customer> Allclients = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(data);

            //creating the columns of the gridview
            DataTable clientsTable = new DataTable();
            clientsTable.Columns.Add("CUSTOMER ID");
            clientsTable.Columns.Add("CUSTOMER NAME");
            clientsTable.Columns.Add("EMAIL");
            clientsTable.Columns.Add("CONTACT #");
            clientsTable.Columns.Add("STATUS");

            foreach (KeyValuePair<string, Model.Customer> entry in Allclients)
            {
                if ((selectedValue == "All" || selectedValue == entry.Value.cus_status) &&
                    (searched == entry.Value.firstName ||
                     searched == entry.Value.lastName ||
                     searched == entry.Value.address ||
                     searched == entry.Value.email ||
                     searched == entry.Key))
                {
                    clientsTable.Rows.Add(
                        entry.Value.cusId,
                        entry.Value.firstName + " " + entry.Value.lastName,
                        entry.Value.email,
                        entry.Value.phoneNumber,
                        entry.Value.cus_status);
                }
            }

            if (clientsTable.Rows.Count == 0)
            {
                Response.Write("<script>alert ('Customer not found!'); window.location.href = '/superAdmin/ManageCustomers.aspx';</script>");
            }

            search.Text = "";
            // Bind DataTable to GridView control based on selected value of dropdown
            if (selectedValue == "All")
            {
                AllGridview.DataSource = clientsTable;
                AllGridview.DataBind();
                AllGridview.Visible = true;
                approvedGridView.Visible = false;
                pendingGridView.Visible = false;
                declinedGridView.Visible = false;
            }
            else if (selectedValue == "Pending")
            {
                pendingGridView.DataSource = clientsTable;
                pendingGridView.DataBind();
                pendingGridView.Visible = true;
                AllGridview.Visible = false;
                approvedGridView.Visible = false;
                declinedGridView.Visible = false;
            }
            else if (selectedValue == "Approved")
            {
                approvedGridView.DataSource = clientsTable;
                approvedGridView.DataBind();
                approvedGridView.Visible = true;
                AllGridview.Visible = false;
                pendingGridView.Visible = false;
                declinedGridView.Visible = false;
            }
            else if (selectedValue == "Declined")
            {
                declinedGridView.DataSource = clientsTable;
                declinedGridView.DataBind();
                declinedGridView.Visible = true;
                AllGridview.Visible = false;
                approvedGridView.Visible = false;
                pendingGridView.Visible = false;
            }
        }



        protected void viewSorted_Click(object sender, EventArgs e)
        {
            string selectedValue = sortDropdown.SelectedValue;

            if (selectedValue == "All")
            {
                Response.Write("<script> window.location.href = '/superAdmin/ManageCustomers.aspx';</script>");


            }
            else if (selectedValue == "Pending")
            {

                search.Text = "";
                pendingGridView.Visible = true;
                AllGridview.Visible = false;
                approvedGridView.Visible = false;
                declinedGridView.Visible = false;

                //Display button and checkbox for SELECT ALL 
                selectAll.Visible = true;
                approveButton.Visible = true;
                declineButton.Visible = true;


            }
            else if (selectedValue == "Approved")
            {
               
                search.Text = "";
                pendingGridView.Visible = false;
                AllGridview.Visible = false;
                approvedGridView.Visible = true;
                declinedGridView.Visible = false;

            }
            else if (selectedValue == "Declined")
            {
               
                search.Text = "";
                pendingGridView.Visible = false;
                approvedGridView.Visible = false;
                AllGridview.Visible = false;
                declinedGridView.Visible = true;

            }
        }

        protected void closeButton_Click(object sender, EventArgs e)
        {

            string selectedValue = sortDropdown.SelectedValue;

            if (selectedValue == "All")
            {
                DisplayAll();
                pendingGridView.Visible = false;
                approvedGridView.Visible = false;
                declinedGridView.Visible = false;
            }
            else if (selectedValue == "Approved")
            {
                DisplayApproved();
                pendingGridView.Visible = false;
                declinedGridView.Visible = false;
                AllGridview.Visible = false;
            }
            else if (selectedValue == "Pending")
            {
                DisplayPending();
                declinedGridView.Visible = false;
                AllGridview.Visible = false;
                approvedGridView.Visible = false;
            }
            else if (selectedValue == "Declined")
            {
                DisplayDeclined();
                pendingGridView.Visible = false;
                approvedGridView.Visible = false;
                AllGridview.Visible = false;
            }

        }
    }
}