using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

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
            //METHODS TO DISPLAY THE WATER AND GALLON TYPES FROM FIREBASE
            if (!IsPostBack)
            {
                DisplayID();
            }

            //Retrieve Data
            //FirebaseResponse response = twoBigDB.Get("WATER_GALLONS");
            //Model.WaterGallon obj = response.ResultAs<Model.WaterGallon>();
            //var json = response.Body;
            //Dictionary<string, Model.WaterGallon> list = JsonConvert.DeserializeObject<Dictionary<string, Model.WaterGallon>>(json);

            //foreach (KeyValuePair<string, Model.WaterGallon> item in list)
            //{
            //    //LstBoxProductGallon.Items.Add(item.Value.gallon_id.ToString());
            //    LstBoxProductGallon.Items.Add(item.Value.gallon_id.ToString()
            //     + " " + item.Value.gallonType.ToString()
            //     + " " + item.Value.Quantity.ToString()
            //     + " " + item.Value.DeliveryPrice.ToString()
            //     + " " + item.Value.PickUp_Price.ToString()
            //    // + " " + item.Value.Image.ToString()
            //     + " " + item.Value.DateAdded.ToString());
            //}
            // lbl_result.Text = "Choose employee id in a listbox you want to view and click the button view details to view records' details....";
        }

        private void DisplayID()
        {
            FirebaseResponse response;
            response = twoBigDB.Get("WATER_GALLONS");
            Model.WaterGallon obj = response.ResultAs<Model.WaterGallon>();
            var json = response.Body;
            Dictionary<string, Model.WaterGallon> list = JsonConvert.DeserializeObject<Dictionary<string, Model.WaterGallon>>(json);

            foreach (KeyValuePair<string, Model.WaterGallon> entry in list)
            {
                ListBox1.Items.Add(entry.Value.gallon_id.ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
                //String searchStr;
                //searchStr = LstBoxProductGallon.SelectedValue;
                //FirebaseResponse response;
                //response = twoBigDB.Get(@"WATER_GALLONS/" + searchStr);
                //Model.WaterGallon obj = response.ResultAs<Model.WaterGallon>();

                //gal_idno.Text = obj.gallon_id.ToString();
                //gal_Type.Text = obj.gallonType.ToString();
                //qty.Text = obj.Quantity.ToString();
                //deliveryPrice.Text = obj.DeliveryPrice.ToString();
                //pickUpPrice.Text = obj.PickUp_Price.ToString();
                ////image.Text = obj.Image.ToString();
                //d8Added.Text = obj.DateAdded.ToString();
        }
        //ADD
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);
                //insert data
                var data = new Model.WaterGallon
                {
                    gallon_id = idnum,
                    gallonType = galType.Text,
                    Quantity = txtQty.Text,
                    DeliveryPrice = txtDeliveryPrice.Text,
                    PickUp_Price = txtPickUp_Price.Text,
                    DateAdded = DateTime.UtcNow
                };

                SetResponse response;
                // GALLONS = tablename, emp_id = key ( PK? )
                response = twoBigDB.Set("WATER_GALLONS/" + data.gallon_id, data);
                Model.WaterGallon result = response.ResultAs<Model.WaterGallon>();

                Response.Write("<script>alert ('Water Gallon with Id number:" + data.gallon_id + " is successfully added!'); location.reload(); window.location.href = '/Admin/ProductGallon.aspx'; </script>");
                //Response.Write("<script> alert ('New Gallon added successfully'); location.reload(); window.location.href = 'ProductGallon.aspx' </script>");

            }
            catch (Exception ex)
            {
                Response.Write("<pre>" + ex.ToString() + "</pre>");
               

            }
        }

        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    Random rnd = new Random();
        //    int idnum = rnd.Next(1, 10000);
        //    Model.WaterGallon data = new Model.WaterGallon()
        //    {
        //        gallon_id = idnum,
        //        gallonType = gal_Type.Text,
        //        Quantity = qty.Text,
        //        DeliveryPrice = deliveryPrice.Text,
        //        PickUp_Price = pickUpPrice.Text,
        //        //Image = FileUpload.Text,
        //        DateAdded = DateTime.UtcNow
        //    };
        //    twoBigDB.Update("WATER_GALLONS/" + data.gallon_id, data);
        //    //lbResult.Text = "Record successfully updated!";
        //    var result = twoBigDB.Get("WATER_GALLONS/" + gal_idno.ToString());
        //    Model.WaterGallon obj = result.ResultAs<Model.WaterGallon>();
        //    DrdGallonType.Text = obj.gallonType;
        //    quantity.Text = obj.Quantity;
        //    DelPrice.Text = obj.DeliveryPrice;
        //    PickUp_Price.Text = obj.PickUp_Price;
        //    //image.Text = obj.Image.ToString();
        //    //d8Added.Text = obj.DateAdded.ToString();
        //    lblResult.Text = "Record Updated Successfully!";
        //}

        //UPDATE
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            String deleteStr;
            deleteStr = ListBox1.SelectedValue;

            var data = new Model.WaterGallon();

            data.gallon_id = int.Parse(LabelID.Text);
            data.gallonType = galName.Text;
            data.Quantity = galQuantity.Text;
            data.DeliveryPrice = DeliveryPrice.Text;
            data.PickUp_Price = PckupPrice.Text;
            data.DateAdded = DateTime.UtcNow;

            FirebaseResponse response;
            response = twoBigDB.Update("WATER_GALLONS/" + deleteStr, data);//Update Product Data 

            var result = twoBigDB.Get("WATER_GALLONS/" + deleteStr);//Retrieve Updated Data From WATERPRODUCT TBL
            Model.WaterGallon obj = response.ResultAs<Model.WaterGallon>();//Database Result

            LabelID.Text = obj.gallon_id.ToString();
            galName.Text = obj.gallonType;
            galQuantity.Text = obj.Quantity;
            PckupPrice.Text = obj.PickUp_Price.ToString();
            DeliveryPrice.Text = obj.DeliveryPrice.ToString();
            DateAdded.Text = obj.DateAdded.ToString();

            Response.Write("<script>alert ('Gallon ID : " + deleteStr + " successfully updated!');</script>");

        }
        //DELETE
        protected void DeleteBtn_Click(object sender, EventArgs e)
        {

            String deleteStr;
            deleteStr = ListBox1.SelectedValue;
            FirebaseResponse response = twoBigDB.Delete("WATER_GALLONS/" + deleteStr);
            Response.Write("<script>alert ('Successfully deleted gallons ID : " + deleteStr + "');location.reload(); window.location.href = '/Admin/ProductGallon.aspx'; </script>");

            //TO DELETE THE ID IN THE LISTBOX AFTER DELETED
            int selected = ListBox1.SelectedIndex;
            if(selected != 1)
            {
                ListBox1.Items.RemoveAt(selected);
            }

        }
        //DISPLAY
        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            String slected;
            slected = ListBox1.SelectedValue;
            FirebaseResponse response;
            response = twoBigDB.Get("WATER_GALLONS/" + slected);
            Model.WaterGallon obj = response.ResultAs<Model.WaterGallon>();
            LabelID.Text = obj.gallon_id.ToString();
            galName.Text = obj.gallonType;
            galQuantity.Text = obj.Quantity;
            PckupPrice.Text = obj.PickUp_Price.ToString();
            DeliveryPrice.Text = obj.DeliveryPrice.ToString();
            DateAdded.Text = obj.DateAdded.ToString();
        }
        protected void ViewID_Click(object sender, EventArgs e)
        {
            //FirebaseResponse response;
            //response = twoBigDB.Get("WATER_GALLONS");
            //Model.WaterGallon obj = response.ResultAs<Model.WaterGallon>();
            //var json = response.Body;
            //Dictionary<string, Model.WaterGallon> list = JsonConvert.DeserializeObject<Dictionary<string, Model.WaterGallon>>(json);

            //foreach (KeyValuePair<string, Model.WaterGallon> entry in list)
            //{
            //    ListBox1.Items.Add(entry.Value.gallon_id.ToString());
            //}
        }
    }
}