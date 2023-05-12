<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="WRS2big_Web.Admin.Reports" %>

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
                                            <h5 class="m-b-10">REPORTS </h5>
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
                        <!-- Page-header end -->
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="page-wrapper">
                                    <!-- Main-body start -->
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 ">
                                            <div class="x_panel>
                                                <div class="x_title">
                                                    <h2>Sales Report<small></small></h2>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <hr />
                                                <div class="x_content">
                                                    <div class="row">
                                                        <div class="col-xl-12 col-md-12">
                                                            <div class="card">
                                                                <div class="card-header">
                                                                     <div class="col-md-12 col-sm-12">
                                                                    <div style="display: flex; justify-content: space-between;">
                                                                          <div style="float: left;"> 
                                                                        <asp:DropDownList ID="ddlSaleTransaction" CssClass="text-center" runat="server" Height="40px" Width="364px">
                                                                             <asp:ListItem Text="Viel All Sales" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Today" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="Yesterday" Value="2"></asp:ListItem>
                                                                             <asp:ListItem Text="Past 7 days" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="Past 30 days" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="2023" Value="5"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                                 <asp:Button ID="btnView" runat="server" Text="View Report" OnClick="btnViewSale_Click" CssClass="btn-primary" Height="40px" />

                                                                               </div>

                                                                          </div>
                                                                        <%--  <input type="date" id="fromDate" style="display: none" />
                                                                        <input type="date" id="toDate" style="display: none" />--%>

                                                                   <%--     <script>
                                                                            function showCalendar() {
                                                                                var selectedOption = document.getElementById("ddlSaleTransaction").value;
                                                                                var fromDateInput = document.getElementById("fromDate");
                                                                                var toDateInput = document.getElementById("toDate");

                                                                                if (selectedOption == "4") { // From Date
                                                                                    fromDateInput.style.display = "block";
                                                                                    toDateInput.style.display = "none";
                                                                                } else if (selectedOption == "5") { // To Date
                                                                                    fromDateInput.style.display = "none";
                                                                                    toDateInput.style.display = "block";
                                                                                } else {
                                                                                    fromDateInput.style.display = "none";
                                                                                    toDateInput.style.display = "none";
                                                                                }
                                                                            }
                                                                        </script>--%>

                                                                     
                                                                        <hr />
                                                                        <br />

                                                                            <div class="col-md-12 col-sm-12">
                                                 
                                                 <asp:Label ID="lbloverallTotalSale" runat="server"  CssClass="text-dark" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                <asp:Label ID="lblTotalSales" runat="server" CssClass="text-c-purple" Font-Bold="true" Font-Size="Large"></asp:Label>
                                              </div>
                                                                         <div class="col-md-12 col-sm-12">
                                                 
                                                 <asp:Label ID="lblCombinedOrders" runat="server" CssClass="text-dark" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                <asp:Label ID="lblOrders" runat="server" CssClass="text-c-purple" Font-Bold="true" Font-Size="Large"></asp:Label>
                                              </div>
                                                                          <div class="col-md-12 col-sm-12">
                                                 <asp:Label ID="lblTodaysSale" runat="server"  CssClass="text-dark" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                <asp:Label ID="lbl_TodaysSale" runat="server" CssClass="text-c-purple" Font-Bold="true" Font-Size="Large"></asp:Label>
                                              </div>
                                                                          <div class="col-md-12 col-sm-12">
                                                  <asp:Label ID="lblYesterdaySale" runat="server"  CssClass="text-dark" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                <asp:Label ID="lbl_YesterdaySale" runat="server" CssClass="text-c-purple" Font-Bold="true" Font-Size="Large"></asp:Label>
                                              </div>
                                                                          <div class="col-md-12 col-sm-12">
                                                 <asp:Label ID="lblPastWeekSale" runat="server"  CssClass="text-dark" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                <asp:Label ID="lbl_PastWeekSale" runat="server" CssClass="text-c-purple" Font-Bold="true" Font-Size="Large"></asp:Label>
                                              </div>
                                                                          <div class="col-md-12 col-sm-12">
                                                  <asp:Label ID="lblPastmonthSale" runat="server" CssClass="text-dark" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                <asp:Label ID="lbl_PastmonthSale" runat="server" CssClass="text-c-purple" Font-Bold="true" Font-Size="Large"></asp:Label>
                                              </div>
                                                                        <div class="col-md-12 col-sm-12">
                                                  <asp:Label ID="lbl2023Sale" runat="server"  CssClass="text-dark" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                <asp:Label ID="lbl_2023Sale" runat="server" CssClass="text-c-purple" Font-Bold="true" Font-Size="Large"></asp:Label>
                                              </div>
                                                <br />
                                                                      
                                                                        <%--<div style="float: right;">
                                                                                        <asp:TextBox ID="txtSearch" Width="364px" Placeholder="enter order id...." ToolTip="enter order id number to search and view record" Height="40px" runat="server"></asp:TextBox>
                                                                                        <asp:Button ID="btnSearchOrder" runat="server" Text="Search" OnClick="btnSearchOrder_Click" CssClass="btn-primary" Height="40px" />
                                                                                    </div>--%>
                                                                    </div>
                                                             <%--       <div class="card">
                                                                        <div class="card-block">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-8">
                                                                                    <asp:Label ID="lblTotalSales" runat="server" CssClass="text-c-purple" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                                                    
                                                                                </div>
                                                                                <div class="col-4 text-right">
                                                                                    <i class="fa fa-bar-chart f-28"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-footer bg-c-purple">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-9">
                                                                                    <p class="text-white m-b-0 text-center">SALES</p>
                                                                                </div>
                                                                                <div class="col-3 text-right">
                                                                                    <i class="fa fa-line-chart text-white f-16"></i>
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
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </div>
</asp:Content>
