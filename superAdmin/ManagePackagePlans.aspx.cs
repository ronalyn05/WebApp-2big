using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace WRS2big_Web.superAdmin
{
    public partial class ManagePackagePlans : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                DisplayPackages();
            }
          

        }
        private void DisplayPackages()
        {

            
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PACKAGES");
            Model.PackagePlans all = response.ResultAs<Model.PackagePlans>();
            var data = response.Body;
            
            
                Dictionary<string, Model.PackagePlans> allPackages = JsonConvert.DeserializeObject<Dictionary<string, Model.PackagePlans>>(data);

             if (allPackages != null)
            {
                //creating the columns of the gridview
                DataTable packagesTable = new DataTable();
                packagesTable.Columns.Add("PACKAGE ID");
                packagesTable.Columns.Add("PACKAGE NAME");
                packagesTable.Columns.Add("PACKAGE DESCRIPTION");
                packagesTable.Columns.Add("PACKAGE PRICE");
                packagesTable.Columns.Add("PACKAGE DURATION");
                packagesTable.Columns.Add("ORDER LIMIT");

                foreach (KeyValuePair<string, Model.PackagePlans> entry in allPackages)
                {

                    packagesTable.Rows.Add(entry.Value.packageID, entry.Value.packageName, entry.Value.packageDescription, entry.Value.packagePrice, entry.Value.packageDuration + " " + entry.Value.durationType, entry.Value.packageLimit);

                }
                // Bind DataTable to GridView control
                packagesGridview.DataSource = packagesTable;
                packagesGridview.DataBind();
            }
               



        }

        protected void createPackage_Click(object sender, EventArgs e)
        {
            //to select all the items selected in the features list
            var features = featuresCheckbox.Items.Cast<ListItem>() //collection of all the items in the checkbox control.
                .Where(li => li.Selected) // LINQ query to select only the items that have been selected by the user.
                .Select(li => li.Value)// LINQ query that selects only the values of the selected items
                .ToList();//converts the selected values to a List of strings

            try
            {
                // INSERT
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                var data = new Model.PackagePlans
                {
                    packageID = idnum,
                    packageName = packageName.Text,
                    packageDescription = packageDescription.Text,
                    durationType = durationTypeSelected.SelectedValue,
                    packageDuration = int.Parse(packageDuration.Text),
                    packagePrice = int.Parse(packagePrice.Text),
                    packageLimit = int.Parse(packageOrderLimit.Text),
                    messaging = messagingOption.SelectedValue,
                    numberOfStations = int.Parse(numofStations.Text),
                    features = features


                };

                SetResponse response;
                response = twoBigDB.Set("SUPERADMIN/SUBSCRIPTION_PACKAGES/" + data.packageID, data);//Storing data to the database
                Model.PackagePlans result = response.ResultAs<Model.PackagePlans>();//Database Result
                Response.Write("<script>alert ('New Subscription Package Added ! Package Name: " + result.packageName + " ');window.location.href = '/superAdmin/ManagePackagePlans.aspx'; </script>");

            }
            catch
            {
                Response.Write("<script>alert('Subscription Name already exist'); window.location.href = '/superAdmin/ManageSubscription.aspx'; </script>");
            }
        }


        protected void viewPackageDetails_Click(object sender, EventArgs e)
        {

            //Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            //Get the order ID from the first cell in the row
            int packageID = int.Parse(row.Cells[1].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PACKAGES/" + packageID);
            Model.PackagePlans packageDetails = response.ResultAs<Model.PackagePlans>();

            int package = packageDetails.packageID;
            Session["currentPackage"] = package;

            Response.Write("<script>window.location.href = '/superAdmin/packageDetails.aspx'; </script>");
        }
    }
}