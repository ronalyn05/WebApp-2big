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
        public string paymentStatus { get; set; }
        public string type { get; set; }
       // public string type { get; set; } //SUBSCRIPTION, RENEWAL
        
    }
    public class SuperAccount
    {
        public int superIDno { get; set; }
        public string lname { get; set; }
        public string fname { get; set; }
        public string mname { get; set; }
        public string bdate { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public string address { get; set; }
        public DateTime dateRegistered { get; set; }
        public string userRole { get; set; }


    }
    public class superLogs
    {
        public int logsId { get; set; }
        public int superID { get; set; }
        public string superFullname { get; set; }
        public string superActivity { get; set; }
        public DateTimeOffset activityTime { get; set; }

    }

    public class subscriptionLogs
    {
        public int logsId { get; set; }
        public int userIdnum { get; set; }
        public string userFullname { get; set; }
        public string userActivity { get; set; }
        public DateTime activityTime { get; set; }
        public decimal total { get; set; }
        public string packageName { get; set; }
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
        public int packageID { get; set; }
        public DateTime subStart { get; set; }
        public string packageName { get; set; }
        public int packagePrice { get; set; }
        public DateTime expiration { get; set; }
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
        public double packagePrice { get; set; }
        public string durationType { get; set; }
        public int packageDuration { get; set; }
        public int productLimit { get; set; }
        public int packageLimit { get; set; }
        public string messaging { get; set; }
        public int numberOfStations { get; set; }
        public string renewable { get; set; }
        public List<string> features { get; set; } //to save the features in the db as a list
        public string status { get; set; }
       public DateTime dateUpdated { get; set; }
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