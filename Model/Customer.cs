﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class Customer
    {
        public int customerID { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string birthdate { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
       // public string CusSecurityQuestion { get; set; }


    }
}