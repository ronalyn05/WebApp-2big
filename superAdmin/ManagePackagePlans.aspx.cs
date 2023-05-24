using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
        private DataTable packagesTable = new DataTable();

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
                loadArchived();

               
                
            }

            deletePackage.Visible = false;
            viewPackageDetails.Visible = false;
            //restorePackage.Visible = true;
            //viewArchived.Visible = true;



        }

        private void loadArchived()
        {


            FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES");
            Model.PackagePlans all = response.ResultAs<Model.PackagePlans>();
            var data = response.Body;


            Dictionary<string, Model.PackagePlans> allPackages = JsonConvert.DeserializeObject<Dictionary<string, Model.PackagePlans>>(data);

            if (allPackages != null)
            {
                //creating the columns of the gridview
                packagesTable = new DataTable();
                packagesTable.Columns.Add("PACKAGE ID");
                packagesTable.Columns.Add("STATUS");
                packagesTable.Columns.Add("PACKAGE NAME");
                packagesTable.Columns.Add("PACKAGE DESCRIPTION");
                packagesTable.Columns.Add("PACKAGE PRICE");
                packagesTable.Columns.Add("PACKAGE DURATION");
                packagesTable.Columns.Add("ORDER LIMIT");

                foreach (KeyValuePair<string, Model.PackagePlans> entry in allPackages)
                {
                    if (entry.Value.status == "Archived")
                    {
                        packagesTable.Rows.Add(entry.Value.packageID,entry.Value.status, entry.Value.packageName, entry.Value.packageDescription, entry.Value.packagePrice, entry.Value.packageDuration + " " + entry.Value.durationType, entry.Value.packageLimit);

                    }

                }
                // Bind DataTable to GridView control
                archivedGrid.DataSource = packagesTable;
                archivedGrid.DataBind();
            }
        }
        private void DisplayPackages()
        {

            FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES");
            Model.PackagePlans all = response.ResultAs<Model.PackagePlans>();
            var data = response.Body;
            
            
            Dictionary<string, Model.PackagePlans> allPackages = JsonConvert.DeserializeObject<Dictionary<string, Model.PackagePlans>>(data);

             if (allPackages != null)
            {
                //creating the columns of the gridview
                packagesTable = new DataTable();
                packagesTable.Columns.Add("PACKAGE ID");
                packagesTable.Columns.Add("STATUS");
                packagesTable.Columns.Add("PACKAGE NAME");
                packagesTable.Columns.Add("PACKAGE DESCRIPTION");
                packagesTable.Columns.Add("PACKAGE PRICE");
                packagesTable.Columns.Add("PACKAGE DURATION");
                packagesTable.Columns.Add("ORDER LIMIT");

                foreach (KeyValuePair<string, Model.PackagePlans> entry in allPackages)
                {
                    if (entry.Value.status != "Archived")
                    {
                        packagesTable.Rows.Add(entry.Value.packageID,entry.Value.status, entry.Value.packageName, entry.Value.packageDescription, entry.Value.packagePrice, entry.Value.packageDuration + " " + entry.Value.durationType, entry.Value.packageLimit);

                    }

                }
                // Bind DataTable to GridView control
                packagesGridview.DataSource = packagesTable;
                packagesGridview.DataBind();
               
            }
        }

        protected void selectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selectAll = (CheckBox)sender;
            //CheckBoxList allFeatures = (CheckBoxList)Page.FindControl("featuresCheckbox");
            CheckBoxList allFeatures = (CheckBoxList)selectAll.NamingContainer.FindControl("featuresCheckbox");

            foreach (ListItem item in allFeatures.Items)
            {
                item.Selected = selectAll.Checked;
                //select.Checked = selectAll.Checked;
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
                    renewable = renewableDDL.SelectedValue,
                    packageDuration = int.Parse(packageDuration.Text),
                    packagePrice = int.Parse(packagePrice.Text),
                    packageLimit = int.Parse(packageOrderLimit.Text),
                    productLimit = int.Parse(packageProductLimit.Text),
                    numberOfStations = int.Parse(packageMangeStation.Text),
                    status = "Active",
                    //messaging = messagingOption.SelectedValue,
                    //numberOfStations = int.Parse(numofStations.Text),
                    features = features


                };

                SetResponse response;
                response = twoBigDB.Set("SUBSCRIPTION_PACKAGES/" + data.packageID, data);//Storing data to the database
                Model.PackagePlans result = response.ResultAs<Model.PackagePlans>();//Database Result

                //SAVE LOGS TO SUPER ADMIN
                //Get the current date and time
                DateTime logTime = DateTime.Now;

                //generate a random number for users logged
                //Random rnd = new Random();
                int logID = rnd.Next(1, 10000);

                var idno = (string)Session["SuperIDno"];
                string superName = (string)Session["superAdminName"];

                //Store the login information in the USERLOG table
                var log = new Model.superLogs
                {
                    logsId = logID,
                    superID = int.Parse(idno),
                    superFullname = superName,
                    superActivity = "CREATED NEW PACKAGE:" + " " + result.packageName,
                    activityTime = logTime
                };

                //Storing the  info
                response = twoBigDB.Set("SUPERADMIN_LOGS/" + log.logsId, log);//Storing data to the database
                Model.superLogs res = response.ResultAs<Model.superLogs>();//Database Result

                Response.Write("<script>alert ('New Subscription Package Added ! Package Name: " + result.packageName + " ');window.location.href = '/superAdmin/ManagePackagePlans.aspx'; </script>");

            }
            catch
            {
                Response.Write("<script>alert('Something went wrong'); window.location.href = '/superAdmin/ManagePackagePlans.aspx'; </script>");
            }
        }


        protected void viewPackageDetails_Click(object sender, EventArgs e)
        {

            List<int> selectedPackage = new List<int>();

            foreach (GridViewRow row in packagesGridview.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("selectedDeleted");
                if (chk != null && chk.Checked)
                {
                    //get the package ID using the index of the gridview
                    int packageID = int.Parse(row.Cells[1].Text);

                    //add the selected into the list
                    selectedPackage.Add(packageID);
                }
            }

            foreach(int packageID in selectedPackage)
            {
                if (selectedPackage.Count == 1)
                {

                    // Retrieve the existing order object from the database
                    FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID);
                    Model.PackagePlans packageDetails = response.ResultAs<Model.PackagePlans>();

                    int package = packageDetails.packageID;
                    Session["currentPackage"] = package;

                    Response.Write("<script>window.location.href = '/superAdmin/packageDetails.aspx'; </script>");

                }
                if (selectedPackage.Count == 0)
                {
                    Response.Write("<script>alert ('Please select at least one package to delete'); </script>");
                    return;
                }
                if (selectedPackage.Count >= 2)
                {
                    Response.Write("<script>alert ('Please choose only 1 package to view its full details'); window.location.href = '/superAdmin/ManagePackagePlans.aspx'; </script>");
                    return;
                }
            }
            
           
        }


        protected void selectedDeleted_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selected = (CheckBox)sender;

            CheckBox toDelete = (CheckBox)selected.NamingContainer.FindControl("selectedDeleted");

           if (toDelete != null && toDelete.Checked)
           {
                deletePackage.Visible = true;
                viewPackageDetails.Visible = true;
           }

        }

        protected void deletePackage_Click(object sender, EventArgs e)
        {

            List<int> selectedPackage = new List<int>();

            foreach (GridViewRow row in packagesGridview.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("selectedDeleted");
                if (chk != null && chk.Checked)
                {
                    int clientID = int.Parse(row.Cells[1].Text);
                    selectedPackage.Add(clientID);
                }
            }

            if (selectedPackage.Count == 0)
            {
                Response.Write("<script>alert ('Please select at least one package to archive'); </script>");
                return;
            }
            
            foreach (int packageID in selectedPackage)
            {

                //// Retrieve the existing order object from the database
                //FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID);
                //Model.PackagePlans packageDetails = response.ResultAs<Model.PackagePlans>();

                //packageDetails.status = "Archived";
                //response = twoBigDB.Update("SUBSCRIPTION_PACKAGES/" + packageID, packageDetails);
                //int packageID = (int)Session["currentPackage"];

                FirebaseResponse subscribed = twoBigDB.Get("SUBSCRIBED_CLIENTS/");

                if (subscribed != null)
                {
                    Dictionary<string, Model.superAdminClients> all = subscribed.ResultAs<Dictionary<string, Model.superAdminClients>>();

                    bool isClientSubscribed = false; // Flag variable to track if a subscribed client is found

                    foreach (var client in all)
                    {
                        if (client.Value.currentSubStatus == "Active" && client.Value.plan == packageID.ToString())
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

                        Response.Write("<script>alert ('Subscription Package archived');window.location.href = '/superAdmin/ManagePackagePlans.aspx'; </script>");
                    }
                }
            }

        }






        protected void viewArchived_Click(object sender, EventArgs e)
        {
            //Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            //Get the order ID from the first cell in the row
            int packageID = int.Parse(row.Cells[1].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_PACKAGES/" + packageID);
            Model.PackagePlans packageDetails = response.ResultAs<Model.PackagePlans>();

            int package = packageDetails.packageID;
            Session["currentPackage"] = package;

            Response.Write("<script>window.location.href = '/superAdmin/packageDetails.aspx'; </script>");
        }
    }
}