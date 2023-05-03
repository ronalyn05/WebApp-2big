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
    public partial class ManageCustomers : System.Web.UI.Page
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
            }
        }
        private void DisplayAll()
        {

            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer all = response.ResultAs<Model.Customer>();
            var data = response.Body;
            Dictionary<string, Model.Customer> Allclients = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(data);

            //creating the columns of the gridview
            DataTable clientsTable = new DataTable();
            clientsTable.Columns.Add("CUSTOMER ID");
            clientsTable.Columns.Add("CUSTOMER NAME");
            clientsTable.Columns.Add("EMAIL");
            clientsTable.Columns.Add("CONTACT #");
            clientsTable.Columns.Add("STATUS"); 


            foreach (KeyValuePair<string, Model.Customer> entry in Allclients)
            {

                clientsTable.Rows.Add(entry.Value.cusId, entry.Value.firstName + " " + entry.Value.lastName, entry.Value.email, entry.Value.phoneNumber, entry.Value.cus_status);

            }


            // Bind DataTable to GridView control
            AllGridview.DataSource = clientsTable;
            AllGridview.DataBind();
        }

        private void DisplayPending()
        {
            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer pending = response.ResultAs<Model.Customer>();
            var data = response.Body;
            Dictionary<string, Model.Customer> pendingCustomers = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(data);

            //creating the columns of the gridview
            DataTable pendingTable = new DataTable();
            pendingTable.Columns.Add("CUSTOMER ID");
            pendingTable.Columns.Add("CUSTOMER NAME");
            pendingTable.Columns.Add("EMAIL");
            pendingTable.Columns.Add("CONTACT #");
            pendingTable.Columns.Add("STATUS");

            foreach (KeyValuePair<string, Model.Customer> entry in pendingCustomers)
            {
                if (entry.Value.cus_status == "Pending")
                {
                    pendingTable.Rows.Add(entry.Value.cusId, entry.Value.firstName + " " + entry.Value.lastName, entry.Value.email, entry.Value.phoneNumber, entry.Value.cus_status);

                }

            }
            // Bind DataTable to GridView control
            pendingGridView.DataSource = pendingTable;
            pendingGridView.DataBind();
        }
        private void DisplayApproved()
        {
            FirebaseResponse response = twoBigDB.Get("CUSTOMER");
            Model.Customer pending = response.ResultAs<Model.Customer>();
            var data = response.Body;
            Dictionary<string, Model.Customer> pendingClients = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(data);

            //creating the columns of the gridview
            DataTable approvedTable = new DataTable();
            approvedTable.Columns.Add("CUSTOMER ID");
            approvedTable.Columns.Add("CUSTOMER NAME");
            approvedTable.Columns.Add("EMAIL");
            approvedTable.Columns.Add("CONTACT #");
            approvedTable.Columns.Add("STATUS");

            foreach (KeyValuePair<string, Model.Customer> entry in pendingClients)
            {
                if (entry.Value.cus_status == "Approved")
                {
                    approvedTable.Rows.Add(entry.Value.cusId, entry.Value.firstName + " " + entry.Value.lastName, entry.Value.email, entry.Value.phoneNumber, entry.Value.cus_status);

                }

            }
            // Bind DataTable to GridView control
            approvedGridView.DataSource = approvedTable;
            approvedGridView.DataBind();
        }
        protected void detailsButton_Click(object sender, EventArgs e)
        {

            //Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            //Get the order ID from the first cell in the row
            int customerID = int.Parse(row.Cells[1].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("CUSTOMER/" + customerID);
            Model.Customer customerDetails = response.ResultAs<Model.Customer>();

            int currentClient = customerDetails.cusId;
            Session["currentCustomer"] = currentClient;

            Response.Write("<script>window.location.href = '/superAdmin/customerDetails.aspx'; </script>");

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
                FirebaseResponse response = twoBigDB.Get("CUSTOMER/" + customerID);
                Model.Customer customerDetails = response.ResultAs<Model.Customer>();


                customerDetails.cus_status = "Approved";
                response = twoBigDB.Update("CUSTOMER/" + customerID, customerDetails);

                Response.Write("<script>alert ('successfully approved!');  window.location.href = '/superAdmin/ManageCustomers.aspx'; </script>");
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
                FirebaseResponse response = twoBigDB.Get("CUSTOMER/" + customerID);
                Model.Customer customerDetails = response.ResultAs<Model.Customer>();


                customerDetails.cus_status = "Declined";
                response = twoBigDB.Update("CUSTOMER/" + customerID, customerDetails);

                Response.Write("<script>alert ('successfully declined!');  window.location.href = '/superAdmin/ManageCustomers.aspx'; </script>");
            }
        }
    }
}