using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace WRS2big_Web.Admin
{
    public partial class CustomerRecord : System.Web.UI.Page
    {
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
                BindOrders();
            }

        }

        private void BindOrders()
        {
            //var idnum = lblCustomerID.Text;

            FirebaseResponse response = twoBigDB.Get("CUSTOMER/");
            Model.Customer record = response.ResultAs<Model.Customer>();
            var json = response.Body;
            Dictionary<string, Model.Customer> list = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(json);

            foreach (KeyValuePair<string, Model.Customer> entry in list)
            {
                //string item = entry.Value.CusID.ToString() + "\t" + entry.Value.CusLastName + "\t" + entry.Value.CusFirstName;
                //ListBox1.Items.Add(entry.Value.CusID.ToString()) ;
                //datatable1.Items.Add(entry.Value.CusID.ToString());

                lblCustomerID.Text += entry.Value.cusId.ToString() + "<br>" + "<br>";
                lblCustomerName.Text += entry.Value.firstName.ToString() + ' ' + entry.Value.middleName.ToString() + ' ' + entry.Value.lastName.ToString() + "<br>" + "<br>";
                lblCustomerBdate.Text += entry.Value.birthdate.ToString() + "<br>" + "<br>";
                lblCustomerAddress.Text += entry.Value.address + "<br>" + "<br>";
                lblCustomerEmail.Text += entry.Value.email + "<br>" + "<br>";
                lblCustomerPhonenumber.Text += entry.Value.phoneNumber + "<br>" + "<br>";
            }
        }
    }

}