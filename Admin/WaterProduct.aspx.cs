using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
            }
            displayTankSupply();
           
            //drdDeliverytype.Visible = false;
            //btnDeliverytype.Visible = false;



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
                    var filteredSupply = supply.Values.FirstOrDefault(d => d.adminId.ToString() == idno && d.dateAdded.Date == DateTime.UtcNow.Date);

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
                        double tankCapacity = double.Parse(filteredSupply.tankVolume);
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

                        // Calculate the total gallons ordered
                        double totalOrderedGallons = 0;
                        if (orders != null)
                        {

                            var filteredOrders = orders.Values.Where(d => d.admin_ID.ToString() == idno && d.order_OrderStatus == "Delivered" || d.order_OrderStatus == "Accepted"
                                                || d.order_OrderStatus == "Pending" && d.order_OrderMethod == "refill" || d.order_OrderMethod == "new gallon");

                            foreach (Order order in filteredOrders)
                            {
                                double orderedGallons = 0;
                                if (order.order_unit == "gallon/s")
                                {
                                    orderedGallons = Double.Parse(order.order_size);
                                }
                                else if (order.order_unit == "L" || order.order_unit == "liter/s")
                                {
                                    double gallonsPerLiter = 3.78541; // conversion factor from gallon to liters
                                    orderedGallons = Double.Parse(order.order_size) * gallonsPerLiter;
                                }
                                else if (order.order_unit == "mL" || order.order_unit == "ML" || order.order_unit == "milliliters")
                                {
                                    double gallonsPerML = 0.00026417205236; // conversion factor from gallons to milliliters
                                    orderedGallons = Double.Parse(order.order_size) / gallonsPerML;
                                }
                                //Get the total of ordered gallons 
                                totalOrderedGallons += orderedGallons * order.order_Quantity;
                            }
                        }

                        //Calculate the ordered gallons of walkin
                        if (walkinOrders != null)
                        {
                            var filteredWalkinOrders = walkinOrders.Values.Where(d => d.adminId.ToString() == idno && d.orderType == "Refill" || d.orderType == "New Gallon");
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
                        double remainingSupply = tankCapacity - totalOrderedGallons;

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
                                    dateUpdated = DateTimeOffset.UtcNow
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
                                    tankBalance = remainingSupply.ToString("N2") + ' ' + "gallons", // Update the remaining supply field
                                    dateUpdated = DateTimeOffset.UtcNow
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
        //RADIO BUTTON SELECTION FOT DELIVERY TYPE
        //RETRIEVE PRODUCTREFILL DATA
        private void productRefillDisplay()
        {
            string idno = (string)Session["idno"];
            decimal discount;

            // Retrieve all orders from the product refill table
            FirebaseResponse response = twoBigDB.Get("PRODUCTREFILL");
            Dictionary<string, ProductRefill> productrefillList = response.ResultAs<Dictionary<string, ProductRefill>>();
            var filteredList = productrefillList.Values.Where(d => d.adminId.ToString() == idno);
        
            // Create the DataTable to hold the orders
            //sa pag create sa table
            DataTable productRefillTable = new DataTable();
            productRefillTable.Columns.Add("PRODUCT ID");
            productRefillTable.Columns.Add("PRODUCT TYPE");
            productRefillTable.Columns.Add("PRODUCT UNIT");
            productRefillTable.Columns.Add("PRODUCT SIZE");
            productRefillTable.Columns.Add("PRODUCT PRICE");
            productRefillTable.Columns.Add("PRODUCT DISCOUNT");
            productRefillTable.Columns.Add("DATE ADDED");
            productRefillTable.Columns.Add("ADDED BY");

            if (response != null && response.ResultAs<ProductRefill>() != null)
            {
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    if (!decimal.TryParse(entry.pro_discount.ToString(), out discount))
                    {
                        // If the discount value is not a valid decimal, assume it is zero
                        discount = 0;
                    }
                    else
                    {
                        // Convert discount from percentage to decimal
                        discount /= 100;
                    }

                    productRefillTable.Rows.Add(entry.pro_refillId, entry.pro_refillWaterType,
                                         entry.pro_refillUnit, entry.pro_refillSize, entry.pro_refillPrice,
                                         discount, entry.dateAdded, entry.addedBy);
                }
            }
            else
            {
                // Handle null response or invalid selected value
                productRefillTable.Rows.Add("No data found", "", "", "", "", "", "");
            }

            // Bind the DataTable to the GridView
            gridProductRefill.DataSource = productRefillTable;
            gridProductRefill.DataBind();
        }
        //RETRIEVE OTHERPRODUCT DATA
        private void otherProductsDisplay()
        {
            string idno = (string)Session["idno"];
            //string empId = (string)Session["emp_id"];
            // int adminId = int.Parse(idno);
            decimal discount;

            

            // Retrieve all orders from the other product table
            FirebaseResponse response = twoBigDB.Get("otherPRODUCTS");
            Dictionary<string, otherProducts> otherproductsList = response.ResultAs<Dictionary<string, otherProducts>>();
            var filteredList = otherproductsList.Values.Where(d => d.adminId.ToString() == idno);

            // Create the DataTable to hold the other product report
            //sa pag create sa table
            DataTable otherProductTable = new DataTable();
            otherProductTable.Columns.Add("PRODUCT ID");
            otherProductTable.Columns.Add("PRODUCT TYPE");
            otherProductTable.Columns.Add("PRODUCT UNIT");
            otherProductTable.Columns.Add("PRODUCT SIZE");
            otherProductTable.Columns.Add("PRODUCT PRICE");
            otherProductTable.Columns.Add("PRODUCT STOCK");
            otherProductTable.Columns.Add("PRODUCT DISCOUNT");
            otherProductTable.Columns.Add("DATE ADDED");
            otherProductTable.Columns.Add("ADDED BY");

            if (response != null && response.ResultAs<otherProducts>() != null)
            {
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    if (!decimal.TryParse(entry.other_productDiscount.ToString(), out discount))
                    {
                        // If the discount value is not a valid decimal, assume it is zero
                        discount = 0;
                    }
                    else
                    {
                        // Convert discount from percentage to decimal
                        discount /= 100;
                    }

                    otherProductTable.Rows.Add(entry.other_productId, entry.other_productName, entry.other_productUnit,
                                               entry.other_productSize, entry.other_productPrice,
                                               entry.other_qtyStock + " " + entry.other_unitStock, 
                                               discount, entry.dateAdded, entry.addedBy);
                }
            }
            else
            {
                // Handle null response or invalid selected value
                otherProductTable.Rows.Add("No data found", "", "", "", "", "", "");
            }

            // Bind the DataTable to the GridView
            gridotherProduct.DataSource = otherProductTable;
            gridotherProduct.DataBind();
        }
        //RETRIEVE TANK SUPPLY
        private void tankSupplyDisplay()
        {
            string idno = (string)Session["idno"];

            // Retrieve all orders from the TANKSUPPLY table
            FirebaseResponse response = twoBigDB.Get("TANKSUPPLY");
            Dictionary<string, TankSupply> otherproductsList = response.ResultAs<Dictionary<string, TankSupply>>();
            var filteredList = otherproductsList.Values.Where(d => d.adminId.ToString() == idno);

            // Create the DataTable to hold the tank supply report
            //sa pag create sa table
            DataTable tankSupplyTable = new DataTable();
            tankSupplyTable.Columns.Add("TANK ID");
            tankSupplyTable.Columns.Add("TANK SUPPLY");
            tankSupplyTable.Columns.Add("TANK BALANCE");
            tankSupplyTable.Columns.Add("ADDED BY");

            if (response != null && response.ResultAs<TankSupply>() != null)
            {
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {

                    tankSupplyTable.Rows.Add(entry.tankId, entry.tankVolume + " " + entry.tankUnit, entry.tankBalance, entry.addedBy);
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
            int logsId = (int)Session["logsId"];
            string name = (string)Session["fullname"];


            try
            {
                // Check if a record with the same adminId and dateAdded exists in the TANKSUPPLY table
                FirebaseResponse res = twoBigDB.Get("TANKSUPPLY");
                Dictionary<string, TankSupply> supply = res.ResultAs<Dictionary<string, TankSupply>>();
                if (supply != null) // Add this null check
                {
                    // Filter the list of orders by the owner's ID and the order status and delivery type
                    var filteredList = supply.Values.FirstOrDefault(d => d.adminId.ToString() == idno && d.dateAdded.Date == DateTime.UtcNow.Date);

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

                    var data = new TankSupply
                    {
                        adminId = adminId,
                        tankId = idnum,
                        tankUnit = drdTankUnit.SelectedValue,
                        tankVolume = tankSize.Text,
                        addedBy = name,
                        dateAdded = DateTimeOffset.UtcNow
                    };

                    SetResponse response;
                    response = twoBigDB.Set("TANKSUPPLY/" + data.tankId, data);
                    TankSupply result = response.ResultAs<TankSupply>();

                    Response.Write("<script>alert ('Tank supply for today with id number: " + data.tankId + " is successfully added!'); location.reload(); window.location.href = '/Admin/WaterProduct.aspx'; </script>");

                // Retrieve the existing Users log object from the database
                FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    activityTime = addedTime,
                    userActivity = "ADDED TANK SUPPLY",
                };

                twoBigDB.Set("USERSLOG/" + log.logsId, log);


                // Display the tank supply result here
                lblDate.Text = data.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                lbltankSupply.Text = data.tankVolume + ' ' + data.tankUnit;
            }

            catch (Exception ex)
            {
                Response.Write("<script>alert('Error adding tank supply! " + ex.Message + "');</script>");
            }
        }

        //STORING DATA TO otherPRODUCT
        protected async void btnAdd_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            int logsId = (int)Session["logsId"];
            string name = (string)Session["fullname"];

            try
            {
                // INSERT DATA TO TABLE = otherPRODUCT
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                var data = new otherProducts 
                {
                    adminId = adminId,
                    other_productId = idnum,
                    offerType = "other Product",
                    other_productName = productName.Text,
                    other_productUnit = drdprodUnit.SelectedValue,
                    other_productSize = productSize.Text,
                    other_productPrice = productPrice.Text,
                    other_productDiscount = int.Parse(productDiscounts.Text),
                    other_unitStock = drdUnitStock.Text,
                    other_qtyStock = int.Parse(stockQty.Text),
                    other_productImage = null,
                    addedBy = name,
                    dateAdded = DateTime.UtcNow
                };


                byte[] fileBytes = null;

                if (imgProduct.HasFile)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imgProduct.PostedFile.InputStream.CopyTo(memoryStream);
                        fileBytes = memoryStream.ToArray();
                    }
                }

                if (fileBytes != null)
                {

                    var storage = new FirebaseStorage("big-system-64b55.appspot.com");
                    var fileExtension = Path.GetExtension(imgProduct.FileName);
                    var filePath = $"otherProduct_images/{data.other_productId}{fileExtension}";
                    //  used the using statement to ensure that the MemoryStream object is properly disposed after it's used.
                    using (var stream = new MemoryStream(fileBytes))
                    {
                        var storageTask = storage.Child(filePath).PutAsync(stream);
                        var downloadUrl = await storageTask;
                        // used Encoding.ASCII.GetBytes to convert the downloadUrl string to a byte[] object.
                        data.other_productImage = downloadUrl;
                    }
                }

                SetResponse response;
                //USER = tablename, Idno = key(PK ? )
               // response = twoBigDB.Set("ADMIN/" + idno + "/Product/" + data.productId, data);
                 response = twoBigDB.Set("otherPRODUCTS/" + data.other_productId, data);
                otherProducts result = response.ResultAs<otherProducts>();
                Response.Write("<script>alert ('Other water product offers with Id number: " + data.other_productId + " is successfully added!'); location.reload(); window.location.href = '/Admin/WaterProduct.aspx'; </script>");

                // Retrieve the existing Users log object from the database
                FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    userActivity = "ADDED OTHER PRODUCT",
                    activityTime = addedTime
                };

                twoBigDB.Set("USERSLOG/" + log.logsId, log);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Data already exist'); location.reload(); window.location.href = '/Admin/WaterProduct.aspx';" + ex.Message);
            }
        }
        //STORING DATA TO PRODUCT REFILL
        protected async void btnSet_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            int logsId = (int)Session["logsId"];
            string name = (string)Session["fullname"];

            try
            {
                // INSERT DATA TO TABLE  
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                var data = new ProductRefill
                {
                    pro_refillId = idnum,
                    adminId = adminId,
                    offerType = "Product Refill",
                    pro_refillWaterType = refillwaterType.Text,
                    pro_Image = null,
                    pro_refillUnit = refillUnit.SelectedValue,
                    pro_discount = int.Parse(refillDiscount.Text),
                    pro_refillSize = refillSize.Text,
                    pro_refillPrice = refillPrice.Text,
                    addedBy = name,
                    dateAdded = DateTime.UtcNow
                };

                //UPLOADING THE IMAGE TO THE STORAGE
                byte[] fileBytes = null;

                if (prodImage.HasFile)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        prodImage.PostedFile.InputStream.CopyTo(memoryStream);
                        fileBytes = memoryStream.ToArray();
                    }
                }

                if (fileBytes != null)
                {

                    var storage = new FirebaseStorage("big-system-64b55.appspot.com");
                    var fileExtension = Path.GetExtension(prodImage.FileName);
                    var filePath = $"productRefill_images/{data.pro_refillId}{fileExtension}";
                    //  used the using statement to ensure that the MemoryStream object is properly disposed after it's used.
                    using (var stream = new MemoryStream(fileBytes))
                    {
                        var storageTask = storage.Child(filePath).PutAsync(stream);
                        var downloadUrl = await storageTask;
                        // used Encoding.ASCII.GetBytes to convert the downloadUrl string to a byte[] object.
                        data.pro_Image = downloadUrl;
                    }
                }

                SetResponse response;
                //USER = tablename, Idno = key(PK ? )
                // response = twoBigDB.Set("Product/" + idno + "/Product/" + data.productId, data);
                response = twoBigDB.Set("PRODUCTREFILL/" + data.pro_refillId, data);
                ProductRefill result = response.ResultAs<ProductRefill>();
                Response.Write("<script>alert ('Product Refill  with Id number: " + data.pro_refillId + " is successfully added!'); location.reload(); window.location.href = '/Admin/WaterProduct.aspx'; </script>");

                //Response.Write("<script>alert ('Delivery details successfully added!');</script>");

                // Retrieve the existing Users log object from the database
                FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    userActivity = "ADDED PRODUCT REFILL",
                    activityTime = addedTime
                };

                twoBigDB.Set("USERSLOG/" + log.logsId, log);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        //SEARCH PRODUCT REPORT
        protected void btnSearch_Click(object sender, EventArgs e)
            {
                try
                {
                    string selectedOption = ddlSearchOptions.SelectedValue;

                
                    if (selectedOption == "0")
                    {
                        lblProductData.Text = "PRODUCT REFILL";
                        gridProductRefill.Visible = true;
                        gridTankSupply.Visible = false;
                        gridotherProduct.Visible = false;
                        productRefillDisplay();
                    } 
                    else if (selectedOption == "1")
                    {
                        lblProductData.Text = "OTHER PRODUCT";
                        gridProductRefill.Visible = false;
                        gridTankSupply.Visible = false;
                        gridotherProduct.Visible = true;
                        otherProductsDisplay();
                    }
                    else if (selectedOption == "2")
                    {
                        lblProductData.Text = "TANK SUPPLY";
                        gridProductRefill.Visible = false;
                        gridotherProduct.Visible = false;
                        gridTankSupply.Visible = true;
                        tankSupplyDisplay();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/WaterProduct.aspx';" + ex.Message);
                }
            }
    }
}