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
                                        <%--BUTTON to ADD--%>
                                        <button type="button" style="font-size:14px;" class="btn btn-success btn-sm" data-toggle="modal" data-target=".add"><i class="fa fa-plus"></i> Add Water Product</button>
                                            <%--<asp:Button ID="ViewID" runat="server" OnClick="ViewID_Click" style="font-size:14px;" class="btn btn-success btn-sm " Text="View List of Product IDs" />--%> </div> 

                                           <div class="modal fade add" tabindex="-1" role="dialog" aria-hidden="true">
                                            <div class="modal-dialog modal-sm">
                                            <div class="modal-content">
                                              <form id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                       <%--header ni diri--%>
                                              <div class="modal-header">
                                                        <%--title ni diri--%>
                                              <h4 class="modal-title" id="myModalLabel">Add Water Product</h4>
                                                        <%--button close ni diri--%>
                                               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">X</span></button>
                                                </div> <%--closing tag ni header--%> 
                                                       <%--body ni diri ni modal--%>
                                                <div class="modal-body">
                                                 <div class="col-md-12 col-sm-12 ">
                                                 <div class="x_content">
                                                  <div class="item form-group">
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <asp:Label ID="Label1" runat="server" Text="Product Name: "></asp:Label><br />
                                               <%--<asp:DropDownList ID="DrdproductType" runat="server" >
                                                   <asp:ListItem Text="Alkaline" Value="Alkaline" Selected="True"></asp:ListItem>
                                                   <asp:ListItem Text="Mineral" Value="Mineral" ></asp:ListItem>
                                                   <asp:ListItem Text="Sparkling Water" Value="Sparkling" ></asp:ListItem>
                                                   <asp:ListItem Text="Distilled Drinking Water" Value="Distilled" ></asp:ListItem>
                                                   <asp:ListItem Text="Purified Drinking Water" Value="Purified" ></asp:ListItem>
                                               </asp:DropDownList>--%>
                                                        <asp:TextBox ID="txtproductName" runat="server"></asp:TextBox>
                                                     </div>
                                                     </div>
                                                     <div class="item form-group">
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <asp:Label ID="Label6" runat="server" Text="Product Image: "></asp:Label><br />
                                                        <%--file upload--%>
                                                        <asp:FileUpload ID="imgProduct" runat="server" Font-Size="Medium" Height="38px" Width="301px"  />
                                                            
                                                        
                                                     </div>
                                                     </div>
                                                     <div class="item form-group">
                                                      <div class="col-md-12 col-sm-12 ">
                                                          <asp:Label ID="Label2" runat="server" Text="Volume/Size Category:"></asp:Label> <br />
                                                        <asp:TextBox ID="txtproductSize" runat="server"></asp:TextBox>
                                                           <%--<asp:DropDownList ID="DrdproductSize" runat="server" Width="180px">
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
                                                          </div>
                                                        </div>
                                                     <div class="item form-group">
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <asp:Label ID="Label3" runat="server" Text="Product Price: "></asp:Label><br />
                                                        <asp:TextBox ID="productPrice" TextMode="Number" runat="server"></asp:TextBox>
                                                     </div>
                                                         <div class="item form-group">
                                                      <div class="col-md-12 col-sm-12 ">
                                                          <asp:Label ID="Label7" runat="server" Text="Product Discount:"></asp:Label>
                                                        <asp:TextBox ID="productDiscounts" TextMode="Number" runat="server"></asp:TextBox>
                                                          </div>
                                                        </div>
                                                     <div class="item form-group">
                                                      <div class="col-md-12 col-sm-12 ">
                                                          <asp:Label ID="Label4" runat="server" Text="Product Available:"></asp:Label>
                                                        <asp:TextBox ID="productAvailable" TextMode="Number" runat="server"></asp:TextBox>
                                                          </div>
                                                        </div>
                                                         </div>
                                                         <hr />
                                                         <h4 style="color:black;font-family:Bahnschrift"> Water Refill Supply</h4>
                                                        <div class="item form-group">
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <asp:Label ID="Label5" runat="server" Text="Set daily amount of water for refill"></asp:Label><br />
                                                        <asp:TextBox ID="txtWaterAmount" runat="server"></asp:TextBox>
                                                     </div>
                                                         </div>
                                                     </div>
                                                    </div>
                                                    </div>
                                                     <div class="modal-footer">
                                                        <%--Button Add ni diri--%>
                                                         <asp:Button ID="btnAdd" runat="server" Text="Add Product" class="btn btn-primary btn-sm" OnClick="btnAdd_Click"/>
                                                </div>
                                               </form>
                                             </div>
                                           </div>
                                         </div>
                                     <%-- end add product--%>
                                    <%--//</div>--%>
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
                                                            <h5>Water Product ID:</h5>
                                                        </div>
                                                        <div class="card-block">        
                                                           <asp:ListBox ID="ListBox1" runat="server" style="border:transparent; font-size:20px;padding:4px 7px 2px 4px;" Width="273px" Height="179px">
                                                           </asp:ListBox> 
                                                            <asp:Button ID="Button1" onclick="btnDisplay_Click" type="button" style="font-size:14px;" class="btn btn-primary btn-sm" runat="server" Text="View Complete Details" />
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

                                                                            <h5>Stock of other products Available: </h5>
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
