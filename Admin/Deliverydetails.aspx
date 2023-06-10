<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Deliverydetails.aspx.cs" Inherits="WRS2big_Web.Admin.Deliverydetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            margin-right: 0px;
            margin-left: 318px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="pcoded" class="pcoded">
        <div class="pcoded-overlay-box">DELIVER</div>
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
                                            <h5 class="m-b-10">DELIVERY MANAGEMENT</h5>
                                            <h6 class="m-b-0">2BiG: Water Refilling Station Management System</h6>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="breadcrumb-title">
                                            <li class="breadcrumb-item">
                                                <a href="/Admin/AdminIndex"><i class="fa fa-home"></i></a>
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
                            <div class="main-body" style="font-size: 16px">
                                <div class="page-wrapper">
                                    <!-- page content -->
                                    <div class="right_col" role="main">
                                        <div class="">
                                            <div class="clearfix">
                                                <%-- add product button--%>
                                                <br />
                                                <button type="button" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".managedetails" id="DeliverydetailsModal" runat="server"><i class="fa fa-plus"></i>Delivery Details</button>
                                                <button type="button" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".manage" id="deliveryTypesModal" runat="server"><i class="fa fa-plus"></i>Add Delivery Types</button>
                                                <button type="button" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".managePayment" id="paymentMethodsModal" runat="server"><i class="fa fa-plus"></i>Add Payment Methods</button>
                                                <button type="button" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".manageVehicles" id="vehiclesModal" runat="server"><i class="fa fa-plus"></i>Add Vehicles</button>

                                                <br />
                                                <br />
                                                <%--VIEW BUTTON --%>
                                          &nbsp;

                                                <%--                                      MODAL FOR PAYMENT METHOD--%>
                                                <div class="modal fade managePayment" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md">
                                                        <div class="modal-content">
                                                            <div id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="paymentModal"></h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">
                                                                            <%-- <div class="item form-group">--%>
                                                                            <h4 style="color: black; font-family: Bahnschrift">PAYMENT METHODS</h4>

                                                                            <div class="col-md-12 col-sm-12" style="font-size: 20px">
                                                                                <h4 style="font-size: 16px; color: black">Please choose the Payment Methods you want to use for the Orders</h4>
                                                                                <p style="font-size: 16px; color: black;">"These payment methods are applicable to all orders made in your station regardless of its Delivery Type"</p>
                                                                                <hr />
                                                                                <asp:CheckBoxList ID="paymentsCheckBox" runat="server" Height="40px" Width="300px">
                                                                                    <%--<asp:ListItem Selected="True">-----Choose One-----</asp:ListItem>--%>
                                                                                    <asp:ListItem style="font-size: 20px; color: black" Text="Cash on Delivery" Value="CashOnDelivery"></asp:ListItem>
                                                                                    <asp:ListItem style="font-size: 20px; color: black" Text="Gcash" Value="Gcash" onclick="gcashPayment();" id="gcashPayment"></asp:ListItem>
                                                                                    <asp:ListItem style="font-size: 20px; color: black" Text="Reward Points" Value="Points"></asp:ListItem>
                                                                                </asp:CheckBoxList>

                                                                            </div>
                                                                            <br />
                                                                            <div class="col-md-12 col-sm-12" id="gcashChecked" style="font-size: 20px; display: none">
                                                                                <h4 style="font-size: 16px; color: black">Please enter your GCASH Registered number:</h4>
                                                                                <h4 style="font-size: 16px; color: black">Reminder: This will be the number where the customer can send their payments</h4>
                                                                                <asp:TextBox ID="gcashnum" runat="server" TextMode="Number" Placeholder="09123456789" Width="364px" MaxLength="11"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" ControlToValidate="gcashnum" ForeColor="Red" ErrorMessage="Invalid phone number format (must be 11 digit)" ValidationExpression="^09\d{9}$"></asp:RegularExpressionValidator>


                                                                            </div>
                                                                            <br />

                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <%--  BUTTON ADD PAYMENT METHOD--%>
                                                                        <asp:Button ID="paymentButton" runat="server" Text="Confirm" class="btn btn-primary btn-sm" OnClick="paymentButton_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
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
                                                <%--  MODAL FOR VEHICLE--%>
                                                <div class="modal fade manageVehicles col-xl-10 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md col-xl-10 col-md-15">
                                                        <div class="modal-content col-xl-10 col-md-10" style="/*background-color: red; */ margin-left: 370px">
                                                            <div id="demo-form5" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="vehicleModal"></h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">
                                                                            <div id="deliveryChecked">
                                                                                <div class="col-md-12 col-sm-12">
                                                                                    <strong>Add the vehicles you use for the delivery:</strong>
                                                                                    <br />
                                                                                    <strong>Note: The vehicle fee on every vehicle will be added to the total payable amount of the customer. If you dont want to add a vehicle fee, please enter zero(0) </strong>
                                                                                    <br />
                                                                                    <br />

                                                                                    <asp:TextBox ID="vehicle1Name" Width="360px" Height="40px" Placeholder="Vehicle Name:" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="vehicle1Fee" Width="250px" Height="40px" TextMode="Number" Placeholder="Vehicle Fee:" runat="server" Style="margin-left: 50px"></asp:TextBox>
                                                                                    <asp:TextBox ID="vehicle1MinQty" Width="100px" Height="40px" TextMode="Number" Placeholder="Minimum:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="vehicle1MaxQty" Width="100px" Height="40px" TextMode="Number" Placeholder="Maximum:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <br />
                                                                                    <br />
                                                                                    <asp:TextBox ID="vehicle2Name" Width="360px" Height="40px" Placeholder="Vehicle Name:" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="vehicle2Fee" Width="250px" Height="40px" TextMode="Number" Placeholder="Vehicle Fee:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="vehicle2MinQty" Width="100px" Height="40px" TextMode="Number" Placeholder="Minimum:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="vehicle2MaxQty" Width="100px" Height="40px" TextMode="Number" Placeholder="Maximum:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <br />
                                                                                    <br />
                                                                                    <asp:TextBox ID="vehicle3Name" Width="360px" Height="40px" Placeholder="Vehicle Name:" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="vehicle3Fee" Width="250px" Height="40px" TextMode="Number" Placeholder="Vehicle Fee:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="vehicle3MinQty" Width="100px" Height="40px" TextMode="Number" Placeholder="Minimum:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="vehicle3MaxQty" Width="100px" Height="40px" TextMode="Number" Placeholder="Maximum:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <br />
                                                                                    <br />
                                                                                    <asp:TextBox ID="vehicle4Name" Width="360px" Height="40px" Placeholder="Vehicle Name:" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="vehicle4Fee" Width="250px" Height="40px" TextMode="Number" Placeholder="Vehicle Fee:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="vehicle4MinQty" Width="100px" Height="40px" TextMode="Number" Placeholder="Minimum:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="vehicle4MaxQty" Width="100px" Height="40px" TextMode="Number" Placeholder="Maximum:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <br />
                                                                                    <br />
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                        <div class="modal-footer">
                                                                            <%--  BUTTON ADD VEHICLES--%>
                                                                            <asp:Button ID="addVehicles" runat="server" Text="Confirm" class="btn btn-primary btn-sm" OnClick="addVehicles_Click" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%-- MODAL FOR Delivery TYPES--%>
                                                <div class="modal fade manage col-xl-10 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md col-xl-10 col-md-15">
                                                        <div class="modal-content col-xl-10 col-md-10" style="margin-left: 370px">
                                                            <div id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="myModalLabel2">Delivery Types</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-18 col-sm-18 ">
                                                                        <div class="x_content">

                                                                            <h5 style="color: black; font-family: Bahnschrift">Set delivery details offered to customers here:</h5>
                                                                            <h6>Note: Please check ONE 'Delivery Type' you offer to your business and fill all the informations needed
                                                                                <br />
                                                                                You can only create ONE delivery type at a time</h6>
                                                                            <hr />
                                                                            <br />
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <%--Delivery Type--%>
                                                                                <strong>Choose the Delivery Type you provide in your business:</strong>
                                                                                <asp:RadioButtonList ID="radDevType" runat="server" OnSelectedIndexChanged="radDevType_SelectedIndexChanged" AutoPostBack="false">
                                                                                    <asp:ListItem Value="Standard" ID="standardRadio" onclick="displayStandardOptions(); disableOtherRadios()"> &nbsp; Standard </asp:ListItem>
                                                                                    <asp:ListItem Value="Reservation" ID="reserveRadio" onclick="displayReserveOptions(); disableOtherRadios()">  &nbsp; Reservation</asp:ListItem>
                                                                                    <asp:ListItem Value="Express" ID="expressRadio" onclick="displayExpressOptions(); disableOtherRadios()">  &nbsp; Express</asp:ListItem>
                                                                                </asp:RadioButtonList>


                                                                            </div>
                                                                            <!--OPTIONS FOR STANDARD WHEN CLICKED-->
                                                                            <div id="standardCheckedDIV" style="display: none">
                                                                                <hr />
                                                                                <strong style="font-size: 18px">Standard Options</strong>
                                                                                <div class="col-md-12 col-sm-12 ">
                                                                                    <strong>Time Schedule for Delivery:</strong>
                                                                                    <br />
                                                                                    <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                    <h7>Set the time schedule for your standard delivery</h7><br />
                                                                                    <h7>From:</h7>
                                                                                    <asp:TextBox ID="standardSchedFrom" TextMode="Time" Width="150px" runat="server"></asp:TextBox>
                                                                                    <h7>To:</h7>
                                                                                    <asp:TextBox ID="standardSchedTo" TextMode="Time" Width="150px" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <br />

                                                                                <%--  delivery fee--%>
                                                                                <div class="col-md-12 col-sm-12 ">

                                                                                    <strong>Distance in km for FREE Delivery:</strong>
                                                                                    <br />
                                                                                    <%--                                                                                    <asp:Label ID="Label8" runat="server" Text="Note: If you don't want to offer FREE DELIVERY, set "></asp:Label><br />--%>
                                                                                    <asp:TextBox ID="FreeDelivery" Width="364px" Placeholder="Set the maximum distance for your FREE delivery" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <br />
                                                                                <div class="col-md-12 col-sm-12 ">
                                                                                    <strong>Delivery FEE:</strong>
                                                                                    <br />
                                                                                    <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                    <asp:TextBox ID="DeliveryFee" Width="364px" TextMode="Number" Placeholder="Set the Delivery fee:" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <br />
                                                                                <%--                                                        <div class="col-md-12 col-sm-12 " id="deliveryTypesPanel">
                                                                <strong>Vehicle Types:</strong> <br />
                                                                <asp:Label ID="vehicleTypes" style="font-size:16px" runat="server"></asp:Label>
                                                                <asp:TextBox ID="vehicleFee" Width="364px" TextMode="Number" Placeholder="Fee:" runat="server"></asp:TextBox>
                                                         </div>--%>
                                                                                <br />
                                                                                <div class="col-md-12 col-sm-12">

                                                                                    <strong>What products do you offer for the STANDARD delivery?</strong>
                                                                                    <asp:CheckBoxList ID="OrderMethod" runat="server">
                                                                                        <asp:ListItem Value="Refill" onclick="refillOptions();" ID="refillSwapOptions">  &nbsp; Refill Products</asp:ListItem>
                                                                                        <asp:ListItem Value="other products"> &nbsp; other Products</asp:ListItem>
                                                                                        <asp:ListItem Value="thirdparty product"> &nbsp; Third-party Products</asp:ListItem>
                                                                                        
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                                <br />
                                                                                <hr />
                                                                                <div>
                                                                                <h8>
                                                                                    <strong style="color: red">LEGEND:</strong> <br />
                                                                                    <strong style="color: dodgerblue"> 
                                                                                        STANDARD DELIVERY:
                                                                                    </strong> <br />
                                                                                        If choosen by the customer, the order can be processed within the day and not urgent.
                                                                                    <br />
                                                                                    <br />
                                                                                    By filling-up the textboxes, YOU allow the following: <br />
                                                                                    <br />
                                                                                    <strong style="color: dodgerblue">Time Schedule for Delivery:</strong>
                                                                                    <br />
                                                                                        Set the time schedule you want to deliver the Standard orders. Make sure to set the appropriate time, also considering your business hours.
                                                                                    <br />
                                                                                    <strong style="color: dodgerblue">Distance in KM for FREE Delivery:</strong>
                                                                                    <br />
                                                                                        Set the distance range for the free delivery in Kilometers. Orders beyond this range will be calculated accordingly. If you wish not to give FREE DELIVERY, just enter zero(0). 
                                                                                    <br />
                                                                                    <%--This gives the customer an option to give 'conditions' on the gallons that they want to receive when ordering water refill--%>
                                                                                    <strong style="color: dodgerblue">Delivery Fee:</strong>
                                                                                    <br />
                                                                                        Set the fee that you want the customers to pay. This will be added to that total payable amount of the customer.
                                                                                    <br /> <br />
                                                                                        By ticking these checkboxes, it means that: <br />
                                                                                    <strong style="color: dodgerblue">Refill:</strong>
                                                                                    <br />
                                                                                        You offer "REFILL" service in your business. This will enable the customers to order water refill.
                                                                                    <br />
                                                                                    <strong style="color: dodgerblue">third-party Products:</strong>
                                                                                    <br />
                                                                                        You offer products outside your business. These products are the one that is not directly from your business but you want to sell in your station as well. <br />
                                                                                    <strong>Example: Nature's Spring Bottled Water </strong>
                                                                                    <br />
                                                                                    <strong style="color: dodgerblue">other Products:</strong>
                                                                                    <br />
                                                                                        You offer owned other products. Example of these products are your personalized products such as bottled water directly from your refilling station.
                                                                                    <br />
                                                                                </h8>
                                                                                </div>

                                                                            </div>
                                                                            <br />

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
                                                                            <div id="reserveCheckedDIV" style="display: none">
                                                                                <hr />
                                                                                <strong style="font-size: 18px">Reservation Options</strong>
                                                                                <div class="col-md-12 col-sm-12 ">
                                                                                    <strong>Distance in km for FREE Delivery:</strong>
                                                                                    <br />
                                                                                    <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                    <asp:TextBox ID="resFreeDel" Width="364px" Placeholder="Set the maximum distance for your FREE delivery" runat="server"></asp:TextBox>

                                                                                </div>
                                                                                <div class="col-md-12 col-sm-12 ">
                                                                                    <strong>Delivery FEE:</strong><br />
                                                                                    <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                    <asp:TextBox ID="resDelFee" Width="364px" TextMode="Number" Placeholder="Set the Delivery fee:" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <br />
                                                                                <div class="col-md-12 col-sm-12">

                                                                                    <strong>What products do you offer for the RESERVATION?</strong>
                                                                                    <asp:CheckBoxList ID="reserveOrderMethod" runat="server">
                                                                                        <asp:ListItem Value="Refill" onclick="reserveRefillOptions();" ID="reserveSwapOptions"> &nbsp; Refill Products</asp:ListItem>
                                                                                        <asp:ListItem Value="other products"> &nbsp; other Products</asp:ListItem>
                                                                                        <asp:ListItem Value="thirdparty product"> &nbsp; Third-party Products</asp:ListItem>
                                                                                        
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                                <br /> <hr />
                                                                                <br />
                                                                                <div>
                                                                                <h8>
                                                                                    <strong style="color: red">LEGEND:</strong> <br />
                                                                                    <strong style="color: dodgerblue"> 
                                                                                        RESERVATION DELIVERY:
                                                                                    </strong> <br />
                                                                                        If choosen by the customer, the customer can set a time and date when should the order be delivered.
                                                                                    <br />
                                                                                    <br />
                                                                                    By filling-up the textboxes, YOU allow the following: <br />
                                                                                   
                                                                                    <strong style="color: dodgerblue">Distance in KM for FREE Delivery:</strong>
                                                                                    <br />
                                                                                        Set the distance range for the free delivery in Kilometers. Orders beyond this range will be calculated accordingly. If you wish not to give FREE DELIVERY, just enter zero(0). 
                                                                                    <br />
                                                                                    <%--This gives the customer an option to give 'conditions' on the gallons that they want to receive when ordering water refill--%>
                                                                                    <strong style="color: dodgerblue">Delivery Fee:</strong>
                                                                                    <br />
                                                                                        Set the fee that you want the customers to pay. This will be added to that total payable amount of the customer.
                                                                                    <br /> <br />
                                                                                        By ticking these checkboxes, it means that: <br />
                                                                                    <strong style="color: dodgerblue">Refill:</strong>
                                                                                    <br />
                                                                                        You offer "REFILL" service in your business. This will enable the customers to order water refill.
                                                                                    <br />
                                                                                    <strong style="color: dodgerblue">third-party Products:</strong>
                                                                                    <br />
                                                                                        You offer products outside your business. These products are the one that is not directly from your business but you want to sell in your station as well. Example: Nature's Spring Bottled Water
                                                                                    <br />
                                                                                    <strong style="color: dodgerblue">other Products:</strong>
                                                                                    <br />
                                                                                        You offer owned other products. Example of these products are your personalized products such as bottled water directly from your refilling station.
                                                                                    <br />
                                                                                </h8>
                                                                                </div>

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
                                                                            <div id="expressCheckedDIV" style="display: none">
                                                                                <hr />
                                                                                <strong style="font-size: 18px">Express Options</strong>
                                                                                <div class="col-md-12 col-sm-12 ">
                                                                                    <strong>Estimated time in minutes for Express Delivery:</strong>
                                                                                    <br />
                                                                                    <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                    <asp:TextBox ID="estimatedTime" Width="364px" Placeholder="Enter Express Delivery Estimated time" runat="server"></asp:TextBox>

                                                                                </div>

                                                                                <%--  delivery fee--%>
                                                                                <div class="col-md-12 col-sm-12 ">
                                                                                    <strong>Express Delivery fee:</strong><br />
                                                                                    <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                    <asp:TextBox ID="expressdeliveryFee" TextMode="Number" Width="364px" Placeholder="Enter the specific amount for the delivery fee" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <br />
                                                                                <div class="col-md-12 col-sm-12 ">
                                                                                    <strong>Distance in km for Delivery:</strong>
                                                                                    <br />
                                                                                    <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                    <asp:TextBox ID="expressDistance" Width="364px" Placeholder="Set the maximum distance for your delivery" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <br />
                                                                                <div class="col-md-12 col-sm-12">

                                                                                    <strong>What products do you offer for the EXPRESS delivery?</strong>
                                                                                    <asp:CheckBoxList ID="expressOrderMethod" runat="server">
                                                                                        <asp:ListItem Value="Refill" onclick="expressRefillOptions();" ID="expressSwapOptions">  &nbsp; Refill Products</asp:ListItem>
                                                                                       <asp:ListItem Value="other products"> &nbsp; other Products</asp:ListItem>
                                                                                        <asp:ListItem Value="thirdparty product"> &nbsp; Third-party Products</asp:ListItem>
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                                <br />
                                                                                <hr />
                                                                                <div>
                                                                                <h8>
                                                                                    <strong style="color: red">LEGEND:</strong> <br />
                                                                                    <strong style="color: dodgerblue"> 
                                                                                        EXPRESS DELIVERY:
                                                                                    </strong> <br />
                                                                                        If choosen by the customer, the order should be PRIORITIZED
                                                                                    <br /> <br />

                                                                                    By filling-up the textboxes, YOU allow the following: <br />
                                                                                   
                                                                                    <strong style="color: dodgerblue">Estimated time(minutes):</strong>
                                                                                    <br />
                                                                                        Set the estimated in minutes for the express delivery. 
                                                                                    <br />
                                                                                    <strong style="color: dodgerblue">Distance in KM for FREE Delivery:</strong>
                                                                                    <br />
                                                                                        Set the distance range for the free delivery in Kilometers. Orders beyond this range will be calculated accordingly. If you wish not to give FREE DELIVERY, just enter zero(0). 
                                                                                    <br />
                                                                                    <strong style="color: dodgerblue">Delivery Fee:</strong>
                                                                                    <br />
                                                                                        Set the fee that you want the customers to pay. This will be added to that total payable amount of the customer.
                                                                                    <br /> <br />
                                                                                        By ticking these checkboxes, it means that: <br />
                                                                                    <strong style="color: dodgerblue">Refill:</strong>
                                                                                    <br />
                                                                                        You offer "REFILL" service in your business. This will enable the customers to order water refill.
                                                                                    <br />
                                                                                    <strong style="color: dodgerblue">third-party Products:</strong>
                                                                                    <br />
                                                                                        You offer products outside your business. These products are the one that is not directly from your business but you want to sell in your station as well. Example: Nature's Spring Bottled Water
                                                                                    <br />
                                                                                    <strong style="color: dodgerblue">other Products:</strong>
                                                                                    <br />
                                                                                        You offer owned other products. Example of these products are your personalized products such as bottled water directly from your refilling station.
                                                                                    <br />
                                                                                </h8>
                                                                                </div>
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
                                                                    <br />



                                                                    <div class="modal-footer">
                                                                        <%-- set data button--%>
                                                                        <asp:Button ID="btnDeliverydetails" runat="server" Text="Add details" class="btn btn-primary btn-sm" OnClick="btnDeliverydetails_Click" AutoPostBack="false" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <%--MODAL FOR UPDATE EXPRESS DELIVERY TYPES--%>

                                                <div class="modal fade updateExpressType col-xl-10 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md col-xl-10 col-md-15">
                                                        <div class="modal-content col-xl-10 col-md-10" style="margin-left: 370px">
                                                            <div id="demo-form-update" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="updateTypesModal">Express Delivery</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-18 col-sm-18 ">
                                                                        <div class="x_content">
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <div class="col-md-12 col-sm-12 ">
                                                                                    <strong>Estimated time for Express Delivery:</strong>
                                                                                    <br />
                                                                                    <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                    <asp:TextBox ID="updateExpressTime" Width="364px" Placeholder="Enter Express Delivery Estimated time" runat="server"></asp:TextBox>

                                                                                </div>

                                                                                <%--  delivery fee--%>
                                                                                <div class="col-md-12 col-sm-12 ">
                                                                                    <strong>Express Delivery fee:</strong><br />
                                                                                    <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                    <asp:TextBox ID="updateExpressFee" TextMode="Number" Width="364px" Placeholder="Enter the specific amount for the delivery fee" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <br />
                                                                                <div class="col-md-12 col-sm-12 ">
                                                                                    <strong>Delivery Distance by km:</strong>
                                                                                    <br />
                                                                                    <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                    <asp:TextBox ID="updateExpressDistance" Width="364px" Placeholder="Set the maximum distance for your delivery" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <br />
                                                                                <div class="col-md-12 col-sm-12">

                                                                                    <strong>EXPRESS Products:</strong>
                                                                                    <asp:CheckBoxList ID="updateExpressChckbx" runat="server">
                                                                                        <asp:ListItem Value="Refill" onclick="expressRefillOptions();" ID="expressRefill">  &nbsp; Refill</asp:ListItem>
                                                                                        <%-- <asp:ListItem Text="New Gallon" Value="New Gallon"></asp:ListItem>--%>
                                                                                        <asp:ListItem Value="other products" ID="expressOther">  &nbsp; other Products</asp:ListItem>
                                                                                    </asp:CheckBoxList>
                                                                                </div>


                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <%-- set data button--%>
                                                                        <asp:Button ID="updateExpressbutton" runat="server" Text="Update" class="btn btn-primary btn-sm" OnClick="updateExpressbutton_Click" AutoPostBack="false" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--MODAL FOR UPDATE STANDARD DELIVERY TYPES--%>

                                                <div class="modal fade updateStandardType col-xl-10 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md col-xl-10 col-md-15">
                                                        <div class="modal-content col-xl-10 col-md-10" style="margin-left: 370px">
                                                            <div id="demo-form-update-standard" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="updateStandardModal">Standard Delivery</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-18 col-sm-18 ">
                                                                        <div class="x_content">
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <%--Delivery Type--%>
                                                                                <!--OPTIONS FOR STANDARD WHEN CLICKED-->
                                                                                <div id="standard">
                                                                                    <hr />

                                                                                    <div class="col-md-12 col-sm-12 ">
                                                                                        <strong>Standard Delivery Schedule:</strong>
                                                                                        <br />
                                                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                        <strong>current schedule:</strong>
                                                                                        <br />
                                                                                        <asp:Label runat="server" ID="exisitingSchdule" Style="font-size: 16px"></asp:Label>
                                                                                        <br />
                                                                                        <h7>From:</h7>
                                                                                        <asp:TextBox ID="updateStandardFrom" TextMode="Time" Width="100px" runat="server"></asp:TextBox>
                                                                                        <h7>To:</h7>
                                                                                        <asp:TextBox ID="updateStandardTo" TextMode="Time" Width="100px" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                    <br />

                                                                                    <%--  delivery fee--%>
                                                                                    <div class="col-md-12 col-sm-12 ">
                                                                                        <strong>Distance in km for FREE Delivery:</strong>
                                                                                        <br />
                                                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                        <asp:TextBox ID="updatestandardDistance" Width="364px" Placeholder="" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                    <br />
                                                                                    <div class="col-md-12 col-sm-12 ">
                                                                                        <strong>Delivery FEE: (php)</strong>
                                                                                        <br />
                                                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                        <asp:TextBox ID="updateStandardFee" Width="364px" TextMode="Number" Placeholder="Set the Delivery fee:" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                    <br />

                                                                                    <br />
                                                                                    <div class="col-md-12 col-sm-12">

                                                                                        <strong>What products do you offer for the STANDARD delivery?</strong>
                                                                                        <asp:CheckBoxList ID="updateStandardChkBx" runat="server">
                                                                                            <asp:ListItem Value="Refill" onclick="refillOptions();" ID="standardRefillOp" runat="server">  &nbsp; Refill</asp:ListItem>
                                                                                            <%--<asp:ListItem Text="New Gallon" Value="New Gallon"></asp:ListItem>--%>
                                                                                            <asp:ListItem Value="other products" ID="standardOtherProd" runat="server"> &nbsp; other Products</asp:ListItem>
                                                                                        </asp:CheckBoxList>
                                                                                    </div>
                                                                                    <br />
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <%-- set data button--%>
                                                                        <asp:Button ID="updateStandardbutton" runat="server" Text="Update" class="btn btn-primary btn-sm" OnClick="updateStandardbutton_Click" AutoPostBack="false" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--MODAL FOR UPDATE RESERVATION DELIVERY TYPES--%>
                                                <div class="modal fade updateReservationType col-xl-10 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md col-xl-10 col-md-15">
                                                        <div class="modal-content col-xl-10 col-md-10" style="margin-left: 370px">
                                                            <div id="demo-form-update-reservation" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="updateReservationModal">Reservation Delivery</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-18 col-sm-18 ">
                                                                        <div class="x_content">
                                                                            <div id="reservation">
                                                                                <hr />


                                                                                <%--  delivery fee--%>
                                                                                <div class="col-md-12 col-sm-12 ">
                                                                                    <strong>Distance in km for FREE Delivery:</strong>
                                                                                    <br />
                                                                                    <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                    <asp:TextBox ID="updateReserveDistance" Width="364px" Placeholder="" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <br />
                                                                                <div class="col-md-12 col-sm-12 ">
                                                                                    <strong>Delivery FEE: (php)</strong>
                                                                                    <br />
                                                                                    <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                                                    <asp:TextBox ID="updateReserveFee" Width="364px" TextMode="Number" Placeholder="Set the Delivery fee:" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <br />

                                                                                <br />
                                                                                <div class="col-md-12 col-sm-12">

                                                                                    <strong>What products do you offer for the STANDARD delivery?</strong>
                                                                                    <asp:CheckBoxList ID="updateReserveChkBx" runat="server">
                                                                                        <asp:ListItem Value="Refill" onclick="refillOptions();" ID="reserveRefill" runat="server">  &nbsp; Refill</asp:ListItem>
                                                                                        <%--<asp:ListItem Text="New Gallon" Value="New Gallon"></asp:ListItem>--%>
                                                                                        <asp:ListItem Value="other products" ID="reserveOther" runat="server"> &nbsp; other Products</asp:ListItem>
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                                <br />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <%-- set data button--%>
                                                                        <asp:Button ID="updateResrveButton" runat="server" Text="Update" class="btn btn-primary btn-sm" OnClick="updateResrveButton_Click" AutoPostBack="false" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <%-- MODAL FOR Delivery details--%>
                                                <div class="modal fade managedetails  col-xl-10 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md col-xl-10 col-md-15">
                                                        <div class="modal-content col-xl-10 col-md-10" style="/*background-color: red; */ margin-left: 370px">
                                                            <div id="demo-form2" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="myModalLabel3">Delivery Details</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-18 col-sm-18 ">
                                                                        <div class="x_content">
                                                                            <h5 style="color: black; font-family: Bahnschrift">Set delivery details offered to customers here:</h5>
                                                                            <%--<h6>Note: You can't change the delivery details once it is already added</h6>--%>
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

                                                                            <hr />
                                                                            <br />
                                                                            <div id="swapOptions">
                                                                                <strong>Swap Gallon Options</strong>
                                                                                <h6>Note: This will give your customers an option on how you can swap their gallons for the REFILL</h6>
                                                                                <asp:CheckBoxList ID="swapOptionItems" runat="server">
                                                                                    <asp:ListItem Value="Swap without Conditions"> &nbsp; Swap without Conditions</asp:ListItem>
                                                                                    <asp:ListItem Value="Swap With Conditions"> &nbsp; Swap with Conditions</asp:ListItem>
                                                                                    <asp:ListItem Value="Gallon Drop-by" ID="gallonDropby"> &nbsp; Gallon Drop-by</asp:ListItem>
                                                                                    <asp:ListItem Value="Request Pick-up" ID="pickupPerGallon" onclick="perGallonFee();"> &nbsp; Request Gallon Pickup</asp:ListItem>

                                                                                </asp:CheckBoxList>
                                                                                <div id="perGallon" style="display: none; margin-left: 15px">
                                                                                    <asp:CheckBoxList ID="perGallonOptions" runat="server">
                                                                                        <asp:ListItem Value="by Gallons" ID="byGallonFee" onclick="byGallons();">  &nbsp; by Gallons </asp:ListItem>
                                                                                        <%--                                                                     <asp:ListItem Value="by "> &nbsp; Swap with Conditions</asp:ListItem>--%>
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                                <div id="byGallonsFee" style="display: none">
                                                                                    <h8 style="margin-left: 50px;">per Gallon Fee:</h8><br />
                                                                                    <asp:TextBox ID="perGallonFee" Width="250px" Height="40px" TextMode="Number" runat="server" Style="margin-left: 80px;"></asp:TextBox>
                                                                                </div>
                                                                                <br />
                                                                                <hr />
                                                                    <div id="standardLegend">
                                                                    <h8>
                                                                        <strong style="color: red">LEGEND:</strong>
                                                                        <br />
                                                                        By checking the following checkboxes, YOU allow the following:
                                                                        <br />
                                                                        <strong style="color: dodgerblue">Pick-up:</strong>
                                                                        <br />
                                                                        Your customers can Pick-up their orders in the station.
                                                                                    <br />
                                                                        <strong style="color: dodgerblue">Delivery:</strong>
                                                                        <br />
                                                                        The orders of the customer will be delivered to their delivery address.
                                                                                    <br />
                                                                        <%--This gives the customer an option to give 'conditions' on the gallons that they want to receive when ordering water refill--%>
                                                                        <strong style="color: dodgerblue">Swap with Condition:</strong>
                                                                        <br />
                                                                        Let the customers give their personal 'conditions' or note about the swapped gallon that they want to receive when ordering water refill.
                                                                                    <br />
                                                                        <strong style="color: dodgerblue">Swap without Condition:</strong>
                                                                        <br />
                                                                        Let YOU, the admin to decide on what gallon should be swapped with the customer gallon when ordering water refill.
                                                                                    <br />
                                                                        <strong style="color: dodgerblue">Gallon drop-by:</strong>
                                                                        <br />
                                                                        You allow the customers to personally drop their gallons in the station for refill
                                                                        <br />
                                                                        <strong style="color: dodgerblue">Request Gallon Pick-up:</strong>
                                                                        <br />
                                                                        Gives the customers an option to request for a 'gallon pick-up' if they dont want their gallons to be swapped.
                                                                                    <br />
                                                                        <strong style="color: dodgerblue">per Gallon Fee: </strong>
                                                                        <br />
                                                                        Additional FEE if the customer requests for a 'Gallon Pick-up'
                                                                    </h8>
                                                                    </div>

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

                                                                            <script>
                                                                                function byGallons() {
                                                                                    var delivery = document.getElementById("byGallonFee");
                                                                                    var stanfields = document.getElementById("byGallonsFee");

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
                                                                        <asp:Button ID="deliveryDetailsAdded" runat="server" Text="Submit" class="btn btn-primary btn-sm" OnClick="deliveryDetailsAdded_Click" AutoPostBack="false" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>


                                                <%-- MODAL FOR UPDATE Delivery details--%>
                                                <div class="modal fade updatedeliveryDetails  col-xl-10 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md col-xl-10 col-md-15">
                                                        <div class="modal-content col-xl-10 col-md-10" style="/*background-color: red; */ margin-left: 370px">
                                                            <div id="demo-form4" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="updateDetailsModal">UPDATE</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-18 col-sm-18 ">
                                                                        <div class="x_content">
                                                                            <h5 style="color: black; font-family: Bahnschrift">DELIVERY DETAILS</h5>
                                                                            <%--<h6>Note: You can't change the delivery details once it is already added</h6>--%>
                                                                            <hr />

                                                                            <br />
                                                                            <!--ORDER TYPE SELECTION-->
                                                                            <div class="col-md-12 col-sm-12" id="updateorderTypeDiv">

                                                                                <strong>Choose the order type you offer to customers:</strong>
                                                                                <asp:CheckBoxList ID="updateOrderTypesChck" runat="server">
                                                                                    <asp:ListItem Value="PickUp" id="updatePickup">&nbsp; Pick-Up</asp:ListItem>
                                                                                    <asp:ListItem Value="Delivery" id="updatedelivery">&nbsp; Delivery</asp:ListItem>
                                                                                </asp:CheckBoxList>
                                                                            </div>
                                                                            <br />
                                                                            <!--OPTIONS FOR DELIVERY WHEN CLICKED-->
                                                                            <div id="updatedeliveryChecked">
                                                                                <div class="col-md-12 col-sm-12">
                                                                                    <strong>Update the vehicle types you have if you offer DELIVERY:</strong>
                                                                                    <br />
                                                                                    <br />
                                                                                    <strong>VEHICLE NAME: </strong>
                                                                                    <strong style="margin-left: 60px">VEHICLE FEE:</strong>
                                                                                    <strong style="margin-left: 60px">MINIMUM Gallons:</strong>
                                                                                    <strong style="margin-left: 30px">MAXIMUM Gallons:</strong>
                                                                                    <br />
                                                                                    <asp:TextBox ID="updateV1Name" Width="150px" Height="40px" Placeholder="Vehicle Name:" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="updateV1Fee" Width="100px" Height="40px" TextMode="Number" Placeholder="Vehicle Fee:" runat="server" Style="margin-left: 50px"></asp:TextBox>
                                                                                    <asp:TextBox ID="updateV1Min" Width="100px" Height="40px" TextMode="Number" Placeholder="Minimum:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="updateV1Max" Width="100px" Height="40px" TextMode="Number" Placeholder="Maximum:" runat="server" Style="margin-left: 60px;"></asp:TextBox>
                                                                                    <br />
                                                                                    <br />
                                                                                    <asp:TextBox ID="updateV2Name" Width="150px" Height="40px" Placeholder="Vehicle Name:" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="updateV2Fee" Width="100px" Height="40px" TextMode="Number" Placeholder="Vehicle Fee:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="updateV2Min" Width="100px" Height="40px" TextMode="Number" Placeholder="Minimum:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="updateV2Max" Width="100px" Height="40px" TextMode="Number" Placeholder="Maximum:" runat="server" Style="margin-left: 60px;"></asp:TextBox>
                                                                                    <br />
                                                                                    <br />
                                                                                    <asp:TextBox ID="updateV3Name" Width="150px" Height="40px" Placeholder="Vehicle Name:" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="updateV3Fee" Width="100px" Height="40px" TextMode="Number" Placeholder="Vehicle Fee:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="updateV3Min" Width="100px" Height="40px" TextMode="Number" Placeholder="Minimum:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="updateV3Max" Width="100px" Height="40px" TextMode="Number" Placeholder="Maximum:" runat="server" Style="margin-left: 60px;"></asp:TextBox>
                                                                                    <br />
                                                                                    <br />
                                                                                    <asp:TextBox ID="updateV4Name" Width="150px" Height="40px" Placeholder="Vehicle Name:" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="updateV4Fee" Width="100px" Height="40px" TextMode="Number" Placeholder="Vehicle Fee:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="updateV4Min" Width="100px" Height="40px" TextMode="Number" Placeholder="Minimum:" runat="server" Style="margin-left: 50px;"></asp:TextBox>
                                                                                    <asp:TextBox ID="updateV4Max" Width="100px" Height="40px" TextMode="Number" Placeholder="Maximum:" runat="server" Style="margin-left: 60px;"></asp:TextBox>
                                                                                    <br />
                                                                                </div>
                                                                            </div>

                                                                            <hr />



                                                                            <br />
                                                                            <div id="updateswapOptions">
                                                                                <strong>Swap Gallon Options</strong>
                                                                                <h6>Note: This will give your customers an option on how you can swap their gallons for the REFILL</h6>
                                                                                <asp:CheckBoxList ID="updateSwap" runat="server">
                                                                                    <asp:ListItem Value="Swap With Conditions" id="updatewithoutCondition"> &nbsp; Swap with NO Conditions</asp:ListItem>
                                                                                    <asp:ListItem Value="Swap With Conditions" id="updatewithCondition"> &nbsp; Swap with Conditions</asp:ListItem>
                                                                                    <asp:ListItem Value="Gallon Drop-by" ID="updategallonDropby"> &nbsp; Gallon Drop-by</asp:ListItem>
                                                                                    <asp:ListItem Value="Request Pick-up" ID="updatepickupPerGallon"> &nbsp; Request Gallon Pickup</asp:ListItem>

                                                                                </asp:CheckBoxList>
                                                                                <div id="updateperGallon" style="display: none; margin-left: 15px">
                                                                                    <asp:CheckBoxList ID="CheckBoxList3" runat="server">
                                                                                        <asp:ListItem Value="by Gallons" ID="updatebyGallonFee">  &nbsp; by Gallons </asp:ListItem>
                                                                                        <%--                                                                     <asp:ListItem Value="by "> &nbsp; Swap with Conditions</asp:ListItem>--%>
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                                <div id="updateGallonsFee" runat="server">
                                                                                    <h8 style="margin-left: 50px;">per Gallon Fee:</h8><br />
                                                                                    <asp:TextBox ID="updatebyGallonsFee" Width="250px" Height="40px" TextMode="Number" runat="server" Style="margin-left: 80px;"></asp:TextBox>
                                                                                </div>
                                                                                <br />
                                                                                <hr />


                                                                            </div>
                                                                            <br />
                                                                            <h4 style="color: black; font-family: Bahnschrift">PAYMENT METHODS</h4>
                                                                            <br />

                                                                            <div class="col-md-12 col-sm-12" style="font-size: 20px">
                                                                                <h6>Please choose the Payment Methods you want to use for the Orders</h6>
                                                                                <h6>"These payment methods are applicable to all orders made in your station regardless of its Delivery Type"</h6>
                                                                                <br />
                                                                                <asp:CheckBoxList ID="updatePayment" runat="server" Height="40px" Width="300px">
                                                                                    <%--<asp:ListItem Selected="True">-----Choose One-----</asp:ListItem>--%>
                                                                                    <asp:ListItem Text="Cash on Delivery" Value="CashOnDelivery" id="updateCOD"></asp:ListItem>

                                                                                    <asp:ListItem Text="Reward Points" Value="Points" id="updatePoints"></asp:ListItem>
                                                                                    <asp:ListItem Text="Gcash" Value="Gcash" id="updategcashPayment"></asp:ListItem>
                                                                                </asp:CheckBoxList>

                                                                            </div>
                                                                            <br />
                                                                            <div class="col-md-12 col-sm-12" id="updategcashChecked" style="font-size: 20px;" runat="server">
                                                                                <h6>Please enter your GCASH Registered number:</h6>
                                                                                <h6>Reminder: This will be the number where the customer can send their payments</h6>
                                                                                <asp:TextBox ID="updateGcashNum" runat="server" TextMode="Number" Placeholder="09123456789" Width="364px" MaxLength="11"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegexValidator" ValidationGroup="a" runat="server" ControlToValidate="updateGcashNum" ForeColor="Red" ErrorMessage="Invalid phone number format (must be 11 digit)" ValidationExpression="^09\d{9}$"></asp:RegularExpressionValidator>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <br />
                                                                    <hr />
                                                                    <h8>
                                                                        <strong style="color: red">LEGEND:</strong>
                                                                        <br />
                                                                        By checking the following checkboxes, YOU allow the following:
                                                                        <br />
                                                                        <strong style="color: dodgerblue">Pick-up:</strong>
                                                                        <br />
                                                                        Your customers can Pick-up their orders in the station.
                                                                                    <br />
                                                                        <strong style="color: dodgerblue">Delivery:</strong>
                                                                        <br />
                                                                        The orders of the customer will be delivered to their delivery address.
                                                                                    <br />
                                                                        <%--This gives the customer an option to give 'conditions' on the gallons that they want to receive when ordering water refill--%>
                                                                        <strong style="color: dodgerblue">Swap with Condition:</strong>
                                                                        <br />
                                                                        Let the customers give their personal 'conditions' or note about the swapped gallon that they want to receive when ordering water refill.
                                                                                    <br />
                                                                        <strong style="color: dodgerblue">Swap without Condition:</strong>
                                                                        <br />
                                                                        Let YOU, the admin to decide on what gallon should be swapped with the customer gallon when ordering water refill.
                                                                                    <br />
                                                                        <strong style="color: dodgerblue">Gallon drop-by:</strong>
                                                                        <br />
                                                                        You allow the customers to personally drop their gallons in the station for refill
                                                                        <br />
                                                                        <strong style="color: dodgerblue">Request Gallon Pick-up:</strong>
                                                                        <br />
                                                                        Gives the customers an option to request for a 'gallon pick-up' if they dont want their gallons to be swapped.
                                                                                    <br />
                                                                        <strong style="color: dodgerblue">per Gallon Fee: </strong>
                                                                        <br />
                                                                        Additional FEE if the customer requests for a 'Gallon Pick-up'
                                                                    </h8>
                                                                    <br />
                                                                    <br />
                                                                    <div class="modal-footer">
                                                                        <%-- set data button--%>
                                                                        <asp:Button ID="updateDeliveryDetails" runat="server" Text="Submit" class="btn btn-primary btn-sm" OnClick="updateDeliveryDetails_Click" AutoPostBack="false" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Label ID="warning" runat="server" Style="color: black; font-size: 18px;"></asp:Label>
                                            <br />
                                            <br />
                                            <%--PAGE CONTENTS FOR DELIVERY DETAILS--%>
                                            <div class="row" id="deliveryDetailsRow" runat="server">
                                                <div class="col-lg-12 col-xl-16">
                                                    <div class="card">
                                                        <div class="card-header">

                                                            <asp:Label ID="Label1" runat="server" Text="DELIVERY DETAILS" Style="color: black; font-size: 20px;"></asp:Label>
                                                            <button type="button" id="updateDetails" runat="server" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".updatedeliveryDetails">UPDATE</button>
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
                                                            <!-- Row start -->
                                                            <div class="row m-b-30">
                                                                <div class="col-lg-12 col-xl-6">

                                                                    <!-- Nav tabs -->
                                                                    <ul class="nav nav-tabs md-tabs" role="tablist">
                                                                        <li class="nav-item">
                                                                            <a class="nav-link active" data-toggle="tab" href="#home3" role="tab">ORDER TYPES</a>
                                                                            <div class="slide"></div>
                                                                        </li>
                                                                        <li class="nav-item">
                                                                            <a class="nav-link" data-toggle="tab" href="#profile3" role="tab">PAYMENT METHODS</a>
                                                                            <div class="slide"></div>
                                                                        </li>
                                                                        <li class="nav-item">
                                                                            <a class="nav-link" data-toggle="tab" href="#messages3" role="tab">SWAP OPTIONS</a>
                                                                            <div class="slide"></div>
                                                                        </li>
                                                                        <li class="nav-item">
                                                                            <a class="nav-link" data-toggle="tab" href="#settings3" role="tab">VEHICLES</a>
                                                                            <div class="slide"></div>
                                                                        </li>
                                                                    </ul>
                                                                    <!-- Tab panes -->
                                                                    <div class="tab-content card-block">
                                                                        <div class="tab-pane active" id="home3" role="tabpanel">
                                                                            <br />
                                                                            <br />
                                                                            <asp:GridView runat="server" ID="orderTypesGrid" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 500px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="400px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                                <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                                <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                            </asp:GridView>
                                                                            <br />
                                                                            <br />


                                                                        </div>
                                                                        <div class="tab-pane" id="profile3" role="tabpanel">
                                                                            <br />
                                                                            <br />
                                                                            <asp:GridView runat="server" ID="paymentsGrid" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 500px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="450px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <%-- <asp:Button runat="server" ID="viewDeliveryType" OnClick="viewDeliveryType_Click" Text="Open" Style="background-color: transparent; font-size: 16px;" class="active btn waves-effect text-center" />
                                                                                            --%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                                <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                                <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                            </asp:GridView>

                                                                        </div>
                                                                        <div class="tab-pane" id="messages3" role="tabpanel">
                                                                            <br />
                                                                            <br />
                                                                            <asp:GridView runat="server" ID="swapOptionsGrid" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 500px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="450px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <%-- <asp:Button runat="server" ID="viewDeliveryType" OnClick="viewDeliveryType_Click" Text="Open" Style="background-color: transparent; font-size: 16px;" class="active btn waves-effect text-center" />
                                                                                            --%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                                <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                                <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                            </asp:GridView>
                                                                        </div>
                                                                        <div class="tab-pane" id="settings3" role="tabpanel">
                                                                            <br />
                                                                            <br />
                                                                            <asp:GridView runat="server" ID="vehiclesGridview" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 500px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="450px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Button runat="server" ID="removeVehicle" OnClick="removeVehicle_Click" Text="Remove" Style="background-color: transparent; font-size: 16px;" class="active btn waves-effect text-center" />

                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                                <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                                <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--PAGE CONTENTS FOR DELIVERY TYPES--%>
                                            <div class="row" id="deliveryTypesRow" runat="server">
                                                <div class="col-lg-12 col-xl-16">
                                                    <div class="card">
                                                        <div class="card-header">

                                                            <asp:Label ID="Label7" runat="server" Text="DELIVERY TYPES" Style="color: black; font-size: 20px;"></asp:Label>
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
                                                                        <asp:Label ID="lblProductData" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>

                                                                        <br />

                                                                        <%-- DELIVERY DETAILS NI DIRI--%>

                                                                        <asp:GridView runat="server" ID="gridDeliveryDetails" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1650px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <%-- <asp:Button runat="server" ID="viewDeliveryType" OnClick="viewDeliveryType_Click" Text="Open" Style="background-color: transparent; font-size: 16px;" class="active btn waves-effect text-center" />
                                                                                        --%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                        </asp:GridView>


                                                                        <%--Gridview ends here--%>
                                                                    </div>
                                                                    <!--/tab-pane-->
                                                                </div>
                                                                <!--/tab-content-->

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

                                                            <asp:Button ID="btnDeliverytype" runat="server" Text="Search" OnClick="btnSearchDeliverytype_Click" CssClass="btn-primary" Height="40px" />
                                                            <div class="table-responsive">
                                                                <div class="tab-content">
                                                                    <div class="tab-pane active">
                                                                        <hr />
                                                                        <asp:Label ID="lblDeliveryType" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                                        <asp:Label ID="nullLabel" Font-Size="16px" runat="server" Width="364px"></asp:Label>
                                                                        <br />
                                                                        <br />

                                                                        <%--GRIDVIEW FOR EXPRESS--%>
                                                                        <asp:GridView runat="server" ID="expressGridview" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1650px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <%--                                                          <ItemTemplate>
                                                       
                                                                <button type="button" class="btn btn-primary btn-md" data-toggle="modal" data-target=".edit"><i class="fa fa-edit"></i> update</button>
                                                          </ItemTemplate>--%>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <br />
                                                                        <button type="button" id="updateExpress" runat="server" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".updateExpressType">UPDATE</button>

                                                                        <%--GRIDVIEW FOR STANDARD--%>
                                                                        <asp:GridView runat="server" ID="standardGridview" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1650px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <%--                                                          <ItemTemplate>
                                                          
                                                                <button type="button" class="btn btn-primary btn-md" data-toggle="modal" data-target=".edit"><i class="fa fa-edit"></i> update</button>
                                                          </ItemTemplate>--%>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                        </asp:GridView>

                                                                        <button type="button" id="updateStandard" runat="server" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".updateStandardType">UPDATE</button>
                                                                        <%--GRIDVIEW FOR RESERVATION--%>
                                                                        <asp:GridView runat="server" ID="reservationGridView" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1650px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <%--                                                          <ItemTemplate>
                                                         
                                                                <button type="button" class="btn btn-primary btn-md" data-toggle="modal" data-target=".edit"><i class="fa fa-edit"></i> update</button>
                                                          </ItemTemplate>--%>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                        </asp:GridView>
                                                                        <button type="button" id="updateReservation" runat="server" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".updateReservationType">UPDATE</button>

                                                                        <%-- </div>--%> <%--Gridview ends here--%>
                                                                    </div>
                                                                    <!--/tab-pane-->
                                                                </div>
                                                                <!--/tab-content-->

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
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
    </div>

</asp:Content>
