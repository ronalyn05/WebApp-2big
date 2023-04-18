﻿using System;
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

        // Define the method to send a notification to FCM
        //protected async Task SendNotification(NotificationMessage message, string[] registrationIds)
        //{
        //    var payload = new
        //    {
        //        notification = message,
        //        registration_ids = registrationIds
        //    };

        //    var httpClient = new HttpClient();
        //    var jsonPayload = JsonConvert.SerializeObject(payload);
        //    var request = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
        //    request.Headers.TryAddWithoutValidation("Authorization", "Bearer AAAAm5wKMi4:APA91bH5jN5pxh7S-BveV_f9rBbhu-pH34M9fTS8BnZB-OEcUTMjutFbhTkyE-UpmUL9OHpGZJ2c6AJJBl87ayegVH22OQibAsXr_oH-CN3FcVMOmStGXN5EBp0X9sJduTSVkuB2qrw1");
        //    request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        //    var response = await httpClient.SendAsync(request);
        //    var responseContent = await response.Content.ReadAsStringAsync();
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new FirebaseException(responseContent);
        //    }

        //    var result = JsonConvert.DeserializeObject<object>(responseContent);
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            //connection to database 
            twoBigDB = new FireSharp.FirebaseClient(config);
            //METHODS TO DISPLAY THE IDs
            if (!IsPostBack)
            {
                //DisplayID();
                DisplayTable();
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
            Order order = response.ResultAs<Order>();

            Session["orderID"] = order.orderID;
        }
        private void DisplayTable()
        {
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();
            var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno);
            //  FirebaseResponse response = twoBigDB.Get("ORDERS/");
            //  Order obj = response.ResultAs<Order>();
            //  var json = response.Body;
            ////  Dictionary<string, Order> orders = JsonConvert.DeserializeObject<Dictionary<string, Order>>(json);
            //  Dictionary<string, Order> orders = response.ResultAs<Dictionary<string, Order>>();

            // Create the DataTable to hold the orders
            DataTable ordersTable = new DataTable();
            ordersTable.Columns.Add("ORDER ID");
            ordersTable.Columns.Add("CUSTOMER ID");
            ordersTable.Columns.Add("DRIVER ID");
            ordersTable.Columns.Add("STORE NAME");
            ordersTable.Columns.Add("PRODUCT NAME");
            ordersTable.Columns.Add("PRODUCT UNIT");
            ordersTable.Columns.Add("PRODUCT SIZE");
            ordersTable.Columns.Add("DELIVERY TYPE");
            ordersTable.Columns.Add("ORDER TYPE");
            ordersTable.Columns.Add("ORDER METHOD");
            ordersTable.Columns.Add("GALLON SWAP OPTION");
            ordersTable.Columns.Add("PRICE");
            ordersTable.Columns.Add("QUANTITY");
            ordersTable.Columns.Add("RESERVATION DATE");
            ordersTable.Columns.Add("STATUS");
            ordersTable.Columns.Add("TOTAL AMOUNT");

            if (response != null && response.ResultAs<Order>() != null)
            {
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_StoreName, entry.order_ProductName,
                                          entry.order_unit, entry.order_size, entry.order_DeliveryTypeValue,
                                          entry.order_OrderTypeValue, entry.order_OrderMethod, entry.order_choosenSwapOption, entry.order_WaterPrice,
                                          entry.order_Quantity, entry.order_ReservationDate,
                                          entry.order_OrderStatus, entry.order_TotalAmount);
                }
            }
            else
            {
                // Handle null response or invalid selected value
                ordersTable.Rows.Add("No orders found", "", "", "", "", "", "", "", "", "", "", "");
            }

            // Bind the DataTable to the GridView
            GridView1.DataSource = ordersTable;
            GridView1.DataBind();
        }

        //UPDATING THE ORDERS
        // trigger the appropriate notifications based on the customer's order status
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            // Get the log ID from the session 
            int logsId = (int)Session["logsId"];

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
            // Check if the order status is "Pickup"
            if (existingOrder.order_OrderTypeValue == "pickup")
            {
                existingOrder.order_OrderStatus = "Accepted";
                existingOrder.driverId = 0; // clear the driver ID
                response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                // Notify the customer that their order has been accepted
                //var customerRegistrationIds = new string[] { existingOrder.cusId.ToString() };
                //var message = new NotificationMessage
                //{
                //    title = "Your order has been accepted",
                //    body = "Your order has been accepted and is ready to pick up.",
                //    data = new { orderId = orderID }
                //};
                //  SendNotification(message, customerRegistrationIds);

                // Display an error message indicating that no driver will be assigned 
                Response.Write("<script>alert ('This order will be pick up by the customer. No driver will be assigned.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                DisplayTable();
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

                    // Notify the customer that their order has been accepted
                    //var customerRegistrationIds = new string[] { existingOrder.cusId.ToString() };
                    //var message = new NotificationMessage
                    //{
                    //    title = "Your order has been accepted",
                    //    body = "Your order has been accepted and will be delivered afterwards.",
                    //    data = new { orderId = orderID }
                    //};
                    // SendNotification(message, customerRegistrationIds);

                    //// Set notification based on amount of gallons ordered
                    //int gallonsOrdered = existingOrder.order_Quantity;
                    //int notificationDelay = 0; // in seconds
                    //switch (gallonsOrdered)
                    //{
                    //    case 1:
                    //        notificationDelay = 2 * 24 * 60 * 60; // 2 days
                    //        break;
                    //    case 2:
                    //        notificationDelay = 4 * 24 * 60 * 60; // 4 days
                    //        break;
                    //    default:
                    //        // Default delay is 1 day
                    //        notificationDelay = 1 * 24 * 60 * 60; // 1 day
                    //        break;
                    //}
                    //DateTimeOffset notificationTime = DateTimeOffset.UtcNow.AddSeconds(notificationDelay);

                    response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                    // Retrieve the existing Users log object from the database
                    FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
                    UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                    // Get the current date and time
                    //DateTime addedTime = DateTime.UtcNow;

                    // Log user activity
                    var log = new UsersLogs
                    {
                        userIdnum = int.Parse(idno),
                        logsId = logsId,
                        orderId = orderID,
                        userFullname = (string)Session["fullname"],
                        emp_id = existingLog.emp_id,
                        empFullname = existingLog.empFullname,
                        empDateAdded = existingLog.empDateAdded,
                        dateLogin = existingLog.dateLogin,
                        deliveryDetailsId = existingLog.deliveryDetailsId,
                        standardAdded = existingLog.standardAdded,
                        reservationAdded = existingLog.reservationAdded,
                        expressAdded = existingLog.expressAdded,
                        productRefillId = existingLog.productRefillId,
                        productrefillDateAdded = existingLog.productrefillDateAdded,
                        other_productId = existingLog.other_productId,
                        otherProductDateAdded = existingLog.otherProductDateAdded,
                        tankId = existingLog.tankId,
                        cusId = existingLog.cusId,
                        tankSupplyDateAdded = existingLog.tankSupplyDateAdded,
                        datePaymentReceived = existingOrder.datePaymentReceived,
                        dateOrderAccepted = existingOrder.dateOrderAccepted,
                        userActivity = "Accepted Order"
                    };

                    twoBigDB.Update("USERSLOG/" + log.logsId, log);
                    DisplayTable();
                }
                else
                {
                    // No drivers are currently available, display an error message
                    Response.Write("<script>alert ('There are no available drivers to accept this order. Wait for an available driver to deliver this order.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                }
            }
        }

        //UPDATING THE STATUS ORDER ID DECLINE
        protected void btnDecline_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            // int driverId = (int)Session["emp_id"];
            // Get the log ID from the session 
            int logsId = (int)Session["logsId"];

            // Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the order ID from the first cell in the row
            int orderID = int.Parse(row.Cells[2].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            // Update the order status in the existing object
            existingOrder.order_OrderStatus = "Declined";
            existingOrder.driverId = 0; // clear the driver ID

            //existingOrder.driverId = (int)driverId; // clear the driver ID

            // Update the existing order object in the database
            response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

            // Retrieve the existing Users log object from the database
            FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
            UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

            // Get the current date and time
            //DateTime addedTime = DateTime.UtcNow;

            // Log user activity
            var log = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = logsId,
                orderId = orderID,
                userFullname = (string)Session["fullname"],
                emp_id = existingLog.emp_id,
                empFullname = existingLog.empFullname,
                empDateAdded = existingLog.empDateAdded,
                dateLogin = existingLog.dateLogin,
                deliveryDetailsId = existingLog.deliveryDetailsId,
                standardAdded = existingLog.standardAdded,
                reservationAdded = existingLog.reservationAdded,
                expressAdded = existingLog.expressAdded,
                productRefillId = existingLog.productRefillId,
                productrefillDateAdded = existingLog.productrefillDateAdded,
                other_productId = existingLog.other_productId,
                otherProductDateAdded = existingLog.otherProductDateAdded,
                tankId = existingLog.tankId,
                cusId = existingLog.cusId,
                tankSupplyDateAdded = existingLog.tankSupplyDateAdded,
                datePaymentReceived = existingOrder.datePaymentReceived,
                dateOrderAccepted = existingOrder.dateOrderAccepted,
                userActivity = "Declined Order",
                dateDeclined = DateTimeOffset.UtcNow
            };

            twoBigDB.Update("USERSLOG/" + log.logsId, log);
            DisplayTable();
        
        }
        //UPDATING THE ORDERS
        protected void btnPaymentAccept_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            // Get the log ID from the session
            int logsId = (int)Session["logsId"];

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

                    existingOrder.datePaymentReceived = DateTimeOffset.UtcNow;
                    //existingOrder.datePaymentReceived = DateTimeOffset.Parse("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                    // Display an error message indicating that no driver will be assigned
                    Response.Write("<script>alert ('Payment successfully received.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");

                    // Retrieve the existing Users log object from the database
                    FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
                    UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                    // Get the current date and time
                    //DateTime addedTime = DateTime.UtcNow;

                    // Log user activity
                    var log = new UsersLogs
                    {
                        userIdnum = int.Parse(idno),
                        logsId = logsId,
                        orderId = orderID,
                        userFullname = (string)Session["fullname"],
                        emp_id = existingLog.emp_id,
                        empFullname = existingLog.empFullname,
                        empDateAdded = existingLog.empDateAdded,
                        dateLogin = existingLog.dateLogin,
                        deliveryDetailsId = existingLog.deliveryDetailsId,
                        standardAdded = existingLog.standardAdded,
                        reservationAdded = existingLog.reservationAdded,
                        expressAdded = existingLog.expressAdded,
                        productRefillId = existingLog.productRefillId,
                        productrefillDateAdded = existingLog.productrefillDateAdded,
                        other_productId = existingLog.other_productId,
                        otherProductDateAdded = existingLog.otherProductDateAdded,
                        cusId = existingLog.cusId,
                        tankId = existingLog.tankId,
                        tankSupplyDateAdded = existingLog.tankSupplyDateAdded,
                        dateOrderAccepted = existingOrder.dateOrderAccepted,
                        datePaymentReceived = existingOrder.datePaymentReceived,
                        userActivity = "Received Payment"
                    };

                    twoBigDB.Update("USERSLOG/" + log.logsId, log);

                    DisplayTable();
                }
            }
            else
            {
                // Display an error messaged
                Response.Write("<script>alert ('Make sure order is successfully delivered.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
            }


        }
    }
}