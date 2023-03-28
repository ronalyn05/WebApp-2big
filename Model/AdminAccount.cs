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
        public double addLattitude { get; set; }
        public double addLongitude { get; set; }
        public byte[] proof { get; set; }
        public string operatingHrs { get; set; }
        public string status { get; set; }
        public string businessDays { get; set; }
        public DateTimeOffset dateAdded { get; set; }
    }

    public class SubscribedPlan
    {
        public int adminID { get; set; }
        public DateTimeOffset subStart { get; set; }
        public string subPlan { get; set; }
        public string subDescription { get; set; }
        public int price { get; set; }
        public DateTimeOffset subEnd { get; set; }

    }

    public class Employee
    {
        public int adminId { get; set; }
        public int emp_id { get; set; }
        public string emp_lastname { get; set; }
        public string emp_firstname { get; set; }
        public string emp_pass { get; set; }
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
    public class TankSupply
    {
        public int tankId { get; set; }
        public int adminId { get; set; }
        public string tankUnit { get; set; }
        public string tankVolume { get; set; }
        public string tankBalance { get; set; }
        public DateTime dateAdded { get; set; }
    }


    public class otherProducts
    {
        public int adminId { get; set; }
        public int other_productId { get; set; }
        public string other_productName { get; set; }
        public string other_productUnit { get; set; }
        public string other_productSize { get; set; }
        public string other_productPrice { get; set; }
        public string other_productStock { get; set; }
        public string other_productDiscount { get; set; }
        public string other_productImage { get; set; }
        public DateTime dateAdded { get; set; }
    }
    public class DeliveryDetails
    {
        public int deliveryId { get; set; }
        public int adminId { get; set; }
        public string deliveryType { get; set; }
        public string deliveryFee { get; set; }
        public string deliveryDistance { get; set; }
        public string expressEstimatedTime { get; set; }
        public string orderType { get; set; }
        public string orderMethod { get; set; }
       // public string status { get; set; }
        public DateTime dateAdded { get; set; }
    }
    public class ProductRefill
    {
        public int pro_refillId { get; set; }
        public int adminId { get; set; }
        public string pro_refillWaterType { get; set; }
        public string pro_Image { get; set; }
        public string pro_refillUnit { get; set; }
        public string pro_refillSize { get; set; }
        public string pro_refillPrice { get; set; }
        public DateTime dateAdded { get; set; }
    }
   
    public class AdminNotification
    {
        public int adminnotificationID { get; set; }
        public int orderID { get; set; }
        public string orderStatus { get; set; }
    }
    public class WalkInOrders
    {
        public int orderNo { get; set; }
        public string productName { get; set; }
        public string productSize { get; set; }
        public int productQty { get; set; }
        public decimal productPrice { get; set; }
        public decimal productDiscount { get; set; }
        public decimal totalAmount { get; set; }
        public string orderType { get; set; }
        public DateTimeOffset dateAdded { get; set; }
    }
}




