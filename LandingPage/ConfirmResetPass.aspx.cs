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
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace WRS2big_Web.LandingPage
{
    public partial class ConfirmResetPass : System.Web.UI.Page
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

        protected void resetPass_Click(object sender, EventArgs e)
        {
            
            // Password validation
            string newUserPass = Server.HtmlEncode(newPassword.Text);
            if (newUserPass.Length < 8 || newUserPass.Length > 20 ||
                !newUserPass.Any(char.IsLetter) || !newUserPass.Any(char.IsDigit) ||
                !newUserPass.Any(c => !char.IsLetterOrDigit(c)))
            {
                Response.Write("<script>alert('Password must be in between 8-20 characters long and contain at least 1 letter, 1 number, and 1 special character.'); </script>");
                return;
            }

            // Encrypt the password using SHA-256
            string hashedPassword = GetSHA256Hash(newUserPass);

            Random rnd = new Random();

            int adminID = (int)Session["currentAdmin"];

            if (adminID != 0)
            {
                FirebaseResponse response;
                response = twoBigDB.Get("ADMIN/" + adminID);
                AdminAccount user = response.ResultAs<AdminAccount>();

                if (user != null)
                {
                    user.pass = hashedPassword;
                    response = twoBigDB.Update("ADMIN/" + adminID, user);


                    //SEND ADMINLOGS 
                    int logID = rnd.Next(1, 10000);
                    // Get the current date and time
                    DateTime now = DateTime.Now;

                    // Log user activity
                    var userLog = new UsersLogs
                    {
                        userIdnum = user.idno,
                        logsId = logID,
                        userFullname = user.fname + " " + user.mname + " " + user.lname,
                        userActivity = "RESET PASSWORD",
                        activityTime = now
                    };

                    FirebaseResponse exResponse = twoBigDB.Set("ADMINLOGS/" + userLog.logsId, userLog);//Storing data to the database

                     Response.Write("<script>alert ('Reset Password Successful! You can now login to your account with your new password');window.location.href = '/LandingPage/Account.aspx'; </script>");
                }
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