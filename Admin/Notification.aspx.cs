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

            FirebaseResponse response = twoBigDB.Get("NOTIFICATIONTEST/");
            Dictionary<string, NotificationMessage> orderlist = response.ResultAs<Dictionary<string, NotificationMessage>>();
            //var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno);
            var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && d.sender == "Customer"  );


            if (filteredList.Any())
            {
                // If there are incoming orders, display them in a button
                // If there are incoming orders, display them in separate buttons
                foreach (var notif in filteredList)
                {
                    // listNotif.Items.Add(order.body + ' ' + "with order id" + ' ' + order.orderID);
                    Button btn = new Button();
                    btnTransaction.Text = notif.body + ' ' + "with order id" + ' ' + notif.orderID + ' ' + " on " + ' ' + notif.notificationDate;
                    btn.CommandArgument = notif.orderID.ToString();
                    btn.Click += new EventHandler(btnTransaction_Click);
                    btnTransaction.Controls.Add(btn);

                }
            }
            else
            {
                // If there are no incoming orders, display a message
              //  Label lblNoOrders = new Label();
                btnTransaction.Text = "You have no incoming orders.";
              //  btnTransaction.Controls.Add(lblNoOrders);
            }
        }

        protected void btnTransaction_Click(object sender, EventArgs e)
        {
            Response.Redirect("OnlineOrders.aspx");
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
