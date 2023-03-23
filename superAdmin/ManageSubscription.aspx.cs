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
            if (Session["email"] == null && Session["password"] == null)
            {
                Response.Write("<script>alert('Please login your account first'); window.location.href = '/superAdmin/Account.aspx'; </script>");
            }
            //connection to database 
            twoBigDB = new FireSharp.FirebaseClient(config);

            if (!IsPostBack)
            {
                //DisplayPlans();
                DisplayTable();
                
             
            }
            //Table();
        }

        //private async void Table()
        //{

        //   var plans = await twoBigDB.GetAsync("SUPERADMIN/SUPSCRIPTION_PLANS");
        //    var data = JsonConvert.DeserializeObject<Dictionary<string, Model.SubscriptionPlans>>(plans.Body);

        //    foreach (var item in data)
        //    {
        //        plansTable.Rows.Add(new HtmlTableRow
        //        {
        //            Cells = 
        //            {
        //                 new HtmlTableCell { InnerText = item.Value.planName },
        //                 new HtmlTableCell { InnerText = item.Value.planDes },
        //                 new HtmlTableCell { InnerText = item.Value.planDuration }
        //            }
        //        });
        //    }

        //}
        private void DisplayTable()
        {


            FirebaseResponse response = twoBigDB.Get("SUPERADMIN/SUBSCRIPTION_PLANS");
            Model.SubscriptionPlans plan = response.ResultAs<Model.SubscriptionPlans>();
            var data = response.Body;
            Dictionary<string, Model.SubscriptionPlans> subplans = JsonConvert.DeserializeObject<Dictionary<string, Model.SubscriptionPlans>>(data);


            DataTable plansTable = new DataTable();
            plansTable.Columns.Add("PLAN ID");
            plansTable.Columns.Add("PLAN NAME");
            plansTable.Columns.Add(" DESCRIPTION");
            plansTable.Columns.Add(" DURATION(months)");
            plansTable.Columns.Add(" PRICE");
            plansTable.Columns.Add("FEATURES");


            foreach (KeyValuePair<string, Model.SubscriptionPlans> entry in subplans)
            {
                string features = string.Join("," , entry.Value.features); // concatenate features into a single line
                plansTable.Rows.Add(entry.Value.idno ,entry.Value.planName, entry.Value.planDes, entry.Value.planDuration, entry.Value.planPrice, features);
            
            }


            // Bind DataTable to GridView control
            GridView1.DataSource = plansTable;
            GridView1.DataBind();
        }


        protected void clientGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                // perform update function using the id value


                var data = new Model.SubscriptionPlans();

                data.planName = TextBox1.Text;
                data.planDes = TextBox2.Text;
                data.planDuration = TextBox3.Text;
                data.planPrice = TextBox4.Text;


                FirebaseResponse response;
                response = twoBigDB.Update("SUPERADMIN/SUBSCRIPTION_PLANS/" + id, data);//Update Product Data 
            }

        }
        //protected void btnUpdate_Click (object sender, EventArgs e)
        //{
        //    string selected;
        //    selected = 
        //}


        //private void DisplayPlans()
        //{
        //    //TO LOAD THE SUBSCRIPTION PLANS

        //    FirebaseResponse response;
        //    response = twoBigDB.Get("SUBSCRIPTION_PLANS");
        //    Model.SubscriptionPlans plan = response.ResultAs<Model.SubscriptionPlans>();
        //    var item = response.Body;
        //    Dictionary<string, Model.SubscriptionPlans> subplans = JsonConvert.DeserializeObject<Dictionary<string, Model.SubscriptionPlans>>(item);

        //    foreach (KeyValuePair<string, Model.SubscriptionPlans> entry in subplans)
        //    {
        //        PlansListbox.Items.Add(new ListItem(entry.Value.planName,entry.Key));
        //    }

        //}

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

        //protected void PlanDetails_Click(object sender, EventArgs e)
        //{
        //    String selected;
        //    selected = PlansListbox.SelectedValue;

        //    FirebaseResponse response;
        //    response = twoBigDB.Get("SUBSCRIPTION_PLANS/" + selected);
        //    Model.SubscriptionPlans obj = response.ResultAs<Model.SubscriptionPlans>();

        //    planID.Text = obj.idno.ToString();
        //    planName.Text = obj.planName.ToString();
        //    planDes.Text = obj.planDes.ToString();
        //    planDue.Text = obj.planDuration.ToString();
        //    planPrice.Text = obj.planPrice.ToString();
        //    // Clear the existing items in the ListBox
        //    planFeatures.Items.Clear();

        //    // Add the features from the database to the ListBox
        //    foreach (var feature in obj.features)
        //    {
        //        planFeatures.Items.Add(feature);
        //    }


        //}

        //    protected void updateBtn_Click(object sender, EventArgs e)
        //    {
        //        String deleteStr;
        //        deleteStr = PlansListbox.SelectedValue;

        //var data = new Model.SubscriptionPlans();

        //data.idno = int.Parse(planID.Text);
        //data.planName = planName.Text;
        //    data.planDes = planDes.Text;
        //    data.planDuration = planDue.Text;
        //    data.planPrice = planPrice.Text;


        //    FirebaseResponse response;
        //response = twoBigDB.Update("SUBSCRIPTION_PLANS/" + deleteStr, data);//Update Product Data 

           

    //        //var result = twoBigDB.Get("SUBSCRIPTION_PLANS/" + deleteStr);//Retrieve Updated Data
    //        //Model.SubscriptionPlans obj = response.ResultAs<Model.SubscriptionPlans>();//Database Result


    //        //planID.Text = obj.idno.ToString();
    //        //planName.Text = obj.planName;
    //        //planDes.Text = obj.planDes;
    //        //planFeatures.Text = obj.planFeatures;
    //        //PlanDue.Text = obj.planDuration.ToString();
    //        //PlanPrice.Text = obj.planPrice.ToString();

    //        Response.Write("<script>alert ('" + data.planName + " successfully updated!');</script>");
    //        DisplayUpdated();


    //    }
    //    private void DisplayUpdated()
    //    {
    //        String selected;
    //        selected = PlansListbox.SelectedValue;

    //        FirebaseResponse response;
    //        response = twoBigDB.Get("SUBSCRIPTION_PLANS/" + selected);
    //        Model.SubscriptionPlans obj = response.ResultAs<Model.SubscriptionPlans>();

    //        planID.Text = obj.idno.ToString();
    //        planName.Text = obj.planName.ToString();
    //        planDes.Text = obj.planDes.ToString();
    //        planDue.Text = obj.planDuration.ToString();
    //        planPrice.Text = obj.planPrice.ToString();



    //    }
    }
}