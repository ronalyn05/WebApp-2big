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

            //WORKING BUT NEED TO BE MODIFIED

            string idno = (string)Session["idno"];

            // Get the log ID from the session
            int logsId = (int)Session["logsId"];

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("USERSLOG/");
            Dictionary<string, UsersLogs> userlog = response.ResultAs<Dictionary<string, UsersLogs>>();
            //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
            var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno)
                               .OrderByDescending(d => d.dateLogin);

            // Create the DataTable to hold the orders
            //sa pag create sa table
            DataTable userLogTable = new DataTable();
            userLogTable.Columns.Add("LOG ID");
            userLogTable.Columns.Add("USER NAME");
            userLogTable.Columns.Add("LOGIN TIME");
            userLogTable.Columns.Add("ACTIVITY");
            userLogTable.Columns.Add("LOGOUT TIME");

            if (response != null && response.ResultAs<Dictionary<string, UsersLogs>>() != null)
            {
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    // Check for user actions and set activity property
                    //string activity = "";
                    //if (!string.IsNullOrEmpty(entry.userActivity.ToString()))
                    //{
                    //    //activity += "Added product with ID " + entry.productRefillId + " on " + entry.productrefillDateAdded;
                    //    activity += entry.userActivity;
                    //}
                    //if (!string.IsNullOrEmpty(entry.tankSupplyDateAdded.ToString()))
                    //{
                    //    //activity += "Added tank supply with ID " + entry.tankId + " on " + entry.tankSupplyDateAdded;
                    //    activity += entry.tankId + " on " + entry.tankSupplyDateAdded;
                    //}
                    //if (!string.IsNullOrEmpty(entry.dateOrderAccepted.ToString()))
                    //{
                    //    activity += entry.userActivity + " of " + entry.orderId + " from customer " + entry.cusId 
                    //                + " on " + entry.dateOrderAccepted;
                    //}
                    //if (!string.IsNullOrEmpty(entry.datePaymentReceived.ToString()))
                    //{
                    //    activity += entry.userActivity + " of " + entry.orderId + " on " + entry.datePaymentReceived;
                    //}
                    //if (activity != "")
                    //{
                    //    // Remove the trailing ", " from the activity string
                    //    activity = activity.Substring(0, activity.Length - 2);
                    //}

                    //Update the user's activity in the database
                    //entry.Activity = activity;
                    //twoBigDB.Set("USERSLOG/" + entry.logsId, entry);


                    // Retrieve the existing Users log object from the database
                    FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
                    UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                    // Log user activity
                    var log = new UsersLogs
                    {
                        userIdnum = int.Parse(idno),
                        logsId = logsId,
                        orderId = existingLog.orderId,
                        userFullname = (string)Session["fullname"],
                        dateLogin = existingLog.dateLogin,

                        //  userActivity = activity
                    };

                    twoBigDB.Update("USERSLOG/" + log.logsId, log);

                    // Update the userLogTable with the user's activity
                    //userLogTable.Rows.Add(entry.logsId, entry.userFullname, entry.dateLogin, activity, entry.dateLogout);
                    userLogTable.Rows.Add(entry.logsId, entry.userFullname, entry.dateLogin, entry.userActivity, entry.dateLogout);
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
        protected void gridUserLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the activity column value of the current row
                string activity = DataBinder.Eval(e.Row.DataItem, "ACTIVITY").ToString();

                // Replace any comma followed by a space with a line break
                activity = activity.Replace(", ", "<br>");

                // Set the modified activity value to the activity column of the current row
                e.Row.Cells[3].Text = activity;
            }
        }
        //SEARCH
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedOption = ddlSearchOptions.SelectedValue;


                if (selectedOption == "0")
                {
                    //lblProductData.Text = "PRODUCT REFILL";
                    //gridProductRefill.Visible = true;
                    //gridotherProduct.Visible = false;
                    //productRefillDisplay();
                }
                else if (selectedOption == "1")
                {
                    //lblProductData.Text = "OTHER PRODUCT";
                    //gridProductRefill.Visible = false;
                    //gridotherProduct.Visible = true;
                    //otherProductsDisplay();
                }
                else if (selectedOption == "2")
                {
                    //lblProductData.Text = "OTHER PRODUCT";
                    //gridProductRefill.Visible = false;
                    //gridotherProduct.Visible = true;
                    //otherProductsDisplay();
                }
                else if (selectedOption == "3")
                {
                    //lblProductData.Text = "DELIVERY DETAILS";
                    //gridProductRefill.Visible = false;
                    //gridotherProduct.Visible = false;
                    //deliveryExpressDisplay();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/UsersLog.aspx';" + ex.Message);
            }
        }

    }
}
