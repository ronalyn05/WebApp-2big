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
using System.Diagnostics;

namespace WRS2big_Web.superAdmin
{
    public partial class packageDetails : System.Web.UI.Page
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

            FirebaseResponse response = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PACKAGES/");

            if (response.Body != null)
            {
                DisplayDetails();
                loadModalDetails();
            }
            else
            {
                Response.Write("<script>alert ('You haven't created a subscription plan yet');window.location.href = '/superAdmin/ManageSubscription.aspx'; </script>");
            }
           
        }
        private void loadModalDetails()
        {
            if (Session["currentPackage"] != null)
            {
                int package = (int)Session["currentPackage"];

                FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + package);
                Model.PackagePlans packageDets = response.ResultAs<Model.PackagePlans>();

                //updatepackageName.Text = packageDets.packageName;
                updatepackageName.Attributes["placeholder"] = packageDets.packageName;

                //updatePackageDes.Text = packageDets.packageDescription;
                updatePackageDes.Attributes["placeholder"] = packageDets.packageDescription;

                //updatePackagePrice.Text = packageDets.packagePrice.ToString();
                updatePackagePrice.Attributes["placeholder"] = packageDets.packagePrice.ToString();

                //updateProductLimit.Text = packageDets.packageLimit.ToString();
                updateProductLimit.Attributes["placeholder"] = packageDets.packageLimit.ToString();

                //updateProductLimit.Text = packageDets.productLimit.ToString();
                updateProductLimit.Attributes["placeholder"] = packageDets.productLimit.ToString();

                //updateOrderLimit.Text = packageDets.packageLimit.ToString();
                updateOrderLimit.Attributes["placeholder"] = packageDets.packageLimit.ToString();

                //updateDuration.Text = packageDets.packageDuration.ToString();
                updateDuration.Attributes["placeholder"] = packageDets.packageDuration.ToString();

                //updateManagebleStation.Text = packageDets.numberOfStations.ToString();
                updateManagebleStation.Attributes["placeholder"] = packageDets.numberOfStations.ToString();

                updateRenewable.SelectedValue = packageDets.renewable;
                updateDurationType.SelectedValue = packageDets.durationType;

                // Retrieve the data from the database
                response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + package + "/features");
                List<string> packageFeatures = response.ResultAs<List<string>>();

                // Bind the features to the ListBox control
                existingFeatures.DataSource = packageFeatures;
                existingFeatures.DataBind();
            }
            else
            {
                Response.Write("<script>alert ('Session Expired! Please login again');window.location.href = '/superAdmin/SuperAdminAccount.aspx'; </script>");
            }

        }
        private void DisplayDetails()
        {
            if (Session["currentPackage"] != null)
            {
                int package = (int)Session["currentPackage"];

                FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + package);
                Model.PackagePlans packageDets = response.ResultAs<Model.PackagePlans>();


                packageID.Text = packageDets.packageID.ToString();
                packageName.Text = packageDets.packageName;
                packageDescription.Text = packageDets.packageDescription;
                packagePrice.Text = packageDets.packagePrice.ToString();
                packageDuration.Text = packageDets.packageDuration.ToString() + " " + packageDets.durationType;
                manageableStation.Text = packageDets.numberOfStations.ToString();
                packageLimit.Text = packageDets.packageLimit.ToString();
                productLimit.Text = packageDets.productLimit.ToString();
                renewable.Text = packageDets.renewable;
                status.Text = packageDets.status;

                FirebaseResponse featuresponse = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageDets.packageID + "/features");
                List<string> listOfFeatures = featuresponse.ResultAs<List<string>>();

                if (packageDets.status == "Active")
                {
                    restorePackage.Visible = false;

                }
                if (packageDets.status == "Archived")
                {
                    restorePackage.Visible = true;
                    archivePackage.Visible = false;
                }
                // Bind the features to the ListBox control
                featuresList.DataSource = listOfFeatures;
                featuresList.DataBind();
            }

        }

        protected void restorePackage_Click(object sender, EventArgs e)
        {

           if (Session["currentPackage"] != null)
            {
                int packageID = (int)Session["currentPackage"];

                // Retrieve the existing order object from the database
                FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID);
                Model.PackagePlans packageDetails = response.ResultAs<Model.PackagePlans>();

                packageDetails.status = "Active";
                response = twoBigDB.Update("SUBSCRIPTION_PACKAGES/" + packageID, packageDetails);

                //SAVE LOGS TO SUPER ADMIN
                //Get the current date and time
                DateTime logTime = DateTime.Now;

                //generate a random number for users logged
                Random rnd = new Random();
                int logID = rnd.Next(1, 10000);

                var idno = (string)Session["SuperIDno"];
                string superName = (string)Session["superAdminName"];

                //Store the login information in the USERLOG table
                var log = new Model.superLogs
                {
                    logsId = logID,
                    superID = int.Parse(idno),
                    superFullname = superName,
                    superActivity = "RESTORED PACKAGE:" + " " + packageDetails.packageName,
                    activityTime = logTime
                };

                //Storing the  info
                response = twoBigDB.Set("SUPERADMIN_LOGS/" + log.logsId, log);//Storing data to the database
                Model.superLogs res = response.ResultAs<Model.superLogs>();//Database Result

                Debug.WriteLine($"RESTORE LOG ID: {logID}");
                Response.Write("<script>alert ('Subscription Package restored');window.location.href = '/superAdmin/packageDetails.aspx'; </script>");
            }
                
        }

        //protected void archivePackage_Click(object sender, EventArgs e)
        //{
        //    int packageID = (int)Session["currentPackage"];

        //    FirebaseResponse subscribed = twoBigDB.Get("SUBSCRIBED_CLIENTS/");

        //    if (subscribed != null)
        //    {
        //        Dictionary<string, Model.superAdminClients> all = subscribed.ResultAs<Dictionary<string, Model.superAdminClients>>();

        //        foreach (var clients in all)
        //        {
        //            if (clients.Value.currentSubStatus == "Active")
        //            {
        //                if(clients.Value.plan == packageID.ToString())
        //                {
        //                    Response.Write("<script>alert ('Warning! A client is currently subscribed to this package. Archiving this package is not possible at the moment.');window.location.href = '/superAdmin/packageDetails.aspx'; </script>");
        //                }
        //                else
        //                {
        //                    // Retrieve the existing order object from the database
        //                    FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID);
        //                    Model.PackagePlans packageDetails = response.ResultAs<Model.PackagePlans>();

        //                    packageDetails.status = "Archived";
        //                    response = twoBigDB.Update("SUBSCRIPTION_PACKAGES/" + packageID, packageDetails);
        //                    Response.Write("<script>alert (' Subscription Package archived');window.location.href = '/superAdmin/packageDetails.aspx'; </script>");
        //                }
        //            }
        //        }
        //    }


        //}
        protected void archivePackage_Click(object sender, EventArgs e)
        {
            if (Session["currentPackage"] != null)
            {
                int packageID = (int)Session["currentPackage"];

                FirebaseResponse currentPackage = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID);
                Model.PackagePlans current = currentPackage.ResultAs<Model.PackagePlans>();

                FirebaseResponse subscribed = twoBigDB.Get("SUBSCRIBED_CLIENTS/");

                if (subscribed != null)
                {
                    Dictionary<string, Model.superAdminClients> all = subscribed.ResultAs<Dictionary<string, Model.superAdminClients>>();

                    bool isClientSubscribed = false; // Flag variable to track if a subscribed client is found

                    foreach (var client in all)
                    {
                        if (client.Value.currentSubStatus == "Active" && client.Value.plan == current.packageName)
                        {
                            isClientSubscribed = true;
                            break; // Exit the loop since a subscribed client is found
                        }
                    }

                    if (isClientSubscribed)
                    {
                        Response.Write("<script>alert ('Warning! A client is currently subscribed to this package. Archiving this package is not possible at the moment.');window.location.href = '/superAdmin/packageDetails.aspx'; </script>");
                    }
                    else
                    {
                        // Retrieve the existing order object from the database
                        FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID);
                        Model.PackagePlans packageDetails = response.ResultAs<Model.PackagePlans>();

                        packageDetails.status = "Archived";
                        response = twoBigDB.Update("SUBSCRIPTION_PACKAGES/" + packageID, packageDetails);

                        //SAVE LOGS TO SUPER ADMIN
                        //Get the current date and time
                        DateTime logTime = DateTime.Now;

                        //generate a random number for users logged
                        Random rnd = new Random();
                        int logID = rnd.Next(1, 10000);

                        var idno = (string)Session["SuperIDno"];
                        string superName = (string)Session["superAdminName"];

                        //Store the login information in the USERLOG table
                        var log = new Model.superLogs
                        {
                            logsId = logID,
                            superID = int.Parse(idno),
                            superFullname = superName,
                            superActivity = "ARCHIVED PACKAGE:" + " " + packageDetails.packageName,
                            activityTime = logTime
                        };

                        //Storing the  info
                        response = twoBigDB.Set("SUPERADMIN_LOGS/" + log.logsId, log);//Storing data to the database
                        Model.superLogs res = response.ResultAs<Model.superLogs>();//Database Result

                        Debug.WriteLine($"ARCHIVE LOG ID: {logID}");
                        Response.Write("<script>alert ('Subscription Package archived');window.location.href = '/superAdmin/packageDetails.aspx'; </script>");
                    }
                }
            }

        }

        protected void updatePackagebtn_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["currentPackage"] != null)
                {
                    int package = (int)Session["currentPackage"];

                    if (Session["currentPackage"] == null)
                    {
                        Response.Write("<script>alert ('Session Expired! Please login again');window.location.href = '/superAdmin/SuperAdminAccount.aspx'; </script>");
                    }
                    var response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + package);
                    Model.PackagePlans packageDets = response.ResultAs<Model.PackagePlans>();

                    var data = new Model.PackagePlans();
                    data.packageID = packageDets.packageID;
                    data.packageName = packageDets.packageName;
                    data.packageDescription = packageDets.packageDescription;
                    data.packageDuration = packageDets.packageDuration;
                    data.packageLimit = packageDets.packageLimit;
                    data.packagePrice = packageDets.packagePrice;
                    data.numberOfStations = packageDets.numberOfStations;
                    data.productLimit = packageDets.productLimit;
                    data.durationType = packageDets.durationType;
                    data.renewable = packageDets.renewable;

                    List<string> features = new List<string>();

                    // Check if the checkbox list has a selected value
                    if (updatefeaturesCheckbox != null && !string.IsNullOrEmpty(Request.Form[updatefeaturesCheckbox.UniqueID]?.ToString()))
                    {
                        // Retrieve the newly selected items from the checkbox list
                        features = updatefeaturesCheckbox.Items.Cast<ListItem>()
                            .Where(li => li.Selected)
                            .Select(li => li.Value)
                            .ToList();
                    }
                    else
                    {
                        // No new features selected, keep the existing features
                        features = packageDets.features;
                    }

                    // Update the features with the combined list of previously selected items and newly selected items
                    data.features = features;
                    data.status = packageDets.status;
                    data.dateUpdated = DateTime.Now;

                    // Update the fields that have changed
                    if (!string.IsNullOrEmpty(updatepackageName.Text))
                    {
                        data.packageName = updatepackageName.Text;
                    }
                    if (!string.IsNullOrEmpty(updatePackageDes.Text))
                    {
                        data.packageDescription = updatePackageDes.Text;
                    }
                    if (!string.IsNullOrEmpty(updatePackagePrice.Text))
                    {
                        data.packagePrice = int.Parse(updatePackagePrice.Text);
                    }
                    if (!string.IsNullOrEmpty(updateDuration.Text))
                    {
                        data.packageDuration = int.Parse(updateDuration.Text);
                    }
                    if (!string.IsNullOrEmpty(updateOrderLimit.Text))
                    {
                        data.packageLimit = int.Parse(updateOrderLimit.Text);
                    }
                    if (!string.IsNullOrEmpty(updateProductLimit.Text))
                    {
                        data.productLimit = int.Parse(updateProductLimit.Text);
                    }
                    if (!string.IsNullOrEmpty(updateManagebleStation.Text))
                    {
                        data.numberOfStations = int.Parse(updateManagebleStation.Text);
                    }

                    if (!string.IsNullOrEmpty(Request.Form[updateDurationType.UniqueID].ToString()))
                    {
                        data.durationType = Request.Form[updateDurationType.UniqueID].ToString();
                    }
                    if (!string.IsNullOrEmpty(Request.Form[updateRenewable.UniqueID].ToString()))
                    {
                        data.renewable = Request.Form[updateRenewable.UniqueID].ToString();
                    }



                    FirebaseResponse updateRes = twoBigDB.Update("SUBSCRIPTION_PACKAGES/" + package, data);

                    //SAVE LOGS TO SUPER ADMIN
                    //Get the current date and time
                    DateTime logTime = DateTime.Now;

                    //generate a random number for users logged
                    Random rnd = new Random();
                    int logID = rnd.Next(1, 10000);

                    var idno = (string)Session["SuperIDno"];
                    string superName = (string)Session["superAdminName"];

                    //Store the login information in the USERLOG table
                    var log = new Model.superLogs
                    {
                        logsId = logID,
                        superID = int.Parse(idno),
                        superFullname = superName,
                        superActivity = "UPDATED PACKAGE DETAILS: " + " " + data.packageName,
                        activityTime = logTime
                    };

                    //Storing the  info
                    response = twoBigDB.Set("SUPERADMIN_LOGS/" + log.logsId, log);//Storing data to the database
                    Model.superLogs res = response.ResultAs<Model.superLogs>();//Database Result

                    Debug.WriteLine($"UPDATE PACKAGE LOG ID: {logID}");

                    Response.Write("<script>alert ('Package details successfully updated!');window.location.href = '/superAdmin/packageDetails.aspx';</script>");
                }
                
            }
            catch
            {
                Response.Write("<script>alert('Subscription Name already exist'); window.location.href = '/superAdmin/ManageSubscription.aspx'; </script>");
            }
        }



     
    }
}