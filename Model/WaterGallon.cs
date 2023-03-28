using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class WaterGallon
    {
        public int gallon_id { get; set; }
        public string gallonType { get; set; }
        public string Quantity { get; set; }
        public string DeliveryPrice { get; set; }
        public string PickUp_Price { get; set; }
        public string Image { get; set; }
        public DateTimeOffset DateAdded { get; set; }
    }
}