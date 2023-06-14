using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using WRS2big_Web.Model;

namespace WRS2big_Web
{
    public partial class WRSsuperAdmin : System.Web.UI.MasterPage
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient twoBigDB;
        protected void Page_Load(object sender, EventArgs e)
        {
            twoBigDB = new FireSharp.FirebaseClient(config);

            if (Session["Superfname"] != null)
            {
                superLbl.Text = "SUPER ADMIN:" + " " + Session["Superfname"].ToString();
            }
            else
            {
                Response.Write("<script>alert('Please Login your account first'); window.location.href = '/superAdmin/SuperAdminAccount.aspx'; </script>");
            }

            if (!IsPostBack)
            {
                loadNotifications();
            }

            
        }
        private void loadNotifications()
        {
            
            // Retrieve the existing Notifications object from the database
            FirebaseResponse notification = twoBigDB.Get("NOTIFICATION");
            var data = notification.Body;

            if (data != null)
            {
                Dictionary<string, Model.Notification> allNotifications = JsonConvert.DeserializeObject<Dictionary<string, Model.Notification>>(data);

                if (allNotifications != null)
                {
                    // Create a list to store all the notifications with the receiver as "Super Admin"
                    List<Model.Notification> superAdminNotifications = new List<Model.Notification>();

                    //List for unread notifications
                    List<Model.Notification> unreadNotifications = new List<Model.Notification>();

                    // Loop through all the notifications
                    foreach (KeyValuePair<string, Model.Notification> entry in allNotifications)
                    {
                        // Check if the current notification has the receiver as "Super Admin"
                        if (entry.Value.receiver == "Super Admin")
                        {

                            // Add the current notification to the list of super admin notifications
                            superAdminNotifications.Add(entry.Value);

                            if (entry.Value.status == "unread")
                            {

                                unreadNotifications.Add(entry.Value);

                            }
                        }
                    }

                    // Sort the super admin notifications based on dateAdded property in descending order
                    superAdminNotifications = superAdminNotifications.OrderByDescending(n => n.notificationDate).ToList();

                    // Bind the list of super admin notifications to the repeater control
                    rptNotifications.DataSource = superAdminNotifications;
                    rptNotifications.DataBind();

                    Debug.WriteLine($"NOTIFICATIONSLOADED: {superAdminNotifications.Count}");

                    Debug.WriteLine($"UNREAD NOTIFS: {unreadNotifications.Count}");

                    int unreadCount = unreadNotifications.Count;

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
            else
            {
                //noNotifications.Text = "No Notifications";
            }
            
        }

        protected void notifMsg_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;
            string notificationID = (sender as LinkButton).CommandArgument;

            int idnum = int.Parse(notificationID);
            // Retrieve the existing Notifications object from the database
            FirebaseResponse notification = twoBigDB.Get("NOTIFICATION/" + idnum);
            Notification notif = notification.ResultAs<Notification>();

          
            string title = notif.title;

            if (title == "New Registered User")
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

                int currentClient = notif.cusId;
                Session["currentCustomer"] = currentClient;
                Response.Write("<script>window.location.href = '/superAdmin/customerDetails.aspx'; </script>");

            }
            else if (title == "New Client")
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

                int currentClient = notif.admin_ID;
                Session["currentClient"] = currentClient;
                Response.Write("<script>window.location.href = '/superAdmin/clientDetails.aspx'; </script>");

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

                int currentClient = notif.admin_ID;
                Session["currentClient"] = currentClient;
                Response.Write("<script>window.location.href = '/superAdmin/clientSubscriptionHistory.aspx'; </script>");
            }
            else if (title == "Subscription Renewal")
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

                int currentClient = notif.admin_ID;
                Session["currentClient"] = currentClient;
                Response.Write("<script>window.location.href = '/superAdmin/clientSubscriptionHistory.aspx'; </script>");
            }
            else if (title == "Re-evaluate Client")
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

                int currentClient = notif.admin_ID;
                Session["currentClient"] = currentClient;
                Response.Write("<script>window.location.href = '/superAdmin/clientDetails.aspx'; </script>");
            }
            else
            {
                Response.Write("<script>alert ('Session Expired. Please login again');  window.location.href = '/superAdmin/SuperAdminAccount.aspx'; </script>");
            }

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
           

            //SAVE LOGS TO SUPER ADMIN
            //Get the current date and time
            DateTime logTime = DateTime.Now;

            //generate a random number for users logged
            Random rnd = new Random();
            int logID = rnd.Next(1, 10000);

            if (Session["SuperIDno"] != null || Session["superAdminName"] != null)
            {
                var idno = (string)Session["SuperIDno"];
                string superName = (string)Session["superAdminName"];

                //Store the login information in the USERLOG table
                var log = new Model.superLogs
                {
                    logsId = logID,
                    superID = int.Parse(idno),
                    superFullname = superName,
                    superActivity = "LOGGED OUT",
                    activityTime = logTime
                };

                //Storing the  info
                FirebaseResponse response = twoBigDB.Set("SUPERADMIN_LOGS/" + log.logsId, log);//Storing data to the database
                Model.superLogs res = response.ResultAs<Model.superLogs>();//Database Result


                Session.Abandon();
                Session.RemoveAll();
                Session["SuperIDno"] = null;
                Session["SuperPass"] = null;
                Session.Clear();
                Response.Redirect("/superAdmin/SuperAdminAccount.aspx");
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
                        if (entry.Value.receiver == "Super Admin" && entry.Value.status == "unread")
                        {
                            unreadNotifications.Add(entry.Value);

                        }
                    }

                    if (unreadNotifications.Count == 0)
                    {

                        Response.Write("<script>alert('No unread notifications'); </script>");
                    }
                    foreach(var unreads in unreadNotifications)
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
                Response.Write("<script>alert ('Something went wrong!');</script>");
                //Redirect to the same page to reload it
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
            }

        }
       
    }
}