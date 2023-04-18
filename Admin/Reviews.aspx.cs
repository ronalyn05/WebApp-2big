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
using WRS2big_Web.Model;

namespace WRS2big_Web.Admin
{
    public partial class Reviews : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                string idno = (string)Session["idno"];
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("STOREREVIEW/");
                Dictionary<string, StoreReview> reviews = response.ResultAs<Dictionary<string, StoreReview>>();
                var filteredList = reviews.Values.Where(d => d.adminID.ToString() == idno);

                GridView1.DataSource = filteredList;
                GridView1.DataBind();
            }
            
        }
    }
}