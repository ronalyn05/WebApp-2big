using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        protected void Page_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            if (lastError is HttpRequestValidationException)
            {
                Response.Clear();
                Response.Write("<script>alert('Invalid Input!');</script>");
                Server.ClearError();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //connection to database 
            twoBigDB = new FireSharp.FirebaseClient(config);

            if (Session["currentClient"] != null)
            {
                int clientID = (int)Session["currentClient"];

                FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + clientID);
                Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();

                if (adminDet == null)
                {
                    Response.Write("<script> alert('Session Expired! Please login again');window.location.href = '/superAdmin/SuperAdminAccount.aspx';</script>");

                }
                if (admin != null)
                {
                    if (admin.status == "Approved" || admin.status == "Declined")
                    {
                        declineButton.Visible = false;
                        approveButton.Visible = false;

                    }
                    else
                    {
                        approveButton.Visible = true;
                        declineButton.Visible = true;
                    }
                }
            }


            DisplayDetails();

        }
        private void DisplayDetails()
        {
            if (Session["currentClient"] != null)
            {
                int clientID = (int)Session["currentClient"];

                FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + clientID);
                Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();

                if (clientID == 0)
                {
                    Response.Write("<script> alert('Session Expired! Please login again');window.location.href = '/superAdmin/SuperAdminAccount.aspx';</script>");

                }
                if (adminDet == null)
                {
                    Response.Write("<script> alert('Session Expired! Please login again');window.location.href = '/superAdmin/SuperAdminAccount.aspx';</script>");

                }

                clientStatus.Text = admin.status;
                clientFullName.Text = admin.fname + " " + admin.mname + " " + admin.lname;
                //clientAddress.Text = admin.
                clientEmail.Text = admin.email;
                firstName.Text = admin.fname;
                middleName.Text = admin.mname;
                lastName.Text = admin.lname;
                clientBirthDate.Text = admin.bdate;
                clientPhone.Text = admin.phone;
                chosenValidID.Text = admin.validID;
                chosenProof.Text = admin.businessProof;


                DisplayImages();

                adminDet = twoBigDB.Get("ADMIN/" + clientID + "/RefillingStation");
                Model.RefillingStation station = adminDet.ResultAs<Model.RefillingStation>();

                clientStationAdd.Text = station.stationAddress;

                clientStationName.Text = station.stationName;

            }

        }
        private void DisplayImages()
        {
            if (Session["currentClient"] != null)
            {
                int clientID = (int)Session["currentClient"];

                FirebaseResponse businessProof = twoBigDB.Get("ADMIN/" + clientID + "/Links/Businessproofs");
                //Model.Links proof = businessProof.ResultAs<Model.Links>();
                List<string> businessProofUrls = JsonConvert.DeserializeObject<List<string>>(businessProof.Body);

                if (businessProof == null)
                {
                    Response.Write("<script> alert('Session Expired! Please login again');window.location.href = '/superAdmin/SuperAdminAccount.aspx';</script>");

                }
                FirebaseResponse validID = twoBigDB.Get("ADMIN/" + clientID + "/Links/ValidIDs");
                //Model.Links valid = validID.ResultAs<Model.Links>();
                List<string> validIDProofUrls = JsonConvert.DeserializeObject<List<string>>(validID.Body);

                if (validID == null)
                {
                    Response.Write("<script> alert('Session Expired! Please login again');window.location.href = '/superAdmin/SuperAdminAccount.aspx';</script>");

                }

                //Combine the URLs into a single list.
                List<string> allImageUrls = new List<string>();

                if (allImageUrls != null)
                {
                    if (businessProofUrls != null)
                    {
                        allImageUrls.AddRange(businessProofUrls);
                    }

                    if (validIDProofUrls != null)
                    {
                        allImageUrls.AddRange(validIDProofUrls);
                    }
                }

               

                if (allImageUrls == null || allImageUrls.Count == 0)
                {
                    noUploaded.Text = "No images available.";
                    noUploaded.Visible = true;
                }
                else
                {
                    //null coalescing operator (??)
                    //checks if allImageUrls is null and, if so, sets it to an empty list to prevent any null reference exceptions during data binding.
                    uploadedImages.DataSource = allImageUrls ?? new List<string>();
                    uploadedImages.DataBind();
                }
            }

        }

        //public string getProofLink()
        //{
        //    string proofLink = "";
        //    FirebaseResponse proof = twoBigDB.Get("ADMIN/" + ClientID);
        //    Model.AdminAccount businessproof = proof.ResultAs<Model.AdminAccount>();

        //    if (businessproof != null)
        //    {
        //        proofLink = businessproof.businessProofLnk;

        //        string extension = Path.GetExtension(proofLink);

        //        if (extension.ToLower() == ".pdf")
        //        {
        //            // Set the data and type attributes of the pdfViewer element
        //            pdfViewer.Attributes["data"] = proofLink;
        //            pdfViewer.Attributes["type"] = "application/pdf";

        //            // Return an empty string since the PDF is rendered using PDF.js
        //            return "";
        //        }
        //        else if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
        //        {
        //            // Set the src attribute of the imgViewer element
        //            businessProofImg.Attributes["src"] = proofLink;

        //            // Return an empty string since the image is rendered using an img element
        //            return "";
        //        }
        //    }
        //    return proofLink;
        //}


        protected void approveButton_Click(object sender, EventArgs e)
        {

            if (Session["currentClient"] != null || Session["SuperIDno"] != null)
            {
                int clientID = (int)Session["currentClient"];
                var idno = (string)Session["SuperIDno"];

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
                admin.dateApproved = DateTime.Now;
                adminDet = twoBigDB.Update("ADMIN/" + clientID, admin);

                //SEND NOTIFICATION TO ADMIN 
                Random rnd = new Random();
                int ID = rnd.Next(1, 20000);
                var Notification = new Notification
                {
                    admin_ID = clientID,
                    sender = "Super Admin",
                    title = "Application Approved",
                    receiver = "Admin",
                    body = "Your application is now approved! You can now subscribe to our Subscription Packages",
                    notificationDate = DateTime.Now,
                    status = "unread",
                    notificationID = ID,
                    superAdmin_ID = int.Parse(idno),

                };

                SetResponse notifResponse;
                notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                //Get the current date and time
                DateTime logTime = DateTime.Now;

                //generate a random number for users logged
                //Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                string superName = (string)Session["superAdminName"];


                //Store the login information in the USERLOG table
                var data = new superLogs
                {
                    logsId = idnum,
                    superID = int.Parse(idno),
                    superFullname = superName,
                    superActivity = "APPROVED CLIENT:" + " " + admin.fname + " " + admin.lname,
                    activityTime = logTime
                };

                //Storing the  info
                FirebaseResponse response = twoBigDB.Set("SUPERADMIN_LOGS/" + data.logsId, data);//Storing data to the database
                superLogs res = response.ResultAs<superLogs>();//Database Result

                Response.Write("<script>alert ('Client Approved!');  window.location.href = '/superAdmin/ManageWRSClients.aspx'; </script>");



            }

        }
        private bool ContainsPotentiallyDangerousInput(string input)
        {
            // Check for specific HTML tags or characters that are potentially dangeroucs
            string pattern = @"<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>|<h1\b[^<]*(?:(?!<\/h1>)<[^<]*)*<\/h1>";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(input);
        }


        protected void declineButton_Click(object sender, EventArgs e)
        {
            try
            {
                string userInput = reasonDecline.Text;

                if (ContainsPotentiallyDangerousInput(userInput))
                {
                    Response.Write("<script>alert ('Invalid Input! Potentially dangerous input detected');  window.location.href = '/superAdmin/clientDetails.aspx'; </script>");

                }
                else
                {
                    if (Session["currentClient"] != null || Session["SuperIDno"] != null)
                    {
                        int clientID = (int)Session["currentClient"];

                        if (Session["SuperIDno"] != null)
                        {
                            var idno = (string)Session["SuperIDno"];

                            string reason = reasonDecline.Text;

                            FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + clientID);
                            Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();

                            //to check if the status is already approved or declined
                            if (admin.status == "Approved" || admin.status == "Declined")
                            {
                                if (admin.status == "Approved")
                                {
                                    Response.Write("<script>alert ('This client is already Approved');  window.location.href = '/superAdmin/clientDetails'; </script>");
                                }
                                if (admin.status == "Declined")
                                {
                                    Response.Write("<script>alert ('This client is already Declined');  window.location.href = '/superAdmin/clientDetails'; </script>");
                                }
                                return;
                            }

                            admin.status = "Declined";
                            admin.dateDeclined = DateTime.Now;
                            adminDet = twoBigDB.Update("ADMIN/" + clientID, admin);

                            //SEND NOTIFICATION TO ADMIN 
                            Random rnd = new Random();
                            int ID = rnd.Next(1, 20000);
                            var Notification = new Notification
                            {
                                superAdmin_ID = int.Parse(idno),
                                admin_ID = clientID,
                                sender = "Super Admin",
                                title = "Application Declined",
                                receiver = "Admin",
                                body = reason,
                                notificationDate = DateTime.Now,
                                status = "unread",
                                notificationID = ID

                            };

                            SetResponse notifResponse;
                            notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                            Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                            //Get the current date and time
                            DateTime logTime = DateTime.Now;

                            //generate a random number for users logged
                            //Random rnd = new Random();
                            int idnum = rnd.Next(1, 10000);

                            string superName = (string)Session["superAdminName"];

                            //Store the login information in the USERLOG table
                            var data = new superLogs
                            {
                                logsId = idnum,
                                superID = int.Parse(idno),
                                superFullname = superName,
                                superActivity = "DECLINED CLIENT:" + " " + admin.fname + " " + admin.lname,
                                activityTime = logTime
                            };

                            //Storing the  info
                            FirebaseResponse response = twoBigDB.Set("SUPERADMIN_LOGS/" + data.logsId, data);//Storing data to the database
                            superLogs res = response.ResultAs<superLogs>();//Database Result

                            Response.Write("<script>alert ('You declined the application!');  window.location.href = '/superAdmin/ManageWRSClients.aspx'; </script>");

                        }


                    }
                }

              
            }
            catch 
            {
              
              
            }



        }
    }
}