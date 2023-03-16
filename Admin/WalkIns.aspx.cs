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
            string idno = (string)Session["idno"];
            try
            {
                // INSERT DATA TO TABLE = WALKINORDERS
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                // Convert input values to numerical format
                int qty;
                decimal price, discount;
                decimal totalAmount;
                if (!int.TryParse(txtQty.Text, out qty))
                {
                    throw new ArgumentException("Invalid quantity value");
                }
                if (!decimal.TryParse(txtprice.Text, out price))
                {
                    throw new ArgumentException("Invalid price value");
                }
                if (!decimal.TryParse(txtDiscount.Text, out discount))
                {
                    // If the discount value is not a valid decimal, assume it is zero
                    discount = 0;
                }

                // Calculate total amount only if a valid discount value is entered
                if (decimal.TryParse(txtprice.Text, out price) && int.TryParse(txtQty.Text, out qty))
                {
                    totalAmount = (qty * price) - discount;
                    txtTotalAmount.Text = totalAmount.ToString();
                }
                else
                {
                    Response.Write("<script>alert ('Invalid price or quantity value. Please enter valid decimal numbers.'); </script>");
                    return;
                }

                // Calculate total amount
                //decimal totalAmount = (qty * price) - discount;

                var data = new WalkInOrders
                {
                    orderNo = idnum,
                    productName = txtprodName.Text,
                    productSize = txtprodSize.Text,
                    productPrice = price,
                    productDiscount = discount,
                    productQty = qty,
                    totalAmount = totalAmount, // Store calculated total amount as decimal
                    orderType = drdOrderType.Text,
                    dateAdded = DateTime.UtcNow
                };

                SetResponse response;
                //USER = tablename, Idno = key(PK ? )
                response = twoBigDB.Set("ADMIN/" + idno + "/WalkInOrders/" + data.orderNo, data);
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