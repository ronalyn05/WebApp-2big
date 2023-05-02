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

            string currentUserId = (string)Session["idno"];

            var responseRefill = twoBigDB.Get("PRODUCTREFILL");
            Dictionary<string, ProductRefill> refillProducts = responseRefill.ResultAs<Dictionary<string, ProductRefill>>();
            var filteredRefillList = refillProducts.Values.Where(p => p.adminId.ToString() == currentUserId);

            var responseOther = twoBigDB.Get("otherPRODUCTS");
            Dictionary<string, otherProducts> otherProducts = responseOther.ResultAs<Dictionary<string, otherProducts>>();
            var filteredOtherList = otherProducts.Values.Where(p => p.adminId.ToString() == currentUserId);

            // Add the unit and sizes data of refill products to the CheckBoxList
            List<string> refillUnitSizesList = new List<string>();
            foreach (var product in filteredRefillList)
            {
                string unit = product.pro_refillUnit;
                string size = product.pro_refillSize;
                if (!string.IsNullOrEmpty(unit) && !string.IsNullOrEmpty(size))
                {
                    string unitSizes = size + "  " + unit;
                    if (!refillUnitSizesList.Contains(unitSizes))
                    {
                        refillUnitSizesList.Add(unitSizes);
                    }
                }
            }

            foreach (string unitSizes in refillUnitSizesList)
            {
                chUnitSizes.Items.Add(new ListItem(unitSizes));
            }

            // Add the unit and sizes data of other products to the CheckBoxList
            List<string> otherUnitSizesList = new List<string>();
            foreach (var product in filteredOtherList)
            {
                string unit = product.other_productUnit;
                string size = product.other_productSize;
                if (!string.IsNullOrEmpty(unit) && !string.IsNullOrEmpty(size))
                {
                    string unitSizes = size + "  " + unit;
                    if (!otherUnitSizesList.Contains(unitSizes))
                    {
                        otherUnitSizesList.Add(unitSizes);
                    }
                }
            }

            foreach (string unitSizes in otherUnitSizesList)
            {
                chUnitSizes.Items.Add(new ListItem(unitSizes));
            }


        }
        //STORE REWARDS OFFERS
        protected void btnAddReward_Click(object sender, EventArgs e)
        {
            try
            { 
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
                    string selectedUnitSizes = "";
                    foreach (ListItem item in chUnitSizes.Items)
                    {
                        if (item.Selected)
                        {
                            selectedUnitSizes += item.Value + ",";

                        }
                    }
                    // Remove the trailing comma if there are any selected values
                    if (!string.IsNullOrEmpty(selectedUnitSizes))
                    {
                        selectedUnitSizes = selectedUnitSizes.TrimEnd(',');
                    }


                    // Get the selected values from the CheckBoxList
                    string cusEarnPoints_selectedValues = "";
                    foreach (ListItem item in radioCusEarnPoints.Items)
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

                // Convert points required to an integer
                int percentageVAlue = 0;
                if (!int.TryParse(txtrewardValue.Text, out percentageVAlue))
                {
                    Response.Write("<script> alert('Invalid percentage required value. Please enter a valid percentage number.'); </script>");
                    return;
                }
                // If txtrange_perAmount is null or empty, range_perAmount will be 0.
                int range_perAmount = 0;
                if (!string.IsNullOrEmpty(txtrange_perAmount.Text) && !int.TryParse(txtrange_perAmount.Text, out range_perAmount))
                {
                    Response.Write("<script> alert('Invalid range amount required value. Please enter a valid range amount in number.'); </script>");
                    return;
                }

                // Add the reward promo to the database
                var data = new Reward
                    {
                        rewardId = idnum,
                        adminId = int.Parse(idno),
                        rewardType = txtrewardname.Text,
                        rewardPercentageValue = percentageVAlue,
                        reward_description = txtdescription.Text,
                        points_requiredToClaim = pointsRequired,
                        rewardsDateAdded = rewardsDateAdded,
                        productOffered = selectedValues,
                        range_perAmount = range_perAmount,
                      //  range_perAmount = int.Parse(txtrange_perAmount.Text),
                        cusEarnPoints = cusEarnPoints_selectedValues,
                        pointsPerTxnOrAmount = decimal.Parse(txtpointsPerTxnOrAmount.Text),
                        promoExpirationFrom = DateTimeOffset.Parse(txtpromoExpirationFrom.Text),
                        promoExpirationTo = DateTimeOffset.Parse(txtpromoExpirationTo.Text),
                        promoAppliedToUnitSizes = selectedUnitSizes
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
            catch (Exception ex)
            {
                Response.Write("<pre>" + ex.ToString() + "</pre>");
            }
        }


    }
}