using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using WRS2big_Web.Model;

namespace WRS2big_Web.Admin
{
    public partial class SubscriptionPackages : System.Web.UI.Page
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"

        };

        IFirebaseClient twoBigDB;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Connection to database
            twoBigDB = new FireSharp.FirebaseClient(config);

            loadFeatures();
        }
        private void loadFeatures()
        {
            FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES");
            var json = response.Body;
            Dictionary<string, Model.PackagePlans> allPackages = JsonConvert.DeserializeObject<Dictionary<string, Model.PackagePlans>>(json);

            if (allPackages != null)
            {
                foreach (KeyValuePair<string, Model.PackagePlans> entry in allPackages)
                {
                    int packageID = entry.Value.packageID;

                    // Find the package container based on its ID
                    string packageContainerID = "packagesPanel" + packageID.ToString();
                    Control packageContainer = FindControl(packageContainerID);

                    if (packageContainer != null)
                    {
                        // Find the controls inside the package container
                        Label packageNameLabel = packageContainer.FindControl("packageAName") as Label;
                        Label packageDescriptionLabel = packageContainer.FindControl("packageAdescription") as Label;
                        Label packagePriceLabel = packageContainer.FindControl("packageAPrice") as Label;
                        Label durationLabel = packageContainer.FindControl("durationA") as Label;
                        Repeater featuresRepeater = packageContainer.FindControl("featuresPackageA") as Repeater;

                        // Set the package details
                        packageNameLabel.Text = entry.Value.packageName;
                        packageDescriptionLabel.Text = entry.Value.packageDescription;
                        packagePriceLabel.Text = entry.Value.packagePrice.ToString();
                        durationLabel.Text = "for " + entry.Value.packageDuration + " " + entry.Value.durationType;

                        // Set styles for the controls
                        packageNameLabel.CssClass = "mb-2 h5-mktg";
                        packageDescriptionLabel.CssClass = "lh-condensed";
                        packagePriceLabel.CssClass = "js-computed-value";
                        durationLabel.CssClass = "text-normal f4 color-fg-muted js-pricing-cost-suffix js-monthly-suffix";

                        // Retrieve the package features from Firebase
                        FirebaseResponse featuresResponse = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID + "/features");
                        var featuresJson = featuresResponse.Body;
                        List<string> featuresList = JsonConvert.DeserializeObject<List<string>>(featuresJson);

                        // Bind featuresList to the repeater control
                        featuresRepeater.DataSource = featuresList;
                        featuresRepeater.DataBind();
                    }
                }
            }
        }



        //private void loadFeatures()
        //{
        //    FirebaseResponse features = twoBigDB.Get("SUBSCRIPTION_PACKAGES");
        //    var list = features.Body;
        //    Dictionary<string, Model.PackagePlans> allFeatures = JsonConvert.DeserializeObject<Dictionary<string, Model.PackagePlans>>(list);

        //    if (allFeatures != null)
        //    {
        //        foreach (KeyValuePair<string, Model.PackagePlans> entry in allFeatures)
        //        {
        //            if (entry.Value.packageName == "Package")
        //            {
        //                int packageID = entry.Value.packageID;

        //                packageAName.Text = entry.Value.packageName;
        //                packageAdescription.Text = entry.Value.packageDescription;
        //                packageAPrice.Text = entry.Value.packagePrice.ToString();
        //                durationA.Text = "for" + " " + entry.Value.packageDuration + " " + entry.Value.durationType + " " + "only";


        //                features = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID + "/features");
        //                var featuresList = JsonConvert.DeserializeObject<List<string>>(features.Body);

        //                // Bind featuresList to your repeater control
        //                featuresPackageA.DataSource = featuresList;
        //                featuresPackageA.DataBind();
        //            }
        //            else
        //            {
        //                int packageID = entry.Value.packageID;

        //                packageAName.Text = entry.Value.packageName;
        //                packageAdescription.Text = entry.Value.packageDescription;
        //                packageAPrice.Text = entry.Value.packagePrice.ToString();
        //                durationA.Text = "for" + " " + entry.Value.packageDuration + " " + entry.Value.durationType + " " + "only";


        //                features = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID + "/features");
        //                var featuresList = JsonConvert.DeserializeObject<List<string>>(features.Body);

        //                // Bind featuresList to your repeater control
        //                featuresPackageA.DataSource = featuresList;
        //                featuresPackageA.DataBind();
        //            }
        //            //if (entry.Value.packageName == "Package C")
        //            //{
        //            //    int packageID = entry.Value.packageID;
        //            //    double price = entry.Value.packagePrice;


        //            //    packageCName.Text = entry.Value.packageName;
        //            //    packageCdescription.Text = entry.Value.packageDescription;
        //            //    packageCPrice.Text = price.ToString();
        //            //    durationC.Text = "for" + " " + entry.Value.packageDuration + " " + entry.Value.durationType;
        //            //    //manageStations.Text = entry.Value.numberOfStations + " " + "Refilling Stations";

        //            //    features = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PACKAGES/" + packageID + "/features");
        //            //    var featuresList = JsonConvert.DeserializeObject<List<string>>(features.Body);

        //            //    // Bind featuresList to your repeater control
        //            //    featuresPackageC.DataSource = featuresList;
        //            //    featuresPackageC.DataBind();
        //            //}
        //            if (entry.Value.packageName == "Package")
        //            {
        //                int packageID = entry.Value.packageID;

        //                packageBName.Text = entry.Value.packageName;
        //                packageBdescription.Text = entry.Value.packageDescription;
        //                packageBPrice.Text = entry.Value.packagePrice.ToString();
        //                durationB.Text = "for" + " " + entry.Value.packageDuration + " " + entry.Value.durationType;

        //                features = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID + "/features");
        //                var featuresList = JsonConvert.DeserializeObject<List<string>>(features.Body);

        //                // Bind featuresList to your repeater control
        //                featuresPackageB.DataSource = featuresList;
        //                featuresPackageB.DataBind();
        //            }
        //            else
        //            {
        //                int packageID = entry.Value.packageID;

        //                packageBName.Text = entry.Value.packageName;
        //                packageBdescription.Text = entry.Value.packageDescription;
        //                packageBPrice.Text = entry.Value.packagePrice.ToString();
        //                durationB.Text = "for" + " " + entry.Value.packageDuration + " " + entry.Value.durationType;

        //                features = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID + "/features");
        //                var featuresList = JsonConvert.DeserializeObject<List<string>>(features.Body);

        //                // Bind featuresList to your repeater control
        //                featuresPackageB.DataSource = featuresList;
        //                featuresPackageB.DataBind();
        //            }
        //        }

        //    }
        //}
    }
}