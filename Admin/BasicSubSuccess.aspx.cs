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
    public partial class BasicSubSuccess : System.Web.UI.Page
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
            int adminId = int.Parse(idno);
            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ADMIN/" + adminId);
            Model.AdminAccount pendingClients = response.ResultAs<Model.AdminAccount>();

            string clientStat = pendingClients.status;

            if (clientStat == "pending")
            {
                Response.Write("<script>alert ('Subscription Unsuccessful. You cannot subscribe yet since your account is still under review. Please wait until your account is approved before you can subscribe'); location.reload(); window.location.href = '/Admin/WaitingPage.aspx'; </script>");

            }

        }
        public void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                FirebaseResponse response = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS");
                Dictionary<string, Model.SubscriptionPlans> plans = response.ResultAs<Dictionary<string, Model.SubscriptionPlans>>();

                foreach (var basicplan in plans)
                {
                    var planName = basicplan.Value.planName;
                    var planID = basicplan.Value.idno;

                    //to check if ang plan is niexist ba sa database
                    if (planName == "BASIC")
                    {
                        var idno = planID;
                        var adminID = Session["idno"].ToString();

                        
                        //to ADD the subscription Status to admin table
                        FirebaseResponse updateAdmin = twoBigDB.Get("ADMIN/" + adminID);
                        Model.AdminAccount update = updateAdmin.ResultAs<Model.AdminAccount>();

                        update.subStatus = "Subscribed";
                        //subscriptionStatus.subStatus = "Subscribed";

                        updateAdmin = twoBigDB.Update("ADMIN/" + adminID, update);


                        FirebaseResponse res = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS/" + idno);
                        Model.SubscriptionPlans obj = res.ResultAs<Model.SubscriptionPlans>();

                        //retrieval the data sa kana nga plan
                        string plan = obj.planName;
                        int basic = int.Parse(obj.planDuration);
                        int amount = int.Parse(obj.planPrice);

                        //declaration of the attributes
                        DateTime now = DateTime.Now;
                        DateTime SubBasic = now.AddMonths(basic);
                        var idnum = Session["idno"].ToString();
                        var firstname = Session["Fname"].ToString();
                        DateTime startDate = now;

                        var data = new Model.SubscribedPlan();

                        data.adminID = int.Parse(idnum);
                        data.subPlan = plan;
                        data.subStart = now;
                        data.subEnd = SubBasic;
                        data.price = amount;
                        

                        //SAVE THE SUBSCRIPTION PLAN DETAILS SA ADMIN NGA TABLE
                        twoBigDB.Update("ADMIN/" + idnum + "/SubscribedPlan/", data);

                        Session["subscribedPlan"] = plan;
                        Session["subscriptionEnd"] = SubBasic;
                        Session["subscriptionStart"] = now;


                        //to generate random ID
                        Random rnd = new Random();
                        int clientNo = rnd.Next(1, 10000);

                        //ISAVE SA SUPERADMIN NGA DATABASE ANG AMGA CLIENTS NGA NISUBSCRIBE 
                        var fullname = Session["fullName"];
                        var contact = Session["contactNumber"];
                        var email = Session["email"];
                        DateTime dateSubscribed = (DateTime)Session["subscriptionStart"];
                         
                        var clients = new Model.superAdminClients
                        {
                            idno = clientNo,
                            clientID = int.Parse(idnum),
                            fullname = fullname.ToString(),
                            email = email.ToString(),
                            phone = contact.ToString(),
                            plan = plan,
                            dateSubscribed = dateSubscribed,
                            status = "Subscribed"
                        };



                        SetResponse clientres;
                        //Storing the info of the admin nga ni subscribe to superadmin
                        clientres = twoBigDB.Set("SUPERADMIN/SUBSCRIBING_CLIENTS/" + clients.idno, clients);//Storing data to the database
                        Model.superAdminClients client = clientres.ResultAs<Model.superAdminClients>();//Database Result


                        Response.Write("<script>alert ('Thankyou for Subscribing ! You successfully subscribed to: " + data.subPlan + " '); window.location.href = '/Admin/AdminProfile.aspx'; </script>");
                    }

                }


            }
            catch
            {
                Response.Write("<script>alert ('Subscription Unsuccessfull'); location.reload(); window.location.href = '/Admin/BasicPlanSub.aspx'; </script>");
            }

        }
    }
}