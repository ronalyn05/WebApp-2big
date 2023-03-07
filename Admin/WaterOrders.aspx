<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WaterOrders.aspx.cs" Inherits="WRS2big_Web.Admin.WaterOrders" %>

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
                                            <h5 class="m-b-10">WATER ORDERS </h5>
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
                                                <div class="col-md-12 col-sm-12 ">
                                                    <div class="x_panel">
                                                        <div class="x_title">
                                                            <h2>Order List</h2>
                                                            <div class="clearfix">
                                                               </div>
                                                        </div>

                                    <div class="clearfix"></div>
                                    </div>
                                     <div class="x_content">
                                      <div class="row">
                                       <div class="col-sm-12">
                                        <div class="card-box table-responsive">
                                        <table id="datatable1" class="table table-striped table-bordered f-1" style="width:100%">     
                                         <thead>
                                         <tr class="bg-c-green text-light" id="trialtable">
                                              <th>ORDER ID</th>
                                              <th>CUSTOMER ID</th>
                                              <th>STORE NAME</th>
                                              <th>DELIVERY TYPE</th>
                                              <th>ORDER TYPE</th>
                                              <th>PRODUCT TYPE</th>
                                              <th>QUANTITY</th>
                                              <th>PRICE</th>
                                              <th>TOTAL AMOUNT</th>
                                             <th>ORDER DATE</th>
                                              <th>RESERVATION DATE</th>
                                              <th>STATUS</th>
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
                                                            <asp:Label ID="lblStoreName" runat="server"/>
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
                                
                                       </tbody>
                                     </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
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
