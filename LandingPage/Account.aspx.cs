using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Storage;
using System.IO;
using WRS2big_Web.Model;
using System.Text;
using System.Diagnostics;

namespace WRS2big_Web.LandingPage
{
    public partial class Account : System.Web.UI.Page
    {
        //Initialize firebase client
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
        }

        //Function to store user data 
        protected async void btnSignup_Click(object sender, EventArgs e)
        {

            try
            {
                if (businessProof == null)
                {
                    Response.Write("<script>alert('You must upload a proof of your business');</script>");
                    return;
                }
                if (validIDUpload == null)
                {
                    Response.Write("<script>alert('You must upload a VALID ID!'); </script>");
                    return;
                }

                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                string address = Request.Form["address"];
                double latitude = double.Parse(Request.Form["lat"]);
                double longitude = double.Parse(Request.Form["long"]);


                string selectedProof = documentDropDown.SelectedValue;
                string validID = validIDList.SelectedValue;

                // Password validation
                string password = txt_password.Text;
                if (password.Length < 8 || password.Length > 20 ||
                    !password.Any(char.IsLetter) || !password.Any(char.IsDigit) ||
                    !password.Any(c => !char.IsLetterOrDigit(c)))
                {
                    Response.Write("<script>alert('Password must be 8-20 characters long and contain at least 1 letter, 1 number, and 1 special character.'); </script>");
                    return;
                }

                // Contact number validation
                string contactNum = txtphoneNum.Text;
                if (contactNum.Length != 11 || !contactNum.All(char.IsDigit))
                {
                    Response.Write("<script>alert('Contact number must be 11 digits long and contain only numbers.'); </script>");
                    return;
                }


                var data = new AdminAccount
                {
                    idno = idnum,
                    lname = txtlname.Text,
                    fname = txtfname.Text,
                    mname = txtmname.Text,
                    bdate = txtbirthdate.Text,
                    phone = contactNum,
                    email = txtEmail.Text,
                    pass = password,
                    businessProof = selectedProof,
                    validID = validID,
                    status = "Pending",
                    businessProofLnk = null,
                    validIDLnk = null,
                    subStatus = "notSubscribed",
                    dateRegistered = DateTime.Now,
                    userRole = "Admin",
                    address = address
                };
                var list = new RefillingStation
                {
                    addLongitude = longitude,
                    addLattitude = latitude,
                    stationName = txtStationName.Text,
                    stationAddress = address,
                    proof = null,
                    dateAdded = DateTime.UtcNow
                };
                //FOR THE VALID ID 
                if (validIDUpload.HasFile)
                {
                    //FOR THE VALID ID
                    // Get the file name and extension
                    string fileName = Path.GetFileName(validIDUpload.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(fileName);

                    // Generate a unique file name
                    string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                    // Upload the file to Firebase Storage
                    var storage = new FirebaseStorage("big-system-64b55.appspot.com");
                    var task = await storage.Child("clientValidID").Child(uniqueFileName).PutAsync(validIDUpload.PostedFile.InputStream);

                    // Get the download URL of the uploaded file
                    string imageUrl = await storage.Child("clientValidID").Child(uniqueFileName).GetDownloadUrlAsync();
                    data.validIDLnk = imageUrl;

                }

                //FOR THE BUSINESS PROOF
                byte[] fileBytes = null;
                if (businessProof.HasFile)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        businessProof.PostedFile.InputStream.CopyTo(memoryStream);
                        fileBytes = memoryStream.ToArray();
                    }
                }

                if (fileBytes != null)
                {
                    var storage = new FirebaseStorage("big-system-64b55.appspot.com");
                    var fileExtension = Path.GetExtension(businessProof.FileName);
                    var filePath = $"clientBusinessProof/{list.stationName}{fileExtension}";
                    //  used the using statement to ensure that the MemoryStream object is properly disposed after it's used.
                    using (var stream = new MemoryStream(fileBytes))
                    {
                        var storageTask = storage.Child(filePath).PutAsync(stream);
                        var downloadUrl = await storageTask;
                        // used Encoding.ASCII.GetBytes to convert the downloadUrl string to a byte[] object.
                        data.businessProofLnk = downloadUrl;
                    }
                }

                SetResponse response;
                //Storing the admin info
                response = twoBigDB.Set("ADMIN/" + data.idno, data);//Storing data to the database
                AdminAccount res = response.ResultAs<AdminAccount>();//Database Result

                //Storing the refilling station info
                response = twoBigDB.Set("ADMIN/" + data.idno + "/RefillingStation/", list);//Storing data to the database
                RefillingStation result = response.ResultAs<RefillingStation>();//Database Result


                Response.Write("<script>alert ('Account " +  res.idno + " created! Use this id number to log in.'); window.location.href = '/LandingPage/Account.aspx'; </script>");
                //Response.Write("<script>alert ('Your account has sucessfully created, please wait for approval before you login! Use this id number to log in.'); location.reload(); window.location.href = '/LandingPage/Account.aspx'; </script>");

                //NOTIFICATION SENT TO SUPERADMIN FOR ACCOUNT APPROVAL
                int ID = rnd.Next(1, 20000);
                var Notification = new Notification
                {
                    admin_ID = data.idno,
                    sender = "Admin",
                    receiver = "Super Admin",
                    title = "New Client",
                    body = "You have a new client! Check the details for approval ",
                    notificationDate = DateTime.Now,
                    status = "unread",
                    notificationID = ID

                };

                SetResponse notifResponse;
                notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                Notification notif = notifResponse.ResultAs<Notification>();//Database Result


                //NOTIFICATION SENT TO ADMIN FOR ACCOUNT BEING PENDING
                int notifID = rnd.Next(1, 30000);
                var adminNotification = new Notification
                {
                    admin_ID = data.idno,
                    sender = "Super Admin",
                    receiver = "Admin",
                    title = "Welcome to 2BiG!",
                    body = " Thankyou for signing up! Currently, your account is under review. You will receive a new notification once your account is approved",
                    notificationDate = DateTime.Now,
                    status = "unread",
                    notificationID = notifID

                };

                SetResponse adminResponse;
                adminResponse = twoBigDB.Set("NOTIFICATION/" + notifID, adminNotification);//Storing data to the database
                Notification adminNotif = adminResponse.ResultAs<Notification>();//Database Result



            }
            catch
            {
                Response.Write("<script>alert('Data already exist'); window.location.href = 'Account.aspx'; </script>");
            }
        }
       // To validate the terms and condition

        //LOGGING IN 
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //Get the id number and password entered by the user
                string idno = txt_idno.Text.Trim();
                string password = txt_password.Text.Trim();

                //generate a random number for users logged
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                // Password validation
                //if (password.Length < 8 || password.Length > 20 ||
                //    !password.Any(char.IsLetter) || !password.Any(char.IsDigit) ||
                //    !password.Any(c => !char.IsLetterOrDigit(c)))
                //{
                //    Response.Write("<script>alert('Password must be 8-20 characters long and contain at least 1 letter, 1 number, and 1 special character.'); </script>");
                //    return;
                //}

                FirebaseResponse response;
                response = twoBigDB.Get("ADMIN/" + idno);
                AdminAccount user = response.ResultAs<AdminAccount>(); //Database result
                //response = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
                //RefillingStation obj = response.ResultAs<RefillingStation>();

                //Check if the id number and password are valid
                if (user != null)
                {
                    if (user.pass == password)
                    {
                        //Generate a unique key for the USERLOG table using the Push method
                        //var userLogResponse = twoBigDB.Push("USERLOG/", userLog);

                        ////Get the unique key generated by the Push method
                        //string userLogKey = userLogResponse.Result.name;

                        Session["idno"] = idno;
                        Session["password"] = password;
                        Session["fname"] = user.fname;
                        Session["mname"] = user.mname;
                        Session["lname"] = user.lname;
                        Session["fullName"] = user.fname + " " + user.mname + " " + user.lname;
                        Session["dob"] = user.bdate;
                        Session["contactNumber"] = user.phone;
                        Session["email"] = user.email;
                        Session["profile_image"] = user.profile_image;
                        //Session["subType"] = user.subType;
                        //Session["subsDate"] = user.subsDate;
                        //Session["subEnd"] = user.subEnd;


                        // Retrieve all RefillingStation objects for the current admin
                        response = twoBigDB.Get("ADMIN/" + user.idno + "/RefillingStation/");
                        RefillingStation stations = response.ResultAs<RefillingStation>();
                        //IDictionary<string, RefillingStation> stations = response.ResultAs<IDictionary<string, RefillingStation>>();
                        Session["stationName"] = stations.stationName;
                        Session["address"] = stations.stationAddress;
                        Session["operatingHrsTo"] = stations.operatingHrsTo;
                        Session["operatingHrsFrom"] = stations.operatingHrsFrom;
                        Session["status"] = stations.status;
                        Session["businessdaysTo"] = stations.businessDaysTo;
                        Session["businessdaysFrom"] = stations.businessDaysFrom;

 

                        // Retrieve all TankSupply objects for the current admin
                        response = twoBigDB.Get("TANKSUPPLY/");
                        TankSupply obj = response.ResultAs<TankSupply>();

                        if (obj != null)
                        { 
                            Session["tankId"] = obj.tankId;
                            Session["tankVolume"] = obj.tankVolume;
                            Session["tankUnit"] = obj.tankUnit;
                            Session["dateAdded"] = obj.dateAdded;
                        }

                        // Retrieve Employee records for the current admin
                        response = twoBigDB.Get("EMPLOYEE/");
                        Employee emp = response.ResultAs<Employee>();

                        if (emp != null)
                        {
                            Session["emp_id"] = emp.emp_id;
                            Session["empName"] = emp.emp_lastname + ' ' + emp.emp_firstname;
                            
                        }

                        //Get the current date and time
                        DateTime loginTime = DateTime.UtcNow;

                        //Store the login information in the USERLOG table
                        var data = new UsersLogs
                        {
                            logsId = idnum,
                            userIdnum = int.Parse(idno),
                            userFullname = (string)Session["fullName"],
                            userActivity = "LOGGED IN",
                            activityTime = loginTime 
                        };

                        //Storing the  info
                        response = twoBigDB.Set("USERSLOG/" + data.logsId, data);//Storing data to the database
                        UsersLogs res = response.ResultAs<UsersLogs>();//Database Result

                        //Get the exsiting data in the database
                        FirebaseResponse resLogs = twoBigDB.Get("USERSLOG/" + data.logsId);
                        UsersLogs existingLog = resLogs.ResultAs<UsersLogs>();

                        if (existingLog != null)
                        {
                            Session["logsId"] = existingLog.logsId;
                        }


                        // Login successful, redirect to admin homepage
                        Response.Write("<script>alert ('Login Successfull!');</script>");
                        //Response.Redirect("/Admin/AdminIndex.aspx");



                        //TO CHECK THE STATUS OF THE ADMIN
                        var admin = (string)Session["idno"];
                        int adminId = int.Parse(admin);

                        // Retrieve the existing admin data from the database
                        FirebaseResponse getStatus = twoBigDB.Get("ADMIN/" + adminId);
                        Model.AdminAccount pendingClients = getStatus.ResultAs<Model.AdminAccount>();

                        string clientStat = pendingClients.status;
                        string subStatus = pendingClients.subStatus;

                        // Retrieve the existing subscribed admin from the database

                        if (clientStat == "Pending") //NOT APPROVED YET, BY THE SUPERADMIN
                        {
                            Response.Write("<script>window.location.href = '/Admin/WaitingPage.aspx'; </script>");

                        }
                        else if (subStatus == "notSubscribed")
                        {
                            Response.Write("<script> window.location.href = '/Admin/AdminProfile.aspx'; </script>");
                        }

                        else if (clientStat == "Approved" && subStatus == "Subscribed") //APPROVED AND SUBSCRIBED
                        {
                            Response.Write("<script>window.location.href = '/Admin/AdminIndex.aspx'; </script>");

                        }

                    }
                    else
                    {
                        // Login failed, display error message
                        //lblError.Text = "Invalid email or password!";
                        Response.Write("<script>alert('Invalid email or password');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('User not found!');</script>");
                }
              
            }
            catch (Exception ex)
            {
                // Handle the exception and log the error message
                string errorMessage = "An error occurred while trying to login: " + ex.Message;
            }
        }

    }
}

