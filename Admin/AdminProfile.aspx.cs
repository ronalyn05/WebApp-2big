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
            txtfname.Text = (string)Session["fname"];
            txtmname.Text = (string)Session["mname"];
            txtlname.Text = (string)Session["lname"];
            txtcontact.Text = (string)Session["contactNumber"];
            txtemail.Text = (string)Session["email"];
            txtdob.Text = (string)Session["dob"];
            Lbl_user.Text = (string)Session["fullName"];

            //retrieve the data of station details
            lblStationName.Text = (string)Session["stationName"];
            lblAddress.Text = (string)Session["address"];
            txtOperatngHours.Text = (string)Session["operatingHrs"];
            txtBssnessDay.Text = (string)Session["businessDays"];
            txt_Status.Text = (string)Session["status"]; 

            //LblSubPlan.Text = (string)Session["subType"];
            //LblDateStarted.Text = Session["subsDate"].ToString();
            //LblSubEnd.Text = Session["subEnd"].ToString();

           // Make sure that the object in the Session is a byte array representing the image
            byte[] profileImage = Session["profile_image"] as byte[];
            if (profileImage != null)
            {
                // Convert the byte array to a base64-encoded string
                string base64String = Convert.ToBase64String(profileImage, 0, profileImage.Length);
                // Set the ImageUrl property of the ImageButton to the base64-encoded string
                ImageButton_new.ImageUrl = "data:image/png;base64," + base64String;
                //string base64String = Convert.ToBase64String(imageData);
            }
           

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
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string firstname = (string)Session["fname"];
            string midname = (string)Session["mname"];
            string lastname = (string)Session["lname"];
            string birthdate = (string)Session["dob"];
            string contactNum = (string)Session["contactNumber"];
            string email = (string)Session["email"];
            string profileimage = (string)Session["profile_image"];
            try
            {
                //Update the admin account info
                AdminAccount data = new AdminAccount()
                {
                    idno = int.Parse(Lbl_Idno.Text),
                    fname = txtfname.Text,
                    mname = txtmname.Text,
                    lname = txtlname.Text,
                    bdate = txtdob.Text,
                    phone = txtcontact.Text,
                    email = txtemail.Text,
                    profile_image = null
                };

                twoBigDB.Update("ADMIN/" + Lbl_Idno.Text, data);//Update Product Data

                var result = twoBigDB.Get("ADMIN/" + Lbl_Idno.Text);//Retrieve Updated Data From ADMIN TBL
                AdminAccount obj = result.ResultAs<AdminAccount>();//Database Result 
                Lbl_Idno.Text = obj.idno.ToString();
                txtfname.Text = obj.fname.ToString();
                txtmname.Text = obj.mname.ToString();
                txtlname.Text = obj.lname.ToString();
                txtcontact.Text = obj.phone.ToString();
                txtemail.Text = obj.email.ToString();
                txtdob.Text = obj.bdate.ToString();
            }
            catch (Exception ex)
            {
                // Handle the exception and log the error message
                string errorMessage = "An error occurred while updating the data: " + ex.Message;
            }
        }

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
        protected void btnManageStation_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            // Retrieve the existing object from the database
            var result = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
            RefillingStation obj = result.ResultAs<RefillingStation>();

            // Update the specific fields that you want to change
            obj.operatingHrs = txtOperatingHrs.Text;
            obj.status = operatingHrsStatus.Text;
            obj.businessDays = txtBusinessDays.Text;

            // Pass the updated object to the Update method
            twoBigDB.Update("ADMIN/" + idno + "/RefillingStation/", obj);

            // Retrieve the updated data from the database
            var res = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
            RefillingStation list = res.ResultAs<RefillingStation>();//Database Result

            // Display the updated data in the UI
            txtOperatngHours.Text = list.operatingHrs;
            txtBssnessDay.Text = list.businessDays.ToString();
            txt_Status.Text = list.status.ToString();


        }
    }
}
