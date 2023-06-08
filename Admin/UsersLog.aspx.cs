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
            lblSearchError.Visible = false;
            lblSearchResult.Visible = false;

            //displayAll();

        }
        //DISPLAY ALL LOGS
        private void displayAll()
        {
            string idno = (string)Session["idno"];

            // Retrieve all user logs from the ADMINLOGS table
            FirebaseResponse response = twoBigDB.Get("ADMINLOGS/");
            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();

            // Retrieve all driver logs from the DRIVERSLOG table
            FirebaseResponse responseDriver = twoBigDB.Get("DRIVERSLOG");
            Dictionary<string, DriversLog> driverlog = responseDriver.ResultAs<Dictionary<string, DriversLog>>();

            // Create the DataTable to hold the logs
            DataTable userLogTable = new DataTable();
            userLogTable.Columns.Add("LOG ID");
            userLogTable.Columns.Add("USER ID");
            userLogTable.Columns.Add("USER NAME");
            userLogTable.Columns.Add("USER ROLE");
            userLogTable.Columns.Add("ACTIVITY");
            userLogTable.Columns.Add("TIMESTAMP");

            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null &&
                responseDriver != null && responseDriver.ResultAs<Dictionary<string, DriversLog>>() != null)
            {
                // Filtered user logs based on the user ID and order by descending date
                var filteredList = userlog.Values
                    .Where(d => d.userIdnum.ToString() == idno)
                    .OrderByDescending(d => d.activityTime);

                // Filtered driver logs based on the admin ID and order by descending date
                var filteredDriverList = driverlog.Values
                    .Where(d => d.admin_ID.ToString() == idno)
                    .OrderByDescending(d => d.date);

                // Combine user logs and driver logs into a single list
                var combinedList = filteredList.Select(entry => new
                {
                    LogId = entry.logsId,
                    UserId = entry.userIdnum,
                    UserName = entry.userFullname,
                    UserRole = entry.role,
                    Activity = entry.userActivity,
                    Timestamp = entry.activityTime
                })
                .Union(filteredDriverList.Select(entry => new
                {
                    LogId = entry.logsId,
                    UserId = entry.driverId,
                    UserName = entry.driverName,
                    UserRole = entry.role,
                    Activity = entry.actions.ToUpper(),
                    Timestamp = entry.date
                }))
                .OrderByDescending(d => d.Timestamp);

                // Loop through the combined list and add them to the DataTable
                foreach (var entry in combinedList)
                {
                    string activity = entry.Activity;

                    if (activity == "0")
                    {
                        activity = "";
                    }

                    string timestamp = entry.Timestamp == DateTime.MinValue ? "" : entry.Timestamp.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    userLogTable.Rows.Add(entry.LogId, entry.UserId, entry.UserName, entry.UserRole, activity, timestamp);
                }
            }
            else
            {
                // Handle null response or invalid selected value
                lblErrorActivity.Visible = true;
                lblErrorActivity.Text = "No record found";
            }

            // Bind the DataTable to the GridView
            gridUserLog.DataSource = userLogTable;
            gridUserLog.DataBind();
        }

        
        //DISPLAY OWNER LOGS
        private void displayOwner()
        {
            string idno = (string)Session["idno"];

            // Retrieve all user logs from the ADMINLOGS table
            FirebaseResponse response = twoBigDB.Get("ADMINLOGS");
            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();

            // Create the DataTable to hold the user logs 
            DataTable userLogTable = new DataTable();
            userLogTable.Columns.Add("LOG ID");
            userLogTable.Columns.Add("USER ID");
            userLogTable.Columns.Add("USER NAME");
            userLogTable.Columns.Add("USER ROLE");
            userLogTable.Columns.Add("ACTIVITY");
            userLogTable.Columns.Add("TIMESTAMP");

            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null)
            {
                //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
                var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno).OrderByDescending(d => d.activityTime);

                if (filteredList.Count() == 0)
                {
                    lblErrorActivity.Text = "No logs found for the owner.";
                    //Response.Write("<script>alert('No logs found for the entered role.');</script>");
                }
                // Loop through the user logs and add them to the DataTable
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

                        userLogTable.Rows.Add(entry.logsId, entry.userIdnum, entry.userFullname, entry.role, activity, timestamp);

                    }
                }
            }
            else
            {
                // Handle null response or invalid selected value
                lblErrorActivity.Visible = true;
                lblErrorActivity.Text = "No record found";
            }

            // Bind the DataTable to the GridView
            gridUserLog.DataSource = userLogTable;
            gridUserLog.DataBind();
        }
        //DISPLAY CASHIER LOGS
        private void displayCashier()
        {
            string idno = (string)Session["idno"];

            // Retrieve all user logs from the ADMINLOGS table
            FirebaseResponse response = twoBigDB.Get("ADMINLOGS/");
            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();

            // Create the DataTable to hold the user logs
            DataTable userLogTable = new DataTable();
            userLogTable.Columns.Add("LOG ID");
            userLogTable.Columns.Add("USER ID");
            userLogTable.Columns.Add("USER NAME");
            userLogTable.Columns.Add("USER ROLE");
            userLogTable.Columns.Add("ACTIVITY");
            userLogTable.Columns.Add("TIMESTAMP");

            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null)
            {
                //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
                var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno).OrderByDescending(d => d.activityTime);

                if (filteredList.Count() == 0)
                {
                    lblErrorActivity.Text = "No logs found for the cashier.";
                    //Response.Write("<script>alert('No logs found for the entered role.');</script>");
                }

                // Loop through the user logs and add them to the DataTable
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

                        userLogTable.Rows.Add(entry.logsId, entry.userIdnum, entry.userFullname, entry.role, activity, timestamp);

                    }
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
            else
            {
                // Handle null response or invalid selected value
                lblErrorActivity.Visible = true;
                lblErrorActivity.Text = "No record found cashier";
            }

            // Bind the DataTable to the GridView
            gridUserLog.DataSource = userLogTable;
            gridUserLog.DataBind();
        }
        //DISPLAY DRIVER LOGS
        private void displayDriver()
        {
            string idno = (string)Session["idno"];

            // Retrieve all logs from the DRIVERSLOG table
            FirebaseResponse responseDriver = twoBigDB.Get("DRIVERSLOG");
            Dictionary<string, DriversLog> driverlog = responseDriver.ResultAs<Dictionary<string, DriversLog>>();

            // Create the DataTable to hold the drivers log
            DataTable userLogTable = new DataTable();
            userLogTable.Columns.Add("LOG ID");
            userLogTable.Columns.Add("USER ID");
            userLogTable.Columns.Add("USER NAME");
            userLogTable.Columns.Add("USER ROLE");
            userLogTable.Columns.Add("ACTIVITY");
            userLogTable.Columns.Add("TIMESTAMP");

            if (responseDriver != null && responseDriver.ResultAs<Dictionary<string, UsersLogs>>() != null)
            {
                //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
                var filtered_driverList = driverlog.Values.Where(d => d.admin_ID.ToString() == idno).OrderByDescending(d => d.date);

                if (filtered_driverList.Count() == 0)
                {
                    lblErrorActivity.Text = "No logs found for the driver.";
                    //Response.Write("<script>alert('No logs found for the entered role.');</script>");
                }
                // Loop through the dricer logs and add them to the DataTable
                foreach (var driver in filtered_driverList)
                {
                    string activity = driver.actions.ToUpper();

                    if (activity == "0")
                    {
                        activity = "";
                    }

                    string timestamp = driver.date == DateTime.MinValue ? "" : driver.date.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    userLogTable.Rows.Add(driver.logsId, driver.driverId, driver.driverName, driver.role, activity, timestamp);
                }
            }
            else
            {
                // Handle null response or invalid selected value
                lblErrorActivity.Text = "No record found for the driver";
            }

            // Bind the DataTable to the GridView
            gridUserLog.DataSource = userLogTable;
            gridUserLog.DataBind();
        }

        //SEARCH ROLE OR FIRSTNAME OF OWNER, CASHIER, OR DRIVER
        protected void btnSearchLogs_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#view').modal();", true);

            string idno = (string)Session["idno"];
            string searchQuery = txtSearch.Text;

            try
            {
                // Check if the search query is valid
                if (string.IsNullOrEmpty(searchQuery))
                {
                    Response.Write("<script>alert ('Please enter a valid user role, first name, or full name!');</script>");
                    return;
                }

                // Retrieve all user logs from the database
                FirebaseResponse response = twoBigDB.Get("ADMINLOGS");
                Dictionary<string, UsersLogs> loglist = response.ResultAs<Dictionary<string, UsersLogs>>();

                // Retrieve all orders from the ORDERS table
                FirebaseResponse responseDriver = twoBigDB.Get("DRIVERSLOG");
                Dictionary<string, DriversLog> driverlog = responseDriver.ResultAs<Dictionary<string, DriversLog>>();

                // Create the DataTable to hold the user logs
                DataTable userLogsTable = new DataTable();
                userLogsTable.Columns.Add("LOG ID");
                userLogsTable.Columns.Add("USER ID"); 
                userLogsTable.Columns.Add("USER NAME");
                userLogsTable.Columns.Add("USER ROLE");
                userLogsTable.Columns.Add("ACTIVITY");
                userLogsTable.Columns.Add("TIMESTAMP");

                if (loglist != null && loglist.Count > 0)
                {
                    // Filter the user logs based on the search query
                    var filteredList = loglist.Values.Where(d =>
                        d.userIdnum.ToString() == idno &&
                        (d.role.ToLower() == searchQuery.ToLower() ||
                         d.userFullname.ToLower().Contains(searchQuery.ToLower())));
                    // Filter the user logs based on the search query
                    var filtered_driverList = driverlog.Values.Where(d =>
                        d.admin_ID.ToString() == idno &&
                        (d.role.ToLower() == searchQuery.ToLower() ||
                         d.driverName.ToLower().Contains(searchQuery.ToLower())));

                    if (filteredList.Count() == 0 && filtered_driverList.Count() == 0)
                    {
                        //display message if search query is not found
                        lblSearchError.Text = "No logs found for the entered role, firstname, lastname or fullname.";
                        //Response.Write("<script>alert('No logs found for the entered role or name.');</script>");
                    }
                    else
                    {
                        // Loop through the entries admin logs and add them to the DataTable
                        foreach (var entry in filteredList)
                        {
                            string timestamp = entry.activityTime == DateTime.MinValue ? "" : entry.activityTime.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            userLogsTable.Rows.Add(entry.logsId, entry.userIdnum, entry.userFullname, entry.role, entry.userActivity, timestamp);
                        }
                        // Loop through the entries for drivers log and add them to the DataTable
                        foreach (var driver in filtered_driverList)
                        {
                            string timestamp = driver.date == DateTime.MinValue ? "" : driver.date.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            userLogsTable.Rows.Add(driver.logsId, driver.driverId, driver.driverName, driver.role, driver.actions.ToUpper(), timestamp);
                        }
                    }
                }
                else
                {
                    lblSearchError.Visible = true;
                    lblSearchError.Text = "Error retrieving logs.";
                    //Response.Write("<script>alert('Error retrieving logs.');</script>");
                }
                lblSearchResult.Visible = true;
                lblSearchResult.Text = "Below is the record of" + " " + searchQuery;

                // Bind the DataTable to the GridView
                GridLogs.DataSource = userLogsTable;
                GridLogs.DataBind();

                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred while processing your request. '); location.reload(); window.location.href = '/Admin/UsersLog.aspx'; </script>" + ex.Message);
            }
        }
        // Sorting data based on the dates entered by the user
        protected void generateSortedData_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];

            //if (string.IsNullOrEmpty(sortStart.Text) && string.IsNullOrEmpty(sortEnd.Text))
            //{
            //    // Handle the missing start or end date condition 
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You must choose a Start or End Date of the logs you want to view.');", true);
            //    return; // Exit the method or return the appropriate response
            //}

            // Get the start and end dates entered by the user
            DateTime? startDate = null;
            DateTime? endDate = null;

            if (!string.IsNullOrEmpty(sortStart.Text))
            {
                startDate = DateTime.Parse(sortStart.Text);
            }

            if (!string.IsNullOrEmpty(sortEnd.Text))
            {
                endDate = DateTime.Parse(sortEnd.Text).AddDays(1); // Add 1 day to include the end date;
            }

            // Check if both start and end dates are the same, then consider only the start date
            if (startDate.HasValue && endDate.HasValue && startDate.Value.Date == endDate.Value.Date)
            {
                endDate = null;
            }

            // Retrieve user logs within the specified date range
            FirebaseResponse response = twoBigDB.Get("ADMINLOGS/");
            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();

            // Retrieve driver logs within the specified date range
            FirebaseResponse responseDriver = twoBigDB.Get("DRIVERSLOG");
            Dictionary<string, DriversLog> driverlog = responseDriver.ResultAs<Dictionary<string, DriversLog>>();

            // Create the DataTable to hold the filtered logs
            DataTable userLogTable = new DataTable();
            userLogTable.Columns.Add("LOG ID");
            userLogTable.Columns.Add("USER ID");
            userLogTable.Columns.Add("USER NAME");
            userLogTable.Columns.Add("USER ROLE");
            userLogTable.Columns.Add("ACTIVITY");
            userLogTable.Columns.Add("TIMESTAMP");

            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null &&
                responseDriver != null && responseDriver.ResultAs<Dictionary<string, DriversLog>>() != null)
            {
                // Combine user logs and driver logs into a single list
                var logsList = userlog.Values
                    .Where(d => d.userIdnum.ToString() == idno &&
                                (!startDate.HasValue || d.activityTime >= startDate.Value) &&
                                (!endDate.HasValue || d.activityTime < endDate.Value))
                    .Select(entry => new
                    {
                        LogId = entry.logsId,
                        UserId = entry.userIdnum,
                        UserName = entry.userFullname,
                        UserRole = entry.role,
                        Activity = entry.userActivity,
                        Timestamp = entry.activityTime
                    })
                    .Union(driverlog.Values
                        .Where(d => (!startDate.HasValue || d.date >= startDate.Value) &&
                                    (!endDate.HasValue || d.date < endDate.Value))
                        .Select(entry => new
                        {
                            LogId = entry.logsId,
                            UserId = entry.driverId,
                            UserName = entry.driverName,
                            UserRole = entry.role,
                            Activity = entry.actions.ToUpper(),
                            Timestamp = entry.date
                        }));

                // Get the selected role from the dropdown
                int selectedRole = int.Parse(drdRole.SelectedValue);

                // Apply sorting based on the selected role
                switch (selectedRole)
                {
                    case 0: // View All
                        logsList = logsList.OrderByDescending(d => d.Timestamp);
                        break;
                    case 1: // Owner/Admin
                        logsList = logsList.Where(d => d.UserRole == "Owner" || d.UserRole == "Admin")
                                           .OrderByDescending(d => d.Timestamp);
                        break;
                    case 2: // Cashier
                        logsList = logsList.Where(d => d.UserRole == "Cashier")
                                           .OrderByDescending(d => d.Timestamp);
                        break;
                    case 3: // Driver
                        logsList = logsList.Where(d => d.UserRole == "Driver")
                                           .OrderByDescending(d => d.Timestamp);
                        break;
                }

                if (logsList.Any())
                {
                    // Loop through the filtered logs and add them to the DataTable
                    foreach (var entry in logsList)
                    {
                        string activity = entry.Activity;

                        if (activity == "0")
                        {
                            activity = "";
                        }

                        string timestamp = entry.Timestamp == DateTime.MinValue ? "" : entry.Timestamp.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        userLogTable.Rows.Add(entry.LogId, entry.UserId, entry.UserName, entry.UserRole, activity, timestamp);
                    }
                }
                else
                {
                    lblErrorActivity.Visible = true;
                    lblErrorActivity.Text = "No logs available between " +
                        (startDate.HasValue ? startDate.Value.ToString("MMMM dd, yyyy") : string.Empty) + " and " +
                        (endDate.HasValue ? endDate.Value.ToString("MMMM dd, yyyy") : string.Empty);
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

        // View logs based on the role chosen
        protected void btnViewRole_Click(object sender, EventArgs e)
        {
            try
            {
                // Call the generateSortedData_Click method to display logs based on the selected role
                generateSortedData_Click(sender, e);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No data exists. " + ex.Message + "'); window.location.href = '/Admin/OnlineOrders.aspx';", true);
            }
        }


        //clear the sorting textbox fields
        protected void clearSort_Click(object sender, EventArgs e)
        {
            sortStart.Text = ""; 
            sortEnd.Text = "";
            Response.Write("<script> window.location.href = '/Admin/UsersLog.aspx'; </script>");
            
        }
    }
}
