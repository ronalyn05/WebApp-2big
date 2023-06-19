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


            //detailsGridView();

         

            // expressDisplay();
            // reservationDisplay();
            //standardDisplay();

            //getDeliveryTypes();

            //hide buttons in page load
            updateExpress.Visible = false;
            updateReservation.Visible = false;
            updateStandard.Visible = false;

            if (Session["idno"] != null)
            {
                var adminID = (string)Session["idno"];

                FirebaseResponse getDetails = twoBigDB.Get("DELIVERY_DETAILS");
                Dictionary<string, Delivery> details = getDetails.ResultAs<Dictionary<string, Delivery>>();

                bool hasMatchingEntries = false;
                bool hasVehicle1Name = false; // Flag to check if a matching entry with Vehicle1Name is found
                bool haspaymentMethod = false;
                bool completeDeliveryTypes = false;
                

                if (details != null)
                {
                    foreach (KeyValuePair<string, Delivery> entry in details)
                    {
                        if (entry.Value.adminId == int.Parse(adminID))
                        {
                            // At least one matching entry found
                            hasMatchingEntries = true;

                            // Check if the matching entry has a non-null Vehicle1Name value
                            if (!string.IsNullOrEmpty(entry.Value.vehicle1Name))
                            {
                                hasVehicle1Name = true;

                                if (!string.IsNullOrEmpty(entry.Value.paymentMethods))
                                {
                                    haspaymentMethod = true;

                                    if (!string.IsNullOrEmpty(entry.Value.exDeliveryType)&& !string.IsNullOrEmpty(entry.Value.stanDeliverytype) && !string.IsNullOrEmpty(entry.Value.resDeliveryType))
                                    {
                                        completeDeliveryTypes = true;

                                        break;
                                    }
                                    
                                }

                               

                            }
                        }
                    }

                    if (hasMatchingEntries )
                    {
                        // There are matching entries with Vehicle1Name, perform the desired actions
                        deliveryTypesGrid();
                        displayDelDetails();
                        DeliverydetailsModal.Visible = false;

                        //check if admin already added vehicles
                        if (hasVehicle1Name )
                        {
                            vehiclesModal.Visible = false;
                        }
                        else
                        {
                            updateDetails.Visible = false;
                        }
                        //checks if admin already added payment methods
                        if (haspaymentMethod)
                        {

                            paymentMethodsModal.Visible = false;
                        }
                        else
                        {
                            updateDetails.Visible = false;
                        }
                        //checks if admin already added all the delivery Types
                        if (completeDeliveryTypes)
                        {
                            deliveryTypesModal.Visible = false;
                        }
                        else
                        {
                            deliveryTypesModal.Visible = true;
                        }

                        

                    }
                    else
                    {
                        // No matching entries with Vehicle1Name found
                        warning.Text = "No 'Delivery Details' found. Manage the Delivery Details first.";
                        deliveryTypesRow.Visible = false;
                        deliveryDetailsRow.Visible = false;
                        paymentMethodsModal.Visible = false;
                        deliveryTypesModal.Visible = false;
                        vehiclesModal.Visible = false;
                        updateDetails.Visible = false;
                    }
                }
                else
                {
                    // No entries found in "DELIVERY_DETAILS"
                    warning.Text = "No 'Delivery Details' found. Manage the Delivery Details first.";
                    deliveryTypesRow.Visible = false;
                    deliveryDetailsRow.Visible = false;
                    paymentMethodsModal.Visible = false;
                    deliveryTypesModal.Visible = false;
                    vehiclesModal.Visible = false;
                }
            }

        }
        // Helper method to check if a property exists within an object

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

                if (response != null && response.ResultAs<Delivery>() != null)
                {
                    DataTable vehicledDetails = new DataTable();
                    vehicledDetails.Columns.Add("VEHICLE ID");
                    vehicledDetails.Columns.Add("VEHICLE NAME");
                    vehicledDetails.Columns.Add("VEHICLE FEE");
                    vehicledDetails.Columns.Add("Min-Max GALLON QTY");

                    DataTable swapDetails = new DataTable();
                    swapDetails.Columns.Add("SWAP OPTIONS");
                    swapDetails.Columns.Add("Fee");

                    DataTable paymentDetails = new DataTable();
                    paymentDetails.Columns.Add("PAYMENT METHODS");
                    paymentDetails.Columns.Add("Number");

                    DataTable orderTypes = new DataTable();
                    orderTypes.Columns.Add("ORDER TYPES");

                    if (filteredList != null)
                    {
                        foreach (var entry in filteredList)
                        {
                            if (entry.vehicle1Name != null && entry.vehicle1Name != "")
                            {
                                DataRow row1 = vehicledDetails.NewRow();
                                row1["VEHICLE ID"] = entry.vehicle1ID;
                                row1["VEHICLE NAME"] = entry.vehicle1Name;
                                row1["VEHICLE FEE"] = entry.vehicle1Fee;
                                row1["Min-Max GALLON QTY"] = entry.vehicle1MinQty + "-" + entry.vehicle1MaxQty;
                                vehicledDetails.Rows.Add(row1);
                            }
                            if (entry.vehicle2Name != null && entry.vehicle2Name != "")
                            {
                                DataRow row2 = vehicledDetails.NewRow();
                                row2["VEHICLE ID"] = entry.vehicle2ID;
                                row2["VEHICLE NAME"] = entry.vehicle2Name;
                                row2["VEHICLE FEE"] = entry.vehicle2Fee;
                                row2["Min-Max GALLON QTY"] = entry.vehicle2MinQty + "-" + entry.vehicle2MaxQty;
                                vehicledDetails.Rows.Add(row2);
                            }
                            if (entry.vehicle3Name != null && entry.vehicle3Name != "")
                            {
                                DataRow row3 = vehicledDetails.NewRow();
                                row3["VEHICLE ID"] = entry.vehicle3ID;
                                row3["VEHICLE NAME"] = entry.vehicle3Name;
                                row3["VEHICLE FEE"] = entry.vehicle3Fee;
                                row3["Min-Max GALLON QTY"] = entry.vehicle3MinQty + "-" + entry.vehicle3MaxQty;
                                vehicledDetails.Rows.Add(row3);
                            }
                            if (entry.vehicle4Name != null && entry.vehicle4Name != "")
                            {

                                DataRow row4 = vehicledDetails.NewRow();
                                row4["VEHICLE ID"] = entry.vehicle4ID;
                                row4["VEHICLE NAME"] = entry.vehicle4Name;
                                row4["VEHICLE FEE"] = entry.vehicle4Fee;
                                row4["Min-Max GALLON QTY"] = entry.vehicle4MinQty + "-" + entry.vehicle4MaxQty;
                                vehicledDetails.Rows.Add(row4);
                            }

                            //POPULATE DETAILS IN THE UPDATE MODAL
                            if (entry.vehicle1Name != null || entry.vehicle1Fee != null || entry.vehicle1MinQty != null || entry.vehicle1MaxQty != null)
                            {
                                updateV1Name.Attributes["placeholder"] = entry.vehicle1Name;
                                updateV1Fee.Attributes["placeholder"] = entry.vehicle1Fee;
                                updateV1Min.Attributes["placeholder"] = entry.vehicle1MinQty;
                                updateV1Max.Attributes["placeholder"] = entry.vehicle1MaxQty;
                            }
                            if (entry.vehicle2Name != null || entry.vehicle2Fee != null || entry.vehicle2MinQty != null || entry.vehicle2MaxQty != null)
                            {
                                updateV2Name.Attributes["placeholder"] = entry.vehicle2Name;
                                updateV2Fee.Attributes["placeholder"] = entry.vehicle2Fee;
                                updateV2Min.Attributes["placeholder"] = entry.vehicle2MinQty;
                                updateV2Max.Attributes["placeholder"] = entry.vehicle2MaxQty;
                            }
                            if (entry.vehicle3Name != null || entry.vehicle3Fee != null || entry.vehicle3MinQty != null || entry.vehicle3MaxQty != null)
                            {
                                updateV3Name.Attributes["placeholder"] = entry.vehicle3Name;
                                updateV3Fee.Attributes["placeholder"] = entry.vehicle3Fee;
                                updateV3Min.Attributes["placeholder"] = entry.vehicle3MinQty;
                                updateV3Max.Attributes["placeholder"] = entry.vehicle3MaxQty;
                            }
                            if (entry.vehicle4Name != null || entry.vehicle4Fee != null || entry.vehicle4MinQty != null || entry.vehicle4MaxQty != null)
                            {
                                updateV4Name.Attributes["placeholder"] = entry.vehicle4Name;
                                updateV4Fee.Attributes["placeholder"] = entry.vehicle4Fee;
                                updateV4Min.Attributes["placeholder"] = entry.vehicle4MinQty;
                                updateV4Max.Attributes["placeholder"] = entry.vehicle4MaxQty;
                            }

                            string[] swapOptions = entry.swapOptions.Split(',');
                            if (entry.swapOptions != null)
                            {
                                foreach (string swapOption in swapOptions)
                                {
                                    DataRow row = swapDetails.NewRow();
                                    row["SWAP OPTIONS"] = swapOption.Trim();
                                    if (swapOption.Trim() == "Request Pick-up")
                                    {
                                        row["Fee"] = "per Gallon :" + " " + entry.perGallonFee;
                                    }
                                    swapDetails.Rows.Add(row);

                                    updatebyGallonsFee.Attributes["placeholder"] = entry.perGallonFee;

                                    //POPULATE DETAILS INTO UPDATE MODAL
                                    if (swapOption.Trim() == "Swap Without Conditions")
                                    {
                                        updatewithoutCondition.Selected = true;
                                    }
                                    if (swapOption.Trim() == "Swap With Conditions")
                                    {
                                        updatewithCondition.Selected = true;
                                    }
                                    if (swapOption.Trim() == "Gallon Drop-by")
                                    {
                                        updategallonDropby.Selected = true;
                                    }
                                    if (swapOption.Trim() == "Request Pick-up")
                                    {
                                        updatepickupPerGallon.Selected = true;
                                        updateGallonsFee.Visible = true;
                                        updatebyGallonsFee.Attributes["placeholder"] = entry.perGallonFee;
                                    }
                                    else
                                    {
                                        updateGallonsFee.Visible = false;
                                    }
                                }

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
                                        updategcashChecked.Visible = true;
                                        updategcashPayment.Selected = true;
                                        updateGcashNum.Attributes["placeholder"] = entry.gcashNumber;

                                    }
                                    paymentDetails.Rows.Add(row);


                                    if (payment.Trim() == "CashOnDelivery")
                                    {
                                        updateCOD.Selected = true;
                                    }
                                    if (payment.Trim() == "Points")
                                    {
                                        updatePoints.Selected = true;
                                    }
                                }
                            }


                            string[] types = entry.orderTypes.Split(',');
                            foreach (string orders in types)
                            {
                                DataRow row = orderTypes.NewRow();
                                row["ORDER TYPES"] = orders.Trim();
                                orderTypes.Rows.Add(row);

                                if (orders.Trim() == "Delivery")
                                {
                                    updatedelivery.Selected = true;
                                }
                                if (orders.Trim() == "PickUp")
                                {
                                    updatePickup.Selected = true;
                                }
                            }

                            Session["deliveryID"] = entry.deliveryId;

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
                    else
                    {
                        updateDetails.Visible = false;
                    }

                }
                else
                {
                    deliveryDetailsRow.Visible = false;
                }

            }

        }
       
        private void deliveryTypesGrid()
        {
                if (Session["idno"] != null)
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
                        if (filteredList != null)
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
                            deliveryTypesRow.Visible = false;
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
               


        }

        //CREATING DELIVERY DETAILS
        protected void btnDeliverydetails_Click(object sender, EventArgs e)
        {
            if (Session["idno"] != null)
            {
                var idno = (string)Session["idno"];
                int adminId = int.Parse(idno);
                //generate a random number
                Random rnd = new Random();

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
                                    delivery.standardDateAdded = DateTime.Now;
                                    delivery.standardID = standardID;
                                    delivery.stanDeliveryFee = DeliveryFee.Text;
                                    delivery.stanDeliveryTime = standardSchedule;
                                    delivery.standistance = FreeDelivery.Text;
                                    //delivery.stanOrderType = GetSelectedValues(DeliveryType);
                                    delivery.standardProducts = GetSelectedValues(OrderMethod);


                                    int logID = rnd.Next(1, 10000);
                                    // Get the current date and time
                                    DateTime now = DateTime.Now;

                                    // Log user activity
                                    var userLog = new UsersLogs
                                    {
                                        userIdnum = int.Parse(idno),
                                        logsId = logID,
                                        role = "Admin",
                                        userFullname = (string)Session["fullname"],
                                        userActivity = "ADDED STANDARD DELIVERY TYPE",
                                        activityTime = now
                                    };

                                    FirebaseResponse exResponse = twoBigDB.Set("ADMINLOGS/" + userLog.logsId, userLog);//Storing data to the database

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
                                    delivery.reservationdateAdded = DateTime.Now;
                                    delivery.reservationID = reserveID;
                                    delivery.resDeliveryFee = resDelFee.Text;
                                    delivery.resDistanceFree = resFreeDel.Text;
                                    delivery.reserveProducts = GetSelectedValues(reserveOrderMethod);

                                    int logID = rnd.Next(1, 10000);
                                    // Get the current date and time
                                    DateTime now = DateTime.Now;

                                    // Log user activity
                                    var userLog = new UsersLogs
                                    {
                                        userIdnum = int.Parse(idno),
                                        logsId = logID,
                                        role = "Admin",
                                        userFullname = (string)Session["fullname"],
                                        userActivity = "ADDED RESERVATION DELIVERY TYPE",
                                        activityTime = now
                                    };

                                    FirebaseResponse exResponse = twoBigDB.Set("ADMINLOGS/" + userLog.logsId, userLog);//Storing data to the database
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
                                    delivery.expressdateAdded = DateTime.Now;
                                    delivery.expressID = expressID;
                                    delivery.exDeliveryFee = expressdeliveryFee.Text;
                                    delivery.exEstimatedDelivery = estimatedTime.Text;
                                    delivery.expressProducts = GetSelectedValues(expressOrderMethod);

                                    if (Session["role"] != null || Session["idno"] != null)
                                    {
                                       
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
                                            role = "Admin",
                                            userFullname = (string)Session["fullname"],
                                            userActivity = "ADDED EXPRESS DELIVERY",
                                            activityTime = addedTime
                                        };

                                        var reslog = twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                                        UsersLogs logRes = reslog.ResultAs<UsersLogs>();//Database Result
                                    }

                                }
                                break;
                        }
                    }
                }
                // Save the updated delivery object to the database
                FirebaseResponse res = twoBigDB.Set("DELIVERY_DETAILS/" + deliveryIdno, delivery);
                Response.Write("<script>alert ('You successfully created the Delivery Types you offer to your business');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");

            }
            else
            {
                Response.Write("<script>alert ('Session Expired. Please login again');  window.location.href = '/LandingPage/Account.aspx'; </script>");

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
        //DISPLAY EXPRESS DETAILS IN THE MODAL
        private void expressDisplay()
        {
            nullLabel.Text = "";

            if (Session["idno"] != null)
            {
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

            
        }
        //DISPLAT STANDARD DETAILS IN THE MODAL
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

                            // GET THE VALUE FROM THE DATABAS AND SPLIT 
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
        //DISPLAY RESERVATION DETAILS IN THE MODAL
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

                if (Session["role"] != null || Session["idno"] != null)
                {
                    //string role = (string)Session["role"];
                    string adminID = (string)Session["idno"];

                    Random rnd = new Random();
                    int logsID = rnd.Next(1, 10000);

                    // Get the current date and time
                    DateTime addedTime = DateTime.Now;

                    // Log user activity
                    var log = new UsersLogs
                    {
                        userIdnum = int.Parse(adminID),
                        logsId = logsID,
                        role = "Admin",
                        userFullname = (string)Session["fullname"],
                        userActivity = "ADDED PAYMENT METHODS",
                        activityTime = addedTime
                    };

                    var res = twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                    UsersLogs logRes = res.ResultAs<UsersLogs>();//Database Result
                }
                Response.Write("<script>alert ('Payment Methods successsfully added');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");


            }
            else
            {
                Response.Write("<script>alert ('FAILED !! Payment Method has been already set-up.');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");
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




        //UPDATES
        protected void updateExpressbutton_Click(object sender, EventArgs e)
        {
            if (Session["idno"] != null)
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
                       //EDITABLE
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


                        //NOT EDITABLE 
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


                    if (Session["role"] != null || Session["idno"] != null)
                    {
                        //string role = (string)Session["role"];
                        string adminID = (string)Session["idno"];

                        Random rnd = new Random();
                        int logsID = rnd.Next(1, 10000);

                        // Get the current date and time
                        DateTime addedTime = DateTime.Now;

                        // Log user activity
                        var log = new UsersLogs
                        {
                            userIdnum = int.Parse(adminID),
                            logsId = logsID,
                            role = "Admin",
                            userFullname = (string)Session["fullname"],
                            userActivity = "UPDATED EXPRESS DELIVERY",
                            activityTime = addedTime
                        };

                        twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                        UsersLogs logRes = res.ResultAs<UsersLogs>();//Database Result
                    }
                }
            }


        }

        protected void updateStandardbutton_Click(object sender, EventArgs e)
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);
            //INSERT DATA TO TABLE
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

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
                        role = "Admin",
                        userFullname = (string)Session["fullname"],
                        userActivity = "UPDATED STANDARD DELIVERY",
                        activityTime = addedTime
                    };

                    twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                    UsersLogs logRes = res.ResultAs<UsersLogs>();//Database Result
                }
            }
        }

        protected void updateResrveButton_Click(object sender, EventArgs e)
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

            //INSERT DATA TO TABLE
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

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
                        role = "Admin",
                        userFullname = (string)Session["fullname"],
                        userActivity = "UPDATED RESERVATION DELIVERY",
                        activityTime = addedTime
                    };

                    twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                    UsersLogs logRes = res.ResultAs<UsersLogs>();//Database Result
                }
            }
        }

        protected void updateDeliveryDetails_Click(object sender, EventArgs e)
        {
            if (Session["idno"] != null)
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
                        //EDITABLE
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


                        //NOT EDITABLE 
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

                    if (!string.IsNullOrEmpty(updatebyGallonsFee.Text))
                    {
                        delivery.perGallonFee = updatebyGallonsFee.Text;
                    }
                    if (!string.IsNullOrEmpty(updateGcashNum.Text))
                    {
                        delivery.gcashNumber = updateGcashNum.Text;
                    }
                    //VEHICLE 1
                    if (!string.IsNullOrEmpty(updateV1Name.Text))
                    {
                        delivery.vehicle1Name = updateV1Name.Text;
                    }
                    if (!string.IsNullOrEmpty(updateV1Fee.Text))
                    {
                        delivery.vehicle1Fee = updateV1Fee.Text;
                    }
                    if (!string.IsNullOrEmpty(updateV1Min.Text))
                    {
                        delivery.vehicle1MinQty = updateV1Min.Text;
                    }
                    if (!string.IsNullOrEmpty(updateV1Max.Text))
                    {
                        delivery.vehicle1MinQty = updateV1Max.Text;
                    }

                    //VEHICLE 2
                    if (!string.IsNullOrEmpty(updateV2Name.Text))
                    {
                        delivery.vehicle2Name = updateV2Name.Text;
                    }
                    if (!string.IsNullOrEmpty(updateV2Fee.Text))
                    {
                        delivery.vehicle2Fee = updateV2Fee.Text;
                    }
                    if (!string.IsNullOrEmpty(updateV2Min.Text))
                    {
                        delivery.vehicle2MinQty = updateV2Min.Text;
                    }
                    if (!string.IsNullOrEmpty(updateV2Max.Text))
                    {
                        delivery.vehicle2MinQty = updateV2Max.Text;
                    }

                    //VEHICLE 3
                    if (!string.IsNullOrEmpty(updateV3Name.Text))
                    {
                        delivery.vehicle3Name = updateV3Name.Text;
                    }
                    if (!string.IsNullOrEmpty(updateV3Fee.Text))
                    {
                        delivery.vehicle3Fee = updateV3Fee.Text;
                    }
                    if (!string.IsNullOrEmpty(updateV3Min.Text))
                    {
                        delivery.vehicle3MinQty = updateV3Min.Text;
                    }
                    if (!string.IsNullOrEmpty(updateV3Max.Text))
                    {
                        delivery.vehicle3MinQty = updateV3Max.Text;
                    }

                    //VEHICLE 4
                    if (!string.IsNullOrEmpty(updateV4Name.Text))
                    {
                        delivery.vehicle4Name = updateV4Name.Text;
                    }
                    if (!string.IsNullOrEmpty(updateV4Fee.Text))
                    {
                        delivery.vehicle4Fee = updateV4Fee.Text;
                    }
                    if (!string.IsNullOrEmpty(updateV4Min.Text))
                    {
                        delivery.vehicle4MinQty = updateV4Min.Text;
                    }
                    if (!string.IsNullOrEmpty(updateV4Max.Text))
                    {
                        delivery.vehicle4MinQty = updateV4Max.Text;
                    }


                    //FOR THE CHECKBOXES
                    if (updateOrderTypesChck.Items.Cast<ListItem>().Any(item => item.Selected))
                    {
                        delivery.orderTypes = string.Join(",",updateOrderTypesChck.Items.Cast<ListItem>()
                            .Where(item => item.Selected)
                            .Select(item => item.Value));
                    }
                    if (updateSwap.Items.Cast<ListItem>().Any(item => item.Selected))
                    {
                        delivery.swapOptions = string.Join(",", updateSwap.Items.Cast<ListItem>()
                            .Where(item => item.Selected)
                            .Select(item => item.Value));
                    }
                    if (updatePayment.Items.Cast<ListItem>().Any(item => item.Selected))
                    {
                        delivery.paymentMethods = string.Join(",",  updatePayment.Items.Cast<ListItem>()
                            .Where(item => item.Selected)
                            .Select(item => item.Value));
                    }


                    // Save the updated delivery object to the database
                    FirebaseResponse res = twoBigDB.Update("DELIVERY_DETAILS/" + deliveryIdno, delivery);
                    Response.Write("<script>alert ('Delivery details updated successfully');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");

                    if (Session["role"] != null || Session["idno"] != null)
                    {
                        string role = (string)Session["role"];
                        string adminID = (string)Session["idno"];

                        Random rnd = new Random();
                        int logsID = rnd.Next(1, 10000);

                        // Get the current date and time
                        DateTime addedTime = DateTime.Now;

                        // Log user activity
                        var log = new UsersLogs
                        {
                            userIdnum = int.Parse(adminID),
                            logsId = logsID,
                            role = "Admin",
                            userFullname = (string)Session["fullname"],
                            userActivity = "UPDATED DELIVERY DETAILS",
                            activityTime = addedTime
                        };

                        twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                        UsersLogs logRes = res.ResultAs<UsersLogs>();//Database Result
                    }
                    
                }
            }
            else
            {
                Response.Write("<script>alert('Session Expired. Please login again'); window.location.href='/LandingPage/Account.aspx'; </script>");
            }
        }

        protected void removeVehicle_Click(object sender, EventArgs e)
        {
            // Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the vehicle ID from the first cell in the row
            int vehicleID = int.Parse(row.Cells[1].Text);

            if (Session["deliveryID"] != null)
            {
                var deliveryID = (string)Session["deliveryID"];

                // Retrieve the existing order object from the database
                FirebaseResponse response = twoBigDB.Get("DELIVERY_DETAILS/" + deliveryID);
                Model.Delivery delivery = response.ResultAs<Model.Delivery>();

                if (vehicleID.ToString() == delivery.vehicle1ID)
                {
                    delivery.vehicle1stat = "Unavailable";

                    // Get the current date and time
                    DateTime addedTime = DateTime.Now; ;
                    //generate a random number for users logged
                    Random rnd = new Random();
                    int idnum = rnd.Next(1, 10000);

                    //Store the login information in the USERLOG table
                    var profilelog = new UsersLogs
                    {
                        userIdnum = delivery.adminId,
                        logsId = idnum,
                        userFullname = (string)Session["fullname"],
                        userActivity = "REMOVED VEHICLE:" + delivery.vehicle1Name,
                        role = "Admin",
                        activityTime = addedTime
                    };

                    //Storing the  info
                    response = twoBigDB.Set("ADMINLOGS/" + profilelog.logsId, profilelog);//Storing data to the database

                    response = twoBigDB.Update("DELIVERY_DETAILS/" + deliveryID, delivery);
                    Response.Write("<script>alert ('You successfully removed a vehicle');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");


                }
                else if (vehicleID.ToString() == delivery.vehicle2ID)
                {
                    delivery.vehicle2stat = "Unavailable";
                    // Get the current date and time
                    DateTime addedTime = DateTime.Now; ;
                    //generate a random number for users logged
                    Random rnd = new Random();
                    int idnum = rnd.Next(1, 10000);

                    //Store the login information in the USERLOG table
                    var profilelog = new UsersLogs
                    {
                        userIdnum = delivery.adminId,
                        logsId = idnum,
                        userFullname = (string)Session["fullname"],
                        userActivity = "REMOVED VEHICLE:" + delivery.vehicle2Name,
                        role = "Admin",
                        activityTime = addedTime
                    };

                    //Storing the  info
                    response = twoBigDB.Set("ADMINLOGS/" + profilelog.logsId, profilelog);//Storing data to the database

                    response = twoBigDB.Update("DELIVERY_DETAILS/" + deliveryID, delivery);
                    Response.Write("<script>alert ('You successfully removed a vehicle');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");

                }
                else if (vehicleID.ToString() == delivery.vehicle3ID)
                {
                    delivery.vehicle3stat = "Unavailable";

                    // Get the current date and time
                    DateTime addedTime = DateTime.Now; ;
                    //generate a random number for users logged
                    Random rnd = new Random();
                    int idnum = rnd.Next(1, 10000);

                    //Store the login information in the USERLOG table
                    var profilelog = new UsersLogs
                    {
                        userIdnum = delivery.adminId,
                        logsId = idnum,
                        userFullname = (string)Session["fullname"],
                        userActivity = "REMOVED VEHICLE:" + delivery.vehicle3Name,
                        role = "Admin",
                        activityTime = addedTime
                    };

                    //Storing the  info
                    response = twoBigDB.Set("ADMINLOGS/" + profilelog.logsId, profilelog);//Storing data to the database

                    response = twoBigDB.Update("DELIVERY_DETAILS/" + deliveryID, delivery);
                    Response.Write("<script>alert ('You successfully removed a vehicle');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");

                }
                else if (vehicleID.ToString() == delivery.vehicle4ID)
                {
                    delivery.vehicle4stat = "Unavailable";

                    // Get the current date and time
                    DateTime addedTime = DateTime.Now; ;
                    //generate a random number for users logged
                    Random rnd = new Random();
                    int idnum = rnd.Next(1, 10000);

                    //Store the login information in the USERLOG table
                    var profilelog = new UsersLogs
                    {
                        userIdnum = delivery.adminId,
                        logsId = idnum,
                        userFullname = (string)Session["fullname"],
                        userActivity = "REMOVED VEHICLE:" + delivery.vehicle4Name,
                        role = "Admin",
                        activityTime = addedTime
                    };

                    //Storing the  info
                    response = twoBigDB.Set("ADMINLOGS/" + profilelog.logsId, profilelog);//Storing data to the database

                    response = twoBigDB.Update("DELIVERY_DETAILS/" + deliveryID, delivery);
                    Response.Write("<script>alert ('You successfully removed a vehicle');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");

                }
            }


        }

        protected void addVehicles_Click(object sender, EventArgs e)
        {
            if (Session["idno"] != null)
            {
                var idno = (string)Session["idno"];
                int adminId = int.Parse(idno);
                //generate a random number
                Random rnd = new Random();
                int vehicleID = new Random().Next(1, 10000);

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
                if (resDelivery.Body != null)
                {
                   

                    delivery = resDelivery.ResultAs<Delivery>();

                    delivery.vehicle1Name = vehicle1Name.Text;
                    delivery.vehicle2Name = vehicle2Name.Text;
                    delivery.vehicle3Name = vehicle3Name.Text;
                    delivery.vehicle4Name = vehicle4Name.Text;

                    delivery.vehicle1Fee = vehicle1Fee.Text;
                    delivery.vehicle2Fee = vehicle2Fee.Text;
                    delivery.vehicle3Fee = vehicle3Fee.Text;
                    delivery.vehicle4Fee = vehicle4Fee.Text;

                    delivery.vehicle1MinQty = vehicle1MinQty.Text;
                    delivery.vehicle2MinQty = vehicle2MinQty.Text;
                    delivery.vehicle3MinQty = vehicle3MinQty.Text;
                    delivery.vehicle4MinQty = vehicle4MinQty.Text;

                    delivery.vehicle1MaxQty = vehicle1MaxQty.Text;
                    delivery.vehicle2MaxQty = vehicle2MaxQty.Text;
                    delivery.vehicle3MaxQty = vehicle3MaxQty.Text;
                    delivery.vehicle4MaxQty = vehicle4MaxQty.Text;

                    delivery.vehicle1ID = vehicleID.ToString();
                    delivery.vehicle2ID = vehicleID.ToString() + 1;
                    delivery.vehicle3ID = vehicleID.ToString() + 2;
                    delivery.vehicle4ID = vehicleID.ToString() + 3;


                    int logsID = rnd.Next(1, 10000);

                    // Get the current date and time
                    DateTime addedTime = DateTime.Now;

                    // Log user activity
                    var log = new UsersLogs
                    {
                        userIdnum = int.Parse(idno),
                        logsId = logsID,
                        role = "Admin",
                        userFullname = (string)Session["fullname"],
                        userActivity = "ADDED VEHICLES",
                        activityTime = addedTime
                    };

                    var reslog = twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                    UsersLogs logRes = reslog.ResultAs<UsersLogs>();//Database Result

                }
                else
                {
                    Response.Write("<script>alert ('You must setup your Delivery Details first before you can set up your Delivery Types');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");

                }

                
                // Save the updated delivery object to the database
                FirebaseResponse res = twoBigDB.Set("DELIVERY_DETAILS/" + deliveryIdno, delivery);
                Response.Write("<script>alert ('You successfully added the Vehicles you can use for your delivery');  window.location.href = '/Admin/Deliverydetails.aspx'; </script>");

            }
            else
            {
                Response.Write("<script>alert ('Session Expired. Please login again');  window.location.href = '/LandingPage/Account.aspx'; </script>");

            }
        }

        protected void deliveryDetailsAdded_Click(object sender, EventArgs e)
        {
            if (Session["idno"] != null)
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
                        perGallonFee = perGallonFee.Text

                    };

                    SetResponse response;
                    response = twoBigDB.Set("DELIVERY_DETAILS/" + idnum, delivery);
                    Delivery result = response.ResultAs<Delivery>();
                    //save the deliveryID in the session
                    Session["deliveryID"] = idnum;

                    if (Session["role"] != null || Session["idno"] != null)
                    {
                        
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
                            role = "Admin",
                            userFullname = (string)Session["fullname"],
                            userActivity = "CREATED DELIVERY DETAILS",
                            activityTime = addedTime
                        };

                        var res = twoBigDB.Set("ADMINLOGS/" + log.logsId, log);
                        UsersLogs logRes = res.ResultAs<UsersLogs>();//Database Result
                    }
                    Response.Write("<script>alert ('Delivery details created Successfully'); window.location.href = '/Admin/Deliverydetails.aspx'; </script>");


                }
                catch
                {
                    Response.Write("<script>alert ('Something went wrong'); window.location.href = '/Admin/Deliverydetails.aspx'; </script>");

                }
            }
        }
    }
}