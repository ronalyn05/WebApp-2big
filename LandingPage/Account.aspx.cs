﻿using System;
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
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                string address = Request.Form["address"];
                double latitude = double.Parse(Request.Form["lat"]);
                double longitude = double.Parse(Request.Form["long"]);

                var data = new AdminAccount
                {
                    idno = idnum,
                    lname = txtlname.Text,
                    fname = txtfname.Text,
                    mname = txtmname.Text,
                    bdate = txtbirthdate.Text,
                    phone = txtphoneNum.Text,
                    email = txtEmail.Text,
                    pass = id_passwordreg.Text
                    
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

                byte[] fileBytes = null;
                if (txtproof.HasFile)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        txtproof.PostedFile.InputStream.CopyTo(memoryStream);
                        fileBytes = memoryStream.ToArray();
                    }
                }

                if (fileBytes != null)
                {
                    var storage = new FirebaseStorage("big-system-64b55.appspot.com");
                    var fileExtension = Path.GetExtension(txtproof.FileName);
                    var filePath = $"station/{list.stationName}{fileExtension}";
                    //  used the using statement to ensure that the MemoryStream object is properly disposed after it's used.
                    using (var stream = new MemoryStream(fileBytes))
                    {
                        var storageTask = storage.Child(filePath).PutAsync(stream);
                        var downloadUrl = await storageTask;
                        // used Encoding.ASCII.GetBytes to convert the downloadUrl string to a byte[] object.
                        list.proof = downloadUrl;
                    }
                }
                
                SetResponse response;
                //Storing the admin info
                response = twoBigDB.Set("ADMIN/" + data.idno, data);//Storing data to the database
                AdminAccount res = response.ResultAs<AdminAccount>();//Database Result

                //Storing the refilling station info
                response = twoBigDB.Set("ADMIN/" + data.idno + "/RefillingStation/", list);//Storing data to the database
                RefillingStation result = response.ResultAs<RefillingStation>();//Database Result

               ValidateTerms();
                Response.Write("<script>alert ('Account " +  res.idno + " created! Use this id number to log in.'); location.reload(); window.location.href = '/LandingPage/Account.aspx'; </script>");
                //Response.Write("<script>alert ('Your account has suucessfully created, please wait for approval before you login! Use this id number to log in.'); location.reload(); window.location.href = '/LandingPage/Account.aspx'; </script>");
            }
            catch
            {
                Response.Write("<script>alert('Data already exist'); window.location.href = 'Account.aspx'; </script>");
            }
        }
       // To validate the terms and condition
        protected void ValidateTerms()
        {
            bool terms = chkTerms.Checked;
            if (!terms)
            {
                lblTerms.Text = "You must agree to our terms and conditions!";
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

                //generate a random number for users logged
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

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

                        //Get the current date and time
                        DateTime loginTime = DateTime.Now;

                        //Store the login information in the USERLOG table
                        var data = new UserLogs
                        {
                            logsId = idnum,
                            userIdnum = int.Parse(idno),
                            dateLogs = loginTime
                        };

                        //Storing the  info
                        response = twoBigDB.Set("USERLOG/" + data.userIdnum, data);//Storing data to the database
                        UserLogs res = response.ResultAs<UserLogs>();//Database Result


                        //Generate a unique key for the USERLOG table using the Push method
                       // var userLogResponse = twoBigDB.Push("USERLOG/", userLog);

                        //Get the unique key generated by the Push method
                       // string userLogKey = userLogResponse.Result.name;

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
                        Session["operatingHrs"] = stations.operatingHrs;
                        Session["status"] = stations.status;
                        Session["businessdays"] = stations.businessDays;

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

                        // Retrieve all TankSupply objects for the current admin
                        response = twoBigDB.Get("EMPLOYEE/");
                        Employee emp = response.ResultAs<Employee>();

                        if (emp != null)
                        {
                            Session["emp_id"] = emp.emp_id;
                            
                        }

                        // Login successful, redirect to admin homepage
                        Response.Write("<script>alert ('Login Successfull!'); location.reload(); window.location.href = '/Admin/AdminIndex.aspx'; </script>");
                        //Response.Redirect("/Admin/AdminIndex.aspx");
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

