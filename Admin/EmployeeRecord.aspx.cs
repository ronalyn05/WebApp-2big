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
                displayLogs();
            }

        }

        private void displayLogs()
        {
           // string idno = (string)Session["idno"];
           // int empId = (int)Session["emp_id"];

           // Retrieve all orders from the ORDERS table
           //FirebaseResponse response;
           // response = twoBigDB.Get("USERLOG/" + idno);
           // UserLogs user = response.ResultAs<UserLogs>(); //Database result

           // Session["userIdnum"] = idno;
           // Session["dateLogs"] = user.dateLogs;




            //if (idno == empId)
            //{
            //lblidnum.Text = idno;
            //lblDateTime.Text = user.dateLogs.ToString();
            //}

        }



        //WORKING BUT NEED TO BE MODIFIED
        private void DisplayTable()
        {
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
            Dictionary<string, Employee> employeelist = response.ResultAs<Dictionary<string, Employee>>();
            var filteredList = employeelist.Values.Where(d => d.adminId.ToString() == idno);
            //FirebaseResponse response = twoBigDB.Get("EMPLOYEES/");
            //Employee emp = response.ResultAs<Employee>();
            //var data = response.Body;
            ////Dictionary<string, Employee> employeeList = JsonConvert.DeserializeObject<Dictionary<string, Employee>>(data);
            //Dictionary<string, Employee> employeeList = response.ResultAs<Dictionary<string, Employee>>();

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

            if (response != null && response.ResultAs<Employee>() != null)
            {
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {

                    employeesTable.Rows.Add(entry.emp_status, entry.emp_id,
                                         entry.emp_firstname + " " + entry.emp_lastname, entry.emp_gender, entry.emp_role,
                                         entry.emp_contactnum, entry.emp_email, entry.emp_dateHired, entry.emp_availability, entry.emp_address, 
                                         entry.emp_emergencycontact, entry.dateAdded, idno);
                }
            }
            else
            {
                // Handle null response or invalid selected value
                employeesTable.Rows.Add("No data found", "", "", "", "", "", "");
            }

            // Bind the DataTable to the GridView
            GridView1.DataSource = employeesTable;
            GridView1.DataBind();
        }
        //STORE/ADD DATA
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
            int logsId = (int)Session["logsId"];

            try
            {
                Random rnd = new Random();
                int employee_id = rnd.Next(1, 10000);
                //  string employee_id = (string)Session["idno"];

                //insert data
                var data = new Employee
                {
                    adminId = int.Parse(idno),
                    emp_id = employee_id,
                    emp_lastname = txtlastname.Text,
                    emp_firstname = txtfirstname.Text,
                    emp_midname = txtmidname.Text,
                    emp_birthdate = BirthDate.Text,
                    emp_gender = drdgender.Text,
                    emp_address = txtaddress.Text,
                    emp_contactnum = txtcontactnum.Text,
                    emp_email = txtemail.Text,
                    emp_pass = txtpass.Text,
                    emp_dateHired = txtdateHired.Text,
                    emp_emergencycontact = txtemergencycontact.Text,
                    emp_role = drdrole.Text,
                    emp_status = Drd_status.Text,
                    dateAdded = DateTimeOffset.UtcNow
                };

                SetResponse response;
                // Employee Records = tablename, emp_id = key ( PK? )
                response = twoBigDB.Set("EMPLOYEES/" + data.emp_id, data);
                Employee obj = response.ResultAs<Employee>();//Database Result

                Response.Write("<script> alert('Employee: " + data.emp_firstname + " " + data.emp_lastname + " successfully added!'); window.location.href = '/Admin/EmployeeRecord.aspx'; </script>");

                // Retrieve the existing employee object from the database
                FirebaseResponse res = twoBigDB.Get("USERSLOG/" + logsId);
                UsersLogs existingLog = res.ResultAs<UsersLogs>();

                // Get the current date and time
                DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = employee_id,
                    userFullname = (string)Session["fullname"],
                    activityTime = addedTime,
                    userActivity = "ADDED EMPLOYEE DETAILS",
                    // userActivity = UserActivityType.AddedEmployeeRecords
                    //deliveryDetailsId = existingLog.deliveryDetailsId,
                    //standardAdded = existingLog.standardAdded,
                    //reservationAdded = existingLog.reservationAdded,
                    //expressAdded = existingLog.expressAdded,
                    //productRefillId = existingLog.productRefillId,
                    //productrefillDateAdded = existingLog.productrefillDateAdded,
                    //other_productId = existingLog.other_productId,
                    //otherProductDateAdded = existingLog.otherProductDateAdded,
                    //tankId = existingLog.tankId,
                    //tankSupplyDateAdded = existingLog.tankSupplyDateAdded,
                    //emp_id = employee_id,
                    //empFullname = txtfirstname.Text + " " + txtlastname.Text,
                    //empDateAdded = data.dateAdded   
                };
                twoBigDB.Set("USERSLOG/" + log.logsId, log);

                DisplayTable(); 
            }
            catch (Exception ex)
            {
                Response.Write("<pre>" + ex.ToString() + "</pre>");

            }
        }
        //UPDATE EMPLOYEE DETAILS
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //generate a random number for employee logged
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int logsId = (int)Session["logsId"];
            //  int adminId = int.Parse(idno);
            try
            {
                string empID = txtempId.Text.Trim();

                // Check if the employee ID is valid
                if (string.IsNullOrEmpty(empID))
                {
                    Response.Write("<script>alert ('Please enter a valid employee ID!');</script>");
                    return;
                }
                // Retrieve the existing employee object from the database using the empID entered
                FirebaseResponse response = twoBigDB.Get("EMPLOYEES/" + empID);
                Employee existingEmp = response.ResultAs<Employee>();

                if (existingEmp == null)
                {
                    // Show error message if the empID entered is invalid
                    Response.Write("<script>alert ('Invalid employee ID!');</script>");
                    return;
                }

                // Check if the current user has permission to edit the employee data
                if (existingEmp.adminId != int.Parse(idno))
                {
                    // Show error message if the user does not have permission to edit the employee data
                    Response.Write("<script>alert ('You do not have permission to edit this employee data!');</script>");
                    return;
                }

                // Get the new status and position from the DropDownList in the modal popup
                string newPosition = drd_empPosition.SelectedValue;
                string newStatus = drd_empStatus.SelectedValue;

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
                    emp_availability = existingEmp.emp_availability
                };

                // Update the fields that have changed
                if (!string.IsNullOrEmpty(newPosition) && newPosition != existingEmp.emp_role)
                {
                    updatedEmp.emp_role = newPosition;
                }
                if (!string.IsNullOrEmpty(newStatus) && newStatus != existingEmp.emp_status)
                {
                    updatedEmp.emp_status = newStatus;
                }

                // Update the existing employee object in the database
                response = twoBigDB.Update("EMPLOYEES/" + empID, updatedEmp);

                // Show success message
                Response.Write("<script>alert ('Employee " + empID + " has been successfully updated!'); location.reload(); window.location.href = '/Admin/EmployeeRecord.aspx';</script>");

                // Get the current date and time
                DateTime addedTime = DateTime.UtcNow;

                // Log user activity
                var log = new UsersLogs
                {
                    userIdnum = int.Parse(idno),
                    logsId = idnum,
                    userFullname = (string)Session["fullname"],
                    activityTime = addedTime,
                    userActivity = "UPDATED EMPLOYEE DETAILS",
                    // userActivity = UserActivityType.UpdatedEmployeeRecords
                };
                twoBigDB.Set("USERSLOG/" + log.logsId, log);

                DisplayTable();
            }
            catch (Exception ex)
            {
                Response.Write("<pre>" + ex.ToString() + "</pre>");

            }
        }


        //UPDATING THE EMPLOYEE DATA
        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{

        //    // Show the modal popup
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#editModal').modal();", true);

        //    // Get the admin ID from the session
        //    string idno = (string)Session["idno"];
        //    int adminId = int.Parse(idno);

        //    // Get the GridViewRow that contains the clicked button
        //    Button btn = (Button)sender;
        //    GridViewRow row = (GridViewRow)btn.NamingContainer;

        //    // Get the employee ID from the first cell in the row
        //    int empID = int.Parse(row.Cells[2].Text);
        //    string status = row.Cells[1].Text;
        //    string position = row.Cells[5].Text;
        //    // Store the employee ID in Session for later use
        //    Session["emp_id"] = empID;
        //    //and existing status/position in Session
        //    Session["emp_status"] = status;
        //    Session["emp_position"] = position;

        //    // updateEmployee();
        //    // Get the employee status and position from the second cell and sixth cell in the row
        //    drd_empPosition.SelectedValue = (string)Session["emp_position"];
        //    drd_empStatus.SelectedValue = (string)Session["emp_status"];

        //}
        //protected void btnupdateData_Click(object sender, EventArgs e)
        //{
        //    // Show the modal popup
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#editModal').modal();", true);

        //    // Get the admin ID from the session
        //    string idno = (string)Session["idno"];
        //    int adminId = int.Parse(idno);

        //    // Get the GridViewRow that contains the clicked button
        //    Button btn = (Button)sender;
        //    GridViewRow row = (GridViewRow)btn.NamingContainer;

        //    // Get the employee ID from the first cell in the row
        //    int empID = int.Parse(row.Cells[2].Text);
        //    string status = row.Cells[1].Text;
        //    string position = row.Cells[5].Text;
        //    // Store the employee ID in Session for later use
        //    Session["emp_id"] = empID;
        //    //and existing status/position in Session
        //    Session["emp_status"] = status;
        //    Session["emp_position"] = position;

        //    // updateEmployee();
        //    // Get the employee status and position from the second cell and sixth cell in the row
        //    drd_empStatus.Text = (string)Session["emp_position"];
        //    drd_empStatus.SelectedValue = (string)Session["emp_status"];

        //    // Get the new status and position from the DropDownList in the modal popup
        //    string newPosition = drd_empPosition.SelectedValue;
        //    string newStatus = drd_empStatus.SelectedValue;

        //    // Retrieve the existing employee object from the database
        //    FirebaseResponse response = twoBigDB.Get("EMPLOYEES/" + empID);
        //    Employee existingEmp = response.ResultAs<Employee>();

        //    // Create a new employee object with the updated data
        //    Employee updatedEmp = new Employee
        //    {
        //        adminId = existingEmp.adminId,
        //        emp_id = existingEmp.emp_id,
        //        emp_firstname = existingEmp.emp_firstname,
        //        emp_midname = existingEmp.emp_midname,
        //        emp_lastname = existingEmp.emp_lastname,
        //        emp_gender = existingEmp.emp_gender,
        //        emp_email = existingEmp.emp_email,
        //        emp_birthdate = existingEmp.emp_birthdate,
        //        emp_contactnum = existingEmp.emp_contactnum,
        //        emp_emergencycontact = existingEmp.emp_emergencycontact,
        //        emp_dateHired = existingEmp.emp_dateHired,
        //        dateAdded = existingEmp.dateAdded,
        //        emp_address = existingEmp.emp_address,
        //        emp_pass = existingEmp.emp_pass,
        //        emp_role = existingEmp.emp_role,
        //        emp_status = existingEmp.emp_status,
        //        //dateUpdated = DateTime.UtcNow
        //    };

        //    // Update the fields that have changed
        //    if (!string.IsNullOrEmpty(newPosition))
        //    {
        //        updatedEmp.emp_role = newPosition;
        //    }
        //    if (!string.IsNullOrEmpty(newStatus))
        //    {
        //        updatedEmp.emp_status = newStatus;
        //    }

        //    // Update the existing employee object in the database
        //    response = twoBigDB.Update("EMPLOYEES/" + empID, updatedEmp);

        //    // Rebind the GridView
        //    //DisplayTable();

        //    // Show success message
        //    Response.Write("<script>alert ('Employee " + empID + " has been successfully updated!');  location.reload(); window.location.href = '/Admin/EmployeeRecord.aspx';</script>");

        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "$('#editModal').modal('hide');", true);
        //}

        //    private void updateEmployee()
        //{
        //    // Get the employee ID from SessionState
        //    int empID = (int)Session["emp_id"];
        //    // Get the employee status and position from the second cell and sixth cell in the row
        //    drd_empPosition.SelectedValue = (string)Session["emp_position"];
        //    drd_empStatus.SelectedValue = (string)Session["emp_status"];

        //    // Get the new status and position from the DropDownList in the modal popup
        //    string newPosition = drd_empPosition.SelectedValue;
        //    string newStatus = drd_empStatus.SelectedValue;

        //    // Retrieve the existing employee object from the database
        //    FirebaseResponse response = twoBigDB.Get("EMPLOYEES/" + empID);
        //    Employee existingEmp = response.ResultAs<Employee>();

        //    // Create a new employee object with the updated data
        //    Employee updatedEmp = new Employee
        //    {
        //        adminId = existingEmp.adminId,
        //        emp_id = existingEmp.emp_id,
        //        emp_firstname = existingEmp.emp_firstname,
        //        emp_midname = existingEmp.emp_midname,
        //        emp_lastname = existingEmp.emp_lastname,
        //        emp_gender = existingEmp.emp_gender,
        //        emp_email = existingEmp.emp_email,
        //        emp_birthdate = existingEmp.emp_birthdate,
        //        emp_contactnum = existingEmp.emp_contactnum,
        //        emp_emergencycontact = existingEmp.emp_emergencycontact,
        //        emp_dateHired = existingEmp.emp_dateHired,
        //        dateAdded = existingEmp.dateAdded,
        //        emp_address = existingEmp.emp_address,
        //        emp_pass = existingEmp.emp_pass,
        //        emp_role = existingEmp.emp_role,
        //        emp_status = existingEmp.emp_status,
        //        dateUpdated = DateTime.UtcNow
        //    };

        //    // Update the fields that have changed
        //    if (!string.IsNullOrEmpty(newPosition))
        //    {
        //        updatedEmp.emp_role = newPosition;
        //    }
        //    if (!string.IsNullOrEmpty(newStatus))
        //    {
        //        updatedEmp.emp_status = newStatus;
        //    }

        //    // Update the existing employee object in the database
        //    response = twoBigDB.Update("EMPLOYEES/" + empID, updatedEmp);

        //    // Rebind the GridView
        //    DisplayTable();

        //    // Show success message
        //    Response.Write("<script>alert ('Employee " + empID + " has been successfully updated!');</script>");

        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "$('#editModal').modal('hide');", true);
        //}


        //protected void btnupdateData_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Get the employee ID from SessionState
        //        int empID = (int)Session["emp_id"];

        //        //// Get the GridViewRow that contains the clicked button
        //        //Button btn = (Button)sender;
        //        //GridViewRow row = (GridViewRow)btn.NamingContainer;

        //        //// Get the employee ID from the first cell in the row
        //        // empID = int.Parse(row.Cells[2].Text);

        //        // Get the employee status and position from the second cell and sixth cell in the row
        //        drd_empPosition.SelectedValue = (string)Session["emp_status"];
        //        drd_empStatus.SelectedValue = (string)Session["emp_position"];

        //        // Get the new status and position from the DropDownList in the modal popup
        //        string newPosition = drd_empPosition.SelectedValue;
        //        string newStatus = drd_empStatus.SelectedValue;

        //        // Retrieve the existing employee object from the database
        //        FirebaseResponse response = twoBigDB.Get("EMPLOYEES/" + empID);
        //        Employee existingEmp = response.ResultAs<Employee>();

        //        // Create a new employee object with the updated data
        //        Employee updatedEmp = new Employee
        //        {
        //            adminId = existingEmp.adminId,
        //            emp_id = existingEmp.emp_id,
        //            emp_firstname = existingEmp.emp_firstname,
        //            emp_midname = existingEmp.emp_midname,
        //            emp_lastname = existingEmp.emp_lastname,
        //            emp_gender = existingEmp.emp_gender,
        //            emp_email = existingEmp.emp_email,
        //            emp_birthdate = existingEmp.emp_birthdate,
        //            emp_contactnum = existingEmp.emp_contactnum,
        //            emp_emergencycontact = existingEmp.emp_emergencycontact,
        //            emp_dateHired = existingEmp.emp_dateHired,
        //            dateAdded = existingEmp.dateAdded,
        //            emp_address = existingEmp.emp_address,
        //            emp_pass = existingEmp.emp_pass,
        //            emp_role = existingEmp.emp_role,
        //            emp_status = existingEmp.emp_status,
        //            dateUpdated = DateTime.UtcNow
        //        };

        //        // Update the fields that have changed
        //        if (!string.IsNullOrEmpty(newPosition))
        //        {
        //            updatedEmp.emp_role = newPosition;
        //        }
        //        if (!string.IsNullOrEmpty(newStatus))
        //        {
        //            updatedEmp.emp_status = newStatus;
        //        }

        //        // Update the existing employee object in the database
        //        response = twoBigDB.Update("EMPLOYEES/" + empID, updatedEmp);

        //        // Rebind the GridView
        //        DisplayTable();

        //        // Show success message
        //        Response.Write("<script>alert ('Employee " + empID + " has been successfully updated!');</script>");

        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "$('#editModal').modal('hide');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<pre>" + ex.ToString() + "</pre>");
        //    }
        //}


        //protected void btnupdateData_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int idno = (int)Session["idno"];
        //        // Get the employee ID from the session
        //        // int empId = (int)Session["emp_id"];
        //        // Get the employee ID from ViewState
        //        int empID = (int)Session["emp_id"];

        //        // Reconstruct the GridViewRow object using the index stored in ViewState
        //        //int index = (int)ViewState["SelectedRowIndex"];
        //        //employee_id = (int)GridView1.DataKeys[row.RowIndex]["employee_id"];
        //        //GridViewRow row = .Rows[index];

        //        //int index = (int)ViewState["SelectedRowIndex"];
        //        //GridViewRow row = GridView1.Rows[index];
        //        //int employeeId = (int)GridView1.DataKeys[row.RowIndex]["EMPLOYEE ID"];

        //        // Get the employee ID from the CommandArgument property of the button
        //        //   Button btn = (Button)sender;
        //        //int empID = Convert.ToInt32(btn.CommandArgument.Trim());
        //        //int empID = GridView1.SelectedRow.Cells[1].Text;
        //        // Get the order ID and selected row from ViewState
        //        //int empID = (int)ViewState["emp_id"];
        //        //GridViewRow row = (GridViewRow)ViewState["SelectedRow"];

        //        // Get the new status from the DropDownList in the modal popup
        //        string newPosition = drd_empPosition.SelectedValue;
        //        string newStatus = drd_empStatus.SelectedValue;

        //        // Retrieve the existing employee object from the database
        //        FirebaseResponse response = twoBigDB.Get("EMPLOYEES/" + empID);
        //        Employee existingEmp = response.ResultAs<Employee>();

        //        // Get the updated status and position values from the form
        //        //string status = drd_empStatus.SelectedValue;
        //        //string position = drd_empPosition.SelectedValue;

        //        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('" + status + "', '" + position + "');", true);

        //        // Create a new employee object with the updated data
        //        Employee updatedEmp = new Employee
        //        {
        //            adminId = existingEmp.adminId,
        //            emp_id = existingEmp.emp_id,
        //            emp_firstname = existingEmp.emp_firstname,
        //            emp_midname = existingEmp.emp_midname,
        //            emp_lastname = existingEmp.emp_lastname,
        //            emp_gender = existingEmp.emp_gender,
        //            emp_email = existingEmp.emp_email,
        //            emp_birthdate = existingEmp.emp_birthdate,
        //            emp_contactnum = existingEmp.emp_contactnum,
        //            emp_emergencycontact = existingEmp.emp_emergencycontact,
        //            emp_dateHired = existingEmp.emp_dateHired,
        //            dateAdded = existingEmp.dateAdded,
        //            emp_address = existingEmp.emp_address,
        //            emp_pass = existingEmp.emp_pass,
        //            emp_role = existingEmp.emp_role,
        //            emp_status = existingEmp.emp_status,
        //            dateUpdated = DateTime.UtcNow
        //        };

        //        // Update the fields that have changed
        //        if (!string.IsNullOrEmpty(newPosition))
        //        {
        //            updatedEmp.emp_role = newPosition;
        //        }
        //        if (!string.IsNullOrEmpty(newStatus))
        //        {
        //            updatedEmp.emp_status = newStatus;
        //        }

        //        // Update the existing employee object in the database
        //        response = twoBigDB.Update("EMPLOYEES/" + empID, updatedEmp);

        //        // Rebind the GridView
        //        DisplayTable();

        //        // Show success message
        //        Response.Write("<script>alert ('Employee " + empID + " has been successfully updated!');</script>");

        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "$('#editModal').modal('hide');", true);

        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<pre>" + ex.ToString() + "</pre>");
        //    }
        //}

        //protected void btnupdate_Click(object sender, EventArgs e)
        //{
        //    // int empId = (int)Session["emp_id"];
        //    string position = drd_empPosition.SelectedValue;
        //    string status = drd_empStatus.SelectedValue;
        //    try
        //    {

        //        // Get the admin ID from the session
        //        string idno = (string)Session["idno"];
        //        int adminId = int.Parse(idno);

        //        // Get the employee ID from the CommandArgument property of the button
        //        //Button btn = (Button)sender;
        //        //string empID = Convert.ToString(btn.CommandArgument.Trim());

        //        // Retrieve the existing employee object from the database
        //        FirebaseResponse response = twoBigDB.Get("EMPLOYEES/");
        //        Employee existingEmp = response.ResultAs<Employee>();

        //        Session["emp_id"] = existingEmp.emp_id;

        //        int empId = (int)Session["emp_id"];

        //        // Create a new employee object with the updated data
        //        Employee updatedEmp = new Employee
        //        {
        //            adminId = adminId,
        //            emp_id = existingEmp.emp_id,
        //            emp_firstname = existingEmp.emp_firstname,
        //            emp_midname = existingEmp.emp_midname,
        //            emp_lastname = existingEmp.emp_lastname,
        //            emp_gender = existingEmp.emp_gender,
        //            emp_email = existingEmp.emp_email,
        //            emp_birthdate = existingEmp.emp_birthdate,
        //            emp_contactnum = existingEmp.emp_contactnum,
        //            emp_emergencycontact = existingEmp.emp_emergencycontact,
        //            emp_dateHired = existingEmp.emp_dateHired,
        //            dateAdded = existingEmp.dateAdded,
        //            emp_address = existingEmp.emp_address,
        //            emp_pass = existingEmp.emp_pass,
        //            emp_role = existingEmp.emp_role,
        //            emp_status = existingEmp.emp_status
        //        };

        //        // Update the fields that have changed
        //        if (!string.IsNullOrEmpty(position))
        //        {
        //            updatedEmp.emp_status = drd_empStatus.SelectedValue;
        //        }
        //        if (!string.IsNullOrEmpty(status))
        //        {
        //            updatedEmp.emp_role = drd_empPosition.SelectedValue;
        //        }

        //        updatedEmp.dateUpdated = DateTime.UtcNow;

        //        // Update the existing employee object in the database
        //        response = twoBigDB.Update("EMPLOYEES/" + empId, updatedEmp);

        //        // Rebind the GridView
        //        DisplayTable();

        //        Response.Write("<script>alert ('Employee " + empId + " has been successfully updated!');</script>");
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<pre>" + ex.ToString() + "</pre>");
        //    }
        //}
    }
}