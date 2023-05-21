using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class Notification
    {
       
        public int admin_ID { get; set; } 
        public string status { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public int notificationID { get; set; }
        public string body { get; set; }
        public DateTime notificationDate { get; set; }
        public int orderID { get; set; }
        public int cusId { get; set; }
        public int driverId { get; set; }
        public string title { get; set; }      
       public DateTime scheduledSent { get; set; }

        //public int unreadCount { get; set; }

    }
    public class scheduledNotification
    {
        public int admin_ID { get; set; }
        public string status { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public int notificationID { get; set; }
        public string body { get; set; }
        public DateTime notificationDate { get; set; }
        public int orderID { get; set; }
        public int cusId { get; set; }
        public string title { get; set; }
        
        public DateTimeOffset scheduledSent { get; set; }
    }
}