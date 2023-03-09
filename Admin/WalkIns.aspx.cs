using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Firebase.Storage;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using WRS2big_Web.Model;

namespace WRS2big_Web.Admin
{
    public partial class POS : System.Web.UI.Page
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
        protected void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                // INSERT DATA TO TABLE = WALKINORDERS
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                // Convert input values to numerical format
                int qty = Convert.ToInt32(txtQty.Text);
                decimal price = Convert.ToDecimal(txtprice.Text);
                decimal discount = Convert.ToDecimal(txtDiscount.Text);

                // Calculate total amount
                decimal totalAmount = (qty * price) - discount;

                var data = new WalkInOrders
                {
                    orderNo = idnum,
                    productName = txtprodName.Text,
                    productSize = txtprodSize.Text,
                    productPrice = txtprice.Text,
                    productDiscount = txtDiscount.Text,
                    productQty = txtQty.Text,
                    totalAmount = totalAmount.ToString(), // Store calculated total amount as string
                    orderType = drdOrderType.Text,
                    dateAdded = DateTime.UtcNow
                };

                SetResponse response;
                //USER = tablename, Idno = key(PK ? )
                response = twoBigDB.Set("WALKINORDERS/" + data.orderNo, data);
                WalkInOrders result = response.ResultAs<WalkInOrders>();

                // Set the text of the txtTotalAmount textbox to the calculated total amount
                txtTotalAmount.Text = totalAmount.ToString();

                Response.Write("<script>alert ('Total Amount is: " + data.totalAmount + " PHP!'); </script>");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}