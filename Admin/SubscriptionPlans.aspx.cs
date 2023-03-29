using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace WRS2big_Web.Admin
{
    public partial class SubscriptionPlans : System.Web.UI.Page
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient twoBigDB;

        protected void Page_Load(object sender, EventArgs e)
        {
            //connection to database 
            twoBigDB = new FireSharp.FirebaseClient(config);

            DisplayBasic();
            DispplayPremium();
        }

        private void DisplayBasic()
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS");
            Dictionary<string, Model.SubscriptionPlans> plans = response.ResultAs<Dictionary<string, Model.SubscriptionPlans>>();

            foreach (var basicplan in plans)
            {
                var planName = basicplan.Value.planName;

                if (planName == "BASIC")
                {
                    var baPlan = planName;

                    FirebaseResponse res = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS/" + baPlan);
                    Model.SubscriptionPlans obj = res.ResultAs<Model.SubscriptionPlans>();

                    //retrieval from database
                    string plan = obj.planName;
                    int basic = int.Parse(obj.planDuration);
                    int amount = int.Parse(obj.planPrice);
                    string description = obj.planDes;

                    basinplanLabel.Text = plan;
                    descriptionBasic.Text = description;
                    basicPriceLabel.Text = amount.ToString() + " " + "Php";
                    basicDurationLabel.Text = basic.ToString() + " " + "month";

                }
            }
             

        }

        private void DispplayPremium()
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS");
            Dictionary<string, Model.SubscriptionPlans> plans = response.ResultAs<Dictionary<string, Model.SubscriptionPlans>>();

            foreach (var basicplan in plans)
            {
                var planName = basicplan.Value.planName;
                //var planIdno = basicplan.Key;

                if (planName == "PREMIUM PLAN")
                {
                    var preplan = planName;

                    FirebaseResponse res = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS/" + preplan);
                    Model.SubscriptionPlans obj = res.ResultAs<Model.SubscriptionPlans>();

                    //retrieval from database
                    string plan = obj.planName;
                    int basic = int.Parse(obj.planDuration);
                    int amount = int.Parse(obj.planPrice);
                    string description = obj.planDes;

                    premiumplanLabel.Text = plan;
                    descriptionPremium.Text = description;
                    premiumPriceLabel.Text = amount.ToString() + " " + "Php";
                    premiumDurationLabel.Text = basic.ToString() + " " + "month";




                }

            }
        }

    }
}