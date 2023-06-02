using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
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
        //chart to track customer sales and get the average rate of cutomer that access the system
        private void BindChart()
        {
            // Clear any existing series and legends
            //chart.Series.Clear();
            //chart.Legends.Clear();

            //var orders = twoBigDB.Get("ORDERS");
            //var orderList = orders.ResultAs<Dictionary<string, Order>>();
            FirebaseResponse orders = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderList = orders.ResultAs<Dictionary<string, Order>>();

            // Calculate average transaction order, average sales, and average number of customers
            double totalTransactionOrder = 0;
            double totalSales = 0;
            double totalCustomers = 0;
            if (orderList != null)
            {
                foreach (var order in orderList.Values)
                {
                    totalTransactionOrder++;
                    totalSales += (double)order.order_TotalAmount;
                    totalCustomers += order.cusId;
                }

                double avgTransactionOrderWeek = totalTransactionOrder / 7;
                double avgSalesWeek = totalSales / 7;
                double avgCustomersWeek = totalCustomers / 7;

                double avgTransactionOrderMonth = totalTransactionOrder / 30;
                double avgSalesMonth = totalSales / 30;
                double avgCustomersMonth = totalCustomers / 30;

                double avgTransactionOrderYear = totalTransactionOrder / 365;
                double avgSalesYear = totalSales / 365;
                double avgCustomersYear = totalCustomers / 365;

                // Generate the chart HTML using Google Charts API
                //StringBuilder chartHtml = new StringBuilder();
                //chartHtml.AppendLine("<div id='chart_div'></div>");
                //chartHtml.AppendLine("<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>");
                //chartHtml.AppendLine("<script type='text/javascript'>");
                //chartHtml.AppendLine("google.charts.load('current', {packages: ['corechart']});");
                //chartHtml.AppendLine("google.charts.setOnLoadCallback(drawChart);");
                //chartHtml.AppendLine("function drawChart() {");
                //chartHtml.AppendLine("var data = google.visualization.arrayToDataTable([");
                //chartHtml.AppendLine("['Time Period', 'Value'],");
                //chartHtml.AppendLine("['Average Transaction Order (Week)', " + avgTransactionOrderWeek + "],");
                //chartHtml.AppendLine("['Average Sales (Week)', " + avgSalesWeek + "],");
                //chartHtml.AppendLine("['Average Number of Customers (Week)', " + avgCustomersWeek + "],");
                //chartHtml.AppendLine("['Average Transaction Order (Month)', " + avgTransactionOrderMonth + "],");
                //chartHtml.AppendLine("['Average Sales (Month)', " + avgSalesMonth + "],");
                //chartHtml.AppendLine("['Average Number of Customers (Month)', " + avgCustomersMonth + "],");
                //chartHtml.AppendLine("['Average Transaction Order (Year)', " + avgTransactionOrderYear + "],");
                //chartHtml.AppendLine("['Average Sales (Year)', " + avgSalesYear + "],");
                //chartHtml.AppendLine("['Average Number of Customers (Year)', " + avgCustomersYear + "]");
                //chartHtml.AppendLine("]);");
                //chartHtml.AppendLine("var options = { title: 'Average Values by Time Period' };");
                //chartHtml.AppendLine("var chart = new google.visualization.PieChart(document.getElementById('chart_div'));");
                //chartHtml.AppendLine("chart.draw(data, options);");
                //chartHtml.AppendLine("}");
                //chartHtml.AppendLine("</script>");

                //// Add the chart HTML to the page
                //chart_div.Controls.Add(new LiteralControl(chartHtml.ToString()));
            }

            //// Add a legend
            //chart.Legends.Add(new Legend("Legend"));
            //chart.Legends["Legend"].Docking = Docking.Right;

            //// Add a new chart area if it doesn't exist
            //if (chart.ChartAreas.Count == 0)
            //{
            //    chart.ChartAreas.Add(new ChartArea("ChartArea"));
            //}
        }

        //private void BindChart()
        //{
        //    // Clear any existing series and legends
        //    chart.Series.Clear();
        //    chart.Legends.Clear();

        //    //var orders = twoBigDB.Get("ORDERS");
        //    //var orderList = orders.ResultAs<Dictionary<string, Order>>();
        //    FirebaseResponse orders = twoBigDB.Get("ORDERS");
        //    Dictionary<string, Order> orderList = orders.ResultAs<Dictionary<string, Order>>();

        //    // Calculate average transaction order, average sales, and average number of customers
        //    double totalTransactionOrder = 0;
        //    double totalSales = 0;
        //    double totalCustomers = 0;
        //    if(orderList != null)
        //    {
        //        foreach (var order in orderList.Values)
        //        {
        //            totalTransactionOrder++;
        //            totalSales += (double)order.order_TotalAmount;
        //            totalCustomers += order.cusId;
        //        }

        //        double avgTransactionOrderWeek = totalTransactionOrder / 7;
        //        double avgSalesWeek = totalSales / 7;
        //        double avgCustomersWeek = totalCustomers / 7;

        //        double avgTransactionOrderMonth = totalTransactionOrder / 30;
        //        double avgSalesMonth = totalSales / 30;
        //        double avgCustomersMonth = totalCustomers / 30;

        //        double avgTransactionOrderYear = totalTransactionOrder / 365;
        //        double avgSalesYear = totalSales / 365;
        //        double avgCustomersYear = totalCustomers / 365;

        //        // Add a new series
        //        Series series = new Series("Average Values");
        //        series.ChartType = SeriesChartType.Pie;

        //        // Add data points to the series
        //        series.Points.AddXY("Average Transaction Order (Week)", avgTransactionOrderWeek);
        //        series.Points.AddXY("Average Sales (Week)", avgSalesWeek);
        //        series.Points.AddXY("Average Number of Customers (Week)", avgCustomersWeek);
        //        series.Points.AddXY("Average Transaction Order (Month)", avgTransactionOrderMonth);
        //        series.Points.AddXY("Average Sales (Month)", avgSalesMonth);
        //        series.Points.AddXY("Average Number of Customers (Month)", avgCustomersMonth);
        //        series.Points.AddXY("Average Transaction Order (Year)", avgTransactionOrderYear);
        //        series.Points.AddXY("Average Sales (Year)", avgSalesYear);
        //        series.Points.AddXY("Average Number of Customers (Year)", avgCustomersYear);

        //        // Customize the appearance of the series
        //        series["PieLabelStyle"] = "Outside";
        //        series["PieLineColor"] = "Black";

        //        // Add the series to the chart
        //        chart.Series.Add(series);

        //    }

        //    // Add a legend
        //    chart.Legends.Add(new Legend("Legend"));
        //    chart.Legends["Legend"].Docking = Docking.Right;

        //    // Add a new chart area if it doesn't exist
        //    if (chart.ChartAreas.Count == 0)
        //    {
        //        chart.ChartAreas.Add(new ChartArea("ChartArea"));
        //    }

        //    // Set chart title and labels
        //    chart.Titles.Add("Average Values by Time Period");
        //    chart.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 8f);
        //    chart.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Arial", 8f);

        //}

        //display sales in a day
        private void dailySalesDisplay()
        {
            string adminID = (string)Session["idno"];

            string idno = adminID.ToString();

            DateTime fromDate = DateTime.Today;
            DateTime toDate = DateTime.Today.AddDays(1).AddSeconds(-1);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

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