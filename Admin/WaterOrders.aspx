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
                                            <div class="col-xl-12 col-md-12">
                                                <div class="card">
                                                    <div class="card-header">
                                                        
                                                        <asp:Label ID="Label7" runat="server" Font-Bold="true" Font-Size="Large" Text="LIST OF SUCCESSFUL ORDERS"></asp:Label>
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
                                                           <%-- <asp:ListItem Text="View All" Value="0"></asp:ListItem>--%>
                                                           <asp:ListItem Text="Online Order" Value="1"></asp:ListItem>
                                                           <asp:ListItem Text="Walkin Order" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn-primary" Height="40px" Width="150px"/>
                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                 <br /> 

                                                                 <asp:Label ID="lblOrder" Font-Bold="true" CssClass="text-center align-items-center"  Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                                   <br />    <br />   <%--the gridview starts here--%>
                                                           <%--   <div style="overflow: auto; height: 832px; text-align:center;" class="texts" >--%>
                                                <asp:GridView runat="server" ID="gridOrder" CellPadding="3" Width="975px" CssClass="auto-style1" SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                      <Columns>
                                                       <%-- <asp:TemplateField>
                                                          <ItemTemplate>--%>
                                                           <%-- <asp:LinkButton ID="selectButton" runat="server" data-toggle="modal" CssClass="fa-edit" data-target=".updateModal" Text="Update" CommandName="Update"/>--%>
                                                              <%--  <button type="button" class="btn btn-primary btn-md" data-toggle="modal" data-target=".edit"><i class="fa fa-edit"></i> update</button>--%>
                                                        <%--  </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                      </Columns>
                                                       <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" VerticalAlign="Middle" Height="50px" Width="400px" ForeColor="White" HorizontalAlign="Center" Font-Size="20px"/>
                                                        <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                                                        <RowStyle Width="200px" Font-Size="16px" ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                    </asp:GridView>
                                                                <%--Gridview ends here--%>
                                             <%--    </div>--%>
                                                              <%--  <br />
                                                                 <br />--%>
                                                               <%-- <hr /> --%>
                                                                <%-- DELIVERY DETAILS NI DIRI--%>
                                                               <%-- <h5> Delivery Details</h5>
                                                                <br />--%>
                                                               <%-- <asp:Label ID="lbldeliveryDetails" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>--%>
                                                                       <%--the gridview starts here--%>
                                                               <%-- <asp:Label ID="lblWalkin" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>--%>
                                                             <%-- <div style="overflow: auto; height: 832px; text-align:center;" class="texts" >--%>
                                                <asp:GridView runat="server" ID="gridWalkIn" CellPadding="3" Width="975px" CssClass="auto-style1" SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                      <Columns>
                                                     
                                                      </Columns>
                                                       <FooterStyle BackColor="White" ForeColor="#000066" />
                                                       <HeaderStyle BackColor="#006699" Font-Bold="True" VerticalAlign="Middle" Height="50px" Width="400px" ForeColor="White" HorizontalAlign="Center" Font-Size="20px"/>
                                                       <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                                                       <RowStyle Width="200px" Font-Size="16px" ForeColor="#000066" />
                                                       <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                       <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                       <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                       <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                    </asp:GridView>
                                               <%--  </div>--%> <%--Gridview ends here--%>
                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
                                                           
                                                        </div>
                                                    </div>
                                                    <div class="card-footer">
                                                                    <%-- <asp:Button ID="EditBtn" style="font-size:14px;" class="btn btn-primary btn-sm"  runat="server" Text="Edit details" OnClick="btnEdit_Click"/>
                                                                       <asp:Button ID="DeleteBtn" style="font-size:14px;" class="btn btn-danger btn-sm" runat="server"  Text="Delete Product" OnClick="DeleteBtn_Click" /> --%>
                                                    </div>
                                                </div>
                                               </div> 

                                       <%-- <table id="datatable1" class="table table-striped table-bordered f-1" style="width:100%">     
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
</asp:Content>
