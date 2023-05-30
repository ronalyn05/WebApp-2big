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

namespace WRS2big_Web.Admin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient twoBigDB;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Connection to database 
            twoBigDB = new FireSharp.FirebaseClient(config);

            // Get the ID of the currently logged-in owner from session state
            string idno = (string)Session["idno"];

            if (idno == null)
            {
                Response.Write("<script>alert ('Session expired. Please log in again.');</script>");
                return;
            }
            dailySalesDisplay();

            //// Retrieve all orders from the ORDERS table
            //FirebaseResponse response = twoBigDB.Get("ORDERS");
            //Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

            //if (orderlist != null)
            //{
            //    // Filter the list of orders by the owner's ID and the order status and delivery type
            //    List<Order> filteredList = orderlist.Values
            //        .Where(d => d.admin_ID.ToString() == idno)
            //        .ToList();

            //    // Retrieve all orders from the ORDERS table
            //    FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
            //    Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

            //    // Filter the list of orders by the owner's ID and the order status and delivery type
            //    List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
            //    if (walkinOrderlist != null)
            //    {
            //        filteredordersList = walkinOrderlist.Values
            //            .Where(d => d.adminId.ToString() == idno)
            //            .ToList();
            //    }

            //    // Compute the total number of orders today
            //    int totalOrdersToday = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
            //                                               || d.order_OrderStatus == "Accepted");
            //    int totalWalkInOrder = filteredordersList.Count();
            //    int CombinedOrder = totalOrdersToday + totalWalkInOrder;
            //    // Compute the total amount of all orders
            //    //decimal totalOrderAmount = filteredList.Count();
            //    decimal totalOrderAmount = 0;
            //    decimal overAllSales = 0;
            //    decimal totalWalkInAmount = 0;
            //    // Compute the total number of delivery orders
            //    int totalDeliveryOrders = filteredList.Count(d => d.order_OrderStatus == "Delivered" || d.order_OrderStatus == "Received" || d.order_OrderStatus == "Payment Received");
            //    // Compute the total number of reservation orders
            //    int totalReservationOrders = filteredList.Count(d => d.order_DeliveryTypeValue == "Reservation");

            //    foreach (Order order in filteredList)
            //    {
            //        if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
            //            && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
            //        {
            //            totalOrderAmount += order.order_TotalAmount;
            //        }
            //    }
            //    foreach (WalkInOrders order in filteredordersList)
            //    {
            //        totalWalkInAmount += order.totalAmount;
            //    }

            //    overAllSales = totalOrderAmount + totalWalkInAmount;

            //    // Display the total amount of all orders
            //    //lblTotalSales.Text = overAllSales.ToString();
            //    //lblDeliveries.Text = totalDeliveryOrders.ToString();// Display the total of deliveries
            //    lblOrders.Text = CombinedOrder.ToString();// Display the total of all orders
            //   // lblReservations.Text = totalReservationOrders.ToString();// Display the total reservations


            //    //displayTankSupply();
            //}
            //else
            //{
            //    // handle the case where orderlist is null
            //    //lblTotalSales.Text = "Sales not found";
            //    //lblDeliveries.Text = "Deliveries not found";
            //    lblOrders.Text = "Orders not found";
            //    //lblReservations.Text = "Reservation not found";

            //}
        }

        private void dailySalesDisplay()
        {
            string idno = (string)Session["idno"];
            //DateTime fromDate = DateTime.MinValue;
            //DateTime toDate = DateTime.MinValue;

            DateTime fromDate = DateTime.Today;
            DateTime toDate = DateTime.Today.AddDays(1).AddSeconds(-1);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

            //// Create the DataTable to hold the orders
            //DataTable salesordersTable = new DataTable();
            //salesordersTable.Columns.Add("Order ID");
            //salesordersTable.Columns.Add("Customer Name");
            //salesordersTable.Columns.Add("Order Status");
            //salesordersTable.Columns.Add("Payment Mode");
            //salesordersTable.Columns.Add("Delivery Type");
            //salesordersTable.Columns.Add("Transaction Type");
            //salesordersTable.Columns.Add("Total Amount");
            //salesordersTable.Columns.Add("Order Date");

            // Get the selected date range from the dropdown list
            //string dateRange = ddlDateRange.SelectedValue;

            if (response != null && response.ResultAs<Order>() != null)
            {
                if (orderlist != null)
                {
                    //  Filter the list of orders by the owner's ID and the order status and delivery type
                    List<Order> filteredList = orderlist.Values
                    .Where(d => d.admin_ID.ToString() == idno && (d.orderDate >= fromDate && d.orderDate <= toDate) && d.order_OrderStatus == "Pending")
                    .ToList();

                    //  Retrieve all orders from the ORDERS table
                    FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                    Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();


                    // Filter the list of orders by the owner's ID and the order status and delivery type
                    List<WalkInOrders> filteredordersList = new List<WalkInOrders>();


                    filteredordersList = walkinOrderlist.Values
                           .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
                           .ToList();

                    // Compute the total number of orders today
                    int totalOrdersToday = filteredList.Count();

                    decimal totalOrderAmount = 0;
                    decimal overAllSales = 0;
                    decimal totalWalkInAmount = 0;
                    // Check if there are no walk-in sales found
                    if (filteredList.Count == 0)
                    {
                        lblOnlineSales.Visible = true;
                        //lblErrorWalkin.Visible = false;
                        lblOnlineSales.Text = "No online order sales found for today.";

                    }
                    foreach (Order order in filteredList)
                    {

                        totalOrderAmount += order.order_TotalAmount;

                       
                    }
                    // Check if there are no walk-in sales found
                    if (filteredordersList.Count == 0)
                    {
                        //lblErrorOnline.Visible = false;
                        lblWalkinOrders.Visible = true;
                        lblWalkinOrders.Text = "No walk-in order sales found for today.";

                    }
                    foreach (WalkInOrders order in filteredordersList)
                    {

                        totalWalkInAmount += order.totalAmount;
                    }


                    overAllSales = totalOrderAmount + totalWalkInAmount;

                    lblOnlineSales.Text = "Php." + " " + totalOrderAmount.ToString();
                    lblWalkinOrders.Text = "Php." + " " + totalWalkInAmount.ToString();
                    lblOverallTotalSales.Text = "Php." + " " + overAllSales.ToString();
                    lblOrders.Text = totalOrdersToday.ToString();

                    lblOverallTotalSales.Visible = true;
                    lblOnlineSales.Visible = true;
                    lblWalkinOrders.Visible = true;

                }
                else
                {
                    // Handle null response or invalid selected value
                    lblOverallTotalSales.Text = "No overall record of sales found for today";
                    lblOnlineSales.Text = "No online record of sales found for today";
                    lblWalkinOrders.Text = "No walkin record of sales found for today";
                    lblOrders.Text = "Orders not found";
                }
            }
            else
            {
                // Handle null response or invalid selected value
                lblOverallTotalSales.Text = "No overall record of sales found for today";
                lblOnlineSales.Text = "No online record of sales found for today";
                lblWalkinOrders.Text = "No walkin record of sales found for today";
                lblOrders.Text = "Orders not found";
            }
        }


        //LOGOUT
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            // Get the log ID from the session
            int logsId = (int)Session["logsId"]; 

            // Retrieve the existing Users log object from the database
            FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
            UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

            //Get the current date and time
            DateTime addedTime = DateTime.Now;

            // Log user activity
            var log = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = logsId,
                userFullname = (string)Session["fullname"],
                userActivity = "LOGGED OUT",
                activityTime = addedTime
            };

            twoBigDB.Update("ADMINLOGS/" + log.logsId, log);

            Session.Abandon();
            Session.RemoveAll();
            Session["idno"] = null;
            Session["password"] = null;
            Session.Clear();
            Response.Redirect("LandingPage/Account.aspx");
        }
    }
}