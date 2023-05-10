using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
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
                lblWalkIn.Text = "LIST OF WALKIN ORDERS";
            }

        }
        //DISPLAY ONLINE ORDERS
        private void onlineordersDisplay()
        {
            string idno = (string)Session["idno"];

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();
            var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno &&
                (d.order_OrderStatus == "Payment Received" || d.order_OrderStatus == "Received"));

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

            if (response != null && response.ResultAs<Order>() != null)
            {
                //var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && d.order_OrderStatus == "Delivered");
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    string dateAccepted = entry.dateOrderAccepted == DateTimeOffset.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateDeclined = entry.dateOrderDeclined == DateTimeOffset.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateDelivered = entry.dateOrderDelivered == DateTimeOffset.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string datePayment = entry.datePaymentReceived == DateTimeOffset.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateDriverAssigned = entry.dateDriverAssigned == DateTimeOffset.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                             dateOrder, dateAccepted, entry.orderAcceptedBy, dateDeclined, entry.orderDeclineddBy, dateDriverAssigned, entry.driverAssignedBy,
                             dateDelivered, datePayment, entry.payment_receivedBy);
                }
                if (ordersTable.Rows.Count == 0)
                {
                    lblMessage.Text = "No  Data Found";
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
                lblMessage.Text = "No data found";
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
            var filteredList = otherproductsList.Values.Where(d => d.adminId.ToString() == idno);

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

                    string dateAdded = entry.dateAdded == DateTimeOffset.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    //walkInordersTable.Rows.Add(entry.orderNo, entry.orderType, entry.productName, entry.productSize + " " + entry.productUnit,
                    //    entry.productPrice, entry.productQty, discount, entry.totalAmount, dateAdded, entry.addedBy);


                    walkInordersTable.Rows.Add(entry.orderNo, entry.orderType, entry.productName, entry.productUnitSize,
                                         entry.productPrice, entry.productQty, discount,
                                         entry.totalAmount, dateAdded, entry.addedBy);
                }

                if (walkInordersTable.Rows.Count == 0)
                {
                    lblMessage.Text = "No Data Found";
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
                lblMessage.Text = "No data found";
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
                            string dateAdded = entry.dateAdded == DateTimeOffset.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");

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
                            string dateAccepted = entry.dateOrderAccepted == DateTimeOffset.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateDeclined = entry.dateOrderDeclined == DateTimeOffset.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateDelivered = entry.dateOrderDelivered == DateTimeOffset.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string datePayment = entry.datePaymentReceived == DateTimeOffset.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateDriverAssigned = entry.dateDriverAssigned == DateTimeOffset.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                                     dateOrder, dateAccepted, entry.orderAcceptedBy, dateDeclined, entry.orderDeclineddBy, dateDriverAssigned, entry.driverAssignedBy,
                                     dateDelivered, datePayment, entry.payment_receivedBy);
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

        //SEARCH CATEGORY ORDERS
        protected void btnView_Click(object sender, EventArgs e)
        {

            try
            {
                string selectedOption = ddlSearchOptions.SelectedValue;

                if (selectedOption == "0")
                {
                    onlineordersDisplay();
                    lblOrder.Text = "LIST OF ONLINE ORDERS";
                    walkinordersDisplay();
                    lblWalkIn.Text = "LIST OF WALKIN ORDERS";
                   
                }
                else if (selectedOption == "1")
                {
                    lblOrder.Text = "LIST OF ONLINE ORDERS";
                    lblWalkIn.Visible = false;
                    gridOrder.Visible = true;
                    gridWalkIn.Visible = false;
                    onlineordersDisplay();

                }
                else if (selectedOption == "2")
                {
                    lblWalkIn.Text = "LIST OF WALKIN ORDERS";
                    lblOrder.Visible = false;
                    gridOrder.Visible = false;
                    gridWalkIn.Visible = true;
                    walkinordersDisplay();

                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/WaterOrders.aspx';" + ex.Message);
            }

        }
       
    }
}