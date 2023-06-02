using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
            FirebaseResponse featuresResponse = twoBigDB.Get("SUBSCRIPTION_PACKAGES");
            var featuresData = featuresResponse.Body;
            
            Dictionary<string, Model.PackagePlans> allFeatures = JsonConvert.DeserializeObject<Dictionary<string, Model.PackagePlans>>(featuresData);

            if (allFeatures != null)
            {
               
                string idno = (string)Session["idno"];

                // Retrieve the existing product data from the database
                var result = twoBigDB.Get("ADMIN/" + idno + "/Subscribed_Package");
                Subscribed_Package currentPackage = result.ResultAs<Subscribed_Package>();

                int subPackage = currentPackage.packageID;

                //List<Model.PackagePlans> packageList = allFeatures.Values.Where(p => p.status == "Active").ToList();
                List<Model.PackagePlans> packageList = allFeatures.Values
                 .Where(p => p.status == "Active" && (p.renewable == "Yes" || (p.renewable == "No" && p.packageID != subPackage)))
                 .ToList();
                    
                packagesRepeater.DataSource = packageList;
                packagesRepeater.DataBind();

            }
        }

        protected void packagesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Model.PackagePlans package = (Model.PackagePlans)e.Item.DataItem;

                    // Find the PayPal container control within the repeater item
                    HtmlGenericControl paypalContainer = (HtmlGenericControl)e.Item.FindControl("paypalContainer");

                    // CREATE PAYPAL BUTTONS ON ALL PACKAGES
                    var paypalButton = new StringBuilder();
                    paypalButton.AppendLine("<script>");
                    paypalButton.AppendLine("paypal.Buttons({");
                    paypalButton.AppendLine("    createOrder: function(data, actions) {");
                    paypalButton.AppendLine("        return actions.order.create({");
                    paypalButton.AppendLine("            payee: {");
                    paypalButton.AppendLine("                name: {");
                    paypalButton.AppendLine("                    given_name: '" + Session["fname"] + "',");
                    paypalButton.AppendLine("                    surname: '" + Session["lname"] + "'");
                    paypalButton.AppendLine("                },");
                    paypalButton.AppendLine("                phone: {");
                    paypalButton.AppendLine("                    phone_type: 'MOBILE',");
                    paypalButton.AppendLine("                    phone_num: '" + Session["contactNumber"] + "'");
                    paypalButton.AppendLine("                },");
                    paypalButton.AppendLine("                email: '" + Session["Email"] + "'");
                    paypalButton.AppendLine("            },");
                    paypalButton.AppendLine("            purchase_units: [{");
                    paypalButton.AppendLine("                amount: {");
                    paypalButton.AppendLine("                    value: " + package.packagePrice);
                    paypalButton.AppendLine("                }");
                    paypalButton.AppendLine("            }]");
                    paypalButton.AppendLine("        });");
                    paypalButton.AppendLine("    },");
                    paypalButton.AppendLine("    onApprove: function(data, actions) {");
                    paypalButton.AppendLine("        return actions.order.capture().then(function(details) {");
                    paypalButton.AppendLine("            console.log(details);");
                    paypalButton.AppendLine("            var packageID = '" + package.packageID + "';");
                    paypalButton.AppendLine("            window.location.replace('SubscriptionSuccess.aspx?packageID=' + packageID);");
                    paypalButton.AppendLine("        });");
                    paypalButton.AppendLine("    },");
                    paypalButton.AppendLine("    onCancel: function(data) {");
                    paypalButton.AppendLine("        window.location.replace('SubscriptionPackages.aspx');");
                    paypalButton.AppendLine("    }");
                    paypalButton.AppendLine("}).render('#" + paypalContainer.ClientID + "');");
                    paypalButton.AppendLine("</script>");

                    // Add the PayPal button script to the PayPal container control
                    paypalContainer.Controls.Add(new LiteralControl(paypalButton.ToString()));

                    //FETCH PACKAGE DETAILS FROM THE DATABASE
                    Label packageNameLabel = (Label)e.Item.FindControl("packageName");
                    Label packageDescriptionLabel = (Label)e.Item.FindControl("packageDescription");
                    Label packagePriceLabel = (Label)e.Item.FindControl("packagePrice");
                    Label packageDurationLabel = (Label)e.Item.FindControl("packageDuration");
                    Repeater featuresRepeater = (Repeater)e.Item.FindControl("featuresRepeater");
                    Label packageProducts = (Label)e.Item.FindControl("unliProducts");


                    

                packageNameLabel.Text = package.packageName;
                    packageDescriptionLabel.Text = package.packageDescription;
                    packagePriceLabel.Text = package.packagePrice.ToString();
                    packageDurationLabel.Text = "for " + package.packageDuration + " " + package.durationType + "/s";


                    // Set styles for the controls
                    packageNameLabel.Style["font-size"] = "24px";
                    packageDescriptionLabel.Style["color"] = "black";
                    packagePriceLabel.CssClass = "js-computed-value";
                    packageDurationLabel.Style["font-size"] = "16px";
                    packageProducts.Style["color"] = "black";

                    FirebaseResponse featuresResponse = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + package.packageID + "/features");
                    var featuresData = featuresResponse.Body;
                    List<string> featuresList = JsonConvert.DeserializeObject<List<string>>(featuresData);

                    featuresRepeater.DataSource = featuresList;
                    featuresRepeater.DataBind();



                //FETCH DETAILS FOR EXCLUSIVE PACKAGE DETAILS
                //TO DISPLAY THE RENEWAL DETAILS
                if (package.renewable == "No")
                    {
                        Label renewable = (Label)e.Item.FindControl("renewable");
                        renewable.Text = "No Renewal";
                        renewable.Style["color"] = "black";
                    }
                    else
                    {
                        Label renewable = (Label)e.Item.FindControl("renewable");
                        renewable.Text = "Monthly Renewal";
                        renewable.Style["color"] = "black";
                    }
                    //TO DISPLAY ORDER LIMITS
                    if (package.packageLimit != 0)
                    {
                        Label limit = (Label)e.Item.FindControl("orderLimit");
                        limit.Text = "Orders Transaction Limit:" + " " + package.packageLimit;
                        limit.Style["color"] = "black";
                    }
                    else
                    {
                        Label limit = (Label)e.Item.FindControl("orderLimit");
                        limit.Text = "Unlimited Transactions";
                        limit.Style["color"] = "black";
                    }
                    //TO DISPLAY PRODUCT LIMITS
                    if (package.productLimit != 0)
                    {
                        Label prodLimit = (Label)e.Item.FindControl("unliProducts");
                        prodLimit.Text = "Product Limit:" + " " + package.productLimit;
                        prodLimit.Style["color"] = "black";
                    }
                    else
                    {
                        Label prodLimit = (Label)e.Item.FindControl("unliProducts");
                        prodLimit.Text = "Unlimited Products";
                        prodLimit.Style["color"] = "black";
                    }

               

            }
        }


        //private void loadFeatures()
        //{
        //    FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES");
        //    var json = response.Body;
        //    Dictionary<string, Model.PackagePlans> allPackages = JsonConvert.DeserializeObject<Dictionary<string, Model.PackagePlans>>(json);

        //    if (allPackages != null)
        //    {
        //        foreach (KeyValuePair<string, Model.PackagePlans> entry in allPackages)
        //        {
        //            int packageID = entry.Value.packageID;

        //            // Find the package container based on its ID
        //            string packageContainerID = "packagesPanel" + packageID.ToString();
        //            Control packageContainer = FindControl(packageContainerID);

        //            if (packageContainer != null)
        //            {
        //                // Find the controls inside the package container
        //                Label packageNameLabel = packageContainer.FindControl("packageAName") as Label;
        //                Label packageDescriptionLabel = packageContainer.FindControl("packageAdescription") as Label;
        //                Label packagePriceLabel = packageContainer.FindControl("packageAPrice") as Label;
        //                Label durationLabel = packageContainer.FindControl("durationA") as Label;
        //                Repeater featuresRepeater = packageContainer.FindControl("featuresPackageA") as Repeater;

        //                // Set the package details
        //                packageNameLabel.Text = entry.Value.packageName;
        //                packageDescriptionLabel.Text = entry.Value.packageDescription;
        //                packagePriceLabel.Text = entry.Value.packagePrice.ToString();
        //                durationLabel.Text = "for " + entry.Value.packageDuration + " " + entry.Value.durationType;

        //                // Set styles for the controls
        //                packageNameLabel.CssClass = "mb-2 h5-mktg";
        //                packageDescriptionLabel.CssClass = "lh-condensed";
        //                packagePriceLabel.CssClass = "js-computed-value";
        //                durationLabel.CssClass = "text-normal f4 color-fg-muted js-pricing-cost-suffix js-monthly-suffix";

        //                // Retrieve the package features from Firebase
        //                FirebaseResponse featuresResponse = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID + "/features");
        //                var featuresJson = featuresResponse.Body;
        //                List<string> featuresList = JsonConvert.DeserializeObject<List<string>>(featuresJson);

        //                // Bind featuresList to the repeater control
        //                featuresRepeater.DataSource = featuresList;
        //                featuresRepeater.DataBind();
        //            }
        //        }
        //    }
        //}



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