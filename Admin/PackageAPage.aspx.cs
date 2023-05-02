using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
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
    public partial class PackageAPage : System.Web.UI.Page
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
                        orderLimit.Text = entry.Value.packageLimit.ToString();


                        //save the package ID into session 
                        Session["currentPackage"] = packageID;


                        features = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PACKAGES/" + packageID + "/features");
                        var featuresList = JsonConvert.DeserializeObject<List<string>>(features.Body);

                        // Bind featuresList to your repeater control
                        featuresPackageA.DataSource = featuresList;
                        featuresPackageA.DataBind();
                    }
                }

            }
        }
    }
}