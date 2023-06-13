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
    public partial class Reports : System.Web.UI.Page
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
                superAdminLog();
            }
        }
        private void superAdminLog()
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN_LOGS");
            Model.superLogs all = response.ResultAs<Model.superLogs>();
            var data = response.Body;
            Dictionary<string, Model.superLogs> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superLogs>>(data);

            if (data != null)
            {
                // Creating the columns of the GridView
                DataTable clientsTable = new DataTable();
                clientsTable.Columns.Add("LOG ID");
                clientsTable.Columns.Add("NAME");
                clientsTable.Columns.Add("ACTIVITY");
                clientsTable.Columns.Add("TIME");

                // Create a list to store the sorted entries
                List<KeyValuePair<string, Model.superLogs>> sortedEntries = subscribed.OrderByDescending(entry => entry.Value.activityTime).ToList();

                foreach (KeyValuePair<string, Model.superLogs> entry in sortedEntries)
                {
                    clientsTable.Rows.Add(entry.Value.logsId, entry.Value.superFullname, entry.Value.superActivity, entry.Value.activityTime);
                }

                // Bind DataTable to GridView control
                superAdminLogs.DataSource = clientsTable;
                superAdminLogs.DataBind();
            }

        }

        protected void generateSortedData_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN_LOGS");
            Model.superLogs all = response.ResultAs<Model.superLogs>();
            var data = response.Body;
            Dictionary<string, Model.superLogs> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superLogs>>(data);

            if (data != null)
            {
                // Get the selected start and end dates
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

                // Filter the data based on the selected dates
                var filteredData = subscribed.Values.Where(s => s.activityTime >= startDate && s.activityTime < endDate);

                // Check if the filtered data is empty
                if (!filteredData.Any())
                {
                    // Handle the empty data condition (e.g., display a message, hide the GridView)
                    // For example, you can display a message in a label control:
                    subSalesLabel.Text = "No data available for the selected date range.";
                   
                    return; // Exit the method or return the appropriate response
                }

                // Create a list to hold the filtered sales data
                List<Model.superLogs> salesList = new List<Model.superLogs>(filteredData);
                // Sort the sales list in descending order by date and time
                salesList.Sort((x, y) => y.activityTime.CompareTo(x.activityTime));

                // Creating the columns of the GridView
                DataTable clientsTable = new DataTable();
                clientsTable.Columns.Add("LOG ID");
                clientsTable.Columns.Add("NAME");
                clientsTable.Columns.Add("ACTIVITY");
                clientsTable.Columns.Add("TIME");

                foreach (Model.superLogs sale in salesList)
                {
                    clientsTable.Rows.Add(sale.logsId, sale.superFullname, sale.superActivity, sale.activityTime);
                }
                // Bind DataTable to GridView control
                superAdminLogs.DataSource = clientsTable;
                superAdminLogs.DataBind();
            }
        }

        protected void clearSort_Click(object sender, EventArgs e)
        {
            sortStart.Text = "";
            sortEnd.Text = "";
            Response.Write("<script> window.location.href = '/superAdmin/Reports.aspx'; </script>");
        }

        protected void clearSearch_Click(object sender, EventArgs e)
        {
            search.Text = "";
            Response.Write("<script> window.location.href = '/superAdmin/Reports.aspx'; </script>");
        }
        private void loginHistory()
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN_LOGS");
            Model.superLogs all = response.ResultAs<Model.superLogs>();
            var data = response.Body;
            Dictionary<string, Model.superLogs> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superLogs>>(data);

            if (data != null)
            {
                // Creating the columns of the GridView
                DataTable clientsTable = new DataTable();
                clientsTable.Columns.Add("LOG ID");
                clientsTable.Columns.Add("NAME");
                clientsTable.Columns.Add("ACTIVITY");
                clientsTable.Columns.Add("TIME");

                // Create a list to store the sorted entries
                List<KeyValuePair<string, Model.superLogs>> sortedEntries = subscribed.OrderByDescending(entry => entry.Value.activityTime).ToList();

                foreach (KeyValuePair<string, Model.superLogs> entry in sortedEntries)
                {
                    if (entry.Value.superActivity == "LOGGED IN")
                    {
                        clientsTable.Rows.Add(entry.Value.logsId, entry.Value.superFullname, entry.Value.superActivity, entry.Value.activityTime);
                    }
                   
                }

                // Bind DataTable to GridView control
                superAdminLogs.DataSource = clientsTable;
                superAdminLogs.DataBind();
            }
        }
        private void customerEvalHistory()
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN_LOGS");
            Model.superLogs all = response.ResultAs<Model.superLogs>();
            var data = response.Body;
            Dictionary<string, Model.superLogs> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superLogs>>(data);

            if (data != null)
            {
                // Creating the columns of the GridView
                DataTable clientsTable = new DataTable();
                clientsTable.Columns.Add("LOG ID");
                clientsTable.Columns.Add("NAME");
                clientsTable.Columns.Add("ACTIVITY");
                clientsTable.Columns.Add("TIME");

                // Create a list to store the sorted entries
                List<KeyValuePair<string, Model.superLogs>> sortedEntries = subscribed.OrderByDescending(entry => entry.Value.activityTime).ToList();

                foreach (KeyValuePair<string, Model.superLogs> entry in sortedEntries)
                {
                    //CHANGE FOR CUSTOMER EVALUATION: APPROVE/DECLINE
                    if (entry.Value.superActivity == "LOGGED IN")
                    {
                        clientsTable.Rows.Add(entry.Value.logsId, entry.Value.superFullname, entry.Value.superActivity, entry.Value.activityTime);
                    }

                }

                // Bind DataTable to GridView control
                superAdminLogs.DataSource = clientsTable;
                superAdminLogs.DataBind();
            }
        }
        private void clientEvalHistory()
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN_LOGS");
            Model.superLogs all = response.ResultAs<Model.superLogs>();
            var data = response.Body;
            Dictionary<string, Model.superLogs> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superLogs>>(data);

            if (data != null)
            {
                // Creating the columns of the GridView
                DataTable clientsTable = new DataTable();
                clientsTable.Columns.Add("LOG ID");
                clientsTable.Columns.Add("NAME");
                clientsTable.Columns.Add("ACTIVITY");
                clientsTable.Columns.Add("TIME");

                // Create a list to store the sorted entries
                List<KeyValuePair<string, Model.superLogs>> sortedEntries = subscribed.OrderByDescending(entry => entry.Value.activityTime).ToList();

                foreach (KeyValuePair<string, Model.superLogs> entry in sortedEntries)
                {
                    //CHANGE FOR CLIENT EVALUATION: APPROVE/DECLINE
                    if (entry.Value.superActivity == "LOGGED IN")
                    {
                        clientsTable.Rows.Add(entry.Value.logsId, entry.Value.superFullname, entry.Value.superActivity, entry.Value.activityTime);
                    }

                }

                // Bind DataTable to GridView control
                superAdminLogs.DataSource = clientsTable;
                superAdminLogs.DataBind();
            }
        }
        private void logoutHistory()
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN_LOGS");
            Model.superLogs all = response.ResultAs<Model.superLogs>();
            var data = response.Body;
            Dictionary<string, Model.superLogs> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superLogs>>(data);

            if (data != null)
            {
                // Creating the columns of the GridView
                DataTable clientsTable = new DataTable();
                clientsTable.Columns.Add("LOG ID");
                clientsTable.Columns.Add("NAME");
                clientsTable.Columns.Add("ACTIVITY");
                clientsTable.Columns.Add("TIME");

                // Create a list to store the sorted entries
                List<KeyValuePair<string, Model.superLogs>> sortedEntries = subscribed.OrderByDescending(entry => entry.Value.activityTime).ToList();

                foreach (KeyValuePair<string, Model.superLogs> entry in sortedEntries)
                {
                    //CHANGE FOR CUSTOMER EVALUATION: APPROVE/DECLINE
                    if (entry.Value.superActivity == "LOGGED OUT")
                    {
                        clientsTable.Rows.Add(entry.Value.logsId, entry.Value.superFullname, entry.Value.superActivity, entry.Value.activityTime);
                    }

                }

                // Bind DataTable to GridView control
                superAdminLogs.DataSource = clientsTable;
                superAdminLogs.DataBind();
            }
        }
        private void packageHistory()
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN_LOGS");
            Model.superLogs all = response.ResultAs<Model.superLogs>();
            var data = response.Body;
            Dictionary<string, Model.superLogs> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superLogs>>(data);

            if (data != null)
            {
                // Creating the columns of the GridView
                DataTable clientsTable = new DataTable();
                clientsTable.Columns.Add("LOG ID");
                clientsTable.Columns.Add("NAME");
                clientsTable.Columns.Add("ACTIVITY");
                clientsTable.Columns.Add("TIME");

                // Create a list to store the sorted entries
                List<KeyValuePair<string, Model.superLogs>> sortedEntries = subscribed.OrderByDescending(entry => entry.Value.activityTime).ToList();

                foreach (KeyValuePair<string, Model.superLogs> entry in sortedEntries)
                {
                    //CHANGE FOR PACKAGE HISTORY 
                    if (entry.Value.superActivity == "LOGGED IN")
                    {
                        clientsTable.Rows.Add(entry.Value.logsId, entry.Value.superFullname, entry.Value.superActivity, entry.Value.activityTime);
                    }

                }

                // Bind DataTable to GridView control
                superAdminLogs.DataSource = clientsTable;
                superAdminLogs.DataBind();
            }
        }
        protected void activityDropdown_Click(object sender, EventArgs e)
        {
            string selectedValue = activityDropdown.SelectedValue;

            if (selectedValue == "Login")
            {
                loginHistory();
            }
            else if (selectedValue == "Customer Evaluation")
            {
                customerEvalHistory();
            }
            else if (selectedValue == "Admin Evaluation")
            {
                clientEvalHistory();
            }
            else if (selectedValue == "Logout")
            {
                logoutHistory();
            }
            else if (selectedValue == "Package")
            {
                packageHistory();
            }
        }
    }
}