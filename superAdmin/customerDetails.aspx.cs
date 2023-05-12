using System;
using System.Collections.Generic;
using System.Data;
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
using Newtonsoft.Json.Linq;

namespace WRS2big_Web.superAdmin
{
    public partial class customerDetails : System.Web.UI.Page
    {
        //Initialize the FirebaseClient with the database URL and secret key.
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

            DisplayDetails();
        }
        private void DisplayDetails()
        {
            int customerID = (int)Session["currentCustomer"];

            FirebaseResponse adminDet = twoBigDB.Get("CUSTOMER/" + customerID);
            Model.Customer admin = adminDet.ResultAs<Model.Customer>();

            //clientImage.ImageUrl = admin.imageSelfie.ToString();
            clientFullName.Text = admin.firstName + " " + admin.middleName + " " + admin.lastName;
            //clientAddress.Text = admin.
            clientEmail.Text = admin.email;
            clientBirthDate.Text = admin.birthdate;
            clientPhone.Text = admin.phoneNumber;
            cusFirstName.Text = admin.firstName;
            cuslastName.Text = admin.lastName;
            cusmiddleName.Text = admin.middleName;
            clientAddress.Text = admin.address;

            


            clientValidID.ImageUrl = admin.imageProof.ToString();

            //if (admin.cus_status == "Approved" && admin.cus_status == "Declined")
            //{
            //    approveButton.Enabled = false;
            //    declineButton.Enabled = false;
            //}

            //TO CHECK IF NAAY PROFILE PIC
            if (admin.imageSelfie != null)
            {
                clientImage.ImageUrl = admin.imageSelfie.ToString();
            }

            double lattitude = admin.lattitudeLocation;
            double longitude = admin.longitudeLocation;
            string apiKey = "AIzaSyBqKUBIswNi5uO3xOh4Boo8kSJyJ3DLkhk";

            string address = GetAddressFromLatLong(lattitude, longitude, apiKey);

            clientAddress.Text = address;

        }


        //TO REVERSE THE COORDINATES INTO ADDRESS
        public static string GetAddressFromLatLong(double latitude, double longitude, string apiKey)
        {
            string url = string.Format("https://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&key={2}", latitude, longitude, apiKey);
            string result = string.Empty;

            using (var client = new WebClient())
            {
                result = client.DownloadString(url);
            }

            JObject json = JObject.Parse(result);
            string address = json["results"][0]["formatted_address"].ToString();

            return address;
        }

        protected void approveButton_Click(object sender, EventArgs e)
        {
            int customerID = (int)Session["currentCustomer"];

            FirebaseResponse adminDet = twoBigDB.Get("CUSTOMER/" + customerID);
            Model.Customer admin = adminDet.ResultAs<Model.Customer>();

            //to check if the status is already approved or declined
            if (admin.cus_status == "Approved" || admin.cus_status == "Declined")
            {

                if (admin.cus_status == "Approved")
                {
                    Response.Write("<script>alert ('This client is already Approved');  window.location.href = '/superAdmin/clientDetails'; </script>");
                }
                else if (admin.cus_status == "Declined")
                {
                    Response.Write("<script>alert ('This client is already Declined');  window.location.href = '/superAdmin/clientDetails'; </script>");
                }
                return;
            }

            admin.cus_status = "Approved";
            adminDet = twoBigDB.Update("CUSTOMER/" + customerID, admin);

            //SEND NOTIFICATION TO CUSTOMER 
            Random rnd = new Random();
            int ID = rnd.Next(1, 20000);
            var Notification = new Model.Notification
            {
                admin_ID = customerID,
                sender = "Super Admin",
                title = "Application Approved",
                receiver = "Customer",
                body = "Your application is now approved! You can now order from your favorite Refilling Stations!",
                notificationDate = DateTime.Now,
                status = "unread",
                notificationID = ID

            };

            SetResponse notifResponse;
            notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
            Model.Notification notif = notifResponse.ResultAs<Model.Notification>();//Database Result

            Response.Write("<script>alert ('successfully approved!');  window.location.href = '/superAdmin/ManageCustomers.aspx'; </script>");
           
        }

        protected void declineButton_Click(object sender, EventArgs e)
        {
            int customerID = (int)Session["currentCustomer"];

            FirebaseResponse adminDet = twoBigDB.Get("CUSTOMER/" + customerID);
            Model.Customer admin = adminDet.ResultAs<Model.Customer>();

            //to check if the status is already approved or declined
            if (admin.cus_status == "Approved" || admin.cus_status == "Declined")
            {
                if (admin.cus_status == "Approved")
                {
                    Response.Write("<script>alert ('This client is already Approved');  window.location.href = '/superAdmin/clientDetails'; </script>");
                }
                if (admin.cus_status == "Declined")
                {
                    Response.Write("<script>alert ('This client is already Declined');  window.location.href = '/superAdmin/clientDetails'; </script>");
                }
                return;
            }

            admin.cus_status = "Declined";
            adminDet = twoBigDB.Update("ADMIN/" + customerID, admin);

            Response.Write("<script>alert ('You declined the application! Notify the client ');  window.location.href = '/superAdmin/ManageCustomers.aspx'; </script>");
            //DIRI I-INSERT ANG PAGSAVE SA NOTIFICATION INTO DATABASE

        }
    }
}