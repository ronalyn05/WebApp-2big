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


            //if (Session["username"] == null)
            //{
            //    //User not found
            //     Response.Write("<script>alert('User not found');</script>");
            //    //Response.Redirect("/LandingPage/Account.aspx");
            ////}
            if (Session["idno"] == null)
            {
                // User not found
                Response.Write("<script>alert('User not found');</script>");
                //Response.Redirect("/LandingPage/Account.aspx");
            }
            else
            {
                string stationName = (string)Session["stationName"];
                lblWRSname.Text = stationName;
            }

            loadNotifications();
            SubscriptionStatus();
            //reminderNotification();
        }

        private void SubscriptionStatus()
        {
            var adminID = Session["idno"].ToString();

            FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + adminID + "/Subscribed_Package");
            Model.Subscribed_Package expiration = adminDet.ResultAs<Model.Subscribed_Package>();

            
            //if (expiration.expiration < DateTime.Now)
            //{
            //    //SEND NOTIFICATION TO ADMIN 
            //    Random rnd = new Random();
            //    int ID = rnd.Next(1, 20000);
            //    var Notification = new Notification
            //    {
            //        admin_ID = int.Parse(adminID),
            //        sender = "Super Admin",
            //        title = "Subscription Expired",
            //        receiver = "Admin",
            //        body = "Your subscription has Expired! Subscribe again to continue using the platform",
            //        notificationDate = DateTime.Now,
            //        status = "unread",
            //        notificationID = ID

            //    };

            //    SetResponse notifResponse;
            //    notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
            //    Notification notif = notifResponse.ResultAs<Notification>();//Database Result

               
            //}
            //Debug.WriteLine($"NOW: {DateTime.Now}");
            //Debug.WriteLine($"DATE: {expiration.expiration}");
        }
        private void loadNotifications()
        {


            //NOTIFICATION FROM CUSTOMER TO ADMIN

            string adminID = (string)Session["idno"];

            int admin = int.Parse(adminID);


            FirebaseResponse adminNotif = twoBigDB.Get("NOTIFICATION");
            var adminBody = adminNotif.Body;
            Dictionary<string, Model.Notification> adminAllNotifs = JsonConvert.DeserializeObject<Dictionary<string, Model.Notification>>(adminBody);

            if (adminAllNotifs != null)
            {
                // Create a list to store all the notifications with the receiver as " Admin"
                List<Model.Notification> AdminNotifications = new List<Model.Notification>();

                // Loop through all the notifications
                foreach (KeyValuePair<string, Model.Notification> entry in adminAllNotifs)
                {
                    // Check if the current notification has the receiver as "Admin"
                    if (entry.Value.receiver == "Admin" && entry.Value.admin_ID == admin)
                    {

                        // Add the current notification to the list of admin notifications
                        AdminNotifications.Add(entry.Value);

                    }
                }

                // Sort the super admin notifications based on dateAdded property in descending order
                AdminNotifications = AdminNotifications.OrderByDescending(n => n.notificationDate).ToList();

                // Bind the list of super admin notifications to the repeater control
                rptNotifications.DataSource = AdminNotifications;
                rptNotifications.DataBind();
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
                Response.Write("<script>window.location.href = '/Admin/WaterProducts.aspx'; </script>");
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
                Response.Write("<script>window.location.href = '/Admin/SubscriptionPackages.aspx'; </script>");
            }

        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            // Get the log ID from the session
            int logsId = (int)Session["logsId"];

            // Retrieve the existing Users log object from the database
            FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
            UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

            //Get the current date and time
            DateTimeOffset addedTime = DateTimeOffset.UtcNow;

            // Log user activity
            var log = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = logsId,
                userFullname = (string)Session["fullname"],
                userActivity = "LOGGED OUT",
                activityTime = addedTime
            };

            twoBigDB.Update("USERSLOG/" + log.logsId, log);


            Session.Abandon();
            Session.RemoveAll();
            Session["idno"] = null;
            Session["password"] = null;
            Session.Clear();
            Response.Redirect("/LandingPage/Account.aspx");
        }
    }
}
