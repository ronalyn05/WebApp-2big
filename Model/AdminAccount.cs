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
        public string status { get; set; }
        public string subStatus { get; set; }

    }

    public class RefillingStation
    {
        // public int stationID { get; set; }
        public string stationName { get; set; }
        public string stationAddress { get; set; }
        public double addLattitude { get; set; }
        public double addLongitude { get; set; }
        public string proof { get; set; }
        public string operatingHrsFrom { get; set; }
        public string operatingHrsTo { get; set; }
        public string status { get; set; }
        public string businessDaysFrom { get; set; }
        public string businessDaysTo { get; set; }
        public DateTimeOffset dateAdded { get; set; }
        public DateTimeOffset dateUpdated { get; set; }
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
        public string emp_availability { get; set; }
        public DateTimeOffset dateAdded { get; set; }
    }
    public class TankSupply
    {
        public int tankId { get; set; }
        public int adminId { get; set; }
        public string tankUnit { get; set; }
        public string tankVolume { get; set; }
        public string tankBalance { get; set; }
        public DateTimeOffset dateAdded { get; set; }
        public DateTimeOffset dateUpdated { get; set; }
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
        public DateTimeOffset dateAdded { get; set; }
    }
    public class DeliveryDetails
    {
        public int deliveryId { get; set; }
        public int adminId { get; set; }
        public int standardID { get; set; }
        public string standistance { get; set; }
        public string stanDeliveryFee { get; set; }

        public string stanDeliverytype { get; set; }
        public string stanDeliveryTime { get; set; }
        public string stanOrderMethod { get; set; }
        public string stanOrderType { get; set; }
        public string standardSwapOptions { get; set; }
        public DateTimeOffset standardDateAdded { get; set; }
        public DateTimeOffset reservationdateAdded { get; set; }
        public DateTimeOffset expressdateAdded { get; set; }
        //EXPRESS DELIVERY
        public int expressID { get; set; }
        public string exDeliveryFee { get; set; }
        public string exEstimatedDelivery { get; set; }
        public string exDeliveryType { get; set; }
        public string exOrderMethod { get; set; }
        public string exOrderType { get; set; }
        public string expressSwapOptions { get; set; }
        //public string swapOptions { get; set; }
        //public DateTime dateAdded { get; set; }
        //RESERVATION DELIVERY
        public int reservationID { get; set; }
        public string resDistanceFree { get; set; }
        public string resDeliveryFee { get; set; }
        public string resOrderMethod { get; set; }
        public string resOrderType { get; set; }
        public string resDeliveryType { get; set; }
        public string reserveSwapOptions { get; set; }
        public string paymentMethods { get; set; }
        public string gcashNumber { get; set; }
        //public string swapOptions { get; set; }
        //public DateTime dateAdded { get; set; }

    }

    public class standardDelivery
    {
        public int standardID { get; set; }
        public int standistance { get; set; }
        public int stanDeliveryFee { get; set; }
        public string stanDeliverytype { get; set; }
        public string stanDeliveryTime { get; set; }
        public string stanOrderMethod { get; set; }
        public string stanOrderType { get; set; }
        public string swapOptions { get; set; }
        public DateTime dateAdded { get; set; }
    }

    public class expressDelivery
    {
        public int expressID { get; set; }
        public int exDeliveryFee { get; set; }
        public string exEstimatedDelivery { get; set; }
        public string exDeliveryType { get; set; }
        public string exOrderMethod { get; set; }
        public string exOrderType { get; set; }
        public string swapOptions { get; set; }
        public DateTime dateAdded { get; set; }

    }

    public class reservationDelivery
    {
        public int reservationID { get; set; }
        public int resDistanceFree { get; set; }
        public int resDeliveryFee { get; set; }
        public string resOrderMethod { get; set; }
        public string resOrderType { get; set; }
        public string resDeliveryType { get; set; }
        public string swapOptions { get; set; }
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
        public string pro_discount { get; set; }
        public DateTimeOffset dateAdded { get; set; }
    }

    public class AdminNotification
    {
        public int adminnotificationID { get; set; }
        public int orderID { get; set; }
        public string orderStatus { get; set; }
    }
    public class WalkInOrders
    {
        public int adminId { get; set; }
        public int orderNo { get; set; }
        public string productName { get; set; }
        public string productUnit { get; set; }
        public string productSize { get; set; }
        public int productQty { get; set; }
        public decimal productPrice { get; set; }
        public decimal productDiscount { get; set; }
        public decimal totalAmount { get; set; }
        public string orderType { get; set; }
        public DateTimeOffset dateAdded { get; set; }
    }
    public class UsersLogs
    {
        public int logsId { get; set; }
        public int userIdnum { get; set; }
        public string userFullname { get; set; }
        public int emp_id { get; set; }
        public string empFullname { get; set; }
        public DateTimeOffset empDateAdded { get; set; }
        public int tankId { get; set; }
        public DateTimeOffset tankSupplyDateAdded { get; set; }
        public int other_productId { get; set; }
        public DateTimeOffset otherProductDateAdded { get; set; }
        public int productRefillId { get; set; }
        public DateTimeOffset productrefillDateAdded { get; set; }
        public int deliveryDetailsId { get; set; }
        //public DateTimeOffset deliveryDetailsDateAdded { get; set; }
        public DateTimeOffset standardAdded { get; set; }
        public DateTimeOffset reservationAdded { get; set; }
        public DateTimeOffset expressAdded { get; set; }

        public int orderId { get; set; }
        public DateTimeOffset datePaymentReceived { get; set; }
        public DateTimeOffset dateLogin { get; set; }
        // public DateTimeOffset lastLogin { get; set; }
        public DateTimeOffset dateLogout { get; set; }
        public int cusId { get; set; }
        public DateTimeOffset dateDeclined { get; set; }
        public DateTimeOffset dateOrderAccepted { get; set; }
        public string userActivity { get; set; } // New property for user activity
    }
    public class Reward
    {
        public int rewardId { get; set; }
        public int adminId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int points_required { get; set; }
        public DateTimeOffset promoExpirationFrom { get; set; }
        public DateTimeOffset promoExpirationTo { get; set; }
        public DateTimeOffset rewardsDateAdded { get; set; }
    }

    public class Logs
    {
        public int adminId { get; set; }
        public DateTimeOffset timestamp { get; set; }
        public string action { get; set; }
       // public int count { get; set; }
    }
}



