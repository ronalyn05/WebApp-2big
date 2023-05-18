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

           displayCustomers();
           displayClients();
           displayDriverUser();

            if (!IsPostBack)
            {
                //BindData("");
            }

        }
        private void displayDriverUser()
        {
            FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
            Model.Employee admin = response.ResultAs<Model.Employee>();
            var data = response.Body;
            Dictionary<string, Model.Employee> employees = JsonConvert.DeserializeObject<Dictionary<string, Model.Employee>>(data);

            if (employees != null)
            {
                DataTable customerTable = new DataTable();
                //customerTable.Columns.Add("PROFILE");
                customerTable.Columns.Add("USER ID");
                customerTable.Columns.Add("ROLE");
                customerTable.Columns.Add("STATUS");
                customerTable.Columns.Add("NAME");
                customerTable.Columns.Add("EMAIL");
                customerTable.Columns.Add("ADDRESS");
                customerTable.Columns.Add("PHONE #");
                customerTable.Columns.Add("EMPLOYER");
                customerTable.Columns.Add("DATE HIRED");
                customerTable.Columns.Add("DATE ADDED");

                foreach (KeyValuePair<string, Model.Employee> driver in employees)
                {
                    if (driver.Value.emp_role == "Driver" || driver.Value.emp_role == "Cashier")
                    {
                        customerTable.Rows.Add(
                          //entry.Value.profile_image,
                          driver.Value.emp_id,
                          driver.Value.emp_role,
                          driver.Value.emp_status,
                           driver.Value.emp_firstname + " " + driver.Value.emp_lastname,
                           driver.Value.emp_email,
                          driver.Value.emp_address,
                          driver.Value.emp_contactnum,
                          driver.Value.addedBy,
                          driver.Value.emp_dateHired,
                          driver.Value.dateAdded); 
                    }
                }
                adminEmployees.DataSource = customerTable;
                adminEmployees.DataBind();
            }
        }
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
                customerTable.Columns.Add("ROLE");
                customerTable.Columns.Add("STATUS");
                customerTable.Columns.Add("NAME");
                customerTable.Columns.Add("EMAIL");
                customerTable.Columns.Add("ADDRESS");
                customerTable.Columns.Add("PHONE #");
                customerTable.Columns.Add("DATE APPROVED");
                customerTable.Columns.Add("DATE REGISTERED");




                foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
                {
                    //clientProfile.ImageUrl = entry.Value.profile_image;

                    FirebaseResponse station = twoBigDB.Get("ADMIN/" + entry.Value.idno + "/RefillingStation");
                    Model.RefillingStation stations = station.ResultAs<Model.RefillingStation>();

                    if (stations != null)
                    {
                        customerTable.Rows.Add(
                           //entry.Value.profile_image,
                           entry.Value.idno,
                           entry.Value.userRole,
                           entry.Value.status,
                            entry.Value.fname + " " + entry.Value.lname,
                            entry.Value.email,
                           stations.stationAddress,
                            entry.Value.phone,
                            entry.Value.dateApproved,
                            entry.Value.dateRegistered);

                    }
                
                }
                // Bind DataTable to GridView control
                clientReports.DataSource = customerTable;
                clientReports.DataBind();

            }

           
        }


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
                    double lattitude = entry.Value.lattitudeLocation;
                    double longitude = entry.Value.longitudeLocation;
                    string apiKey = "AIzaSyBqKUBIswNi5uO3xOh4Boo8kSJyJ3DLkhk";
                    string role = "Customer";

                    string address = GetAddressFromLatLong(lattitude, longitude, apiKey);

                    //customerProfile.ImageUrl = entry.Value.imageProof;

                    
                        customerTable.Rows.Add(
                                               //entry.Value.imageProof,
                                               entry.Value.cusId,
                                               role,
                                               entry.Value.cus_status,
                                               entry.Value.firstName + " " + entry.Value.lastName,
                                               entry.Value.email,
                                               address,
                                               entry.Value.phoneNumber,
                                               entry.Value.dateApproved,
                                               entry.Value.dateRegistered);
                    
                   

                }
                // Bind DataTable to GridView control
                customerReports.DataSource = customerTable;
                customerReports.DataBind();
            }

           

        }


        //TO REVERSE THE COORDINATES INTO ADDRESS
        public static string GetAddressFromLatLong(double latitude, double longitude, string apiKey)
        {
            string url = string.Format("https://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&key={2}", latitude, longitude, apiKey);
            string result = string.Empty;

            using (var client = new WebClient())
            {
                result = client.DownloadString(url);
            }

            JObject json = JObject.Parse(result);
            string address = json["results"][0]["formatted_address"].ToString();

            return address;
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string searched = search.Text;


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
                if (searched == entry.Value.firstName)
                {
                    double lattitude = entry.Value.lattitudeLocation;
                    double longitude = entry.Value.longitudeLocation;
                    string apiKey = "AIzaSyBqKUBIswNi5uO3xOh4Boo8kSJyJ3DLkhk";

                    string address = GetAddressFromLatLong(lattitude, longitude, apiKey);

                    customerTable.Rows.Add(
                    //entry.Value.imageProof,
                    entry.Value.cusId,
                    entry.Value.firstName,
                    entry.Value.lastName,
                    entry.Value.email,
                    address,
                    entry.Value.phoneNumber);

                    customerReports.Visible = true;
                    clientReports.Visible = false;
                    clientsLabel.Visible = false;
                    customersLabel.Visible = true;
                }
                else
                {
                    checkClient();
                }
            }

            // Bind DataTable to GridView control
            customerReports.DataSource = customerTable;
            customerReports.DataBind();
        }
       
        private void checkClient()
        {
            string searched = search.Text;

            //FETCH THE CLIENTS
            FirebaseResponse clientsResponse = twoBigDB.Get("ADMIN");
            Model.AdminAccount admin = clientsResponse.ResultAs<Model.AdminAccount>();
            var clientData = clientsResponse.Body;
            Dictionary<string, Model.AdminAccount> clientAdmin = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(clientData);

            DataTable customerTable = new DataTable();
            customerTable.Columns.Add("USER ID");
            customerTable.Columns.Add("FIRST NAME");
            customerTable.Columns.Add("LAST NAME");
            customerTable.Columns.Add("EMAIL");
            customerTable.Columns.Add("ADDRESS");
            customerTable.Columns.Add("PHONE #");

            foreach (KeyValuePair<string, Model.AdminAccount> adminEntry in clientAdmin)
            {
                if (searched == adminEntry.Value.fname)
                {
                    customerTable.Rows.Add(
                  //entry.Value.profile_image,
                  adminEntry.Value.idno,
                   adminEntry.Value.fname,
                   adminEntry.Value.lname,
                   adminEntry.Value.email,
                   adminEntry.Value.address,
                  adminEntry.Value.phone
                  );

                    customerReports.Visible = false;
                    clientReports.Visible = true;
                    clientsLabel.Visible = true;
                    customersLabel.Visible = false;
                }
            }
            // Bind DataTable to GridView control
            clientReports.DataSource = customerTable;
            clientReports.DataBind();
        }

        protected void viewSorted_Click(object sender, EventArgs e)
        {
            string selectedValue = sortDropdown.SelectedValue;
          
            if (selectedValue == "All")
            {
                customerReports.Visible = true;
                clientReports.Visible = true;
                clientsLabel.Visible = true;   
                customersLabel.Visible = true;
            }
            else if (selectedValue == "Registered Customers")
            {
                customerReports.Visible = true;
                clientReports.Visible = false;
                clientsLabel.Visible = false;
                customersLabel.Visible = true;
            }
            else if (selectedValue == "Registered Clients")
            {
                customerReports.Visible = false;
                clientsLabel.Visible = true;
                customersLabel.Visible = false;
                clientReports.Visible = true;
            }
            else if (selectedValue == "Subscribed Clients")
            {
                //hide other grids
                customerReports.Visible = false;
                customersLabel.Visible = false;



                FirebaseResponse response = twoBigDB.Get("ADMIN");
                Model.AdminAccount admin = response.ResultAs<Model.AdminAccount>();
                var data = response.Body;
                Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);


                //response = twoBigDB.Get("SUPERADMIN/SUBSCRIBED_CLIENTS");
                //Model.superAdminClients subscribedClients = response.ResultAs<Model.superAdminClients>();
                //var clientData = response.Body;
                //Dictionary<string, Model.superAdminClients> subscribed = response.ResultAs<Dictionary<string, Model.superAdminClients>>();


                DataTable customerTable = new DataTable();
                //customerTable.Columns.Add("PROFILE");
                customerTable.Columns.Add("USER ID");
                customerTable.Columns.Add("FIRST NAME");
                customerTable.Columns.Add("LAST NAME");
                customerTable.Columns.Add("EMAIL");
                customerTable.Columns.Add("ADDRESS");
                customerTable.Columns.Add("PHONE #");
                customerTable.Columns.Add("DATE REGISTERED");
                customerTable.Columns.Add("DATE SUBSCRIBED");
                customerTable.Columns.Add("PACKAGE");


                foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
                {
                    //clientProfile.ImageUrl = entry.Value.profile_image;
                    if (entry.Value.subStatus == "Subscribed")
                    {
                        FirebaseResponse station = twoBigDB.Get("ADMIN/" + entry.Value.idno + "/RefillingStation");
                        Model.RefillingStation stations = station.ResultAs<Model.RefillingStation>();

                        station = twoBigDB.Get("ADMIN/" + entry.Value.idno + "/Subscribed_Package");
                        Model.Subscribed_Package package = station.ResultAs<Model.Subscribed_Package>();


                        customerTable.Rows.Add(
                           //entry.Value.profile_image,
                           entry.Value.idno,
                            entry.Value.fname,
                            entry.Value.lname,
                            entry.Value.email,
                           stations.stationAddress,
                            entry.Value.phone,
                            entry.Value.dateRegistered,
                            package.subStart,
                            package.packageName);
                    }

                }
                // Bind DataTable to GridView control
                clientReports.DataSource = customerTable;
                clientReports.DataBind();
            }
            else if (selectedValue == "Declined Customers")
            {
                //hide other grids
                clientReports.Visible = false;
                clientsLabel.Visible = false;

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
                customerTable.Columns.Add("USER STATUS");



                foreach (KeyValuePair<string, Model.Customer> entry in clients)
                {
                    if (entry.Value.cus_status == "Declined")
                    {
                        double lattitude = entry.Value.lattitudeLocation;
                        double longitude = entry.Value.longitudeLocation;
                        string apiKey = "AIzaSyBqKUBIswNi5uO3xOh4Boo8kSJyJ3DLkhk";

                        string address = GetAddressFromLatLong(lattitude, longitude, apiKey);

                        //customerProfile.ImageUrl = entry.Value.imageProof;

                        customerTable.Rows.Add(
                            //entry.Value.imageProof,
                            entry.Value.cusId,
                            entry.Value.firstName,
                            entry.Value.lastName,
                            entry.Value.email,
                            address,
                            entry.Value.phoneNumber,
                            entry.Value.cus_status);

                    }

                }
                // Bind DataTable to GridView control
                customerReports.DataSource = customerTable;
                customerReports.DataBind();
            }
            else if (selectedValue == "Declined Clients")
            {
                //hide other grids
                customerReports.Visible = false;
                customersLabel.Visible = false;

                FirebaseResponse response = twoBigDB.Get("ADMIN");
                Model.AdminAccount admin = response.ResultAs<Model.AdminAccount>();
                var data = response.Body;
                Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);



                DataTable customerTable = new DataTable();
                //customerTable.Columns.Add("PROFILE");
                customerTable.Columns.Add("USER ID");
                customerTable.Columns.Add("FIRST NAME");
                customerTable.Columns.Add("LAST NAME");
                customerTable.Columns.Add("EMAIL");
                customerTable.Columns.Add("ADDRESS");
                customerTable.Columns.Add("PHONE #");
                customerTable.Columns.Add("DATE REGISTERED");
                customerTable.Columns.Add("USER STATUS");
                


                foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
                {
                    //clientProfile.ImageUrl = entry.Value.profile_image;
                    if (entry.Value.status == "Declined")
                    {
                        FirebaseResponse station = twoBigDB.Get("ADMIN/" + entry.Value.idno + "/RefillingStation");
                        Model.RefillingStation stations = station.ResultAs<Model.RefillingStation>();

                        

                        customerTable.Rows.Add(
                           //entry.Value.profile_image,
                           entry.Value.idno,
                            entry.Value.fname,
                            entry.Value.lname,
                            entry.Value.email,
                           stations.stationAddress,
                            entry.Value.phone,
                            entry.Value.dateRegistered,
                            entry.Value.status
                            );
                    }

                }
                // Bind DataTable to GridView control
                clientReports.DataSource = customerTable;
                clientReports.DataBind();
            }


        }
    }
}
