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
    
    public partial class Subscriptions1 : System.Web.UI.Page
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
            //if (Session["email"] == null && Session["password"] == null)
            //{
            //    Response.Write("<script>alert('Please login your account first'); window.location.href = '/superAdmin/Account.aspx'; </script>");
            //}
            //connection to database 
            twoBigDB = new FireSharp.FirebaseClient(config);

            if (!IsPostBack)
            {
                //DisplayPlans();
                //DisplayTable();
                
             
            }
           
        }

        private void DisplayTable()
        {
            // Retrieve all subscription plans from Firebase
            FirebaseResponse response = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS");
            Dictionary<string, Model.SubscriptionPlans> subplans =
                JsonConvert.DeserializeObject<Dictionary<string, Model.SubscriptionPlans>>(response.Body);

            // Create a DataTable to store the subscription plans
            DataTable plansTable = new DataTable();
            plansTable.Columns.Add("PLAN ID");
            plansTable.Columns.Add("PLAN NAME");
            plansTable.Columns.Add("DESCRIPTION");
            plansTable.Columns.Add("DURATION(months)");
            plansTable.Columns.Add("PRICE");
            plansTable.Columns.Add("FEATURES");
           

            // Populate the DataTable with the subscription plans
            foreach (KeyValuePair<string, Model.SubscriptionPlans> entry in subplans)
            {
                string features = string.Join(",", entry.Value.features);

                plansTable.Rows.Add(entry.Value.idno, entry.Value.planName, entry.Value.planDes,
                    entry.Value.planDuration, entry.Value.planPrice, features);

            }

            // Bind the DataTable to the GridView control
            GridView1.DataSource = plansTable;
            GridView1.DataBind();
        }




        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //to select all the items selected in the features list
            var features = featuresList.Items.Cast<ListItem>() //collection of all the items in the checkbox control.
                .Where(li => li.Selected) // LINQ query to select only the items that have been selected by the user.
                .Select(li => li.Value)// LINQ query that selects only the values of the selected items
                .ToList();//converts the selected values to a List of strings

            try
            {
                // INSERT
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                var data = new Model.SubscriptionPlans
                {
                    idno = idnum,
                    planName = subPlan.Text,
                    planDes = PlanDescription.Text,
                    planDuration = duration.Text,
                    planPrice = txtamount.Text,
                    features = features
                        

                 };

                SetResponse response;
                response = twoBigDB.Set("SUPERADMIN/SUBSCRIPTION_PLANS/" + data.idno, data);//Storing data to the database
                Model.SubscriptionPlans result = response.ResultAs<Model.SubscriptionPlans>();//Database Result
                Response.Write("<script>alert ('New Subscription Plan Added ! Plan Name: " + result.planName + " ');window.location.href = '/superAdmin/ManageSubscription.aspx'; </script>");
               
            }
            catch
            {
                Response.Write("<script>alert('Subscription Name already exist'); window.location.href = '/superAdmin/ManageSubscription.aspx'; </script>");
            }
           
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {

        }

        protected void updateButton_Click1(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            GridViewRow row = (GridViewRow)button.NamingContainer;


            int id = int.Parse(row.Cells[1].Text);
            string name = row.Cells[2].Text;
            string description = row.Cells[3].Text;
            string duration = row.Cells[4].Text;
            string price = row.Cells[5].Text;
            string features = row.Cells[6].Text;

            planID.Text = id.ToString();
            planName.Text = name;
            planDes.Text = description;
            planDuration.Text = duration;
            planAmount.Text = price;


            string[] featuresArray = features.Split('\n');
            string featuresList = string.Join(Environment.NewLine, featuresArray);
            planFeatures.Text = featuresList;
            //updatePlan.Text = name;
            //updateDesc.Text = description;
            //updateDuration.Text = duration;
            //updateAmount.Text = price;


        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {
            var plans = new Model.SubscriptionPlans();

            plans.idno = int.Parse(planID.Text); // get the ID from the GridView
            plans.planName = planName.Text;
            plans.planDes = planDes.Text;
            plans.planDuration = planDuration.Text;

            FirebaseResponse response;
            response = twoBigDB.Update("SUPERADMIN/SUBSCRIPTION_PLANS/" + plans.idno, plans);
            Response.Write("<script>alert ('PLAN ID : " + plans.idno +" successfully updated!');</script>");

            var result = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS/" + plans.idno);
            Model.SubscriptionPlans obj = response.ResultAs<Model.SubscriptionPlans>();

            planID.Text = obj.idno.ToString();
            planName.Text = obj.planName.ToString();
            planDes.Text = obj.planDes.ToString();
            planDuration.Text = obj.planDuration.ToString();
            planAmount.Text = obj.planPrice.ToString();

        }
    }
}