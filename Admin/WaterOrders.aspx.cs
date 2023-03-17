using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

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
                BindOrders();
                DisplayOrders();
            }
            
        }

        private void DisplayOrders()
        {
            //var idnum = lblOrderID.Text; 

            FirebaseResponse response = twoBigDB.Get("ADMINNOTIFICATION/");
            Model.AdminNotification orders = response.ResultAs<Model.AdminNotification>();
            var json = response.Body;
            Dictionary<string, Model.AdminNotification> list = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminNotification>>(json);

            if (list != null && list.Count > 0)
            {
                foreach (KeyValuePair<string, Model.AdminNotification> entry in list)
                {
                    lblStatus.Text += entry.Value.orderStatus;
                }
            }
        }

        private void BindOrders()
        {
            var idnum = lblOrderID.Text;

            FirebaseResponse response = twoBigDB.Get("CUSTOMERNOTIFICATION/" + idnum);
            Model.OrderData orders = response.ResultAs<Model.OrderData>();
            var json = response.Body;
            Dictionary<string, Model.OrderData> list = JsonConvert.DeserializeObject<Dictionary<string, Model.OrderData>>(json);

            foreach (KeyValuePair<string, Model.OrderData> entry in list)
            {
                //string item = entry.Value.CusID.ToString() + "\t" + entry.Value.CusLastName + "\t" + entry.Value.CusFirstName;
                //ListBox1.Items.Add(entry.Value.CusID.ToString()) ;
                //datatable1.Items.Add(entry.Value.CusID.ToString());

                lblOrderID.Text += entry.Value.orderID.ToString() + "<br>" + "<br>";
                lblCustomerID.Text += entry.Value.order_CUSTOMERID.ToString() + "<br>" + "<br>";
                lblStoreName.Text += entry.Value.orderFrom_store.ToString() + "<br>" + "<br>";
                lblDeliveryType.Text += entry.Value.orderDeliveryType + "<br>" + "<br>";
                lblOrderType.Text += entry.Value.orderType + "<br>" + "<br>";
                lblProductType.Text += entry.Value.OrderProductType + "<br>" + "<br>";
                lblQuantity.Text += entry.Value.orderQuantity.ToString() + "<br>" + "<br>";
                lblPrice.Text += entry.Value.orderPrice.ToString() + "<br>" + "<br>";
                lblTotalAmount.Text += entry.Value.orderTotalAmount.ToString() + "<br>" + "<br>";
                lblDateOrder.Text += entry.Value.orderDateTime + "<br>" + "<br>";
                lblReservationDate.Text += entry.Value.OrderReservationDate + "<br>" + "<br>";
                //lblStatus.Text += entry.Value.OrderStatus + "<br>" + "<br>";
            }
        }
    }   
}