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
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

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

      
        protected async void btnSignup_Click(object sender, EventArgs e) //SIGNUP FOR EMAIL VALIDATION
        {

            try
            {

                //CHECK IF THE ENETRED DETAILS MATCHED FROM THE REGISTERED CLIENTS IN THE DATABASE

                bool isDuplicate = checkDuplication();

                if (!isDuplicate)
                {
                    if (!businessProof.HasFile)
                    {
                        Response.Write("<script>alert('You must upload a proof of your business');</script>");
                        return;
                    }

                    if (!validIDUpload.HasFile)
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
                    string password = Server.HtmlEncode(id_passwordreg.Text);
                    if (password.Length < 8 || password.Length > 20 ||
                        !password.Any(char.IsLetter) || !password.Any(char.IsDigit) ||
                        !password.Any(c => !char.IsLetterOrDigit(c)))
                    {
                        Response.Write("<script>alert('Password must be in between 8-20 characters long and contain at least 1 letter, 1 number, and 1 special character.'); </script>");
                        return;
                    }

                    // Contact number validation
                    string contactNum = txtphoneNum.Text;
                    if (contactNum.Length != 11 || !contactNum.All(char.IsDigit))
                    {
                        Response.Write("<script>alert('Contact number must be 11 digits long and contain only numbers.'); </script>");
                        return;
                    }

                    // Birthdate validation
                    DateTime birthdate;
                    if (!DateTime.TryParse(txtbirthdate.Text, out birthdate))
                    {
                        Response.Write("<script>alert('Invalid birthdate. Please enter a valid date in the format YYYY/MM/D.');</script>");
                        return;
                    }

                    int age = DateTime.Now.Year - birthdate.Year;
                    if (birthdate > DateTime.Now.AddYears(-age))
                    {
                        age--;
                    }
                    if (age < 18 || age > 100)
                    {
                        Response.Write("<script>alert('You must be at least 18 years old to sign up and at most 100 years old.');</script>");
                        return;
                    }

                    Session["receiverIdno"] = idnum.ToString();

                    //EMAIL VALIDATION - EMAIL IS USED TO LOGIN
                    checkRegisteredEmail();



                    // Encrypt the password using SHA-256
                    string hashedPassword = GetSHA256Hash(password);

                    var data = new AdminAccount
                    {
                        idno = idnum,
                        lname = Server.HtmlEncode(txtlname.Text),
                        fname = Server.HtmlEncode(txtfname.Text),
                        mname = Server.HtmlEncode(txtmname.Text),
                        bdate = txtbirthdate.Text,
                        phone = contactNum,
                        email = Server.HtmlEncode(txtEmail.Text),
                        pass = hashedPassword,
                        businessProof = selectedProof,
                        validID = validID,
                        status = "notVerified",
                        //businessProofLnk = null,
                        //validIDLnk = null,
                        subStatus = "notSubscribed",
                        dateRegistered = DateTime.Now,
                        userRole = "Admin",
                        address = address
                    };
                    var list = new RefillingStation
                    {
                        addLongitude = longitude,
                        addLattitude = latitude,
                        stationName = Server.HtmlEncode(txtStationName.Text),
                        stationAddress = address,
                        proof = null,
                        dateAdded = DateTime.UtcNow
                    };


                    //CREATE LIST TO STORE THE LINKS
                    List<string> businessProofs = new List<string>();
                    List<string> validIDs = new List<string>();

                    //BUSINESS PROOF UPLOAD
                    if (businessProof.HasFile)
                    {
                        foreach (HttpPostedFile file in businessProof.PostedFiles)
                        {
                            string fileName = Path.GetFileName(file.FileName);
                            string fileExtension = Path.GetExtension(fileName);

                            // Generate a unique file name
                            string uniqueFileName = data.fname + data.lname + "_" + Guid.NewGuid().ToString() + fileExtension;

                            try
                            {
                                // Upload the file to Firebase Storage
                                var storage = new FirebaseStorage("big-system-64b55.appspot.com");
                                var task = storage.Child("clientBusinessProof").Child(data.fname + data.lname).Child(uniqueFileName).PutAsync(file.InputStream);

                                // Wait for the upload task to complete
                                await task;

                                // Get the download URL of the uploaded file
                                string proofUrl = await storage.Child("clientBusinessProof").Child(data.fname + data.lname).Child(uniqueFileName).GetDownloadUrlAsync();

                                // Save the link to the list
                                businessProofs.Add(proofUrl);
                            }
                            catch (Exception ex)
                            {
                                // Handle any exceptions that occur during the upload process
                                // You can log the exception details or handle the error in a way that suits your application
                                Console.WriteLine("Error uploading file: " + ex.Message);
                            }
                        }

                    }


                    Debug.WriteLine($"PROOFS : {string.Join(", ", businessProofs)}");

                    //VALID ID UPLOAD
                    if (validIDUpload.HasFile)
                    {
                        foreach (HttpPostedFile file in validIDUpload.PostedFiles)
                        {
                            string fileName = Path.GetFileName(file.FileName);
                            string fileExtension = Path.GetExtension(fileName);

                            // Generate a unique file name
                            string uniqueFileName = data.fname + data.lname + "_" + Guid.NewGuid().ToString() + fileExtension;

                            try
                            {
                                // Upload the file to Firebase Storage
                                var storage = new FirebaseStorage("big-system-64b55.appspot.com");
                                var task = storage.Child("clientValidID").Child(data.fname + data.lname).Child(uniqueFileName).PutAsync(file.InputStream);

                                // Wait for the upload task to complete
                                await task;

                                // Get the download URL of the uploaded file
                                string proofUrl = await storage.Child("clientValidID").Child(data.fname + data.lname).Child(uniqueFileName).GetDownloadUrlAsync();

                                // Save the link to the list
                                validIDs.Add(proofUrl);
                            }
                            catch (Exception ex)
                            {
                                // Handle any exceptions that occur during the upload process
                                // You can log the exception details or handle the error in a way that suits your application
                                Console.WriteLine("Error uploading file: " + ex.Message);
                            }
                        }

                    }
                    Debug.WriteLine($"VALIDID: {string.Join(", ", validIDs)}");

                    var links = new Links
                    {
                        Businessproofs = businessProofs,
                        ValidIDs = validIDs,

                    };

                    SetResponse response;
                    //Storing the admin info
                    response = twoBigDB.Set("ADMIN/" + data.idno, data);//Storing data to the database
                    AdminAccount res = response.ResultAs<AdminAccount>();//Database Result

                    //Storing the refilling station info
                    response = twoBigDB.Set("ADMIN/" + data.idno + "/RefillingStation/", list);//Storing data to the database
                    RefillingStation result = response.ResultAs<RefillingStation>();//Database Result

                    //SAVE BUSINESS PROOF LINKS 
                    response = twoBigDB.Set("ADMIN/" + data.idno + "/Links/", links);//Storing data to the database
                    Links Linkresult = response.ResultAs<Links>();//Database Result

                    ////SAVE VALID ID LINKS
                    //response = twoBigDB.Set("ADMIN/" + data.idno + "/ValidID/", links.ValidIDs);//Storing data to the database
                    //Links validIDresult = response.ResultAs<Links>();//Database Result

                    Response.Write("<script>alert ('Account created! Please Check your email for the verification code'); window.location.href = '/LandingPage/EmailVerification.aspx'; </script>");
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




            }
            catch (Exception ex)
            {
                // Output the exception message to the console or log it
                Debug.WriteLine("Error creating account: " + ex.Message);

                Response.Write("<script>alert('Data already exist'); window.location.href = 'Account.aspx'; </script>");
            }
        }

        //protected async void btnSignup_Click(object sender, EventArgs e) //ID NUMBER IS USED TO LOGIN
        //{

        //    try
        //    {
        //        bool isDuplicate = checkDuplication();

        //        if (!isDuplicate)
        //        {
        //            if (!businessProof.HasFile)
        //            {
        //                Response.Write("<script>alert('You must upload a proof of your business');</script>");
        //                return;
        //            }

        //            if (!validIDUpload.HasFile)
        //            {
        //                Response.Write("<script>alert('You must upload a VALID ID!'); </script>");
        //                return;
        //            }


        //            Random rnd = new Random();
        //            int idnum = rnd.Next(1, 10000);

        //            string address = Request.Form["address"];
        //            double latitude = double.Parse(Request.Form["lat"]);
        //            double longitude = double.Parse(Request.Form["long"]);


        //            string selectedProof = documentDropDown.SelectedValue;
        //            string validID = validIDList.SelectedValue;

        //            //Password validation
        //            string password = Server.HtmlEncode(id_passwordreg.Text);
        //            if (password.Length < 8 || password.Length > 20 ||
        //                !password.Any(char.IsLetter) || !password.Any(char.IsDigit) ||
        //                !password.Any(c => !char.IsLetterOrDigit(c)))
        //            {
        //                Response.Write("<script>alert('Password must be in between 8-20 characters long and contain at least 1 letter, 1 number, and 1 special character.'); </script>");
        //                return;
        //            }

        //            //Contact number validation
        //            string contactNum = txtphoneNum.Text;
        //            if (contactNum.Length != 11 || !contactNum.All(char.IsDigit))
        //            {
        //                Response.Write("<script>alert('Contact number must be 11 digits long and contain only numbers.'); </script>");
        //                return;
        //            }

        //            // Birthdate validation
        //            DateTime birthdate;
        //            if (!DateTime.TryParse(txtbirthdate.Text, out birthdate))
        //            {
        //                Response.Write("<script>alert('Invalid birthdate. Please enter a valid date in the format YYYY/MM/D.');</script>");
        //                return;
        //            }

        //            int age = DateTime.Now.Year - birthdate.Year;
        //            if (birthdate > DateTime.Now.AddYears(-age))
        //            {
        //                age--;
        //            }
        //            if (age < 18 || age > 100)
        //            {
        //                Response.Write("<script>alert('You must be at least 18 years old to sign up and at most 100 years old.');</script>");
        //                return;
        //            }

        //            Session["receiverIdno"] = idnum.ToString();
        //            //SEND ID NUMBER TO EMAIL - IDNO IS USED TO LOGIN
        //            checkEmail();

        //            //Encrypt the password using SHA-256
        //            string hashedPassword = GetSHA256Hash(password);

        //            var data = new AdminAccount
        //            {
        //                idno = idnum,
        //                lname = Server.HtmlEncode(txtlname.Text),
        //                fname = Server.HtmlEncode(txtfname.Text),
        //                mname = Server.HtmlEncode(txtmname.Text),
        //                bdate = txtbirthdate.Text,
        //                phone = contactNum,
        //                email = Server.HtmlEncode(txtEmail.Text),
        //                pass = hashedPassword,
        //                businessProof = selectedProof,
        //                validID = validID,
        //                status = "Pending",
        //                subStatus = "notSubscribed",
        //                dateRegistered = DateTime.Now,
        //                userRole = "Admin",
        //                address = address
        //            };
        //            var list = new RefillingStation
        //            {
        //                addLongitude = longitude,
        //                addLattitude = latitude,
        //                stationName = Server.HtmlEncode(txtStationName.Text),
        //                stationAddress = address,
        //                proof = null,
        //                dateAdded = DateTime.UtcNow
        //            };


        //            //CREATE LIST TO STORE THE LINKS
        //            List<string> businessProofs = new List<string>();
        //            List<string> validIDs = new List<string>();

        //            //BUSINESS PROOF UPLOAD
        //            if (businessProof.HasFile)
        //            {
        //                foreach (HttpPostedFile file in businessProof.PostedFiles)
        //                {
        //                    string fileName = Path.GetFileName(file.FileName);
        //                    string fileExtension = Path.GetExtension(fileName);

        //                    // Generate a unique file name
        //                    string uniqueFileName = data.fname + data.lname + "_" + Guid.NewGuid().ToString() + fileExtension;

        //                    try
        //                    {
        //                        // Upload the file to Firebase Storage
        //                        var storage = new FirebaseStorage("big-system-64b55.appspot.com");
        //                        var task = storage.Child("clientBusinessProof").Child(data.fname + data.lname).Child(uniqueFileName).PutAsync(file.InputStream);

        //                        //Wait for the upload task to complete

        //                        await task;

        //                        //Get the download URL of the uploaded file
        //                        string proofUrl = await storage.Child("clientBusinessProof").Child(data.fname + data.lname).Child(uniqueFileName).GetDownloadUrlAsync();

        //                        //Save the link to the list
        //                        businessProofs.Add(proofUrl);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        //Handle any exceptions that occur during the upload process
        //                        //You can log the exception details or handle the error in a way that suits your application
        //                        Console.WriteLine("Error uploading file: " + ex.Message);
        //                    }
        //                }

        //            }


        //            Debug.WriteLine($"PROOFS : {string.Join(", ", businessProofs)}");

        //            //VALID ID UPLOAD
        //            if (validIDUpload.HasFile)
        //            {
        //                foreach (HttpPostedFile file in validIDUpload.PostedFiles)
        //                {
        //                    string fileName = Path.GetFileName(file.FileName);
        //                    string fileExtension = Path.GetExtension(fileName);

        //                    //Generate a unique file name
        //                    string uniqueFileName = data.fname + data.lname + "_" + Guid.NewGuid().ToString() + fileExtension;

        //                    try
        //                    {
        //                        //Upload the file to Firebase Storage
        //                        var storage = new FirebaseStorage("big-system-64b55.appspot.com");
        //                        var task = storage.Child("clientValidID").Child(data.fname + data.lname).Child(uniqueFileName).PutAsync(file.InputStream);

        //                        //Wait for the upload task to complete

        //                        await task;

        //                        //Get the download URL of the uploaded file
        //                        string proofUrl = await storage.Child("clientValidID").Child(data.fname + data.lname).Child(uniqueFileName).GetDownloadUrlAsync();

        //                        //Save the link to the list
        //                        validIDs.Add(proofUrl);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        //Handle any exceptions that occur during the upload process
        //                        //You can log the exception details or handle the error in a way that suits your application
        //                        Console.WriteLine("Error uploading file: " + ex.Message);
        //                    }
        //                }

        //            }
        //            Debug.WriteLine($"VALIDID: {string.Join(", ", validIDs)}");

        //            var links = new Links
        //            {
        //                Businessproofs = businessProofs,
        //                ValidIDs = validIDs,

        //            };

        //            SetResponse response;
        //            //Storing the admin info
        //            response = twoBigDB.Set("ADMIN/" + data.idno, data);//Storing data to the database
        //            AdminAccount res = response.ResultAs<AdminAccount>();//Database Result

        //            //Storing the refilling station info
        //            response = twoBigDB.Set("ADMIN/" + data.idno + "/RefillingStation/", list);//Storing data to the database
        //            RefillingStation result = response.ResultAs<RefillingStation>();//Database Result

        //            //SAVE BUSINESS PROOF LINKS
        //            response = twoBigDB.Set("ADMIN/" + data.idno + "/Links/", links);//Storing data to the database
        //            Links Linkresult = response.ResultAs<Links>();//Database Result

        //            //SAVE VALID ID LINKS
        //            response = twoBigDB.Set("ADMIN/" + data.idno + "/ValidID/", links.ValidIDs);//Storing data to the database
        //            Links validIDresult = response.ResultAs<Links>();//Database Result

        //            Response.Write("<script>alert ('Account created! Please use your ID Number to Login. A copy of your ID number is sent to your email'); window.location.href = 'Account.aspx'; </script>");
        //            Response.Write("<script>alert ('Your account has sucessfully created, please wait for approval before you login! Use this id number to log in.'); location.reload(); window.location.href = '/LandingPage/Account.aspx'; </script>");

        //            //NOTIFICATION SENT TO SUPERADMIN FOR ACCOUNT APPROVAL
        //            int ID = rnd.Next(1, 20000);
        //            var Notification = new Notification
        //            {
        //                admin_ID = data.idno,
        //                sender = "Admin",
        //                receiver = "Super Admin",
        //                title = "New Client",
        //                body = "You have a new client! Check the details for approval ",
        //                notificationDate = DateTime.Now,
        //                status = "unread",
        //                notificationID = ID

        //            };

        //            SetResponse notifResponse;
        //            notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
        //            Notification notif = notifResponse.ResultAs<Notification>();//Database Result


        //            //NOTIFICATION SENT TO ADMIN FOR ACCOUNT BEING PENDING
        //            int notifID = rnd.Next(1, 30000);
        //            var adminNotification = new Notification
        //            {
        //                admin_ID = data.idno,
        //                sender = "Super Admin",
        //                receiver = "Admin",
        //                title = "Welcome to 2BiG!",
        //                body = " Thankyou for signing up! Currently, your account is under review. You will receive a new notification once your account is approved",
        //                notificationDate = DateTime.Now,
        //                status = "unread",
        //                notificationID = notifID

        //            };

        //            SetResponse adminResponse;
        //            adminResponse = twoBigDB.Set("NOTIFICATION/" + notifID, adminNotification);//Storing data to the database
        //            Notification adminNotif = adminResponse.ResultAs<Notification>();//Database Result
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //Output the exception message to the console or log it
        //        Debug.WriteLine("Error creating account: " + ex.Message);

        //        Response.Write("<script>alert('Data already exist'); window.location.href = 'Account.aspx'; </script>");
        //    }
        //}

        private bool checkDuplication()
        {
            string firstname = txtfname.Text;
            string lastname = txtlname.Text;
            string middlename = txtmname.Text;
            string email = txtEmail.Text;
            string phoneNumber = txtphoneNum.Text;
            string address = Request.Form["address"];
            string stationName = txtStationName.Text;

            bool isDuplicate = false;
           

            FirebaseResponse empResponse = twoBigDB.Get("ADMIN/");
            var admins = empResponse.ResultAs<Dictionary<string, AdminAccount>>();

            if (admins != null)
            {
                foreach (KeyValuePair<string, AdminAccount> client in admins)
                {
                    empResponse = twoBigDB.Get("ADMIN/" + client.Value.idno + "/RefillingStation");
                    RefillingStation station = empResponse.ResultAs<RefillingStation>();

                    // CHECK IF THESE FIELDS ARE ALREADY IN THE DATABASE
                    if (email == client.Value.email &&
                        phoneNumber == client.Value.phone &&
                        firstname == client.Value.fname &&
                        lastname == client.Value.lname
                        )
                        //address == client.Value.address &&
                        //stationName == station.stationName)
                    {
                        isDuplicate = true;
                        Debug.WriteLine($"DUPLICATE:{isDuplicate}");
                        break;
                    }
                }

                if (isDuplicate)
                {
                    Debug.WriteLine($"INSIDE IF DUPLICATE:{isDuplicate}");
                    Response.Write("<script>alert('WARNING: Duplicate account detected! It appears that all the information you entered is already saved in the database. Account duplication is strictly prohibited. Creating multiple accounts for one Refilling Station may result in severe consequences, including a higher chance of being banned or blocked from accessing the system. To avoid any issues, please refrain from creating duplicate accounts.'); window.location.href = 'Account.aspx'; </script>");
                   
                }
                else
                {
                    Response.Write("<scipt>alert('NO DUPLICATION'); </script>");
                }
            }
            return isDuplicate;
        }

        private void checkRegisteredEmail() //FOR EMAIL USED TO LOGIN
        {
            string fullname = txtfname.Text + " " + txtlname.Text;
            string email = txtEmail.Text;

            bool emailFound = false;

            Debug.WriteLine($"FULLNAME: {fullname}");
            Debug.WriteLine($"EMAIL: {email}");

            try
            {
                FirebaseResponse empResponse = twoBigDB.Get("ADMIN/");
                var admins = empResponse.ResultAs<Dictionary<string, AdminAccount>>();

                if (admins != null)
                {
                    foreach (KeyValuePair<string, AdminAccount> entry in admins)
                    {
                        if (entry.Value.email == email)
                        {
                            Response.Write("<script> window.location.href = 'Account.aspx';</script>");
                        }
                        else
                        {
                            emailFound = true;
                        }
                    }

                    if (emailFound)
                    {
                        Random rnd = new Random();
                        int verificationCode = rnd.Next(1, 30000);
                        string idno = Session["receiverIdno"].ToString();

                        string fromMail = "technique.services2022@gmail.com";
                        string fromPassword = "qenrtopcfoifbvbo";

                        MailMessage message = new MailMessage();
                        message.From = new MailAddress(fromMail);
                        message.Subject = "2BiG Account Verification";
                        message.To.Add(new MailAddress(email));
                        message.Body = "<html><title>Verify your Identity</title> <body> Hey " + fullname + "! You just created an account in the 2BiG Platform. To complete your account registration, enter the code on the Verification Page. <br> Verification Code: <strong>" + verificationCode + "</strong></body></html>";
                        message.IsBodyHtml = true;

                        //SAVE TO SESSIONS TO USE IN THE REQUEST ANOTHER CODE
                        Session["receiverEmail"] = email;
                        Session["receiverName"] = fullname;


                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(fromMail, fromPassword),
                            EnableSsl = true,
                        };

                        try
                        {
                            smtpClient.Send(message);

                            Session["verificationCode"] = verificationCode.ToString();
                        }
                        catch (Exception ex)
                        {
                            // Log the exception
                            Debug.WriteLine("Error sending email: " + ex.ToString());
                            Response.Write("<script>alert('Error sending email. Please try again later.');</script>");
                        }

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Debug.WriteLine("Error retrieving data from Firebase: " + ex.ToString());
                Response.Write("<script>alert('Error retrieving data from Firebase. Please try again later.');</script>");
            }
        }

        private void checkEmail() //FOR IDNO AS LOG-IN
        {
            string fullname = txtfname.Text + " " + txtlname.Text;
            string email = txtEmail.Text;

            bool emailFound = false;

            Debug.WriteLine($"FULLNAME: {fullname}");
            Debug.WriteLine($"EMAIL: {email}");

            try
            {
                FirebaseResponse empResponse = twoBigDB.Get("ADMIN/");
                var admins = empResponse.ResultAs<Dictionary<string, AdminAccount>>();

                if (admins != null)
                {
                    foreach (KeyValuePair<string, AdminAccount> entry in admins)
                    {
                        if (entry.Value.email == email)
                        {
                            Response.Write("<script>alert('This email is already registered to the application. Log-in now.');</script>");
                        }
                        else
                        {

                            emailFound = true;
                        }
                    }

                    if (emailFound)
                    {
                        Random rnd = new Random();
                        int verificationCode = rnd.Next(1, 30000);
                        string idno = Session["receiverIdno"].ToString();

                        string fromMail = "technique.services2022@gmail.com";
                        string fromPassword = "qenrtopcfoifbvbo";

                        MailMessage message = new MailMessage();
                        message.From = new MailAddress(fromMail);
                        message.Subject = "2BiG Account Verification";
                        message.To.Add(new MailAddress(email));
                        message.Body = "<html><title>Verify your Identity</title> <body> Hey " + fullname + "! <br> Thankyou for signing-up in the 2BiG Platform! Use your ID number to log-in. <br> ID Number: <strong>" + idno + "</strong></body></html>";
                        message.IsBodyHtml = true;

                        //SAVE TO SESSIONS TO USE IN THE REQUEST ANOTHER CODE
                        Session["receiverEmail"] = email;
                        Session["receiverName"] = fullname;


                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(fromMail, fromPassword),
                            EnableSsl = true,
                        };

                        try
                        {
                            smtpClient.Send(message);

                            Session["verificationCode"] = verificationCode.ToString();
                        }
                        catch (Exception ex)
                        {
                            // Log the exception
                            Debug.WriteLine("Error sending email: " + ex.ToString());
                            Response.Write("<script>alert('Error sending email. Please try again later.');</script>");
                        }

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Debug.WriteLine("Error retrieving data from Firebase: " + ex.ToString());
                Response.Write("<script>alert('Error retrieving data from Firebase. Please try again later.');</script>");
            }
        }
        //ENCRYPTING THE PASSWORD
        private string GetSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        //LOGGING IN 
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //Get the id number and password entered by the user
                string idno = txt_idno.Text.Trim();
                string password = txt_password.Text.Trim();

                if (idno != null || password != null)
                {
                    string temp = (string)Session["temporaryRole"];

                    if (temp == "Admin")
                    {
                        isAdmin();
                    }
                    else if (temp == "Cashier")
                    {
                        isCashier();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please enter your credentials to login');</script>");
                }
              
            }
            catch (Exception ex)
            {
                // Handle the exception and log the error message
                string errorMessage = "An error occurred while trying to login: " + ex.Message;
            }
        }

        protected void roleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected value from the DropDownList
            string selectedValue = roleType.SelectedValue;

            // Perform actions based on the selected value
            if (selectedValue == "Admin")
            {
                Session["temporaryRole"] = "Admin";
                Debug.WriteLine($"selectedRole: {selectedValue}");
                string role = (string)Session["temporaryRole"];
                Debug.WriteLine($"TempRole:{role}");
            }
            else if (selectedValue == "Cashier")
            {
                Session["temporaryRole"] = "Cashier";

                string role = (string)Session["temporaryRole"];
                Debug.WriteLine($"TempRole:{role}");
            }
            
        }

        //USING ID NUMBER TO LOGIN
        //private void isCashier()
        //{
        //    //Get the id number and password entered by the user
        //    string idno = txt_idno.Text.Trim();
        //    string password = txt_password.Text.Trim();

        //    string hashedPassword = GetSHA256Hash(password);

        //    bool isLoggedIn = false; // Flag to track if login is successful

        //    FirebaseResponse empResponse = twoBigDB.Get("EMPLOYEES/");
        //    Employee employee = empResponse.ResultAs<Employee>(); //Database result
        //    var employees = empResponse.Body;
        //    Dictionary<string, Employee> cashiers = JsonConvert.DeserializeObject<Dictionary<string, Employee>>(employees);

        //    // Create a list to store all the cashier employees 
        //    List<Model.Employee> cashierEmployees = new List<Model.Employee>();

        //    if (cashiers != null)
        //    {
        //        foreach (KeyValuePair<string, Employee> entry in cashiers)
        //        {
        //            string empID = entry.Value.emp_id.ToString();
                    
        //            if (entry.Value.emp_role == "Cashier")
        //            {
        //                cashierEmployees.Add(entry.Value);
        //            }
        //        }

        //        foreach (var cashier in cashierEmployees)
        //        {
        //            //check all the cashier employee of that admin if ni match sa gi enter sa user
        //            if (cashier.emp_pass == hashedPassword && cashier.emp_id == int.Parse(idno))
        //            {
        //                //store sa session storage
        //                Session["idno"] = cashier.emp_id;
        //                Session["pass"] = cashier.emp_pass;

        //                isLoggedIn = true; // Set the flag to indicate successful login
        //                break; // Exit the loop since login is successful
        //            }
                   
        //        }

        //        if (isLoggedIn)
        //        {
        //            //use the session storage to access the idno and pass nga ni match sa gi input sa user

        //            int currentID = (int)Session["idno"];
        //            string currentPass = Session["pass"].ToString();

        //            //compare the hashedpassword (from the input) if it matches the hash password stored in the database
        //            if (currentID == int.Parse(idno) && currentPass == hashedPassword)
        //            {
        //                Debug.WriteLine($"originalPass:{currentPass}");
        //                Debug.WriteLine($"inputHashed:{hashedPassword}");

        //                empResponse = twoBigDB.Get("EMPLOYEES/" + currentID);
        //                Employee cashier = empResponse.ResultAs<Employee>();

        //                string ownerID = cashier.adminId.ToString();

        //                //SAVE THE EMPLOYEE DETAILS INTO SESSION
        //                Session["role"] = cashier.emp_role;
        //                Session["cashierID"] = idno;
        //                Session["idno"] = ownerID;
        //                Session["password"] = password;
        //                Session["fname"] = cashier.emp_firstname;
        //                Session["mname"] = cashier.emp_midname;
        //                Session["lname"] = cashier.emp_lastname;
        //                Session["fullName"] = cashier.emp_firstname + " " + cashier.emp_midname + " " + cashier.emp_lastname;
        //                Session["dob"] = cashier.emp_birthdate;
        //                Session["contactNumber"] = cashier.emp_contactnum;
        //                Session["email"] = cashier.emp_email;

        //                int adminID = cashier.adminId;

        //                // Retrieve all RefillingStation objects for the current admin
        //                empResponse = twoBigDB.Get("ADMIN/" + adminID + "/RefillingStation/");
        //                RefillingStation stations = empResponse.ResultAs<RefillingStation>();
        //                //IDictionary<string, RefillingStation> stations = response.ResultAs<IDictionary<string, RefillingStation>>();
        //                Session["stationName"] = stations.stationName;
        //                Session["address"] = stations.stationAddress;
        //                Session["operatingHrsTo"] = stations.operatingHrsTo;
        //                Session["operatingHrsFrom"] = stations.operatingHrsFrom;
        //                Session["status"] = stations.status;
        //                Session["businessdaysTo"] = stations.businessDaysTo;
        //                Session["businessdaysFrom"] = stations.businessDaysFrom;

        //                //generate a random number for users logged
        //                Random rnd = new Random();
        //                int idnum = rnd.Next(1, 10000);
        //                //Get the current date and time
        //                DateTime loginTime = DateTime.Now;

        //                //Store the login information in the USERLOG table
        //                var data = new UsersLogs
        //                {
        //                    logsId = idnum,
        //                    userIdnum = int.Parse(idno),
        //                    userFullname = (string)Session["fullName"],
        //                    userActivity = "LOGGED IN",
        //                    activityTime = loginTime,
        //                    role = "Cashier"
        //                };

        //                //Storing the  info
        //                var response = twoBigDB.Set("ADMINLOGS/" + data.logsId, data);//Storing data to the database
        //                UsersLogs res = response.ResultAs<UsersLogs>();//Database Result

        //                Response.Write("<script>alert ('Login Successfull!');  window.location.href = '/Admin/AdminIndex.aspx'; </script>");
        //            }
        //            else
        //            {
        //                Response.Write("<script>alert ('Cashier Account not found! Try again');  window.location.href = '/LandingPage/Account.aspx'; </script>");
        //            }
        //        }
        //    }
        //}
        private void isCashier() //LOGIN USING EMAIL FOR CASHIER
        {
            //Get the id number and password entered by the user
            string email = txt_idno.Text.Trim();
            string password = txt_password.Text.Trim();

            string hashedPassword = GetSHA256Hash(password);

            bool isLoggedIn = false; // Flag to track if login is successful
            bool emailFound = false; // Flag to track if the email is found

            FirebaseResponse empResponse = twoBigDB.Get("EMPLOYEES/");
            var cashiers = empResponse.ResultAs<Dictionary<string, Employee>>();

            // Create a list to store all the cashier employees 
            List<Model.Employee> cashierEmployees = new List<Model.Employee>();

            if (cashiers != null)
            {
                foreach (KeyValuePair<string, Employee> entry in cashiers)
                {
                    
                    if (entry.Value.emp_role == "Cashier")
                    {
                        cashierEmployees.Add(entry.Value);
                    }
                    
                }
                if (cashierEmployees.Count == 0)
                {
                    Response.Write("<script>alert ('No existing CASHIER Account.');  window.location.href = '/LandingPage/Account.aspx'; </script>");

                }

                foreach (var cashier in cashierEmployees)
                {
                    if (cashier.emp_email == email)
                    {
                        emailFound = true; // Set the flag to indicate email found

                        Debug.WriteLine($"EMAIL:{cashier.emp_email}");

                        if (cashier.emp_pass == hashedPassword)
                        {
                            Debug.WriteLine($"password:{cashier.emp_pass}");

                            Session["idno"] = cashier.emp_id.ToString();
                            Session["email"] = cashier.emp_email;
                            Session["pass"] = cashier.emp_pass;
                            isLoggedIn = true; // Set the flag to indicate successful login
                            break; // Exit the loop since login is successful
                        }
                        else
                        {
                            Response.Write("<script>alert ('Incorrect Password! Try again');  window.location.href = '/LandingPage/Account.aspx'; </script>");

                        }
                    }

                }

                if (!emailFound)
                {
                    Response.Write("<script>alert ('Email not found! Try again');  window.location.href = '/LandingPage/Account.aspx'; </script>");
                }

                if (isLoggedIn)
                {
                    //use the session storage to access the idno and pass nga ni match sa gi input sa user

                    string currentEmail = Session["email"].ToString();
                    string currentPass = Session["pass"].ToString();
                    string idno = Session["idno"].ToString();

                    //compare the hashedpassword (from the input) if it matches the hash password stored in the database
                    if (currentEmail == email && currentPass == hashedPassword)
                    {
                        Debug.WriteLine($"originalPass:{currentPass}");
                        Debug.WriteLine($"inputHashed:{hashedPassword}");

                        empResponse = twoBigDB.Get("EMPLOYEES/" + idno);
                        Employee cashier = empResponse.ResultAs<Employee>();

                        string ownerID = cashier.adminId.ToString();

                        //SAVE THE EMPLOYEE DETAILS INTO SESSION
                        Session["role"] = cashier.emp_role;
                        Session["cashierID"] = cashier.emp_id;
                        Session["idno"] = ownerID;
                        Session["password"] = password;
                        Session["fname"] = cashier.emp_firstname;
                        Session["mname"] = cashier.emp_midname;
                        Session["lname"] = cashier.emp_lastname;
                        Session["fullName"] = cashier.emp_firstname + " " + cashier.emp_midname + " " + cashier.emp_lastname;
                        Session["dob"] = cashier.emp_birthdate;
                        Session["contactNumber"] = cashier.emp_contactnum;
                        Session["email"] = cashier.emp_email;

                        int adminID = cashier.adminId;

                        // Retrieve all RefillingStation objects for the current admin
                        empResponse = twoBigDB.Get("ADMIN/" + adminID + "/RefillingStation/");
                        RefillingStation stations = empResponse.ResultAs<RefillingStation>();
                        //IDictionary<string, RefillingStation> stations = response.ResultAs<IDictionary<string, RefillingStation>>();
                        Session["stationName"] = stations.stationName;
                        Session["address"] = stations.stationAddress;
                        Session["operatingHrsTo"] = stations.operatingHrsTo;
                        Session["operatingHrsFrom"] = stations.operatingHrsFrom;
                        Session["status"] = stations.status;
                        Session["businessdaysTo"] = stations.businessDaysTo;
                        Session["businessdaysFrom"] = stations.businessDaysFrom;

                        //generate a random number for users logged
                        Random rnd = new Random();
                        int idnum = rnd.Next(1, 10000);
                        //Get the current date and time
                        DateTime loginTime = DateTime.Now;

                        //Store the login information in the USERLOG table
                        var data = new UsersLogs
                        {
                            logsId = idnum,
                            userIdnum = cashier.emp_id,
                            userFullname = (string)Session["fullName"],
                            userActivity = "LOGGED IN",
                            activityTime = loginTime,
                            role = "Cashier"
                        };

                        //Storing the  info
                        var response = twoBigDB.Set("ADMINLOGS/" + data.logsId, data);//Storing data to the database
                        UsersLogs res = response.ResultAs<UsersLogs>();//Database Result

                        Response.Write("<script>alert ('Login Successfull!');  window.location.href = '/Admin/AdminIndex.aspx'; </script>");
                    }
                    else
                    {
                        Response.Write("<script>alert ('Cashier Account not found! Try again');  window.location.href = '/LandingPage/Account.aspx'; </script>");
                    }
                }
            }
        } 
       
        //private void isAdmin() //LOGIN USING ID NUMBER FOR ADMIN
        //{

        //    //Get the id number and password entered by the user
        //    string idno = txt_idno.Text.Trim();
        //    string password = txt_password.Text.Trim();

        //    string hashedPassword = GetSHA256Hash(password);

        //    Debug.WriteLine($"inputHashed:{hashedPassword}");

        //    FirebaseResponse response;
        //    response = twoBigDB.Get("ADMIN/" + idno);
        //    AdminAccount user = response.ResultAs<AdminAccount>(); //Database result


        //    if (user != null)
        //    {
        //        //check all admins if ni match sa gi enter sa user
        //        if (user.pass == hashedPassword && user.idno == int.Parse(idno))
        //        {
        //            //Generate a unique key for the USERLOG table using the Push method
        //            //var userLogResponse = twoBigDB.Push("USERLOG/", userLog);

        //            ////Get the unique key generated by the Push method
        //            //string userLogKey = userLogResponse.Result.name;
        //            Session["role"] = user.userRole;
        //            Session["idno"] = idno;
        //            Session["password"] = password;
        //            Session["fname"] = user.fname;
        //            Session["mname"] = user.mname;
        //            Session["lname"] = user.lname;
        //            Session["fullName"] = user.fname + " " + user.mname + " " + user.lname;
        //            Session["dob"] = user.bdate;
        //            Session["contactNumber"] = user.phone;
        //            Session["email"] = user.email;
        //            Session["profile_image"] = user.profile_image;


        //            //Retrieve all RefillingStation objects for the current admin

        //           response = twoBigDB.Get("ADMIN/" + user.idno + "/RefillingStation/");
        //            RefillingStation stations = response.ResultAs<RefillingStation>();
                    
        //            Session["stationName"] = stations.stationName;
        //            Session["address"] = stations.stationAddress;
        //            Session["operatingHrsTo"] = stations.operatingHrsTo;
        //            Session["operatingHrsFrom"] = stations.operatingHrsFrom;
        //            Session["status"] = stations.status;
        //            Session["businessdaysTo"] = stations.businessDaysTo;
        //            Session["businessdaysFrom"] = stations.businessDaysFrom;



        //            // Retrieve all TankSupply objects for the current admin
        //            response = twoBigDB.Get("TANKSUPPLY/");
        //            TankSupply obj = response.ResultAs<TankSupply>();

        //            if (obj != null)
        //            {
        //                Session["tankId"] = obj.tankId;
        //                Session["tankVolume"] = obj.tankVolume;
        //                Session["tankUnit"] = obj.tankUnit;
        //                Session["dateAdded"] = obj.dateAdded;
        //            }

        //            // Retrieve Employee records for the current admin
        //            response = twoBigDB.Get("EMPLOYEE/");
        //            Employee emp = response.ResultAs<Employee>();

        //            if (emp != null)
        //            {
        //                Session["emp_id"] = emp.emp_id;
        //                Session["empName"] = emp.emp_lastname + ' ' + emp.emp_firstname;

        //            }


        //            //generate a random number for users logged
        //            Random rnd = new Random();
        //            int idnum = rnd.Next(1, 10000);
        //            //Get the current date and time
        //            DateTime loginTime = DateTime.Now;

        //            //Store the login information in the USERLOG table
        //            var data = new UsersLogs
        //            {
        //                logsId = idnum,
        //                userIdnum = int.Parse(idno),
        //                userFullname = (string)Session["fullName"],
        //                userActivity = "LOGGED IN",
        //                activityTime = loginTime,
        //                role = "Admin"
        //            };

        //            //Storing the  info
        //            response = twoBigDB.Set("ADMINLOGS/" + data.logsId, data);//Storing data to the database
        //            UsersLogs res = response.ResultAs<UsersLogs>();//Database Result


        //            // Login successful, redirect to admin homepage
        //            Response.Write("<script>alert ('Login Successfull!');</script>");
        //            //Response.Redirect("/Admin/AdminIndex.aspx");



        //            //TO CHECK THE STATUS OF THE ADMIN
        //            var admin = (string)Session["idno"];
        //            int adminId = int.Parse(admin);

        //            // Retrieve the existing admin data from the database
        //            FirebaseResponse getStatus = twoBigDB.Get("ADMIN/" + adminId);
        //            Model.AdminAccount pendingClients = getStatus.ResultAs<Model.AdminAccount>();

        //            string clientStat = pendingClients.status;
        //            string subStatus = pendingClients.subStatus;

        //            // Retrieve the existing subscribed admin from the database

        //            if (clientStat == "Pending") //NOT APPROVED YET, BY THE SUPERADMIN
        //            {
        //                Response.Write("<script>window.location.href = '/Admin/WaitingPage.aspx'; </script>");

        //            }
        //            else if (subStatus == "notSubscribed")
        //            {
        //                Response.Write("<script> window.location.href = '/Admin/AdminProfile.aspx'; </script>");
        //            }

        //            else if (clientStat == "Approved" && subStatus == "Subscribed") //APPROVED AND SUBSCRIBED
        //            {
        //                Response.Write("<script>window.location.href = '/Admin/AdminIndex.aspx'; </script>");

        //            }

        //        }
        //        else
        //        {
        //            Response.Write("<script>alert ('Admin Account not found! Try again');  window.location.href = '/LandingPage/Account.aspx'; </script>");
        //        }
        //    }
        //}
        private void isAdmin() //LOGIN USING EMAIL NUMBER FOR ADMIN
        {

            try
            {
                //Get the id number and password entered by the user
                string email = txt_idno.Text.Trim();
                string password = txt_password.Text.Trim();

                string hashedPassword = GetSHA256Hash(password);

                Debug.WriteLine($"inputHashed:{hashedPassword}");

                bool isLoggedIn = false; // Flag to track if login is successful
                bool emailFound = false; // Flag to track if the email is found

                FirebaseResponse empResponse = twoBigDB.Get("ADMIN/");
                var admins = empResponse.ResultAs<Dictionary<string, AdminAccount>>();

                // Create a list to store all the cashier employees 
                List<Model.AdminAccount> regAdmins = new List<Model.AdminAccount>();

               
                if (admins != null)
                {
                    foreach (KeyValuePair<string, AdminAccount> entry in admins)
                    {
                        if (entry.Value.email == email)
                        {
                            emailFound = true; // Set the flag to indicate email found

                            Debug.WriteLine($"EMAIL:{entry.Value.email}");

                            if (entry.Value.pass == hashedPassword)
                            {
                                Debug.WriteLine($"inputHashed:{entry.Value.pass}");


                                Session["idno"] = entry.Value.idno.ToString();
                                isLoggedIn = true; // Set the flag to indicate successful login
                                break; // Exit the loop since login is successful
                            }
                            else
                            {
                                Response.Write("<script>alert ('Incorrect Password! Try again');  window.location.href = '/LandingPage/Account.aspx'; </script>");
                               
                            }
                        }
                    }

                    if (!emailFound)
                    {
                        Response.Write("<script>alert ('Email not found! Try again');  window.location.href = '/LandingPage/Account.aspx'; </script>");
                    }

                    if (isLoggedIn)
                    {
                        
                        string idno = (string)Session["idno"];
                        empResponse = twoBigDB.Get("ADMIN/" + idno);
                        AdminAccount user = empResponse.ResultAs<AdminAccount>(); //Database result

                        if (user.status == "notVerified")
                        {
                            Response.Write("<script>alert ('It seems like your account is not VERIFIED yet. Please check your email for the Verification Code and verify your account.');  window.location.href = '/LandingPage/EmailVerification.aspx'; </script>");
                        }

                        Session["role"] = user.userRole;
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

                        // Retrieve all RefillingStation objects for the current admin
                        empResponse = twoBigDB.Get("ADMIN/" + user.idno + "/RefillingStation/");
                        RefillingStation stations = empResponse.ResultAs<RefillingStation>();
                        //IDictionary<string, RefillingStation> stations = response.ResultAs<IDictionary<string, RefillingStation>>();
                        Session["stationName"] = stations.stationName;
                        Session["address"] = stations.stationAddress;
                        Session["operatingHrsTo"] = stations.operatingHrsTo;
                        Session["operatingHrsFrom"] = stations.operatingHrsFrom;
                        Session["status"] = stations.status;
                        Session["businessdaysTo"] = stations.businessDaysTo;
                        Session["businessdaysFrom"] = stations.businessDaysFrom;

                        // Retrieve all TankSupply objects for the current admin
                        empResponse = twoBigDB.Get("TANKSUPPLY/");
                        TankSupply obj = empResponse.ResultAs<TankSupply>();

                        if (obj != null)
                        {
                            Session["tankId"] = obj.tankId;
                            Session["tankVolume"] = obj.tankVolume;
                            Session["tankUnit"] = obj.tankUnit;
                            Session["dateAdded"] = obj.dateAdded;
                        }

                        //// Retrieve Employee records for the current admin
                        //empResponse = twoBigDB.Get("EMPLOYEE/");
                        //Employee emp = empResponse.ResultAs<Employee>();

                        //if (emp != null)
                        //{
                        //    Session["emp_id"] = emp.emp_id;
                        //    Session["empName"] = emp.emp_lastname + ' ' + emp.emp_firstname;

                        //}


                        //USERS LOG
                        Random rnd = new Random(); //generate a random number 
                        int idnum = rnd.Next(1, 10000);
                        //Get the current date and time
                        DateTime loginTime = DateTime.Now;

                        //Store the login information in the USERLOG table
                        var data = new UsersLogs
                        {
                            logsId = idnum,
                            userIdnum = int.Parse(idno),
                            userFullname = (string)Session["fullName"],
                            userActivity = "LOGGED IN",
                            activityTime = loginTime,
                            role = "Admin"
                        };

                        //Storing the  info
                        empResponse = twoBigDB.Set("ADMINLOGS/" + data.logsId, data);//Storing data to the database
                        UsersLogs res = empResponse.ResultAs<UsersLogs>();//Database Result


                        // Login successful, redirect to admin homepage
                        Response.Write("<script>alert ('Login Successfull!');</script>");
                        //Response.Redirect("/Admin/AdminIndex.aspx");


                        //TO CHECK THE STATUS OF THE ADMIN
                        string clientStat = user.status;
                        string subStatus = user.subStatus;

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
                        Response.Write("<script>alert ('Account not found. Please try again');  window.location.href = '/LandingPage/Account.aspx'; </script>");
                    }


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

