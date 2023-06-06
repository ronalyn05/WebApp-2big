using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
//using System.Web.UI.DataVisualization.Charting;
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

            //// Get the ID of the currently logged-in owner from session state
            //string idno = (string)Session["idno"];
            //string cashierID = (string)Session["cashierID"];

            //if (idno == null || cashierID == null)
            //{
            //    Response.Write("<script>alert ('Session expired ADMIN INDEX. Please log in again.');</script>");
            //    return;
            //}
            if (!IsPostBack)
            {
                BindChart();
                dailySalesDisplay();
            }
            
        }
        //chart to track customer sales and get the average rate of customer that access the system
        private void BindChart()
        {
            string idno = (string)Session["idno"];

            // Retrieve the order transactions
            FirebaseResponse orders = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderList = orders.ResultAs<Dictionary<string, Order>>();
            // Retrieve the order transactions for walk-in
            FirebaseResponse walkinorders = twoBigDB.Get("WALKINORDERS");
            Dictionary<string, WalkInOrders> walkinorderList = orders.ResultAs<Dictionary<string, WalkInOrders>>();

            // Calculate average transaction order, average sales, and average number of customers
            double totalTransactionOrder = 0;
            double totalSales = 0;
            double totalCustomers = 0;

            if (orderList != null && walkinorderList != null)
            {
                var filteredList = orderList.Values.Where(d => d.admin_ID.ToString() == idno && (d.order_OrderStatus == "Payment Received"));
                var walkinfilteredList = walkinorderList.Values.Where(d => d.adminId.ToString() == idno);

                // Iterate over the values of orderlist that contain each data retrieved from the database
                foreach (var order in filteredList)
                {
                    totalTransactionOrder += 1; // incremented by 1 for each order transaction
                    totalSales += (double)order.order_TotalAmount;
                }
                // Iterate over the values of walk-in orderlist that contain each data retrieved from the database
                foreach (var order in walkinfilteredList)
                {
                    totalTransactionOrder += 1; // incremented by 1 for each order transaction
                    totalSales += (double)order.totalAmount;
                }

                totalCustomers = filteredList.Count() + walkinfilteredList.Count(); // Get the total number of customers for both walk-in and online orders
                totalTransactionOrder += filteredList.Count() + walkinfilteredList.Count(); // Get the total of transaction order for both walk-in and online orders


                // Calculate general averages
                double avgTransactionOrder = totalTransactionOrder > 0 ? totalTransactionOrder / (filteredList.Count() + walkinfilteredList.Count()) : 0.001;
                double avgSales = totalSales > 0 ? totalSales / (filteredList.Count() + walkinfilteredList.Count()) : 0.001;
                double avgCustomers = totalCustomers > 0 ? totalCustomers / (filteredList.Count() + walkinfilteredList.Count()) : 0.001;

           
            // Calculate general averages
            //double avgTransactionOrder = totalTransactionOrder > 0 ? totalTransactionOrder / 3 : 0.001;
            //double avgSales = totalSales > 0 ? totalSales / 3 : 0.001;
            //double avgCustomers = totalCustomers > 0 ? totalCustomers / 3 : 0.001;

            // Load the Google Charts API
            ScriptManager.RegisterStartupScript(this, GetType(), "GoogleCharts", "google.charts.load('current', { packages: ['corechart'] });", true);

            // Render the chart using the Google Charts API
            ScriptManager.RegisterStartupScript(this, GetType(), "DrawChart", @"
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            // Create the data table
            var data = google.visualization.arrayToDataTable([
                ['Time Period', 'Value'],
                ['Average Transaction Order', " + avgTransactionOrder + @"],
                ['Average Sales', " + avgSales + @"],
                ['Average Number of Customers', " + avgCustomers + @"]
            ]);

            // Set chart options
            var options = {
                pieSliceText: 'value',
                pieSliceTextStyle: {
                    color: 'black',  // Set the text color of the pie slices
                },
                legend: {
                    position: 'right'  // Display the legend with labels
                },
                chartArea: {
                    left: 10,
                    top: 10,
                    width: '90%',
                    height: '90%'
                },
                slices: [
                    { color: '#4e86e4' },   // Change color to a water-related theme
                    { color: '#73bfb8' },
                    { color: '#d3efbd' }
                ]
            };

            // Instantiate and draw the chart
            var chart = new google.visualization.PieChart(document.getElementById('chartContainer'));
            chart.draw(data, options);
        }
    ", true);
            }
        }

        //display sales in a day
        private void dailySalesDisplay()
        {
            string idno = (string)Session["idno"];

            DateTime fromDate = DateTime.Today;
            DateTime toDate = DateTime.Today.AddDays(1).AddSeconds(-1);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

            if (response != null && response.ResultAs<Order>() != null)
            {
                if (orderlist != null)
                {
                    // Filter the list of orders by the owner's ID, order status, and delivery type
                    List<Order> filteredList = orderlist.Values
                        .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate && d.order_OrderStatus == "Payment Received")
                        .ToList();

                    // Count the number of pending orders
                    int pendingOrdersCount = orderlist.Values
                        .Count(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate && d.order_OrderStatus == "Pending");

                    // Retrieve all orders from the WALKINORDERS table
                    FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                    Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                    // Filter the list of orders by the owner's ID and order date
                    List<WalkInOrders> filteredWalkInOrdersList = walkinOrderlist.Values
                        .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
                        .ToList();

                    // Compute the total number of orders today
                 //   int totalOrdersToday = pendingOrdersCount.Count();

                    decimal totalOrderAmount = 0;
                    decimal overAllSales = 0;
                    decimal totalWalkInAmount = 0;

                    // Check if there are no online order sales found
                    if (filteredList.Count == 0)
                    {
                        lblOnlineSales.Visible = true;
                        lblOnlineSales.Text = "No online order sales found for today.";
                    }
                    else
                    {
                        // Calculate the total order amount for online orders
                        foreach (Order order in filteredList)
                        {
                            totalOrderAmount += order.order_TotalAmount;
                        }
                    }

                    // Check if there are no walk-in order sales found
                    if (filteredWalkInOrdersList.Count == 0)
                    {
                        lblWalkinOrders.Visible = true;
                        lblWalkinOrders.Text = "No walk-in order sales found for today.";
                    }
                    else
                    {
                        // Calculate the total amount for walk-in orders
                        foreach (WalkInOrders order in filteredWalkInOrdersList)
                        {
                            totalWalkInAmount += order.totalAmount;
                        }
                    }

                    overAllSales = totalOrderAmount + totalWalkInAmount;

                    lblOnlineSales.Text = "Php." + " " + totalOrderAmount.ToString();
                    lblWalkinOrders.Text = "Php." + " " + totalWalkInAmount.ToString();
                    lblOverallTotalSales.Text = "Php." + " " + overAllSales.ToString();
                    lblOrders.Text = pendingOrdersCount.ToString();
                  

                    lblOverallTotalSales.Visible = true;
                    lblOnlineSales.Visible = true;
                    lblWalkinOrders.Visible = true;                 
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblOverallTotalSales.Text = "No overall record of sales found for today";
                    lblOnlineSales.Text = "No online record of sales found for today";
                    lblWalkinOrders.Text = "No walk-in record of sales found for today";
                    lblOrders.Text = "Orders not found";                 
                }
            }
            else
            {
                // Handle null response or invalid selected value
                lblOverallTotalSales.Text = "No overall record of sales found for today";
                lblOnlineSales.Text = "No online record of sales found for today";
                lblWalkinOrders.Text = "No walk-in record of sales found for today";
                lblOrders.Text = "Orders not found";
             
            }
        }

    //logout here
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            // Get the log ID from the session
            // int logsId = (int)Session["logsId"]; '
            //INSERT DATA TO TABLE
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Retrieve the existing Users log object from the database
            FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS");
            UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

            //Get the current date and time
            DateTime addedTime = DateTime.Now;

            // Log user activity
            var log = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = idnum,
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

        //    private void BindChart()
        //    {
        //        string idno = (string)Session["idno"];

        //        // Retrieve the order transactions
        //        FirebaseResponse orders = twoBigDB.Get("ORDERS");
        //        Dictionary<string, Order> orderList = orders.ResultAs<Dictionary<string, Order>>();
        //        // Retrieve the order transactions for walkin
        //        FirebaseResponse walkinorders = twoBigDB.Get("WALKINORDERS");
        //        Dictionary<string, WalkInOrders> walkinorderList = orders.ResultAs<Dictionary<string, WalkInOrders>>();

        //        // Calculate average transaction order, average sales, and average number of customers
        //        double totalTransactionOrder = 0;
        //        double totalSales = 0;
        //        double totalCustomers = 0;

        //        if (orderList != null && walkinorderList != null)
        //        {
        //            var filteredList = orderList.Values.Where(d => d.admin_ID.ToString() == idno && (d.order_OrderStatus == "Payment Received"));
        //            var walkinfilteredList = walkinorderList.Values.Where(d => d.adminId.ToString() == idno);

        //            // Iterate over the values of orderlist that contain each data retrieved from the database
        //            foreach (var order in filteredList)
        //            {
        //                totalTransactionOrder += 1; // incremented by 1 for each order transaction
        //                totalSales += (double)order.order_TotalAmount;
        //            }
        //            // Iterate over the values of walkin orderlist that contain each data retrieved from the database
        //            foreach (var order in walkinfilteredList)
        //            {
        //                totalTransactionOrder += 1; // incremented by 1 for each order transaction
        //                totalSales += (double)order.totalAmount;
        //            }

        //            totalCustomers = filteredList.Count();

        //            totalCustomers += totalCustomers; //get the number of customers for both walkin and online orders
        //            totalTransactionOrder += totalTransactionOrder; //get the total of transaction order for both walkin and online orders

        //        }

        //        // Calculate averages for different time periods
        //        double avgTransactionOrderWeek = totalTransactionOrder / 7;
        //        double avgSalesWeek = totalSales / 7;
        //        double avgCustomersWeek = totalCustomers / 7;
        //        double avgTransactionOrderMonth = totalTransactionOrder / 30;
        //        double avgSalesMonth = totalSales / 30;
        //        double avgCustomersMonth = totalCustomers / 30;
        //        double avgTransactionOrderYear = totalTransactionOrder / 365;
        //        double avgSalesYear = totalSales / 365;
        //        double avgCustomersYear = totalCustomers / 365;

        //        // Load the Google Charts API
        //        ScriptManager.RegisterStartupScript(this, GetType(), "GoogleCharts", "google.charts.load('current', { packages: ['corechart'] });", true);

        //        // Render the chart using the Google Charts API
        //        ScriptManager.RegisterStartupScript(this, GetType(), "DrawChart", @"google.charts.setOnLoadCallback(drawChart);

        //    function drawChart() {
        //        // Create the data table
        //        var data = google.visualization.arrayToDataTable([
        //            ['Time Period', 'Value'],
        //            //if the value is 0 or less, it is replaced with a small non-zero value (0.001 in this case) to ensure the slice is visible in the chart
        //            //['Average Transaction Order ', " + (avgTransactionOrderWeek > 0 ? avgTransactionOrderWeek : 0.001) + @"],
        //            //['Average Sales ', " + (avgSalesWeek > 0 ? avgSalesWeek : 0.001) + @"],
        //            //['Average Number of Customers ', " + (avgCustomersWeek > 0 ? avgCustomersWeek : 0.001) + @"],
        //            //['Average Transaction Order (Month)', " + (avgTransactionOrderMonth > 0 ? avgTransactionOrderMonth : 0.001) + @"],
        //            //['Average Sales (Month)', " + (avgSalesMonth > 0 ? avgSalesMonth : 0.001) + @"],
        //            //['Average Number of Customers (Month)', " + (avgCustomersMonth > 0 ? avgCustomersMonth : 0.001) + @"],
        //            ['Average Transaction Order ', " + (avgTransactionOrderYear > 0 ? avgTransactionOrderYear : 0.001) + @"],
        //            ['Average Sales', " + (avgSalesYear > 0 ? avgSalesYear : 0.001) + @"],
        //            ['Average Number of Customers', " + (avgCustomersYear > 0 ? avgCustomersYear : 0.001) + @"]
        //        ]);

        //        //// Create the data table
        //        //var data = google.visualization.arrayToDataTable([
        //        //        ['Time Period', 'Value'],
        //        //        ['Average Transaction Order (Week)', " + avgTransactionOrderWeek + @"],
        //        //        ['Average Sales (Week)', " + avgSalesWeek + @"],
        //        //        ['Average Number of Customers (Week)', " + avgCustomersWeek + @"],
        //        //        ['Average Transaction Order (Month)', " + avgTransactionOrderMonth + @"],
        //        //        ['Average Sales (Month)', " + avgSalesMonth + @"],
        //        //        ['Average Number of Customers (Month)', " + avgCustomersMonth + @"],
        //        //        ['Average Transaction Order (Year)', " + avgTransactionOrderYear + @"],
        //        //        ['Average Sales (Year)', " + avgSalesYear + @"],
        //        //        ['Average Number of Customers (Year)', " + avgCustomersYear + @"]
        //        //]);

        //                // Set chart options
        //                    var options = {
        //                        pieSliceText: 'value',
        //                        pieSliceTextStyle: {
        //                            color: 'black',  // Set the text color of the pie slices
        //                        },
        //                        legend: {
        //                        position: 'labeled'  // Display the legend with labels
        //                        position: 'right'  // Display the legend with labels
        //                    },
        //                        chartArea: {
        //                        left: 5,
        //                        top: 10,
        //                        width: '90%',
        //                        height: '90%'
        //                    },
        //                    slices: [
        //                        { color: '#4e86e4' },   // Change color to a water-related theme
        //                        { color: '#73bfb8' },
        //                        { color: '#a8dba8' }
        //                        //{ color: '#d3efbd' },
        //                        //{ color: '#00CED1' },
        //                        //{ color: '#fde8a3' },
        //                        //{ color: '#fdb366' },
        //                        //{ color: '#e26761' },
        //                        //{ color: '#ADD8E6' }
        //                    ]
        //                };

        //        // Instantiate and draw the chart
        //        var chart = new google.visualization.PieChart(document.getElementById('chartContainer'));
        //        chart.draw(data, options);
        //    }
        //", true);
        //    }
    }
}