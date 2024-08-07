<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WaterProduct.aspx.cs" Inherits="WRS2big_Web.Admin.WaterProduct" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Include jQuery and the Timepicker plugin -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-timepicker/1.13.18/jquery.timepicker.min.css">

    <style>
        texts {
            font-size: 16px;
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
                            <div class="main-body">
                                <div class="page-wrapper">
                                    <!-- page content -->
                                    <div class="right_col" role="main">
                                        <div class="">
                                            <div class="clearfix">
                                                <%-- add product button--%>
                                                <button type="button" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".add"><i class="fa fa-plus"></i>Add Third Party Product Offers</button>
                                                <button type="button" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".set"><i class="fa fa-plus"></i>Add Product Refill Offers</button>
                                               <button type="button" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".addtank"><i class="fa fa-plus"></i>Add Water Tank Supply</button>
                                                <%--VIEW BUTTON --%>
                                          &nbsp;
                                       <%-- MODAL FOR TANK SUPPLY --%>
                                                <div class="modal fade addtank" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md">
                                                        <div class="modal-content">
                                                            <form id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="myModalLabel3">Add Water Tank Supply</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 "> 
                                                                        <div class="x_content">
                                                                            <%-- <div class="item form-group">--%>
                                                                            <h4 style="color: black; font-family: Bahnschrift">Set the daily amount of the water in your tank supply.:</h4>

                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <%--tank unit --%>
                                                                                <strong>Tank Unit:</strong>
                                                                                <asp:DropDownList ID="drdTankUnit" runat="server" Height="40px" Width="300px">
                                                                                    <%--<asp:ListItem Selected="True">-----Choose One-----</asp:ListItem>--%>
                                                                                    <asp:ListItem Text="Gallon" Value="gallon" Selected="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="Liters" Value="liter/s"></asp:ListItem>
                                                                                    <asp:ListItem Text="Mililiters" Value="mililiter/s"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <br />
                                                                            <%--  Tank Size--%>
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <strong>Quantity/Size of Tank Unit:</strong>
                                                                                <asp:TextBox ID="tankSize" Placeholder="Enter the number of units of tank water supply" runat="server" Width="300px"></asp:TextBox>
                                                                            </div>
                                                                            <br />
                                                                            <%-- <asp:Button ID="AddTanksupply" runat="server" class="btn btn-primary btn-sm" Text="Add Supply" OnClick="btnAddSupply_Click" Width="131px"/>--%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <%--  BUTTON TANK SUPPLY HERE--%>
                                                                        <asp:Button ID="btnAddSupply" runat="server" Text="Add Supply" class="btn btn-primary btn-sm" OnClick="btnAddSupply_Click" />
                                                                    </div>
                                                                </div>
                                                        </div>
                                                        <%--  </form>--%>
                                                    </div>
                                                </div>
                                                <%-- MODAL FOR ADD third party PRODUCT--%>
                                                <div class="modal fade add" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-lg">
                                                        <div class="modal-content">
                                                            <form id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="myModalLabel">Add Third Party Product Offered</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">
                                                                            <h4 style="color: black; font-family: Bahnschrift">Set third party offered products here:</h4>
                                                                            <hr />
                                                                            <strong>Note: This is intended for third party product offered.</strong>
                                                                            <hr />
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <%--PRODUCT TYPE--%>
                                                                                <strong>Product Name:</strong>
                                                                                <asp:TextBox ID="productName" runat="server" Placeholder="Enter the product name offered" Height="40px" Width="464px"></asp:TextBox>
                                                                            </div>
                                                                            <br />
                                                                            <div class="col-md-12 col-sm-12">
                                                                                <%--PRODUCT NAME--%>
                                                                                <strong>Product Image:</strong>
                                                                                <%--file upload--%>
                                                                                <asp:FileUpload ID="imgProduct" runat="server" Font-Size="Medium" Height="38px" Width="464px" />
                                                                                 </div>
                                                                            <br />
                                                                            <div class="col-md-12 col-sm-12">
                                                                                <strong>Product Unit of Volume:</strong>
                                                                                <asp:DropDownList ID="drdprodUnitVolume" runat="server" Height="40px" Width="464px">
                                                                                   <asp:ListItem Text="Gallon" Value="gallon"></asp:ListItem>
                                                                                    <asp:ListItem Text="Liters" Value="liters"></asp:ListItem>
                                                                                    <asp:ListItem Text="Milliliters" Value="ml"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <br />
                                                                          <div class="col-md-12 col-sm-12">
                                                                        <strong>Product Quantity/Number of Unit:</strong>
                                                                        <asp:TextBox ID="productQty" TextMode="Number" Height="40px" Placeholder="Enter the number of quantity/size the unit of volume have" runat="server" Width="464px"></asp:TextBox>
                                                                               <br />
                                                                         <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="productQty" Type="Double" MinimumValue="0" ErrorMessage="Please enter a valid number of unit of the product  (must input a number)" ForeColor="Red"></asp:RangeValidator>
                                                                    </div>
                                                                    <br />
                                                                  <div class="col-md-12 col-sm-12">
                                                                    <strong>Product Price:</strong>
                                                                    <asp:TextBox ID="productPrice" TextMode="Number" Height="40px" Placeholder="Enter the price of the product" runat="server" Width="464px"></asp:TextBox>
                                                                    <br />
                                                                    <asp:RangeValidator ID="pricerangeValidator" runat="server" ControlToValidate="productPrice" Type="Double" MinimumValue="0" ErrorMessage="Please enter a valid price (must be a non-negative number)" ForeColor="Red"></asp:RangeValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="productPrice"
                                                                        ErrorMessage="Please enter a valid price (must be a number or decimal)" ValidationExpression="^\d+(\.\d+)?$" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                </div>
                                                                    <br />
                                                                    <div class="col-md-12 col-sm-12">
                                                                        <strong>Product Discount:</strong>
                                                                        <br />
                                                                        <h7>Please enter the discount percentage (%) you offer for this product</h7>
                                                                        <asp:TextBox ID="productDiscounts" TextMode="Number" Placeholder="Enter discount offered" runat="server" Height="40px" Width="464px"></asp:TextBox>
                                                                         <br />
                                                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="productDiscounts" Type="Double" MinimumValue="0" ErrorMessage="Please enter a valid discount (must input a number or in decimal)" ForeColor="Red"></asp:RangeValidator>
                                                                        <asp:RegularExpressionValidator ID="discountValidator" runat="server" ControlToValidate="productDiscounts"
                                                                            ErrorMessage="Please enter a valid discount" ValidationExpression="^\d+(\.\d+)?$" ValidationGroup="validationGroup"></asp:RegularExpressionValidator>
                                                                    </div>
                                                                              <br />
                                                                            <hr />
                                                                            <asp:Label ID="Label5" runat="server" Text="PRODUCT STOCK"></asp:Label>
                                                                            <div class="col-md-12 col-sm-12">
                                                                                <strong>Unit</strong>
                                                                                <asp:DropDownList ID="drdUnitStock" runat="server" Height="40px" Width="464px">
                                                                                    <asp:ListItem Selected="False">--- Select the applicable unit for your stock---</asp:ListItem>
                                                                                    <asp:ListItem Text="Bottle" Value="bottle"></asp:ListItem>
                                                                                    <asp:ListItem Text="Gallon" Value="gallon"></asp:ListItem>
                                                                                    <asp:ListItem Text="Box/Case" Value="box/case"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <br />
                                                                            <%--PRODUCT Available--%>
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <strong>Quantity(Stock)/Number of unit:</strong>
                                                                                <asp:TextBox ID="stockQuantity" Height="40px" TextMode="Number" step="any" Placeholder="Enter how many stock available base on the unit being selected" Product="Enter the available stock of the product above" runat="server" Width="464px"></asp:TextBox>
                                                                             <br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="stockQuantity"
                                                                            ErrorMessage="Please enter a valid number of unit for product stock (must input a number)" ValidationExpression="^\d+(\.\d+)?$" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                            </div>
                                                                            <br />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <%-- add data button--%>
                                                                    <asp:Button ID="btnAdd" runat="server" Text="Add Product" class="btn btn-primary btn-sm" OnClick="btnAdd_Click" AutoPostBack="false" />
                                                              </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%-- MODAL FOR Set Product Refill Supply--%>
                                                <div class="modal fade set" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-lg">
                                                        <div class="modal-content">
                                                            <form id="demo-form1" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="myModalLabel1">Product Refill</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">
                                                                            <h4 style="color: black; font-family: Bahnschrift">Set your offered products details here:</h4>
                                                                            <hr />
                                                                            <strong>Note: add other product offered if any here ( e.g empty gallon )</strong>
                                                                            <hr />
                                                                            <div class="col-md-12 col-sm-12">
                                                                                <strong>Choose only one that is applicable to your product offered</strong>
                                                                              <asp:RadioButtonList ID="radioType_productoffered" runat="server" RepeatDirection="Horizontal" onclick="handleProductRefillCheckbox()">
                                                                                    <asp:ListItem Text="Product Refill" Value="Product Refill" />
                                                                                    <asp:ListItem Text="other Product" Value="other Product" />
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <strong>Product Name:</strong>
                                                                                <asp:TextBox ID="refillwaterType" Width="464px" Height="40px" Placeholder="Enter product name offered to customer. Ex: Purified or Alkaline" runat="server"></asp:TextBox>
                                                                            </div>
                                                                            <br />
                                                                            <div class="col-md-12 col-sm-12">
                                                                                <%--PRODUCT NAME--%>
                                                                                <strong>Product Image:</strong>
                                                                                <%--file upload--%>
                                                                                <asp:FileUpload ID="prodImage" Height="40px" runat="server" Font-Size="Medium" Width="301px" />
                                                                                </div>
                                                                            <br />
                                                                            <div class="col-md-12 col-sm-12">
                                                                                <strong>Unit of Volume:</strong>
                                                                                <asp:DropDownList ID="refillUnitOfVolume" runat="server" Height="40px" Width="464px">
                                                                                    <asp:ListItem Selected="False">-----Choose One-----</asp:ListItem>
                                                                                    <asp:ListItem Text="Gallon" Value="gallon"></asp:ListItem>
                                                                                    <asp:ListItem Text="Liters" Value="liter/s"></asp:ListItem>
                                                                                    <asp:ListItem Text="Mililiters" Value="mililiter/s"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <br />
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <strong>Quantity/Number of unit:</strong>
                                                                                <asp:TextBox ID="refillQty" Height="40px" Width="464px" Placeholder="Enter how many quantity does the unit of volume have" TextMode="Number" step="any" runat="server"></asp:TextBox>
                                                                                 <br />
                                                                                <asp:RegularExpressionValidator ID="priceValidator" runat="server" ControlToValidate="refillQty"
                                                                            ErrorMessage="Please enter a valid number of unit (must input a number)" ValidationExpression="^\d+(\.\d+)?$" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                            </div>
                                                                            <br />
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <strong>Price:</strong>
                                                                               <asp:TextBox ID="refillPrice" Height="40px" Width="464px" Placeholder="Enter the price of refill cost" TextMode="Number" step="any" runat="server"></asp:TextBox>
                                                                                 <br />  
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="refillPrice"
                                                                            ErrorMessage="Please enter a valid price (must input a number)" ValidationExpression="^\d+(\.\d+)?$" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                            </div>
                                                                            <br />
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <strong>Discounts</strong>
                                                                                <br />
                                                                                <h7>Please enter the discount percentage (%) you offer for this product</h7>
                                                                                <asp:TextBox ID="refillDiscount" Height="40px" Width="464px" TextMode="Number" Placeholder="Enter discount offered" step="any" runat="server"></asp:TextBox>
                                                                                <br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="refillDiscount"
                                                                            ErrorMessage="Please enter a valid discount (must input a number)" ValidationExpression="^\d+(\.\d+)?$" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                            </div>
                                                                            <hr />
                                                                            <strong>This is intended for your other product offered (e.g empty gallon)</strong>
                                                                            <hr />
                                                                            <asp:Label ID="Label6" runat="server" Text="PRODUCT STOCK"></asp:Label>
                                                                            <div class="col-md-12 col-sm-12">
                                                                                <strong>Unit</strong>
                                                                                <asp:DropDownList ID="drdProductStock" runat="server" Height="40px" Width="464px" CssClass="product-stock-dropdown">
                                                                                    <asp:ListItem Selected="False">--- Select one unit applicable for product stock ----</asp:ListItem>
                                                                                    <asp:ListItem Text="Gallon" Value="gallon"></asp:ListItem>
                                                                                    <asp:ListItem Text="Liters" Value="liter/s"></asp:ListItem>
                                                                                    <asp:ListItem Text="Mililiters" Value="mililiter/s"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <br />
                                                                            <%--PRODUCT Available--%>
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <strong>Quantity(Stock)/Number:</strong>
                                                                               <asp:TextBox ID="txtStockQty" Height="40px" TextMode="Number" CssClass="stock-qty-textbox" Placeholder="Enter how many stock available base on the unit being selected" Product="Enter the available stock of the product above" runat="server" Width="464px"></asp:TextBox>
                                                                                 <br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtStockQty"
                                                                            ErrorMessage="Please enter a valid number of unit of the product stock (must input a number)" ValidationExpression="^\d+(\.\d+)?$" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                            </div>
                                                                            <br />
                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <%-- set data button--%>
                                                                        <asp:Button ID="btnSet" runat="server" Text="Add Product Refill" class="btn btn-primary btn-sm" OnClick="btnSet_Click" />
                                                                    </div>
                                                                </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%-- end set product refill --%>
                                                            <script>
                                                                function handleProductRefillCheckbox() {
                                                                    console.log("handleProductRefillCheckbox called");
                                                                    var refillCheckbox = document.querySelector("#<%= radioType_productoffered.ClientID %> input:checked");
                                                                    var stockQtyTextbox = document.querySelector("#<%= txtStockQty.ClientID %>");
                                                                    var stockDropdown = document.querySelector("#<%= drdProductStock.ClientID %>");

                                                                    if (refillCheckbox && refillCheckbox.value === "Product Refill") {
                                                                        stockQtyTextbox.disabled = true;
                                                                        stockDropdown.disabled = true;
                                                                    } else if (refillCheckbox && refillCheckbox.value === "other Product") {
                                                                        stockQtyTextbox.disabled = false;
                                                                        stockDropdown.disabled = false;
                                                                    }
                                                                }
                                                            </script>
                                                    <!-- MODAL FOR EDITING PRODUCT DETAILS -->
                                                <div class="modal fade" id="editproduct" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-lg">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <h5 class="modal-title">Edit Product Details</h5>
                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                    <span aria-hidden="true">X</span>
                                                                </button>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="form-group">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">

                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <strong>Price:</strong>
                                                                               <asp:TextBox ID="txtproductprice" Height="40px" Width="464px" Placeholder="Enter the price of refill cost you want to update" runat="server"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtproductprice"
                                                                            ErrorMessage="Please enter a valid price (must input a number)" ValidationExpression="^\d+(\.\d+)?$" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                            </div>
                                                                            <br />
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <strong>Discounts</strong>
                                                                                <br />
                                                                                <h7>Please enter the discount percentage (%) you offer for this product</h7>
                                                                                <asp:TextBox ID="txtproductdiscount" Height="40px" Width="464px" TextMode="Number" Placeholder="Enter discount offered you want to update" runat="server"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtproductdiscount"
                                                                            ErrorMessage="Please enter a valid discount (must input a number)" ValidationExpression="^\d+(\.\d+)?$" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                            </div>
                                                                            <br />
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                </div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <asp:Button ID="btnEditDetails" runat="server" Text="Update" class="btn btn-primary btn-sm" OnClick="btnEditProductDetails_Click" />
                                                             </div>
                                                        </div>
                                                    </div>
                                                    <asp:HiddenField runat="server" ID="hfeditProductDetails" />
                                                </div>
                                                <%-- MODAL FOR update product details --%>
                                                <div class="modal fade" id="editthirdpartyproduct" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-lg">
                                                        <div class="modal-content">
                                                            <form id="demo-form4" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="myModalLabel4">Edit third party product details here:</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <strong>Price:</strong>
                                                                                <asp:TextBox ID="txt_price" Height="40px" Width="464px" Placeholder="Enter the price of refill cost you want to update" runat="server"></asp:TextBox>
                                                                            </div>
                                                                            <br />
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <strong>Discounts</strong>
                                                                                <br />
                                                                                <h7>Please enter the discount percentage (%) you offer for this product</h7>
                                                                                <asp:TextBox ID="txt_discount" Height="40px" Width="464px" TextMode="Number" Placeholder="Enter discount offered you want to update" runat="server"></asp:TextBox>
                                                                            </div>
                                                                            <br />
                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <%--  BUTTON PrODUCT EDIT HERE--%>
                                                                        <div style="float: right;">
                                                                            <asp:Button ID="btnUpdateDetails" runat="server" Text="Update third party product details" class="btn btn-primary btn-sm" OnClick="btnUpdateOtherProductDetails_Click" />

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <asp:HiddenField runat="server" ID="hfThirdpartyProduct" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <%-- END FOR MODAL UPDATE--%>
                                                <%-- MODAL TO VIEW CERTAIN RECORDS --%>
                                                <div class="modal fade" id="view" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
                                                        <div class="modal-content">
                                                            <form id="demo-form5" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="myModalLabel5"> PRODUCT DETAILS </h4>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-xl-12 col-xl-12 ">
                                                                        <div class="x_content">
                                                                            <div class="card-block">
                                                                                <div class="table-responsive">
                                                                                    <div class="tab-content">
                                                                                        <div class="tab-pane active">
                                                                                            <asp:Label ID="lblViewSearch" runat="server" Font-Underline="true" Font-Size="14" ForeColor="black" />
                                                                                            <%--the gridview starts here--%>
                                                                                            <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                                                <asp:Label ID="lblMessage" runat="server" Font-Underline="true" ForeColor="red" />
                                                                                                <br />
                                                                                                <asp:GridView runat="server" ID="GridPro_Details" CellPadding="3" Width="975px" CssClass="auto-style1" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                                    SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                                                                                
                                                                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                                                    <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                                                    <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                                                </asp:GridView>
                                                                                                <asp:GridView runat="server" ID="GridotherProduct_Details" CellPadding="3" Width="975px" CssClass="auto-style1" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                                    SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                                                                                
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

                                                                                            <%--Gridview ends here--%>
                                                                                        </div>
                                                                                        <!--/tab-pane-->
                                                                                    </div>
                                                                                    <!--/tab-content-->
                                                                                    <%--TAB end --%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </form>
                                                        </div>
                                                    </div>
                                                </div>

                                                <br />
                                                <br />
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
                                                            </div>
                                                            <%-- TANK SUPPLY ENDS HERE --%>
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
                                                                    <asp:ListItem Text="View All" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Product Refill" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Third Party Product" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Tank Supply" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                 <div style="float: right;">
                                                                                <asp:TextBox ID="txtSearch" Width="364px" Placeholder="search by product id  or product name ...." ToolTip="enter product id number or product name to search and view record" Height="40px" runat="server"></asp:TextBox>
                                                                                <asp:Button ID="btnSearchOrder" runat="server" Text="Search" OnClick="btnSearchProduct_Click" CssClass="btn-primary" Height="40px" />
                                                                                 </div>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn-primary" Height="40px" />
                                                                <div class="table-responsive">
                                                                    <div class="tab-content">
                                                                        <div class="tab-pane active">
                                                                            <hr />
                                                                            <div style="text-align: center;">
                                                                                <asp:Label ID="lblProductData" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                                            </div>
                                                                            <br />
                                                                            <asp:GridView runat="server" ID="gridProductRefill" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="ACTION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Button ID="btnEditProduct" runat="server" Text="Edit" OnClick="btnEditProduct_Click" CssClass="btn-primary" Height="40px" />
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
                                                                            <div style="text-align: center;">
                                                                                <asp:Label ID="lblThirdparty" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                                            </div>
                                                                            <br />
                                                                            <asp:GridView runat="server" ID="gridotherProduct" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="ACTION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Button ID="btnthirdpartyProduct" runat="server" Text="Edit" OnClick="btnthirdpartyProduct_Click" CssClass="btn-primary" Height="40px" />
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
                                                                            <br />
                                                                            <div style="text-align: center;">
                                                                                <asp:Label ID="lbl_tankSupply" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                                            </div>
                                                                            <br />
                                                                            <asp:GridView runat="server" ID="gridTankSupply" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">

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
                                                                        <!--/tab-pane-->
                                                                    </div>
                                                                    <!--/tab-content-->
                                                                </div>
                                                            </div>
                                                            <div class="card-block">
                                                                <div class="table-responsive">
                                                                    <div class="tab-content">
                                                                        <div class="tab-pane active">
                                                                            <hr />
                                                                            <asp:Label ID="lblDeliveryType" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                                            <asp:Label ID="nullLabel" Font-Size="16px" runat="server" Width="364px"></asp:Label>
                                                                            <br  />
                                                                            <br />
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
