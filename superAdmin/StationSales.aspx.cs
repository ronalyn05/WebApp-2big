using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using WRS2big_Web.Model;


namespace WRS2big_Web.superAdmin
{
    public partial class SalesReport : System.Web.UI.Page
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
               

                LoadRegisteredClients();
                generateAdminSales.Visible = false;

                sortStart.Text = "";
                sortEnd.Text = "";

            }

        }
        private void LoadRegisteredClients()
        {

            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount all = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

            DataTable clientsTable = new DataTable();
            clientsTable.Columns.Add("CLIENT ID");
            clientsTable.Columns.Add("CLIENT NAME");
            clientsTable.Columns.Add("REFILLING STATION");
            clientsTable.Columns.Add("ADDRESS");
            clientsTable.Columns.Add("ACCOUNT STATUS"); //active or inactive
            clientsTable.Columns.Add("DATE REGISTERED"); //based on the reviews - get the average kung KAYA !

            if (data != null && clients != null)
            {
                foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
                {
                    response = twoBigDB.Get("ADMIN/" + entry.Key + "/RefillingStation");
                    Model.RefillingStation station = response.ResultAs<Model.RefillingStation>();

                    if (entry.Value.status == "Approved")
                    {

                        clientsTable.Rows.Add(entry.Value.idno,
                        entry.Value.fname + " " + entry.Value.lname,
                        station.stationName,
                        station.stationAddress,
                        entry.Value.currentSubscription,
                        entry.Value.dateRegistered);

                    }
                }
                stationSalesReport.DataSource = clientsTable;
                stationSalesReport.DataBind();
            }
        }

        protected void adminSales_Click(object sender, EventArgs e)
        {
            List<int> selectedClient = new List<int>();

            foreach (GridViewRow row in stationSalesReport.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("selectedClient");
                if (chk != null && chk.Checked)
                {
                    int clientID = int.Parse(row.Cells[1].Text);
                    selectedClient.Add(clientID);

                    Session["currentClient"] = clientID;
                    Response.Write("<script>window.location.href = '/superAdmin/ClientsSalesReportPage.aspx'; </script>");

                }
            }

            if (selectedClient.Count > 1)
            {
                Response.Write("<script>alert ('Please select one client only'); </script>");
                return;
            }
        }


        //private void loadClientSales()
        //{
        //    int clientID = (int)Session["currentClient"];

        //    FirebaseResponse response = twoBigDB.Get("ORDERS");
        //    Model.Order all = response.ResultAs<Model.Order>();
        //    var data = response.Body;
        //    Dictionary<string, Model.Order> clientSale = JsonConvert.DeserializeObject<Dictionary<string, Model.Order>>(data);

        //    if (data != null && clientSale != null)
        //    {
        //        List<Model.Order> paymentReceivedOrders = new List<Model.Order>();

        //        foreach (KeyValuePair<string, Model.Order> entry in clientSale)
        //        {
        //            if (entry.Value.order_OrderStatus == "Payment Received" && entry.Value.admin_ID == clientID)
        //            {
        //                paymentReceivedOrders.Add(entry.Value);
        //            }
        //        }

        //        if (paymentReceivedOrders.Count == 0)
        //        {
        //            Response.Write("<script>alert ('No record of sale found for this client'); window.location.href = '/superAdmin/StationSales.aspx'; </script>");
        //            return;
        //        }

        //        paymentReceivedOrders.Sort((x, y) => DateTime.Compare(x.orderDate, y.orderDate)); // Sort the orders based on orderDate

        //        DataTable ordersTable = new DataTable();
        //        ordersTable.Columns.Add("ORDER ID");
        //        ordersTable.Columns.Add("CUSTOMER NAME");
        //        ordersTable.Columns.Add("QUANTITY");
        //        ordersTable.Columns.Add("AMOUNT PAID");
        //        ordersTable.Columns.Add("DATE ORDERED");
        //        ordersTable.Columns.Add("TOTAL REVENUE");

        //        decimal revenue = 0;

        //        foreach (var clientOrders in paymentReceivedOrders)
        //        {
        //            int customerID = clientOrders.cusId;
        //            response = twoBigDB.Get("CUSTOMER/" + customerID);
        //            Model.Customer orderedCus = response.ResultAs<Model.Customer>();

        //            revenue += clientOrders.order_TotalAmount;

        //            ordersTable.Rows.Add(clientOrders.orderID,
        //                orderedCus.firstName + " " + orderedCus.lastName,
        //                clientOrders.order_OverallQuantities,
        //                clientOrders.order_TotalAmount,
        //                clientOrders.orderDate,
        //                revenue);
        //        }

        //        clientSalesReport.DataSource = ordersTable;
        //        clientSalesReport.DataBind();
        //    }
        //}


        protected void selectedClient_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selected = (CheckBox)sender;

            CheckBox client = (CheckBox)selected.NamingContainer.FindControl("selectedClient");

            if (client != null && client.Checked)
            {
                generateAdminSales.Visible = true;
            }

            
        }

        protected void generateSortedData_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount all = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

            DataTable clientsTable = new DataTable();
            clientsTable.Columns.Add("CLIENT ID");
            clientsTable.Columns.Add("CLIENT NAME");
            clientsTable.Columns.Add("REFILLING STATION");
            clientsTable.Columns.Add("ADDRESS");
            clientsTable.Columns.Add("ACCOUNT STATUS"); //active or inactive
            clientsTable.Columns.Add("DATE REGISTERED");

            if (data != null && clients != null)
            {
                if (string.IsNullOrEmpty(sortStart.Text) || string.IsNullOrEmpty(sortEnd.Text))
                {
                    // Handle the missing start or end date condition (e.g., display an alert)
                    // For example, you can use JavaScript to show an alert box:
                    Response.Write("<script>alert ('You must choose a Start and End Date');</script>");
                    return; // Exit the method or return the appropriate response
                }

                // Get the selected start and end dates
                DateTime startDate = DateTime.Parse(sortStart.Text);
                DateTime endDate = DateTime.Parse(sortEnd.Text).AddDays(1); // Add one day to include the end date

                List<Model.AdminAccount> filteredClients = new List<Model.AdminAccount>();

                foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
                {
                    response = twoBigDB.Get("ADMIN/" + entry.Key + "/RefillingStation");
                    Model.RefillingStation station = response.ResultAs<Model.RefillingStation>();

                    if (entry.Value.status == "Approved")
                    {
                        // Filter the clients based on the selected date range
                        if (entry.Value.dateRegistered >= startDate && entry.Value.dateRegistered < endDate)
                        {
                            filteredClients.Add(entry.Value);
                        }

                        clientsTable.Rows.Add(entry.Value.idno,
                            entry.Value.fname + " " + entry.Value.lname,
                            station.stationName,
                            station.stationAddress,
                            entry.Value.currentSubscription,
                            entry.Value.dateRegistered);
                    }
                }

                // Sort the filtered clients by dateRegistered
                filteredClients.Sort((x, y) => DateTime.Compare(x.dateRegistered, y.dateRegistered));

                clientsTable.Clear(); // Clear the existing rows

                foreach (Model.AdminAccount client in filteredClients)
                {
                    response = twoBigDB.Get("ADMIN/" + client.idno + "/RefillingStation");
                    Model.RefillingStation station = response.ResultAs<Model.RefillingStation>();

                    clientsTable.Rows.Add(client.idno,
                        client.fname + " " + client.lname,
                        station.stationName,
                        station.stationAddress,
                        client.currentSubscription,
                        client.dateRegistered);
                }

                if (filteredClients.Count == 0)
                {
                    subSalesLabel.Text = "No client found";
                    sortStart.Text = "";
                    sortEnd.Text = "";
                }
                stationSalesReport.DataSource = clientsTable;
                stationSalesReport.DataBind();
            }

        }

        //protected void searchButton_Click(object sender, EventArgs e)
        //{
        //    string searched = search.Text;

        //    FirebaseResponse response = twoBigDB.Get("ADMIN");
        //    Model.AdminAccount all = response.ResultAs<Model.AdminAccount>();
        //    var data = response.Body;
        //    Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

        //    DataTable clientsTable = new DataTable();
        //    clientsTable.Columns.Add("CLIENT ID");
        //    clientsTable.Columns.Add("CLIENT NAME");
        //    clientsTable.Columns.Add("REFILLING STATION");
        //    clientsTable.Columns.Add("ADDRESS");
        //    clientsTable.Columns.Add("ACCOUNT STATUS"); //active or inactive
        //    clientsTable.Columns.Add("DATE REGISTERED"); //based on the reviews - get the average kung KAYA !

        //    if (data != null && clients != null)
        //    {
        //        foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
        //        {
        //                response = twoBigDB.Get("ADMIN/" + entry.Key + "/RefillingStation");
        //                Model.RefillingStation station = response.ResultAs<Model.RefillingStation>();

        //                if (entry.Value.status == "Approved")
        //                {
        //                    if (searched == entry.Value.fname || searched == entry.Value.lname || searched == entry.Value.mname)
        //                    {
        //                        clientsTable.Rows.Add(entry.Value.idno,
        //                        entry.Value.fname + " " + entry.Value.lname,
        //                        station.stationName,
        //                        station.stationAddress,
        //                        entry.Value.currentSubscription,
        //                        entry.Value.dateRegistered);
        //                    }


        //                }
        //        }

        //        if (clientsTable.Rows.Count == 0)
        //        {
        //            subSalesLabel.Text = "No client found";
        //            search.Text = "";
        //            sortStart.Text = "";
        //            sortEnd.Text = "";
        //        }
        //        stationSalesReport.DataSource = clientsTable;
        //        stationSalesReport.DataBind();
        //    }

        //}

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string searched = search.Text;

            DateTime startDate, endDate;

            if (!string.IsNullOrEmpty(sortStart.Text) && !string.IsNullOrEmpty(sortEnd.Text))
            {
                // Get the selected start and end dates
                startDate = DateTime.Parse(sortStart.Text);
                endDate = DateTime.Parse(sortEnd.Text).AddDays(1); // Add one day to include the end date
            }
            else
            {
                // Set the start and end dates to a broad range to include all dates
                startDate = DateTime.MinValue;
                endDate = DateTime.MaxValue;
            }

            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount all = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

            DataTable clientsTable = new DataTable();
            clientsTable.Columns.Add("CLIENT ID");
            clientsTable.Columns.Add("CLIENT NAME");
            clientsTable.Columns.Add("REFILLING STATION");
            clientsTable.Columns.Add("ADDRESS");
            clientsTable.Columns.Add("ACCOUNT STATUS"); //active or inactive
            clientsTable.Columns.Add("DATE REGISTERED"); //based on the reviews - get the average kung KAYA !

            if (data != null && clients != null)
            {
                foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
                {
                    response = twoBigDB.Get("ADMIN/" + entry.Key + "/RefillingStation");
                    Model.RefillingStation station = response.ResultAs<Model.RefillingStation>();

                    if (entry.Value.status == "Approved")
                    {
                        // Check if the date is within the sorting range or if the range is empty
                        if (entry.Value.dateRegistered >= startDate && entry.Value.dateRegistered < endDate)
                        {
                            if (searched == entry.Value.fname || searched == entry.Value.lname || searched == entry.Value.mname)
                            {
                                clientsTable.Rows.Add(entry.Value.idno,
                                    entry.Value.fname + " " + entry.Value.lname,
                                    station.stationName,
                                    station.stationAddress,
                                    entry.Value.currentSubscription,
                                    entry.Value.dateRegistered);
                            }
                        }
                    }
                }

                if (clientsTable.Rows.Count == 0)
                {
                    subSalesLabel.Text = "No client found";
                    
                }
                stationSalesReport.DataSource = clientsTable;
                stationSalesReport.DataBind();
            }
        }

        protected void clearSort_Click(object sender, EventArgs e)
        {
            sortStart.Text = "";
            sortEnd.Text = "";
            Response.Write("<script> window.location.href = '/superAdmin/StationSales.aspx'; </script>");
        }

        protected void closeButton_Click(object sender, EventArgs e)
        {
            search.Text = "";
            subSalesLabel.Text = "";
            LoadRegisteredClients();

        }
    }
}