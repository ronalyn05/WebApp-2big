using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using WRS2big_Web.Model;

namespace WRS2big_Web.superAdmin
{
    public partial class SubscriptionReports : System.Web.UI.Page
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
                loadSubscriptions();
                    
                LoadSales();

                clientSubHistoryButton.Visible = false;

                Session["ModalOpen"] = null;
            }




        }
        private void LoadSales()
        {
            FirebaseResponse response = twoBigDB.Get("SUBSCRIBED_CLIENTS");
            Model.superAdminClients all = response.ResultAs<Model.superAdminClients>();
            var data = response.Body;
            Dictionary<string, Model.superAdminClients> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superAdminClients>>(data);

            if (data != null && subscribed != null)
            {
                // Create a list to hold the sales data
                List<Model.superAdminClients> salesList = new List<Model.superAdminClients>();

                decimal totalAmount = 0;
                foreach (KeyValuePair<string, Model.superAdminClients> entry in subscribed)
                {
                    
                    salesList.Add(entry.Value);
                }
              
                // Sort the sales list in descending order by date and time
                salesList.Sort((x, y) => y.dateSubscribed.CompareTo(x.dateSubscribed));

                DataTable salesTable = new DataTable();
                salesTable.Columns.Add("SUBSCRIPTION ID");
                salesTable.Columns.Add("DATE / TIME");
                salesTable.Columns.Add("TRANSACTION TYPE");
                salesTable.Columns.Add("CLIENT NAME");
                salesTable.Columns.Add("AMOUNT");

                foreach (Model.superAdminClients sale in salesList)
                {
                    totalAmount += sale.amount;
                    salesTable.Rows.Add(sale.subscriptionID, sale.dateSubscribed, sale.type, sale.fullname, sale.amount);
                }

                // Apply paging
                int pageIndex = subscriptionSales.PageIndex;
                int pageSize = subscriptionSales.PageSize;
                int totalItems = salesTable.Rows.Count;

                // Calculate the number of pages required
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                // Set the page index within the valid range
                pageIndex = Math.Max(0, Math.Min(pageIndex, totalPages - 1));

                // Get the subset of data for the current page
                salesTable = salesTable.AsEnumerable()
                                       .Skip(pageIndex * pageSize)
                                       .Take(pageSize)
                                       .CopyToDataTable();

                // Bind DataTable to GridView control
                subscriptionSales.DataSource = salesTable;
                subscriptionSales.DataBind();

                // Update the pager settings
                subscriptionSales.PageIndex = pageIndex;
                subscriptionSales.PagerSettings.PageButtonCount = totalPages;
                decimal revenue = totalAmount;
                overallRevenue.InnerText = "Php." + " " + revenue.ToString();
            }
        }



        protected void subscriptionSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            subscriptionSales.PageIndex = e.NewPageIndex;
            LoadSales(); // Re-bind the GridView with updated data for the new page
        }

        protected void pagerRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Page")
            {
                int pageIndex = 0;
                if (int.TryParse(e.CommandArgument.ToString(), out pageIndex))
                {
                    subscriptionSales.PageIndex = pageIndex;
                    LoadSales();
                }
            }
        }


        protected void subscriptionSales_PreRender(object sender, EventArgs e)
        {
            GridViewRow pagerRow = subscriptionSales.BottomPagerRow;
            if (pagerRow != null)
            {
                GridViewRow pageRow = subscriptionSales.BottomPagerRow;
                if (pageRow != null)
                {
                    DropDownList pageDropDownList = pageRow.FindControl("subscriptionSalesPager") as DropDownList;
                    if (pageDropDownList != null)
                    {
                        int totalPages = subscriptionSales.PageCount;
                        for (int i = 1; i <= totalPages; i++)
                        {
                            pageDropDownList.Items.Add(i.ToString());
                        }
                        pageDropDownList.SelectedValue = (subscriptionSales.PageIndex + 1).ToString();
                    }
                }
            }
        }



        ////SORTING VIA HEADER
        //protected void salesGrid_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    // Get the data source and apply sorting based on the selected column
        //    // You can use LINQ or any other sorting mechanism here
        //    DataTable salesData = GetData(); // Replace this with your data retrieval logic

        //    if (salesData != null)
        //    {
        //        DataView sortedDataView = new DataView(salesData);
        //        sortedDataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

        //        subscriptionSales.DataSource = sortedDataView;
        //        subscriptionSales.DataBind();
        //    }
        //}

        //private string GetSortDirection(string sortExpression)
        //{
        //    // Check the current sort direction and return the opposite
        //    string currentDirection = ViewState["SortDirection"] as string;

        //    if (currentDirection != null && currentDirection.Equals("ASC") && currentDirection.Equals(sortExpression))
        //        return "DESC";
        //    else
        //        return "ASC";
        //}

        //private DataTable GetData()
        //{
        //    // Retrieve data from the Firebase database and convert it to a DataTable
        //    FirebaseResponse response = twoBigDB.Get("SUBSCRIBED_CLIENTS");
        //    var data = response.Body;
        //    Dictionary<string, Model.superAdminClients> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superAdminClients>>(data);

        //    DataTable salesData = new DataTable();
        //    salesData.Columns.Add("SUBSCRIPTION ID");
        //    salesData.Columns.Add("DATE / TIME");
        //    salesData.Columns.Add("TRANSACTION TYPE");
        //    salesData.Columns.Add("CLIENT NAME");
        //    salesData.Columns.Add("AMOUNT");
        //    salesData.Columns.Add("BALANCE");

        //    decimal totalAmount = 0;
        //    foreach (KeyValuePair<string, Model.superAdminClients> entry in subscribed)
        //    {
        //        totalAmount += entry.Value.amount;
        //        salesData.Rows.Add(entry.Value.subscriptionID, entry.Value.dateSubscribed, entry.Value.type, entry.Value.fullname, entry.Value.amount, totalAmount);
        //    }

        //    return salesData;
        //}
        ////END OF SORTING VIA HEADER





        private void loadSubscriptions()
        {
            FirebaseResponse response = twoBigDB.Get("SUBSCRIBED_CLIENTS");//CHANGE TO ADMIN TABLE
            Model.superAdminClients all = response.ResultAs<Model.superAdminClients>();
            var data = response.Body;
            Dictionary<string, Model.superAdminClients> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superAdminClients>>(data);
            
            if (data != null && subscribed != null)
            {
                //creating the columns of the gridview
                DataTable clientsTable = new DataTable();
                clientsTable.Columns.Add("ID");
                clientsTable.Columns.Add("DATE");
                clientsTable.Columns.Add("NAME");
                clientsTable.Columns.Add("AMOUNT");
                clientsTable.Columns.Add("PACKAGE");
                clientsTable.Columns.Add("STATUS");

                foreach (KeyValuePair<string, Model.superAdminClients> entry in subscribed)
                {

                    clientsTable.Rows.Add(entry.Value.clientID,entry.Value.dateSubscribed, entry.Value.fullname, entry.Value.amount, entry.Value.plan, entry.Value.currentSubStatus);

                }
                // Bind DataTable to GridView control
                subscriptionReport.DataSource = clientsTable;
                subscriptionReport.DataBind();
            }
           

        }
        protected void closeModal_Click(object sender, EventArgs e)
        {
            Session["ModalOpen"] = null;
        }

        protected void modalSearch_Click(object sender, EventArgs e)
        {
            string searched = searchClient.Text;

            FirebaseResponse response = twoBigDB.Get("SUBSCRIBED_CLIENTS");//CHANGE TO ADMIN TABLE
            Model.superAdminClients all = response.ResultAs<Model.superAdminClients>();
            var data = response.Body;
            Dictionary<string, Model.superAdminClients> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superAdminClients>>(data);

            if (data != null && subscribed != null)
            {
                //creating the columns of the gridview
                DataTable clientsTable = new DataTable();
                clientsTable.Columns.Add("ID");
                clientsTable.Columns.Add("DATE");
                clientsTable.Columns.Add("NAME");
                clientsTable.Columns.Add("AMOUNT");
                clientsTable.Columns.Add("PACKAGE");
                clientsTable.Columns.Add("STATUS");

                foreach (KeyValuePair<string, Model.superAdminClients> entry in subscribed)
                {
                    int clientID = entry.Value.clientID;
                    response = twoBigDB.Get("ADMIN/" + clientID);
                    AdminAccount admin = response.ResultAs<AdminAccount>();

                    if (searched == admin.fname || searched == admin.lname || searched == admin.mname || searched == entry.Key)
                    {
                        clientsTable.Rows.Add(entry.Value.clientID, entry.Value.dateSubscribed, entry.Value.fullname, entry.Value.amount, entry.Value.plan, entry.Value.currentSubStatus);

                    }

                }
                if (clientsTable.Rows.Count == 0)
                {
                    searchClient.Text = "";
                    Response.Write("<script>alert ('Client not found!');</script>");

                }
                // Bind DataTable to GridView control
                subscriptionReport.DataSource = clientsTable;
                subscriptionReport.DataBind();
            }

        }

        protected void viewSubscriptionHistory_Click(object sender, EventArgs e)
        {
            //Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            //Get the order ID from the first cell in the row
            int adminID = int.Parse(row.Cells[1].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("ADMIN/" + adminID);
            Model.AdminAccount clientDetails = response.ResultAs<Model.AdminAccount>();

            int currentClient = clientDetails.idno;
            Session["currentClient"] = currentClient;

            Response.Write("<script>window.location.href = '/superAdmin/clientSubscriptionHistory.aspx'; </script>");
        }

        protected void subscriptionSales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                GridViewRow pagerRow = e.Row;
                foreach (TableCell cell in pagerRow.Cells)
                {
                    LinkButton lnkButton = cell.Controls.OfType<LinkButton>().FirstOrDefault();
                    if (lnkButton != null)
                    {
                        if (lnkButton.CommandArgument == subscriptionSales.PageIndex.ToString())
                        {
                            lnkButton.Enabled = false; // Disable the active page link
                            lnkButton.BackColor = System.Drawing.Color.Blue; // Change the color of the active page link
                        }
                    }
                }
            }
        }
        

        protected void selectedClient_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selected = (CheckBox)sender;

            CheckBox client = (CheckBox)selected.NamingContainer.FindControl("selectedClient");

            if (client != null && client.Checked)
            {
                clientSubHistoryButton.Visible = true;
                Session["ModalOpen"] = selected.Checked;

            }
            else
            {
                Session["ModalOpen"] = null;
            }

            List<int> selectedClient = new List<int>();

            foreach (GridViewRow row in subscriptionReport.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("selectedClient");
                if (chk != null && chk.Checked)
                {
                    int clientID = int.Parse(row.Cells[1].Text);
                    selectedClient.Add(clientID);

                    Session["currentClient"] = clientID;
                  

                }
            }

            if (selectedClient.Count > 1)
            {
                Response.Write("<script>alert ('Please select one client only'); </script>");
                return;
            }
        }

        //private void LoadClientHistory()
        //{

        //    int clientID = (int)Session["currentClient"];

        //    if (clientID == 0 && Session["currentClient"] == null)
        //    {
        //        Response.Write("<script>alert ('Session Expired! Please login again'); window.location.href = '/superAdmin/SuperAdminAccount.aspx'; </script>");
        //    }
        //    FirebaseResponse response = twoBigDB.Get("SUBSCRIPTION_LOGS");
        //    Model.subscriptionLogs all = response.ResultAs<Model.subscriptionLogs>();
        //    var data = response.Body;
        //    Dictionary<string, Model.subscriptionLogs> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.subscriptionLogs>>(data);

        //    decimal totalAmount = 0;
        //    if (data != null)
        //    {
        //        //creating the columns of the gridview
        //        DataTable clientsTable = new DataTable();
        //        clientsTable.Columns.Add("LOG ID");
        //        clientsTable.Columns.Add("NAME");
        //        clientsTable.Columns.Add("SUBSCRIPTION TYPE");
        //        clientsTable.Columns.Add("DATE / TIME");
        //        clientsTable.Columns.Add("PACKAGE");
        //        clientsTable.Columns.Add("TOTAL");


        //        foreach (KeyValuePair<string, Model.subscriptionLogs> entry in subscribed)
        //        {
        //            if (entry.Value.userIdnum == clientID)
        //            {
        //                FirebaseResponse adminDet = twoBigDB.Get("ADMIN/" + clientID);
        //                Model.AdminAccount admin = adminDet.ResultAs<Model.AdminAccount>();

        //                totalAmount += entry.Value.total;
        //                clientsTable.Rows.Add(entry.Value.logsId, entry.Value.userFullname, entry.Value.userActivity, entry.Value.activityTime, entry.Value.packageName,totalAmount);

        //            }

        //        }
        //        // Bind DataTable to GridView control
        //        clientSubHistory.DataSource = clientsTable;
        //        clientSubHistory.DataBind();
        //    }
        //}




        protected void generateSortedData_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = twoBigDB.Get("SUBSCRIBED_CLIENTS");
            Model.superAdminClients all = response.ResultAs<Model.superAdminClients>();
            var data = response.Body;
            Dictionary<string, Model.superAdminClients> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superAdminClients>>(data);

            if (data != null && subscribed != null)
            {
                // Get the selected start and end dates
                if (string.IsNullOrEmpty(sortStart.Text) || string.IsNullOrEmpty(sortEnd.Text))
                {
                    // Handle the missing start or end date condition (e.g., display an alert)
                    // For example, you can use JavaScript to show an alert box:
                    Response.Write("<script>alert ('You must choose a Start and End Date');</script>");
                    return; // Exit the method or return the appropriate response
                }

                // Get the selected start and end dates
                DateTime startDate = DateTime.Parse(sortStart.Text);
                DateTime endDate = DateTime.Parse(sortEnd.Text).AddDays(1); // Add one day to include the end date

                // Filter the data based on the selected dates
                var filteredData = subscribed.Values.Where(s => s.dateSubscribed >= startDate && s.dateSubscribed < endDate);

                // Check if the filtered data is empty
                if (!filteredData.Any())
                {
                    // Handle the empty data condition (e.g., display a message, hide the GridView)
                    // For example, you can display a message in a label control:
                    subSalesLabel.Text = "No data available for the selected date range.";
                    subscriptionRevenue.Visible = false;
                    subscriptionSales.Visible = false;
                    overallRevenue.InnerText = ""; // Clear the revenue if no data is available
                    return; // Exit the method or return the appropriate response
                }


                // Create a list to hold the filtered sales data
                List<Model.superAdminClients> salesList = new List<Model.superAdminClients>(filteredData);

                // Sort the sales list in descending order by date and time
                salesList.Sort((x, y) => y.dateSubscribed.CompareTo(x.dateSubscribed));

                DataTable salesTable = new DataTable();
                salesTable.Columns.Add("SUBSCRIPTION ID");
                salesTable.Columns.Add("DATE / TIME");
                salesTable.Columns.Add("TRANSACTION TYPE");
                salesTable.Columns.Add("CLIENT NAME");
                salesTable.Columns.Add("AMOUNT");

                decimal totalAmount = 0;

                foreach (Model.superAdminClients sale in salesList)
                {
                    totalAmount += sale.amount;
                    salesTable.Rows.Add(sale.subscriptionID, sale.dateSubscribed, sale.type, sale.fullname, sale.amount);
                }

                // Apply paging
                int pageIndex = subscriptionSales.PageIndex;
                int pageSize = subscriptionSales.PageSize;
                int totalItems = salesTable.Rows.Count;

                // Calculate the number of pages required
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                // Set the page index within the valid range
                pageIndex = Math.Max(0, Math.Min(pageIndex, totalPages - 1));

                // Get the subset of data for the current page
                salesTable = salesTable.AsEnumerable()
                                       .Skip(pageIndex * pageSize)
                                       .Take(pageSize)
                                       .CopyToDataTable();

                // Bind DataTable to GridView control
                subscriptionSales.DataSource = salesTable;
                subscriptionSales.DataBind();

                // Update the pager settings
                subscriptionSales.PageIndex = pageIndex;
                subscriptionSales.PagerSettings.PageButtonCount = totalPages;

                decimal revenue = totalAmount;
                overallRevenue.InnerText = "Php." + " " + revenue.ToString();
            }

        }

        protected void clearSort_Click(object sender, EventArgs e)
        {
            sortStart.Text = "";
            sortEnd.Text = "";
            Response.Write("<script> window.location.href = '/superAdmin/SubscriptionReports.aspx'; </script>");
            //sortbyMonthEnd.SelectedValue = "";
            //sortbyMonthStart.SelectedValue = "";
        }

        protected void closeButton_Click(object sender, EventArgs e)
        {
            searchClient.Text = "";
        }

        protected void clientSubHistory_Click(object sender, EventArgs e)
        {
            List<int> selectedClient = new List<int>();

            foreach (GridViewRow row in subscriptionReport.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("selectedClient");
                if (chk != null && chk.Checked)
                {
                    int clientID = int.Parse(row.Cells[1].Text);
                    selectedClient.Add(clientID);

                    Session["currentClient"] = clientID;
                    Response.Write("<script>window.location.href = '/superAdmin/clientSubscriptionHistory.aspx'; </script>");

                }
            }

            if (selectedClient.Count > 1)
            {
                Response.Write("<script>alert ('Please select one client only'); </script>");
                return;
            }
        }

        //protected void generateMonthSort_Click(object sender, EventArgs e)
        //{
        //    FirebaseResponse response = twoBigDB.Get("SUBSCRIBED_CLIENTS");
        //    Model.superAdminClients all = response.ResultAs<Model.superAdminClients>();
        //    var data = response.Body;
        //    Dictionary<string, Model.superAdminClients> subscribed = JsonConvert.DeserializeObject<Dictionary<string, Model.superAdminClients>>(data);

        //    if (data != null && subscribed != null)
        //    {
        //        // Get the selected start and end months
        //        int startMonth = sortbyMonthStart.SelectedIndex;
        //        int endMonth = sortbyMonthEnd.SelectedIndex;

        //        // Filter the data based on the selected months
        //        var filteredData = subscribed.Values.Where(s => s.dateSubscribed.Month >= startMonth && s.dateSubscribed.Month <= endMonth);

        //        // Check if the filtered data is empty
        //        if (!filteredData.Any())
        //        {
        //            // Handle the empty data condition (e.g., display a message, hide the GridView)
        //            // For example, you can display a message in a label control:
        //            subSalesLabel.Text = "No data available for the selected month range.";
        //            subscriptionSales.Visible = false;
        //            overallRevenue.InnerText = ""; // Clear the revenue if no data is available
        //            return; // Exit the method or return the appropriate response
        //        }

        //        // Create a list to hold the filtered sales data
        //        List<Model.superAdminClients> salesList = new List<Model.superAdminClients>(filteredData);

        //        // Sort the sales list in descending order by date and time
        //        salesList.Sort((x, y) => y.dateSubscribed.CompareTo(x.dateSubscribed));

        //        DataTable salesTable = new DataTable();
        //        salesTable.Columns.Add("SUBSCRIPTION ID");
        //        salesTable.Columns.Add("DATE / TIME");
        //        salesTable.Columns.Add("TRANSACTION TYPE");
        //        salesTable.Columns.Add("CLIENT NAME");
        //        salesTable.Columns.Add("AMOUNT");

        //        decimal totalAmount = 0;

        //        foreach (Model.superAdminClients sale in salesList)
        //        {
        //            totalAmount += sale.amount;
        //            salesTable.Rows.Add(sale.subscriptionID, sale.dateSubscribed, sale.type, sale.fullname, sale.amount);
        //        }

        //        // Apply paging
        //        int pageIndex = subscriptionSales.PageIndex;
        //        int pageSize = subscriptionSales.PageSize;
        //        int totalItems = salesTable.Rows.Count;

        //        // Calculate the number of pages required
        //        int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        //        // Set the page index within the valid range
        //        pageIndex = Math.Max(0, Math.Min(pageIndex, totalPages - 1));

        //        // Get the subset of data for the current page
        //        salesTable = salesTable.AsEnumerable()
        //                               .Skip(pageIndex * pageSize)
        //                               .Take(pageSize)
        //                               .CopyToDataTable();

        //        // Bind DataTable to GridView control
        //        subscriptionSales.DataSource = salesTable;
        //        subscriptionSales.DataBind();

        //        // Update the pager settings
        //        subscriptionSales.PageIndex = pageIndex;
        //        subscriptionSales.PagerSettings.PageButtonCount = totalPages;

        //        decimal revenue = totalAmount;
        //        overallRevenue.InnerText = "Php." + " " + revenue.ToString();
        //    }

        //}
    }
}