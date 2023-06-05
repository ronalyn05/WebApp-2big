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
    public partial class ManageWRSClients : System.Web.UI.Page
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

            if (Session["email"] == null && Session["password"] == null)
            {
                Response.Write("<script>alert('Please login your account first'); window.location.href = '/superAdmin/SuperAdminAccount.aspx'; </script>");
            }

            if (!IsPostBack)
            {
                //DisplayAll();
                DisplayPending();
                //DisplayApproved();
                //DisplayDeclined();

               
                //pendingGridView.Visible = false;
                //approvedGridView.Visible = false;
                //declinedGridView.Visible = false;
                //AllGridview.Visible = false;
                //selectAll.Visible = false;
                //approveButton.Visible = false;
                //declineButton.Visible = false;




            }
        }

        //private void DisplayAll()
        //{

        //    FirebaseResponse response = twoBigDB.Get("ADMIN");
        //    Model.AdminAccount all = response.ResultAs<Model.AdminAccount>();
        //    var data = response.Body;
        //    Dictionary<string, Model.AdminAccount> Allclients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

        //    //creating the columns of the gridview
        //    DataTable clientsTable = new DataTable();
        //    clientsTable.Columns.Add("CLIENT ID");
        //    clientsTable.Columns.Add("CLIENT NAME");
        //    clientsTable.Columns.Add("EMAIL");
        //    clientsTable.Columns.Add("CONTACT #");
        //    clientsTable.Columns.Add("STATION NAME");
        //    clientsTable.Columns.Add("STATION ADDRESS");
        //    clientsTable.Columns.Add("STATUS");
        //    clientsTable.Columns.Add("DATE REGISTERED");



        //    foreach (KeyValuePair<string, Model.AdminAccount> entry in Allclients)
        //    {
        //        FirebaseResponse stationResponse = twoBigDB.Get("ADMIN/" + entry.Key + "/RefillingStation");
        //        Model.RefillingStation station = stationResponse.ResultAs<Model.RefillingStation>();
               
        //        clientsTable.Rows.Add(
        //            entry.Value.idno, 
        //            entry.Value.fname + " " + entry.Value.lname, 
        //            entry.Value.email, 
        //            entry.Value.phone, 
        //            station.stationName, 
        //            station.stationAddress, 
        //            entry.Value.status,
        //            entry.Value.dateRegistered);

        //    }


        //    // Bind DataTable to GridView control
        //    AllGridview.DataSource = clientsTable;
        //    AllGridview.DataBind();
        //}


        private void DisplayPending()
        {
            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount pending = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> pendingClients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

            //creating the columns of the gridview
            DataTable pendingTable = new DataTable();
            pendingTable.Columns.Add("CLIENT ID");
            pendingTable.Columns.Add("CLIENT NAME");
            pendingTable.Columns.Add("EMAIL");
            pendingTable.Columns.Add("CONTACT #");
            pendingTable.Columns.Add("STATION NAME");
            pendingTable.Columns.Add("STATION ADDRESS");
            pendingTable.Columns.Add("STATUS");
            pendingTable.Columns.Add("DATE REGISTERED");

            foreach (KeyValuePair<string, Model.AdminAccount> entry in pendingClients)
            {
                if (entry.Value.status == "Pending")
                {
                    FirebaseResponse stationResponse = twoBigDB.Get("ADMIN/" + entry.Key + "/RefillingStation");
                    Model.RefillingStation station = stationResponse.ResultAs<Model.RefillingStation>();
                    pendingTable.Rows.Add(
                        entry.Value.idno, 
                        entry.Value.fname + " " + entry.Value.lname, 
                        entry.Value.email, 
                        entry.Value.phone, 
                        station.stationName, 
                        station.stationAddress, 
                        entry.Value.status,
                        entry.Value.dateRegistered);

                }

            }

            if (pendingTable.Rows.Count == 0)
            {
                pendingLabel.Text = "NO PENDING CLIENTS";
                pendingLabel.Style["font-size"] = "24px";
                pendingLabel.Style["color"] = "red";
                selectAll.Visible = false;
                approveButton.Visible = false;
                declineButton.Visible = false;
            }
            // Bind DataTable to GridView control
            pendingGridView.DataSource = pendingTable;
            pendingGridView.DataBind();
        }
       // private void DisplayApproved()
       // {
       //     FirebaseResponse response = twoBigDB.Get("ADMIN");
       //     Model.AdminAccount pending = response.ResultAs<Model.AdminAccount>();
       //     var data = response.Body;
       //     Dictionary<string, Model.AdminAccount> pendingClients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

       //     //creating the columns of the gridview
       //     DataTable approvedTable = new DataTable();
       //     approvedTable.Columns.Add("CLIENT ID");
       //     approvedTable.Columns.Add("CLIENT NAME");
       //     approvedTable.Columns.Add("EMAIL");
       //     approvedTable.Columns.Add("CONTACT #");
       //     approvedTable.Columns.Add("STATION NAME");
       //     approvedTable.Columns.Add("STATION ADDRESS");
       //     approvedTable.Columns.Add("STATUS");
       //     approvedTable.Columns.Add("DATE REGISTERED");

       //     foreach (KeyValuePair<string, Model.AdminAccount> entry in pendingClients)
       //     {
       //         if (entry.Value.status == "Approved")
       //         {
       //             FirebaseResponse stationResponse = twoBigDB.Get("ADMIN/" + entry.Key + "/RefillingStation");
       //             Model.RefillingStation station = stationResponse.ResultAs<Model.RefillingStation>();
       //             approvedTable.Rows.Add(
       //                 entry.Value.idno, 
       //                 entry.Value.fname + " " + entry.Value.lname, 
       //                 entry.Value.email, 
       //                 entry.Value.phone, 
       //                 station.stationName, 
       //                 station.stationAddress, 
       //                 entry.Value.status,
       //                 entry.Value.dateRegistered);

       //         }

       //     }
       //     // Bind DataTable to GridView control
       //     approvedGridView.DataSource = approvedTable;
       //     approvedGridView.DataBind();
       // }
       //private void DisplayDeclined()
       // {
       //     FirebaseResponse response = twoBigDB.Get("ADMIN");
       //     Model.AdminAccount admin = response.ResultAs<Model.AdminAccount>();
       //     var data = response.Body;
       //     Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

       //     DataTable approvedTable = new DataTable();
       //     approvedTable.Columns.Add("CLIENT ID");
       //     approvedTable.Columns.Add("CLIENT NAME");
       //     approvedTable.Columns.Add("EMAIL");
       //     approvedTable.Columns.Add("CONTACT #");
       //     approvedTable.Columns.Add("STATION NAME");
       //     approvedTable.Columns.Add("STATION ADDRESS");
       //     approvedTable.Columns.Add("STATUS");
       //     approvedTable.Columns.Add("DATE REGISTERED");

       //     foreach (KeyValuePair<string, Model.AdminAccount> adminEntry in clients)
       //     {
       //         FirebaseResponse stationResponse = twoBigDB.Get("ADMIN/" + adminEntry.Key + "/RefillingStation");
       //         Model.RefillingStation station = stationResponse.ResultAs<Model.RefillingStation>();

       //         if (adminEntry.Value.status == "Declined")
       //         {
       //             approvedTable.Rows.Add(
       //       //entry.Value.profile_image,
       //       adminEntry.Value.idno,
       //        adminEntry.Value.fname + " " + adminEntry.Value.lname,
       //        adminEntry.Value.email,
       //        adminEntry.Value.phone,
       //        station.stationName,
       //        station.stationAddress,
       //        adminEntry.Value.status,
       //        adminEntry.Value.dateRegistered);
       //         }
       //     }
       //     declinedGridView.DataSource = approvedTable;
       //     declinedGridView.DataBind();
       // }



        protected void detailsButton_Click(object sender, EventArgs e)
        {

            //Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            //Get the order ID from the first cell in the row
            int adminID = int.Parse(row.Cells[1].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ADMIN/" + adminID);
            Model.AdminAccount clientDetails = response.ResultAs<Model.AdminAccount>();

            int currentClient = clientDetails.idno;
            Session["currentClient"] = currentClient;

            Response.Write("<script>window.location.href = '/superAdmin/clientDetails.aspx'; </script>");

        }

        //multiple approve the clients
        protected void approveButton_Click(object sender, EventArgs e)
        {
            List<int> clientIDs = new List<int>();

            if (Session["SuperIDno"] != null)
            {
                var idno = (string)Session["SuperIDno"];

                foreach (GridViewRow row in pendingGridView.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("select");
                    if (chk != null && chk.Checked)
                    {
                        int clientID = int.Parse(row.Cells[1].Text);
                        clientIDs.Add(clientID);
                    }
                }

                if (clientIDs.Count == 0)
                {
                    Response.Write("<script>alert ('Please select at least one client to approve'); </script>");
                    return;
                }

                foreach (int clientID in clientIDs)
                {
                    FirebaseResponse response = twoBigDB.Get("ADMIN/" + clientID);
                    Model.AdminAccount admin = response.ResultAs<Model.AdminAccount>();


                    admin.status = "Approved";
                    admin.dateApproved = DateTime.Now;
                    response = twoBigDB.Update("ADMIN/" + clientID, admin);

                    //SEND NOTIFICATION TO ADMIN 
                    Random rnd = new Random();
                    int ID = rnd.Next(1, 20000);

                    var Notification = new Model.Notification
                    {
                        superAdmin_ID = int.Parse(idno),
                        admin_ID = clientID,
                        sender = "Super Admin",
                        title = "Application Approved",
                        receiver = "Admin",
                        body = "Your application is now approved! You can now subscribe to our Subscription Packages",
                        notificationDate = DateTime.Now,
                        status = "unread",
                        notificationID = ID

                    };

                    SetResponse notifResponse;
                    notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                    Model.Notification notif = notifResponse.ResultAs<Model.Notification>();//Database Result

                    //SAVE LOGS TO SUPER ADMIN
                    //Get the current date and time
                    DateTime logTime = DateTime.Now;

                    //generate a random number for users logged
                    //Random rnd = new Random();
                    int logID = rnd.Next(1, 10000);

                    if (Session["superAdminName"] != null)
                    {
                        string superName = (string)Session["superAdminName"];

                        //Store the login information in the USERLOG table
                        var log = new Model.superLogs
                        {

                            logsId = logID,
                            superID = int.Parse(idno),
                            superFullname = superName,
                            superActivity = "APPROVED CLIENT:" + " " + admin.fname + " " + admin.lname,
                            activityTime = logTime
                        };

                        //Storing the  info
                        response = twoBigDB.Set("SUPERADMIN_LOGS/" + log.logsId, log);//Storing data to the database
                        Model.superLogs res = response.ResultAs<Model.superLogs>();//Database Result

                        Response.Write("<script>alert ('successfully approved!');  window.location.href = '/superAdmin/ManageWRSClients.aspx'; </script>");
                    }
                    
                }
            }
            
        }
        protected void selectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selectAll = (CheckBox)sender;
            foreach (GridViewRow row in pendingGridView.Rows)
            {
                CheckBox select = (CheckBox)row.FindControl("select");
                select.Checked = selectAll.Checked;
            }
        }


        protected void declineButton_Click(object sender, EventArgs e)
        {

            List<int> customerIDs = new List<int>();

            if (Session["SuperIDno"] != null)
            {
                int superID = (int)Session["SuperIDno"];
                string reason = reasonDecline.Text;

                foreach (GridViewRow row in pendingGridView.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("select");
                    if (chk != null && chk.Checked)
                    {
                        int customerID = int.Parse(row.Cells[1].Text);
                        customerIDs.Add(customerID);
                    }
                }

                if (customerIDs.Count == 0)
                {
                    Response.Write("<script>alert ('Please select at least one client to decline'); </script>");
                    return;
                }

                foreach (int customerID in customerIDs)
                {
                    FirebaseResponse response = twoBigDB.Get("ADMIN/" + customerID);
                    Model.AdminAccount customerDetails = response.ResultAs<Model.AdminAccount>();


                    customerDetails.status = "Declined";
                    customerDetails.dateDeclined = DateTime.Now;
                    response = twoBigDB.Update("ADMIN/" + customerID, customerDetails);

                    //SEND NOTIFICATION
                    Random rnd = new Random();
                    int ID = rnd.Next(1, 20000);

                    var Notification = new Model.Notification
                    {
                        admin_ID = customerID,
                        superAdmin_ID = superID,
                        sender = "Super Admin",
                        title = "Application Declined",
                        receiver = "Admin",
                        body = reason,
                        notificationDate = DateTime.Now,
                        status = "unread",
                        notificationID = ID

                    };

                    SetResponse notifResponse;
                    notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                    Model.Notification notif = notifResponse.ResultAs<Model.Notification>();//Database Result

                    //SAVE LOGS TO SUPER ADMIN
                    //Get the current date and time
                    DateTime logTime = DateTime.Now;

                    //generate a random number for users logged
                    //Random rnd = new Random();
                    int logID = rnd.Next(1, 10000);

                    var idno = (string)Session["SuperIDno"];

                    if (Session["superAdminName"] != null)
                    {
                        string superName = (string)Session["superAdminName"];

                        //Store the login information in the USERLOG table
                        var log = new Model.superLogs
                        {
                            logsId = logID,
                            superID = int.Parse(idno),
                            superFullname = superName,
                            superActivity = "DECLINED CLIENT:" + " " + customerDetails.fname + " " + customerDetails.lname,
                            activityTime = logTime
                        };

                        //Storing the  info
                        response = twoBigDB.Set("SUPERADMIN_LOGS/" + log.logsId, log);//Storing data to the database
                        Model.superLogs res = response.ResultAs<Model.superLogs>();//Database Result


                        Response.Write("<script>alert ('successfully declined!');  window.location.href = '/superAdmin/ManageWRSClients.aspx'; </script>");
                    }
                   
                }
            }
            
        }

    
        protected void searchButton_Click(object sender, EventArgs e)
        {
            string searched = search.Text;
            //string selectedValue = sortDropdown.SelectedValue;

            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount all = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> Allclients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

            //creating the columns of the gridview
            DataTable clientsTable = new DataTable();
            clientsTable.Columns.Add("CLIENT ID");
            clientsTable.Columns.Add("CLIENT NAME");
            clientsTable.Columns.Add("EMAIL");
            clientsTable.Columns.Add("CONTACT #");
            clientsTable.Columns.Add("STATUS");
            clientsTable.Columns.Add("STATION NAME");
            clientsTable.Columns.Add("STATION ADDRESS");
            clientsTable.Columns.Add("DATE REGISTERED");

            foreach (KeyValuePair<string, Model.AdminAccount> entry in Allclients)
            {
                response = twoBigDB.Get("ADMIN/" + entry.Value.idno + "/RefillingStation");
                Model.RefillingStation station = response.ResultAs<Model.RefillingStation>();


                if (searched == entry.Value.fname || searched == entry.Value.lname || searched == station.stationName || searched == entry.Value.email || searched == entry.Key)
                {
                    if (entry.Value.status == "Pending")
                    {
                        clientsTable.Rows.Add(
                            entry.Value.idno,
                            entry.Value.fname + " " + entry.Value.lname,
                            entry.Value.email,
                            entry.Value.phone,
                            entry.Value.status,
                            station.stationName,
                            station.stationAddress,
                            entry.Value.dateRegistered);
                    }

                }
            }

            if (clientsTable.Rows.Count == 0)
            {
               
                Response.Write("<script>alert ('Client not found!'); window.location.href = '/superAdmin/ManageWRSClients.aspx'; </script>");
               
            }
            // Bind DataTable to GridView control
            pendingGridView.DataSource = clientsTable;
            pendingGridView.DataBind();

        }

        //protected void viewSorted_Click(object sender, EventArgs e)
        //{
        //    string selectedValue = sortDropdown.SelectedValue;

        //    if (selectedValue == "All")
        //    {
        //        Response.Write("<script> window.location.href = '/superAdmin/ManageWRSClients.aspx';</script>");

        //        //Display button and checkbox for SELECT ALL 
        //        selectAll.Visible = false;
        //        approveButton.Visible = false;
        //        declineButton.Visible = false;

        //    }
        //    else if (selectedValue == "Pending")
        //    {

        //        search.Text = "";
        //        pendingGridView.Visible = true;
        //        AllGridview.Visible = false;
        //        approvedGridView.Visible = false;
        //        declinedGridView.Visible = false;

        //        //Display button and checkbox for SELECT ALL 
        //        selectAll.Visible = true;
        //        approveButton.Visible = true;
        //        declineButton.Visible = true;


        //    }
        //    else if (selectedValue == "Approved")
        //    {

        //        search.Text = "";
        //        pendingGridView.Visible = false;
        //        AllGridview.Visible = false;
        //        approvedGridView.Visible = true;
        //        declinedGridView.Visible = false;
        //        //Display button and checkbox for SELECT ALL 
        //        selectAll.Visible = false;
        //        approveButton.Visible = false;
        //        declineButton.Visible = false;

        //    }
        //    else if (selectedValue == "Declined")
        //    {

        //        search.Text = "";
        //        pendingGridView.Visible = false;
        //        approvedGridView.Visible = false;
        //        AllGridview.Visible = false;
        //        declinedGridView.Visible = true;
        //        //Display button and checkbox for SELECT ALL 
        //        selectAll.Visible = false;
        //        approveButton.Visible = false;
        //        declineButton.Visible = false;

        //    }
        //}

        protected void closeButton_Click(object sender, EventArgs e)
        {
            search.Text = "";
            DisplayPending();
        }



    }
}