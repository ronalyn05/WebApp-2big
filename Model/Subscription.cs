using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class Subscription
    {
        public string Idno { get; set; }
        public string Lname { get; set; }
        public string Fname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTimeOffset SubsDate { get; set; }
        public string SubType { get; set; }
        public DateTimeOffset SubEnd { get; set; }
    }
}