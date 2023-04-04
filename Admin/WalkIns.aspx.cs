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
            try
            {
                // INSERT DATA TO TABLE = WALKINORDERS
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                // Convert input values to numerical format
                int qty;
                decimal price, discount, totalAmount, freeGallon;
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

                // Calculate total amount only if a valid discount value is entered
                if (decimal.TryParse(lblprice.Text, out price) && int.TryParse(txtQty.Text, out qty))
                {
                    // Calculate free gallon if applicable
                    freeGallon = 0;
                    if (qty >= 5)
                    {
                        freeGallon = Math.Floor((decimal)qty / 5);
                    }
                    if (discount > 0 && freeGallon > 0)
                    {
                        Response.Write("<script>alert ('You are eligible for a free gallon!'); </script>");
                    }
                    totalAmount = (qty * price) - discount + freeGallon * price;
                    lblAmount.Text = totalAmount.ToString();
                }
                else
                {
                    Response.Write("<script>alert ('Invalid price or quantity value. Please enter valid decimal numbers.'); </script>");
                    return;
                }

                // Update data with the calculated total amount, discount and adjusted quantity
                var data = new WalkInOrders
                {
                    adminId = int.Parse(idno),
                    orderNo = idnum,
                    productName = drdProdName.SelectedValue,
                    productUnit = drdUnit.SelectedValue,
                    productSize = drdSize.SelectedValue,
                    productPrice = price,
                    productDiscount = discount, // Store the discount value in the database
                    productQty = (int)(qty - freeGallon), // Adjust quantity to account for free gallon
                    totalAmount = totalAmount, // Store calculated total amount as decimal
                    orderType = drdOrderType.Text,
                    dateAdded = DateTime.UtcNow
                };

                SetResponse response;
                //USER = tablename, Idno = key(PK ? )
                response = twoBigDB.Set("WALKINORDERS/" + data.orderNo, data);
                WalkInOrders result = response.ResultAs<WalkInOrders>();

                // Set the text of the txtTotalAmount textbox to the calculated total amount
                lblAmount.Text = totalAmount.ToString();

                Response.Write("<script>alert ('Total Amount is: " + data.totalAmount + " PHP!'); </script>");
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
            drdUnit.Items.Clear();
            drdSize.Items.Clear();

            if (selectedOption == "Refill" || selectedOption == "New Gallon")
            {
                FirebaseResponse response = twoBigDB.Get("PRODUCTREFILL/");
                Dictionary<string, ProductRefill> products = response.ResultAs<Dictionary<string, ProductRefill>>();

                foreach (var product in products.Values)
                {
                    drdProdName.Items.Add(new ListItem(product.pro_refillWaterType));
                    drdUnit.Items.Add(new ListItem(product.pro_refillUnit));
                    drdSize.Items.Add(new ListItem(product.pro_refillSize));
                }


            }
            else if (selectedOption == "Other Products")
            {
                FirebaseResponse response = twoBigDB.Get("otherPRODUCTS/");
                Dictionary<string, otherProducts> otherproducts = response.ResultAs<Dictionary<string, otherProducts>>();

                foreach (var otherproduct in otherproducts.Values)
                {
                    drdProdName.Items.Add(new ListItem(otherproduct.other_productName));
                    drdUnit.Items.Add(new ListItem(otherproduct.other_productUnit));
                    drdSize.Items.Add(new ListItem(otherproduct.other_productSize));
                }
               
            }
            lblDescription.Text = "Choose product type, unit and size:";
            btnSearchPrice.Visible = true;

        }
        //RETRIEVING THE PRICE AND DISCOUNT 
        protected void btnSearchPrice_Click(object sender, EventArgs e)
        {
            string selectedSize = drdSize.SelectedValue;
            string selectedType = drdProdName.SelectedValue;
            string selectedUnit = drdUnit.SelectedValue;

            //if (!string.IsNullOrEmpty(selectedSize))
            //{
            //    if (!string.IsNullOrEmpty(selectedType) && !string.IsNullOrEmpty(selectedUnit))
            //    {
            //        FirebaseResponse response = twoBigDB.Get("otherPRODUCTS/");
            //        Dictionary<string, otherProducts> otherproducts = response.ResultAs<Dictionary<string, otherProducts>>();

            //        otherProducts otherselectedProduct = otherproducts.Values.FirstOrDefault(p => p.other_productSize == selectedSize && p.other_productName == selectedType && p.other_productUnit == selectedUnit);

            //        if (otherselectedProduct != null)
            //        {
            //            // Display the unit for the selected product
            //            if (otherselectedProduct.other_productPrice != null || otherselectedProduct.other_productDiscount != null)
            //            {
            //                lblprice.Text = otherselectedProduct.other_productPrice.ToString();
            //                lblDiscount.Text = otherselectedProduct.other_productDiscount.ToString();
            //            }
            //            else
            //            {
            //                lblprice.Text = "";
            //                lblDiscount.Text = ""; 
            //            }

            //        }
            //        else
            //        {

            //            Response.Write("<script>alert ('No product found with the selected options!');</script>");

            //        }
            //    }
            //    else
            //    {
            //        Response.Write("<script>alert ('Please select the product type and unit');</script>");
            //    }
            //}
            if (!string.IsNullOrEmpty(selectedSize) && !string.IsNullOrEmpty(selectedType) && !string.IsNullOrEmpty(selectedUnit))
            {
                // Check if the product exists in the "otherPRODUCTS" table
                FirebaseResponse response = twoBigDB.Get("otherPRODUCTS/");
                Dictionary<string, otherProducts> otherproducts = response.ResultAs<Dictionary<string, otherProducts>>();
                otherProducts otherselectedProduct = otherproducts.Values.FirstOrDefault(p => p.other_productSize == selectedSize && p.other_productName == selectedType && p.other_productUnit == selectedUnit);

                if (otherselectedProduct != null)
                {
                    // Display the unit for the selected product
                    if (otherselectedProduct.other_productPrice != null)
                    {
                        lblprice.Text = otherselectedProduct.other_productPrice.ToString();
                    }
                    else
                    {
                        lblprice.Text = "";
                    }

                    if (otherselectedProduct.other_productDiscount != null)
                    {
                        lblDiscount.Text = otherselectedProduct.other_productDiscount.ToString();
                    }
                    else
                    {
                        lblDiscount.Text = "0"; // or some default value
                    }
                }
                else
                {
                    // Check if the product exists in the "PRODUCTREFILL" table
                    response = twoBigDB.Get("PRODUCTREFILL/");
                    Dictionary<string, ProductRefill> products = response.ResultAs<Dictionary<string, ProductRefill>>();
                    ProductRefill selectedProduct = products.Values.FirstOrDefault(p => p.pro_refillSize == selectedSize && p.pro_refillWaterType == selectedType && p.pro_refillUnit == selectedUnit);

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

                        if (selectedProduct.pro_discount != null)
                        {
                            lblDiscount.Text = selectedProduct.pro_discount.ToString();
                        }
                        else
                        {
                            lblDiscount.Text = "0"; // or some default value
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
            txtQty.Text = "";
            lblprice.Text = "";
            lblDiscount.Text = "";
            lblAmount.Text = "";
            drdProdName.Items.Clear();
            drdUnit.Items.Clear();
            drdSize.Items.Clear();
        }


    }
}