using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using WRS2big_Web.Model;

namespace WRS2big_Web.Admin
{
    public partial class DeliveryRecords : System.Web.UI.Page
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
                DisplayDelivery();
            }
        }
        private void DisplayDelivery()
        {
            string idno = (string)Session["idno"];
            FirebaseResponse response;
            response = twoBigDB.Get("ADMIN/" + idno + "/DeliveryDetails/");
            DeliveryDetails obj = response.ResultAs<DeliveryDetails>();
            var json = response.Body;
            Dictionary<string, DeliveryDetails> list = JsonConvert.DeserializeObject<Dictionary<string, DeliveryDetails>>(json);

            if (list != null && list.Count > 0)
            {
                foreach (KeyValuePair<string, DeliveryDetails> entry in list)
                {
                    //ListBox1.Items.Add(entry.Value.deliveryId.ToString());
                    if (entry.Value.status == "Available")
                    {
                        ListBox1.Items.Add(entry.Value.deliveryId.ToString());
                    }
                    else if (entry.Value.status == "Unavailable")
                    {
                        ListBox2.Items.Add(entry.Value.deliveryId.ToString());
                    }
                }
            }
        }
            //DISPLAY AVAILABLE DELIVERY DETAILS
        protected void btnDisplayDelivery_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            String slected;
            slected = ListBox1.SelectedValue;
            FirebaseResponse response;
            response = twoBigDB.Get("ADMIN/" + idno + "/DeliveryDetails/" + slected);
            DeliveryDetails res = response.ResultAs<DeliveryDetails>();

            LblID.Text = res.deliveryId.ToString();
            txtdeliveryType.Text = res.deliveryType.ToString();
            txtdeliveryFee.Text = res.deliveryFee.ToString();
            txtEstimatedTime.Text = res.estimatedTime.ToString();
            drdstatus.Text = res.status.ToString();
        }
        //DISPLAY UNAVAILABLE DELIVERY DETAILS
        protected void btnDisplayDelivery2_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            String slected;
            slected = ListBox2.SelectedValue;
            FirebaseResponse response;
            response = twoBigDB.Get("ADMIN/" + idno + "/DeliveryDetails/" + slected);
            DeliveryDetails res = response.ResultAs<DeliveryDetails>();

            LblID.Text = res.deliveryId.ToString();
            txtdeliveryType.Text = res.deliveryType.ToString();
            txtdeliveryFee.Text = res.deliveryFee.ToString();
            txtEstimatedTime.Text = res.estimatedTime.ToString();
            drdstatus.Text = res.status.ToString();

            if (res.status.ToString() == "Unavailable")
            {
                txtdeliveryType.ReadOnly = true;
                txtdeliveryFee.ReadOnly = true;
                txtEstimatedTime.ReadOnly = true;
                drdstatus.Enabled = false;
            }
            else if (res.status.ToString() == "Available")
            {
                txtdeliveryType.ReadOnly = false;
                txtdeliveryFee.ReadOnly = false;
                txtEstimatedTime.ReadOnly = false;
                drdstatus.Enabled = true;                
            }
        }
        //ADD DELIVERY DETAILS
        public void btnAdd_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            try
            {
                // INSERT DATA TO TABLE = DELIVERY
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                var data = new Model.DeliveryDetails
                {
                    deliveryId = idnum,
                    deliveryType = DeliveryType.Text,
                    deliveryFee = DeliveryFee.Text,
                    estimatedTime = EstimatedTime.Text,
                    status = Drd_Status.Text
                };

                SetResponse response;
                response = twoBigDB.Set("ADMIN/" + idno + "/DeliveryDetails/" + data.deliveryId, data);//Store data to DELIVERY table
                DeliveryDetails res = response.ResultAs<DeliveryDetails>();//Database Result

                Response.Write("<script>alert ('Delivery details with Id number: " + data.deliveryId + " is successfully added!'); location.reload(); window.location.href = '/Admin/DeliveryRecords.aspx'; </script>");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/DeliveryRecords.aspx';" + ex.Message);
            }
        }
        //UPDATE DELIVERY DETAILS
        protected void btnEditDeliveryType_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            String updateStr;
            updateStr = ListBox1.SelectedValue;

            var data = new DeliveryDetails();

            data.deliveryId = int.Parse(LblID.Text);
            data.deliveryType = txtdeliveryType.Text;
            data.deliveryFee = txtdeliveryFee.Text;
            data.estimatedTime = txtEstimatedTime.Text;
            data.status = drdstatus.Text;
            //data.DateAdded = DateTime.UtcNow;

            //if (data.productType != 0 && data.productSize != 0)
            //{
            FirebaseResponse response;
            response = twoBigDB.Update("ADMIN/" + idno + "/DeliveryDetails/" + updateStr, data);//Update Delivery Data 

            var result = twoBigDB.Get("ADMIN/" + idno + "/DeliveryDetails/" + updateStr);//Retrieve Updated Data From DELIVERY/ TBL
            DeliveryDetails obj = response.ResultAs<DeliveryDetails>();//Database Result

            LblID.Text = obj.deliveryId.ToString();
            txtdeliveryType.Text = obj.deliveryType.ToString();
            txtdeliveryFee.Text = obj.deliveryFee.ToString();
            txtEstimatedTime.Text = obj.estimatedTime.ToString();
            drdstatus.Text = obj.status.ToString();

            Response.Write("<script>alert ('Delivery ID : " + updateStr + " successfully updated!');</script>");
            //}
            //else
            //{
            //    Response.Write("<script> alert ('Invalid Product Type! Please select one.. ') </script>");
            //}
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            FirebaseResponse response = twoBigDB.Get("ADMIN/" + idno + "/DeliveryDetails/");
            Dictionary<string, DeliveryDetails> list = JsonConvert.DeserializeObject<Dictionary<string, DeliveryDetails>>(response.Body);
            List<DeliveryDetails> deliveryDetails = list.Values.ToList();
            // assuming the above code fetches the data correctly
            // set the deliveryDetails list as the data source for the table
            //Table.DataSource = deliveryDetails;
            //Table.DataBind();

            //DataTable dt = new DataTable();
            //dt.Columns.Add("Delivery ID");
            //dt.Columns.Add("Delivery Type");
            //dt.Columns.Add("Delivery Fee");
            //dt.Columns.Add("Estimated Time");
            //dt.Columns.Add("Status");

            //foreach (var item in list)
            //{
            //    DataRow dr = dt.NewRow();
            //    //dr["Delivery ID"] = item.deliveryId;
            //    dr["Delivery Type"] = item.Value.deliveryType;
            //    dr["Delivery Fee"] = item.Value.deliveryFee;
            //    dr["Estimated Time"] = item.Value.estimatedTime;
            //    dr["Status"] = item.Value.status;
            //    dt.Rows.Add(dr);
            //}

            //    GridView1.DataSource = list.Values;
            //    GridView1.DataBind();
        }
    }
}