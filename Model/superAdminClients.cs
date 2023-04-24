using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class superAdminClients
    {

        public string fullname { get; set; }
        public string email { get; set; }
        public int idno { get; set; }
        public string phone { get; set; }

        public string plan { get; set; }
        public int clientID { get; set; }

        public DateTime dateSubscribed { get; set; }
        public string status { get; set; }
        public int amount { get; set; }
        
    }

    public class SubscriptionPlans
    {
        public int idno { get; set; }
        public string planName { get; set; }
        public string planDuration { get; set; }
        public string planDes { get; set; }
        public string planPrice { get; set; }
        public List<string> features { get; set; } //to save the features in the db as a list

    }
    public class PackagePlans
    {
        public int packageID { get; set; }
        public string packageName { get; set; }
        public string packageDescription { get; set; }
        public int packagePrice { get; set; }
        public string durationType { get; set; }
        public int packageDuration { get; set; }

        public int packageLimit { get; set; }
        public List<string> features { get; set; } //to save the features in the db as a list

    }

    public class SuperAdminNotification
    {
        public int adminID { get; set; }
        public string status { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }

        public int notificationID { get; set; }
        public string body { get; set; }
        public DateTimeOffset notificationDate { get; set; }
        

    }
}