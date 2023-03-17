<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OnlineOrders.aspx.cs" Inherits="WRS2big_Web.Admin.ProductGallon" %>
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
                        <!-- Page-header start -->
                        <div class="page-header">
                            <div class="page-block">
                                <div class="row align-items-center">
                                    <div class="col-md-8">
                                        <div class="page-header-title">
                                            <h5 class="m-b-10">ORDERS - ONLINE </h5>
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
                        <!-- Page-header end -->
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="page-wrapper">
                                    <!-- Page-body start -->
                                    <!-- page content -->
                                    <div class="right_col" role="main">
                                        <div class="">
                                            <asp:Label ID="Label2" runat="server" Text="DI PA NI MAO ANG UI" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>
                                             <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>
                                            <div class="clearfix">
                                                <button type="button" class="btn btn-success btn-sm"  style="font-size:14px;" data-toggle="modal" data-target=".add"><i class="fa fa-plus"></i> Add new Gallon</button>
                                           <%-- <asp:Button ID="ViewID" runat="server" OnClick="ViewID_Click" style="font-size:14px;" class="btn btn-success btn-sm " Text="View List of Product IDs" />--%> </div> 
                                             <br />
                                            </div>

                                        <div class="modal fade add" tabindex="-1" role="dialog" aria-hidden="true">
                                                <div class="modal-dialog modal-md">
                                                    <div class="modal-content">
                                                        <form id="demo-form2" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                            <div class="modal-header">
                                                                <h4 class="modal-title" id="myModalLabel2">Add Gallon Product</h4>
                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                    <span aria-hidden="true">X</span>
                                                                </button>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="col-md-12 col-sm-12 ">
                                                                    <div class="x_content">
                                                                        <div class="item form-group">
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                 <asp:TextBox ID="galType" runat="server" class="form-control"  placeholder="Gallon Name" ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="item form-group">
                                                                            <div class="col-md-21 col-sm-12 ">
                                                                                <asp:TextBox ID="txtQty" runat="server" class="form-control" TextMode="Number" placeholder="Quantity" ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="item form-group">
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <asp:TextBox ID="txtDeliveryPrice" runat="server" class="form-control" TextMode="Number" placeholder="Delivery Price"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="item form-group">
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <asp:TextBox ID="txtPickUp_Price" TextMode="Number" runat="server" class="form-control" type="text" placeholder="Pick-up Price"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="item form-group">
                                                                            <div class="col-md-12  col-sm-12 ">
                                                                                    <label class="small mb-1" ID="Label1" for="imgUpload" runat="server">Product Image</label>
                                                                                     <asp:FileUpload ID="ImgUpload" runat="server"/> <br/> 
                                                                                     <asp:Button ID="btnUpload" runat="server" class="form-control btn btn-dark btn-sm" Text="Upload File"/>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <%--BUTTON NI DIRI SA PAG STORE SA DATA NI GALLON--%>
                                                                <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" Text="Add" ValidationGroup="a" OnClick="btnSave_Click"/>
                                                            </div>
                                                        </form>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 ">
                                                    <div class="x_panel">
                                                        <div class="x_title">
                                                        <div class="x_content">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="card-box table-responsive">

                                                                          <!--PAGE CONTENTS-->
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
                                                                                        <h5>Gallon Product ID:</h5>
                                                                                    </div>
                                                                                    <div class="card-block">        
                                                                                       <asp:ListBox ID="ListBox1" runat="server" style="border:transparent; font-size:20px;padding:4px 7px 2px 4px;" Width="241px" Height="179px">
                                                                                       </asp:ListBox> 
                                                                                        <asp:Button ID="Button2" onclick="btnDisplay_Click" type="button" style="font-size:14px;" class="btn btn-primary btn-sm" runat="server" Text="View Complete Details" />
                                                                                    </div>
                                                                                <div class="card-footer">
                                                       
                                                        
                                                                                </div>
                                                                                </div>
                                                                              </div>
                                                   
                                                                            <div class="col-xl-9 col-md-12">
                                                                                <div class="card" style="background-color:#f2e2ff">
                                                                                    <div class="card-header">
                                                                                        <h5>GALLON PRODUCT INFORMATION</h5>
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
                                                                                                            <h5>Gallon Name: </h5>
                                                                                                            <asp:TextBox  ID="galName" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                          
                                                                                                            <h5>Quantity: </h5>
                                                                                                              <asp:TextBox  ID="galQuantity" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                                                                
                                                                                                            <h5>Pick-up Price: </h5>
                                                                                                              <asp:TextBox  ID="PckupPrice" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                                                             
                                                                                                            <h5>Delivery Price:  </h5>
                                                                                                            <asp:TextBox ID="DeliveryPrice" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px" ></asp:TextBox>

                                                                                                            <h5>Date Added:</h5> 
                                                                                                            <asp:Label ID="DateAdded" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px" ></asp:Label>
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
                                            </div>
                                        </div>
                                    </div>


                                    <!-- page content -->
                                    <!-- Page-body end -->

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>


</asp:Content>

