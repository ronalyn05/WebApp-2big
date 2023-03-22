 <%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WaterProduct.aspx.cs" Inherits="WRS2big_Web.Admin.WaterProduct" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                                         <button type="button" style="font-size:14px;" class="btn btn-success btn-sm" data-toggle="modal" data-target=".add"><i class="fa fa-plus"></i> Add Other Product Offers</button>
                                       <button type="button" style="font-size:14px;" class="btn btn-success btn-sm" data-toggle="modal" data-target=".set"><i class="fa fa-plus"></i> Add Product Refill Offers</button>
                                       <button type="button" style="font-size:14px;" class="btn btn-success btn-sm" data-toggle="modal" data-target=".manage"><i class="fa fa-plus"></i> Manage Delivery Details</button>

                                         <%--VIEW BUTTON --%>
                                          &nbsp;
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
                                                          <strong>Product Discount:</strong>
                                                        <asp:TextBox ID="productDiscounts" Placeholder="Enter discounts offered" runat="server" Width="364px"></asp:TextBox>
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
                                               <asp:Button ID="btnAdd" runat="server" Text="Add other Product" class="btn btn-primary btn-sm" OnClick="btnAdd_Click"/>
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
                                                        <strong>Discounts</strong>
                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                        <asp:TextBox ID="refillDiscount" Width="364px" Placeholder="Enter discounts offered" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                  </div>
                                                </div>
                                                <div class="modal-footer">
                                               <%-- set data button--%>
                                               <asp:Button ID="btnSet" runat="server" Text="Add Product Refill" class="btn btn-primary btn-sm" OnClick="btnSet_Click"/>
                                                </div>
                                              </div>
                                            </div>
                                             <%-- </form>--%>
                                             </div>
                                            </div>
                                        <%-- end set refill supply--%>
                                       <%-- MODAL FOR Delivery details--%>
                                       <div class="modal fade manage" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-md">
                                            <div class="modal-content">
                                            <form id="demo-form2" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel2">Manage delivery details</h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-12 col-sm-12 ">
                                            <div class="x_content">
                                                <h4 style="color:black;font-family:Bahnschrift"> Set delivery details offered to customer here:</h4>
                                                <hr />
                                           <%-- <div class="item form-group">--%>
                                            <div class="col-md-12 col-sm-12 ">
                                                  <%--Delivery Type--%>
                                                  <strong>Delivery Type:</strong>
                                                        <asp:CheckBoxList ID="chkdevType" runat="server">
                                                            <asp:ListItem Text="Standard" Value="Standard"></asp:ListItem>
                                                            <asp:ListItem Text="Reservation" Value="Reservation"></asp:ListItem>
                                                            <asp:ListItem Text="Express" Value="Express"></asp:ListItem>
                                                        </asp:CheckBoxList>  
                                                        <strong>Estimated time for Express:</strong>
                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                        <asp:TextBox ID="estimatedTime" Placeholder="Enter estimated time" runat="server"></asp:TextBox>
                                                     
                                               </div>
                                              <%--  Distance--%>
                                                <div class="col-md-12 col-sm-12 ">
                                                        <strong>Free Delivery Distance:</strong>
                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                        <asp:TextBox ID="distanceDelivery" Width="364px" Placeholder="Enter the specific distance offered for free delivery" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                 <%--  delivery fee--%>
                                                <div class="col-md-12 col-sm-12 ">
                                                        <strong>Delivery fee:</strong>
                                                        <%--<asp:Label ID="Label8" runat="server" Text="Set daily amount of water refill"></asp:Label><br />--%>
                                                        <asp:TextBox ID="deliveryFee" Width="364px" Placeholder="Enter the specific amount for the delivery fee" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />
                                                <%--  estimated time--%>
                                                <%--<div class="col-md-12 col-sm-12 ">
                                                        <strong>Estimated time:</strong>
                                                        <asp:TextBox ID="estimatedTime" runat="server"></asp:TextBox>
                                                     </div>
                                                <br />--%>
                                               <div class="col-md-12 col-sm-12">
                                                   <%--Delivery Method--%>
                                                   <strong>Choose types of ordered offered to customers:</strong>
                                                        <asp:CheckBoxList ID="chkOrderMethod" runat="server">
                                                            <asp:ListItem Text="Refill" Value="Refill"></asp:ListItem>
                                                            <asp:ListItem Text="New Gallon" Value="New Gallon"></asp:ListItem>
                                                             <asp:ListItem Text="Other Products" Value="other products"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                               </div> 
                                               <div class="col-md-12 col-sm-12">
                                                   <%--Order Type--%>
                                                   <strong>Choose the type of service offered to the customers:</strong>
                                              <%--<asp:Label ID="Label9" runat="server" Text="Order Type:"></asp:Label><br />--%>
                                                        <asp:CheckBoxList ID="chkOrderType" runat="server">
                                                            <asp:ListItem Text="PickUp" Value="PickUp"></asp:ListItem>
                                                            <asp:ListItem Text="Delivery" Value="Delivery"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                              <%-- </div>--%>
                                               </div>
                                                <br />
                                                  </div>
                                                </div>
                                                <div class="modal-footer">
                                               <%-- set data button--%>
                                               <asp:Button ID="btnDeliverydetails" runat="server" Text="Add details" class="btn btn-primary btn-sm" OnClick="btnDeliverydetails_Click"/>
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
                                                            <%--<h5>Add Tank Supply:</h5>--%>
                                                            <asp:Label ID="Label1" runat="server" Text="Add Tank Supply" Font-Bold="true" Font-Size="Medium" Width="364px"></asp:Label>
                                                            <%--<button type="button" style="font-size:14px; width: 154px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".addSupply"><i class="fa fa-plus"></i> Add Tank Supply</button>--%>

                                                        </div>
                                                        <div class="card-block">   
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
                                              <%--  Tank Size--%>
                                                <div class="col-md-12 col-sm-12 ">
                                                     <strong>Tank Size:</strong>
                                                        <asp:TextBox ID="tankSize" Placeholder="Enter the size of tank water supply" runat="server" Width="300px"></asp:TextBox>
                                                     </div>
                                                         <br />
                                                          <%--  BUTTON TANK SUPPLY HERE--%>
                                                            <asp:Button ID="AddTanksupply" runat="server" class="btn btn-primary btn-sm" Text="Add Supply" OnClick="btnAddSupply_Click" Width="131px"/>
                                                          <%--  <hr />
                                                            <h5>Tank Supply for the day:</h5>--%>
                                                            <hr />
                                                             <div class="col-md-12 col-sm-12">
                                                    <strong>Tank Supply of the day:</strong>
                                                        <asp:Label ID="lbltankSupply" runat="server" Width="364px"></asp:Label>
                                                   <br />
                                                   <strong>Remaining Supply:</strong>
                                                        <asp:Label ID="lblremainingSupply" runat="server" Width="364px"></asp:Label><br />
                                               </div>
                                                            
                                                          <%-- <asp:ListBox ID="ListBox1" runat="server" style="border:transparent; font-size:20px;padding:4px 7px 2px 4px;" Width="273px" Height="179px">
                                                           </asp:ListBox> 
                                                            <asp:Button ID="Button1" onclick="btnDisplay_Click" type="button" style="font-size:14px;" class="btn btn-primary btn-sm" runat="server" Text="View Complete Details" />
                                                        --%>
                                                        </div>
                                                    <div class="card-footer">
                                                       
                                                        
                                                    </div>
                                                    </div>
                                                  </div>
                                                    
                                            <div class="col-xl-9 col-md-12">
                                                <div class="card" style="background-color:#f2e2ff">
                                                    <div class="card-header">
                                                        <h5>WATER PRODUCT INFORMATION</h5>
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
                                                                      <div class="form-group">                
                                                                        <div class="col-xs-12" style="font-size:16px">
                                                                           <h5>Product ID:</h5> 
                                                                            <asp:Label ID="LabelID" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px" ></asp:Label>
                                                                            <br>
                                                                            <h5>Product Name: </h5>
                                                                            <asp:TextBox  ID="prodName" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                          <%--<asp:DropDownList ID="DrdprodType" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px">
                                                                           <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                                           <asp:ListItem Text="Alkaline" Value="Alkaline"></asp:ListItem>
                                                                           <asp:ListItem Text="Mineral" Value="Mineral" ></asp:ListItem>
                                                                           <asp:ListItem Text="Sparkling Water" Value="Sparkling" ></asp:ListItem>
                                                                           <asp:ListItem Text="Distilled Drinking Water" Value="Distilled" ></asp:ListItem>
                                                                           <asp:ListItem Text="Purified Drinking Water" Value="Purified" ></asp:ListItem>
                                                                       </asp:DropDownList>--%>
                                                                            <h5>Volume/Size Category: </h5>
                                                                              <asp:TextBox  ID="prodSize" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                            <%--<asp:DropDownList ID="DrdprodSize" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px">
                                                                                   <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                                                   <asp:ListItem Text="1 gallon" Value="1 gallon"></asp:ListItem>
                                                                                   <asp:ListItem Text="10 liters" Value="10 liters" ></asp:ListItem>
                                                                                   <asp:ListItem Text="6.6 liters" Value="6.6 liters" ></asp:ListItem>
                                                                                   <asp:ListItem Text="1 liter" Value="1 liter" ></asp:ListItem>
                                                                                   <asp:ListItem Text="1000 ml" Value="1000 ml" ></asp:ListItem>
                                                                                   <asp:ListItem Text="500 ml" Value="500 ml" ></asp:ListItem>
                                                                                   <asp:ListItem Text="350 ml" Value="350 ml" ></asp:ListItem>
                                                                                   <asp:ListItem Text="330 ml" Value="330 ml" ></asp:ListItem>
                                                                           </asp:DropDownList>--%>
                                                                            <h5>Product Price: </h5>
                                                                              <asp:TextBox  ID="prodPrice" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                            <h5>Product Discount: </h5>
                                                                              <asp:TextBox  ID="prodDiscount" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>

                                                                            <h5>Product Available: </h5>
                                                                              <asp:TextBox  ID="prodAvailable" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>

                                                                            <h5>Water supply for refill available: </h5>
                                                                              <asp:TextBox  ID="waterSupAvailable" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>

                                                                             <h5>Date Added:  </h5>
                                                                            <asp:Label ID="LblDate" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px" ></asp:Label>
                                                                          </div>
                                                                      </div>
                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
                                                           
                                                        </div>
                                                    </div>
                                                    <div class="card-footer">
                                                                     <asp:Button ID="EditBtn" style="font-size:14px;" class="btn btn-primary btn-sm"  runat="server" Text="Edit details" OnClick="btnEdit_Click"/>
                                                                       <asp:Button ID="DeleteBtn" style="font-size:14px;" class="btn btn-danger btn-sm" runat="server"  Text="Delete Product" OnClick="DeleteBtn_Click" /> 
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
