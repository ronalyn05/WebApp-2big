//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using FireSharp;
//using FireSharp.Config;
//using FireSharp.Interfaces;
//using FireSharp.Response;
//using Newtonsoft.Json;
//using System.Threading.Tasks;
//using System.Data;

//namespace WRS2big_Web.Admin
//{
//    public partial class WaterRefill : System.Web.UI.Page
//    {
//        //Initialize the FirebaseClient with the database URL and secret key.
//        IFirebaseConfig config = new FirebaseConfig
//        {
//            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
//            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"

//        };
//        IFirebaseClient twoBigDB;

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            //connection to database 
//            twoBigDB = new FireSharp.FirebaseClient(config);
//            //METHODS TO DISPLAY THE WATER AND GALLON TYPES FROM FIREBASE
//            if (!IsPostBack )
//            {
//                ShowGallonList();
//                DisplayWater();
//                DisplayID();
//            }
//            //LabelID.Text = "";
//            //GallonName.Text
//            //= "";
//            //WaterTypeDDL.Text = "";
//            //RefillName.Text = "";
//            //PricePckup.Text = "";
//            //DeliveryPrice.Text = "";
//            //DateAdded.Text = "";
//        }


//        protected void SaveBtn_Click(object sender, EventArgs e)
//        {
            
//            //TO ADD NEW REFILL SERVICE
//            Random rnd = new Random();
//                int idnum = rnd.Next(1, 10000);
//                //insert data
//                var data = new Model.RefillProduct
//                {
//                    refill_id = idnum,
//                    gallonType = DDLGal.Text,
//                    waterType = DDLWater.Text,
//                    refillName = srviceName.Text,
//                    DeliveryPrice = dliveryprice.Text,
//                    PickUp_Price = pckupPrice.Text,
//                    DateAdded = DateTime.UtcNow
//                };

//                SetResponse response;
//                // GALLONS = tablename, emp_id = key ( PK? )
//                response = twoBigDB.Set("PRODUCT_REFILL/" + data.refill_id, data);
//                Model.RefillProduct result = response.ResultAs<Model.RefillProduct>();

//                Response.Write("<script>alert ('Refill Service with ID number:" + data.refill_id + " is successfully added!'); location.reload(); window.location.href = '/Admin/WaterRefill.aspx'; </script>");
//                //Response.Write("<script> alert ('New Gallon added successfully'); location.reload(); window.location.href = 'ProductGallon.aspx' </script>");

          
//        }
//        protected void btnDisplay_Click(object sender, EventArgs e)
//        {
//            String slected;
//            slected = ListBox1.SelectedValue;
//            FirebaseResponse response;
//            response = twoBigDB.Get("PRODUCT_REFILL/" + slected);
//            Model.RefillProduct obj = response.ResultAs<Model.RefillProduct>();
//            LabelID.Text = obj.refill_id.ToString();
//            GallonName.Text = obj.gallonType;
//            WaterTypeDDL.Text = obj.waterType;
//            RefillName.Text = obj.refillName.ToString();
//            PricePckup.Text = obj.PickUp_Price.ToString();
//            DeliveryPrice.Text = obj.DeliveryPrice.ToString();
//            DateAdded.Text = obj.DateAdded.ToString();
//            WaterProducts();
//            GallonProducts();

//        }


//        protected void btnEdit_Click(object sender, EventArgs e)
//        {
//            String deleteStr;
//            deleteStr = ListBox1.SelectedValue;

//            var data = new Model.RefillProduct();

//            data.refill_id = int.Parse(LabelID.Text);
//            data.gallonType = GallonName.Text;
//            data.waterType = WaterTypeDDL.Text;
//            data.refillName = RefillName.Text;
//            data.DeliveryPrice = DeliveryPrice.Text;
//            data.PickUp_Price = PricePckup.Text;
//            data.DateAdded = DateTime.UtcNow;

//            FirebaseResponse response;
//            response = twoBigDB.Update("PRODUCT_REFILL/" + deleteStr, data);//Update Product Data 

//            var result = twoBigDB.Get("PRODUCT_REFILL/" + deleteStr);//Retrieve Updated Data From WATERPRODUCT TBL
//            Model.RefillProduct obj = response.ResultAs<Model.RefillProduct>();//Database Result

//            LabelID.Text = obj.refill_id.ToString();
//            GallonName.Text = obj.gallonType;
//            WaterTypeDDL.Text = obj.waterType;
//            RefillName.Text = obj.refillName;
//            PricePckup.Text = obj.PickUp_Price.ToString();
//            DeliveryPrice.Text = obj.DeliveryPrice.ToString();
//            DateAdded.Text = obj.DateAdded.ToString();

//            Response.Write("<script>alert ('Product Refill ID : " + deleteStr + " successfully updated!');</script>");

//        }
//        protected void DeleteBtn_Click(object sender, EventArgs e)
//        {

//            String deleteStr;
//            deleteStr = ListBox1.SelectedValue;
//            FirebaseResponse response = twoBigDB.Delete("PRODUCT_REFILL/" + deleteStr);
//            Response.Write("<script>alert ('Successfully deleted Product Refill ID : " + deleteStr + "');location.reload(); window.location.href = '/Admin/WaterRefill.aspx'; </script>");

//            //TO DELETE THE ID IN THE LISTBOX AFTER BEING DELETED
//            int selected = ListBox1.SelectedIndex;
//            if (selected != 1)
//            {
//                ListBox1.Items.RemoveAt(selected);
              

//            }


//        }

//        private void Clear()
//        {
//            //clears the textboxes
//            LabelID.Text = "";
//            GallonName.Text = "";
//            WaterTypeDDL.Text = "";
//            RefillName.Text = "";
//            PricePckup.Text = "";
//            DeliveryPrice.Text = "";
//            DateAdded.Text = "";
//        }

//        protected void Button1_Click(object sender, EventArgs e)
//        {

//        }
//        //DISPLAY ALL THE ID's using PAGE LOAD
//        private void DisplayID()
//        {
//            FirebaseResponse response;
//            response = twoBigDB.Get("PRODUCT_REFILL");
//            Model.RefillProduct obj = response.ResultAs<Model.RefillProduct>();
//            var json = response.Body;
//            Dictionary<string, Model.RefillProduct> list = JsonConvert.DeserializeObject<Dictionary<string, Model.RefillProduct>>(json);

//            foreach (KeyValuePair<string, Model.RefillProduct> entry in list)
//            {
//                ListBox1.Items.Add(entry.Value.refill_id.ToString());
//            }
//        }

//        private void DisplayWater()
//        {
//            //TO LOAD THE WATER AND GALLON TYPES 
//            // FOR THE WATER
//            FirebaseResponse res;
//            res = twoBigDB.Get("WATERPRODUCT");
//            Model.WaterProduct water = res.ResultAs<Model.WaterProduct>();
//            var item = res.Body;
//            Dictionary<string, Model.WaterProduct> gallons = JsonConvert.DeserializeObject<Dictionary<string, Model.WaterProduct>>(item);

//            foreach (KeyValuePair<string, Model.WaterProduct> entry in gallons)
//            {
//                DDLWater.Items.Add(entry.Value.waterType.ToString());
//                //WaterTypeDDL.Items.Add(entry.Value.waterType.ToString());
//            }
//        }

//        private void ShowGallonList()
//        {
//            //TO LOAD THE WATER AND GALLON TYPES 
//            // FOR THE GALLONS
//            FirebaseResponse client;
//            client = twoBigDB.Get("WATER_GALLONS");
//            Model.WaterGallon objt = client.ResultAs<Model.WaterGallon>();
//            var json = client.Body;
//            Dictionary<string, Model.WaterGallon> list = JsonConvert.DeserializeObject<Dictionary<string, Model.WaterGallon>>(json);

//            //DISPLAY THE GALLONS FROM FIREBASE TO THE DROPDOWN LIST
//            foreach (KeyValuePair<string, Model.WaterGallon> entry in list)
//            {
//                DDLGal.Items.Add(entry.Value.gallonType.ToString());
//                //GallonName.Items.Add(entry.Value.gallonType.ToString());
//            }
//        }

//        private void GallonProducts()
//        {
//            //TO LOAD THE WATER AND GALLON TYPES 
//            // FOR THE GALLONS
//            FirebaseResponse client;
//            client = twoBigDB.Get("WATER_GALLONS");
//            Model.WaterGallon objt = client.ResultAs<Model.WaterGallon>();
//            var json = client.Body;
//            Dictionary<string, Model.WaterGallon> list = JsonConvert.DeserializeObject<Dictionary<string, Model.WaterGallon>>(json);

//            //DISPLAY THE GALLONS FROM FIREBASE TO THE DROPDOWN LIST
//            foreach (KeyValuePair<string, Model.WaterGallon> entry in list)
//            {
//                GallonName.Items.Add(entry.Value.gallonType.ToString());
//            }
//        }

//        private void WaterProducts()
//        {
//            //TO LOAD THE WATER AND GALLON TYPES 
//            // FOR THE WATER
//            FirebaseResponse res;
//            res = twoBigDB.Get("WATERPRODUCT");
//            Model.WaterProduct water = res.ResultAs<Model.WaterProduct>();
//            var item = res.Body;
//            Dictionary<string, Model.WaterProduct> gallons = JsonConvert.DeserializeObject<Dictionary<string, Model.WaterProduct>>(item);

//            foreach (KeyValuePair<string, Model.WaterProduct> entry in gallons)
//            {
//                WaterTypeDDL.Items.Add(entry.Value.waterType.ToString());
//            }
//        }

       
//    }
//}