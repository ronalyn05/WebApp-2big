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
using WRS2big_Web.Model;

namespace WRS2big_Web.superAdmin
{
    public partial class clientDetails : System.Web.UI.Page
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
            int clientID = (int)Session["currentClient"];

            FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + clientID);
            Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();

            clientImage.ImageUrl = admin.profile_image.ToString();
            clientFullName.Text = admin.fname + " " + admin.mname + " " + admin.lname ;
            //clientAddress.Text = admin.
            clientEmail.Text = admin.email;
            firstName.Text = admin.fname;
            middleName.Text = admin.mname;
            lastName.Text = admin.lname;
            clientBirthDate.Text = admin.bdate;
            clientPhone.Text = admin.phone;
            chosenValidID.Text = admin.validID;
            chosenProof.Text = admin.businessProof;
            proofChosen.Text = admin.businessProof + " " + "File:";

            //FILE 
            string proofLink = admin.businessProofLnk;
            fileProofLink.NavigateUrl = proofLink;




            //TO CHECK IF NAY VALID ID AND PROOF
            if (admin.businessProofLnk != null || admin.validIDLnk != null)
            {
                clientValidID.ImageUrl = admin.validIDLnk.ToString();

            }

           
            

            adminDet = twoBigDB.Get("ADMIN/" + clientID + "/RefillingStation");
            Model.RefillingStation station = adminDet.ResultAs<Model.RefillingStation>();

            clientStationAdd.Text = station.stationAddress;

            clientStationName.Text = station.stationName;

        }

        protected void approveButton_Click(object sender, EventArgs e)
        {
            int clientID = (int)Session["currentClient"];

            FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + clientID);
            Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();

            //to check if the status is already approved or declined
            if (admin.status == "Approved" || admin.status == "Declined")
            {

                if (admin.status == "Approved")
                {
                    Response.Write("<script>alert ('This client is already Approved');  window.location.href = '/superAdmin/clientDetails'; </script>");
                }
                else if (admin.status == "Declined")
                {
                    Response.Write("<script>alert ('This client is already Declined');  window.location.href = '/superAdmin/clientDetails'; </script>");
                }
                return;
            }

            admin.status = "Approved";
            adminDet = twoBigDB.Update("ADMIN/" + clientID, admin);

            Response.Write("<script>alert ('successfully approved! Notify the client ');  window.location.href = '/superAdmin/ManageWRSClients.aspx'; </script>");

           

        }

        protected void declineButton_Click(object sender, EventArgs e)
        {
            int clientID = (int)Session["currentClient"];

            FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + clientID);
            Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();

            //to check if the status is already approved or declined
            if (admin.status == "Approved" || admin.status == "Declined")
            {
                if (admin.status == "Approved")
                {
                    Response.Write("<script>alert ('This client is already Approved');  window.location.href = '/superAdmin/clientDetails'; </script>");
                }
                if(admin.status == "Declined")
                {
                    Response.Write("<script>alert ('This client is already Declined');  window.location.href = '/superAdmin/clientDetails'; </script>");
                }
               return;
            }

            admin.status = "Declined";
            adminDet = twoBigDB.Update("ADMIN/" + clientID, admin);

            Response.Write("<script>alert ('You declined the application! Notify the client ');  window.location.href = '/superAdmin/ManageWRSClients.aspx'; </script>");

        }
    }
}