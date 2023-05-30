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

            
            btnSearchPrice.Visible = false;
        }
        //STORE DATA TO WALKINORDER TBL AND CALCULATE ITS TOTAL AMOUNT
        protected void btnPayment_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            string name = (string)Session["fullname"];
            try
            {
                // INSERT DATA TO TABLE = WALKINORDERS
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                // Convert input values to numerical format
                int qty;
                decimal price, discount, totalAmount, discountAmount, subtotal;
                

                if (!int.TryParse(txtQty.Text, out qty))
                {
                    throw new ArgumentException("Invalid quantity value");
                }
                if (!decimal.TryParse(lblprice.Text, out price))
                {
                    throw new ArgumentException("Invalid price value");
                }
                if (!decimal.TryParse(lblDiscount.Text, out discount))
                {
                    // If the discount value is not a valid decimal, assume it is zero
                    discount = 0;
                }
                else
                { 
                    discount = decimal.Parse(lblDiscount.Text);
                }

                // Calculate total amount only if a valid discount value is entered
                if (decimal.TryParse(lblprice.Text, out price) && int.TryParse(txtQty.Text, out qty))
                {
                    //discount /= 100; // 
                    discountAmount = price * discount * qty;
                    subtotal = price * qty; 
                    totalAmount = subtotal - discountAmount;
                                                            
                    lblDiscount.Text = discountAmount.ToString();
                    lblAmount.Text = totalAmount.ToString();
                }
                else
                {
                    Response.Write("<script>alert ('Invalid price or quantity value. Please enter valid decimal numbers.');  location.reload(); window.location.href = '/Admin/WalkIns.aspx'; </script>");
                    return;
                }

                // Update data with the calculated total amount, discount and adjusted quantity
                var data = new WalkInOrders
                {
                    adminId = int.Parse(idno),
                    orderNo = idnum,
                    productName = drdProdName.SelectedValue,
                    productUnitSize = drdUnit_Size.SelectedValue,
                   // productSize = drdSize.SelectedValue,
                    productPrice = price,
                    productDiscount = discount.ToString(), // Store the discount value in the database
                    productQty = qty, // Adjust quantity to account for free gallon
                    //productQty = (int)(qty - freeGallon), // Adjust quantity to account for free gallon
                    totalAmount = totalAmount, // Store calculated total amount as decimal
                    orderType = drdOrderType.Text,
                    addedBy = name,
                    dateAdded = DateTime.UtcNow
                };

                SetResponse response;
                //USER = tablename, Idno = key(PK ? )
                response = twoBigDB.Set("WALKINORDERS/" + data.orderNo, data);
                WalkInOrders result = response.ResultAs<WalkInOrders>();

                // Set the text of the txtTotalAmount textbox to the calculated total amount
                lblAmount.Text = totalAmount.ToString();

                //int logsId = (int)Session["logsId"];

                // Retrieve the existing Users log object from the database
                //FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
                //UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

               // Get the current date and time
                DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    activityTime = addedTime,
                    userActivity = "CATER WALKIN ORDER",
                };

                //Storing the  info
                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                //response = twoBigDB.Set("USERSLOG/" + log.logsId, log);//Storing data to the database
                //UsersLogs res = response.ResultAs<UsersLogs>();//Database Result

                Response.Write("<script>alert ('Total Amount is: " + data.totalAmount + " PHP!');</script>");
                //Response.Write("<script>alert ('Total Amount is: " + data.totalAmount + " PHP!'); location.reload(); window.location.href = '/Admin/WalkIns.aspx'; </script>");

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        //RETRIEVING THE PRODUCTS USING SEARCH
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];

            string selectedOption = drdOrderType.SelectedValue;

            drdProdName.Items.Clear();
            drdUnit_Size.Items.Clear();
           
            if (selectedOption == "Refill")
            {
                FirebaseResponse response = twoBigDB.Get("PRODUCTREFILL/");
                Dictionary<string, ProductRefill> products = response.ResultAs<Dictionary<string, ProductRefill>>();

                foreach (var product in products.Values)
                {
                    if (product.adminId.ToString() == idno) // check if the product belongs to the current user
                    {
                        drdProdName.Items.Add(new ListItem(product.pro_refillWaterType));
                        drdUnit_Size.Items.Add(new ListItem(product.pro_refillQty + " " + product.pro_refillUnitVolume));
                    }
                    
                }


            }
            else if (selectedOption == "Third Party Products")
            {
                FirebaseResponse response = twoBigDB.Get("thirdparty_PRODUCTS/");
                Dictionary<string, thirdpartyProducts> otherproducts = response.ResultAs<Dictionary<string, thirdpartyProducts>>();

                foreach (var otherproduct in otherproducts.Values)
                {
                    if (otherproduct.adminId.ToString() == idno) // check if the product belongs to the current user
                    {
                        drdProdName.Items.Add(new ListItem(otherproduct.thirdparty_productName));
                        drdUnit_Size.Items.Add(new ListItem(otherproduct.thirdparty_productQty + " " + otherproduct.thirdparty_productUnitVolume));
                        // drdSize.Items.Add(new ListItem(otherproduct.other_productSize));
                    }
                }
               
            }
            lblDescription.Text = "Choose product type, unit and size:";
            btnSearchPrice.Visible = true;

        }
        //RETRIEVING THE PRICE AND DISCOUNT 
        protected void btnSearchPrice_Click(object sender, EventArgs e)
        {
            //string selectedSize = drdSize.SelectedValue;
             string selectedType = drdProdName.SelectedValue;
            //string selectedUnit_Size = drdUnit_Size.SelectedValue;

            decimal discount;

            //if (!string.IsNullOrEmpty(selectedUnit_Size) && !string.IsNullOrEmpty(selectedType))
            if (!string.IsNullOrEmpty(selectedType))
            {
                // Check if the product exists in the "otherPRODUCTS" table
                FirebaseResponse response = twoBigDB.Get("thirdparty_PRODUCTS/");
                Dictionary<string, thirdpartyProducts> otherproducts = response.ResultAs<Dictionary<string, thirdpartyProducts>>();
                thirdpartyProducts otherselectedProduct = otherproducts.Values.FirstOrDefault(p => p.thirdparty_productName == selectedType);


                if (otherselectedProduct != null)
                {
                    // Display the unit for the selected product
                    if (otherselectedProduct.thirdparty_productPrice != null)
                    {
                        lblprice.Text = otherselectedProduct.thirdparty_productPrice.ToString();
                    }
                    else
                    {
                        lblprice.Text = "";
                    }
                    if (!decimal.TryParse(otherselectedProduct.thirdparty_productDiscount.ToString(), out discount))
                    {
                        // If the discount value is not a valid decimal, assume it is zero
                        discount = 0;
                        lblDiscount.Text = discount.ToString();
                    }
                    else
                    {
                        // Convert discount from percentage to decimal
                        discount /= 100;
                        lblDiscount.Text = discount.ToString();
                    }

                    //if (otherselectedProduct.other_productDiscount != null)
                    //{
                    //    lblDiscount.Text = otherselectedProduct.other_productDiscount.ToString();
                    //}
                    //else
                    //{
                    //    lblDiscount.Text = "0"; // or some default value
                    //}
                }
                else
                {
                    // Check if the product exists in the "PRODUCTREFILL" table
                    response = twoBigDB.Get("PRODUCTREFILL/");
                    Dictionary<string, ProductRefill> products = response.ResultAs<Dictionary<string, ProductRefill>>();
                    ProductRefill selectedProduct = products.Values.FirstOrDefault(p => p.pro_refillWaterType.ToString() == selectedType);

                    //ProductRefill selectedProduct = products.Values.FirstOrDefault(p => p.pro_refillSize == selectedSize && p.pro_refillWaterType == selectedType && p.pro_refillUnit == selectedUnit);

                    if (selectedProduct != null)
                    {
                        // Display the unit for the selected product
                        if (selectedProduct.pro_refillPrice != null)
                        {
                            lblprice.Text = selectedProduct.pro_refillPrice.ToString();
                        }
                        else
                        {
                            lblprice.Text = "";
                        }
                        if (!decimal.TryParse(selectedProduct.pro_discount.ToString(), out discount))
                        {
                            // If the discount value is not a valid decimal, assume it is zero
                            discount = 0;
                            lblDiscount.Text = discount.ToString();
                        }
                        else
                        {
                            // Convert discount from percentage to decimal
                            discount /= 100;
                            lblDiscount.Text = discount.ToString();
                        }
                    }
                    else
                    {
                        lblprice.Text = "";
                        lblDiscount.Text = "";
                        // display a message to the user that no product was found
                        var noProductFound = new Label { Text = "No product found with the selected options!", ForeColor = System.Drawing.Color.Red };
                        this.Controls.Add(noProductFound);
                    }
                }
            }
            else
            {
                Response.Write("<script>alert ('Please select the product size, type, and unit');</script>");
            }

            btnSearchPrice.Visible = true;

        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Write("<script> location.reload(); window.location.href = '/Admin/WalkIns.aspx'; </script>");
        }


    }
}