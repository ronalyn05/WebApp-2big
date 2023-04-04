﻿using System;
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

            //if (driver != null)
            //{
            //    Session["emp_id"] = driver.emp_id;
            //}


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
          //  ordersTable.Columns.Add("ACTION");
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
            //ordersTable.Columns.Add("GALLON TYPE");
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
                                              entry.order_OrderTypeValue, entry.order_OrderMethod, entry.order_WaterPrice,
                                              entry.order_Quantity, entry.order_ReservationDate, 
                                              entry.order_OrderStatus, entry.order_InitialAmount);
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
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

            // Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the order ID from the first cell in the row
            int orderID = int.Parse(row.Cells[1].Text);

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
                
                // Display an error message indicating that no driver will be assigned
                Response.Write("<script>alert ('This order will be pick up by the owner. No driver will be assigned.');</script>");
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
                    response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);
                    DisplayTable();
                }
                else
                {
                    // No drivers are currently available, display an error message
                    Response.Write("<script>alert ('There are no available drivers to accept this order. Wait for an available driver to deliver this order.');</script>");
                }
            }

            //// Retrieve the existing order object from the database
            //FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            //Order existingOrder = response.ResultAs<Order>();

            //// Get a list of all available drivers
            //FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES");
            //Dictionary<string, Employee> driverData = driverResponse.ResultAs<Dictionary<string, Employee>>();

            //List<Employee> allDrivers = driverData.Values.Where(emp => emp.adminId.ToString() == idno && emp.emp_role == "Driver"
            //                                                    && emp.emp_availability == "Available").ToList();
            //// Check if any available driver is available to accept the order
            //if (allDrivers.Count > 0)
            //{
            //    // Assign the order to the first available driver
            //    Employee driver = allDrivers[0];
            //    existingOrder.driverId = driver.emp_id;
            //    existingOrder.order_OrderStatus = "Accepted";
            //    response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);
            //    DisplayTable();
            //}
            //else
            //{
            //    // No drivers are currently available, display an error message
            //    Response.Write("<script>alert ('There are no available drivers to accept this order. Wait for an available driver to deliver this order.');</script>");
            //}

        }

        //UPDATING THE STATUS ORDER ID DECLINE
        protected void btnDecline_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
           // int driverId = (int)Session["emp_id"];

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

            //existingOrder.driverId = (int)driverId; // clear the driver ID

            // Update the existing order object in the database
            response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

            // Rebind the GridView
            DisplayTable();
        }
    }
}