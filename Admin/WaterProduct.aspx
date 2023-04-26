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
                                         <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".add"><i class="fa fa-plus"></i> Add Other Product Offers</button>
                                       <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".set"><i class="fa fa-plus"></i> Add Product Refill Offers</button>
                                     <%--  <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".manage"><i class="fa fa-plus"></i> Manage Delivery Details</button>--%>
                                       <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".addtank"><i class="fa fa-plus"></i> Add Tank Supply</button>
<%--                                       <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".managePayment"><i class="fa fa-plus"></i> Manage Payment Methods</button> --%>
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
