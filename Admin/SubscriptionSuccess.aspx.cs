﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class SubscriptionSuccess : System.Web.UI.Page
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

            if (clientStat == "Subscribed")
            {
                subSuccessTitle.InnerText = "Subscription Renewal";
            }

            if (clientStat == "pending")
            {
                Response.Write("<script>alert ('Subscription Unsuccessful. You cannot subscribe yet since your account is still under review. Please wait until your account is approved before you can subscribe'); location.reload(); window.location.href = '/Admin/WaitingPage.aspx'; </script>");

            }
            if (Request.QueryString["packageID"] != null)
            {
                string selectedPackageID = Request.QueryString["packageID"];
                Debug.WriteLine($"PACKAGE ID: {selectedPackageID}");
                //Session["packageID"] = selectedPackageID;

                FirebaseResponse getPackage = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + selectedPackageID);
                Model.PackagePlans package = getPackage.ResultAs<Model.PackagePlans>();

                if (package != null)
                {
                    packageName.Text = package.packageName;
                    packagedescription.Text = package.packageDescription;
                    packagePrice.Text = package.packagePrice.ToString();
                    packageDuration.Text = package.packageDuration + " " + package.durationType;
                }
            }

            //POPULATE THE PACKAGE DETAILS IN THE CONFIRMATION PAGE
            //int packageID = (int)Session["packageID"];

            


        }
        public void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                var adminID = Session["idno"].ToString();

                if (adminID != null)
                {

                            // FOR NEW SUBSCRIPTIONS
                             string selectedPackageID = Request.QueryString["packageID"];
                             int packageID = int.Parse(selectedPackageID);

                            FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES");
                            Dictionary<string, Model.PackagePlans> plans = response.ResultAs<Dictionary<string, Model.PackagePlans>>();

                            if (plans != null)
                            {
                                foreach (var packages in plans)
                                {
                                    var planName = packages.Value.packageName;
                                    var planID = packages.Value.packageID;

                                    //check what plan does the packageID matches
                                    if (packageID == planID)
                                    {
                                        FirebaseResponse res = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID);
                                        Model.PackagePlans package = res.ResultAs<Model.PackagePlans>();

                                        //var adminID = Session["idno"].ToString();      

                                        //to ADD the subscription Status to admin table
                                        FirebaseResponse updateAdmin = twoBigDB.Get("ADMIN/" + adminID);
                                        Model.AdminAccount update = updateAdmin.ResultAs<Model.AdminAccount>();

                                        if (update.subStatus != "notSubscribed")
                                        {  //PROCEED TO RENEWAL
                                            checkforRenewal();
                                        }
                                        //PROCEED TO NEW SUBSCRIPTION
                                        else
                                        {
                                            update.subStatus = "Subscribed";
                                            update.currentSubscription = "Active";
                                            //subscriptionStatus.subStatus = "Subscribed";

                                            updateAdmin = twoBigDB.Update("ADMIN/" + adminID, update);



                                            //declaration of the attributes
                                            DateTime now = DateTime.Now;

                                            var data = new Model.Subscribed_Package();

                                            //data.currentSubscription = "Active";
                                            data.expiration = DateTime.Now.AddMonths(package.packageDuration); //add the current month to the duration saved in the database
                                            data.packageName = package.packageName;
                                            data.packageDescription = package.packageDescription;
                                            data.packagePrice = (int)package.packagePrice;
                                            data.subStart = DateTime.Now;
                                            data.subStatus = "Active";
                                            data.packageID = package.packageID;
                                            data.orderLimit = package.packageLimit;


                                            //SAVE THE SUBSCRIPTION PLAN DETAILS SA ADMIN NGA TABLE
                                            twoBigDB.Update("ADMIN/" + adminID + "/Subscribed_Package/", data);

                                            DateTime subExpired = now.AddMonths(package.packageDuration);

                                            //end for the ADMIN

                                            //SAVING TO SUPERADMIN TABLE - DETAILS OF THE ADMIN AND THE PACKAGE SUBSCRIBED
                                            //to generate random ID for the SUPERADMIN TABLE
                                            Random rnd = new Random();
                                            int clientNo = rnd.Next(1, 10000);

                                            //ISAVE SA SUPERADMIN NGA DATABASE ANG AMGA CLIENTS NGA NISUBSCRIBE 
                                            var newClients = new Model.superAdminClients
                                            {
                                                subscriptionID = clientNo,
                                                clientID = int.Parse(adminID),
                                                fullname = update.fname + " " + update.mname + " " + update.lname,
                                                email = update.email,
                                                phone = update.phone,
                                                amount = (int)package.packagePrice,
                                                plan = package.packageName,
                                                currentSubStatus = "Active",
                                                dateSubscribed = DateTime.Now,
                                                subExpiration = subExpired,
                                                paymentStatus = "Completed",
                                                status = "Subscribed",
                                                type = "New Subscription"

                                            };

                                            SetResponse clientres;
                                            //Storing the info of the admin nga ni subscribe to superadmin
                                            clientres = twoBigDB.Set("SUBSCRIBED_CLIENTS/" + clientNo, newClients);//Storing data to the database
                                            Model.superAdminClients client = clientres.ResultAs<Model.superAdminClients>();//Database Result

                                            //SEND ANOTHER NOTIFICATION FOR THE INSTRUCTIONS
                                            int newID = rnd.Next(1, 20000);
                                            var newNotif = new Model.Notification
                                            {
                                                admin_ID = client.clientID,
                                                sender = "Super Admin",
                                                title = "Subscription Success",
                                                receiver = "Admin",
                                                body = "Thankyou for Subscribing to our platform. You subscribed to" + " " + package.packageName + ". Set-up your Profile and Refilling Station now to start growing your business!",
                                                notificationDate = DateTime.Now,
                                                status = "unread",
                                                notificationID = newID

                                            };
                                            SetResponse notifResponse;
                                            notifResponse = twoBigDB.Set("NOTIFICATION/" + newID, newNotif);//Storing data to the database
                                            Model.Notification notif = notifResponse.ResultAs<Model.Notification>();//Database Result

                                            //SEND NOTIFICATION TO SUPER ADMIN
                                            int notifID = rnd.Next(1, 20000);
                                            var superNotif = new Model.Notification
                                            {
                                                admin_ID = int.Parse(adminID),
                                                sender = "Admin",
                                                title = "Client Subscription",
                                                receiver = "Super Admin",
                                                body = "CLIENT"+ " " + update.fname + " " + update.lname + " " + "SUBSCRIBED TO" + " " + package.packageName,
                                                notificationDate = DateTime.Now,
                                                status = "unread",
                                                notificationID = notifID

                                            };
                                            SetResponse superResponse;
                                            superResponse = twoBigDB.Set("NOTIFICATION/" + notifID, superNotif);//Storing data to the database
                                            Model.Notification notifsuper = superResponse.ResultAs<Model.Notification>();//Database Result


                                            int logsID = rnd.Next(1, 10000);
                                            // Get the current date and time
                                            DateTime addedTime = DateTime.Now;

                                            // Log user activity
                                            var log = new Model.UsersLogs
                                            {
                                                userIdnum = int.Parse(adminID),
                                                logsId = logsID,
                                                userFullname = (string)Session["fullname"],
                                                userActivity = "PACKAGE SUBSCRIPTION:" + " " + package.packageName,
                                                activityTime = addedTime
                                            };

                                            twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                                            Model.UsersLogs subsLog = response.ResultAs<Model.UsersLogs>();


                                            //SEND USERS LOG TO SUPERADMIN
                                            DateTime logTime = DateTime.UtcNow; //Get the current date and time

                                            //generate a random number for users logged
                                            //Random rnd = new Random();
                                            int idnum = rnd.Next(1, 10000);

                                            string superName = (string)Session["name"];

                                           
                                            var superLog = new Model.subscriptionLogs
                                            {
                                                logsId = idnum,
                                                userIdnum = update.idno,
                                                userFullname = update.fname + " " + update.lname,
                                                userActivity = "NEW SUBSCRIPTION",
                                                packageName = package.packageName,
                                                activityTime = logTime,
                                                total = (decimal)package.packagePrice

                                            };

                                            //Storing the  info
                                            response = twoBigDB.Set("SUBSCRIPTION_LOGS/" + superLog.logsId, superLog);//Storing data to the database
                                            Model.subscriptionLogs superRes = response.ResultAs<Model.subscriptionLogs>();//Database Result

                                            Response.Write("<script>alert ('Subscription Success!'); window.location.href = '/Admin/AdminProfile.aspx'; </script>");
                                }
                                        
                                    }

                                }
                                
                                //checkforRenewal();

                            }
                }
                else
                {
                    Response.Write("<script>alert ('Session Expired! Please login again');  window.location.href = '/LandingPage/Account.aspx'; </script>");
                }

            }
            catch
            {
                Response.Write("<script>alert ('Subscription Unsuccessfull'); location.reload(); window.location.href = '/Admin/SubscriptionPackages.aspx'; </script>");
            }

        }

        //TO PERFORM RENEWAL
        private void checkforRenewal()
        {
            var adminID = Session["idno"].ToString();
           

            FirebaseResponse currentStat = twoBigDB.Get("SUBSCRIBED_CLIENTS/");
            Dictionary<string, Model.superAdminClients> subscribed = currentStat.ResultAs<Dictionary<string, Model.superAdminClients>>();

            if (subscribed != null)
            {

                    string selectedPackageID = Request.QueryString["packageID"];
                    int packageID = int.Parse(selectedPackageID);

                    FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES");
                    Dictionary<string, Model.PackagePlans> plans = response.ResultAs<Dictionary<string, Model.PackagePlans>>();

                    foreach (var packages in plans)
                    {
                        var planName = packages.Value.packageName;
                        var planID = packages.Value.packageID;

                        //check what plan does the packageID matches
                        if (packageID == planID)
                        {
                            FirebaseResponse res = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID);
                            Model.PackagePlans package = res.ResultAs<Model.PackagePlans>();

                        //FirebaseResponse current = twoBigDB.Get("ADMIN/" + adminID);
                        //Model.AdminAccount checkStat = current.ResultAs<Model.AdminAccount>();
                        FirebaseResponse updateAdmin = twoBigDB.Get("ADMIN/" + adminID);
                        Model.AdminAccount update = updateAdmin.ResultAs<Model.AdminAccount>();

                        //TO CHECK IF THAT ADMIN IS FOR RENEWAL OR NEW SUBSCRIPTION
                        if (update.subStatus == "Subscribed" || update.subStatus == "Expired")
                          {
                              if (update.subStatus == "Subscribed" || update.subStatus == "Active")
                              {
                                //PERFORM RENEWAL
                                //to ADD the NEW subscription Status to admin table
                                update.subStatus = "Subscribed";
                                update.currentSubscription = "Active";
                                //subscriptionStatus.subStatus = "Subscribed";

                                updateAdmin = twoBigDB.Update("ADMIN/" + adminID, update);



                                //declaration of the attributes
                                DateTime now = DateTime.Now;

                                var data = new Model.Subscribed_Package();

                                //data.currentSubscription = "Active";
                                data.expiration = DateTime.Now.AddMonths(package.packageDuration); //add the current month to the duration saved in the database
                                data.packageName = package.packageName;
                                data.packageDescription = package.packageDescription;
                                data.packagePrice = (int)package.packagePrice;
                                data.subStart = DateTime.Now;
                                data.subStatus = "Active";
                                data.orderLimit = package.packageLimit;


                                //SAVE THE SUBSCRIPTION PLAN DETAILS SA ADMIN NGA TABLE
                                twoBigDB.Update("ADMIN/" + adminID + "/Subscribed_Package/", data);

                                DateTime subExpired = now.AddMonths(package.packageDuration);

                                //end for the ADMIN

                                //SAVING TO SUPERADMIN TABLE - DETAILS OF THE ADMIN AND THE PACKAGE SUBSCRIBED
                                //to generate random ID for the SUPERADMIN TABLE
                                Random rnd = new Random();
                                int clientNo = rnd.Next(1, 10000);

                                //ISAVE SA SUPERADMIN NGA DATABASE ANG AMGA CLIENTS NGA NISUBSCRIBE 
                                var updateClients = new Model.superAdminClients
                                {
                                    subscriptionID = clientNo,
                                    clientID = int.Parse(adminID),
                                    fullname = update.fname + " " + update.mname + " " + update.lname,
                                    email = update.email,
                                    phone = update.phone,
                                    amount = (int)package.packagePrice,
                                    plan = package.packageName,
                                    currentSubStatus = "Active",
                                    dateSubscribed = DateTime.Now,
                                    subExpiration = subExpired,
                                    paymentStatus = "Completed",
                                    status = "Subscribed"

                                };

                                //UPDATE THE SUBSCRIBED CLIENTS TABLE SINCE RENEWAL NA
                                twoBigDB.Update("SUBSCRIBED_CLIENTS/" + adminID, updateClients);//Storing data to the database
                                                                                                //Model.superAdminClients client = clientres.ResultAs<Model.superAdminClients>();//Database Result

                                //SEND ANOTHER NOTIFICATION FOR THE INSTRUCTIONS
                                int newID = rnd.Next(1, 20000);
                                var newNotif = new Model.Notification
                                {
                                    admin_ID = int.Parse(adminID),
                                    sender = "Super Admin",
                                    title = "Renewal Success",
                                    receiver = "Admin",
                                    body = "Thankyou for subscribing again! You can now continue growing your business with 2BiG Platform!",
                                    notificationDate = DateTime.Now,
                                    status = "unread",
                                    notificationID = newID

                                };
                                SetResponse notifResponse;
                                notifResponse = twoBigDB.Set("NOTIFICATION/" + newID, newNotif);//Storing data to the database
                                Model.Notification notif = notifResponse.ResultAs<Model.Notification>();//Database Result

                                //create logs
                                int logsId = (int)Session["logsId"];
                                // Retrieve the existing Users log object from the database
                                FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
                                Model.UsersLogs existingLog = resLog.ResultAs<Model.UsersLogs>();

                                // Get the current date and time
                                DateTime addedTime = DateTime.Now;

                                // Log user activity
                                var log = new Model.UsersLogs
                                {
                                    userIdnum = int.Parse(adminID),
                                    logsId = logsId,
                                    userFullname = (string)Session["fullname"],
                                    userActivity = "SUBSCRIPTION RENEWED:" + " " + package.packageName,
                                    activityTime = addedTime
                                };
                                twoBigDB.Update("ADMINLOGS/" + log.logsId, log);

                                //SEND USERS LOG TO SUPERADMIN
                                DateTime logTime = DateTime.UtcNow; //Get the current date and time

                                //generate a random number for users logged
                                //Random rnd = new Random();
                                int idnum = rnd.Next(1, 10000);

                                string superName = (string)Session["name"];

                                //Store the login information in the USERLOG table
                                var superLog = new Model.subscriptionLogs
                                {
                                    logsId = idnum,
                                    userIdnum = update.idno,
                                    userFullname = update.fname,
                                    userActivity = "SUBSCRIPTION RENEWAL",
                                    packageName = package.packageName,
                                    activityTime = logTime,
                                    total = (decimal)package.packagePrice
                                };

                                //Storing the  info
                                response = twoBigDB.Set("SUBSCRIPTION_LOGS/" + superLog.logsId, superLog);//Storing data to the database
                                Model.UsersLogs superRes = response.ResultAs<Model.UsersLogs>();//Database Result


                                //SEND NOTIFICATION TO SUPER ADMIN
                                int notifID = rnd.Next(1, 20000);
                                var superNotif = new Model.Notification
                                {
                                    admin_ID = int.Parse(adminID),
                                    sender = "Admin",
                                    title = "Client Renewal",
                                    receiver = "Super Admin",
                                    body = "CLIENT" + update.fname + " " + update.lname + " " + "RENEWED HIS SUBSCRIPTION",
                                    notificationDate = DateTime.Now,
                                    status = "unread",
                                    notificationID = notifID

                                };
                                SetResponse superResponse;
                                superResponse = twoBigDB.Set("NOTIFICATION/" + newID, superNotif);//Storing data to the database
                                Model.Notification notifsuper = superResponse.ResultAs<Model.Notification>();//Database Result

                                Response.Write("<script>alert ('Renewal Success!'); window.location.href = '/Admin/AdminProfile.aspx'; </script>");
                            }

                        }

 
                        }

                    }
                
            }
        }
    }
}