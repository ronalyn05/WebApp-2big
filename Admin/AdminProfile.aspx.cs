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
           
           //  string idno = (string)Session["idno"];
            Lbl_Idno.Text = (string)Session["idno"];
            lblfname.Text = (string)Session["fname"];
            lblmname.Text = (string)Session["mname"];
            lblLname.Text = (string)Session["lname"];
            lblcontactnum.Text = (string)Session["contactNumber"];
            lblemail.Text = (string)Session["email"];
            lbldob.Text = (string)Session["dob"];
            Lbl_user.Text = (string)Session["fullName"];


            firstname.Text = (string)Session["fname"];
            middlename.Text = (string)Session["mname"];
            lastname.Text = (string)Session["lname"];
            contactnum.Text = (string)Session["contactNumber"];
            email.Text = (string)Session["email"];
            birthdate.Text = (string)Session["dob"];

            //to GET the subscription
            var adminID = Session["idno"].ToString();
           
            FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + adminID);
            Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();

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
            else
            {
               
                Response.Write("<script>alert ('You haven't subscribed to a plan yet. Please proceed with the subscription now.'); window.location.href = '/Admin/SubscriptionPlans.aspx';</script>");
            }

            

            //TO GET THE REFILLING STATION DETAILS
            FirebaseResponse stationDetails = twoBigDB.Get("ADMIN/" + adminID + "/RefillingStation/");
            Model.RefillingStation refillStation = stationDetails.ResultAs<Model.RefillingStation>();

            string days = refillStation.businessDaysFrom;
            
            //to check if naka add na ug station details
            if (days != null)
            {
                //retrieve the data of station details
                lblStationName.Text = refillStation.stationName;
                lblAddress.Text = refillStation.stationAddress;
                lblOperatingHours.Text = refillStation.operatingHrsFrom + "AM - " + refillStation.operatingHrsTo + "PM";
                lblBusinessday.Text = refillStation.businessDaysFrom + " - " + refillStation.businessDaysTo;

                DateTime timeNow = DateTime.Now;
                DateTime operatingHrsFrom = DateTime.Parse(refillStation.operatingHrsFrom);
                DateTime operatingHrsTo = DateTime.Parse(refillStation.operatingHrsTo);
                string businessClose = refillStation.businessDaysTo;
                //method to convert the businessDaysTo from DB value to a DayOfWeek enum value
                DayOfWeek storeClose = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), businessClose);


                //TO CHECK IF THE CURRENT TIME IS WITHIN THE OPERATING HOURS SET BY THE ADMIN
                string status = "CLOSE";
                lblstatus.Text = status;
                if (timeNow >= operatingHrsFrom && timeNow <= operatingHrsTo)
                {

                    status = "OPEN";
                    lblstatus.Text = status; //display sa profile

                    //if (timeNow.DayOfWeek == storeClose)
                    //{
                    //    status = "CLOSE";
                    //    lblstatus.Text = status;
                    //}
                }

                else
                {
                    status = "CLOSE";
                    lblstatus.Text = status;
                }
                var statusUpdate = new Dictionary<string, object>
                {
                  { "status", status }
                };

                FirebaseResponse response = twoBigDB.Update("ADMIN/" + adminID + "/RefillingStation/", statusUpdate);
                //POPULATE THE REFILL STATION DETAILS IN THE MODAL
                txtOperatingHrsFrom.Text = refillStation.operatingHrsFrom;
                txtOperatingHrsTo.Text = refillStation.operatingHrsTo;
                drdBusinessDaysFrom.Text = refillStation.businessDaysFrom;
                drdBusinessDaysTo.Text = refillStation.businessDaysTo;

            }
            else
            {

                Response.Write("<script>alert ('You haven't finished setting up your station details yet. Please complete your refilling station details now.');</script>");
            }

            

            ImageButton_new.ImageUrl = (string)Session["profile_image"];

          
        }

        protected void btnSubscription_Click(object sender, EventArgs e)
        {



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
        protected void btnEditStationDetails_Click(object sender, EventArgs e)  //EDIT STATION DETAILS - UPDATE BUTTON
        {
            try
            {
                string idno = (string)Session["idno"];

                // Retrieve the existing product data from the database
                var result = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
                RefillingStation obj = result.ResultAs<RefillingStation>();

                // Create a new object and copy the existing data - DISPLAY THE DATA IN THE MODAL
                var data = new RefillingStation();
                data.stationName = obj.stationName;
                data.stationAddress = obj.stationAddress;
                data.addLattitude = obj.addLattitude;
                data.addLongitude = obj.addLongitude;
                data.proof = obj.proof;
                data.operatingHrsTo = obj.operatingHrsTo;
                data.operatingHrsFrom = obj.operatingHrsFrom;
                data.status = obj.status;
                data.businessDaysTo = obj.businessDaysTo;
                data.businessDaysFrom = obj.businessDaysFrom;
                data.dateAdded = obj.dateAdded;

                // Update the fields that have changed
                if (!string.IsNullOrEmpty(txtOperatingHrsFrom.Text) || !string.IsNullOrEmpty(txtOperatingHrsTo.Text))
                {
                    data.operatingHrsFrom = txtOperatingHrsFrom.Text;
                    data.operatingHrsTo = txtOperatingHrsTo.Text;
                }
                if (!string.IsNullOrEmpty(drdBusinessDaysFrom.SelectedValue) || !string.IsNullOrEmpty(drdBusinessDaysTo.SelectedValue))
                {
                    data.businessDaysFrom = drdBusinessDaysFrom.SelectedValue;
                    data.businessDaysTo = drdBusinessDaysTo.SelectedValue;
                }

                data.dateUpdated = DateTime.UtcNow;

                FirebaseResponse response;
                response = twoBigDB.Update("ADMIN/" + idno + "/RefillingStation/", data);

                // Retrieve the updated product data from the database
                result = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
                obj = result.ResultAs<RefillingStation>();

                lblStationName.Text = obj.stationName.ToString();
                lblAddress.Text = obj.stationAddress.ToString();
                lblOperatingHours.Text = obj.operatingHrsFrom.ToString() + " - " + obj.operatingHrsTo.ToString();
                lblBusinessday.Text = obj.businessDaysFrom.ToString() + " - " + obj.businessDaysTo.ToString();
                lblstatus.Text = obj.status.ToString();

                Response.Write("<script>alert ('Station details has successfully updated!');</script>");
            }
            catch (Exception ex)
            {
                // Handle the exception and log the error message
                string errorMessage = "An error occurred while updating the data: " + ex.Message;
            }
        }
        //UPDATE THE PROFILE INFORMATION
        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            try
            {
                string idno = (string)Session["idno"];


                // Retrieve the existing product data from the database
                var result = twoBigDB.Get("ADMIN/" + idno);
                AdminAccount obj = result.ResultAs<AdminAccount>();

                // Create a new object and copy the existing data
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
                if (!string.IsNullOrEmpty(ImageButton_new.ImageUrl))
                {
                    data.profile_image = ImageButton_new.ImageUrl;
                }

                FirebaseResponse response;
                response = twoBigDB.Update("ADMIN/" + idno, data);

                // Retrieve the updated product data from the database
                result = twoBigDB.Get("ADMIN/" + idno);
                obj = result.ResultAs<AdminAccount>();

                Lbl_Idno.Text = obj.idno.ToString();
                lblfname.Text = obj.fname.ToString();
                lblmname.Text = obj.mname.ToString();
                lblLname.Text = obj.lname.ToString();
                lbldob.Text = obj.bdate.ToString();
                lblcontactnum.Text = obj.phone.ToString();
                lblemail.Text = obj.email.ToString();
                ImageButton_new.ImageUrl = obj.profile_image.ToString();


                Response.Write("<script>alert ('Personal info has successfully updated!'); location.reload(); window.location.href = '/Admin/AdminProfile.aspx';</script>");
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
        //STORE STATION DETAILS - ADD BUTTON
        //protected void btnManageStation_Click(object sender, EventArgs e)
        //{
        //    string idno = (string)Session["idno"];
        //    // Retrieve the existing object from the database
        //    var result = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
        //    RefillingStation obj = result.ResultAs<RefillingStation>();

        //    // Update the specific fields that you want to change
        //    obj.operatingHrsFrom = txtOperatingHrsFrom.Text;
        //    obj.operatingHrsTo = txtOperatingHrsTo.Text;
        //    obj.businessDaysFrom = drdBusinessDaysFrom.SelectedValue;
        //    obj.businessDaysTo = drdBusinessDaysTo.SelectedValue;

        //    // Pass the updated object to the Update method
        //    twoBigDB.Update("ADMIN/" + idno + "/RefillingStation/", obj);

        //    // Retrieve the updated data from the database  
        //    var res = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
        //    RefillingStation list = res.ResultAs<RefillingStation>();//Database Result

        //    Response.Write("<script>alert ('Station details successfully added!');</script>");
        //}

        protected void btnManageStation_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];

            // Check if the RefillingStation object already exists in the database
            var result = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
            if (result != null)
            {
                
                // Update the existing RefillingStation object
                RefillingStation obj = result.ResultAs<RefillingStation>();

                // Check if the textboxes and dropdown lists have values
                if (!string.IsNullOrEmpty(txtOperatingHrsFrom.Text.Trim()))
                {
                    obj.operatingHrsFrom = txtOperatingHrsFrom.Text;
                }

                if (!string.IsNullOrEmpty(txtOperatingHrsTo.Text.Trim()))
                {
                    obj.operatingHrsTo = txtOperatingHrsTo.Text;
                }

                if (!string.IsNullOrEmpty(drdBusinessDaysFrom.SelectedValue))
                {
                    obj.businessDaysFrom = drdBusinessDaysFrom.SelectedValue;
                }

                if (!string.IsNullOrEmpty(drdBusinessDaysTo.SelectedValue))
                {
                    obj.businessDaysTo = drdBusinessDaysTo.SelectedValue;
                }

                // Pass the updated object to the Update method
                twoBigDB.Update("ADMIN/" + idno + "/RefillingStation/", obj);


                Response.Write("<script>alert ('Station details successfully updated!');</script>");
            }
            else
            {
                // Create a new RefillingStation object
                RefillingStation obj = new RefillingStation();

                // Set the properties of the new object
                obj.operatingHrsFrom = txtOperatingHrsFrom.Text;
                obj.operatingHrsTo = txtOperatingHrsTo.Text;
                obj.businessDaysFrom = drdBusinessDaysFrom.SelectedValue;
                obj.businessDaysTo = drdBusinessDaysTo.SelectedValue;

                // Save the new object to the database
                twoBigDB.Set("ADMIN/" + idno + "/RefillingStation/", obj);

                Response.Write("<script>alert ('Station details successfully added!');</script>");
            }
        }


    }
}
