<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Deliverydetails.aspx.cs" Inherits="WRS2big_Web.Admin.Deliverydetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
      <div id="pcoded" class="pcoded">
        <div class="pcoded-overlay-box">ELIVER</div>
        <div class="pcoded-container navbar-wrapper">
            <div class="pcoded-main-container">
                <div class="pcoded-wrapper">
 
                    <div class="pcoded-content">
                        <!-- Page-header start -->
                        <div class="page-header">
                            <div class="page-block">
                                <div class="row align-items-center">
                                    <div class="col-md-8">
                                        <div class="page-header-title">
                                            <h5 class="m-b-10">PRODUCTS and DELIVERY MANAGEMENT</h5>
                                            <h6 class="m-b-0">2BiG: Water Refilling Station Management System</h6>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="breadcrumb-title">
                                            <li class="breadcrumb-item">
                                                <a href="/Admin/AdminIndex"> <i class="fa fa-home"></i> </a>
                                            </li>
                                            <li class="breadcrumb-item">
                                                <a href="/Admin/AdminIndex">Dashboard</a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                           <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body" style="font-size:16px">
                             <div class="page-wrapper">
                              <!-- page content -->
                               <div class="right_col" role="main">
                                <div class="">
                                  <div class="clearfix">
                                       <%-- add product button--%>
                                    <br />
                                        <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".managedetails"><i class="fa fa-plus"></i> Delivery Details</button> 
                                       <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".manage"><i class="fa fa-plus"></i> Add Delivery Types</button>
                                       <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".managePayment"><i class="fa fa-plus"></i> Add Payment Methods</button> 

                                      <br /><br />
                                      <%--VIEW BUTTON --%>
                                          &nbsp;

<%--                                      MODAL FOR PAYMENT METHOD--%>
                                       <div class="modal fade managePayment" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-md">
                                            <div class="modal-content">
                                            <form id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="paymentModal"></h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-12 col-sm-12 ">
                                            <div class="x_content">
                                           <%-- <div class="item form-group">--%>
                                                <h4 style="color:black;font-family:Bahnschrift"> PAYMENT METHODS</h4>

                                                 <div class="col-md-12 col-sm-12" style="font-size:20px">
                                                    <h4 style="font-size:16px;color:black"> Please choose the Payment Methods you want to use for the Orders</h4>
                                                    <p style="font-size:16px;color:black;">"These payment methods are applicable to all orders made in your station regardless of its Delivery Type"</p> <hr />
                                                        <asp:CheckBoxList ID="paymentsCheckBox" runat="server" Height="40px" Width="300px" >
                                                       <%--<asp:ListItem Selected="True">-----Choose One-----</asp:ListItem>--%>
                                                       <asp:ListItem style="font-size:20px;color:black" Text="Cash on Delivery" Value="CashOnDelivery"></asp:ListItem>
                                                       <asp:ListItem style="font-size:20px;color:black" Text="Gcash" Value="Gcash" onclick="gcashPayment();" id="gcashPayment"></asp:ListItem>
                                                       <asp:ListItem style="font-size:20px;color:black" Text="Reward Points" Value="Points" ></asp:ListItem>
                                                   </asp:CheckBoxList>

                                               </div><br />
                                                 <div class="col-md-12 col-sm-12" id="gcashChecked" style="font-size:20px;display:none">
                                                    <h4 style="font-size:16px;color:black">Please enter your GCASH Registered number:</h4>
                                                   <h4 style="font-size:16px;color:black"> Reminder: This will be the number where the customer can send their payments</h4>
                                                         <asp:TextBox ID="gcashnum" runat="server" TextMode="Number" Placeholder="09123456789" Width="364px"></asp:TextBox>

                                               </div><br />
                                              
                                                  </div>
                                                </div>
                                                <div class="modal-footer">
                                                <%--  BUTTON ADD PAYMENT METHOD--%>
                                               <asp:Button ID="paymentButton" runat="server" Text="Confirm" class="btn btn-primary btn-sm" OnClick="paymentButton_Click"/>
                                            </div>
                                              </div>
                                            </div>
                                             <%--  </form>--%>
                                             </div>
                                           </div>
                                    <%--  SCRIPT FOR GCASH NUMBER TEXTBOX--%>
                                                <script>
                                                    function gcashPayment() {
                                                        var gcashNumber = document.getElementById("gcashPayment");
                                                        var fields = document.getElementById("gcashChecked");
                                                        if (gcashNumber.checked) {
                                                            fields.style.display = "none";
                                                        }
                                                        else {
                                                            fields.style.display = "block";
                                                        }


                                                    }
                                                </script>

                                    <%-- MODAL FOR Delivery details--%>
                                       <div class="modal fade manage col-xl-8 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-md col-xl-10 col-md-10">
                                            <div class="modal-content col-xl-10 col-md-10" style="/*background-color:red;*/ margin-left:370px">
                                            <div id="demo-form2" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel2">Delivery Types</h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-18 col-sm-18 ">
                                            <div class="x_content">

                                                <h5 style="color:black;font-family:Bahnschrift"> Set delivery details offered to customers here:</h5>
                                                <h6> Note: Please check ONE 'Delivery Type' you offer to your business and fill all the informations needed <br /> You can only create ONE delivery type at a time</h6>
                                                <hr />
                                                <br />
                                            <div class="col-md-12 col-sm-12 ">
                                                  <%--Delivery Type--%>
                                                  <strong>Choose the Delivery Type you provide in your business:</strong>
                                                    <asp:CheckBoxList ID="radDevType" runat="server" OnSelectedIndexChanged="radDevType_SelectedIndexChanged" AutoPostBack="false">
                                                        <asp:ListItem Value="Standard" ID="standardRadio" onclick="displayStandardOptions(); disableOtherRadios()"> &nbsp; Standard </asp:ListItem>
                                                        <asp:ListItem Value="Reservation" ID="reserveRadio" onclick="displayReserveOptions(); disableOtherRadios()">  &nbsp; Reservation</asp:ListItem>
                                                        <asp:ListItem Value="Express" ID="expressRadio" onclick="displayExpressOptions(); disableOtherRadios()">  &nbsp; Express</asp:ListItem>
                                                    </asp:CheckBoxList>


                                            </div>
                                                <!--OPTIONS FOR STANDARD WHEN CLICKED--> 
                                                <div ID="standardCheckedDIV" style="display:none"> <hr />
                                                    <strong style="font-size:18px">Standard Options</strong>
                                                        <div  class="col-md-12 col-sm-12 ">
                                                                <strong>Time Schedule for Delivery:</strong> <br />
                                                                <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                   <h7>Set the time schedule for your standard delivery</h7><br />
                                                                 <h7>From:</h7>
                                                                <asp:TextBox ID="standardSchedFrom" textmode="Time" Width="100px" runat="server"></asp:TextBox> 
                                                                 <h7>To:</h7> 
                                                            <asp:TextBox ID="standardSchedTo" textmode="Time" Width="100px"  runat="server"></asp:TextBox>   
                                                        </div><br />
                                                        
                                                         <%--  delivery fee--%>
                                                        <div class="col-md-12 col-sm-12 ">
                                                                <strong>Distance in km for FREE Delivery:</strong> <br />
                                                                <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                <asp:TextBox ID="FreeDelivery" Width="364px" Placeholder="Set the maximum distance for your FREE delivery" runat="server"></asp:TextBox>
                                                         </div> <br />
                                                        <div class="col-md-12 col-sm-12 ">
                                                                <strong>Delivery FEE:</strong> <br />
                                                                <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                <asp:TextBox ID="DeliveryFee" Width="364px" TextMode="Number" Placeholder="Set the Delivery fee:" runat="server"></asp:TextBox>
                                                         </div> <br />
<%--                                                        <div class="col-md-12 col-sm-12 " id="deliveryTypesPanel">
                                                                <strong>Vehicle Types:</strong> <br />
                                                                <asp:Label ID="vehicleTypes" style="font-size:16px" runat="server"></asp:Label>
                                                                <asp:TextBox ID="vehicleFee" Width="364px" TextMode="Number" Placeholder="Fee:" runat="server"></asp:TextBox>
                                                         </div>--%>
                                                    <br />
                                                       <div class="col-md-12 col-sm-12">
                                                  
                                                           <strong>What products do you offer for the STANDARD delivery?</strong>
                                                                <asp:CheckBoxList ID="OrderMethod" runat="server">
                                                                    <asp:ListItem  Value="Refill" onclick="refillOptions();" ID="refillSwapOptions">  &nbsp; Refill</asp:ListItem>
                                                                    <%--<asp:ListItem Text="New Gallon" Value="New Gallon"></asp:ListItem>--%>
                                                                     <asp:ListItem  Value="other products"> &nbsp; other Products</asp:ListItem>
                                                                </asp:CheckBoxList>
                                                       </div>  <br />
                                                </div>

                                                <!--SCRIPT FOR STANDARD WHEN CLICKED-->
                                                <script>
                                                    function displayStandardOptions() {
                                                        var standard = document.getElementById("standardRadio");
                                                        var stanfields = document.getElementById("standardCheckedDIV");

                                                        if (standard.checked) {
                                                            stanfields.style.display = "none";
                                                            // Disable other options
                                                            //document.getElementById("reserveChcked").disabled = true;
                                                            //document.getElementById("expressChcked").disabled = true;

                                                        } else {

                                                            stanfields.style.display = "block";
                                                            // Enable other options
                                                            //document.getElementById("reserveChcked").disabled = false;
                                                            //document.getElementById("expressChcked").disabled = false;
                                                        }
                                                    }
                                                </script>
                                                


                                                <!--OPTIONS FOR RESERVATION WHEN CLICKED--> 
                                                <div ID="reserveCheckedDIV" style="display:none"><hr /> 
                                                    <strong style="font-size:18px">Reservation Options</strong>
                                                        <div  class="col-md-12 col-sm-12 ">
                                                                <strong>Distance in km for FREE Delivery:</strong> <br />
                                                                <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                <asp:TextBox ID="resFreeDel"  Width="364px" Placeholder="Set the maximum distance for your FREE delivery" runat="server"></asp:TextBox>
                                                     
                                                        </div>
                                                        <div class="col-md-12 col-sm-12 ">
                                                                <strong>Delivery FEE:</strong><br />
                                                                <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                <asp:TextBox ID="resDelFee" Width="364px" TextMode="Number" Placeholder="Set the Delivery fee:" runat="server"></asp:TextBox>
                                                         </div> <br />
                                                       <div class="col-md-12 col-sm-12">
                                                  
                                                           <strong>What products do you offer for the RESERVATION?</strong>
                                                                <asp:CheckBoxList ID="reserveOrderMethod" runat="server">
                                                                    <asp:ListItem Value="Refill" onclick="reserveRefillOptions();" ID="reserveSwapOptions"> &nbsp; Refill</asp:ListItem>
                                                                   <%-- <asp:ListItem Text="New Gallon" Value="New Gallon"></asp:ListItem>--%>
                                                                     <asp:ListItem  Value="other products">  &nbsp; other Products</asp:ListItem>
                                                                </asp:CheckBoxList>
                                                       </div> 

<%--                                                       <div class="col-md-12 col-sm-12">
                                                
                                                           <strong>Choose types of service you offer to customers:</strong>
                                                                <asp:CheckBoxList ID="reserveOrderType" runat="server">
                                                                    <asp:ListItem Text="Pick-Up" Value="PickUp"></asp:ListItem>
                                                                    <asp:ListItem Text="Delivery" Value="Delivery"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                       </div>--%>
                                                </div>
                                                

                                                <!--SCRIPT FOR RESERVATION WHEN CLICKED-->
                                                <script> 
                                                    function displayReserveOptions() {
                                                        var reserve = document.getElementById("reserveRadio");
                                                        var reservefields = document.getElementById("reserveCheckedDIV");

                                                        if (reserve.checked) {
                                                            reservefields.style.display = "none";
                                                        } else {
                                                            reservefields.style.display = "block";

                                                        }
                                                    }
                                                </script>
                            

                                                <!--OPTIONS FOR EXPRESS WHEN CLICKED--> 
                                                <div ID="expressCheckedDIV" style="display:none"> <hr />
                                                     <strong style="font-size:18px">Express Options</strong>
                                                        <div  class="col-md-12 col-sm-12 ">
                                                                <strong>Estimated time in minutes for Express Delivery:</strong> <br />
                                                                <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                <asp:TextBox ID="estimatedTime" Width="364px" Placeholder="Enter Express Delivery Estimated time" runat="server"></asp:TextBox>
                                                     
                                                        </div>
                                                        
                                                         <%--  delivery fee--%>
                                                        <div class="col-md-12 col-sm-12 ">
                                                                <strong>Express Delivery fee:</strong><br />
                                                                <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                <asp:TextBox ID="expressdeliveryFee" TextMode="Number" Width="364px" Placeholder="Enter the specific amount for the delivery fee" runat="server"></asp:TextBox>
                                                         </div> <br />
                                                       <div class="col-md-12 col-sm-12">
                                                  
                                                           <strong>What products do you offer for the EXPRESS delivery?</strong>
                                                                <asp:CheckBoxList ID="expressOrderMethod" runat="server">
                                                                    <asp:ListItem Value="Refill" onclick="expressRefillOptions();" ID="expressSwapOptions">  &nbsp; Refill</asp:ListItem>
                                                                   <%-- <asp:ListItem Text="New Gallon" Value="New Gallon"></asp:ListItem>--%>
                                                                     <asp:ListItem Value="other products">  &nbsp; other Products</asp:ListItem>
                                                                </asp:CheckBoxList>
                                                       </div>  

<%--                                                       <div class="col-md-12 col-sm-12">
                                                
                                                           <strong>Choose types of service you offer to customers:</strong>
                                                                <asp:CheckBoxList ID="expressOrderType" runat="server">
                                                                    <asp:ListItem Text="Pick-Up" Value="PickUp"></asp:ListItem>
                                                                    <asp:ListItem Text="Delivery" Value="Delivery"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                       </div>--%>
                                                </div>
                                                

                                                 <!--SCRIPT FOR EXPRESS WHEN CLICKED-->
                                                    <script>
                                                        function displayExpressOptions() {

                                                            var express = document.getElementById("expressRadio");
                                                            var expressFields = document.getElementById("expressCheckedDIV");

                                                            if (express.checked) {
                                                                expressFields.style.display = "none";


                                                            } else {

                                                                expressFields.style.display = "block";
                                                                
                                                            }

                                                        }
                                                    </script>

                                               <%-- SCRIPT TO DISABLE OTHER RADIO BUTTONS--%>
                                                <script>
                                                        function disableOtherRadios() {
                                                          var radios = document.getElementsByName("devType");
                                                          for (var i = 0; i < radios.length; i++) {
                                                            if (radios[i].id !== this.id) {
                                                              radios[i].checked = false;
                                                            }
                                                          }
                                                        }


                                                </script>

                                                  </div>
                                                </div>
                                                <div class="modal-footer">
                                               <%-- set data button--%>
                                               <asp:Button ID="btnDeliverydetails" runat="server" Text="Add details" class="btn btn-primary btn-sm" OnClick="btnDeliverydetails_Click" AutoPostBack="false" />
                                                </div>
                                              </div>
                                            </div>
                                               </div>
                                             </div>
                                            </div>
                                       

                                        <%-- MODAL FOR Delivery details--%>
                                       <div class="modal fade managedetails col-xl-8 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-md col-xl-10 col-md-10">
                                            <div class="modal-content col-xl-10 col-md-10" style="/*background-color:red;*/ margin-left:370px">
                                            <div id="demo-form2" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel3">Delivery Details</h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-18 col-sm-18 ">
                                            <div class="x_content">
                                                <h5 style="color:black;font-family:Bahnschrift"> Set delivery details offered to customers here:</h5>
                                                <h6> Note: You can't change the delivery details once it is already added</h6>
                                                <hr />

                                                <br />
                                                       <!--ORDER TYPE SELECTION-->
                                                       <div class="col-md-12 col-sm-12" id="orderTypeDiv">
                                                
                                                           <strong>Choose the order type you offer to customers:</strong>
                                                                <asp:CheckBoxList ID="orderTypes" runat="server">
                                                                    <asp:ListItem Value="PickUp">&nbsp; Pick-Up</asp:ListItem>
                                                                    <asp:ListItem Value="Delivery" id="delivery" onclick="vehicleTypeOptions();">&nbsp; Delivery</asp:ListItem>
                                                                </asp:CheckBoxList>
                                                       </div>
                                                <br />
                                                <!--OPTIONS FOR DELIVERY WHEN CLICKED-->
                                                <div ID="deliveryChecked" style="display:none">
                                                       <div class="col-md-12 col-sm-12" >
                                                           <strong>Manage the vehicle types you use for the delivery:</strong> <br /> <br />
                                                         
                                                           <asp:TextBox ID="vehicle1Name" Width="360px" Height="40px" Placeholder="Vehicle Name:" runat="server"></asp:TextBox>
                                                            <asp:TextBox ID="vehicle1Fee" Width="250px" Height="40px" TextMode="Number" Placeholder="Vehicle Fee:" runat="server" style="margin-left:50px"></asp:TextBox>
                                                           <br /> <br />
                                                            <asp:TextBox ID="vehicle2Name" Width="360px" Height="40px" Placeholder="Vehicle Name:" runat="server" ></asp:TextBox>
                                                                <asp:TextBox ID="vehicle2Fee" Width="250px" Height="40px" TextMode="Number" Placeholder="Vehicle Fee:" runat="server" style="margin-left:50px;"></asp:TextBox>
                                                               <br /> <br />
                                                               <asp:TextBox ID="vehicle3Name" Width="360px" Height="40px" Placeholder="Vehicle Name:" runat="server" ></asp:TextBox>
                                                                <asp:TextBox ID="vehicle3Fee" Width="250px" Height="40px" TextMode="Number" Placeholder="Vehicle Fee:" runat="server" style="margin-left:50px;"></asp:TextBox>
                                                               <br /> <br />
                                                                <asp:TextBox ID="vehicle4Name" Width="360px" Height="40px" Placeholder="Vehicle Name:" runat="server"></asp:TextBox>
                                                                <asp:TextBox ID="vehicle4Fee" Width="250px" Height="40px" TextMode="Number" Placeholder="Vehicle Fee:" runat="server" style="margin-left:50px;"></asp:TextBox>
                                                               <br />
                                                       </div>
                                                 </div>
                                                           <div ID="anothervehicle" style="display:none">
                                                               
                                                           </div>

                                                <hr />
                                                <!--SCRIPT FOR DELIVERY WHEN CLICKED-->
                                                <script>
                                                    function vehicleTypeOptions() {
                                                        var delivery = document.getElementById("delivery");
                                                        var stanfields = document.getElementById("deliveryChecked");

                                                        if (delivery.checked) {
                                                            stanfields.style.display = "none";
                                                           
                                                        } else {

                                                            stanfields.style.display = "block";
                                                            
                                                        }
                                                    }
                                                </script>
                                                <br />
                                                <div id="swapOptions"> 
                                                    <strong> Swap Gallon Options</strong>
                                                    <h6> Note: This will give your customers an option on how you can swap their gallons for the REFILL</h6>
                                                                <asp:CheckBoxList ID="swapOptionItems" runat="server">
                                                                     <asp:ListItem Value="Swap Without Conditions">  &nbsp; Swap with no Conditions</asp:ListItem>
                                                                    <asp:ListItem Value="Swap With Conditions"> &nbsp; Swap with Conditions</asp:ListItem>
                                                                   <asp:ListItem Value="Request Pick-up" ID="pickupPerGallon" onclick="perGallonFee();"> &nbsp; Request Gallon Pickup</asp:ListItem>
                                                                </asp:CheckBoxList>
                                                <div id="perGallon" style="display:none">
                                                    <h8 style="margin-left:50px;">per Gallon Fee:</h8><br />
                                                     <asp:TextBox ID="perGallonFee"  Width="250px" Height="40px" TextMode="Number" runat="server" style="margin-left:80px;"></asp:TextBox>
                                                </div>
                                                    <br /><hr />
                                                       <h8> <strong >LEGEND:</strong> <br /> 
                                                           <strong> Pick-up:</strong> <br /> The customer will pick-up their order in the station. <br />
                                                           <strong> Delivery:</strong> <br /> The order of the customer will be delivered to their delivery address. <br />
                                                           <%--This gives the customer an option to give 'conditions' on the gallons that they want to receive when ordering water refill--%>
                                                           <strong> Swap with Condition:</strong> <br />Lets the customers leave their personal 'conditions' or note about the swapped gallon that they want to receive when ordering water refill. <br />
                                                           <strong> Swap without Condition:</strong> <br /> Let YOU, the admin to decide on what gallon should be swapped with the customer gallon when ordering water refill. <br />
                                                            <strong>Request Gallon Pick-up:</strong> <br /> Gives the customers an option to request for a 'gallon pick-up' if they dont want their gallons to be swapped. <br />
                                                           <strong> per Gallon Fee: </strong> <br /> 
                                                           </h8>

                                                </div>
<br />

                                                <!--SCRIPT FOR REQUEST PICK-UP WHEN CLICKED-->
                                                <script>
                                                    function perGallonFee() {
                                                        var delivery = document.getElementById("pickupPerGallon");
                                                        var stanfields = document.getElementById("perGallon");

                                                        if (delivery.checked) {
                                                            stanfields.style.display = "none";

                                                        } else {

                                                            stanfields.style.display = "block";

                                                        }
                                                    }
                                                </script>
                                                  </div>
                                                </div>
                                                <div class="modal-footer">
                                               <%-- set data button--%>
                                               <asp:Button ID="vehicleAdded" runat="server" Text="Submit" class="btn btn-primary btn-sm" Onclick="vehicleAdded_Click" AutoPostBack="false" />
                                                </div>
                                              </div>
                                            </div>
                                               </div>
                                             </div>
                                            </div>
                                        <br /><br />
                                    <%--PAGE CONTENTS FOR LISTBOX--%> 
                                               <div class="row">
                                                   <div class="col-xl-3 col-md-12">
                                                    <div class="card ">
                                                        <div class="card-header">
                                                            <div class="card-header-right">
                                                                <ul class="list-unstyled card-option">
                                                                    <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                                    <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                    <li><i class="fa fa-minus minimize-card"></i></li>
                                                                    <li><i class="fa fa-refresh reload-card"></i></li>
                                                                    <li><i class="fa fa-trash close-card"></i></li>
                                                                </ul>
                                                            </div>
                                                            <%--<h5> Tank Supply:</h5>--%>
                                                            <asp:Label ID="Label1" runat="server" Text="Tank Supply" Font-Bold="true" Font-Size="Large" Width="364px"></asp:Label>
                                                            <%--<button type="button" style="font-size:14px; width: 154px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".addSupply"><i class="fa fa-plus"></i> Add Tank Supply</button>--%>

                                                        </div>
                                                        <%-- TANK SUPPLY STARTS HERE --%>
                                              <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Label ID="label2" Font-Bold="true" runat="server" Text="Date:" Font-Size="Large" Width="349px"></asp:Label>
                                                                  <asp:Label ID="lblDate" runat="server" CssClass="text-c-blue" Font-Bold="true" Font-Size="18px" Width="349px"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="label4" Font-Bold="true" runat="server" Text="Tank Supply of the day:" Font-Size="Large" Width="349px"></asp:Label>
                                                                  <asp:Label ID="lbltankSupply" runat="server" CssClass="text-c-blue" Font-Bold="true" Font-Size="18px" Width="349px"></asp:Label>
                                                                 <br />
                                                                <asp:Label ID="label3" Font-Bold="true" runat="server" Text="Remaining Supply:" Font-Size="Large" Width="349px"></asp:Label>
                                                                <asp:Label ID="lblremainingSupply" Font-Bold="true" runat="server" CssClass="text-c-blue" Font-Size="18px" Width="349px"></asp:Label>
                                                            
                                                            </div>
                                                        </div>
                                                    </div>
                                                    </div>
                                                </div><%-- TANK SUPPLY ENDS HERE --%>
                                               </div>
                                              </div>
                                            <div class="col-xl-9 col-md-12">
                                                <div class="card">
                                                    <div class="card-header">
                                                        
                                                        <asp:Label ID="Label7" runat="server" Font-Bold="true" Font-Size="Large" Text="DELIVERY DETAILS"></asp:Label>
                                                        <div class="card-header-right">
                                                            <ul class="list-unstyled card-option">
                                                                <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                                <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                <li><i class="fa fa-minus minimize-card"></i></li>
                                                                <li><i class="fa fa-refresh reload-card"></i></li>
                                                                <li><i class="fa fa-trash close-card"></i></li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                    <div class="card-block">
                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                <hr />
                                                            
                                                             
                                                                <asp:Label ID="lblProductData" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                           
                                                             <br />

                                                                <%-- DELIVERY DETAILS NI DIRI--%>

                                                <asp:GridView runat="server" ID="gridDeliveryDetails" CellPadding="3" Width="975px" CssClass="auto-style1" SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                      <Columns>
                                                        <asp:TemplateField>
                                                          <ItemTemplate>
                                                          <asp:Button runat="server"  ID="viewButton" OnClick="viewButton_Click" Text="View" style="background-color:transparent;font-size:16px;"  class="active btn waves-effect text-center"/> 
                                                          </ItemTemplate>
                                                        </asp:TemplateField>
                                                      </Columns>
                                                      <FooterStyle BackColor="White" ForeColor="#000066" />
                                                      <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                      <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                                                      <RowStyle Width="200px" ForeColor="#000066" />
                                                      <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                      <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                      <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                      <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                      <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                    </asp:GridView>

                                            
                                                <%--Gridview ends here--%>
                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
                                                           
                                                        </div>
                                                    </div>
                                                    <div class="card-footer">
                                                                    <%-- <asp:Button ID="EditBtn" style="font-size:14px;" class="btn btn-primary btn-sm"  runat="server" Text="Edit details" OnClick="btnEdit_Click"/>
                                                                       <asp:Button ID="DeleteBtn" style="font-size:14px;" class="btn btn-danger btn-sm" runat="server"  Text="Delete Product" OnClick="DeleteBtn_Click" /> --%>
                                                    </div>
                                                    <div class="card-block">
                                                        <asp:DropDownList ID="drdDeliverytype" runat="server" CssClass="text-center" Height="40px" Width="364px">
                                                            <%--<asp:ListItem Text="---Select---"></asp:ListItem>--%>
                                                            <%--<asp:ListItem Text="View All" Value="0"></asp:ListItem>--%>
                                                           <asp:ListItem Text="Express" Value="1"></asp:ListItem>
                                                           <asp:ListItem Text="Standard" Value="2"></asp:ListItem>
                                                           <asp:ListItem Text="Reservation" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:Button ID="btnDeliverytype" runat="server" Text="Search" OnClick="btnSearchDeliverytype_Click" CssClass="btn-primary" Height="40px"/>
                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                <hr />
                                                                <asp:Label ID="lblDeliveryType" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                                <asp:Label ID="nullLabel" Font-Size="16px" runat="server" Width="364px"></asp:Label>
                                                             <br /> <br />

                                            <%--GRIDVIEW FOR EXPRESS--%>
                                               <asp:GridView runat="server" ID="expressGridview" CellPadding="3" Width="975px" CssClass="auto-style1" SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                      <Columns>
                                                        <asp:TemplateField>
                                                          <ItemTemplate>
                                                           <%-- <asp:LinkButton ID="selectButton" runat="server" data-toggle="modal" CssClass="fa-edit" data-target=".updateModal" Text="Update" CommandName="Update"/>--%>
                                                                <button type="button" class="btn btn-primary btn-md" data-toggle="modal" data-target=".edit"><i class="fa fa-edit"></i> update</button>
                                                          </ItemTemplate>
                                                        </asp:TemplateField>
                                                      </Columns>
                                                      <FooterStyle BackColor="White" ForeColor="#000066" />
                                                      <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                      <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                                                      <RowStyle Width="200px" ForeColor="#000066" />
                                                      <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                      <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                      <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                      <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                      <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                    </asp:GridView>

                                               
                                                    <%--GRIDVIEW FOR STANDARD--%>
                                                <asp:GridView runat="server" ID="standardGridview" CellPadding="3" Width="975px" CssClass="auto-style1" SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                      <Columns>
                                                        <asp:TemplateField>
                                                          <ItemTemplate>
                                                           <%-- <asp:LinkButton ID="selectButton" runat="server" data-toggle="modal" CssClass="fa-edit" data-target=".updateModal" Text="Update" CommandName="Update"/>--%>
                                                                <button type="button" class="btn btn-primary btn-md" data-toggle="modal" data-target=".edit"><i class="fa fa-edit"></i> update</button>
                                                          </ItemTemplate>
                                                        </asp:TemplateField>
                                                      </Columns>
                                                      <FooterStyle BackColor="White" ForeColor="#000066" />
                                                      <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                      <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                                                      <RowStyle Width="200px" ForeColor="#000066" />
                                                      <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                      <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                      <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                      <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                      <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                    </asp:GridView>
                                                            

                                            <%--GRIDVIEW FOR RESERVATION--%>
                                                <asp:GridView runat="server" ID="reservationGridView" CellPadding="3" Width="975px" CssClass="auto-style1" SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                      <Columns>
                                                        <asp:TemplateField>
                                                          <ItemTemplate>
                                                           <%-- <asp:LinkButton ID="selectButton" runat="server" data-toggle="modal" CssClass="fa-edit" data-target=".updateModal" Text="Update" CommandName="Update"/>--%>
                                                                <button type="button" class="btn btn-primary btn-md" data-toggle="modal" data-target=".edit"><i class="fa fa-edit"></i> update</button>
                                                          </ItemTemplate>
                                                        </asp:TemplateField>
                                                      </Columns>
                                                      <FooterStyle BackColor="White" ForeColor="#000066" />
                                                      <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                      <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                                                      <RowStyle Width="200px" ForeColor="#000066" />
                                                      <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                      <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                      <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                      <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                      <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                    </asp:GridView>

                                            
                                                <%-- </div>--%> <%--Gridview ends here--%>
                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
                                                           
                                                        </div>
                                                    </div>
                                                </div>
                                               </div> 
                                             </div>
                                      <!--PAGE CONTENTS END-->

                                                  </div>

                                               </div>
                                        </div>
                                      </div>
                                    </div>
                                  </div>
                                <br /><br />
                             </div>
                           </div>
                          </div>
                         </div>
                       </div>

</asp:Content>
