using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Exceptions;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using WRS2big_Web.Model;
using System.Drawing; 
//using AutoMapper;

namespace WRS2big_Web.Admin
{
    public partial class ProductGallon : System.Web.UI.Page
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
            //METHODS TO DISPLAY THE IDs
            if (!IsPostBack)
            {
              
                DisplayRewardPoints_order();
                lblViewOrders.Text = "COD ORDER";
                DisplayGcash_order();
                lblViewGcashOrder.Text = "GCASH ORDER";
                DisplayCOD_order();
                lblViewRewardPoints.Text = "REWARD POINTS ORDER";
            }

            string idno = (string)Session["idno"];

            FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
            Dictionary<string, Employee> employees = response.ResultAs<Dictionary<string, Employee>>();

            // Filter the employees to get only those belonging to the logged-in admin and whose position is "driver"
            Employee driver = employees.Values.FirstOrDefault(emp => emp.adminId.ToString() == idno && emp.emp_role == "Driver");

            if (driver != null)
            {
                // Store the driver's employee ID in the session
                Session["emp_id"] = driver.emp_id;
            }

            response = twoBigDB.Get("ORDERS");
            Order existingOrder = response.ResultAs<Order>();

            Session["orderID"] = existingOrder.orderID;
            // Retrieve the existing order object from the database

           
        }
        //DISPLAY THE ORDER FROM THE CUSTOMER THRU COD
        private void DisplayCOD_order()
        {
            string idno = (string)Session["idno"];
            //string selectedDeliveryType = drdDeliveryType.SelectedValue;

            try
            {
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("RESERVATION DATE");
                ordersTable.Columns.Add("RESERVATION TIME");
                ordersTable.Columns.Add("DELIVERY TYPE SELECTED");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                ordersTable.Columns.Add("PAYMENT METHOD");
                ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE ");
                ordersTable.Columns.Add("STORE NAME");


                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery")).OrderByDescending(d => d.orderDate);

                    foreach (var order in filteredList)
                    {
                        if (order.order_OrderTypeValue == "PickUp")
                        {
                            if (order.order_Products != null)
                            {
                                string productrefill_order = "";
                                string otherproduct_order = "";
                                string productrefill_qty = "";
                                string otherproduct_qty = "";

                                foreach (var product in order.order_Products)
                                {

                                    if (product.offerType == "Product Refill")
                                    {
                                        productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        productrefill_qty += product.qtyPerItem;
                                    }
                                    else if (product.offerType == "thirdparty Product") // corrected spelling of "Other Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }

                                }

                                string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                   dateOrder, order.order_ReservationDate, order.order_StoreName);
                            }
                        }
                        else if (order.order_OrderTypeValue == "Delivery")
                        {
                            string productrefill_order = "";
                            string otherproduct_order = "";
                            string productrefill_qty = "";
                            string otherproduct_qty = "";

                            foreach (var product in order.order_Products)
                            {

                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    productrefill_qty += product.qtyPerItem;
                                }
                                else if (product.offerType == "thirdparty Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }

                            }

                            string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                               order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                               order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                               dateOrder, order.order_StoreName);
                        }
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblCodError.Text = "No  Orders Found";
                        lblCodError.Visible = true;
                    }
                    else
                    {
                        gridCOD_order.DataSource = ordersTable;
                        gridCOD_order.DataBind();
                    }
                }
                else
                {
                    lblCodError.Text = "No Orders Found";
                    lblCodError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblCodError.Text = "There was an error retrieving orders" + ex.Message;
                lblCodError.Visible = true;
            }
        }

        //DISPLAY THE ORDER FROM THE CUSTOMER THRU GCASH
        private void DisplayGcash_order()
        {
            string idno = (string)Session["idno"];
            try
            {
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("RESERVATION DATE");
                ordersTable.Columns.Add("RESERVATION TIME");
                ordersTable.Columns.Add("DELIVERY TYPE SELECTED");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                ordersTable.Columns.Add("PAYMENT METHOD");
                ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE ");
                ordersTable.Columns.Add("STORE NAME");


                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Gcash")).OrderByDescending(d => d.orderDate);

                    foreach (var order in filteredList)
                    {
                        if (order.order_OrderTypeValue == "PickUp")
                        {
                            if (order.order_Products != null)
                            {
                                string productrefill_order = "";
                                string otherproduct_order = "";
                                string productrefill_qty = "";
                                string otherproduct_qty = "";

                                foreach (var product in order.order_Products)
                                {

                                    if (product.offerType == "Product Refill")
                                    {
                                        productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        productrefill_qty += product.qtyPerItem;
                                    }
                                    else if (product.offerType == "thirdparty Product") // corrected spelling of "Other Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }

                                }

                                string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                   dateOrder, order.order_ReservationDate, order.order_StoreName);
                            }
                        }
                        else if (order.order_OrderTypeValue == "Delivery")
                        {
                            string productrefill_order = "";
                            string otherproduct_order = "";
                            string productrefill_qty = "";
                            string otherproduct_qty = "";

                            foreach (var product in order.order_Products)
                            {

                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    productrefill_qty += product.qtyPerItem;
                                }
                                else if (product.offerType == "thirdparty Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }

                            }

                            string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                               order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                               order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                               dateOrder, order.order_StoreName);
                        }
                    }


                    if (ordersTable.Rows.Count == 0)
                    {
                        lblGcashError.Text = "No  Orders Found";
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
                    lblGcashError.Text = "No Orders Found";
                    lblGcashError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblGcashError.Text = "There was an error retrieving orders" + ex.Message;
                lblGcashError.Visible = true;
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

        //   DISPLAY THE ORDER FROM THE CUSTOMER THRU REWARD POINTS
        private void DisplayRewardPoints_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("RESERVATION DATE");
                ordersTable.Columns.Add("RESERVATION TIME");
                ordersTable.Columns.Add("DELIVERY TYPE SELECTED");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                ordersTable.Columns.Add("PAYMENT METHOD");
                ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE ");
                ordersTable.Columns.Add("STORE NAME");


                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Points")).OrderByDescending(d => d.orderDate);

                    foreach (var order in filteredList)
                    {
                        if (order.order_OrderTypeValue == "PickUp")
                        {
                            if (order.order_Products != null)
                            {
                                string productrefill_order = "";
                                string otherproduct_order = "";
                                string productrefill_qty = "";
                                string otherproduct_qty = "";

                                foreach (var product in order.order_Products)
                                {

                                    if (product.offerType == "Product Refill")
                                    {
                                        productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        productrefill_qty += product.qtyPerItem;
                                    }
                                    else if (product.offerType == "thirdparty Product") // corrected spelling of "Other Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }

                                }

                                string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                   dateOrder, order.order_ReservationDate, order.order_StoreName);
                            }
                        }
                        else if (order.order_OrderTypeValue == "Delivery")
                        {
                            string productrefill_order = "";
                            string otherproduct_order = "";
                            string productrefill_qty = "";
                            string otherproduct_qty = "";

                            foreach (var product in order.order_Products)
                            {

                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    productrefill_qty += product.qtyPerItem;
                                }
                                else if (product.offerType == "thirdparty Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }

                            }

                            string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                               order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                               order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                               dateOrder, order.order_StoreName);
                        }
                    }


                    if (ordersTable.Rows.Count == 0)
                    {
                        lblPointsError.Text = "No  Orders Found";
                        lblPointsError.Visible = true;
                    }
                    else
                    {
                        gridRewardPoints_order.DataSource = ordersTable;
                        gridRewardPoints_order.DataBind();
                    }
                }
                else
                {
                    lblPointsError.Text = "No Orders Found";
                    lblPointsError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblPointsError.Text = "There was an error retrieving orders" + ex.Message;
                lblPointsError.Visible = true;
            }
        } 
   
        //ASSIGNING THE DRIVER
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //  generate a random number for employee logged
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            string name = (string)Session["fullname"];

            try
            {
                string orderId = txtOrderId.Text.Trim();
                string driverId = txtDriverId.Text.Trim();

                FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderId);
                Order existingOrder = response.ResultAs<Order>();

                if (existingOrder == null)
                {
                    // Show error message if the order ID entered is invalid
                    Response.Write("<script>alert ('Invalid order ID! Order ID you entered does not exist.');</script>");
                    return;
                }

                // Check if the driver ID is valid
                if (string.IsNullOrEmpty(driverId))
                {
                    Response.Write("<script>alert ('Please enter a valid driver ID!');</script>");
                    return;
                }

                // Update the existing order object with the new driver ID
                existingOrder.driverId = int.Parse(driverId);
                existingOrder.dateDriverAssigned = DateTimeOffset.UtcNow;
                existingOrder.driverAssignedBy = name;

                // Update the existing order object in the database
                response = twoBigDB.Update("ORDERS/" + orderId, existingOrder);


                // Show success message
                Response.Write("<script>alert('You have successfully assigned driver " + driverId + " to order number " + orderId + "'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");

                // Get the current date and time
                DateTime addedTime = DateTime.UtcNow;

                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    activityTime = addedTime,
                    userActivity = "ASSIGNED THE DRIVER TO DELIVER THE ORDER",
                    // userActivity = UserActivityType.UpdatedEmployeeRecords
                };
                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                DisplayRewardPoints_order();
                DisplayGcash_order();
                DisplayCOD_order();

            }
            catch (Exception ex)
            {
                // Show error message
                Response.Write("<script>alert ('An error occurred while processing your request.');</script>" + ex.Message);
            }
        }
        //UPDATING THE STATUS ORDER FOR COD IF ACCEPTED
        protected void btnAcceptCOD_Click(object sender, EventArgs e)
        {
            try
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
                int orderID = int.Parse(row.Cells[1].Text);

                // Retrieve the existing order object from the database
                FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
                Order existingOrder = response.ResultAs<Order>();

                // Check if the order status is "Accepted", "Payment Received", or "Delivered"
                if (existingOrder.order_OrderStatus != "Pending")
                {
                    // Disable the button
                    btn.Enabled = false;

                    // Store the disabled state in the session
                    Session["DisabledButton_" + orderID] = true;
                }

                if (response != null && response.ResultAs<Order>() != null)
                {
                    if (existingOrder.order_OrderStatus == "Accepted")
                    {
                        Response.Write("<script>alert ('Order has already been accepted!');</script>");
                        return;
                    }
                    // Check if the order already has a driver assigned
                    if (existingOrder.driverId != 0)
                    {
                        // Get the driver object from the database
                        FirebaseResponse driverRes = twoBigDB.Get("EMPLOYEES/" + existingOrder.driverId);
                        Employee driver = driverRes.ResultAs<Employee>();


                        // Accept the order with the assigned driver
                        existingOrder.driverId = driver.emp_id;
                        existingOrder.order_OrderStatus = "Accepted";
                        existingOrder.dateOrderAccepted = DateTimeOffset.UtcNow;
                        existingOrder.orderAcceptedBy = name;

                        response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                        //SEND NOTIFICATION TO CUSTOMER FOR ORDER BEING DECLINED
                        //Random rnd = new Random();
                        int ID = rnd.Next(1, 20000);
                        var Notification = new Model.Notification
                        {
                            admin_ID = adminId,
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Customer",
                            title = "Order Accepted",
                            driverId = existingOrder.driverId,
                            body = "Your order is now accepted and is assigned to a driver! Check the order page to view the details of your order",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = ID

                        };

                        SetResponse notifResponse;
                        notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                        Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                        //SEND NOTIFICATION TO DRIVER
                        int notifID = rnd.Next(1, 20000);
                        var driverNotif = new Model.Notification
                        {
                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            driverId = driver.emp_id,
                            sender = "Admin",
                            title = "New Assigned Order",
                            receiver = "Driver",
                            body = "Order ID:" + orderID + " has been assigned to you. Check the order page for the details of the order",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = notifID
                        };
                        SetResponse driverNotifRes;
                        driverNotifRes = twoBigDB.Set("NOTIFICATION/" + notifID, driverNotif);//Storing data to the database
                        Notification Drivernotif = driverNotifRes.ResultAs<Notification>();//Database Result

                        Response.Write("<script>alert ('Order Accepted!'); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                        //else
                        //{
                        //    // Handle the case where the existing notification does not exist
                        //}

                        // Retrieve the existing Users log object from the database
                        FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
                        UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                        // Get the current date and time
                        DateTime addedTime = DateTime.UtcNow;

                        // Log user activity
                        var log = new UsersLogs
                        {
                            userIdnum = int.Parse(idno),
                            logsId = idnum,
                            //orderId = orderID,
                            userFullname = (string)Session["fullname"],
                            userActivity = "ACCEPTED ORDER",
                            activityTime = addedTime
                        };

                        twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                        gridCOD_order.Visible = true;
                        //DisplayRewardPoints_order();
                        //DisplayGcash_order();
                        DisplayCOD_order();
                    }
                    else
                    {
                        // Get a list of all available drivers
                        FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES");
                        Dictionary<string, Employee> driverData = driverResponse.ResultAs<Dictionary<string, Employee>>();

                        List<Employee> allDrivers = driverData.Values.Where(emp => emp.adminId.ToString() == idno && (emp.emp_role == "Driver"
                                                                                && emp.emp_availability == "Available")).ToList();
                        // Check if the order status is "Pickup"
                        if (existingOrder.order_OrderTypeValue == "PickUp")
                        {
                            existingOrder.order_OrderStatus = "Accepted";
                            existingOrder.driverId = 0; // clear the driver ID

                            response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                            // Display an error message indicating that no driver will be assigned 
                            Response.Write("<script>alert ('This order will be pick up by the customer. No driver will be assigned.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                            //DisplayRewardPoints_order();
                            //DisplayGcash_order();
                            gridCOD_order.Visible = true;
                            DisplayCOD_order();
                        }
                        else
                        {
                            // Check if any available driver is available to accept the order
                            if (allDrivers.Count > 0)
                            {
                                // Assign the order to the first available driver
                                Employee driver = allDrivers[0];
                                existingOrder.driverId = driver.emp_id;
                                existingOrder.order_OrderStatus = "Accepted";
                                existingOrder.dateOrderAccepted = DateTimeOffset.UtcNow;
                                existingOrder.dateDriverAssigned = DateTimeOffset.UtcNow;
                                existingOrder.driverAssignedBy = name;

                                response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                                //SEND NOTIFICATION TO CUSTOMER FOR ORDER BEING DECLINED
                                //Random rnd = new Random();
                                int ID = rnd.Next(1, 20000);
                                var Notification = new Model.Notification
                                {
                                    admin_ID = adminId,
                                    sender = "Admin",
                                    orderID = orderID,
                                    cusId = existingOrder.cusId,
                                    receiver = "Customer",
                                    title = "Order Accepted",
                                    driverId = existingOrder.driverId,
                                    body = "Your order is now accepted and is assigned to a driver! Check the order page to view the details of your order",
                                    notificationDate = DateTime.Now,
                                    status = "unread",
                                    notificationID = ID

                                };

                                SetResponse notifResponse;
                                notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                                Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                                //SEND NOTIFICATION TO DRIVER
                                int notifID = rnd.Next(1, 20000);
                                var driverNotif = new Model.Notification
                                {
                                    admin_ID = adminId,
                                    orderID = orderID,
                                    cusId = existingOrder.cusId,
                                    driverId = driver.emp_id,
                                    sender = "Admin",
                                    title = "New Assigned Order",
                                    receiver = "Driver",
                                    body = "Order ID:" + orderID + " has been assigned to you. Check the order page for the details of the order",
                                    notificationDate = DateTime.Now,
                                    status = "unread",
                                    notificationID = notifID
                                };
                                SetResponse driverNotifRes;
                                driverNotifRes = twoBigDB.Set("NOTIFICATION/" + notifID, driverNotif);//Storing data to the database
                                Notification Drivernotif = driverNotifRes.ResultAs<Notification>();//Database Result

                                Response.Write("<script>alert ('Order Accepted!'); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                              
                                // Retrieve the existing Users log object from the database
                                FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
                                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                                // Get the current date and time
                                DateTime addedTime = DateTime.UtcNow;

                                // Log user activity
                                var log = new UsersLogs
                                {
                                    userIdnum = int.Parse(idno),
                                    logsId = idnum,
                                    //orderId = orderID,
                                    userFullname = (string)Session["fullname"],
                                    userActivity = "ACCEPTED ORDER",
                                    activityTime = addedTime
                                };

                                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                                //DisplayRewardPoints_order();
                                //DisplayGcash_order();
                                DisplayCOD_order();
                                gridCOD_order.Visible = true;
                            }
                            else
                            {
                                // No drivers are currently available, display an error message
                                Response.Write("<script>alert ('There are no available drivers to accept this order. Wait for an available driver to deliver this order.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Show error message
                Response.Write("<script>alert ('An error occurred while processing your request.');</script>" + ex.Message);
            }

        }
        //UPDATING THE STATUS ORDER FOR COD IF DECLINE
        protected void btnDeclineCOD_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            // int driverId = (int)Session["emp_id"];
            // Get the log ID from the session 
            int logsId = (int)Session["logsId"];
            //INSERT DATA TO TABLE
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);


            // Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the order ID from the first cell in the row
            int orderID = int.Parse(row.Cells[1].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            // Update the order status in the existing object
            existingOrder.order_OrderStatus = "Declined";
            existingOrder.driverId = 0; // clear the driver ID
            existingOrder.dateOrderDeclined = DateTimeOffset.UtcNow;

            //existingOrder.driverId = (int)driverId; // clear the driver ID

            // Update the existing order object in the database
            response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

            Response.Write("<script>alert ('Order Declined!'); window.location.href = '/Admin/OnlineOrders.aspx';</script>");

            //SEND NOTIFICATION TO CUSTOMER FOR ORDER BEING DECLINED
            //Random rnd = new Random();
            int ID = rnd.Next(1, 20000);
            var Notification = new Model.Notification
            {
                admin_ID = adminId,
                sender = "Admin",
                orderID = orderID,
                cusId = existingOrder.cusId,
                receiver = "Customer",
                title = "Order Declined",
                driverId = existingOrder.driverId,
                body = "Unfortunately, your order with Order ID:" + orderID + " is declined due to some technical issues.",
                notificationDate = DateTime.Now,
                status = "unread",
                notificationID = ID

            };

            SetResponse notifResponse;
            notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
            Notification notif = notifResponse.ResultAs<Notification>();//Database Result


            // Retrieve the existing Users log object from the database
            FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
            UsersLogs existingLog = resLog.ResultAs<UsersLogs>();


           // Get the current date and time
            DateTime addedTime = DateTime.UtcNow;

            // Log user activity
            var log = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = idnum,
                userFullname = (string)Session["fullname"],
                userActivity = "DECLINED ORDER",
                activityTime = addedTime
            };

            twoBigDB.Update("ADMINLOGS/" + log.logsId, log);
            //DisplayRewardPoints_order();
            //DisplayGcash_order();
            gridCOD_order.Visible = true;
            DisplayCOD_order();

        }
        //UPDATING THE STATUS ORDER FOR GCASH IF ACCEPTED
        protected void btnAcceptGcash_Click(object sender, EventArgs e)
        {
            try
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

                if (response != null && response.ResultAs<Order>() != null)
                {
                    // Check if the order already has a driver assigned
                    if (existingOrder.driverId != 0)
                    {
                        // Get the driver object from the database
                        FirebaseResponse driverRes = twoBigDB.Get("EMPLOYEES/" + existingOrder.driverId);
                        Employee driver = driverRes.ResultAs<Employee>();


                        // Accept the order with the assigned driver
                        existingOrder.driverId = driver.emp_id;
                        existingOrder.order_OrderStatus = "Accepted";
                        existingOrder.dateOrderAccepted = DateTimeOffset.UtcNow;

                        response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                        //SEND NOTIFICATION TO CUSTOMER FOR ORDER BEING DECLINED
                        //Random rnd = new Random();
                        int ID = rnd.Next(1, 20000);
                        var Notification = new Model.Notification
                        {
                            admin_ID = adminId,
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Customer",
                            title = "Order Accepted",
                            driverId = existingOrder.driverId,
                            body = "Your order is now accepted and is assigned to a driver! Check the order page to view the details of your order",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = ID

                        };

                        SetResponse notifResponse;
                        notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                        Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                        //SEND NOTIFICATION TO DRIVER
                        int notifID = rnd.Next(1, 20000);
                        var driverNotif = new Model.Notification
                        {
                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            driverId = driver.emp_id,
                            sender = "Admin",
                            title = "New Assigned Order",
                            receiver = "Driver",
                            body = "Order ID:" + orderID + " has been assigned to you. Check the order page for the details of the order",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = notifID
                        };
                        SetResponse driverNotifRes;
                        driverNotifRes = twoBigDB.Set("NOTIFICATION/" + notifID, driverNotif);//Storing data to the database
                        Notification Drivernotif = driverNotifRes.ResultAs<Notification>();//Database Result

                        Response.Write("<script>alert ('Order Accepted!'); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                       
                        // Retrieve the existing Users log object from the database
                        FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
                        UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                        // Get the current date and time
                        DateTime addedTime = DateTime.UtcNow;

                        // Log user activity
                        var log = new UsersLogs
                        {
                            userIdnum = int.Parse(idno),
                            logsId = idnum,
                            //orderId = orderID,
                            userFullname = (string)Session["fullname"],
                            userActivity = "ACCEPTED ORDER",
                            activityTime = addedTime
                        };

                        twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                        //DisplayRewardPoints_order();
                        DisplayGcash_order();
                        gridGcash_order.Visible = true;
                        //DisplayCOD_order();
                    }
                    else
                    {
                        // Get a list of all available drivers
                        FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES");
                        Dictionary<string, Employee> driverData = driverResponse.ResultAs<Dictionary<string, Employee>>();

                        List<Employee> allDrivers = driverData.Values.Where(emp => emp.adminId.ToString() == idno && (emp.emp_role == "Driver"
                                                                                && emp.emp_availability == "Available")).ToList();
                        // Check if the order status is "Pickup"
                        if (existingOrder.order_OrderTypeValue == "PickUp")
                        {
                            existingOrder.order_OrderStatus = "Accepted";
                            existingOrder.driverId = 0; // clear the driver ID

                            response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                            // Display an error message indicating that no driver will be assigned 
                            Response.Write("<script>alert ('This order will be pick up by the customer. No driver will be assigned.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                            //DisplayRewardPoints_order();
                            DisplayGcash_order();
                            gridGcash_order.Visible = true;
                            //DisplayCOD_order();
                        }
                        else
                        {
                            // Check if any available driver is available to accept the order
                            if (allDrivers.Count > 0)
                            {
                                // Assign the order to the first available driver
                                Employee driver = allDrivers[0];
                                existingOrder.driverId = driver.emp_id;
                                existingOrder.order_OrderStatus = "Accepted";
                                existingOrder.dateOrderAccepted = DateTimeOffset.UtcNow;
                                existingOrder.dateDriverAssigned = DateTimeOffset.UtcNow;
                                existingOrder.driverAssignedBy = name;

                              
                                response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                                //SEND NOTIFICATION TO CUSTOMER FOR ORDER BEING DECLINED
                                //Random rnd = new Random();
                                int ID = rnd.Next(1, 20000);
                                var Notification = new Model.Notification
                                {
                                    admin_ID = adminId,
                                    sender = "Admin",
                                    orderID = orderID,
                                    cusId = existingOrder.cusId,
                                    receiver = "Customer",
                                    title = "Order Accepted",
                                    driverId = existingOrder.driverId,
                                    body = "Your order is now accepted and is assigned to a driver! Check the order page to view the details of your order",
                                    notificationDate = DateTime.Now,
                                    status = "unread",
                                    notificationID = ID

                                };

                                SetResponse notifResponse;
                                notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                                Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                                //SEND NOTIFICATION TO DRIVER
                                int notifID = rnd.Next(1, 20000);
                                var driverNotif = new Model.Notification
                                {
                                    admin_ID = adminId,
                                    orderID = orderID,
                                    cusId = existingOrder.cusId,
                                    driverId = driver.emp_id,
                                    sender = "Admin",
                                    title = "New Assigned Order",
                                    receiver = "Driver",
                                    body = "Order ID:" + orderID + " has been assigned to you. Check the order page for the details of the order",
                                    notificationDate = DateTime.Now,
                                    status = "unread",
                                    notificationID = notifID
                                };
                                SetResponse driverNotifRes;
                                driverNotifRes = twoBigDB.Set("NOTIFICATION/" + notifID, driverNotif);//Storing data to the database
                                Notification Drivernotif = driverNotifRes.ResultAs<Notification>();//Database Result

                                Response.Write("<script>alert ('Order Accepted!'); window.location.href = '/Admin/OnlineOrders.aspx';</script>");

                                // Retrieve the existing Users log object from the database
                                FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
                                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                                // Get the current date and time
                                DateTime addedTime = DateTime.UtcNow;

                                // Log user activity
                                var log = new UsersLogs
                                {
                                    userIdnum = int.Parse(idno),
                                    logsId = idnum,
                                    //orderId = orderID,
                                    userFullname = (string)Session["fullname"],
                                    userActivity = "ACCEPTED ORDER",
                                    activityTime = addedTime
                                };

                                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                                //DisplayRewardPoints_order();
                                DisplayGcash_order();
                                gridGcash_order.Visible = true;
                                //DisplayCOD_order();
                            }
                            else
                            {
                                // No drivers are currently available, display an error message
                                Response.Write("<script>alert ('There are no available drivers to accept this order. Manually assign the driver to deliver this order.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Show error message
                Response.Write("<script>alert ('An error occurred while processing your request.');</script>" + ex.Message);
            }

        }
        //UPDATING THE STATUS ORDER FOR GCASH IF DECLINE
        protected void btnDeclineGcash_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            // int driverId = (int)Session["emp_id"];
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

            // Retrieve the quantity and unit of the declined order from the existingOrder object
            double quantity = existingOrder.order_OverallQuantities;
            //string unit = existingOrder.productUnit;
            string unit = existingOrder.order_Products[0].pro_refillUnitVolume;

            // Retrieve the tank object from the database
            FirebaseResponse tankResponse = twoBigDB.Get("TANKSUPPLY/");
            TankSupply tank = tankResponse.ResultAs<TankSupply>();

            if (unit == "L" || unit == "liter/s")
            {
                tank.tankBalance += quantity;
            }
            else if (unit == "gallon/s")
            {
                tank.tankBalance += (quantity * 3.78541); // convert gallons to liters
            }
            else if (unit == "mL" || unit == "ML" || unit == "milliliters")
            {
                tank.tankBalance += quantity;
            }

            // Update the tank object in the database
            twoBigDB.Update("TANKSUPPLY/" + tank.tankId, tank);
            // Update the order status in the existing object
            existingOrder.order_OrderStatus = "Declined";
            existingOrder.driverId = 0; // clear the driver ID

            //existingOrder.driverId = (int)driverId; // clear the driver ID

            // Update the existing order object in the database
            response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);


            //SEND NOTIFICATION TO CUSTOMER FOR ORDER BEING DECLINED
            //Random rnd = new Random();
            int ID = rnd.Next(1, 20000);
            var Notification = new Model.Notification
            {
                admin_ID = adminId,
                sender = "Admin",
                orderID = orderID,
                cusId = existingOrder.cusId,
                receiver = "Customer",
                title = "Order Declined",
                driverId = existingOrder.driverId,
                body = "Unfortunately, your order with Order ID:" + orderID + " is declined due to some technical issues.",
                notificationDate = DateTime.Now,
                status = "unread",
                notificationID = ID

            };

            SetResponse notifResponse;
            notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
            Notification notif = notifResponse.ResultAs<Notification>();//Database Result


            // Retrieve the existing Users log object from the database
            FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
            UsersLogs existingLog = resLog.ResultAs<UsersLogs>();


            // Get the current date and time
            DateTime addedTime = DateTime.UtcNow;

            // Log user activity
            var log = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = idnum,
                userFullname = (string)Session["fullname"],
                userActivity = "DECLINED ORDER",
                activityTime = addedTime
            };

            twoBigDB.Update("ADMINLOGS/" + log.logsId, log);
            //DisplayRewardPoints_order();
            DisplayGcash_order();
            gridGcash_order.Visible = true;
            //DisplayCOD_order();

        }
        //UPDATING THE STATUS ORDERS FOR RECEIVING GCASH PAYMENT
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
                    existingOrder.datePaymentReceived = DateTimeOffset.UtcNow;
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
                    DateTime addedTime = DateTime.UtcNow;

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

                    //DisplayRewardPoints_order();
                    gridGcash_order.Visible = true;
                    DisplayGcash_order();
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
                    DateTimeOffset orderedDate = existingOrder.orderDate;
                    //add two days from the orderedDate to set the scheduled date
                    DateTimeOffset schedule = orderedDate.AddDays(2);

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
                    DateTimeOffset orderedDate = existingOrder.orderDate;
                    //add two days from the orderedDate to set the scheduled date
                    DateTimeOffset schedule = orderedDate.AddDays(4);

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
                    DateTimeOffset orderedDate = existingOrder.orderDate;
                    //add two days from the orderedDate to set the scheduled date
                    DateTimeOffset schedule = orderedDate.AddDays(7);

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
                    DateTimeOffset orderedDate = existingOrder.orderDate;
                    //add two days from the orderedDate to set the scheduled date
                    DateTimeOffset schedule = orderedDate.AddDays(7);

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
        //UPDATING THE STATUS ORDER FOR REWARD POINTS IF ACCEPTED
        protected void btnAcceptPoints_Click(object sender, EventArgs e)
        {
            try
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

                if (response != null && response.ResultAs<Order>() != null)
                {
                    // Check if the order already has a driver assigned
                    if (existingOrder.driverId != 0)
                    {
                        // Get the driver object from the database
                        FirebaseResponse driverRes = twoBigDB.Get("EMPLOYEES/" + existingOrder.driverId);
                        Employee driver = driverRes.ResultAs<Employee>();


                        // Accept the order with the assigned driver
                        existingOrder.driverId = driver.emp_id;
                        existingOrder.order_OrderStatus = "Accepted";
                        existingOrder.dateOrderAccepted = DateTimeOffset.UtcNow;

                        response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                        //SEND NOTIFICATION TO CUSTOMER FOR ORDER BEING DECLINED
                        //Random rnd = new Random();
                        int ID = rnd.Next(1, 20000);
                        var Notification = new Model.Notification
                        {
                            admin_ID = adminId,
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Customer",
                            title = "Order Accepted",
                            driverId = existingOrder.driverId,
                            body = "Your order is now accepted and is assigned to a driver! Check the order page to view the details of your order",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = ID

                        };

                        SetResponse notifResponse;
                        notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                        Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                        //SEND NOTIFICATION TO DRIVER
                        int notifID = rnd.Next(1, 20000);
                        var driverNotif = new Model.Notification
                        {
                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            driverId = driver.emp_id,
                            sender = "Admin",
                            title = "New Assigned Order",
                            receiver = "Driver",
                            body = "Order ID:" + orderID + " has been assigned to you. Check the order page for the details of the order",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = notifID
                        };
                        SetResponse driverNotifRes;
                        driverNotifRes = twoBigDB.Set("NOTIFICATION/" + notifID, driverNotif);//Storing data to the database
                        Notification Drivernotif = driverNotifRes.ResultAs<Notification>();//Database Result

                        Response.Write("<script>alert ('Order Accepted!'); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                      
                        // Retrieve the existing Users log object from the database
                        FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
                        UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                        // Get the current date and time
                        DateTime addedTime = DateTime.UtcNow;

                        // Log user activity
                        var log = new UsersLogs
                        {
                            userIdnum = int.Parse(idno),
                            logsId = idnum,
                            //orderId = orderID,
                            userFullname = (string)Session["fullname"],
                            userActivity = "ACCEPTED ORDER",
                            activityTime = addedTime
                        };

                        twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                        DisplayRewardPoints_order();
                        gridRewardPoints_order.Visible = true;
                        //DisplayGcash_order();
                        //DisplayCOD_order();
                    }
                    else
                    {
                        // Get a list of all available drivers
                        FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES");
                        Dictionary<string, Employee> driverData = driverResponse.ResultAs<Dictionary<string, Employee>>();

                        List<Employee> allDrivers = driverData.Values.Where(emp => emp.adminId.ToString() == idno && (emp.emp_role == "Driver"
                                                                                && emp.emp_availability == "Available")).ToList();
                        // Check if the order status is "Pickup"
                        if (existingOrder.order_OrderTypeValue == "PickUp")
                        {
                            existingOrder.order_OrderStatus = "Accepted";
                            existingOrder.driverId = 0; // clear the driver ID

                            response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                            // Display an error message indicating that no driver will be assigned 
                            Response.Write("<script>alert ('This order will be pick up by the customer. No driver will be assigned.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                           
                            DisplayRewardPoints_order();
                            gridRewardPoints_order.Visible = true;
                            //DisplayGcash_order();
                            //DisplayCOD_order();
                        }
                        else
                        {
                            // Check if any available driver is available to accept the order
                            if (allDrivers.Count > 0)
                            {
                                // Assign the order to the first available driver
                                Employee driver = allDrivers[0];
                                existingOrder.driverId = driver.emp_id;
                                existingOrder.order_OrderStatus = "Accepted";
                                existingOrder.dateOrderAccepted = DateTimeOffset.UtcNow;
                                existingOrder.dateDriverAssigned = DateTimeOffset.UtcNow;
                                existingOrder.driverAssignedBy = name;


                                response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                                //SEND NOTIFICATION TO CUSTOMER FOR ORDER BEING DECLINED
                                //Random rnd = new Random();
                                int ID = rnd.Next(1, 20000);
                                var Notification = new Model.Notification
                                {
                                    admin_ID = adminId,
                                    sender = "Admin",
                                    orderID = orderID,
                                    cusId = existingOrder.cusId,
                                    receiver = "Customer",
                                    title = "Order Accepted",
                                    driverId = existingOrder.driverId,
                                    body = "Your order is now accepted and is assigned to a driver! Check the order page to view the details of your order",
                                    notificationDate = DateTime.Now,
                                    status = "unread",
                                    notificationID = ID

                                };

                                SetResponse notifResponse;
                                notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                                Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                                //SEND NOTIFICATION TO DRIVER
                                int notifID = rnd.Next(1, 20000);
                                var driverNotif = new Model.Notification
                                {
                                    admin_ID = adminId,
                                    orderID = orderID,
                                    cusId = existingOrder.cusId,
                                    driverId = driver.emp_id,
                                    sender = "Admin",
                                    title = "New Assigned Order",
                                    receiver = "Driver",
                                    body = "Order ID:" + orderID + " has been assigned to you. Check the order page for the details of the order",
                                    notificationDate = DateTime.Now,
                                    status = "unread",
                                    notificationID = notifID
                                };
                                SetResponse driverNotifRes;
                                driverNotifRes = twoBigDB.Set("NOTIFICATION/" + notifID, driverNotif);//Storing data to the database
                                Notification Drivernotif = driverNotifRes.ResultAs<Notification>();//Database Result

                                Response.Write("<script>alert ('Order Accepted!'); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                              
                                // Retrieve the existing Users log object from the database
                                FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
                                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                                // Get the current date and time
                                DateTime addedTime = DateTime.UtcNow;

                                // Log user activity
                                var log = new UsersLogs
                                {
                                    userIdnum = int.Parse(idno),
                                    logsId = idnum,
                                    //orderId = orderID,
                                    userFullname = (string)Session["fullname"],
                                    userActivity = "ACCEPTED ORDER",
                                    activityTime = addedTime
                                };

                                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                                gridRewardPoints_order.Visible = true;
                                DisplayRewardPoints_order();
                                //DisplayGcash_order();
                                //DisplayCOD_order();
                            }
                            else
                            {
                                // No drivers are currently available, display an error message
                                Response.Write("<script>alert ('There are no available drivers to accept this order. Wait for an available driver to deliver this order.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Show error message
                Response.Write("<script>alert ('An error occurred while processing your request.');</script>" + ex.Message);
            }

        }
        //UPDATING THE STATUS ORDER FOR REWARD POINTS IF DECLINE
        protected void btnDeclinePoints_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            // int driverId = (int)Session["emp_id"];
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

            // Retrieve the quantity and unit of the declined order from the existingOrder object
            double quantity = existingOrder.order_OverallQuantities;
            //string unit = existingOrder.productUnit;
            string unit = existingOrder.order_Products[0].pro_refillUnitVolume;

            // Retrieve the tank object from the database
            FirebaseResponse tankResponse = twoBigDB.Get("TANKSUPPLY/");
            TankSupply tank = tankResponse.ResultAs<TankSupply>();

            if (unit == "L" || unit == "liter/s")
            {
                tank.tankBalance += quantity;
            }
            else if (unit == "gallon/s")
            {
                tank.tankBalance += (quantity * 3.78541); // convert gallons to liters
            }
            else if (unit == "mL" || unit == "ML" || unit == "milliliters")
            {
                tank.tankBalance += quantity;
            }

            // Update the tank object in the database
            twoBigDB.Update("TANKSUPPLY/" + tank.tankId, tank);
            // Update the order status in the existing object
            existingOrder.order_OrderStatus = "Declined";
            existingOrder.driverId = 0; // clear the driver ID

            // Update the existing order object in the database
            response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);


            //SEND NOTIFICATION TO CUSTOMER FOR ORDER BEING DECLINED
            //Random rnd = new Random();
            int ID = rnd.Next(1, 20000);
            var Notification = new Model.Notification
            {
                admin_ID = adminId,
                sender = "Admin",
                orderID = orderID,
                cusId = existingOrder.cusId,
                receiver = "Customer",
                title = "Order Declined",
                driverId = existingOrder.driverId,
                body = "Unfortunately, your order with Order ID:" + orderID + " is declined due to some technical issues.",
                notificationDate = DateTime.Now,
                status = "unread",
                notificationID = ID

            };

            SetResponse notifResponse;
            notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
            Notification notif = notifResponse.ResultAs<Notification>();//Database Result


            // Retrieve the existing Users log object from the database
            FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
            UsersLogs existingLog = resLog.ResultAs<UsersLogs>();


            // Get the current date and time
            DateTime addedTime = DateTime.UtcNow;

            // Log user activity
            var log = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = idnum,
                userFullname = (string)Session["fullname"],
                userActivity = "DECLINED ORDER",
                activityTime = addedTime
            };

            twoBigDB.Update("ADMINLOGS/" + log.logsId, log);

            gridRewardPoints_order.Visible = true;
            DisplayRewardPoints_order();
            //DisplayGcash_order();
            //DisplayCOD_order();

        }
        //UPDATING THE STATUS ORDERS FOR REWARD POINTS IN RECEIVEING GCASH PAYMENT
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
                    existingOrder.paymentPointsReceivedBy = name;
                    existingOrder.datePaymentReceived = DateTimeOffset.UtcNow;
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
                    DateTime addedTime = DateTime.UtcNow;

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

                    DisplayRewardPoints_order();
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
                    DateTimeOffset orderedDate = existingOrder.orderDate;
                    //add two days from the orderedDate to set the scheduled date
                    DateTimeOffset schedule = orderedDate.AddDays(2);

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
                    DateTimeOffset orderedDate = existingOrder.orderDate;
                    //add two days from the orderedDate to set the scheduled date
                    DateTimeOffset schedule = orderedDate.AddDays(4);

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
                    DateTimeOffset orderedDate = existingOrder.orderDate;
                    //add two days from the orderedDate to set the scheduled date
                    DateTimeOffset schedule = orderedDate.AddDays(7);

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
                    DateTimeOffset orderedDate = existingOrder.orderDate;
                    //add two days from the orderedDate to set the scheduled date
                    DateTimeOffset schedule = orderedDate.AddDays(7);

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
        //OPTION DISPLAY ORDERS
        protected void btnViewOrders_Click(object sender, EventArgs e)
        {
            string selectedOption = drdPaymentMode.SelectedValue;

            try
            {
               
                if (selectedOption == "0")
                {
                    DisplayRewardPoints_order();
                    lblViewOrders.Text = "COD ORDER";
                    DisplayGcash_order();
                    lblViewGcashOrder.Text = "GCASH ORDER";
                    DisplayCOD_order();
                    lblViewRewardPoints.Text = "REWARD POINTS ORDER";
                    
                }
                else if (selectedOption == "1")
                {
                    lblViewOrders.Text = "COD ORDER";
                    lblViewGcashOrder.Visible = false;
                    lblViewRewardPoints.Visible = false;
                    gridCOD_order.Visible = true;
                    gridGcash_order.Visible = false;
                    gridRewardPoints_order.Visible = false;
                    DisplayCOD_order();
                    lblGcashError.Visible = false;
                    lblPointsError.Visible = false;

                }
                else if (selectedOption == "2")
                {
                    lblViewOrders.Text = "GCASH ORDER";
                    lblViewGcashOrder.Visible = false;
                    lblViewRewardPoints.Visible = false;
                    gridGcash_order.Visible = true;
                    gridCOD_order.Visible = false;
                    gridRewardPoints_order.Visible = false;
                    DisplayGcash_order();
                    lblPointsError.Visible = false;
                    lblCodError.Visible = false;

                }
                else if (selectedOption == "3")
                {
                    lblViewOrders.Text = "REWARD POINTS ORDER";
                    lblViewGcashOrder.Visible = false;
                    lblViewRewardPoints.Visible = false;
                    gridRewardPoints_order.Visible = true;
                    gridGcash_order.Visible = false;
                    gridCOD_order.Visible = false;
                    DisplayRewardPoints_order();
                    lblGcashError.Visible = false;
                    lblCodError.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/OnlineOrders.aspx';" + ex.Message);
            }
        }
        //OPTION DISPLAY DELIVERY TYPES
        protected void btnViewDeliveryType_Click(object sender, EventArgs e)
        {

            string selectedOption = drdDeliveryType.SelectedValue;
            try
            {
                if (selectedOption == "1")//cod - express
                {
                    lblViewOrders.Text = "COD / EXPRESS";
                    lblViewGcashOrder.Visible = false;
                    lblViewRewardPoints.Visible = false;
                    gridCOD_order.Visible = true;
                    gridGcash_order.Visible = false;
                    gridRewardPoints_order.Visible = false;
                    COD_express();
                    lblGcashError.Visible = false;
                    lblPointsError.Visible = false;

                }
                else if (selectedOption == "2") // cod - standard
                {
                    lblViewOrders.Text = "COD / STANDARD";
                    lblViewGcashOrder.Visible = false;
                    lblViewRewardPoints.Visible = false;
                    gridGcash_order.Visible = true;
                    gridCOD_order.Visible = false;
                    gridRewardPoints_order.Visible = false;
                    COD_standard();
                    lblPointsError.Visible = false;
                    lblCodError.Visible = false;

                }
                else if (selectedOption == "3") // cod -reservation
                {
                    lblViewOrders.Text = "COD / RESERVATION";
                    lblViewGcashOrder.Visible = false;
                    lblViewRewardPoints.Visible = false;
                    gridRewardPoints_order.Visible = true;
                    gridGcash_order.Visible = false;
                    gridCOD_order.Visible = false;
                    COD_reservation();
                    lblGcashError.Visible = false;
                    lblCodError.Visible = false;
                }
                else if (selectedOption == "1") // gcash - express
                {
                    lblViewOrders.Text = "GCASH / EXPRESS";
                    lblViewGcashOrder.Visible = false;
                    lblViewRewardPoints.Visible = false;
                    gridCOD_order.Visible = true;
                    gridGcash_order.Visible = false;
                    gridRewardPoints_order.Visible = false;
                    gcash_express();
                    lblGcashError.Visible = false;
                    lblPointsError.Visible = false;

                }
                else if (selectedOption == "2") // gcash - standard
                {
                    lblViewOrders.Text = "GCASH / STANDARD";
                    lblViewGcashOrder.Visible = false;
                    lblViewRewardPoints.Visible = false;
                    gridGcash_order.Visible = true;
                    gridCOD_order.Visible = false;
                    gridRewardPoints_order.Visible = false;
                    gcash_standard();
                    lblPointsError.Visible = false;
                    lblCodError.Visible = false;

                }
                else if (selectedOption == "3") // gcash - reservation
                {
                    lblViewOrders.Text = "GCASH / RESERVATION";
                    lblViewGcashOrder.Visible = false;
                    lblViewRewardPoints.Visible = false;
                    gridRewardPoints_order.Visible = true;
                    gridGcash_order.Visible = false;
                    gridCOD_order.Visible = false;
                    gcash_reservation();
                    lblGcashError.Visible = false;
                    lblCodError.Visible = false;
                }
                else if (selectedOption == "1") // points - express
                {
                    lblViewOrders.Text = "POINTS / EXPRESS";
                    lblViewGcashOrder.Visible = false;
                    lblViewRewardPoints.Visible = false;
                    gridCOD_order.Visible = true;
                    gridGcash_order.Visible = false;
                    gridRewardPoints_order.Visible = false;
                    points_express();
                    lblGcashError.Visible = false;
                    lblPointsError.Visible = false;

                }
                else if (selectedOption == "2") // points - standard
                {
                    lblViewOrders.Text = "POINTS / STANDARD";
                    lblViewGcashOrder.Visible = false;
                    lblViewRewardPoints.Visible = false;
                    gridGcash_order.Visible = true;
                    gridCOD_order.Visible = false;
                    gridRewardPoints_order.Visible = false;
                    points_standard();
                    lblPointsError.Visible = false;
                    lblCodError.Visible = false;

                }
                else if (selectedOption == "3") // points - reservation
                {
                    lblViewOrders.Text = "POINTS / RESERVATION";
                    lblViewGcashOrder.Visible = false;
                    lblViewRewardPoints.Visible = false;
                    gridRewardPoints_order.Visible = true;
                    gridGcash_order.Visible = false;
                    gridCOD_order.Visible = false;
                    points_reservation();
                    lblGcashError.Visible = false;
                    lblCodError.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/OnlineOrders.aspx';" + ex.Message);
            }
        }

        //DISPLAY THE ORDER FROM THE CUSTOMER THRU COD by express delivery
        private void COD_express()
        {
            string idno = (string)Session["idno"];
            //string selectedDeliveryType = drdDeliveryType.SelectedValue;

            try
            {

                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("RESERVATION DATE");
                ordersTable.Columns.Add("RESERVATION TIME");
                ordersTable.Columns.Add("RESERVATION DELIVERY TYPE SELECTED");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                ordersTable.Columns.Add("PAYMENT METHOD");
                ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE ");
                ordersTable.Columns.Add("STORE NAME");


                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery" && d.order_DeliveryTypeValue == "Express")).OrderByDescending(d => d.orderDate);

                    foreach (var order in filteredList)
                    {
                        if (order.order_OrderTypeValue == "PickUp")
                        {
                            if (order.order_Products != null)
                            {
                                string productrefill_order = "";
                                string otherproduct_order = "";
                                string productrefill_qty = "";
                                string otherproduct_qty = "";

                                foreach (var product in order.order_Products)
                                {
                                    if (product.offerType == "Product Refill")
                                    {
                                        productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        productrefill_qty += product.qtyPerItem;
                                    }
                                    else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }
                                    else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }

                                }

                                string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                   dateOrder, order.order_ReservationDate, order.order_StoreName);
                            }
                        }
                        else if (order.order_OrderTypeValue == "Delivery")
                        {
                            string productrefill_order = "";
                            string otherproduct_order = "";
                            string productrefill_qty = "";
                            string otherproduct_qty = "";

                            foreach (var product in order.order_Products)
                            {

                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    productrefill_qty += product.qtyPerItem;
                                }
                                else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }
                                else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }

                            }

                            string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                               order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                               order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                               dateOrder, order.order_StoreName);
                        }
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblCodError.Text = "No  Orders Found";
                        lblCodError.Visible = true;
                    }
                    else
                    {
                        gridCOD_order.DataSource = ordersTable;
                        gridCOD_order.DataBind();
                    }
                }
                else
                {
                    lblCodError.Text = "No Orders Found";
                    lblCodError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblCodError.Text = "There was an error retrieving orders" + ex.Message;
                lblCodError.Visible = true;
            }
        }
        //DISPLAY THE ORDER FROM THE CUSTOMER THRU COD by standard delivery
        private void COD_standard()  
        {
            string idno = (string)Session["idno"];
            //string selectedDeliveryType = drdDeliveryType.SelectedValue;

            try
            {

                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("RESERVATION DATE");
                ordersTable.Columns.Add("RESERVATION TIME");
                ordersTable.Columns.Add("RESERVATION DELIVERY TYPE SELECTED");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                ordersTable.Columns.Add("PAYMENT METHOD");
                ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE ");
                ordersTable.Columns.Add("STORE NAME");


                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery" && d.order_DeliveryTypeValue == "Standard")).OrderByDescending(d => d.orderDate);

                    foreach (var order in filteredList)
                    {
                        if (order.order_OrderTypeValue == "PickUp")
                        {
                            if (order.order_Products != null)
                            {
                                string productrefill_order = "";
                                string otherproduct_order = "";
                                string productrefill_qty = "";
                                string otherproduct_qty = "";

                                foreach (var product in order.order_Products)
                                {

                                    if (product.offerType == "Product Refill")
                                    {
                                        productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        productrefill_qty += product.qtyPerItem;
                                    }
                                    else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }
                                    else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }

                                }

                                string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                   dateOrder, order.order_ReservationDate, order.order_StoreName);
                            }
                        }
                        else if (order.order_OrderTypeValue == "Delivery")
                        {
                            string productrefill_order = "";
                            string otherproduct_order = "";
                            string productrefill_qty = "";
                            string otherproduct_qty = "";

                            foreach (var product in order.order_Products)
                            {
                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    productrefill_qty += product.qtyPerItem;
                                }
                                else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }
                                else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }

                            }

                            string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                               order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                               order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                               dateOrder, order.order_StoreName);
                        }
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblCodError.Text = "No  Orders Found";
                        lblCodError.Visible = true;
                    }
                    else
                    {
                        gridCOD_order.DataSource = ordersTable;
                        gridCOD_order.DataBind();
                    }
                }
                else
                {
                    lblCodError.Text = "No Orders Found";
                    lblCodError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblCodError.Text = "There was an error retrieving orders" + ex.Message;
                lblCodError.Visible = true;
            }
        }
        //DISPLAY THE ORDER FROM THE CUSTOMER THRU COD by reservation
        private void COD_reservation()
        {
            string idno = (string)Session["idno"];
            //string selectedDeliveryType = drdDeliveryType.SelectedValue;

            try
            {

                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("RESERVATION DATE");
                ordersTable.Columns.Add("RESERVATION TIME");
                ordersTable.Columns.Add("RESERVATION DELIVERY TYPE SELECTED");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                ordersTable.Columns.Add("PAYMENT METHOD");
                ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE ");
                ordersTable.Columns.Add("STORE NAME");


                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery" && d.order_DeliveryTypeValue == "Reservation")).OrderByDescending(d => d.orderDate);

                    foreach (var order in filteredList)
                    {
                        if (order.order_OrderTypeValue == "PickUp")
                        {
                            if (order.order_Products != null)
                            {
                                string productrefill_order = "";
                                string otherproduct_order = "";
                                string productrefill_qty = "";
                                string otherproduct_qty = "";

                                foreach (var product in order.order_Products)
                                {

                                    if (product.offerType == "Product Refill")
                                    {
                                        productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        productrefill_qty += product.qtyPerItem;
                                    }
                                    else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }
                                    else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }

                                }

                                string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                   dateOrder, order.order_ReservationDate, order.order_StoreName);
                            }
                        }
                        else if (order.order_OrderTypeValue == "Delivery")
                        {
                            string productrefill_order = "";
                            string otherproduct_order = "";
                            string productrefill_qty = "";
                            string otherproduct_qty = "";

                            foreach (var product in order.order_Products)
                            {
                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    productrefill_qty += product.qtyPerItem;
                                }
                                else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }
                                else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }

                            }

                            string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                               order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                               order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                               dateOrder, order.order_StoreName);
                        }
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblCodError.Text = "No  Orders Found";
                        lblCodError.Visible = true;
                    }
                    else
                    {
                        gridCOD_order.DataSource = ordersTable;
                        gridCOD_order.DataBind();
                    }
                }
                else
                {
                    lblCodError.Text = "No Orders Found";
                    lblCodError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblCodError.Text = "There was an error retrieving orders" + ex.Message;
                lblCodError.Visible = true;
            }
        }
        //DISPLAY THE ORDER FROM THE CUSTOMER THRU GCASH by express
        private void gcash_express()
        {
            string idno = (string)Session["idno"];
            try
            {
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("RESERVATION DATE");
                ordersTable.Columns.Add("RESERVATION TIME");
                ordersTable.Columns.Add("RESERVATION DELIVERY TYPE SELECTED");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                ordersTable.Columns.Add("PAYMENT METHOD");
                ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE ");
                ordersTable.Columns.Add("STORE NAME");


                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Gcash" && d.order_DeliveryTypeValue == "Express")).OrderByDescending(d => d.orderDate);

                    foreach (var order in filteredList)
                    {
                        if (order.order_OrderTypeValue == "PickUp")
                        {
                            if (order.order_Products != null)
                            {
                                string productrefill_order = "";
                                string otherproduct_order = "";
                                string productrefill_qty = "";
                                string otherproduct_qty = "";

                                foreach (var product in order.order_Products)
                                {

                                    if (product.offerType == "Product Refill")
                                    {
                                        productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        productrefill_qty += product.qtyPerItem;
                                    }
                                    else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }
                                    else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }

                                }

                                string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                   dateOrder, order.order_ReservationDate, order.order_StoreName);
                            }
                        }
                        else if (order.order_OrderTypeValue == "Delivery")
                        {
                            string productrefill_order = "";
                            string otherproduct_order = "";
                            string productrefill_qty = "";
                            string otherproduct_qty = "";

                            foreach (var product in order.order_Products)
                            {
                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    productrefill_qty += product.qtyPerItem;
                                }
                                else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }
                                else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }

                            }

                            string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                               order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                               order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                               dateOrder, order.order_StoreName);
                        }
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblGcashError.Text = "No  Orders Found";
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
                    lblGcashError.Text = "No Orders Found";
                    lblGcashError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblGcashError.Text = "There was an error retrieving orders" + ex.Message;
                lblGcashError.Visible = true;
            }
        }
        //DISPLAY THE ORDER FROM THE CUSTOMER THRU GCASH by standard
        private void gcash_standard()
        {
            string idno = (string)Session["idno"];
            try
            {
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("RESERVATION DATE");
                ordersTable.Columns.Add("RESERVATION TIME");
                ordersTable.Columns.Add("RESERVATION DELIVERY TYPE SELECTED");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                ordersTable.Columns.Add("PAYMENT METHOD");
                ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE ");
                ordersTable.Columns.Add("STORE NAME");


                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Gcash" && d.order_DeliveryTypeValue == "Standard")).OrderByDescending(d => d.orderDate);

                    foreach (var order in filteredList)
                    {
                        if (order.order_OrderTypeValue == "PickUp")
                        {
                            if (order.order_Products != null)
                            {
                                string productrefill_order = "";
                                string otherproduct_order = "";
                                string productrefill_qty = "";
                                string otherproduct_qty = "";

                                foreach (var product in order.order_Products)
                                {
                                    if (product.offerType == "Product Refill")
                                    {
                                        productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        productrefill_qty += product.qtyPerItem;
                                    }
                                    else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }
                                    else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }

                                }

                                string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                   dateOrder, order.order_ReservationDate, order.order_StoreName);
                            }
                        }
                        else if (order.order_OrderTypeValue == "Delivery")
                        {
                            string productrefill_order = "";
                            string otherproduct_order = "";
                            string productrefill_qty = "";
                            string otherproduct_qty = "";

                            foreach (var product in order.order_Products)
                            {
                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    productrefill_qty += product.qtyPerItem;
                                }
                                else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }
                                else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }

                            }

                            string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                               order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                               order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                               dateOrder, order.order_StoreName);
                        }
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblGcashError.Text = "No  Orders Found";
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
                    lblGcashError.Text = "No Orders Found";
                    lblGcashError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblGcashError.Text = "There was an error retrieving orders" + ex.Message;
                lblGcashError.Visible = true;
            }
        }
        //DISPLAY THE ORDER FROM THE CUSTOMER THRU GCASH by reservation
        private void gcash_reservation()
        {
            string idno = (string)Session["idno"];
            try
            {
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("RESERVATION DATE");
                ordersTable.Columns.Add("RESERVATION TIME");
                ordersTable.Columns.Add("RESERVATION DELIVERY TYPE SELECTED");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                ordersTable.Columns.Add("PAYMENT METHOD");
                ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE ");
                ordersTable.Columns.Add("STORE NAME");


                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Gcash" && d.order_DeliveryTypeValue == "Reservation")).OrderByDescending(d => d.orderDate);

                    foreach (var order in filteredList)
                    {
                        if (order.order_OrderTypeValue == "PickUp")
                        {
                            if (order.order_Products != null)
                            {
                                string productrefill_order = "";
                                string otherproduct_order = "";
                                string productrefill_qty = "";
                                string otherproduct_qty = "";

                                foreach (var product in order.order_Products)
                                {
                                    if (product.offerType == "Product Refill")
                                    {
                                        productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        productrefill_qty += product.qtyPerItem;
                                    }
                                    else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }
                                    else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }

                                }

                                string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                   dateOrder, order.order_ReservationDate, order.order_StoreName);
                            }
                        }
                        else if (order.order_OrderTypeValue == "Delivery")
                        {
                            string productrefill_order = "";
                            string otherproduct_order = "";
                            string productrefill_qty = "";
                            string otherproduct_qty = "";

                            foreach (var product in order.order_Products)
                            {
                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    productrefill_qty += product.qtyPerItem;
                                }
                                else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }
                                else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }

                            }

                            string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                               order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                               order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                               dateOrder, order.order_StoreName);
                        }
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblGcashError.Text = "No  Orders Found";
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
                    lblGcashError.Text = "No Orders Found";
                    lblGcashError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblGcashError.Text = "There was an error retrieving orders" + ex.Message;
                lblGcashError.Visible = true;
            }
        }
        //DISPLAY THE ORDER FROM THE CUSTOMER THRU REWARD POINTS by express
        private void points_express()
        {
            string idno = (string)Session["idno"];

            try
            {
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("RESERVATION DATE");
                ordersTable.Columns.Add("RESERVATION TIME");
                ordersTable.Columns.Add("RESERVATION DELIVERY TYPE SELECTED");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                ordersTable.Columns.Add("PAYMENT METHOD");
                ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE ");
                ordersTable.Columns.Add("STORE NAME");


                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Points" && d.order_DeliveryTypeValue == "Express")).OrderByDescending(d => d.orderDate);

                    foreach (var order in filteredList)
                    {
                        if (order.order_OrderTypeValue == "PickUp")
                        {
                            if (order.order_Products != null)
                            {
                                string productrefill_order = "";
                                string otherproduct_order = "";
                                string productrefill_qty = "";
                                string otherproduct_qty = "";

                                foreach (var product in order.order_Products)
                                {
                                    if (product.offerType == "Product Refill")
                                    {
                                        productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        productrefill_qty += product.qtyPerItem;
                                    }
                                    else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }
                                    else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }

                                }

                                string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                   dateOrder, order.order_ReservationDate, order.order_StoreName);
                            }
                        }
                        else if (order.order_OrderTypeValue == "Delivery")
                        {
                            string productrefill_order = "";
                            string otherproduct_order = "";
                            string productrefill_qty = "";
                            string otherproduct_qty = "";

                            foreach (var product in order.order_Products)
                            {

                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    productrefill_qty += product.qtyPerItem;
                                }
                                else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }
                                else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }

                            }

                            string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                               order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                               order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                               dateOrder, order.order_StoreName);
                        }
                    }
                    if (ordersTable.Rows.Count == 0)
                    {
                        lblPointsError.Text = "No  Orders Found";
                        lblPointsError.Visible = true;
                    }
                    else
                    {
                        gridRewardPoints_order.DataSource = ordersTable;
                        gridRewardPoints_order.DataBind();
                    }
                }
                else
                {
                    lblPointsError.Text = "No Orders Found";
                    lblPointsError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblPointsError.Text = "There was an error retrieving orders" + ex.Message;
                lblPointsError.Visible = true;
            }
        }
        //DISPLAY THE ORDER FROM THE CUSTOMER THRU REWARD POINTS by standard
        private void points_standard()
        {
            string idno = (string)Session["idno"];

            try
            {
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("RESERVATION DATE");
                ordersTable.Columns.Add("RESERVATION TIME");
                ordersTable.Columns.Add("RESERVATION DELIVERY TYPE SELECTED");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                ordersTable.Columns.Add("PAYMENT METHOD");
                ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE ");
                ordersTable.Columns.Add("STORE NAME");


                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Points" && d.order_DeliveryTypeValue == "Standard")).OrderByDescending(d => d.orderDate);

                    foreach (var order in filteredList)
                    {
                        if (order.order_OrderTypeValue == "PickUp")
                        {
                            if (order.order_Products != null)
                            {
                                string productrefill_order = "";
                                string otherproduct_order = "";
                                string productrefill_qty = "";
                                string otherproduct_qty = "";

                                foreach (var product in order.order_Products)
                                {
                                    if (product.offerType == "Product Refill")
                                    {
                                        productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        productrefill_qty += product.qtyPerItem;
                                    }
                                    else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }
                                    else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }

                                }

                                string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                   dateOrder, order.order_ReservationDate, order.order_StoreName);
                            }
                        }
                        else if (order.order_OrderTypeValue == "Delivery")
                        {
                            string productrefill_order = "";
                            string otherproduct_order = "";
                            string productrefill_qty = "";
                            string otherproduct_qty = "";

                            foreach (var product in order.order_Products)
                            {

                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    productrefill_qty += product.qtyPerItem;
                                }
                                else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }
                                else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }
                            }

                            string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                               order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                               order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                               dateOrder, order.order_StoreName);
                        }
                    }
                    if (ordersTable.Rows.Count == 0)
                    {
                        lblPointsError.Text = "No  Orders Found";
                        lblPointsError.Visible = true;
                    }
                    else
                    {
                        gridRewardPoints_order.DataSource = ordersTable;
                        gridRewardPoints_order.DataBind();
                    }
                }
                else
                {
                    lblPointsError.Text = "No Orders Found";
                    lblPointsError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblPointsError.Text = "There was an error retrieving orders" + ex.Message;
                lblPointsError.Visible = true;
            }
        }
        //DISPLAY THE ORDER FROM THE CUSTOMER THRU REWARD POINTS by reservation
        private void points_reservation()
        {
            string idno = (string)Session["idno"];

            try
            {
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("RESERVATION DATE");
                ordersTable.Columns.Add("RESERVATION TIME");
                ordersTable.Columns.Add("DELIVERY TYPE SELECTED");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                ordersTable.Columns.Add("PAYMENT METHOD");
                ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE ");
                ordersTable.Columns.Add("STORE NAME");


                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Points" && d.order_DeliveryTypeValue == "Reservation")).OrderByDescending(d => d.orderDate);

                    foreach (var order in filteredList)
                    {
                        if (order.order_OrderTypeValue == "PickUp")
                        {
                            if (order.order_Products != null)
                            {
                                string productrefill_order = "";
                                string otherproduct_order = "";
                                string productrefill_qty = "";
                                string otherproduct_qty = "";

                                foreach (var product in order.order_Products)
                                {
                                    if (product.offerType == "Product Refill")
                                    {
                                        productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        productrefill_qty += product.qtyPerItem;
                                    }
                                    else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }
                                    else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                    {
                                        otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                        otherproduct_qty += product.qtyPerItem + " " + " ";
                                    }

                                }

                                string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                   dateOrder, order.order_ReservationDate, order.order_StoreName);
                            }
                        }
                        else if (order.order_OrderTypeValue == "Delivery")
                        {
                            string productrefill_order = "";
                            string otherproduct_order = "";
                            string productrefill_qty = "";
                            string otherproduct_qty = "";

                            foreach (var product in order.order_Products)
                            {
                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    productrefill_qty += product.qtyPerItem;
                                }
                                else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }
                                else if (product.offerType == "thirdparty product") // corrected spelling of "thirdparty Product"
                                {
                                    otherproduct_order += product.pro_refillQty + " " + product.pro_refillUnitVolume + " " + product.order_ProductName + " " + " ";
                                    otherproduct_qty += product.qtyPerItem + " " + " ";
                                }

                            }

                            string dateOrder = order.orderDate == DateTimeOffset.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                               order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                               order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                               dateOrder, order.order_StoreName);
                        }
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        lblPointsError.Text = "No  Orders Found";
                        lblPointsError.Visible = true;
                    }
                    else
                    {
                        gridRewardPoints_order.DataSource = ordersTable;
                        gridRewardPoints_order.DataBind();
                    }
                }
                else
                {
                    lblPointsError.Text = "No Orders Found";
                    lblPointsError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblPointsError.Text = "There was an error retrieving orders" + ex.Message;
                lblPointsError.Visible = true;
            }
        }    
    }
}