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
            // Connection to database 
            twoBigDB = new FireSharp.FirebaseClient(config);

            // Get the ID of the currently logged-in owner from session state
            string idno = (string)Session["idno"];

            if (idno == null)
            {
                Response.Write("<script>alert ('Session expired. Please log in again.');</script>");
                return;
            }


        }

        protected void btnViewSale_Click(object sender, EventArgs e)
        {
            // Get the ID of the currently logged-in owner from session state
            string idno = (string)Session["idno"];

            try
            {
                string selectedOption = ddlSaleTransaction.SelectedValue;

                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                DateTime fromDate = DateTime.MinValue;
                DateTime toDate = DateTime.MinValue;

                switch (selectedOption)
                {
                    case "0": // View All
                        //fromDate = DateTime.Today;
                        //toDate = DateTime.Today.AddDays(1).AddSeconds(-1);

                        // Retrieve all orders from the ORDERS table
                        //FirebaseResponse response = twoBigDB.Get("ORDERS");
                        //Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

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
                            //int totalDeliveryOrders = filteredList.Count(d => d.order_OrderStatus == "Delivered" || d.order_OrderStatus == "Received" || d.order_OrderStatus == "Payment Received");
                            //// Compute the total number of reservation orders
                            //int totalReservationOrders = filteredList.Count(d => d.order_DeliveryTypeValue == "Reservation");

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
                            lbloverallTotalSale.Text = "OVERALL TOTAL SALES";
                            // Display the total number of all orders
                            lblOrders.Text = CombinedOrder.ToString();
                            lblCombinedOrders.Text = "TOTAL ORDERS ";

                            lblTodaysSale.Visible = false;
                            lblYesterdaySale.Visible = false;
                            lblPastWeekSale.Visible = false;
                            lblPastmonthSale.Visible = false;
                            lbl2023Sale.Visible = false;
                            lbl_TodaysSale.Visible = false;
                            lbl_YesterdaySale.Visible = false;
                            lbl_PastWeekSale.Visible = false;
                            lbl_PastmonthSale.Visible = false;
                            lbl_2023Sale.Visible = false;
                        }
                        else
                        {
                            // handle the case where orderlist is null
                            lblTotalSales.Text = "Sales not found";
                            //lblDeliveries.Text = "Deliveries not found";
                            //lblOrders.Text = "Orders not found";
                            //lblReservations.Text = "Reservation not found";

                        }
                        break;
                    case "1": // Today
                        fromDate = DateTime.Today;
                        toDate = DateTime.Today.AddDays(1).AddSeconds(-1);
                        if (orderlist != null)
                        {
                            // Filter the list of orders by the owner's ID and the order status and delivery type
                            List<Order> filteredList = orderlist.Values
                                .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
                                .ToList();

                            // Retrieve all orders from the WALKINORDERS table
                            FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                            Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                            // Filter the list of walk-in orders by the owner's ID and the order status and delivery type
                            List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
                            if (walkinOrderlist != null)
                            {
                                filteredordersList = walkinOrderlist.Values
                                    .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
                                    .ToList();
                            }

                            // Compute the total number of orders today
                            int totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
                                                                       || d.order_OrderStatus == "Accepted");
                            int totalWalkInOrder = filteredordersList.Count();
                            int CombinedOrder = totalOrders + totalWalkInOrder;

                            // Compute the total amount of orders today
                            decimal totalAmount = 0;
                            decimal totalWalkInAmount = 0;

                            foreach (Order order in filteredList)
                            {
                                if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
                                    && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
                                {
                                    totalAmount += order.order_TotalAmount;
                                }
                            }

                            foreach (WalkInOrders order in filteredordersList)
                            {
                                totalWalkInAmount += order.totalAmount;
                            }

                            // Compute todays sales 
                            decimal todaySales = totalAmount + totalWalkInAmount;

                            // Display todays total sales 
                            lblTodaysSale.Text = todaySales.ToString();
                            lbl_TodaysSale.Text = "TODAY/S SALES";
                            // Display the total number of todays orders
                            lblOrders.Text = CombinedOrder.ToString();
                            lblCombinedOrders.Text = "TODAY/S ORDERS ";

                            lbloverallTotalSale.Visible = false;
                            lblTotalSales.Visible = false;
                            lblYesterdaySale.Visible = false;
                            lblPastWeekSale.Visible = false;
                            lblPastmonthSale.Visible = false;
                            lbl2023Sale.Visible = false;
                            lbl_YesterdaySale.Visible = false;
                            lbl_PastWeekSale.Visible = false;
                            lbl_PastmonthSale.Visible = false;
                            lbl_2023Sale.Visible = false;
                        }
                        else
                        {
                            // handle the case where orderlist is null
                            lblTodaysSale.Text = "Sales not found";
                            lblOrders.Text = "Orders not found";
                        }
                        break;


                    case "¨2": // Yesterday
                        fromDate = DateTime.Today.AddDays(-1);
                        toDate = DateTime.Today.AddSeconds(-1);
                        if (orderlist != null)
                        {
                            // Filter the list of orders by the owner's ID and the order status and delivery type
                            List<Order> filteredList = orderlist.Values
                                .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
                                .ToList();

                            // Retrieve all orders from the WALKINORDERS table
                            FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                            Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                            // Filter the list of walk-in orders by the owner's ID and the order status and delivery type
                            List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
                            if (walkinOrderlist != null)
                            {
                                filteredordersList = walkinOrderlist.Values
                                    .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
                                    .ToList();
                            }

                            // Compute the total number of orders from yesterday
                            int totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
                                                                       || d.order_OrderStatus == "Accepted");
                            int totalWalkInOrder = filteredordersList.Count();
                            int CombinedOrder = totalOrders + totalWalkInOrder;

                            // Compute the total amount of orders from yesterday
                            decimal totalAmount = 0;
                            decimal totalWalkInAmount = 0;

                            foreach (Order order in filteredList)
                            {
                                if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
                                    && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
                                {
                                    totalAmount += order.order_TotalAmount;
                                }
                            }

                            foreach (WalkInOrders order in filteredordersList)
                            {
                                totalWalkInAmount += order.totalAmount;
                            }

                            // Compute yesterdays sales 
                            decimal yesterdaySales = totalAmount + totalWalkInAmount;

                            // Display yesterdays sales
                            lblYesterdaySale.Text = yesterdaySales.ToString();
                            lbl_YesterdaySale.Text = "YESTERDAY/S SALES";
                            // Display the total number of orders yesterday
                            lblOrders.Text = CombinedOrder.ToString();
                            lblCombinedOrders.Text = "TOTAL ORDERS ";

                            lbloverallTotalSale.Visible = false;
                            lblTotalSales.Visible = false;
                            lblTodaysSale.Visible = false;
                            lblPastWeekSale.Visible = false;
                            lblPastmonthSale.Visible = false;
                            lbl2023Sale.Visible = false;
                            lbl_TodaysSale.Visible = false;
                            lbl_PastWeekSale.Visible = false;
                            lbl_PastmonthSale.Visible = false;
                            lbl_2023Sale.Visible = false;
                        }
                        else
                        {
                            // handle the case where orderlist is null
                            lblTotalSales.Text = "Sales not found";
                            lblOrders.Text = "Orders not found";
                        }
                        break;

                    case "3": // Past 7 days
                        fromDate = DateTime.Today.AddDays(-6);
                        toDate = DateTime.Today.AddDays(1).AddSeconds(-1);

                        if (orderlist != null)
                        {
                            // Filter the list of orders by the owner's ID and the order status and delivery type
                            List<Order> filteredList = orderlist.Values
                                .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
                                .ToList();

                            // Retrieve all orders from the WALKINORDERS table
                            FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                            Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                            // Filter the list of walk-in orders by the owner's ID and the order status and delivery type
                            List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
                            if (walkinOrderlist != null)
                            {
                                filteredordersList = walkinOrderlist.Values
                                    .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
                                    .ToList();
                            }

                            // Compute the total number of orders in the past 7 days
                            int totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
                                                                       || d.order_OrderStatus == "Accepted");
                            int totalWalkInOrder = filteredordersList.Count();
                            int CombinedOrder = totalOrders + totalWalkInOrder;

                            // Compute the total amount of orders in the past 7 days
                            decimal totalAmount = 0;
                            decimal totalWalkInAmount = 0;

                            foreach (Order order in filteredList)
                            {
                                if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
                                    && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
                                {
                                    totalAmount += order.order_TotalAmount;
                                }
                            }

                            foreach (WalkInOrders order in filteredordersList)
                            {
                                totalWalkInAmount += order.totalAmount;
                            }

                            // Compute the overall sales in the past 7 days
                            decimal pastWeekSale = totalAmount + totalWalkInAmount;

                            // Display the total sales in the past 7 days
                            lblPastWeekSale.Text = pastWeekSale.ToString();
                            lbl_PastWeekSale.Text = "PAST WEEK SALES";
                            // Display the total number of orders in the past 7 days
                            lblOrders.Text = CombinedOrder.ToString();
                            lblCombinedOrders.Text = "TOTAL ORDERS ";

                            lbloverallTotalSale.Visible = false;
                            lblTotalSales.Visible = false;
                            lblTodaysSale.Visible = false;
                            lblYesterdaySale.Visible = false;
                            lblPastmonthSale.Visible = false;
                            lbl2023Sale.Visible = false;
                            lbl_TodaysSale.Visible = false;
                            lbl_YesterdaySale.Visible = false;
                            lbl_PastmonthSale.Visible = false;
                            lbl_2023Sale.Visible = false;
                        }
                        else
                        {
                            // handle the case where orderlist is null
                            lblTotalSales.Text = "Sales not found";
                            lblOrders.Text = "Orders not found";
                        }
                        break;


                    case "4":  // Past 30 days
                        fromDate = DateTime.Today.AddDays(-30);
                        toDate = DateTime.Today.AddDays(1).AddSeconds(-1);
                        if (orderlist != null)
                        {
                            // Filter the list of orders by the owner's ID and the order status and delivery type
                            List<Order> filteredList = orderlist.Values
                                .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
                                .ToList();

                            // Retrieve all orders from the WALKINORDERS table
                            FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                            Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                            // Filter the list of walk-in orders by the owner's ID and the order status and delivery type
                            List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
                            if (walkinOrderlist != null)
                            {
                                filteredordersList = walkinOrderlist.Values
                                    .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
                                    .ToList();
                            }

                            // Compute the total number of orders in the past 30 days
                            int totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
                                                                       || d.order_OrderStatus == "Accepted");
                            int totalWalkInOrder = filteredordersList.Count();
                            int CombinedOrder = totalOrders + totalWalkInOrder;

                            // Compute the total amount of orders in the past 30 days
                            decimal totalAmount = 0;
                            decimal totalWalkInAmount = 0;

                            foreach (Order order in filteredList)
                            {
                                if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
                                    && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
                                {
                                    totalAmount += order.order_TotalAmount;
                                }
                            }

                            foreach (WalkInOrders order in filteredordersList)
                            {
                                totalWalkInAmount += order.totalAmount;
                            }

                            // Compute the overall sales in the past 30 days
                            decimal pastMonthSale = totalAmount + totalWalkInAmount;

                            // Display the total sales in the past 30 days
                            lblPastmonthSale.Text = pastMonthSale.ToString();
                            lbl_PastmonthSale.Text = "PAST MONTH SALES";
                            // Display the total number of orders in the past 30 days
                            lblOrders.Text = CombinedOrder.ToString();
                            lblCombinedOrders.Text = "TOTAL ORDERS ";

                            lbloverallTotalSale.Visible = false;
                            lblTotalSales.Visible = false;
                            lblTodaysSale.Visible = false;
                            lblYesterdaySale.Visible = false;
                            lblPastWeekSale.Visible = false;
                            lbl2023Sale.Visible = false;
                            lbl_TodaysSale.Visible = false;
                            lbl_YesterdaySale.Visible = false;
                            lbl_PastWeekSale.Visible = false;
                            lbl_2023Sale.Visible = false;
                        }
                        else
                        {
                            // handle the case where orderlist is null
                            lblTotalSales.Text = "Sales not found";
                            lblOrders.Text = "Orders not found";
                        }
                        break;
                    case "5": // Year 2023
                        fromDate = new DateTime(2023, 1, 1);
                        toDate = new DateTime(2023, 12, 31, 23, 59, 59);
                        if (orderlist != null)
                        {
                            // Filter the list of orders by the owner's ID and the order status and delivery type
                            List<Order> filteredList = orderlist.Values
                                .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
                                .ToList();

                            // Retrieve all orders from the WALKINORDERS table
                            FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                            Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                            // Filter the list of walk-in orders by the owner's ID and the order status and delivery type
                            List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
                            if (walkinOrderlist != null)
                            {
                                filteredordersList = walkinOrderlist.Values
                                    .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
                                    .ToList();
                            }

                            // Compute the total number of orders in the past year
                            int totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
                                                                       || d.order_OrderStatus == "Accepted");
                            int totalWalkInOrder = filteredordersList.Count();
                            int CombinedOrder = totalOrders + totalWalkInOrder;

                            // Compute the total amount of orders year
                            decimal totalAmount = 0;
                            decimal totalWalkInAmount = 0;

                            foreach (Order order in filteredList)
                            {
                                if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
                                    && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
                                {
                                    totalAmount += order.order_TotalAmount;
                                }
                            }

                            foreach (WalkInOrders order in filteredordersList)
                            {
                                totalWalkInAmount += order.totalAmount;
                            }

                            // Compute the overall sales within a year
                            decimal yearSales = totalAmount + totalWalkInAmount;

                            // Display the total sales within a year
                            lbl2023Sale.Text = yearSales.ToString();
                            lbl_2023Sale.Text = "2023 SALES";
                            // Display the total number of orders within a year
                            lblOrders.Text = CombinedOrder.ToString();
                            lblCombinedOrders.Text = "TOTAL ORDERS ";

                            lbloverallTotalSale.Visible = false;
                            lblTotalSales.Visible = false;
                            lblTodaysSale.Visible = false;
                            lblYesterdaySale.Visible = false;
                            lblPastmonthSale.Visible = false;
                            lblPastWeekSale.Visible = false;
                            lbl_TodaysSale.Visible = false;
                            lbl_YesterdaySale.Visible = false;
                            lbl_PastWeekSale.Visible = false;
                            lbl_PastmonthSale.Visible = false;
                        }
                        else
                        {
                            // handle the case where orderlist is null
                            lblTotalSales.Text = "Sales not found";
                            lblOrders.Text = "Orders not found";
                        }
                        break;

                    default:
                        // Invalid option selected
                        break;
                }

                // Now that you have the fromDate and toDate, you can use them to generate your sales data.
                // For example, you could call a method that queries your database for sales data between these dates,
                // and then bind the results to a GridView or some other control on your page.
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('No data exists.'); window.location.href = '/Admin/Reports.aspx';</script>" + ex.Message);
            }
        }

      



            //protected void btnViewSale_Click(object sender, EventArgs e)
            //{
            //    // Get the selected value of the ddlSaleTransaction dropdown list
            //    int selectedValue = Convert.ToInt32(ddlSaleTransaction.SelectedValue);

            //    // Connection to database 
            //    twoBigDB = new FireSharp.FirebaseClient(config);

            //    // Get the ID of the currently logged-in owner from session state
            //    string idno = (string)Session["idno"];

            //    if (idno == null)
            //    {
            //        Response.Write("<script>alert ('Session expired. Please log in again.');</script>");
            //        return;
            //    }

            //    // Retrieve all orders from the ORDERS table
            //    FirebaseResponse response = twoBigDB.Get("ORDERS");
            //    Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

            //    if (orderlist != null)
            //    {
            //        // Filter the list of orders by the owner's ID and the order status and delivery type
            //        List<Order> filteredList = orderlist.Values
            //            .Where(d => d.admin_ID.ToString() == idno)
            //            .ToList();

            //        // Retrieve all orders from the ORDERS table
            //        FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
            //        Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

            //        // Filter the list of orders by the owner's ID and the order status and delivery type
            //        List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
            //        if (walkinOrderlist != null)
            //        {
            //            filteredordersList = walkinOrderlist.Values
            //                .Where(d => d.adminId.ToString() == idno)
            //                .ToList();
            //        }

            //        // Compute the total number of orders based on the selected value of the ddlSaleTransaction dropdown list
            //        int totalOrders = 0;
            //        decimal totalAmount = 0;
            //        switch (selectedValue)
            //        {
            //            case 0: // Today
            //                totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
            //                                                           || d.order_OrderStatus == "Accepted");
            //                totalAmount = filteredList
            //                    .Where(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
            //                        || d.order_OrderStatus == "Accepted")
            //                    .Sum(d => d.order_TotalAmount);
            //                break;
            //            case 1: // Yesterday
            //                DateTime yesterday = DateTime.Today.AddDays(-1);
            //                totalOrders = filteredList.Count(d => d.order_OrderStatus == "Delivered" && d.order_DeliveryDate == yesterday
            //                                                           || d.order_OrderStatus == "Received" && d.order_DeliveryDate == yesterday
            //                                                           || d.order_OrderStatus == "Payment Received" && d.order_DeliveryDate == yesterday);
            //                totalAmount = filteredList
            //                    .Where(d => d.order_OrderStatus == "Delivered" && d.order_DeliveryDate == yesterday
            //                        || d.order_OrderStatus == "Received" && d.order_DeliveryDate == yesterday
            //                        || d.order_OrderStatus == "Payment Received" && d.order_DeliveryDate == yesterday)
            //                    .Sum(d => d.order_TotalAmount);
            //                // Compute the total number of walk-in orders for yesterday
            //                int walkinOrdersYesterday = filteredordersList.Count(d => d.status == "Delivered" && d.deliveryDate == yesterday
            //                                                                        || d.status == "Received" && d.deliveryDate == yesterday
            //                                                                        || d.status == "Payment Received" && d.deliveryDate == yesterday);
            //                decimal walkinTotalAmountYesterday = filteredordersList
            //                    .Where(d => d.status == "Delivered" && d.deliveryDate == yesterday
            //                        || d.status == "Received" && d.deliveryDate == yesterday
            //                        || d.status == "Payment Received" && d.deliveryDate == yesterday)
            //                    .Sum(d => d.totalAmount);

            //                // Add the walk-in orders to the total number and amount of orders
            //                totalOrders += walkinOrdersYesterday;
            //                totalAmount += walkinTotalAmountYesterday;
            //                break;
            //            case 2: // This week
            //                DateTime today = DateTime.Today;
            //                DateTime thisWeekStart = today.AddDays(-(int)today.DayOfWeek);
            //                DateTime thisWeekEnd = thisWeekStart.AddDays(6);

            //                totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
            //                                                           || d.order_OrderStatus == "Accepted")
            //                              + filteredordersList.Count(d => d.status == "Out for Delivery" || d.status == "Pending"
            //                                                                   || d.status == "Accepted");

            //                totalAmount = filteredList
            //                    .Where(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
            //                        || d.order_OrderStatus == "Accepted")
            //                    .Sum(d => d.order_TotalAmount)
            //                              + filteredordersList
            //                                  .Where(d => d.status == "Out for Delivery" || d.status == "Pending"
            //                                             || d.status == "Accepted")
            //                                  .Sum(d => d.totalAmount);
            //                break;
            //            case 3: // This month
            //                DateTime todayDate = DateTime.Today;
            //                DateTime thisMonthStart = new DateTime(todayDate.Year, todayDate.Month, 1);
            //                DateTime thisMonthEnd = thisMonthStart.AddMonths(1).AddDays(-1);

            //                totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
            //                                                           || d.order_OrderStatus == "Accepted")
            //                              + filteredordersList.Count(d => d.status == "Out for Delivery" || d.status == "Pending"
            //                                                                   || d.status == "Accepted");

            //                totalAmount = filteredList
            //                    .Where(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
            //                        || d.order_OrderStatus == "Accepted")
            //                    .Sum(d => d.order_TotalAmount)
            //                              + filteredordersList
            //                                  .Where(d => d.status == "Out for Delivery" || d.status == "Pending"
            //                                             || d.status == "Accepted")
            //                                  .Sum(d => d.totalAmount);
            //                break;
            //        }

            //        // Display the total number and amount of orders in the lblSaleTotalOrders and lblSaleTotalAmount labels
            //        lblSaleTotalOrders.Text = totalOrders.ToString();
            //        lblSaleTotalAmount.Text = "₱ " + totalAmount.ToString("N2");
            //    }


            //    //protected void btnViewSale_Click(object sender, EventArgs e)
            //    //{

            //    //    try
            //    //    {
            //    //        string selectedOption = ddlSaleTransaction.SelectedValue;

            //    //        if (selectedOption == "0") // Today
            //    //        {
            //    //            DateTime today = DateTime.Today;
            //    //            // Implement code to generate report for today using today variable
            //    //        }
            //    //        else if (selectedOption == "1") // Yesterday
            //    //        {
            //    //            DateTime yesterday = DateTime.Today.AddDays(-1);
            //    //            // Implement code to generate report for yesterday using yesterday variable
            //    //        }
            //    //        else if (selectedOption == "2") // Past 30 days
            //    //        {
            //    //            DateTime startDate = DateTime.Today.AddDays(-30);
            //    //            DateTime endDate = DateTime.Today;
            //    //            // Implement code to generate report for past 30 days using startDate and endDate variables
            //    //        }
            //    //        else if (selectedOption == "3") // Year 2023
            //    //        {
            //    //            DateTime startDate = new DateTime(2023, 1, 1);
            //    //            DateTime endDate = new DateTime(2023, 12, 31);
            //    //            // Implement code to generate report for year 2023 using startDate and endDate variables
            //    //        }
            //    //        else if (selectedOption == "4") // From Date
            //    //        {
            //    //            // Implement code to show from date fields
            //    //            lblFromDate.Visible = true;
            //    //            txtFromDate.Visible = true;
            //    //        }
            //    //        else if (selectedOption == "5") // To Date
            //    //        {
            //    //            // Implement code to show to date fields
            //    //            lblToDate.Visible = true;
            //    //            txtToDate.Visible = true;
            //    //        }
            //    //    }
            //    //    catch (Exception ex)
            //    //    {
            //    //        Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/Reports.aspx';" + ex.Message);
            //    //    }


            //    //}
            //}
        }
    }