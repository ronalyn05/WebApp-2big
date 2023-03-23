//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using FireSharp;
//using FireSharp.Config;
//using FireSharp.Interfaces;
//using FireSharp.Response;
//using Newtonsoft.Json;
//using System.Threading.Tasks;
//using System.Data;
//using System.Diagnostics;


//namespace WRS2big_Web.superAdmin
//{
//    public partial class ClientReports : System.Web.UI.Page
//    {
//        //Initialize the FirebaseClient with the database URL and secret key.
//        IFirebaseConfig config = new FirebaseConfig
//        {
//            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
//            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"

//        };
//        IFirebaseClient twoBigDB;
//        protected void Page_Load(object sender, EventArgs e)
//        {
            
//            twoBigDB = new FireSharp.FirebaseClient(config);

//            displayClients();


//        }

//        protected void clientGridView_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
//        {
//            //var selected = clientGridView.SelectedIndex;

//            //FirebaseResponse response;

//            //response = twoBigDB.Get("ADMIN/" + selected);
//            //Model.AdminAccount obj = response.ResultAs<Model.AdminAccount>();
//            //// Get the selected row index

//            //// Get the selected row data
//            //string idno = clientGridView.Rows[selected].Cells[0].Text;
//            //string fname = clientGridView.Rows[selected].Cells[1].Text;
//            //string lname = clientGridView.Rows[selected].Cells[2].Text;
//            //string WRS_Name = clientGridView.Rows[selected].Cells[3].Text;
//            //string WRS_Address = clientGridView.Rows[selected].Cells[4].Text;
//            //string phone = clientGridView.Rows[selected].Cells[5].Text;
//            //string email = clientGridView.Rows[selected].Cells[6].Text;

//            //FirebaseResponse res = twoBigDB.Update("ADMIN/" + idno, new Model.AdminAccount
//            //{
//            //    fname = fname,
//            //    lname = lname,
//            //    WRS_Name = WRS_Name,
//            //    WRS_Address = WRS_Address,
//            //    phone = phone,
//            //    email = email
//            //});
//            if (e.CommandName == "SelectRow")
//            {
//                // Retrieve the index of the selected row
//                int index = Convert.ToInt32(e.CommandArgument);

//                // Retrieve the data for the selected row from the GridView's DataSource property
//                DataTable dataSource = (DataTable)clientGridView.DataSource;
//                DataRow selectedRow = dataSource.Rows[index];

//                // Retrieve the data for the selected row
//                string clientId = selectedRow["CLIENT ID"].ToString();
//                string firstName = selectedRow["FIRST NAME"].ToString();
//                string lastName = selectedRow["LAST NAME"].ToString();
//                string stationName = selectedRow["STATION NAME"].ToString();
//                string address = selectedRow["ADDRESS"].ToString();
//                string phone = selectedRow["PHONE #"].ToString();
//                string email = selectedRow["EMAIL"].ToString();

//                // Update the data in Firebase Realtime Database
//                FirebaseResponse response = twoBigDB.Update("ADMIN/" + clientId, new Model.AdminAccount()
//                {
//                    fname = firstName,
//                    lname = lastName,
//                    WRS_Name = stationName,
//                    WRS_Address = address,
//                    phone = phone,
//                    email = email
//                });

//                // Bind the data to the GridView control to refresh the data
//                displayClients();
//            }
//        }



//        private void displayClients()
//        {

//            FirebaseResponse response = twoBigDB.Get("ADMIN");
//            Model.AdminAccount plan = response.ResultAs<Model.AdminAccount>();
//            var data = response.Body;
//            Dictionary<string, Model.AdminAccount> clients = JsonConvert.DeserializeObject<Dictionary<string, Model.AdminAccount>>(data);


//            DataTable plansTable = new DataTable();
//            plansTable.Columns.Add("CLIENT ID");
//            plansTable.Columns.Add("FIRST NAME");
//            plansTable.Columns.Add("LAST NAME");
//            plansTable.Columns.Add("STATION NAME");
//            //plansTable.Columns.Add("BIRTHDATE");
//            plansTable.Columns.Add("ADDRESS");
//            plansTable.Columns.Add("PHONE #");
//            plansTable.Columns.Add("EMAIL");



//            foreach (KeyValuePair<string, Model.AdminAccount> entry in clients)
//            {
//                plansTable.Rows.Add(entry.Value.idno, 
//                    entry.Value.fname,
//                    entry.Value.lname, 
//                    entry.Value.WRS_Name, 
//                    //entry.Value.bdate, 
//                    entry.Value.WRS_Address, 
//                    entry.Value.phone,
//                    entry.Value.email);
//            }
//            // Bind DataTable to GridView control
//            clientGridView.DataSource = plansTable;
//            clientGridView.DataBind();
//        }
//    }
//}