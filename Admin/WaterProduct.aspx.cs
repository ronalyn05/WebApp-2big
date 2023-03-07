using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            DrdprodType.Text = obj.productType.ToString();
            DrdprodSize.Text = obj.productSize.ToString();
            prodPrice.Text = obj.productPrice.ToString();
            prodAvailable.Text = obj.productAvailable.ToString();
            LblDate.Text = obj.DateAdded.ToString();
        }
        public void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // INSERT DATA TO TABLE = PRODUCT
                Random rnd = new Random();
                int idnum = rnd.Next(1, 10000);

                var data = new Model.Product
                {
                    productId = idnum,
                    productType = DrdproductType.Text,
                    productSize = DrdproductSize.Text,
                    productPrice = productPrice.Text,
                    productAvailable = productAvailable.Text,
                    DateAdded = DateTime.UtcNow
                };
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
            data.productType = DrdprodType.Text;
            data.productSize = DrdprodSize.Text;
            data.productPrice = prodPrice.Text;
            data.productAvailable = prodAvailable.Text;
            data.DateAdded = DateTime.UtcNow;

            //if (data.productType != 0 && data.productSize != 0)
            //{
                FirebaseResponse response;
            response = twoBigDB.Update("PRODUCT/" + deleteStr, data);//Update Product Data 

            var result = twoBigDB.Get("PRODUCT/" + deleteStr);//Retrieve Updated Data From WATERPRODUCT TBL
            Model.Product obj = response.ResultAs<Model.Product>();//Database Result

            LabelID.Text = obj.productId.ToString();
            DrdprodType.Text = obj.productType.ToString();
            DrdprodSize.Text = obj.productSize.ToString();
            prodPrice.Text = obj.productPrice.ToString();
            prodAvailable.Text = obj.productAvailable.ToString();
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