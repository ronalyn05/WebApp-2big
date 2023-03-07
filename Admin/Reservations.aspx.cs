//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//using FireSharp.Config;
//using FireSharp.Interfaces;
//using FireSharp.Response;
//using Newtonsoft.Json;

//namespace WRS2big_Web.Admin
//{
//    public partial class Reservations : System.Web.UI.Page
//    {
//        IFirebaseConfig config = new FirebaseConfig
//        {
//            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
//            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"
//        };
//        IFirebaseClient twoBigDB;
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            //connection to database
//            twoBigDB = new FireSharp.FirebaseClient(config);

//            if (!IsPostBack)
//            {
//                DisplayOrders();
//            }
//        }

//        private void DisplayOrders()
//        {
//            FirebaseResponse response;
//            response = twoBigDB.Get("CUSTOMER");
//            Model.Customer obj = response.ResultAs<Model.Customer>();
//            var json = response.Body;
//            Dictionary<string, Model.Customer> list = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(json);

//            foreach (KeyValuePair<string, Model.Customer> entry in list)
//            {
//                ListBox1.Items.Add(entry.Value.CusFirstName.ToString() + entry.Value.CusLastName.ToString());
//            }
//        }
//        protected void btnDisplay_Click(object sender, EventArgs e)
//        {
//            String slected;
//            slected = ListBox1.SelectedValue;
//            FirebaseResponse response;
//            response = twoBigDB.Get("WATER_ORDER/" + slected);
//            Model.OrderData obj = response.ResultAs<Model.OrderData>();
//            //LabelID.Text = obj.Order_id.ToString();
//           // LblCustName.Text = 
//            LblBorrowGal.Text = obj.OrderBorrowGallons.ToString();
//            Lbl_Category.Text = obj.OrderType.ToString();
//            Lbl_OwnGallons.Text = obj.OrderOwnGallons.ToString();
//            Lbl_ProdType.Text = obj.OrderProductType.ToString();
//            Lbl_ReservDate.Text = obj.OrderReservationDate.ToString();
//            Lbl_Qty.Text = obj.OrderQuantity.ToString();
//        }
//        protected void DeclineBtn_Click(object sender, EventArgs e)
//        {
//            // Update order status in the database
//            // ...

//            // Create a notification payload using the Firebase Notification object
//            //    var notification = new FirebaseNotification
//            //    {
//            //        Title = "Order Declined",
//            //        Body = "Unfortunately, we are unable to process your order at this time.",
//            //        Data = new Dictionary<string, string>
//            //{
//            //    { "orderId", Order.Order_Id.ToString() },
//            //    { "status", "declined" }
//            //}
//            //    };

//            //    // Send the notification to the customer's device
//            //    twoBigDB.Push("/devices/CUSTOMER_DEVICE_REGISTRATION_TOKEN", notification);

//        }
//    }
//}
