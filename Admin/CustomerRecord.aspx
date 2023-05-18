<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CustomerRecord.aspx.cs" Inherits="WRS2big_Web.Admin.CustomerRecord" %>

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
                                            <h5 class="m-b-10">LIST OF CUSTOMER RECORDS </h5>
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
                                                            <h2>List of Customer Record</h2>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>

                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div class="x_content">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="card-box table-responsive">
                                                                    <div class="col-xl-12 col-md-12">
                                                                        <div class="card">
                                                                            <div class="card-header">
                                                                                <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                                    <asp:Label ID="lblMessage" runat="server" Font-Underline="true" ForeColor="red" />
                                                                                    <br />
                                                                                    <asp:GridView runat="server" ID="gridCustomer_Details" CellPadding="3" Width="975px" CssClass="auto-style1" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">

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
                                                                                <%-- <table id="datatable1" class="table table-striped table-bordered f-1" style="width:100%">     
                                         <thead>
                                         <tr class="bg-c-green text-light" id="trialtable">
                                              <th>CUSTOMER ID</th>
                                              <th>CUSTOMER NAME</th>
                                              <th>BIRTHDATE</th>
                                              <th>ADDRESS</th>
                                              <th>EMAIL</th>
                                              <th>PHONE NUMBER</th>
                                          </tr>
                                          </thead>
                                          <tbody>
                                          <tr>
                                              <td>
                                                            <asp:Label ID="lblCustomerID" runat="server"/>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCustomerName" runat="server"/>
                                                        </td>
                                                         <td>
                                                            <asp:Label ID="lblCustomerBdate" runat="server"/>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCustomerAddress" runat="server"/>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCustomerEmail" runat="server"/>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCustomerPhonenumber" runat="server"/>
                                                        </td>
                                       </tbody>
                                     </table>--%>
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

