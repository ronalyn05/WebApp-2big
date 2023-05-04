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
        public string subStatus { get; set; } //subscribed or unsubscribed
        public string currentSubscription { get; set; } //Active or Expired/Inactive
        public string businessProof { get; set; }
        public string validID { get; set; }
        public string businessProofLnk { get; set; }
        public string validIDLnk { get; set; }
        public string address { get; set; }
        public DateTime dateApproved { get; set; }
        public DateTime dateRegistered { get; set; }
        public DateTime dateDeclined { get; set; }
        public string userRole { get; set; }


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
        public string addedBy { get; set; }
        public DateTimeOffset dateAdded { get; set; }
        public string updatedBy { get; set; }
        public DateTimeOffset dateUpdated { get; set; }
    }
    public class TankSupply
    {
        public int tankId { get; set; }
        public int adminId { get; set; }
        public string tankUnit { get; set; }
        public string tankVolume { get; set; }
        public string tankBalance { get; set; }
        public string addedBy { get; set; }
        public DateTimeOffset dateAdded { get; set; }
        public DateTimeOffset dateUpdated { get; set; }
    }


    public class otherProducts
    {
        public int adminId { get; set; }
        public int other_productId { get; set; }
        public string offerType { get; set; }
        public string other_productName { get; set; }
        public string other_productUnit { get; set; }
        public string other_productSize { get; set; }
        public string other_productPrice { get; set; }
        public string other_unitStock { get; set; }
        public int other_qtyStock { get; set; }
        public int? other_productDiscount { get; set; }
        public string other_productImage { get; set; }
        public string addedBy { get; set; }
        public DateTimeOffset dateAdded { get; set; }
        public DateTimeOffset dateUpdated { get; set; }
        public string updatedBy { get; set; }
    }
    public class Delivery
    {

        public int deliveryId { get; set; }
        public int adminId { get; set; }
        public string vehicles { get; set; }
        public string swapOptions { get; set; }
        public string paymentMethods { get; set; }
        public string gcashNumber { get; set; }
        
        public string orderTypes { get; set; }
        public string vehicle1Name { get; set; }
        public string vehicle1Fee { get; set; }
        public string vehicle2Name { get; set; }
        public string vehicle2Fee { get; set; }
        public string vehicle3Name { get; set; }
        public string vehicle3Fee { get; set; }
        public string vehicle4Name { get; set; }
        public string vehicle4Fee { get; set; }
        public string perGallonFee { get; set; }

        //STANDARD
        public int standardID { get; set; }
        public string standistance { get; set; }
        public string stanDeliveryFee { get; set; }

        public string stanDeliverytype { get; set; }
        public string stanDeliveryTime { get; set; }
        public string standardProducts { get; set; }
        //public string stanOrderType { get; set; }
        //public string standardSwapOptions { get; set; }
        public DateTimeOffset standardDateAdded { get; set; }


        //EXRPESS
        public DateTimeOffset expressdateAdded { get; set; }
        public int expressID { get; set; }
        public string exDeliveryFee { get; set; }
        public string exEstimatedDelivery { get; set; }
        public string exDeliveryType { get; set; }
        public int expressDistance { get; set; }
        public string expressProducts { get; set; }
        //public string exOrderType { get; set; }
        //public string expressSwapOptions { get; set; }

        //public DateTime dateAdded { get; set; }

        //RESERVATION DELIVERY
        public int reservationID { get; set; }
        public string resDistanceFree { get; set; }
        public string resDeliveryFee { get; set; }
        public string reserveProducts { get; set; }
        //public string resOrderType { get; set; }
        public string resDeliveryType { get; set; }
        public DateTimeOffset reservationdateAdded { get; set; }
        public string addedBy { get; set; }
        //public string reserveSwapOptions { get; set; }

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
        public string addedBy { get; set; }
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
        public string addedBy { get; set; }

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
        public string addedBy { get; set; }

    }
    public class ProductRefill
    {
        public int pro_refillId { get; set; }
        public int adminId { get; set; }
        public string offerType { get; set; }
        public string pro_refillWaterType { get; set; }
        public string pro_Image { get; set; }
        public string pro_refillUnit { get; set; }
        public string pro_refillSize { get; set; }
        public string pro_refillPrice { get; set; }
        public int? pro_discount { get; set; }
        public DateTimeOffset dateAdded { get; set; }
        public DateTimeOffset dateUpdated { get; set; }
        public string addedBy { get; set; }
        public string updatedBy { get; set; }
    }

    public class WalkInOrders
    {
        public int adminId { get; set; }
        public int orderNo { get; set; }
        public string productName { get; set; }
        public string productUnit { get; set; }
        public string productSize { get; set; }
        public int productQty { get; set; }
        public string productUnitSize { get; set; }
        public decimal productPrice { get; set; }
        public decimal productDiscount { get; set; }
        public decimal totalAmount { get; set; }
        public string orderType { get; set; }
        public DateTimeOffset dateAdded { get; set; }
        public string addedBy { get; set; }
    }
    public class UsersLogs
    {
        public int logsId { get; set; }
        public int userIdnum { get; set; }
        public string userFullname { get; set; }
        public string userActivity { get; set; }
        public DateTimeOffset activityTime { get; set; }

    }

    public class RewardSystem
    {
        public int rewardId { get; set; }
        public int adminId { get; set; }
        public string rewardWaysToEarn { get; set; }
        public decimal rewardPointsToEarn { get; set; }
        public decimal reward_minRange_perAmount { get; set; }
        public decimal reward_maxRange_perAmount { get; set; }
        public DateTimeOffset rewardsDateAdded { get; set; }
        public string addedBy { get; set; }
    }
    public class PromoOffered
    {
        public int promoId { get; set; }
        public int adminId { get; set; }
        public string promoName { get; set; }
        public int promoDiscountValue { get; set; }
        public string promoDescription { get; set; }
        public string promoAppliedToProductOffers { get; set; }
        public string promoAppliedToUnitSizes { get; set; }
        public DateTimeOffset promoExpirationFrom { get; set; }
        public DateTimeOffset promoExpirationTo { get; set; }
        public DateTimeOffset promoDateAdded { get; set; }
        public string addedBy { get; set; }
    }

}



