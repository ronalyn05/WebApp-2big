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
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using WRS2big_Web.Model;
using System.Text;

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
                string latitude = Request.Form["lat"];
                string longitude = Request.Form["long"];

                var data = new AdminAccount
                {
                    idno = idnum,
                    lname = txtlname.Text,
                    fname = txtfname.Text,
                    mname = txtmname.Text,
                    bdate = txtbirthdate.Text,
                    phone = txtphoneNum.Text,
                    email = txtEmail.Text,
                    pass = id_passwordreg.Text,
                    
                };
                var list = new RefillingStation
                {

                    addLongitude = longitude,
                    addLattitude = latitude,
                    stationName = txtStationName.Text,
                    stationAddress = address,
                    proof = null
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
                        list.proof = Encoding.ASCII.GetBytes(downloadUrl);
                    }
                }
                SetResponse response;
                //Storing the admin info
                response = twoBigDB.Set("ADMIN/" + data.idno, data);//Storing data to the database
                AdminAccount res = response.ResultAs<AdminAccount>();//Database Result

                //Storing the refilling station info
                response = twoBigDB.Set("ADMIN/" + data.idno + "/RefillingStation/" + list.stationName, list);//Storing data to the database
                RefillingStation result = response.ResultAs<RefillingStation>();//Database Result
                Response.Write("<script>alert ('Account " + res.idno + " created! Use this id number to log in.'); location.reload(); window.location.href = '/LandingPage/Account.aspx'; </script>");

            }
            catch
            {
                Response.Write("<script>alert('ID No. already exist'); window.location.href = 'Account.aspx'; </script>");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            //Get the email and password entered by the user
            //string username = txt_username.Text;
            //string password = txt_password.Text;
            string idno = txt_idno.Text;
            string password = txt_password.Text;

            // //Check if the email and password are valid
            //FirebaseResponse response;
            //response = twoBigDB.Get("ADMIN" + username);
            ////response = twoBigDB.Get("ADMIN" );
            //Model.AdminAccount user = response.ResultAs<Model.AdminAccount>();
            ////if (user == null)
            ////    user = new Model.AdminAccount();

            //if (user.Username == username && user.Pass == password)
            //{
            //    Session["username"] = username;
            //    Session["password"] = password;
            //    Session["WRSname"] = user.WRS_Name;
            //    // Login successful, redirect to admin homepage
            //    Response.Redirect("/Admin/AdminIndex.aspx");

            //}
            //else
            //{
            //    // Login failed, display error message
            //    //lblError.Text = "Invalid email or password!";
            //    Response.Write("<script>alert('Invalid username or password');</script>");
            //}

            //string email = txtEmail.Text;
            //string password = txtPassword.Text;

            //FirebaseResponse response = await twoBigDB.GetAsync("ADMIN/" + txt_username.Text);
            //var result = twoBigDB.Get("ADMIN/" + username);
            //Model.AdminAccount user = result.ResultAs<Model.AdminAccount>();
            ////lblError.Text = result;

            //Response.Write("<script>alert('Response: ' " + result + ");</script>");
            //if (user.Username != null)
            //{
            //    Response.Write("<script>alert('Password: ' "+ user.Pass + ");</script>");

            //    if (user.Pass == password)
            //    {
            //        // Login successful
            //        Session["user"] = user;
            //        Session["WRSname"] = user.WRS_Name;
            //        Response.Redirect("/Admin/Account.aspx");
            //    }
            //    else
            //    {
            //        // Incorrect password
            //        lblError.Text = "Incorrect username or password";
            //    }
            //}
            //else
            //{
            //    // User not found
            //    lblError.Text = " User not found";
            //}

            FirebaseResponse response;
            response = twoBigDB.Get("ADMIN/" + idno);
            AdminAccount user = response.ResultAs<AdminAccount>();

            //if (user.Idno == int.Parse(idno) && user.Pass == password)
            //{
            //    Session["idno"] = idno;
            //    Session["password"] = password;
            //    Session["WRSname"] = user.WRS_Name;
            //    // Login successful, redirect to admin homepage
            //    Response.Redirect("/Admin/AdminIndex.aspx");
            //}
            //else
            //{
            //    // Login failed, display error message
            //    //lblError.Text = "Invalid email or password!";
            //    Response.Write("<script>alert('Invalid username or password');</script>");
            //}

            //Check if the id number and password are valid
            if (user != null)
            {
                if (user.pass == password)
                {
                    Session["idno"] = idno;
                    Session["password"] = password;
                    //Session["WRSname"] = user.WRS_Name;
                    Session["fname"] = user.fname;
                    Session["mname"] = user.mname;
                    Session["lname"] = user.lname;
                    Session["fullName"] = user.fname + " " + user.mname + " " + user.lname;
                    Session["dob"] = user.bdate;
                    Session["contactNumber"] = user.phone;
                    Session["email"] = user.email;
                    //Session["subType"] = user.subType;
                    //Session["subsDate"] = user.subsDate;
                    //Session["subEnd"] = user.subEnd;
                    Session["profile_image"] = user.profile_image;


                    // Login successful, redirect to admin homepage
                    Response.Write("<script>alert ('Login Successfull!'); location.reload(); window.location.href = '/Admin/AdminIndex.aspx'; </script>");
                    //Response.Redirect("/Admin/WaitingPage.aspx");
                }
                else
                {
                    // Login failed, display error message
                    //lblError.Text = "Invalid email or password!";
                    Response.Write("<script>alert('Invalid username or password');</script>");
                }
            }
        }
        //else
        //{
        //    // User not found
        //    //lblError.Text = " User not found";
        //    Response.Write("<script>alert('User not found');</script>");
        //}             

    }
}

