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

            FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + package);
            Model.PackagePlans packageDets = response.ResultAs<Model.PackagePlans>();

            
            packageID.Text = packageDets.packageID.ToString();
            packageName.Text = packageDets.packageName;
            packageDescription.Text = packageDets.packageDescription;
            packagePrice.Text = packageDets.packagePrice.ToString();
            packageDuration.Text = packageDets.packageDuration.ToString() + " " + packageDets.durationType;
            packageLimit.Text = packageDets.packageLimit.ToString();
            numofStations.Text = packageDets.numberOfStations.ToString();
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
        protected void restorePackage_Click(object sender, EventArgs e)
        {


                int packageID = (int)Session["currentPackage"];

                // Retrieve the existing order object from the database
                FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID);
                Model.PackagePlans packageDetails = response.ResultAs<Model.PackagePlans>();

                packageDetails.status = "Active";
                response = twoBigDB.Update("SUBSCRIPTION_PACKAGES/" + packageID, packageDetails);
            


            Response.Write("<script>alert (' Subscription Package restored');window.location.href = '/superAdmin/packageDetails.aspx'; </script>");
        }

        protected void archivePackage_Click(object sender, EventArgs e)
        {
            int packageID = (int)Session["currentPackage"];
            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID);
            Model.PackagePlans packageDetails = response.ResultAs<Model.PackagePlans>();

            packageDetails.status = "Archived";
            response = twoBigDB.Update("SUBSCRIPTION_PACKAGES/" + packageID, packageDetails);
            Response.Write("<script>alert (' Subscription Package archived');window.location.href = '/superAdmin/packageDetails.aspx'; </script>");
        }
    }
}