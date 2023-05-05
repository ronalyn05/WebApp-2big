using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRS2big_Web.Model
{
    public class Customer
    {
        public int cusId { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string birthdate { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string cus_status { get; set; }
        public string imageProof { get; set; }
        public string imageSelfie { get; set; }
        public double lattitudeLocation { get; set; }
        public double longitudeLocation { get; set; }
        public DateTime dateApproved { get; set; }
        public DateTime dateDeclined { get; set; }
        public DateTime dateRegistered { get; set; }
        public string userRole { get; set; }
       // public string CusSecurityQuestion { get; set; }


    }
    public class Order
    {
        public int orderID { get; set; }
        public int admin_ID { get; set; }
        public int cusId { get; set; }
        public int driverId { get; set; }
        public string orderPaymentMethod { get; set; }
        public string order_DeliveryTypeValue { get; set; }
        public string order_OrderStatus { get; set; }
        public string order_OrderTypeValue { get; set; }
        public List<OrderProduct> order_Products { get; set; }
        public int order_OverallQuantities { get; set; }
        public string order_ReservationDate { get; set; }
        public string order_StoreName { get; set; }
        public decimal order_TotalAmount { get; set; }
        public string order_RefllSelectedOption { get; set; }
        public DateTimeOffset dateOrderAccepted { get; set; }
        public DateTimeOffset datePaymentReceived { get; set; }
        public DateTimeOffset dateOrderDeclined { get; set; }
        public DateTimeOffset dateOrderDelivered { get; set; }
        public string payment_receivedBy { get; set; }
    }

    public class OrderProduct
    {
        public int order_ProductId { get; set; }
        public string order_ProductName { get; set; }
        public decimal order_ProductPrice { get; set; }
        public decimal order_size { get; set; }
        public string order_unit { get; set; }
        public string qtyPerItem { get; set; }
        public string offerType { get; set; }
    }

    //    public class Order
    //    {
    //        public int orderID { get; set; }
    //        public int admin_ID { get; set; }
    //        public int cusId { get; set; }
    //        public int driverId { get; set; }
    //        public string order_DeliveryTypeValue { get; set; }
    //        public decimal order_InitialAmount { get; set; }
    //        public string order_OrderStatus { get; set; }
    //        public string order_OrderTypeValue { get; set; }
    //        public string order_ProductName { get; set; }
    //        public int order_Quantity { get; set; }
    //        public string order_ReservationDate { get; set; }
    //        public string order_StoreName { get; set; }
    //        public string order_OrderMethod { get; set; }
    //        public string order_WaterPrice { get; set; }
    //        public string order_unit { get; set; }
    //        public string order_size { get; set; }
    //        public string order_choosenSwapOption { get; set; }
    //        public string order_TotalAmount { get; set; }
    //        public DateTimeOffset dateOrderAccepted { get; set; }
    //        public DateTimeOffset datePaymentReceived { get; set; }
    //        public DateTimeOffset dateOrderDeclined { get; set; }
    //        public DateTimeOffset dateOrderDelivered { get; set; }
    //        public string payment_receivedBy { get; set; }
    //}


    // Define the notification message class
    public class NotificationMessage
    {
        public int notificationID{ get; set; }
        public string sender { get; set; }
        public int driverId { get; set; }
        public string body { get; set; }
        public string receiver { get; set; }
        public int orderID { get; set; }
        public string status { get; set; }
       // public string title { get; set; }
        public int admin_ID { get; set; }
        public int cusId { get; set; }
        public DateTimeOffset notificationDate { get; set; }
    }

    public class StoreReview
    {
        public int orderId { get; set; }
        public int adminID { get; set; }
        public int cusId { get; set; }
        public string feedback { get; set; }
        public int ratings { get; set; }
    }

}