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
}