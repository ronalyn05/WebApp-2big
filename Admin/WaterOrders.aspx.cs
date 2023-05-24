using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WRS2big_Web.Model;

namespace WRS2big_Web.Admin
{
    public partial class WaterOrders : System.Web.UI.Page
    {
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

            if (!IsPostBack)
            {
                onlineordersDisplay();
                lblOrder.Text = "LIST OF ONLINE ORDERS";
                walkinordersDisplay();
                //lblWalkIn.Text = "LIST OF WALKIN ORDERS";
            }

        }
        //DISPLAY ONLINE ORDERS
        private void onlineordersDisplay()
        {
            string idno = (string)Session["idno"];

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();
            

            // Create the DataTable to hold the orders
            DataTable ordersTable = new DataTable();
            ordersTable.Columns.Add("ORDER ID");
            ordersTable.Columns.Add("CUSTOMER ID");
            ordersTable.Columns.Add("DRIVER ID");
            ordersTable.Columns.Add("STATUS");
            ordersTable.Columns.Add("STORE NAME");
            ordersTable.Columns.Add("PAYMENT MODE");
            ordersTable.Columns.Add("TOTAL AMOUNT");
            ordersTable.Columns.Add("ORDER DATE");
            ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
            ordersTable.Columns.Add("ORDER ACCEPTED BY");
            ordersTable.Columns.Add("DATE OF ORDER DECLINED");
            ordersTable.Columns.Add("ORDER DECLINED BY");
            ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
            ordersTable.Columns.Add("DRIVER ASSIGNED BY");
            ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
            ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
            ordersTable.Columns.Add("PAYMENT RECEIVED BY");

            if (response != null && response.ResultAs<Order>() != null)
            {
                var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno &&
                (d.order_OrderStatus == "Payment Received" || d.order_OrderStatus == "Received"));
                //var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && d.order_OrderStatus == "Delivered");
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    //if(entry.orderPaymentMethod == "CashOnDelivery")
                    //{

                    //}
                    string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string datePayment = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                             dateOrder, dateAccepted, entry.orderAcceptedBy, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy,
                             dateDelivered, datePayment, entry.paymentReceivedBy);
                }
                if (ordersTable.Rows.Count == 0)
                {
                    lblMessage.Text = "No record of successful order found  ";
                    lblMessage.Visible = true;
                }
                else
                {
                    // Bind the DataTable to the GridView
                    gridOrder.DataSource = ordersTable;
                    gridOrder.DataBind();
                }
              
            }
            else
            {
                // Handle null response or invalid selected value
                lblMessage.Text = "No record found";
            }

            
        }

        //DISPLAY WALKIN ORDERS
        private void walkinordersDisplay()
        {
            string idno = (string)Session["idno"];
            decimal discount;

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("WALKINORDERS");
            Dictionary<string, WalkInOrders> otherproductsList = response.ResultAs<Dictionary<string, WalkInOrders>>();
            

            // Create the DataTable to hold the orders
            DataTable walkInordersTable = new DataTable();
            walkInordersTable.Columns.Add("ORDER ID");
            walkInordersTable.Columns.Add("ORDER TYPE");
            walkInordersTable.Columns.Add("PRODUCT NAME");
            walkInordersTable.Columns.Add("PRODUCT UNIT & SIZE");
            walkInordersTable.Columns.Add("PRICE");
            walkInordersTable.Columns.Add("QUANTITY");
            walkInordersTable.Columns.Add("DISCOUNT");
            walkInordersTable.Columns.Add("TOTAL AMOUNT");
            walkInordersTable.Columns.Add("DATE");
            walkInordersTable.Columns.Add("ADDED BY");

            // Get the selected date range from the dropdown list
            //string dateRange = ddlDateRange.SelectedValue;

            if (response != null && response.ResultAs<WalkInOrders>() != null)
            {
                var filteredList = otherproductsList.Values.Where(d => d.adminId.ToString() == idno);

                // Loop through the filtered orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    if (!decimal.TryParse(entry.productDiscount.ToString(), out discount))
                    {
                        // If the discount value is not a valid decimal, assume it is zero
                        discount = 0;
                    }
                    else
                    {
                        // Convert discount from percentage to decimal
                        discount /= 100;
                    }

                    string dateAdded = entry.dateAdded == DateTime.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    //walkInordersTable.Rows.Add(entry.orderNo, entry.orderType, entry.productName, entry.productSize + " " + entry.productUnit,
                    //    entry.productPrice, entry.productQty, discount, entry.totalAmount, dateAdded, entry.addedBy);


                    walkInordersTable.Rows.Add(entry.orderNo, entry.orderType, entry.productName, entry.productUnitSize,
                                         entry.productPrice, entry.productQty, discount,
                                         entry.totalAmount, dateAdded, entry.addedBy);
                }

                if (walkInordersTable.Rows.Count == 0)
                {
                    lblMessage.Text = "No record found";
                    lblMessage.Visible = true;
                }
                else
                {
                    // Bind the DataTable to the GridView
                    gridWalkIn.DataSource = walkInordersTable;
                    gridWalkIn.DataBind();
                }
               
            }
            else
            {
                // Handle null response or invalid selected value
                lblMessage.Text = "No record found";
            }
        }
        //SEARCH REPORTS
        protected void btnSearchOrder_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#view').modal();", true);

            string idno = (string)Session["idno"];
            decimal discount;

            try
            {
                string ordernum = txtSearch.Text;

                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("WALKINORDERS");
                Dictionary<string, WalkInOrders> walkinOrderList = response.ResultAs<Dictionary<string, WalkInOrders>>();

                // Create the DataTable to hold the orders
                DataTable walkInordersTable = new DataTable();
                walkInordersTable.Columns.Add("ORDER ID");
                walkInordersTable.Columns.Add("ORDER TYPE");
                walkInordersTable.Columns.Add("PRODUCT NAME");
                walkInordersTable.Columns.Add("PRODUCT UNIT & SIZE");
                // walkInordersTable.Columns.Add("PRODUCT SIZE");
                walkInordersTable.Columns.Add("PRICE");
                walkInordersTable.Columns.Add("QUANTITY");
                walkInordersTable.Columns.Add("DISCOUNT");
                walkInordersTable.Columns.Add("TOTAL AMOUNT");
                walkInordersTable.Columns.Add("DATE ADDED");
                walkInordersTable.Columns.Add("ADDED BY");

                // Retrieve all orders from the ORDERS table
                FirebaseResponse responselist = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> onlineOrderlist = responselist.ResultAs<Dictionary<string, Order>>();

                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
                ordersTable.Columns.Add("ORDER ACCEPTED BY");
                ordersTable.Columns.Add("DATE OF ORDER DECLINED");
                ordersTable.Columns.Add("ORDER DECLINED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");
                ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
                ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
                ordersTable.Columns.Add("PAYMENT RECEIVED BY");

             

                //condition to fetch the product refill data
                if (response != null && response.ResultAs<WalkInOrders>() != null)
                {
                    //var filteredList = productsList.Values.Where(d => d.adminId.ToString() == idno && (d.pro_refillId.ToString() == productnum));
                    var filteredList = walkinOrderList.Values.Where(d => d.adminId.ToString() == idno);


                    // Loop through the entries and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        if (ordernum == entry.orderNo.ToString())
                        {
                           
                            if (!decimal.TryParse(entry.productDiscount.ToString(), out discount))
                            {
                                // If the discount value is not a valid decimal, assume it is zero
                                discount = 0;
                            }
                            else
                            {
                                // Convert discount from percentage to decimal
                                discount /= 100;
                            }
                            string dateAdded = entry.dateAdded == DateTime.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            walkInordersTable.Rows.Add(entry.orderNo, entry.orderType, entry.productName, entry.productSize + " " + entry.productUnit,
                                entry.productPrice, entry.productQty, discount, entry.totalAmount, dateAdded, entry.addedBy);
                        }
                    }

                }
                else
                {
                    //Response.Write("<script>alert('Error retrieving product data.');</script>");
                    lblMessage.Text = "No data found for order with id number" + ordernum;
                }

                //condition to fetch the other product data
                if (responselist != null && responselist.ResultAs<Order>() != null)
                {
                    var filteredList = onlineOrderlist.Values.Where(d => d.admin_ID.ToString() == idno);

                    // Loop through the entries and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        if (ordernum == entry.orderID.ToString())
                        {
                            string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string datePayment = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateOrder = entry.orderDate == DateTime.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                                     dateOrder, dateAccepted, entry.orderAcceptedBy, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy,
                                     dateDelivered, datePayment, entry.paymentReceivedBy);
                        }
                    }
                }
                else
                {
                    //Response.Write("<script>alert('Error retrieving product data.');</script>");
                    lblError_Message.Text = "No data found for order with id number" + ordernum;
                }

                // Bind the DataTable to the GridView
                gridOnline_Order.DataSource = ordersTable;
                gridOnline_Order.DataBind();

                gridWalkin_Order.DataSource = walkInordersTable;
                gridWalkin_Order.DataBind();
                // lblProductId.Text = productnum;

                //  Response.Write("<script> location.reload(); window.location.href = '/Admin/WaterOrders.aspx'; </script>");
                txtSearch.Text = null;

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Select '); location.reload(); window.location.href = '/Admin/WaterOrders.aspx'; </script>" + ex.Message);
            }
        }
        //DISPLAY ALL COD ORDER 
        private void displayAllCOD_order()
        {
           
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
                ordersTable.Columns.Add("ORDER ACCEPTED BY");
                ordersTable.Columns.Add("DATE OF ORDER DECLINED");
                ordersTable.Columns.Add("ORDER DECLINED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");
                ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
                ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
                ordersTable.Columns.Add("PAYMENT RECEIVED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus != "Pending")).OrderByDescending(d => d.orderDate);
                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        //if(entry.orderPaymentMethod == "CashOnDelivery")
                        //{

                        //}
                        string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string datePayment = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                                 dateOrder, dateAccepted, entry.orderAcceptedBy, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy,
                                 dateDelivered, datePayment, entry.paymentReceivedBy);
                    }
                    if (ordersTable.Rows.Count == 0)
                    {
                        lblMessage.Text = "No record of successful order found  ";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        // Bind the DataTable to the GridView
                        gridOrder.DataSource = ordersTable;
                        gridOrder.DataBind();
                    }

                }
                else
                {
                    // Handle null response or invalid selected value
                    lblMessage.Text = "No record found";
                }
               
            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //DISPLAY THE ACCEPTED ORDER
        private void displayAccepted_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                    // Retrieve all orders from the ORDERS table
                    FirebaseResponse response = twoBigDB.Get("ORDERS");
                    Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                    // Create the DataTable to hold the orders
                    DataTable ordersTable = new DataTable();
                    ordersTable.Columns.Add("ORDER ID");
                    ordersTable.Columns.Add("CUSTOMER ID");
                    ordersTable.Columns.Add("DRIVER ID");
                    ordersTable.Columns.Add("STATUS");
                    ordersTable.Columns.Add("DELIVERY TYPE");
                    ordersTable.Columns.Add("ORDER TYPE");
                    ordersTable.Columns.Add("STORE NAME");
                    ordersTable.Columns.Add("PAYMENT MODE");
                    ordersTable.Columns.Add("TOTAL AMOUNT");
                    ordersTable.Columns.Add("ORDER DATE");
                    ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
                    ordersTable.Columns.Add("ORDER ACCEPTED BY");
                    ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                    ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                     var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus == "Accepted")).OrderByDescending(d => d.orderDate);
                        
                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                        {
                            string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                           dateOrder, dateAccepted, entry.orderAcceptedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }
                    if (ordersTable.Rows.Count == 0)
                    {
                        lblMessage.Text = "No Delivered Orders Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridStatusAccepted.DataSource = ordersTable;
                        gridStatusAccepted.DataBind();
                    }
                }
                else
                {
                   // Handle null response or invalid selected value
                   lblMessage.Text = "No Accepted Order found";
                }

                displayDeclined_order();

            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //UPDATING THE STATUS ORDER IF DECLINE
        protected void btnDecline_Click(object sender, EventArgs e)
        {
            // Retrieve the button that was clicked
            Button btnDecline = (Button)sender;
            // Get the order ID from the command argument
            //int orderID = int.Parse(btnDecline.CommandArgument);
            // Find the GridView row containing the button
            GridViewRow row = (GridViewRow)btnDecline.NamingContainer;

            // Get the order ID from the specific column
            int orderIDColumnIndex = 1; //  the actual column index of the order ID
            int orderID = int.Parse(row.Cells[orderIDColumnIndex].Text);

            // Store the order ID in a hidden field for later use
            hfDeclineOrderID.Value = orderID.ToString();

            // Show the modal popup
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "declineModal", "$('#declineModal').modal('show');", true);
        }
        //DECLINE ORDER
        protected void btnSubmitDecline_Click(object sender, EventArgs e)
        {
            // Get the order ID from the hidden field
            int orderID = int.Parse(hfDeclineOrderID.Value);

            // Get the reason input value
            string reason = reasonInput.Value;

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            if (response != null && existingOrder != null)
            {
                // Update the order status and other details
                existingOrder.order_OrderStatus = "Declined";
                existingOrder.driverId = 0; // Clear the driver ID
                existingOrder.dateOrderDeclined = DateTime.Now;
                existingOrder.orderDeclinedBy = (string)Session["fullname"];

                // Update the existing order object in the database
                response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                // Store the entered reasons in the NOTIFICATION table
                Random rnd = new Random();
                int notificationID = rnd.Next(1, 20000);
                var notification = new Model.Notification
                {
                    admin_ID = int.Parse((string)Session["idno"]),
                    sender = "Admin",
                    orderID = orderID,
                    cusId = existingOrder.cusId,
                    receiver = "Customer",
                    title = "Order Declined",
                    driverId = existingOrder.driverId,
                    body = reason,
                    notificationDate = DateTime.Now,
                    status = "unread",
                    notificationID = notificationID
                };

                SetResponse notifResponse = twoBigDB.Set("NOTIFICATION/" + notificationID, notification);

                // Notify the driver if a driver is assigned to the order
                if (!string.IsNullOrEmpty(existingOrder.driverId.ToString()))
                {
                    // Retrieve the driver object from the database
                    FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES/" + existingOrder.driverId);
                    Employee driver = driverResponse.ResultAs<Employee>();

                    if (driverResponse != null && driver != null)
                    {
                        // Create a notification for the driver
                        var driverNotification = new Model.Notification
                        {
                            admin_ID = int.Parse((string)Session["idno"]),
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Driver",
                            title = "Order Declined",
                            driverId = existingOrder.driverId,
                            body = "Your assigned order has been declined. Reason: " + reason,
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = rnd.Next(1, 20000)
                        };

                        SetResponse driverNotifResponse = twoBigDB.Set("NOTIFICATION/" + driverNotification.notificationID, driverNotification);
                    }
                }

                // Perform  additional actions or display messages as needed

                Response.Write("<script>alert ('Order Declined!'); window.location.href = '/Admin/OnlineOrders.aspx';</script>");

                // Retrieve the existing Users log object from the database
                FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + (int)Session["logsId"]);
                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse((string)Session["idno"]),
                    logsId = rnd.Next(1, 10000),
                    userFullname = (string)Session["fullname"],
                    userActivity = "DECLINED ORDER",
                    activityTime = addedTime
                };

                twoBigDB.Update("ADMINLOGS/" + log.logsId, log);
            }

            displayDeclined_order();
        }
        //   DISPLAY THE DECLINED ORDER
        private void displayDeclined_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER DECLINED");
                ordersTable.Columns.Add("ORDER DECLINED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus == "Declined")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                           dateOrder, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblMessage.Text = "No Delivered Orders Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridOrder.DataSource = ordersTable;
                        gridOrder.DataBind();
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblMessage.Text = "No Declined Order found";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retreiving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //   DISPLAY THE DELIVERED ORDER
        private void displayDelivered_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus == "Declined")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, 
                            entry.orderPaymentMethod, entry.order_TotalAmount, dateOrder, dateDelivered, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblMessage.Text = "No Delivered Orders Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridOrder.DataSource = ordersTable;
                        gridOrder.DataBind();
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblMessage.Text = "No Delivered Order found";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //   DISPLAY THE PAYMENT RECEIVED ORDER
        private void displayPaymentReceived_order()
        {
            string idno = (string)Session["idno"];

            try
            {

                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
                ordersTable.Columns.Add("PAYMENT RECEIVED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus == "Payment Received")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string datePaymentReceived = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName,
                            entry.orderPaymentMethod, entry.order_TotalAmount, dateOrder, datePaymentReceived, entry.paymentReceivedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblMessage.Text = "No Payment Received Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridOrder.DataSource = ordersTable;
                        gridOrder.DataBind();
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblMessage.Text = "No  Payment Received found";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //DISPLAY ALL GCASH ORDER 
        private void displayAllGcash_order()
        {

            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
                ordersTable.Columns.Add("ORDER ACCEPTED BY");
                ordersTable.Columns.Add("DATE OF ORDER DECLINED");
                ordersTable.Columns.Add("ORDER DECLINED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");
                ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
                ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
                ordersTable.Columns.Add("PAYMENT RECEIVED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Gcash") && (d.order_OrderStatus != "Pending")).OrderByDescending(d => d.orderDate);
                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        //if(entry.orderPaymentMethod == "CashOnDelivery")
                        //{

                        //}
                        string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string datePayment = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                                 dateOrder, dateAccepted, entry.orderAcceptedBy, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy,
                                 dateDelivered, datePayment, entry.paymentReceivedBy);
                    }
                    if (ordersTable.Rows.Count == 0)
                    {
                        lblGcashError.Text = "No Orders Found";
                        lblGcashError.Visible = true;
                    }
                    else
                    {
                        gridGcash_order.DataSource = ordersTable;
                        gridGcash_order.DataBind();
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblGcashError.Text = "No Orders found";
                }
            }
            catch (Exception ex)
            {
                lblGcashError.Text = "There was an error retrieving orders" + ex.Message;
                lblGcashError.Visible = true;
            }
        }
        //DISPLAY THE ACCEPTED ORDER
        private void displayAcceptedGcash_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
                ordersTable.Columns.Add("ORDER ACCEPTED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Gcash") && (d.order_OrderStatus == "Accepted")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                           dateOrder, dateAccepted, entry.orderAcceptedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }
                    if (ordersTable.Rows.Count == 0)
                    {
                        lblMessage.Text = "No Delivered Orders Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridGcashAccepted_order.DataSource = ordersTable;
                        gridGcashAccepted_order.DataBind();
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblMessage.Text = "No Accepted Order found";
                }

                displayDeclinedGcash_order();

            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //UPDATING THE STATUS ORDER IF DECLINE
        protected void btnDeclineGcash_Click(object sender, EventArgs e)
        {
            // Retrieve the button that was clicked
            Button btnDecline = (Button)sender;
            // Get the order ID from the command argument
            //int orderID = int.Parse(btnDecline.CommandArgument);
            // Find the GridView row containing the button
            GridViewRow row = (GridViewRow)btnDecline.NamingContainer;

            // Get the order ID from the specific column
            int orderIDColumnIndex = 1; //  the actual column index of the order ID
            int orderID = int.Parse(row.Cells[orderIDColumnIndex].Text);

            // Store the order ID in a hidden field for later use
            hfDeclineOrderID.Value = orderID.ToString();

            // Show the modal popup
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "declineModal", "$('#declineModal').modal('show');", true);
        }
        //DECLINE ORDER
        protected void btnSubmitDecline_Click(object sender, EventArgs e)
        {
            // Get the order ID from the hidden field
            int orderID = int.Parse(hfDeclineOrderID.Value);

            // Get the reason input value
            string reason = reasonInput.Value;

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            if (response != null && existingOrder != null)
            {
                // Update the order status and other details
                existingOrder.order_OrderStatus = "Declined";
                existingOrder.driverId = 0; // Clear the driver ID
                existingOrder.dateOrderDeclined = DateTime.Now;
                existingOrder.orderDeclinedBy = (string)Session["fullname"];

                // Update the existing order object in the database
                response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                // Store the entered reasons in the NOTIFICATION table
                Random rnd = new Random();
                int notificationID = rnd.Next(1, 20000);
                var notification = new Model.Notification
                {
                    admin_ID = int.Parse((string)Session["idno"]),
                    sender = "Admin",
                    orderID = orderID,
                    cusId = existingOrder.cusId,
                    receiver = "Customer",
                    title = "Order Declined",
                    driverId = existingOrder.driverId,
                    body = reason,
                    notificationDate = DateTime.Now,
                    status = "unread",
                    notificationID = notificationID
                };

                SetResponse notifResponse = twoBigDB.Set("NOTIFICATION/" + notificationID, notification);

                // Notify the driver if a driver is assigned to the order
                if (!string.IsNullOrEmpty(existingOrder.driverId.ToString()))
                {
                    // Retrieve the driver object from the database
                    FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES/" + existingOrder.driverId);
                    Employee driver = driverResponse.ResultAs<Employee>();

                    if (driverResponse != null && driver != null)
                    {
                        // Create a notification for the driver
                        var driverNotification = new Model.Notification
                        {
                            admin_ID = int.Parse((string)Session["idno"]),
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Driver",
                            title = "Order Declined",
                            driverId = existingOrder.driverId,
                            body = "Your assigned order has been declined. Reason: " + reason,
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = rnd.Next(1, 20000)
                        };

                        SetResponse driverNotifResponse = twoBigDB.Set("NOTIFICATION/" + driverNotification.notificationID, driverNotification);
                    }
                }

                // Perform  additional actions or display messages as needed

                Response.Write("<script>alert ('Order Declined!'); window.location.href = '/Admin/OnlineOrders.aspx';</script>");

                // Retrieve the existing Users log object from the database
                FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + (int)Session["logsId"]);
                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse((string)Session["idno"]),
                    logsId = rnd.Next(1, 10000),
                    userFullname = (string)Session["fullname"],
                    userActivity = "DECLINED ORDER",
                    activityTime = addedTime
                };

                twoBigDB.Update("ADMINLOGS/" + log.logsId, log);
            }

            displayDeclinedGcash_order();
        }
        //   DISPLAY THE DECLINED ORDER
        private void displayDeclinedGcash_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER DECLINED");
                ordersTable.Columns.Add("ORDER DECLINED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus == "Declined")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                           dateOrder, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblMessage.Text = "No Delivered Orders Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridOrder.DataSource = ordersTable;
                        gridOrder.DataBind();
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblMessage.Text = "No Declined Order found";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retreiving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //   DISPLAY THE DELIVERED ORDER
        private void displayDelivered_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus == "Declined")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName,
                            entry.orderPaymentMethod, entry.order_TotalAmount, dateOrder, dateDelivered, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblMessage.Text = "No Delivered Orders Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridOrder.DataSource = ordersTable;
                        gridOrder.DataBind();
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblMessage.Text = "No Delivered Order found";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //   DISPLAY THE PAYMENT RECEIVED ORDER
        private void displayPaymentReceived_order()
        {
            string idno = (string)Session["idno"];

            try
            {

                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
                ordersTable.Columns.Add("PAYMENT RECEIVED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus == "Payment Received")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string datePaymentReceived = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName,
                            entry.orderPaymentMethod, entry.order_TotalAmount, dateOrder, datePaymentReceived, entry.paymentReceivedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblMessage.Text = "No Payment Received Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridOrder.DataSource = ordersTable;
                        gridOrder.DataBind();
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblMessage.Text = "No  Payment Received found";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }

        //SEARCH COD ORDERS
        protected void btnView_Click(object sender, EventArgs e)
        {
            string selectedOption = ddlSearchOptions.SelectedValue;
            try
            {
                

                if (selectedOption == "0")
                {
                    displayAllCOD_order();


                }
                else if (selectedOption == "1")
                {

                    displayAccepted_order();

                }
                else if (selectedOption == "2")
                {

                    displayDeclined_order();
                }
                else if (selectedOption == "3")
                {

                    displayDelivered_order();

                }
                else if (selectedOption == "4")
                {
                    displayPaymentReceived_order();

                }
               

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/WaterOrders.aspx';" + ex.Message);
            }

        }
        //SEARCH POINTS ORDERS
        protected void btnViewPoints_Click(object sender, EventArgs e)
        {
            string selectedOption = drdPoints.SelectedValue;
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
                else if (selectedOption == "4")
                {
                    

                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/WaterOrders.aspx';" + ex.Message);
            }

        }
        //SEARCH POINTS ORDERS
        protected void btnViewGcash_Click(object sender, EventArgs e)
        {

            string selectedOption = drdGcash.SelectedValue;
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
                else if (selectedOption == "4")
                {


                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/WaterOrders.aspx';" + ex.Message);
            }

        }

        //DISPLAY GCASH PROOF IMAGE
        protected void btnPaymentProof_Click(object sender, EventArgs e)
        {
            // Show the modal using JavaScript
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#viewPaymentProof').modal();", true);

            // Get the clicked button and its row
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Retrieve the order id from the data source
            // Get the order ID from the first cell in the row
            int orderID = int.Parse(row.Cells[3].Text);
            //int orderId = Convert.ToInt32(((DataRowView)row.DataItem)["orderID"]);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            // Check if the Gcash proof of payment is available
            string imagePath = existingOrder.order_GcashProofOfPayment;

            // Set the ImageUrl property of the imgGcashProof control in the modal to the retrieved image path 
            imgGcashProof.ImageUrl = imagePath;
        }
     //   UPDATING THE STATUS ORDERS FOR RECEIVING GCASH PAYMENT
        protected void btnPaymentAcceptGcash_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            string name = (string)Session["fullname"];
            // Get the log ID from the session
            int logsId = (int)Session["logsId"];

            //INSERT DATA TO TABLE
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the order ID from the first cell in the row
            int orderID = int.Parse(row.Cells[3].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            // Get a list of all available drivers
            FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES");
            Dictionary<string, Employee> driverData = driverResponse.ResultAs<Dictionary<string, Employee>>();


            if (response != null && response.ResultAs<Order>() != null)
            {
                // Check if the order status is "Accepted", "Payment Received", or "Delivered"
                if (existingOrder.order_OrderStatus == "Payment Received")
                {
                    // Disable the button
                    btn.Enabled = false;


                }

                if (existingOrder.order_OrderStatus == "Payment Received")
                {
                    Response.Write("<script>alert ('Payment of this order has already been received!');</script>");
                    return;
                }

                List<Employee> allDrivers = driverData.Values.Where(emp => emp.adminId.ToString() == idno && emp.emp_role == "Driver"
                                                                    && emp.emp_availability == "Available").ToList();

                if (existingOrder.order_OrderStatus == "Delivered" || existingOrder.order_OrderStatus == "Received Order")
                {
                    // Check if any available driver is available to accept the order
                    if (allDrivers.Count > 0)
                    {
                        // Assign the order to the first available driver
                        Employee driver = allDrivers[0];
                        existingOrder.driverId = driver.emp_id;
                        existingOrder.order_OrderStatus = "Payment Received";
                        existingOrder.paymentGcashReceivedBy = name;
                        existingOrder.datePaymentReceived = DateTime.Now;
                        //existingOrder.datePaymentReceived = DateTimeOffset.Parse("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                        response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                        //SEND NOTIFICATION TO DRIVER FOR PAYMENT RECEIVED
                        int notifID = rnd.Next(1, 20000);
                        var driverNotif = new Model.Notification
                        {
                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            driverId = driver.emp_id,
                            sender = "Admin",
                            receiver = "Driver",
                            body = "The payment for Order ID:" + orderID + " has been received",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = notifID
                        };
                        SetResponse driverNotifRes;
                        driverNotifRes = twoBigDB.Set("NOTIFICATION/" + notifID, driverNotif);//Storing data to the database
                        Notification Drivernotif = driverNotifRes.ResultAs<Notification>();//Database Result


                        //SEND NOTIFICATION TO CUSTOMER 
                        int ID = rnd.Next(1, 20000);
                        var Notification = new Model.Notification
                        {
                            admin_ID = adminId,
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Customer",
                            driverId = driver.emp_id,
                            body = "Your payment for your order with Order ID:" + orderID + "has been received.",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = ID

                        };

                        SetResponse notifResponse;
                        notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                        Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                        //ADMINLOGS
                        // Retrieve the existing Users log object from the database
                        FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
                        UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                        // Get the current date and time
                        DateTime addedTime = DateTime.Now;

                        // Log user activity
                        var log = new UsersLogs
                        {
                            userIdnum = int.Parse(idno),
                            logsId = idnum,
                            userFullname = (string)Session["fullname"],
                            userActivity = "RECEIVED PAYMENT",
                            activityTime = addedTime
                        };

                        twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                    }
                    else
                    {
                        // Display an error messaged
                        Response.Write("<script>alert ('Make sure order is successfully delivered.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                    }

                    //SEND SCHEDULED NOTIFICATION
                    int schedNotifID = rnd.Next(1, 20000);

                    //condition to check if the quantity is less than or equal to 2, schedule notif after 2 days since ordered. 
                    if (existingOrder.order_OverallQuantities <= 2)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(2);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "It's been 2 days since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("SCHEDULED_NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_OverallQuantities > 2 && existingOrder.order_OverallQuantities <= 5)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(4);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "It's been 4 days since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("SCHEDULED_NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_OverallQuantities > 5 && existingOrder.order_OverallQuantities <= 8)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(7);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "It's been a week since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("SCHEDULED_NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_OverallQuantities > 8 && existingOrder.order_OverallQuantities <= 15)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(7);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "We miss you! It's been a long time since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("SCHEDULED_NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                }
            }
        }

    //    UPDATING THE STATUS ORDERS FOR REWARD POINTS IN RECEIVEING GCASH PAYMENT
        protected void btnPaymentAcceptPoints_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            string name = (string)Session["fullname"];
            // Get the log ID from the session
            int logsId = (int)Session["logsId"];

            //INSERT DATA TO TABLE
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the order ID from the first cell in the row
            int orderID = int.Parse(row.Cells[2].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            // Get a list of all available drivers
            FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES");
            Dictionary<string, Employee> driverData = driverResponse.ResultAs<Dictionary<string, Employee>>();

            if (response != null && response.ResultAs<Order>() != null)
            {
                List<Employee> allDrivers = driverData.Values.Where(emp => emp.adminId.ToString() == idno && emp.emp_role == "Driver"
                                                                    && emp.emp_availability == "Available").ToList();

                // Check if the order status is  "Payment Received"
                if (existingOrder.order_OrderStatus == "Payment Received")
                {
                    // Disable the button
                    btn.Enabled = false;

                    // Store the disabled state in the session
                    Session["DisabledButton_" + orderID] = true;
                }

                if (existingOrder.order_OrderStatus == "Payment Received")
                {
                    Response.Write("<script>alert ('Payment for this order has already been received!');</script>");
                    return;
                }

                if (existingOrder.order_OrderStatus == "Delivered" || existingOrder.order_OrderStatus == "Received Order")
                {

                    // Check if any available driver is available to accept the order
                    if (allDrivers.Count > 0)
                    {
                        // Assign the order to the first available driver
                        Employee driver = allDrivers[0];
                        existingOrder.driverId = driver.emp_id;
                        existingOrder.order_OrderStatus = "Payment Received";
                        existingOrder.paymentPointsReceivedBy = name;
                        existingOrder.datePaymentReceived = DateTime.Now;
                        //existingOrder.datePaymentReceived = DateTimeOffset.Parse("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                        response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                        //SEND NOTIFICATION TO DRIVER FOR PAYMENT RECEIVED
                        int notifID = rnd.Next(1, 20000);
                        var driverNotif = new Model.Notification
                        {
                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            driverId = driver.emp_id,
                            sender = "Admin",
                            receiver = "Driver",
                            title = "Payment Received",
                            body = "The payment for Order ID:" + orderID + " has been received",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = notifID
                        };
                        SetResponse driverNotifRes;
                        driverNotifRes = twoBigDB.Set("NOTIFICATION/" + notifID, driverNotif);//Storing data to the database
                        Notification Drivernotif = driverNotifRes.ResultAs<Notification>();//Database Result


                        //SEND NOTIFICATION TO CUSTOMER 
                        int ID = rnd.Next(1, 20000);
                        var Notification = new Model.Notification
                        {
                            admin_ID = adminId,
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Customer",
                            driverId = driver.emp_id,
                            body = "Your payment for your order with Order ID:" + orderID + "has been received.",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = ID

                        };

                        SetResponse notifResponse;
                        notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                        Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                        //ADMINLOGS
                        // Retrieve the existing Users log object from the database
                        FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
                        UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                        // Get the current date and time
                        DateTime addedTime = DateTime.Now;

                        // Log user activity
                        var log = new UsersLogs
                        {
                            userIdnum = int.Parse(idno),
                            logsId = idnum,
                            userFullname = (string)Session["fullname"],
                            userActivity = "RECEIVED PAYMENT",
                            activityTime = addedTime
                        };

                        twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                        gridRewardPoints_order.Visible = true;
                        //DisplayGcash_order();
                    }
                    else
                    {
                        // Display an error messaged
                        Response.Write("<script>alert ('Make sure order is successfully delivered.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                    }


                    //SEND SCHEDULED NOTIFICATION
                    int schedNotifID = rnd.Next(1, 20000);

                    //condition to check if the quantity is less than or equal to 2, schedule notif after 2 days since ordered. 
                    if (existingOrder.order_OverallQuantities <= 2)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(2);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "It's been 2 days since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("SCHEDULED_NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_OverallQuantities > 2 && existingOrder.order_OverallQuantities <= 5)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(4);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "It's been 4 days since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("SCHEDULED_NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_OverallQuantities > 5 && existingOrder.order_OverallQuantities <= 8)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(7);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "It's been a week since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("SCHEDULED_NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_OverallQuantities > 8 && existingOrder.order_OverallQuantities <= 15)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(7);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "We miss you! It's been a long time since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("SCHEDULED_NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }

                }
            }


        }

    }
}