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
                //DisplayIDs();
            }
        }

        //private void DisplayIDs()
        //{
        //    //TO LOAD THE IDs FROM THE DATABASE
        //    FirebaseResponse response;
        //    response = twoBigDB.Get("CUSTOMER");
        //    Model.Customer admin = response.ResultAs<Model.Customer>();
        //    var item = response.Body;
        //    ListBoxCustomers.Items.Clear();

        //    Dictionary<string, Model.Customer> list = JsonConvert.DeserializeObject<Dictionary<string, Model.Customer>>(item);

        //    foreach (KeyValuePair<string, Model.Customer> entry in list)
        //    {

        //        ListBoxCustomers.Items.Add(entry.Value.cusID.ToString());
        //        //WaterTypeDDL.Items.Add(entry.Value.waterType.ToString());
        //    }
        //}
        //protected void CustomerDetails_Click(object sender, EventArgs e)
        //{
        //        String selected;
        //        selected = ListBoxCustomers.SelectedValue;

        //        FirebaseResponse response;
        //        response = twoBigDB.Get("CUSTOMER/" + selected);
        //        Model.Customer obj = response.ResultAs<Model.Customer>();

        //        LblID.Text = obj.cusID.ToString();
        //        firstname.Text = obj.cusFirstName.ToString();
        //        midname.Text = obj.cusMiddleName.ToString();
        //        lastname.Text = obj.cusLastName.ToString();
        //        LblDOB.Text = obj.cusBirthOfDate.ToString();
        //        address.Text = obj.cusAddress.ToString();
        //        contactnum.Text = obj.cusContactNumber.ToString();
        //        email.Text = obj.cusEmail.ToString();


        //}
    }
}