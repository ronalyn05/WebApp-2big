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
        public string phone { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public string profile_image { get; set; }

    }

    public class RefillingStation
    {
        public int stationID { get; set; }
        public string stationName { get; set; }
        public string stationAddress { get; set; }
        public string addLattitude { get; set; }
        public string addLongitude { get; set; }
        public byte[] proof { get; set; }
        public string operatingHrs { get; set; }
        public string status { get; set; }
        public string businessDays { get; set; }
        public DateTimeOffset DateAdded { get; set; }
    }

    public class SubscribedPlan
    {
        public DateTimeOffset subStart { get; set; }
        public string subPlan { get; set; }
        public string subDescription { get; set; }
        public int price { get; set; }
        public DateTimeOffset subEnd { get; set; }

    }

    public class Employee
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

    public class Products
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productSize { get; set; }
        public string productPrice { get; set; }
        public string productAvailable { get; set; }
        public string productDiscount { get; set; }
        public string waterRefillSupply { get; set; }
        public byte[] productImage { get; set; }
        public DateTimeOffset DateAdded { get; set; }
    }
    public class DeliveryDetails
    {
        public int deliveryId { get; set; }
        public string deliveryType { get; set; }
        public string deliveryFee { get; set; }
        public string estimatedTime { get; set; }
        public string status { get; set; }

        // public DateTimeOffset DateAdded { get; set; }
    }

    public class AdminNotification
    {
        public int adminnotificationID { get; set; }
        public int orderID { get; set; }
        public string OrderStatus { get; set; }
    }
    public class WalkInOrders
    {
        public int orderNo { get; set; }
        public string productName { get; set; }
        public string productSize { get; set; }
        public string productQty { get; set; }
        public string productPrice { get; set; }
        public string productDiscount { get; set; }
        public string totalAmount { get; set; }
        public string orderType { get; set; }
        public DateTimeOffset dateAdded { get; set; }
    }
}




