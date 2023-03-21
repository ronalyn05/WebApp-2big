using System;
using System.Collections.Generic;
using System.Data;
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

namespace WRS2big_Web.Admin
{
    public partial class ProductGallon : System.Web.UI.Page
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
            //METHODS TO DISPLAY THE IDs
            if (!IsPostBack)
            {
                //DisplayID();
                DisplayTable();
            }
        }
        private void DisplayTable()
        {
            string idno = (string)Session["idno"];
           // int adminId = int.Parse(idno);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS/");
            Order obj = response.ResultAs<Order>();
            var json = response.Body;
          //  Dictionary<string, Order> orders = JsonConvert.DeserializeObject<Dictionary<string, Order>>(json);
            Dictionary<string, Order> orders = response.ResultAs<Dictionary<string, Order>>();

            // Create the DataTable to hold the orders
            DataTable ordersTable = new DataTable();
            ordersTable.Columns.Add("ORDER ID");
            ordersTable.Columns.Add("CUSTOMER ID");
            ordersTable.Columns.Add("STORE NAME");
            ordersTable.Columns.Add("PRODUCT NAME");
            ordersTable.Columns.Add("DELIVERY TYPE");
            ordersTable.Columns.Add("ORDER TYPE");
            ordersTable.Columns.Add("GALLON TYPE");
            ordersTable.Columns.Add("PRICE");
            ordersTable.Columns.Add("QUANTITY");
            ordersTable.Columns.Add("TOTAL AMOUNT");
            ordersTable.Columns.Add("RESERVATION DATE");
            ordersTable.Columns.Add("STATUS");

            if (response != null && response.ResultAs<Order>() != null)
            {
                // Loop through the orders and add them to the DataTable
                foreach (KeyValuePair<string, Order> entry in orders)
                {
                    // Only add orders that belong to the specified admin
                    //if (entry.Value.adminID != null && entry.Value.adminID == adminId)
                    //{
                        ordersTable.Rows.Add(entry.Value.orderID, entry.Value.cusId,
                                              entry.Value.order_StoreName, entry.Value.order_ProductName,
                                              entry.Value.order_DeliveryTypeValue, entry.Value.order_OrderTypeValue,
                                              entry.Value.order_SwapGallonTypeValue, entry.Value.order_WaterPrice,
                                              entry.Value.order_Quantity, entry.Value.order_InitialAmount,
                                              entry.Value.order_ReservationDate, entry.Value.order_OrderStatus);
                    //}
                }
            }
            else
            {
                // Handle null response or invalid selected value
                ordersTable.Rows.Add("No orders found", "", "", "", "", "", "", "", "", "", "", "");
            }

            // Bind the DataTable to the GridView
            GridView1.DataSource = ordersTable;
            GridView1.DataBind();
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all orders from the ORDERS table
            //FirebaseResponse response = twoBigDB.Update("ORDERS/" + orderId + "/order_OrderStatus", "Accepted");
            //Order obj = response.ResultAs<Order>();
            //var json = response.Body;
            //  Dictionary<string, Order> orders = JsonConvert.DeserializeObject<Dictionary<string, Order>>(json);
            //Dictionary<string, Order> orders = response.ResultAs<Dictionary<string, Order>>();
            //// Get the selected order ID from the GridView
            //GridViewRow gridViewRow = ((Button)sender).NamingContainer as GridViewRow;
            //if (gridViewRow != null && gridViewRow.RowIndex >= 0 && gridViewRow.RowIndex < GridView1.Rows.Count)
            //{
            //    string orderId = GridView1.DataKeys[gridViewRow.RowIndex].Value.ToString();

            //    // Update the order status to "Accepted" in the Firebase Realtime Database
            //    FirebaseResponse response = twoBigDB.Update("ORDERS/" + orderId + "/order_OrderStatus", "Accepted");

            //    // Display a message to the user
            //    if (response.StatusCode == HttpStatusCode.OK)
            //    {
            //        DisplayTable();
            //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Order accepted successfully');", true);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Error accepting order');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid row index');", true);
            //}
        }


        protected void btnDecline_Click(object sender, EventArgs e)
        {
            // Get the selected order ID from the GridView
            GridViewRow gridViewRow = ((Button)sender).NamingContainer as GridViewRow;
            if (gridViewRow != null && gridViewRow.RowIndex >= 0 && gridViewRow.RowIndex < GridView1.Rows.Count)
            {
                string orderId = GridView1.DataKeys[gridViewRow.RowIndex].Value.ToString();

                // Update the order status to "Declined" in the Firebase Realtime Database
                FirebaseResponse response = twoBigDB.Update("ORDERS/" + orderId + "/order_OrderStatus", "Declined");

                // Display a message to the user
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    DisplayTable();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Order declined successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Error declining order');", true);
                }
            }
        }

      




        //private void DisplayTable()
        //{
        //   string idno = (string)Session["idno"];
        //  //  string adminID = (string)Session["adminID"];
        //   // if(idno == adminId)
        //    FirebaseResponse response = twoBigDB.Get("ORDERS/" + idno );
        //    Order orders = response.ResultAs<Order>();
        //    var data = response.Body;
        //    Dictionary<string, Order> order = JsonConvert.DeserializeObject<Dictionary<string, Order>>(data);

        //    //sa pag create sa table
        //    DataTable employeesTable = new DataTable();
        //    employeesTable.Columns.Add("ORDER ID");
        //    employeesTable.Columns.Add("CUSTOMER ID");
        //    employeesTable.Columns.Add("STORE NAME");
        //    employeesTable.Columns.Add("PRODUCT NAME");
        //    employeesTable.Columns.Add("DELIVERY TYPE");
        //    employeesTable.Columns.Add("ORDER TYPE");
        //    employeesTable.Columns.Add("GALLON TYPE");
        //    employeesTable.Columns.Add("PRICE");
        //    employeesTable.Columns.Add("QUANTITY");
        //    employeesTable.Columns.Add("TOTAL AMOUNT");
        //    employeesTable.Columns.Add("RESERVATION DATE");
        //    employeesTable.Columns.Add("STATUS");

        //    // Loop through the orders and add them to the DataTable
        //    if (response != null && response.ResultAs<Order>() != null)
        //    {
        //        foreach (KeyValuePair<string, Order> entry in order)
        //        {
        //            // Only add orders that belong to the specified admin
        //            if (entry.Value.adminID == idno)
        //            {
        //                employeesTable.Rows.Add(entry.Value.orderID, entry.Value.order_CustomerID,
        //                                    entry.Value.order_StoreName, entry.Value.order_ProductName,
        //                                    entry.Value.order_DeliveryTypeValue, entry.Value.order_OrderTypeValue,
        //                                    entry.Value.order_SwapGallonTypeValue,
        //                                    entry.Value.order_WaterPrice, entry.Value.order_Quantity, entry.Value.order_InitialAmount,
        //                                    entry.Value.order_ReservationDate, entry.Value.order_OrderStatus);
        //            }
        //        }

        //        // Bind DataTable to GridView control
        //        GridView1.DataSource = employeesTable;
        //        GridView1.DataBind();
        //    }

        //}

        //private void DisplayID()
        //{
        //    FirebaseResponse response;
        //    response = twoBigDB.Get("WATER_GALLONS");
        //    Model.WaterGallon obj = response.ResultAs<Model.WaterGallon>();
        //    var json = response.Body;
        //    Dictionary<string, Model.WaterGallon> list = JsonConvert.DeserializeObject<Dictionary<string, Model.WaterGallon>>(json);

        //    foreach (KeyValuePair<string, Model.WaterGallon> entry in list)
        //    {
        //        ListBox1.Items.Add(entry.Value.gallon_id.ToString());
        //    }
        //}

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{

        //    //String searchStr;
        //    //searchStr = LstBoxProductGallon.SelectedValue;
        //    //FirebaseResponse response;
        //    //response = twoBigDB.Get(@"WATER_GALLONS/" + searchStr);
        //    //Model.WaterGallon obj = response.ResultAs<Model.WaterGallon>();

        //    //gal_idno.Text = obj.gallon_id.ToString();
        //    //gal_Type.Text = obj.gallonType.ToString();
        //    //qty.Text = obj.Quantity.ToString();
        //    //deliveryPrice.Text = obj.DeliveryPrice.ToString();
        //    //pickUpPrice.Text = obj.PickUp_Price.ToString();
        //    ////image.Text = obj.Image.ToString();
        //    //d8Added.Text = obj.DateAdded.ToString();
        //}
        ////ADD
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    //try
        //    //{
        //    //    Random rnd = new Random();
        //    //    int idnum = rnd.Next(1, 10000);
        //    //    //insert data
        //    //    var data = new Model.WaterGallon
        //    //    {
        //    //        gallon_id = idnum,
        //    //        gallonType = galType.Text,
        //    //        Quantity = txtQty.Text,
        //    //        DeliveryPrice = txtDeliveryPrice.Text,
        //    //        PickUp_Price = txtPickUp_Price.Text,
        //    //        DateAdded = DateTime.UtcNow
        //    //    };

        //    //    SetResponse response;
        //    //    // GALLONS = tablename, emp_id = key ( PK? )
        //    //    response = twoBigDB.Set("WATER_GALLONS/" + data.gallon_id, data);
        //    //    Model.WaterGallon result = response.ResultAs<Model.WaterGallon>();

        //    //    Response.Write("<script>alert ('Water Gallon with Id number:" + data.gallon_id + " is successfully added!'); location.reload(); window.location.href = '/Admin/ProductGallon.aspx'; </script>");
        //    //    //Response.Write("<script> alert ('New Gallon added successfully'); location.reload(); window.location.href = 'ProductGallon.aspx' </script>");

        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Response.Write("<pre>" + ex.ToString() + "</pre>");


        //    //}
        //}

        ////protected void btnUpdate_Click(object sender, EventArgs e)
        ////{
        ////    Random rnd = new Random();
        ////    int idnum = rnd.Next(1, 10000);
        ////    Model.WaterGallon data = new Model.WaterGallon()
        ////    {
        ////        gallon_id = idnum,
        ////        gallonType = gal_Type.Text,
        ////        Quantity = qty.Text,
        ////        DeliveryPrice = deliveryPrice.Text,
        ////        PickUp_Price = pickUpPrice.Text,
        ////        //Image = FileUpload.Text,
        ////        DateAdded = DateTime.UtcNow
        ////    };
        ////    twoBigDB.Update("WATER_GALLONS/" + data.gallon_id, data);
        ////    //lbResult.Text = "Record successfully updated!";
        ////    var result = twoBigDB.Get("WATER_GALLONS/" + gal_idno.ToString());
        ////    Model.WaterGallon obj = result.ResultAs<Model.WaterGallon>();
        ////    DrdGallonType.Text = obj.gallonType;
        ////    quantity.Text = obj.Quantity;
        ////    DelPrice.Text = obj.DeliveryPrice;
        ////    PickUp_Price.Text = obj.PickUp_Price;
        ////    //image.Text = obj.Image.ToString();
        ////    //d8Added.Text = obj.DateAdded.ToString();
        ////    lblResult.Text = "Record Updated Successfully!";
        ////}

        ////UPDATE
        //protected void btnEdit_Click(object sender, EventArgs e)
        //{
        //    //String deleteStr;
        //    //deleteStr = ListBox1.SelectedValue;

        //    //var data = new Model.WaterGallon();

        //    //data.gallon_id = int.Parse(LabelID.Text);
        //    //data.gallonType = galName.Text;
        //    //data.Quantity = galQuantity.Text;
        //    //data.DeliveryPrice = DeliveryPrice.Text;
        //    //data.PickUp_Price = PckupPrice.Text;
        //    //data.DateAdded = DateTime.UtcNow;

        //    //FirebaseResponse response;
        //    //response = twoBigDB.Update("WATER_GALLONS/" + deleteStr, data);//Update Product Data 

        //    //var result = twoBigDB.Get("WATER_GALLONS/" + deleteStr);//Retrieve Updated Data From WATERPRODUCT TBL
        //    //Model.WaterGallon obj = response.ResultAs<Model.WaterGallon>();//Database Result

        //    //LabelID.Text = obj.gallon_id.ToString();
        //    //galName.Text = obj.gallonType;
        //    //galQuantity.Text = obj.Quantity;
        //    //PckupPrice.Text = obj.PickUp_Price.ToString();
        //    //DeliveryPrice.Text = obj.DeliveryPrice.ToString();
        //    //DateAdded.Text = obj.DateAdded.ToString();

        //    //Response.Write("<script>alert ('Gallon ID : " + deleteStr + " successfully updated!');</script>");

        //}
        ////DELETE
        //protected void DeleteBtn_Click(object sender, EventArgs e)
        //{

        //    //String deleteStr;
        //    //deleteStr = ListBox1.SelectedValue;
        //    //FirebaseResponse response = twoBigDB.Delete("WATER_GALLONS/" + deleteStr);
        //    //Response.Write("<script>alert ('Successfully deleted gallons ID : " + deleteStr + "');location.reload(); window.location.href = '/Admin/ProductGallon.aspx'; </script>");

        //    ////TO DELETE THE ID IN THE LISTBOX AFTER DELETED
        //    //int selected = ListBox1.SelectedIndex;
        //    //if (selected != 1)
        //    //{
        //    //    ListBox1.Items.RemoveAt(selected);
        //    //}

        //}
        ////DISPLAY
        //protected void btnDisplay_Click(object sender, EventArgs e)
        //{
        //    //String slected;
        //    //slected = ListBox1.SelectedValue;
        //    //FirebaseResponse response;
        //    //response = twoBigDB.Get("WATER_GALLONS/" + slected);
        //    //Model.WaterGallon obj = response.ResultAs<Model.WaterGallon>();
        //    //LabelID.Text = obj.gallon_id.ToString();
        //    //galName.Text = obj.gallonType;
        //    //galQuantity.Text = obj.Quantity;
        //    //PckupPrice.Text = obj.PickUp_Price.ToString();
        //    //DeliveryPrice.Text = obj.DeliveryPrice.ToString();
        //    //DateAdded.Text = obj.DateAdded.ToString();
        //}
        //protected void ViewID_Click(object sender, EventArgs e)
        //{
        //    //FirebaseResponse response;
        //    //response = twoBigDB.Get("WATER_GALLONS");
        //    //Model.WaterGallon obj = response.ResultAs<Model.WaterGallon>();
        //    //var json = response.Body;
        //    //Dictionary<string, Model.WaterGallon> list = JsonConvert.DeserializeObject<Dictionary<string, Model.WaterGallon>>(json);

        //    //foreach (KeyValuePair<string, Model.WaterGallon> entry in list)
        //    //{
        //    //    ListBox1.Items.Add(entry.Value.gallon_id.ToString());
        //    //}
        //}
    }
}