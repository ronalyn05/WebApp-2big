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

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

            if (orderlist != null)
            {
                // Filter the list of orders by the owner's ID and the order status and delivery type
                List<Order> filteredList = orderlist.Values
                    .Where(d => d.admin_ID.ToString() == idno)
                    .ToList();

                // Retrieve all orders from the ORDERS table
                FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                // Filter the list of orders by the owner's ID and the order status and delivery type
                List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
                if (walkinOrderlist != null)
                {
                    filteredordersList = walkinOrderlist.Values
                        .Where(d => d.adminId.ToString() == idno)
                        .ToList();
                }

                // Compute the total number of orders today
                int totalOrdersToday = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
                                                           || d.order_OrderStatus == "Accepted");
                int totalWalkInOrder = filteredordersList.Count();
                int CombinedOrder = totalOrdersToday + totalWalkInOrder;
                // Compute the total amount of all orders
                //decimal totalOrderAmount = filteredList.Count();
                decimal totalOrderAmount = 0;
                decimal overAllSales = 0;
                decimal totalWalkInAmount = 0;
                // Compute the total number of delivery orders
                int totalDeliveryOrders = filteredList.Count(d => d.order_OrderStatus == "Delivered" || d.order_OrderStatus == "Received" || d.order_OrderStatus == "Payment Received");
                // Compute the total number of reservation orders
                int totalReservationOrders = filteredList.Count(d => d.order_DeliveryTypeValue == "Reservation");

                foreach (Order order in filteredList)
                {
                    if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
                        && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
                    {
                        totalOrderAmount += order.order_TotalAmount;
                    }
                }
                foreach (WalkInOrders order in filteredordersList)
                {
                    totalWalkInAmount += order.totalAmount;
                }

                overAllSales = totalOrderAmount + totalWalkInAmount;

                // Display the total amount of all orders
                lblTotalSales.Text = overAllSales.ToString();
                lblDeliveries.Text = totalDeliveryOrders.ToString();// Display the total of deliveries
                lblOrders.Text = CombinedOrder.ToString();// Display the total of all orders
                lblReservations.Text = totalReservationOrders.ToString();// Display the total reservations


                //displayTankSupply();
            }
            else
            {
                // handle the case where orderlist is null
                lblTotalSales.Text = "Sales not found";
                lblDeliveries.Text = "Deliveries not found";
                lblOrders.Text = "Orders not found";
                lblReservations.Text = "Reservation not found";

            }
        }




        //LOGOUT
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            // Get the log ID from the session
            int logsId = (int)Session["logsId"]; 

            // Retrieve the existing Users log object from the database
            FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
            UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

            //Get the current date and time
            DateTimeOffset addedTime = DateTimeOffset.UtcNow;

            // Log user activity
            var log = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = logsId,
                userFullname = (string)Session["fullname"],
                userActivity = "LOGGED OUT",
                activityTime = addedTime
            };

            twoBigDB.Update("USERSLOG/" + log.logsId, log);

            Session.Abandon();
            Session.RemoveAll();
            Session["idno"] = null;
            Session["password"] = null;
            Session.Clear();
            Response.Redirect("LandingPage/Account.aspx");
        }
    }
}