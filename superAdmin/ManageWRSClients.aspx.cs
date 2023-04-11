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
                DisplayTable();
                

            }
           
        }

        private void DisplayTable()
        {

            FirebaseResponse response = twoBigDB.Get("ADMIN");
            Model.AdminAccount wrs = response.ResultAs<Model.AdminAccount>();
            var data = response.Body;
            Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);

            //creating the columns of the gridview
            DataTable plansTable = new DataTable();
            plansTable.Columns.Add("CLIENT ID");
            plansTable.Columns.Add("CLIENT NAME");
            plansTable.Columns.Add("EMAIL");
            plansTable.Columns.Add("CONTACT #");
            plansTable.Columns.Add("STATION NAME");
            plansTable.Columns.Add("STATION ADDRESS");
            plansTable.Columns.Add("STATUS");


            foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
            {
                FirebaseResponse stationResponse = twoBigDB.Get("ADMIN/" + entry.Key + "/RefillingStation");
                Model.RefillingStation station = stationResponse.ResultAs<Model.RefillingStation>();
                plansTable.Rows.Add(entry.Value.idno, entry.Value.fname + " " + entry.Value.lname, entry.Value.email, entry.Value.phone, station.stationName, station.stationAddress, entry.Value.status);

            }


            // Bind DataTable to GridView control
            GridView1.DataSource = plansTable;
            GridView1.DataBind();
        }


        protected void btnAccept_Click(object sender, EventArgs e)
        {
            

            // Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the order ID from the first cell in the row
            int adminID = int.Parse(row.Cells[1].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ADMIN/" + adminID);
            Model.AdminAccount pendingClients = response.ResultAs<Model.AdminAccount>();

            //to check if the status is already approved or declined 
            if (pendingClients.status == "Approved" || pendingClients.status == "Declined")
            {
                
                return;
            }

            pendingClients.status = "Approved";
            response = twoBigDB.Update("ADMIN/" + adminID, pendingClients);

            DisplayTable();
        }


        protected void btnDecline_Click(object sender, EventArgs e)
        {
         

            // Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the order ID from the first cell in the row
            int adminID = int.Parse(row.Cells[1].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ADMIN/" + adminID);
            Model.AdminAccount pendingClients = response.ResultAs<Model.AdminAccount>();

            //to check if the status is already approved or declined 
            if (pendingClients.status =="Approved" || pendingClients.status == "Declined")
            {
                return;
            }

                pendingClients.status = "Declined";
                response = twoBigDB.Update("ADMIN/" + adminID, pendingClients);


            //DisableUpdateActionButtons();
            DisplayTable();
        }

        
    }
}