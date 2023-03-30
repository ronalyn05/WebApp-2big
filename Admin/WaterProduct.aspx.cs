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
                //  DisplayID();
                //GetTankSupplyForToday();
                //productRefillDisplay();
                //otherProductsDisplay();
                //deliveryDetailsDisplay();
               // tanksupplyDisplay();
            }

            string idno = (string)Session["idno"];
            //int tankID = (int)Session["tankId"];

            //lbl_Date.Text = (string)Session["dateAdded"];
            //lbltankSupply.Text = (string)Session["tankVolume"] + " " + (string)Session["tankUnit"];

            if (Session["dateAdded"] != null && Session["tankVolume"] != null && Session["tankUnit"] != null)
            {
                DateTime dateAdded = (DateTime)Session["dateAdded"];
                date.Text = dateAdded.ToString(); // format the date as a string

                TankSupply.Text = (string)Session["tankVolume"] + " " + (string)Session["tankUnit"];
            }
            //date.Text = (string)Session["dateAdded"];
            //TankSupply.Text = (string)Session["tankVolume"] + " " + (string)Session["tankUnit"];

            FirebaseResponse response;
            //THIS RETRIEVE THE DATA FORM THE TANKSUPPLY TBL
            //response = twoBigDB.Get("TANKSUPPLY/");
            //TankSupply obj = response.ResultAs<TankSupply>();

            //THIS RETRIEVE THE DATA FROM THE ORDERS TBL
            var result = twoBigDB.Get("ORDERS/");
            Order order = result.ResultAs<Order>();
            // state the variable for orders
            if (order != null && order.order_size != null && order.order_unit != null)
            {

                //state the variable for tank supply
                string tankVolume = (string)Session["tankVolume"];
                string tankUnit = (string)Session["tankUnit"];
                string date = (string)Session["dateAdded"];

                //state the variable for orders
                Session["size"] = order.order_size;
                Session["unit"] = order.order_unit;
                Session["quantity"] = order.order_Quantity;
           
                //state the variable for orders
                int orderSize = (int)Session["size"];
                string orderUnit = (string)Session["unit"];
                int qty = (int)Session["quantity"];

                //Coversion
                double tankCapacity = double.Parse(tankVolume + tankUnit);
                double orderedVolume = double.Parse(orderSize + orderUnit);
                double gallonsPerLiter = 0.26417205236; // conversion factor from gallon to liters
                double gallonsPerML = 3785.41; // conversion factor from gallons to milliliters

                //quantity of the customers ordered
                double orders = qty * orderedVolume;

                // convert ordered volume to gallons or milliliters based on order unit
                double orderedGallons = 0;
                if (orderUnit == "L" || orderUnit == "liter/s")
                {
                    orderedGallons = orders * gallonsPerLiter;
                }
                else if (orderUnit == "mL" || orderUnit == "ML" || orderUnit == "milliliters")
                {
                    orderedGallons = orders / gallonsPerML;
                }

                // calculate remaining supply in gallons
                double remainingSupply = tankCapacity - orderedGallons;

                // check if remaining supply is less than 0
                if (remainingSupply <= 0)
                {
                    //Console.WriteLine("Sorry, we don't have enough alkaline water in stock to fulfill your order.");
                   // The customer will be notified. Apply a firebase clound messaging here
                }
                else
                {
                    // display the remaining supply and the variable for tank supply
                    string tankSupply = $"{tankVolume} {tankUnit}";
                    lbl_Date.Text = date;
                    lbltankSupply.Text = ($"Remaining supply in your tank is ({tankSupply})");
                    lblremainingSupply.Text = ($"Remaining supply of alkaline water in the owner's tank ({tankSupply}): {remainingSupply} gallons");
                   // Console.WriteLine($"Remaining supply of alkaline water in the owner's tank ({tankSupply}): {remainingSupply} gallons");
                }

                } 
                else
                {
                    lbltankSupply.Text = "No data available for today! Add tank supply...";
                }

            //if (tankUnit != null && tankVolume != null)
            //{
            //    lbltankSupply.Text = "Your tank supply added for today is: " + (string)Session["tankVolume"] + " " + (string)Session["tankUnit"];

            //    string orders = order.order_size + order.order_unit 
            //   // int orders = orderSize * qty;

            //    //lblremainingSupply.Text = orderVolume - orders.ToString();

            //}
            //else
            //    {
            //        lbltankSupply.Text = "No data available for today! You first need to add your water tank supply for today!";
            //    }

        }
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
            string empId = (string)Session["emp_id"];
            // int adminId = int.Parse(idno);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("PRODUCTREFILL");
            Dictionary<string, ProductRefill> productrefillList = response.ResultAs<Dictionary<string, ProductRefill>>();
            var filteredList = productrefillList.Values.Where(d => d.adminId.ToString() == idno);
            //FirebaseResponse response = twoBigDB.Get("PRODUCTREFILL/" + idno);
            //ProductRefill emp = response.ResultAs<ProductRefill>();
            //var data = response.Body;
            ////Dictionary<string, Employee> employeeList = JsonConvert.DeserializeObject<Dictionary<string, Employee>>(data);
            //Dictionary<string, ProductRefill> productrefillList = response.ResultAs<Dictionary<string, ProductRefill>>();

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
            string empId = (string)Session["emp_id"];
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
        //private void deliveryDetailsDisplay()
        //{
        //    string idno = (string)Session["idno"];
        //    string empId = (string)Session["emp_id"];
        //     int adminId = int.Parse(idno);

        //     Retrieve the delivery details for the logged in user
        //    FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS");
        //    Dictionary<string, DeliveryDetails> deliverydetailsList = response.ResultAs<Dictionary<string, DeliveryDetails>>();
        //    var filteredList = deliverydetailsList.Values.Where(d => d.adminId.ToString() == idno);

        //     Retrieve all orders from the ORDERS table
        //    FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS/" + idno);
        //    DeliveryDetails emp = response.ResultAs<DeliveryDetails>();
        //    var data = response.Body;
        //    //Dictionary<string, Employee> employeeList = JsonConvert.DeserializeObject<Dictionary<string, Employee>>(data);
        //    Dictionary<string, DeliveryDetails> deliverydetailsList = response.ResultAs<Dictionary<string, DeliveryDetails>>();

        //     Create the DataTable to hold the orders
        //    sa pag create sa table
        //    DataTable deliverydetailsTable = new DataTable();
        //    deliverydetailsTable.Columns.Add("DELVERY ID");
        //    deliverydetailsTable.Columns.Add("DELVERY TYPE");
        //    deliverydetailsTable.Columns.Add("DELIVERY FEE");
        //    deliverydetailsTable.Columns.Add("DELIVERY DISTANCE");
        //    deliverydetailsTable.Columns.Add("ESTIMATED TIME");
        //    deliverydetailsTable.Columns.Add("ORDER TYPE");
        //    deliverydetailsTable.Columns.Add("ORDER OPTION");
        //    deliverydetailsTable.Columns.Add("DATE ADDED");
        //    deliverydetailsTable.Columns.Add("ADDED BY");

        //    if (response != null && response.ResultAs<DeliveryDetails>() != null)
        //    {
        //         Loop through the orders and add them to the DataTable
        //        foreach (KeyValuePair<string, DeliveryDetails> entry in filteredList)
        //        {

        //            deliverydetailsTable.Rows.Add(entry.Value.deliveryId, entry.Value.deliveryType,
        //                                 entry.Value.deliveryFee, entry.Value.deliveryDistance, entry.Value.estimatedTime,
        //                                 entry.Value.orderType, entry.Value.orderMethod, entry.Value.dateAdded, entry.Value.adminId);
        //        }
        //        foreach (var deliveryDetail in filteredList)
        //        {
        //            deliverydetailsTable.Rows.Add(deliveryDetail.deliveryId, deliveryDetail.deliveryType,
        //                                           deliveryDetail.deliveryFee, deliveryDetail.deliveryDistance, deliveryDetail.estimatedTime,
        //                                           deliveryDetail.orderType, deliveryDetail.orderMethod, deliveryDetail.dateAdded, deliveryDetail.adminId);
        //        }
        //    }
        //    else
        //    {
        //         Handle null response or invalid selected value
        //        deliverydetailsTable.Rows.Add("No data found", "", "", "", "", "", "");
        //    }

        //     Bind the DataTable to the GridView
        //    gridDeliveryDetails.DataSource = deliverydetailsTable;
        //    gridDeliveryDetails.DataBind();
        //}

        //TO BE UPDATED
        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string idno = (string)Session["idno"];
            //    string selected = ListBox1.SelectedValue;
            //    //FirebaseResponse response = twoBigDB.Get("ADMIN/" + idno + "/Product/" + selected);
            //    FirebaseResponse response = twoBigDB.Get("Product/" + selected);

            //    if (response != null && response.ResultAs<Products>() != null)
            //    {
            //        Products obj = response.ResultAs<Products>();
            //        LabelID.Text = obj.productId.ToString();
            //        prodName.Text = obj.productName.ToString();
            //        prodSize.Text = obj.productSize.ToString();
            //        prodPrice.Text = obj.productPrice.ToString();
            //        prodDiscount.Text = obj.productDiscount.ToString();
            //        prodAvailable.Text = obj.productStock.ToString();
            //       // waterSupAvailable.Text = obj.waterRefillSupply.ToString(); 
            //        LblDate.Text = obj.dateAdded.ToString();
            //    }
            //    else
            //    {
            //        // Handle null response or invalid selected value
            //        LabelID.Text = "";
            //        prodName.Text = "";
            //        prodSize.Text = "";
            //        prodPrice.Text = "";
            //        prodDiscount.Text = "";
            //        prodAvailable.Text = "";
            //        waterSupAvailable.Text = "";
            //        LblDate.Text = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Handle exception
            //    LabelID.Text = "";
            //    prodName.Text = "";
            //    prodSize.Text = "";
            //    prodPrice.Text = "";
            //    prodDiscount.Text = "";
            //    prodAvailable.Text = "";
            //    waterSupAvailable.Text = "";
            //    LblDate.Text = "";
            //}
            //// string idno = (string)Session["idno"];
            //// String slected;
            //// slected = ListBox1.SelectedValue;
            //// FirebaseResponse response;
            //// response = twoBigDB.Get("ADMIN/" + idno + "/Product/" + slected);
            ////// response = twoBigDB.Get("PRODUCT/" + slected);
            //// Products obj = response.ResultAs<Products>();
            //// LabelID.Text = obj.productId.ToString();
            //// prodName.Text = obj.productName.ToString();
            //// prodSize.Text = obj.productSize.ToString();
            //// prodPrice.Text = obj.productPrice.ToString();
            //// prodDiscount.Text = obj.productDiscount.ToString();
            //// prodAvailable.Text = obj.productAvailable.ToString();
            //// waterSupAvailable.Text = obj.waterRefillSupply.ToString();
            //// LblDate.Text = obj.dateAdded.ToString();
        }

        protected void btnAddSupply_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

            try
            {
                // INSERT DATA TO TABLE = TANKSUPPLY
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                var data = new TankSupply
                {
                    adminId = adminId,
                    tankId = idnum,
                    tankUnit = drdTankUnit.SelectedValue,
                    tankVolume = tankSize.Text,
                    dateAdded = DateTime.UtcNow
                };

                SetResponse response;
                //TANKSUPPLY = tablename, Idno = key(PK ? )
                // response = twoBigDB.Set("ADMIN/" + idno + "/TANKSUPPLY/" + data.productId, data);
                response = twoBigDB.Set("TANKSUPPLY/" + data.tankId, data);
                TankSupply result = response.ResultAs<TankSupply>();
                //btnAddSupply.Enabled = false;

                // Get the data from the database
                //FirebaseResponse res = twoBigDB.Get("TANKSUPPLY/" + data.tankId);
                //TankSupply obj = res.ResultAs<TankSupply>();

                // Display the tank supply result here
                lbl_Date.Text = data.dateAdded.ToString();
                lbltankSupply.Text = data.tankVolume + ' ' + data.tankUnit;
               // lblremainingSupply.Text = ""; // Set this value as needed

                // btnAddSupply.Enabled = false;

                //if (res.Body != null)
                //{
                //    TankSupply obj = res.ResultAs<TankSupply>();

                // Display the tank supply result here
                //lbl_Date.Text = obj.dateAdded.ToString();
                //lbltankSupply.Text = obj.tankVolume.ToString() + ' ' + obj.tankUnit.ToString();

                //date.Text = obj.dateAdded.ToString();
                //TankSupply.Text = obj.tankVolume.ToString() + ' ' + obj.tankUnit.ToString();

                Response.Write("<script>alert ('Tank supply for today with id number: " + data.tankId + " is successfully added!'); location.reload(); window.location.href = '/Admin/WaterProduct.aspx'; </script>");
               // };

                //// Get the data in the database
                // var res = twoBigDB.Get("TANKSUPPLY/" + data.tankId);
                // TankSupply obj = res.ResultAs<TankSupply>();

                // //display the tank supply result here
                // lbl_Date.Text = obj.dateAdded.ToString();
                // lbltankSupply.Text = obj.tankVolume.ToString() + ' ' + obj.tankUnit.ToString();

                Response.Write("<script>alert ('Tank supply for today with id number: " + data.tankId + " is successfully added!'); location.reload(); window.location.href = '/Admin/WaterProduct.aspx'; </script>");
                
            }

            catch (Exception ex)
            {
                Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/WaterProduct.aspx';" + ex.Message);
            }
        }


        //STORING DATA TO otherPRODUCT
        protected async void btnAdd_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

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
                Response.Write("<script>alert ('Your other water product with Id number: " + data.other_productId + " is successfully added!'); location.reload(); window.location.href = '/Admin/WaterProduct.aspx'; </script>");
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/WaterProduct.aspx';" + ex.Message);
            }
        }
        //STORING DATA TO PRODUCT REFILL
        protected async void btnSet_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            try
            {
                // INSERT DATA TO TABLE = 
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
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        //STORING DATA TO DELIVERY_DETAILS
        protected void btnDeliverydetails_Click(object sender, EventArgs e)
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

            Random rnd = new Random();
            int deliveryId = rnd.Next(1, 10000);
            int standardID = rnd.Next(1, 10000);
            int expressID = rnd.Next(1, 10000);
            int reservationDelivery = rnd.Next(1, 10000);

            var adminData = new DeliveryDetails
            {
                adminId = adminId,
                deliveryId = deliveryId
            };

            SetResponse response;
            response = twoBigDB.Set("DELIVERY_DETAILS/" + deliveryId, adminData);

            //store the generated deliveryID into session
            Session["deliveryID"] = deliveryId;

            // Loop through the items in the radiobutton list to build the deliveryType string
            string deliveryType = "";
            foreach (ListItem item in radDevType.Items)
            {
                if (item.Selected)
                {
                    deliveryType += item.Value + ", ";
                }
            }
            deliveryType = deliveryType.TrimEnd(' ', ',');

            //IF STANDARD ANG PILION
           if (deliveryType == "Standard")
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

                //store the value of session into deliveryID
                int deliveryID = (int)Session["deliveryID"];

                // Retrieve the existing product data from the database
                var result = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryID);
                DeliveryDetails obj = result.ResultAs<DeliveryDetails>();

                //delivery ID fetched from database
                int delID = obj.deliveryId;
                
                //compare the deliveryID fetched from database and the deliveryID from the session
                if(delID == deliveryID)
                {
                    var standard = new standardDelivery
                    {
                        stanDeliverytype = deliveryType,
                        stanDeliveryFee = int.Parse(DeliveryFee.Text), // DELIVERY FEE for STANDARD and RESERVATION
                        stanDeliveryTime = standardSchedFrom.Text + "AM - " + standardSchedTo.Text + "PM", //for STANDARD ONLY
                        standistance = int.Parse(FreeDelivery.Text), //FREE DELIVERY for STANDARD and RESERVATION
                        stanOrderType = orderType,
                        stanOrderMethod = orderMethod,
                        standardID = standardID,
                        dateAdded = DateTime.UtcNow
                    };

                    SetResponse standardre;
                    standardre = twoBigDB.Set("DELIVERY_DETAILS/" + delID + "/deliveryTypes/" + standard.standardID, standard);
                    DeliveryDetails res = standardre.ResultAs<DeliveryDetails>();

                    Response.Write("<script>alert ('You successfully created the " + standard.stanDeliverytype + " Delivery with ID number: " + standard.standardID + "');  window.location.href = '/Admin/WaterProduct.aspx'; </script>");
                }

            }            
           //IF RESERVATION ANG PILION
            else if (deliveryType == "Reservation")
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

                //store the value of session into deliveryID
                int deliveryID = (int)Session["deliveryID"];

                // Retrieve the existing product data from the database
                var result = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryID);
                DeliveryDetails obj = result.ResultAs<DeliveryDetails>();

                //delivery ID fetched from database
                int delID = obj.deliveryId;

                //compare the deliveryID fetched from database and the deliveryID from the session
                if (delID == deliveryID)
                {
                    var reservation = new reservationDelivery
                    {
                        reservationID = reservationDelivery,
                        resDeliveryType = deliveryType,
                        resDeliveryFee = int.Parse(resDelFee.Text),
                        resDistanceFree = int.Parse(resFreeDel.Text),
                        resOrderMethod = resOrderMethod,
                        resOrderType = resOrderType,
                        dateAdded = DateTime.UtcNow
                    };
                    SetResponse standardre;
                    standardre = twoBigDB.Set("DELIVERY_DETAILS/" + delID + "/deliveryTypes/" + reservation.reservationID, reservation);
                    DeliveryDetails res = standardre.ResultAs<DeliveryDetails>();

                    Response.Write("<script>alert ('You successfully created the " + reservation.resDeliveryType + " Delivery with ID number: " + reservation.reservationID + "');  window.location.href = '/Admin/WaterProduct.aspx'; </script>");
                }
            }
            //IF EXPRESS ANG PILION
            else if (deliveryType == "Express")
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

                //store the value of session into deliveryID
                int deliveryID = (int)Session["deliveryID"];

                // Retrieve the existing product data from the database
                var result = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryID);
                DeliveryDetails obj = result.ResultAs<DeliveryDetails>();

                //delivery ID fetched from database
                int delID = obj.deliveryId;

                //compare the deliveryID fetched from database and the deliveryID from the session
                if (delID == deliveryID)
                {
                    var express = new expressDelivery
                    {
                        expressID = expressID,
                        exDeliveryType = deliveryType,
                        exEstimatedDelivery = estimatedTime.Text,
                        exDeliveryFee = int.Parse(expressdeliveryFee.Text),
                        exOrderMethod = resOrderMethod,
                        exOrderType = resOrderType,
                        dateAdded = DateTime.UtcNow
                    };
                    SetResponse standardre;
                    standardre = twoBigDB.Set("DELIVERY_DETAILS/" + delID + "/deliveryTypes/" + express.expressID, express);
                    DeliveryDetails res = standardre.ResultAs<DeliveryDetails>();

                    Response.Write("<script>alert ('You successfully created the " + express.exDeliveryType + " Delivery with ID number: " + express.expressID + "');  window.location.href = '/Admin/WaterProduct.aspx'; </script>");
                }
            }



        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedOption = ddlSearchOptions.SelectedValue;
            
            if (selectedOption == "1")
            {
                lblProductRefill.Text = "PRODUCT REFILL";
                lbldeliveryDetails.Visible = false;
                lblotherProduct.Visible = false;
                gridProductRefill.Visible = true;
                gridotherProduct.Visible = false;
                gridDeliveryDetails.Visible = false;
                productRefillDisplay();
            } 
            else if (selectedOption == "2")
            {
                lblotherProduct.Text = "OTHER PRODUCT";
                lbldeliveryDetails.Visible = false;
                lblProductRefill.Visible = false;
                gridProductRefill.Visible = false;
                gridotherProduct.Visible = true;
                gridDeliveryDetails.Visible = false;
                otherProductsDisplay();
            }
            else if (selectedOption == "3")
            {
                lbldeliveryDetails.Text = "DELIVERY DETAILS";
                lblProductRefill.Visible = false;
                lblotherProduct.Visible = false;
                gridProductRefill.Visible = false;
                gridotherProduct.Visible = false;
                gridDeliveryDetails.Visible = true;
                //deliveryDetailsDisplay();
            }
        }



        //Display product ni diri
        //private void DisplayID()
        //{
        //    string idno = (string)Session["idno"];
        //    FirebaseResponse response;
        //    //response = twoBigDB.Get("ADMIN/" + idno + "/Product/");
        //    response = twoBigDB.Get("PRODUCTS/" + idno + );
        //    Products obj = response.ResultAs<Products>();
        //    var json = response.Body;
        //    Dictionary<string, Products> list = JsonConvert.DeserializeObject<Dictionary<string, Products>>(json);


        //    if (list != null && list.Count > 0)
        //    {
        //        foreach (KeyValuePair<string, Products> entry in list)
        //        {
        //            ListBox1.Items.Add(entry.Value.productId.ToString());
        //        }
        //    }

        //}

        protected async void btnEdit_Click(object sender, EventArgs e)
        {
          //  string idno = (string)Session["idno"];
          //  int adminId = int.Parse(idno);
          //  String deleteStr = ListBox1.SelectedValue;

          //  // Retrieve the existing product data from the database
          //  var result = await twoBigDB.GetAsync("PRODUCTS/" + deleteStr);
          //  Products obj = result.ResultAs<Products>();

          //  var data = new Products();
          // // data.adminId = adminId;
          //  data.productId = int.Parse(LabelID.Text);
          //  data.productName = prodName.Text;
          //  data.productSize = prodSize.Text;
          //  data.productPrice = float.Parse(productPrice.Text);
          ////  data.productDiscount = float.Parse(productDiscounts.Text);
          //  data.productStock = int.Parse(productStock.Text);
          //  //data.waterRefillSupply = waterSupAvailable.Text;
          //  data.dateAdded = DateTime.UtcNow;
          //  // Keep the existing productImage
          //  data.productImage = obj.productImage;

          //  FirebaseResponse response;
          //  response = await twoBigDB.UpdateAsync("Product/" + deleteStr, data);

          //  // Retrieve the updated product data from the database
          //  result = await twoBigDB.GetAsync("Product/" + deleteStr);
          //  obj = result.ResultAs<Products>();

          //  LabelID.Text = obj.productId.ToString();
          //  prodName.Text = obj.productName.ToString();
          //  prodSize.Text = obj.productSize.ToString();
          //  prodPrice.Text = obj.productPrice.ToString();
          //  prodDiscount.Text = obj.productDiscount.ToString();
          //  prodAvailable.Text = obj.productStock.ToString();
          ////  waterSupAvailable.Text = obj.waterRefillSupply.ToString();
          //  LblDate.Text = obj.dateAdded.ToString();

          //  Response.Write("<script>alert ('Product ID : " + deleteStr + " successfully updated!');</script>");
        }



        //protected void btnEdit_Click(object sender, EventArgs e)
        //{
        //    string idno = (string)Session["idno"];
        //    String deleteStr;
        //    deleteStr = ListBox1.SelectedValue;

        //    var data = new Products();

        //    data.productId = int.Parse(LabelID.Text);
        //    data.productName = prodName.Text;
        //    data.productSize = prodSize.Text;
        //    data.productPrice = prodPrice.Text;
        //    data.productDiscount = prodDiscount.Text;
        //    data.productAvailable = prodAvailable.Text;
        //    data.waterRefillSupply = waterSupAvailable.Text;
        //    data.dateAdded = DateTime.UtcNow;

        //    FirebaseResponse response;
        //    response = twoBigDB.Update("ADMIN/" + idno + "/Product/" + deleteStr, data);//Update Product Data 

        //    var result = twoBigDB.Get("ADMIN/" + idno + "/Product/" + deleteStr);//Retrieve Updated Data From WATERPRODUCT TBL
        //    Products obj = response.ResultAs<Products>();//Database Result

        //    LabelID.Text = obj.productId.ToString();
        //    prodName.Text = obj.productName.ToString();
        //    prodSize.Text = obj.productSize.ToString();
        //    prodPrice.Text = obj.productPrice.ToString();
        //    prodDiscount.Text = obj.productDiscount.ToString();
        //    prodAvailable.Text = obj.productAvailable.ToString();
        //    waterSupAvailable.Text = obj.waterRefillSupply.ToString();
        //    LblDate.Text = obj.dateAdded.ToString();

        //    Response.Write("<script>alert ('Product ID : " + deleteStr + " successfully updated!');</script>");  
        //}

        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            //string idno = (string)Session["idno"];
            //String deleteStr;
            //deleteStr = ListBox1.SelectedValue;
            //FirebaseResponse response = twoBigDB.Delete("Product/ " + deleteStr);

            ////lbl_result.Text = "Records Successfully deleted!";
            ////Response.Write("<div>Successfully deleted product ID : "+ deleteStr +" </div>");
            //Response.Write("<script>alert ('Successfully deleted product ID : " + deleteStr + "'); window.location.href = '/Admin/WaterProduct.aspx'; </script>");

            ////TO DELETE THE ID IN THE LISTBOX AFTER DELETED
            //int selected = ListBox1.SelectedIndex;
            //if (selected != 1)
            //{
            //    ListBox1.Items.RemoveAt(selected);
            //}


        }
    }
}