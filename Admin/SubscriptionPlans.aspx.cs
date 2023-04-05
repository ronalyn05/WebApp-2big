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

            // Get the ID of the currently logged-in owner from session state
            string idno = (string)Session["idno"];

            //CheckAdminStatus();
            DisplayBasic();
            DispplayPremium();
           

        }

        private void CheckAdminStatus()
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ADMIN/" + adminId);
            Model.AdminAccount pendingClients = response.ResultAs<Model.AdminAccount>();

            string clientStat = pendingClients.status;

            if (clientStat == "pending")
            {
                Response.Write("<script>alert ('Reminder: You can't proceed with the subscription yet since your account is under review. Please wait for your account to be approved before subscribing); location.reload(); window.location.href = '/Admin/WaitingPage.aspx'; </script>");

            }
        }
        private void DisplayBasic()
        {
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS");
            Dictionary<string, Model.SubscriptionPlans> plans = response.ResultAs<Dictionary<string, Model.SubscriptionPlans>>();

            foreach (var basicplan in plans)
            {
                var planName = basicplan.Value.planName;
                int idno = basicplan.Value.idno;

                if (planName == "BASIC")
                {
                    

                    FirebaseResponse res = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS/" + idno);
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

                    FirebaseResponse featuresponse = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS/" + idno + "/features");
                    List<string> features = featuresponse.ResultAs<List<string>>();

                    // Bind the features to the ListBox control
                    BasicfeaturesList.DataSource = features;
                    BasicfeaturesList.DataBind();

                    
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
                int idno = basicplan.Value.idno;

                if (planName == "PREMIUM PLAN")
                {


                    FirebaseResponse res = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS/" + idno);
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

                    FirebaseResponse featuresponse = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS/" + idno + "/features");
                    List<string> features = featuresponse.ResultAs<List<string>>();

                    // Bind the features to the ListBox control
                    premiumFeaturesList.DataSource = features;
                    premiumFeaturesList.DataBind();


                }
            }
        }

    }
}