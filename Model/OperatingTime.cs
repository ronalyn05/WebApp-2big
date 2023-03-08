using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class OperatingTime
    {
        public string operatingHrs { get; set; }
        public string status { get; set; }
        public string businessDays { get; set; }
        public DateTimeOffset DateAdded { get; set; }
    }
}