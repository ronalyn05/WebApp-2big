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

        public string address { get; set; }
        public DateTime dateApproved { get; set; }
        public DateTime dateRegistered { get; set; }
        public DateTime dateDeclined { get; set; }
        public string userRole { get; set; }


    }



    public class Links
    {
        public List<string> Businessproofs { get; set; }
        public List<string> ValidIDs { get; set; }
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
        public DateTime dateAdded { get; set; }
        public DateTime dateUpdated { get; set; }
    }

    public class SubscribedPlan
    {
        public int adminID { get; set; }
        public DateTime subStart { get; set; }
        public string subPlan { get; set; }
        public string subDescription { get; set; }
        public int price { get; set; }
        public DateTime subEnd { get; set; }


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
        public DateTime dateAdded { get; set; }
        public string updatedBy { get; set; }
        public DateTime dateUpdated { get; set; }
        public string status_ModifiedBy { get; set; }
        public DateTime statusDateModified { get; set; }
    }

    public class TankSupply
    {
        public int tankId { get; set; }
        public int adminId { get; set; }
        public string tankUnit { get; set; }
        public string tankVolume { get; set; }
        public string tankBalance { get; set; }
        public string addedBy { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime dateUpdated { get; set; }
    }


    public class thirdpartyProducts
    {
        public int adminId { get; set; }
        public int thirdparty_productId { get; set; }
        public string offerType { get; set; }
        public string thirdparty_productName { get; set; }
        public string thirdparty_productUnitVolume { get; set; }
        public string thirdparty_productQty { get; set; }
        public string thirdparty_productPrice { get; set; }
        public string thirdparty_unitStock { get; set; }
        public string thirdparty_qtyStock { get; set; }
        public string thirdparty_productDiscount { get; set; }
        public string thirdparty_productImage { get; set; }
        public string addedBy { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime dateUpdated { get; set; }
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

        public string vehicle1MinQty { get; set; }
        public string vehicle2MinQty { get; set; }
        public string vehicle3MinQty { get; set; }
        public string vehicle4MinQty { get; set; }

        public string vehicle1MaxQty { get; set; }
        public string vehicle2MaxQty { get; set; }
        public string vehicle3MaxQty { get; set; }
        public string vehicle4MaxQty { get; set; }

        public string vehicle1stat { get; set; }
        public string vehicle2stat { get; set; }
        public string vehicle3stat { get; set; }
        public string vehicle4stat { get; set; }

        public int vehicle1ID { get; set; }
        public int vehicle2ID { get; set; }
        public int vehicle3ID { get; set; }
        public int vehicle4ID { get; set; }

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
        public DateTime expressdateAdded { get; set; }
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
        public DateTime reservationdateAdded { get; set; }
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
        public string pro_refillUnitVolume { get; set; }
        public string pro_refillQty { get; set; }
        public string pro_refillPrice { get; set; }
        public string pro_stockUnit { get; set; }
        public string pro_stockQty { get; set; }
        public string pro_stockBalance { get; set; }
        public string pro_discount { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime dateUpdated { get; set; }
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
        public string productDiscount { get; set; }
        public decimal totalAmount { get; set; }
        public string orderType { get; set; }
        public DateTime dateAdded { get; set; }
        public string addedBy { get; set; }
    }
    public class UsersLogs
    {
        public int logsId { get; set; }
        public int userIdnum { get; set; }
        public string userFullname { get; set; }
        public string userActivity { get; set; }
        public DateTime activityTime { get; set; }
        public string role { get; set; }

    }
    public class DriversLog
    {
        public int logsId { get; set; }
        public int driverId { get; set; }
        public int admin_ID { get; set; }
        public DateTime date { get; set; }
        public string actions { get; set; }
        public string driverName { get; set; }
        public string role { get; set; }

    }

    public class RewardSystem
    {
        public int rewardId { get; set; }
        public int adminId { get; set; }
        public string rewardWaysToEarn { get; set; }
        public string rewardPointsToEarn { get; set; }
        public string reward_minRange_perAmount { get; set; }
        public string reward_maxRange_perAmount { get; set; }
        public DateTime rewardsDateAdded { get; set; }
        public string addedBy { get; set; }
    }
    public class DiscounCoupon
    {
        public int couponId { get; set; }
        public int adminId { get; set; }
        public string couponName { get; set; }
        public int couponDiscountValue { get; set; }
        public int couponPointsRequiredToClaim { get; set; }
        public string couponDescription { get; set; }
        public string couponAppliedToProductOffers { get; set; }
        public string couponAppliedTo_productRefillUnitSizes { get; set; }
        public string couponAppliedTo_thirdpartyProductUnitSizes { get; set; }
        public DateTime couponExpirationFrom { get; set; }
        public DateTime couponExpirationTo { get; set; }
        public DateTime couponDateAdded { get; set; }
        public string addedBy { get; set; }
    }

}



