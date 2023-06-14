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
using System.Threading.Tasks;
using System.Data;
using WRS2big_Web.Model;
using System.Security.Cryptography;
using System.Text;

namespace WRS2big_Web.Admin
{
    public partial class Employees : System.Web.UI.Page
    {
        //Initialize the FirebaseClient with the database URL and secret key.
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
                DisplayTable();
            }
        }

        //DISPLAY ALL EMPLOYEE
        private void DisplayTable()
        {
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
            Dictionary<string, Employee> employeelist = response.ResultAs<Dictionary<string, Employee>>();
           
            // Create the DataTable to hold the orders
            //sa pag create sa table
            DataTable employeesTable = new DataTable();
            employeesTable.Columns.Add("STATUS");
            employeesTable.Columns.Add("EMPLOYEE ID");
            employeesTable.Columns.Add("EMPLOYEE NAME");
            employeesTable.Columns.Add("GENDER");
            employeesTable.Columns.Add("POSITION");
            employeesTable.Columns.Add("CONTACT NUMBER");
            employeesTable.Columns.Add("EMAIL ADDRESS");
            employeesTable.Columns.Add("DATE HIRED");
            employeesTable.Columns.Add("AVAILABILITY");
            employeesTable.Columns.Add("ADDRESS");
            employeesTable.Columns.Add("EMERGENCY CONTACT");
            employeesTable.Columns.Add("DATE ADDED");
            employeesTable.Columns.Add("ADDED BY");
            employeesTable.Columns.Add("DETAILS DATE UPDATED");
            employeesTable.Columns.Add("DETAILS UPDATED BY");
            employeesTable.Columns.Add("DATE RESIGNED");
            employeesTable.Columns.Add("STATUS MODIFIED BY");
            //employeesTable.Columns.Add("PASSWORD");

            if (response != null && response.ResultAs<Employee>() != null)
            {
                var filteredList = employeelist.Values.Where(d => d.adminId.ToString() == idno).OrderByDescending(d => d.dateAdded);

                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    // Decrypt the password
                    //string decryptedPassword = DecryptSHA1Hash(entry.emp_pass);

                    string dateAdded = entry.dateAdded == DateTime.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateUpdated = entry.dateUpdated == DateTime.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateStatusModified = entry.statusDateModified == DateTime.MinValue ? "" : entry.statusDateModified.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    employeesTable.Rows.Add(entry.emp_status, entry.emp_id, entry.emp_firstname + " " + entry.emp_midname + " " + entry.emp_lastname,
                        entry.emp_gender, entry.emp_role,  entry.emp_contactnum, entry.emp_email, entry.emp_dateHired, entry.emp_availability, entry.emp_address, entry.emp_emergencycontact,
                        dateAdded, entry.addedBy, dateUpdated, entry.updatedBy, dateStatusModified, entry.status_ModifiedBy);

                    //employeesTable.Rows.Add(entry.emp_status, entry.emp_id, entry.emp_firstname + " " + entry.emp_midname + " " + entry.emp_lastname,
                    //    entry.emp_gender, entry.emp_role, entry.emp_contactnum, entry.emp_email, entry.emp_dateHired, entry.emp_availability, entry.emp_address, entry.emp_emergencycontact,
                    //    dateAdded, entry.addedBy, dateUpdated, entry.updatedBy, dateStatusModified, entry.status_ModifiedBy, decryptedPassword);
                }
                if (employeesTable.Rows.Count == 0)
                {
                    // Handle null response or invalid selected value
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    lblError.Text = "No Employee Data Found";
                    lblError.Visible = true;
                }
                else
                {
                    // Bind the DataTable to the GridView
                    GridView1.DataSource = employeesTable;
                    GridView1.DataBind();
                    lblError.Visible = false;
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblError.Text = "No Employee Data Found";
                lblError.Visible = true;
            }
        }
       
        //DISPLAY ACTIVE EMPLOYEE
        private void displayActiveEmp()
        {
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
            Dictionary<string, Employee> employeelist = response.ResultAs<Dictionary<string, Employee>>();

            // Create the DataTable to hold the orders
            //sa pag create sa table
            DataTable employeesTable = new DataTable();
            employeesTable.Columns.Add("STATUS");
            employeesTable.Columns.Add("EMPLOYEE ID");
            employeesTable.Columns.Add("EMPLOYEE NAME");
            employeesTable.Columns.Add("GENDER");
            employeesTable.Columns.Add("POSITION");
            employeesTable.Columns.Add("CONTACT NUMBER");
            employeesTable.Columns.Add("EMAIL ADDRESS");
            employeesTable.Columns.Add("DATE HIRED");
            employeesTable.Columns.Add("AVAILABILITY");
            employeesTable.Columns.Add("ADDRESS");
            employeesTable.Columns.Add("EMERGENCY CONTACT");
            employeesTable.Columns.Add("DATE ADDED");
            employeesTable.Columns.Add("ADDED BY");
            employeesTable.Columns.Add("DETAILS DATE UPDATED");
            employeesTable.Columns.Add("DETAILS UPDATED BY");
            employeesTable.Columns.Add("DATE RESIGNED");
            employeesTable.Columns.Add("STATUS MODIFIED BY");

            if (response != null && response.ResultAs<Employee>() != null)
            {
                var filteredList = employeelist.Values.Where(d => (d.adminId.ToString() == idno) && (d.emp_status == "Active")).OrderByDescending(d => d.dateAdded);

                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    string dateAdded = entry.dateAdded == DateTime.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateUpdated = entry.dateUpdated == DateTime.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateStatusModified = entry.statusDateModified == DateTime.MinValue ? "" : entry.statusDateModified.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    employeesTable.Rows.Add(entry.emp_status, entry.emp_id, entry.emp_firstname + " " + entry.emp_midname + " " + entry.emp_lastname,
                        entry.emp_gender, entry.emp_role, entry.emp_contactnum, entry.emp_email, entry.emp_dateHired, entry.emp_availability, entry.emp_address, entry.emp_emergencycontact,
                        dateAdded, entry.addedBy, dateUpdated, entry.updatedBy, dateStatusModified, entry.status_ModifiedBy);

                    //employeesTable.Rows.Add(entry.emp_status, entry.emp_id,
                    //                     entry.emp_firstname + " " + entry.emp_lastname, entry.emp_gender, entry.emp_role,
                    //                     entry.emp_contactnum, entry.emp_email, entry.emp_dateHired, entry.emp_availability, entry.emp_address, 
                    //                     entry.emp_emergencycontact, entry.dateAdded, entry.addedBy);
                }
                if (employeesTable.Rows.Count == 0)
                {
                    // Handle null response or invalid selected value
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    lblError.Text = "No Active Employee Record Found";
                    lblError.Visible = true;
                }
                else
                {
                    // Bind the DataTable to the GridView
                    GridView1.DataSource = employeesTable;
                    GridView1.DataBind();
                    lblError.Visible = false;
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblError.Text = "No Active Employee Record Found";
                lblError.Visible = true;
            }
        }
        //DISPLAY INACTIVE EMPLOYEE
        private void displayInActiveEmp()
        {
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
            Dictionary<string, Employee> employeelist = response.ResultAs<Dictionary<string, Employee>>();

            // Create the DataTable to hold the orders
            //sa pag create sa table
            DataTable employeesTable = new DataTable();
            employeesTable.Columns.Add("STATUS");
            employeesTable.Columns.Add("EMPLOYEE ID");
            employeesTable.Columns.Add("EMPLOYEE NAME");
            employeesTable.Columns.Add("GENDER");
            employeesTable.Columns.Add("POSITION");
            employeesTable.Columns.Add("CONTACT NUMBER");
            employeesTable.Columns.Add("EMAIL ADDRESS");
            employeesTable.Columns.Add("DATE HIRED");
            employeesTable.Columns.Add("AVAILABILITY");
            employeesTable.Columns.Add("ADDRESS");
            employeesTable.Columns.Add("EMERGENCY CONTACT");
            employeesTable.Columns.Add("DATE ADDED");
            employeesTable.Columns.Add("ADDED BY");
            employeesTable.Columns.Add("DETAILS DATE UPDATED");
            employeesTable.Columns.Add("DETAILS UPDATED BY");
            employeesTable.Columns.Add("DATE RESIGNED");
            employeesTable.Columns.Add("STATUS MODIFIED BY");

            if (response != null && response.ResultAs<Employee>() != null)
            {
                var filteredList = employeelist.Values.Where(d => (d.adminId.ToString() == idno) && (d.emp_status == "Inactive")).OrderByDescending(d => d.dateAdded);

                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    string dateAdded = entry.dateAdded == DateTime.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateUpdated = entry.dateUpdated == DateTime.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateStatusModified = entry.statusDateModified == DateTime.MinValue ? "" : entry.statusDateModified.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    employeesTable.Rows.Add(entry.emp_status, entry.emp_id, entry.emp_firstname + " " + entry.emp_midname + " " + entry.emp_lastname,
                        entry.emp_gender, entry.emp_role, entry.emp_contactnum, entry.emp_email, entry.emp_dateHired, entry.emp_availability, entry.emp_address, entry.emp_emergencycontact,
                        dateAdded, entry.addedBy, dateUpdated, entry.updatedBy, dateStatusModified, entry.status_ModifiedBy);

                    //employeesTable.Rows.Add(entry.emp_status, entry.emp_id,
                    //                     entry.emp_firstname + " " + entry.emp_lastname, entry.emp_gender, entry.emp_role,
                    //                     entry.emp_contactnum, entry.emp_email, entry.emp_dateHired, entry.emp_availability, entry.emp_address, 
                    //                     entry.emp_emergencycontact, entry.dateAdded, entry.addedBy);
                }
                if (employeesTable.Rows.Count == 0)
                {
                    // Handle null response or invalid selected value
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    lblError.Text = "No Inactive Employee Record Found";
                    lblError.Visible = true;
                }
                else
                {
                    // Bind the DataTable to the GridView
                    GridView1.DataSource = employeesTable;
                    GridView1.DataBind();
                    lblError.Visible = false;
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblError.Text = "No Inactive Employee Record Found";
                lblError.Visible = true;
            }
        }
        //STORE/ADD DATA
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            //int logsId = (int)Session["logsId"];
            string name = (string)Session["fullname"];

            try
            {
                Random rnd = new Random();
                int employee_id = rnd.Next(1, 10000);
                //  string employee_id = (string)Session["idno"];

                // Password validation
                string password = txtpass.Text;

                if (password.Length < 8 || password.Length > 20 ||
                    !password.Any(char.IsLetter) || !password.Any(char.IsDigit) ||
                    !password.Any(c => !char.IsLetterOrDigit(c)))
                {
                    Response.Write("<script>alert('Password must be between 8-20 characters long and contain at least 1 letter, 1 number, and 1 special character.'); </script>");
                    return;
                }

                // Encrypt the password using SHA-256
                string hashedPassword = GetSHA256Hash(password);

                // Contact number validation
                string contactNum = txtcontactnum.Text;
                if (contactNum.Length != 11 || !contactNum.All(char.IsDigit))
                { 
                    Response.Write("<script>alert('Contact number must be 11 digits long and contain only numbers.'); </script>");
                    return;
                }

                //insert data
                var data = new Employee
                {
                    adminId = int.Parse(idno),
                    emp_id = employee_id,
                    emp_lastname = txtlastname.Text,
                    emp_firstname = txtfirstname.Text,
                    emp_midname = txtmidname.Text,
                    emp_birthdate = BirthDate.Text,
                    emp_gender = drdgender.SelectedValue,
                    emp_address =txtaddress.Text,
                    emp_contactnum = txtcontactnum.Text,
                    emp_email = txtemail.Text,
                    //emp_pass = txtpass.Text,
                    emp_pass = hashedPassword,
                    emp_dateHired = txtdateHired.Text,
                    emp_emergencycontact =txtemergencycontact.Text,
                    emp_role = drdrole.SelectedValue,
                    emp_status =Drd_status.SelectedValue,
                    addedBy = name,
                    dateAdded = DateTime.Now
                };

                SetResponse response;
                // Employee Records = tablename, emp_id = key ( PK? )
                response = twoBigDB.Set("EMPLOYEES/" + data.emp_id, data);
                Employee obj = response.ResultAs<Employee>();//Database Result

                Response.Write("<script> alert('Employee: " + data.emp_firstname + " " + data.emp_lastname + " successfully added!'); </script>");

                // Retrieve the existing employee object from the database
                FirebaseResponse res = twoBigDB.Get("ADMINLOGS");
                UsersLogs existingLog = res.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = employee_id,
                    userFullname = (string)Session["fullname"],
                    role = "Admin",
                    activityTime = addedTime,
                    userActivity = "ADDED EMPLOYEE DETAILS"
                };
                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);

                // Clear textbox fields
                ClearTextBoxes();

                DisplayTable();
            }
            catch (Exception ex)
            {
                Response.Write("<pre>" + ex.ToString() + "</pre>");

            }
        }
        // Method to clear all textbox fields
        private void ClearTextBoxes()
        {
            txtpass.Text = "";
            txtcontactnum.Text = "";
            txtlastname.Text = "";
            txtfirstname.Text = "";
            txtmidname.Text = "";
            BirthDate.Text = "";
            //drdgender.SelectedIndex = 0;
            txtaddress.Text = "";
            txtemail.Text = "";
            txtdateHired.Text = "";
            txtemergencycontact.Text = "";
            //drdrole.SelectedIndex = 0;
            //Drd_status.SelectedIndex = 0;
        }
        //ENCRYPTING THE PASSWORD
        private string GetSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        //private string GetSHA1Hash(string input)
        //{
        //    using (SHA1 sha1 = SHA1.Create())
        //    {
        //        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        //        byte[] hashBytes = sha1.ComputeHash(inputBytes);

        //        StringBuilder sb = new StringBuilder();
        //        for (int i = 0; i < hashBytes.Length; i++)
        //        {
        //            sb.Append(hashBytes[i].ToString("x2"));
        //        }

        //        return sb.ToString();
        //    }
        //}
        protected void btnViewEmployee_Click(object sender, EventArgs e)
        {

            string selectedOption = drdEmpStatus.SelectedValue;
            try
            {
                if (selectedOption == "0")
                {
                    DisplayTable();

                }
                else if (selectedOption == "1")
                {
                    displayActiveEmp();

                }
                else if (selectedOption == "2")
                {
                    displayInActiveEmp();

                }
               

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' No data exist'); window.location.href = '/Admin/EmployeeRecord.aspx';" + ex.Message);
            }
        }
        //STORE THE EMPLOYEE ID FOR LATER USE AND UPDATE 
        protected void btnEditEmp_Click(object sender, EventArgs e)
        {
            // Retrieve the button that was clicked
            Button btnEditEmp = (Button)sender;
           
            // Find the GridView row containing the button
            GridViewRow row = (GridViewRow)btnEditEmp.NamingContainer;

            // Get the employee ID from the specific column
            int employeeIDColumnIndex = 2; //  the actual column index of the employee ID
            int employeeID = int.Parse(row.Cells[employeeIDColumnIndex].Text);

            // Store the employee ID in a hidden field for later use
            hfeditEmployeeDetails.Value = employeeID.ToString();

            // Retrieve the existing employee object from the database using the empID entered
            FirebaseResponse response = twoBigDB.Get("EMPLOYEES/" + employeeID);
            Employee existingEmp = response.ResultAs<Employee>();

            if (response == null || existingEmp == null)
            {
                // Show error message if the response or existingEmp is null
                Response.Write("<script>alert ('An error occurred while retrieving the employee details.');</script>");
                return;
            }

            // Assign the values to the UI controls
            txt_address.Text = existingEmp.emp_address;
            txt_contactNumber.Text = existingEmp.emp_contactnum;
            drd_empPosition.SelectedValue = existingEmp.emp_role;
            drd_empStatus.SelectedValue = existingEmp.emp_status;
            txtEmail_address.Text = existingEmp.emp_email;

            // Store the employee ID in a hidden field for later use
            hfeditEmployeeDetails.Value = employeeID.ToString();

            // Show the modal popup
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editEmpDetails", "$('#editEmpDetails').modal('show');", true);
        }
        //PERFORM THE EMPLOYEE ID TO UPDATE HERE
        protected void btnEditEmployeeDetails_Click(object sender, EventArgs e)
        {
            //  generate a random number for employee logged
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            string name = (string)Session["fullname"];

            // Get the employee ID from the hidden field
            int employeeID = int.Parse(hfeditEmployeeDetails.Value);

            // Get the new status and position from the DropDownList in the modal popup
            string newPosition = drd_empPosition.SelectedValue;
            string newStatus = drd_empStatus.SelectedValue;
            string contactnumber = txt_contactNumber.Text;
            string email =txtEmail_address.Text;
            string address = txt_address.Text;

            string resetPassword =resetPass.Text.Trim(); // Get the reset password from the TextBox

            try
            {

                // Retrieve the existing employee object from the database using the empID entered
                FirebaseResponse response = twoBigDB.Get("EMPLOYEES/" + employeeID);
                Employee existingEmp = response.ResultAs<Employee>();

                if (response == null || existingEmp == null)
                {
                    // Show error message if the response or existingEmp is null
                    Response.Write("<script>alert ('An error occurred while retrieving the employee details.');</script>");
                    return;
                }

                // Check if the current user has permission to edit the employee data
                if (existingEmp.adminId != int.Parse(idno))
                {
                    // Show error message if the user does not have permission to edit the employee data
                    Response.Write("<script>alert ('You do not have permission to edit this employee data!');</script>");
                    return;
                }

                // Create a new employee object with the updated data
                Employee updatedEmp = new Employee
                {
                    adminId = existingEmp.adminId,
                    emp_id = existingEmp.emp_id,
                    emp_firstname = existingEmp.emp_firstname,
                    emp_midname = existingEmp.emp_midname,
                    emp_lastname = existingEmp.emp_lastname,
                    emp_gender = existingEmp.emp_gender,
                    emp_email = existingEmp.emp_email,
                    emp_birthdate = existingEmp.emp_birthdate,
                    emp_contactnum = existingEmp.emp_contactnum,
                    emp_emergencycontact = existingEmp.emp_emergencycontact,
                    emp_dateHired = existingEmp.emp_dateHired,
                    dateAdded = existingEmp.dateAdded,
                    emp_address = existingEmp.emp_address,
                    emp_pass = existingEmp.emp_pass,
                    emp_role = existingEmp.emp_role,
                    emp_status = existingEmp.emp_status,
                    addedBy = existingEmp.addedBy,
                    emp_availability = existingEmp.emp_availability
                };

                // Update the fields that have changed
                bool fieldUpdated = false; // Flag to track if any field has been updated

                if (!string.IsNullOrEmpty(newPosition) && newPosition != existingEmp.emp_role)
                {
                    updatedEmp.emp_role = newPosition;
                    fieldUpdated = true;
                }
                else if (!string.IsNullOrEmpty(address) && address != existingEmp.emp_address)
                {
                    updatedEmp.emp_address = address;
                    fieldUpdated = true;
                }
                else if (!string.IsNullOrEmpty(contactnumber) && contactnumber != existingEmp.emp_contactnum)
                {
                    updatedEmp.emp_contactnum = contactnumber;
                    fieldUpdated = true;
                }
                else if (!string.IsNullOrEmpty(email) && email != existingEmp.emp_email)
                {
                    updatedEmp.emp_email = email;
                    fieldUpdated = true;
                }

                // Check if the reset password is provided
                if (!string.IsNullOrEmpty(resetPassword))
                {
                    // Encrypt the reset password
                    string encryptedPassword = GetResetSHA256Hash(resetPassword);

                    // Update the employee's password in the database
                    updatedEmp.emp_pass = encryptedPassword;
                    fieldUpdated = true;
                }

                // Update the common properties if any field has been updated
                if (fieldUpdated)
                {
                    updatedEmp.dateUpdated = DateTime.Now;
                    updatedEmp.updatedBy = name;
                }


                //// Update the fields that have changed
                //if (!string.IsNullOrEmpty(newPosition) && newPosition != existingEmp.emp_role)
                //{
                //    updatedEmp.emp_role = newPosition;
                //    updatedEmp.dateUpdated = DateTime.Now;
                //    updatedEmp.updatedBy = name;
                //}
                //else if (!string.IsNullOrEmpty(address) && address != existingEmp.emp_address)
                //{
                //    updatedEmp.emp_address = address;
                //    updatedEmp.dateUpdated = DateTime.Now;
                //    updatedEmp.updatedBy = name;
                //}
                //else if (!string.IsNullOrEmpty(contactnumber) && contactnumber != existingEmp.emp_contactnum)
                //{
                //    updatedEmp.emp_contactnum = contactnumber;
                //    updatedEmp.dateUpdated = DateTime.Now;
                //    updatedEmp.updatedBy = name;
                //}
                //else if (!string.IsNullOrEmpty(email) && email != existingEmp.emp_email)
                //{
                //    updatedEmp.emp_email = email;
                //    updatedEmp.dateUpdated = DateTime.Now;
                //    updatedEmp.updatedBy = name;
                //}
                //// Check if the reset password is provided
                //if (!string.IsNullOrEmpty(resetPassword))
                //{
                //    // Encrypt the reset password
                //    string encryptedPassword = GetResetSHA1Hash(resetPassword);

                //    // Update the employee's password in the database
                //    updatedEmp.emp_pass = encryptedPassword;
                //    updatedEmp.dateUpdated = DateTime.Now;
                //    updatedEmp.updatedBy = name;
                //}

                //if emp status is already inactive
                if (existingEmp.emp_status == "Inactive")
                {
                    Response.Write("<script>alert ('Error: This employee is inactive and cannot be updated.'); location.reload(); window.location.href = '/Admin/EmployeeRecord.aspx';</script>");

                    return;
                }

                if (!string.IsNullOrEmpty(newStatus) && newStatus != existingEmp.emp_status)
                {
                    updatedEmp.emp_status = newStatus;
                    updatedEmp.statusDateModified = DateTime.Now;
                    updatedEmp.status_ModifiedBy = name;
                }


                // Update the existing employee object in the database
                response = twoBigDB.Update("EMPLOYEES/" + employeeID, updatedEmp);

                // Show success message
                Response.Write("<script>alert ('Employee " + employeeID + " has been successfully updated!'); location.reload(); window.location.href = '/Admin/EmployeeRecord.aspx';</script>");
                // Get the current date and time
                DateTime addedTime = DateTime.Now;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    role = "Admin",
                    activityTime = addedTime,
                    userActivity = "UPDATED EMPLOYEE DETAILS",
                    // userActivity = UserActivityType.UpdatedEmployeeRecords
                };
                twoBigDB.Set("ADMINLOGS/" + log.logsId, log);


                DisplayTable();
                
            }
            catch (Exception ex)
            {
                // Show error message
                Response.Write("<script>alert ('An error occurred while processing your request.');</script>" + ex.Message);
            }
        }
        //ENCRYPTING THE RESET PASSWORD
        private string GetResetSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }


        //SEARCH CERTAIN EMPLOYEE REPORT
        protected void btnSearchEmployee_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#view').modal();", true);

            string idno = (string)Session["idno"];
            string empSearch = txtSearch.Text;
            try
            {
                // Check if the employee ID is valid
                if (string.IsNullOrEmpty(empSearch))
                {
                    Response.Write("<script>alert ('Please enter a valid employee ID or firstname!');</script>");
                    return;
                }
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
                Dictionary<string, Employee> employeeList = response.ResultAs<Dictionary<string, Employee>>();

                // Create the DataTable to hold the orders
                DataTable employeeTable = new DataTable();
                employeeTable.Columns.Add("EMPLOYEE ID");
                employeeTable.Columns.Add("EMPLOYEE NAME");
                employeeTable.Columns.Add("GENDER");
                employeeTable.Columns.Add("POSITION");
                employeeTable.Columns.Add("STATUS");
                employeeTable.Columns.Add("CONTACT NUMBER");
                employeeTable.Columns.Add("EMAIL ADDRESS");
                employeeTable.Columns.Add("DATE HIRED");
                employeeTable.Columns.Add("ADDRESS");
                employeeTable.Columns.Add("EMERGENCY CONTACT");
                employeeTable.Columns.Add("DATE ADDED");
                employeeTable.Columns.Add("ADDED BY");
                employeeTable.Columns.Add("DATE UPDATED");
                employeeTable.Columns.Add("UPDATED BY");
                employeeTable.Columns.Add("DATE RESIGNED");
                employeeTable.Columns.Add("STATUS MODIFIED BY");


                // Filter the employee list based on the search query
                if (response != null && response.ResultAs<Employee>() != null)
                {
                    var filteredList = employeeList.Values.Where(d => d.adminId.ToString() == idno && (d.emp_id.ToString() == empSearch ||
                        d.emp_firstname.ToLower().Contains(empSearch.ToLower())));

                    // Loop through the entries and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        string dateAdded = entry.dateAdded == DateTime.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateUpdated = entry.dateUpdated == DateTime.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateStatusModified = entry.statusDateModified == DateTime.MinValue ? "" : entry.statusDateModified.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        employeeTable.Rows.Add(entry.emp_id, entry.emp_firstname + " " + entry.emp_midname + " " + entry.emp_lastname,
                            entry.emp_gender, entry.emp_role, entry.emp_status, entry.emp_contactnum, entry.emp_email,
                            entry.emp_dateHired, entry.emp_address, entry.emp_emergencycontact, dateAdded, entry.addedBy, dateUpdated, entry.updatedBy,
                            dateStatusModified, entry.status_ModifiedBy);

                       
                    }
                    lblSearhRecord.Text = " You search the record of " + " " + " employee " + " " + empSearch;
                }
                else
                {
                    lblMessage.Text = "No employee data found for the given search query.";
                }

               
                // Bind the DataTable to the GridView
                gridEmp_Details.DataSource = employeeTable;
                gridEmp_Details.DataBind();

                //  Response.Write("<script> location.reload(); window.location.href = '/Admin/WaterOrders.aspx'; </script>");
                txtSearch.Text = null;

            }
            catch (Exception ex)
            {
                // Show error message
                Response.Write("<script>alert ('An error occurred while processing your request.');</script>" + ex.Message);
            }
        }
        ////DECRYPTING THE PASSWORD
        //private string DecryptSHA1Hash(string hashedPassword)
        //{
        //    // Convert the hashed password to byte array
        //    byte[] hashBytes = Encoding.UTF8.GetBytes(hashedPassword);

        //    // Create an instance of the SHA1 algorithm
        //    using (SHA1 sha1 = SHA1.Create())
        //    {
        //        // Compute the hash of the byte array
        //        byte[] decryptedBytes = sha1.ComputeHash(hashBytes);

        //        // Convert the decrypted bytes to string
        //        StringBuilder sb = new StringBuilder();
        //        for (int i = 0; i < decryptedBytes.Length; i++)
        //        {
        //            sb.Append(decryptedBytes[i].ToString("x2"));
        //        }

        //        return sb.ToString();
        //    }
        //}

    }
}