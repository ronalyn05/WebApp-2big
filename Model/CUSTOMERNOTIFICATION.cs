using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class CUSTOMERNOTIFICATION
    {
        public int notifID { get; set; }
        public int orderID { get; set; }
        public int order_CUSTOMERID { get; set; }
        public string orderFrom_store { get; set; }
        public string orderDeliveryType { get; set; }
        public string orderType { get; set; }
        public string OrderProductType { get; set; }
        public int orderQuantity { get; set; }
        public decimal orderPrice { get; set; }
        public decimal orderTotalAmount { get; set; }
        public string OrderReservationDate { get; set; }
        public DateTime orderDateTime { get; set; }
        public string OrderStatus { get; set; }
    }
}