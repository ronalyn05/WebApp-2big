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
    public partial class RefillingStationReports : System.Web.UI.Page
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
                displayClients(); //All
                //displayEmployees(); //Employees
                //displaySubscribed(); //Subscribed 
                displayDeclined(); //Declined
                displayApproved();


                declinedClients.Visible = false;
                declinedLabel.Visible = false;
                approvedLabel.Visible = false;
                approvedClients.Visible = false;
                //subscribedClients.Visible = false;
                //subscribedLabel.Visible = false;
                //employeesLabel.Visible = false;
                //adminEmployees.Visible = false;
            }

        }
        private void displayApproved()
        {
            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount admin = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);


            DataTable customerTable = new DataTable();
            //customerTable.Columns.Add("PROFILE");
            customerTable.Columns.Add("USER ID");
            customerTable.Columns.Add("STATUS");
            customerTable.Columns.Add("NAME");
            customerTable.Columns.Add("EMAIL");
            customerTable.Columns.Add("ADDRESS");
            customerTable.Columns.Add("PHONE #");
            customerTable.Columns.Add("DATE EVALUATED");
            customerTable.Columns.Add("DATE REGISTERED");



            foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
            {
                //clientProfile.ImageUrl = entry.Value.profile_image;
                if (entry.Value.status == "Approved")
                {
                   

                    customerTable.Rows.Add(
                       //entry.Value.profile_image,
                       entry.Value.idno,
                        entry.Value.status,
                        entry.Value.fname + " " + entry.Value.lname,
                        entry.Value.email,
                       entry.Value.address,
                        entry.Value.phone,
                        entry.Value.dateApproved,
                        entry.Value.dateRegistered

                        );
                }

            }
            if (customerTable.Rows.Count == 0)
            {
                approvedLabel.Text = "NO 'APPROVED CLIENTS' FOUND";
                approvedLabel.Style["font-size"] = "24px";
                approvedLabel.Style["color"] = "black";
            }
            // Bind DataTable to GridView control
            approvedClients.DataSource = customerTable;
            approvedClients.DataBind();
        }
        //private void displaySubscribed()
        //{
        //    FirebaseResponse response = twoBigDB.Get("ADMIN");
        //    Model.AdminAccount admin = response.ResultAs<Model.AdminAccount>();
        //    var data = response.Body;
        //    Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

        //    DataTable customerTable = new DataTable();
        //    //customerTable.Columns.Add("PROFILE");
        //    customerTable.Columns.Add("USER ID");
        //    customerTable.Columns.Add("FIRST NAME");
        //    customerTable.Columns.Add("LAST NAME");
        //    customerTable.Columns.Add("EMAIL");
        //    customerTable.Columns.Add("ADDRESS");
        //    customerTable.Columns.Add("PHONE #");
        //    customerTable.Columns.Add("DATE REGISTERED");
        //    customerTable.Columns.Add("DATE SUBSCRIBED");
        //    customerTable.Columns.Add("PACKAGE");


        //    foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
        //    {
        //        //clientProfile.ImageUrl = entry.Value.profile_image;
        //        if (entry.Value.subStatus == "Subscribed")
        //        {
        //            FirebaseResponse station = twoBigDB.Get("ADMIN/" + entry.Value.idno + "/RefillingStation");
        //            Model.RefillingStation stations = station.ResultAs<Model.RefillingStation>();

        //            station = twoBigDB.Get("ADMIN/" + entry.Value.idno + "/Subscribed_Package");
        //            Model.Subscribed_Package package = station.ResultAs<Model.Subscribed_Package>();


        //            customerTable.Rows.Add(
        //               //entry.Value.profile_image,
        //               entry.Value.idno,
        //                entry.Value.fname,
        //                entry.Value.lname,
        //                entry.Value.email,
        //               stations.stationAddress,
        //                entry.Value.phone,
        //                entry.Value.dateRegistered,
        //                package.subStart,
        //                package.packageName);
        //        }

        //    }
        //    // Bind DataTable to GridView control
        //    subscribedClients.DataSource = customerTable;
        //    subscribedClients.DataBind();
        //}
        
        private void displayDeclined()
        {
            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount admin = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);


            DataTable customerTable = new DataTable();
            //customerTable.Columns.Add("PROFILE");
            customerTable.Columns.Add("USER ID");
            customerTable.Columns.Add("STATUS");
            customerTable.Columns.Add("NAME");
            customerTable.Columns.Add("EMAIL");
            customerTable.Columns.Add("ADDRESS");
            customerTable.Columns.Add("PHONE #");
            customerTable.Columns.Add("DATE EVALUATED");
            customerTable.Columns.Add("DATE REGISTERED");



            foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
            {
                //clientProfile.ImageUrl = entry.Value.profile_image;
                if (entry.Value.status == "Declined")
                {

                    customerTable.Rows.Add(
                       //entry.Value.profile_image,
                       entry.Value.idno,
                       entry.Value.status,
                        entry.Value.fname + " " + entry.Value.lname,
                        entry.Value.email,
                       entry.Value.address,
                        entry.Value.phone,
                        entry.Value.dateDeclined,
                        entry.Value.dateRegistered
                        
                        );
                }

            }

            if (customerTable.Rows.Count == 0)
            {

                declinedLabel.Text = "NO 'DECLINED CLIENTS' FOUND";
                declinedLabel.Style["font-size"] = "24px";
                declinedLabel.Style["color"] = "red";

            }
            // Bind DataTable to GridView control
            declinedClients.DataSource = customerTable;
            declinedClients.DataBind();
        }
        //private void displayEmployees()
        //{
        //    FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
        //    Model.Employee admin = response.ResultAs<Model.Employee>();
        //    var data = response.Body;
        //    Dictionary<string, Model.Employee> employees = JsonConvert.DeserializeObject<Dictionary<string, Model.Employee>>(data);

        //    if (employees != null)
        //    {
        //        DataTable customerTable = new DataTable();
        //        //customerTable.Columns.Add("PROFILE");
        //        customerTable.Columns.Add("USER ID");
        //        customerTable.Columns.Add("ROLE");
        //        customerTable.Columns.Add("STATUS");
        //        customerTable.Columns.Add("NAME");
        //        customerTable.Columns.Add("EMAIL");
        //        customerTable.Columns.Add("ADDRESS");
        //        customerTable.Columns.Add("PHONE #");
        //        customerTable.Columns.Add("EMPLOYER");
        //        customerTable.Columns.Add("DATE HIRED");
        //        customerTable.Columns.Add("DATE ADDED");

        //        foreach (KeyValuePair<string, Model.Employee> driver in employees)
        //        {
        //                if (driver.Value.emp_role == "Driver" || driver.Value.emp_role == "Cashier")
        //            {
        //                customerTable.Rows.Add(
        //                  //entry.Value.profile_image,
        //                  driver.Value.emp_id,
        //                  driver.Value.emp_role,
        //                  driver.Value.emp_status,
        //                   driver.Value.emp_firstname + " " + driver.Value.emp_lastname,
        //                   driver.Value.emp_email,
        //                  driver.Value.emp_address,
        //                  driver.Value.emp_contactnum,
        //                  driver.Value.addedBy,
        //                  driver.Value.emp_dateHired,
        //                  driver.Value.dateAdded);
        //            }
        //        }
        //        adminEmployees.DataSource = customerTable;
        //        adminEmployees.DataBind();
        //    }
        //}

        protected void displayClients()
        {
            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount admin = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

            if (clients != null)
            {
                DataTable customerTable = new DataTable();
                //customerTable.Columns.Add("PROFILE");
                customerTable.Columns.Add("USER ID");
                customerTable.Columns.Add("STATUS");
                customerTable.Columns.Add("NAME");
                customerTable.Columns.Add("EMAIL");
                customerTable.Columns.Add("ADDRESS");
                customerTable.Columns.Add("PHONE #");
                customerTable.Columns.Add("DATE EVALUATED");
                customerTable.Columns.Add("DATE REGISTERED");




                foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
                {
                    //clientProfile.ImageUrl = entry.Value.profile_image;

                    FirebaseResponse station = twoBigDB.Get("ADMIN/" + entry.Value.idno + "/RefillingStation");
                    Model.RefillingStation stations = station.ResultAs<Model.RefillingStation>();

                    if (stations != null)
                    {
                        if (entry.Value.status != "Pending")
                        {
                            if (entry.Value.status == "Approved")
                            {
                                customerTable.Rows.Add(
                                   //entry.Value.profile_image,
                                   entry.Value.idno,
                                   entry.Value.status,
                                    entry.Value.fname + " " + entry.Value.lname,
                                    entry.Value.email,
                                   stations.stationAddress,
                                    entry.Value.phone,
                                    entry.Value.dateApproved,
                                    entry.Value.dateRegistered);
                            }
                            else
                            {
                                customerTable.Rows.Add(
                                   //entry.Value.profile_image,
                                   entry.Value.idno,
                                   entry.Value.status,
                                    entry.Value.fname + " " + entry.Value.lname,
                                    entry.Value.email,
                                   stations.stationAddress,
                                    entry.Value.phone,
                                    entry.Value.dateDeclined,
                                    entry.Value.dateRegistered);
                            }

                        }


                    }

                }
                if (customerTable.Rows.Count == 0)
                {
                    allClientsLabel.Text = "NO 'CLIENTS' FOUND";
                    allClientsLabel.Style["font-size"] = "24px";
                    allClientsLabel.Style["color"] = "black";
                }
                // Bind DataTable to GridView control
                allClients.DataSource = customerTable;
                allClients.DataBind();

            }


        }

        //protected void searchButton_Click(object sender, EventArgs e)
        //{
        //    string searched = search.Text;
        //    string selectedValue = sortDropdown.SelectedValue;

        //    //FETCH THE CLIENTS
        //    FirebaseResponse clientsResponse = twoBigDB.Get("ADMIN");
        //    Model.AdminAccount admin = clientsResponse.ResultAs<Model.AdminAccount>();
        //    var clientData = clientsResponse.Body;
        //    Dictionary<string, Model.AdminAccount> clientAdmin = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(clientData);

        //    DataTable customerTable = new DataTable();
        //    customerTable.Columns.Add("USER ID");
        //    customerTable.Columns.Add("FIRST NAME");
        //    customerTable.Columns.Add("LAST NAME");
        //    customerTable.Columns.Add("EMAIL");
        //    customerTable.Columns.Add("ADDRESS");
        //    customerTable.Columns.Add("PHONE #");

        //    foreach (KeyValuePair<string, Model.AdminAccount> adminEntry in clientAdmin)
        //    {
        //        if (searched == adminEntry.Value.fname)
        //        {
        //            customerTable.Rows.Add(
        //          //entry.Value.profile_image,
        //          adminEntry.Value.idno,
        //           adminEntry.Value.fname,
        //           adminEntry.Value.lname,
        //           adminEntry.Value.email,
        //           adminEntry.Value.address,
        //          adminEntry.Value.phone
        //          );

        //            //customerReports.Visible = false;
        //            clientReports.Visible = true;
        //            clientsLabel.Visible = true;
        //            //customersLabel.Visible = false;
        //        }
        //        else
        //        {
        //            searchEmployees();
        //        }
        //    }
        //    // Bind DataTable to GridView control
        //    clientReports.DataSource = customerTable;
        //    clientReports.DataBind();

        //}

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string searched = search.Text;
            string selectedValue = sortDropdown.SelectedValue;

            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount all = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> Allclients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

            //creating the columns of the gridview
            DataTable clientsTable = new DataTable();
            clientsTable.Columns.Add("USER ID");
            clientsTable.Columns.Add("FIRST NAME");
            clientsTable.Columns.Add("LAST NAME");
            clientsTable.Columns.Add("EMAIL");
            clientsTable.Columns.Add("ADDRESS");
            clientsTable.Columns.Add("PHONE #");

            foreach (KeyValuePair<string, Model.AdminAccount> entry in Allclients)
            {
               
                if ((selectedValue == "All" || selectedValue == entry.Value.status) &&
                    (searched == entry.Value.fname || searched == entry.Value.lname  || searched == entry.Value.email || searched == entry.Key))
                {
                    clientsTable.Rows.Add(
                        entry.Value.idno,
                        entry.Value.fname,
                        entry.Value.lname,
                        entry.Value.email,
                        entry.Value.address,
                        entry.Value.phone
                        );
                }
            }

            if (clientsTable.Rows.Count == 0)
            {

                Response.Write("<script>alert ('Client not found!'); window.location.href = '/superAdmin/RefillingStationReports.aspx'; </script>");

            }

            //search.Text = "";
            // Bind DataTable to GridView control based on selected value of dropdown
            if (selectedValue == "All")
            {
                allClients.DataSource = clientsTable;
                allClients.DataBind();
                allClients.Visible = true;
                allClientsLabel.Visible = true;
                declinedClients.Visible = false;
                declinedLabel.Visible = false;
                approvedClients.Visible = false;
                approvedClients.Visible = false;
                //declinedGridView.Visible = false;
            }
            else if (selectedValue == "Declined")
            {
                declinedClients.DataSource = clientsTable;
                declinedClients.DataBind();
                declinedClients.Visible = true;
                declinedLabel.Visible = true;
                approvedClients.Visible = false;
                approvedClients.Visible = false;
                allClients.Visible = false;
                allClientsLabel.Visible = false;
            }
            else if (selectedValue == "Approved")
            {
                approvedClients.DataSource = clientsTable;
                approvedClients.DataBind();
                approvedClients.Visible = true;
                approvedLabel.Visible = true;
                declinedClients.Visible = false;
                declinedLabel.Visible = false;
                allClients.Visible = false;
                allClientsLabel.Visible = false;
            }

        }
        //private void searchEmployees()
        //{
        //    string searched = search.Text;

        //    FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
        //    Model.Employee admin = response.ResultAs<Model.Employee>();
        //    var data = response.Body;
        //    Dictionary<string, Model.Employee> employees = JsonConvert.DeserializeObject<Dictionary<string, Model.Employee>>(data);

        //    if (employees != null)
        //    {
        //        DataTable customerTable = new DataTable();
        //        //customerTable.Columns.Add("PROFILE");
        //        customerTable.Columns.Add("USER ID");
        //        customerTable.Columns.Add("ROLE");
        //        customerTable.Columns.Add("STATUS");
        //        customerTable.Columns.Add("NAME");
        //        customerTable.Columns.Add("EMAIL");
        //        customerTable.Columns.Add("ADDRESS");
        //        customerTable.Columns.Add("PHONE #");
        //        customerTable.Columns.Add("EMPLOYER");
        //        customerTable.Columns.Add("DATE HIRED");
        //        customerTable.Columns.Add("DATE ADDED");

        //        foreach (KeyValuePair<string, Model.Employee> driver in employees)
        //        {

        //            if (driver.Value.emp_role == "Driver" || driver.Value.emp_role == "Cashier")
        //            {
        //                if (searched == driver.Value.emp_firstname || searched == driver.Value.emp_lastname || searched == driver.Value.emp_id.ToString())
        //                {
        //                    customerTable.Rows.Add(
        //                      //entry.Value.profile_image,
        //                      driver.Value.emp_id,
        //                      driver.Value.emp_role,
        //                      driver.Value.emp_status,
        //                       driver.Value.emp_firstname + " " + driver.Value.emp_lastname,
        //                       driver.Value.emp_email,
        //                      driver.Value.emp_address,
        //                      driver.Value.emp_contactnum,
        //                      driver.Value.addedBy,
        //                      driver.Value.emp_dateHired,
        //                      driver.Value.dateAdded);
        //                }

        //            }
        //        }
        //        adminEmployees.DataSource = customerTable;
        //        adminEmployees.DataBind();
        //    }
        //}

        protected void viewSorted_Click(object sender, EventArgs e)
        {
            string selectedValue = sortDropdown.SelectedValue;

            if (selectedValue == "Approved")
            {

                approvedLabel.Visible = true;
                approvedClients.Visible = true;

                declinedClients.Visible = false;
                declinedLabel.Visible = false;
                allClients.Visible = false;
                allClientsLabel.Visible = false;

            }

            else if (selectedValue == "Declined")
            {
                //subscribedClients.Visible = false;
                approvedClients.Visible = false;
                allClients.Visible = false;
                allClientsLabel.Visible = false;
                approvedLabel.Visible = false;

                declinedClients.Visible = true;
                declinedLabel.Visible = true;

                //subscribedLabel.Visible = false;

            }
            else if (selectedValue == "All")
            {

                approvedClients.Visible = false;
                approvedLabel.Visible = false;
                declinedClients.Visible = false;
                declinedLabel.Visible = false;

                allClients.Visible = true;
                allClientsLabel.Visible = true;

            }


        }

        protected void closeButton_Click(object sender, EventArgs e)
        {
            search.Text = "";
            string selectedValue = sortDropdown.SelectedValue;


             if (selectedValue == "Declined")
            {
                displayDeclined();

                allClients.Visible = false;
                allClientsLabel.Visible = false;

                declinedLabel.Visible = true;
                declinedClients.Visible = true;


            }
            else if (selectedValue == "Approved")
            {
                displayApproved();

                allClientsLabel.Visible = false;
                allClients.Visible = false;
                declinedClients.Visible = false;
                declinedLabel.Visible = false;
                approvedLabel.Visible = true;
                approvedClients.Visible = true;
            }
             else if (selectedValue == "All")
            {
                displayClients();
                approvedClients.Visible = false;
                approvedLabel.Visible = false;
                declinedClients.Visible = false;
                declinedLabel.Visible = false;
            }


        }
        protected void detailsButton_Click(object sender, EventArgs e)
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

            Response.Write("<script>window.location.href = '/superAdmin/clientDetails.aspx'; </script>");

        }
    }
}