using System;
using System.Collections.Generic;
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
                DisplayID();
            }
           // string idno = (string)Session["idno"];

           //Session["productImage"] as byte[];
        }
        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                string idno = (string)Session["idno"];
                string selected = ListBox1.SelectedValue;
                FirebaseResponse response = twoBigDB.Get("ADMIN/" + idno + "/Product/" + selected);

                if (response != null && response.ResultAs<Products>() != null)
                {
                    Products obj = response.ResultAs<Products>();
                    LabelID.Text = obj.productId.ToString();
                    prodName.Text = obj.productName.ToString();
                    prodSize.Text = obj.productSize.ToString();
                    prodPrice.Text = obj.productPrice.ToString();
                    prodDiscount.Text = obj.productDiscount.ToString();
                    prodAvailable.Text = obj.productAvailable.ToString();
                    waterSupAvailable.Text = obj.waterRefillSupply.ToString(); 
                    LblDate.Text = obj.dateAdded.ToString();
                }
                else
                {
                    // Handle null response or invalid selected value
                    LabelID.Text = "";
                    prodName.Text = "";
                    prodSize.Text = "";
                    prodPrice.Text = "";
                    prodDiscount.Text = "";
                    prodAvailable.Text = "";
                    waterSupAvailable.Text = "";
                    LblDate.Text = "";
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                LabelID.Text = "";
                prodName.Text = "";
                prodSize.Text = "";
                prodPrice.Text = "";
                prodDiscount.Text = "";
                prodAvailable.Text = "";
                waterSupAvailable.Text = "";
                LblDate.Text = "";
            }
            // string idno = (string)Session["idno"];
            // String slected;
            // slected = ListBox1.SelectedValue;
            // FirebaseResponse response;
            // response = twoBigDB.Get("ADMIN/" + idno + "/Product/" + slected);
            //// response = twoBigDB.Get("PRODUCT/" + slected);
            // Products obj = response.ResultAs<Products>();
            // LabelID.Text = obj.productId.ToString();
            // prodName.Text = obj.productName.ToString();
            // prodSize.Text = obj.productSize.ToString();
            // prodPrice.Text = obj.productPrice.ToString();
            // prodDiscount.Text = obj.productDiscount.ToString();
            // prodAvailable.Text = obj.productAvailable.ToString();
            // waterSupAvailable.Text = obj.waterRefillSupply.ToString();
            // LblDate.Text = obj.dateAdded.ToString();
        }
        protected async void btnAdd_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            try
            {
                // INSERT DATA TO TABLE = PRODUCT
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                var data = new Products
                {
                    productId = idnum,
                    productName = txtproductName.Text,
                    productSize = txtproductSize.Text,
                    productPrice = productPrice.Text,
                    productDiscount = productDiscounts.Text,
                    productAvailable = productAvailable.Text,
                    waterRefillSupply = txtWaterAmount.Text,
                    productImage = null,
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
                    var filePath = $"productimages/{data.productName}{fileExtension}";
                    //  used the using statement to ensure that the MemoryStream object is properly disposed after it's used.
                    using (var stream = new MemoryStream(fileBytes))
                    {
                        var storageTask = storage.Child(filePath).PutAsync(stream);
                        var downloadUrl = await storageTask;
                        // used Encoding.ASCII.GetBytes to convert the downloadUrl string to a byte[] object.
                        data.productImage = Encoding.ASCII.GetBytes(downloadUrl);
                    }
                }
                //if(data.productType != 0 && data.productSize != 0)
                //{
                SetResponse response;
                //USER = tablename, Idno = key(PK ? )
                response = twoBigDB.Set("ADMIN/" + idno + "/Product/" + data.productId, data);
                Products result = response.ResultAs<Products>();
                Response.Write("<script>alert ('Water Product with Id number: " + data.productId + " is successfully added!'); location.reload(); window.location.href = '/Admin/WaterProduct.aspx'; </script>");
                //}
                //else
                //{
                //    Response.Write("<script> alert ('Invalid Product Type! Please select one.. ') </script>");
                //}   
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Data already exist'); window.location.href = '/Admin/WaterProduct.aspx';" + ex.Message);
            }
        }
        private void DisplayID()
        {
            string idno = (string)Session["idno"];
            FirebaseResponse response;
            response = twoBigDB.Get("ADMIN/" + idno + "/Product/");
            Products obj = response.ResultAs<Products>();
            var json = response.Body;
            Dictionary<string, Products> list = JsonConvert.DeserializeObject<Dictionary<string, Products>>(json);


            if (list != null && list.Count > 0)
            {
                foreach (KeyValuePair<string, Products> entry in list)
                {
                    ListBox1.Items.Add(entry.Value.productId.ToString());
                }
            }

        }

        protected async void btnEdit_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            String deleteStr = ListBox1.SelectedValue;

            // Retrieve the existing product data from the database
            var result = await twoBigDB.GetAsync("ADMIN/" + idno + "/Product/" + deleteStr);
            Products obj = result.ResultAs<Products>();

            var data = new Products();
            data.productId = int.Parse(LabelID.Text);
            data.productName = prodName.Text;
            data.productSize = prodSize.Text;
            data.productPrice = prodPrice.Text;
            data.productDiscount = prodDiscount.Text;
            data.productAvailable = prodAvailable.Text;
            data.waterRefillSupply = waterSupAvailable.Text;
            data.dateAdded = DateTime.UtcNow;
            // Keep the existing productImage
            data.productImage = obj.productImage;

            // Check if there is a new image uploaded by the user
            //HttpPostedFile uploadedFile = imgProduct.PostedFile;
            //if (uploadedFile != null && uploadedFile.ContentLength > 0)
            //{
            //    using (BinaryReader reader = new BinaryReader(uploadedFile.InputStream))
            //    {
            //        data.productImage = reader.ReadBytes(uploadedFile.ContentLength);
            //    }
            //}
            //else
            //{

            //}

            FirebaseResponse response;
            response = await twoBigDB.UpdateAsync("ADMIN/" + idno + "/Product/" + deleteStr, data);//Update Product Data 

            // Retrieve the updated product data from the database
            result = await twoBigDB.GetAsync("ADMIN/" + idno + "/Product/" + deleteStr);
            obj = result.ResultAs<Products>();

            LabelID.Text = obj.productId.ToString();
            prodName.Text = obj.productName.ToString();
            prodSize.Text = obj.productSize.ToString();
            prodPrice.Text = obj.productPrice.ToString();
            prodDiscount.Text = obj.productDiscount.ToString();
            prodAvailable.Text = obj.productAvailable.ToString();
            waterSupAvailable.Text = obj.waterRefillSupply.ToString();
            LblDate.Text = obj.dateAdded.ToString();


            Response.Write("<script>alert ('Product ID : " + deleteStr + " successfully updated!');</script>");
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
            string idno = (string)Session["idno"];
            String deleteStr;
            deleteStr = ListBox1.SelectedValue;
            FirebaseResponse response = twoBigDB.Delete("ADMIN/ " + idno + " /Product/ " + deleteStr);

            //lbl_result.Text = "Records Successfully deleted!";
            //Response.Write("<div>Successfully deleted product ID : "+ deleteStr +" </div>");
            Response.Write("<script>alert ('Successfully deleted product ID : " + deleteStr + "'); window.location.href = '/Admin/WaterProduct.aspx'; </script>");

            //TO DELETE THE ID IN THE LISTBOX AFTER DELETED
            int selected = ListBox1.SelectedIndex;
            if (selected != 1)
            {
                ListBox1.Items.RemoveAt(selected);
            }


        }
    }
}