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

            radDevType.Attributes.Add("onclick", "showHideExpressDiv();");

            if (!IsPostBack)
            {
                //productRefillDisplay();
                //otherProductsDisplay();
                //deliveryExpressDisplay();
                //displayTankSupply();
            }
            displayTankSupply();

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
                        lblDate.Text = filteredSupply.dateAdded.ToString("MM/dd/yyyy hh:mm:ss tt");
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

                            var filteredOrders = orders.Values.Where(d => d.admin_ID.ToString() == idno
                                && (d.order_OrderStatus == "Delivered" || d.order_OrderStatus == "Accepted")
                                && (d.order_OrderMethod == "refill" || d.order_OrderMethod == "new gallon"));

                            foreach (Order order in filteredOrders)
                            {
                                double orderedGallons = 0;
                                if (order.order_unit == "gallon/s")
                                {
                                    orderedGallons = Double.Parse(order.order_size);
                                }
                                else if (order.order_unit == "L" || order.order_unit == "liter/s")
                                {
                                    double gallonsPerLiter = 0.26417205236; // conversion factor from gallon to liters
                                    orderedGallons = Double.Parse(order.order_size) * gallonsPerLiter;
                                }
                                else if (order.order_unit == "mL" || order.order_unit == "ML" || order.order_unit == "milliliters")
                                {
                                    double gallonsPerML = 3785.41; // conversion factor from gallons to milliliters
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
                        if (remainingSupply <= 0)
                        {
                           // remainingSupply = 0;
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
        //private void displayTankSupply()
        //{
        //    //private void displayTankSupply()
        //    //{
        //    //    // Get the ID of the currently logged-in owner from session state
        //    //    string idno = (string)Session["idno"];
        //    //    try
        //    //    {
        //    //        // Retrieve the tank supply for today from the TANKSUPPLY table
        //    //        FirebaseResponse response = twoBigDB.Get("TANKSUPPLY");
        //    //        Dictionary<string, TankSupply> supply = response.ResultAs<Dictionary<string, TankSupply>>();

        //    //        if (supply != null)
        //    //        {
        //    //            // Filter the tank supply list by the owner's ID and the date added
        //    //            var filteredSupply = supply.Values.FirstOrDefault(d => d.adminId.ToString() == idno && d.dateAdded.Date == DateTime.UtcNow.Date);

        //    //            if (filteredSupply != null)
        //    //            {
        //    //                lbl_Date.Text = filteredSupply.dateAdded.ToString("MM/dd/yyyy hh:mm:ss tt");
        //    //                lbltankSupply.Text = filteredSupply.tankVolume.ToString() + ' ' + filteredSupply.tankUnit.ToString();

        //    //                // Convert tank capacity to gallons if it's not already in gallons
        //    //                double tankCapacity = double.Parse(filteredSupply.tankVolume);
        //    //                if (filteredSupply.tankUnit == "L" || filteredSupply.tankUnit == "liter/s")
        //    //                {
        //    //                    double gallonsPerLiter = 0.26417205236; // conversion factor from gallon to liters
        //    //                    tankCapacity *= gallonsPerLiter;
        //    //                }
        //    //                else if (filteredSupply.tankUnit == "mL" || filteredSupply.tankUnit == "ML" || filteredSupply.tankUnit == "milliliters")
        //    //                {
        //    //                    double gallonsPerML = 3785.41; // conversion factor from gallons to milliliters
        //    //                    tankCapacity /= gallonsPerML;
        //    //                }

        //    //                // Retrieve all orders from the ORDERS table
        //    //                FirebaseResponse responseOrder = twoBigDB.Get("ORDERS");
        //    //                Dictionary<string, Order> orders = responseOrder.ResultAs<Dictionary<string, Order>>();

        //    //                // Retrieve all walk-in orders from the WalkInOrders table
        //    //                FirebaseResponse walkInOrder = twoBigDB.Get("WalkInOrders");
        //    //                Dictionary<string, WalkInOrders> walkin = walkInOrder.ResultAs<Dictionary<string, WalkInOrders>>();

        //    //                // Sum up the total gallons ordered for today
        //    //                double totalOrders = 0;
        //    //                foreach (Order order in orders.Values)
        //    //                {
        //    //                    if (order.admin_ID.ToString() != idno || order.order_OrderStatus != "Pending" || order.order_DeliveryTypeValue == "Scheduled")
        //    //                    {
        //    //                        continue;
        //    //                    }

        //    //                    if (order.order_OrderMethod == "New Gallon" || order.order_OrderMethod == "Refill")
        //    //                    {
        //    //                        double orderedGallons = 0;
        //    //                        if (order.order_unit == "L" || order.order_unit == "liter/s")
        //    //                        {
        //    //                            double gallonsPerLiter = 0.26417205236; // conversion factor from gallon to liters
        //    //                            orderedGallons = Double.Parse(order.order_size) * gallonsPerLiter;
        //    //                        }
        //    //                        else if (order.order_unit == "mL" || order.order_unit == "ML" || order.order_unit == "milliliters")
        //    //                        {
        //    //                            double gallonsPerML = 3785.41; // conversion factor from gallons to milliliters
        //    //                            orderedGallons = Double.Parse(order.order_size) / gallonsPerML;
        //    //                        }

        //    //                        totalOrders += orderedGallons;
        //    //                    }
        //    //                }

        //                    // Reduce tank supply based on the walkin orders for today
        //                    double remaining_supply = tankCapacity;
        //                foreach (WalkInOrders order in filteredWalkin)
        //                {
        //                    // Convert order size to gallons or milliliters based on order unit
        //                    double orderedGallons = 0;
        //                    if (order.productUnit == "L" || order.productUnit == "liter/s")
        //                    {
        //                        double gallonsPerLiter = 0.26417205236; // conversion factor from gallon to liters
        //                        orderedGallons = Double.Parse(order.productSize) * gallonsPerLiter;
        //                    }
        //                    else if (order.productUnit == "mL" || order.productUnit == "ML" || order.productUnit == "milliliters")
        //                    {
        //                        double gallonsPerML = 3785.41; // conversion factor from gallons to milliliters
        //                        orderedGallons = Double.Parse(order.productSize) / gallonsPerML;
        //                    }
        //                      totalOrders = order.productQty + orderedGallons;

        //                      remaining_supply -= totalOrders;
        //                }
        //                double totalRemainingSupply = remainingSupply - remaining_supply;

        //                // display the remaining supply
        //                if (totalRemainingSupply <= 0)
        //                {
        //                    lblremainingSupply.Text = ("There is no remaining supply. Unable to fullfill any further orders.");
        //                }
        //                else
        //                {
        //                    lblremainingSupply.Text = totalRemainingSupply.ToString("N2") + ' ' + "gallons";
        //                }

        //                // Update the remaining supply in the TANKSUPPLY table
        //                TankSupply tankSupply = new TankSupply
        //                {
        //                    tankId = filteredSupply.tankId,
        //                    adminId = filteredSupply.adminId,
        //                    dateAdded = filteredSupply.dateAdded,
        //                    tankVolume = filteredSupply.tankVolume,
        //                    tankUnit = filteredSupply.tankUnit,
        //                    tankBalance = totalRemainingSupply.ToString("N2") + ' ' + "gallons", // Update the remaining supply field
        //                    dateUpdated = DateTimeOffset.UtcNow
        //                };


        //                FirebaseResponse tankResponse = twoBigDB.Update("TANKSUPPLY/" + filteredSupply.tankId, tankSupply);

        //            }
        //            else
        //            {
        //                lbl_Date.Text = "";
        //                lbltankSupply.Text = "No supply found for today.";
        //                lblremainingSupply.Text = "";
        //            }
        //        }
        //        else
        //        {
        //            lbl_Date.Text = "";
        //            lbltankSupply.Text = "No tank supply records found.";
        //            lblremainingSupply.Text = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('An error occurred while retrieving tank supply data: " + ex.Message + "'); window.location.href = '/Admin/WaterProduct.aspx';</script>");
        //    }
        //}

        //RADIO BUTTON SELECTION FOT DELIVERY TYPE
        protected void radDevType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected value
            string selectedValue = radDevType.SelectedValue;

            // Disable the other radio buttons
            foreach (ListItem item in radDevType.Items)
            {
                if (item.Value != selectedValue)
                {
                    item.Enabled = false;

                }
            }
        }



        //RETRIEVE PRODUCTREFILL DATA
        private void productRefillDisplay()
        {
            string idno = (string)Session["idno"];

            // Retrieve all orders from the ORDERS table
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

                    productRefillTable.Rows.Add(entry.pro_refillId, entry.pro_refillWaterType,
                                         entry.pro_refillUnit, entry.pro_refillSize, entry.pro_refillPrice,
                                         entry.pro_discount, entry.dateAdded, entry.adminId);
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

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("otherPRODUCTS");
            Dictionary<string, otherProducts> otherproductsList = response.ResultAs<Dictionary<string, otherProducts>>();
            var filteredList = otherproductsList.Values.Where(d => d.adminId.ToString() == idno);
            //FirebaseResponse response = twoBigDB.Get("otherPRODUCTS/" + idno);
            //otherProducts emp = response.ResultAs<otherProducts>();
            //var data = response.Body;
            ////Dictionary<string, Employee> employeeList = JsonConvert.DeserializeObject<Dictionary<string, Employee>>(data);
            //Dictionary<string, otherProducts> otherproductList = response.ResultAs<Dictionary<string, otherProducts>>();

            // Create the DataTable to hold the orders
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

                    otherProductTable.Rows.Add(entry.other_productId, entry.other_productName,
                                         entry.other_productUnit, entry.other_productSize, entry.other_productPrice,
                                         entry.other_productStock, entry.other_productDiscount, entry.dateAdded, entry.adminId);
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
        //RETRIEVE DELIVERY DETAILS DATA
        //private void deliveryExpressDisplay()
        //{
        //    string idno = (string)Session["idno"];
        //    string empId = (string)Session["emp_id"];
        //    // int adminId = int.Parse(idno);

        //    // Retrieve all data from the DELIVERY_DETAILS table
        //    FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS");
        //    Dictionary<string, DeliveryDetails> deliveryList = response.ResultAs<Dictionary<string, DeliveryDetails>>();
        //    var filteredList = deliveryList.Values.Where(d => d.adminId.ToString() == idno && d.deliveryId );

        //    // Retrieve all orders from the ORDERS table
        //    FirebaseResponse res = twoBigDB.Get("DELIVERY_DETAILS/" + idno + "/deliveryTypes");
        //    Dictionary<string, expressDelivery> expressList = res.ResultAs<Dictionary<string, expressDelivery>>();
        //    var filteredexpressList = expressList.Values.Where(e => e.exDeliveryType == "Express"); 


        //    // Create the DataTable to hold the orders
        //    //sa pag create sa table
        //    DataTable expressTable = new DataTable();
        //    expressTable.Columns.Add("DELIVERY ID");
        //    expressTable.Columns.Add("DELIVERY TYPE");
        //    expressTable.Columns.Add("ESTIMATED DELIVERY TIME");
        //    expressTable.Columns.Add("ORDER TYPE");
        //    expressTable.Columns.Add("ORDER METHOD");
        //    expressTable.Columns.Add("DELIVERY FEE");
        //    expressTable.Columns.Add("DATE ADDED");
        //    expressTable.Columns.Add("ADDED BY");

        //    if (response != null && response.ResultAs<DeliveryDetails>() != null && res.ResultAs<expressDelivery>() != null)
        //    {
        //        foreach (var entry in filteredList)
        //        {
        //            expressTable.Rows.Add(entry.deliveryId, "", "", "", "", "", "", entry.adminId);
        //        }
        //        // Loop through the data and add them to the DataTable
        //        foreach (var entry in filteredexpressList)
        //        {
        //            expressTable.Rows.Add("", entry.exDeliveryType, entry.exEstimatedDelivery, entry.exOrderType,
        //                entry.exOrderMethod, entry.exDeliveryFee, entry.dateAdded);
        //        }

        //    }
        //    else
        //    {
        //        // Handle null response or invalid selected value
        //        expressTable.Rows.Add("No data found", "", "", "", "", "", "");
        //    }

        //    // Bind the DataTable to the GridView
        //    gridExpress.DataSource = expressTable;
        //    gridExpress.DataBind();
        //}


        //TO BE UPDATED

        //STORE TANK SUPPLY
        
        protected void btnAddSupply_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            int logsId = (int)Session["logsId"];


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
                //DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = logsId,
                    userFullname = (string)Session["fullname"],
                    emp_id = existingLog.emp_id,
                    empFullname = existingLog.empFullname,
                    empDateAdded = existingLog.empDateAdded,
                    dateLogin = existingLog.dateLogin,
                    deliveryDetailsId = existingLog.deliveryDetailsId,
                    deliveryDetailsDateAdded = existingLog.deliveryDetailsDateAdded,
                    productRefillId = existingLog.productRefillId,
                    productrefillDateAdded = existingLog.productrefillDateAdded,
                    other_productId = existingLog.other_productId,
                    otherProductDateAdded = existingLog.otherProductDateAdded,
                    tankId = data.tankId,
                    tankSupplyDateAdded = data.dateAdded
                };

                twoBigDB.Update("USERSLOG/" + log.logsId, log);


                // Display the tank supply result here
                lblDate.Text = data.dateAdded.ToString("MM/dd/yyyy hh:mm:ss tt");
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

            try
            {
                // INSERT DATA TO TABLE = otherPRODUCT
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                var data = new otherProducts 
                {
                    adminId = adminId,
                    other_productId = idnum,
                    other_productName = productName.Text,
                    other_productUnit = drdprodUnit.SelectedValue,
                    other_productSize = productSize.Text,
                    other_productPrice = productPrice.Text,
                    other_productDiscount = productDiscounts.Text,
                    other_productStock = productStock.Text,
                    other_productImage = null,
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
                //DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = logsId,
                    userFullname = (string)Session["fullname"],
                    emp_id = existingLog.emp_id,
                    empFullname = existingLog.empFullname,
                    empDateAdded = existingLog.empDateAdded,
                    dateLogin = existingLog.dateLogin,
                    tankId = existingLog.tankId,
                    tankSupplyDateAdded = existingLog.tankSupplyDateAdded,
                    deliveryDetailsId = existingLog.deliveryDetailsId,
                    deliveryDetailsDateAdded = existingLog.deliveryDetailsDateAdded,
                    productRefillId = existingLog.productRefillId,
                    productrefillDateAdded = existingLog.productrefillDateAdded,
                    other_productId = data.other_productId,
                    otherProductDateAdded = data.dateAdded
                };

                twoBigDB.Update("USERSLOG/" + log.logsId, log);
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

            try
            {
                // INSERT DATA TO TABLE  
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                var data = new ProductRefill
                {
                    pro_refillId = idnum,
                    adminId = adminId,
                    pro_refillWaterType = refillwaterType.Text,
                    pro_Image = null,
                    pro_refillUnit = refillUnit.SelectedValue,
                    pro_discount = productDiscounts.Text,
                    pro_refillSize = refillSize.Text,
                    pro_refillPrice = refillPrice.Text,
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
                //DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = logsId,
                    userFullname = (string)Session["fullname"],
                    emp_id = existingLog.emp_id,
                    empFullname = existingLog.empFullname,
                    empDateAdded = existingLog.empDateAdded,
                    dateLogin = existingLog.dateLogin,
                    tankId = existingLog.tankId,
                    tankSupplyDateAdded = existingLog.tankSupplyDateAdded,
                    other_productId = existingLog.other_productId,
                    otherProductDateAdded = existingLog.otherProductDateAdded,
                    deliveryDetailsId = existingLog.deliveryDetailsId,
                    deliveryDetailsDateAdded = existingLog.deliveryDetailsDateAdded,
                    productRefillId = data.pro_refillId,
                    productrefillDateAdded = data.dateAdded
                };

                twoBigDB.Update("USERSLOG/" + log.logsId, log);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        //STORING DELIVERY DETAILS NI DIRI
        protected void btnDeliverydetails_Click(object sender, EventArgs e)
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            int logsId = (int)Session["logsId"];

            Random rnd = new Random();
            int deliveryId = rnd.Next(1, 10000);
            
            //create the deliveryID and save the adminID
            var adminData = new DeliveryDetails
            {
                adminId = adminId,
                deliveryId = deliveryId
            };

                SetResponse response;
                response = twoBigDB.Set("DELIVERY_DETAILS/" + deliveryId, adminData);


                //FOR THE CHECKBOXES OF DELIVERY TYPES
                string[] selectedItems = new string[radDevType.Items.Count];
                int index = 0;

                foreach (ListItem item in radDevType.Items)
                {
                    if (item.Selected)
                    {
                        selectedItems[index] = item.Value;
                        index++;
                    }
                }

                //access each selected item separately using the index of the array
                string standard = selectedItems[0];
                string reservation = selectedItems[1];
                string express = selectedItems[2];



                //IF GICHECK STANDARD
                if (!string.IsNullOrEmpty(standard))
                {
                    // Loop through the items in the CheckBoxList to build the orderType string
                    string orderType = "";
                    foreach (ListItem item in DeliveryType.Items)
                    {
                        if (item.Selected)
                        {
                            orderType += item.Value + ", ";
                        }
                    }
                    orderType = orderType.TrimEnd(' ', ',');

                    // Loop through the items in the CheckBoxList to build the orderMethod string
                    string orderMethod = "";
                    foreach (ListItem item in OrderMethod.Items)
                    {
                        if (item.Selected)
                        {
                            orderMethod += item.Value + ", ";
                        }
                    }
                    orderMethod = orderMethod.TrimEnd(' ', ',');

                string swapOptions = "";
                foreach (ListItem item in standardSwapOptions.Items)
                {
                    if (item.Selected)
                    {
                        swapOptions += item.Value + ", ";
                    }
                }
                swapOptions = swapOptions.TrimEnd(' ', ',');

                Random standardRanID = new Random();
                int standardID = standardRanID.Next(1, 10000);

                var standardDeliver = new DeliveryDetails
                {
                    stanDeliverytype = standard,
                    stanDeliveryFee = DeliveryFee.Text, // DELIVERY FEE for STANDARD and RESERVATION
                    stanDeliveryTime = standardSchedFrom.Text + "AM - " + standardSchedTo.Text + "PM", //for STANDARD ONLY
                    standistance = FreeDelivery.Text, //FREE DELIVERY for STANDARD and RESERVATION
                    stanOrderType = orderType,
                    stanOrderMethod = orderMethod,
                    standardSwapOptions = swapOptions,
                    standardID = standardID,
                    dateAdded = DateTime.UtcNow
                };

                    SetResponse standardre;
                    standardre = twoBigDB.Set("DELIVERY_DETAILS/" + deliveryId + "/deliveryTypes/" + standardDeliver.standardID, standardDeliver);
                    DeliveryDetails res = standardre.ResultAs<DeliveryDetails>();

                }

            //IF GICHECK RESERVATION
            if (!string.IsNullOrEmpty(reservation))
            {
                // Loop through the items in the CheckBoxList to build the orderType string
                string resOrderType = "";
                foreach (ListItem item in reserveOrderType.Items)
                {
                    if (item.Selected)
                    {
                        resOrderType += item.Value + ", ";
                    }
                }
                resOrderType = resOrderType.TrimEnd(' ', ',');

                    // Loop through the items in the CheckBoxList to build the orderMethod string
                    string resOrderMethod = "";
                    foreach (ListItem item in reserveOrderMethod.Items)
                    {
                        if (item.Selected)
                        {
                            resOrderMethod += item.Value + ", ";
                        }
                    }
                    resOrderMethod = resOrderMethod.TrimEnd(' ', ',');


                string swapOptions = "";
                foreach (ListItem item in reserveSwap.Items)
                {
                    if (item.Selected)
                    {
                        swapOptions += item.Value + ", ";
                    }
                }
                swapOptions = swapOptions.TrimEnd(' ', ',');

                Random reserveRanID = new Random();
                int reservationDelivery = reserveRanID.Next(1, 10000);


                var reservationDeliver = new DeliveryDetails
                {
                    reservationID = reservationDelivery,
                    resDeliveryType = reservation,
                    resDeliveryFee = resDelFee.Text,
                    resDistanceFree = resFreeDel.Text,
                    resOrderMethod = resOrderMethod,
                    resOrderType = resOrderType,
                    reserveSwapOptions = swapOptions,
                    dateAdded = DateTime.UtcNow
                };
                SetResponse reservere;
                reservere = twoBigDB.Set("DELIVERY_DETAILS/" + deliveryId + "/deliveryTypes/" + reservationDeliver.reservationID, reservationDeliver);
                DeliveryDetails res = reservere.ResultAs<DeliveryDetails>();

                ////REMOVE ALL THE NULL VALUE
                //FirebaseResponse getNull = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryId + "/deliveryTypes/" + reservationDeliver.reservationID);
                //Model.AdminAccount pendingClients = getNull.ResultAs<Model.AdminAccount>();




            }

                //IF GICHECK ANG EXPRESS
                if (!string.IsNullOrEmpty(express))
                {
                    // Loop through the items in the CheckBoxList to build the orderType string
                    string resOrderType = "";
                    foreach (ListItem item in expressOrderType.Items)
                    {
                        if (item.Selected)
                        {
                            resOrderType += item.Value + ", ";
                        }
                    }
                    resOrderType = resOrderType.TrimEnd(' ', ',');

                    // Loop through the items in the CheckBoxList to build the orderMethod string
                    string resOrderMethod = "";
                    foreach (ListItem item in expressOrderMethod.Items)
                    {
                        if (item.Selected)
                        {
                            resOrderMethod += item.Value + ", ";
                        }
                    }
                    resOrderMethod = resOrderMethod.TrimEnd(' ', ',');

                string swapOptions = "";
                foreach (ListItem item in expressSwap.Items)
                {
                    if (item.Selected)
                    {
                        swapOptions += item.Value + ", ";
                    }
                }
                swapOptions = swapOptions.TrimEnd(' ', ',');

                //Generate random ID for expressID
                Random expressRanID = new Random();
                int expressID = expressRanID.Next(1, 10000);

                var expressDeliver = new DeliveryDetails
                {
                    expressID = expressID,
                    exDeliveryType = express,
                    exEstimatedDelivery = estimatedTime.Text,
                    exDeliveryFee = expressdeliveryFee.Text,
                    exOrderMethod = resOrderMethod,
                    exOrderType = resOrderType,
                    expressSwapOptions = swapOptions,
                    dateAdded = DateTime.UtcNow
                };
                    SetResponse expreessres;
                    expreessres = twoBigDB.Set("DELIVERY_DETAILS/" + deliveryId + "/deliveryTypes/" + expressDeliver.expressID, expressDeliver);
                    DeliveryDetails res = expreessres.ResultAs<DeliveryDetails>();
                }

            Response.Write("<script>alert ('You successfully created the Delivery Types you offer to your business');  window.location.href = '/Admin/WaterProduct.aspx'; </script>");

            // Retrieve the existing Users log object from the database
            FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
            UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

            // Get the current date and time
            //DateTime addedTime = DateTime.UtcNow;

            // Log user activity
            var log = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = logsId,
                userFullname = (string)Session["fullname"],
                emp_id = existingLog.emp_id,
                empFullname = existingLog.empFullname,
                empDateAdded = existingLog.empDateAdded,
                dateLogin = existingLog.dateLogin,
                tankId = existingLog.tankId,
                tankSupplyDateAdded = existingLog.tankSupplyDateAdded,
                other_productId = existingLog.other_productId,
                otherProductDateAdded = existingLog.otherProductDateAdded,
                productRefillId = existingLog.productRefillId,
                productrefillDateAdded = existingLog.productrefillDateAdded,
                deliveryDetailsId = adminData.deliveryId,
                deliveryDetailsDateAdded = adminData.dateAdded
            };

            twoBigDB.Update("USERSLOG/" + log.logsId, log);
        }

            protected void btnSearch_Click(object sender, EventArgs e)
            {
                try
                {
                    string selectedOption = ddlSearchOptions.SelectedValue;

                
                    if (selectedOption == "1")
                    {
                        lblProductData.Text = "PRODUCT REFILL";
                        gridProductRefill.Visible = true;
                        gridotherProduct.Visible = false;
                        productRefillDisplay();
                    } 
                    else if (selectedOption == "2")
                    {
                        lblProductData.Text = "OTHER PRODUCT";
                        gridProductRefill.Visible = false;
                        gridotherProduct.Visible = true;
                        otherProductsDisplay();
                    }
                    else if (selectedOption == "3")
                    {
                        lblProductData.Text = "DELIVERY DETAILS";
                        gridProductRefill.Visible = false;
                        gridotherProduct.Visible = false;
                        //deliveryExpressDisplay();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/WaterProduct.aspx';" + ex.Message);
                }
            }

    }
}