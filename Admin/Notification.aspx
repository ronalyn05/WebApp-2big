<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="WRS2big_Web.Admin.Notification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {

            var chart = new CanvasJS.Chart("chartContainer", {
                animationEnabled: true,
                title: {
                    text: "Sales Category Pie Graph - 2022"
                },
                data: [{
                    type: "pie",
                    startAngle: 240,
                    yValueFormatString: "00",
                    indexLabel: "{label} {y}",
                    dataPoints: [
                        { y: 20, label: "Container" },
                        { y: 20, label: "Per Bottles" },
                        { y: 70, label: "Accessories" },
                    ]
                }]
            });
            chart.render();

        }
    </script>
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
                                            <h5 class="m-b-10">NOTIFICATION </h5>
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
                                            <div class="row">
                                                <div class="col-md-12 col-md-12 ">
                                                    <div class="x_panel">
                                                        <div class="x_title">
                                                              <asp:Label ID="lblMessage" runat="server" Font-Bold="true" Font-Size="Medium"/>
                                                            <div class="clearfix">
                                                                <%--BUTTON to ADD--%>
                                                                <%--<button type="button" style="font-size:14px;" class="btn btn-success btn-sm" data-toggle="modal" data-target=".add"><i class="fa fa-plus"></i> Add Delivery Type</button>--%>
                                                                <%--<button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".edit"><i class="fa fa-plus"></i> Delivery Details</button>--%>
                                                            </div>
                                                        </div>
                                                        <!--PAGE CONTENTS-->
                                                        <div class="row">
                                                           
                                                            <div class="col-xl-12 col-sm-12">
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
                                                                        
                                                                          <asp:Label ID="Label1" runat="server" Text="TRANSACTION" ForeColor="Blue" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                                    </div>
                                                                    <div class="card-block">
                                                                         <asp:Button ID="btnTransaction" type="button" Style="font-size: 14px;" OnClick="btnTransaction_Click" Text="View Orders" class="btn btn-default btn-sm" runat="server" />
                                                                       <%-- //<asp:ListBox ID="listNotif" runat="server" ></asp:ListBox>--%>
                                                                    </div>
                                                                    <div class="card-footer">
                                                                        <%-- <asp:Button ID="btnTransaction" type="button" Style="font-size: 14px;" OnClick="btnTransaction_Click" Text="View Orders" class="btn btn-default btn-sm" runat="server" />--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                         
                                                        </div>
                                                        <!--PAGE CONTENTS END-->
                                                        <!-- /page content -->
                                                         <%--<table>
                <thead>
                    <tr>
                        <th>Order ID</th>
                        <th>Delivery Type</th>
                        <th>Order Type</th>
                        <th>Order Product Type</th>
                        <th>Quantity</th>
                        <th>Price</th>
                        <th>Accept</th>
                        <th>Decline</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptOrders" runat="server">
                     
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("orderID") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblCustomerID" runat="server" Text='<%# Eval("order_CUSTOMERID") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblDeliveryType" runat="server" Text='<%# Eval("orderDeliveryType") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblOrderType" runat="server" Text='<%# Eval("orderType") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblProductType" runat="server" Text='<%# Eval("OrderProductType") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("orderQuantity") %>' />
                                </td>
                              
                                <td>
                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("orderTotalAmount") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblReservationDate" runat="server" Text='<%# Eval("OrderReservationDate") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("OrderStatus") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnAccept" runat="server" Text="Accept" CommandName="Accept" CommandArgument='<%# Eval("order_CUSTOMERID") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnDecline" runat="server" Text="Decline" CommandName="Decline" CommandArgument='<%# Eval("order_CUSTOMERID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>--%>
                                                        <%--<h2>ORDER <small>List</small></h2>
            <div class="clearfix"></div>
            </div>
             <div class="x_content">
              <div class="row">
               <div class="col-sm-12">
                <div class="card-box table-responsive">
                <table id="datatable1" class="table table-striped table-bordered" style="width:100%">  
                 <thead>
                 <tr id="trialtable">
                      <th>Order ID</th>
                      <th>Customer ID</th>
                      <th>Delivery Type</th>
                      <th>Order Type</th>
                      <th>Order Product Type</th>
                      <th>Quantity</th>
                      <th>Price</th>
                      <th>Total Amount</th>
                     <th>Order Date</th>
                      <th>Reservation Date</th>
                      <th>Status</th>
                  </tr>
                  </thead>
                  <tbody>
                   <tr>
                      <td>
                                    <asp:Label ID="lblOrderID" runat="server"/>
                                </td>
                                <td>
                                    <asp:Label ID="lblCustomerID" runat="server"/>
                                </td>
                                <td>
                                    <asp:Label ID="lblDeliveryType" runat="server"/>
                                </td>
                                <td>
                                    <asp:Label ID="lblOrderType" runat="server"/>
                                </td>
                                <td>
                                    <asp:Label ID="lblProductType" runat="server"/>
                                </td>
                                <td>
                                    <asp:Label ID="lblQuantity" runat="server"/>
                                </td>
                                <td>
                                    <asp:Label ID="lblPrice" runat="server"/>
                                </td>
                              
                                <td>
                                    <asp:Label ID="lblTotalAmount" runat="server"/>
                                </td>
                                <td>
                                    <asp:Label ID="lblDateOrder" runat="server"/>
                                </td>
                                <td>
                                    <asp:Label ID="lblReservationDate" runat="server"/>
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server"/>
                                </td>
                              </tr>
               </tbody>
             </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

