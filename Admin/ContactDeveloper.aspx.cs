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

namespace WRS2big_Web.Admin
{
    public partial class ContactDeveloper : System.Web.UI.Page
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

            loadMessages();
        }

        private void loadMessages()
        {
            string adminID = (string)Session["idno"];

            int admin = int.Parse(adminID);


            FirebaseResponse adminNotif = twoBigDB.Get("SUPERADMIN/MESSAGES");
            var adminBody = adminNotif.Body;
            Dictionary<string, Model.Messages> adminMessages = JsonConvert.DeserializeObject<Dictionary<string, Model.Messages>>(adminBody);

            if (adminMessages != null)
            {
                // Create a list to store all the notifications with the receiver as "Admin"
                List<Model.Messages> sentMessages = new List<Model.Messages>();

                // Loop through all the notifications
                foreach (KeyValuePair<string, Model.Messages> entry in adminMessages)
                {
                    // Check if the current notification has the sender as "Admin"
                    if (entry.Value.sender == "Admin" && entry.Value.clientID == admin)
                    {

                        // Add the current notification to the list of admin notifications
                        sentMessages.Add(entry.Value);

                    }
                }

                // Sort the super admin notifications based on dateAdded property in descending order
                sentMessages = sentMessages.OrderByDescending(n => n.sent).ToList();

                // Bind the list of super admin notifications to the repeater control
                messagesrepeater.DataSource = sentMessages;
                messagesrepeater.DataBind();
            }

        }
        protected void sendMessage_Click(object sender, EventArgs e)
        {

            //SEND MESSAGE TO SUPERADMIN 
            string idno = (string)Session["idno"];

            
            Random rnd = new Random();
            int ID = rnd.Next(1, 20000);
            var message = new Messages
            {
                clientID = int.Parse(idno),
                sender = "Admin",
                receiver = "Super Admin",
                body = enterMessage.Text,
                sent = DateTime.Now,
                status = "unread",
                messageID = ID

            };

            SetResponse notifResponse;
            notifResponse = twoBigDB.Set("SUPERADMIN/MESSAGES/" + ID, message);//Storing data to the database
            Messages notif = notifResponse.ResultAs<Messages>();//Database Result

            Response.Write("<script>alert ('Message sent!');  window.location.href = '/Admin/ContactDeveloper.aspx'; </script>");
        }
    }
}