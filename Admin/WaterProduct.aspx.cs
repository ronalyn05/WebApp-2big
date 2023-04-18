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
            displayModeofPayment();
            drdDeliverytype.Visible = false;
            btnDeliverytype.Visible = false;



        }

       private void displayModeofPayment()
        {
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all data from the DELIVERY_DETAILS table
            FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS2");
            Dictionary<string, DeliveryDetails> deliveryList = response.ResultAs<Dictionary<string, DeliveryDetails>>();
            var filteredList = deliveryList.Values.Where(d => d.adminId.ToString() == idno);


            // Create the DataTable to hold the orders
            //sa pag create sa table
            DataTable MOPTable = new DataTable();
            //MOPTable.Columns.Add("Mode of Payments");


            //if (response != null && response.ResultAs<DeliveryDetails>() != null)
            //{
            //    foreach (var entry in filteredList)
            //    {
                   
            //        MOPTable.Rows.Add(entry.paymentMethods);
            //    }
            //}
            //else
            //{
            //    modeofPaymentGrid.Visible = false;
            //}

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

                            var filteredOrders = orders.Values.Where(d => d.admin_ID.ToString() == idno && d.order_OrderStatus == "Delivered" || d.order_OrderStatus == "Accepted"
                                && d.order_OrderMethod == "refill" || d.order_OrderMethod == "new gallon");

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
        //    string delID = (string)Session["deliveryID"];
        //    // int adminId = int.Parse(idno);

        //    // Retrieve all data from the DELIVERY_DETAILS table
        //    //("ADMIN/" + user.idno + "/RefillingStation/");
        //    FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS2/");
        //    Dictionary<string, DeliveryDetails> deliveryList = response.ResultAs<Dictionary<string, DeliveryDetails>>();

        //    //var filteredList = deliveryList.Values.Where(d => d.adminId.ToString() == idno);

        //    // Retrieve all orders from the ORDERS table
        //    //FirebaseResponse res = twoBigDB.Get("DELIVERY_DETAILS/");
        //    //Dictionary<string, expressDelivery> expressList = res.ResultAs<Dictionary<string, expressDelivery>>();
        //    //var filteredexpressList = expressList.Values.Where(e => e.exDeliveryType == "Express"); 


        //    // Create the DataTable to hold the orders
        //    //sa pag create sa table
        //    DataTable expressTable = new DataTable();
        //    expressTable.Columns.Add("DELIVERY ID");
        //    expressTable.Columns.Add("DELIVERY TYPE");
        //    expressTable.Columns.Add("DATE ADDED");
        //    expressTable.Columns.Add("ADDED BY");

        //    if (response != null && response.ResultAs<DeliveryDetails>() != null)
        //    {
        //        foreach (var entry in deliveryList)
        //        {
        //            expressTable.Rows.Add(entry.Key, entry.Value.exDeliveryType + ' ' + entry.Value.stanDeliverytype + ' ' + entry.Value.resDeliveryType,
        //                                    entry.Value.dateAdded, entry.Value.adminId);
        //        }


        //    }
        //    else
        //    {
        //        // Handle null response or invalid selected value
        //        expressTable.Rows.Add("No data found", "", "", "", "", "", "");
        //    }

        //    // Bind the DataTable to the GridView
        //    gridDeliveryDetails.DataSource = expressTable;
        //    gridDeliveryDetails.DataBind();

        //    drdDeliverytype.Visible = true;
        //    btnDeliverytype.Visible = true;


        //}


        //TO BE UPDATED

        //STORE TANK SUPPLY


        private void deliveryExpressDisplay()
        {
            string idno = (string)Session["idno"];

            //string empId = (string)Session["emp_id"];
            // int adminId = int.Parse(idno);

            // Retrieve all data from the DELIVERY_DETAILS table
            FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS2");
            Dictionary<string, DeliveryDetails> deliveryList = response.ResultAs<Dictionary<string, DeliveryDetails>>();
            var filteredList = deliveryList.Values.Where(d => d.adminId.ToString() == idno);

            // Create the DataTable to hold the orders
            //sa pag create sa table
            DataTable expressTable = new DataTable();
            expressTable.Columns.Add("DELIVERY TYPE ID");
            expressTable.Columns.Add("DELIVERY TYPE");
            expressTable.Columns.Add("DATE ADDED");
            expressTable.Columns.Add("ADDED BY");

            if (response != null && response.ResultAs<DeliveryDetails>() != null)
            {
                foreach (var entry in filteredList)
                {
                    if (entry.resDeliveryType == "Reservation")
                    {
                        //ROW FOR THE RESERVATION
                        expressTable.Rows.Add(entry.reservationID, entry.resDeliveryType,
                                                entry.reservationdateAdded, entry.adminId);
                    }
                    else
                    {
                        
                    }
                    if (entry.stanDeliverytype == "Standard")
                    {
                        //ROW FOR THE STANDARD
                        expressTable.Rows.Add(entry.standardID, entry.stanDeliverytype,
                            entry.standardDateAdded, entry.adminId);
                    }
                    else
                    {
                       
                    }
                    if (entry.exDeliveryType == "Express")
                    {
                        //ROW FOR THE EXPRESS
                        expressTable.Rows.Add(entry.expressID, entry.exDeliveryType,
                            entry.expressdateAdded, entry.adminId);
                    }
                    else
                    {
                        
                    }

                }
            }
            else
            {
                // Handle null response or invalid selected value
                expressTable.Rows.Add("No data found", "", "", "", "", "", "");
            }

            // Bind the DataTable to the GridView
            gridDeliveryDetails.DataSource = expressTable;
            gridDeliveryDetails.DataBind();

            drdDeliverytype.Visible = true;
            btnDeliverytype.Visible = true;
        }



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
                    productRefillId = existingLog.productRefillId,
                    productrefillDateAdded = existingLog.productrefillDateAdded,
                    other_productId = existingLog.other_productId,
                    otherProductDateAdded = existingLog.otherProductDateAdded,
                    tankId = data.tankId,
                    tankSupplyDateAdded = data.dateAdded,
                    standardAdded = existingLog.standardAdded,
                    reservationAdded = existingLog.reservationAdded,
                    expressAdded = existingLog.expressAdded
                };

                twoBigDB.Update("USERSLOG/" + log.logsId, log);


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
                    productRefillId = existingLog.productRefillId,
                    productrefillDateAdded = existingLog.productrefillDateAdded,
                    other_productId = data.other_productId,
                    otherProductDateAdded = data.dateAdded,

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
                    productRefillId = data.pro_refillId,
                    productrefillDateAdded = data.dateAdded,
                    standardAdded = existingLog.standardAdded,
                    reservationAdded = existingLog.reservationAdded,
                    expressAdded = existingLog.expressAdded

                };

                twoBigDB.Update("USERSLOG/" + log.logsId, log);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }


        //STORING DELIVERY DETAILS NI DIRI - PREVIOUS WORKING DELIVERY DETAILS  (OLD STRUCTURE)
        //protected void btnDeliverydetails_Click(object sender, EventArgs e)
        //{
        //    var idno = (string)Session["idno"];
        //    int adminId = int.Parse(idno);
        //    int logsId = (int)Session["logsId"];

        //    Random rnd = new Random();
        //    int deliveryId = rnd.Next(1, 10000);

        //    Session["deliveryID"] = deliveryId;

        //    //create the deliveryID and save the adminID
        //    var adminData = new DeliveryDetails
        //    {
        //        adminId = adminId,
        //        deliveryId = deliveryId
        //    };

        //    SetResponse response;
        //    response = twoBigDB.Set("DELIVERY_DETAILS/" + deliveryId, adminData);


        //    //FOR THE CHECKBOXES OF DELIVERY TYPES
        //    string[] selectedItems = new string[radDevType.Items.Count];
        //    int index = 0;

        //    foreach (ListItem item in radDevType.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            selectedItems[index] = item.Value;
        //            index++;
        //        }
        //    }

        //    //access each selected item separately using the index of the array
        //    string standard = selectedItems[0];
        //    string reservation = selectedItems[1];
        //    string express = selectedItems[2];



        //    //IF GICHECK STANDARD
        //    if (!string.IsNullOrEmpty(standard))
        //    {
        //        // Loop through the items in the CheckBoxList to build the orderType string
        //        string orderType = "";
        //        foreach (ListItem item in DeliveryType.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                orderType += item.Value + ", ";
        //            }
        //        }
        //        orderType = orderType.TrimEnd(' ', ',');

        //        // Loop through the items in the CheckBoxList to build the orderMethod string
        //        string orderMethod = "";
        //        foreach (ListItem item in OrderMethod.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                orderMethod += item.Value + ", ";
        //            }
        //        }
        //        orderMethod = orderMethod.TrimEnd(' ', ',');

        //        string swapOptions = "";
        //        foreach (ListItem item in standardSwapOptions.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                swapOptions += item.Value + ", ";
        //            }
        //        }
        //        swapOptions = swapOptions.TrimEnd(' ', ',');

        //        Random standardRanID = new Random();
        //        int standardID = standardRanID.Next(1, 10000);

        //        var standardDeliver = new DeliveryDetails
        //        {
        //            stanDeliverytype = standard,
        //            stanDeliveryFee = DeliveryFee.Text, // DELIVERY FEE for STANDARD and RESERVATION
        //            stanDeliveryTime = standardSchedFrom.Text + "AM - " + standardSchedTo.Text + "PM", //for STANDARD ONLY
        //            standistance = FreeDelivery.Text, //FREE DELIVERY for STANDARD and RESERVATION
        //            stanOrderType = orderType,
        //            stanOrderMethod = orderMethod,
        //            standardSwapOptions = swapOptions,
        //            standardID = standardID,
        //            dateAdded = DateTimeOffset.UtcNow
        //        };

        //        SetResponse standardre;
        //        standardre = twoBigDB.Set("DELIVERY_DETAILS/" + deliveryId + "/deliveryTypes/" + standardDeliver.standardID, standardDeliver);
        //        DeliveryDetails res = standardre.ResultAs<DeliveryDetails>();

        //    }

        //    //IF GICHECK RESERVATION
        //    if (!string.IsNullOrEmpty(reservation))
        //    {
        //        // Loop through the items in the CheckBoxList to build the orderType string
        //        string resOrderType = "";
        //        foreach (ListItem item in reserveOrderType.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                resOrderType += item.Value + ", ";
        //            }
        //        }
        //        resOrderType = resOrderType.TrimEnd(' ', ',');

        //        // Loop through the items in the CheckBoxList to build the orderMethod string
        //        string resOrderMethod = "";
        //        foreach (ListItem item in reserveOrderMethod.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                resOrderMethod += item.Value + ", ";
        //            }
        //        }
        //        resOrderMethod = resOrderMethod.TrimEnd(' ', ',');


        //        string swapOptions = "";
        //        foreach (ListItem item in reserveSwap.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                swapOptions += item.Value + ", ";
        //            }
        //        }
        //        swapOptions = swapOptions.TrimEnd(' ', ',');

        //        Random reserveRanID = new Random();
        //        int reservationDelivery = reserveRanID.Next(1, 10000);


        //        var reservationDeliver = new DeliveryDetails
        //        {
        //            reservationID = reservationDelivery,
        //            resDeliveryType = reservation,
        //            resDeliveryFee = resDelFee.Text,
        //            resDistanceFree = resFreeDel.Text,
        //            resOrderMethod = resOrderMethod,
        //            resOrderType = resOrderType,
        //            reserveSwapOptions = swapOptions,
        //            dateAdded = DateTimeOffset.UtcNow
        //        };
        //        SetResponse reservere;
        //        reservere = twoBigDB.Set("DELIVERY_DETAILS/" + deliveryId + "/deliveryTypes/" + reservationDeliver.reservationID, reservationDeliver);
        //        DeliveryDetails res = reservere.ResultAs<DeliveryDetails>();

        //        ////REMOVE ALL THE NULL VALUE
        //        //FirebaseResponse getNull = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryId + "/deliveryTypes/" + reservationDeliver.reservationID);
        //        //Model.AdminAccount pendingClients = getNull.ResultAs<Model.AdminAccount>();




        //    }

        //    //IF GICHECK ANG EXPRESS
        //    if (!string.IsNullOrEmpty(express))
        //    {
        //        // Loop through the items in the CheckBoxList to build the orderType string
        //        string resOrderType = "";
        //        foreach (ListItem item in expressOrderType.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                resOrderType += item.Value + ", ";
        //            }
        //        }
        //        resOrderType = resOrderType.TrimEnd(' ', ',');

        //        // Loop through the items in the CheckBoxList to build the orderMethod string
        //        string resOrderMethod = "";
        //        foreach (ListItem item in expressOrderMethod.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                resOrderMethod += item.Value + ", ";
        //            }
        //        }
        //        resOrderMethod = resOrderMethod.TrimEnd(' ', ',');

        //        string swapOptions = "";
        //        foreach (ListItem item in expressSwap.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                swapOptions += item.Value + ", ";
        //            }
        //        }
        //        swapOptions = swapOptions.TrimEnd(' ', ',');

        //        //Generate random ID for expressID
        //        Random expressRanID = new Random();
        //        int expressID = expressRanID.Next(1, 10000);

        //        var expressDeliver = new DeliveryDetails
        //        {
        //            expressID = expressID,
        //            exDeliveryType = express,
        //            exEstimatedDelivery = estimatedTime.Text,
        //            exDeliveryFee = expressdeliveryFee.Text,
        //            exOrderMethod = resOrderMethod,
        //            exOrderType = resOrderType,
        //            expressSwapOptions = swapOptions,
        //            dateAdded = DateTimeOffset.UtcNow
        //        };
        //        SetResponse expreessres;
        //        expreessres = twoBigDB.Set("DELIVERY_DETAILS3/" + deliveryId + "/deliveryTypes/" + expressDeliver.expressID, expressDeliver);
        //        DeliveryDetails res = expreessres.ResultAs<DeliveryDetails>();
        //    }

        //    Response.Write("<script>alert ('You successfully created the Delivery Types you offer to your business');  window.location.href = '/Admin/WaterProduct.aspx'; </script>");

        //    // Retrieve the existing Users log object from the database
        //    FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
        //    UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

        //    // Get the current date and time
        //    //DateTime addedTime = DateTime.UtcNow;

        //    // Log user activity
        //    var log = new UsersLogs
        //    {
        //        userIdnum = int.Parse(idno),
        //        logsId = logsId,
        //        userFullname = (string)Session["fullname"],
        //        emp_id = existingLog.emp_id,
        //        empFullname = existingLog.empFullname,
        //        empDateAdded = existingLog.empDateAdded,
        //        dateLogin = existingLog.dateLogin,
        //        tankId = existingLog.tankId,
        //        tankSupplyDateAdded = existingLog.tankSupplyDateAdded,
        //        other_productId = existingLog.other_productId,
        //        otherProductDateAdded = existingLog.otherProductDateAdded,
        //        productRefillId = existingLog.productRefillId,
        //        productrefillDateAdded = existingLog.productrefillDateAdded,
        //        deliveryDetailsId = adminData.deliveryId,
        //        deliveryDetailsDateAdded = adminData.dateAdded
        //    };

        //    twoBigDB.Update("USERSLOG/" + log.logsId, log);
        //}



        //protected void btnDeliverydetails_Click(object sender, EventArgs e) //WORKING- FLAT STRUCTURE
        //{
        //    var idno = (string)Session["idno"];
        //    int adminId = int.Parse(idno);

        //    Random rnd = new Random();
        //    int deliveryId = rnd.Next(1, 10000);



        //    // Create the delivery object and set its properties based on the selected delivery types
        //    var delivery = new DeliveryDetails
        //    {
        //        adminId = adminId,
        //        deliveryId = deliveryId,
        //        dateAdded = DateTimeOffset.UtcNow
        //    };

        //    // Loop through the items in the CheckBoxList to check which delivery types are selected
        //    foreach (ListItem item in radDevType.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            switch (item.Value)
        //            {
        //                case "Standard":
        //                    int standardID = rnd.Next(1, 10000);
        //                    // Set the properties for standard delivery
        //                    delivery.stanDeliverytype = "Standard";
        //                    delivery.standardID = standardID;
        //                    delivery.stanDeliveryFee = DeliveryFee.Text;
        //                    delivery.stanDeliveryTime = standardSchedFrom.Text + "AM - " + standardSchedTo.Text + "PM";
        //                    delivery.standistance = FreeDelivery.Text;
        //                    delivery.stanOrderType = GetSelectedValues(DeliveryType);
        //                    delivery.stanOrderMethod = GetSelectedValues(OrderMethod);
        //                    delivery.standardSwapOptions = GetSelectedValues(standardSwapOptions);
        //                    break;
        //                case "Reservation":
        //                    int reserveID = rnd.Next(1, 10000);
        //                    // Set the properties for reservation delivery
        //                    delivery.resDeliveryType = "Reservation";
        //                    delivery.reservationID = reserveID;
        //                    delivery.resDeliveryFee = resDelFee.Text;
        //                    delivery.resDistanceFree = resFreeDel.Text;
        //                    delivery.resOrderMethod = GetSelectedValues(reserveOrderMethod);
        //                    delivery.resOrderType = GetSelectedValues(reserveOrderType);
        //                    delivery.reserveSwapOptions = GetSelectedValues(reserveSwap);
        //                    break;
        //                case "Express":
        //                    int expressID = rnd.Next(1, 10000);
        //                    // Set the properties for express delivery
        //                    delivery.exDeliveryType = "Express";
        //                    delivery.expressID = expressID;
        //                    delivery.exDeliveryFee = expressdeliveryFee.Text;
        //                    delivery.exEstimatedDelivery = estimatedTime.Text;
        //                    delivery.exOrderType = GetSelectedValues(expressOrderType);
        //                    delivery.exOrderMethod = GetSelectedValues(expressOrderMethod);
        //                    delivery.expressSwapOptions = GetSelectedValues(expressSwap);
        //                    break;
        //            }
        //        }
        //    }

        //    // Save the delivery object to the database under the DELIVERY_DETAILS2 -> deliveryID location
        //    SetResponse response = twoBigDB.Set("DELIVERY_DETAILS2/" + deliveryId, delivery);
        //    DeliveryDetails res = response.ResultAs<DeliveryDetails>();
        //    Response.Write("<script>alert ('You successfully created the Delivery Types you offer to your business');  window.location.href = '/Admin/WaterProduct.aspx'; </script>");

        //    int logsId = (int)Session["logsId"];
        //    // Retrieve the existing Users log object from the database
        //    FirebaseResponse resLog = twoBigDB.Get("USERSLOG/" + logsId);
        //    UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

        //    // Get the current date and time
        //    //DateTime addedTime = DateTime.UtcNow;

        //    // Log user activity
        //    var log = new UsersLogs
        //    {
        //        userIdnum = int.Parse(idno),
        //        logsId = logsId,
        //        userFullname = (string)Session["fullname"],
        //        emp_id = existingLog.emp_id,
        //        empFullname = existingLog.empFullname,
        //        empDateAdded = existingLog.empDateAdded,
        //        dateLogin = existingLog.dateLogin,
        //        tankId = existingLog.tankId,
        //        tankSupplyDateAdded = existingLog.tankSupplyDateAdded,
        //        other_productId = existingLog.other_productId,
        //        otherProductDateAdded = existingLog.otherProductDateAdded,
        //        productRefillId = existingLog.productRefillId,
        //        productrefillDateAdded = existingLog.productrefillDateAdded,
        //        deliveryDetailsId = delivery.deliveryId,
        //        deliveryDetailsDateAdded = delivery.dateAdded
        //    };

        //    twoBigDB.Update("USERSLOG/" + log.logsId, log);
        //}

        // Helper method to get the selected values from a CheckBoxList

        protected void btnDeliverydetails_Click(object sender, EventArgs e)
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

            // Check if there is an existing delivery object for this admin
            FirebaseResponse resDelivery = twoBigDB.Get("DELIVERY_DETAILS2/" + adminId);
            DeliveryDetails delivery = null;
            if (resDelivery.Body != "null")
            {
                delivery = resDelivery.ResultAs<DeliveryDetails>();
            }
            else
            {
                // If there is no existing delivery object, create a new one
                Random rnd = new Random();
                int deliveryId = rnd.Next(1, 10000);
                delivery = new DeliveryDetails
                {
                    adminId = adminId,
                    deliveryId = adminId,
                    
                };
                Session["deliveryID"] = deliveryId;
            }

            // Loop through the items in the CheckBoxList to check which delivery types are selected
            foreach (ListItem item in radDevType.Items)
            {
                if (item.Selected)
                {
                    switch (item.Value)
                    {
                        case "Standard":
                            // check If there is an existing standard delivery type
                            if (delivery.stanDeliverytype == "Standard")
                            {
                                Response.Write("<script>alert ('FAILED! Existing STANDARD Delivery type! You already created Standard delivery.');  window.location.href = '/Admin/WaterProduct.aspx'; </script>");
                                break;
                            }
                            else
                            {
                                int standardID = new Random().Next(1, 10000);
                                delivery.stanDeliverytype = "Standard";
                                delivery.standardDateAdded = DateTimeOffset.UtcNow;
                                delivery.standardID = standardID;
                                delivery.stanDeliveryFee = DeliveryFee.Text;
                                delivery.stanDeliveryTime = standardSchedFrom.Text + "AM - " + standardSchedTo.Text + "PM";
                                delivery.standistance = FreeDelivery.Text;
                                delivery.stanOrderType = GetSelectedValues(DeliveryType);
                                delivery.stanOrderMethod = GetSelectedValues(OrderMethod);
                                delivery.standardSwapOptions = GetSelectedValues(standardSwapOptions);
                            }
                            break;
                        case "Reservation":
                            // If there is an existing reservation delivery object, update it, otherwise create a new one
                            if (delivery.resDeliveryType == "Reservation")
                            {
                                Response.Write("<script>alert ('FAILED! Existing RESERVATION Delivery type! You already created Reservation delivery.');  window.location.href = '/Admin/WaterProduct.aspx'; </script>");
                                break;
                            }
                            else
                            {
                                int reserveID = new Random().Next(1, 10000);
                                delivery.resDeliveryType = "Reservation";
                                delivery.reservationdateAdded = DateTimeOffset.UtcNow;
                                delivery.reservationID = reserveID;
                                delivery.resDeliveryFee = resDelFee.Text;
                                delivery.resDistanceFree = resFreeDel.Text;
                                delivery.resOrderMethod = GetSelectedValues(reserveOrderMethod);
                                delivery.resOrderType = GetSelectedValues(reserveOrderType);
                                delivery.reserveSwapOptions = GetSelectedValues(reserveSwap);
                            }
                            break;
                        case "Express":
                            // check If there is an existing express delivery type
                            if (delivery.exDeliveryType == "Express")
                            {
                                Response.Write("<script>alert ('FAILED! Existing EXPRESS Delivery type! You already created Express delivery.');  window.location.href = '/Admin/WaterProduct.aspx'; </script>");
                                break;
                            }
                            else
                            {
                                int expressID = new Random().Next(1, 10000);
                                delivery.exDeliveryType = "Express";
                                delivery.expressdateAdded = DateTimeOffset.UtcNow;
                                delivery.expressID = expressID;
                                delivery.exDeliveryFee = expressdeliveryFee.Text;
                                delivery.exEstimatedDelivery = estimatedTime.Text;
                                delivery.exOrderMethod = GetSelectedValues(expressOrderMethod);
                                delivery.exOrderType = GetSelectedValues(expressOrderType);
                                delivery.expressSwapOptions = GetSelectedValues(expressSwap);
                            }
                            break;
                    }
                }
            }   
            // Save the updated delivery object to the database
            FirebaseResponse res = twoBigDB.Set("DELIVERY_DETAILS2/" + adminId, delivery);
            Response.Write("<script>alert ('You successfully created the Delivery Types you offer to your business');  window.location.href = '/Admin/WaterProduct.aspx'; </script>");

            int logsId = (int)Session["logsId"];
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
                deliveryDetailsId = delivery.deliveryId,
                standardAdded = delivery.standardDateAdded,
                reservationAdded = delivery.reservationdateAdded,
                expressAdded = delivery.expressdateAdded
            };

            twoBigDB.Update("USERSLOG/" + log.logsId, log);
        }
        private string GetSelectedValues(CheckBoxList checkboxList)
        {
            string values = "";
            foreach (ListItem item in checkboxList.Items)
            {
                if (item.Selected)
                {
                    values += item.Value + ", ";
                }
            }
            if (!string.IsNullOrEmpty(values))
            {
                values = values.TrimEnd(',', ' '); // remove trailing comma and space
            }
            return values;
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
                    gridDeliveryDetails.Visible = false;
                    gridotherProduct.Visible = false;
                        productRefillDisplay();
                    } 
                    else if (selectedOption == "2")
                    {
                        lblProductData.Text = "OTHER PRODUCT";
                        gridProductRefill.Visible = false;
                    gridDeliveryDetails.Visible = false;
                    gridotherProduct.Visible = true;
                        otherProductsDisplay();
                    }
                    else if (selectedOption == "3")
                    {
                        lblProductData.Text = "DELIVERY DETAILS";
                        gridProductRefill.Visible = false;
                        gridotherProduct.Visible = false;
                        gridDeliveryDetails.Visible = true;
                        deliveryExpressDisplay();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/WaterProduct.aspx';" + ex.Message);
                }
            }
        //SEARCH DELIVERY TYPE
        protected void btnSearchDeliverytype_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedOption = drdDeliverytype.SelectedValue;


                if (selectedOption == "1")
                {
                    lblDeliveryType.Text = "EXPRESS";
                    standardGridview.Visible = false;
                    reservationGridView.Visible = false;
                    expressGridview.Visible = true;
                    expressDisplay();
                }
                else if (selectedOption == "2")
                {
                    lblDeliveryType.Text = "STANDARD";
                    expressGridview.Visible = false;
                    reservationGridView.Visible = false;
                    standardGridview.Visible = true;
                    standardDisplay();
                }
                else if (selectedOption == "3")
                {
                    lblDeliveryType.Text = "RESERVATION";
                    expressGridview.Visible = false;
                    standardGridview.Visible = false;
                    reservationGridView.Visible = true;
                    reservationDisplay();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/WaterProduct.aspx';" + ex.Message);
            }
        }
        //DISPLAY EXPRESS DETAILS
        private void expressDisplay()
        {
            nullLabel.Text = "";
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all data from the DELIVERY_DETAILS table
            FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS2");
            Dictionary<string, DeliveryDetails> deliveryList = response.ResultAs<Dictionary<string, DeliveryDetails>>();
            var filteredList = deliveryList.Values.Where(d => d.adminId.ToString() == idno);


            // Create the DataTable to hold the orders
            //sa pag create sa table
            DataTable expressTable = new DataTable();
            expressTable.Columns.Add("EXPRESS ID");
            expressTable.Columns.Add("ESTIMATED DELIVERY TIME");
            expressTable.Columns.Add("DELIVERY FEE");
            expressTable.Columns.Add("REFILL SWAP OPTIONS");
            expressTable.Columns.Add("ORDER TYPE");
            expressTable.Columns.Add("ORDER METHOD");
            expressTable.Columns.Add("DATE ADDED");
            expressTable.Columns.Add("PAYMENT METHOD");
            expressTable.Columns.Add("ADDED BY");

            if (response != null && response.ResultAs<DeliveryDetails>() != null)
            {
                foreach (var entry in filteredList)
                {
                    if(entry.exDeliveryType == "Express")
                    {
                        expressTable.Rows.Add(entry.expressID, entry.exEstimatedDelivery, entry.exDeliveryFee, entry.expressSwapOptions,
                                          entry.exOrderType, entry.exOrderMethod, entry.expressdateAdded, entry.paymentMethods, entry.adminId);
                    }
                    else
                    { 
                        nullLabel.Text = "No 'Express Delivery' data avaialble";
                    }
                   
                }

            }
            else
            {
                // Handle null response or invalid selected value
                nullLabel.Text = "No 'Express Delivery' data avaialble";
            }

            // Bind the DataTable to the GridView
            expressGridview.DataSource = expressTable;
            expressGridview.DataBind();


            drdDeliverytype.Visible = true;
            btnDeliverytype.Visible = true;
        }
        private void standardDisplay()
        {
            nullLabel.Text = "";

            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all data from the DELIVERY_DETAILS table
            FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS2");
            Dictionary<string, DeliveryDetails> deliveryList = response.ResultAs<Dictionary<string, DeliveryDetails>>();
            var filteredList = deliveryList.Values.Where(d => d.adminId.ToString() == idno);

            // Create the DataTable to hold the details
            DataTable standardTable = new DataTable();
            standardTable.Columns.Add("STANDARD ID");
            standardTable.Columns.Add("TIME SCHEDULE FOR DELIVERY");
            standardTable.Columns.Add("DISTANCE FOR FREE DELIVERY ");
            standardTable.Columns.Add("DELIVERY FEE");
            standardTable.Columns.Add("REFILL SWAP OPTIONS");
            standardTable.Columns.Add("ORDER TYPE");
            standardTable.Columns.Add("ORDER METHOD");
            standardTable.Columns.Add("DATE ADDED");
            standardTable.Columns.Add("PAYMENT METHOD");
            standardTable.Columns.Add("ADDED BY");

            if (response != null && response.ResultAs<DeliveryDetails>() != null)
            {
                foreach (var entry in filteredList)
                {
                    if (entry.stanDeliverytype == "Standard")
                    {
                        standardTable.Rows.Add(entry.standardID, entry.stanDeliveryTime, entry.standistance, entry.stanDeliveryFee,
                       entry.standardSwapOptions, entry.stanOrderType, entry.stanOrderMethod, entry.standardDateAdded, entry.paymentMethods, entry.adminId);
                    }
                    else
                    {
                        nullLabel.Text = "No 'Standard Delivery' data avaialble";
                    }
                       
                }
            }
            else
            {
                // Handle null response or invalid selected value
                nullLabel.Text = "No 'Standard Delivery' data avaialble";
            }

            // Bind the DataTable to the GridView
            standardGridview.DataSource = standardTable;
            standardGridview.DataBind();

            drdDeliverytype.Visible = true;
            btnDeliverytype.Visible = true;
        }
        //DISPLAY RESERVATION DETAILS
        private void reservationDisplay()
        {
            nullLabel.Text = "";
            string idno = (string)Session["idno"];
            string empId = (string)Session["emp_id"];
            // int adminId = int.Parse(idno);

            FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS2");
            Dictionary<string, DeliveryDetails> deliveryList = response.ResultAs<Dictionary<string, DeliveryDetails>>();
                var filteredList = deliveryList.Values.Where(d => d.adminId.ToString() == idno);


                // Create the DataTable to hold the orders
                //sa pag create sa table
                DataTable reservationTable = new DataTable();
                reservationTable.Columns.Add("RESERVATION ID");
                reservationTable.Columns.Add("DISTANCE FOR FREE DELIVERY");
                reservationTable.Columns.Add("DELIVERY FEE");
                reservationTable.Columns.Add("REFILL SWAP OPTIONS");
                reservationTable.Columns.Add("ORDER TYPE");
                reservationTable.Columns.Add("ORDER METHOD");
                reservationTable.Columns.Add("DATE ADDED");
                reservationTable.Columns.Add("PAYMENT METHOD");
                reservationTable.Columns.Add("ADDED BY");

                if (response != null && response.ResultAs<DeliveryDetails>() != null)
                {
                    foreach (var entry in filteredList)
                    {
                        if (entry.resDeliveryType == "Reservation")
                        {
                            reservationTable.Rows.Add(entry.reservationID, entry.resDistanceFree, entry.resDeliveryFee, entry.reserveSwapOptions,
                                                  entry.resOrderType, entry.resOrderMethod, entry.reservationdateAdded, entry.paymentMethods, entry.adminId);
                        }
                        else
                        {
                        nullLabel.Text = "No 'Reservation Delivery' data avaialble";
                        }
                      
                    }


                }
                else
                {
                // Handle null response or invalid selected value
                nullLabel.Text = "No 'Reservation Delivery' data avaialble";
            }

                // Bind the DataTable to the GridView
                reservationGridView.DataSource = reservationTable;
                reservationGridView.DataBind();


                drdDeliverytype.Visible = true;
                btnDeliverytype.Visible = true;
        }

        protected void paymentButton_Click(object sender, EventArgs e)
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

            // Check if there is an existing delivery object for this admin
            FirebaseResponse paymentResponse = twoBigDB.Get("DELIVERY_DETAILS2/" + adminId);
            //Model.AdminAccount update = updateAdmin.ResultAs<Model.AdminAccount>();
            Model.DeliveryDetails payment = paymentResponse.ResultAs<Model.DeliveryDetails>();

            if (payment.paymentMethods == null)
            {
                // Loop through the items in the CheckBoxList to build the orderMethod string
                string paymentMethods = "";
                foreach (ListItem item in paymentsCheckBox.Items)
                {
                    if (item.Selected)
                    {
                        paymentMethods += item.Value + ", ";
                    }
                }
                paymentMethods = paymentMethods.TrimEnd(' ', ',');

                payment.paymentMethods = paymentMethods;
                payment.gcashNumber = gcashnum.Text;

                paymentResponse = twoBigDB.Update("DELIVERY_DETAILS2/" + adminId, payment);
                Response.Write("<script>alert ('Payment Methods successsfully added');  window.location.href = '/Admin/WaterProduct.aspx'; </script>");
            }
            else
            {
                Response.Write("<script>alert ('FAILED !! Payment Method has been already set-up.');  window.location.href = '/Admin/WaterProduct.aspx'; </script>");
            }

           
            

           



        }
    }
}