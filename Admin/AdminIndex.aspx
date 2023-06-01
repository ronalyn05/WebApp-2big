<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminIndex.aspx.cs" Inherits="WRS2big_Web.Admin.WebForm1" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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
                                            <h5 class="m-b-10">ADMIN DASHBOARD </h5>
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
                                    <!-- Page-body start -->
                                    <div class="page-body">
                                        <div class="row">
                                            <!-- task, page, download counter  start -->
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Label ID="lblOverallTotalSales" runat="server" CssClass="text-c-purple" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                                <%--  <h4 class="text-c-purple">₱30,200</h4>--%>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-bar-chart f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card-footer bg-c-purple">
                                                        <div class="row align-items-center">
                                                            <div class="col-9">
                                                                <a href="/Admin/SalesReports" class="text-white m-b-0 text-center">Overall Order Sales / day</a>
                                                                <%--  <p class="text-white m-b-0 text-center"> SALES</p>--%>
                                                            </div>
                                                            <div class="col-3 text-right">
                                                                <i class="fa fa-line-chart text-white f-16"></i>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Label ID="lblOnlineSales" runat="server" CssClass="text-c-green" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                                <%--  <h4 class="text-c-green">290+</h4>--%>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-file-text-o f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card-footer bg-c-green">
                                                        <div class="row align-items-center">
                                                            <div class="col-9">
                                                                <a href="/Admin/SalesReports" class="text-white m-b-0 text-center">Online Order Sales / day</a>
                                                                <%-- <p class="text-white m-b-0 text-center">DELIVERIES</p>--%>
                                                            </div>
                                                            <div class="col-3 text-right">
                                                                <i class="fa fa-line-chart text-white f-16"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Label ID="lblWalkinOrders" runat="server" CssClass="text-c-red" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                                <%--  <h4 class="text-c-red">145</h4>--%>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-shopping-cart f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card-footer bg-c-red">
                                                        <div class="row align-items-center">
                                                            <div class="col-9">
                                                                <a href="/Admin/SalesReports" class="text-white m-b-0 text-center">Walkin Order Sales / day</a>
                                                                <%--  <p class="text-white m-b-0 text-center">ORDERS</p>--%>
                                                            </div>
                                                            <div class="col-3 text-right">
                                                                <i class="fa fa-line-chart text-white f-16"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Label ID="lblOrders" runat="server" CssClass="text-c-blue" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                                <%--  <h4 class="text-c-blue">500</h4>--%>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-calendar-check-o f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card-footer bg-c-blue">
                                                        <div class="row align-items-center">
                                                            <div class="col-9">
                                                                <a href="/Admin/OnlineOrders" class="text-white m-b-0 text-center">Pending Orders</a>
                                                                <%-- <p class="text-white m-b-0 text-center">RESERVATIONS</p>--%>
                                                            </div>
                                                            <div class="col-3 text-right">
                                                                <i class="fa fa-line-chart text-white f-16"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                          
                                            <%-- <asp:Chart ID="chart" runat="server">

                                            </asp:Chart>--%>

                                            <%--<asp:Chart runat="server">
                                                <Series>
                                                    <asp:Series Name="Series1"></asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>--%>
                                        </div>
                                          <%--  customer graph here--%>
                                            <div id="chart_div">

                                            </div>
                                            <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
                                            <script type="text/javascript">
                                                google.charts.load('current', { packages: ['corechart'] });
                                                google.charts.setOnLoadCallback(drawChart);

                                                function drawChart() {
                                                    // Your chart data and options here
                                                    var data = google.visualization.arrayToDataTable([
                                                        ['Time Period', 'Value'],
                                                        ['Average Transaction Order (Week)', 10],
                                                        ['Average Sales (Week)', 20],
                                                        // Add more data as needed
                                                    ]);

                                                    var options = {
                                                        title: 'Average Values by Time Period',
                                                        //titleTextStyle: {
                                                        //    fontSize: 18
                                                        //},
                                                        //chartArea: {
                                                        //    width: '80%',
                                                        //    height: '80%'
                                                        //},
                                                        colors: ['blue', 'green']
                                                    };

                                                    var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
                                                    chart.draw(data, options);
                                                    
                                                }
                                            </script>
                                    </div>
                                </div>
                                <div id="styleSelector"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
