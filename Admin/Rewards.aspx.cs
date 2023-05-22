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

            var responseOther = twoBigDB.Get("thirdparty_PRODUCTS");
            Dictionary<string, thirdpartyProducts> otherProducts = responseOther.ResultAs<Dictionary<string, thirdpartyProducts>>();
            var filteredOtherList = otherProducts.Values.Where(p => p.adminId.ToString() == currentUserId);

            // Add the unit and sizes data of refill products to the CheckBoxList
            List<string> refillUnitSizesList = new List<string>();
            foreach (var product in filteredRefillList)
            {
                string unitofVolume = product.pro_refillUnitVolume;
                string qty = product.pro_refillQty;
                if (!string.IsNullOrEmpty(unitofVolume) && !string.IsNullOrEmpty(qty))
                {
                    string unitSizes = qty + "  " + unitofVolume;
                    if (!refillUnitSizesList.Contains(unitSizes))
                    {
                        refillUnitSizesList.Add(unitSizes);
                    }
                }
            }

            foreach (string unitSizes in refillUnitSizesList)
            {
                chUnitSizes_proRefill.Items.Add(new ListItem(unitSizes));
            }

            // Add the unit and sizes data of other products to the CheckBoxList
            List<string> otherUnitSizesList = new List<string>();
            foreach (var product in filteredOtherList)
            {
                string unitofVolume = product.thirdparty_productUnitVolume;
                string qty = product.thirdparty_productQty;
                if (!string.IsNullOrEmpty(unitofVolume) && !string.IsNullOrEmpty(qty))
                {
                    string unitSizes = qty + "  " + unitofVolume;
                    if (!otherUnitSizesList.Contains(unitSizes))
                    {
                        otherUnitSizesList.Add(unitSizes);
                    }
                }
            }

            foreach (string unitSizes in otherUnitSizesList)
            {
                chUnitSizes_otherProduct.Items.Add(new ListItem(unitSizes));
            }
        }

        //RETRIEVE REPORTS
        private void rewardReportsDisplay()
        {

            string idno = (string)Session["idno"];

            // Get the log ID from the session
            int logsId = (int)Session["logsId"];

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("REWARDSYSTEM/");
            Dictionary<string, RewardSystem> userReward = response.ResultAs<Dictionary<string, RewardSystem>>();
            //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
          

            // Create the DataTable to hold the orders
            DataTable rewardTable = new DataTable();
            rewardTable.Columns.Add("REWARD ID");
            rewardTable.Columns.Add("WAYS TO EARN REWARD POINTS");
            rewardTable.Columns.Add("REWARD POINTS TO EARN");
            rewardTable.Columns.Add("MINIMUM RANGE PER AMOUNT");
            rewardTable.Columns.Add("MAXIMUM RANGE PER AMOUNT");
            rewardTable.Columns.Add("DATE ADDED");
            rewardTable.Columns.Add("ADDED BY");

            //condition to fetch the product refill data
            if (response != null && response.ResultAs<RewardSystem>() != null)
            {
                var filteredList = userReward.Values.Where(d => d.adminId.ToString() == idno).OrderByDescending(d => d.rewardsDateAdded);
                //var filteredList = productsList.Values.Where(d => d.adminId.ToString() == idno && (d.pro_refillId.ToString() == productnum));
                //   var filteredList = rewardList.Values.Where(d => d.adminId.ToString() == idno);

                // Loop through the entries and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    
                        string dateAdded = entry.rewardsDateAdded == DateTime.MinValue ? "" : entry.rewardsDateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        //string dateUpdated = entry.dateUpdated == DateTimeOffset.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        rewardTable.Rows.Add(entry.rewardId, entry.rewardWaysToEarn, entry.rewardPointsToEarn,
                            entry.reward_minRange_perAmount, entry.reward_maxRange_perAmount, dateAdded, entry.addedBy);
                    
                }

            }
            else
            {
                // Handle null response or invalid selected value
                lblMessage.Text = "No data found ";
            }



            // Bind the DataTable to the GridView
            gridRewardReport.DataSource = rewardTable;
            gridRewardReport.DataBind();

        }
        //DISPLAY PROMO OFFERED REPORTS
        private void promoOfferedReportsDisplay()
        {

            string idno = (string)Session["idno"];

            // Get the log ID from the session
            int logsId = (int)Session["logsId"];

            // Retrieve all records from the PROMO_OFFERED table
            FirebaseResponse response = twoBigDB.Get("DISCOUNTCOUPON/");
            Dictionary<string, DiscounCoupon> userReward = response.ResultAs<Dictionary<string, DiscounCoupon>>();
            //var filteredList = userlog.Values.Where(d => d.userIdnum.ToString() == idno);
            

            // Create the DataTable to hold the orders
            DataTable promoOfferedTable = new DataTable();
            promoOfferedTable.Columns.Add("COUPON ID");
            promoOfferedTable.Columns.Add("COUPON NAME");
            promoOfferedTable.Columns.Add("COUPON DESCRIPTION");
            promoOfferedTable.Columns.Add("COUPON APPLIED TO PRODUCT OFFERS");
            promoOfferedTable.Columns.Add("COUPON APPLIED TO PRODUCT REFILL");
            promoOfferedTable.Columns.Add("COUPON APPLIED TO THIRD PARTY PRODUCT");
            promoOfferedTable.Columns.Add("COUPON EXPIRATION FROM");
            promoOfferedTable.Columns.Add("COUPON EXPIRATION TO");
            promoOfferedTable.Columns.Add("DATE ADDED");
            promoOfferedTable.Columns.Add("ADDED BY");

           
            //condition to fetch the other product data
            if (response != null && response.ResultAs<DiscounCoupon>() != null)
            {
                //  var filteredList = promolist.Values.Where(d => d.adminId.ToString() == idno);

                var filteredList = userReward.Values.Where(d => d.adminId.ToString() == idno).OrderByDescending(d => d.couponDateAdded);

                // Loop through the entries and add them to the DataTable
                foreach (var entry in filteredList)
                {

                    string dateAdded = entry.couponDateAdded == DateTime.MinValue ? "" : entry.couponDateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string promoValidFrom = entry.couponExpirationFrom == DateTime.MinValue ? "" : entry.couponExpirationFrom.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string promoValidUntil = entry.couponExpirationTo == DateTime.MinValue ? "" : entry.couponExpirationTo.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    //string dateUpdated = entry.dateUpdated == DateTimeOffset.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    promoOfferedTable.Rows.Add(entry.couponId, entry.couponName, entry.couponDescription, entry.couponAppliedToProductOffers,
                             entry.couponAppliedTo_productRefillUnitSizes, entry.couponAppliedTo_thirdpartyProductUnitSizes, promoValidFrom,
                             promoValidUntil, dateAdded, entry.addedBy);
                    
                }
            }
            else
            {
                // Handle null response or invalid selected value
                lblMessage.Text = "No data found ";
            }
            
            
            // Bind the DataTable to the GridView
            gridPromoReports.DataSource = promoOfferedTable;
            gridPromoReports.DataBind();

        }
       // STORE PROMO OFFERED
        protected void btnAddPromoOffered_Click(object sender, EventArgs e)
        {

            string idno = (string)Session["idno"];
            int logsId = (int)Session["logsId"];
            string name = (string)Session["fullname"];

            try
            {

                // Generate reward id number to make as the unique key
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                
                
                // Validate user input
                if (string.IsNullOrEmpty(txtpromoname.Text) || string.IsNullOrEmpty(txtpromodescription.Text) || string.IsNullOrEmpty(txtpromo_pointsToClaimReward.Text))
                {
                    Response.Write("<script> alert('Please fill all the required fields.'); </script>");
                    return;
                }

                // Convert discount value to an integer
                int discountValue = 0;
                if (!int.TryParse(txtpromoDiscountValue.Text, out discountValue))
                {
                    Response.Write("<script> alert('Invalid discount required value. Please enter a valid number.'); </script>");
                    return;
                }

                // Convert points required to an integer
                int pointsRequired = 0;
                if (!int.TryParse(txtpromo_pointsToClaimReward.Text, out pointsRequired))
                {
                    Response.Write("<script> alert('Invalid points required value. Please enter a valid number.'); </script>");
                    return;
                }

                // Get the current UTC date and time as a DateTimeOffset object
                DateTime currentDateTime = DateTime.Now;


                // Define the format of the date string
                string format = "MMMM, dd yyyy";

                // Convert the date string to a DateTimeOffset using ParseExact
                DateTime promoDateAdded = DateTime.ParseExact(currentDateTime.ToString(format), format, CultureInfo.InvariantCulture);

                // Get the selected values from the CheckBoxList
                string selectedPromo_productOffered = "";
                foreach (ListItem item in checkPromo_productOffered.Items)
                {
                    if (item.Selected)
                    {
                        selectedPromo_productOffered += item.Value + ",";
                    }
                }
                // Remove the trailing comma if there are any selected values
                if (!string.IsNullOrEmpty(selectedPromo_productOffered))
                {
                    selectedPromo_productOffered = selectedPromo_productOffered.TrimEnd(',');
                }

                // Get the selected values from the CheckBoxList
                string selectedUnitSizesproRefill = "";
                foreach (ListItem item in chUnitSizes_proRefill.Items)
                {
                    if (item.Selected)
                    {
                        selectedUnitSizesproRefill += item.Value + ",";

                    }
                }
                // Remove the trailing comma if there are any selected values
                if (!string.IsNullOrEmpty(selectedUnitSizesproRefill))
                {
                    selectedUnitSizesproRefill = selectedUnitSizesproRefill.TrimEnd(',');
                }

                // Get the selected values from the CheckBoxList
                string selectedUnitSizesOtherProduct = "";
                foreach (ListItem item in chUnitSizes_otherProduct.Items)
                {
                    if (item.Selected)
                    {
                        selectedUnitSizesOtherProduct += item.Value + ",";

                    }
                }
                // Remove the trailing comma if there are any selected values
                if (!string.IsNullOrEmpty(selectedUnitSizesOtherProduct))
                {
                    selectedUnitSizesOtherProduct = selectedUnitSizesOtherProduct.TrimEnd(',');
                }

                // Convert points required to an integer
                int percentageVAlue = 0;
                if (!int.TryParse(txtpromoDiscountValue.Text, out percentageVAlue))
                {
                    Response.Write("<script> alert('Invalid percentage required value. Please enter a valid percentage number.'); </script>");
                    return;
                }

                // Add the reward promo to the database
                var data = new DiscounCoupon
                {
                    couponId = idnum,
                    adminId = int.Parse(idno),
                    couponName = txtpromoname.Text,
                    couponDiscountValue = percentageVAlue,
                    couponDescription = txtpromodescription.Text,
                    couponPointsRequiredToClaim = pointsRequired,
                    couponAppliedToProductOffers = selectedPromo_productOffered,
                    couponExpirationFrom = DateTime.Parse(txtpromoExpirationFrom.Text),
                    couponExpirationTo = DateTime.Parse(txtpromoExpirationTo.Text),
                    couponAppliedTo_thirdpartyProductUnitSizes = selectedUnitSizesOtherProduct,
                    couponAppliedTo_productRefillUnitSizes = selectedUnitSizesproRefill,
                    couponDateAdded = promoDateAdded,
                    addedBy = name

                };

                SetResponse response;
                response = twoBigDB.Set("DISCOUNTCOUPON/" + data.couponId, data);


                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                //Store the login information in the USERLOG table
                var promoLog = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    activityTime = addedTime,
                    userActivity = "ADDED DISCOUNT COUPON OFFERED",
                };

                //Storing the  info
                response = twoBigDB.Set("ADMINLOGS/" + promoLog.logsId, promoLog);//Storing data to the database

                txtpromoname.Text = null;
                txtpromoDiscountValue.Text = null;
                txtpromodescription.Text = null;
                txtpromo_pointsToClaimReward.Text = null;
                txtpromoExpirationFrom.Text = null;
                txtpromoExpirationTo.Text = null;
                checkPromo_productOffered.SelectedValue = null;
                chUnitSizes_otherProduct.SelectedValue = null;
                chUnitSizes_proRefill.SelectedValue = null;
                // Show success message
                Response.Write("<script> alert('Promo offered added successfully!') </script>");
                    

            }
            catch (Exception ex)
            {
                Response.Write("<pre>" + ex.ToString() + "</pre>");
            }
        }
        //STORE REWARD
        protected void btnAddReward_Click(object sender, EventArgs e)
        {
            try
            {
                string idno = (string)Session["idno"];
                int logsId = (int)Session["logsId"];
                string name = (string)Session["fullname"];

                // Generate reward id number to make as the unique key
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                // Get the current UTC date and time as a DateTimeOffset object
                DateTime currentDateTime = DateTime.Now;


                // Define the format of the date string
                string format = "MMMM, dd yyyy";

                // Convert the date string to a DateTimeOffset using ParseExact
                DateTime rewardsDateAdded = DateTime.ParseExact(currentDateTime.ToString(format), format, CultureInfo.InvariantCulture);


               
                    

                // Get the selected values from the CheckBoxList
                string waysToEarnPoints_selectedValues = "";
                foreach (ListItem item in radioWaysToEarnPoints.Items)
                {
                    if (item.Selected)
                    {
                        waysToEarnPoints_selectedValues += item.Value + ",";
                    }
                }

                // Remove the trailing comma if there are any selected values
                if (!string.IsNullOrEmpty(waysToEarnPoints_selectedValues))
                {
                    waysToEarnPoints_selectedValues = waysToEarnPoints_selectedValues.TrimEnd(',');
                }
              //  // Convert rewards points to earn to an integer
                //string rewardspoints = " ";
                //if (!string.IsNullOrEmpty(txtrewardspointsPerTxnOrAmount.Text) && !decimal.TryParse(txtrewardspointsPerTxnOrAmount.Text, out rewardspoints))
                //{
                //    Response.Write("<script> alert('Invalid points required value. Please enter a valid number.'); </script>");
                //    return;
                //}
                string rewardspoints = " ";
                if (!string.IsNullOrEmpty(txtrewardspointsPerTxnOrAmount.Text))
                {
                    if (!decimal.TryParse(txtrewardspointsPerTxnOrAmount.Text, out decimal points))
                    {
                        Response.Write("<script> alert('Invalid points required value. Please enter a valid number or decimal.'); </script>");
                        return;
                    }
                    rewardspoints = points.ToString();
                }

                //// If minimum range per amount is null or empty, minRange_perAmount will be null.
                //string minRange_perAmount = " ";
                //if (!string.IsNullOrEmpty(txtminRange_perAmount.Text) && !string(txtminRange_perAmount.Text, out minRange_perAmount))
                //{
                //    Response.Write("<script> alert('Invalid minimum range amount required value. Please enter a valid range amount in number or decimal.'); </script>");
                //    return;
                //}

                //// If maximum range per amount is null or empty, maxRange_perAmount will be null.
                //string maxRange_perAmount = " ";
                //if (!string.IsNullOrEmpty(txtmaxRange_perAmount.Text) && !string(txtmaxRange_perAmount.Text, out maxRange_perAmount))
                //{
                //    Response.Write("<script> alert('Invalid maximum range amount required value. Please enter a valid range amount in number or decimal.'); </script>");
                //    return;
                //}
                // If minimum range per amount is null or empty, minRange_perAmount will be null.
                string minRange_perAmount = " ";
                if (!string.IsNullOrEmpty(txtminRange_perAmount.Text))
                {
                    if (!decimal.TryParse(txtminRange_perAmount.Text, out decimal minRange))
                    {
                        Response.Write("<script> alert('Invalid minimum range amount required value. Please enter a valid range amount in number or decimal.'); </script>");
                        return;
                    }
                    minRange_perAmount = minRange.ToString();
                }

                // If maximum range per amount is null or empty, maxRange_perAmount will be null.
                string maxRange_perAmount = " ";
                if (!string.IsNullOrEmpty(txtmaxRange_perAmount.Text))
                {
                    if (!decimal.TryParse(txtmaxRange_perAmount.Text, out decimal maxRange))
                    {
                        Response.Write("<script> alert('Invalid maximum range amount required value. Please enter a valid range amount in number or decimal.'); </script>");
                        return;
                    }
                    maxRange_perAmount = maxRange.ToString();
                }


                // Add the reward promo to the database
                var data = new RewardSystem
                {
                    rewardId = idnum,
                    adminId = int.Parse(idno),
                    rewardWaysToEarn = waysToEarnPoints_selectedValues,
                    rewardPointsToEarn = rewardspoints,
                    reward_minRange_perAmount = minRange_perAmount,
                    reward_maxRange_perAmount = maxRange_perAmount,
                    rewardsDateAdded = rewardsDateAdded,
                    addedBy = name
                };



                SetResponse response;
                response = twoBigDB.Set("REWARDSYSTEM/" + data.rewardId, data);


                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                //Store the login information in the USERLOG table
                var rewardLog = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    activityTime = addedTime,
                    userActivity = "ADDED REWARD SYSTEM",
                };

                //Storing the  info
                response = twoBigDB.Set("ADMINLOGS/" + rewardLog.logsId, rewardLog);//Storing data to the database

                // Show success message
                Response.Write("<script> alert('Reward added successfully!'); </script>");

                txtrewardspointsPerTxnOrAmount.Text = null;
                txtmaxRange_perAmount.Text = null;
                txtminRange_perAmount.Text = null;
                radioWaysToEarnPoints.SelectedValue = null;



            }
            catch (Exception ex)
            {
                Response.Write("<pre>" + ex.ToString() + "</pre>");
            }
        }

        protected void radioWaysToEarnPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioWaysToEarnPoints.SelectedValue == "per amount")
            {
                txtminRange_perAmount.Enabled = true;
                txtmaxRange_perAmount.Enabled = true;
            }
            else
            {
                txtminRange_perAmount.Enabled = false;
                txtmaxRange_perAmount.Enabled = false;
            }
        }
        //OPTION DISPLAY REPORTS
        protected void btnDisplayReports_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedOption = ddlSearchOptions.SelectedValue;

                if (selectedOption == "0")
                {
                    lblreports.Text = "DISCOUNT COUPON REPORT";
                    lblReward.Text = "REWARD SYSTEM REPORT";
                    gridRewardReport.Visible = true;
                    rewardReportsDisplay();
                    gridPromoReports.Visible = true;
                    promoOfferedReportsDisplay();

                }
                else if (selectedOption == "1")
                {
                    lblreports.Text = "REWARD SYSTEM REPORT";
                    gridRewardReport.Visible = true;
                    gridPromoReports.Visible = false;
                    lblReward.Visible = false;
                    rewardReportsDisplay();

                }
                else if (selectedOption == "2")
                {
                    lblreports.Text = "DISCOUNT COUPON REPORT";
                    gridPromoReports.Visible = true;
                    lblReward.Visible = false;
                    gridRewardReport.Visible = false;
                    promoOfferedReportsDisplay();
                    
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/Rewards.aspx';" + ex.Message);
            }
        }
        //SEARCH PROMO REPORTS OF CERTAIN RECORD
        protected void btnSearchReports_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#viewpromo').modal();", true);

            string idno = (string)Session["idno"];
            string searchname = txtSearch.Text;

            try
            {
               
                // Check if the employee ID is valid
                if (string.IsNullOrEmpty(searchname))
                {
                    Response.Write("<script>alert ('Invalid coupon name!');</script>");
                    return;
                }

                //// Retrieve all orders from the ORDERS table
                //FirebaseResponse response = twoBigDB.Get("REWARDSYSTEM");
                //Dictionary<string, RewardSystem> rewardList = response.ResultAs<Dictionary<string, RewardSystem>>();

                // Retrieve all orders from the ORDERS table
                FirebaseResponse responselist = twoBigDB.Get("DISCOUNTCOUPON");
                Dictionary<string, DiscounCoupon> promolist = responselist.ResultAs<Dictionary<string, DiscounCoupon>>();

                // Create the DataTable to hold the orders
                DataTable promoOfferedTable = new DataTable();
                promoOfferedTable.Columns.Add("COUPON ID");
                promoOfferedTable.Columns.Add("COUPON NAME");
                promoOfferedTable.Columns.Add("COUPON DESCRIPTION");
                promoOfferedTable.Columns.Add("COUPON APPLIED TO PRODUCT OFFERS");
                promoOfferedTable.Columns.Add("COUPON APPLIED TO PRODUCT REFILL");
                promoOfferedTable.Columns.Add("COUPON APPLIED TO THIRD PARTY PRODUCT");
                promoOfferedTable.Columns.Add("COUPON EXPIRATION FROM");
                promoOfferedTable.Columns.Add("COUPON EXPIRATION TO");
                promoOfferedTable.Columns.Add("DATE ADDED");
                promoOfferedTable.Columns.Add("ADDED BY");

                //condition to fetch the other product data
                if (responselist != null && responselist.ResultAs<DiscounCoupon>() != null)
                {
                    var filteredList = promolist.Values.Where(d => d.adminId.ToString() == idno);

                    // Loop through the entries and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        if (searchname == entry.couponName.ToString())
                        {

                            string dateAdded = entry.couponDateAdded == DateTime.MinValue ? "" : entry.couponDateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string promoValidFrom = entry.couponExpirationFrom == DateTime.MinValue ? "" : entry.couponExpirationFrom.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string promoValidUntil = entry.couponExpirationTo == DateTime.MinValue ? "" : entry.couponExpirationTo.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            //string dateUpdated = entry.dateUpdated == DateTimeOffset.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            promoOfferedTable.Rows.Add(entry.couponId, entry.couponName, entry.couponDescription, entry.couponAppliedToProductOffers,
                                     entry.couponAppliedTo_productRefillUnitSizes, entry.couponAppliedTo_thirdpartyProductUnitSizes, promoValidFrom,
                                     promoValidUntil, dateAdded, entry.addedBy);
                        }
                    }
                }
                else
                {
                    //Response.Write("<script>alert('Error retrieving product data.');</script>");
                    lblMessageError.Text = "No data found for " + " " + searchname;
                }

                

                gridPromoOffered.DataSource = promoOfferedTable;
                gridPromoOffered.DataBind();
                // lblProductId.Text = productnum;

                //  Response.Write("<script> location.reload(); window.location.href = '/Admin/WaterOrders.aspx'; </script>");
                txtSearch.Text = null;

            }
            catch (Exception ex)
            {
                Response.Write("<pre>" + ex.ToString() + "</pre>");
            }
        }
        //SEARCH REWARD REPORTS OF CERTAIN RECORD
        protected void btnSearchReward_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#view').modal();", true);

            string idno = (string)Session["idno"];
            string searchname = txtSearchReward.Text;
            try
            {
                

                // Check if the employee ID is valid
                if (string.IsNullOrEmpty(searchname))
                {
                    Response.Write("<script>alert ('Enter per transaction or per amount only!');</script>");
                    return;
                }

                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("REWARDSYSTEM");
                Dictionary<string, RewardSystem> rewardList = response.ResultAs<Dictionary<string, RewardSystem>>();

                // Create the DataTable to hold the orders
                DataTable rewardTable = new DataTable();
                rewardTable.Columns.Add("REWARD ID");
                rewardTable.Columns.Add("WAYS TO EARN REWARD POINTS");
                rewardTable.Columns.Add("REWARD POINTS TO EARN");
                rewardTable.Columns.Add("MINIMUM RANGE PER AMOUNT");
                rewardTable.Columns.Add("MAXIMUM RANGE PER AMOUNT");
                rewardTable.Columns.Add("DATE ADDED");
                rewardTable.Columns.Add("ADDED BY");
                //condition to fetch the product refill data
                if (response != null && response.ResultAs<RewardSystem>() != null)
                {
                    //var filteredList = productsList.Values.Where(d => d.adminId.ToString() == idno && (d.pro_refillId.ToString() == productnum));
                    var filteredList = rewardList.Values.Where(d => d.adminId.ToString() == idno);

                    // Loop through the entries and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        if (searchname == entry.rewardWaysToEarn.ToString())
                        {

                            string dateAdded = entry.rewardsDateAdded == DateTime.MinValue ? "" : entry.rewardsDateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            //string dateUpdated = entry.dateUpdated == DateTimeOffset.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            rewardTable.Rows.Add(entry.rewardId, entry.rewardWaysToEarn, entry.rewardPointsToEarn,
                                entry.reward_minRange_perAmount, entry.reward_maxRange_perAmount, dateAdded, entry.addedBy);
                        }
                    }

                }
                else
                {
                    //Response.Write("<script>alert('Error retrieving product data.');</script>");
                    lblMessageError.Text = "No data found for " + " " + searchname;
                }
               
                // Bind the DataTable to the GridView
                gridReward.DataSource = rewardTable;
                gridReward.DataBind();

                txtSearchReward.Text = null;

            }
            catch (Exception ex)
            {
                Response.Write("<pre>" + ex.ToString() + "</pre>");
            }
        }

    }
}