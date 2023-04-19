using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;


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

            clientImage.ImageUrl = admin.imageSelfie.ToString();
            clientFullName.Text = admin.firstName + " " + admin.middleName + " " + admin.lastName;
            //clientAddress.Text = admin.
            clientEmail.Text = admin.email;
            clientBirthDate.Text = admin.birthdate;
            clientPhone.Text = admin.phoneNumber;
            cusFirstName.Text = admin.firstName;
            cuslastName.Text = admin.lastName;
            cusmiddleName.Text = admin.middleName;
            clientValidID.ImageUrl = admin.imageProof.ToString();

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

            Response.Write("<script>alert ('successfully approved! Notify the client ');  window.location.href = '/superAdmin/ManageCustomers.aspx'; </script>");
            //DIRI I-INSERT ANG PAGSAVE SA NOTIFICATION INTO DATABASE
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