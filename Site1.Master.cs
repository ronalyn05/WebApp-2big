﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using WRS2big_Web.Model;

namespace WRS2big_Web
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient twoBigDB;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Connection to database
            twoBigDB = new FireSharp.FirebaseClient(config);


            if (Session["idno"] == null)
            {
                // User not found

                Response.Write("<script>alert ('Session expired. Please login again');window.location.href = '/LandingPage/Account.aspx';</script>");
                //Response.Redirect("/LandingPage/Account.aspx");
            }
            else
            {
                string stationName = (string)Session["stationName"];
                lblWRSname.Text = stationName;

                string adminID = Session["idno"].ToString();
                FirebaseResponse response;
                response = twoBigDB.Get("ADMIN/" + adminID);
                AdminAccount user = response.ResultAs<AdminAccount>(); //Database result

                //TO REMOVE THE SIDE NAVBARS 
                if (user.status == "pending")
                {
                    navigationBarMaster.Visible = false;
                } 
                if (user.subStatus == "notSubscribed")
                {
                    navigationBarMaster.Visible = false;
                }
                
            }

           



            loadNotifications();
            checkStationStatus();
            //SubscriptionStatus();
            //reminderNotification();

         
        }
        private void checkStationStatus()
        {
            var adminID = Session["idno"].ToString();

            //TO GET THE REFILLING STATION DETAILS
            FirebaseResponse stationDetails = twoBigDB.Get("ADMIN/" + adminID + "/RefillingStation/");
            Model.RefillingStation refillStation = stationDetails.ResultAs<Model.RefillingStation>();

            //string name = refillStation.stationName;
            string days = refillStation.businessDaysFrom;

            //to check if naka add na ug station details
            if (days != null)
            {


                DateTime timeNow = DateTime.Now;
                DateTime operatingHrsFrom = DateTime.Parse(refillStation.operatingHrsFrom);
                DateTime operatingHrsTo = DateTime.Parse(refillStation.operatingHrsTo);
                //string businessClose = refillStation.businessDaysTo;
                ////method to convert the businessDaysTo from DB value to a DayOfWeek enum value
                //DayOfWeek storeClose = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), businessClose);

                // Check if the current day is within the business days
                DayOfWeek currentDayOfWeek = DateTime.Today.DayOfWeek;
                DayOfWeek businessDayFrom = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), refillStation.businessDaysFrom);
                DayOfWeek businessDayTo = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), refillStation.businessDaysTo);

                //convert into INT
                int currentDay = (int)currentDayOfWeek;
                int businessDayStart = (int)businessDayFrom;
                int businessDayEnd = (int)businessDayTo;

                bool isBusinessDay = false;
                // check if the businessDayFrom is less than or equal to businessDayTo
                if (businessDayStart <= businessDayEnd)
                {
                    //check if the current day is within the range using the >= and <= operators.
                    isBusinessDay = (currentDay >= businessDayStart && currentDay <= businessDayEnd);
                }
                else
                {//check if the current day is greater than or equal to businessDayFrom OR less than or equal to businessDayTo to determine if it is within the range.
                    isBusinessDay = (currentDay >= businessDayStart || currentDay <= businessDayEnd);
                }



                //DEBUG STATEMENTS
                Debug.WriteLine($"currentDayOfWeek: {currentDayOfWeek}");
                Debug.WriteLine($"businessDayFrom: {businessDayFrom}");
                Debug.WriteLine($"businessDayTo: {businessDayTo}");
                Debug.WriteLine($"isBusinessDay: {isBusinessDay}");

                //TO CHECK IF THE CURRENT TIME IS WITHIN THE OPERATING HOURS SET BY THE ADMIN
                string status = "CLOSE";
                //lblstatus.Text = status;
                if (isBusinessDay && timeNow >= operatingHrsFrom && timeNow <= operatingHrsTo)
                {
                    status = "OPEN";
                }
               // lblstatus.Text = status;

                var statusUpdate = new Dictionary<string, object>
                {
                  { "status", status }
                };  

                FirebaseResponse response = twoBigDB.Update("ADMIN/" + adminID + "/RefillingStation/", statusUpdate);
            }
        }

        private void SubscriptionStatus()
        {
            string adminID = Session["idno"].ToString();

            if (adminID == null && Session["idno"] == null)
            {
                Response.Write("<script> alert ('Session Expired! Please login again'); window.location.href = '/LandingPage/Account.aspx';</script>");
            }
            else
            {
               
                FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + adminID + "/Subscribed_Package");
                Model.Subscribed_Package expiration = adminDet.ResultAs<Model.Subscribed_Package>();

                if (expiration != null)
                {

                        //UPDATE THE SUBSTATUS OF SUBSCRIBED_PACKAGE FROM ACTIVE TO EXPIRED
                       adminDet = twoBigDB.Get("ADMIN/" + adminID + "/Subscribed_Package");
                        Model.Subscribed_Package subStatus = adminDet.ResultAs<Model.Subscribed_Package>();
                        
                        if (subStatus != null)
                        {
                        DateTime currentDate = DateTime.Now.Date;
                        DateTime reminderNotif = expiration.expiration.AddDays(-2); //2 days before the expiration
                        DateTime expirationDate = expiration.expiration; //actual expiration date on database
                        DateTime finalExpiration = expiration.expiration.AddDays(2); //final expiration !

                            Debug.WriteLine($"NOW: {currentDate}");
                            Debug.WriteLine($"REMINDER: {reminderNotif}");
                            Debug.WriteLine($"EXPIRATION: {expirationDate}");
                            Debug.WriteLine($"final EXPIRATION: {finalExpiration}");

                        if (currentDate.Date == reminderNotif.Date)
                        {
                            int admin = int.Parse(adminID);

                            FirebaseResponse adminNotif = twoBigDB.Get("NOTIFICATION");
                            var adminBody = adminNotif.Body;
                            Dictionary<string, Model.Notification> adminAllNotifs = JsonConvert.DeserializeObject<Dictionary<string, Model.Notification>>(adminBody);

                            // Check if there is already a Subscription Reminder notification for this admin_ID
                            bool hasSubscriptionReminder = adminAllNotifs.Values.Any(n => n.admin_ID == admin && n.title == "Subscription Reminder");

                            if (!hasSubscriptionReminder)
                            {
                                //SEND FIRST NOTIFICATION TO ADMIN FOR SUBSCRIPTION REMINDER
                                Random rnd = new Random();
                                int ID = rnd.Next(1, 20000);
                                var Notification = new Notification
                                {
                                    admin_ID = int.Parse(adminID),
                                    sender = "Super Admin",
                                    title = "Subscription Reminder",
                                    receiver = "Admin",
                                    body = "Your subscription is about to expire in 2 days! Don't forget to renew your subscription to continue using the platform.",
                                    notificationDate = DateTime.Now,
                                    status = "unread",
                                    notificationID = ID
                                };

                                SetResponse notifResponse;
                                notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification); //Storing data to the database
                                Notification notif = notifResponse.ResultAs<Notification>(); //Database Result

                                //Debug.WriteLine($"NOW: {DateTime.Now}");
                                Debug.WriteLine($"REMINDER SENT: {ID}");
                            }
                        }
                        else if (currentDate.Date == expirationDate.Date)
                            {
                            int admin = int.Parse(adminID);

                            FirebaseResponse adminNotif = twoBigDB.Get("NOTIFICATION");
                            var adminBody = adminNotif.Body;
                            Dictionary<string, Model.Notification> adminAllNotifs = JsonConvert.DeserializeObject<Dictionary<string, Model.Notification>>(adminBody);

                            // Check if there is already a Subscription Reminder notification for this admin_ID
                            bool hasSubscriptionReminder = adminAllNotifs.Values.Any(n => n.admin_ID == admin && n.title == "Subscription Extended");
                            
                            if (!hasSubscriptionReminder)
                            {
                                //SEND SECOND NOTIFICATION TO ADMIN FOR SUBSCRIPTION REMINDER
                                Random rnd = new Random();
                                int ID = rnd.Next(1, 20000);
                                var Notification = new Notification
                                {
                                    admin_ID = int.Parse(adminID),
                                    sender = "Super Admin",
                                    title = "Subscription Extended",
                                    receiver = "Admin",
                                    body = "Your subscription has expired today! However, as our valued client, you are given an extra 2 days before your account will be locked. Locked account means you can't perform transactions anymore. Don't forget to renew your subscription in your Profile page to continue using the platform.",
                                    notificationDate = DateTime.Now,
                                    status = "unread",
                                    notificationID = ID
                                };

                                SetResponse notifResponse;
                                notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification); //Storing data to the database
                                Notification notif = notifResponse.ResultAs<Notification>(); //Database Result

                            }

                        }

                        else if (currentDate.Date == finalExpiration.Date)

                        {
                            int admin = int.Parse(adminID);

                            FirebaseResponse adminNotif = twoBigDB.Get("NOTIFICATION");
                            var adminBody = adminNotif.Body;
                            Dictionary<string, Model.Notification> adminAllNotifs = JsonConvert.DeserializeObject<Dictionary<string, Model.Notification>>(adminBody);

                            // Check if there is already a Subscription Reminder notification for this admin_ID
                            bool hasSubscriptionReminder = adminAllNotifs.Values.Any(n => n.admin_ID == admin && n.title == "Subscription Expired");

                            if (!hasSubscriptionReminder)
                            {
                                //change the status in ADMIN table
                                subStatus.subStatus = "Expired";
                                adminDet = twoBigDB.Update("ADMIN/" + adminID + "/Subscribed_Package", subStatus);

                                //fetch admin data
                                FirebaseResponse client = twoBigDB.Get("ADMIN/" + adminID);
                                Model.AdminAccount clientstat = client.ResultAs<Model.AdminAccount>();
                                //updte
                                clientstat.currentSubscription = "Expired";
                                client = twoBigDB.Update("ADMIN/" + adminID, clientstat);

                                //fetch from superadmin table
                                FirebaseResponse superAdmin = twoBigDB.Get("SUBSCRIBED_CLIENTS/" + adminID);
                                Model.superAdminClients subscribed = superAdmin.ResultAs<Model.superAdminClients>();

                                //change the status in SUPERADMIN Table
                                subscribed.currentSubStatus = "Expired";
                                superAdmin = twoBigDB.Update("SUBSCRIBED_CLIENTS/" + adminID, subscribed);

                                //SEND LAST NOTIFICATION TO ADMIN FOR SUBSCRIPTION REMINDER
                                Random rnd = new Random();
                                int ID = rnd.Next(1, 20000);
                                var Notification = new Notification
                                {
                                    admin_ID = int.Parse(adminID),
                                    sender = "Super Admin",
                                    title = "Subscription Expired",
                                    receiver = "Admin",
                                    body = "Your subscription has expired! Please renew your subscription in the Profile Page to continue using the platform.",
                                    notificationDate = DateTime.Now,
                                    status = "unread",
                                    notificationID = ID
                                };

                                SetResponse notifResponse;
                                notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification); //Storing data to the database
                                Notification notif = notifResponse.ResultAs<Notification>(); //Database Result
                            }



                        }



                    }

                    
                    Debug.WriteLine($"NOW: {DateTime.Now}");
                    Debug.WriteLine($"DATE: {expiration.expiration}");
                }
            }


        }
        private void loadNotifications()
        {
            SubscriptionStatus();

            //NOTIFICATION FROM CUSTOMER TO ADMIN
            string adminID = (string)Session["idno"];

            if (Session["idno"] == null)
            {
                Response.Write("<script>alert('Session Expired. Please login again'); window.location.href = '/LandingPage/Account.aspx'; </script>");
            }

            if (adminID != null)
            {
                int admin = int.Parse(adminID);

                FirebaseResponse adminNotif = twoBigDB.Get("NOTIFICATION");
                var adminBody = adminNotif.Body;
                Dictionary<string, Model.Notification> adminAllNotifs = JsonConvert.DeserializeObject<Dictionary<string, Model.Notification>>(adminBody);

                //int unreadCount = 0;

                if (adminAllNotifs != null)
                {
                    // Create a list to store all the notifications with the receiver as "Admin"
                    List<Model.Notification> AdminNotifications = new List<Model.Notification>();

                    //List for unread notifications
                    List<Model.Notification> unreadNotifications = new List<Model.Notification>();

                    // Loop through all the notifications
                    foreach (KeyValuePair<string, Model.Notification> entry in adminAllNotifs)
                    {
                        // Check if the current notification has the receiver as "Admin"
                        if (entry.Value.receiver == "Admin" && entry.Value.admin_ID == admin)
                        {
                            // Add the current notification to the list of admin notifications
                            AdminNotifications.Add(entry.Value);

                            if (entry.Value.status == "unread")
                            {
                               
                                unreadNotifications.Add(entry.Value);

                            }
                        }
                    }

                    // Sort the super admin notifications based on dateAdded property in descending order
                    AdminNotifications = AdminNotifications.OrderByDescending(n => n.notificationDate).ToList();

                    // Bind the list of super admin notifications to the repeater control
                    rptNotifications.DataSource = AdminNotifications;
                    rptNotifications.DataBind();

                    int unreadCount = unreadNotifications.Count;
                    // return unreadCount;
                    unread.Text = unreadNotifications.Count.ToString();

                    if (unreadCount > 0)
                    {
                        unread.Visible = true;
                    }
                    else
                    {
                        unread.Visible = false;
                    }
                }
            }
        }


        //the notificationID is clicked
        protected void notifMsg_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;
            string notificationID = (sender as LinkButton).CommandArgument;

            int idnum = int.Parse(notificationID);
            // Retrieve the existing Notifications object from the database

            FirebaseResponse notification = twoBigDB.Get("NOTIFICATION/" + idnum);
            Notification notif = notification.ResultAs<Notification>();

            string title = notif.title;

            if (title == "Application Approved")
            {
                var updatedNotif = new Notification
                {

                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/SubscriptionPackages.aspx'; </script>");
            }
            //NEED TO ANALYZE UNSAY BUHATON SA ADMIN IF DECLINED IYA APPLICATION
            else if (title == "Application Declined")
            {
                var updatedNotif = new Notification
                {

                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/SubscriptionPlans.aspx'; </script>");
            }
            else if (title == "New Order")
            {
                var updatedNotif = new Notification
                {
                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/OnlineOrders.aspx'; </script>");
            }
            else if (title == "Order Status")
            {
                var updatedNotif = new Notification
                {
                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/OnlineOrders.aspx'; </script>");
            }
            else if (title == "Subscription Success")
            {
                var updatedNotif = new Notification
                {
                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/AdminProfile.aspx'; </script>");
            }
            else if (title == "Welcome to 2BiG!")
            {
                var updatedNotif = new Notification
                {
                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/WaitingPage.aspx'; </script>");
            }
            else if (title == "Set-up Profile")
            {
                var updatedNotif = new Notification
                {
                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/AdminProfile.aspx'; </script>");
            }
            else if (title == "Subscription Expired")
            {
                var updatedNotif = new Notification
                {
                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/AdminProfile.aspx'; </script>");
            }
            else if (title == "Station update")
            {
                var updatedNotif = new Notification
                {
                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/AdminProfile.aspx'; </script>");
            }
            else if (title == "Refilling Station Pre-setup")
            {
                var updatedNotif = new Notification
                {
                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/WaitingPage.aspx'; </script>");
            }
            else if (title == "Refilling Station Set-up")
            {
                var updatedNotif = new Notification
                {
                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/AdminProfile.aspx'; </script>");
            }
            else if (title == "Subscription Reminder")
            {
                var updatedNotif = new Notification
                {
                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/AdminProfile.aspx'; </script>");
            }
            else if (title == "Subscription Extended")
            {
                var updatedNotif = new Notification
                {
                    notificationID = notif.notificationID,
                    notificationDate = notif.notificationDate,
                    receiver = notif.receiver,
                    sender = notif.sender,
                    title = notif.title,
                    orderID = notif.orderID,
                    cusId = notif.cusId,
                    driverId = notif.driverId,
                    //UPDATE THE STATUS FROM UNREAD TO READ
                    status = "read",
                    body = notif.body,
                    admin_ID = notif.admin_ID
                };
                notification = twoBigDB.Update("NOTIFICATION/" + idnum, updatedNotif);
                Response.Write("<script>window.location.href = '/Admin/AdminProfile.aspx'; </script>");
            }

        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            // Get the log ID from the session
            int logsId = (int)Session["logsId"];

            // Retrieve the existing Users log object from the database
            FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
            UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

            //Get the current date and time
            DateTime addedTime = DateTime.Now;

            // Log user activity
            var log = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = logsId,
                userFullname = (string)Session["fullname"],
                userActivity = "LOGGED OUT",
                activityTime = addedTime
            };

            twoBigDB.Update("ADMINLOGS/" + log.logsId, log);


            Session.Abandon();
            Session.RemoveAll();
            Session["idno"] = null;
            Session["password"] = null;
            Session.Clear();
            Response.Redirect("/LandingPage/Account.aspx");
        }

    }
}
