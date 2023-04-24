 <%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WaterProduct.aspx.cs" Inherits="WRS2big_Web.Admin.WaterProduct" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Include jQuery and the Timepicker plugin -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-timepicker/1.13.18/jquery.timepicker.min.css">

<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-timepicker/1.13.18/jquery.timepicker.min.js" integrity="sha512-7QDFvrSg50P7i5/lCZ/IM5ozmavhK26X7l3qy/Z3wsSaLKhjGwDd7QPNdlZmepnJVPl0bzmmPqj3qBwtJ1h9cw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>

    <style>
        texts{
            font-size:16px;

        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div id="pcoded" class="pcoded">
        <div class="pcoded-overlay-box"></div>
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
                                            <h5 class="m-b-10">PRODUCTS - WATER </h5>
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
                            <div class="main-body">
                             <div class="page-wrapper">
                              <!-- page content -->
                               <div class="right_col" role="main">
                                <div class="">
                                  <div class="clearfix">
                                       <%-- add product button--%>
                                         <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".add"><i class="fa fa-plus"></i> Add Other Product Offers</button>
                                       <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".set"><i class="fa fa-plus"></i> Add Product Refill Offers</button>
                                       <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".manage"><i class="fa fa-plus"></i> Manage Delivery Details</button>
                                       <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".addtank"><i class="fa fa-plus"></i> Add Tank Supply</button>
                                       <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".managePayment"><i class="fa fa-plus"></i> Manage Payment Methods</button> 
                                         <%--VIEW BUTTON --%>
                                          &nbsp;
                                       <%-- MODAL FOR TANK SUPPLY --%>
                                       <div class="modal fade addtank" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-md">
                                            <div class="modal-content">
                                            <form id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel3">Add Tank Supply</h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-12 col-sm-12 ">
                                            <div class="x_content">
                                           <%-- <div class="item form-group">--%>
                                                <h4 style="color:black;font-family:Bahnschrift"> Set daily amount of tank supply here:</h4>

                                                 <div class="col-md-12 col-sm-12 ">
                                                  <%--tank unit --%>
                                                  <strong>Tank Unit:</strong>
                                                        <asp:DropDownList ID="drdTankUnit" runat="server" Height="40px" Width="300px">
                                                       <%--<asp:ListItem Selected="True">-----Choose One-----</asp:ListItem>--%>
                                                       <asp:ListItem Text="Gallon" Value="gallon" Selected="True"></asp:ListItem>
                                                       <asp:ListItem Text="Liters" Value="liter/s" ></asp:ListItem>
                                                     <asp:ListItem Text="Mililiters" Value="mililiter/s" ></asp:ListItem>
                                                   </asp:DropDownList>
                                               </div>
                                                            <br />
                                              <%--  Tank Size--%>
                                                <div class="col-md-12 col-sm-12 ">
                                                     <strong>Tank Size:</strong>
                                                        <asp:TextBox ID="tankSize" Placeholder="Enter the size of tank water supply" runat="server" Width="300px"></asp:TextBox>
                                                     </div>
                                                         <br />
                                                         
                                                           <%-- <asp:Button ID="AddTanksupply" runat="server" class="btn btn-primary btn-sm" Text="Add Supply" OnClick="btnAddSupply_Click" Width="131px"/>--%>
                                            
                                                  </div>
                                                </div>
                                                <div class="modal-footer">
                                                <%--  BUTTON TANK SUPPLY HERE--%>
                                               <asp:Button ID="btnAddSupply" runat="server" Text="Add Supply" class="btn btn-primary btn-sm" OnClick="btnAddSupply_Click"/>
                                            </div>
                                              </div>
                                            </div>
                                             <%--  </form>--%>
                                             </div>
                                           </div>
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
                                       <%-- MODAL FOR ADD PRODUCT--%>
                                       <div class="modal fade add" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-md">
                                            <div class="modal-content">
                                            <form id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel">Add Other Product</h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-12 col-sm-12 ">
                                            <div class="x_content">
                                           <%-- <div class="item form-group">--%>
                                                <h4 style="color:black;font-family:Bahnschrift"> Set other offered products here:</h4>
                                            <div class="col-md-12 col-sm-12 ">
                                               <%--PRODUCT NAME--%>
                                             <strong>Product Name:</strong>
                                                 <asp:TextBox ID="productName" runat="server" Placeholder="Enter the type of product offered" Width="364px"></asp:TextBox>
                                            </div>
                                                <br />
                                             <div class="col-md-12 col-sm-12">
                                                 <%--PRODUCT NAME--%>
                                                 <strong>Product Image:</strong>
                                                        <%--file upload--%>
                                                        <asp:FileUpload ID="imgProduct" runat="server" Font-Size="Medium" Height="38px" Width="301px"  />
                                              </div>
                                                <br />
                                                <div class="col-md-12 col-sm-12">
                                                  <strong>Product Unit:</strong>
                                                 <asp:DropDownList ID="drdprodUnit" runat="server" Height="40px" Width="364px">
                                                       <%--<asp:ListItem Selected="True">-----Choose One-----</asp:ListItem>--%>
                                                       <asp:ListItem Text="Gallon" Value="gallon" Selected="True"></asp:ListItem>
                                                       <asp:ListItem Text="ML" Value="ml" ></asp:ListItem>
                                                       <asp:ListItem Text="Liters" Value="liter/s" ></asp:ListItem>
                                                   </asp:DropDownList>
                                                     </div>
                                                <br />
                                             <div class="col-md-12 col-sm-12">
                                                  <strong>Product Size:</strong>
                                                 <asp:TextBox ID="productSize" TextMode="Number" Placeholder="Enter size of product" runat="server" Width="364px"></asp:TextBox>
                                              </div>
                                                <br />
                                                <%--PRODUCT PRICE--%>
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <strong>Product Price:</strong>
                                                       <%-- <asp:Label ID="Label1" runat="server" Text="Product Price: "></asp:Label><br />--%>
                                                        <asp:TextBox ID="productPrice" Placeholder="Enter the price of product cost" runat="server" Width="364px"></asp:TextBox>
                                                     </div>
                                                <br />
                                                 <%--PRODUCT Discount--%>
                                                      <div class="col-md-12 col-sm-12 ">
                                                          <strong>Product Discount:</strong> <br />
                                                          <h7>Please enter the discount percentage (%) you offer for this product</h7>
                                                        <asp:TextBox ID="productDiscounts" TextMode="Number" Placeholder="Enter discount offered" runat="server" Width="364px"></asp:TextBox>
                                                          </div>
                                                <br />
                                                        <%--PRODUCT Available--%>
                                                      <div class="col-md-12 col-sm-12 ">
                                                          <strong>Product Stock:</strong>
                                                         <%-- <asp:Label ID="Label6" runat="server" Text="Product Available:"></asp:Label>--%>
                                                        <asp:TextBox ID="productStock" Placeholder="Enter the available stock based on the unit above" Product="Enter the available stock of the product above" runat="server" Width="364px"></asp:TextBox>
                                                          </div>
                                                             <br />
                                                         </div>
                                                  </div>
                                                </div>
                                                <div class="modal-footer">
                                               <%-- add data button--%>
                                               <asp:Button ID="btnAdd" runat="server" Text="Add other Product" class="btn btn-primary btn-sm" OnClick="btnAdd_Click" AutoPostBack="false"/>
                                                <%--<asp:Button ID="btnupdate" class="btn btn-primary" runat="server" Text="Update Data" ValidationGroup="a" OnClick="btnupdate_Click" />
                                                --%></div>
                                              </div>
                                            </div>
                                             <%--  </form>--%>
                                             </div>
                                       <%-- MODAL FOR Set Product Refill Supply--%>
                                      <div class="modal fade set" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-md">
                                            <div class="modal-content">
                                            <form id="demo-form1" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel1"> Product Refill</h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-12 col-sm-12 ">
                                            <div class="x_content">
                                                <h4 style="color:black;font-family:Bahnschrift"> Set offered products refill details here:</h4>
                                                <hr />
                                                <div class="col-md-12 col-sm-12 ">
                                                        <strong>Water Type:</strong>
                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                        <asp:TextBox ID="refillwaterType" Width="364px" Placeholder="Enter type of water offered to customer. Ex: Purified or Alkaline" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                <div class="col-md-12 col-sm-12">
                                                 <%--PRODUCT NAME--%>
                                                 <strong>Product Image:</strong>
                                                        <%--file upload--%>
                                                        <asp:FileUpload ID="prodImage" runat="server" Font-Size="Medium" Height="38px" Width="301px"  />
                                              </div>
                                                <br />
                                                <div class="col-md-12 col-sm-12">
                                                  <strong>Refill Unit:</strong>
                                                 <asp:DropDownList ID="refillUnit" runat="server" Height="40px" Width="364px">
                                                       <%--<asp:ListItem Selected="True">-----Choose One-----</asp:ListItem>--%>
                                                       <asp:ListItem Text="Gallon" Value="gallon" Selected="True"></asp:ListItem>
                                                       <asp:ListItem Text="Liters" Value="liter/s" ></asp:ListItem>
                                                     <asp:ListItem Text="Mililiters" Value="mililiter/s" ></asp:ListItem>
                                                   </asp:DropDownList>
                                                     </div>
                                                <br />
                                                
                                                 <div class="col-md-12 col-sm-12 ">
                                                        <strong>Size:</strong>
                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                        <asp:TextBox ID="refillSize" Width="364px" Placeholder="Enter the size/volume of water dispensed for each customer " runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                 <div class="col-md-12 col-sm-12 ">
                                                        <strong>Price:</strong>
                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                        <asp:TextBox ID="refillPrice" Width="364px" Placeholder="Enter the price of refill cost" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <strong>Discounts</strong> <br />
                                                         <h7>Please enter the discount percentage (%) you offer for this product</h7>
                                                        <asp:TextBox ID="refillDiscount" Width="364px" TextMode="Number" Placeholder="Enter discount offered" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                  </div>
                                                </div>
                                                <div class="modal-footer">
                                               <%-- set data button--%>
                                               <asp:Button ID="btnSet" runat="server" Text="Add Product Refill" class="btn btn-primary btn-sm" OnClick="btnSet_Click" AutoPostBack="false"/>
                                                </div>
                                              </div>
                                            </div>
                                             <%-- </form>--%>
                                             </div>
                                            </div>
                                        <%-- end set product refill --%>


                                    <%-- MODAL FOR Delivery details--%>
                                       <div class="modal fade manage col-xl-8 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-md col-xl-10 col-md-10">
                                            <div class="modal-content col-xl-10 col-md-10" style="/*background-color:red;*/ margin-left:370px">
                                            <form id="demo-form2" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel2">Manage delivery details</h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-18 col-sm-18 ">
                                            <div class="x_content">
                                                <h5 style="color:black;font-family:Bahnschrift"> Set delivery details offered to customers here:</h5>
                                                <h6> Note: Please check ONE 'Delivery Type' you offer to your business and fill all the informations needed <br /> You can only create a delivery type ONCE</h6>
                                                <hr />
                                          
                                                <hr />
                                                <br />
                                                       <!--ORDER TYPE SELECTION-->
                                                       <div class="col-md-12 col-sm-12" id="orderTypeDiv">
                                                
                                                           <strong>Choose the order type you offer to customers:</strong>
                                                                <asp:CheckBoxList ID="orderTypes" runat="server">
                                                                    <asp:ListItem Text="Pick-Up" Value="PickUp"></asp:ListItem>
                                                                    <asp:ListItem Text="Delivery" Value="Delivery"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                       </div>
                                                <br />
                                            <div class="col-md-12 col-sm-12 ">
                                                  <%--Delivery Type--%>
                                                  <strong>Choose the Delivery Type you provide in your business:</strong>
                                                    <asp:CheckBoxList ID="radDevType" runat="server" OnSelectedIndexChanged="radDevType_SelectedIndexChanged" AutoPostBack="false">
                                                        <asp:ListItem Text="Standard" Value="Standard" ID="standardRadio" onclick="displayStandardOptions(); disableOtherRadios()"></asp:ListItem>
                                                        <asp:ListItem Text="Reservation" Value="Reservation" ID="reserveRadio" onclick="displayReserveOptions(); disableOtherRadios()"></asp:ListItem>
                                                        <asp:ListItem Text="Express" Value="Express" ID="expressRadio" onclick="displayExpressOptions(); disableOtherRadios()"></asp:ListItem>
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
                                                         </div>
                                                        <div class="col-md-12 col-sm-12 ">
                                                                <strong>Delivery FEE:</strong> <br />
                                                                <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                <asp:TextBox ID="DeliveryFee" Width="364px" TextMode="Number" Placeholder="Set the Delivery fee:" runat="server"></asp:TextBox>
                                                         </div>
                                                    <br />
                                                       <div class="col-md-12 col-sm-12">
                                                  
                                                           <strong>Choose types of order method you want to offer to customers:</strong>
                                                                <asp:CheckBoxList ID="OrderMethod" runat="server">
                                                                    <asp:ListItem Text="Refill" Value="Refill" onclick="refillOptions();" ID="refillSwapOptions"></asp:ListItem>
                                                                    <asp:ListItem Text="New Gallon" Value="New Gallon"></asp:ListItem>
                                                                     <asp:ListItem Text="Other Products" Value="other products"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                       </div>  <br />
                                                <div id="standardrefillOptions" style="display:none"> 
                                                    <strong> Refill options: <br />Swap Gallon Options for Standard</strong>
                                                    <h6> Note: This will give your customers an option on how you can swap their gallons for the refill</h6>
                                                    <h8>Checking this option gives the customer an option to swap their gallons with no conditions</h8>
                                                                <asp:CheckBoxList ID="standardSwapOptions" runat="server">
                                                                     <asp:ListItem Text="Swap with no Reservations" Value="Swap Without Reservation"></asp:ListItem>
                                                                    <asp:ListItem Text="Swap with Reservations" Value="Swap With Reservation"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                      <h8>Checking this option gives the customer an option to swap their gallons with conditions</h8>
                                                </div> <br />
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
                                                

                                                <script>
                                                    function refillOptions() {
                                                        var standardRefill = document.getElementById("refillSwapOptions");
                                                        var reserveRefill = document.getElementById("reserveSwapOptions");
                                                        var expressRefill = document.getElementById("expressSwapOptions")
                                                        var fields = document.getElementById("standardrefillOptions");

                                                        if (standardRefill.checked || reserveRefill.checked || expressRefill.checked) {
                                                            fields.style.display = "none";
                                                        }
                                                        else {
                                                            fields.style.display = "block";
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
                                                  
                                                           <strong>Choose types of orders you want to offer to customers:</strong>
                                                                <asp:CheckBoxList ID="reserveOrderMethod" runat="server">
                                                                    <asp:ListItem Text="Refill" Value="Refill" onclick="reserveRefillOptions();" ID="reserveSwapOptions"></asp:ListItem>
                                                                    <asp:ListItem Text="New Gallon" Value="New Gallon"></asp:ListItem>
                                                                     <asp:ListItem Text="Other Products" Value="other products"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                       </div> 
                                                    <div id="reserverefillOptions" style="display:none"> <hr />
                                                    <strong> Swap Gallon Options for Reservation</strong>
                                                    <h6> Note: This will give your customers an option on how you can swap their gallons for the refill</h6> <br />
                                                    <h8>Checking this option gives the customer an option to swap their gallons with no conditions</h8>
                                                                <asp:CheckBoxList ID="reserveSwap" runat="server">
                                                                     <asp:ListItem Text="Swap with no Reservations" Value="Swap Without Reservation"></asp:ListItem>
                                                                    <asp:ListItem Text="Swap with Reservations" Value="Swap With Reservation"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                      <h8>Checking this option gives the customer an option to swap their gallons with conditions</h8>
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
                                                <script>
                                                    function reserveRefillOptions() {
                                                        
                                                        var reserveRefill = document.getElementById("reserveSwapOptions");
                                                        //var expressRefill = document.getElementById("expressSwapOptions")
                                                        var fields = document.getElementById("reserverefillOptions");

                                                        if (reserveRefill.checked) {
                                                            fields.style.display = "none";
                                                        }
                                                        else {
                                                            fields.style.display = "block";
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
                                                  
                                                           <strong>Choose types of orders you want to offer to customers:</strong>
                                                                <asp:CheckBoxList ID="expressOrderMethod" runat="server">
                                                                    <asp:ListItem Text="Refill" Value="Refill" onclick="expressRefillOptions();" ID="expressSwapOptions"></asp:ListItem>
                                                                    <asp:ListItem Text="New Gallon" Value="New Gallon"></asp:ListItem>
                                                                     <asp:ListItem Text="Other Products" Value="other products"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                       </div>  
                                                    <div id="expressrefillOptions" style="display:none"> <hr />
                                                    <strong> Swap Gallon Options for Experss</strong>
                                                    <h6> Note: This will give your customers an option on how you can swap their gallons for the refill</h6> <br />
                                                    <h8>Checking this option gives the customer an option to swap their gallons with no conditions</h8>
                                                                <asp:CheckBoxList ID="expressSwap" runat="server">
                                                                     <asp:ListItem Text="Swap with no Reservations" Value="Swap Without Reservation"></asp:ListItem>
                                                                    <asp:ListItem Text="Swap with Reservations" Value="Swap With Reservation"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                      <h8>Checking this option gives the customer an option to swap their gallons with conditions</h8>
                                                </div> <br />
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
                                                <script>
                                                    function expressRefillOptions() {
                                                        var expressRefill = document.getElementById("expressSwapOptions")
                                                        var fields = document.getElementById("expressrefillOptions");

                                                        if (expressRefill.checked) {
                                                            fields.style.display = "none";
                                                        }
                                                        else {
                                                            fields.style.display = "block";
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
                                               </form>
                                             </div>
                                            </div>
                                        <%-- end set product refill--%>
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
                                                        
                                                        <asp:Label ID="Label7" runat="server" Font-Bold="true" Font-Size="Large" Text="PRODUCTS DATA"></asp:Label>
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
                                                        <asp:DropDownList ID="ddlSearchOptions" runat="server" CssClass="text-center" Height="40px" Width="364px">
                                                            <%--<asp:ListItem Text="---Select---"></asp:ListItem>--%>
                                                            <%--<asp:ListItem Text="View All" Value="0"></asp:ListItem>--%>
                                                           <asp:ListItem Text="Product Refill" Value="1"></asp:ListItem>
                                                           <asp:ListItem Text="other Product" Value="2"></asp:ListItem>
                                                           <asp:ListItem Text="Delivery Details" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn-primary" Height="40px"/>
                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                <hr />
                                                               <%-- PRODUCTS REFILL NI DIRI--%>
                                                               <%-- <h5> Products Refill</h5>--%>
                                                                <asp:Label ID="lblProductData" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                               <%--  <asp:Label ID="lblotherProduct" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                                 <asp:Label ID="lbldeliveryDetails" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>--%>
                                                             <br />
                                                                       <%--the gridview starts here--%>
                                                             <%-- <div style="overflow: auto; height: 832px; text-align:center;" class="texts" >--%>
                                                <asp:GridView runat="server" ID="gridProductRefill" CellPadding="3" Width="975px" CssClass="auto-style1" SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
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

                                                 <%--</div>--%> <%--Gridview ends here--%>
                                                              <%--  <hr />--%>
                                                                 <%-- OTHER PRODUCTS NI DIRI--%>
                                                               <%-- <h5> Other Products</h5>--%>
                                                                <%-- <br />--%>
                                                               <%--  <asp:Label ID="lblotherProduct" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>--%>
                                                                       <%--the gridview starts here--%>
                                                            <%--  <div style="overflow: auto; height: 832px; text-align:center;" class="texts" >--%>
                                                <asp:GridView runat="server" ID="gridotherProduct" CellPadding="3" Width="975px" CssClass="auto-style1" SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
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
                                                                <%--Gridview ends here--%>
                                                <%-- </div>
                                                                <hr />--%> 
                                                                <%-- DELIVERY DETAILS NI DIRI--%>
                                                               <%-- <h5> Delivery Details</h5>
                                                                <br />--%>
                                                               <%-- <asp:Label ID="lbldeliveryDetails" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>--%>
                                                                       <%--the gridview starts here--%>
                                                                <%--<asp:Label ID="lblExpress" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>--%>
                                                             <%-- <div style="overflow: auto; height: 832px; text-align:center;" class="texts" >--%>
                                                <asp:GridView runat="server" ID="gridDeliveryDetails" CellPadding="3" Width="975px" CssClass="auto-style1" SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
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
                       <%--</div>--%>

 </asp:Content>
