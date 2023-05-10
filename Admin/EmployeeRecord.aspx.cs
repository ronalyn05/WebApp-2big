﻿using System;
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
            }
            

        }

        //WORKING BUT NEED TO BE MODIFIED
        private void DisplayTable()
        {
            string idno = (string)Session["idno"];
            // int adminId = int.Parse(idno);

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
            Dictionary<string, Employee> employeelist = response.ResultAs<Dictionary<string, Employee>>();
            var filteredList = employeelist.Values.Where(d => d.adminId.ToString() == idno).OrderByDescending(d => d.dateAdded);

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
            // employeesTable.Columns.Add("AVAILABILITY");
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
                // Loop through the orders and add them to the DataTable
                foreach (var entry in filteredList)
                {
                    string dateAdded = entry.dateAdded == DateTimeOffset.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateUpdated = entry.dateUpdated == DateTimeOffset.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    string dateStatusModified = entry.statusDateModified == DateTimeOffset.MinValue ? "" : entry.statusDateModified.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    employeesTable.Rows.Add(entry.emp_status, entry.emp_id, entry.emp_firstname + " " + entry.emp_midname + " " + entry.emp_lastname,
                        entry.emp_gender, entry.emp_role,  entry.emp_contactnum, entry.emp_email, entry.emp_dateHired, entry.emp_address, entry.emp_emergencycontact,
                        dateAdded, entry.addedBy, dateUpdated, entry.updatedBy, dateStatusModified, entry.status_ModifiedBy);

                    //employeesTable.Rows.Add(entry.emp_status, entry.emp_id,
                    //                     entry.emp_firstname + " " + entry.emp_lastname, entry.emp_gender, entry.emp_role,
                    //                     entry.emp_contactnum, entry.emp_email, entry.emp_dateHired, entry.emp_availability, entry.emp_address, 
                    //                     entry.emp_emergencycontact, entry.dateAdded, entry.addedBy);
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
                    Response.Write("<script>alert('Password must be 8-20 characters long and contain at least 1 letter, 1 number, and 1 special character.'); </script>");
                    return;
                }

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
                    emp_gender = drdgender.Text,
                    emp_address = txtaddress.Text,
                    emp_contactnum = txtcontactnum.Text,
                    emp_email = txtemail.Text,
                    emp_pass = txtpass.Text,
                    emp_dateHired = txtdateHired.Text,
                    emp_emergencycontact = txtemergencycontact.Text,
                    emp_role = drdrole.Text,
                    emp_status = Drd_status.Text,
                    addedBy = name,
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
                    userActivity = "ADDED EMPLOYEE DETAILS"
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
        protected void btnUpdateEmpRecord_Click(object sender, EventArgs e)
        {
            //  generate a random number for employee logged
            Random rnd = new Random();
            int idnum = rnd.Next(1, 10000);

            // Get the admin ID from the session
            string idno = (string)Session["idno"];
            string name = (string)Session["fullname"];
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
                string contactnumber = txt_contactNumber.Text;
                string email = txtEmail_address.Text;
                 
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
                if (!string.IsNullOrEmpty(newPosition) && newPosition != existingEmp.emp_role)  
                {
                    updatedEmp.emp_role = newPosition;
                    updatedEmp.dateUpdated = DateTimeOffset.UtcNow;
                    updatedEmp.updatedBy = name;
                }
                else if(!string.IsNullOrEmpty(contactnumber) && contactnumber != existingEmp.emp_contactnum)
                {
                    updatedEmp.emp_contactnum = contactnumber;
                    updatedEmp.dateUpdated = DateTimeOffset.UtcNow;
                    updatedEmp.updatedBy = name;
                }
                else if (!string.IsNullOrEmpty(email) && email != existingEmp.emp_email)
                {
                    updatedEmp.emp_email = email;
                    updatedEmp.dateUpdated = DateTimeOffset.UtcNow;
                    updatedEmp.updatedBy = name;
                }

                //if emp status is already inactive
                if (existingEmp.emp_status == "Inactive")
                {
                    Response.Write("<script>alert ('Error: This employee is inactive and cannot be updated.'); location.reload(); window.location.href = '/Admin/EmployeeRecord.aspx';</script>");
                    
                    return;
                }

                if (!string.IsNullOrEmpty(newStatus) && newStatus != existingEmp.emp_status)
                {
                    updatedEmp.emp_status = newStatus;
                    updatedEmp.statusDateModified = DateTimeOffset.UtcNow;
                    updatedEmp.status_ModifiedBy = name;
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
        //SEARCH CERTAIN EMPLOYEE REPORT
        protected void btnSearchEmployee_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#view').modal();", true);

            string idno = (string)Session["idno"];
            try
            {
                string empSearch = txtSearch.Text;

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
                        string dateAdded = entry.dateAdded == DateTimeOffset.MinValue ? "" : entry.dateAdded.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateUpdated = entry.dateUpdated == DateTimeOffset.MinValue ? "" : entry.dateUpdated.ToString("MMMM dd, yyyy hh:mm:ss tt");
                        string dateStatusModified = entry.statusDateModified == DateTimeOffset.MinValue ? "" : entry.statusDateModified.ToString("MMMM dd, yyyy hh:mm:ss tt");

                        employeeTable.Rows.Add(entry.emp_id, entry.emp_firstname + " " + entry.emp_midname + " " + entry.emp_lastname,
                            entry.emp_gender, entry.emp_role, entry.emp_status, entry.emp_contactnum, entry.emp_email,
                            entry.emp_dateHired, entry.emp_address, entry.emp_emergencycontact, dateAdded, entry.addedBy, dateUpdated, entry.updatedBy,
                            dateStatusModified, entry.status_ModifiedBy);
                    }
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
        //SEARCH EMPLOYEE DETAILS TO UPDATE
        protected void btnSearchEmpDetails_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#edit').modal();", true);

            string idno = (string)Session["idno"];
            try
            {
                string empSearch = txtempId.Text;

                // Check if the employee ID is valid
                if (string.IsNullOrEmpty(empSearch))
                {
                    Response.Write("<script>alert ('Please enter a valid employee ID !');</script>");
                    return;
                }
                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("EMPLOYEES");
                Dictionary<string, Employee> employeeList = response.ResultAs<Dictionary<string, Employee>>();

                // Filter the employee list based on the search query
                if (response != null && response.ResultAs<Employee>() != null)
                {
                    var filteredList = employeeList.Values.Where(d => d.adminId.ToString() == idno && (d.emp_id.ToString() == empSearch));

                    

                    //var filteredList = employeeList.Values.Where(d => d.adminId.ToString() == idno && (d.emp_id.ToString() == empSearch ||
                    //    d.emp_firstname.ToLower().Contains(empSearch.ToLower())));

                    // Loop through the entries and add them to the DataTable
                    foreach (var entry in filteredList)
                    {
                        txtEmail_address.Text = entry.emp_email;
                        txt_contactNumber.Text = entry.emp_contactnum;
                        drd_empPosition.SelectedValue = entry.emp_role;
                        drd_empStatus.SelectedValue = entry.emp_status;
                    }

                }
                else
                {
                    lblMessage.Text = "No employee data found for the given search query.";
                }

            }
            catch (Exception ex)
            {
                // Show error message
                Response.Write("<script>alert ('An error occurred while processing your request.');</script>" + ex.Message);
            }
        }

    }
}