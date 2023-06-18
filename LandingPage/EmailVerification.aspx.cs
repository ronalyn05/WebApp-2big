using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
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
            if (Session["verificationCode"] != null)
            {
                string verificationCode = Session["verificationCode"].ToString();
                string verifyCode = enteredCode.Text;



                if (verifyCode == verificationCode)
                {
                    if (Session["receiverIdno"] != null)
                    {
                        string idno = Session["receiverIdno"].ToString();
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
                        Response.Write("<script>alert('Something went wrong. Try again'); window.location.href = 'Account.aspx'; </script>");
                    }
                   
                }
                else
                {
                    Response.Write("<script>alert('Incorrect Verification Code. Double check your email.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Verification Code Expired! Request another code.'); </script>");
            }

        }

        protected void anotherCode_Click(object sender, EventArgs e)
        {
            if (Session["receiverEmail"] != null && Session["receiverName"] != null)
            {
           
                string receiverEmail = Session["receiverEmail"].ToString();
                string receiverName = Session["receiverName"].ToString();

                Random rnd = new Random();
                int verificationCode = rnd.Next(1, 30000);

                string fromMail = "technique.services2022@gmail.com";
                string fromPassword = "qenrtopcfoifbvbo";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "2BiG Account Verification";
                message.To.Add(new MailAddress(receiverEmail));
                message.Body = "<html><title>Verify your Identity</title> <body> Hey " + receiverName + "! You just created an account in the 2BiG Platform. To complete your account registration, enter the code on the Verification Page. <br> Verification Code: <strong>" + verificationCode + "</strong></body></html>";
                message.IsBodyHtml = true;


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
                    Debug.WriteLine($"CODE:{verificationCode}");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Debug.WriteLine("Error sending email: " + ex.ToString());
                    Response.Write("<script>alert('Error sending email. Please try again later.');</script>");
                }

                Response.Write("<script>alert('Verification Code Sent! Please check your email.'); window.location.href = 'EmailVerification.aspx'; </script>");


            }
            else
            {
                Response.Write("<script>alert('Something went wrong!'); </script>");
            }


        }
    }
}