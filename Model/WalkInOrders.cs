using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class WalkInOrders
    {
        public int orderNo { get; set; }
        public string productName { get; set; }
        public string productSize { get; set; }
        public string productQty { get; set; }
        public string productPrice { get; set; }
        public string productDiscount { get; set; }
        public string totalAmount { get; set; }
        public string orderType { get; set; }
        public DateTimeOffset dateAdded { get; set; }
    }
}