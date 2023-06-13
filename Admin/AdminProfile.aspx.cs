using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using System.Web.Optimization;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Storage;
using Newtonsoft.Json;
using WRS2big_Web.Model;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace WRS2big_Web.Admin
{
    public partial class AdminProfile : System.Web.UI.Page
    {
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

            if (Session["idno"] == null)
            {
                // User not found
                Response.Redirect("../LandingPage/Account.aspx");
                //Response.Write("<script>alert('User not found');</script>");
                //Response.Redirect("/LandingPage/Account.aspx");
            }

            fetchAdminData();
            fetchStationData();
            SubscriptionStatus();

            string role = (string)Session["role"];
            
            if (role != null)
            {
                if (role == "Cashier")
                {
                    //hide all the buttons for managements since the CASHIER is not allowed to manage them
                    changePackage.Visible = false;
                    renewBTN.Visible = false;
                    subscribeBTN.Visible = false;
                    btnManageStation.Visible = false;
                    btnEdit.Visible = false;
                    profileBtn.Visible = false;
                    imgProfile.Visible = false;
                    firstname.ReadOnly = true;
                    middlename.ReadOnly = true;
                    lastname.ReadOnly = true;
                    birthdate.Enabled = false;
                    contactnum.ReadOnly = true;
                    email.ReadOnly = true;
                    birthdate.ReadOnly = true;
                    txtOperatingHrsTo.Enabled = false;
                    txtOperatingHrsFrom.Enabled = false;
                    drdBusinessDaysFrom.Enabled = false;
                    drdBusinessDaysTo.Enabled = false;
                }

            }
        }
        private void SubscriptionStatus() //to send the notification depending on the subscription status
        {
            if (Session["idno"] != null)
            {
                string adminID = Session["idno"].ToString();

                //get the details of the subscribed package
                FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + adminID + "/Subscribed_Package");
                Model.Subscribed_Package expiration = adminDet.ResultAs<Model.Subscribed_Package>();

                if (expiration != null)
                {
                    //get the package renewable details
                    adminDet = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + expiration.packageID);
                    PackagePlans packageDet = adminDet.ResultAs<Model.PackagePlans>();

                    if (packageDet.renewable == "No")
                    {
                        DateTime currentDate = DateTime.Now;
                        DateTime subStart = expiration.subStart;
                        DateTime finalChangePackage = subStart.AddDays(5); // final expiration date

                        Debug.WriteLine($"NOW: {currentDate}");
                        Debug.WriteLine($"SUB START: {subStart}");
                        Debug.WriteLine($"FINAL CHANGE PACKAGE: {finalChangePackage}");

                        if (currentDate <= finalChangePackage)
                        {
                            // currentDate is on or before the finalChangePackage, enable the button
                            changePackage.Visible = true;

                            currentSubscription.InnerText = expiration.packageName;

                            DateTime expirationDate = expiration.expiration;
                            string formattedExp = expirationDate.ToString("MMMM dd, yyyy - hh:mm tt");
                            currentExpiration.InnerText = formattedExp;

                        }
                        else
                        {
                            // currentDate is after the finalChangePackage, disable the button
                            changePackage.Visible = false;
                        }
                    }
                    else
                    {
                        changePackage.Visible = false;
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

        private void fetchStationData()
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
                //POPULATE THE STATION DETAILS IN THE TEXTBOXES
                lblStationName.Text = refillStation.stationName;
                lblAddress.Text = refillStation.stationAddress;
                lblOperatingHours.Text = refillStation.operatingHrsFrom + "- " + refillStation.operatingHrsTo + "";
                lblBusinessday.Text = refillStation.businessDaysFrom + " - " + refillStation.businessDaysTo;

                //POPULATE THE REFILL STATION DETAILS IN THE MODAL
                //txtOperatingHrsFrom.Text = refillStation.operatingHrsFrom;
                //txtOperatingHrsTo.Text = refillStation.operatingHrsTo;
                drdBusinessDaysFrom.Text = refillStation.businessDaysFrom;
                drdBusinessDaysTo.Text = refillStation.businessDaysTo;



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
                lblstatus.Text = status;
                if (isBusinessDay && timeNow >= operatingHrsFrom && timeNow <= operatingHrsTo)
                {
                    status = "OPEN";
                }
                lblstatus.Text = status;

                var statusUpdate = new Dictionary<string, object>
                {
                  { "status", status }
                };

                FirebaseResponse response = twoBigDB.Update("ADMIN/" + adminID + "/RefillingStation/", statusUpdate);
            }
            else
            {

                Response.Write("<script>alert ('You haven't finished setting up your station details yet. Please complete your refilling station details now.');</script>");
            }
        }

        private void fetchAdminData()
        {
            if (Session["idno"] != null)
            {
                var adminID = Session["idno"].ToString();

                FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + adminID);
                Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();


                if (adminDet != null)
                {
                    DateTime birthdate = DateTime.Parse(admin.bdate);

                    //POPULATE THE PERSONAL DETAILS IN THE TEXTBOXES
                    Lbl_Idno.Text = admin.idno.ToString();
                    lblfname.Text = admin.fname;
                    lblmname.Text = admin.mname;
                    lblLname.Text = admin.lname;
                    lblcontactnum.Text = admin.phone;
                    lblemail.Text = admin.email;
                    lbldob.Text = birthdate.ToString("MMMM dd, yyyy"); ;

                    if (admin.profile_image != null)
                    {
                        ImageButton_new.ImageUrl = admin.profile_image.ToString();

                    }
                    else
                    {
                        uploadinstruction.Text = "Upload your profile picture";
                        ImageButton_new.Visible = false;
                    }

                    //POPULATE THE PERSONAL DETAILS IN THE MODAL
                    firstname.Attributes["placeholder"] = admin.fname;
                    middlename.Attributes["placeholder"] = admin.mname;
                    lastname.Attributes["placeholder"] = admin.lname;
                    contactnum.Attributes["placeholder"] = admin.phone;
                    email.Attributes["placeholder"] = admin.email;
                    //birthdate.Text = admin.bdate;

                    //to GET the subscription details
                    string subStatus = admin.subStatus;
                    //check if naka subscribe na ba ang admin 
                    if (subStatus == "Subscribed")
                    {
                        subscribeBTN.Visible = false;

                        FirebaseResponse subDetails = twoBigDB.Get("ADMIN/" + adminID + "/Subscribed_Package/");
                        Model.Subscribed_Package subscription = subDetails.ResultAs<Model.Subscribed_Package>();

                        if (subscription != null)
                        {
                            string subscribedPlan = subscription.packageName;
                            DateTime start = subscription.subStart;
                            DateTime end = subscription.expiration;

                            //populate the textboxes for the subscription details
                            LblSubPlan.Text = subscribedPlan;

                            DateTime subscriptionStart = start;
                            LblDateStarted.Text = subscriptionStart.ToString("MMMM dd, yyyy - hh:mm tt");
                            DateTime subscriptionEnd = end;
                            LblSubEnd.Text = subscriptionEnd.ToString("MMMM dd, yyyy - hh:mm tt");


                        }
                    }
                    else if (subStatus == "notSubscribed")
                    {
                        warningMsg.Text = "Warning: You haven't subscribed to a package yet. Access to the features are disabled since your status is 'Not Subscribed' ";
                        subscriptionLabel.Text = "You haven't subscribed to a plan yet. Please proceed with the subscription now.";
                        changePackage.Visible = false;
                        renewBTN.Visible = false;
                        Label1.Visible = false;
                        LblSubPlan.Visible = false;
                        Label5.Visible = false;
                        LblDateStarted.Visible = false;
                        Label6.Visible = false;
                        LblSubEnd.Visible = false;
                       
                    }

                }
                else
                {
                    Response.Write("<script> window.location.href = '/LandingPage/Account.aspx';</script>");

                }
            }

        }
        //RENEW SUBSCRIPTION
        protected void btnSubscription_Click(object sender, EventArgs e)
        {
            var adminID = Session["idno"].ToString();
            FirebaseResponse subDetails = twoBigDB.Get("ADMIN/" + adminID + "/SubscribedPlan/");
            Model.SubscribedPlan subscription = subDetails.ResultAs<Model.SubscribedPlan>();

            Response.Write("<script>alert ('You are about to RENEW your " + subscription.subPlan + " subscription ! Click 'OK' to continue.'); window.location.href = '/Admin/PremiumSubSuccess.aspx'; </script>");



        }


        protected void btnEditProfile_Click(object sender, EventArgs e)  //UPDATE THE PROFILE INFORMATION
        {
            try
            {
                //generate a random number for users logged
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                string idno = (string)Session["idno"];

                // Retrieve the existing product data from the database
                var result = twoBigDB.Get("ADMIN/" + idno);
                AdminAccount obj = result.ResultAs<AdminAccount>();

                //fetch the existing data from the database
                var data = new AdminAccount();
                data.idno = obj.idno;
                data.fname = obj.fname;
                data.mname = obj.mname;
                data.lname = obj.lname;
                data.bdate = obj.bdate;
                data.phone = obj.phone;
                data.email = obj.email;
                data.pass = obj.pass;
                data.profile_image = obj.profile_image;
                data.status = obj.status;
                data.subStatus = obj.subStatus;

                // Update the fields that have changed
                if (!string.IsNullOrEmpty(firstname.Text))
                {
                    data.fname = Server.HtmlEncode(firstname.Text);
                }
                if (!string.IsNullOrEmpty(middlename.Text))
                {
                    data.mname = Server.HtmlEncode(middlename.Text);
                }
                if (!string.IsNullOrEmpty(lastname.Text))
                {
                    data.lname = Server.HtmlEncode(lastname.Text);
                }
                if (!string.IsNullOrEmpty(birthdate.Text))
                {
                    data.bdate = Server.HtmlEncode(birthdate.Text);
                }
                if (!string.IsNullOrEmpty(contactnum.Text))
                {
                    data.phone = Server.HtmlEncode(contactnum.Text);
                }
                if (!string.IsNullOrEmpty(email.Text))
                {
                    data.email = Server.HtmlEncode(email.Text);
                }


                FirebaseResponse response;
                response = twoBigDB.Update("ADMIN/" + idno, data);

                //SEND NOTIFICATION TO ADMIN 
                
                int ID = rnd.Next(1, 20000);
                var Notification = new Notification
                {
                    admin_ID = int.Parse(idno),
                    sender = "Super Admin",
                    title = "Profile Set-up",
                    receiver = "Admin",
                    body = "You have completed your personal information set-up. Please proceed in setting-up your Refilling Station details",
                    notificationDate = DateTime.Now,
                    status = "unread",
                    notificationID = ID

                };

                SetResponse notifResponse;
                notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                Notification notif = notifResponse.ResultAs<Notification>();//Database Result


                Response.Write("<script>alert ('Personal info has successfully updated!');window.location.href = '/Admin/AdminProfile.aspx';</script>");

                // Get the current date and time
                DateTime addedTime = DateTime.Now; ;


                //Store the login information in the USERLOG table
                var profilelog = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    userActivity = "UPDATE PROFILE",
                    role = "Admin",
                    activityTime = addedTime
                };

                //Storing the  info
                response = twoBigDB.Set("ADMINLOGS/" + profilelog.logsId, profilelog);//Storing data to the database

                // Display the image on the profile page
                ImageButton_new.ImageUrl = (string)Session["profile_image"];
            }
            catch (Exception ex)
            {
                // Handle the exception and log the error message
                string errorMessage = "An error occurred while updating the data: " + ex.Message;
            }
        }
        //UPDATE PROFILE PIC
        protected async void profileBtn_Click(object sender, EventArgs e)
        {
            //generate a random number for users logged
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            string idno = (string)Session["idno"];

            // Retrieve the existing product data from the database
            var result = twoBigDB.Get("ADMIN/" + idno);
            AdminAccount obj = result.ResultAs<AdminAccount>();

            if (imgProfile.HasFile)
            {
                // Get the file name and extension
                string fileName = Path.GetFileName(imgProfile.PostedFile.FileName);
                string fileExtension = Path.GetExtension(fileName);

                // Generate a unique file name
                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                // Upload the file to Firebase Storage
                var storage = new FirebaseStorage("big-system-64b55.appspot.com");
                var task = await storage.Child("profile-images").Child(uniqueFileName).PutAsync(imgProfile.PostedFile.InputStream);

                // Get the download URL of the uploaded file
                string imageUrl = await storage.Child("profile-images").Child(uniqueFileName).GetDownloadUrlAsync();

                // Insert the download URL into the Admin table
                var data = new
                {
                    idno = int.Parse(Lbl_Idno.Text),
                    profile_image = imageUrl
                };
                var response = await twoBigDB.UpdateAsync("ADMIN/" + Lbl_Idno.Text, data);


                // Display the image on the profile page
                ImageButton_new.ImageUrl = imageUrl;
            }

            // Get the current date and time
            DateTime addedTime = DateTime.Now; ;


            //Store the login information in the USERLOG table
            var profilelog = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = idnum,
                userFullname = (string)Session["fullname"],
                userActivity = "UPLOADED PROFILE IMAGE",
                role = "Admin",
                activityTime = addedTime
            };

            //Storing the  info
            SetResponse logresponse = twoBigDB.Set("ADMINLOGS/" + profilelog.logsId, profilelog);//Storing data to the database
            Response.Write("<script>window.location.href = '/Admin/AdminProfile.aspx';</script>");
        }


        //UPDATE AND CREATE REFILLING STATION DETAILS
        protected void btnManageStation_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];

            //generate a random number for users logged
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Check if the RefillingStation object already exists in the database
            var result = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
            RefillingStation stationObject = result.ResultAs<RefillingStation>();
            string checkHours = stationObject.operatingHrsFrom;

            //UPDATE THE EXISTING DETAILS
            if (checkHours != null)
            {
                var station = new RefillingStation();

               
                RefillingStation obj = result.ResultAs<RefillingStation>();

                //FETCH ALL THE DATA PARA DI MAWALA IG UPDATE
                //EDITABLE
                station.operatingHrsFrom = obj.operatingHrsFrom;
                station.operatingHrsTo = obj.operatingHrsTo;

                station.businessDaysFrom = obj.businessDaysFrom;
                station.businessDaysTo = obj.businessDaysTo;

                //NOT EDITABLE
                station.stationName = obj.stationName;
                station.status = obj.status;
                station.addLattitude = obj.addLattitude;
                station.addLongitude = obj.addLongitude;
                station.dateAdded = obj.dateAdded;
                station.stationAddress = obj.stationAddress;
                station.dateUpdated = DateTime.Now;

                if (!string.IsNullOrEmpty(Request.Form[txtOperatingHrsFrom.UniqueID].ToString()))
                {
                    DateTime operatingHrsFrom = DateTime.ParseExact(txtOperatingHrsFrom.Text, "HH:mm", CultureInfo.InvariantCulture);
                    station.operatingHrsFrom = operatingHrsFrom.ToString("h:mm tt");
                }
                if (!string.IsNullOrEmpty(Request.Form[txtOperatingHrsTo.UniqueID].ToString()))
                {
                    DateTime operatingHrsTo = DateTime.ParseExact(txtOperatingHrsTo.Text, "HH:mm", CultureInfo.InvariantCulture);
                    station.operatingHrsTo = operatingHrsTo.ToString("h:mm tt");
                    //station.operatingHrsTo = operatingHrsTo12Hr;
                }


                // Update the station business days
                if (!string.IsNullOrEmpty(Request.Form[drdBusinessDaysFrom.UniqueID].ToString()))
                {
                    station.businessDaysFrom = Request.Form[drdBusinessDaysFrom.UniqueID].ToString();
                }
                if (!string.IsNullOrEmpty(Request.Form[drdBusinessDaysTo.UniqueID].ToString()))
                {
                    station.businessDaysTo = Request.Form[drdBusinessDaysTo.UniqueID].ToString();
                }

                // Pass the updated object to the Update method
                FirebaseResponse response;
                response = twoBigDB.Update("ADMIN/" + idno + "/RefillingStation/", station);
                //SEND NOTIFICATION TO ADMIN 

                int ID = rnd.Next(1, 20000);
                var Notification = new Notification
                {
                    admin_ID = int.Parse(idno),
                    sender = "Super Admin",
                    title = "Station update",
                    receiver = "Admin",
                    body = "You updated your Refilling Station details.",
                    notificationDate = DateTime.Now,
                    status = "unread",
                    notificationID = ID

                };

                SetResponse notifResponse;
                notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                Notification notif = notifResponse.ResultAs<Notification>();//Database Result


                Response.Write("<script>alert ('Station details successfully updated!');window.location.href = '/Admin/AdminProfile.aspx';</script>");



                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    role =  "Admin",
                    userFullname = (string)Session["fullname"],
                    activityTime = addedTime,
                    userActivity = "UPDATED STATION DETAILS",
                };
                
                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

            } 
            //CREATE NEW DETAILS
            else 
            {

                // Parse the operating hours from and to times as DateTime objects
                DateTime operatingHrsFrom = DateTime.ParseExact(txtOperatingHrsFrom.Text, "HH:mm", CultureInfo.InvariantCulture);
                DateTime operatingHrsTo = DateTime.ParseExact(txtOperatingHrsTo.Text, "HH:mm", CultureInfo.InvariantCulture);

                // Convert the times to 12-hour format with AM and PM
                string operatingHrsFrom12Hr = operatingHrsFrom.ToString("h:mm tt");
                string operatingHrsTo12Hr = operatingHrsTo.ToString("h:mm tt");

                //FETCH ALL THE DATA PARA DI MAWALA IG UPDATE
                RefillingStation obj = result.ResultAs<RefillingStation>();

                // Create a new RefillingStation object
                RefillingStation newObj = new RefillingStation();

                // Set the properties of the new object
                newObj.operatingHrsFrom = operatingHrsFrom12Hr;
                newObj.operatingHrsTo = operatingHrsTo12Hr;
                newObj.businessDaysFrom = drdBusinessDaysFrom.SelectedValue;
                newObj.businessDaysTo = drdBusinessDaysTo.SelectedValue;
                newObj.dateAdded = DateTime.Now;
                newObj.dateUpdated = DateTime.Now;
                //NOT EDITABLE
                newObj.stationAddress = obj.stationAddress;
                newObj.stationName = obj.stationName;
                newObj.addLongitude = obj.addLongitude;
                newObj.addLattitude = obj.addLattitude;
                newObj.status = obj.status;

                // Save the new object to the database
                twoBigDB.Set("ADMIN/" + idno + "/RefillingStation/", newObj);

                // Check if the RefillingStation object already exists in the database
                var adminresult = twoBigDB.Get("ADMIN/" + idno);
                AdminAccount admin = adminresult.ResultAs<AdminAccount>();

                if (admin.status == "pending")
                {
                    //SEND NOTIFICATION TO ADMIN 

                    int ID = rnd.Next(1, 20000);
                    var Notification = new Notification
                    {
                        admin_ID = int.Parse(idno),
                        sender = "Super Admin",
                        title = "Refilling Station Pre-setup",
                        receiver = "Admin",
                        body = "You have completed your Refilling Station set-up! However, your account is still pending. Please wait for your account to be approved before you can completely manage your refilling business.",
                        notificationDate = DateTime.Now,
                        status = "unread",
                        notificationID = ID

                    };

                    SetResponse notifResponse;
                    notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                    Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                }
                else
                {
                    //SEND NOTIFICATION TO ADMIN 

                    int ID = rnd.Next(1, 20000);
                    var Notification = new Notification
                    {
                        admin_ID = int.Parse(idno),
                        sender = "Super Admin",
                        title = "Refilling Station Set-up",
                        receiver = "Admin",
                        body = "You have completed your Refilling Station set-up! You can now add your products! Please proceed to Products page.",
                        notificationDate = DateTime.Now,
                        status = "unread",
                        notificationID = ID

                    };

                    SetResponse notifResponse;
                    notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                    Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                }


                Response.Write("<script>alert ('Station details successfully added!');window.location.href = '/Admin/AdminProfile.aspx';</script>");
               
                //int logsId = (int)Session["logsId"];

                //// Retrieve the existing Users log object from the database
                //FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
                //UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    activityTime = addedTime,
                    role = "Admin",
                    userActivity = "ADDED THE STATION DETAILS",
                };

                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                // Display the image on the profile page
                ImageButton_new.ImageUrl = (string)Session["profile_image"];
            }
        }

        protected void renewBTN_Click(object sender, EventArgs e)
        {
            string adminID = Session["idno"].ToString();

            FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + adminID + "/Subscribed_Package");
            Model.Subscribed_Package package = adminDet.ResultAs<Model.Subscribed_Package>();

            if (package != null)
            {
                adminDet = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + package.packageID);
                Model.PackagePlans subscribed = adminDet.ResultAs<Model.PackagePlans>();

                //to check if the package is renewable or not
                if (subscribed.renewable == "No")
                {
                    //renewBTN.Enabled = false;
                    Response.Write("<script>alert ('Your current package is not RENEWABLE. Redirecting you to Subscription Packages now.');  window.location.href = '/Admin/SubscriptionPackages.aspx';</script>");
                   
                }
                else
                {
                    Response.Write("<script> window.location.href = '/Admin/SubscriptionPackages.aspx';</script>");
                }
  
            }
        }

        protected void confirmChangePackage_Click(object sender, EventArgs e)
        {
            
            Response.Write("<script>alert('Redirecting you to the active packages');window.location.href = '/Admin/SubscriptionPackages.aspx';</script>");
        }
    }
}
