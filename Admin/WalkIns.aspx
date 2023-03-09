<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WalkIns.aspx.cs" Inherits="WRS2big_Web.Admin.POS" %>
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
                                            <h5 class="m-b-10">WALK - IN ORDERS </h5>
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
                        <!-- Page-header end -->
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="page-wrapper">

        <!-- page content -->
        <div class="right_col" role="main">
          <div class="">

            <div class="col-xl-9 col-md-12">
                                                <div class="card" style="background-color:#f2e2ff">
                                                    <div class="card-header">
                                                        <h5>CREATE NEW ORDERS FOR WALK IN</h5>
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
                                                                           
                                                                            <h5>Product Name: </h5>
                                                                            <asp:TextBox  ID="txtprodName" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                         <br />
                                                                            <h5>Order Type: </h5>
                                                                            <asp:DropDownList ID="drdOrderType" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px">
                                                                           <asp:ListItem Selected="True" Text="Select"></asp:ListItem>
                                                                           <asp:ListItem Text="Water Refill" Value="Refill"></asp:ListItem>
                                                                           <asp:ListItem Text="New Order" Value="New Order" ></asp:ListItem>
                                                                           <asp:ListItem Text="Other Products" Value="other product" ></asp:ListItem>
                                                                       </asp:DropDownList>
                                                                            <br />
                                                                            <h5>Product Volume/Size: </h5>
                                                                              <asp:TextBox  ID="txtprodSize" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                           
                                                                            <br />
                                                                            <h5>Product Price: </h5>
                                                                              <asp:TextBox  ID="txtprice" runat="server" TextMode="Number" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                           <br />
                                                                            <h5>Product Discount: </h5>
                                                                              <asp:TextBox  ID="txtDiscount" TextMode="Number" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                            <br />
                                                                            <h5>Product Quantity: </h5>
                                                                              <asp:TextBox  ID="txtQty" TextMode="Number" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                            <br />
                                                                            <h5>Total Amount: </h5>
                                                                              <asp:TextBox  ID="txtTotalAmount" TextMode="Number" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff; font-size:16px; color:darkred; font-family:Bahnschrift;width:700px"></asp:TextBox>

                                                                            </div>
                                                                      </div>
                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
                                                           
                                                        </div>
                                                    </div>
                                                    <div class="card-footer">
                                                        <asp:Button ID="PaymentBtn" style="font-size:14px;" class="btn btn-success btn-sm"  runat="server" Text="PROCESS PAYMENT" OnClick="btnPayment_Click"/>
                                                    </div>
                                                </div>
                                               </div> 
  </div>
  </div>
                                </div>
                            </div>

          
        <!-- /page content -->
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
