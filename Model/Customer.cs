﻿using System;
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
        public string cus_status { get; set; }
        public string imageProof { get; set; }
        public string imageSelfie { get; set; }
        public double lattitudeLocation { get; set; }
        public double longitudeLocation { get; set; }
        public DateTime dateApproved { get; set; }
        public DateTime dateDeclined { get; set; }
        public DateTime dateRegistered { get; set; }
        public string userRole { get; set; }
        public List<StationCustomerAccess> orderedStore { get; set; }
        // public string CusSecurityQuestion { get; set; }

        public string FullName => firstName + " " + lastName; // Property that returns the driver's full name

    }
    public class StationCustomerAccess
    {
        public int adminId { get; set; }
      
    }

    public class Order
    {
        public int orderID { get; set; }
        public int admin_ID { get; set; }
        public int cusId { get; set; }
        public int driverId { get; set; }
        public string orderPaymentMethod { get; set; }
        public string orderPaymentMethod2 { get; set; }
        public string order_DeliveryTypeValue { get; set; }
        public string order_OrderStatus { get; set; }
        public string order_OrderTypeValue { get; set; }
        public List<OrderProduct> order_Products { get; set; }
        public int order_overAllQuantities { get; set; }
        public string order_ReservationDate { get; set; }
        public string order_StoreName { get; set; }
        public decimal order_TotalAmount { get; set; }
        public decimal order_InitialAmount { get; set; }
        public string order_GcashProofOfPayment { get; set; }
        public string order_RefillSelectedOption { get; set; }
        // public DateTimeOffset orderDate { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime dateOrderAccepted { get; set; }
        public string orderAcceptedBy { get; set; }
        public DateTime datePaymentReceived { get; set; }
        public string paymentReceivedBy { get; set; }
        public string paymentGcashReceivedBy { get; set; }
        public string paymentPointsReceivedBy { get; set; }
        public DateTime dateOrderDeclined { get; set; }
        public string orderDeclinedBy { get; set; }
        public DateTime dateOrderDelivered { get; set; }
        public DateTime dateDriverAssigned { get; set; }
        public decimal orderDeliveryfee { get; set; }
        public int ordervehiclefee { get; set; }
        public string driverAssignedBy { get; set; }

        public string order_deliveryReservationDeliveryReserveDate { get; set; }
        public string order_deliveryReservationDeliveryReserveTime { get; set; }
        public string order_deliveryReservationDeliveryTypeSelected { get; set; }
    }

    public class OrderProduct
    {
        public int order_ProductId { get; set; }
        public string order_ProductName { get; set; }
        public decimal order_ProductPrice { get; set; }
        public int  pro_refillQty{ get; set; }
        public string pro_refillUnitVolume { get; set; }
        public string qtyPerItem { get; set; }
        public string offerType { get; set; }
        public string order_ProductDiscount { get; set; }
    }


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
        public DateTime notificationDate { get; set; }
    }

    public class StoreReview
    {
        public int orderId { get; set; }
        public int adminID { get; set; }
        public int cusId { get; set; }
        public string customerFirstName { get; set; }
        public string customerLastName { get; set; }
        public string feedback { get; set; }
        public int ratings { get; set; }
        public DateTime reviewedDate { get; set; }
    }

}