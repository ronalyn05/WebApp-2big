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
           
            
            var adminID = Session["idno"].ToString();

            FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + adminID);
            Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();

           
            if (adminDet != null)
            {
                //POPULATE THE PERSONAL DETAILS IN THE TEXTBOXES
                Lbl_Idno.Text = admin.idno.ToString();
                lblfname.Text = admin.fname;
                lblmname.Text = admin.mname;
                lblLname.Text = admin.lname;
                lblcontactnum.Text = admin.phone;
                lblemail.Text = admin.email;
                lbldob.Text = admin.bdate;

                if (admin.profile_image != null)
                {
                    ImageButton_new.ImageUrl = admin.profile_image.ToString();

                }

                //POPULATE THE PERSONAL DETAILS IN THE MODAL
                firstname.Attributes["placeholder"] = admin.fname;
                middlename.Attributes["placeholder"] = admin.mname;
                lastname.Attributes["placeholder"] = admin.lname;
                contactnum.Attributes["placeholder"] = admin.phone;
                email.Attributes["placeholder"] = admin.email;
                birthdate.Text = admin.bdate;

                //to GET the subscription details
                string subStatus = admin.subStatus;
                //check if naka subscribe na ba ang admin 
                if (subStatus == "Subscribed")
                {
                    FirebaseResponse subDetails = twoBigDB.Get("ADMIN/" + adminID + "/SubscribedPlan/");
                    Model.SubscribedPlan subscription = subDetails.ResultAs<Model.SubscribedPlan>();

                    string subscribedPlan = subscription.subPlan;
                    DateTimeOffset start = subscription.subStart;
                    DateTimeOffset end = subscription.subEnd;

                    //populate the textboxes for the subscription details
                    LblSubPlan.Text = subscribedPlan;

                    DateTimeOffset subscriptionStart = start;
                    LblDateStarted.Text = subscriptionStart.ToString();
                    DateTimeOffset subscriptionEnd = end;
                    LblSubEnd.Text = subscriptionEnd.ToString();


                }
                else if (subStatus == "notSubscribed")
                {

                    Response.Write("<script>alert ('You haven't subscribed to a plan yet. Please proceed with the subscription now.'); window.location.href = '/Admin/SubscriptionPlans.aspx';</script>");
                }
            }
            else
            {
                Response.Write("<script> window.location.href = '/LandingPage/Account.aspx';</script>");

            }





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
                txtOperatingHrsFrom.Text = refillStation.operatingHrsFrom.ToString();
                txtOperatingHrsTo.Text = refillStation.operatingHrsTo;
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

            

            ImageButton_new.ImageUrl = (string)Session["profile_image"];

          
        }

        //RENEW SUBSCRIPTION
        protected void btnSubscription_Click(object sender, EventArgs e)
        {
            var adminID = Session["idno"].ToString();
            FirebaseResponse subDetails = twoBigDB.Get("ADMIN/" + adminID + "/SubscribedPlan/");
            Model.SubscribedPlan subscription = subDetails.ResultAs<Model.SubscribedPlan>();

            Response.Write("<script>alert ('You are about to RENEW your " + subscription.subPlan + " subscription ! Click 'OK' to continue.'); window.location.href = '/Admin/PremiumSubSuccess.aspx'; </script>");



        }

        //private void RetrieveRecords()
        //{
        //    try
        //    {
        //        //Retrieve Data
        //        FirebaseResponse response = twoBigDB.Get("ADMIN");
        //        Model.AdminAccount obj = response.ResultAs<Model.AdminAccount>();
        //        var json = response.Body;
        //        Dictionary<string, Model.AdminAccount> list = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(json);

        //        foreach (KeyValuePair<string, Model.AdminAccount> item in list)
        //        {
        //            LstBoxAdminProfile.Items.Add(item.Value.Idno.ToString());
        //            LstBoxAdminProfile.Items.Add(item.Value.Fname.ToString()
        //            + " " + item.Value.Mname.ToString()
        //            + " " + item.Value.Lname.ToString()
        //            + " " + item.Value.Phone.ToString()
        //            + " " + item.Value.Email.ToString()
        //            + " " + item.Value.Address.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<pre>" + ex.ToString() + "<pre>");
        //    }
        //}
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{

        //    String searchStr;
        //    searchStr = LstBoxAdminProfile.SelectedValue;
        //    FirebaseResponse response;
        //    response = twoBigDB.Get("ADMIN/" + searchStr);
        //    Model.AdminAccount obj = response.ResultAs<Model.AdminAccount>();

        //    Lbl_fname.Text = obj.Fname.ToString();
        //    Lbl_mname.Text = obj.Mname.ToString();
        //    Lbl_lname.Text = obj.Lname.ToString();
        //    Lbl_contact.Text = obj.Phone.ToString();
        //    Lbl_email.Text = obj.Email.ToString();
        //    Lbl_address.Text = obj.Address.ToString();
        //}

        //UPDATE THE STATION DETAILS

        //EDIT STATION DETAILS - UPDATE BUTTON
        //protected void btnEditStationDetails_Click(object sender, EventArgs e)  //EDIT STATION DETAILS - UPDATE BUTTON
        //{
        //    try
        //    {
        //        string idno = (string)Session["idno"];

        //        // Retrieve the existing product data from the database
        //        var result = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
        //        RefillingStation obj = result.ResultAs<RefillingStation>();

        //        // Create a new object and copy the existing data - DISPLAY THE DATA IN THE MODAL
        //        var data = new RefillingStation();
        //        data.stationName = obj.stationName;
        //        data.stationAddress = obj.stationAddress;
        //        data.addLattitude = obj.addLattitude;
        //        data.addLongitude = obj.addLongitude;
        //        data.proof = obj.proof;
        //        data.operatingHrsTo = obj.operatingHrsTo;
        //        data.operatingHrsFrom = obj.operatingHrsFrom;
        //        data.status = obj.status;
        //        data.businessDaysTo = obj.businessDaysTo;
        //        data.businessDaysFrom = obj.businessDaysFrom;
        //        data.dateAdded = obj.dateAdded;

        //        // Update the fields that have changed
        //        if (!string.IsNullOrEmpty(txtOperatingHrsFrom.Text) || !string.IsNullOrEmpty(txtOperatingHrsTo.Text))
        //        {
        //            data.operatingHrsFrom = txtOperatingHrsFrom.Text;
        //            data.operatingHrsTo = txtOperatingHrsTo.Text;
        //        }
        //        if (!string.IsNullOrEmpty(drdBusinessDaysFrom.SelectedValue) || !string.IsNullOrEmpty(drdBusinessDaysTo.SelectedValue))
        //        {
        //            data.businessDaysFrom = drdBusinessDaysFrom.SelectedValue;
        //            data.businessDaysTo = drdBusinessDaysTo.SelectedValue;
        //        }

        //        data.dateUpdated = DateTime.UtcNow;

        //        FirebaseResponse response;
        //        response = twoBigDB.Update("ADMIN/" + idno + "/RefillingStation/", data);

        //        // Retrieve the updated product data from the database
        //        result = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
        //        obj = result.ResultAs<RefillingStation>();

        //        lblStationName.Text = obj.stationName.ToString();
        //        lblAddress.Text = obj.stationAddress.ToString();
        //        lblOperatingHours.Text = obj.operatingHrsFrom.ToString() + " - " + obj.operatingHrsTo.ToString();
        //        lblBusinessday.Text = obj.businessDaysFrom.ToString() + " - " + obj.businessDaysTo.ToString();
        //        lblstatus.Text = obj.status.ToString();

        //        Response.Write("<script>alert ('Station details has successfully updated!');</script>");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the exception and log the error message
        //        string errorMessage = "An error occurred while updating the data: " + ex.Message;
        //    }
        //}


       
        protected void btnEditProfile_Click(object sender, EventArgs e)  //UPDATE THE PROFILE INFORMATION
        {
            try
            {
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
                    data.fname = firstname.Text;
                }
                if (!string.IsNullOrEmpty(middlename.Text))
                {
                    data.mname = middlename.Text;
                }
                if (!string.IsNullOrEmpty(lastname.Text))
                {
                    data.lname = lastname.Text;
                }
                if (!string.IsNullOrEmpty(birthdate.Text))
                {
                    data.bdate = birthdate.Text;
                }
                if (!string.IsNullOrEmpty(contactnum.Text))
                {
                    data.phone = contactnum.Text;
                }
                if (!string.IsNullOrEmpty(email.Text))
                {
                    data.email = email.Text;
                }


                FirebaseResponse response;
                response = twoBigDB.Update("ADMIN/" + idno, data);
                Response.Write("<script>alert ('Personal info has successfully updated!');window.location.href = '/Admin/AdminProfile.aspx';</script>");
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

        }


        //UPDATE AND CREATE REFILLING STATION DETAILS
        protected void btnManageStation_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];

            // Check if the RefillingStation object already exists in the database
            var result = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
            RefillingStation stationObject = result.ResultAs<RefillingStation>();
            string checkHours = stationObject.operatingHrsFrom;

            //UPDATE THE EXISTING DETAILS
            if (checkHours != null)
            {
                var station = new RefillingStation();

                //FETCH ALL THE DATA PARA DI MAWALA IG UPDATE
                RefillingStation obj = result.ResultAs<RefillingStation>();

                DateTime operatingHrsFrom = DateTime.ParseExact(obj.operatingHrsFrom, "h:mm tt", CultureInfo.InvariantCulture);
                DateTime operatingHrsTo = DateTime.ParseExact(obj.operatingHrsTo, "h:mm tt", CultureInfo.InvariantCulture);

                // Convert the updated time strings to DateTime objects in 12-hour format with AM/PM
                DateTime updatedOperatingHrsFrom = DateTime.ParseExact(Request.Form[txtOperatingHrsFrom.UniqueID].ToString(), "h:mm tt", CultureInfo.InvariantCulture);
                DateTime updatedOperatingHrsTo = DateTime.ParseExact(Request.Form[txtOperatingHrsTo.UniqueID].ToString(), "h:mm tt", CultureInfo.InvariantCulture);


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
                station.dateUpdated = DateTime.UtcNow;


                // Convert the operating hours from and to times to 12-hour format with AM and PM
                if (!string.IsNullOrEmpty(Request.Form[txtOperatingHrsFrom.UniqueID].ToString()))
                {
                    station.operatingHrsFrom = updatedOperatingHrsFrom.ToString("h:mm tt");
                }
                if (!string.IsNullOrEmpty(Request.Form[txtOperatingHrsTo.UniqueID].ToString()))
                {
                    station.operatingHrsTo = updatedOperatingHrsTo.ToString("h:mm tt");
                }

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
                Response.Write("<script>alert ('Station details successfully updated!');window.location.href = '/Admin/AdminProfile.aspx';</script>");

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
                newObj.dateAdded = DateTime.UtcNow;
                newObj.dateUpdated = DateTime.UtcNow;
                //NOT EDITABLE
                newObj.stationAddress = obj.stationAddress;
                newObj.stationName = obj.stationName;
                newObj.addLongitude = obj.addLongitude;
                newObj.addLattitude = obj.addLattitude;
                newObj.status = obj.status;

                // Save the new object to the database
                twoBigDB.Set("ADMIN/" + idno + "/RefillingStation/", newObj);

                Response.Write("<script>alert ('Station details successfully added!');window.location.href = '/Admin/AdminProfile.aspx';</script>");

            }
        }

    }
}
