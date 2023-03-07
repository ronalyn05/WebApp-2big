using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class RefillProduct
    {
        public int refill_id { get; set; }
        public string gallonType { get; set; }
        public string waterType { get; set; }
        public string refillName { get; set; }
        public string DeliveryPrice { get; set; }
        public string PickUp_Price { get; set; }
        public string Image { get; set; }
        public DateTimeOffset DateAdded { get; set; }
    }
}