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

            lblErrorActivity.Visible = false;

            displayAll();

        }
        private void displayAll()
        {
            string idno = (string)Session["idno"];


            // Get the log ID from the session
            //int logsId = (int)Session["logsId"];

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ADMINLOGS/");
            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();
            //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
            var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno).OrderByDescending(d => d.activityTime);

            // Create the DataTable to hold the orders
            //sa pag create sa table 
            DataTable userLogTable = new DataTable();
            userLogTable.Columns.Add("LOG ID");
            userLogTable.Columns.Add("USER NAME");
            userLogTable.Columns.Add("USER ROLE");
            userLogTable.Columns.Add("ACTIVITY");
            userLogTable.Columns.Add("TIMESTAMP");

            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null)
            {
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    string activity = entry.userActivity;

                    if (activity == "0")
                    {
                        activity = "";
                    }

                    string timestamp = entry.activityTime == DateTime.MinValue ? "" : entry.activityTime.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    userLogTable.Rows.Add(entry.logsId, entry.userFullname, entry.role, activity, timestamp);

                    ////Retrieve the existing Users log object from the database
                    //FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + entry.logsId);
                    //UsersLogs existingLog = resLog.ResultAs<UsersLogs>();


                    //// Log user activity
                    //var log = new UsersLogs
                    //{
                    //    userIdnum = int.Parse(idno),
                    //    logsId = logsId,
                    //    userFullname = (string)Session["fullname"],
                    //    activityTime = existingLog.activityTime,
                    //    userActivity = activity
                    //};
                    //// Update the userLogTable with the user's activity
                    //twoBigDB.Update("ADMINLOGS/" + log.logsId, log);

                }
            }
            else
            {
                // Handle null response or invalid selected value
                lblErrorActivity.Text = "No record found";
            }

            // Bind the DataTable to the GridView
            gridUserLog.DataSource = userLogTable;
            gridUserLog.DataBind();
        }
        private void displayOwner()
        {
            string idno = (string)Session["idno"];

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ADMINLOGS");
            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();
            //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
            var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno).OrderByDescending(d => d.activityTime);

            // Create the DataTable to hold the orders
            //sa pag create sa table 
            DataTable userLogTable = new DataTable();
            userLogTable.Columns.Add("LOG ID");
            userLogTable.Columns.Add("USER NAME");
            userLogTable.Columns.Add("USER ROLE");
            userLogTable.Columns.Add("ACTIVITY");
            userLogTable.Columns.Add("TIMESTAMP");

            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null)
            {
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    if(entry.role == "Admin")
                    {
                        string activity = entry.userActivity;

                        if (activity == "0")
                        {
                            activity = "";
                        }

                        string timestamp = entry.activityTime == DateTime.MinValue ? "" : entry.activityTime.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        userLogTable.Rows.Add(entry.logsId, entry.userFullname, entry.role, activity, timestamp);

                    }

                    ////Retrieve the existing Users log object from the database
                    //FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + entry.logsId);
                    //UsersLogs existingLog = resLog.ResultAs<UsersLogs>();


                    //// Log user activity
                    //var log = new UsersLogs
                    //{
                    //    userIdnum = int.Parse(idno),
                    //    logsId = logsId,
                    //    userFullname = (string)Session["fullname"],
                    //    activityTime = existingLog.activityTime,
                    //    userActivity = activity
                    //};
                    //// Update the userLogTable with the user's activity
                    //twoBigDB.Update("ADMINLOGS/" + log.logsId, log);

                }
            }
            else
            {
                // Handle null response or invalid selected value
                lblErrorActivity.Text = "No record found";
            }

            // Bind the DataTable to the GridView
            gridUserLog.DataSource = userLogTable;
            gridUserLog.DataBind();
        }
        private void displayCashier()
        {
            string idno = (string)Session["idno"];


            // Get the log ID from the session
            //int logsId = (int)Session["logsId"];

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ADMINLOGS/");
            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();
            //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
            var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno).OrderByDescending(d => d.activityTime);

            // Create the DataTable to hold the orders
            //sa pag create sa table 
            DataTable userLogTable = new DataTable();
            userLogTable.Columns.Add("LOG ID");
            userLogTable.Columns.Add("USER NAME");
            userLogTable.Columns.Add("USER ROLE");
            userLogTable.Columns.Add("ACTIVITY");
            userLogTable.Columns.Add("TIMESTAMP");

            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null)
            {
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    if (entry.role == "Cashier")
                    {
                        string activity = entry.userActivity;

                        if (activity == "0")
                        {
                            activity = "";
                        }

                        string timestamp = entry.activityTime == DateTime.MinValue ? "" : entry.activityTime.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        userLogTable.Rows.Add(entry.logsId, entry.userFullname, entry.role, activity, timestamp);

                    }

                    ////Retrieve the existing Users log object from the database
                    //FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + entry.logsId);
                    //UsersLogs existingLog = resLog.ResultAs<UsersLogs>();


                    //// Log user activity
                    //var log = new UsersLogs
                    //{
                    //    userIdnum = int.Parse(idno),
                    //    logsId = logsId,
                    //    userFullname = (string)Session["fullname"],
                    //    activityTime = existingLog.activityTime,
                    //    userActivity = activity
                    //};
                    //// Update the userLogTable with the user's activity
                    //twoBigDB.Update("ADMINLOGS/" + log.logsId, log);

                }
            }
            else
            {
                // Handle null response or invalid selected value
                lblErrorActivity.Text = "No record found";
            }

            // Bind the DataTable to the GridView
            gridUserLog.DataSource = userLogTable;
            gridUserLog.DataBind();
        }
        private void displayDriver()
        {
            string idno = (string)Session["idno"];


            // Get the log ID from the session
            //int logsId = (int)Session["logsId"];

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("DRIVERSLOG/");
            Dictionary<string, DriversLog> driverlog = response.ResultAs<Dictionary<string, DriversLog>>();
            //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
            var filteredList = driverlog.Values.Where(d => d.empId.ToString() == idno).OrderByDescending(d => d.date);

            // Create the DataTable to hold the orders
            //sa pag create sa table 
            DataTable userLogTable = new DataTable();
            userLogTable.Columns.Add("EMP ID");
            userLogTable.Columns.Add("USER NAME");
            userLogTable.Columns.Add("USER ROLE");
            userLogTable.Columns.Add("ACTIVITY");
            userLogTable.Columns.Add("TIMESTAMP");

            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null)
            {
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    string activity = entry.actions;
                    string role = "Driver";
                    if (activity == "0")
                    {
                        activity = "";
                    }

                    string timestamp = entry.date == DateTime.MinValue ? "" : entry.date.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    userLogTable.Rows.Add(entry.empId, entry.driverName, role, activity, timestamp);

                    ////Retrieve the existing Users log object from the database
                    //FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + entry.logsId);
                    //UsersLogs existingLog = resLog.ResultAs<UsersLogs>();


                    //// Log user activity
                    //var log = new UsersLogs
                    //{
                    //    userIdnum = int.Parse(idno),
                    //    logsId = logsId,
                    //    userFullname = (string)Session["fullname"],
                    //    activityTime = existingLog.activityTime,
                    //    userActivity = activity
                    //};
                    //// Update the userLogTable with the user's activity
                    //twoBigDB.Update("ADMINLOGS/" + log.logsId, log);

                }
            }
            else
            {
                // Handle null response or invalid selected value
                lblErrorActivity.Text = "No record found";
            }

            // Bind the DataTable to the GridView
            gridUserLog.DataSource = userLogTable;
            gridUserLog.DataBind();
        }

        //SEARCH ACTIVITY
        protected void btnSearchLogs_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#view').modal();", true);

            string idno = (string)Session["idno"];
            string activity = txtSearch.Text;
            try
            {
                

                // Retrieve all orders from the ORDERS table
                FirebaseResponse responselist = twoBigDB.Get("ADMINLOGS");
                Dictionary<string, UsersLogs> loglist = responselist.ResultAs<Dictionary<string, UsersLogs>>();

                // Create the DataTable to hold the orders
                DataTable userLogsTable = new DataTable();
                userLogsTable.Columns.Add("LOG ID");
                userLogsTable.Columns.Add("USER ID");
                userLogsTable.Columns.Add("USER NAME");
                userLogsTable.Columns.Add("USER ROLE");
                userLogsTable.Columns.Add("ACTIVITY");
                // walkInordersTable.Columns.Add("PRODUCT SIZE");
                userLogsTable.Columns.Add("TIMESTAMP");


                if (responselist != null && responselist.ResultAs<UsersLogs>() != null)
                {
                    var filteredList = loglist.Values.Where(d => d.userIdnum.ToString() == idno && d.userActivity.ToString() == activity);

                    if (filteredList.Count() == 0)
                    {
                        Response.Write("<script>alert('No logs found for the entered activity. Search activity in capital letters and with space in between each word.');</script>");
                    }
                    else
                    {
                        // Loop through the entries and add them to the DataTable
                        foreach (var entry in filteredList)
                        {
                            string timestamp = entry.activityTime == DateTime.MinValue ? "" : entry.activityTime.ToString("MMMM dd, yyyy hh:mm:ss tt");
                           // string dateAdded = entry.activityTime.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            userLogsTable.Rows.Add(entry.logsId, entry.userIdnum, entry.role, entry.userFullname, entry.userActivity, timestamp);
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('Error retrieving logs.');</script>");
                }

                // Bind the DataTable to the GridView
                GridLogs.DataSource = userLogsTable;
                GridLogs.DataBind();

                txtSearch.Text = null;

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred while processing your request. '); location.reload(); window.location.href = '/Admin/UsersLog.aspx'; </script>" + ex.Message);
            }
        }
        //Sorting data base on the dates entered by the user
        protected void generateSortedData_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];

            if (string.IsNullOrEmpty(sortStart.Text) || string.IsNullOrEmpty(sortEnd.Text))
            {
                // Handle the missing start or end date condition 
                Response.Write("<script>alert ('You must choose a Start and End Date of the logs you want to view.');</script>");
                return; // Exit the method or return the appropriate response
            }

            // Get the start and end dates entered by the user
            DateTime startDate = DateTime.Parse(sortStart.Text);
            DateTime endDate = DateTime.Parse(sortEnd.Text).AddDays(1); // Add 1 day to include the end date

            // Retrieve user logs within the specified date range
            FirebaseResponse response = twoBigDB.Get("ADMINLOGS/");
            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();

            // Create the DataTable to hold the filtered logs
            DataTable userLogTable = new DataTable();
            userLogTable.Columns.Add("LOG ID");
            userLogTable.Columns.Add("USER NAME");
            userLogTable.Columns.Add("USER ROLE");
            userLogTable.Columns.Add("ACTIVITY");
            userLogTable.Columns.Add("TIMESTAMP");

            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null)
            {
                var filteredList = userlog.Values
                    .Where(d => d.userIdnum.ToString() == idno && d.activityTime >= startDate && d.activityTime < endDate)
                    .OrderByDescending(d => d.activityTime);

                if (filteredList.Any())
                {
                    // Loop through the filtered logs and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string activity = entry.userActivity;

                        if (activity == "0")
                        {
                            activity = "";
                        }

                        string timestamp = entry.activityTime == DateTime.MinValue ? "" : entry.activityTime.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        userLogTable.Rows.Add(entry.logsId, entry.userFullname, entry.role, activity, timestamp);
                    }
                }
                else
                {
                    lblErrorActivity.Visible = true;
                    lblErrorActivity.Text = "No logs available between " + startDate.ToString("MMMM dd, yyyy") + " and " + endDate.ToString("MMMM dd, yyyy");
                }
            }
            else
            {
                // Handle null response or invalid selected value
                lblErrorActivity.Visible = true;
                lblErrorActivity.Text = "No data found";
            }

            // Bind the DataTable to the GridView
            gridUserLog.DataSource = userLogTable;
            gridUserLog.DataBind();
        }
        protected void btnViewRole_Click(object sender, EventArgs e)
        {

            string selectedOption = drdRole.SelectedValue;
            try
            {
                if (selectedOption == "0")
                {
                    displayAll();
                    //lblViewOrders.Text = "ALL ORDER";

                }
                else if (selectedOption == "1")
                {
                    //lblViewOrders.Text = "EXPRESS ORDER";
                    displayOwner();
                    //lblError.Visible = false;

                }
                else if (selectedOption == "2")
                {
                    //lblViewOrders.Text = "STANDARD ORDER";
                    displayCashier();
                    //lblError.Visible = false;

                }
                else if (selectedOption == "3")
                {
                    //lblViewOrders.Text = "RESERVATION ORDER";
                    displayDriver();
                    //lblError.Visible = false;
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/OnlineOrders.aspx';" + ex.Message);
            }
        }

        //clear the sorting textbox fields
        protected void clearSort_Click(object sender, EventArgs e)
        {
            sortStart.Text = "";
            sortEnd.Text = "";
            Response.Write("<script> window.location.href = '/superAdmin/UsersLog.aspx'; </script>");
            
        }
    }
}
