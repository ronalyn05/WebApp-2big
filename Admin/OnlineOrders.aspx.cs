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
    public partial class OnlineOrders : System.Web.UI.Page
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
                displayAll_order();
                // Call the method to populate the dropdown with order IDs
                PopulateOrderDropdown();
                checkAdminStatus();
                //DisplayRewardPoints_order();
                //lblViewOrders.Text = "COD ORDER";
                //DisplayGcash_order();
                //lblViewGcashOrder.Text = "GCASH ORDER";
                //DisplayCOD_order();
                //lblViewRewardPoints.Text = "REWARD POINTS ORDER";
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

            //drdOrderId.SelectedValue = existingOrder.orderID.ToString();
            // Store the disabled state in the session
            Session["DisabledButton_" + existingOrder.orderID] = true;
            // Retrieve the existing order object from the database


        }
        private void checkAdminStatus()
        {
            if (Session["idno"] != null)
            {
                var adminID = Session["idno"];

                FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + adminID);
                Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();

                if (admin.currentSubscription == "LimitReached")
                {
                    btnViewOrders.Enabled = false;

                    warning.Text = "WARNING: You have reached your 'ORDER TRANSACTION LIMIT'. Existing pending orders cannot be processed using the platform anymore. If you wish to continue using the system, subscribe to a different package. ";
                }

            }
            else
            {
                // Display an error message 
                Response.Write("<script>alert ('Session Expired! Please login again.'); location.reload(); window.location.href = '/LandingPage/Account.aspx';</script>");
            }
        }
        //Fetch the employee 'driver' and get the id 
        private void PopulateOrderDropdown()
        {
            // Fetch all the employees from the database
            FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
            Dictionary<string, Employee> employees = response.ResultAs<Dictionary<string, Employee>>();

            if (response != null && employees != null)
            {
                // Create a list to store the driver employees
                List<Employee> driverEmployees = new List<Employee>();

                // Iterate over the employees and add the employees with the role "Driver" to the list
                foreach (var employee in employees.Values)
                {
                    if (employee.emp_role != null && employee.emp_role.ToLower() == "driver")
                    {
                        driverEmployees.Add(employee);
                    }
                }

                // Bind the driver employees' names to the dropdown
                drdAssignDriver.DataSource = driverEmployees;
                drdAssignDriver.DataTextField = "FullName"; //  property that returns the driver's full name
                drdAssignDriver.DataValueField = "emp_id"; //  property holding the employee's ID
                drdAssignDriver.DataBind();

                // Set a default item as the first item in the dropdown
                drdAssignDriver.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select driver to assign", ""));
            }
        }
        //DISPLAY ALL ORDER 
        private void displayAll_order()
        {
            if (Session["idno"] != null)
            {
                string idno = (string)Session["idno"];
                //string selectedDeliveryType = drdDeliveryType.SelectedValue;

                try
                {
                    FirebaseResponse response = twoBigDB.Get("ORDERS");
                    Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                    // Retrieve all customers from the CUSTOMER table and compare the current customer name
                    FirebaseResponse customerResponse = twoBigDB.Get("CUSTOMER");
                    Dictionary<string, Customer> customerlist = customerResponse.ResultAs<Dictionary<string, Customer>>();

                    // Fetch all the employees from the database
                    FirebaseResponse responseEmp = twoBigDB.Get("EMPLOYEES");
                    Dictionary<string, Employee> employees = responseEmp.ResultAs<Dictionary<string, Employee>>();

                    DataTable ordersTable = new DataTable();
                    ordersTable.Columns.Add("ORDER ID");
                    ordersTable.Columns.Add("CUSTOMER NAME");
                    ordersTable.Columns.Add("DRIVER NAME");
                    ordersTable.Columns.Add("STATUS");
                    ordersTable.Columns.Add("DELIVERY TYPE");
                    ordersTable.Columns.Add("PAYMENT METHOD");
                    ordersTable.Columns.Add("ORDER TYPE");
                    ordersTable.Columns.Add("RESERVATION DATE");
                    ordersTable.Columns.Add("RESERVATION TIME");
                    ordersTable.Columns.Add("RESERVATION DELIVERY TYPE SELECTED");
                    ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                    ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                    ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                    ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                    ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                    ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                    ordersTable.Columns.Add("TOTAL AMOUNT");
                    ordersTable.Columns.Add("ORDER DATE ");
                    ordersTable.Columns.Add("STORE NAME");

                    if (response != null && response.ResultAs<Order>() != null)
                    {
                        var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.order_OrderStatus == "Pending")).OrderByDescending(d => d.orderDate);

                        foreach (var order in filteredList)
                        {
                            // Retrieve the customer details based on the customer ID from the order
                            if (customerlist.TryGetValue(order.cusId.ToString(), out Customer customer))
                            {
                                string customerName = customer.firstName + " " + customer.lastName;
                                // Retrieve the driver details based on the driver ID from the order
                                if (employees.TryGetValue(order.driverId.ToString(), out Employee driver))
                                {
                                    string driverName = driver.FullName;

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

                                            string dateOrder = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");
                                            //string dateOrder = order.order_deliveryReservationDeliveryReserveDate == DateTime.MinValue ? "" : order.order_deliveryReservationDeliveryReserveDate.ToString("MMMM dd, yyyy hh:mm:ss tt");
                                            //string dateOrder = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");
                                            //string dateOrder = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");
                                            if (order.order_DeliveryTypeValue == "Reservation")
                                            {
                                                ordersTable.Rows.Add(order.orderID, customerName, driverName, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                                    order.orderPaymentMethod, order.order_OrderTypeValue, order.order_ReservationDate, order.order_deliveryReservationDeliveryReserveTime,
                                                    order.order_deliveryReservationDeliveryTypeSelected, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                                    order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount, dateOrder, order.order_StoreName);
                                                //ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                                //    order.orderPaymentMethod, order.order_OrderTypeValue, order.order_ReservationDate, order.order_deliveryReservationDeliveryReserveTime,
                                                //    order.order_deliveryReservationDeliveryTypeSelected, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                                //    order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount, dateOrder, order.order_StoreName);
                                            }
                                            else
                                            {
                                                ordersTable.Rows.Add(order.orderID, customerName, driverName, order.order_OrderStatus, order.order_DeliveryTypeValue, order.orderPaymentMethod,
                                               order.order_OrderTypeValue, order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime,
                                               order.order_deliveryReservationDeliveryTypeSelected, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                               order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount, dateOrder, order.order_StoreName);
                                            }

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

                                        string dateOrder = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                        ordersTable.Rows.Add(order.orderID, customerName, driverName, order.order_OrderStatus, order.order_DeliveryTypeValue, order.orderPaymentMethod,
                                            order.order_OrderTypeValue, order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime,
                                            order.order_deliveryReservationDeliveryTypeSelected, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                            order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount, dateOrder, order.order_StoreName);
                                    }
                                }
                            }
                        }

                        if (ordersTable.Rows.Count == 0)
                        {
                            lblError.Text = "No  Orders Found";
                            lblError.Visible = true;
                        }
                        else
                        {
                            gridView_order.DataSource = ordersTable;
                            gridView_order.DataBind();
                        }
                    }
                    else
                    {
                        lblError.Text = "No Orders Found";
                        lblError.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "There was an error retrieving orders" + ex.Message;
                    lblError.Visible = true;
                }
            }
            else
            {
                // Display an error message 
                Response.Write("<script>alert ('Session Expired! Please login again.'); location.reload(); window.location.href = '/LandingPage/Account.aspx';</script>");
            }

        }

        //DISPLAY THE EXPRESS ORDER
        private void displayExpress_order()
        {
            if (Session["idno"] != null)
            {
                string idno = (string)Session["idno"];
                try
                {
                    FirebaseResponse response = twoBigDB.Get("ORDERS");
                    Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                    // Retrieve all customers from the CUSTOMER table and compare the current customer name
                    FirebaseResponse customerResponse = twoBigDB.Get("CUSTOMER");
                    Dictionary<string, Customer> customerlist = customerResponse.ResultAs<Dictionary<string, Customer>>();

                    // Fetch all the employees from the database
                    FirebaseResponse responseEmp = twoBigDB.Get("EMPLOYEES");
                    Dictionary<string, Employee> employees = responseEmp.ResultAs<Dictionary<string, Employee>>();

                    DataTable ordersTable = new DataTable();
                    ordersTable.Columns.Add("ORDER ID");
                    ordersTable.Columns.Add("CUSTOMER NAME");
                    ordersTable.Columns.Add("DRIVER NAME");
                    ordersTable.Columns.Add("STATUS");
                    ordersTable.Columns.Add("DELIVERY TYPE");
                    ordersTable.Columns.Add("PAYMENT METHOD");
                    ordersTable.Columns.Add("ORDER TYPE");
                    ordersTable.Columns.Add("RESERVATION DATE");
                    ordersTable.Columns.Add("RESERVATION TIME");
                    ordersTable.Columns.Add("RESERVATION DELIVERY TYPE SELECTED");
                    ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                    ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                    ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                    ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                    ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                    ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                    ordersTable.Columns.Add("TOTAL AMOUNT");
                    ordersTable.Columns.Add("ORDER DATE ");
                    ordersTable.Columns.Add("STORE NAME");

                    if (response != null && response.ResultAs<Order>() != null)
                    {
                        var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.order_DeliveryTypeValue == "Express") && (d.order_OrderStatus == "Pending")).OrderByDescending(d => d.orderDate);
                        //var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Gcash")).OrderByDescending(d => d.orderDate);

                        foreach (var order in filteredList)
                        {
                            // Retrieve the customer details based on the customer ID from the order
                            if (customerlist.TryGetValue(order.cusId.ToString(), out Customer customer))
                            {
                                string customerName = customer.firstName + " " + customer.lastName;

                                // Retrieve the driver details based on the driver ID from the order
                                if (employees.TryGetValue(order.driverId.ToString(), out Employee driver))
                                {
                                    string driverName = driver.FullName;

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

                                            string dateOrder = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                            ordersTable.Rows.Add(order.orderID, customerName, driverName, order.order_OrderStatus, order.order_DeliveryTypeValue, order.orderPaymentMethod, order.order_OrderTypeValue,
                                            order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                                            productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                            order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                           dateOrder, order.order_StoreName);
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

                                        string dateOrder = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                        ordersTable.Rows.Add(order.orderID, customerName, driverName, order.order_OrderStatus, order.order_DeliveryTypeValue, order.orderPaymentMethod, order.order_OrderTypeValue,
                                             order.order_deliveryReservationDeliveryReserveDate, order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                                             productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                             order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                            dateOrder, order.order_StoreName);
                                    }
                                }
                            }
                        }

                        if (ordersTable.Rows.Count == 0)
                        {
                            lblError.Text = "No  Orders Found";
                            lblError.Visible = true;
                        }
                        else
                        {
                            gridView_order.DataSource = ordersTable;
                            gridView_order.DataBind();
                        }
                    }
                    else
                    {
                        lblError.Text = "No Orders Found";
                        lblError.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "There was an error retrieving orders" + ex.Message;
                    lblError.Visible = true;
                }
            }
            else
            {
                // Display an error message 
                Response.Write("<script>alert ('Session Expired! Please login again.'); location.reload(); window.location.href = '/LandingPage/Account.aspx';</script>");
            }

        }
       
        //   DISPLAY THE STANDARD ORDER
        private void displayStandard_order()
        {
            if (Session["idno"] != null)
            {
                string idno = (string)Session["idno"];

                try
                {
                    FirebaseResponse response = twoBigDB.Get("ORDERS");
                    Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                    // Retrieve all customers from the CUSTOMER table and compare the current customer name
                    FirebaseResponse customerResponse = twoBigDB.Get("CUSTOMER");
                    Dictionary<string, Customer> customerlist = customerResponse.ResultAs<Dictionary<string, Customer>>();

                    // Fetch all the employees from the database
                    FirebaseResponse responseEmp = twoBigDB.Get("EMPLOYEES");
                    Dictionary<string, Employee> employees = responseEmp.ResultAs<Dictionary<string, Employee>>();


                    DataTable ordersTable = new DataTable();
                    ordersTable.Columns.Add("ORDER ID");
                    ordersTable.Columns.Add("CUSTOMER NAME");
                    ordersTable.Columns.Add("DRIVER NAME");
                    ordersTable.Columns.Add("STATUS");
                    ordersTable.Columns.Add("DELIVERY TYPE");
                    ordersTable.Columns.Add("PAYMENT METHOD");
                    ordersTable.Columns.Add("ORDER TYPE");
                    ordersTable.Columns.Add("RESERVATION DATE");
                    ordersTable.Columns.Add("RESERVATION TIME");
                    ordersTable.Columns.Add("RESERVATION DELIVERY TYPE SELECTED");
                    ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                    ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                    ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                    ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                    ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                    ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                    ordersTable.Columns.Add("TOTAL AMOUNT");
                    ordersTable.Columns.Add("ORDER DATE ");
                    ordersTable.Columns.Add("STORE NAME");



                    if (response != null && response.ResultAs<Order>() != null)
                    {
                        var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.order_DeliveryTypeValue == "Standard") && (d.order_OrderStatus == "Pending")).OrderByDescending(d => d.orderDate);
                        //var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Points")).OrderByDescending(d => d.orderDate);

                        foreach (var order in filteredList)
                        {
                            // Retrieve the customer details based on the customer ID from the order
                            if (customerlist.TryGetValue(order.cusId.ToString(), out Customer customer))
                            {
                                string customerName = customer.firstName + " " + customer.lastName;

                                // Retrieve the driver details based on the driver ID from the order
                                if (employees.TryGetValue(order.driverId.ToString(), out Employee driver))
                                {
                                    string driverName = driver.FullName;

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

                                            string dateOrder = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                            ordersTable.Rows.Add(order.orderID, customerName, driverName, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                                order.orderPaymentMethod, order.order_OrderTypeValue, order.order_deliveryReservationDeliveryReserveDate,
                                                order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                                                productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                                order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                                dateOrder, order.order_StoreName);
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

                                        string dateOrder = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                        ordersTable.Rows.Add(order.orderID, customerName, driverName, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                            order.orderPaymentMethod, order.order_OrderTypeValue, order.order_deliveryReservationDeliveryReserveDate,
                                            order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                                            productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                           order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                           dateOrder, order.order_StoreName);
                                    }
                                }
                            }
                        }

                        if (ordersTable.Rows.Count == 0)
                        {
                            lblError.Text = "No  Orders Found";
                            lblError.Visible = true;
                        }
                        else
                        {
                            gridView_order.DataSource = ordersTable;
                            gridView_order.DataBind();
                        }
                    }
                    else
                    {
                        lblError.Text = "No Orders Found";
                        lblError.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "There was an error retrieving orders" + ex.Message;
                    lblError.Visible = true;
                }
            }
            else
            {
                // Display an error message 
                Response.Write("<script>alert ('Session Expired! Please login again.'); location.reload(); window.location.href = '/LandingPage/Account.aspx';</script>");
            }

        }
        private void displayReservation_order()
        {
            if (Session["idno"] != null)
            {
                string idno = (string)Session["idno"];

                try
                {
                    FirebaseResponse response = twoBigDB.Get("ORDERS");
                    Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                    // Retrieve all customers from the CUSTOMER table and compare the current customer name
                    FirebaseResponse customerResponse = twoBigDB.Get("CUSTOMER");
                    Dictionary<string, Customer> customerlist = customerResponse.ResultAs<Dictionary<string, Customer>>();

                    // Fetch all the employees from the database
                    FirebaseResponse responseEmp = twoBigDB.Get("EMPLOYEES");
                    Dictionary<string, Employee> employees = responseEmp.ResultAs<Dictionary<string, Employee>>();


                    DataTable ordersTable = new DataTable();
                    ordersTable.Columns.Add("ORDER ID");
                    ordersTable.Columns.Add("CUSTOMER NAME");
                    ordersTable.Columns.Add("DRIVER NAME");
                    ordersTable.Columns.Add("STATUS");
                    ordersTable.Columns.Add("DELIVERY TYPE");
                    ordersTable.Columns.Add("PAYMENT METHOD");
                    ordersTable.Columns.Add("ORDER TYPE");
                    ordersTable.Columns.Add("RESERVATION DATE");
                    ordersTable.Columns.Add("RESERVATION TIME");
                    ordersTable.Columns.Add("RESERVATION DELIVERY TYPE SELECTED");
                    ordersTable.Columns.Add("PRODUCT REFILL ORDER");
                    ordersTable.Columns.Add("THIRD PARTY PRODUCT ORDER ");
                    ordersTable.Columns.Add("PRODUCT REFILL QUANTITY");
                    ordersTable.Columns.Add("THIRD PARTY PRODUCT  QUANTITY");
                    ordersTable.Columns.Add("ADDTIONAL MODE OF PAYMENT");
                    ordersTable.Columns.Add("GALLON CONDITION / OPTION");
                    ordersTable.Columns.Add("TOTAL AMOUNT");
                    ordersTable.Columns.Add("ORDER DATE ");
                    ordersTable.Columns.Add("STORE NAME");



                    if (response != null && response.ResultAs<Order>() != null)
                    {
                        var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.order_DeliveryTypeValue == "Reservation") && (d.order_OrderStatus == "Pending")).OrderByDescending(d => d.orderDate);
                        //var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Points")).OrderByDescending(d => d.orderDate);

                        foreach (var order in filteredList)
                        {
                            // Retrieve the customer details based on the customer ID from the order
                            if (customerlist.TryGetValue(order.cusId.ToString(), out Customer customer))
                            {
                                string customerName = customer.firstName + " " + customer.lastName;

                                // Retrieve the driver details based on the driver ID from the order
                                if (employees.TryGetValue(order.driverId.ToString(), out Employee driver))
                                {
                                    string driverName = driver.FullName;

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

                                            string dateOrder = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                            ordersTable.Rows.Add(order.orderID, customerName, driverName, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                                order.orderPaymentMethod, order.order_OrderTypeValue, order.order_ReservationDate,
                                                order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                                                productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                                order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                                dateOrder, order.order_StoreName);

                                            //ordersTable.Rows.Add(order.orderID, order.cusId, order.driverId, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                            //   order.order_OrderTypeValue, productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                            //   order.orderPaymentMethod, order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                            //   dateOrder, order.order_ReservationDate, order.order_StoreName);

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

                                        string dateOrder = order.orderDate == DateTime.MinValue ? "" : order.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                                        ordersTable.Rows.Add(order.orderID, customerName, driverName, order.order_OrderStatus, order.order_DeliveryTypeValue,
                                            order.orderPaymentMethod, order.order_OrderTypeValue, order.order_deliveryReservationDeliveryReserveDate,
                                            order.order_deliveryReservationDeliveryReserveTime, order.order_deliveryReservationDeliveryTypeSelected,
                                            productrefill_order, otherproduct_order, productrefill_qty, otherproduct_qty,
                                            order.orderPaymentMethod2, order.order_RefillSelectedOption, order.order_TotalAmount,
                                           dateOrder, order.order_StoreName);
                                    }
                                }
                            }
                        }

                        if (ordersTable.Rows.Count == 0)
                        {
                            lblError.Text = "No  Orders Found";
                            lblError.Visible = true;
                        }
                        else
                        {
                            gridView_order.DataSource = ordersTable;
                            gridView_order.DataBind();
                        }
                    }
                    else
                    {
                        lblError.Text = "No Orders Found";
                        lblError.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "There was an error retrieving orders" + ex.Message;
                    lblError.Visible = true;
                }
            }
            else
            {
                // Display an error message 
                Response.Write("<script>alert ('Session Expired! Please login again.'); location.reload(); window.location.href = '/LandingPage/Account.aspx';</script>");
            }

        }

        //UPDATING THE STATUS ORDER FOR COD IF ACCEPTED
        protected void btnAccept_Click(object sender, EventArgs e)
        {

          

            if (Session["role"] != null || Session["idno"] != null || Session["fullname"] != null)
            {
                string role = (string)Session["role"];
                // Get the admin ID from the session
                string idno = (string)Session["idno"];
                int adminId = int.Parse(idno);
                string name = (string)Session["fullname"];

                try
                {

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
                        //// Check if the order status is "Accepted", "Payment Received", or "Delivered"
                        //if (existingOrder.order_OrderStatus != "Pending")
                        //{
                        //    // Disable the button
                        //    btn.Enabled = false;

                        //    // Store the disabled state in the session
                        //    Session["DisabledButton_" + orderID] = true;
                        //}

                        //if (existingOrder.order_OrderStatus != "Pending")
                        //{
                        //    Response.Write("<script>alert ('Order has already been accepted!');</script>");
                        //    return;
                        //}

                        // Check if the order already has a driver assigned
                        if (existingOrder.driverId != 0)
                        {
                            // Get the driver object from the database
                            FirebaseResponse driverRes = twoBigDB.Get("EMPLOYEES/" + existingOrder.driverId);
                            Employee driver = driverRes.ResultAs<Employee>();


                            // Accept the order with the assigned driver
                            existingOrder.driverId = driver.emp_id;
                            existingOrder.order_OrderStatus = "Accepted";
                            existingOrder.dateOrderAccepted = DateTime.Now;
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

                            //Random rnd = new Random();
                            int logsID = rnd.Next(1, 10000);

                            // Get the current date and time
                            DateTime addedTime = DateTime.Now;

                            // Log user activity
                            var log = new UsersLogs
                            {
                                userIdnum = int.Parse(idno),
                                logsId = logsID,
                                role = role,
                                userFullname = (string)Session["fullname"],
                                userActivity = "ACCEPTED ORDER",
                                activityTime = addedTime,
                              
                            };

                            twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                            displayAll_order();
                            displayExpress_order();
                            displayReservation_order();
                            displayStandard_order();
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
                                existingOrder.driverId = existingOrder.driverId; // clear the driver ID
                                existingOrder.dateOrderAccepted = DateTime.Now;
                                existingOrder.orderAcceptedBy = name;

                                response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                                // Display an error message indicating that no driver will be assigned 
                                Response.Write("<script>alert ('This order will be pick up by the customer. No driver will be assigned.'); location.reload(); window.location.href = '/Admin/OnlineOrders.aspx';</script>");

                                displayAll_order();
                                displayExpress_order();
                                displayReservation_order();
                                displayStandard_order();
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
                                    existingOrder.dateOrderAccepted = DateTime.Now;
                                    existingOrder.dateDriverAssigned = DateTime.Now;
                                    existingOrder.driverAssignedBy = name;
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

                                    Response.Write("<script>alert ('Order has been Accepted!'); window.location.href = '/Admin/OnlineOrders.aspx';</script>");




                                    //Random rnd = new Random();
                                    int logsID = rnd.Next(1, 10000);

                                    // Get the current date and time
                                    DateTime addedTime = DateTime.Now;

                                    // Log user activity
                                    var log = new UsersLogs
                                    {
                                        userIdnum = int.Parse(idno),
                                        logsId = logsID,
                                        role = role,
                                        userFullname = (string)Session["fullname"],
                                        userActivity = "ACCEPTED ORDER",
                                        activityTime = addedTime
                                    };

                                    twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                                    displayAll_order();
                                    displayExpress_order();
                                    displayReservation_order();
                                    displayStandard_order();
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
           

        }
        //SAVING ORDER ID FOR ASSIGNING THE DRIVER
        protected void btnAssignDriverClick(object sender, EventArgs e)
        {
            // Retrieve the button that was clicked
            Button btnAssign = (Button)sender;
            // Get the order ID from the command argument
            //int orderID = int.Parse(btnDecline.CommandArgument);
            // Find the GridView row containing the button
            GridViewRow row = (GridViewRow)btnAssign.NamingContainer;

            // Get the order ID from the specific column
            int orderIDColumnIndex = 2; //  the actual column index of the order ID
            int orderID = int.Parse(row.Cells[orderIDColumnIndex].Text);

            // Store the order ID in a hidden field for later use
            hfAssignDriver.Value = orderID.ToString();

            // Show the modal popup
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "assignDriver", "$('#assignDriver').modal('show');", true);
           
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
            string role = (string)Session["role"];

            // Get the order ID from the hidden field
            int orderID = int.Parse(hfAssignDriver.Value);

            // Get the reason input value
            string driverId = drdAssignDriver.SelectedValue;

            try
            {

                // Retrieve the existing order object from the database
                FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
                Order existingOrder = response.ResultAs<Order>();

                if (response != null && response.ResultAs<Order>() != null)
                {
                    if(existingOrder.order_OrderTypeValue == "Delivery")
                    {
                        // Update the existing order object with the new driver ID
                        existingOrder.driverId = int.Parse(driverId);
                        existingOrder.dateDriverAssigned = DateTime.Now;
                        existingOrder.driverAssignedBy = name;

                        // Update the existing order object in the database
                        response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                        // Show success message
                        Response.Write("<script>alert('You have successfully assigned driver " + driverId + " to order number " + orderID + ". You can now proceed to accept the order.'); </script>");
                    }
                    else
                    {
                        // Show  message
                        Response.Write("<script>alert('You are not allowed to assign driver since order wil be pick up by the customer.'); </script>");
                    }
                    

                  
                }

              

                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    role = role,
                    userFullname = name,
                    activityTime = addedTime,
                    userActivity = "ASSIGNED THE DRIVER TO DELIVER THE ORDER",
                    // userActivity = UserActivityType.UpdatedEmployeeRecords
                };
                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                displayAll_order();
                //displayExpress_order();
                //displayReservation_order();
                //displayStandard_order();

            }
            catch (Exception ex)
            {
                // Show error message
                Response.Write("<script>alert ('An error occurred while processing your request.');</script>" + ex.Message);
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
            int orderIDColumnIndex = 2; //  the actual column index of the order ID
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

               

                if (Session["role"] != null || Session["idno"] != null)
                {
                    string role = (string)Session["role"];
                    string idno = (string)Session["idno"];

                    //Random rnd = new Random();
                    int logsID = rnd.Next(1, 10000);

                    // Get the current date and time
                    DateTime addedTime = DateTime.Now;

                    // Log user activity
                    var log = new UsersLogs
                    {
                        userIdnum = int.Parse(idno),
                        logsId = logsID,
                        role = role,
                        userFullname = (string)Session["fullname"],
                        userActivity = "DECLINED ORDER",
                        activityTime = addedTime
                    };

                    twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                }

            }

            displayAll_order();
           
        }


        //METHOD IN CHOOSING TO VIEW ORDER BASE ON DELIVERY TYPES
        protected void btnViewOrders_Click(object sender, EventArgs e)
        {

            string selectedOption = drdViewOrders.SelectedValue;
            try
            {
                if (selectedOption == "0")
                {
                    displayAll_order();
                    //lblViewOrders.Text = "ALL ORDER";

                }
                else if (selectedOption == "1")
                {
                    //lblViewOrders.Text = "EXPRESS ORDER";
                    displayExpress_order();
                    //lblError.Visible = false;

                }
                else if (selectedOption == "2")
                {
                    //lblViewOrders.Text = "STANDARD ORDER";
                    displayStandard_order();
                    //lblError.Visible = false;

                }
                else if (selectedOption == "3")
                {
                    //lblViewOrders.Text = "RESERVATION ORDER";
                    displayReservation_order();
                    //lblError.Visible = false;
                }
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/OnlineOrders.aspx';" + ex.Message);
            }
        }

       
    }
}