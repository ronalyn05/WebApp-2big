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

            overallSalesRevenue.Visible = false;
            Label2.Visible = false;
            Label1.Visible = false;
            Label4.Visible = false;
            overallSalesRevenue.Visible = false;
            onlinesalesRevenue.Visible = false;
            walkinsalesRevenue.Visible = false;
            lblErrorOnline.Visible = false;
            lblErrorWalkin.Visible = false;
            overAllSalesDisplay();
        }

        private void overAllSalesDisplay()
        {
            string idno = (string)Session["idno"];

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

            // Retrieve all customers from the CUSTOMER table and compare the current customer name
            FirebaseResponse customerResponse = twoBigDB.Get("CUSTOMER");
            Dictionary<string, Customer> customerlist = customerResponse.ResultAs<Dictionary<string, Customer>>();

            // Create the DataTable to hold the orders
            DataTable salesordersTable = new DataTable();
            salesordersTable.Columns.Add("Order ID");
            salesordersTable.Columns.Add("Customer Name");
            salesordersTable.Columns.Add("Order Status");
            salesordersTable.Columns.Add("Payment Mode");
            salesordersTable.Columns.Add("Delivery Type");
            salesordersTable.Columns.Add("Transaction Type");
            salesordersTable.Columns.Add("Total Amount");
            salesordersTable.Columns.Add("Order Date");

            if (orderlist != null)
            {
                // Filter the list of orders by the owner's ID and the order status and delivery type
                List<Order> filteredList = orderlist.Values
                  .Where(d => d.admin_ID.ToString() == idno && (d.order_OrderTypeValue == "PickUp" || d.order_OrderTypeValue == "Delivery")
                      && ( d.order_OrderStatus == "Payment Received"))
                  .ToList();
                //List<Order> filteredList = orderlist.Values
                //    .Where(d => d.admin_ID.ToString() == idno && (d.order_OrderTypeValue == "PickUp" || d.order_OrderTypeValue == "Delivery")
                //        && ( d.order_OrderStatus == "Delivered" || d.order_OrderStatus == "Payment Received"))
                //    .ToList();


                // Retrieve all orders from the WALKINORDERS table
                FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                // Create the DataTable to hold the orders
                DataTable walkinsalesTable = new DataTable();
                walkinsalesTable.Columns.Add("Order ID");
                walkinsalesTable.Columns.Add("Added By");
                walkinsalesTable.Columns.Add("Amount Paid");
                walkinsalesTable.Columns.Add("Transaction Type");
                walkinsalesTable.Columns.Add("Order Date");
                // Filter the list of orders by the owner's ID
                //List<WalkInOrders> filteredordersList = walkinOrderlist?.Values
                //    .Where(d => d.adminId.ToString() == idno)
                //    .ToList();
                List<WalkInOrders> filteredordersList = walkinOrderlist.Values
                           .Where(d => d.adminId.ToString() == idno)
                           .ToList();

                decimal totalOrderAmount = 0;
                decimal overAllSales = 0;
                decimal totalWalkInAmount = 0;

                // Check if there are no walk-in sales found
                if (filteredList.Count == 0)
                {
                    lblErrorOnline.Visible = true;
                    //lblErrorWalkin.Visible = false;
                    lblErrorOnline.Text = "No online order sales found.";
                    
                }
                foreach (Order order in filteredList)
                {
                    // Retrieve the customer details based on the customer ID from the order
                    if (customerlist.TryGetValue(order.cusId.ToString(), out Customer customer))
                    {
                        string customerName = customer.firstName + " " + customer.lastName;

                        // Add the order details to the DataTable with the customer name
                        string dateOrdered = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        salesordersTable.Rows.Add(order.orderID, customerName, order.order_OrderStatus, order.orderPaymentMethod, order.order_DeliveryTypeValue,
                            order.order_OrderTypeValue, order.order_TotalAmount, dateOrdered);

                        totalOrderAmount += order.order_TotalAmount;
                    }
                    //string dateOrdered = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    ////  string datePaymentReceived = order.datePaymentReceived == DateTime.MinValue ? "" : order.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    //salesordersTable.Rows.Add(order.orderID, order.cusId, order.order_OrderStatus, order.order_DeliveryTypeValue, order.order_OrderTypeValue,
                    //    order.order_TotalAmount, dateOrdered);

                    //totalOrderAmount += order.order_TotalAmount;
                    //}
                }
                // Check if there are no walk-in sales found
                if (filteredordersList.Count == 0)
                {
                    //lblErrorOnline.Visible = false;
                    lblErrorWalkin.Visible = true;
                    lblErrorWalkin.Text = "No walk-in order sales found.";
                    
                }

                if (filteredordersList != null)
                {
                    foreach (WalkInOrders order in filteredordersList)
                    {
                        string dateOrdered = order.dateAdded == DateTime.MinValue ? "" : order.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        walkinsalesTable.Rows.Add(order.orderNo, order.addedBy, order.totalAmount, "Walkin Order", dateOrdered);

                        totalWalkInAmount += order.totalAmount;
                    }
                }

                overAllSales = totalOrderAmount + totalWalkInAmount;

                onlinesalesRevenue.Text = "Php." + " " + totalOrderAmount.ToString();
                walkinsalesRevenue.Text = "Php." + " " + totalWalkInAmount.ToString();
                overallSalesRevenue.Text = "Php." + " " + overAllSales.ToString();
                salesRevenueGridView.DataSource = salesordersTable;
                salesRevenueGridView.DataBind();
                walkinSales.DataSource = walkinsalesTable;
                walkinSales.DataBind();
                Label2.Visible = true;
                Label1.Visible = true;
                Label4.Visible = true;
                overallSalesRevenue.Visible = true;
                onlinesalesRevenue.Visible = true;
                walkinsalesRevenue.Visible = true;
               
            }
            else
            {
                // Handle null response or invalid selected value
                overallSalesRevenue.Text = "No record of sales found";
                Label2.Visible = false;
                Label1.Visible = false;
                Label4.Visible = false;
                overallSalesRevenue.Visible = true;
                onlinesalesRevenue.Visible = false;
                walkinsalesRevenue.Visible = false;
               
            }
        }

        //private void overAllSalesDisplay()
        //{
        //    string idno = (string)Session["idno"];

        //    // Retrieve all orders from the ORDERS table
        //    FirebaseResponse response = twoBigDB.Get("ORDERS");
        //    Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


        //    // Create the DataTable to hold the orders
        //    DataTable salesordersTable = new DataTable();
        //    salesordersTable.Columns.Add("Order ID");
        //    salesordersTable.Columns.Add("Customer ID");
        //    salesordersTable.Columns.Add("Order Status");
        //    salesordersTable.Columns.Add("Delivery Type");
        //    salesordersTable.Columns.Add("Transaction Type");
        //    salesordersTable.Columns.Add("Total Amount");
        //    salesordersTable.Columns.Add("Order Date");

        //    // Get the selected date range from the dropdown list
        //    //string dateRange = ddlDateRange.SelectedValue;

        //    if (response != null && response.ResultAs<Order>() != null)
        //    {
        //         if (orderlist != null)
        //        {
        //            //  Filter the list of orders by the owner's ID and the order status and delivery type
        //            List<Order> filteredList = orderlist.Values
        //                .Where(d => d.admin_ID.ToString() == idno)
        //                .ToList();

        //            //  Retrieve all orders from the ORDERS table
        //            FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
        //            Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

        //            // Filter the list of orders by the owner's ID and the order status and delivery type
        //            List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
        //            if (walkinOrderlist != null)
        //            {
        //                filteredordersList = walkinOrderlist.Values
        //                    .Where(d => d.adminId.ToString() == idno)
        //                    .ToList();
        //            }

        //            decimal totalOrderAmount = 0;
        //            decimal overAllSales = 0;
        //            decimal totalWalkInAmount = 0;

        //            foreach (Order order in filteredList)
        //            {
        //                if ((order.order_OrderTypeValue == "PickUp") && (order.order_OrderStatus == "Accepted") || (order.order_OrderTypeValue == "Delivery")
        //                    && (order.order_OrderStatus == "Delivered") && (order.order_OrderStatus == "Payment Received"))
        //                {
        //                    string dateOrdered = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");
        //                  //  string datePaymentReceived = order.datePaymentReceived == DateTime.MinValue ? "" : order.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");

        //                    salesordersTable.Rows.Add(order.orderID, order.cusId, order.order_OrderStatus, order.order_DeliveryTypeValue, order.order_OrderTypeValue,
        //                        order.order_TotalAmount, dateOrdered);


        //                    totalOrderAmount += order.order_TotalAmount;
        //                }
        //            }
        //            foreach (WalkInOrders order in filteredordersList)
        //            {
        //                string dateOrdered = order.dateAdded == DateTime.MinValue ? "" : order.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
        //                //  string datePaymentReceived = order.datePaymentReceived == DateTime.MinValue ? "" : order.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");

        //                salesordersTable.Rows.Add(order.orderNo, " ", " ", " ", order.orderType, order.totalAmount, dateOrdered);

        //                totalWalkInAmount += order.totalAmount;
        //            }

        //            overAllSales = totalOrderAmount + totalWalkInAmount;
        //            salesRevenue.Text = "Php." + " " + overAllSales;
        //            salesRevenueGridView.DataSource = salesordersTable;
        //            salesRevenueGridView.DataBind();
        //            Label1.Visible = true;
        //            salesRevenue.Visible = true;
        //        }
        //        else
        //        {
        //            // Handle null response or invalid selected value
        //            salesRevenue.Text = "No record of sales found";
        //        }
        //    }
        //    else
        //    {
        //        // Handle null response or invalid selected value
        //        salesRevenue.Text = "No record of sales found";
        //    }
        //}
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

            // Retrieve all customers from the CUSTOMER table and compare the current customer name
            FirebaseResponse customerResponse = twoBigDB.Get("CUSTOMER");
            Dictionary<string, Customer> customerlist = customerResponse.ResultAs<Dictionary<string, Customer>>();

            // Create the DataTable to hold the orders
            DataTable salesordersTable = new DataTable();
            salesordersTable.Columns.Add("Order ID");
            salesordersTable.Columns.Add("Customer Name");
            salesordersTable.Columns.Add("Order Status");
            salesordersTable.Columns.Add("Payment Mode");
            salesordersTable.Columns.Add("Delivery Type");
            salesordersTable.Columns.Add("Transaction Type");
            salesordersTable.Columns.Add("Total Amount");
            salesordersTable.Columns.Add("Order Date");

            // Get the selected date range from the dropdown list
            //string dateRange = ddlDateRange.SelectedValue;

            if (response != null && response.ResultAs<Order>() != null)
            {
                if (orderlist != null)
                {
                    //  Filter the list of orders by the owner's ID and the order status and delivery type
                    List<Order> filteredList = orderlist.Values
                    .Where(d => d.admin_ID.ToString() == idno && (d.orderDate >= fromDate && d.orderDate <= toDate) && (d.order_OrderTypeValue == "PickUp" || d.order_OrderTypeValue == "Delivery")
                        && ( d.order_OrderStatus == "Payment Received"))
                    .ToList();
                    //List<Order> filteredList = orderlist.Values
                    //        .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
                    //        .ToList();

                    //  Retrieve all orders from the ORDERS table
                    FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                    Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                    // Create the DataTable to hold the orders
                    DataTable walkinsalesTable = new DataTable();
                    walkinsalesTable.Columns.Add("Order ID");
                    walkinsalesTable.Columns.Add("Added By");
                    walkinsalesTable.Columns.Add("Amount Paid");
                    walkinsalesTable.Columns.Add("Transaction Type");
                    walkinsalesTable.Columns.Add("Order Date");
                    // Filter the list of orders by the owner's ID and the order status and delivery type
                    List<WalkInOrders> filteredordersList = new List<WalkInOrders>();

                    //if (walkinOrderlist != null)
                    //{
                        filteredordersList = walkinOrderlist.Values
                               .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
                               .ToList();
                    //}

                    decimal totalOrderAmount = 0;
                    decimal overAllSales = 0;
                    decimal totalWalkInAmount = 0;
                    // Check if there are no walk-in sales found
                    if (filteredList.Count == 0)
                    {
                        lblErrorOnline.Visible = true;
                        //lblErrorWalkin.Visible = false;
                        lblErrorOnline.Text = "No daily online order sales found.";
                        
                    }
                    foreach (Order order in filteredList)
                    {
                        // Retrieve the customer details based on the customer ID from the order
                        if (customerlist.TryGetValue(order.cusId.ToString(), out Customer customer))
                        {
                            string customerName = customer.firstName + " " + customer.lastName;

                            // Add the order details to the DataTable with the customer name
                            string dateOrdered = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            salesordersTable.Rows.Add(order.orderID, customerName, order.order_OrderStatus, order.orderPaymentMethod, order.order_DeliveryTypeValue,
                                order.order_OrderTypeValue, order.order_TotalAmount, dateOrdered);

                            totalOrderAmount += order.order_TotalAmount;
                        }
                        ////if ((order.order_OrderTypeValue == "PickUp") && (order.order_OrderStatus == "Accepted") || (order.order_OrderTypeValue == "Delivery")
                        ////     && (order.order_OrderStatus == "Delivered") && (order.order_OrderStatus == "Payment Received"))
                        ////{
                        //    string dateOrdered = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        //    //  string datePaymentReceived = order.datePaymentReceived == DateTime.MinValue ? "" : order.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        //    salesordersTable.Rows.Add(order.orderID, order.cusId, order.order_OrderStatus, order.order_DeliveryTypeValue, order.order_OrderTypeValue,
                        //        order.order_TotalAmount, dateOrdered);

                        //    totalOrderAmount += order.order_TotalAmount;
                        ////}
                    }
                    // Check if there are no walk-in sales found
                    if (filteredordersList.Count == 0)
                    {
                        //lblErrorOnline.Visible = false;
                        lblErrorWalkin.Visible = true;
                        lblErrorWalkin.Text = "No daily walk-in order sales found.";
                       
                    }
                    foreach (WalkInOrders order in filteredordersList)
                    {
                        string dateOrdered = order.dateAdded == DateTime.MinValue ? "" : order.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        //  string datePaymentReceived = order.datePaymentReceived == DateTime.MinValue ? "" : order.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        walkinsalesTable.Rows.Add(order.orderNo, order.addedBy, order.totalAmount, "Walkin Order", dateOrdered);

                        totalWalkInAmount += order.totalAmount;
                    }
                   

                    overAllSales = totalOrderAmount + totalWalkInAmount;

                    onlinesalesRevenue.Text = "Php." + " " + totalOrderAmount.ToString();
                    walkinsalesRevenue.Text = "Php." + " " + totalWalkInAmount.ToString();
                    overallSalesRevenue.Text = "Php." + " " + overAllSales.ToString();
                    salesRevenueGridView.DataSource = salesordersTable;
                    salesRevenueGridView.DataBind();
                    walkinSales.DataSource = walkinsalesTable;
                    walkinSales.DataBind();
                    Label2.Visible = true;
                    Label1.Visible = true;
                    Label4.Visible = true;
                    overallSalesRevenue.Visible = true;
                    onlinesalesRevenue.Visible = true;
                    walkinsalesRevenue.Visible = true;
                    //lblErrorOnline.Visible = false;
                    //lblErrorWalkin.Visible = false;
                }
                else
                {
                    // Handle null response or invalid selected value
                    overallSalesRevenue.Text = "No record of sales found";
                    Label2.Visible = false;
                    Label1.Visible = false;
                    Label4.Visible = false;
                    overallSalesRevenue.Visible = true;
                    onlinesalesRevenue.Visible = false;
                    walkinsalesRevenue.Visible = false;
                    //lblErrorOnline.Visible = false;
                    //lblErrorWalkin.Visible = false;
                }
            }
            else
            {
                // Handle null response or invalid selected value
                overallSalesRevenue.Text = "No record of sales found";
                Label2.Visible = false;
                Label1.Visible = false;
                Label4.Visible = false;
                overallSalesRevenue.Visible = true;
                onlinesalesRevenue.Visible = false;
                walkinsalesRevenue.Visible = false;
                //lblErrorOnline.Visible = false;
                //lblErrorWalkin.Visible = false;
            }
        }
        private void weeklySalesDisplay()
        {
            string idno = (string)Session["idno"];

            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MinValue;

            fromDate = DateTime.Today.AddDays(-6);
            toDate = DateTime.Today.AddDays(1).AddSeconds(-1);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

            // Retrieve all customers from the CUSTOMER table and compare the current customer name
            FirebaseResponse customerResponse = twoBigDB.Get("CUSTOMER");
            Dictionary<string, Customer> customerlist = customerResponse.ResultAs<Dictionary<string, Customer>>();

            // Create the DataTable to hold the orders
            DataTable salesordersTable = new DataTable();
            salesordersTable.Columns.Add("Order ID");
            salesordersTable.Columns.Add("Customer Name");
            salesordersTable.Columns.Add("Order Status");
            salesordersTable.Columns.Add("Payment Mode");
            salesordersTable.Columns.Add("Delivery Type");
            salesordersTable.Columns.Add("Transaction Type");
            salesordersTable.Columns.Add("Total Amount");
            salesordersTable.Columns.Add("Order Date");

            // Get the selected date range from the dropdown list
            //string dateRange = ddlDateRange.SelectedValue;

            if (response != null && response.ResultAs<Order>() != null)
            {
                if (orderlist != null)
                {
                    //  Filter the list of orders by the owner's ID and the order status and delivery type
                    List<Order> filteredList = orderlist.Values
                   .Where(d => d.admin_ID.ToString() == idno && (d.orderDate >= fromDate && d.orderDate <= toDate) && (d.order_OrderTypeValue == "PickUp" || d.order_OrderTypeValue == "Delivery")
                       && ( d.order_OrderStatus == "Payment Received"))
                   .ToList();
                    //List<Order> filteredList = orderlist.Values
                    //        .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
                    //        .ToList();

                    //  Retrieve all orders from the ORDERS table
                    FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                    Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                    // Create the DataTable to hold the orders
                    DataTable walkinsalesTable = new DataTable();
                    walkinsalesTable.Columns.Add("Order ID");
                    walkinsalesTable.Columns.Add("Added By");
                    walkinsalesTable.Columns.Add("Amount Paid");
                    walkinsalesTable.Columns.Add("Transaction Type");
                    walkinsalesTable.Columns.Add("Order Date");
                    // Filter the list of orders by the owner's ID and the order status and delivery type
                    List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
                    //if (walkinOrderlist != null)
                    //{
                        filteredordersList = walkinOrderlist.Values
                               .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
                               .ToList();
                    //}

                    decimal totalOrderAmount = 0;
                    decimal overAllSales = 0;
                    decimal totalWalkInAmount = 0;

                    // Check if there are no walk-in sales found
                    if (filteredList.Count == 0)
                    {
                        lblErrorOnline.Visible = true;
                        //lblErrorWalkin.Visible = false;
                        lblErrorOnline.Text = "No weekly online order sales found.";
                        
                    }
                    foreach (Order order in filteredList)
                    {
                        // Retrieve the customer details based on the customer ID from the order
                        if (customerlist.TryGetValue(order.cusId.ToString(), out Customer customer))
                        {
                            string customerName = customer.firstName + " " + customer.lastName;

                            // Add the order details to the DataTable with the customer name
                            string dateOrdered = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            salesordersTable.Rows.Add(order.orderID, customerName, order.order_OrderStatus, order.orderPaymentMethod, order.order_DeliveryTypeValue,
                                order.order_OrderTypeValue, order.order_TotalAmount, dateOrdered);

                            totalOrderAmount += order.order_TotalAmount;
                        }
                        ////if ((order.order_OrderTypeValue == "PickUp") && (order.order_OrderStatus == "Accepted") || (order.order_OrderTypeValue == "Delivery")
                        ////     && (order.order_OrderStatus == "Delivered") && (order.order_OrderStatus == "Payment Received"))
                        ////{
                        //string dateOrdered = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        ////  string datePaymentReceived = order.datePaymentReceived == DateTime.MinValue ? "" : order.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        //salesordersTable.Rows.Add(order.orderID, order.cusId, order.order_OrderStatus, order.order_DeliveryTypeValue, order.order_OrderTypeValue,
                        //    order.order_TotalAmount, dateOrdered);

                        //totalOrderAmount += order.order_TotalAmount;
                        ////}
                    }
                    // Check if there are no walk-in sales found
                    if (filteredordersList.Count == 0)
                    {
                        //lblErrorOnline.Visible = false;
                        lblErrorWalkin.Visible = true;
                        lblErrorWalkin.Text = "No weekly walk-in order sales found.";
                        
                    }
                    foreach (WalkInOrders order in filteredordersList)
                    {
                        string dateOrdered = order.dateAdded == DateTime.MinValue ? "" : order.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        //  string datePaymentReceived = order.datePaymentReceived == DateTime.MinValue ? "" : order.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        walkinsalesTable.Rows.Add(order.orderNo, order.addedBy, order.totalAmount, "Walkin Order", dateOrdered);

                        totalWalkInAmount += order.totalAmount;
                    }

                    overAllSales = totalOrderAmount + totalWalkInAmount;

                    onlinesalesRevenue.Text = "Php." + " " + totalOrderAmount.ToString();
                    walkinsalesRevenue.Text = "Php." + " " + totalWalkInAmount.ToString();
                    overallSalesRevenue.Text = "Php." + " " + overAllSales.ToString();
                    salesRevenueGridView.DataSource = salesordersTable;
                    salesRevenueGridView.DataBind();
                    walkinSales.DataSource = walkinsalesTable;
                    walkinSales.DataBind();
                    Label2.Visible = true;
                    Label1.Visible = true;
                    Label4.Visible = true;
                    overallSalesRevenue.Visible = true;
                    onlinesalesRevenue.Visible = true;
                    walkinsalesRevenue.Visible = true;
                    //lblErrorOnline.Visible = false;
                    //lblErrorWalkin.Visible = false;
                }
                else
                {
                    // Handle null response or invalid selected value
                    overallSalesRevenue.Text = "No record of sales found";
                    Label2.Visible = false;
                    Label1.Visible = false;
                    Label4.Visible = false;
                    overallSalesRevenue.Visible = true;
                    onlinesalesRevenue.Visible = false;
                    walkinsalesRevenue.Visible = false;
                    //lblErrorOnline.Visible = false;
                    //lblErrorWalkin.Visible = false;
                }
            }
            else
            {
                // Handle null response or invalid selected value
                overallSalesRevenue.Text = "No record of sales found";
                Label2.Visible = false;
                Label1.Visible = false;
                Label4.Visible = false;
                overallSalesRevenue.Visible = true;
                onlinesalesRevenue.Visible = false;
                walkinsalesRevenue.Visible = false;
                //lblErrorOnline.Visible = false;
                //lblErrorWalkin.Visible = false;
            }
        }
        private void monthlySalesDisplay()
        {
            string idno = (string)Session["idno"];

            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MinValue;

            fromDate = DateTime.Today.AddDays(-30);
            toDate = DateTime.Today.AddDays(1).AddSeconds(-1);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

            // Retrieve all customers from the CUSTOMER table and compare the current customer name
            FirebaseResponse customerResponse = twoBigDB.Get("CUSTOMER");
            Dictionary<string, Customer> customerlist = customerResponse.ResultAs<Dictionary<string, Customer>>();

            // Create the DataTable to hold the orders
            DataTable salesordersTable = new DataTable();
            salesordersTable.Columns.Add("Order ID");
            salesordersTable.Columns.Add("Customer Name");
            salesordersTable.Columns.Add("Order Status");
            salesordersTable.Columns.Add("Payment Mode");
            salesordersTable.Columns.Add("Delivery Type");
            salesordersTable.Columns.Add("Transaction Type");
            salesordersTable.Columns.Add("Total Amount");
            salesordersTable.Columns.Add("Order Date");

            // Get the selected date range from the dropdown list
            //string dateRange = ddlDateRange.SelectedValue;

            if (response != null && response.ResultAs<Order>() != null)
            {
                if (orderlist != null )
                {
                    //  Filter the list of orders by the owner's ID
                    List<Order> filteredList = orderlist.Values
                   .Where(d => d.admin_ID.ToString() == idno && (d.orderDate >= fromDate && d.orderDate <= toDate) && (d.order_OrderTypeValue == "PickUp" || d.order_OrderTypeValue == "Delivery")
                       && (d.order_OrderStatus == "Payment Received"))
                   .ToList();
                    //List<Order> filteredList = orderlist.Values
                    //        .Where(d => d.admin_ID.ToString() == idno && d.orderDate >= fromDate && d.orderDate <= toDate)
                    //        .ToList();

                    //  Retrieve all orders from the ORDERS table
                    FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                    Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                    // Create the DataTable to hold the orders
                    DataTable walkinsalesTable = new DataTable();
                    walkinsalesTable.Columns.Add("Order ID");
                    walkinsalesTable.Columns.Add("Added By");
                    walkinsalesTable.Columns.Add("Amount Paid");
                    walkinsalesTable.Columns.Add("Transaction Type");
                    walkinsalesTable.Columns.Add("Order Date");
                    // Filter the list of orders by the owner's ID and the order status and delivery type
                    List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
                    //if (walkinOrderlist != null)
                    //{
                    filteredordersList = walkinOrderlist.Values
                           .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
                           .ToList();
                    //}

                    decimal totalOrderAmount = 0;
                    decimal overAllSales = 0;
                    decimal totalWalkInAmount = 0;

                    // Check if there are no walk-in sales found
                    if (filteredList.Count == 0)
                    {
                        lblErrorOnline.Visible = true;
                        //lblErrorWalkin.Visible = false;
                        lblErrorOnline.Text = "No monthly online order sales found.";
                       
                    }
                    foreach (Order order in filteredList)
                    {
                        // Retrieve the customer details based on the customer ID from the order
                        if (customerlist.TryGetValue(order.cusId.ToString(), out Customer customer))
                        {
                            string customerName = customer.firstName + " " + customer.lastName;

                            // Add the order details to the DataTable with the customer name
                            string dateOrdered = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            salesordersTable.Rows.Add(order.orderID, customerName, order.order_OrderStatus, order.orderPaymentMethod, order.order_DeliveryTypeValue,
                                order.order_OrderTypeValue, order.order_TotalAmount, dateOrdered);

                            totalOrderAmount += order.order_TotalAmount;
                        }
                        ////if ((order.order_OrderTypeValue == "PickUp") && (order.order_OrderStatus == "Accepted") || (order.order_OrderTypeValue == "Delivery")
                        ////     && (order.order_OrderStatus == "Delivered") && (order.order_OrderStatus == "Payment Received"))
                        ////{
                        //string dateOrdered = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        ////  string datePaymentReceived = order.datePaymentReceived == DateTime.MinValue ? "" : order.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        //salesordersTable.Rows.Add(order.orderID, order.cusId, order.order_OrderStatus, order.order_DeliveryTypeValue, order.order_OrderTypeValue,
                        //    order.order_TotalAmount, dateOrdered);

                        //totalOrderAmount += order.order_TotalAmount;
                        ////}
                    }
                    // Check if there are no walk-in sales found
                    if (filteredordersList.Count == 0)
                    {
                        //lblErrorOnline.Visible = false;
                        lblErrorWalkin.Visible = true;
                        lblErrorWalkin.Text = "No monthly walk-in order sales found.";
                        
                    }
                    foreach (WalkInOrders order in filteredordersList)
                    {
                        string dateOrdered = order.dateAdded == DateTime.MinValue ? "" : order.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        //  string datePaymentReceived = order.datePaymentReceived == DateTime.MinValue ? "" : order.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        walkinsalesTable.Rows.Add(order.orderNo, order.addedBy, order.totalAmount, "Walkin Order", dateOrdered);

                        totalWalkInAmount += order.totalAmount;
                    }

                    overAllSales = totalOrderAmount + totalWalkInAmount;

                    onlinesalesRevenue.Text = "Php." + " " + totalOrderAmount.ToString();
                    walkinsalesRevenue.Text = "Php." + " " + totalWalkInAmount.ToString();
                    overallSalesRevenue.Text = "Php." + " " + overAllSales.ToString();
                    salesRevenueGridView.DataSource = salesordersTable;
                    salesRevenueGridView.DataBind();
                    walkinSales.DataSource = walkinsalesTable;
                    walkinSales.DataBind();
                    Label2.Visible = true;
                    Label1.Visible = true;
                    Label4.Visible = true;
                    overallSalesRevenue.Visible = true;
                    onlinesalesRevenue.Visible = true;
                    walkinsalesRevenue.Visible = true;
                    //lblErrorOnline.Visible = false;
                    //lblErrorWalkin.Visible = false;
                }
                else
                {
                    // Handle null response or invalid selected value
                    overallSalesRevenue.Text = "No record of sales found";
                    Label2.Visible = false;
                    Label1.Visible = false;
                    Label4.Visible = false;
                    overallSalesRevenue.Visible = true;
                    onlinesalesRevenue.Visible = false;
                    walkinsalesRevenue.Visible = false;
                    //lblErrorOnline.Visible = false;
                    //lblErrorWalkin.Visible = false;
                }
            }
            else
            {
                // Handle null response or invalid selected value
                overallSalesRevenue.Text = "No record of sales found";
                Label2.Visible = false;
                Label1.Visible = false;
                Label4.Visible = false;
                overallSalesRevenue.Visible = true;
                onlinesalesRevenue.Visible = false;
                walkinsalesRevenue.Visible = false;
                //lblErrorOnline.Visible = false;
                //lblErrorWalkin.Visible = false;
            }
        }

        private void yearlySalesDisplay()
        {
            string idno = (string)Session["idno"];

            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MinValue;

            fromDate = new DateTime(2023, 1, 1);
            toDate = new DateTime(2023, 12, 31, 23, 59, 59);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

            // Retrieve all customers from the CUSTOMER table and compare the current customer name
            FirebaseResponse customerResponse = twoBigDB.Get("CUSTOMER");
            Dictionary<string, Customer> customerlist = customerResponse.ResultAs<Dictionary<string, Customer>>();

            // Create the DataTable to hold the orders
            DataTable salesordersTable = new DataTable();
            salesordersTable.Columns.Add("Order ID");
            salesordersTable.Columns.Add("Customer Name");
            salesordersTable.Columns.Add("Payment Mode");
            salesordersTable.Columns.Add("Order Status");
            salesordersTable.Columns.Add("Delivery Type");
            salesordersTable.Columns.Add("Transaction Type");
            salesordersTable.Columns.Add("Total Amount");
            salesordersTable.Columns.Add("Order Date");
            // Get the selected date range from the dropdown list
            //string dateRange = ddlDateRange.SelectedValue;

            if (response != null && response.ResultAs<Order>() != null)
            {
                if (orderlist != null)
                {
                    //  Filter the list of orders by the owner's ID
                    List<Order> filteredList = orderlist.Values
                   .Where(d => d.admin_ID.ToString() == idno && (d.orderDate >= fromDate && d.orderDate <= toDate) && (d.order_OrderTypeValue == "PickUp" || d.order_OrderTypeValue == "Delivery")
                       && (d.order_OrderStatus == "Payment Received"))
                   .ToList();
                    
                    //  Retrieve all orders from the ORDERS table
                    FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                    Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                    // Create the DataTable to hold the orders
                    DataTable walkinsalesTable = new DataTable();
                    walkinsalesTable.Columns.Add("Order ID");
                    walkinsalesTable.Columns.Add("Added By");
                    walkinsalesTable.Columns.Add("Amount Paid");
                    walkinsalesTable.Columns.Add("Transaction Type");
                    walkinsalesTable.Columns.Add("Order Date");

                    // Filter the list of orders by the owner's ID and the order status and delivery type
                    List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
                  
                        filteredordersList = walkinOrderlist.Values
                               .Where(d => d.adminId.ToString() == idno && d.dateAdded >= fromDate && d.dateAdded <= toDate)
                               .ToList();
                   
                    decimal totalOrderAmount = 0;
                    decimal overAllSales = 0;
                    decimal totalWalkInAmount = 0;

                    // Check if there are no walk-in sales found
                    if (filteredList.Count == 0)
                    {
                        lblErrorOnline.Visible = true;
                        //lblErrorWalkin.Visible = false;
                        lblErrorOnline.Text = "No yearly online order sales found.";
                        
                    }
                    foreach (Order order in filteredList)
                    {
                        // Retrieve the customer details based on the customer ID from the order
                        if (customerlist.TryGetValue(order.cusId.ToString(), out Customer customer))
                        {
                            string customerName = customer.firstName + " " + customer.lastName;

                            // Add the order details to the DataTable with the customer name
                            string dateOrdered = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            salesordersTable.Rows.Add(order.orderID, customerName, order.order_OrderStatus, order.orderPaymentMethod, order.order_DeliveryTypeValue,
                                order.order_OrderTypeValue, order.order_TotalAmount, dateOrdered);

                            totalOrderAmount += order.order_TotalAmount;
                        }
                     
                    }
                    // Check if there are no walk-in sales found
                    if (filteredordersList.Count == 0)
                    {
                        //lblErrorOnline.Visible = false;
                        lblErrorWalkin.Visible = true;
                        lblErrorWalkin.Text = "No yearly walk-in order sales found.";
                      
                    }
                    foreach (WalkInOrders order in filteredordersList)
                    {
                        string dateOrdered = order.dateAdded == DateTime.MinValue ? "" : order.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        //  string datePaymentReceived = order.datePaymentReceived == DateTime.MinValue ? "" : order.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        walkinsalesTable.Rows.Add(order.orderNo, order.addedBy, order.totalAmount, "Walkin Order", dateOrdered);


                        totalWalkInAmount += order.totalAmount;
                    }

                    overAllSales = totalOrderAmount + totalWalkInAmount;

                    onlinesalesRevenue.Text = "Php." + " " + totalOrderAmount.ToString();
                    walkinsalesRevenue.Text = "Php." + " " + totalWalkInAmount.ToString();
                    overallSalesRevenue.Text = "Php." + " " + overAllSales.ToString();
                    salesRevenueGridView.DataSource = salesordersTable;
                    salesRevenueGridView.DataBind();
                    walkinSales.DataSource = walkinsalesTable;
                    walkinSales.DataBind();
                    Label2.Visible = true;
                    Label1.Visible = true;
                    Label4.Visible = true;
                    overallSalesRevenue.Visible = true;
                    onlinesalesRevenue.Visible = true;
                    walkinsalesRevenue.Visible = true;
                    
                }
                else
                {
                    // Handle null response or invalid selected value
                    overallSalesRevenue.Text = "No record of sales found";
                    Label2.Visible = false;
                    Label1.Visible = false;
                    Label4.Visible = false;
                    overallSalesRevenue.Visible = true;
                    onlinesalesRevenue.Visible = false;
                    walkinsalesRevenue.Visible = false;
                   
                }
            }
            else
            {
                // Handle null response or invalid selected value
                overallSalesRevenue.Text = "No record of sales found";
                Label2.Visible = false;
                Label1.Visible = false;
                Label4.Visible = false;
                overallSalesRevenue.Visible = true;
                onlinesalesRevenue.Visible = false;
                walkinsalesRevenue.Visible = false;
               
            }
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
                    overAllSalesDisplay();
                }
                else if (selectedOption == "1")
                {
                    dailySalesDisplay();
                }
                else if (selectedOption == "2")
                {
                    weeklySalesDisplay();
                }
                else if (selectedOption == "3")
                {
                    monthlySalesDisplay();
                }
                else if (selectedOption == "4")
                {
                    yearlySalesDisplay();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/SalesReports.aspx';" + ex.Message);
            }
        }

        //Sorting date to view
        protected void generateSortedData_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];

            if (string.IsNullOrEmpty(sortStart.Text) || string.IsNullOrEmpty(sortEnd.Text))
            {
                // Handle the missing start or end date condition 
                Response.Write("<script>alert ('You must choose a Start and End Date of the logs you want to view.');</script>");
                return; // Exit the method or return the appropriate response
            }

            // Get the start and end dates entered by the user
            DateTime startDate = DateTime.Parse(sortStart.Text);
            DateTime endDate = DateTime.Parse(sortEnd.Text).AddDays(1); // Add 1 day to include the end date

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

            // Retrieve all customers from the CUSTOMER table and compare the current customer name
            FirebaseResponse customerResponse = twoBigDB.Get("CUSTOMER");
            Dictionary<string, Customer> customerlist = customerResponse.ResultAs<Dictionary<string, Customer>>();

            // Create the DataTable to hold the orders
            DataTable salesordersTable = new DataTable();
            salesordersTable.Columns.Add("Order ID");
            salesordersTable.Columns.Add("Customer Name");
            salesordersTable.Columns.Add("Payment Mode");
            salesordersTable.Columns.Add("Order Status");
            salesordersTable.Columns.Add("Delivery Type");
            salesordersTable.Columns.Add("Transaction Type");
            salesordersTable.Columns.Add("Total Amount");
            salesordersTable.Columns.Add("Order Date");

            if (response != null && response.ResultAs<Order>() != null)
            {
                if (orderlist != null)
                {
                    //  Filter the list of orders by the owner's ID
                    List<Order> filteredList = orderlist.Values
                   .Where(d => d.admin_ID.ToString() == idno && (d.orderDate >= startDate && d.orderDate <= endDate) && (d.order_OrderTypeValue == "PickUp" || d.order_OrderTypeValue == "Delivery")
                       && (d.order_OrderStatus == "Payment Received"))
                   .ToList();
                  
                    //  Retrieve all orders from the ORDERS table
                    FirebaseResponse res = twoBigDB.Get("WALKINORDERS");
                    Dictionary<string, WalkInOrders> walkinOrderlist = res.ResultAs<Dictionary<string, WalkInOrders>>();

                    // Create the DataTable to hold the orders
                    DataTable walkinsalesTable = new DataTable();
                    walkinsalesTable.Columns.Add("Order ID");
                    walkinsalesTable.Columns.Add("Added By");
                    walkinsalesTable.Columns.Add("Amount Paid");
                    walkinsalesTable.Columns.Add("Transaction Type");
                    walkinsalesTable.Columns.Add("Order Date");

                    // Filter the list of orders by the owner's ID and the order status and delivery type
                    List<WalkInOrders> filteredordersList = new List<WalkInOrders>();
                    
                    filteredordersList = walkinOrderlist.Values
                           .Where(d => d.adminId.ToString() == idno && d.dateAdded >= startDate && d.dateAdded <= endDate)
                           .ToList();

                    decimal totalOrderAmount = 0;
                    decimal overAllSales = 0;
                    decimal totalWalkInAmount = 0;

                    // Check if there are no walk-in sales found
                    if (filteredList.Count == 0)
                    {
                        lblErrorOnline.Visible = true;
                        lblErrorOnline.Text = "No yearly online order sales found.";

                    }
                    foreach (Order order in filteredList)
                    {
                        // Retrieve the customer details based on the customer ID from the order
                        if (customerlist.TryGetValue(order.cusId.ToString(), out Customer customer))
                        {
                            string customerName = customer.firstName + " " + customer.lastName;

                            // Add the order details to the DataTable with the customer name
                            string dateOrdered = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            salesordersTable.Rows.Add(order.orderID, customerName, order.order_OrderStatus, order.orderPaymentMethod, order.order_DeliveryTypeValue,
                                order.order_OrderTypeValue, order.order_TotalAmount, dateOrdered);

                            totalOrderAmount += order.order_TotalAmount;
                        }
                    }
                    // Check if there are no walk-in sales found
                    if (filteredordersList.Count == 0)
                    {
                        //lblErrorOnline.Visible = false;
                        lblErrorWalkin.Visible = true;
                        lblErrorWalkin.Text = "No yearly walk-in order sales found.";

                    }
                    foreach (WalkInOrders order in filteredordersList)
                    {
                        string dateOrdered = order.dateAdded == DateTime.MinValue ? "" : order.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        //  string datePaymentReceived = order.datePaymentReceived == DateTime.MinValue ? "" : order.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        walkinsalesTable.Rows.Add(order.orderNo, order.addedBy, order.totalAmount, "Walkin Order", dateOrdered);


                        totalWalkInAmount += order.totalAmount;
                    }

                    overAllSales = totalOrderAmount + totalWalkInAmount;

                    onlinesalesRevenue.Text = "Php." + " " + totalOrderAmount.ToString();
                    walkinsalesRevenue.Text = "Php." + " " + totalWalkInAmount.ToString();
                    overallSalesRevenue.Text = "Php." + " " + overAllSales.ToString();
                    salesRevenueGridView.DataSource = salesordersTable;
                    salesRevenueGridView.DataBind();
                    walkinSales.DataSource = walkinsalesTable;
                    walkinSales.DataBind();
                    Label2.Visible = true;
                    Label1.Visible = true;
                    Label4.Visible = true;
                    overallSalesRevenue.Visible = true;
                    onlinesalesRevenue.Visible = true;
                    walkinsalesRevenue.Visible = true;
                    //lblErrorOnline.Visible = false;
                    //lblErrorWalkin.Visible = false;
                }
                else
                {
                    // Handle null response or invalid selected value
                    overallSalesRevenue.Text = "No record of sales found";
                    Label2.Visible = false;
                    Label1.Visible = false;
                    Label4.Visible = false;
                    overallSalesRevenue.Visible = true;
                    onlinesalesRevenue.Visible = false;
                    walkinsalesRevenue.Visible = false;
                    //lblErrorOnline.Visible = false;
                    //lblErrorWalkin.Visible = false;
                }
            }
            else
            {
                // Handle null response or invalid selected value
                overallSalesRevenue.Text = "No record of sales found";
                Label2.Visible = false;
                Label1.Visible = false;
                Label4.Visible = false;
                overallSalesRevenue.Visible = true;
                onlinesalesRevenue.Visible = false;
                walkinsalesRevenue.Visible = false;
                //lblErrorOnline.Visible = false;
                //lblErrorWalkin.Visible = false;
            }
        }
        //Cleat the sorting date textbox fields
        protected void clearSort_Click(object sender, EventArgs e)
        {
            sortStart.Text = "";
            sortEnd.Text = "";
            Response.Write("<script> window.location.href = '/superAdmin/SalesReports.aspx'; </script>");

        }

    }
}
