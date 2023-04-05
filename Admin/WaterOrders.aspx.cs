using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
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

            //if (!IsPostBack)
            //{
            //    BindOrders();
            //    DisplayOrders();
            //}
            //onlineordersDisplay();
            //walkinordersDisplay();
            //gridOrder.Visible = false;
            //gridWalkIn.Visible = false;
        }
        private void onlineordersDisplay()
        {
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

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
            //ordersTable.Columns.Add("GALLON TYPE");
            ordersTable.Columns.Add("PRICE");
            ordersTable.Columns.Add("QUANTITY");
            ordersTable.Columns.Add("RESERVATION DATE");
            ordersTable.Columns.Add("STATUS");
            ordersTable.Columns.Add("TOTAL AMOUNT");
            //ordersTable.Rows.Add("OVERALL AMOUNT");

            if (response != null && response.ResultAs<Order>() != null)
            {
                var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && d.order_OrderStatus == "Received"
                || d.order_OrderStatus == "Delivered");
                //var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && d.order_OrderStatus == "Delivered");
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
            gridOrder.DataSource = ordersTable;
            gridOrder.DataBind();
        }

        private void walkinordersDisplay()
        {
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("WALKINORDERS");
            Dictionary<string, WalkInOrders> otherproductsList = response.ResultAs<Dictionary<string, WalkInOrders>>();

            // Create the DataTable to hold the orders
            DataTable walkInordersTable = new DataTable();
            walkInordersTable.Columns.Add("ORDER ID");
            walkInordersTable.Columns.Add("ORDER TYPE");
            walkInordersTable.Columns.Add("PRODUCT NAME");
            walkInordersTable.Columns.Add("PRODUCT UNIT");
            walkInordersTable.Columns.Add("PRODUCT SIZE");
            walkInordersTable.Columns.Add("PRICE");
            walkInordersTable.Columns.Add("QUANTITY");
            walkInordersTable.Columns.Add("DISCOUNT");
            walkInordersTable.Columns.Add("TOTAL AMOUNT");
            walkInordersTable.Columns.Add("DATE");
            walkInordersTable.Columns.Add("ADDED BY");

            if (response != null && response.ResultAs<Order>() != null)
            {
                var filteredList = otherproductsList.Values.Where(d => d.adminId.ToString() == idno);
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    walkInordersTable.Rows.Add(entry.orderNo, entry.orderType, entry.productName, entry.productUnit, entry.productSize,
                                          entry.productPrice, entry.productQty, entry.productDiscount,
                                          entry.totalAmount, entry.dateAdded, entry.adminId);
                }
            }
            else
            {
                // Handle null response or invalid selected value
                walkInordersTable.Rows.Add("No data found", "", "", "", "", "", "");
            }

            // Bind the DataTable to the GridView
            gridWalkIn.DataSource = walkInordersTable;
            gridWalkIn.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedOption = ddlSearchOptions.SelectedValue;

                //if (selectedOption == "0")
                //{
                //    lblOrder.Text = "LIST OF ORDERS";
                //    lblWalkin.Text = "LIST OF WALKIN ORDERS";
                //    gridOrder.Visible = true;
                //    gridWalkIn.Visible = true;
                //    onlineordersDisplay();
                //    walkinordersDisplay();
                //}
                if (selectedOption == "1")
                {
                    lblOrder.Text = "LIST OF ONLINE ORDERS";
                    gridOrder.Visible = true;
                    gridWalkIn.Visible = false;
                    onlineordersDisplay();
                }
                else if (selectedOption == "2")
                {
                    lblOrder.Text = "LIST OF WALKIN ORDERS";
                    gridOrder.Visible = false;
                    gridWalkIn.Visible = true;
                    walkinordersDisplay();
                }
               
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/WaterProduct.aspx';" + ex.Message);
            }
        }

        //private void DisplayOrders()
        //{
        //    //var idnum = lblOrderID.Text; 

        //    FirebaseResponse response = twoBigDB.Get("ADMINNOTIFICATION/");
        //    Model.AdminNotification orders = response.ResultAs<Model.AdminNotification>();
        //    var json = response.Body;
        //    Dictionary<string, Model.AdminNotification> list = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminNotification>>(json);

        //    if (list != null && list.Count > 0)
        //    {
        //        foreach (KeyValuePair<string, Model.AdminNotification> entry in list)
        //        {
        //            lblStatus.Text += entry.Value.orderStatus;
        //        }
        //    }
        //}

        //private void BindOrders()
        //{
        //    var idnum = lblOrderID.Text;

        //    FirebaseResponse response = twoBigDB.Get("CUSTOMERNOTIFICATION/" + idnum);
        //    Model.OrderData orders = response.ResultAs<Model.OrderData>();
        //    var json = response.Body;
        //    Dictionary<string, Model.OrderData> list = JsonConvert.DeserializeObject<Dictionary<string, Model.OrderData>>(json);

        //    foreach (KeyValuePair<string, Model.OrderData> entry in list)
        //    {
        //        //string item = entry.Value.CusID.ToString() + "\t" + entry.Value.CusLastName + "\t" + entry.Value.CusFirstName;
        //        //ListBox1.Items.Add(entry.Value.CusID.ToString()) ;
        //        //datatable1.Items.Add(entry.Value.CusID.ToString());

        //        lblOrderID.Text += entry.Value.orderID.ToString() + "<br>" + "<br>";
        //        lblCustomerID.Text += entry.Value.order_CUSTOMERID.ToString() + "<br>" + "<br>";
        //        lblStoreName.Text += entry.Value.orderFrom_store.ToString() + "<br>" + "<br>";
        //        lblDeliveryType.Text += entry.Value.orderDeliveryType + "<br>" + "<br>";
        //        lblOrderType.Text += entry.Value.orderType + "<br>" + "<br>";
        //        lblProductType.Text += entry.Value.OrderProductType + "<br>" + "<br>";
        //        lblQuantity.Text += entry.Value.orderQuantity.ToString() + "<br>" + "<br>";
        //        lblPrice.Text += entry.Value.orderPrice.ToString() + "<br>" + "<br>";
        //        lblTotalAmount.Text += entry.Value.orderTotalAmount.ToString() + "<br>" + "<br>";
        //        lblDateOrder.Text += entry.Value.orderDateTime + "<br>" + "<br>";
        //        lblReservationDate.Text += entry.Value.OrderReservationDate + "<br>" + "<br>";
        //        //lblStatus.Text += entry.Value.OrderStatus + "<br>" + "<br>";
        //    }
        //}
    }   
}