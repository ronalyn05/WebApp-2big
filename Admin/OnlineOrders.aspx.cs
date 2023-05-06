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
        //DISPLAY THE ORDER FROM THE CUSTOMER
        private void DisplayTable()
        {
            string idno = (string)Session["idno"];

            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();
            var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno);

            DataTable ordersTable = new DataTable();
            ordersTable.Columns.Add("ORDER ID");
            ordersTable.Columns.Add("CUSTOMER ID");
            ordersTable.Columns.Add("DRIVER ID");
            ordersTable.Columns.Add("STORE NAME");
            ordersTable.Columns.Add("PRODUCT REFILL ORDER");
            ordersTable.Columns.Add("OTHER PRODUCT ORDER ");
            ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
            ordersTable.Columns.Add("OTHER PRODUCT  QUANTITY");
          //  ordersTable.Columns.Add("OVERALL ORDER QUANTITY");
            ordersTable.Columns.Add("DELIVERY TYPE");
            ordersTable.Columns.Add("ORDER TYPE");
            ordersTable.Columns.Add("PAYMENT METHOD");
            ordersTable.Columns.Add("REFILL SELECTED OPTION");
            ordersTable.Columns.Add("RESERVATION DATE");
            ordersTable.Columns.Add("STATUS");
            ordersTable.Columns.Add("TOTAL AMOUNT");
            

            if (response != null && response.ResultAs<Order>() != null)
            {
                foreach (var order in filteredList)
                {
                    if (order.order_Products != null)
                    {
                        string productrefill_order = "";
                        string otherproduct_order = "";
                        string productrefill_qty = "";
                        string otherproduct_qty = "";

                        foreach (var product in order.order_Products)
                        {
                            //if(product.offerType == "Product Refill")
                            //{
                            //    productrefill_order += product.order_size + " " + product.order_unit + " " + product.order_ProductName + " ";
                            //    productrefill_qty = product.qtyPerItem;
                            //}
                            //if (product.offerType == "other Product")
                            //{
                            //    otherproduct_order += product.order_size + " " + product.order_unit + " " + product.order_ProductName + " ";
                            //    otherproduct_qty = product.qtyPerItem;
                            //}
                           
                                if (product.offerType == "Product Refill")
                                {
                                    productrefill_order += product.order_size + " " + product.order_unit + " " + product.order_ProductName + " ";
                                    productrefill_qty = product.qtyPerItem;
                                }
                                else if (product.offerType == "other Product") // corrected spelling of "Other Product"
                                {
                                    otherproduct_order += product.order_size + " " + product.order_unit + " " + product.order_ProductName + " ";
                                    otherproduct_qty = product.qtyPerItem;
                                }
                           
                        }

                        ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_StoreName, productrefill_order, otherproduct_order,
                            productrefill_qty, otherproduct_qty, order.order_DeliveryTypeValue, order.order_OrderTypeValue, order.orderPaymentMethod,
                            order.order_RefillSelectedOption, order.order_ReservationDate, order.order_OrderStatus, order.order_TotalAmount);
                    }
                }
            }
            else
            {
                ordersTable.Rows.Add("No orders found", "", "", "", "", "", "", "", "", "", "", "");
            }

            GridView1.DataSource = ordersTable;
            GridView1.DataBind();
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
                twoBigDB.Set("USERSLOG/" + log.logsId, log);

                DisplayTable();
            }
            catch (Exception ex)
            {
                // Show error message
                Response.Write("<script>alert ('An error occurred while processing your request.');</script>" + ex.Message);
            }
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    //  generate a random number for employee logged
        //    Random rnd = new Random();
        //    int idnum = rnd.Next(1, 10000);

        //    // Get the admin ID from the session
        //    string idno = (string)Session["idno"];
        //    string name = (string)Session["fullname"];
        //    //  int adminId = int.Parse(idno);
        //    try
        //    {
        //        string orderId = txtOrderId.Text.Trim();
        //        string driver_Id = txtDriverId.Text.Trim();

        //        FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderId);
        //        Order existingOrder = response.ResultAs<Order>();

        //        if (existingOrder == null)
        //        {
        //            // Show error message if the empID entered is invalid
        //            Response.Write("<script>alert ('Invalid order ID!');</script>");
        //            return;
        //        }
        //        // Check if the employee ID is valid
        //        if (string.IsNullOrEmpty(orderId))
        //        {
        //            Response.Write("<script>alert ('Please enter a valid order ID!');</script>");
        //            return;
        //        }
        //        else if(string.IsNullOrEmpty(driver_Id))
        //        {
        //            Response.Write("<script>alert ('Please enter a valid driver ID!');</script>");
        //            return;
        //        }

        //        // Create a new order object with the updated data
        //        Order updatedOrder = new Order
        //        {
        //            admin_ID = existingOrder.admin_ID,
        //            emp_id = existingOrder.emp_id,
        //            emp_firstname = existingOrder.emp_firstname,
        //            emp_midname = existingOrder.emp_midname,
        //            emp_lastname = existingOrder.emp_lastname,
        //            emp_gender = existingOrder.emp_gender,
        //            emp_email = existingOrder.emp_email,
        //            emp_birthdate = existingOrder.emp_birthdate,
        //            emp_contactnum = existingOrder.emp_contactnum,
        //            emp_emergencycontact = existingOrder.emp_emergencycontact,
        //            emp_dateHired = existingOrder.emp_dateHired,
        //            dateAdded = existingOrder.dateAdded,
        //            emp_address = existingOrder.emp_address,
        //            emp_pass = existingOrder.emp_pass,
        //            emp_role = existingOrder.emp_role,
        //            emp_status = existingOrder.emp_status,
        //            addedBy = existingOrder.addedBy,
        //            emp_availability = existingOrder.emp_availability
        //        };

        //        // Update the fields that have changed
        //        if (!string.IsNullOrEmpty(newPosition) && newPosition != existingEmp.emp_role || !string.IsNullOrEmpty(contactnumber) && contactnumber != existingEmp.emp_contactnum || !string.IsNullOrEmpty(email) && email != existingEmp.emp_email)
        //        {
        //            updatedEmp.emp_email = email;
        //            updatedEmp.emp_contactnum = contactnumber;
        //            updatedEmp.emp_role = newPosition;
        //            updatedEmp.dateUpdated = DateTimeOffset.UtcNow;
        //            updatedEmp.updatedBy = name;
        //        }
        //        //if emp status is already inactive
        //        if (existingEmp.emp_status == "Inactive")
        //        {
        //            Response.Write("<script>alert ('Error: This employee is inactive and cannot be updated.'); location.reload(); window.location.href = '/Admin/EmployeeRecord.aspx';</script>");

        //            return;
        //        }

        //        if (!string.IsNullOrEmpty(newStatus) && newStatus != existingEmp.emp_status)
        //        {
        //            updatedEmp.emp_status = newStatus;
        //            updatedEmp.statusDateModified = DateTimeOffset.UtcNow;
        //            updatedEmp.status_ModifiedBy = name;
        //        }


        //        // Update the existing employee object in the database
        //        response = twoBigDB.Update("ORDERS/" + orderId, existingOrder);

        //        // Show success message
        //        Response.Write("<script>alert('You have successfully assigned driver " + " "  + driver_Id + " to the order number " + " " + orderId + "'); location.reload(); window.location.href = '/Admin/EmployeeRecord.aspx';</script>");

        //        // Get the current date and time
        //        DateTime addedTime = DateTime.UtcNow;

        //        // Log user activity
        //        var log = new UsersLogs
        //        {
        //            userIdnum = int.Parse(idno),
        //            logsId = idnum,
        //            userFullname = (string)Session["fullname"],
        //            activityTime = addedTime,
        //            userActivity = "ASSIGNED THE DRIVER TO DELIVER THE ORDER",
        //            // userActivity = UserActivityType.UpdatedEmployeeRecords
        //        };
        //        twoBigDB.Set("USERSLOG/" + log.logsId, log);

        //        DisplayTable();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<pre>" + ex.ToString() + "</pre>");

        //    }
        //}

        //private void DisplayTable()
        //{
        //    string idno = (string)Session["idno"];
        //    // int adminId = int.Parse(idno);

        //    // Retrieve all orders from the ORDERS table
        //    FirebaseResponse response = twoBigDB.Get("ORDERS");
        //    Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();
        //    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno);

        //    // Create the DataTable to hold the orders
        //    DataTable ordersTable = new DataTable();
        //    ordersTable.Columns.Add("ORDER ID");
        //    ordersTable.Columns.Add("CUSTOMER ID");
        //    ordersTable.Columns.Add("DRIVER ID");
        //    ordersTable.Columns.Add("STORE NAME");
        //    ordersTable.Columns.Add("PRODUCT ORDER");
        //    ordersTable.Columns.Add("DELIVERY TYPE");
        //    ordersTable.Columns.Add("ORDER TYPE");
        //    ordersTable.Columns.Add("PAYMENT METHOD");
        //    ordersTable.Columns.Add("REFILL SELECTED OPTION");
        //    ordersTable.Columns.Add("RESERVATION DATE");
        //    ordersTable.Columns.Add("STATUS");
        //    ordersTable.Columns.Add("TOTAL AMOUNT");


        //    if (response != null && response.ResultAs<Order>() != null)
        //    {
        //        // Loop through the orders and add them to the DataTable
        //        //foreach (var entry in filteredList)
        //        //{
        //        //    ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_StoreName, entry.order_ProductName,
        //        //                          entry.order_unit, entry.order_size, entry.order_DeliveryTypeValue,
        //        //                          entry.order_OrderTypeValue, entry.order_OrderMethod, entry.order_choosenSwapOption, entry.order_WaterPrice,
        //        //                          entry.order_Quantity, entry.order_ReservationDate,
        //        //                          entry.order_OrderStatus, entry.order_TotalAmount);
        //        //}
        //        foreach (var order in filteredList)
        //        {
        //            if (order.order_Products != null)
        //            {
        //                string product_order = "";

        //                foreach (var product in order.order_Products)
        //                {

        //                    // Concatenate the product name, size, and unit
        //                    product_order += product.order_size + " " + product.order_unit + " " + product.order_ProductName + "<br/>";

        //                }
        //                // Add a new row for the order and concatenate the product names, sizes, and units
        //                ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_StoreName, product_order,
        //                                        order.order_OverallQuantities, order.order_DeliveryTypeValue,
        //                                      order.order_OrderTypeValue, order.orderPaymentMethod, order.order_RefllSelectedOption,
        //                                       order.order_ReservationDate, order.order_OrderStatus, order.order_TotalAmount);
        //            }
        //        }

        //        //foreach (var order in filteredList)
        //        //{
        //        //    if (order.order_Products != null)
        //        //    {
        //        //        foreach (var product in order.order_Products)
        //        //        {
        //        //            ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_StoreName, product.productName,
        //        //                                  product.productUnit, product.productSize, order.order_DeliveryTypeValue,
        //        //                                  order.order_OrderTypeValue, order.orderPaymentMethod, order.order_choosenSwapOption, order.order_WaterPrice,
        //        //                                  order.order_Quantity, order.order_ReservationDate,
        //        //                                  order.order_OrderStatus, order.order_TotalAmount);
        //        //        }
        //        //    }
        //        //}

        //    }
        //    else
        //    {
        //        // Handle null response or invalid selected value
        //        ordersTable.Rows.Add("No orders found", "", "", "", "", "", "", "", "", "", "", "");
        //    }

        //    // Bind the DataTable to the GridView
        //    GridView1.DataSource = ordersTable;
        //    GridView1.DataBind();
        //}

        //UPDATING THE ORDERS
        // trigger the appropriate notifications based on the customer's order status

        protected void btnAccept_Click(object sender, EventArgs e)
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
                //else
                //{
                //    // Handle the case where the existing notification does not exist
                //}

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

                twoBigDB.Set("USERSLOG/" + log.logsId, log);

                DisplayTable();
            }
            else
            {
                // Get a list of all available drivers
                FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES");
                Dictionary<string, Employee> driverData = driverResponse.ResultAs<Dictionary<string, Employee>>();

                List<Employee> allDrivers = driverData.Values.Where(emp => emp.adminId.ToString() == idno && emp.emp_role == "Driver"
                                                                        && emp.emp_availability == "Available").ToList();
                // Check if the order status is "Pickup"
                if (existingOrder.order_OrderTypeValue == "PickUp")
                {
                    existingOrder.order_OrderStatus = "Accepted";
                    existingOrder.driverId = 0; // clear the driver ID

                    response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

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
                        existingOrder.dateDriverAssigned = DateTimeOffset.UtcNow;
                        existingOrder.driverAssignedBy = name;

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

                        twoBigDB.Set("USERSLOG/" + log.logsId, log);
                        DisplayTable();
                    }
                    else
                    {
                        // No drivers are currently available, display an error message
                        Response.Write("<script>alert ('There are no available drivers to accept this order. Wait for an available driver to deliver this order.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                    }

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
            string unit = existingOrder.order_Products[0].order_unit;

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
            FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
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

            twoBigDB.Update("USERSLOG/" + log.logsId, log);
            DisplayTable();
        
        }
        //UPDATING THE ORDERS
        protected void btnPaymentAccept_Click(object sender, EventArgs e)
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
                    existingOrder.payment_receivedBy = name;
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

                    //USERSLOG
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
                        userFullname = (string)Session["fullname"],
                        userActivity = "RECEIVED PAYMENT",
                        activityTime = addedTime
                    };

                    twoBigDB.Set("USERSLOG/" + log.logsId, log);

                    DisplayTable();
                }
                else
                {
                    // Display an error messaged
                    Response.Write("<script>alert ('Make sure order is successfully delivered.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");
                }

            }
        }
    }
}