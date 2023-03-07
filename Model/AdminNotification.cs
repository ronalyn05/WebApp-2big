using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class AdminNotification
    {
        public int adminnotificationID { get; set; }
        public int orderID { get; set; }
        public string OrderStatus { get; set; }
    }
}