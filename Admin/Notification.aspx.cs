using System;
using System.Collections.Generic;
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
    public partial class Notification : System.Web.UI.Page
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient twoBigDB;
        protected void Page_Load(object sender, EventArgs e)
        {
            twoBigDB = new FireSharp.FirebaseClient(config);

            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        protected void LoadOrders()
        {
            string idno = (string)Session["idno"];

            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();
            var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && d.order_OrderStatus == "Pending");

            if (filteredList.Any())
            {
                // If there are incoming orders, display them in a button
                Button btn = new Button();
                int orderCount = filteredList.Count();
                if (orderCount == 1)
                {
                    // If there is only one incoming order, display its details in the button text
                    var order = filteredList.First();
                    btn.Text = "You have an pending order from customer id " + order.cusId.ToString();
                    btn.CommandArgument = order.orderID.ToString();
                    btn.Click += new EventHandler(btnTransaction_Click);
                    btnTransaction.Controls.Add(btn);
                }
                else
                {
                    // If there are multiple incoming orders, display a message that iterates through them
                    string message = "You have " + orderCount.ToString() + " pending orders. Click to view the first order.";
                    btn.Text = message;
                    btn.Click += new EventHandler(btnTransaction_Click);
                    btnTransaction.Controls.Add(btn);
                    int index = 0;
                    foreach (var order in filteredList)
                    {
                        // Store the order information in the session variable to pass it to the order details page
                        Session["order_" + index.ToString()] = order;
                        index++;
                    }
                    Session["order_count"] = index.ToString();
                }
            }
            else
            {
                // If there are no incoming orders, display a message
                Label lblNoOrders = new Label();
                lblNoOrders.Text = "You have no incoming orders.";
                btnTransaction.Controls.Add(lblNoOrders);
            }
        }

        protected void btnTransaction_Click(object sender, EventArgs e)
        {
            // Check if there are multiple orders pending
            if (Session["order_count"] != null)
            {
                int orderCount = Int32.Parse(Session["order_count"].ToString());
                if (orderCount > 1)
                {
                    // Iterate to the next order and update the button text
                    int currentIndex = Int32.Parse(Session["order_index"].ToString());
                    currentIndex++;
                    if (currentIndex >= orderCount)
                    {
                        currentIndex = 0;
                    }
                    //var order = (Order)Session["order_" + currentIndex.ToString()];
                    //string message = "You have " + orderCount.ToString() + " pending orders. Click to view the next order.";
                    //Button btn = (Button)sender;
                    //btn.Text = message;
                    //Session["order_index"] = currentIndex.ToString();
                    //Session["order_current"] = order;
                    //return;
                }
            }
            // If there is only one pending order or the last order in the iteration has been reached, redirect to the order details page
            Button btn = (Button)sender;
            var order = (Order)Session["order_current"];
            string orderID = order.orderID.ToString();
            Response.Redirect("OnlineOrders.aspx?orderID=" + orderID);
        }

        //SEARCH ORDER DETAILS
        //private void DisplayOrders()
        //{
        //    FirebaseResponse response;
        //    response = twoBigDB.Get("ORDER/");
        //    Model.OrderData obj = response.ResultAs<Model.OrderData>();

        //    var json = response.Body;
        //    Dictionary<string, Model.OrderData> list = JsonConvert.DeserializeObject<Dictionary<string, Model.OrderData>>(json);

        //    foreach (KeyValuePair<string, Model.OrderData> entry in list)
        //    {
        //       ListBox1.Items.Add(entry.Value.orderID.ToString());
        //        //ListBox1.Items.Add(json);
        //    }
        //}

        //DISPLAY ORDER DETAILS
        //protected void btnDisplay_Click(object sender, EventArgs e)
        //{
        //    String slected;
        //    slected = ListBox1.SelectedValue;
        //    FirebaseResponse response;
        //    response = twoBigDB.Get("ORDER/" + slected);
        //    if (response != null)
        //    {
        //        Model.OrderData obj = response.ResultAs<Model.OrderData>();
        //        //var obj = response.ResultAs<Model.OrderData>();

        //        LabelID.Text = obj.orderID.ToString();
        //        custID.Text = obj.order_CUSTOMERID.ToString();
        //        lblStore.Text = obj.orderFrom_store.ToString();
        //        DeliveryType.Text = obj.orderDeliveryType.ToString();
        //        OrderType.Text = obj.orderType.ToString();
        //        ProductType.Text = obj.OrderProductType.ToString();
        //        Quantity.Text = obj.orderQuantity.ToString();
        //        price.Text = obj.orderPrice.ToString();
        //        totalAmount.Text = obj.orderTotalAmount.ToString();
        //        dateOrder.Text = obj.orderDateTime.ToString();
        //        ReservDate.Text = obj.OrderReservationDate.ToString(); 
        //        Drd_Status.Text = obj.OrderStatus.ToString();
        //    }
        //}

        //protected void DeclineBtn_Click(object sender, EventArgs e)
        //{
        //    String AcceptStr;
        //    AcceptStr = ListBox1.SelectedValue;

        //    Random rnd = new Random();
        //    int notifid = rnd.Next(1, 10000);

        //    var data = new Model.CUSTOMERNOTIFICATION();

        //    data.notifID = notifid;
        //    data.orderID = int.Parse(LabelID.Text);
        //    data.order_CUSTOMERID = int.Parse(custID.Text);
        //    data.orderFrom_store = lblStore.Text; 
        //    data.orderDeliveryType = DeliveryType.Text;
        //    data.orderType = OrderType.Text;
        //    data.OrderProductType = ProductType.Text;
        //    data.orderQuantity = int.Parse(Quantity.Text);
        //    data.orderPrice = Convert.ToDecimal(price.Text);
        //    data.orderTotalAmount = Convert.ToDecimal(totalAmount.Text);
        //    data.orderDateTime = Convert.ToDateTime(dateOrder.Text);
        //    data.OrderReservationDate = ReservDate.Text;
        //    data.OrderStatus = Drd_Status.Text;

        //    // Code to decline the order goes here
        //    FirebaseResponse response = twoBigDB.Update("CUSTOMERNOTIFICATION/" + AcceptStr, data);

        //    var result1 = twoBigDB.Get("CUSTOMERNOTIFICATION/" + AcceptStr);//Retrieve Updated Data From ORDER TBL
        //    Model.CUSTOMERNOTIFICATION obj = response.ResultAs<Model.CUSTOMERNOTIFICATION>();//Database Result



        //    LabelID.Text = obj.orderID.ToString();
        //    custID.Text = obj.order_CUSTOMERID.ToString();
        //    lblStore.Text = obj.orderFrom_store.ToString();
        //    DeliveryType.Text = obj.orderDeliveryType.ToString();
        //    OrderType.Text = obj.orderType.ToString();
        //    ProductType.Text = obj.OrderProductType.ToString();
        //    Quantity.Text = obj.orderQuantity.ToString();
        //    price.Text = obj.orderPrice.ToString();
        //    totalAmount.Text = obj.orderTotalAmount.ToString();
        //    dateOrder.Text = obj.orderDateTime.ToString();
        //    ReservDate.Text = obj.OrderReservationDate.ToString();
        //    Drd_Status.Text = obj.OrderStatus.ToString();

        //    Response.Write("<script> alert('Order declined, notification sent!')</script>");
        //}
        //protected void btnAccept_Click(object sender, EventArgs e)
        //{
        //    String AcceptStr;
        //    AcceptStr = ListBox1.SelectedValue;

        //    Random rnd = new Random();
        //    int notifid = rnd.Next(1, 10000);

        //    var data = new Model.CUSTOMERNOTIFICATION();

        //    data.notifID = notifid;
        //    data.orderID = int.Parse(LabelID.Text);
        //    data.order_CUSTOMERID = int.Parse(custID.Text);
        //    data.orderFrom_store = lblStore.Text;
        //    data.orderDeliveryType = DeliveryType.Text;
        //    data.orderType = OrderType.Text;
        //    data.OrderProductType = ProductType.Text;
        //    data.orderQuantity = int.Parse(Quantity.Text);
        //    data.orderPrice = Convert.ToDecimal(price.Text);
        //    data.orderTotalAmount = Convert.ToDecimal(totalAmount.Text);
        //    data.orderDateTime = Convert.ToDateTime(dateOrder.Text);
        //    data.OrderReservationDate = ReservDate.Text;
        //    data.OrderStatus = Drd_Status.Text;

        //    // Code to accept the order goes here
        //    FirebaseResponse response = twoBigDB.Update("CUSTOMERNOTIFICATION/" + AcceptStr, data);

        //    var result1 = twoBigDB.Get("CUSTOMERNOTIFICATION/" + AcceptStr);//Retrieve Updated Data From ORDER TBL
        //    Model.CUSTOMERNOTIFICATION obj = response.ResultAs<Model.CUSTOMERNOTIFICATION>();//Database Result

        //    LabelID.Text = obj.orderID.ToString();
        //    custID.Text = obj.order_CUSTOMERID.ToString();
        //    lblStore.Text = obj.orderFrom_store.ToString();
        //    DeliveryType.Text = obj.orderDeliveryType.ToString();
        //    OrderType.Text = obj.orderType.ToString();
        //    ProductType.Text = obj.OrderProductType.ToString();
        //    Quantity.Text = obj.orderQuantity.ToString();
        //    price.Text = obj.orderPrice.ToString();
        //    totalAmount.Text = obj.orderTotalAmount.ToString();
        //    dateOrder.Text = obj.orderDateTime.ToString();
        //    ReservDate.Text = obj.OrderReservationDate.ToString();
        //    Drd_Status.Text = obj.OrderStatus.ToString();

        //    Response.Write("<script> alert('Order accepted, notification sent!')</script>");
        //}

    }
}
