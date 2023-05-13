  <%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WaterProduct.aspx.cs" Inherits="WRS2big_Web.Admin.WaterProduct" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Include jQuery and the Timepicker plugin -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-timepicker/1.13.18/jquery.timepicker.min.css">

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
                                         <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".add"><i class="fa fa-plus"></i> Add Third Party Product Offers</button>
                                       <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".set"><i class="fa fa-plus"></i> Add Product Refill Offers</button>
                                     <%--  <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".manage"><i class="fa fa-plus"></i> Manage Delivery Details</button>--%>
                                       <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".addtank"><i class="fa fa-plus"></i> Add Tank Supply</button>
                                       <button type="button" style="font-size:14px; float:right" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".edit"><i class="fa fa-edit"></i> Edit Product Details</button> 
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

                                       <%-- MODAL FOR ADD PRODUCT--%>
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
                                           <%-- <div class="item form-group">--%>
                                                <h4 style="color:black;font-family:Bahnschrift"> Set outside offered products here:</h4>
                                                <hr />
                                                <strong>Note: This is intended for third party product offered.</strong>
                                                <hr />
                                            <div class="col-md-12 col-sm-12 ">
                                               <%--PRODUCT TYPE--%>
                                             <strong>Product Name:</strong>
                                                 <asp:TextBox ID="productName" runat="server" Placeholder="Enter the product name offered" Width="464px"></asp:TextBox>
                                            </div>
                                                <br />
                                             <div class="col-md-12 col-sm-12">
                                                 <%--PRODUCT NAME--%>
                                                 <strong>Product Image:</strong>
                                                        <%--file upload--%>
                                                        <asp:FileUpload ID="imgProduct" runat="server" Font-Size="Medium" Height="38px" Width="464px"  />
                                              </div>
                                                <br />
                                                <div class="col-md-12 col-sm-12">
                                                  <strong>Product Unit of Volume:</strong>
                                                 <asp:DropDownList ID="drdprodUnitVolume" runat="server" Height="40px" Width="464px">
                                                       <%--<asp:ListItem Selected="True">-----Choose One-----</asp:ListItem>--%>
                                                       <asp:ListItem Text="Gallon" Value="gallon" Selected="True"></asp:ListItem>
                                                       <asp:ListItem Text="ML" Value="ml" ></asp:ListItem>
                                                       <asp:ListItem Text="Liters" Value="liter/s" ></asp:ListItem>
                                                   </asp:DropDownList>
                                                     </div>
                                                <br />
                                             <div class="col-md-12 col-sm-12">
                                                  <strong>Product Quantity:</strong>
                                                 <asp:TextBox ID="productQty" TextMode="Number" step="any" Placeholder="Enter how many quantity the unit of volume have" runat="server" Width="464px"></asp:TextBox>
                                              </div>
                                                <br />
                                                <%--PRODUCT PRICE--%>
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <strong>Product Price:</strong>
                                                       <%-- <asp:Label ID="Label1" runat="server" Text="Product Price: "></asp:Label><br />--%>
                                                        <asp:TextBox ID="productPrice" TextMode="Number" step="any" Placeholder="Enter the price of product cost" runat="server" Width="464px"></asp:TextBox>
                                                     </div>
                                                <br />
                                                 <%--PRODUCT Discount--%>
                                                      <div class="col-md-12 col-sm-12 ">
                                                          <strong>Product Discount:</strong> <br />
                                                          <h7>Please enter the discount percentage (%) you offer for this product</h7>
                                                        <asp:TextBox ID="productDiscounts" TextMode="Number" step="any" Placeholder="Enter discount offered" runat="server" Width="464px"></asp:TextBox>
                                                          </div>
                                                <br />
                                                <hr />
                                                <asp:Label ID="Label5" runat="server" Text="PRODUCT STOCK"></asp:Label>
                                                <div class="col-md-12 col-sm-12">
                                                  <strong>Unit</strong>
                                                 <asp:DropDownList ID="drdUnitStock" runat="server" Height="40px" Width="464px">
                                                       <%--<asp:ListItem Selected="False">--- Select the unit of measurement for your stock---</asp:ListItem>--%>
                                                       <asp:ListItem Text="Bottle" Value="bottle"></asp:ListItem>
                                                     <asp:ListItem Text="Gallon" Value="gallon"></asp:ListItem>
                                                       <asp:ListItem Text="Box/Case" Value="box/case" ></asp:ListItem>
                                                   </asp:DropDownList>
                                                     </div>
                                                <br />
                                                        <%--PRODUCT Available--%>
                                                      <div class="col-md-12 col-sm-12 ">
                                                          <strong>Quantity(Stock):</strong>
                                                         <%-- <asp:Label ID="Label6" runat="server" Text="Product Available:"></asp:Label>--%>
                                                        <asp:TextBox ID="stockQuantity" TextMode="Number" step="any" Placeholder="Enter how many stock available base on the unit being selected" Product="Enter the available stock of the product above" runat="server" Width="464px"></asp:TextBox>
                                                          </div>
                                                             <br />
                                                         </div>
                                                  </div>
                                                </div>
                                                <div class="modal-footer">
                                               <%-- add data button--%>
                                               <asp:Button ID="btnAdd" runat="server" Text="Add Product" class="btn btn-primary btn-sm" OnClick="btnAdd_Click" AutoPostBack="false"/>
                                                <%--<asp:Button ID="btnupdate" class="btn btn-primary" runat="server" Text="Update Data" ValidationGroup="a" OnClick="btnupdate_Click" />
                                                --%></div>
                                              </div>
                                            </div>
                                             <%--  </form>--%>
                                             </div>

                                       <%-- MODAL FOR Set Product Refill Supply--%>
                                      <div class="modal fade set" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-lg">
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
                                                <h4 style="color:black;font-family:Bahnschrift"> Set your offered products details here:</h4>
                                                <hr />
                                                <strong>Note: add other product offered if any here (e.g empty gallon or new gallon with water)</strong>
                                                 <hr />
                                                <div class="col-md-12 col-sm-12 ">
                                                        <strong>Product Name:</strong>
                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                        <asp:TextBox ID="refillwaterType" Width="364px" Placeholder="Enter product name offered to customer. Ex: Purified or Alkaline" runat="server"></asp:TextBox>
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
                                                 <asp:DropDownList ID="refillUnitOfVolume" runat="server" Height="40px" Width="364px">
                                                       <%--<asp:ListItem Selected="True">-----Choose One-----</asp:ListItem>--%>
                                                       <asp:ListItem Text="Gallon" Value="gallon" Selected="True"></asp:ListItem>
                                                       <asp:ListItem Text="Liters" Value="liter/s" ></asp:ListItem>
                                                     <asp:ListItem Text="Mililiters" Value="mililiter/s" ></asp:ListItem>
                                                   </asp:DropDownList>
                                                     </div>
                                                <br />
                                                
                                                 <div class="col-md-12 col-sm-12 ">
                                                        <strong>Quantity:</strong>
                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                        <asp:TextBox ID="refillQty" Width="464px" Placeholder="Enter how many quantity does the unit of volume have" TextMode="Number" step="any" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                 <div class="col-md-12 col-sm-12 ">
                                                        <strong>Price:</strong>
                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                        <asp:TextBox ID="refillPrice" Width="364px" Placeholder="Enter the price of refill cost" TextMode="Number" step="any" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <strong>Discounts</strong> <br />
                                                         <h7>Please enter the discount percentage (%) you offer for this product</h7>
                                                        <asp:TextBox ID="refillDiscount" Width="364px" TextMode="Number" Placeholder="Enter discount offered" step="any" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                 <hr />
                                                <strong>This is intended for your other product offered(e.g empty gallon or new gallon with water)</strong>
                                                 <hr />
                                                <hr />
                                                <asp:Label ID="Label6" runat="server" Text="PRODUCT STOCK"></asp:Label>
                                                <div class="col-md-12 col-sm-12">
                                                  <strong>Unit</strong>
                                                 <asp:DropDownList ID="drdProductStock" runat="server" Height="40px" Width="364px">
                                                       <asp:ListItem Selected="False">--- Select the unit of measurement for your stock---</asp:ListItem>
                                                       <asp:ListItem Text="Bottle" Value="bottle"></asp:ListItem>
                                                     <asp:ListItem Text="Gallon" Value="gallon"></asp:ListItem>
                                                       <asp:ListItem Text="Box/Case" Value="box/case" ></asp:ListItem>
                                                   </asp:DropDownList>
                                                     </div>
                                                <br />
                                                        <%--PRODUCT Available--%>
                                                      <div class="col-md-12 col-sm-12 ">
                                                          <strong>Quantity(Stock):</strong>
                                                         <%-- <asp:Label ID="Label6" runat="server" Text="Product Available:"></asp:Label>--%>
                                                        <asp:TextBox ID="txtStockQty" TextMode="Number" Placeholder="Enter how many stock available base on the unit being selected" Product="Enter the available stock of the product above" runat="server" Width="464px"></asp:TextBox>
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
                                      <%-- MODAL FOR update product details --%>
                                       <div class="modal fade edit" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-md">
                                            <div class="modal-content">
                                            <form id="demo-form4" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel4">Edit product details here:</h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-12 col-sm-12 ">
                                            <div class="x_content">
                                                <div class="col-md-12 col-sm-12 ">
                                                        <strong>Product Id:</strong> <br />
                                                        <asp:TextBox ID="txt_productId" Width="364px" TextMode="Number" Placeholder="Enter product id you want to update" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                  <div class="col-md-12 col-sm-12 ">
                                                        <strong>Price:</strong>
                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                        <asp:TextBox ID="txt_price" Width="364px" Placeholder="Enter the price of refill cost you want to update" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <strong>Discounts</strong> <br />
                                                         <h7>Please enter the discount percentage (%) you offer for this product</h7>
                                                        <asp:TextBox ID="txt_discount" Width="364px" TextMode="Number" Placeholder="Enter discount offered you want to update" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                  </div>
                                                </div>
                                                <div class="modal-footer">
                                                <%--  BUTTON PrODUCT EDIT HERE--%>
                                                    <div style="float:right;"> 
                                                     <asp:Button ID="btnUpdateDetails" runat="server" Text="Update other product details" class="btn btn-primary btn-sm" OnClick="btnUpdateOtherProductDetails_Click"/>
                                               <asp:Button ID="btnEditDetails" runat="server" Text="Update product refill details" class="btn btn-primary btn-sm" OnClick="btnEditProductDetails_Click"/>
                                                        </div>
                                            </div>
                                              </div>
                                            </div>
                                             <%--  </form>--%>
                                             </div>
                                           </div><%-- END FOR MODAL UPDATE--%>
                                       <%-- MODAL TO VIEW CERTAIN RECORDS --%>
                                                    <div class="modal fade" id="view" tabindex="-1" role="dialog" aria-hidden="true">
                      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
                        <div class="modal-content">
                          <form id="demo-form5" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                            <div class="modal-header">
                              <h4 class="modal-title" id="myModalLabel5"> PRODUCT DETAILS
                                
                              </h4>
                              <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                            </div>
                            <div class="modal-body">
                              <div class="col-xl-12 col-xl-12 ">
                                <div class="x_content">
                                   <%-- PRODUCT DETAILS REPORTS--%>
                                     <div class="card-block">
                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                <%--the gridview starts here--%>
                                                               <div style="overflow: auto; height: 600px; text-align:center;" class="texts" >
                                                                   <asp:Label ID="lblMessage" runat="server" Font-Underline="true" ForeColor="red"/>
                                                                   <br />
                                                                    <asp:GridView runat="server" ID="GridPro_Details" CellPadding="3" Width="975px" CssClass="auto-style1" style=" text-align:center; overflow-y: auto;max-height: 500px; margin-left: 14px;" 
                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                                  
                                                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                        </asp:GridView>
                                                                   <asp:GridView runat="server" ID="GridotherProduct_Details" CellPadding="3" Width="975px" CssClass="auto-style1" style=" text-align:center; overflow-y: auto;max-height: 500px; margin-left: 14px;" 
                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                                  
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

                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
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
                                                        <div style="float:right;"> 
                                                             
                                                             <asp:TextBox ID="txtSearch" Width="364px" Placeholder="search by product id ...." ToolTip="enter product id number to search and view record" Height="40px" runat="server"></asp:TextBox>
                                                            <asp:Button ID="btnSearchOrder" runat="server" Text="Search" OnClick="btnSearchProduct_Click" CssClass="btn-primary" Height="40px"/>
                                                           <%-- <asp:TextBox ID="txtSearch" Placeholder="enter id number...." ToolTip="enter product id number to search and view record" runat="server" style="background-color:transparent; border-color:blue; border-style:solid"></asp:TextBox> --%>
                                                        <%-- <asp:Button ID="btnSearchOrder" runat="server" Text="Search" style="background-color:transparent; font-size:18px; border-color:green; border-style:solid" OnClick="btnSearchOrder_Click"/>--%>
                                                        </div>
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
                                                           <asp:ListItem Text="Product Refill" Value="0"></asp:ListItem>
                                                           <asp:ListItem Text="Third Party Product" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Tank Supply" Value="2"></asp:ListItem>
                                                         
                                                        </asp:DropDownList>

                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn-primary" Height="40px"/>
                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                <hr />
                                                               <%-- PRODUCTS REFILL NI DIRI--%>
                                                               <%-- <h5> Products Refill</h5>--%>
                                                              <div style="text-align: center;">
                                                                    <asp:Label ID="lblProductData" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                               </div>

                                                               <%--  <asp:Label ID="lblotherProduct" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                                 <asp:Label ID="lbldeliveryDetails" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>--%>
                                                             <br />
                                                                       <%--the gridview starts here--%>
                                                             <%-- <div style="overflow: auto; height: 832px; text-align:center;" class="texts" >--%>
                                                <asp:GridView runat="server" ID="gridProductRefill" class="texts table-responsive table-hover"  style=" text-align:center;overflow-y: auto;max-height: 500px; margin-left: 14px;"  
                                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px" >
                                                    <%--  <Columns>
                                                        <asp:TemplateField>
                                                          <ItemTemplate>
                                                           <%-- <asp:LinkButton ID="selectButton" runat="server" data-toggle="modal" CssClass="fa-edit" data-target=".updateModal" Text="Update" CommandName="Update"/>--%>
                                                               <%-- <button type="button" class="btn btn-primary btn-md" data-toggle="modal" data-target=".edit"><i class="fa fa-edit"></i> update</button>--%>
                                                         <%-- </ItemTemplate>
                                                        </asp:TemplateField>
                                                      </Columns>--%>
                                                       <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                                    </asp:GridView>

                                                 <%--</div>--%> <%--Gridview ends here--%>
                                                              <%--  <hr />--%>
                                                                 <%-- OTHER PRODUCTS NI DIRI--%>
                                                               <%-- <h5> Other Products</h5>--%>
                                                                <%-- <br />--%>
                                                               <%--  <asp:Label ID="lblotherProduct" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>--%>
                                                                       <%--the gridview starts here--%>
                                                            <%--  <div style="overflow: auto; height: 832px; text-align:center;" class="texts" >--%>
                                                <asp:GridView runat="server" ID="gridotherProduct" class="texts table-responsive table-hover"  style=" text-align:center;overflow-y: auto;max-height: 500px; margin-left: 14px;"  
                                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px" >
                                                     <%-- <Columns>
                                                        <asp:TemplateField>
                                                          <ItemTemplate>--%>
                                                           <%-- <asp:LinkButton ID="selectButton" runat="server" data-toggle="modal" CssClass="fa-edit" data-target=".updateModal" Text="Update" CommandName="Update"/>--%>
                                                               <%-- <button type="button" class="btn btn-primary btn-md" data-toggle="modal" data-target=".edit"><i class="fa fa-edit"></i> update</button>--%>
                                                          <%--</ItemTemplate>
                                                        </asp:TemplateField>
                                                      </Columns>--%>
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
                                                                 <asp:GridView runat="server" ID="gridTankSupply" class="texts table-responsive table-hover"  style=" text-align:center; overflow-y: auto;max-height: 500px; margin-left: 14px;"  
                                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px" >
                                                 
                                                       <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                                    </asp:GridView>
                                                <%-- </div>
                                                                <hr />--%> 
                                                                <%-- DELIVERY DETAILS NI DIRI--%>
                                                               <%-- <h5> Delivery Details</h5>
                                                                <br />--%>
                                                               <%-- <asp:Label ID="lbldeliveryDetails" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>--%>
                                                                       <%--the gridview starts here--%>
                                                                <%--<asp:Label ID="lblExpress" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>--%>
                                                             <%-- <div style="overflow: auto; height: 832px; text-align:center;" class="texts" >--%>

                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
                                                           
                                                        </div>
                                                    </div>
                                                    <div class="card-footer">
                                                                    <%-- <asp:Button ID="EditBtn" style="font-size:14px;" class="btn btn-primary btn-sm"  runat="server" Text="Edit details" OnClick="btnEdit_Click"/>
                                                                       <asp:Button ID="DeleteBtn" style="font-size:14px;" class="btn btn-danger btn-sm" runat="server"  Text="Delete Product" OnClick="DeleteBtn_Click" /> --%>
                                                    </div>
                                                    <div class="card-block">
                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                <hr />
                                                                <asp:Label ID="lblDeliveryType" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                                <asp:Label ID="nullLabel" Font-Size="16px" runat="server" Width="364px"></asp:Label>
                                                             <br /> <br />

                                            
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
