using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class Product
    {
        public int productId { get; set; }
        public string productType { get; set; }
        public string productSize { get; set; }
        public string productPrice { get; set; }
        public string productAvailable { get; set; }
        public DateTimeOffset DateAdded { get; set; }
    }
}