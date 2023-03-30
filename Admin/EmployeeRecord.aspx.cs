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
                //DisplayID();
                DisplayTable();
                displayLogs();
            }

            //Button btnUpdate = new Button();
            //btnUpdate.ID = "btnUpdate";
            //btnUpdate.Text = "Update";
            //btnUpdate.Click += new EventHandler(btnUpdate_Click); // add breakpoint here

        }

        private void displayLogs()
        {
            string idno = (string)Session["idno"];
            //  int empId = (int)Session["emp_id"];

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response;
            response = twoBigDB.Get("USERLOG/" + idno);
            UserLogs user = response.ResultAs<UserLogs>(); //Database result

            Session["userIdnum"] = idno;
            Session["dateLogs"] = user.dateLogs;


            //if (idno == empId)
            //{
            //lblidnum.Text = idno;
            //lblDateTime.Text = user.dateLogs.ToString();
            //}

        }

        //private void DisplayID()
        //{
        //    string employee_id = (string)Session["idno"];

        //    //int idno = int.Parse(employee_id);
        //    FirebaseResponse response;
        //    //"ADMIN/" + data.emp_id + "/Employees/"
        //    response = twoBigDB.Get("ADMIN/" + employee_id + "/Employees/");
        //    Model.Employee obj = response.ResultAs<Model.Employee>();
        //    var json = response.Body;
        //    Dictionary<string, Model.Employee> list = JsonConvert.DeserializeObject<Dictionary<string, Model.Employee>>(json);


        //    foreach (KeyValuePair<string, Model.Employee> entry in list)
        //    {
        //        //ListBoxEmployeeRecord.Items.Add(entry.Value.emp_id.ToString());
        //        if (entry.Value.emp_status == "Active")
        //        {
        //            ListBoxEmployeeRecord.Items.Add(entry.Value.emp_id.ToString());
        //        }
        //        else if (entry.Value.emp_status == "Inactive")
        //        {
        //            ListBox1.Items.Add(entry.Value.emp_id.ToString());
        //        }

        //    }
        //}

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
            employeesTable.Columns.Add("DATE HIRED");
            employeesTable.Columns.Add("ADDRESS");
            employeesTable.Columns.Add("DATE ADDED");

            if (response != null && response.ResultAs<Employee>() != null)
            {
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {

                    employeesTable.Rows.Add(entry.emp_status, entry.emp_id,
                                         entry.emp_firstname + " " + entry.emp_lastname, entry.emp_gender, entry.emp_role,
                                         entry.emp_contactnum, entry.emp_dateHired, entry.emp_address, entry.dateAdded);
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
        // STORE/ADD DATA
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string idno = (string)Session["idno"];
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
                    dateAdded = DateTime.UtcNow
                };

                SetResponse response;
                // Employee Records = tablename, emp_id = key ( PK? )
                // response = twoBigDB.Set("Employees/" + data.emp_id, data);
                //  response = twoBigDB.Set("EMPLOYEES/" + data.emp_firstname + " " + data.emp_lastname, data);//Store Data to database 
                response = twoBigDB.Set("EMPLOYEES/" + data.emp_id, data);

                Employee obj = response.ResultAs<Employee>();//Database Result

                Response.Write("<script> alert('Employee: " + data.emp_firstname + " " + data.emp_lastname + " successfully added!'); </script>");
            }
            catch (Exception ex)
            {
                Response.Write("<pre>" + ex.ToString() + "</pre>");

            }
        }

        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Get the selected employee's ID from the GridView
        //        if (GridView1.SelectedRow == null)
        //        {
        //            // show an error message and return
        //            return;
        //        }
        //        // Get the selected employee's ID from the GridView
        //        string empId = GridView1.SelectedRow.Cells[1].Text;

        //        // Retrieve the employee's data from the database
        //        FirebaseResponse response = twoBigDB.Get("EMPLOYEES/" + empId);
        //        Employee employee = response.ResultAs<Employee>();

        //        // Update the status and position
        //        employee.emp_status = drd_empStatus.SelectedItem.Text;
        //        employee.emp_role = drd_empPosition.SelectedItem.Text;

        //        // Save the changes back to the database
        //        FirebaseResponse updateResponse = twoBigDB.Update("EMPLOYEES/" + empId, employee);

        //        // Refresh the GridView
        //        DisplayTable();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<pre>" + ex.ToString() + "</pre>");
        //    }
        //}


        //UPDATE EMPLOYEE DATA
        //protected void btnupdate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Get the admin ID from the session
        //        string idno = (string)Session["idno"];
        //        int adminId = int.Parse(idno);

        //        // Get the employee ID from the CommandArgument property of the button
        //        Button btn = (Button)sender;
        //        string empID = Convert.ToString(btn.CommandArgument.Trim());

        //        // Retrieve the existing employee object from the database
        //        FirebaseResponse response = twoBigDB.Get("EMPLOYEES/" + empID);
        //        Employee existingEmp = response.ResultAs<Employee>();

        //        // Set the session variable for the employee ID
        //        // Session["emp_id"] = empID;

        //        // Set the values for the modal
        //        string status = existingEmp.emp_status;
        //        string position = existingEmp.emp_role;
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('" + status + "', '" + position + "');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<pre>" + ex.ToString() + "</pre>");
        //    }
        //}

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        // Set the GridView's EditIndex property to the index of the row being edited
        //        // GridView1.EditIndex = e.NewEditIndex;

        //        if (e.CommandName == "Edit")
        //        {
        //            string empId = e.CommandArgument.ToString();
        //            // Perform action to show modal and retrieve employee information
        //            // Retrieve the existing employee object from the database
        //            FirebaseResponse response = twoBigDB.Get("EMPLOYEES/" + empId);
        //            Employee existingEmp = response.ResultAs<Employee>();

        //            // Get the updated status and position values from the form
        //            string status = drd_empStatus.SelectedValue;
        //            string position = drd_empPosition.SelectedValue;

        //            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('" + status + "', '" + position + "');", true);

        //            // Create a new employee object with the updated data
        //            Employee updatedEmp = new Employee
        //            {
        //                adminId = existingEmp.adminId,
        //                emp_id = existingEmp.emp_id,
        //                emp_firstname = existingEmp.emp_firstname,
        //                emp_midname = existingEmp.emp_midname,
        //                emp_lastname = existingEmp.emp_lastname,
        //                emp_gender = existingEmp.emp_gender,
        //                emp_email = existingEmp.emp_email,
        //                emp_birthdate = existingEmp.emp_birthdate,
        //                emp_contactnum = existingEmp.emp_contactnum,
        //                emp_emergencycontact = existingEmp.emp_emergencycontact,
        //                emp_dateHired = existingEmp.emp_dateHired,
        //                dateAdded = existingEmp.dateAdded,
        //                emp_address = existingEmp.emp_address,
        //                emp_pass = existingEmp.emp_pass,
        //                emp_role = existingEmp.emp_role,
        //                emp_status = existingEmp.emp_status,
        //                dateUpdated = DateTime.UtcNow
        //            };

        //            // Update the fields that have changed
        //            if (!string.IsNullOrEmpty(position))
        //            {
        //                updatedEmp.emp_role = position;
        //            }
        //            if (!string.IsNullOrEmpty(status))
        //            {
        //                updatedEmp.emp_status = status;
        //            }

        //            // Update the existing employee object in the database
        //            response = twoBigDB.Update("EMPLOYEES/" + empId, updatedEmp);

        //            // Rebind the GridView
        //            DisplayTable();

        //            // Show success message
        //            Response.Write("<script>alert ('Employee " + empId + " has been successfully updated!');</script>");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<pre>" + ex.ToString() + "</pre>");
        //    }
        //}

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            
            // Show the modal popup
          //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#editModal').modal();", true);

            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

            // Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the employee ID from the first cell in the row
            int empID = int.Parse(row.Cells[2].Text);
            string status = row.Cells[1].Text;
            string position = row.Cells[5].Text;
            // Store the employee ID in Session for later use
            Session["emp_id"] = empID;
            //and existing status/position in Session
            Session["emp_status"] = status;
            Session["emp_position"] = position;

            // updateEmployee();
            // Get the employee status and position from the second cell and sixth cell in the row
            drd_empPosition.SelectedValue = (string)Session["emp_position"];
            drd_empStatus.SelectedValue = (string)Session["emp_status"];

        }
        protected void btnupdateData_Click(object sender, EventArgs e)
        {
            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            int adminId = int.Parse(idno);

            // Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the employee ID from the first cell in the row
            int empID = int.Parse(row.Cells[2].Text);
            string status = row.Cells[1].Text;
            string position = row.Cells[5].Text;
            // Store the employee ID in Session for later use
            Session["emp_id"] = empID;
            //and existing status/position in Session
            Session["emp_status"] = status;
            Session["emp_position"] = position;

            // updateEmployee();
            // Get the employee status and position from the second cell and sixth cell in the row
            drd_empStatus.Text = (string)Session["emp_position"];
            drd_empStatus.SelectedValue = (string)Session["emp_status"];

            // Get the new status and position from the DropDownList in the modal popup
            string newPosition = drd_empPosition.SelectedValue;
            string newStatus = drd_empStatus.SelectedValue;

            // Retrieve the existing employee object from the database
            FirebaseResponse response = twoBigDB.Get("EMPLOYEES/" + empID);
            Employee existingEmp = response.ResultAs<Employee>();

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
                dateUpdated = DateTime.UtcNow
            };

            // Update the fields that have changed
            if (!string.IsNullOrEmpty(newPosition))
            {
                updatedEmp.emp_role = newPosition;
            }
            if (!string.IsNullOrEmpty(newStatus))
            {
                updatedEmp.emp_status = newStatus;
            }

            // Update the existing employee object in the database
            response = twoBigDB.Update("EMPLOYEES/" + empID, updatedEmp);

            // Rebind the GridView
            DisplayTable();

            // Show success message
            Response.Write("<script>alert ('Employee " + empID + " has been successfully updated!');</script>");

          //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "$('#editModal').modal('hide');", true);
        }

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