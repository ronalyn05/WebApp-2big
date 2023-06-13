using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;


namespace WRS2big_Web.superAdmin
{
    public partial class ClientsSalesReportPage : System.Web.UI.Page
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

            if(!IsPostBack)
            {
                loadClientSales();
                loadWalkinSales();

               

                if (Session["onlineRevenue"] == null || Session["WalkinSale"] == null)
                {

                    overallRevenue.InnerText = "No ORDERS and REVENUE available";

                }
                else
                {
                    //TO CALCULATE THE TOTAL REVENUE
                    decimal onlineSales = (decimal)Session["onlineRevenue"];
                    decimal walkinSales = (decimal)Session["WalkinSale"];

                    if (walkinSales != 0 || onlineSales != 0)
                    {
                        decimal TotalRevenue = onlineSales + walkinSales;
                        overallRevenue.InnerText = TotalRevenue.ToString();

                    }
                    else
                    {
                        overallRevenue.InnerText = "No ORDERS and REVENUE available";
                    }
                }
               

                displayClientdetails();
            }
        }
        private void displayClientdetails()
        {
            if (Session["currentClient"] != null)
            {
                int clientID = (int)Session["currentClient"];
                //RETRIEVE CLIENT DETAILS
                FirebaseResponse response = twoBigDB.Get("ADMIN/" + clientID);
                Model.AdminAccount admin = response.ResultAs<Model.AdminAccount>();

                //REFILLING STATION DETAILS
                response = twoBigDB.Get("ADMIN/" + clientID + "/RefillingStation");
                Model.RefillingStation station = response.ResultAs<Model.RefillingStation>();

                //CLIENT WALKIN ORDERS
                response = twoBigDB.Get("WALKINORDERS");
                var data = response.Body;
                Dictionary<string, Model.WalkInOrders> walkinOrderList = JsonConvert.DeserializeObject<Dictionary<string, Model.WalkInOrders>>(data);

                //CLIENT ONLINE ORDERS
                response = twoBigDB.Get("ORDERS");
                var orders = response.Body;
                Dictionary<string, Model.Order> onlineOrderList = JsonConvert.DeserializeObject<Dictionary<string, Model.Order>>(orders);


                if (admin != null)
                {
                    currentClient.InnerText = admin.fname + " " + admin.mname + " " + admin.lname;
                    currentStationName.InnerText = station.stationName;
                    currentStationAddress.InnerText = station.stationAddress;

                }

                // Create a list to store all the Walkin orders
                List<Model.WalkInOrders> clientWalkins = new List<Model.WalkInOrders>();

                // Create a list to store all the online orders
                List<Model.Order> clientOnline = new List<Model.Order>();

                if (walkinOrderList != null)
                {

                    foreach (KeyValuePair<string, Model.WalkInOrders> entry in walkinOrderList)
                    {
                        if (entry.Value.adminId == clientID)
                        {
                            clientWalkins.Add(entry.Value);
                        }

                        
                    }


                }
                int totalWalkins = clientWalkins.Count;

                if (onlineOrderList != null)
                {
                    foreach (KeyValuePair<string, Model.Order> entry in onlineOrderList)
                    {

                        if (entry.Value.admin_ID == clientID && entry.Value.order_OrderStatus == "Payment Received")
                        {
                            clientOnline.Add(entry.Value);
                        }
                        
                    }

                }
                int totalOnlineOrders = clientOnline.Count;

                int totalAdminOrders = totalWalkins + totalOnlineOrders;
                currentTotalOrders.InnerText = totalAdminOrders.ToString();
            }
            else
            {
                Response.Write("<script>alert (Something went wrong. Please select a client again'); window.location.href = '/superAdmin/ClientsSalesReportPage.aspx'; </script>");
            }

        }
        private void loadWalkinSales()
        {
            

            if (Session["currentClient"] != null)
            {
                int clientID = (int)Session["currentClient"];
                FirebaseResponse response = twoBigDB.Get("WALKINORDERS");
                Model.WalkInOrders all = response.ResultAs<Model.WalkInOrders>();
                var data = response.Body;
                Dictionary<string, Model.WalkInOrders> walkinOrderList = JsonConvert.DeserializeObject<Dictionary<string, Model.WalkInOrders>>(data);

                List<Model.WalkInOrders> successwalkInOrders = new List<Model.WalkInOrders>();


                if (data != null && walkinOrderList != null)
                {
                    foreach (KeyValuePair<string, Model.WalkInOrders> entry in walkinOrderList)
                    {
                        if (entry.Value.adminId == clientID)
                        {
                            successwalkInOrders.Add(entry.Value);
                            
                        }
                    }
                    if (successwalkInOrders.Count != 0)
                    {
                        decimal revenue = 0;
                        string orderType = "Walk-in Order";

                        successwalkInOrders.Sort((x, y) => DateTime.Compare(y.dateAdded, x.dateAdded)); // Sort the orders based on orderDate


                        DataTable ordersTable = new DataTable();
                        ordersTable.Columns.Add("ORDER ID");
                        ordersTable.Columns.Add("CREATED BY");
                        ordersTable.Columns.Add("QUANTITY");
                        ordersTable.Columns.Add("AMOUNT PAID");
                        ordersTable.Columns.Add("ORDER TYPE");
                        ordersTable.Columns.Add("DATE ORDERED");

                        foreach(var entry in successwalkInOrders)
                        {
                            revenue += entry.totalAmount;

                            ordersTable.Rows.Add(entry.orderNo,
                                entry.addedBy,
                                entry.productQty,
                                 "₱" + " " + entry.totalAmount,
                                orderType,
                                entry.dateAdded);
                        }

                        walkInOrders.DataSource = ordersTable;
                        walkInOrders.DataBind();

                        walkinOrdersRev.InnerText = "₱" + " " + revenue.ToString();

                        Session["WalkinSale"] = revenue;

                    }
                }
            }
            else
            {
                Response.Write("<script>alert (Something went wrong. Please select a client again'); window.location.href = '/superAdmin/ClientsSalesReportPage.aspx'; </script>");
            }




         }
        private void loadClientSales()
        {


            if (Session["currentClient"] != null)
            {
                int clientID = (int)Session["currentClient"];


                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Model.Order all = response.ResultAs<Model.Order>();
                var data = response.Body;
                Dictionary<string, Model.Order> clientSale = JsonConvert.DeserializeObject<Dictionary<string, Model.Order>>(data);

                if (data != null && clientSale != null)
                {

                    List<Model.Order> paymentReceivedOrders = new List<Model.Order>();

                    foreach (KeyValuePair<string, Model.Order> entry in clientSale)
                    {
                        if (entry.Value.order_OrderStatus == "Payment Received" && entry.Value.admin_ID == clientID)
                        {
                            paymentReceivedOrders.Add(entry.Value);
                        }
                    }

                    if (paymentReceivedOrders.Count != 0)
                    {
                        paymentReceivedOrders.Sort((x, y) => DateTime.Compare(y.orderDate, x.orderDate));

                        DataTable ordersTable = new DataTable();
                        ordersTable.Columns.Add("ORDER ID");
                        ordersTable.Columns.Add("NAME");
                        ordersTable.Columns.Add("QUANTITY");
                        ordersTable.Columns.Add("AMOUNT PAID");
                        ordersTable.Columns.Add("ORDER TYPE");
                        ordersTable.Columns.Add("DATE ORDERED");

                        decimal revenue = 0;
                        string orderType = "Online Order";
                        foreach (var clientOrders in paymentReceivedOrders)
                        {
                            int customerID = clientOrders.cusId;
                            response = twoBigDB.Get("CUSTOMER/" + customerID);
                            Model.Customer orderedCus = response.ResultAs<Model.Customer>();

                            revenue += clientOrders.order_TotalAmount;

                            ordersTable.Rows.Add(clientOrders.orderID,
                                orderedCus.firstName + " " + orderedCus.lastName,
                                clientOrders.order_overAllQuantities,
                                "₱" + " " + clientOrders.order_TotalAmount,
                                orderType,
                                clientOrders.orderDate
                                );
                        }

                        clientSalesReport.DataSource = ordersTable;
                        clientSalesReport.DataBind();

                        onlineOrdersRev.InnerText = "₱" + " " + revenue.ToString();
                        Session["onlineRevenue"] = revenue;

                    }



                }

            }
            else
            {
                Response.Write("<script>alert (Something went wrong. Please select a client again'); window.location.href = '/superAdmin/ClientsSalesReportPage.aspx'; </script>");
            }
            

        }

        //protected void generateSortedData_Click(object sender, EventArgs e)
        //{
        //    if (Session["currentClient"] != null)
        //    {
        //        int clientID = (int)Session["currentClient"];

        //        FirebaseResponse response = twoBigDB.Get("ORDERS");
        //        Model.Order all = response.ResultAs<Model.Order>();
        //        var data = response.Body;
        //        Dictionary<string, Model.Order> clientSale = JsonConvert.DeserializeObject<Dictionary<string, Model.Order>>(data);

        //        response = twoBigDB.Get("WALKINORDERS");
        //        Model.WalkInOrders walkins = response.ResultAs<Model.WalkInOrders>();
        //        var walkin = response.Body;
        //        Dictionary<string, Model.WalkInOrders> walkinOrders = JsonConvert.DeserializeObject<Dictionary<string, Model.WalkInOrders>>(walkin);


        //        if (data != null && clientSale != null)
        //        {
        //            // Get the selected start and end dates
        //            if (string.IsNullOrEmpty(sortStart.Text) || string.IsNullOrEmpty(sortEnd.Text))
        //            {
        //                // Handle the missing start or end date condition (e.g., display an alert)
        //                // For example, you can use JavaScript to show an alert box:
        //                Response.Write("<script>alert ('You must choose a Start and End Date');</script>");
        //                return; // Exit the method or return the appropriate response
        //            }

        //            // Get the selected start and end dates
        //            DateTime startDate = DateTime.Parse(sortStart.Text);
        //            DateTime endDate = DateTime.Parse(sortEnd.Text).AddDays(1); // Add one day to include the end date

        //            List<Model.Order> paymentReceivedOrders = new List<Model.Order>();
        //            List<Model.WalkInOrders> addedwalkinOrders = new List<Model.WalkInOrders>();


        //            foreach (KeyValuePair<string, Model.Order> entry in clientSale)
        //            {
        //                if (entry.Value.order_OrderStatus == "Payment Received" && entry.Value.admin_ID == clientID)
        //                {
        //                    paymentReceivedOrders.Add(entry.Value);
        //                }
        //            }
        //            foreach (KeyValuePair<string, Model.WalkInOrders> orders in walkinOrders)
        //            {
        //                if (orders.Value.adminId == clientID)
        //                {
        //                    addedwalkinOrders.Add(orders.Value);
        //                }
        //            }


        //            if (paymentReceivedOrders.Count == 0)
        //            {
        //                Response.Write("<script>alert ('No Records for ONLINE ORDERS found for this client'); window.location.href = '/superAdmin/StationSales.aspx'; </script>");
        //                return;
        //            }
        //            if (addedwalkinOrders.Count == 0)
        //            {
        //                Response.Write("<script>alert ('No Records for WALK-IN ORDERS found for this client'); window.location.href = '/superAdmin/StationSales.aspx'; </script>");
        //                return;
        //            }

        //            // Filter the orders based on the selected date range
        //            List<Model.Order> filteredOrders = paymentReceivedOrders.Where(o => o.orderDate >= startDate && o.orderDate < endDate).ToList();
        //            List<Model.WalkInOrders> filteredWalkins = addedwalkinOrders.Where(w => w.dateAdded >= startDate && w.dateAdded < endDate).ToList();

        //            // Sort the orders by order date
        //            filteredOrders.Sort((x, y) => DateTime.Compare(x.orderDate, y.orderDate));
        //            filteredWalkins.Sort((x, y) => DateTime.Compare(x.dateAdded, y.dateAdded));


        //            // Check if the filtered data is empty
        //            if (!filteredOrders.Any())
        //            {
        //                // Handle the empty data condition (e.g., display a message, hide the GridView)
        //                // For example, you can display a message in a label control:
        //                onlineOrdersLabel.Text = "No data available for the selected date range.";
        //                //subscriptionRevenue.Visible = false;
        //                clientSalesReport.Visible = false;
        //                onlineOrdersRev.InnerText = ""; // Clear the revenue if no data is available
        //                return; // Exit the method or return the appropriate response
        //            }
        //            if (!filteredWalkins.Any())
        //            {
        //                walkinLabel.Text = "No data available for the selected date range.";
        //                walkInOrders.Visible = false;

        //                walkinOrdersRev.InnerText = "";

        //                return;
        //            }

        //            //CREATE DATATABLES
        //            DataTable ordersTable = new DataTable();
        //            ordersTable.Columns.Add("ORDER ID");
        //            ordersTable.Columns.Add("NAME");
        //            ordersTable.Columns.Add("QUANTITY");
        //            ordersTable.Columns.Add("AMOUNT PAID");
        //            ordersTable.Columns.Add("ORDER TYPE");
        //            ordersTable.Columns.Add("DATE ORDERED");

        //            DataTable walkinsTable = new DataTable();
        //            walkinsTable.Columns.Add("ORDER ID");
        //            walkinsTable.Columns.Add("CREATED BY");
        //            walkinsTable.Columns.Add("QUANTITY");
        //            walkinsTable.Columns.Add("AMOUNT PAID");
        //            walkinsTable.Columns.Add("ORDER TYPE");
        //            walkinsTable.Columns.Add("DATE ORDERED");

        //            //FOR THE ONLINE ORDERS
        //            decimal revenue = 0;
        //            string orderType = "Online Order";
        //            foreach (var clientOrder in filteredOrders)
        //            {
        //                int customerID = clientOrder.cusId;
        //                response = twoBigDB.Get("CUSTOMER/" + customerID);
        //                Model.Customer orderedCus = response.ResultAs<Model.Customer>();

        //                revenue += clientOrder.order_TotalAmount;

        //                ordersTable.Rows.Add(clientOrder.orderID,
        //                    orderedCus.firstName + " " + orderedCus.lastName,
        //                    clientOrder.order_overAllQuantities,
        //                    clientOrder.order_TotalAmount,
        //                    orderType,
        //                    clientOrder.orderDate
        //                    );
        //            }

        //            clientSalesReport.DataSource = ordersTable;
        //            clientSalesReport.DataBind();

        //            decimal totalRevenue = revenue;
        //            onlineOrdersRev.InnerText = "₱" + " " + totalRevenue.ToString();

        //            //FOR THE WALKIN ORDERS
        //            decimal revenueW = 0;
        //            string orderTypeW = "Walk-in Orders";

        //            foreach (var adminWalkins in filteredWalkins)
        //            {
        //                if (adminWalkins.adminId == clientID)
        //                {
        //                    revenueW += adminWalkins.totalAmount;
        //                    walkinsTable.Rows.Add(adminWalkins.orderNo,
        //                        adminWalkins.addedBy,
        //                        adminWalkins.productQty,
        //                        adminWalkins.totalAmount,
        //                        orderTypeW,
        //                        adminWalkins.dateAdded);
        //                }
        //            }
        //            walkInOrders.DataSource = walkinsTable;
        //            walkInOrders.DataBind();

        //            walkinOrdersRev.InnerText = "₱" + " " + revenueW.ToString();


        //        }
        //    }
        //    else
        //    {

        //        Response.Write("<script>alert ('Something went wrong. Please select a client again'); window.location.href = '/superAdmin/ClientsSalesReportPage.aspx'; </script>");
        //    }

        //}

        protected void generateSortedData_Click(object sender, EventArgs e)
        {
            if (Session["currentClient"] != null)
            {
                int clientID = (int)Session["currentClient"];

                FirebaseResponse response = twoBigDB.Get("ORDERS");
                Model.Order all = response.ResultAs<Model.Order>();
                var data = response.Body;
                Dictionary<string, Model.Order> clientSale = JsonConvert.DeserializeObject<Dictionary<string, Model.Order>>(data);

                response = twoBigDB.Get("WALKINORDERS");
                Model.WalkInOrders walkins = response.ResultAs<Model.WalkInOrders>();
                var walkin = response.Body;
                Dictionary<string, Model.WalkInOrders> walkinOrders = JsonConvert.DeserializeObject<Dictionary<string, Model.WalkInOrders>>(walkin);

                if (data != null && clientSale != null)
                {
                    // Get the selected start and end dates
                    if (string.IsNullOrEmpty(sortStart.Text) || string.IsNullOrEmpty(sortEnd.Text))
                    {
                        Response.Write("<script>alert('You must choose a Start and End Date');</script>");
                        return;
                    }

                    DateTime startDate = DateTime.Parse(sortStart.Text);
                    DateTime endDate = DateTime.Parse(sortEnd.Text).AddDays(1);

                    List<Model.Order> paymentReceivedOrders = new List<Model.Order>();
                    List<Model.WalkInOrders> addedwalkinOrders = new List<Model.WalkInOrders>();

                    foreach (KeyValuePair<string, Model.Order> entry in clientSale)
                    {
                        if (entry.Value.order_OrderStatus == "Payment Received" && entry.Value.admin_ID == clientID)
                        {
                            paymentReceivedOrders.Add(entry.Value);
                        }
                    }

                    foreach (KeyValuePair<string, Model.WalkInOrders> orders in walkinOrders)
                    {
                        if (orders.Value.adminId == clientID)
                        {
                            addedwalkinOrders.Add(orders.Value);
                        }
                    }

                    if (paymentReceivedOrders.Count == 0)
                    {
                        Response.Write("<script>alert('No Records for ONLINE ORDERS found for this client'); window.location.href = '/superAdmin/StationSales.aspx'; </script>");
                        return;
                    }

                    if (addedwalkinOrders.Count == 0)
                    {
                        Response.Write("<script>alert('No Records for WALK-IN ORDERS found for this client'); window.location.href = '/superAdmin/StationSales.aspx'; </script>");
                        return;
                    }

                    List<Model.Order> filteredOrders = paymentReceivedOrders.Where(o => o.orderDate >= startDate && o.orderDate < endDate).ToList();

                    List<Model.WalkInOrders> filteredWalkins = addedwalkinOrders.Where(w => w.dateAdded >= startDate && w.dateAdded < endDate).ToList();

                    filteredOrders.Sort((x, y) => DateTime.Compare(x.orderDate, y.orderDate));
                    filteredWalkins.Sort((x, y) => DateTime.Compare(y.dateAdded, x.dateAdded));

                    if (!filteredOrders.Any())
                    {
                        onlineOrdersLabel.Text = "No data available for the selected date range.";
                        clientSalesReport.Visible = false;
                        onlineOrdersRev.InnerText = "";
                    }
                    else
                    {
                        DataTable ordersTable = new DataTable();
                        ordersTable.Columns.Add("ORDER ID");
                        ordersTable.Columns.Add("NAME");
                        ordersTable.Columns.Add("QUANTITY");
                        ordersTable.Columns.Add("AMOUNT PAID");
                        ordersTable.Columns.Add("ORDER TYPE");
                        ordersTable.Columns.Add("DATE ORDERED");

                        decimal revenue = 0;
                        string orderType = "Online Order";

                        foreach (var clientOrder in filteredOrders)
                        {
                            int customerID = clientOrder.cusId;
                            response = twoBigDB.Get("CUSTOMER/" + customerID);
                            Model.Customer orderedCus = response.ResultAs<Model.Customer>();

                            revenue += clientOrder.order_TotalAmount;

                            ordersTable.Rows.Add(clientOrder.orderID,
                                orderedCus.firstName + " " + orderedCus.lastName,
                                clientOrder.order_overAllQuantities,
                                clientOrder.order_TotalAmount,
                                orderType,
                                clientOrder.orderDate);
                        }

                        clientSalesReport.DataSource = ordersTable;
                        clientSalesReport.DataBind();

                        decimal totalRevenue = revenue;
                        onlineOrdersRev.InnerText = "₱" + " " + totalRevenue.ToString();
                    }

                    if (!filteredWalkins.Any())
                    {
                        walkinLabel.Text = "No data available for the selected date range.";
                        walkInOrders.Visible = false;
                        walkinOrdersRev.InnerText = "";
                    }
                    else
                    {
                        DataTable walkinsTable = new DataTable();
                        walkinsTable.Columns.Add("ORDER ID");
                        walkinsTable.Columns.Add("CREATED BY");
                        walkinsTable.Columns.Add("QUANTITY");
                        walkinsTable.Columns.Add("AMOUNT PAID");
                        walkinsTable.Columns.Add("ORDER TYPE");
                        walkinsTable.Columns.Add("DATE ORDERED");

                        decimal revenueW = 0;
                        string orderTypeW = "Walk-in Orders";

                        foreach (var adminWalkins in filteredWalkins)
                        {
                            if (adminWalkins.adminId == clientID)
                            {
                                revenueW += adminWalkins.totalAmount;
                                walkinsTable.Rows.Add(adminWalkins.orderNo,
                                    adminWalkins.addedBy,
                                    adminWalkins.productQty,
                                    adminWalkins.totalAmount,
                                    orderTypeW,
                                    adminWalkins.dateAdded);
                            }
                        }

                        walkInOrders.DataSource = walkinsTable;
                        walkInOrders.DataBind();

                        walkinOrdersRev.InnerText = "₱" + " " + revenueW.ToString();
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Something went wrong. Please select a client again'); window.location.href = '/superAdmin/ClientsSalesReportPage.aspx'; </script>");
            }
        }

        protected void clearSort_Click(object sender, EventArgs e)
        {
            sortStart.Text = "";
            sortEnd.Text = "";
            Response.Write("<script> window.location.href = '/superAdmin/ClientsSalesReportPage.aspx'; </script>");
        }

    }
}