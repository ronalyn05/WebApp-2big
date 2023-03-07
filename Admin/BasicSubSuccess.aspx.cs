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

namespace WRS2big_Web.Admin
{
    public partial class BasicSubSuccess : System.Web.UI.Page
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
        }
        public void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime SubBasic = now.AddMonths(6);
                var idnum = Session["idno"].ToString();
                var firstname = Session["Fname"].ToString();
                DateTime startDate = now;
                string type = "BASIC";

                //var data = new Model.AdminAccount
                //{
                //    Idno = int.Parse(idnum),
                //    Fname = firstname,
                //    SubType = type,
                //    SubsDate = now,
                //    SubEnd = SubBasic,

                //};
                var data = new Model.Subscription();

                data.Idno = idnum;
                data.SubType = type;
                data.SubsDate = now;
                data.SubEnd = SubBasic;
                FirebaseResponse response;
                response = twoBigDB.Update("ADMIN/" + idnum, data);//Update Product Data 

                //Model.AdminAccount data = new Model.AdminAccount()
                //{
                //    Idno = int.Parse(idnum),
                //    SubType = type,
                //    SubsDate = now,
                //    SubEnd = SubBasic,

                //};
                //twoBigDB.Update("ADMIN/" + idnum, data);

                //FirebaseResponse response;
                //response = twoBigDB.Update("ADMIN/" + data.Idno, data);
                //Model.AdminAccount result = response.ResultAs<Model.AdminAccount>();

                Response.Write("<script>alert ('Thankyou for Subscribing ! You successfully subscribed to: " + data.SubType + " PLAN '); location.reload(); window.location.href = '/Admin/AdminProfile.aspx'; </script>");
                //Response.Write("<script> alert ('New Gallon added successfully'); location.reload(); window.location.href = 'ProductGallon.aspx' </script>");

            }
            catch
            {

            }

        }
    }
}