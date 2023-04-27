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
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Data;
using WRS2big_Web.Model;
using System.Globalization;

namespace WRS2big_Web.Admin
{
    public partial class Rewards : System.Web.UI.Page
    {
        //Initialize the FirebaseClient with the database URL and secret key.
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
        protected void btnAddReward_Click(object sender, EventArgs e)
        {
            try
            {
                // Ask the user for confirmation
                string confirmValue = Request.Form["confirm_add"];
                if (confirmValue == "Yes")
                {
                    // The user has confirmed, so proceed with adding the data
                    string idno = (string)Session["idno"];
                    int logsId = (int)Session["logsId"];

                    // Generate reward id number to make as the unique key
                    Random rnd = new Random();
                    int idnum = rnd.Next(1, 10000);

                    // Validate user input
                    if (string.IsNullOrEmpty(txtrewardname.Text) || string.IsNullOrEmpty(txtdescription.Text) || string.IsNullOrEmpty(txtpointsrequired.Text))
                    {
                        Response.Write("<script> alert('Please fill all the required fields.'); </script>");
                        return;
                    }

                    // Convert points required to an integer
                    int pointsRequired = 0;
                    if (!int.TryParse(txtpointsrequired.Text, out pointsRequired))
                    {
                        Response.Write("<script> alert('Invalid points required value. Please enter a valid number.'); </script>");
                        return;
                    }

                    // Get the current UTC date and time as a DateTimeOffset object
                    DateTimeOffset currentDateTime = DateTimeOffset.UtcNow;

                    //// Query the "REWARDS" node in your database
                    //var responseReward = twoBigDB.Get("REWARDS");
                    //Dictionary<string, Reward> rewards = responseReward.ResultAs<Dictionary<string, Reward>>();
                    //if (rewards != null) // Add this null check
                    //{
                    //    // Check if there are any rewards with an expiration date later than the current UTC date and time
                    //    foreach (var reward in rewards.Values)
                    //    {
                    //        if (currentDateTime < reward.promoExpirationTo)
                    //        {
                    //            Response.Write("<script> alert('There is an existing reward promo with a later expiration date. Please wait until that promo expires before adding a new one.'); location.reload(); window.location.href = '/Admin/Rewards.aspx';</script>");
                    //            return;
                    //        }
                    //    }
                    //}

                    // Define the format of the date string
                    string format = "MMMM, dd yyyy";

                    // Convert the date string to a DateTimeOffset using ParseExact
                    DateTimeOffset rewardsDateAdded = DateTimeOffset.ParseExact(currentDateTime.ToString(format), format, CultureInfo.InvariantCulture);

                    // Get the selected values from the CheckBoxList
                    string selectedValues = "";
                    foreach (ListItem item in checkPromoOffered.Items)
                    {
                        if (item.Selected)
                        {
                            selectedValues += item.Value + ",";
                        }
                    }

                    // Remove the trailing comma if there are any selected values
                    if (!string.IsNullOrEmpty(selectedValues))
                    {
                        selectedValues = selectedValues.TrimEnd(',');
                    }
                    // Get the selected values from the CheckBoxList
                    string cusEarnPoints_selectedValues = "";
                    foreach (ListItem item in check_cusEarnPoints.Items)
                    {
                        if (item.Selected)
                        {
                            cusEarnPoints_selectedValues += item.Value + ",";
                        }
                    }

                    // Remove the trailing comma if there are any selected values
                    if (!string.IsNullOrEmpty(cusEarnPoints_selectedValues))
                    {
                        cusEarnPoints_selectedValues = cusEarnPoints_selectedValues.TrimEnd(',');
                    }

                    // Add the reward promo to the database
                    var data = new Reward
                    {
                        rewardId = idnum,
                        adminId = int.Parse(idno),
                        rewardType = txtrewardname.Text,
                        description = txtdescription.Text,
                        points_required = pointsRequired,
                        rewardsDateAdded = rewardsDateAdded,
                        productOffered = selectedValues,
                        cusEarnPoints = cusEarnPoints_selectedValues,
                        promoExpirationFrom = DateTimeOffset.Parse(txtpromoExpirationFrom.Text),
                        promoExpirationTo = DateTimeOffset.Parse(txtpromoExpirationTo.Text)
                    };

                    SetResponse response;
                    response = twoBigDB.Set("REWARDS/" + data.rewardId, data);

                    // Get the current date and time
                    DateTime addedTime = DateTime.UtcNow;

                    //Store the login information in the USERLOG table
                    var rewardLog = new UsersLogs
                    {
                        userIdnum = int.Parse(idno),
                        logsId = idnum,
                        userFullname = (string)Session["fullname"],
                        activityTime = addedTime,
                        userActivity = "ADDED REWARDS OFFERED",
                    };

                    //Storing the  info
                    response = twoBigDB.Set("USERSLOG/" + rewardLog.logsId, rewardLog);//Storing data to the database

                    // Show success message
                    Response.Write("<script> alert('Reward promo added successfully!'); location.reload(); window.location.href = '/Admin/Rewards.aspx'; </script>");
                }
                else
                {
                    // The user has cancelled, so do nothing
                    return;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<pre>" + ex.ToString() + "</pre>");
            }
        }

        //protected void btnAddDiscount_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Ask the user for confirmation
        //        string confirmValue = Request.Form["confirm_add"];
        //        if (confirmValue == "Yes")
        //        {
        //            // The user has confirmed, so proceed with adding the data
        //            string idno = (string)Session["idno"];
        //            int logsId = (int)Session["logsId"];

        //            // Generate reward id number to make as the unique key
        //            Random rnd = new Random();
        //            int idnum = rnd.Next(1, 10000);

        //            // Validate user input
        //            if (string.IsNullOrEmpty(txtrewardname.Text) || string.IsNullOrEmpty(txtdescription.Text) || string.IsNullOrEmpty(txtpointsrequired.Text))
        //            {
        //                Response.Write("<script> alert('Please fill all the required fields.'); </script>");
        //                return;
        //            }

        //            // Convert points required to an integer
        //            int pointsRequired = 0;
        //            if (!int.TryParse(txtpointsrequired.Text, out pointsRequired))
        //            {
        //                Response.Write("<script> alert('Invalid points required value. Please enter a valid number.'); </script>");
        //                return;
        //            }

        //            // Get the current UTC date and time as a DateTimeOffset object
        //            DateTimeOffset currentDateTime = DateTimeOffset.UtcNow;

        //            // Query the "REWARDS" node in your database
        //            var responseReward = twoBigDB.Get("REWARDS");
        //            Dictionary<string, Reward> rewards = responseReward.ResultAs<Dictionary<string, Reward>>();
        //            if (rewards != null) // Add this null check
        //            {
        //                // Check if there are any rewards with an expiration date later than the current UTC date and time
        //                foreach (var reward in rewards.Values)
        //                {
        //                    if (currentDateTime < reward.promoExpirationTo)
        //                    {
        //                        Response.Write("<script> alert('There is an existing reward promo with a later expiration date. Please wait until that promo expires before adding a new one.'); location.reload(); window.location.href = '/Admin/Rewards.aspx';</script>");
        //                        return;
        //                    }
        //                }
        //            }

        //            // Define the format of the date string
        //            string format = "MMMM, dd yyyy";

        //            // Convert the date string to a DateTimeOffset using ParseExact
        //            DateTimeOffset rewardsDateAdded = DateTimeOffset.ParseExact(currentDateTime.ToString(format), format, CultureInfo.InvariantCulture);

        //            // Get the selected values from the CheckBoxList
        //            string selectedValues = "";
        //            foreach (ListItem item in checkPromoOffered.Items)
        //            {
        //                if (item.Selected)
        //                {
        //                    selectedValues += item.Value + ",";
        //                }
        //            }

        //            // Remove the trailing comma if there are any selected values
        //            if (!string.IsNullOrEmpty(selectedValues))
        //            {
        //                selectedValues = selectedValues.TrimEnd(',');
        //            }

        //            // Add the reward promo to the database
        //            var data = new Reward
        //            {
        //                rewardId = idnum,
        //                adminId = int.Parse(idno),
        //                name = txtrewardname.Text,
        //                description = txtdescription.Text,
        //                points_required = pointsRequired,
        //                rewardsDateAdded = rewardsDateAdded,
        //                productOffered = selectedValues,
        //                promoExpirationFrom = DateTimeOffset.Parse(txtpromoExpirationFrom.Text),
        //                promoExpirationTo = DateTimeOffset.Parse(txtpromoExpirationTo.Text)
        //            };

        //            SetResponse response;
        //            response = twoBigDB.Set("REWARDS/" + data.rewardId, data);

        //            // Create a log entry for the user
        //            var log = new Logs
        //            {
        //                adminId = int.Parse(idno),
        //                timestamp = rewardsDateAdded,
        //                action = "Added reward promo: " + data.name
        //            };

        //            // Save the log entry to the "USER_LOGS" node
        //            twoBigDB.Update("USER_LOGS/" + idno, log);

        //            // Show success message
        //            Response.Write("<script> alert('Discount added successfully!'); location.reload(); window.location.href = '/Admin/Rewards.aspx'; </script>");
        //        }
        //        else
        //        {
        //            // The user has cancelled, so do nothing
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<pre>" + ex.ToString() + "</pre>");
        //    }
        //}
    }
}