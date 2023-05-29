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

            //displayActivityAsc();
            //displayActivityDesc();
            //displayDateOld();
            //displayDateNew();
            //WORKING BUT NEED TO BE MODIFIED

            string idno = (string)Session["idno"];

            // Get the log ID from the session
            int logsId = (int)Session["logsId"];

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
                    //string timestamp = entry.activityTime.ToString();

                    //if (DateTimeOffset.Parse(timestamp) == DateTimeOffset.MinValue)
                    //{
                    //    timestamp = " ";
                    //}
                    string timestamp = entry.activityTime == DateTime.MinValue ? "" : entry.activityTime.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    userLogTable.Rows.Add(entry.logsId, entry.userFullname, activity, timestamp);
                   
                    //Retrieve the existing Users log object from the database
                    FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + entry.logsId);
                    UsersLogs existingLog = resLog.ResultAs<UsersLogs>();


                    // Log user activity
                    var log = new UsersLogs
                    {
                        userIdnum = int.Parse(idno),
                        logsId = logsId,
                       // orderId = existingLog.orderId,
                        userFullname = (string)Session["fullname"],
                        activityTime = existingLog.activityTime,
                        userActivity = activity
                    };
                    // Update the userLogTable with the user's activity
                    twoBigDB.Update("ADMINLOGS/" + log.logsId, log);
                  
                }
            }
            else
            {
                // Handle null response or invalid selected value
                userLogTable.Rows.Add("No data found", "", "", "", "", "", "");
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

                            userLogsTable.Rows.Add(entry.logsId, entry.userIdnum, entry.userFullname, entry.userActivity, timestamp);
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
        protected void generateSortedData_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            // Get the selected start and end dates
            DateTime startDate = DateTime.Parse(sortStart.Text);
            DateTime endDate = DateTime.Parse(sortEnd.Text).AddDays(1); // Add one day to include the end date

            try
            {

                // Retrieve all orders from the ORDERS table
                FirebaseResponse responselist = twoBigDB.Get("ADMINLOGS");
                Dictionary<string, UsersLogs> loglist = responselist.ResultAs<Dictionary<string, UsersLogs>>();

                //sa pag create sa table 
                DataTable userLogTable = new DataTable();
                userLogTable.Columns.Add("LOG ID");
                userLogTable.Columns.Add("USER NAME");
                userLogTable.Columns.Add("ACTIVITY");
                userLogTable.Columns.Add("TIMESTAMP");

                if (responselist != null && loglist != null)
                {

                    if (!DateTime.TryParse(sortStart.Text, out startDate) || !DateTime.TryParse(sortEnd.Text, out endDate))
                    {
                        // Handle the invalid date format or parsing error (e.g., display an alert)
                        Response.Write("<script>alert('Invalid date format. Please enter the dates in the correct format.');</script>");
                        return; // Exit the method or return the appropriate response
                    }

                    // Check for empty start or end date
                    if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
                    {
                        // Handle the missing start or end date condition (e.g., display an alert)
                        Response.Write("<script>alert('You must choose a Start and End Date');</script>");
                        return; // Exit the method or return the appropriate response
                    }

                    // Filter the data based on the selected dates
                    var filteredList = loglist.Values.Where(d => d.userIdnum.ToString() == idno && d.activityTime >= startDate && d.activityTime < endDate);

                    // Create a list to hold the filtered sales data
                    List<Model.UsersLogs> logsList = new List<Model.UsersLogs>(filteredList);

                    // Sort the sales list in descending order by date and time
                    logsList.Sort((x, y) => y.activityTime.CompareTo(x.activityTime));

                    // Loop through the entries and add them to the DataTable
                    foreach (var entry in logsList)
                    {
                        string timestamp = entry.activityTime == DateTime.MinValue ? "" : entry.activityTime.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        userLogTable.Rows.Add(entry.logsId, entry.userIdnum, entry.userFullname, entry.userActivity, timestamp);
                    }


                    // Bind the DataTable to the GridView
                    gridUserLog.DataSource = userLogTable;
                    gridUserLog.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('Error retrieving logs.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred while processing your request. '); location.reload(); window.location.href = '/Admin/UsersLog.aspx'; </script>" + ex.Message);
            }

        }
        protected void clearSort_Click(object sender, EventArgs e)
        {
            sortStart.Text = "";
            sortEnd.Text = "";
            Response.Write("<script> window.location.href = '/superAdmin/UsersLog.aspx'; </script>");
            
        }
    }
}
