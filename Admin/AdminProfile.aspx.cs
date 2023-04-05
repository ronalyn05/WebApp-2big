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

            //to GET the subbscription
            var adminID = Session["idno"].ToString();
            FirebaseResponse subDetails = twoBigDB.Get("ADMIN/" + adminID + "/SubscribedPlan/");
            Model.SubscribedPlan subscription = subDetails.ResultAs<Model.SubscribedPlan>();

           string subscribedPlan = subscription.subPlan;
            DateTime start = subscription.subStart;
            DateTime end = subscription.subEnd;
            
            //populate the textboxes for the subscription details
            LblSubPlan.Text = subscribedPlan;

            DateTime subscriptionStart = start;
            LblDateStarted.Text = subscriptionStart.ToString();
            DateTime subscriptionEnd = end;
            LblSubEnd.Text = subscriptionEnd.ToString();

            //Session["subscribedPlan"] = plan;
            //Session["subscriptionEnd"] = SubBasic;
            //Session["subscriptionStart"] = now;


            //firstname.Text = (string)Session["fname"];
            //middlename.Text = (string)Session["mname"];
            //lastname.Text = (string)Session["lname"];
            //contactnum.Text = (string)Session["contactNumber"];
            //email.Text = (string)Session["email"];
            //birthdate.Text = (string)Session["dob"];
            //Lbl_user.Text = (string)Session["fullName"];

            //retrieve the data of station details
            lblStationName.Text = (string)Session["stationName"];
            lblAddress.Text = (string)Session["address"];
            lblOperatingHours.Text = (string)Session["operatingHrs"];
            lblBusinessday.Text = (string)Session["businessDays"];
            lblstatus.Text = (string)Session["status"];
            ImageButton_new.ImageUrl = (string)Session["profile_image"];

            //LblSubPlan.Text = (string)Session["subType"];
            //LblDateStarted.Text = Session["subsDate"].ToString();
            //LblSubEnd.Text = Session["subEnd"].ToString();

            // Make sure that the object in the Session is a byte array representing the image
            //byte[] profileImage = Session["profile_image"] as byte[];
            //if (profileImage != null)
            //{
            //    // Convert the byte array to a base64-encoded string
            //    string base64String = Convert.ToBase64String(profileImage, 0, profileImage.Length);
            //    // Set the ImageUrl property of the ImageButton to the base64-encoded string
            //    ImageButton_new.ImageUrl = "data:image/png;base64," + base64String;
            //    //string base64String = Convert.ToBase64String(imageData);
            //}
           

            //FirebaseResponse response;
            //response = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
            //RefillingStation obj = response.ResultAs<RefillingStation>();

            //LblSubPlan.Text = obj.SubType.ToString();
            //LblDateStarted.Text = obj.SubsDate.ToString();
            //LblSubEnd.Text = obj.SubEnd.ToString();
            //Session["operatingHrs"] = obj.operatingHrs;
            //Session["status"] = obj.status;
            //Session["businessdays"] = obj.businessDays;

            



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
        protected void btnEditStationDetails_Click(object sender, EventArgs e)
        {
            try
            {
                string idno = (string)Session["idno"];

                // Retrieve the existing product data from the database
                var result = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
                RefillingStation obj = result.ResultAs<RefillingStation>();

                // Create a new object and copy the existing data
                var data = new RefillingStation();
                data.stationName = obj.stationName;
                data.stationAddress = obj.stationAddress;
                data.addLattitude = obj.addLattitude;
                data.addLongitude = obj.addLongitude;
                data.proof = obj.proof;
                data.operatingHrs = obj.operatingHrs;
                data.status = obj.status;
                data.businessDays = obj.businessDays;
                data.dateAdded = obj.dateAdded;

                // Update the fields that have changed
                if (!string.IsNullOrEmpty(txtOperatingHrs.Text))
                {
                    data.operatingHrs = txtOperatingHrs.Text;
                }
                if (!string.IsNullOrEmpty(txtBusinessDays.Text))
                {
                    data.businessDays = txtBusinessDays.Text;
                }
                if (!string.IsNullOrEmpty(operatingStatus.Text))
                {
                    data.status = operatingStatus.Text;
                }
                data.dateUpdated = DateTime.UtcNow;

                FirebaseResponse response;
                response = twoBigDB.Update("ADMIN/" + idno + "/RefillingStation/", data);

                // Retrieve the updated product data from the database
                result = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
                obj = result.ResultAs<RefillingStation>();

                lblStationName.Text = obj.stationName.ToString();
                lblAddress.Text = obj.stationAddress.ToString();
                lblOperatingHours.Text = obj.operatingHrs.ToString();
                lblBusinessday.Text = obj.businessDays.ToString();
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


                Response.Write("<script>alert ('Personal info has successfully updated!');</script>");
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
        //STORE STATION DETAILS
        protected void btnManageStation_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            // Retrieve the existing object from the database
            var result = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
            RefillingStation obj = result.ResultAs<RefillingStation>();

            // Update the specific fields that you want to change
            obj.operatingHrs = txtOperatingHrs.Text;
            obj.status = operatingStatus.Text;
            obj.businessDays = txtBusinessDays.Text;

            // Pass the updated object to the Update method
            twoBigDB.Update("ADMIN/" + idno + "/RefillingStation/", obj);

            // Retrieve the updated data from the database
            var res = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
            RefillingStation list = res.ResultAs<RefillingStation>();//Database Result

            // Display the updated data in the UI
            lblOperatingHours.Text = list.operatingHrs;
            lblBusinessday.Text = list.businessDays.ToString();
            lblstatus.Text = list.status.ToString();
        }
    }
}
