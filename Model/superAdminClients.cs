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
        public int subscriptionID { get; set; }
        public string phone { get; set; }

        public string plan { get; set; }
        public int clientID { get; set; }

        public DateTime dateSubscribed { get; set; }
        public string status { get; set; }
        public int amount { get; set; }
        public DateTime subExpiration { get; set; }
        public string currentSubStatus { get; set; }
        
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
    public class Subscribed_Package
    {

        public DateTimeOffset subStart { get; set; }
        public string packageName { get; set; }
        public int packagePrice { get; set; }
        public DateTimeOffset expiration { get; set; }
        //public string currentSubscription { get; set; } //Active or Expired/Inactive
        public string subStatus { get; set; } //active or expired/inactive
        public string packageDescription { get; set; }
        public int orderLimit { get;set; }

    }
    public class Messages
    {
        public int messageID { get; set; }

        public string body { get; set; }
        public string sender {get;set;}
        public string receiver { get; set; }
        public int clientID { get; set; }
        public DateTime sent { get; set; } //time message sent
        public string status { get; set; } //read or unread

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
        public string messaging { get; set; }
        public int numberOfStations { get; set; }

        public List<string> features { get; set; } //to save the features in the db as a list

    }

    public class SuperAdminNotification
    {
        public int adminID { get; set; }

        public int admin_ID { get; set; }
        public string status { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }

        public int notificationID { get; set; }
        public string body { get; set; }
        public DateTimeOffset notificationDate { get; set; }
        public int orderID { get; set; }
        public int cusId { get; set; }
    }
}