﻿using System;
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
            FirebaseResponse response = twoBigDB.Get("USERSLOG/");
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
                    string timestamp = entry.activityTime.ToString();

                    if (DateTimeOffset.Parse(timestamp) == DateTimeOffset.MinValue)
                    {
                        timestamp = " ";
                    }
                        userLogTable.Rows.Add(entry.logsId, entry.userFullname, activity, timestamp);
                   
                    //Retrieve the existing Users log object from the database
                    FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + entry.logsId);
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
                    twoBigDB.Update("USERSLOG/" + log.logsId, log);
                  
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
            try
            {
                string activity = txtSearch.Text;

              

                // Retrieve all orders from the ORDERS table
                FirebaseResponse responselist = twoBigDB.Get("USERSLOG");
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
                            string dateAdded = entry.activityTime.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            userLogsTable.Rows.Add(entry.logsId, entry.userIdnum, entry.userFullname, entry.userActivity, dateAdded);
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



            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Select '); location.reload(); window.location.href = '/Admin/UsersLog.aspx'; </script>" + ex.Message);
            }
        }

        //protected void gridUserLog_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        // Get the activity column value of the current row
        //        string activity = DataBinder.Eval(e.Row.DataItem, "ACTIVITY").ToString();

        //        // Replace any comma followed by a space with a line break
        //        activity = activity.Replace(", ", "<br>");

        //        // Set the modified activity value to the activity column of the current row
        //        e.Row.Cells[3].Text = activity;
        //    }
        //}

        //SEARCH
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string selectedOption = ddlSearchOptions.SelectedValue;


        //        if (selectedOption == "0")
        //        {
        //            string idno = (string)Session["idno"];

        //            // Get the log ID from the session
        //            int logsId = (int)Session["logsId"];

        //            // Retrieve all orders from the ORDERS table
        //            FirebaseResponse response = twoBigDB.Get("USERSLOG/");
        //            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();
        //            //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
        //            var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno).OrderByDescending(d => d.userActivity);

        //            // Create the DataTable to hold the orders
        //            //sa pag create sa table 
        //            DataTable userLogTable = new DataTable();
        //            userLogTable.Columns.Add("LOG ID");
        //            userLogTable.Columns.Add("USER NAME");
        //            userLogTable.Columns.Add("ACTIVITY");
        //            userLogTable.Columns.Add("TIMESTAMP");

        //            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null)
        //            {
        //                // Loop through the orders and add them to the DataTable
        //                foreach (var entry in filteredList)
        //                {
        //                    string activity = entry.userActivity;

        //                    if (activity == "0")
        //                    {
        //                        activity = "";
        //                    }
        //                    string timestamp = entry.activityTime.ToString();

        //                    if (DateTimeOffset.Parse(timestamp) == DateTimeOffset.MinValue)
        //                    {
        //                        timestamp = " ";
        //                    }
        //                    userLogTable.Rows.Add(entry.logsId, entry.userFullname, activity, timestamp);

        //                    //Retrieve the existing Users log object from the database
        //                    FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + entry.logsId);
        //                    UsersLogs existingLog = resLog.ResultAs<UsersLogs>();


        //                    // Log user activity
        //                    var log = new UsersLogs
        //                    {
        //                        userIdnum = int.Parse(idno),
        //                        logsId = logsId,
        //                        // orderId = existingLog.orderId,
        //                        userFullname = (string)Session["fullname"],
        //                        activityTime = existingLog.activityTime,
        //                        userActivity = activity
        //                    };
        //                    // Update the userLogTable with the user's activity
        //                    twoBigDB.Update("USERSLOG/" + log.logsId, log);

        //                }
        //            }
        //            else
        //            {
        //                // Handle null response or invalid selected value
        //                userLogTable.Rows.Add("No data found", "", "", "", "", "", "");
        //            }

        //            // Bind the DataTable to the GridView
        //            gridUserLog.DataSource = userLogTable;
        //            gridUserLog.DataBind();

        //        }
        //        else if (selectedOption == "1")
        //        {
        //            string idno = (string)Session["idno"];

        //            // Get the log ID from the session
        //            int logsId = (int)Session["logsId"];

        //            // Retrieve all orders from the ORDERS table
        //            FirebaseResponse response = twoBigDB.Get("USERSLOG/");
        //            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();
        //            //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
        //            var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno).OrderBy(d => d.userActivity);

        //            // Create the DataTable to hold the orders
        //            //sa pag create sa table 
        //            DataTable userLogTable = new DataTable();
        //            userLogTable.Columns.Add("LOG ID");
        //            userLogTable.Columns.Add("USER NAME");
        //            userLogTable.Columns.Add("ACTIVITY");
        //            userLogTable.Columns.Add("TIMESTAMP");

        //            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null)
        //            {
        //                // Loop through the orders and add them to the DataTable
        //                foreach (var entry in filteredList)
        //                {
        //                    string activity = entry.userActivity;

        //                    if (activity == "0")
        //                    {
        //                        activity = "";
        //                    }
        //                    string timestamp = entry.activityTime.ToString();

        //                    if (DateTimeOffset.Parse(timestamp) == DateTimeOffset.MinValue)
        //                    {
        //                        timestamp = " ";
        //                    }
        //                    userLogTable.Rows.Add(entry.logsId, entry.userFullname, activity, timestamp);

        //                    //Retrieve the existing Users log object from the database
        //                    FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + entry.logsId);
        //                    UsersLogs existingLog = resLog.ResultAs<UsersLogs>();


        //                    // Log user activity
        //                    var log = new UsersLogs
        //                    {
        //                        userIdnum = int.Parse(idno),
        //                        logsId = logsId,
        //                        // orderId = existingLog.orderId,
        //                        userFullname = (string)Session["fullname"],
        //                        activityTime = existingLog.activityTime,
        //                        userActivity = activity
        //                    };
        //                    // Update the userLogTable with the user's activity
        //                    twoBigDB.Update("USERSLOG/" + log.logsId, log);

        //                }
        //            }
        //            else
        //            {
        //                // Handle null response or invalid selected value
        //                userLogTable.Rows.Add("No data found", "", "", "", "", "", "");
        //            }

        //            // Bind the DataTable to the GridView
        //            gridUserLog.DataSource = userLogTable;
        //            gridUserLog.DataBind();

        //        }
        //        else if (selectedOption == "2")
        //        {
        //            string idno = (string)Session["idno"];

        //            // Get the log ID from the session
        //            int logsId = (int)Session["logsId"];

        //            // Retrieve all orders from the ORDERS table
        //            FirebaseResponse response = twoBigDB.Get("USERSLOG/");
        //            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();
        //            //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
        //            var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno).OrderByDescending(d => d.activityTime);

        //            // Create the DataTable to hold the orders
        //            //sa pag create sa table 
        //            DataTable userLogTable = new DataTable();
        //            userLogTable.Columns.Add("LOG ID");
        //            userLogTable.Columns.Add("USER NAME");
        //            userLogTable.Columns.Add("ACTIVITY");
        //            userLogTable.Columns.Add("TIMESTAMP");

        //            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null)
        //            {
        //                // Loop through the orders and add them to the DataTable
        //                foreach (var entry in filteredList)
        //                {
        //                    string activity = entry.userActivity;

        //                    if (activity == "0")
        //                    {
        //                        activity = "";
        //                    }
        //                    string timestamp = entry.activityTime.ToString();

        //                    if (DateTimeOffset.Parse(timestamp) == DateTimeOffset.MinValue)
        //                    {
        //                        timestamp = " ";
        //                    }
        //                    userLogTable.Rows.Add(entry.logsId, entry.userFullname, activity, timestamp);

        //                    //Retrieve the existing Users log object from the database
        //                    FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + entry.logsId);
        //                    UsersLogs existingLog = resLog.ResultAs<UsersLogs>();


        //                    // Log user activity
        //                    var log = new UsersLogs
        //                    {
        //                        userIdnum = int.Parse(idno),
        //                        logsId = logsId,
        //                        // orderId = existingLog.orderId,
        //                        userFullname = (string)Session["fullname"],
        //                        activityTime = existingLog.activityTime,
        //                        userActivity = activity
        //                    };
        //                    // Update the userLogTable with the user's activity
        //                    twoBigDB.Update("USERSLOG/" + log.logsId, log);

        //                }
        //            }
        //            else
        //            {
        //                // Handle null response or invalid selected value
        //                userLogTable.Rows.Add("No data found", "", "", "", "", "", "");
        //            }

        //            // Bind the DataTable to the GridView
        //            gridUserLog.DataSource = userLogTable;
        //            gridUserLog.DataBind();

        //        }
        //        else if (selectedOption == "3")
        //        {
        //            string idno = (string)Session["idno"];

        //            // Get the log ID from the session
        //            int logsId = (int)Session["logsId"];

        //            // Retrieve all orders from the ORDERS table
        //            FirebaseResponse response = twoBigDB.Get("USERSLOG/");
        //            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();
        //            //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
        //            var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno).OrderBy(d => d.activityTime);

        //            // Create the DataTable to hold the orders
        //            //sa pag create sa table 
        //            DataTable userLogTable = new DataTable();
        //            userLogTable.Columns.Add("LOG ID");
        //            userLogTable.Columns.Add("USER NAME");
        //            userLogTable.Columns.Add("ACTIVITY");
        //            userLogTable.Columns.Add("TIMESTAMP");

        //            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null)
        //            {
        //                // Loop through the orders and add them to the DataTable
        //                foreach (var entry in filteredList)
        //                {
        //                    string activity = entry.userActivity;

        //                    if (activity == "0")
        //                    {
        //                        activity = "";
        //                    }
        //                    string timestamp = entry.activityTime.ToString();

        //                    if (DateTimeOffset.Parse(timestamp) == DateTimeOffset.MinValue)
        //                    {
        //                        timestamp = " ";
        //                    }
        //                    userLogTable.Rows.Add(entry.logsId, entry.userFullname, activity, timestamp);

        //                    //Retrieve the existing Users log object from the database
        //                    FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + entry.logsId);
        //                    UsersLogs existingLog = resLog.ResultAs<UsersLogs>();


        //                    // Log user activity
        //                    var log = new UsersLogs
        //                    {
        //                        userIdnum = int.Parse(idno),
        //                        logsId = logsId,
        //                        // orderId = existingLog.orderId,
        //                        userFullname = (string)Session["fullname"],
        //                        activityTime = existingLog.activityTime,
        //                        userActivity = activity
        //                    };
        //                    // Update the userLogTable with the user's activity
        //                    twoBigDB.Update("USERSLOG/" + log.logsId, log);

        //                }
        //            }
        //            else
        //            {
        //                // Handle null response or invalid selected value
        //                userLogTable.Rows.Add("No data found", "", "", "", "", "", "");
        //            }

        //            // Bind the DataTable to the GridView
        //            gridUserLog.DataSource = userLogTable;
        //            gridUserLog.DataBind();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/UsersLog.aspx';" + ex.Message);
        //    }
        //}

    }
}
