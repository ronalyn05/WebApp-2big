using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class DeliveryDetails
    {
        public int deliveryId { get; set; }
        public string deliveryType { get; set; }
        public string deliveryFee { get; set; }
        public string estimatedTime { get; set; }
        public string status { get; set; }

        // public DateTimeOffset DateAdded { get; set; }
    }
}