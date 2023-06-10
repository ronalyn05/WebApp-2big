using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
//using System.Reflection.Metadata;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WRS2big_Web.Model;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;

namespace WRS2big_Web.Admin
{
    public partial class WaterOrders : System.Web.UI.Page
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
                //onlineordersDisplay();
                //lblOrder.Text = "LIST OF ONLINE ORDERS";
                walkinordersDisplay();
                PopulateAssignDriver();
                //displayAllCOD_order();
                //displayAllGcash_order();
                //displayAllPoints_order();
                //lblWalkIn.Text = "LIST OF WALKIN ORDERS";
            }

        }
        //Fetch the employee 'driver' and get the id 
        private void PopulateAssignDriver()
        {
            // Fetch all the employees from the database
            FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
            Dictionary<string, Employee> employees = response.ResultAs<Dictionary<string, Employee>>();

            if (response != null && employees != null)
            {
                // Create a list to store the driver employees
                List<Employee> driverEmployees = new List<Employee>();

                // Iterate over the employees and add the employees with the role "Driver" to the list
                foreach (var employee in employees.Values)
                {
                    if (employee.emp_role != null && employee.emp_role.ToLower() == "driver")
                    {
                        driverEmployees.Add(employee);
                    }
                }

                // Bind the driver employees' names to the dropdown
                drdAssignDriver.DataSource = driverEmployees;
                drdAssignDriver.DataTextField = "FullName"; //  property that returns the driver's full name
                drdAssignDriver.DataValueField = "emp_id"; //  property holding the employee's ID
                drdAssignDriver.DataBind();

                // Set a default item as the first item in the dropdown
                drdAssignDriver.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select driver to assign", ""));
            }
        }
        //DISPLAY WALKIN ORDERS
        private void walkinordersDisplay()
            {
                string idno = (string)Session["idno"];
                decimal discount;

                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("WALKINORDERS");
                Dictionary<string, WalkInOrders> otherproductsList = response.ResultAs<Dictionary<string, WalkInOrders>>();
            

                // Create the DataTable to hold the orders
                DataTable walkInordersTable = new DataTable();
                walkInordersTable.Columns.Add("ORDER ID");
                walkInordersTable.Columns.Add("ORDER TYPE");
                walkInordersTable.Columns.Add("PRODUCT NAME");
                walkInordersTable.Columns.Add("PRODUCT UNIT");
                walkInordersTable.Columns.Add("PRICE");
                walkInordersTable.Columns.Add("QUANTITY");
                walkInordersTable.Columns.Add("DISCOUNT");
                walkInordersTable.Columns.Add("TOTAL AMOUNT");
                walkInordersTable.Columns.Add("DATE");
                walkInordersTable.Columns.Add("ADDED BY");

                // Get the selected date range from the dropdown list
                //string dateRange = ddlDateRange.SelectedValue;

                if (response != null && response.ResultAs<WalkInOrders>() != null)
                {
                    var filteredList = otherproductsList.Values.Where(d => d.adminId.ToString() == idno);

                    // Loop through the filtered orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        if (!decimal.TryParse(entry.productDiscount.ToString(), out discount))
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

                        //walkInordersTable.Rows.Add(entry.orderNo, entry.orderType, entry.productName, entry.productSize + " " + entry.productUnit,
                        //    entry.productPrice, entry.productQty, discount, entry.totalAmount, dateAdded, entry.addedBy);


                        walkInordersTable.Rows.Add(entry.orderNo, entry.orderType, entry.productName, entry.productUnitSize,
                                             entry.productPrice, entry.productQty, discount,
                                             entry.totalAmount, dateAdded, entry.addedBy);
                    }

                    if (walkInordersTable.Rows.Count == 0)
                    {
                        lblWalkinError.Text = "No record found";
                        lblWalkinError.Visible = true;
                    }
                    else
                    {
                        // Bind the DataTable to the GridView
                        gridWalkIn.DataSource = walkInordersTable;
                        gridWalkIn.DataBind();
                    }
               
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblWalkinError.Text = "No record found";
                }
            }
        //Print wALKIN receipts
        protected void btnPrintReceipts_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            decimal discount;

            // Retrieve the button that was clicked
            Button btnPrint = (Button)sender;

            // Find the GridView row containing the button
            GridViewRow row = (GridViewRow)btnPrint.NamingContainer;

            // Get the order ID from the specific column
            int orderIDColumnIndex = 1; // the actual column index of the order ID
            int orderID = int.Parse(row.Cells[orderIDColumnIndex].Text);

            // Retrieve the order from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("WALKINORDERS/" + orderID);
            WalkInOrders walkInOrder = response.ResultAs<WalkInOrders>();

            if (walkInOrder != null)
            {
                // Check if the order belongs to the current admin
                if (walkInOrder.adminId.ToString() == idno)
                {
                    // Create the DataTable to hold the order
                    DataTable walkInordersTable = new DataTable();
                    walkInordersTable.Columns.Add("PRODUCT NAME");
                    walkInordersTable.Columns.Add("PRODUCT UNIT");
                    walkInordersTable.Columns.Add("PRICE");
                    walkInordersTable.Columns.Add("QUANTITY");
                    walkInordersTable.Columns.Add("DISCOUNT");
                    walkInordersTable.Columns.Add("TOTAL AMOUNT");

                    // Retrieve the RefillingStation for the current admin
                    var empResponse = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
                    RefillingStation station = empResponse.ResultAs<RefillingStation>();

                    // Set the session variables
                    Session["stationName"] = station.stationName;
                    Session["address"] = station.stationAddress;

                    // Set the label values
                    lblStationName.Text = (string)Session["stationName"];
                    lblStationAddress.Text = (string)Session["address"];
                    lblownerName.Text = walkInOrder.addedBy;
                    lblTransNo.Text = orderID.ToString();
                    lblDate.Text = walkInOrder.dateAdded == DateTime.MinValue ? "" : walkInOrder.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    // Add the order details to the DataTable
                    if (!decimal.TryParse(walkInOrder.productDiscount, out discount))
                    {
                        // If the discount value is not a valid decimal, assume it is zero
                        discount = 0;
                    }
                    else
                    {
                        // Convert discount from percentage to decimal
                        discount /= 100;
                    }

                    walkInordersTable.Rows.Add(walkInOrder.productName, walkInOrder.productUnitSize,
                                               walkInOrder.productPrice, walkInOrder.productQty, discount,
                                               walkInOrder.totalAmount);

                    // Bind the DataTable to the GridView
                    gridSalesInvoice.DataSource = walkInordersTable;
                    gridSalesInvoice.DataBind();
                }
                else
                {
                    lblWalkinError.Text = "No record found";
                }
            }
            else
            {
                lblWalkinError.Text = "No record found";
            }


            // Store the order ID in a hidden field for later use
            hfPrintReceipts.Value = orderID.ToString();

            // Show the modal popup
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "printReceipts", "$('#printReceipts').modal('show');", true);
        }
        //generate pdf to print ORDER RECEIPTS for walkin order
        private string GenerateInvoiceHtml(int orderID)
        {
            // Retrieve the necessary data from the database based on the order ID
            string ownerName = lblownerName.Text;  // Owner / Cashier Name
            string date = lblDate.Text;  // Date
            string transactionNo = lblTransNo.Text;  // Transaction No.
            string error = lblError.Text;  // Error (if applicable)

            // Retrieve the station logo, address, and name from the respective controls
          // string stationLogo = "C:/Users/User1/Desktop/Capstone-Backup/2bigSystem-Version3/WebApp-2big/images/FinalLogo.png";  // Refilling station logo
            string stationName = lblStationName.Text;  // Station name
            string stationAddress = lblStationAddress.Text;  // Station address

            // Create the HTML content for the invoice
            string invoiceHtml = $@"<html>
                           <head>
                            <style>
                                .form-group {{
                                    margin-top: 20px;
                                    margin-bottom: 20px;
                                    text-align: center;
                                    font-weight: bold;
                                }}
                                .invoice-header {{
                                    margin-top: 20px;
                                    margin-bottom: 20px;
                                    text-align: center;
                                }}

                                .invoice-details {{
                                    text-align: left;
                                    margin-bottom: 20px;
                                }}

                                .invoice-details strong {{
                                    display: block;
                                    font-weight: bold;
                                }}

                                .invoice-error {{
                                    color: red;
                                }}

                                .invoice-table {{
                                    margin-bottom: 20px;
                                    width: 100%;
                                }}
                            </style>
                        </head>
                            <body>
                                    <div class='form-group text-center font-weight-bold'>
                                        <br />
                                        <span>{stationName}</span>
                                        <br />
                                        <span>{stationAddress}</span>
                                        <br />
                                    </div>
                                <div class='invoice-header'>
                                    <h4 class='text-center font-weight-bold'>SALES INVOICE</h4>
                                </div>
                                <div class='invoice-details'>
                                    <strong class='text-left font-weight-bold'>Owner / Cashier Name:</strong>
                                    <span>{ownerName}</span>
                                    <br />
                                    <strong class='text-left font-weight-bold'>Date:</strong>
                                    <span>{date}</span>
                                    <br />
                                    <strong class='text-left font-weight-bold'>Transaction No. :</strong>
                                    <span>{transactionNo}</span>
                                    <br />
                                    <span class='invoice-error'>{error}</span>
                                </div>
                                <hr />
                                <br />
                                <div>
                                    <table class='invoice-table'>
                                        <thead>
                                            <tr>
                                                <th>PRODUCT NAME</th>
                                                <th>PRODUCT UNIT</th>
                                                <th>PRICE</th>
                                                <th>QUANTITY</th>
                                                <th>DISCOUNT</th>
                                                <th>TOTAL AMOUNT</th>
                                            </tr>
                                        </thead>
                                        <tbody>";

            // Add the order details to the HTML content
            GridViewRowCollection rows = gridSalesInvoice.Rows;
            foreach (GridViewRow row in rows)
            {
                string productName = row.Cells[0].Text;
                string productUnit = row.Cells[1].Text;
                string price = row.Cells[2].Text;
                string quantity = row.Cells[3].Text;
                string discount = row.Cells[4].Text;
                string totalAmount = row.Cells[5].Text;

                invoiceHtml += $@"<tr>
                            <td>{productName}</td>
                            <td>{productUnit}</td>
                            <td>{price}</td>
                            <td>{quantity}</td>
                            <td>{discount}</td>
                            <td>{totalAmount}</td>
                        </tr>";
            }

            invoiceHtml += @"</tbody>
                    </table>
                </div>
                <div>
                    <br />
                     <div class='form-group text-center font-weight-bold'>
                <strong>Thank you for purchasing! Drink well and order again!</strong>
            </div>
                </div>
            </body>
        </html>";

            return invoiceHtml;
        }

        //method to generate pdf printing receipts for walkin order
        [Obsolete]
        protected void btnPrintingReceipts_Click(object sender, EventArgs e)
        {
            // Get the order ID from the hidden field
            int orderID = int.Parse(hfPrintReceipts.Value);

            // Generate the invoice HTML
            string invoiceHtml = GenerateInvoiceHtml(orderID);

            // Create the PDF document
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Parse the HTML and add it to the document
            //StringReader stringReader = new StringReader(invoiceHtml);
            //HTMLWorker htmlWorker = new HTMLWorker(document);
            //htmlWorker.Parse(stringReader);
            StringReader stringReader = new StringReader(invoiceHtml);
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, stringReader);

            // Close the document
            document.Close();

            // Set the response headers for downloading the PDF
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=invoice.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(memoryStream.ToArray());
            Response.End();
        }

        //Print Online Receipts for COD order
        protected void btnPrintReceiptsOnline_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];

            // Retrieve the button that was clicked
            Button btnPrintOnlineOrder = (Button)sender;

            // Find the GridView row containing the button
            GridViewRow row = (GridViewRow)btnPrintOnlineOrder.NamingContainer;

            // Get the order ID from the specific column
            int orderIDColumnIndex = 1; // the actual column index of the order ID
            int orderID = int.Parse(row.Cells[orderIDColumnIndex].Text);

            // Retrieve the order from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order onlineOrder = response.ResultAs<Order>();


            // Retrieve all customers from the CUSTOMER table and compare the current customer name
            FirebaseResponse customerResponse = twoBigDB.Get("CUSTOMER");
            Dictionary<string, Customer> customerlist = customerResponse.ResultAs<Dictionary<string, Customer>>();


            if (onlineOrder != null)
            {
                // Check if the order belongs to the current admin
                if (onlineOrder.admin_ID.ToString() == idno)
                {
                    if (onlineOrder.order_OrderStatus != "Declined")
                    {

                        // Create the DataTable to hold the order
                        DataTable onlineordersTable = new DataTable();
                        onlineordersTable.Columns.Add("PRODUCT NAME");
                        onlineordersTable.Columns.Add("PRODUCT UNIT OF VOLUME");
                        onlineordersTable.Columns.Add("QUANTITY PER ITEM");
                        onlineordersTable.Columns.Add("PRICE");
                        //onlineordersTable.Columns.Add("OVERALL QUANTITY");
                        onlineordersTable.Columns.Add("DISCOUNT");
                        //onlineordersTable.Columns.Add("VEHICLE FEE");
                        //onlineordersTable.Columns.Add("DELIVERY FEE");
                        //onlineordersTable.Columns.Add("TOTAL AMOUNT");


                    // Retrieve the RefillingStation for the current admin
                    var empResponse = twoBigDB.Get("ADMIN/" + idno + "/RefillingStation/");
                    RefillingStation station = empResponse.ResultAs<RefillingStation>();

                    // Set the session variables
                    Session["stationName"] = station.stationName;
                    Session["address"] = station.stationAddress;

                    // Retrieve the customer details based on the customer ID from the order
                    if (customerlist.TryGetValue(onlineOrder.cusId.ToString(), out Customer customer))
                    {
                        string customerName = customer.firstName + " " + customer.lastName;
                        //store customer name here
                        lblCustomerName.Text = customerName;
                    }

                    // Set the label values
                        lbl_StationName.Text = (string)Session["stationName"];
                        lbl_StationAddress.Text = (string)Session["address"];
                        lblCashier_ownerName.Text = onlineOrder.orderAcceptedBy;
                        lbl_transNo.Text = orderID.ToString();
                        lbl_date.Text = onlineOrder.orderDate == DateTime.MinValue ? "" : onlineOrder.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        lblOverallQuantity.Text = onlineOrder.order_overAllQuantities.ToString();
                        lblInitialtotalAmount.Text = "Php." + " " + onlineOrder.order_InitialAmount.ToString();
                        lblTotalAmount.Text = "Php." + " " + onlineOrder.order_TotalAmount.ToString();
                        lblVehicleFee.Text = onlineOrder.ordervehiclefee.ToString();
                        lbldeliveryFee.Text = onlineOrder.orderDeliveryfee.ToString();
                        //lblInitialtotalAmount.Text = onlineOrder.order_InitialAmount.ToString();
                        //lblTotalAmount.Text = onlineOrder.order_TotalAmount.ToString();


                        // Retrieve the total amount, vehicle fee and delivery fee from the order
                        //int vehicleFee = onlineOrder.ordervehiclefee;
                        //decimal deliveryFee = onlineOrder.orderDeliveryfee;

                        string productname = " ";
                        string productunit = " ";
                        string productprice = " ";
                        string discount = " ";
                        string qtyperItem = " ";

                        foreach (var product in onlineOrder.order_Products)
                            {
                                 productname = product.order_ProductName;
                                 productunit = product.pro_refillQty + " " + product.pro_refillUnitVolume;
                                 productprice = product.order_ProductPrice.ToString();
                                 discount = product.order_ProductDiscount;
                                 qtyperItem = product.qtyPerItem;

                            onlineordersTable.Rows.Add(
                              productname,
                              productunit,
                              qtyperItem,
                              productprice,
                              discount
                          );
                            // onlineordersTable.Rows.Add(
                            //    productname,
                            //    productunit,
                            //    qtyperItem,
                            //    productprice,
                            //    discount,
                            //    vehicleFee,
                            //    deliveryFee
                            //);
                        }
                        
                        // Bind the DataTable to the GridView
                        gridOnlineInvoiceCOD.DataSource = onlineordersTable;
                        gridOnlineInvoiceCOD.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('You are not able to print invoice since the order is declined.'); </script>");
                        return;
                    }
                }
                else
                {
                    lblErrorMessage.Text = "No record of order to print";
                }
            }
            else
            {
                lblErrorMessage.Text = "No record of order to print";
            }



            // Store the order ID in a hidden field for later use
            hfOnlinePrintingReceipts.Value = orderID.ToString();

            // Show the modal popup
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "onlineprintReceipts", "$('#onlineprintReceipts').modal('show');", true);
        }
        //generate pdf for printing the cod order invoice
        private string GenerateOnlineOrderInvoiceHtml(int orderID)
        {
            // Retrieve the necessary data from the database based on the order ID
            string ownerName = lblCashier_ownerName.Text;  // Owner / Cashier Name
            string customerName = lblCustomerName.Text;  // Customer Name
            string date = lbl_date.Text;  // Date
            string transactionNo = lbl_transNo.Text;  // Transaction No.
            string error = lblError_Message.Text;  // Error (if applicable)

            // Retrieve the station logo, address, and name from the respective controls
            // string stationLogo = "C:/Users/User1/Desktop/Capstone-Backup/2bigSystem-Version3/WebApp-2big/images/FinalLogo.png";  // Refilling station logo
            string stationName = lbl_StationName.Text;  // Station name
            string stationAddress = lbl_StationAddress.Text;  // Station address
            string overallQty = lblOverallQuantity.Text;
            string initialAmount = lblInitialtotalAmount.Text;
            string totalAmount = lblTotalAmount.Text;
            string vehiclefee = lblVehicleFee.Text;
            string deliveryfee = lbldeliveryFee.Text;


            // Create the HTML content for the invoice
            string invoiceHtml = $@"<html>
                           <head>
                            <style>
                                .invoice-container {{
                                    text-align: center;
                                }}

                                .form-group {{
                                    margin-top: 20px;
                                    margin-bottom: 20px;
                                    text-align: center;
                                    font-weight: bold;
                                }}
                                .invoice-header {{
                                    margin-top: 20px;
                                    margin-bottom: 20px;
                                    text-align: center;
                                }}

                                .invoice-details {{
                                    text-align: left;
                                    margin-bottom: 20px;
                                }}

                                .invoice-details strong {{
                                    display: block;
                                    font-weight: bold;
                                }}

                                .invoice-error {{
                                    color: red;
                                }}

                                .invoice-table {{
                                    margin-bottom: 20px;
                                    width: 100%;
                                }}
                            </style>
                        </head>
                            <body>
                                    <div class='form-group text-center font-weight-bold'>
                                        <br />
                                        <span>{stationName}</span>
                                        <br />
                                        <span>{stationAddress}</span>
                                        <br />
                                    </div>
                                <div class='invoice-header'>
                                    <h4 class='text-center font-weight-bold'>SALES INVOICE</h4>
                                </div>
                                <div class='invoice-details'>
                                    <strong class='text-left font-weight-bold'>Owner / Cashier Name:</strong>
                                    <span>{ownerName}</span>
                                    <br />
                                    <strong class='text-left font-weight-bold'>Customer Name:</strong>
                                    <span>{customerName}</span>
                                    <br />
                                    <strong class='text-left font-weight-bold'>Date:</strong>
                                    <span>{date}</span>
                                    <br />
                                    <strong class='text-left font-weight-bold'>Transaction No. :</strong>
                                    <span>{transactionNo}</span>
                                    <br />
                                    <span class='invoice-error'>{error}</span>
                                </div>
                                <br />
                                <hr />
                                <div>
                                    <table class='invoice-table'>
                                        <thead>
                                            <tr>
                                                <th>PRODUCT NAME</th>
                                                <th>PRODUCT UNIT</th>
                                                <th>QTY PER ITEM</th>
                                                <th>PRICE</th>
                                                <th>DISCOUNT</th>
                                            </tr>
                                        </thead>
                                        <tbody>";

            // Add the order details to the HTML content
            GridViewRowCollection rows = gridOnlineInvoiceCOD.Rows;
            foreach (GridViewRow row in rows)
            {
                string productname = row.Cells[0].Text;
                string productunit = row.Cells[1].Text;
                string quantity = row.Cells[2].Text;
                string price = row.Cells[3].Text;
                string discount = row.Cells[4].Text;
                //string vehicleFee = row.Cells[5].Text;
                //string deliveryFee = row.Cells[6].Text;

                invoiceHtml += $@"<tr>
                            <td>{productname}</td>
                            <td>{productunit}</td>
                            <td>{quantity}</td>
                            <td>{price}</td>
                            <td>{discount}</td>
                          
                        </tr>";
            }

            invoiceHtml += $@"</tbody>
                </table>
                <br />
                <hr />
                <strong>Overall Quantity: {overallQty}</strong>
                <br />
                <strong>Vehicle Fee: {vehiclefee}</strong>
                <br />
                <strong>Delivery Fee: {deliveryfee}</strong>
                <br />
                <strong>Initial Total Amount: {initialAmount}</strong>
                <br />
                <strong>Total Amount: {totalAmount}</strong>
                <br />
                <br />
                <br />
                <br />
                <div class='form-group text-center font-weight-bold'>
                <strong>Thank you for purchasing! Drink well and order again!</strong>
            </div>
            </div>
        </body>
    </html>";

            return invoiceHtml;
        }

        //method for generating pdf for cod order invoice
        [Obsolete]
        protected void btnOnlineOrderPrintingReceipts_Click(object sender, EventArgs e)
        {
            // Get the order ID from the hidden field
            int orderID = int.Parse(hfOnlinePrintingReceipts.Value);

            // Generate the invoice HTML
            string invoiceHtml = GenerateOnlineOrderInvoiceHtml(orderID);

            // Create the PDF document
            Document document = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            //// Parse the HTML and add it to the document
            //StringReader stringReader = new StringReader(invoiceHtml);
            //HTMLWorker htmlWorker = new HTMLWorker(document);
            //htmlWorker.Parse(stringReader);

            // Parse the HTML and add it to the document
            StringReader stringReader = new StringReader(invoiceHtml);
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, stringReader);


            // Close the document
            document.Close();

            // Set the response headers for downloading the PDF
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=invoice.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(memoryStream.ToArray());
            Response.End();
        }
        //SEARCH REPORTS
        protected void btnSearchOrder_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#view').modal();", true);

            string idno = (string)Session["idno"];
            decimal discount;

            try
            {
                string ordernum = txtSearch.Text;

                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("WALKINORDERS");
                Dictionary<string, WalkInOrders> walkinOrderList = response.ResultAs<Dictionary<string, WalkInOrders>>();

                // Create the DataTable to hold the orders
                DataTable walkInordersTable = new DataTable();
                walkInordersTable.Columns.Add("ORDER ID");
                walkInordersTable.Columns.Add("ORDER TYPE");
                walkInordersTable.Columns.Add("PRODUCT NAME");
                walkInordersTable.Columns.Add("PRODUCT UNIT & SIZE");
                // walkInordersTable.Columns.Add("PRODUCT SIZE");
                walkInordersTable.Columns.Add("PRICE");
                walkInordersTable.Columns.Add("QUANTITY");
                walkInordersTable.Columns.Add("DISCOUNT");
                walkInordersTable.Columns.Add("TOTAL AMOUNT");
                walkInordersTable.Columns.Add("DATE ADDED");
                walkInordersTable.Columns.Add("ADDED BY");

                // Retrieve all orders from the ORDERS table
                FirebaseResponse responselist = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> onlineOrderlist = responselist.ResultAs<Dictionary<string, Order>>();

                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
                ordersTable.Columns.Add("ORDER ACCEPTED BY");
                ordersTable.Columns.Add("DATE OF ORDER DECLINED");
                ordersTable.Columns.Add("ORDER DECLINED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");
                ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
                ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
                ordersTable.Columns.Add("PAYMENT RECEIVED BY");

             

                //condition to fetch the product refill data
                if (response != null && response.ResultAs<WalkInOrders>() != null)
                {
                    //var filteredList = productsList.Values.Where(d => d.adminId.ToString() == idno && (d.pro_refillId.ToString() == productnum));
                    var filteredList = walkinOrderList.Values.Where(d => d.adminId.ToString() == idno);


                    // Loop through the entries and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        if (ordernum == entry.orderNo.ToString())
                        {
                           
                            if (!decimal.TryParse(entry.productDiscount.ToString(), out discount))
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

                            walkInordersTable.Rows.Add(entry.orderNo, entry.orderType, entry.productName, entry.productSize + " " + entry.productUnit,
                                entry.productPrice, entry.productQty, discount, entry.totalAmount, dateAdded, entry.addedBy);
                        }
                    }

                }
                else
                {
                    //Response.Write("<script>alert('Error retrieving product data.');</script>");
                    lblMessage.Text = "No data found for order with id number" + ordernum;
                }

                //condition to fetch the other product data
                if (responselist != null && responselist.ResultAs<Order>() != null)
                {
                    var filteredList = onlineOrderlist.Values.Where(d => d.admin_ID.ToString() == idno);

                    // Loop through the entries and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        if (ordernum == entry.orderID.ToString())
                        {
                            string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string datePayment = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateOrder = entry.orderDate == DateTime.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                            ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                                     dateOrder, dateAccepted, entry.orderAcceptedBy, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy,
                                     dateDelivered, datePayment, entry.paymentReceivedBy);
                        }
                    }
                }
                else
                {
                    //Response.Write("<script>alert('Error retrieving product data.');</script>");
                    lblError_Message.Text = "No data found for order with id number" + ordernum;
                }

                // Bind the DataTable to the GridView
                gridOnline_Order.DataSource = ordersTable;
                gridOnline_Order.DataBind();

                gridWalkin_Order.DataSource = walkInordersTable;
                gridWalkin_Order.DataBind();
                // lblProductId.Text = productnum;

                //  Response.Write("<script> location.reload(); window.location.href = '/Admin/WaterOrders.aspx'; </script>");
                txtSearch.Text = null;

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('There was an error retrieving orders'); location.reload(); window.location.href = '/Admin/WaterOrders.aspx'; </script>" + ex.Message);
            }
        }
        //DISPLAY ALL COD ORDER 
        private void displayAllCOD_order()
        {
           
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
                ordersTable.Columns.Add("ORDER ACCEPTED BY");
                ordersTable.Columns.Add("DATE OF ORDER DECLINED");
                ordersTable.Columns.Add("ORDER DECLINED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");
                ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
                ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
                ordersTable.Columns.Add("PAYMENT RECEIVED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus != "Pending")).OrderByDescending(d => d.orderDate);
                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        //if(entry.orderPaymentMethod == "CashOnDelivery")
                        //{

                        //}
                        string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string datePayment = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTime.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                                 dateOrder, dateAccepted, entry.orderAcceptedBy, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy,
                                 dateDelivered, datePayment, entry.paymentReceivedBy);
                    }
                    if (ordersTable.Rows.Count == 0)
                    {
                        gridOrder.DataSource = null;
                        gridOrder.DataBind();
                        gridStatusAccepted.DataSource = null;
                        gridStatusAccepted.DataBind();
                        lblMessage.Text = "No Accepted Orders Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridOrder.DataSource = ordersTable;
                        gridOrder.DataBind();
                        gridOrder.Visible = true;
                        gridStatusAccepted.Visible = false;
                        lblMessage.Visible = false;
                    }
                    //gridStatusAccepted.Visible = false;
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblMessage.Text = "No Accepted Order found";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //DISPLAY THE COD ACCEPTED ORDER
        private void displayAccepted_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                    // Retrieve all orders from the ORDERS table
                    FirebaseResponse response = twoBigDB.Get("ORDERS");
                    Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                    // Create the DataTable to hold the orders
                    DataTable ordersTable = new DataTable();
                    ordersTable.Columns.Add("ORDER ID");
                    ordersTable.Columns.Add("CUSTOMER ID");
                    ordersTable.Columns.Add("DRIVER ID");
                    ordersTable.Columns.Add("STATUS");
                    ordersTable.Columns.Add("DELIVERY TYPE");
                    ordersTable.Columns.Add("ORDER TYPE");
                    ordersTable.Columns.Add("STORE NAME");
                    ordersTable.Columns.Add("PAYMENT MODE");
                    ordersTable.Columns.Add("TOTAL AMOUNT");
                    ordersTable.Columns.Add("ORDER DATE");
                    ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
                    ordersTable.Columns.Add("ORDER ACCEPTED BY");
                    ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                    ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                     var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus == "Accepted")).OrderByDescending(d => d.orderDate);
                        
                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                        {
                            string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                            string dateOrder = entry.orderDate == DateTime.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                           dateOrder, dateAccepted, entry.orderAcceptedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }
                    if (ordersTable.Rows.Count == 0)
                    {
                        gridOrder.DataSource = null;
                        gridOrder.DataBind();
                        gridStatusAccepted.DataSource = null;
                        gridStatusAccepted.DataBind();
                        lblMessage.Text = "No Accepted Orders Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridStatusAccepted.DataSource = ordersTable;
                        gridStatusAccepted.DataBind();
                        gridStatusAccepted.Visible = true;
                        gridOrder.Visible = false;
                        lblMessage.Visible = false;
                    }
                     //gridOrder.Visible = false;
                }
                else
                {
                   // Handle null response or invalid selected value
                   lblMessage.Text = "No Accepted Order found";
                    lblMessage.Visible = true;
                }

                

            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //UPDATING THE STATUS OF COD ORDER IF DECLINE
        protected void btnDecline_Click(object sender, EventArgs e)
        {
            // Retrieve the button that was clicked
            Button btnDecline = (Button)sender;
            // Get the order ID from the command argument
            //int orderID = int.Parse(btnDecline.CommandArgument);
            // Find the GridView row containing the button
            GridViewRow row = (GridViewRow)btnDecline.NamingContainer;

            // Get the order ID from the specific column
            int orderIDColumnIndex = 1; //  the actual column index of the order ID
            int orderID = int.Parse(row.Cells[orderIDColumnIndex].Text);

            // Store the order ID in a hidden field for later use
            hfDeclineOrderID.Value = orderID.ToString();

            // Show the modal popup
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "declineModal", "$('#declineModal').modal('show');", true);
        }
        //DECLINE COD ORDER
        protected void btnSubmitDecline_Click(object sender, EventArgs e)
        {
            // Get the order ID from the hidden field
            int orderID = int.Parse(hfDeclineOrderID.Value);

            // Get the reason input value
            string reason = reasonInput.Value;
            

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            if (response != null && existingOrder != null)
            {
                // Update the order status and other details
                existingOrder.order_OrderStatus = "Declined";
                existingOrder.driverId = 0; // Clear the driver ID
                existingOrder.dateOrderDeclined = DateTime.Now;
                existingOrder.orderDeclinedBy = (string)Session["fullname"];

                // Update the existing order object in the database
                response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                // Store the entered reasons in the NOTIFICATION table
                Random rnd = new Random();
                int notificationID = rnd.Next(1, 20000);
                var notification = new Model.Notification
                {
                    admin_ID = int.Parse((string)Session["idno"]),
                    sender = "Admin",
                    orderID = orderID,
                    cusId = existingOrder.cusId,
                    receiver = "Customer",
                    title = "Order Declined",
                    driverId = existingOrder.driverId,
                    body = reason,
                    notificationDate = DateTime.Now,
                    status = "unread",
                    notificationID = notificationID
                };

                SetResponse notifResponse = twoBigDB.Set("NOTIFICATION/" + notificationID, notification);

                // Notify the driver if a driver is assigned to the order
                if (!string.IsNullOrEmpty(existingOrder.driverId.ToString()))
                {
                    // Retrieve the driver object from the database
                    FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES/" + existingOrder.driverId);
                    Employee driver = driverResponse.ResultAs<Employee>();

                    if (driverResponse != null && driver != null)
                    {
                        // Create a notification for the driver
                        var driverNotification = new Model.Notification
                        {
                            admin_ID = int.Parse((string)Session["idno"]),
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Driver",
                            title = "Order Declined",
                            driverId = existingOrder.driverId,
                            body = "Your assigned order has been declined. Reason: " + reason,
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = rnd.Next(1, 20000)
                        };

                        SetResponse driverNotifResponse = twoBigDB.Set("NOTIFICATION/" + driverNotification.notificationID, driverNotification);
                    }
                }

                // Perform  additional actions or display messages as needed

                Response.Write("<script>alert ('Order Declined!'); location.reload(); window.location.href = '/Admin/WaterOrders.aspx';</script>");

                if (Session["role"] != null || Session["idno"] != null)
                {
                    string role = (string)Session["role"];
                    string idno = (string)Session["idno"];

                    //Random rnd = new Random();
                    int logsID = rnd.Next(1, 10000);

                    // Get the current date and time
                    DateTime addedTime = DateTime.Now;

                    // Log user activity
                    var log = new UsersLogs
                    {
                        userIdnum = int.Parse(idno),
                        logsId = logsID,
                        role = role,
                        userFullname = (string)Session["fullname"],
                        userActivity = "DECLINED ORDER",
                        activityTime = addedTime
                    };

                    twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                }
            }
            displayDeclined_order();
            gridOrder.Visible = true;
            gridStatusAccepted.Visible = false;

        }
        //   DISPLAY THE COD DECLINED ORDER
        private void displayDeclined_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER DECLINED");
                ordersTable.Columns.Add("ORDER DECLINED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus == "Declined")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTime.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                           dateOrder, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        gridOrder.DataSource = null;
                        gridOrder.DataBind();
                        gridStatusAccepted.DataSource = null;
                        gridStatusAccepted.DataBind();

                        lblMessage.Text = "No Delivered Orders Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {

                        gridOrder.DataSource = ordersTable;
                        gridOrder.DataBind();
                        gridOrder.Visible = true;
                        gridStatusAccepted.Visible = false;
                        lblMessage.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblMessage.Text = "No Declined Order found";
                    lblMessage.Visible = true;
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retreiving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //   DISPLAY THE COD DELIVERED ORDER
        private void displayDelivered_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus == "Delivered")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTime.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, 
                            entry.orderPaymentMethod, entry.order_TotalAmount, dateOrder, dateDelivered, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        gridOrder.DataSource = null;
                        gridOrder.DataBind();
                        gridStatusAccepted.DataSource = null;
                        gridStatusAccepted.DataBind();
                        lblMessage.Text = "No Delivered Orders Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridOrder.DataSource = ordersTable;
                        gridOrder.DataBind();
                        gridOrder.Visible = true;
                        gridStatusAccepted.Visible = false;
                        lblMessage.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblMessage.Text = "No Delivered Order found";
                    lblMessage.Visible = true;
                }
                
            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //   DISPLAY THE COD PAYMENT RECEIVED ORDER
        private void displayPaymentReceived_order()
        {
            string idno = (string)Session["idno"];

            try
            {

                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
                ordersTable.Columns.Add("PAYMENT RECEIVED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "CashOnDelivery") && (d.order_OrderStatus == "Payment Received")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string datePaymentReceived = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTime.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName,
                            entry.orderPaymentMethod, entry.order_TotalAmount, dateOrder, datePaymentReceived, entry.paymentReceivedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        gridOrder.DataSource = null;
                        gridOrder.DataBind();
                        gridStatusAccepted.DataSource = null;
                        gridStatusAccepted.DataBind();
                        lblMessage.Text = "No Payment Received Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridOrder.DataSource = ordersTable;
                        gridOrder.DataBind();
                        gridOrder.Visible = true;
                        gridStatusAccepted.Visible = false;
                        lblMessage.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblMessage.Text = "No  Payment Received found";
                    lblMessage.Visible = true;
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblMessage.Visible = true;
            }
        }
        //DISPLAY ALL GCASH ORDER 
        private void displayAllGcash_order()
        {

            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
                ordersTable.Columns.Add("ORDER ACCEPTED BY");
                ordersTable.Columns.Add("DATE OF ORDER DECLINED");
                ordersTable.Columns.Add("ORDER DECLINED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");
                ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
                ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
                ordersTable.Columns.Add("PAYMENT RECEIVED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Gcash") && (d.order_OrderStatus != "Pending")).OrderByDescending(d => d.orderDate);
                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        //if(entry.orderPaymentMethod == "CashOnDelivery")
                        //{

                        //}
                        string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string datePayment = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                                 dateOrder, dateAccepted, entry.orderAcceptedBy, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy,
                                 dateDelivered, datePayment, entry.paymentReceivedBy);
                    }
                    if (ordersTable.Rows.Count == 0)
                    {
                        gridGcash_order.DataSource = null;
                        gridGcash_order.DataBind();
                        gridDelivered.DataSource = null;
                        gridDelivered.DataBind();
                        gridGcashAccepted_order.DataSource = null;
                        gridGcashAccepted_order.DataBind();
                        lblGcashError.Text = "No Orders Found";
                        lblGcashError.Visible = true;
                    }
                    else
                    {
                        gridGcash_order.DataSource = ordersTable;
                        gridGcash_order.DataBind();
                        gridGcash_order.Visible = true;
                        gridDelivered.Visible = false;
                        gridGcashAccepted_order.Visible = false;
                        lblGcashError.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblGcashError.Text = "No Orders found";
                    lblGcashError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblGcashError.Text = "There was an error retrieving orders" + ex.Message;
                lblGcashError.Visible = true;
            }
        }
        //DISPLAY THE ACCEPTED ORDER
        private void displayAcceptedGcash_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
                ordersTable.Columns.Add("ORDER ACCEPTED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Gcash") && (d.order_OrderStatus == "Accepted")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                           dateOrder, dateAccepted, entry.orderAcceptedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }
                    if (ordersTable.Rows.Count == 0)
                    {
                        gridGcash_order.DataSource = null;
                        gridGcash_order.DataBind();
                        gridDelivered.DataSource = null;
                        gridDelivered.DataBind();
                        gridGcashAccepted_order.DataSource = null;
                        gridGcashAccepted_order.DataBind();
                        lblMessage.Text = "No Accepted Orders Found";
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        gridGcashAccepted_order.DataSource = ordersTable;
                        gridGcashAccepted_order.DataBind();
                        gridGcashAccepted_order.Visible = true;
                        gridDelivered.Visible = false;
                        gridGcash_order.Visible = false;
                        lblGcashError.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblGcashError.Text = "No Accepted Order found";
                    lblGcashError.Visible = true;
                }


            }
            catch (Exception ex)
            {
                lblGcashError.Text = "There was an error retrieving orders" + ex.Message;
                lblGcashError.Visible = true;
            }
        }
        //UPDATING THE STATUS OF GCASH ORDER IF DECLINE
        protected void btnDeclineGcash_Click(object sender, EventArgs e)
        {
            // Retrieve the button that was clicked
            Button btnDeclineGcash = (Button)sender;
            // Get the order ID from the command argument
            //int orderID = int.Parse(btnDecline.CommandArgument);
            // Find the GridView row containing the button
            GridViewRow row = (GridViewRow)btnDeclineGcash.NamingContainer;

            // Get the order ID from the specific column
            int orderIDColumnIndex = 1; //  the actual column index of the order ID
            int orderID = int.Parse(row.Cells[orderIDColumnIndex].Text);

            // Store the order ID in a hidden field for later use
            hfDeclineGcashOrderID.Value = orderID.ToString();

            // Show the modal popup
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "declineGcashModal", "$('#declineGcashModal').modal('show');", true);
        }
        //DECLINE ORDER
        protected void btnSubmitDeclineGcash_Click(object sender, EventArgs e)
        {
            // Get the order ID from the hidden field
            int orderID = int.Parse(hfDeclineGcashOrderID.Value);

            // Get the reason input value
            string reason = reasonGcashInput.Value;

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            if (response != null && existingOrder != null)
            {
                // Update the order status and other details
                existingOrder.order_OrderStatus = "Declined";
                existingOrder.driverId = 0; // Clear the driver ID
                existingOrder.dateOrderDeclined = DateTime.Now;
                existingOrder.orderDeclinedBy = (string)Session["fullname"];

                // Update the existing order object in the database
                response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                // Store the entered reasons in the NOTIFICATION table
                Random rnd = new Random();
                int notificationID = rnd.Next(1, 20000);
                var notification = new Model.Notification
                {
                    admin_ID = int.Parse((string)Session["idno"]),
                    sender = "Admin",
                    orderID = orderID,
                    cusId = existingOrder.cusId,
                    receiver = "Customer",
                    title = "Order Declined",
                    driverId = existingOrder.driverId,
                    body = reason,
                    notificationDate = DateTime.Now,
                    status = "unread",
                    notificationID = notificationID
                };

                SetResponse notifResponse = twoBigDB.Set("NOTIFICATION/" + notificationID, notification);

                // Notify the driver if a driver is assigned to the order
                if (!string.IsNullOrEmpty(existingOrder.driverId.ToString()))
                {
                    // Retrieve the driver object from the database
                    FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES/" + existingOrder.driverId);
                    Employee driver = driverResponse.ResultAs<Employee>();

                    if (driverResponse != null && driver != null)
                    {
                        // Create a notification for the driver
                        var driverNotification = new Model.Notification
                        {
                            admin_ID = int.Parse((string)Session["idno"]),
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Driver",
                            title = "Order Declined",
                            driverId = existingOrder.driverId,
                            body = "Your assigned order has been declined. Reason: " + reason,
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = rnd.Next(1, 20000)
                        };

                        SetResponse driverNotifResponse = twoBigDB.Set("NOTIFICATION/" + driverNotification.notificationID, driverNotification);
                    }
                }

                // Perform  additional actions or display messages as needed

                Response.Write("<script>alert ('Order Declined!'); window.location.href = '/Admin/WaterOrders.aspx';</script>");

                if (Session["role"] != null || Session["idno"] != null)
                {
                    string role = (string)Session["role"];
                    string idno = (string)Session["idno"];

                    //Random rnd = new Random();
                    int logsID = rnd.Next(1, 10000);

                    // Get the current date and time
                    DateTime addedTime = DateTime.Now;

                    // Log user activity
                    var log = new UsersLogs
                    {
                        userIdnum = int.Parse(idno),
                        logsId = logsID,
                        role = role,
                        userFullname = (string)Session["fullname"],
                        userActivity = "DECLINED ORDER",
                        activityTime = addedTime
                    };

                    twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                }

                displayDeclinedGcash_order();
                gridGcash_order.Visible = true;
                gridDelivered.Visible = false;
                gridGcashAccepted_order.Visible = false;

            }

        }
        //   DISPLAY THE GCASH DECLINED ORDER
        private void displayDeclinedGcash_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER DECLINED");
                ordersTable.Columns.Add("ORDER DECLINED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Gcash") && (d.order_OrderStatus == "Declined")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                           dateOrder, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        gridGcash_order.DataSource = null;
                        gridGcash_order.DataBind();
                        gridDelivered.DataSource = null;
                        gridDelivered.DataBind();
                        gridGcashAccepted_order.DataSource = null;
                        gridGcashAccepted_order.DataBind();
                        lblGcashError.Text = "No Declined Orders Found";
                        lblGcashError.Visible = true;
                    }
                    else
                    {
                        gridGcash_order.DataSource = ordersTable;
                        gridGcash_order.DataBind();
                        gridGcash_order.Visible = true;
                        gridDelivered.Visible = false;
                        gridGcashAccepted_order.Visible = false;
                        lblGcashError.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblGcashError.Text = "No Declined Order found";
                    lblGcashError.Visible = true;
                }

            }
            catch (Exception ex)
            {
                lblGcashError.Text = "There was an error retreiving orders" + ex.Message;
                lblGcashError.Visible = true;
            }
        }
        //   DISPLAY THE GCASH DELIVERED ORDER
        private void displayGcashDelivered_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Gcash") && (d.order_OrderStatus == "Delivered")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName,
                            entry.orderPaymentMethod, entry.order_TotalAmount, dateOrder, dateDelivered, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        gridGcash_order.DataSource = null;
                        gridGcash_order.DataBind();
                        gridDelivered.DataSource = null;
                        gridDelivered.DataBind();
                        gridGcashAccepted_order.DataSource = null;
                        gridGcashAccepted_order.DataBind();
                        lblGcashError.Text = "No Delivered Orders Found";
                        lblGcashError.Visible = true;
                    }
                    else
                    {
                        gridDelivered.DataSource = ordersTable;
                        gridDelivered.DataBind();
                        gridDelivered.Visible = true;
                        gridGcash_order.Visible = false;
                        gridGcashAccepted_order.Visible = false;
                        lblGcashError.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblGcashError.Text = "No Delivered Order found";
                    lblGcashError.Visible = true;
                }

            }
            catch (Exception ex)
            {
                lblGcashError.Text = "There was an error retrieving orders" + ex.Message;
                lblGcashError.Visible = true;
            }
        }
        //   DISPLAY THE GCASH PAYMENT RECEIVED ORDER
        private void displayGcashPaymentReceived_order()
        {
            string idno = (string)Session["idno"];

            try
            {

                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
                ordersTable.Columns.Add("PAYMENT RECEIVED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Gcash") && (d.order_OrderStatus == "Payment Received")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string datePaymentReceived = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName,
                            entry.orderPaymentMethod, entry.order_TotalAmount, dateOrder, datePaymentReceived, entry.paymentReceivedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        gridGcash_order.DataSource = null;
                        gridGcash_order.DataBind();
                        gridDelivered.DataSource = null;
                        gridDelivered.DataBind();
                        gridGcashAccepted_order.DataSource = null;
                        gridGcashAccepted_order.DataBind();
                        lblGcashError.Text = "No Payment Received Found";
                        lblGcashError.Visible = true;
                    }
                    else
                    {
                        gridGcash_order.DataSource = ordersTable;
                        gridGcash_order.DataBind();
                        gridGcash_order.Visible = true;
                        gridDelivered.Visible = false;
                        gridGcashAccepted_order.Visible = false;
                        lblGcashError.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblGcashError.Text = "No  Payment Received found";
                }

            }
            catch (Exception ex)
            {
                lblGcashError.Text = "There was an error retrieving orders" + ex.Message;
                lblGcashError.Visible = true;
            }
        }
        //DISPLAY ALL POINTS ORDER 
        private void displayAllPoints_order()
        {

            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
                ordersTable.Columns.Add("ORDER ACCEPTED BY");
                ordersTable.Columns.Add("DATE OF ORDER DECLINED");
                ordersTable.Columns.Add("ORDER DECLINED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");
                ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
                ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
                ordersTable.Columns.Add("PAYMENT RECEIVED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Points") && (d.order_OrderStatus != "Pending")).OrderByDescending(d => d.orderDate);
                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        //if(entry.orderPaymentMethod == "CashOnDelivery")
                        //{

                        //}
                        string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string datePayment = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                                 dateOrder, dateAccepted, entry.orderAcceptedBy, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy,
                                 dateDelivered, datePayment, entry.paymentReceivedBy);
                    }
                    if (ordersTable.Rows.Count == 0)
                    {
                        gridReceivedPaymentPoints_order.DataSource = null;
                        gridReceivedPaymentPoints_order.DataBind();
                        gridorder_DeclineAccepted.DataSource = null;
                        gridorder_DeclineAccepted.DataBind();
                        gridPointsOrder.DataSource = null;
                        gridPointsOrder.DataBind();
                        lblPointsErrorMessage.Text = "No Orders Found";
                        lblPointsErrorMessage.Visible = true;
                    }
                    else
                    {
                        gridPointsOrder.DataSource = ordersTable;
                        gridPointsOrder.DataBind();
                        gridPointsOrder.Visible = true;
                        gridReceivedPaymentPoints_order.Visible = false;
                        gridorder_DeclineAccepted.Visible = false;
                        lblPointsErrorMessage.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblPointsErrorMessage.Text = "No Orders found";
                }
            }
            catch (Exception ex)
            {
                lblPointsErrorMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblPointsErrorMessage.Visible = true;
            }
        }
        //DISPLAY THE POINTS ACCEPTED ORDER
        private void displayAcceptedPoints_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER ACCEPTED");
                ordersTable.Columns.Add("ORDER ACCEPTED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Points") && (d.order_OrderStatus == "Accepted")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateAccepted = entry.dateOrderAccepted == DateTime.MinValue ? "" : entry.dateOrderAccepted.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                           dateOrder, dateAccepted, entry.orderAcceptedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }
                    if (ordersTable.Rows.Count == 0)
                    {
                        gridReceivedPaymentPoints_order.DataSource = null;
                        gridReceivedPaymentPoints_order.DataBind();
                        gridorder_DeclineAccepted.DataSource = null;
                        gridorder_DeclineAccepted.DataBind();
                        gridPointsOrder.DataSource = null;
                        gridPointsOrder.DataBind();
                        lblPointsErrorMessage.Text = "No Accepted Orders Found";
                        lblPointsErrorMessage.Visible = true;
                    }
                    else
                    {
                        gridorder_DeclineAccepted.DataSource = ordersTable;
                        gridorder_DeclineAccepted.DataBind();
                        gridorder_DeclineAccepted.Visible = true;
                        gridReceivedPaymentPoints_order.Visible = false;
                        gridPointsOrder.Visible = false;
                        lblPointsErrorMessage.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblPointsErrorMessage.Text = "No Accepted Order found";
                }

                //displayDeclinedPoints_order();

            }
            catch (Exception ex)
            {
                lblPointsErrorMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblPointsErrorMessage.Visible = true;
            }
        }
        //UPDATING THE STATUS OF POINTS ORDER IF DECLINE
        protected void btnDeclinePoints_Click(object sender, EventArgs e)
        {
            // Retrieve the button that was clicked
            Button btnDeclinePoints = (Button)sender;
            // Get the order ID from the command argument
            //int orderID = int.Parse(btnDecline.CommandArgument);
            // Find the GridView row containing the button
            GridViewRow row = (GridViewRow)btnDeclinePoints.NamingContainer;

            // Get the order ID from the specific column
            int orderIDColumnIndex = 1; //  the actual column index of the order ID
            int orderID = int.Parse(row.Cells[orderIDColumnIndex].Text);

            // Store the order ID in a hidden field for later use
            hfDeclinePointsOrderID.Value = orderID.ToString();

            // Show the modal popup
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "declinePointsModal", "$('#declinePointsModal').modal('show');", true);
        }
        //DECLINE ORDER
        protected void btnSubmitDeclinePoints_Click(object sender, EventArgs e)
        {
            // Get the order ID from the hidden field
            int orderID = int.Parse(hfDeclinePointsOrderID.Value);

            // Get the reason input value
            string reason = reasonPointsInput.Value;

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            if (response != null && existingOrder != null)
            {
                // Update the order status and other details
                existingOrder.order_OrderStatus = "Declined";
                existingOrder.driverId = 0; // Clear the driver ID
                existingOrder.dateOrderDeclined = DateTime.Now;
                existingOrder.orderDeclinedBy = (string)Session["fullname"];

                // Update the existing order object in the database
                response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                // Store the entered reasons in the NOTIFICATION table
                Random rnd = new Random();
                int notificationID = rnd.Next(1, 20000);
                var notification = new Model.Notification
                {
                    admin_ID = int.Parse((string)Session["idno"]),
                    sender = "Admin",
                    orderID = orderID,
                    cusId = existingOrder.cusId,
                    receiver = "Customer",
                    title = "Order Declined",
                    driverId = existingOrder.driverId,
                    body = reason,
                    notificationDate = DateTime.Now,
                    status = "unread",
                    notificationID = notificationID
                };

                SetResponse notifResponse = twoBigDB.Set("NOTIFICATION/" + notificationID, notification);

                // Notify the driver if a driver is assigned to the order
                if (!string.IsNullOrEmpty(existingOrder.driverId.ToString()))
                {
                    // Retrieve the driver object from the database
                    FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES/" + existingOrder.driverId);
                    Employee driver = driverResponse.ResultAs<Employee>();

                    if (driverResponse != null && driver != null)
                    {
                        // Create a notification for the driver
                        var driverNotification = new Model.Notification
                        {
                            admin_ID = int.Parse((string)Session["idno"]),
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Driver",
                            title = "Order Declined",
                            driverId = existingOrder.driverId,
                            body = "Your assigned order has been declined. Reason: " + reason,
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = rnd.Next(1, 20000)
                        };

                        SetResponse driverNotifResponse = twoBigDB.Set("NOTIFICATION/" + driverNotification.notificationID, driverNotification);
                    }
                }

                // Perform  additional actions or display messages as needed

                Response.Write("<script>alert ('Order Declined!'); window.location.href = '/Admin/WaterOrders.aspx';</script>");

                if (Session["role"] != null || Session["idno"] != null)
                {
                    string role = (string)Session["role"];
                    string idno = (string)Session["idno"];

                    //Random rnd = new Random();
                    int logsID = rnd.Next(1, 10000);

                    // Get the current date and time
                    DateTime addedTime = DateTime.Now;

                    // Log user activity
                    var log = new UsersLogs
                    {
                        userIdnum = int.Parse(idno),
                        logsId = logsID,
                        role = role,
                        userFullname = (string)Session["fullname"],
                        userActivity = "DECLINED ORDER",
                        activityTime = addedTime
                    };

                    twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                }
            }

            displayDeclinedPoints_order();
            gridPointsOrder.Visible = true;
            gridReceivedPaymentPoints_order.Visible = false;
            gridorder_DeclineAccepted.Visible = false;
        }
        //   DISPLAY THE POINTS DECLINED ORDER
        private void displayDeclinedPoints_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER DECLINED");
                ordersTable.Columns.Add("ORDER DECLINED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Points") && (d.order_OrderStatus == "Declined")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateDeclined = entry.dateOrderDeclined == DateTime.MinValue ? "" : entry.dateOrderDeclined.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName, entry.orderPaymentMethod, entry.order_TotalAmount,
                           dateOrder, dateDeclined, entry.orderDeclinedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        gridReceivedPaymentPoints_order.DataSource = null;
                        gridReceivedPaymentPoints_order.DataBind();
                        gridorder_DeclineAccepted.DataSource = null;
                        gridorder_DeclineAccepted.DataBind();
                        gridPointsOrder.DataSource = null;
                        gridPointsOrder.DataBind();
                        lblPointsErrorMessage.Text = "No Declined Orders Found";
                        lblPointsErrorMessage.Visible = true;
                    }
                    else
                    {
                        gridPointsOrder.DataSource = ordersTable;
                        gridPointsOrder.DataBind();
                        gridPointsOrder.Visible = true;
                        gridReceivedPaymentPoints_order.Visible = false;
                        gridorder_DeclineAccepted.Visible = false;
                        lblPointsErrorMessage.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblPointsErrorMessage.Text = "No Declined Order found";
                }

            }
            catch (Exception ex)
            {
                lblPointsErrorMessage.Text = "There was an error retreiving orders" + ex.Message;
                lblPointsErrorMessage.Visible = true;
            }
        }
        //   DISPLAY THE POINTS DELIVERED ORDER
        private void displayPointsDelivered_order()
        {
            string idno = (string)Session["idno"];

            try
            {
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF ORDER DELIVERED");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Points") && (d.order_OrderStatus == "Delivered")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateDelivered = entry.dateOrderDelivered == DateTime.MinValue ? "" : entry.dateOrderDelivered.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName,
                            entry.orderPaymentMethod, entry.order_TotalAmount, dateOrder, dateDelivered, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        gridReceivedPaymentPoints_order.DataSource = null;
                        gridReceivedPaymentPoints_order.DataBind();
                        gridorder_DeclineAccepted.DataSource = null;
                        gridorder_DeclineAccepted.DataBind();
                        gridPointsOrder.DataSource = null;
                        gridPointsOrder.DataBind();
                        lblPointsErrorMessage.Text = "No Delivered Orders Found";
                        lblPointsErrorMessage.Visible = true;
                    }
                    else
                    {
                        gridReceivedPaymentPoints_order.DataSource = ordersTable;
                        gridReceivedPaymentPoints_order.DataBind();
                        gridReceivedPaymentPoints_order.Visible = true;
                        gridPointsOrder.Visible = false;
                        gridorder_DeclineAccepted.Visible = false;
                        lblPointsErrorMessage.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblPointsErrorMessage.Text = "No Delivered Order found";
                }

            }
            catch (Exception ex)
            {
                lblPointsErrorMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblPointsErrorMessage.Visible = true;
            }
        }
        //   DISPLAY THE POINTS PAYMENT RECEIVED ORDER
        private void displayPointsPaymentReceived_order()
        {
            string idno = (string)Session["idno"];

            try
            {

                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();


                // Create the DataTable to hold the orders
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("ORDER ID");
                ordersTable.Columns.Add("CUSTOMER ID");
                ordersTable.Columns.Add("DRIVER ID");
                ordersTable.Columns.Add("STATUS");
                ordersTable.Columns.Add("DELIVERY TYPE");
                ordersTable.Columns.Add("ORDER TYPE");
                ordersTable.Columns.Add("STORE NAME");
                ordersTable.Columns.Add("PAYMENT MODE");
                ordersTable.Columns.Add("TOTAL AMOUNT");
                ordersTable.Columns.Add("ORDER DATE");
                ordersTable.Columns.Add("DATE OF PAYMENT RECEIVED");
                ordersTable.Columns.Add("PAYMENT RECEIVED BY");
                ordersTable.Columns.Add("DATE OF DRIVER ASSIGNED");
                ordersTable.Columns.Add("DRIVER ASSIGNED BY");

                if (response != null && response.ResultAs<Order>() != null)
                {
                    var filteredList = orderlist.Values.Where(d => d.admin_ID.ToString() == idno && (d.orderPaymentMethod == "Points") && (d.order_OrderStatus == "Payment Received")).OrderByDescending(d => d.orderDate);

                    // Loop through the orders and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string datePaymentReceived = entry.datePaymentReceived == DateTime.MinValue ? "" : entry.datePaymentReceived.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateDriverAssigned = entry.dateDriverAssigned == DateTime.MinValue ? "" : entry.dateDriverAssigned.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateOrder = entry.orderDate == DateTimeOffset.MinValue ? "" : entry.orderDate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        ordersTable.Rows.Add(entry.orderID, entry.cusId, entry.driverId, entry.order_OrderStatus, entry.order_DeliveryTypeValue, entry.order_OrderTypeValue, entry.order_StoreName,
                            entry.orderPaymentMethod, entry.order_TotalAmount, dateOrder, datePaymentReceived, entry.paymentReceivedBy, dateDriverAssigned, entry.driverAssignedBy);
                    }

                    if (ordersTable.Rows.Count == 0)
                    {
                        gridReceivedPaymentPoints_order.DataSource = null;
                        gridReceivedPaymentPoints_order.DataBind();
                        gridorder_DeclineAccepted.DataSource = null;
                        gridorder_DeclineAccepted.DataBind();
                        gridPointsOrder.DataSource = null;
                        gridPointsOrder.DataBind();
                        lblPointsErrorMessage.Text = "No Payment Received Found";
                        lblPointsErrorMessage.Visible = true;
                    }
                    else
                    {
                        gridPointsOrder.DataSource = ordersTable;
                        gridPointsOrder.DataBind();
                        gridPointsOrder.Visible = true;
                        gridReceivedPaymentPoints_order.Visible = false;
                        gridorder_DeclineAccepted.Visible = false;
                        lblPointsErrorMessage.Visible = false;
                    }
                }
                else
                {
                    // Handle null response or invalid selected value
                    lblPointsErrorMessage.Text = "No  Payment Received found";
                }

            }
            catch (Exception ex)
            {
                lblPointsErrorMessage.Text = "There was an error retrieving orders" + ex.Message;
                lblPointsErrorMessage.Visible = true;
            }
        }
        //SEARCH COD ORDERS
        protected void btnView_Click(object sender, EventArgs e)
        {
            string selectedOption = ddlSearchOptions.SelectedValue;
            try
            {
                

                if (selectedOption == "0")
                {
                    displayAllCOD_order();
                    //gridStatusAccepted.Visible = false;
                    //lblMessage.Visible = false;

                }
                else if (selectedOption == "1")
                {

                    displayAccepted_order();
                   
                }
                else if (selectedOption == "2")
                {

                    displayDeclined_order();
                   
                }
                else if (selectedOption == "3")
                {

                    displayDelivered_order();
                   

                }
                else if (selectedOption == "4")
                {
                    displayPaymentReceived_order();
                    
                }
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/WaterOrders.aspx';" + ex.Message);
            }

        }
        //SEARCH POINTS ORDERS
        protected void btnViewPoints_Click(object sender, EventArgs e)
        {
            string selectedOption = drdPoints.SelectedValue;
            try
            {
             

                if (selectedOption == "0")
                {
                    displayAllPoints_order();
                    
                }
                else if (selectedOption == "1")
                {
                    displayAcceptedPoints_order();
                   


                }
                else if (selectedOption == "2")
                {
                    displayDeclinedPoints_order();
                   

                }
                else if (selectedOption == "3")
                {

                    displayPointsDelivered_order();
                   
                }
                else if (selectedOption == "4")
                {

                    displayPointsPaymentReceived_order();
                  
                }
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/WaterOrders.aspx';" + ex.Message);
            }

        }
        //SEARCH POINTS ORDERS
        protected void btnViewGcash_Click(object sender, EventArgs e)
        {

            string selectedOption = drdGcash.SelectedValue;
            try
            {

                if (selectedOption == "0")
                {
                    displayAllGcash_order();
                    
                }
                else if (selectedOption == "1")
                {
                    displayAcceptedGcash_order();
                   
                }
                else if (selectedOption == "2")
                {
                    displayDeclinedGcash_order();
                 
                }
                else if (selectedOption == "3")
                {
                    displayGcashDelivered_order();
                  
                }
                else if (selectedOption == "4")
                {
                    displayGcashPaymentReceived_order();
                   
                }
                

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/WaterOrders.aspx';" + ex.Message);
            }

        }
        //DISPLAY GCASH PROOF IMAGE
        protected void btnPaymentProof_Click(object sender, EventArgs e)
        {
            // Show the modal using JavaScript
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#viewPaymentProof').modal();", true);

            // Get the clicked button and its row
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Retrieve the order id from the data source
            // Get the order ID from the first cell in the row
            int orderID = int.Parse(row.Cells[1].Text);
            //int orderId = Convert.ToInt32(((DataRowView)row.DataItem)["orderID"]);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            // Check if the Gcash proof of payment is available
            string imagePath = existingOrder.order_GcashProofOfPayment;

            // Set the ImageUrl property of the imgGcashProof control in the modal to the retrieved image path 
            imgGcashProof.ImageUrl = imagePath;
        }
     //   UPDATING THE STATUS ORDERS FOR RECEIVING GCASH PAYMENT
        protected void btnPaymentAcceptGcash_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            string name = (string)Session["fullname"];
            // Get the log ID from the session
            int logsId = (int)Session["logsId"];

            //INSERT DATA TO TABLE
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the order ID from the first cell in the row
            int orderID = int.Parse(row.Cells[2].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            // Get a list of all available drivers
            FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES");
            Dictionary<string, Employee> driverData = driverResponse.ResultAs<Dictionary<string, Employee>>();


            if (response != null && response.ResultAs<Order>() != null)
            {
                // Check if the order status is "Accepted", "Payment Received", or "Delivered"
                if (existingOrder.order_OrderStatus == "Payment Received")
                {
                    // Disable the button
                    btn.Enabled = false;


                }

                if (existingOrder.order_OrderStatus == "Payment Received")
                {
                    Response.Write("<script>alert ('Payment of this order has already been received!'); </script>");
                    return;
                }

                List<Employee> allDrivers = driverData.Values.Where(emp => emp.adminId.ToString() == idno && emp.emp_role == "Driver"
                                                                    && emp.emp_availability == "Available").ToList();

                if (existingOrder.order_OrderStatus == "Delivered" || existingOrder.order_OrderStatus == "Received Order")
                {
                    // Check if any available driver is available to accept the order
                    if (allDrivers.Count > 0)
                    {
                        // Assign the order to the first available driver
                        Employee driver = allDrivers[0];
                        existingOrder.driverId = driver.emp_id;
                        existingOrder.order_OrderStatus = "Payment Received";
                        existingOrder.paymentGcashReceivedBy = name;
                        existingOrder.datePaymentReceived = DateTime.Now;
                        //existingOrder.datePaymentReceived = DateTimeOffset.Parse("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                        response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                        //SEND NOTIFICATION TO DRIVER FOR PAYMENT RECEIVED
                        int notifID = rnd.Next(1, 20000);
                        var driverNotif = new Model.Notification
                        {
                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            driverId = driver.emp_id,
                            sender = "Admin",
                            receiver = "Driver",
                            body = "The payment for Order ID:" + orderID + " has been received",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = notifID
                        };
                        SetResponse driverNotifRes;
                        driverNotifRes = twoBigDB.Set("NOTIFICATION/" + notifID, driverNotif);//Storing data to the database
                        Notification Drivernotif = driverNotifRes.ResultAs<Notification>();//Database Result


                        //SEND NOTIFICATION TO CUSTOMER 
                        int ID = rnd.Next(1, 20000);
                        var Notification = new Model.Notification
                        {
                            admin_ID = adminId,
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Customer",
                            driverId = driver.emp_id,
                            body = "Your payment for your order with Order ID:" + orderID + " " + "has been received.",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = ID

                        };

                        SetResponse notifResponse;
                        notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                        Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                        if (Session["role"] != null || Session["idno"] != null)
                        {
                            string role = (string)Session["role"];
                            string adminID = (string)Session["idno"];

                            //Random rnd = new Random();
                            int logsID = rnd.Next(1, 10000);

                            // Get the current date and time
                            DateTime addedTime = DateTime.Now;

                            // Log user activity
                            var log = new UsersLogs
                            {
                                userIdnum = int.Parse(adminID),
                                logsId = logsID,
                                role = role,
                                userFullname = (string)Session["fullname"],
                                userActivity = "RECEIVED PAYMENT",
                                activityTime = addedTime
                            };

                            twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                        }

                    }
                    else
                    {
                        // Display an error messaged
                        Response.Write("<script>alert ('Make sure order is successfully delivered.'); location.reload(); window.location.href = '/Admin/WaterOrders.aspx';</script>");
                    }

                    //SEND SCHEDULED NOTIFICATION
                    int schedNotifID = rnd.Next(1, 20000);

                    //condition to check if the quantity is less than or equal to 2, schedule notif after 2 days since ordered. 
                    if (existingOrder.order_overAllQuantities <= 2)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(2);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "It's been 2 days since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,//DATE SCHED NGA MASEND ANG NOTIFICATION
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_overAllQuantities > 2 && existingOrder.order_overAllQuantities <= 5)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(4);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "It's been 4 days since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_overAllQuantities > 5 && existingOrder.order_overAllQuantities <= 8)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(7);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "It's been a week since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_overAllQuantities > 8 && existingOrder.order_overAllQuantities <= 15)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(10);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "We miss you! It's been a long time since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_overAllQuantities > 15)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(20);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "We miss you! It's been a long time since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    displayGcashPaymentReceived_order();
                   
                }
            }
        }
    //    UPDATING THE STATUS ORDERS POINTS IN RECEIVEING POINTS PAYMENT
        protected void btnPaymentAcceptPoints_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            string name = (string)Session["fullname"];
            // Get the log ID from the session
            int logsId = (int)Session["logsId"];

            //INSERT DATA TO TABLE
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the order ID from the first cell in the row
            int orderID = int.Parse(row.Cells[1].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
            Order existingOrder = response.ResultAs<Order>();

            // Get a list of all available drivers
            FirebaseResponse driverResponse = twoBigDB.Get("EMPLOYEES");
            Dictionary<string, Employee> driverData = driverResponse.ResultAs<Dictionary<string, Employee>>();

            if (response != null && response.ResultAs<Order>() != null)
            {
                List<Employee> allDrivers = driverData.Values.Where(emp => emp.adminId.ToString() == idno && emp.emp_role == "Driver"
                                                                    && emp.emp_availability == "Available").ToList();

                // Check if the order status is  "Payment Received"
                if (existingOrder.order_OrderStatus == "Payment Received")
                {
                    // Disable the button
                    btn.Enabled = false;

                    // Store the disabled state in the session
                    Session["DisabledButton_" + orderID] = true;
                }

                if (existingOrder.order_OrderStatus == "Payment Received")
                {
                    Response.Write("<script>alert ('Payment for this order has already been received!');</script>");
                    return;
                }

                if (existingOrder.order_OrderStatus == "Delivered" || existingOrder.order_OrderStatus == "Received Order")
                {

                    // Check if any available driver is available to accept the order
                    if (allDrivers.Count > 0)
                    {
                        // Assign the order to the first available driver
                        Employee driver = allDrivers[0];
                        existingOrder.driverId = driver.emp_id;
                        existingOrder.order_OrderStatus = "Payment Received";
                        existingOrder.paymentPointsReceivedBy = name;
                        existingOrder.datePaymentReceived = DateTime.Now;
                        //existingOrder.datePaymentReceived = DateTimeOffset.Parse("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                        response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                        //SEND NOTIFICATION TO DRIVER FOR PAYMENT RECEIVED
                        int notifID = rnd.Next(1, 20000);
                        var driverNotif = new Model.Notification
                        {
                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            driverId = driver.emp_id,
                            sender = "Admin",
                            receiver = "Driver",
                            title = "Payment Received",
                            body = "The payment for Order ID:" + orderID + " has been received",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = notifID
                        };
                        SetResponse driverNotifRes;
                        driverNotifRes = twoBigDB.Set("NOTIFICATION/" + notifID, driverNotif);//Storing data to the database
                        Notification Drivernotif = driverNotifRes.ResultAs<Notification>();//Database Result


                        //SEND NOTIFICATION TO CUSTOMER 
                        int ID = rnd.Next(1, 20000);
                        var Notification = new Model.Notification
                        {
                            admin_ID = adminId,
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Customer",
                            driverId = driver.emp_id,
                            body = "Your payment for your order with Order ID:" + orderID + "has been received.",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = ID

                        };

                        SetResponse notifResponse;
                        notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                        Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                        if (Session["role"] != null || Session["idno"] != null)
                        {
                            string role = (string)Session["role"];
                            //string idno = (string)Session["idno"];

                            //Random rnd = new Random();
                            int logsID = rnd.Next(1, 10000);

                            // Get the current date and time
                            DateTime addedTime = DateTime.Now;

                            // Log user activity
                            var log = new UsersLogs
                            {
                                userIdnum = int.Parse(idno),
                                logsId = logsID,
                                role = role,
                                userFullname = (string)Session["fullname"],
                                userActivity = "RECEIVED PAYMENT",
                                activityTime = addedTime
                            };

                            twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                        }

                        displayPointsPaymentReceived_order();
                    }
                    else
                    {
                        // Display an error messaged
                        Response.Write("<script>alert ('Make sure order is successfully delivered.'); </script>");
                    }


                    //SEND SCHEDULED NOTIFICATION
                    int schedNotifID = rnd.Next(1, 20000);

                    //condition to check if the quantity is less than or equal to 2, schedule notif after 2 days since ordered. 
                    if (existingOrder.order_overAllQuantities <= 2)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(2);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "It's been 2 days since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_overAllQuantities > 2 && existingOrder.order_overAllQuantities <= 5)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(4);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "It's been 4 days since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_overAllQuantities > 5 && existingOrder.order_overAllQuantities <= 8)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(7);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "It's been a week since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    else if (existingOrder.order_overAllQuantities > 8 && existingOrder.order_overAllQuantities <= 15)
                    {
                        //get the ordered date from the order
                        DateTime orderedDate = existingOrder.orderDate;
                        //add two days from the orderedDate to set the scheduled date
                        DateTime schedule = orderedDate.AddDays(7);

                        var scheduleNotif = new Model.scheduledNotification
                        {

                            admin_ID = adminId,
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            sender = "Admin",
                            title = "Order Reminder",
                            receiver = "Customer",
                            body = "We miss you! It's been a long time since your last tubig order! Order again to earn points!",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            scheduledSent = schedule,
                            notificationID = schedNotifID

                        };
                        SetResponse scheduledNotification;
                        scheduledNotification = twoBigDB.Set("NOTIFICATION/" + schedNotifID, scheduleNotif);
                        scheduledNotification scheduled = scheduledNotification.ResultAs<scheduledNotification>();

                        Debug.WriteLine($"ORDERED DATE: {orderedDate}");
                        Debug.WriteLine($"SCHEDULE: {schedule}");
                    }
                    displayPointsPaymentReceived_order();
                }
            }


        }
        //SAVING ORDER ID FOR ASSIGNING THE DRIVER
        protected void btnAssignDriverClick(object sender, EventArgs e)
        {
            // Retrieve the button that was clicked
            Button btnAssign = (Button)sender;
            // Get the order ID from the command argument
            //int orderID = int.Parse(btnDecline.CommandArgument);
            // Find the GridView row containing the button
            GridViewRow row = (GridViewRow)btnAssign.NamingContainer;

            // Get the order ID from the specific column
            int orderIDColumnIndex = 1; //  the actual column index of the order ID
            int orderID = int.Parse(row.Cells[orderIDColumnIndex].Text);

            // Store the order ID in a hidden field for later use
            hfAssignDriver.Value = orderID.ToString();

            // Show the modal popup
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "assignDriver", "$('#assignDriver').modal('show');", true);

        }
        //RE-ASSIGNING THE DRIVER
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //  generate a random number for employee logged
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            string name = (string)Session["fullname"];
            string role = (string)Session["role"];

            // Get the order ID from the hidden field
            int orderID = int.Parse(hfAssignDriver.Value);

            // Get the driver id to assing
            string driverId = drdAssignDriver.SelectedValue;
            //Get the reason input value
            string reason = txtRe_assigningDriver.Value;


            try
            {

                // Retrieve the existing order object from the database
                FirebaseResponse response = twoBigDB.Get("ORDERS/" + orderID);
                Order existingOrder = response.ResultAs<Order>();

                if (response != null && response.ResultAs<Order>() != null)
                {
                    if (existingOrder.order_OrderTypeValue == "Delivery")
                    {
                        // Update the existing order object with the new driver ID
                        int previousDriverId = existingOrder.driverId; // Retrieve the previous driver ID
                        existingOrder.driverId = int.Parse(driverId);
                        existingOrder.dateDriverAssigned = DateTime.Now;
                        existingOrder.driverAssignedBy = name;

                        // Update the existing order object in the database
                        response = twoBigDB.Update("ORDERS/" + orderID, existingOrder);

                        // Show success message
                        Response.Write("<script>alert('You have successfully re-assigned driver " + driverId + " to order number " + orderID + ".'); </script>");

                        //SEND NOTIFICATION TO CUSTOMER FOR ORDER THAT HAVE BEEN RE-ASSIGNED
                        //Random rnd = new Random();
                        int ID = rnd.Next(1, 20000);
                        var Notification = new Model.Notification
                        {
                            admin_ID = int.Parse(idno),
                            sender = "Admin",
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            receiver = "Customer",
                            title = "Re-assigning Driver",
                            driverId = existingOrder.driverId,
                            body =  reason,
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = ID

                        };

                        SetResponse notifResponse;
                        notifResponse = twoBigDB.Set("NOTIFICATION/" + ID, Notification);//Storing data to the database
                        Notification notif = notifResponse.ResultAs<Notification>();//Database Result

                        //SEND NOTIFICATION TO CURRENT DRIVER
                        int notifID = rnd.Next(1, 20000);
                        var driverNotif = new Model.Notification
                        {
                            admin_ID = int.Parse(idno),
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            driverId = existingOrder.driverId,
                            sender = "Admin",
                            title = "New Assigned Order",
                            receiver = "Driver",
                            body = "Order ID:" + orderID + " has been assigned to you. Check the order page for the details of the order and wait for the confirmation of the customer if they want to push through the order.",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = notifID
                        };
                        SetResponse driverNotifRes;
                        driverNotifRes = twoBigDB.Set("NOTIFICATION/" + notifID, driverNotif);//Storing data to the database
                        Notification Drivernotif = driverNotifRes.ResultAs<Notification>();//Database Result


                        // Send notification to the previous driver
                        int previousDriverNotifID = rnd.Next(1, 20000);
                        var previousDriverNotif = new Model.Notification
                        {
                            admin_ID = int.Parse(idno),
                            orderID = orderID,
                            cusId = existingOrder.cusId,
                            driverId = previousDriverId, // Use the previous driver ID
                            sender = "Admin",
                            title = "Order Reassigned",
                            receiver = "Driver",
                            body = "Order ID: " + orderID + " has been reassigned to another driver.",
                            notificationDate = DateTime.Now,
                            status = "unread",
                            notificationID = previousDriverNotifID
                        };

                        SetResponse previousDriverNotifRes;
                        previousDriverNotifRes = twoBigDB.Set("NOTIFICATION/" + previousDriverNotifID, previousDriverNotif);
                        Notification DriverNotif = previousDriverNotifRes.ResultAs<Notification>();
                    }
                    else
                    {
                        // Show  message
                        Response.Write("<script>alert('You are not allowed to re-assign driver since order wil be pick up by the customer.'); </script>");
                    }
                }

                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    role = role,
                    userFullname = name,
                    activityTime = addedTime,
                    userActivity = "RE-ASSIGNED THE DRIVER TO DELIVER THE ORDER",
                    // userActivity = UserActivityType.UpdatedEmployeeRecords
                };
                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
            }
            catch (Exception ex)
            {
                // Show error message
                Response.Write("<script>alert ('An error occurred while processing your request.');</script>" + ex.Message);
            }
        }

    }
}