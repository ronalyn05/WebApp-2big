using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
    public partial class Deliverydetails : System.Web.UI.Page
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

            deliveryTypesGrid();
            //detailsGridView();
            displayDelDetails();
            // expressDisplay();
            // reservationDisplay();
            //standardDisplay();

            //getDeliveryTypes();

            //hide buttons in page load
            updateExpress.Visible = false;
            updateReservation.Visible = false;
            updateStandard.Visible = false;


            //POPULATE THE UPDATE MODALS WITH THEIR CORRESPONDING EXISTING DATA 


        }
        private void displayDelDetails()
        {

            FirebaseResponse getDetails = twoBigDB.Get("DELIVERY_DETAILS");
            Dictionary<string, Delivery> details = getDetails.ResultAs<Dictionary<string, Delivery>>();

            if (details != null)
            {

                string idno = (string)Session["idno"];

                // Retrieve all data from the DELIVERY_DETAILS table
                FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS");
                Dictionary<string, Delivery> deliveryList = response.ResultAs<Dictionary<string, Delivery>>();
                var filteredList = deliveryList.Values.Where(d => d.adminId.ToString() == idno);

                DataTable vehicledDetails = new DataTable();
                vehicledDetails.Columns.Add("VEHICLE NAME");
                vehicledDetails.Columns.Add("VEHICLE FEE");

                DataTable swapDetails = new DataTable();
                swapDetails.Columns.Add("SWAP OPTIONS");
                swapDetails.Columns.Add("Fee");

                DataTable paymentDetails = new DataTable();
                paymentDetails.Columns.Add("PAYMENT METHODS");
                paymentDetails.Columns.Add("Number");

                DataTable orderTypes = new DataTable();
                orderTypes.Columns.Add("ORDER TYPES");

                if (response != null && response.ResultAs<Delivery>() != null)
                {
                    foreach (var entry in filteredList)
                    {

                        DataRow row1 = vehicledDetails.NewRow();
                        row1["VEHICLE NAME"] = entry.vehicle1Name;
                        row1["VEHICLE FEE"] = entry.vehicle1Fee;
                        vehicledDetails.Rows.Add(row1);

                        DataRow row2 = vehicledDetails.NewRow();
                        row2["VEHICLE NAME"] = entry.vehicle2Name;
                        row2["VEHICLE FEE"] = entry.vehicle2Fee;
                        vehicledDetails.Rows.Add(row2);

                        DataRow row3 = vehicledDetails.NewRow();
                        row3["VEHICLE NAME"] = entry.vehicle3Name;
                        row3["VEHICLE FEE"] = entry.vehicle3Fee;
                        vehicledDetails.Rows.Add(row3);

                        DataRow row4 = vehicledDetails.NewRow();
                        row4["VEHICLE NAME"] = entry.vehicle4Name;
                        row4["VEHICLE FEE"] = entry.vehicle4Fee;
                        vehicledDetails.Rows.Add(row4);

                        string[] swapOptions = entry.swapOptions.Split(',');
                        foreach (string swapOption in swapOptions)
                        {
                            DataRow row = swapDetails.NewRow();
                            row["SWAP OPTIONS"] = swapOption.Trim();
                            if (swapOption.Trim() == "Request Pick-up")
                            {
                                row["Fee"] = "per Gallon :" + " " +  entry.perGallonFee;
                            }
                            swapDetails.Rows.Add(row);
                        }

                        if (entry.paymentMethods != null)
                        {
                            string[] payments = entry.paymentMethods.Split(',');
                            foreach (string payment in payments)
                            {
                                DataRow row = paymentDetails.NewRow();
                                row["PAYMENT METHODS"] = payment.Trim();
                                if (payment.Trim() == "Gcash")
                                {
                                    row["Number"] = entry.gcashNumber;
                                }
                                paymentDetails.Rows.Add(row);
                            }
                        }
                        

                        string[] types = entry.orderTypes.Split(',');
                        foreach(string orders in types)
                        {
                            DataRow row = orderTypes.NewRow();
                            row["ORDER TYPES"] = orders.Trim();
                            orderTypes.Rows.Add(row);
                        }
                        
                    }
                }
                else
                {

                }
                //Bind the DataTable to the respective GridViews
                vehiclesGridview.DataSource = vehicledDetails;
                vehiclesGridview.DataBind();

                swapOptionsGrid.DataSource = swapDetails;
                swapOptionsGrid.DataBind();

                paymentsGrid.DataSource = paymentDetails;
                paymentsGrid.DataBind();

                orderTypesGrid.DataSource = orderTypes;
                orderTypesGrid.DataBind();
            }

        }
       
        private void deliveryTypesGrid()
        {
            FirebaseResponse getDetails = twoBigDB.Get("DELIVERY_DETAILS");
            Dictionary<string, Delivery> details = getDetails.ResultAs<Dictionary<string, Delivery>>();

            if (details != null)
            {

                string idno = (string)Session["idno"];

                // Retrieve all data from the DELIVERY_DETAILS table
                FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS");
                Dictionary<string, Delivery> deliveryList = response.ResultAs<Dictionary<string, Delivery>>();
                var filteredList = deliveryList.Values.Where(d => d.adminId.ToString() == idno);

               
                // Create the DataTable to hold the orders
                //sa pag create sa table
                DataTable expressTable = new DataTable();
                expressTable.Columns.Add("DELIVERY TYPE ID");
                expressTable.Columns.Add("DELIVERY TYPE");
                expressTable.Columns.Add("DATE ADDED");
                expressTable.Columns.Add("ADDED BY");

                if (response != null && response.ResultAs<Delivery>() != null)
                {
                    foreach (var entry in filteredList)
                    {
                        if (entry.resDeliveryType == "Reservation")
                        {
                            response = twoBigDB.Get("ADMIN/" + idno);
                            AdminAccount adminDetail = response.ResultAs<AdminAccount>();
                            //ROW FOR THE RESERVATION
                            expressTable.Rows.Add(entry.reservationID, entry.resDeliveryType,
                                                    entry.reservationdateAdded, adminDetail.fname + " " + adminDetail.lname);

                            Session["reservationID"] = entry.reservationID;

                        }
                        else
                        {

                        }
                        if (entry.stanDeliverytype == "Standard")
                        {
                            response = twoBigDB.Get("ADMIN/" + idno);
                            AdminAccount adminDetail = response.ResultAs<AdminAccount>();
                            //ROW FOR THE STANDARD
                            expressTable.Rows.Add(entry.standardID, entry.stanDeliverytype,
                                entry.standardDateAdded, adminDetail.fname + " " + adminDetail.lname);
                        }
                        else
                        {

                        }
                        if (entry.exDeliveryType == "Express")
                        {
                            response = twoBigDB.Get("ADMIN/" + idno);
                            AdminAccount adminDetail = response.ResultAs<AdminAccount>();
                            //ROW FOR THE EXPRESS
                            expressTable.Rows.Add(entry.expressID, entry.exDeliveryType,
                                entry.expressdateAdded, adminDetail.fname + " " + adminDetail.lname);
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
            else
            {
                //hide the dropdown for deliveryDetails
                btnDeliverytype.Visible = false;
                drdDeliverytype.Visible = false;
            }



        }
        protected void btnDeliverydetails_Click(object sender, EventArgs e)
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);


            FirebaseResponse allDelivery = twoBigDB.Get("DELIVERY_DETAILS/");
            var all = allDelivery.Body;
            Dictionary<string, Model.Delivery> adminAllDelivery = JsonConvert.DeserializeObject<Dictionary<string, Model.Delivery>>(all);

            // Loop through all the deliverydetails
            foreach (KeyValuePair<string, Model.Delivery> entry in adminAllDelivery)
            {
                if (entry.Value.adminId == adminId)
                {
                    int deliveryID = entry.Value.deliveryId;
                    Session["deliveryID"] = deliveryID;
                } 
            }

            int deliveryIdno = (int)Session["deliveryID"];

            // Check if there is an existing delivery object for this admin
            FirebaseResponse resDelivery = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryIdno);
            Delivery delivery = null;
            if (resDelivery.Body != "null")
            {
                delivery = resDelivery.ResultAs<Delivery>();

            }
            else
            {
                Response.Write("<script>alert ('You must setup your Delivery Details first before you can set up your Delivery Types');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");

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
                                Response.Write("<script>alert ('FAILED! Existing STANDARD Delivery type! You already created Standard delivery. You can update the Standard Delivery in the Delivery details page');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");
                                break;
                            }
                            else
                            {
                                DateTime operatingHrsFrom = DateTime.ParseExact(standardSchedFrom.Text, "HH:mm", CultureInfo.InvariantCulture);
                                DateTime operatingHrsTo = DateTime.ParseExact(standardSchedTo.Text, "HH:mm", CultureInfo.InvariantCulture);

                                // Convert the times to 12-hour format with AM and PM
                                string operatingHrsFrom12Hr = operatingHrsFrom.ToString("h:mm tt");
                                string operatingHrsTo12Hr = operatingHrsTo.ToString("h:mm tt");

                                string standardSchedule = operatingHrsFrom12Hr + "-" + operatingHrsTo12Hr;

                                int standardID = new Random().Next(1, 10000);
                                delivery.stanDeliverytype = "Standard";
                                delivery.standardDateAdded = DateTimeOffset.UtcNow;
                                delivery.standardID = standardID;
                                delivery.stanDeliveryFee = DeliveryFee.Text;
                                delivery.stanDeliveryTime = standardSchedule;
                                delivery.standistance = FreeDelivery.Text;
                                //delivery.stanOrderType = GetSelectedValues(DeliveryType);
                                delivery.standardProducts = GetSelectedValues(OrderMethod);
                                //delivery.standardSwapOptions = GetSelectedValues(standardSwapOptions);

                                // Parse the operating hours from and to times as DateTime objects

                            }
                            break;
                        case "Reservation":
                            // If there is an existing reservation delivery object, update it, otherwise create a new one
                            if (delivery.resDeliveryType == "Reservation")
                            {
                                Response.Write("<script>alert ('FAILED! Existing RESERVATION Delivery type! You already created Reservation delivery.');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");
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
                                delivery.reserveProducts = GetSelectedValues(reserveOrderMethod);
                                //delivery.resOrderType = GetSelectedValues(reserveOrderType);
                                //delivery.reserveSwapOptions = GetSelectedValues(reserveSwap);
                            }
                            break;
                        case "Express":
                            // check If there is an existing express delivery type
                            if (delivery.exDeliveryType == "Express")
                            {
                                Response.Write("<script>alert ('FAILED! Existing EXPRESS Delivery type! You already created Express delivery.');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");
                                break;
                            }
                            else
                            {
                                int expressID = new Random().Next(1, 10000);
                                delivery.exDeliveryType = "Express";
                                delivery.expressDistance = int.Parse(expressDistance.Text);
                                delivery.expressdateAdded = DateTimeOffset.UtcNow;
                                delivery.expressID = expressID;
                                delivery.exDeliveryFee = expressdeliveryFee.Text;
                                delivery.exEstimatedDelivery = estimatedTime.Text;
                                delivery.expressProducts = GetSelectedValues(expressOrderMethod);
                                //delivery.exOrderType = GetSelectedValues(expressOrderType);
                                //delivery.expressSwapOptions = GetSelectedValues(expressSwap);
                            }
                            break;
                    }
                }
            }
            // Save the updated delivery object to the database
            FirebaseResponse res = twoBigDB.Set("DELIVERY_DETAILS/" + deliveryIdno, delivery);
            Response.Write("<script>alert ('You successfully created the Delivery Types you offer to your business');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");

            int logsId = (int)Session["logsId"];
            // Retrieve the existing Users log object from the database
            FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
            UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

            // Get the current date and time
            DateTime addedTime = DateTime.UtcNow;

            // Log user activity
            var log = new UsersLogs
            {
                userIdnum = int.Parse(idno),
                logsId = logsId,
                userFullname = (string)Session["fullname"],
                userActivity = "ADDED DELIVERY DETAILS",
                activityTime = addedTime
            };

            twoBigDB.Update("ADMINLOGS/" + log.logsId, log);
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
                    updateExpress.Visible = true;
                    expressDisplay();
                }
                else if (selectedOption == "2")
                {
                    lblDeliveryType.Text = "STANDARD";
                    expressGridview.Visible = false;
                    reservationGridView.Visible = false;
                    standardGridview.Visible = true;
                    updateStandard.Visible = true;
                    standardDisplay();
                }
                else if (selectedOption == "3")
                {
                    lblDeliveryType.Text = "RESERVATION";
                    expressGridview.Visible = false;
                    standardGridview.Visible = false;
                    reservationGridView.Visible = true;
                    updateReservation.Visible = true;
                    reservationDisplay();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/Deliverydetails.aspx';" + ex.Message);
            }
        }
        //DISPLAY EXPRESS DETAILS
        private void expressDisplay()
        {
            nullLabel.Text = "";
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all data from the DELIVERY_DETAILS table
            FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS");
            Dictionary<string, Delivery> deliveryList = response.ResultAs<Dictionary<string, Delivery>>();
            var filteredList = deliveryList.Values.Where(d => d.adminId.ToString() == idno);

            if (filteredList != null)
            {
                // Create the DataTable to hold the orders
                //sa pag create sa table
                DataTable expressTable = new DataTable();
                expressTable.Columns.Add("EXPRESS ID");
                expressTable.Columns.Add("ESTIMATED DELIVERY TIME");
                expressTable.Columns.Add("DELIVERY FEE");
                expressTable.Columns.Add("EXPRESS PRODUCTS");
                expressTable.Columns.Add("DATE ADDED");
                expressTable.Columns.Add("ADDED BY");

                if (response != null && response.ResultAs<Delivery>() != null)
                {
                    foreach (var entry in filteredList)
                    {
                        if (entry.exDeliveryType == "Express")
                        {
                            response = twoBigDB.Get("ADMIN/" + idno);
                            AdminAccount adminDetail = response.ResultAs<AdminAccount>();
                            expressTable.Rows.Add(entry.expressID, entry.exEstimatedDelivery, entry.exDeliveryFee, entry.expressProducts, entry.expressdateAdded, adminDetail.fname + " " + adminDetail.lname);

                           
                            //POPULATE THE UPDATE MODAL WITH THE RESPECTIVE DETAILS
                            updateExpressTime.Attributes["placeholder"] = entry.exEstimatedDelivery;
                            updateExpressDistance.Attributes["placeholder"] = entry.expressDistance.ToString();
                            updateExpressFee.Attributes["placeholder"] = entry.exDeliveryFee;

                            //  string[] swapOptions = entry.swapOptions.Split(',');
                            string[] expressProds = entry.expressProducts.Split(',');
                            foreach (string product in expressProds)
                            {
                                if (product.Trim() == "Refill")
                                {
                                    expressRefill.Selected = true;
                                }
                                if (product.Trim() == "other products")
                                {
                                    expressOther.Selected = true;
                                }
                            }
                        }
                        else
                        {
                            nullLabel.Text = "No 'Express Delivery' data available";
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
            
        }
        private void standardDisplay()
        {
            nullLabel.Text = "";

            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all data from the DELIVERY_DETAILS table
            FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS");
            Dictionary<string, Delivery> deliveryList = response.ResultAs<Dictionary<string, Delivery>>();
            var filteredList = deliveryList.Values.Where(d => d.adminId.ToString() == idno);

            if (filteredList != null)
            {
                // Create the DataTable to hold the details
                DataTable standardTable = new DataTable();
                standardTable.Columns.Add("STANDARD ID");
                standardTable.Columns.Add("TIME SCHEDULE FOR DELIVERY");
                standardTable.Columns.Add("DISTANCE FOR FREE DELIVERY ");
                standardTable.Columns.Add("DELIVERY FEE");
                standardTable.Columns.Add("STANDARD PRODUCTS");
                standardTable.Columns.Add("DATE ADDED");
                standardTable.Columns.Add("ADDED BY");

                if (response != null && response.ResultAs<Delivery>() != null)
                {
                    foreach (var entry in filteredList)
                    {
                        if (entry.stanDeliverytype == "Standard")
                        {
                            response = twoBigDB.Get("ADMIN/" + idno);
                            AdminAccount adminDetail = response.ResultAs<AdminAccount>();
                            standardTable.Rows.Add(entry.standardID, entry.stanDeliveryTime, entry.standistance, entry.stanDeliveryFee, entry.standardProducts,
                            entry.standardDateAdded, adminDetail.fname + " " + adminDetail.lname);

                            //POPULATE THE DETAILS FOR THE MODAL
                            exisitingSchdule.Text = entry.stanDeliveryTime;
                            updatestandardDistance.Attributes["placeholder"] = entry.standistance;
                            updateStandardFee.Attributes["placeholder"] = entry.stanDeliveryFee;

                            //  string[] swapOptions = entry.swapOptions.Split(',');
                            string[] standardProds = entry.standardProducts.Split(',');
                            foreach (string product in standardProds)
                            {
                                if (product.Trim() == "Refill")
                                {
                                    standardRefillOp.Selected = true;
                                }
                                if (product.Trim() == "other products")
                                {
                                    standardOtherProd.Selected = true;
                                }
                            }
                           

                            //firstname.Attributes["placeholder"] = admin.fname;
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
            
        }
        //DISPLAY RESERVATION DETAILS
        private void reservationDisplay()
        {
            nullLabel.Text = "";
            string idno = (string)Session["idno"];
            string empId = (string)Session["emp_id"];
            // int adminId = int.Parse(idno);

            FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS");
            Dictionary<string, Delivery> deliveryList = response.ResultAs<Dictionary<string, Delivery>>();
            var filteredList = deliveryList.Values.Where(d => d.adminId.ToString() == idno);

            if (filteredList != null)
            {
                // Create the DataTable to hold the orders
                //sa pag create sa table
                DataTable reservationTable = new DataTable();
                reservationTable.Columns.Add("RESERVATION ID");
                reservationTable.Columns.Add("DISTANCE FOR FREE DELIVERY");
                reservationTable.Columns.Add("DELIVERY FEE");
                reservationTable.Columns.Add("RESERVATION PRODUCTS");
                reservationTable.Columns.Add("DATE ADDED");
                reservationTable.Columns.Add("ADDED BY");

                if (response != null && response.ResultAs<Delivery>() != null)
                {
                    foreach (var entry in filteredList)
                    {
                        if (entry.resDeliveryType == "Reservation")
                        {
                            response = twoBigDB.Get("ADMIN/" + idno);
                            AdminAccount adminDetail = response.ResultAs<AdminAccount>();
                            reservationTable.Rows.Add(entry.reservationID, entry.resDistanceFree, entry.resDeliveryFee, entry.reserveProducts, entry.reservationdateAdded, adminDetail.fname + " " + adminDetail.lname);

                            //POPULATE THE UPDATE MODAL WITH THE RESPECTIVE DETAILS
                            updateReserveDistance.Attributes["placeholder"] = entry.resDistanceFree;
                            updateReserveFee.Attributes["placeholder"] = entry.resDeliveryFee;

                            //  string[] swapOptions = entry.swapOptions.Split(',');
                            string[] reserveProds = entry.reserveProducts.Split(',');
                            foreach (string product in reserveProds)
                            {
                                if (product.Trim() == "Refill")
                                {
                                    reserveRefill.Selected = true;
                                }
                                if (product.Trim() == "other products")
                                {
                                    reserveOther.Selected = true;
                                }
                            }

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
           
        }

        protected void paymentButton_Click(object sender, EventArgs e)
        {


            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);


            FirebaseResponse allDelivery = twoBigDB.Get("DELIVERY_DETAILS/");
            var all = allDelivery.Body;
            Dictionary<string, Model.Delivery> adminAllDelivery = JsonConvert.DeserializeObject<Dictionary<string, Model.Delivery>>(all);

            // Loop through all the deliverydetails
            foreach (KeyValuePair<string, Model.Delivery> entry in adminAllDelivery)
            {
                if (entry.Value.adminId == adminId)
                {
                    int deliveryID = entry.Value.deliveryId;
                    Session["deliveryID"] = deliveryID;
                }
            }

            int deliveryIdno = (int)Session["deliveryID"];

          
            // Check if there is an existing delivery object for this admin
            FirebaseResponse paymentResponse = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryIdno);
            //Model.AdminAccount update = updateAdmin.ResultAs<Model.AdminAccount>();
            Delivery payment = paymentResponse.ResultAs<Delivery>();

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

                paymentResponse = twoBigDB.Update("DELIVERY_DETAILS/" + deliveryIdno, payment);
                Response.Write("<script>alert ('Payment Methods successsfully added');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");
            }
            else
            {
                Response.Write("<script>alert ('FAILED !! Payment Method has been already set-up.');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");
            }

        }




        protected void viewButton_Click(object sender, EventArgs e)
        {
            //    //Get the GridViewRow that contains the clicked button
            //    Button btn = (Button)sender;
            //    GridViewRow row = (GridViewRow)btn.NamingContainer;
            //    string idno = (string)Session["idno"];
            //    int adminID = int.Parse(idno);

            //    //Get the order ID from the first cell in the row
            //    int typeId = int.Parse(row.Cells[1].Text);

            //    FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS/" + adminID);
            //    Model.DeliveryDetails delivery = response.ResultAs<Model.DeliveryDetails>();

            //    if (typeId == delivery.expressID)
            //    {
            //        nullLabel.Text = "";

            //        // Create the DataTable to hold the orders
            //        //sa pag create sa table
            //        DataTable expressTable = new DataTable();
            //        expressTable.Columns.Add("EXPRESS ID");
            //        expressTable.Columns.Add("ESTIMATED DELIVERY TIME");
            //        expressTable.Columns.Add("DELIVERY FEE");
            //        expressTable.Columns.Add("REFILL SWAP OPTIONS");
            //        expressTable.Columns.Add("ORDER TYPE");
            //        expressTable.Columns.Add("ORDER METHOD");
            //        expressTable.Columns.Add("DATE ADDED");
            //        expressTable.Columns.Add("PAYMENT METHOD");
            //        expressTable.Columns.Add("ADDED BY");

            //        if (response != null && response.ResultAs<Model.DeliveryDetails>() != null)
            //        {

            //                if (entry.exDeliveryType == "Express")
            //                {
            //                    //expressTable.Rows.Add(entry.expressID, entry.exEstimatedDelivery, entry.exDeliveryFee, entry.expressSwapOptions,
            //                    //                  entry.exOrderType, entry.exOrderMethod, entry.expressdateAdded, entry.paymentMethods, entry.adminId);
            //                    expressTable.Rows.Add(entry.expressID, entry.exEstimatedDelivery, entry.exDeliveryFee, entry.expressdateAdded, entry.paymentMethods, entry.adminId);
            //                }
            //                else
            //                {
            //                    nullLabel.Text = "No 'Express Delivery' data avaialble";
            //                }


            //        }
            //        else
            //        {
            //            // Handle null response or invalid selected value
            //            nullLabel.Text = "No 'Express Delivery' data avaialble";
            //        }

            //        // Bind the DataTable to the GridView
            //        expressGridview.DataSource = expressTable;
            //        expressGridview.DataBind();

            //    }


        }

        protected void vehicleAdded_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

            try
            {
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                var delivery = new Delivery
                {
                    adminId = adminId,
                    deliveryId = idnum,
                    orderTypes = GetSelectedValues(orderTypes),
                    swapOptions = GetSelectedValues(swapOptionItems),
                    vehicle1Name = vehicle1Name.Text,
                    vehicle2Name = vehicle2Name.Text,
                    vehicle3Name = vehicle3Name.Text,
                    vehicle4Name = vehicle4Name.Text,
                    vehicle1Fee = vehicle1Fee.Text,
                    vehicle2Fee = vehicle2Fee.Text,
                    vehicle3Fee = vehicle3Fee.Text,
                    vehicle4Fee = vehicle4Fee.Text,
                    perGallonFee = perGallonFee.Text
                };

               

                SetResponse response;
                response = twoBigDB.Set("DELIVERY_DETAILS/" + idnum, delivery);
                Delivery result = response.ResultAs<Delivery>();
                //save the deliveryID in the session
                Session["deliveryID"] = idnum;
                Response.Write("<script>alert ('Thankyou for setting up your Delivery Details! You can now proceed with creating your Delivery Types'); window.location.href = '/Admin/Deliverydetails.aspx'; </script>");


            }
            catch
            {
                Response.Write("<script>alert ('Something went wrong'); window.location.href = '/Admin/Deliverydetails.aspx'; </script>");

            }
        }

        protected void selectAll_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((sender as CheckBox).NamingContainer as GridViewRow);
            

            if ((sender as CheckBox).Checked)
            {
                int deliveryTypeID;
                if (int.TryParse(gridDeliveryDetails.Rows[row.RowIndex].Cells[1].Text, out deliveryTypeID))
                {
                    // Retrieve the existing order object from the database
                    FirebaseResponse getDetails = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryTypeID);
                    Model.Delivery typeDetails = getDetails.ResultAs<Model.Delivery>();

                    if (deliveryTypeID == typeDetails.standardID)
                    {
                        standardGridview.Visible = true;
                        expressGridview.Visible = false;
                        reservationGridView.Visible = false;
                    }

                    else if (deliveryTypeID == typeDetails.expressID)
                    {
                      
                    }
                    else if (deliveryTypeID == typeDetails.reservationID)
                    {
                       
                    }
                }
               
            }

        }

        protected void viewDeliveryType_Click(object sender, EventArgs e)
        {
            //Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            //Get the order ID from the first cell in the row
            int deliveryTypeID = int.Parse(row.Cells[2].Text);

            // Retrieve the existing order object from the database
            FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryTypeID);
            Model.Delivery customerDetails = response.ResultAs<Model.Delivery>();

            if (deliveryTypeID == customerDetails.expressID)
            {
                expressGridview.Visible = true;
            }
            else if (deliveryTypeID == customerDetails.standardID)
            {
                standardGridview.Visible = true;
            }
            else if (deliveryTypeID == customerDetails.reservationID)
            {
                reservationGridView.Visible = true;
            }
        }



        protected void updateExpressbutton_Click(object sender, EventArgs e)
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);


            FirebaseResponse allDelivery = twoBigDB.Get("DELIVERY_DETAILS/");
            var all = allDelivery.Body;
            Dictionary<string, Model.Delivery> adminAllDelivery = JsonConvert.DeserializeObject<Dictionary<string, Model.Delivery>>(all);

            // Loop through all the deliverydetails
            foreach (KeyValuePair<string, Model.Delivery> entry in adminAllDelivery)
            {
                if (entry.Value.adminId == adminId)
                {
                    int deliveryID = entry.Value.deliveryId;
                    Session["deliveryID"] = deliveryID;
                }
            }

            int deliveryIdno = (int)Session["deliveryID"];

            // Check if there is an existing delivery object for this admin
            FirebaseResponse resDelivery = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryIdno);
            Delivery delivery = null;
            if (resDelivery.Body != "null")
            {
                delivery = resDelivery.ResultAs<Delivery>();

                var updatedExpress = new Delivery
                {
                    //NOT EDITABLE
                    swapOptions = delivery.swapOptions,
                    vehicle1Fee = delivery.vehicle1Fee,
                    vehicle1Name = delivery.vehicle1Name,
                    vehicle2Fee = delivery.vehicle2Fee,
                    vehicle2Name = delivery.vehicle2Name,
                    vehicle3Fee = delivery.vehicle3Fee,
                    vehicle3Name = delivery.vehicle3Name,
                    vehicle4Fee = delivery.vehicle4Fee,
                    vehicle4Name = delivery.vehicle4Name,
                    orderTypes = delivery.orderTypes,
                    perGallonFee = delivery.perGallonFee,
                    exDeliveryType = delivery.exDeliveryType,
                    stanDeliverytype = delivery.stanDeliverytype,
                    resDeliveryType = delivery.resDeliveryType,

                    //EDITABLE
                    
                    exEstimatedDelivery = delivery.exEstimatedDelivery,
                    expressDistance = delivery.expressDistance,
                    expressID = delivery.expressID,
                    expressProducts = delivery.expressProducts,
                    expressdateAdded = delivery.expressdateAdded,
                    resDeliveryFee = delivery.resDeliveryFee,
                    resDistanceFree = delivery.resDistanceFree,
                    reservationID = delivery.reservationID,
                    reservationdateAdded = delivery.reservationdateAdded,
                    reserveProducts = delivery.reserveProducts,
                    stanDeliveryFee = delivery.stanDeliveryFee,
                    stanDeliveryTime = delivery.stanDeliveryTime,
                    standardDateAdded = delivery.standardDateAdded,
                    standardID = delivery.standardID,
                    standardProducts = delivery.standardProducts,
                    standistance = delivery.standistance
            };


                if (!string.IsNullOrEmpty(updateExpressTime.Text))
                {
                    delivery.exEstimatedDelivery = updateExpressTime.Text;
                }
                if (!string.IsNullOrEmpty(updateExpressFee.Text))
                {
                    delivery.exDeliveryFee = updateExpressFee.Text;
                }
                if (!string.IsNullOrEmpty(updateExpressDistance.Text))
                {
                    delivery.expressDistance = int.Parse(updateExpressDistance.Text);
                }
                if (updateExpressChckbx.Items.Cast<ListItem>().Any(item => item.Selected))
                {
                    delivery.expressProducts = string.Join(",", updateExpressChckbx.Items.Cast<ListItem>()
                        .Where(item => item.Selected)
                        .Select(item => item.Value));
                }

                // Save the updated delivery object to the database
                FirebaseResponse res = twoBigDB.Update("DELIVERY_DETAILS/" + deliveryIdno, delivery);
                Response.Write("<script>alert ('Express Delivery updated successfully');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");


                int logsId = (int)Session["logsId"];
                // Retrieve the existing Users log object from the database
                FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = logsId,
                    userFullname = (string)Session["fullname"],
                    userActivity = "UPDATED EXPRESS DELIVERY",
                    activityTime = addedTime
                };

                twoBigDB.Update("ADMINLOGS/" + log.logsId, log);
            }

        }

        protected void updateStandardbutton_Click(object sender, EventArgs e)
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);


            FirebaseResponse allDelivery = twoBigDB.Get("DELIVERY_DETAILS/");
            var all = allDelivery.Body;
            Dictionary<string, Model.Delivery> adminAllDelivery = JsonConvert.DeserializeObject<Dictionary<string, Model.Delivery>>(all);

            // Loop through all the deliverydetails
            foreach (KeyValuePair<string, Model.Delivery> entry in adminAllDelivery)
            {
                if (entry.Value.adminId == adminId)
                {
                    int deliveryID = entry.Value.deliveryId;
                    Session["deliveryID"] = deliveryID;
                }
            }

            int deliveryIdno = (int)Session["deliveryID"];

            // Check if there is an existing delivery object for this admin
            FirebaseResponse resDelivery = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryIdno);
            Delivery delivery = null;
            if (resDelivery.Body != "null")
            {
                delivery = resDelivery.ResultAs<Delivery>();

                var updatedExpress = new Delivery
                {
                    //NOT EDITABLE
                    swapOptions = delivery.swapOptions,
                    vehicle1Fee = delivery.vehicle1Fee,
                    vehicle1Name = delivery.vehicle1Name,
                    vehicle2Fee = delivery.vehicle2Fee,
                    vehicle2Name = delivery.vehicle2Name,
                    vehicle3Fee = delivery.vehicle3Fee,
                    vehicle3Name = delivery.vehicle3Name,
                    vehicle4Fee = delivery.vehicle4Fee,
                    vehicle4Name = delivery.vehicle4Name,
                    orderTypes = delivery.orderTypes,
                    perGallonFee = delivery.perGallonFee,
                    exDeliveryType = delivery.exDeliveryType,
                    stanDeliverytype = delivery.stanDeliverytype,
                    resDeliveryType = delivery.resDeliveryType,

                    //EDITABLE

                    exEstimatedDelivery = delivery.exEstimatedDelivery,
                    expressDistance = delivery.expressDistance,
                    expressID = delivery.expressID,
                    expressProducts = delivery.expressProducts,
                    expressdateAdded = delivery.expressdateAdded,
                    resDeliveryFee = delivery.resDeliveryFee,
                    resDistanceFree = delivery.resDistanceFree,
                    reservationID = delivery.reservationID,
                    reservationdateAdded = delivery.reservationdateAdded,
                    reserveProducts = delivery.reserveProducts,
                    stanDeliveryFee = delivery.stanDeliveryFee,
                    stanDeliveryTime = delivery.stanDeliveryTime,
                    standardDateAdded = delivery.standardDateAdded,
                    standardID = delivery.standardID,
                    standardProducts = delivery.standardProducts,
                    standistance = delivery.standistance
                };

               

                if (!string.IsNullOrEmpty(Request.Form[updateStandardFrom.UniqueID].ToString()))
                {
                    DateTime operatingHrsFrom = DateTime.ParseExact(updateStandardFrom.Text, "HH:mm", CultureInfo.InvariantCulture);

                    if (!string.IsNullOrEmpty(Request.Form[updateStandardTo.UniqueID].ToString()))
                    {
                        DateTime operatingHrsTo = DateTime.ParseExact(updateStandardTo.Text, "HH:mm", CultureInfo.InvariantCulture);
                        delivery.stanDeliveryTime = operatingHrsFrom.ToString("h:mm tt") + "-" + operatingHrsTo.ToString("h:mm tt") ;
                        //station.operatingHrsTo = operatingHrsTo12Hr;
                    }
                }
                

                if (!string.IsNullOrEmpty(updatestandardDistance.Text))
                {
                    delivery.stanDeliveryFee = updatestandardDistance.Text;
                }
                if (!string.IsNullOrEmpty(updateStandardFee.Text))
                {
                    delivery.standistance = updateStandardFee.Text;
                }
                if (updateStandardChkBx.Items.Cast<ListItem>().Any(item => item.Selected))
                {
                    delivery.standardProducts = string.Join(",", updateStandardChkBx.Items.Cast<ListItem>()
                        .Where(item => item.Selected)
                        .Select(item => item.Value));
                }

                // Save the updated delivery object to the database
                FirebaseResponse res = twoBigDB.Update("DELIVERY_DETAILS/" + deliveryIdno, delivery);
                Response.Write("<script>alert ('Standard Delivery updated successfully');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");


                int logsId = (int)Session["logsId"];
                // Retrieve the existing Users log object from the database
                FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = logsId,
                    userFullname = (string)Session["fullname"],
                    userActivity = "UPDATED STANDARD DELIVERY",
                    activityTime = addedTime
                };

                twoBigDB.Update("ADMINLOGS/" + log.logsId, log);
            }
        }

        protected void updateResrveButton_Click(object sender, EventArgs e)
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);


            FirebaseResponse allDelivery = twoBigDB.Get("DELIVERY_DETAILS/");
            var all = allDelivery.Body;
            Dictionary<string, Model.Delivery> adminAllDelivery = JsonConvert.DeserializeObject<Dictionary<string, Model.Delivery>>(all);

            // Loop through all the deliverydetails
            foreach (KeyValuePair<string, Model.Delivery> entry in adminAllDelivery)
            {
                if (entry.Value.adminId == adminId)
                {
                    int deliveryID = entry.Value.deliveryId;
                    Session["deliveryID"] = deliveryID;
                }
            }

            int deliveryIdno = (int)Session["deliveryID"];

            // Check if there is an existing delivery object for this admin
            FirebaseResponse resDelivery = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryIdno);
            Delivery delivery = null;
            if (resDelivery.Body != "null")
            {
                delivery = resDelivery.ResultAs<Delivery>();

                var updatedExpress = new Delivery
                {
                    //NOT EDITABLE
                    swapOptions = delivery.swapOptions,
                    vehicle1Fee = delivery.vehicle1Fee,
                    vehicle1Name = delivery.vehicle1Name,
                    vehicle2Fee = delivery.vehicle2Fee,
                    vehicle2Name = delivery.vehicle2Name,
                    vehicle3Fee = delivery.vehicle3Fee,
                    vehicle3Name = delivery.vehicle3Name,
                    vehicle4Fee = delivery.vehicle4Fee,
                    vehicle4Name = delivery.vehicle4Name,
                    orderTypes = delivery.orderTypes,
                    perGallonFee = delivery.perGallonFee,
                    exDeliveryType = delivery.exDeliveryType,
                    stanDeliverytype = delivery.stanDeliverytype,
                    resDeliveryType = delivery.resDeliveryType,

                    //EDITABLE

                    exEstimatedDelivery = delivery.exEstimatedDelivery,
                    expressDistance = delivery.expressDistance,
                    expressID = delivery.expressID,
                    expressProducts = delivery.expressProducts,
                    expressdateAdded = delivery.expressdateAdded,
                    resDeliveryFee = delivery.resDeliveryFee,
                    resDistanceFree = delivery.resDistanceFree,
                    reservationID = delivery.reservationID,
                    reservationdateAdded = delivery.reservationdateAdded,
                    reserveProducts = delivery.reserveProducts,
                    stanDeliveryFee = delivery.stanDeliveryFee,
                    stanDeliveryTime = delivery.stanDeliveryTime,
                    standardDateAdded = delivery.standardDateAdded,
                    standardID = delivery.standardID,
                    standardProducts = delivery.standardProducts,
                    standistance = delivery.standistance
                };




                if (!string.IsNullOrEmpty(updateReserveDistance.Text))
                {
                    delivery.resDistanceFree = updateReserveDistance.Text;
                }
                if (!string.IsNullOrEmpty(updateReserveFee.Text))
                {
                    delivery.resDeliveryFee = updateReserveFee.Text;
                }
                if (updateStandardChkBx.Items.Cast<ListItem>().Any(item => item.Selected))
                {
                    delivery.reserveProducts = string.Join(",", updateReserveChkBx.Items.Cast<ListItem>()
                        .Where(item => item.Selected)
                        .Select(item => item.Value));
                }

                // Save the updated delivery object to the database
                FirebaseResponse res = twoBigDB.Update("DELIVERY_DETAILS/" + deliveryIdno, delivery);
                Response.Write("<script>alert ('Standard Delivery updated successfully');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");


                int logsId = (int)Session["logsId"];
                // Retrieve the existing Users log object from the database
                FirebaseResponse resLog = twoBigDB.Get("ADMINLOGS/" + logsId);
                UsersLogs existingLog = resLog.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = logsId,
                    userFullname = (string)Session["fullname"],
                    userActivity = "UPDATED RESERVATION TYPE",
                    activityTime = addedTime
                };

                twoBigDB.Update("ADMINLOGS/" + log.logsId, log);
            }
        }
    }
}