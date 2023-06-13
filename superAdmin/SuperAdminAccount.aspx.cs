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

namespace WRS2big_Web.superAdmin
{
    public partial class SuperAdminAccount : System.Web.UI.Page
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
        protected void btnSignup_Click(object sender, EventArgs e)
        {

            try
            {

                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                string address = Request.Form["address"];
                double latitude = double.Parse(Request.Form["lat"]);
                double longitude = double.Parse(Request.Form["long"]);


                // Password validation
                string password = Server.HtmlEncode(id_passwordreg.Text);
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

                // Encrypt the password using SHA-256
                string hashedPassword = GetSHA256Hash(password);

                var data = new SuperAccount
                {
                    superIDno = idnum,
                    lname = txtlname.Text,
                    fname = txtfname.Text,
                    mname = txtmname.Text,
                    bdate = txtbirthdate.Text,
                    phone = contactNum,
                    email = txtEmail.Text,
                    pass = hashedPassword,
                    dateRegistered = DateTime.Now,
                    userRole = "Super Admin",
                    address = address
                };
               

                SetResponse response;
                //Storing the admin info
                response = twoBigDB.Set("SUPERADMIN/" + data.superIDno, data);//Storing data to the database
                SuperAccount res = response.ResultAs<SuperAccount>();//Database Result

                //SAVE LOGS TO SUPER ADMIN
                //Get the current date and time
                DateTime logTime = DateTime.Now;

                //generate a random number for users logged
                //Random rnd = new Random();
                int logID = rnd.Next(1, 10000);

                //var idno = (string)Session["SuperIDno"];
                //string superName = (string)Session["superAdminName"];

                //Store the login information in the USERLOG table
                var log = new Model.superLogs
                {
                    logsId = logID,
                    superID = idnum,
                    superFullname = txtfname.Text + txtmname.Text + txtlname.Text,
                    superActivity = "CREATED NEW SUPERADMIN ACCOUNT",
                    activityTime = logTime
                };

                //Storing the  info
                response = twoBigDB.Set("SUPERADMIN_LOGS/" + log.logsId, log);//Storing data to the database
                Model.superLogs resLog = response.ResultAs<Model.superLogs>();//Database Result

                //SEND NOTIFICATION TO CUSTOMER 
                //Random rnd = new Random();
                int ID = rnd.Next(1, 20000);
                var Notification = new Model.Notification
                {
                   
                    sender = "System",
                    title = "Super Admin New Account",
                    receiver = "Super Admin",
                    body = "A user just created a SuperAdmin Account! SuperAdmin:" + txtfname.Text + txtmname.Text + txtlname.Text,
                    notificationDate = DateTime.Now,
                    status = "unread",
                    notificationID = ID,
                    superAdmin_ID = idnum,

                };

                SetResponse notifResponse;
                notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                Model.Notification notif = notifResponse.ResultAs<Model.Notification>();//Database Result


                Response.Write("<script>alert ('Super Admin Account " + res.superIDno + " created! Use this id number to log in.'); window.location.href = '/superAdmin/SuperAdminAccount.aspx'; </script>");
            }
            catch
            {
                Response.Write("<script>alert('Something went wrong! Try again.'); window.location.href = '/superAdmin/SuperAdminAccount.aspx'; </script>");
            }
        }
       

        //LOGGING IN 
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //DEFAULT CREDENTIALS
                string defaultemail = "42223";
                string defaultpass = "TechniqueServices";

                string uEmail = txt_idno.Text;
                string uPass = txt_password.Text;

                //Get the id number and password entered by the user
                string idno = txt_idno.Text.Trim();
                string password = txt_password.Text.Trim();

                // Encrypt the password using SHApassword-256
                string hashedPassword = GetSHA256Hash(uPass);

                //generate a random number for users logged
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);


                //LOG-IN FOR DEFAULT ACCOUNT
                if (uEmail == defaultemail && uPass == defaultpass)
                {

                    string superAdmin = "Technique Services";

                    Session["SuperIDno"] = defaultemail;
                    Session["email"] = uEmail;
                    Session["password"] = uPass;
                    Session["superAdminName"] = superAdmin;
                    Session["fname"] = superAdmin;

                    //SAVE LOGS TO SUPER ADMIN
                    //Get the current date and time
                    DateTime logTime = DateTime.Now;

                    //generate a random number for users logged
                    //Random rnd = new Random();
                    int logID = rnd.Next(1, 10000);

                    //var idno = (string)Session["SuperIDno"];
                    string superName = (string)Session["superAdminName"];

                    //Store the login information in the USERLOG table
                    var log = new Model.superLogs
                    {
                        logsId = logID,
                        superID = int.Parse(idno),
                        superFullname = superName,
                        superActivity = "LOGGED IN",
                        activityTime = logTime
                    };

                    //Storing the  info
                    FirebaseResponse response = twoBigDB.Set("SUPERADMIN_LOGS/" + log.logsId, log);//Storing data to the database
                    Model.superLogs res = response.ResultAs<Model.superLogs>();//Database Result


                    Response.Write("<script>alert ('Login Successfull!'); window.location.href = '/superAdmin/SAdminIndex.aspx'; </script>");


                }
                else
                {

                    FirebaseResponse response;
                    response = twoBigDB.Get("SUPERADMIN/" + idno);
                    SuperAccount user = response.ResultAs<SuperAccount>(); //Database result
                                                                         

                    //Check if the id number and password are valid
                    if (user != null)
                    {
                        Debug.WriteLine($"PASS FROM DB:{user.pass}");
                        Debug.WriteLine($"ENTERED: {hashedPassword}");
                        //LOG-IN FOR CREATED ACCOUNT
                        if (user.pass == hashedPassword)
                        {
                            //Generate a unique key for the USERLOG table using the Push method
                            //var userLogResponse = twoBigDB.Push("USERLOG/", userLog);

                            ////Get the unique key generated by the Push method
                            //string userLogKey = userLogResponse.Result.name;

                            Session["SuperIDno"] = idno;
                            Session["password"] = password;
                            Session["fname"] = user.fname;
                            Session["mname"] = user.mname;
                            Session["lname"] = user.lname;
                            Session["superAdminName"] = user.fname + " " + user.mname + " " + user.lname;
                            Session["dob"] = user.bdate;
                            Session["contactNumber"] = user.phone;
                            Session["email"] = user.email;



                            //SAVE LOGS TO SUPER ADMIN
                            //Get the current date and time
                            DateTime logTime = DateTime.Now;

                            //generate a random number for users logged
                            //Random rnd = new Random();
                            int logID = rnd.Next(1, 10000);

                            //var idno = (string)Session["SuperIDno"];
                            string superName = (string)Session["superAdminName"];

                            //Store the login information in the USERLOG table
                            var log = new Model.superLogs
                            {
                                logsId = logID,
                                superID = int.Parse(idno),
                                superFullname = superName,
                                superActivity = "LOGGED IN",
                                activityTime = logTime
                            };

                            //Storing the  info
                            response = twoBigDB.Set("SUPERADMIN_LOGS/" + log.logsId, log);//Storing data to the database
                            Model.superLogs res = response.ResultAs<Model.superLogs>();//Database Result


                            Response.Write("<script>alert ('Login Successfull!'); window.location.href = '/superAdmin/SAdminIndex.aspx'; </script>");




                        }
                        else
                        {
                            // Login failed, display error message
                            //lblError.Text = "Invalid email or password!";
                            Response.Write("<script>alert('Incorrect password! Try again.');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('ID Number not found!Try again.');</script>");
                    }
                }

                

            }
            catch
            {
                // Handle the exception and log the error message
                Response.Write("<script>alert('Something went wrong!');</script>");
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
    }
}