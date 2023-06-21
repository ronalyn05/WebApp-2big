using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Firebase.Storage;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WRS2big_Web.Model;


namespace WRS2big_Web.Admin
{
    public partial class WaterProduct : System.Web.UI.Page
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

            //radDevType.Attributes.Add("onclick", "showHideExpressDiv();");

            if (!IsPostBack)
            {
                //productRefillDisplay();
                //otherProductsDisplay();
                //deliveryExpressDisplay();
                //displayTankSupply();
                displayTankSupply();
                
            }
            //btnSearchOrder.Visible = false;
            //txtSearch.Visible = false;
            //btnSearchThirdParty.Visible = false;
            //txtSearchThirdParty.Visible = false;
           
            string idno = (string)Session["idno"];

            FirebaseResponse response = twoBigDB.Get("PRODUCTS");
            Dictionary<string, Products> productrefillList = response.ResultAs<Dictionary<string, Products>>();

            if (response != null && productrefillList != null)
            {
                var filteredList = productrefillList.Values.Where(d => d.adminId.ToString() == idno).OrderByDescending(d => d.dateAdded);

                foreach (var entry in filteredList)
                {
                    // Check if the product name is "empty gallon", "EMPTY GALLON", or "Empty Gallon"
                    if (entry.productType.ToLower() == "empty gallon")
                    {
                        // Subtract the product quantity from the product stock quantity
                        decimal stockQty;
                        decimal stockBalance = 0;
                        if (decimal.TryParse(entry.productStockQty, out stockQty))
                        {
                            stockBalance = stockQty - decimal.Parse(entry.productQty);
                            entry.productStockBalance = stockBalance.ToString();

                            // Store the updated stock balance in the database
                            Products products = entry;
                            products.productStockBalance = entry.productStockBalance;
                            twoBigDB.Update("PRODUCTS/" + products.productId, products);
                        }
                    }
                    else
                    {
                        decimal stockQty;
                        decimal refillQty;
                        decimal stockBalance = 0;

                        if (decimal.TryParse(entry.productStockQty, out stockQty) && decimal.TryParse(entry.productQty, out refillQty))
                        {
                            // Check the unit of volume
                            string refillUnit = entry.product_UnitOfVolume.ToLower();
                            string stockUnit = entry.productStockUnit.ToLower();

                            // Convert refill quantity to gallons if needed
                            if (refillUnit == "liters")
                            {
                                refillQty /= 3.78541m; // Convert liters to gallons
                            }
                            else if (refillUnit == "mililiters")
                            {
                                refillQty /= 3785.41m; // Convert milliliters to gallons
                            }

                            // Convert stock quantity to gallons if needed
                            if (stockUnit == "liters")
                            {
                                stockQty /= 3.78541m; // Convert liters to gallons
                            }
                            else if (stockUnit == "mililiters")
                            {
                                stockQty /= 3785.41m; // Convert milliliters to gallons
                            }

                            stockBalance = stockQty - refillQty;
                            entry.productStockBalance = (stockBalance < 0) ? "0" : stockBalance.ToString("0.00"); // Convert the balance to gallons and format it

                            // Store the updated stock balance in the database
                            Products productRefill = entry;
                            productRefill.productStockBalance = entry.productStockBalance;
                            twoBigDB.Update("PRODUCTREFILL/" + productRefill.productId, productRefill);
                        }
                        else
                        {
                            // Handle parsing errors for stock quantity or refill quantity
                            Console.WriteLine("Invalid stock quantity or refill quantity for entry with ID: " + entry.productId);
                        }
                    }
                }
            }
        }
       
        //DISPLAY TANK SUPPLY NI DIRI
        private void displayTankSupply()
        {

            // Get the ID of the currently logged-in owner from session state
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the TANKSUPPLY table
                FirebaseResponse response = twoBigDB.Get("TANKSUPPLY");
                Dictionary<string, TankSupply> supply = response.ResultAs<Dictionary<string, TankSupply>>();

                if (supply != null)
                {
                    // Filter the list of orders by the owner's ID and the order status and delivery type
                    var filteredSupply = supply.Values.FirstOrDefault(d => d.adminId.ToString() == idno && d.dateAdded.Date == DateTime.Now.Date);

                    if (filteredSupply != null)
                    {
                        lblDate.Text = filteredSupply.dateAdded.ToString("MMMM/dd/yyyy hh:mm:ss tt");
                        lbltankSupply.Text = filteredSupply.tankVolume.ToString() + ' ' + filteredSupply.tankUnit.ToString();

                        // Retrieve all orders from the ORDERS table
                        FirebaseResponse responseOrder = twoBigDB.Get("ORDERS");
                        Dictionary<string, Order> orders = responseOrder.ResultAs<Dictionary<string, Order>>();

                        // Retrieve all walk-in orders from the WALKINORDERS table
                        FirebaseResponse walkinResponse = twoBigDB.Get("WALKINORDERS");
                        Dictionary<string, WalkInOrders> walkinOrders = walkinResponse.ResultAs<Dictionary<string, WalkInOrders>>();

                        // Convert tank capacity to gallons if it's not already in gallons
                        double tankCapacity = 0;
                        if (!string.IsNullOrEmpty(filteredSupply.tankVolume) && double.TryParse(filteredSupply.tankVolume, out double volume))
                        {
                            tankCapacity = volume;
                            if (filteredSupply.tankUnit == "L" || filteredSupply.tankUnit == "liter/s")
                            {
                                double gallonsPerLiter = 0.26417205236; // conversion factor from gallon to liters
                                tankCapacity *= gallonsPerLiter;
                            }
                            else if (filteredSupply.tankUnit == "mL" || filteredSupply.tankUnit == "ML" || filteredSupply.tankUnit == "milliliters")
                            {
                                double gallonsPerML = 3785.41; // conversion factor from gallons to milliliters
                                tankCapacity /= gallonsPerML;
                            }
                        }

                        // Calculate the total gallons ordered
                        double totalOrderedGallons = 0;
                        //Calculate the total gallons declined
                        double totalDeclinedGallons = 0;
                        if (orders != null)
                        {
                            var filteredOrders = orders.Values.Where(d => d.admin_ID.ToString() == idno &&
                                                (d.order_OrderStatus == "Delivered" || d.order_OrderStatus == "Accepted" || d.order_OrderStatus == "Pending" || d.order_OrderStatus == "Out for Delivery" || d.order_OrderStatus == "Payment Received"));

                            foreach (Order order in filteredOrders)
                            {
                                double orderedGallons = 0;
                                if (order.order_Products[0].pro_refillUnitVolume == "gallon")
                                {
                                    orderedGallons = double.Parse(order.order_Products[0].qtyPerItem);
                                }
                                else if (order.order_Products[0].pro_refillUnitVolume == "L" || order.order_Products[0].pro_refillUnitVolume == "liter/s")
                                {
                                    double gallonsPerLiter = 3.78541; // conversion factor from gallon to liters
                                    orderedGallons = double.Parse(order.order_Products[0].qtyPerItem) * gallonsPerLiter;
                                }
                                else if (order.order_Products[0].pro_refillUnitVolume == "mL" || order.order_Products[0].pro_refillUnitVolume == "ML" || order.order_Products[0].pro_refillUnitVolume == "milliliters")
                                {
                                    double gallonsPerML = 0.00026417205236; // conversion factor from gallons to milliliters
                                    //orderedGallons = order.order_Products[0].order_size / gallonsPerML;
                                    orderedGallons = double.Parse(order.order_Products[0].qtyPerItem) / gallonsPerML;

                                }

                                //Get the total of ordered gallons 
                                totalOrderedGallons += orderedGallons * order.order_overAllQuantities;

                            }

                            var declinedOrders = orders.Values.Where(d => d.admin_ID.ToString() == idno && d.order_OrderStatus == "Declined");
                            foreach (Order order in declinedOrders)
                            {
                                double declinedGallons = 0;
                                if (order.order_Products[0].pro_refillUnitVolume == "gallon/s")
                                {
                                    declinedGallons = double.Parse(order.order_Products[0].qtyPerItem);
                                }
                                else if (order.order_Products[0].pro_refillUnitVolume == "L" || order.order_Products[0].pro_refillUnitVolume == "liter/s")
                                {
                                    double gallonsPerLiter = 3.78541; // conversion factor from gallon to liters
                                    declinedGallons = double.Parse(order.order_Products[0].qtyPerItem) * gallonsPerLiter;
                                }
                                else if (order.order_Products[0].pro_refillUnitVolume == "mL" || order.order_Products[0].pro_refillUnitVolume == "ML" || order.order_Products[0].pro_refillUnitVolume == "milliliters")
                                {
                                    double gallonsPerML = 0.00026417205236; // conversion factor from gallons to milliliters
                                    declinedGallons = double.Parse(order.order_Products[0].qtyPerItem) / gallonsPerML;
                                }
                                // Get the total of declined gallons
                                totalDeclinedGallons += declinedGallons * order.order_overAllQuantities;
                            }
                        }
                        

                        // Update the tank supply and remaining balance
                        //double remainingGallons = tankCapacity + totalOrderedGallons - totalDeclinedGallons;
                        //double remainingBalance = remainingGallons * filteredSupply.pricePerGallon;

                        //lblremainingGallons.Text = remainingGallons.ToString() + " gallons";
                        //lblRemainingBalance.Text = remainingBalance.ToString("C");

                        //Calculate the ordered gallons of walkin
                        if (walkinOrders != null)
                        {
                            var filteredWalkinOrders = walkinOrders.Values.Where(d => d.adminId.ToString() == idno);
                            foreach (WalkInOrders order in filteredWalkinOrders)
                            {
                                // Convert order size to gallons or milliliters based on order unit
                                double orderedGallons = 0;
                                if (order.productUnit == "L" || order.productUnit == "liter/s")
                                {
                                    double gallonsPerLiter = 0.26417205236; // conversion factor from gallon to liters
                                    orderedGallons = Double.Parse(order.productSize) * gallonsPerLiter;
                                }
                                else if (order.productUnit == "mL" || order.productUnit == "ML" || order.productUnit == "milliliters")
                                {
                                    double gallonsPerML = 3785.41; // conversion factor from gallons to milliliters
                                    orderedGallons = Double.Parse(order.productSize) / gallonsPerML;
                                }
                                //Get the total of ordered gallons 
                                totalOrderedGallons += orderedGallons * order.productQty;
                            }
                        }

                        //Get the total of remaining supply base on each ordered place
                        double remainingSupply = tankCapacity - totalOrderedGallons + totalDeclinedGallons;
                        //double remainingSupply = tankCapacity - totalOrderedGallons;

                        // display the remaining supply
                        if (remainingSupply < 0)
                        {
                            remainingSupply = 0;

                            TankSupply tankSupply = new TankSupply
                            {
                                tankId = filteredSupply.tankId,
                                adminId = filteredSupply.adminId,
                                dateAdded = filteredSupply.dateAdded,
                                tankVolume = filteredSupply.tankVolume,
                                tankUnit = filteredSupply.tankUnit,
                                tankBalance = remainingSupply.ToString() + ' ' + "gallons", // Update the remaining supply field
                                dateUpdated = DateTime.Now
                            };

                            FirebaseResponse tankResponse = twoBigDB.Update("TANKSUPPLY/" + filteredSupply.tankId, tankSupply);

                            lblremainingSupply.Text = "There is no remaining supply. Currently unable to fullfill any further orders.";

                        }
                        else
                        {
                            lblremainingSupply.Text = remainingSupply.ToString("N2") + ' ' + "gallons";


                            // Update the remaining supply in the TANKSUPPLY table
                            TankSupply tankSupply = new TankSupply
                            {
                                tankId = filteredSupply.tankId,
                                adminId = filteredSupply.adminId,
                                dateAdded = filteredSupply.dateAdded,
                                tankVolume = filteredSupply.tankVolume,
                                tankUnit = filteredSupply.tankUnit,
                                addedBy = filteredSupply.addedBy,
                                tankBalance = remainingSupply.ToString("N2") + ' ' + "gallons", // Update the remaining supply field
                                dateUpdated = DateTime.Now
                            };

                            FirebaseResponse tankResponse = twoBigDB.Update("TANKSUPPLY/" + filteredSupply.tankId, tankSupply);

                        }
                    }
                    else
                    {
                        lblDate.Text = "";
                        lbltankSupply.Text = "No supply found for today.";
                        lblremainingSupply.Text = "";
                    }
                }
                else
                {
                    lblDate.Text = "";
                    lbltankSupply.Text = "No tank supply records found.";
                    lblremainingSupply.Text = "";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred while retrieving tank supply data: " + ex.Message + "'); window.location.href = '/Admin/WaterProduct.aspx';</script>");
            }
        }

        //RETRIEVE PRODUCTREFILL DATA
        private void productRefillDisplay()
        {
            string idno = (string)Session["idno"];
            decimal discount;

            // Retrieve all products from the database
            FirebaseResponse response = twoBigDB.Get("PRODUCTS");
            Dictionary<string, Products> productList = response.ResultAs<Dictionary<string, Products>>();

            // Create the DataTable to hold the products
            DataTable productTable = new DataTable();
            productTable.Columns.Add("PRODUCT ID");
            productTable.Columns.Add("PRODUCT NAME");
            productTable.Columns.Add("PRODUCT QUANTITY");
            productTable.Columns.Add("PRODUCT UNIT OF VOLUME");
            productTable.Columns.Add("PRODUCT PRICE");
            productTable.Columns.Add("PRODUCT DISCOUNT");
            productTable.Columns.Add("PRODUCT STOCK");
            productTable.Columns.Add("DATE ADDED");
            productTable.Columns.Add("ADDED BY");
            productTable.Columns.Add("DATE UPDATED");
            productTable.Columns.Add("UPDATED BY");

            if (response != null && response.ResultAs<Dictionary<string, Products>>() != null)
            {
                var filteredList = productList.Values
                    .Where(d => d.adminId.ToString() == idno && (d.offerType == "Product Refill" || d.offerType == "other Product"))
                    .OrderByDescending(d => d.dateAdded);

                // Loop through the products and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    if (!decimal.TryParse(entry.productDiscount, out discount))
                    {
                        // If the discount value is not a valid decimal, assume it is zero
                        discount = 0;
                    }
                    else
                    {
                        // Convert discount from percentage to decimal
                        discount /= 100;
                    }

                    string dateAdded = entry.dateAdded == DateTime.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateUpdated = entry.dateUpdated == DateTime.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    productTable.Rows.Add(
                        entry.productId,
                        entry.productType,
                        entry.productQty,
                        entry.product_UnitOfVolume,
                        entry.productPrice,
                        discount,
                        entry.productStockQty + " " + entry.productStockUnit,
                        dateAdded,
                        entry.addedBy,
                        dateUpdated,
                        entry.updatedBy);
                }
            }
            else
            {
                // Handle null response or invalid selected value
                productTable.Rows.Add("No data found", "", "", "", "", "", "", "", "", "");
            }

            // Bind the DataTable to the GridView
            gridProductRefill.DataSource = productTable;
            gridProductRefill.DataBind();
        }
        //RETRIEVE OTHERPRODUCT DATA
        private void thirdpartyProductsDisplay()
        {
            string idno = (string)Session["idno"];
            decimal discount;

            // Retrieve all products from the database
            FirebaseResponse response = twoBigDB.Get("PRODUCTS");
            Dictionary<string, Products> productsList = response.ResultAs<Dictionary<string, Products>>();

            // Create the DataTable to hold the third-party product report
            DataTable thirdPartyProductTable = new DataTable();
            thirdPartyProductTable.Columns.Add("PRODUCT ID");
            thirdPartyProductTable.Columns.Add("PRODUCT NAME");
            thirdPartyProductTable.Columns.Add("PRODUCT QUANTITY");
            thirdPartyProductTable.Columns.Add("PRODUCT UNIT OF VOLUME");
            thirdPartyProductTable.Columns.Add("PRODUCT PRICE");
            thirdPartyProductTable.Columns.Add("PRODUCT STOCK");
            thirdPartyProductTable.Columns.Add("PRODUCT DISCOUNT");
            thirdPartyProductTable.Columns.Add("DATE ADDED");
            thirdPartyProductTable.Columns.Add("ADDED BY");
            thirdPartyProductTable.Columns.Add("DATE UPDATED");
            thirdPartyProductTable.Columns.Add("UPDATED BY");

            if (response != null && response.ResultAs<Dictionary<string, Products>>() != null)
            {
                var filteredList = productsList.Values
                    .Where(d => d.adminId.ToString() == idno && d.offerType == "thirdparty product")
                    .OrderByDescending(d => d.dateAdded);

                // Loop through the products and add the third-party ones to the DataTable
                foreach (var entry in filteredList)
                {
                    if (!decimal.TryParse(entry.productDiscount, out discount))
                    {
                        // If the discount value is not a valid decimal, assume it is zero
                        discount = 0;
                    }
                    else
                    {
                        // Convert discount from percentage to decimal
                        discount /= 100;
                    }

                    string dateAdded = entry.dateAdded == DateTime.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateUpdated = entry.dateUpdated == DateTime.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    thirdPartyProductTable.Rows.Add(
                        entry.productId,
                        entry.productType,
                        entry.productQty,
                        entry.product_UnitOfVolume,
                        entry.productPrice,
                        entry.productStockQty + " " + entry.productStockUnit,
                        discount,
                        dateAdded,
                        entry.addedBy,
                        dateUpdated,
                        entry.updatedBy);
                }
            }
            else
            {
                // Handle null response or invalid selected value
                thirdPartyProductTable.Rows.Add("No data found", "", "", "", "", "", "", "", "", "");
            }

            // Bind the DataTable to the GridView
            gridotherProduct.DataSource = thirdPartyProductTable;
            gridotherProduct.DataBind();
        }

        //RETRIEVE TANK SUPPLY
        private void tankSupplyDisplay()
        {
            string idno = (string)Session["idno"];

            // Retrieve all orders from the TANKSUPPLY table
            FirebaseResponse response = twoBigDB.Get("TANKSUPPLY");
            Dictionary<string, TankSupply> otherproductsList = response.ResultAs<Dictionary<string, TankSupply>>();
          
            // Create the DataTable to hold the tank supply report
            //sa pag create sa table
            DataTable tankSupplyTable = new DataTable();
            tankSupplyTable.Columns.Add("TANK ID");
            tankSupplyTable.Columns.Add("TANK SUPPLY");
            tankSupplyTable.Columns.Add("TANK BALANCE");
            tankSupplyTable.Columns.Add("DATE ADDED");
            tankSupplyTable.Columns.Add("ADDED BY");

            if (response != null && response.ResultAs<TankSupply>() != null)
            {
                var filteredList = otherproductsList.Values.Where(d => d.adminId.ToString() == idno).OrderByDescending(d => d.dateAdded);

                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    string dateAdded = entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    if (DateTime.Parse(dateAdded) == DateTime.MinValue)
                    {
                        dateAdded = " ";
                    }


                    tankSupplyTable.Rows.Add(entry.tankId, entry.tankVolume + " " + entry.tankUnit, entry.tankBalance, dateAdded, entry.addedBy);
                }
            }
            else
            {
                // Handle null response or invalid selected value
                tankSupplyTable.Rows.Add("No data found", "", "", "", "", "", "");
            }

            // Bind the DataTable to the GridView
            gridTankSupply.DataSource = tankSupplyTable;
            gridTankSupply.DataBind();



        }
        //STORE TANK SUPPLY
        protected void btnAddSupply_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            //int logsId = (int)Session["logsId"];
            string name = (string)Session["fullname"];

            try
            {
                // Check if a record with the same adminId and dateAdded exists in the TANKSUPPLY table
                FirebaseResponse res = twoBigDB.Get("TANKSUPPLY");
                Dictionary<string, TankSupply> supply = res.ResultAs<Dictionary<string, TankSupply>>();

                if (supply != null) // Add this null check
                {
                    // Filter the list of orders by the owner's ID and the order status and delivery type
                    var filteredList = supply.Values.FirstOrDefault(d => d.adminId.ToString() == idno && d.dateAdded.Date == DateTime.Now.Date);

                    if (filteredList != null)
                    {
                        // A record with the same adminId and dateAdded already exists
                        lblDate.Text = filteredList.dateAdded.ToString("MM/dd/yyyy hh:mm:ss tt");
                        lbltankSupply.Text = filteredList.tankVolume.ToString() + ' ' + filteredList.tankUnit.ToString();
                        Response.Write("<script>alert ('A tank supply record for today already exists!');</script>");
                        return;
                    }

                }
                // INSERT DATA TO TABLE = TANKSUPPLY
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                // Check if the refillDiscount field is empty
                int discount = 0;
                if (!string.IsNullOrEmpty(refillDiscount.Text))
                {
                    discount = int.Parse(refillDiscount.Text);
                }

                var data = new TankSupply
                {
                    adminId = adminId,
                    tankId = idnum,
                    tankUnit = drdTankUnit.SelectedValue,
                    tankVolume = tankSize.Text,
                    addedBy = name,
                    dateAdded = DateTime.Now
                };

                SetResponse response;
                response = twoBigDB.Set("TANKSUPPLY/" + data.tankId, data);
                TankSupply result = response.ResultAs<TankSupply>();

                Response.Write("<script>alert ('Tank supply for today with id number: " + data.tankId + " is successfully added!'); location.reload(); window.location.href = '/Admin/WaterProduct.aspx';</script>");

                // Retrieve the existing Users log object from the database
                FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS");
                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    role = "Admin",
                    activityTime = addedTime,
                    userActivity = "ADDED TANK SUPPLY",
                };

                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);


                // Display the tank supply result here
                lblDate.Text = data.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                lbltankSupply.Text = data.tankVolume + ' ' + data.tankUnit;

                tankSize.Text = null;

                //gridTankSupply.DataSource = null;
                //gridTankSupply.DataBind();
                //GridotherProduct_Details.DataSource = null;
                //GridotherProduct_Details.DataBind();
                //GridPro_Details.DataSource = null;
                //GridPro_Details.DataBind();

                gridProductRefill.Visible = false;
                gridotherProduct.Visible = false;
                lblThirdparty.Visible = false;
                lbl_tankSupply.Visible = false;


                gridTankSupply.Visible = true;
                tankSupplyDisplay();
                lblProductData.Text = "TANK SUPPLY";
                lblProductData.Visible = true;
            }

            catch (Exception ex)
            {
                Response.Write("<script>alert('Error adding tank supply! " + ex.Message + "');</script>");
            }
        }

        //STORING DATA TO third party product
        protected async void btnAdd_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            //int logsId = (int)Session["logsId"];
            string name = (string)Session["fullname"];

            try
            {
                // INSERT DATA TO TABLE = otherPRODUCT
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                // Check if the product Discounts field is empty
                string discount = string.Empty;

                if (!string.IsNullOrEmpty(productDiscounts.Text))
                {
                    discount = productDiscounts.Text;
                }

                string productStockUnit = string.Empty;

                if (drdUnitStock.SelectedValue == "bottle")
                {
                    productStockUnit = drdUnitStock.SelectedValue;
                }
                else if (drdUnitStock.SelectedValue == "gallon")
                {
                    productStockUnit = drdUnitStock.SelectedValue;
                }
                else if (drdUnitStock.SelectedValue == "box/case")
                {
                    productStockUnit = drdUnitStock.SelectedValue;
                }
                else
                {
                    productStockUnit = string.Empty;
                }
                string productStockQty = string.Empty;
                if (!string.IsNullOrEmpty(stockQuantity.Text))
                {
                    productStockQty = stockQuantity.Text;
                }

                byte[] fileBytes = null;

                // Check if the uploaded file is valid and meets the requirements
                if (imgProduct.HasFile)
                {
                    // Get the file extension
                    string fileExtension = Path.GetExtension(imgProduct.FileName);

                    //// Check if the file extension is allowed (JPG)
                    //if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg")
                    //{
                    // Check if the file extension is allowed (JPG)
                    if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".bmp" || fileExtension.ToLower() == ".svg" || fileExtension.ToLower() == ".gif")
                    {
                        // Check if the file extension is allowed
                        //if (fileExtension.ToLower() != ".pdf" && fileExtension.ToLower() != ".pptx" && fileExtension.ToLower() != ".docx" && fileExtension.ToLower() != ".xlsx")
                        //{
                        //        //Check if the file size is within the allowed limit
                        int maxFileSizeInBytes = 5 * 1024 * 1024; // 5MB
                        if (imgProduct.FileContent.Length <= maxFileSizeInBytes)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                imgProduct.PostedFile.InputStream.CopyTo(memoryStream);
                                fileBytes = memoryStream.ToArray();
                            }

                            var data = new Products
                            {
                                adminId = adminId,
                                productId = idnum,
                                offerType = "thirdparty product",
                                productType = productName.Text.ToLower(),
                                product_UnitOfVolume = drdprodUnitVolume.SelectedValue,
                                productQty = productQty.Text,
                                productPrice = productPrice.Text,
                                productDiscount = discount,
                                productStockUnit = productStockUnit,
                                productStockQty = productStockQty,
                                productImage = null,
                                addedBy = name,
                                dateAdded = DateTime.UtcNow

                            }; 
                                // Upload the image to the storage
                                var storage = new FirebaseStorage("big-system-64b55.appspot.com");
                                var filePath = $"thirdpartyProduct_images/{data.productId}{fileExtension}";

                                using (var stream = new MemoryStream(fileBytes))
                                {
                                    var storageTask = storage.Child(filePath).PutAsync(stream);
                                    var downloadUrl = await storageTask;
                                    data.productImage = downloadUrl;
                                }

                            SetResponse response;
                            //USER = tablename, Idno = key(PK ? )
                            // response = twoBigDB.Set("ADMIN/" + idno + "/Product/" + data.productId, data);
                            response = twoBigDB.Set("PRODUCTS/" + data.productId, data);
                            Products result = response.ResultAs<Products>();
                            Response.Write("<script>alert ('Third party product offers with Id number: " + data.productId + " is successfully added!'); </script>");
                          
                        }
                        else
                        {
                            //lblErrorMessage.Text = "File size should not exceed 5MB.";
                            Response.Write("<script>alert('File size should not exceed 5MB.'); </script>");
                            return;
                        }
                }
                else
                {
                    //lblError.Text = "Only JPG files are allowed.";
                    Response.Write("<script>alert('Only image files are allowed such as jpg or jpeg, png, gif, bmp, and svg !'); </script>");
                    return;
                }
            }
                else
                {
                    // No image uploaded, handle accordingly
                    Response.Write("<script>alert('No image uploaded, please upload one'); </script>");
                    //lblErrorMessage.Text = "No image uploaded, please upload one";
                }
                
                productName.Text = null;
                productQty.Text = null;
                productPrice.Text = null;
                drdprodUnitVolume.SelectedValue = null;
                productDiscounts.Text = null;
                stockQuantity.Text = null;
                drdUnitStock.SelectedValue = null;

                // Retrieve the existing Users log object from the database
                FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS");
                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    role = "Admin",
                    userActivity = "ADDED THIRD PARTY PRODUCT",
                    activityTime = addedTime
                };

                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                //GridotherProduct_Details.DataSource = null;
                //GridotherProduct_Details.DataBind();
                //GridPro_Details.DataSource = null;
                //GridPro_Details.DataBind();
                //gridTankSupply.DataSource = null;
                //gridTankSupply.DataBind();

                gridProductRefill.Visible = false;
                gridTankSupply.Visible = false;
                lblThirdparty.Visible = false;
                lbl_tankSupply.Visible = false;

                gridotherProduct.Visible = true;
                thirdpartyProductsDisplay();

                lblProductData.Text = "THIRDPARTY PRODUCT";
                lblProductData.Visible = true;
                //txtSearchThirdParty.Visible = true;
                //btnSearchThirdParty.Visible = true;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Data already exist'); location.reload(); window.location.href = '/Admin/WaterProduct.aspx';" + ex.Message);
            }
        }
       
        //STORING DATA TO PRODUCT REFILL and other product offered
        protected async void btnSet_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            //int logsId = (int)Session["logsId"];
            string name = (string)Session["fullname"];

            try
            {
                // INSERT DATA TO TABLE  
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                // Check if the productDiscounts field is empty
                string discount = " ";
                if (!string.IsNullOrEmpty(refillDiscount.Text))
                {
                    discount = refillDiscount.Text;
                }
                string productStockUnit = " ";
                if (drdProductStock.SelectedValue == "bottle")
                {
                    productStockUnit = drdProductStock.SelectedValue;
                }
               else if(drdProductStock.SelectedValue == "gallon")
                {
                    productStockUnit = drdProductStock.SelectedValue;
                }
                else if (drdProductStock.SelectedValue == "box/case")
                {
                    productStockUnit = drdProductStock.SelectedValue;
                }
                else
                {
                    productStockUnit = " ";
                }
                string refillunit = " ";
                if (refillUnitOfVolume.SelectedValue == "gallon")
                {
                    refillunit = refillUnitOfVolume.SelectedValue;
                }
                else if (refillUnitOfVolume.SelectedValue == "liter/s")
                {
                    refillunit = refillUnitOfVolume.SelectedValue;
                }
                else if (refillUnitOfVolume.SelectedValue == "mililiter/s")
                {
                    refillunit = refillUnitOfVolume.SelectedValue;
                }
                else
                {
                    refillunit = " ";
                }
                string offerType_selectedValues = "";
                foreach (ListItem item in radioType_productoffered.Items)
                {
                    if (item.Selected)
                    {
                        offerType_selectedValues += item.Value.Trim() + ",";
                    }
                }

                // Remove the trailing comma if there are any selected values
                if (!string.IsNullOrEmpty(offerType_selectedValues))
                {
                    offerType_selectedValues = offerType_selectedValues.TrimEnd(',');
                }

                string productStockQty = " ";
                if (!string.IsNullOrEmpty(txtStockQty.Text))
                {
                    productStockQty = txtStockQty.Text;
                }
               
                //UPLOADING THE IMAGE TO THE STORAGE
                byte[] fileBytes = null;
              
                // Check if the uploaded file is valid and meets the requirements
                if (prodImage.HasFile)
                {
                    // Get the file extension
                    string fileExtension = Path.GetExtension(prodImage.FileName);

                    // Check if the file extension is allowed (JPG)
                    if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".bmp" || fileExtension.ToLower() == ".svg" || fileExtension.ToLower() == ".gif")
                    {
                        // Check if the file size is within the allowed limit
                        int maxFileSizeInBytes = 5 * 1024 * 1024; // 5MB

                        if (prodImage.FileContent.Length <= maxFileSizeInBytes)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                prodImage.PostedFile.InputStream.CopyTo(memoryStream);
                                fileBytes = memoryStream.ToArray();
                            }

                            var data = new Products
                            {
                                productId = idnum,
                                adminId = adminId,
                                offerType = offerType_selectedValues,
                                productType = refillwaterType.Text.ToLower(),
                                productImage = null,
                                product_UnitOfVolume = refillUnitOfVolume.SelectedValue,
                                productDiscount = discount,
                                productQty = refillQty.Text,
                                productPrice = refillPrice.Text,
                                productStockUnit = productStockUnit,
                                productStockQty = productStockQty,
                                addedBy = name,
                                dateAdded = DateTime.Now
                            };

                                // Upload the image to the storage
                                var storage = new FirebaseStorage("big-system-64b55.appspot.com");
                                var filePath = $"productRefill_images/{data.productId}{fileExtension}";

                                using (var stream = new MemoryStream(fileBytes))
                                {
                                    var storageTask = storage.Child(filePath).PutAsync(stream);
                                    var downloadUrl = await storageTask;
                                    data.productImage = downloadUrl;
                                }

                                SetResponse response;
                                //USER = tablename, Idno = key(PK ? )
                                // response = twoBigDB.Set("Product/" + idno + "/Product/" + data.productId, data);
                                response = twoBigDB.Set("PRODUCTS/" + data.productId, data);
                                ProductRefill result = response.ResultAs<ProductRefill>();
                                Response.Write("<script>alert ('Product with Id number: " + data.productId + " is successfully added!'); </script>");
                          
                        }
                        else
                        {
                            //lblError.Text = "File size should not exceed 5MB.";
                            Response.Write("<script>alert('File size should not exceed 5MB.'); </script>");
                            return;
                        }
                    }
                    else
                    {
                        //lblError.Text = "Only JPG files are allowed.";
                        Response.Write("<script>alert('Only image files are allowed such as jpg or jpeg, png, gif, bmp, and svg !'); </script>");
                        return;
                    }
                }
                else
                {
                    // No image uploaded, handle accordingly
                    //lblError.Text = "No image uploaded, please upload one";
                    Response.Write("<script>alert('No image uploaded, please upload one.'); </script>");
                }
               
                refillwaterType.Text = null;
                refillDiscount.Text = null;
                refillQty.Text = null;
                refillUnitOfVolume.SelectedValue = null;
                refillPrice.Text = null;
                drdProductStock.SelectedValue = null;
                txtStockQty.Text = null;
               
                // Retrieve the existing Users log object from the database
                FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS");
                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    role = "Admin",
                    userActivity = "ADDED PRODUCT REFILL",
                    activityTime = addedTime
                };

                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                
                //GridPro_Details.DataSource = null;
                //GridPro_Details.DataBind();
                //GridotherProduct_Details.DataSource = null;
                //GridotherProduct_Details.DataBind();
                //gridTankSupply.DataSource = null;
                //gridTankSupply.DataBind();
                gridProductRefill.Visible = true;
                gridTankSupply.Visible = false;
                gridotherProduct.Visible = false;
                lblThirdparty.Visible = false; 
                lbl_tankSupply.Visible = false;

                productRefillDisplay();
                lblProductData.Text = "PRODUCT REFILL";
                lblProductData.Visible = true;
                btnSearchOrder.Visible = true;
                txtSearch.Visible = true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        //STORE THE PRODUCT ID FOR LATER USE AND UPDATE 
        protected void btnEditProduct_Click(object sender, EventArgs e)
        {
            // Retrieve the button that was clicked
            Button btnEditProduct = (Button)sender;

            // Find the GridView row containing the button
            GridViewRow row = (GridViewRow)btnEditProduct.NamingContainer;

            // Get the product ID from the specific column
            int productIDColumnIndex = 1; //  the actual column index of the product ID
            int productID = int.Parse(row.Cells[productIDColumnIndex].Text);

            // Store the product ID in a hidden field for later use
            hfeditProductDetails.Value = productID.ToString();

            // Retrieve the existing product object from the database using the productID entered
            FirebaseResponse response = twoBigDB.Get("PRODUCTS/" + productID);
            Products existingproduct = response.ResultAs<Products>();

            if (response == null || existingproduct == null)
            {
                // Show error message if the response or existingproduct is null
                Response.Write("<script>alert ('An error occurred while retrieving the product details.');</script>");
                return;
            }

            // Assign the values to the UI controls
            txtproductprice.Text = existingproduct.productPrice;
            txtproductdiscount.Text = existingproduct.productDiscount;


            // Store the product ID in a hidden field for later use
            hfeditProductDetails.Value = productID.ToString();

            // Show the modal popup
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editproduct", "$('#editproduct').modal('show');", true);
        }
        //PERFORM THE PRODUCT DETAILS ID TO UPDATE HERE
        protected void btnEditProductDetails_Click(object sender, EventArgs e)
        {
            //  generate a random number for employee logged
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            string name = (string)Session["fullname"];

            // Get the product ID from the hidden field
            int productID = int.Parse(hfeditProductDetails.Value);

            // Get the new price and discount from the DropDownList in the modal popup
            string newprice = txtproductprice.Text;
            string newdiscount = txtproductdiscount.Text;
          

            try
            {

                // Retrieve the existing product object from the database using the productID entered
                FirebaseResponse response = twoBigDB.Get("PRODUCTS/" + productID);
                Products existingproduct = response.ResultAs<Products>();

                if (response == null || existingproduct == null)
                {
                    // Show error message if the response or existingproduct is null
                    Response.Write("<script>alert ('An error occurred while retreiving the product details.');</script>");
                    return;
                }

                // Update the fields that have changed
                if (!string.IsNullOrEmpty(newdiscount) && newdiscount != existingproduct.productDiscount)
                {
                    existingproduct.productDiscount = newdiscount;
                    existingproduct.dateUpdated = DateTime.Now;
                    existingproduct.updatedBy = name;
                }
               if (!string.IsNullOrEmpty(newprice) && newprice != existingproduct.productPrice)
                {
                    existingproduct.productPrice = newprice;
                    existingproduct.dateUpdated = DateTime.Now;
                    existingproduct.updatedBy = name;
                }


                // Update the existing employee object in the database
                response = twoBigDB.Update("PRODUCTS/" + productID, existingproduct);

                // Show success message
                Response.Write("<script>alert ('Product " + productID + " has been successfully updated!'); </script>");

                txt_price.Text = null;
                txt_discount.Text = null;

                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    activityTime = addedTime,
                    userActivity = "UPDATED PRODUCT REFILL DETAILS",
                    // userActivity = UserActivityType.UpdatedEmployeeRecords
                };
                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

               
                //GridPro_Details.DataSource = null;
                //GridPro_Details.DataBind();
                gridProductRefill.Visible = true;
                gridTankSupply.Visible = false;
                gridotherProduct.Visible = false;
                lblThirdparty.Visible = false;
                lbl_tankSupply.Visible = false;

                productRefillDisplay();
                lblProductData.Text = "PRODUCT REFILL";
                lblProductData.Visible = true;
            }
            catch (Exception ex)
            {
                // Show error message
                Response.Write("<script>alert ('An error occurred while processing your request.');</script>" + ex.Message);
            }
        }
        //STORE THE THIRD PARTY PRODUCT ID FOR LATER USE AND UPDATE 
        protected void btnthirdpartyProduct_Click(object sender, EventArgs e)
        {
            // Retrieve the button that was clicked
            Button btnthirdpartyProduct = (Button)sender;

            // Find the GridView row containing the button
            GridViewRow row = (GridViewRow)btnthirdpartyProduct.NamingContainer;

            // Get the product ID from the specific column
            int productIDColumnIndex = 1; //  the actual column index of the product ID
            int productID = int.Parse(row.Cells[productIDColumnIndex].Text);

            // Store the product ID in a hidden field for later use
            hfThirdpartyProduct.Value = productID.ToString();

            // Retrieve the existing product object from the database using the productID entered
            FirebaseResponse response = twoBigDB.Get("PRODUCTS/" + productID);
            Products existingproduct = response.ResultAs<Products>();

            if (response == null || existingproduct == null)
            {
                // Show error message if the response or existingproduct is null
                Response.Write("<script>alert ('An error occurred while retrieving the product details.');</script>");
                return;
            }

            // Assign the values to the UI controls
            txt_price.Text = existingproduct.productPrice;
            txt_discount.Text = existingproduct.productDiscount;


            // Store the product ID in a hidden field for later use
            hfThirdpartyProduct.Value = productID.ToString();

            // Show the modal popup
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editthirdpartyproduct", "$('#editthirdpartyproduct').modal('show');", true);
        }
        //PERFORM THE THIRD PARTY PRODUCT ID TO UPDATE HERE
        protected void btnUpdateOtherProductDetails_Click(object sender, EventArgs e)
        {
            //  generate a random number for employee logged
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            string name = (string)Session["fullname"];

            // Get the product ID from the hidden field
            int productID = int.Parse(hfThirdpartyProduct.Value);

            // Get the new price and discount from the DropDownList in the modal popup
            string newprice = txt_price.Text;
            string newdiscount = txt_discount.Text;

            try
            {
                // Retrieve the existing product object from the database using the productID entered
                FirebaseResponse response = twoBigDB.Get("PRODUCTS/" + productID);
                Products existingproduct = response.ResultAs<Products>();

                if (response == null || existingproduct == null)
                {
                    // Show error message if the response or existingproduct is null
                    Response.Write("<script>alert ('An error occurred while retreiving the third party product details.');</script>");
                    return;
                }

                // Update the fields that have changed
                if (!string.IsNullOrEmpty(newdiscount) && newdiscount != existingproduct.productDiscount)
                {
                    existingproduct.productDiscount = newdiscount;
                    existingproduct.dateUpdated = DateTime.Now;
                    existingproduct.updatedBy = name;
                }
                if (!string.IsNullOrEmpty(newprice) && newprice != existingproduct.productPrice)
                {
                    existingproduct.productPrice = newprice;
                    existingproduct.dateUpdated = DateTime.Now;
                    existingproduct.updatedBy = name;
                }


                // Update the existing employee object in the database
                response = twoBigDB.Update("PRODUCTS/" + productID, existingproduct);

                // Show success message
                Response.Write("<script>alert ('Product " + productID + " has been successfully updated!'); </script>");

                txt_price.Text = null;
                txt_discount.Text = null;

                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    role = "Admin",
                    activityTime = addedTime,
                    userActivity = "UPDATED THIRD PARTY PRODUCT DETAILS",
                    // userActivity = UserActivityType.UpdatedEmployeeRecords
                };
                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                //GridotherProduct_Details.DataSource = null;
                //GridotherProduct_Details.DataBind();
                gridProductRefill.Visible = false ;
                gridTankSupply.Visible = false;
                gridotherProduct.Visible = true;
                lblThirdparty.Visible = false;
                lbl_tankSupply.Visible = false;

                thirdpartyProductsDisplay();
                lblProductData.Text = "THIRD PARTY PRODUCT";
                lblProductData.Visible = true;
            }
            catch (Exception ex)
            {
                // Show error message
                Response.Write("<script>alert ('An error occurred while processing your request.');</script>" + ex.Message);
            }
        } 

        //SEARCH PRODUCT REPORT
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedOption = ddlSearchOptions.SelectedValue;

            try
            {
                if (selectedOption == "0")
                {
                    lblProductData.Text = "PRODUCT REFILL";
                    lblThirdparty.Text = "THIRDPARTY PRODUCT";
                    lbl_tankSupply.Text = "TANK SUPPLY";
                    productRefillDisplay();
                    thirdpartyProductsDisplay();
                    tankSupplyDisplay();
                    gridProductRefill.Visible = true;
                    gridTankSupply.Visible = true;
                    gridotherProduct.Visible = true;
                    lblProductData.Visible = true;
                    lblThirdparty.Visible = true;
                    lbl_tankSupply.Visible = true;
                }
                else if (selectedOption == "1")
                {
                    lblProductData.Text = "PRODUCT REFILL";
                    lblThirdparty.Visible = false;
                    lbl_tankSupply.Visible = false;
                    gridProductRefill.Visible = true;
                    gridTankSupply.Visible = false;
                    gridotherProduct.Visible = false;
                    productRefillDisplay();
                    btnSearchOrder.Visible = true;
                    txtSearch.Visible = true;
                }
                else if (selectedOption == "2")
                {
                    lblProductData.Text = "THIRDPARTY PRODUCT";
                    lblThirdparty.Visible = false;
                    lbl_tankSupply.Visible = false;
                    gridProductRefill.Visible = false;
                    gridTankSupply.Visible = false;
                    gridotherProduct.Visible = true;
                    thirdpartyProductsDisplay();
                    //btnSearchThirdParty.Visible = true;
                    //txtSearchThirdParty.Visible = true;
                }
                else if (selectedOption == "3")
                {
                    lblProductData.Text = "TANK SUPPLY";
                    lblThirdparty.Visible = false;
                    lbl_tankSupply.Visible = false;
                    gridProductRefill.Visible = false;
                    gridotherProduct.Visible = false;
                    gridTankSupply.Visible = true;
                    tankSupplyDisplay();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/WaterProduct.aspx';" + ex.Message);
            }
        }
        //SEARCH CERTAIN PRODUCT REPORT
        protected void btnSearchProduct_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#view').modal();", true);

            string idno = (string)Session["idno"];
            string productSearch = txtSearch.Text;
            decimal discount;

            // Check if the product search term is valid
            if (string.IsNullOrEmpty(productSearch))
            {
                Response.Write("<script>alert ('Please enter a valid product ID or name!');</script>");
                return;
            }

            try
            {
                // Retrieve all products from the database
                FirebaseResponse response = twoBigDB.Get("PRODUCTS");
                Dictionary<string, Products> productList = response.ResultAs<Dictionary<string, Products>>();

                // Create the DataTable to hold the products
                DataTable productTable = new DataTable();
                productTable.Columns.Add("PRODUCT ID");
                productTable.Columns.Add("PRODUCT NAME");
                productTable.Columns.Add("PRODUCT QUANTITY");
                productTable.Columns.Add("PRODUCT UNIT OF VOLUME");
                productTable.Columns.Add("PRODUCT PRICE");
                productTable.Columns.Add("PRODUCT DISCOUNT");
                productTable.Columns.Add("PRODUCT STOCK");
                productTable.Columns.Add("DATE ADDED");
                productTable.Columns.Add("ADDED BY");
                productTable.Columns.Add("DATE UPDATED");
                productTable.Columns.Add("UPDATED BY");

                // Filter the products based on the search criteria
                var filteredList = productList.Values.Where(d =>
                    d.adminId.ToString() == idno &&
                    (d.productId.ToString() == productSearch || d.productType.Contains(productSearch))
                );

                // Loop through the filtered products and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    if (!decimal.TryParse(entry.productDiscount, out discount))
                    {
                        // If the discount value is not a valid decimal, assume it is zero
                        discount = 0;
                    }
                    else
                    {
                        // Convert discount from percentage to decimal
                        discount /= 100;
                    }

                    string dateAdded = entry.dateAdded == DateTime.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateUpdated = entry.dateUpdated == DateTime.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    productTable.Rows.Add(
                        entry.productId,
                        entry.productType,
                        entry.productQty,
                        entry.product_UnitOfVolume,
                        entry.productPrice,
                        discount,
                        entry.productStockQty + " " + entry.productStockUnit,
                        dateAdded,
                        entry.addedBy,
                        dateUpdated,
                        entry.updatedBy);
                }

                // Check if any products were found
                if (productTable.Rows.Count == 0)
                {
                    GridotherProduct_Details.DataSource = null;
                    GridotherProduct_Details.DataBind();
                    GridPro_Details.DataSource = null;
                    GridPro_Details.DataBind();
                    lblMessage.Text = "No record found for product with ID number/name: " + productSearch;
                    lblMessage.Visible = true;
                    lblViewSearch.Visible = false;
                }
                else
                {
                    // Bind the DataTable to the respective GridView
                    lblViewSearch.Text = "You searched for the product with ID number/name: " + productSearch;
                    GridPro_Details.DataSource = productTable;
                    GridPro_Details.DataBind();
                    lblMessage.Visible = false;
                    lblViewSearch.Visible = true;
                    GridotherProduct_Details.DataSource = null;
                    GridotherProduct_Details.DataBind();
                }
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error displaying the data. '); location.reload(); window.location.href = '/Admin/WaterProduct.aspx'; </script>" + ex.Message);
            }
        }

    }
}