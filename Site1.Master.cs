using System;
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

            if (Session["idno"] != null)
            {
                string adminID = Session["idno"].ToString();

                FirebaseResponse response;
                response = twoBigDB.Get("ADMIN/" + adminID);
                AdminAccount user = response.ResultAs<AdminAccount>(); //Database result

                response = twoBigDB.Get("ADMIN/" + adminID + "/RefillingStation");
                RefillingStation station = response.ResultAs<RefillingStation>(); //Database result

                if (response == null)
                {
                    Response.Write("<script>alert ('Session Expired. Please login again');  window.location.href = '/LandingPage/Account.aspx'; </script>");
                }

                lblWRSname.Text = station.stationName;

                if (response != null)
                {

                    string role = (string)Session["role"];


                    //to check the role sa ni login
                    if (role == "Admin")
                    {
                        lblWRSname.Text = station.stationName;

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
                    else if (role == "Cashier")
                    {

                        string cashierName = (string)Session["fullName"];

                        if (cashierName != null)
                        {
                            lblWRSname.Text = "CASHIER:" + " " + cashierName;
                        }
                        else
                        {
                            lblWRSname.Text = "CASHIER:";
                        }

                        navigationBarMaster.Visible = true;
                        navigationBarMaster.Visible = true;
                    }


                }

            }
            else
            {

                Response.Write("<script>alert ('Session Expired! Please login again');window.location.href = '/superAdmin/SuperAdminAccount.aspx'; </script>");
            }


            loadNotifications(); //load the notifications
            checkStationStatus(); //Update the status of the station : Open/Close
            checkOrderLimit(); //check if the admin has reached the order limit 
        }
        private void checkOrderLimit()
        {
            if (Session["idno"] != null)
            {
                var adminID = Session["idno"].ToString();

                //TO GET THE REFILLING STATION DETAILS
                FirebaseResponse response = twoBigDB.Get("ADMIN/" + adminID + "/Subscribed_Package/");
                Model.Subscribed_Package package = response.ResultAs<Model.Subscribed_Package>();

                if (package != null)
                {
                    if (package.orderLimit != 0)
                    {
                        int orderLimit = package.orderLimit;
                        //int  = Limit + 1;

                        int currentClient = int.Parse(adminID);

                        FirebaseResponse adminNotif = twoBigDB.Get("NOTIFICATION");
                        var adminBody = adminNotif.Body;
                        Dictionary<string, Model.Notification> adminAllNotifs = JsonConvert.DeserializeObject<Dictionary<string, Model.Notification>>(adminBody);

                        // Check if there is already a Subscription Expired notification for this admin_ID
                        bool hasLimitReached = adminAllNotifs.Values.Any(n => n.admin_ID == currentClient && n.title == "Limit Reached");

                        if (!hasLimitReached)
                        {

                            //CLIENT WALKIN ORDERS
                            response = twoBigDB.Get("WALKINORDERS");
                            var data = response.Body;
                            Dictionary<string, Model.WalkInOrders> walkinOrderList = JsonConvert.DeserializeObject<Dictionary<string, Model.WalkInOrders>>(data);

                            //CLIENT ONLINE ORDERS
                            response = twoBigDB.Get("ORDERS");
                            var orders = response.Body;
                            Dictionary<string, Model.Order> onlineOrderList = JsonConvert.DeserializeObject<Dictionary<string, Model.Order>>(orders);


                            // Create a list to store all the Walkin orders
                            List<WalkInOrders> clientWalkins = new List<WalkInOrders>();

                            // Create a list to store all the online orders
                            List<Order> clientOnline = new List<Model.Order>();

                            if (Session["idno"] != null)
                            {
                                string adminSession = (string)Session["idno"];
                                int currentAdmin = int.Parse(adminSession);

                                foreach (var online in onlineOrderList)
                                {


                                    if (online.Value.admin_ID == currentAdmin)
                                    {
                                        clientOnline.Add(online.Value);
                                    }

                                }

                                foreach (var walkins in walkinOrderList)
                                {
                                    if (walkins.Value.adminId == currentAdmin)
                                    {
                                        clientWalkins.Add(walkins.Value);
                                    }
                                }

                                if (clientOnline != null || clientWalkins != null)
                                {
                                    int onlineOrders = clientOnline.Count;
                                    int walkinOrders = clientWalkins.Count;

                                    int totalOverall = onlineOrders + walkinOrders;

                                    Debug.WriteLine($"ONLINE ORDERS:{onlineOrders}");
                                    Debug.WriteLine($"WALKIN ORDERS:{walkinOrders}");
                                    Debug.WriteLine($"TOTAL ORDERS: {totalOverall}");
                                    Debug.WriteLine($"ORDER LIMIT: {orderLimit}");

                                    if (totalOverall >= orderLimit)
                                    {
                                        FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + adminID);
                                        Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();

                                        //change the currentSubscription into Limit Reached
                                        admin.currentSubscription = "LimitReached";
                                        adminDet = twoBigDB.Update("ADMIN/" + adminID, admin);

                                        //change substatus into LimitReached
                                        package.subStatus = "LimitReached";
                                        response = twoBigDB.Update("ADMIN/" + adminID + "/Subscribed_Package/", package);


                                        //SEND NOTIFICATION
                                        Random rnd = new Random();
                                        int ID = rnd.Next(1, 20000);
                                        var Notification = new Notification
                                        {
                                            admin_ID = int.Parse(adminID),
                                            sender = "Super Admin",
                                            title = "Limit Reached",
                                            receiver = "Admin",
                                            body = "You've reached the order limit based on your currently subscribed package. Note: Your customers cant place an order from your station since you already reached the limit. Creating walk-in order is also not possible at this moment.  You can subscribe to another package to continue receiving orders from your valued customers.",
                                            notificationDate = DateTime.Now,
                                            status = "unread",
                                            notificationID = ID


                                        };

                                        SetResponse notifResponse;
                                        notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                                        Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                                        //Redirect to the same page to reload it
                                        Response.Redirect(Request.RawUrl, false);
                                        Context.ApplicationInstance.CompleteRequest();

                                    }
                                }
                            }
                        }


                    }
                }

            }
            else
            {
                Response.Write("<script> alert ('Session Expired! Please login again'); window.location.href = '/LandingPage/Account.aspx';</script>");
            }

        }
        private void checkStationStatus()
        {
            if (Session["idno"] != null)
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
            else
            {
                Response.Write("<script> alert ('Session Expired! Please login again'); window.location.href = '/LandingPage/Account.aspx';</script>");
            }

        }

        private void SubscriptionStatus() //to send the notification depending on the subscription status
        {
            if (Session["idno"] != null)
            {
                string adminID = Session["idno"].ToString();

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

                            // Check if there is already a Subscription Expired notification for this admin_ID
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
            else
            {
                Response.Write("<script> alert ('Session Expired! Please login again'); window.location.href = '/LandingPage/Account.aspx';</script>");
            }
        }
        private void loadNotifications()
        {
            SubscriptionStatus(); //to send the notification depending on the subscription status

            if (Session["idno"] != null)
            {
                //NOTIFICATION FROM CUSTOMER TO ADMIN
                string adminID = (string)Session["idno"];

                if (Session["idno"] == null || adminID == null)
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
                Response.Write("<script>window.location.href = '/Admin/WaterOrders.aspx'; </script>");
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
            if (Session["role"] != null)
            {
                Random rnd = new Random();
                string role = (string)Session["role"];

                if (role == "Cashier")
                {
                    string currentID = (string)Session["cashierID"];

                    //generate a random number for users logged
                    int idnum = rnd.Next(1, 10000);
                    // Get the current date and time
                    DateTime addedTime = DateTime.Now; ;


                    //Store the login information in the USERLOG table
                    var logoutLog = new UsersLogs
                    {
                        userIdnum = int.Parse(currentID),
                        logsId = idnum,
                        userFullname = (string)Session["fullname"],
                        userActivity = "LOGGED OUT",
                        activityTime = addedTime,
                        role = role
                    };

                    //Storing the  info
                    SetResponse logresponse = twoBigDB.Set("ADMINLOGS/" + logoutLog.logsId, logoutLog);//Storing data to the database
                    Model.UsersLogs res = logresponse.ResultAs<Model.UsersLogs>();//Database Result

                }
                else if (role == "Admin")
                {
                    //generate a random number for users logged
                    int idnum = rnd.Next(1, 10000);
                    // Get the current date and time
                    DateTime addedTime = DateTime.Now; ;

                    string currentID = (string)Session["idno"];

                    //Store the login information in the USERLOG table
                    var logoutLog = new UsersLogs
                    {
                        userIdnum = int.Parse(currentID),
                        logsId = idnum,
                        userFullname = (string)Session["fullname"],
                        userActivity = "LOGGED OUT",
                        activityTime = addedTime,
                        role = role
                    };

                    //Storing the  info
                    SetResponse logresponse = twoBigDB.Set("ADMINLOGS/" + logoutLog.logsId, logoutLog);//Storing data to the database
                    Model.UsersLogs res = logresponse.ResultAs<Model.UsersLogs>();//Database Result
                }



                Session.Abandon();
                Session.RemoveAll();
                Session["idno"] = null;
                Session["password"] = null;
                Session["cashierID"] = null;
                Session.Clear();
                Response.Redirect("/LandingPage/Account.aspx");
            }

        }

        protected void markAsRead_Click(object sender, EventArgs e)
        {
            // Retrieve the existing Notifications object from the database
            FirebaseResponse notification = twoBigDB.Get("NOTIFICATION");
            var data = notification.Body;

            if (data != null)
            {
                Dictionary<string, Model.Notification> allNotifications = JsonConvert.DeserializeObject<Dictionary<string, Model.Notification>>(data);

                if (allNotifications != null)
                {
                    //List for unread notifications
                    List<Model.Notification> unreadNotifications = new List<Model.Notification>();

                    // Loop through all the notifications
                    foreach (KeyValuePair<string, Model.Notification> entry in allNotifications)
                    {
                        // Check if the current notification has the receiver as "Super Admin"
                        if (entry.Value.receiver == "Admin" && entry.Value.status == "unread")
                        {
                            unreadNotifications.Add(entry.Value);

                        }
                    }

                    if (unreadNotifications.Count == 0)
                    {

                        Response.Write("<script>alert('No unread notifications'); </script>");
                    }
                    foreach (var unreads in unreadNotifications)
                    {
                        var updatedNotif = new Notification
                        {
                            notificationID = unreads.notificationID,
                            notificationDate = unreads.notificationDate,
                            receiver = unreads.receiver,
                            sender = unreads.sender,
                            title = unreads.title,
                            orderID = unreads.orderID,
                            cusId = unreads.cusId,
                            driverId = unreads.driverId,
                            //UPDATE THE STATUS FROM UNREAD TO READ
                            status = "read",
                            body = unreads.body,
                            admin_ID = unreads.admin_ID
                        };
                        notification = twoBigDB.Update("NOTIFICATION/" + unreads.notificationID, updatedNotif);
                    }


                    //Redirect to the same page to reload it
                    Response.Redirect(Request.RawUrl, false);
                    Context.ApplicationInstance.CompleteRequest();


                }
            }
            else
            {
                // noNotifications.Text = "No Notifications";
            }
        }
    }
}
