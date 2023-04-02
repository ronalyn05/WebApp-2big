using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using WRS2big_Web.Model;

namespace WRS2big_Web.Admin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient twoBigDB;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Connection to database 
            twoBigDB = new FireSharp.FirebaseClient(config);

            // Get the ID of the currently logged-in owner from session state
            string idno = (string)Session["idno"];

            // Retrieve all orders from the ORDERS table
            FirebaseResponse response = twoBigDB.Get("ORDERS");
            Dictionary<string, Order> orderlist = response.ResultAs<Dictionary<string, Order>>();

            // Filter the list of orders by the owner's ID and the order status and delivery type
                List<Order> filteredList = orderlist.Values
                            .Where(d => d.admin_ID.ToString() == idno)
                            .ToList();

            // Compute the total number of orders today
            int totalOrdersToday = filteredList.Count();
            // Compute the total amount of all orders
            decimal totalAmount = 0;
            // Compute the total number of delivery orders
            int totalDeliveryOrders = filteredList.Count(d => d.order_OrderStatus == "Delivered");
            // Compute the total number of reservation orders
            int totalReservationOrders = filteredList.Count(d => d.order_DeliveryTypeValue == "Reservation");

            foreach (Order order in filteredList)
            {
                totalAmount += order.order_InitialAmount;
            }

            // Display the total amount of all orders
            lblTotalSales.Text = totalAmount.ToString();
            lblDeliveries.Text = totalDeliveryOrders.ToString();// Display the total of deliveries
            lblOrders.Text = totalOrdersToday.ToString();// Display the total of all orders
            lblReservations.Text = totalReservationOrders.ToString();// Display the total reservations

            displayTankSupply();
        }
        //THIS DISPLAY THE TANK SUPPLY
        private void displayTankSupply()
        {
            try
            {
                // Get the ID of the currently logged-in owner from session state
                string idno = (string)Session["idno"];

                // Retrieve all orders from the ORDERS table
                FirebaseResponse response = twoBigDB.Get("TANKSUPPLY");
                Dictionary<string, TankSupply> supply = response.ResultAs<Dictionary<string, TankSupply>>();

                // Filter the list of orders by the owner's ID and the order status and delivery type
                var filteredList = supply.Values.SingleOrDefault(d => d.adminId.ToString() == idno && d.dateAdded.Date == DateTime.UtcNow.Date);

                if (filteredList != null)
                {
                    lblDate.Text = filteredList.dateAdded.ToString(("MM/dd/yyyy hh:mm:ss tt"));
                    lbltanksupply.Text = filteredList.tankVolume.ToString() + ' ' + filteredList.tankUnit.ToString();
                }
                else
                {
                    lblDate.Text = "No supply found for today.";
                    lbltanksupply.Text = "";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred while retrieving tank supply data: " + ex.Message + "'); window.location.href = '/Admin/WaterProduct.aspx';</script>");
            }
        }

        //LOGOUT
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.RemoveAll();
            Session["idno"] = null;
            Session["password"] = null;
            Session.Clear();
            Response.Redirect("LandingPage/Index.aspx");
        }
    }
}