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
       // public string CusSecurityQuestion { get; set; }


    }
    public class Order
    {
        public int orderID { get; set; }
        public int admin_ID { get; set; }
        public int cusId { get; set; }
        public int driverId { get; set; }
        public string order_DeliveryTypeValue { get; set; }
        public decimal order_InitialAmount { get; set; }
        public string order_OrderStatus { get; set; }
        public string order_OrderTypeValue { get; set; }
        public string order_ProductName { get; set; }
        public int order_Quantity { get; set; }
        public string order_ReservationDate { get; set; }
        public string order_StoreName { get; set; }
        public string order_OrderMethod { get; set; }
        public string order_WaterPrice { get; set; }
        public string order_unit { get; set; }
        public string order_size { get; set; }
        public string order_choosenSwapOption { get; set; }
        public string order_TotalAmount { get; set; }
        public DateTimeOffset dateOrderAccepted { get; set; }
        public DateTimeOffset datePaymentReceived { get; set; }
    }
}