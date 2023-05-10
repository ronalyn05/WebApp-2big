using System;
using System.Collections.Generic;
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

            string name = (string)Session["name"];
            if (name == null)
            {
                Response.Write("<script>alert('Please Login your account first'); window.location.href = '/superAdmin/Account.aspx'; </script>");
            }
            else
            {
                superLbl.Text = Session["name"].ToString();
            }

            loadNotifications();
        }
        private void loadNotifications()
        {
            //int count = 0;
            // Retrieve the existing Notifications object from the database
            FirebaseResponse notification = twoBigDB.Get("NOTIFICATION");
            var data = notification.Body;

            if (data != null)
            {
                Dictionary<string, Model.Notification> allNotifications = JsonConvert.DeserializeObject<Dictionary<string, Model.Notification>>(data);

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


        }

    }
}