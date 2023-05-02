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
                Response.Write("<script>alert('Please login your account first'); window.location.href = '/superAdmin/Account.aspx'; </script>");
            }

            if (!IsPostBack)
            {
                DisplayAll();
                DisplayPending();
                DisplayApproved();
                DisplayFullDetails();

            }
        }

        private void DisplayAll()
        {

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
            clientsTable.Columns.Add("STATION NAME");
            clientsTable.Columns.Add("STATION ADDRESS");
            clientsTable.Columns.Add("STATUS");



            foreach (KeyValuePair<string, Model.AdminAccount> entry in Allclients)
            {


                FirebaseResponse stationResponse = twoBigDB.Get("ADMIN/" + entry.Key + "/RefillingStation");
                Model.RefillingStation station = stationResponse.ResultAs<Model.RefillingStation>();
               
                clientsTable.Rows.Add(entry.Value.idno, entry.Value.fname + " " + entry.Value.lname, entry.Value.email, entry.Value.phone, station.stationName, station.stationAddress, entry.Value.status);

            }


            // Bind DataTable to GridView control
            AllGridview.DataSource = clientsTable;
            AllGridview.DataBind();
        }


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

            foreach (KeyValuePair<string, Model.AdminAccount> entry in pendingClients)
            {
                if (entry.Value.status == "pending")
                {
                    FirebaseResponse stationResponse = twoBigDB.Get("ADMIN/" + entry.Key + "/RefillingStation");
                    Model.RefillingStation station = stationResponse.ResultAs<Model.RefillingStation>();
                    pendingTable.Rows.Add(entry.Value.idno, entry.Value.fname + " " + entry.Value.lname, entry.Value.email, entry.Value.phone, station.stationName, station.stationAddress, entry.Value.status);

                }

            }
            // Bind DataTable to GridView control
            pendingGridView.DataSource = pendingTable;
            pendingGridView.DataBind();
        }
        private void DisplayApproved()
        {
            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount pending = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> pendingClients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

            //creating the columns of the gridview
            DataTable approvedTable = new DataTable();
            approvedTable.Columns.Add("CLIENT ID");
            approvedTable.Columns.Add("CLIENT NAME");
            approvedTable.Columns.Add("EMAIL");
            approvedTable.Columns.Add("CONTACT #");
            approvedTable.Columns.Add("STATION NAME");
            approvedTable.Columns.Add("STATION ADDRESS");
            approvedTable.Columns.Add("STATUS");

            foreach (KeyValuePair<string, Model.AdminAccount> entry in pendingClients)
            {
                if (entry.Value.status == "Approved")
                {
                    FirebaseResponse stationResponse = twoBigDB.Get("ADMIN/" + entry.Key + "/RefillingStation");
                    Model.RefillingStation station = stationResponse.ResultAs<Model.RefillingStation>();
                    approvedTable.Rows.Add(entry.Value.idno, entry.Value.fname + " " + entry.Value.lname, entry.Value.email, entry.Value.phone, station.stationName, station.stationAddress, entry.Value.status);

                }

            }
            // Bind DataTable to GridView control
            approvedGridView.DataSource = approvedTable;
            approvedGridView.DataBind();
        }
        private void DisplayFullDetails()
        {


        }


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

        protected void approveButton_Click(object sender, EventArgs e)
        {
            List<int> customerIDs = new List<int>();

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
                Response.Write("<script>alert ('Please select at least one customer to approve'); </script>");
                return;
            }

            foreach (int customerID in customerIDs)
            {
                FirebaseResponse response = twoBigDB.Get("ADMIN/" + customerID);
                Model.AdminAccount customerDetails = response.ResultAs<Model.AdminAccount>();


                customerDetails.status = "Approved";
                response = twoBigDB.Update("ADMIN/" + customerID, customerDetails);

                //SEND NOTIFICATION

                Response.Write("<script>alert ('successfully approved!');  window.location.href = '/superAdmin/ManageWRSClients.aspx'; </script>");
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
                Response.Write("<script>alert ('Please select at least one customer to approve'); </script>");
                return;
            }

            foreach (int customerID in customerIDs)
            {
                FirebaseResponse response = twoBigDB.Get("ADMIN/" + customerID);
                Model.AdminAccount customerDetails = response.ResultAs<Model.AdminAccount>();


                customerDetails.status = "Declined";
                response = twoBigDB.Update("ADMIN/" + customerID, customerDetails);

                //SEND NOTIFICATION

                Response.Write("<script>alert ('successfully declined!');  window.location.href = '/superAdmin/ManageWRSClients.aspx'; </script>");
            }
        }

        //protected void btnAccept_Click(object sender, EventArgs e)
        //{


        //     Get the GridViewRow that contains the clicked button
        //    Button btn = (Button)sender;
        //    GridViewRow row = (GridViewRow)btn.NamingContainer;

        //     Get the order ID from the first cell in the row
        //    int adminID = int.Parse(row.Cells[1].Text);

        //     Retrieve the existing order object from the database
        //    FirebaseResponse response = twoBigDB.Get("ADMIN/" + adminID);
        //    Model.AdminAccount pendingClients = response.ResultAs<Model.AdminAccount>();

        //    to check if the status is already approved or declined 
        //    if (pendingClients.status == "Approved" || pendingClients.status == "Declined")
        //    {

        //        return;
        //    }

        //    pendingClients.status = "Approved";
        //    response = twoBigDB.Update("ADMIN/" + adminID, pendingClients);

        //    DisplayTable();
        //}


        //protected void btnDecline_Click(object sender, EventArgs e)
        //{


        //     Get the GridViewRow that contains the clicked button
        //    Button btn = (Button)sender;
        //    GridViewRow row = (GridViewRow)btn.NamingContainer;

        //     Get the order ID from the first cell in the row
        //    int adminID = int.Parse(row.Cells[1].Text);

        //     Retrieve the existing order object from the database
        //    FirebaseResponse response = twoBigDB.Get("ADMIN/" + adminID);
        //    Model.AdminAccount pendingClients = response.ResultAs<Model.AdminAccount>();

        //    to check if the status is already approved or declined 
        //    if (pendingClients.status =="Approved" || pendingClients.status == "Declined")
        //    {
        //        return;
        //    }

        //        pendingClients.status = "Declined";
        //        response = twoBigDB.Update("ADMIN/" + adminID, pendingClients);


        //    DisableUpdateActionButtons();
        //    DisplayTable();
        //}


    }
}