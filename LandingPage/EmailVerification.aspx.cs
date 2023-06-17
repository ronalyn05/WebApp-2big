using System;
using System.Diagnostics;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using WRS2big_Web.Model;

namespace WRS2big_Web.LandingPage
{
    public partial class EmailVerification : System.Web.UI.Page
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

        protected void submitCode_Click(object sender, EventArgs e)
        {
            string verificationCode = Session["verificationCode"].ToString();
            string verifyCode = enteredCode.Text;



            if (verifyCode == verificationCode)
            {
                string idno = (string)Session["idno"];
                Debug.WriteLine($"IDNO: {idno}");
                FirebaseResponse empResponse = twoBigDB.Get("ADMIN/" + idno);
                AdminAccount user = empResponse.ResultAs<AdminAccount>(); //Database result

                user.dateApproved = user.dateApproved;
                user.dateDeclined = user.dateDeclined;
                user.idno = user.idno;
                user.status = "Pending";
                user.dateVerified = DateTime.Now;
                empResponse = twoBigDB.Update("ADMIN/" + idno, user); //update into database

                Response.Write("<script>alert('Verification Successfull! You can now login your account'); window.location.href = 'Account.aspx'; </script>");
            }
            else
            {
                Response.Write("<script>alert('Incorrect Verification Code. Double check your email.');</script>");
            }
        }
    }
}