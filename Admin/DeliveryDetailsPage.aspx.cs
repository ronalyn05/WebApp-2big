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


namespace WRS2big_Web
{
    public partial class DeliveryDetails : System.Web.UI.Page
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
        }
        protected void btnDeliverydetails_Click(object sender, EventArgs e)
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

            // Check if there is an existing delivery object for this admin
            FirebaseResponse resDelivery = twoBigDB.Get("DELIVERY_DETAILS2/" + adminId);
            Model.DeliveryDetails delivery = null;
            if (resDelivery.Body == "null")
            {

                Response.Write("<script>alert ('You must setup your Delivery Details first before you can set up your Delivery Types');  window.location.href = '/Admin/DeliveryDetailsPage.aspx'; </script>");
                //// If there is no existing delivery object, create a new one
                //Random rnd = new Random();
                //int deliveryId = rnd.Next(1, 10000);
                //delivery = new DeliveryDetails
                //{
                //    adminId = adminId,
                //    deliveryId = adminId,
                //    orderTypes = delivery.orderTypes,
                //    vehicles = delivery.vehicles,

                //    //EXPRESS
                //    expressID = delivery.expressID,
                //    exDeliveryFee = delivery.exDeliveryFee,
                //    exDeliveryType = delivery.exDeliveryType,
                //    exEstimatedDelivery = delivery.exEstimatedDelivery,
                //    exOrderMethod = delivery.exOrderMethod,
                //    expressdateAdded = delivery.expressdateAdded,
                //    expressSwapOptions = delivery.expressSwapOptions,
                //    //STANDARD
                //    standardID = delivery.standardID,
                //    standardDateAdded = delivery.standardDateAdded,
                //    standardSwapOptions = delivery.standardSwapOptions,
                //    stanDeliveryFee = delivery.stanDeliveryFee,
                //    stanDeliveryTime = delivery.stanDeliveryTime,
                //    stanDeliverytype = delivery.stanDeliverytype,
                //    stanOrderMethod = delivery.stanOrderMethod,
                //    standistance = delivery.standistance,
                //    //RESERVATION
                //    reservationID = delivery.reservationID,
                //    resDeliveryFee = delivery.resDeliveryFee,
                //    resDeliveryType = delivery.stanDeliverytype,
                //    resDistanceFree = delivery.resDistanceFree,
                //    reservationdateAdded = delivery.reservationdateAdded,
                //    reserveSwapOptions = delivery.reserveSwapOptions,
                //    resOrderMethod = delivery.resOrderMethod

                //    //orderTypes = GetSelectedValues(orderTypes)

                //};
                //Session["deliveryID"] = deliveryId;


            }
            else
            {
                delivery = resDelivery.ResultAs<Model.DeliveryDetails>();
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
                                Response.Write("<script>alert ('FAILED! Existing STANDARD Delivery type! You already created Standard delivery.');  window.location.href = '/Admin/DeliveryDetailsPage.aspx'; </script>");
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
                                //delivery.stanOrderType = GetSelectedValues(DeliveryType);
                               // delivery.stanOrderMethod = GetSelectedValues(OrderMethod);
                                //delivery.standardSwapOptions = GetSelectedValues(standardSwapOptions);
                            }
                            break;
                        case "Reservation":
                            // If there is an existing reservation delivery object, update it, otherwise create a new one
                            if (delivery.resDeliveryType == "Reservation")
                            {
                                Response.Write("<script>alert ('FAILED! Existing RESERVATION Delivery type! You already created Reservation delivery.');  window.location.href = '/Admin/DeliveryDetailsPage.aspx'; </script>");
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
                                //delivery.resOrderMethod = GetSelectedValues(reserveOrderMethod);
                                //delivery.resOrderType = GetSelectedValues(reserveOrderType);
                                //delivery.reserveSwapOptions = GetSelectedValues(reserveSwap);
                            }
                            break;
                        case "Express":
                            // check If there is an existing express delivery type
                            if (delivery.exDeliveryType == "Express")
                            {
                                Response.Write("<script>alert ('FAILED! Existing EXPRESS Delivery type! You already created Express delivery.');  window.location.href = '/Admin/DeliveryDetailsPage.aspx'; </script>");
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
                                //delivery.exOrderMethod = GetSelectedValues(expressOrderMethod);
                                //delivery.exOrderType = GetSelectedValues(expressOrderType);
                                //delivery.expressSwapOptions = GetSelectedValues(expressSwap);
                            }
                            break;
                    }
                }
            }
            // Save the updated delivery object to the database
            FirebaseResponse res = twoBigDB.Set("DELIVERY_DETAILS2/" + adminId, delivery);
            Response.Write("<script>alert ('You successfully created the Delivery Types you offer to your business');  window.location.href = '/Admin/DeliveryDetailsPage.aspx'; </script>");

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
                userActivity = "ADDED DELIVERY DETAILS",
                //userActivity = UserActivityType.AddedDeliveryDetails,
                dateLogin = existingLog.dateLogin
                //emp_id = existingLog.emp_id,
                //empFullname = existingLog.empFullname,
                //empDateAdded = existingLog.empDateAdded,
                //dateLogin = existingLog.dateLogin,
                //tankId = existingLog.tankId,
                //tankSupplyDateAdded = existingLog.tankSupplyDateAdded,
                //other_productId = existingLog.other_productId,
                //otherProductDateAdded = existingLog.otherProductDateAdded,
                //productRefillId = existingLog.productRefillId,
                //productrefillDateAdded = existingLog.productrefillDateAdded,
                //deliveryDetailsId = delivery.deliveryId,
                //standardAdded = delivery.standardDateAdded,
                //reservationAdded = delivery.reservationdateAdded,
                //expressAdded = delivery.expressdateAdded
            };

            twoBigDB.Update("USERSLOG/" + log.logsId, log);
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
                Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/DeliveryDetailsPage.aspx';" + ex.Message);
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
            Dictionary<string, Model.DeliveryDetails> deliveryList = response.ResultAs<Dictionary<string, Model.DeliveryDetails>>();
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

            if (response != null && response.ResultAs<Model.DeliveryDetails>() != null)
            {
                foreach (var entry in filteredList)
                {
                    if (entry.exDeliveryType == "Express")
                    {
                        //expressTable.Rows.Add(entry.expressID, entry.exEstimatedDelivery, entry.exDeliveryFee, entry.expressSwapOptions,
                        //                  entry.exOrderType, entry.exOrderMethod, entry.expressdateAdded, entry.paymentMethods, entry.adminId);
                        expressTable.Rows.Add(entry.expressID, entry.exEstimatedDelivery, entry.exDeliveryFee,entry.expressdateAdded, entry.paymentMethods, entry.adminId);
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
            Dictionary<string, Model.DeliveryDetails> deliveryList = response.ResultAs<Dictionary<string, Model.DeliveryDetails>>();
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

            if (response != null && response.ResultAs<Model.DeliveryDetails>() != null)
            {
                foreach (var entry in filteredList)
                {
                    if (entry.stanDeliverytype == "Standard")
                    {
                       // standardTable.Rows.Add(entry.standardID, entry.stanDeliveryTime, entry.standistance, entry.stanDeliveryFee,
                       //entry.standardSwapOptions, entry.stanOrderType, entry.stanOrderMethod, entry.standardDateAdded, entry.paymentMethods, entry.adminId);
                        standardTable.Rows.Add(entry.standardID, entry.stanDeliveryTime, entry.standistance, entry.stanDeliveryFee,
                        entry.standardDateAdded, entry.paymentMethods, entry.adminId);
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
            Dictionary<string, Model.DeliveryDetails> deliveryList = response.ResultAs<Dictionary<string, Model.DeliveryDetails>>();
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

            if (response != null && response.ResultAs<Model.DeliveryDetails>() != null)
            {
                foreach (var entry in filteredList)
                {
                    if (entry.resDeliveryType == "Reservation")
                    {
                        //reservationTable.Rows.Add(entry.reservationID, entry.resDistanceFree, entry.resDeliveryFee, entry.reserveSwapOptions,
                        //                      entry.resOrderType, entry.resOrderMethod, entry.reservationdateAdded, entry.paymentMethods, entry.adminId);
                        reservationTable.Rows.Add(entry.reservationID, entry.resDistanceFree, entry.resDeliveryFee, entry.reservationdateAdded, entry.paymentMethods, entry.adminId);
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
                Response.Write("<script>alert ('Payment Methods successsfully added');  window.location.href = '/Admin/DeliveryDetailsPage.aspx'; </script>");
            }
            else
            {
                Response.Write("<script>alert ('FAILED !! Payment Method has been already set-up.');  window.location.href = '/Admin/DeliveryDetailsPage.aspx'; </script>");
            }








        }

        protected void vehicleAdded_Click(object sender, EventArgs e)
        {
            var idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

            // Check if there is an existing delivery object for this admin
            FirebaseResponse resDelivery = twoBigDB.Get("DELIVERY_DETAILS2/" + adminId);
            Model.DeliveryDetails delivery = null;
            if (resDelivery.Body != "null")
            {
                delivery = resDelivery.ResultAs<Model.DeliveryDetails>();

                // If there is no existing delivery object, create a new one
                Random rnd = new Random();
                int deliveryId = rnd.Next(1, 10000);
                delivery = new Model.DeliveryDetails
                {

                    adminId = adminId,
                    deliveryId = adminId,
                    orderTypes = GetSelectedValues(orderTypes),
                    vehicles = GetSelectedValues(vehicles),
                    swapOptions = GetSelectedValues(swapOptionItems),

                    //EXPRESS
                    expressID = delivery.expressID,
                    exDeliveryFee = delivery.exDeliveryFee,
                    exDeliveryType = delivery.exDeliveryType,
                    exEstimatedDelivery = delivery.exEstimatedDelivery,
                    //exOrderMethod = delivery.exOrderMethod,
                    expressdateAdded = delivery.expressdateAdded,
                    //expressSwapOptions = delivery.expressSwapOptions,
                    //STANDARD
                    standardID = delivery.standardID,
                    standardDateAdded = delivery.standardDateAdded,
                    //standardSwapOptions = delivery.standardSwapOptions,
                    stanDeliveryFee = delivery.stanDeliveryFee,
                    stanDeliveryTime = delivery.stanDeliveryTime,
                    stanDeliverytype = delivery.stanDeliverytype,
                    //stanOrderMethod = delivery.stanOrderMethod,
                    standistance = delivery.standistance,
                    //RESERVATION
                    reservationID = delivery.reservationID,
                    resDeliveryFee = delivery.resDeliveryFee,
                    resDeliveryType = delivery.stanDeliverytype,
                    resDistanceFree = delivery.resDistanceFree,
                    reservationdateAdded = delivery.reservationdateAdded,
                    //reserveSwapOptions = delivery.reserveSwapOptions,
                    //resOrderMethod = delivery.resOrderMethod


                };
                Session["deliveryID"] = deliveryId;
                // Save the updated delivery object to the database
                FirebaseResponse res = twoBigDB.Update("DELIVERY_DETAILS2/" + adminId, delivery);
                Response.Write("<script>alert ('You successfully created the Delivery Details you offer to your business');  window.location.href = '/Admin/DeliveryDetailsPage.aspx'; </script>");



            }
            else
            {
                // If there is no existing delivery object, create a new one
                Random rnd = new Random();
                int deliveryId = rnd.Next(1, 10000);
                delivery = new Model.DeliveryDetails
                {
                    adminId = adminId,
                    deliveryId = adminId,
                    orderTypes = GetSelectedValues(orderTypes),
                    vehicles = GetSelectedValues(vehicles),
                    swapOptions = GetSelectedValues(swapOptionItems)

                };
                Session["deliveryID"] = deliveryId;
                // Save the updated delivery object to the database
                FirebaseResponse res = twoBigDB.Set("DELIVERY_DETAILS2/" + adminId, delivery);
                Response.Write("<script>alert ('You successfully created the Delivery Details you offer to your business');  window.location.href = '/Admin/DeliveryDetailsPage.aspx'; </script>");

            }

        }
    }
}