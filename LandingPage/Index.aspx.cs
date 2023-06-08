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

namespace WRS2big_Web.LandingPage
{
    public partial class Index : System.Web.UI.Page
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
                List<Model.PackagePlans> packageList = allFeatures.Values
 .Where(p => p.status == "Active")
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

        protected void subsribeNow_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            
            if (Session["idno"] == null)
            {
                
                Response.Write("<script>alert ('It seems like you are not logged in yet. Please login your account first before you can subscribe. If no account yet, create your account now!');  window.location.href = '/LandingPage/Account.aspx';</script>");
            }
        }
    }
}