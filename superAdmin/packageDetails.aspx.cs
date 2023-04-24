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
            }
            else
            {
                Response.Write("<script>alert ('You haven't created a subscription plan yet');window.location.href = '/superAdmin/ManageSubscription.aspx'; </script>");
            }
           
        }
        private void DisplayDetails()
        {
            int package = (int)Session["currentPackage"];

            FirebaseResponse response = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PACKAGES/" + package);
            Model.PackagePlans packageDets = response.ResultAs<Model.PackagePlans>();

            packageID.Text = packageDets.packageID.ToString();
            packageName.Text = packageDets.packageName;
            packageDescription.Text = packageDets.packageDescription;
            packagePrice.Text = packageDets.packagePrice.ToString();
            packageDuration.Text = packageDets.packageDuration.ToString() + " " + packageDets.durationType;
            packageLimit.Text = packageDets.packageLimit.ToString();
            numofStations.Text = packageDets.numberOfStations.ToString();

            FirebaseResponse featuresponse = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PACKAGES/" + packageDets.packageID + "/features");
            List<string> listOfFeatures = featuresponse.ResultAs<List<string>>();

            // Bind the features to the ListBox control
            featuresList.DataSource = listOfFeatures;
            featuresList.DataBind();



        }
    }
}