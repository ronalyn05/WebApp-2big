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
        }
        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            String slected;
            slected = ListBox1.SelectedValue;
            FirebaseResponse response;
            response = twoBigDB.Get("PRODUCT/" + slected);
            Model.Product obj = response.ResultAs<Model.Product>();
            LabelID.Text = obj.productId.ToString();
            prodName.Text = obj.productName.ToString();
            prodSize.Text = obj.productSize.ToString();
            prodPrice.Text = obj.productPrice.ToString();
            prodDiscount.Text = obj.productDiscount.ToString();
            prodAvailable.Text = obj.productAvailable.ToString();
            waterSupAvailable.Text = obj.waterRefillSupply.ToString();
            LblDate.Text = obj.DateAdded.ToString();
        }
        protected async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // INSERT DATA TO TABLE = PRODUCT
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                var data = new Model.Product
                {
                    productId = idnum,
                    productName = txtproductName.Text,
                    productSize = txtproductSize.Text,
                    productPrice = productPrice.Text,
                    productDiscount = productDiscounts.Text,
                    productAvailable = productAvailable.Text,
                    waterRefillSupply = txtWaterAmount.Text,
                    productImage = null,
                    DateAdded = DateTime.UtcNow
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
                    response = twoBigDB.Set("PRODUCT/" + data.productId, data);
                    Model.Product result = response.ResultAs<Model.Product>();
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
            FirebaseResponse response;
            response = twoBigDB.Get("PRODUCT/");
            Model.Product obj = response.ResultAs<Model.Product>();
            var json = response.Body;
            Dictionary<string, Model.Product> list = JsonConvert.DeserializeObject<Dictionary<string, Model.Product>>(json);

            foreach (KeyValuePair<string, Model.Product> entry in list)
            {
                ListBox1.Items.Add(entry.Value.productId.ToString());
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            String deleteStr;
            deleteStr = ListBox1.SelectedValue;

            var data = new Model.Product();

            data.productId = int.Parse(LabelID.Text);
            data.productName = prodName.Text;
            data.productSize = prodSize.Text;
            data.productPrice = prodPrice.Text;
            data.productDiscount = prodDiscount.Text;
            data.productAvailable = prodAvailable.Text;
            data.waterRefillSupply = waterSupAvailable.Text;
            data.DateAdded = DateTime.UtcNow;

            //if (data.productType != 0 && data.productSize != 0)
            //{
                FirebaseResponse response;
            response = twoBigDB.Update("PRODUCT/" + deleteStr, data);//Update Product Data 

            var result = twoBigDB.Get("PRODUCT/" + deleteStr);//Retrieve Updated Data From WATERPRODUCT TBL
            Model.Product obj = response.ResultAs<Model.Product>();//Database Result

            LabelID.Text = obj.productId.ToString();
            prodName.Text = obj.productName.ToString();
            prodSize.Text = obj.productSize.ToString();
            prodPrice.Text = obj.productPrice.ToString();
            prodDiscount.Text = obj.productDiscount.ToString();
            prodAvailable.Text = obj.productAvailable.ToString();
            waterSupAvailable.Text = obj.waterRefillSupply.ToString();
            LblDate.Text = obj.DateAdded.ToString();

            Response.Write("<script>alert ('Product ID : " + deleteStr + " successfully updated!');</script>");
            //}
            //else
            //{
            //    Response.Write("<script> alert ('Invalid Product Type! Please select one.. ') </script>");
            //}
        }
      
        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            String deleteStr;
            deleteStr = ListBox1.SelectedValue;
            FirebaseResponse response = twoBigDB.Delete("PRODUCT/" + deleteStr);

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