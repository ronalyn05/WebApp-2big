using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
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
    public partial class SalesReports : System.Web.UI.Page
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

            if (twoBigDB != null)
            {
                // Retrieve the orders from the database
                Dictionary<string, Order> orders = GetOrdersFromDatabase();

                // Calculate and display the daily sales
                DataTable dailySalesTable = CalculateDailySales(orders);
                dailySalesGridView.DataSource = dailySalesTable;
                dailySalesGridView.DataBind();

                // Calculate and display the weekly sales
                DataTable weeklySalesTable = CalculateWeeklySales(orders);
                weeklySalesGridView.DataSource = weeklySalesTable;
                weeklySalesGridView.DataBind();

                // Calculate and display the monthly sales
                DataTable monthlySalesTable = CalculateMonthlySales(orders);
                monthlySalesGridView.DataSource = monthlySalesTable;
                monthlySalesGridView.DataBind();

                // Calculate and display the overall sales
                decimal overallSales = CalculateOverallSales(orders);
                overallSalesLabel.Text = "Overall Sales: $" + overallSales;
            }
        }

        private Dictionary<string, Order> GetOrdersFromDatabase()
        {
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            return JsonConvert.DeserializeObject<Dictionary<string, Order>>(response.Body);
        }

        private DataTable CalculateDailySales(Dictionary<string, Order> orders)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("OrderID");
            dt.Columns.Add("OrderDate");
            dt.Columns.Add("TotalAmount");

            var dailySales = orders.Values.GroupBy(o => o.orderDate.Date)
                                          .Select(g => new
                                          {
                                              OrderDate = g.Key,
                                              TotalAmount = g.Sum(o => o.order_TotalAmount)
                                          })
                                          .OrderBy(g => g.OrderDate);

            foreach (var sale in dailySales)
            {
                DataRow dr = dt.NewRow();
                dr["OrderID"] = string.Join(", ", orders.Values.Where(o => o.orderDate.Date == sale.OrderDate).Select(o => o.orderID));
                dr["OrderDate"] = sale.OrderDate.ToString("MM/dd/yyyy");
                dr["TotalAmount"] = sale.TotalAmount;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private DataTable CalculateWeeklySales(Dictionary<string, Order> orders)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Week");
            dt.Columns.Add("TotalAmount");

            var weeklySales = orders.Values.GroupBy(o => GetIso8601WeekOfYear(o.orderDate))
                                           .Select(g => new
                                           {
                                               Week = g.Key,
                                               TotalAmount = g.Sum(o => o.order_TotalAmount)
                                           })
                                           .OrderBy(g => g.Week);

            foreach (var sale in weeklySales)
            {
                DataRow dr = dt.NewRow();
                dr["Week"] = sale.Week;
                dr["TotalAmount"] = sale.TotalAmount;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private DataTable CalculateMonthlySales(Dictionary<string, Order> orders)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Month");
            dt.Columns.Add("TotalAmount");

            var monthlySales = orders.Values.GroupBy(o => o.orderDate.Month)
                                            .Select(g => new
                                            {
                                                Month = g.Key,
                                                TotalAmount = g.Sum(o => o.order_TotalAmount)
                                            })
                                            .OrderBy(g => g.Month);

            foreach (var sale in monthlySales)
            {
                DataRow dr = dt.NewRow();
                dr["Month"] = GetMonthName(sale.Month);
                dr["TotalAmount"] = sale.TotalAmount;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private decimal CalculateOverallSales(Dictionary<string, Order> orders)
        {
            return orders.Values.Sum(o => o.order_TotalAmount);
        }

        private string GetMonthName(int month)
        {
            return new DateTime(1, month, 1).ToString("MMMM");
        }

        private int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        protected void btnViewSale_Click(object sender, EventArgs e)
        {
            // Get the ID of the currently logged-in owner from session state
            string idno = (string)Session["idno"];
            string selectedOption = ddlSaleTransaction.SelectedValue;

           try
           {


                    if (selectedOption == "0")
                    {

                    }
                    else if (selectedOption == "1")
                    {

                       

                    }
                    else if (selectedOption == "2")
                    {

                       

                    }
                    else if (selectedOption == "3")
                    {

                        


                    }
                    

           }
           catch (Exception ex)
           {
               Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/WaterOrders.aspx';" + ex.Message);
           }

        }

        //protected void btnViewSale_Click(object sender, EventArgs e)
        //{
        //    // Get the ID of the currently logged-in owner from session state
        //    string idno = (string)Session["idno"];
        //    string selectedOption = ddlSaleTransaction.SelectedValue;

        //    try
        //    {
               

        //        // Retrieve all orders from the ORDERS table
        //        FirebaseResponse response = twoBigDB.Get("ORDERS");
        //        Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

        //        DateTime fromDate = DateTime.MinValue;
        //        DateTime toDate = DateTime.MinValue;

        //        if(selectedOption == "0") // View All
        //        {
        //            if (orderlist != null)
        //            {
        //                // Filter the list of orders by the owner's ID and the order status and delivery type
        //                List<Order> filteredList = orderlist.Values
        //                    .Where(d => d.admin_ID.ToString() == idno)
        //                    .ToList();

        //                // Retrieve all orders from the ORDERS table
        //                FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
        //                Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

        //                // Filter the list of orders by the owner's ID and the order status and delivery type
        //                List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
        //                if (walkinOrderlist != null)
        //                {
        //                    filteredordersList = walkinOrderlist.Values
        //                        .Where(d => d.adminId.ToString() == idno)
        //                        .ToList();
        //                }

        //                // Compute the total number of orders today
        //                int totalOrdersToday = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
        //                                                           || d.order_OrderStatus == "Accepted");
        //                int totalWalkInOrder = filteredordersList.Count();
        //                int CombinedOrder = totalOrdersToday + totalWalkInOrder;
        //                // Compute the total amount of all orders
        //                //decimal totalOrderAmount = filteredList.Count();
        //                decimal totalOrderAmount = 0;
        //                decimal overAllSales = 0;
        //                decimal totalWalkInAmount = 0;
        //                // Compute the total number of delivery orders
        //                //int totalDeliveryOrders = filteredList.Count(d => d.order_OrderStatus == "Delivered" || d.order_OrderStatus == "Received" || d.order_OrderStatus == "Payment Received");
        //                //// Compute the total number of reservation orders
        //                //int totalReservationOrders = filteredList.Count(d => d.order_DeliveryTypeValue == "Reservation");

        //                foreach (Order order in filteredList)
        //                {
        //                    if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
        //                        && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
        //                    {
        //                        totalOrderAmount += order.order_TotalAmount;
        //                    }
        //                }
        //                foreach (WalkInOrders order in filteredordersList)
        //                {
        //                    totalWalkInAmount += order.totalAmount;
        //                }

        //                overAllSales = totalOrderAmount + totalWalkInAmount;

        //                // Display the total amount of all orders
        //                lblTotalSales.Text = overAllSales.ToString();
        //                lbloverallTotalSale.Text = "OVERALL TOTAL SALES";
        //                // Display the total number of all orders
        //                lblOrders.Text = CombinedOrder.ToString();
        //                lblCombinedOrders.Text = "TOTAL ORDERS ";

        //                lblTodaysSale.Visible = false;
        //                lblYesterdaySale.Visible = false;
        //                lblPastWeekSale.Visible = false;
        //                lblPastmonthSale.Visible = false;
        //                lbl2023Sale.Visible = false;
        //                lbl_TodaysSale.Visible = false;
        //                lbl_YesterdaySale.Visible = false;
        //                lbl_PastWeekSale.Visible = false;
        //                lbl_PastmonthSale.Visible = false;
        //                lbl_2023Sale.Visible = false;
        //            }
        //            else
        //            {
        //                // handle the case where orderlist is null
        //                lblTotalSales.Text = "Sales not found";
        //                //lblDeliveries.Text = "Deliveries not found";
        //                //lblOrders.Text = "Orders not found";
        //                //lblReservations.Text = "Reservation not found";

        //            }
        //        }
        //        else if (selectedOption == "1") // Today
        //        {
        //            fromDate = DateTime.Today;
        //            toDate = DateTime.Today.AddDays(1).AddSeconds(-1);

        //            if (orderlist != null)
        //            {
        //                // Filter the list of orders by the owner's ID and the order status and delivery type
        //                List<Order> filteredList = orderlist.Values
        //                    .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
        //                    .ToList();

        //                // Retrieve all orders from the WALKINORDERS table
        //                FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
        //                Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

        //                // Filter the list of walk-in orders by the owner's ID and the order status and delivery type
        //                List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
        //                if (walkinOrderlist != null)
        //                {
        //                    filteredordersList = walkinOrderlist.Values
        //                        .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
        //                        .ToList();
        //                }

        //                // Compute the total number of orders today
        //                int totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
        //                                                           || d.order_OrderStatus == "Accepted");
        //                int totalWalkInOrder = filteredordersList.Count();
        //                int CombinedOrder = totalOrders + totalWalkInOrder;

        //                // Compute the total amount of orders today
        //                decimal totalAmount = 0;
        //                decimal totalWalkInAmount = 0;

        //                foreach (Order order in filteredList)
        //                {
        //                    if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
        //                        && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
        //                    {
        //                        totalAmount += order.order_TotalAmount;
        //                    }
        //                }

        //                foreach (WalkInOrders order in filteredordersList)
        //                {
        //                    totalWalkInAmount += order.totalAmount;
        //                }

        //                // Compute todays sales 
        //                decimal todaySales = totalAmount + totalWalkInAmount;

        //                // Display todays total sales 
        //                lblTodaysSale.Text = todaySales.ToString();
        //                lbl_TodaysSale.Text = "TODAY/S SALES";
        //                // Display the total number of todays orders
        //                lblOrders.Text = CombinedOrder.ToString();
        //                lblCombinedOrders.Text = "TODAY/S ORDERS ";

        //                lbloverallTotalSale.Visible = false;
        //                lblTotalSales.Visible = false;
        //                lblYesterdaySale.Visible = false;
        //                lblPastWeekSale.Visible = false;
        //                lblPastmonthSale.Visible = false;
        //                lbl2023Sale.Visible = false;
        //                lbl_YesterdaySale.Visible = false;
        //                lbl_PastWeekSale.Visible = false;
        //                lbl_PastmonthSale.Visible = false;
        //                lbl_2023Sale.Visible = false;
        //            }
        //            else
        //            {
        //                // handle the case where orderlist is null
        //                lblTodaysSale.Text = "Sales not found";
        //                lblOrders.Text = "Orders not found";
        //            }
        //        }
        //        else if (selectedOption == "2")//Yesterday
        //        {
        //            fromDate = DateTime.Today.AddDays(-1);
        //            toDate = DateTime.Today.AddSeconds(-1);
        //            if (orderlist != null)
        //            {
        //                // Filter the list of orders by the owner's ID and the order status and delivery type
        //                List<Order> filteredList = orderlist.Values
        //                    .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
        //                    .ToList();

        //                // Retrieve all orders from the WALKINORDERS table
        //                FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
        //                Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

        //                // Filter the list of walk-in orders by the owner's ID and the order status and delivery type
        //                List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
        //                if (walkinOrderlist != null)
        //                {
        //                    filteredordersList = walkinOrderlist.Values
        //                        .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
        //                        .ToList();
        //                }

        //                // Compute the total number of orders from yesterday
        //                int totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
        //                                                           || d.order_OrderStatus == "Accepted");
        //                int totalWalkInOrder = filteredordersList.Count();
        //                int CombinedOrder = totalOrders + totalWalkInOrder;

        //                // Compute the total amount of orders from yesterday
        //                decimal totalAmount = 0;
        //                decimal totalWalkInAmount = 0;

        //                foreach (Order order in filteredList)
        //                {
        //                    if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
        //                        && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
        //                    {
        //                        totalAmount += order.order_TotalAmount;
        //                    }
        //                }

        //                foreach (WalkInOrders order in filteredordersList)
        //                {
        //                    totalWalkInAmount += order.totalAmount;
        //                }

        //                // Compute yesterdays sales 
        //                decimal yesterdaySales = totalAmount + totalWalkInAmount;

        //                // Display yesterdays sales
        //                lblYesterdaySale.Text = "YESTERDAY/S SALES";
        //                lbl_YesterdaySale.Text = yesterdaySales.ToString();
        //                // Display the total number of orders yesterday
        //                lblOrders.Text = CombinedOrder.ToString();
        //                lblCombinedOrders.Text = "TOTAL ORDERS ";

        //                lbloverallTotalSale.Visible = false;
        //                lblTotalSales.Visible = false;
        //                lblTodaysSale.Visible = false;
        //                lblPastWeekSale.Visible = false;
        //                lblPastmonthSale.Visible = false;
        //                lbl2023Sale.Visible = false;
        //                lbl_TodaysSale.Visible = false;
        //                lbl_PastWeekSale.Visible = false;
        //                lbl_PastmonthSale.Visible = false;
        //                lbl_2023Sale.Visible = false;
        //            }
        //            else
        //            {
        //                // handle the case where orderlist is null
        //                lblTotalSales.Text = "Sales not found";
        //                lblOrders.Text = "Orders not found";
        //            }
        //        }
        //        else if (selectedOption == "3")// Past 7 days
        //        {
        //            fromDate = DateTime.Today.AddDays(-6);
        //            toDate = DateTime.Today.AddDays(1).AddSeconds(-1);

        //            if (orderlist != null)
        //            {
        //                // Filter the list of orders by the owner's ID and the order status and delivery type
        //                List<Order> filteredList = orderlist.Values
        //                    .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
        //                    .ToList();

        //                // Retrieve all orders from the WALKINORDERS table
        //                FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
        //                Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

        //                // Filter the list of walk-in orders by the owner's ID and the order status and delivery type
        //                List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
        //                if (walkinOrderlist != null)
        //                {
        //                    filteredordersList = walkinOrderlist.Values
        //                        .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
        //                        .ToList();
        //                }

        //                // Compute the total number of orders in the past 7 days
        //                int totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
        //                                                           || d.order_OrderStatus == "Accepted");
        //                int totalWalkInOrder = filteredordersList.Count();
        //                int CombinedOrder = totalOrders + totalWalkInOrder;

        //                // Compute the total amount of orders in the past 7 days
        //                decimal totalAmount = 0;
        //                decimal totalWalkInAmount = 0;

        //                foreach (Order order in filteredList)
        //                {
        //                    if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
        //                        && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
        //                    {
        //                        totalAmount += order.order_TotalAmount;
        //                    }
        //                }

        //                foreach (WalkInOrders order in filteredordersList)
        //                {
        //                    totalWalkInAmount += order.totalAmount;
        //                }

        //                // Compute the overall sales in the past 7 days
        //                decimal pastWeekSale = totalAmount + totalWalkInAmount;

        //                // Display the total sales in the past 7 days
        //                lblPastWeekSale.Text = pastWeekSale.ToString();
        //                lbl_PastWeekSale.Text = "PAST WEEK SALES";
        //                // Display the total number of orders in the past 7 days
        //                lblOrders.Text = CombinedOrder.ToString();
        //                lblCombinedOrders.Text = "TOTAL ORDERS ";

        //                lbloverallTotalSale.Visible = false;
        //                lblTotalSales.Visible = false;
        //                lblTodaysSale.Visible = false;
        //                lblYesterdaySale.Visible = false;
        //                lblPastmonthSale.Visible = false;
        //                lbl2023Sale.Visible = false;
        //                lbl_TodaysSale.Visible = false;
        //                lbl_YesterdaySale.Visible = false;
        //                lbl_PastmonthSale.Visible = false;
        //                lbl_2023Sale.Visible = false;
        //            }
        //            else
        //            {
        //                // handle the case where orderlist is null
        //                lblTotalSales.Text = "Sales not found";
        //                lblOrders.Text = "Orders not found";
        //            }
        //        }
        //        else if (selectedOption == "4")//past month
        //        {
        //            fromDate = DateTime.Today.AddDays(-30);
        //            toDate = DateTime.Today.AddDays(1).AddSeconds(-1);
        //            if (orderlist != null)
        //            {
        //                // Filter the list of orders by the owner's ID and the order status and delivery type
        //                List<Order> filteredList = orderlist.Values
        //                    .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
        //                    .ToList();

        //                // Retrieve all orders from the WALKINORDERS table
        //                FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
        //                Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

        //                // Filter the list of walk-in orders by the owner's ID and the order status and delivery type
        //                List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
        //                if (walkinOrderlist != null)
        //                {
        //                    filteredordersList = walkinOrderlist.Values
        //                        .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
        //                        .ToList();
        //                }

        //                // Compute the total number of orders in the past 30 days
        //                int totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
        //                                                           || d.order_OrderStatus == "Accepted");
        //                int totalWalkInOrder = filteredordersList.Count();
        //                int CombinedOrder = totalOrders + totalWalkInOrder;

        //                // Compute the total amount of orders in the past 30 days
        //                decimal totalAmount = 0;
        //                decimal totalWalkInAmount = 0;

        //                foreach (Order order in filteredList)
        //                {
        //                    if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
        //                        && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
        //                    {
        //                        totalAmount += order.order_TotalAmount;
        //                    }
        //                }

        //                foreach (WalkInOrders order in filteredordersList)
        //                {
        //                    totalWalkInAmount += order.totalAmount;
        //                }

        //                // Compute the overall sales in the past 30 days
        //                decimal pastMonthSale = totalAmount + totalWalkInAmount;

        //                // Display the total sales in the past 30 days
        //                lblPastmonthSale.Text = pastMonthSale.ToString();
        //                lbl_PastmonthSale.Text = "PAST MONTH SALES";
        //                // Display the total number of orders in the past 30 days
        //                lblOrders.Text = CombinedOrder.ToString();
        //                lblCombinedOrders.Text = "TOTAL ORDERS ";

        //                lbloverallTotalSale.Visible = false;
        //                lblTotalSales.Visible = false;
        //                lblTodaysSale.Visible = false;
        //                lblYesterdaySale.Visible = false;
        //                lblPastWeekSale.Visible = false;
        //                lbl2023Sale.Visible = false;
        //                lbl_TodaysSale.Visible = false;
        //                lbl_YesterdaySale.Visible = false;
        //                lbl_PastWeekSale.Visible = false;
        //                lbl_2023Sale.Visible = false;
        //            }
        //            else
        //            {
        //                // handle the case where orderlist is null
        //                lblTotalSales.Text = "Sales not found";
        //                lblOrders.Text = "Orders not found";
        //            }
        //        }
        //        else if (selectedOption == "5")//2023
        //        {
        //            fromDate = new DateTime(2023, 1, 1);
        //            toDate = new DateTime(2023, 12, 31, 23, 59, 59);
        //            if (orderlist != null)
        //            {
        //                // Filter the list of orders by the owner's ID and the order status and delivery type
        //                List<Order> filteredList = orderlist.Values
        //                    .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
        //                    .ToList();

        //                // Retrieve all orders from the WALKINORDERS table
        //                FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
        //                Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

        //                // Filter the list of walk-in orders by the owner's ID and the order status and delivery type
        //                List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
        //                if (walkinOrderlist != null)
        //                {
        //                    filteredordersList = walkinOrderlist.Values
        //                        .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
        //                        .ToList();
        //                }

        //                // Compute the total number of orders in the past year
        //                int totalOrders = filteredList.Count(d => d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Pending"
        //                                                           || d.order_OrderStatus == "Accepted");
        //                int totalWalkInOrder = filteredordersList.Count();
        //                int CombinedOrder = totalOrders + totalWalkInOrder;

        //                // Compute the total amount of orders year
        //                decimal totalAmount = 0;
        //                decimal totalWalkInAmount = 0;

        //                foreach (Order order in filteredList)
        //                {
        //                    if (order.order_OrderTypeValue == "pickup" && order.order_OrderStatus == "Accepted" || order.order_OrderTypeValue == "delivery"
        //                        && order.order_OrderStatus == "Delivered" || order.order_OrderStatus == "Payment Received")
        //                    {
        //                        totalAmount += order.order_TotalAmount;
        //                    }
        //                }

        //                foreach (WalkInOrders order in filteredordersList)
        //                {
        //                    totalWalkInAmount += order.totalAmount;
        //                }

        //                // Compute the overall sales within a year
        //                decimal yearSales = totalAmount + totalWalkInAmount;

        //                // Display the total sales within a year
        //                lbl2023Sale.Text = yearSales.ToString();
        //                lbl_2023Sale.Text = "2023 SALES";
        //                // Display the total number of orders within a year
        //                lblOrders.Text = CombinedOrder.ToString();
        //                lblCombinedOrders.Text = "TOTAL ORDERS ";

        //                lbloverallTotalSale.Visible = false;
        //                lblTotalSales.Visible = false;
        //                lblTodaysSale.Visible = false;
        //                lblYesterdaySale.Visible = false;
        //                lblPastmonthSale.Visible = false;
        //                lblPastWeekSale.Visible = false;
        //                lbl_TodaysSale.Visible = false;
        //                lbl_YesterdaySale.Visible = false;
        //                lbl_PastWeekSale.Visible = false;
        //                lbl_PastmonthSale.Visible = false;
        //            }
        //            else
        //            {
        //                // handle the case where orderlist is null
        //                lblTotalSales.Text = "Sales not found";
        //                lblOrders.Text = "Orders not found";
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('No data exists.'); window.location.href = '/Admin/Reports.aspx';</script>" + ex.Message);
        //    }
        //}
    }
}
