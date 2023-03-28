using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class EmployeeData
    {
        public int emp_id { get; set; }
        public string emp_lastname { get; set; }
        public string emp_firstname { get; set; }
        public string emp_midname { get; set; }
        public string emp_birthdate { get; set; }
        public string emp_gender { get; set; }
        public string emp_address { get; set; }
        public string emp_contactnum { get; set; }
        public string emp_email { get; set; }
        public string emp_dateHired { get; set; }
        public string emp_emergencycontact { get; set; }
        public string emp_role { get; set; }
        public string emp_status { get; set; }
    }
}