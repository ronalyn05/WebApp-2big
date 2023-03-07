using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WRS2big_Web.Model
{
    public class AdminAccount
    {
        public int idno { get; set; }
        public string lname { get; set; }
        public string fname { get; set; }
        public string mname { get; set; }
        public string bdate { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string WRS_Name { get; set; }
        public string pass { get; set; }
        public byte[] profilePic { get; set; }
        public DateTimeOffset subsDate { get; set; }
        public string subType { get; set; }
        public DateTimeOffset subEnd { get; set; }

        //public StorageReference RootReference { get; internal set; }

    }
}

    

   
