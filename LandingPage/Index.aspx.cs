﻿using System;
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
            FirebaseResponse features = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PACKAGES");
            var list = features.Body;
            Dictionary<string, Model.PackagePlans> allFeatures = JsonConvert.DeserializeObject<Dictionary<string, Model.PackagePlans>>(list);

            if (allFeatures != null)
            {
                foreach (KeyValuePair<string, Model.PackagePlans> entry in allFeatures)
                {
                    if (entry.Value.packageName == "Package A")
                    {
                        int packageID = entry.Value.packageID;

                        packageAName.Text = entry.Value.packageName;
                        packageAdescription.Text = entry.Value.packageDescription;
                        packageAPrice.Text = entry.Value.packagePrice.ToString();
                        durationA.Text = "for" + " " + entry.Value.packageDuration + " " + entry.Value.durationType + " " + "only";


                        features = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PACKAGES/" + packageID + "/features");
                        var featuresList = JsonConvert.DeserializeObject<List<string>>(features.Body);

                        // Bind featuresList to your repeater control
                        featuresPackageA.DataSource = featuresList;
                        featuresPackageA.DataBind();
                    }
                    //if (entry.Value.packageName == "Package C")
                    //{
                    //    int packageID = entry.Value.packageID;
                    //    double price = entry.Value.packagePrice;


                    //    packageCName.Text = entry.Value.packageName;
                    //    packageCdescription.Text = entry.Value.packageDescription;
                    //    packageCPrice.Text = price.ToString();
                    //    durationC.Text = "for" + " " + entry.Value.packageDuration + " " + entry.Value.durationType;
                    //    //manageStations.Text = entry.Value.numberOfStations + " " + "Refilling Stations";

                    //    features = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PACKAGES/" + packageID + "/features");
                    //    var featuresList = JsonConvert.DeserializeObject<List<string>>(features.Body);

                    //    // Bind featuresList to your repeater control
                    //    featuresPackageC.DataSource = featuresList;
                    //    featuresPackageC.DataBind();
                    //}
                    if (entry.Value.packageName == "Package B")
                    {
                        int packageID = entry.Value.packageID;

                        packageBName.Text = entry.Value.packageName;
                        packageBdescription.Text = entry.Value.packageDescription;
                        packageBPrice.Text = entry.Value.packagePrice.ToString();
                        durationB.Text = "for" + " " + entry.Value.packageDuration + " " + entry.Value.durationType;

                        features = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PACKAGES/" + packageID + "/features");
                        var featuresList = JsonConvert.DeserializeObject<List<string>>(features.Body);

                        // Bind featuresList to your repeater control
                        featuresPackageB.DataSource = featuresList;
                        featuresPackageB.DataBind();
                    }
                }

            }
        }
    }
}