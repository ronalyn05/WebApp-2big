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
//    public partial class Employees : System.Web.UI.Page
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

//            //METHODS TO DISPLAY THE IDs
//            if (!IsPostBack)
//            {
//                DisplayID();
//            }

           
//        }

//        private void DisplayID()
//        {
//            FirebaseResponse response;
//            response = twoBigDB.Get("EMPLOYEERECORD/");
//            Model.EmployeeData obj = response.ResultAs<Model.EmployeeData>();
//            var json = response.Body;
//            Dictionary<string, Model.EmployeeData> list = JsonConvert.DeserializeObject<Dictionary<string, Model.EmployeeData>>(json);

           
//            foreach (KeyValuePair<string, Model.EmployeeData> entry in list)
//            {
//                //ListBoxEmployeeRecord.Items.Add(entry.Value.emp_id.ToString());
//                if (entry.Value.emp_status == "Active")
//                {
//                    ListBoxEmployeeRecord.Items.Add(entry.Value.emp_id.ToString());
//                }
//                else if (entry.Value.emp_status == "Inactive")
//                {
//                    ListBox1.Items.Add(entry.Value.emp_id.ToString());
//                }

//            }
//        }
//    protected void btnDisplay_Click(object sender, EventArgs e)
//        {
//            String slected;
//            slected = ListBoxEmployeeRecord.SelectedValue;

//            FirebaseResponse response;
//            response = twoBigDB.Get("EMPLOYEERECORD/" + slected);
//            Model.EmployeeData obj = response.ResultAs<Model.EmployeeData>();

//            LblID.Text = obj.emp_id.ToString();
//            firstname.Text = obj.emp_firstname.ToString();
//            midname.Text = obj.emp_midname.ToString();
//            lastname.Text = obj.emp_lastname.ToString();
//            LblDOB.Text = obj.emp_birthdate.ToString();
//            LblGender.Text = obj.emp_gender.ToString();
//            address.Text = obj.emp_address.ToString();
//            contactnum.Text = obj.emp_contactnum.ToString();
//            email.Text = obj.emp_email.ToString();
//            LbldateHired.Text = obj.emp_dateHired.ToString();
//            emergencycontact.Text = obj.emp_emergencycontact.ToString();
//            drdPosition.Text = obj.emp_role.ToString();
//            drdStatus.Text = obj.emp_status.ToString();
//        }

//        protected void btnInActiveEmp_Click(object sender, EventArgs e)
//        {
//            String slected;
//            slected = ListBox1.SelectedValue;

//            FirebaseResponse response; 
//            response = twoBigDB.Get("EMPLOYEERECORD/" + slected);
//            Model.EmployeeData obj = response.ResultAs<Model.EmployeeData>();

//            LblID.Text = obj.emp_id.ToString();
//            firstname.Text = obj.emp_firstname.ToString();
//            midname.Text = obj.emp_midname.ToString();
//            lastname.Text = obj.emp_lastname.ToString();
//            LblDOB.Text = obj.emp_birthdate.ToString();
//            LblGender.Text = obj.emp_gender.ToString();
//            address.Text = obj.emp_address.ToString();
//            contactnum.Text = obj.emp_contactnum.ToString();
//            email.Text = obj.emp_email.ToString();
//            LbldateHired.Text = obj.emp_dateHired.ToString();
//            emergencycontact.Text = obj.emp_emergencycontact.ToString();
//            drdPosition.Text = obj.emp_role.ToString();
//            drdStatus.Text = obj.emp_status.ToString();

//            if (obj.emp_status.ToString() == "Inactive")
//            {
//                firstname.ReadOnly = true;
//                midname.ReadOnly = true;
//                lastname.ReadOnly = true;
//                address.ReadOnly = true;
//                contactnum.ReadOnly = true;
//                email.ReadOnly = true;
//                emergencycontact.ReadOnly = true;
//                drdPosition.Enabled = false;
//                drdStatus.Enabled = false;
//            }
//            else if (obj.emp_status.ToString() == "Active")
//            {
//                firstname.ReadOnly = false;
//                midname.ReadOnly = false;
//                lastname.ReadOnly = false;
//                address.ReadOnly = false;
//                contactnum.ReadOnly = false;
//                email.ReadOnly = false;
//                emergencycontact.ReadOnly = false;
//                drdPosition.Enabled = true;
//                drdStatus.Enabled = true;
//            }
//        }

//        // STORE/ADD DATA
//        protected void btnAdd_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                Random rnd = new Random();
//                int employee_id = rnd.Next(1, 10000);
//                //insert data
//                var data = new Model.EmployeeData
//                {
//                    emp_id = employee_id,
//                    emp_lastname = txtlastname.Text,
//                    emp_firstname = txtfirstname.Text,
//                    emp_midname = txtmidname.Text,
//                    emp_birthdate = BirthDate.Text,
//                    emp_gender = drdgender.Text,
//                    emp_address = txtaddress.Text,
//                    emp_contactnum = txtcontactnum.Text,
//                    emp_email = txtemail.Text,
//                    emp_dateHired = txtdateHired.Text,
//                    emp_emergencycontact = txtemergencycontact.Text,
//                    emp_role = drdrole.Text,
//                    emp_status = Drd_status.Text
//                };
//                SetResponse response;
//                // Employee Records = tablename, emp_id = key ( PK? )
//                response = twoBigDB.Set("EMPLOYEERECORD/" + data.emp_id, data);//Store Data to database   
//                Model.EmployeeData obj = response.ResultAs<Model.EmployeeData>();//Database Result
               
//                Response.Write("<script> alert ('Employee record: " + data.emp_id + " successfully added!' </script>");
//            }
//            catch (Exception ex)
//            {
//                Response.Write("<pre>" + ex.ToString() + "</pre>");

//            }
//        }
//        //UPDATE DATA
//        protected void btnEdit_Click(object sender, EventArgs e)
//        {
//            String EditStr;
//            EditStr = ListBoxEmployeeRecord.SelectedValue;

//            var data = new Model.EmployeeData();

//            data.emp_id = int.Parse(LblID.Text);
//            data.emp_firstname = firstname.Text;
//            data.emp_midname = midname.Text;
//            data.emp_lastname = lastname.Text;
//            data.emp_birthdate = LblDOB.Text;
//            data.emp_gender = LblGender.Text;
//            data.emp_address = address.Text;
//            data.emp_contactnum = contactnum.Text;
//            data.emp_email = email.Text;
//            data.emp_dateHired = LbldateHired.Text;
//            data.emp_emergencycontact = emergencycontact.Text;
//            data.emp_role = drdPosition.Text;
//            data.emp_status = drdStatus.Text;

//            FirebaseResponse response;
//            response = twoBigDB.Update("EMPLOYEERECORD/" + EditStr, data);//Update Employee Data 

//            var result = twoBigDB.Get("EMPLOYEERECORD/" + EditStr);//Retrieve Updated Data From EMPLOYEERECORD TBL
//            Model.EmployeeData obj = response.ResultAs<Model.EmployeeData>();//Database Result

//            LblID.Text = obj.emp_id.ToString();
//            firstname.Text = obj.emp_firstname.ToString();
//            midname.Text = obj.emp_midname.ToString();
//            lastname.Text = obj.emp_lastname.ToString();
//            LblDOB.Text = obj.emp_birthdate.ToString();
//            LblGender.Text = obj.emp_gender.ToString();
//            address.Text = obj.emp_address.ToString();
//            contactnum.Text = obj.emp_contactnum.ToString();
//            email.Text = obj.emp_email.ToString();
//            LbldateHired.Text = obj.emp_dateHired.ToString();
//            emergencycontact.Text = obj.emp_emergencycontact.ToString();
//            drdPosition.Text = obj.emp_role.ToString();
//            drdStatus.Text = obj.emp_status.ToString();

//            Response.Write("<script>alert ('Employee ID : " + EditStr + " successfully updated!');</script>");
//        }

//        //DELETE DATA
//        //protected void DeleteBtn_Click(object sender, EventArgs e)
//        //{
//        //    String deleteStr;
//        //    deleteStr = ListBoxEmployeeRecord.SelectedValue;
//        //    FirebaseResponse response = twoBigDB.Delete("EMPLOYEERECORD/" + deleteStr);

//        //    //TO DELETE THE ID IN THE LISTBOX AFTER DELETED
//        //    int selected = ListBoxEmployeeRecord.SelectedIndex;
//        //    if (selected != 1)
//        //    {
//        //        ListBoxEmployeeRecord.Items.RemoveAt(selected);
//        //    }

//        //    Response.Write("<script>alert ('Employee ID : " + deleteStr + " successfully deleted ! '); window.location.href = '/Admin/EmployeeRecord.aspx'; </script>");

//        //}

//    }
//}