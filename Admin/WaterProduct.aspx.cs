using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Firebase.Storage;
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
            if (!IsPostBack)
            {
                //  DisplayID();
                //GetTankSupplyForToday();
            }
            string idno = (string)Session["idno"];
            string tankId = (string)Session["tankId"];

//            Retrieve tank supply data
            FirebaseResponse response;
            response = twoBigDB.Get("TANKSUPPLY/" + idno);
            TankSupply tankSupply = response.ResultAs<TankSupply>();

            //    response = twoBigDB.Get("ORDERS/" + idno);
            //    Order orders = response.ResultAs<Order>();




            //    if (tankSupply != null)
            //    {
            //        // Debug code: check if the expected values are present
            //        Debug.WriteLine("tankUnit = " + tankSupply.tankUnit);
            //        Debug.WriteLine("tankVolume = " + tankSupply.tankVolume);


            //        Session["tankUnit"] = tankSupply.tankUnit;
            //        Session["tankVolume"] = tankSupply.tankVolume;

            //    }
            string tankUnit = (string)Session["tankUnit"];
            string tankVolume = (string)Session["tankVolume"];
            //    double tankBalance = (double)Session["tankBalance"];


            if (tankUnit != null && tankVolume != null)
            {
                lbltankSupply.Text = "Your tank supply added for today is: " + tankSupply.tankUnit.ToString() + " " + tankSupply.tankVolume.ToString();

                //       // lblremainingSupply.Text = ""
                //        //// Assume you have a method GetTankSupplyForToday() that retrieves the tank supply data for the current day from the database
                //        //string tankSupplyForToday = GetTankSupplyForToday(); 
                //        //if (tankSupplyForToday != null)
                //        //{
                //        //lbltankSupply.Text = tankUnit + " " + tankVolume + " added today: " + tankSupplyForToday;
                //        //}
                //        //else
                //        //{
                //        //lbltankSupply.Text = "No data available for today";
                //        //}
                    }
                else
                {
                    lbltankSupply.Text = "No data available for today! You first need to add your water tank supply for today!";
                }

            }

        //keeps track of whether the supply data has been added
        //private bool supplyDataAdded = false;

        //STORING DATA TO TANKSUPPLY
        //protected void btnAddSupply_Click(object sender, EventArgs e)
        //{
        //    if (supplyDataAdded)
        //    {
        //        Response.Write("<script>alert('Supply data has already been added!'); </script>");
        //        return;
        //    }

        //    string idno = (string)Session["idno"];
        //    int adminId = int.Parse(idno);

        //    try
        //    {
        //        // INSERT DATA TO TABLE = otherPRODUCT
        //        Random rnd = new Random();
        //        int idnum = rnd.Next(1, 10000);

        //        var data = new TankSupply
        //        {
        //            adminId = adminId,
        //            tankId = idnum,
        //            tankUnit = drdTankUnit.SelectedValue,
        //            tankVolume = tankSize.Text,
        //            dateAdded = DateTime.UtcNow
        //        };

        //        SetResponse response;
        //        //USER = tablename, Idno = key(PK ? )
        //        // response = twoBigDB.Set("ADMIN/" + idno + "/TANKSUPPLY/" + data.productId, data);
        //        response = twoBigDB.Set("TANKSUPPLY/" + data.tankId, data);

        //        // Set supplyDataAdded to true to prevent further additions
        //        supplyDataAdded = true;

        //        // Disable the button to prevent further additions
        //        AddTanksupply.Enabled = false;
        //        AddTanksupply.Text = "Supply data added";

        //        var supplyDataResponse = twoBigDB.Get("TANKSUPPLY/" + data.tankId);
        //        TankSupply supplyData = supplyDataResponse.ResultAs<TankSupply>();

        //        //display the tank supply result here
        //        lbltankSupply.Text = supplyData.tankVolume.ToString() + ' ' + supplyData.tankUnit.ToString();
        //      //  lblremainingSupply.Text = 

        //        Response.Write("<script>alert ('Tank supply for today with id number: " + data.tankId + " is successfully added!'); location.reload(); window.location.href = '/Admin/WaterProduct.aspx'; </script>");
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/WaterProduct.aspx';" + ex.Message);
        //    }
        //}

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
                //USER = tablename, Idno = key(PK ? )
                // response = twoBigDB.Set("ADMIN/" + idno + "/TANKSUPPLY/" + data.productId, data);
                response = twoBigDB.Set("TANKSUPPLY/" + data.tankId, data);
                TankSupply result = response.ResultAs<TankSupply>();

                //Get the data in the database
                var res = twoBigDB.Get("TANKSUPPLY/" + idno);
                TankSupply obj = res.ResultAs<TankSupply>();

              
                    //display the tank supply result here
                    lbltankSupply.Text = obj.tankVolume.ToString() + ' ' + obj.tankUnit.ToString();

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
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

            try
            {
                // INSERT DATA TO TABLE = DELIVERY_DETAILS
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                // Loop through the items in the CheckBoxList to build the deliveryType string
                string deliveryType = "";
                foreach (ListItem item in chkdevType.Items)
                {
                    if (item.Selected)
                    {
                        deliveryType += item.Value + ", ";
                    }
                }
                deliveryType = deliveryType.TrimEnd(' ', ',');

                // Loop through the items in the CheckBoxList to build the orderType string
                string orderType = "";
                foreach (ListItem item in chkOrderType.Items)
                {
                    if (item.Selected)
                    {
                        orderType += item.Value + ", ";
                    }
                }
                orderType = orderType.TrimEnd(' ', ',');

                // Loop through the items in the CheckBoxList to build the orderMethod string
                string orderMethod = "";
                foreach (ListItem item in chkOrderMethod.Items)
                {
                    if (item.Selected)
                    {
                        orderMethod += item.Value + ", ";
                    }
                }
                orderMethod = orderMethod.TrimEnd(' ', ',');

                var data = new DeliveryDetails
                {
                    deliveryId = idnum,
                    adminId = adminId,
                    deliveryType = deliveryType,
                    expressEstimatedTime = estimatedTime.Text,
                    deliveryFee = deliveryFee.Text,
                    orderType = orderType,
                    orderMethod = orderMethod,
                    deliveryDistance = distanceDelivery.Text,
                    dateAdded = DateTime.UtcNow
                };

                SetResponse response;
                response = twoBigDB.Set("DELIVERY_DETAILS/" + data.deliveryId, data);
                DeliveryDetails result = response.ResultAs<DeliveryDetails>();
                Response.Write("<script>alert ('Delivery details  with Id number: " + data.deliveryId + " is successfully added!'); location.reload(); window.location.href = '/Admin/WaterProduct.aspx'; </script>");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
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