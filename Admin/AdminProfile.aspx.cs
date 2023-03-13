//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using FireSharp;
//using FireSharp.Config;
//using FireSharp.Interfaces;
//using FireSharp.Response;
//using Newtonsoft.Json;

//namespace WRS2big_Web.Admin
//{
//    public partial class AdminProfile : System.Web.UI.Page
//    {
//        IFirebaseConfig config = new FirebaseConfig
//        {
//            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
//            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"

//        };
//        IFirebaseClient twoBigDB;
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (Session["idno"] == null)
//            {
//                // User not found
//                Response.Write("<script>alert('User not found');</script>");
//                //Response.Redirect("/LandingPage/Account.aspx");
//            }
//            else
//            {
//                //string SubPlan = (string)Session["SubType"];
//                //LblSubPlan.Text = SubPlan;

//                //string SubDate = (string)Session["SubsDate"];
//                //LblDateStarted.Text = SubDate;

//                //string SubsEnd = (string)Session["SubEnd"];
//                //LblSubEnd.Text = SubsEnd;
//            }

//            //connection to database 
//            twoBigDB = new FireSharp.FirebaseClient(config);

//            Lbl_Idno.Text = (string)Session["idno"];
//            txtfname.Text = (string)Session["fname"];
//            txtmname.Text = (string)Session["mname"];
//            txtlname.Text = (string)Session["lname"];
//            txtcontact.Text = (string)Session["contactNumber"];
//            txtemail.Text = (string)Session["email"];
//            txtaddress.Text = (string)Session["address"];
//            txtdob.Text = (string)Session["dob"];
//            lblStationName.Text = (string)Session["WRSname"];
//            Lbl_user.Text = (string)Session["fullName"];

//            //LblSubPlan.Text = (string)Session["SubType"];
//            //LblDateStarted.Text = (string)Session["SubsDate"];
//            //LblSubEnd.Text = (string)Session["SubEnd"];

//            //FirebaseResponse response;
//            //response = twoBigDB.Get("ADMIN/");
//            //Model.AdminAccount obj = response.ResultAs<Model.AdminAccount>();

//            //LblSubPlan.Text = obj.SubType.ToString();
//            //LblDateStarted.Text = obj.SubsDate.ToString();
//            //LblSubEnd.Text = obj.SubEnd.ToString();


//        }

//        protected void btnSubscription_Click(object sender, EventArgs e)
//        {



//        }

//        //private void RetrieveRecords()
//        //{
//        //    try
//        //    {
//        //        //Retrieve Data
//        //        FirebaseResponse response = twoBigDB.Get("ADMIN");
//        //        Model.AdminAccount obj = response.ResultAs<Model.AdminAccount>();
//        //        var json = response.Body;
//        //        Dictionary<string, Model.AdminAccount> list = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(json);

//        //        foreach (KeyValuePair<string, Model.AdminAccount> item in list)
//        //        {
//        //            LstBoxAdminProfile.Items.Add(item.Value.Idno.ToString());
//        //            LstBoxAdminProfile.Items.Add(item.Value.Fname.ToString()
//        //            + " " + item.Value.Mname.ToString()
//        //            + " " + item.Value.Lname.ToString()
//        //            + " " + item.Value.Phone.ToString()
//        //            + " " + item.Value.Email.ToString()
//        //            + " " + item.Value.Address.ToString());
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Response.Write("<pre>" + ex.ToString() + "<pre>");
//        //    }
//        //}
//        //protected void btnSearch_Click(object sender, EventArgs e)
//        //{

//        //    String searchStr;
//        //    searchStr = LstBoxAdminProfile.SelectedValue;
//        //    FirebaseResponse response;
//        //    response = twoBigDB.Get("ADMIN/" + searchStr);
//        //    Model.AdminAccount obj = response.ResultAs<Model.AdminAccount>();

//        //    Lbl_fname.Text = obj.Fname.ToString();
//        //    Lbl_mname.Text = obj.Mname.ToString();
//        //    Lbl_lname.Text = obj.Lname.ToString();
//        //    Lbl_contact.Text = obj.Phone.ToString();
//        //    Lbl_email.Text = obj.Email.ToString();
//        //    Lbl_address.Text = obj.Address.ToString();
//        //}
//        protected void btnUpdate_Click(object sender, EventArgs e)
//        {

//            Model.AdminAccount data = new Model.AdminAccount()
//            {
//                Idno = int.Parse(Lbl_Idno.Text),
//                Fname = txtfname.Text,
//                Mname = txtmname.Text,
//                Lname = txtlname.Text,
//                Bdate = txtdob.Text,
//                Phone = txtcontact.Text,
//                Email = txtemail.Text,
//                Address = txtaddress.Text,
//                WRS_Name = lblStationName.Text,
//               // ImageProfile = 

//            };
//            twoBigDB.Update("ADMIN/" + Lbl_Idno.Text, data);//Update Product Data
//            //lbResult.Text = "Record successfully updated!";
//            var result = twoBigDB.Get("ADMIN/" + Lbl_Idno.Text);//Retrieve Updated Data From ADMIN TBL
//            Model.AdminAccount obj = result.ResultAs<Model.AdminAccount>();//Database Result

//            Lbl_Idno.Text = obj.Idno.ToString();
//            txtfname.Text = obj.Fname.ToString();
//            txtmname.Text = obj.Mname.ToString();
//            txtlname.Text = obj.Lname.ToString();
//            txtcontact.Text = obj.Phone.ToString();
//            txtemail.Text = obj.Email.ToString();
//            txtaddress.Text = obj.Address.ToString();
//            txtdob.Text = obj.Bdate.ToString();
//            lblStationName.Text = obj.WRS_Name.ToString();


//        }

//        protected void ImageButton(object sender, ImageClickEventArgs e)
//        {
//            if (FileUpload1.HasFile)
//            {
//                // Get the file that was uploaded
//                HttpPostedFile postedFile = FileUpload1.PostedFile;

//                // Get the file name and content type
//                string fileName = Path.GetFileName(postedFile.FileName);
//                string contentType = postedFile.ContentType;

//                // Get the file content as a byte array
//                byte[] fileData = new byte[postedFile.ContentLength];
//                postedFile.InputStream.Read(fileData, 0, postedFile.ContentLength);

//                // Update the image to the Firebase Realtime Database
//                twoBigDB.Update("ADMIN/ImageProfile/" + fileName, new
//                {
//                    name = fileName,
//                    contentType = contentType,
//                    data = fileData
//                });

//                // Get the image data from the Firebase Realtime Database
//                var imageData = twoBigDB.Get("ADMIN/" + fileName).ResultAs<byte[]>();

//                // Convert the image data to a base64 string
//                string imageBase64 = Convert.ToBase64String(imageData);

//                // Set the image source to the base64 string
//                Image_Profile.ImageUrl = "data:" + contentType + ";base64," + imageBase64;


//                //byte[] imageData = FileUpload1.FileBytes;
//                //SetResponse response = twoBigDB.Set("ADMIN/"+ fileData);
//                // You can check the response for success or failure here
//            }
//        }
//        //protected void btnLogout_Click(object sender, EventArgs e)
//        //{
//        //    Session.Abandon();
//        //    Session.RemoveAll();
//        //    Session["idno"] = null;
//        //    Session["password"] = null;
//        //    Session.Clear();
//        //    Response.Redirect("/LandingPage/Index.aspx");

//        //}
//    }
//}
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
            else
            {
                //string SubPlan = (string)Session["SubType"];
                //LblSubPlan.Text = SubPlan;

                //string SubDate = (string)Session["SubsDate"];
                //LblDateStarted.Text = SubDate;

                //string SubsEnd = (string)Session["SubEnd"];
                //LblSubEnd.Text = SubsEnd;
            }

            //Session["profile_image"] = ImageButton_new;  

            Lbl_Idno.Text = (string)Session["idno"];
            txtfname.Text = (string)Session["fname"];
            txtmname.Text = (string)Session["mname"];
            txtlname.Text = (string)Session["lname"];
            txtcontact.Text = (string)Session["contactNumber"];
            txtemail.Text = (string)Session["email"];
            txtaddress.Text = (string)Session["address"];
            txtdob.Text = (string)Session["dob"];
            lblStationName.Text = (string)Session["WRSname"];
            Lbl_user.Text = (string)Session["fullName"];

            LblSubPlan.Text = (string)Session["subType"];
            LblDateStarted.Text = Session["subsDate"].ToString();
            LblSubEnd.Text = Session["subEnd"].ToString();

            // Make sure that the object in the Session is a byte array representing the image
            byte[] imageData = Session["profile_image"] as byte[];
            if (imageData != null)
            {
                // Convert the byte array to a base64-encoded string
                string base64String = Convert.ToBase64String(imageData);
                // Set the ImageUrl property of the ImageButton to the base64-encoded string
                ImageButton_new.ImageUrl = "data:image/png;base64," + base64String;
            }
            //ImageButton_new = Session["profile_image"]; 

            FirebaseResponse response;
            response = twoBigDB.Get("ADMIN/REFILLINGSTATION/" + Lbl_Idno.Text);
            RefillingStation obj = response.ResultAs<RefillingStation>();

            //LblSubPlan.Text = obj.SubType.ToString();
            //LblDateStarted.Text = obj.SubsDate.ToString();
            //LblSubEnd.Text = obj.SubEnd.ToString();
            Session["operatingHrs"] = obj.operatingHrs;
            Session["status"] = obj.status;
            Session["businessdays"] = obj.businessDays;

            txtOperatngHrs.Text = (string)Session["operatingHrs"];
            txtBssnessDay.Text = Session["status"].ToString();
            txt_Status.Text = Session["businessdays"].ToString();

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
           
                AdminAccount data = new AdminAccount()
                {
                    idno = int.Parse(Lbl_Idno.Text),
                    fname = txtfname.Text,
                    mname = txtmname.Text,
                    lname = txtlname.Text,
                    bdate = txtdob.Text,
                    phone = txtcontact.Text,
                    email = txtemail.Text,
                    //WRS_Name = lblStationName.Text,
                    profile_image = null

                };

                twoBigDB.Update("ADMIN/" + Lbl_Idno.Text, data);//Update Product Data
                                                                //lbResult.Text = "Record successfully updated!";
                var result = twoBigDB.Get("ADMIN/" + Lbl_Idno.Text);//Retrieve Updated Data From ADMIN TBL
                AdminAccount obj = result.ResultAs<AdminAccount>();//Database Result

                Lbl_Idno.Text = obj.idno.ToString();
                txtfname.Text = obj.fname.ToString();
                txtmname.Text = obj.mname.ToString();
                txtlname.Text = obj.lname.ToString();
                txtcontact.Text = obj.phone.ToString();
                txtemail.Text = obj.email.ToString();
                txtdob.Text = obj.bdate.ToString();
                //lblStationName.Text = obj.WRS_Name.ToString();
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
            var data = new RefillingStation
            {
                operatingHrs = txtOperatingHrs.Text,
                status = operatingHrsStatus.Text,
                businessDays = txtBusinessDays.Text,
                DateAdded = DateTime.UtcNow
            };

            twoBigDB.Set("OPERATINGTIME/", data);//Update Product Data
            twoBigDB.Update("OPERATINGTIME/" + Lbl_Idno.Text, data);
            var result = twoBigDB.Get("OPERATINGTIME/" + Lbl_Idno.Text);//Retrieve Updated Data From ADMIN TBL
            RefillingStation obj = result.ResultAs<RefillingStation>();//Database Result

            txtOperatngHrs.Text = obj.operatingHrs;
            txtBssnessDay.Text = obj.businessDays.ToString();
            txt_Status.Text = obj.status.ToString();
           
        }
    }
}
