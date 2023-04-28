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
                                                        <div style="float:right;"> 
                                                            <asp:TextBox ID="txtSearch" Placeholder="enter id number...." ToolTip="enter order id number to search and view record" runat="server" style="background-color:transparent; border-color:blue; border-style:solid"></asp:TextBox> 
                                                         <asp:Button ID="btnSearchOrder" runat="server" Text="Search" style="background-color:transparent; font-size:18px; border-color:green; border-style:solid" OnClick="btnSearchOrder_Click"/>
                                                        </div>
                                                        
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

                                                    <%-- MODAL TO VIEW CERTAIN RECORDS --%>
                                                    <div class="modal fade" id="view" tabindex="-1" role="dialog" aria-hidden="true">
                      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
                        <div class="modal-content">
                          <form id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                            <div class="modal-header">
                              <h4 class="modal-title" id="myModalLabel3"> ORDER DETAILS: 
                                <asp:Label ID="lblOrderId" runat="server" Font-Underline="true" ForeColor="#0066ff"/>
                              </h4>
                              <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                            </div>
                            <div class="modal-body">
                              <div class="col-xl-12 col-xl-12 ">
                                <div class="x_content">
                                   <%-- WALKIN ORDERS REPORTS--%>
                                    <div class="row">
                                      <div class="col-xl-6">
                                        <asp:Label ID="Label1" Text="Order ID:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                        <asp:Label ID="lblOrder_id" runat="server" Width="364px"></asp:Label>
                                      </div>
                                        </div>
                                        <div class="row">
                                        <div class="col-xl-6">
                                        <asp:Label ID="Label2" Text="Order Type:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                        <asp:Label ID="lblorderType" runat="server" Width="364px"></asp:Label>
                                      </div>
                                            </div>
                                            <div class="row">
                                        <div class="col-xl-6">
                                         <asp:Label ID="Label3" Text="Product Name:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                        <asp:Label ID="lblproductname" runat="server" Width="364px"></asp:Label>
                                      </div>
                                                </div>
                                       <div class="row">
                                        <div class="col-xl-6">
                                        <asp:Label ID="Label4" Text="Product Unit and Size:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                        <asp:Label ID="lblproductSizeUnit" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-xl-6">
                                         <asp:Label ID="Label5" Text="Price:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                        <asp:Label ID="lblprice" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-xl-6">
                                         <asp:Label ID="Label6" Text="Quantity:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                        <asp:Label ID="lblqty" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-xl-6">
                                        <asp:Label ID="Label8" Text="Discount:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                        <asp:Label ID="lbldiscount" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-xl-6">
                                         <asp:Label ID="Label9" Text="Total Amount:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                        <asp:Label ID="lbltotalamount" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6">
                                        <asp:Label ID="Label10" Text="Date Added:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                        <asp:Label ID="lbldate" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                   <div class="row">
                                        <div class="col-xl-6">
                                         <asp:Label ID="Label11" Text="Added By:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                       <asp:Label ID="lblAddedby" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                    
                                    <%-- ONLINE ORDERS REPORTS--%>
                                     <div class="row">
                                        <div class="col-xl-6">
                                          <asp:Label ID="Label12" Text="Order ID:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                      <asp:Label ID="Lbl_orderId" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6">
                                          <asp:Label ID="Label13" Text="Customer ID:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                     <asp:Label ID="Lbl_cusId" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-xl-6">
                                           <asp:Label ID="Label14" Text="Driver Id:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                     <asp:Label ID="Lbl_driverId" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6">
                                          <asp:Label ID="Label15" Text="Total Amount:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                     <asp:Label ID="Lbl_totalAmount" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6">
                                         <asp:Label ID="Label16" Text="Date of order accepted::" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                     <asp:Label ID="Lbl_dateAccepted" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6">
                                         <asp:Label ID="Label18" Text="Date of order delivered:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                     <asp:Label ID="Lbl_dateDelivered" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-xl-6">
                                         <asp:Label ID="Label19" Text="Date of payment received:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                     <asp:Label ID="Lbl_datePayment" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6">
                                         <asp:Label ID="Label20" Text="Payment Received By:" runat="server" Width="364px"></asp:Label>
                                      </div>
                                      <div class="col-xl-6">
                                     <asp:Label ID="Lbl_payment" runat="server" Width="364px"></asp:Label>
                                      </div>
                                    </div>
                                  <%--<asp:GridView runat="server" ID="gridViewRecord" CellPadding="3" CssClass="auto-style1" SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                    <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                  </asp:GridView>--%>
                                </div>
                              </div>
                            </div>
                          </form>
                        </div>
                      </div>
                    </div>
            <%-- SEARCH, DISPLAY --%>
              <div class="card-block">
                                                       <div>
                                                        <asp:DropDownList ID="ddlSearchOptions" runat="server" CssClass="text-center" Height="40px" Width="364px">
                                                            <%-- <asp:ListItem Text="View All" Value="0"></asp:ListItem>--%>
                                                            <asp:ListItem Text="------- Select one category ----" Selected="False"></asp:ListItem>
                                                            <asp:ListItem Text="Online Order" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Walkin Order" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn-primary" Height="40px" Width="150px"/>

                                                        <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="text-center" Height="40px" Width="150px" style="float:right;">
                                                            <%-- <asp:ListItem Text="View All" Value="0"></asp:ListItem>--%>
                                                            <asp:ListItem Text="------- View list by: ----" Selected="False"></asp:ListItem>
                                                            <asp:ListItem Text="Day" Value="day"></asp:ListItem>
                                                            <asp:ListItem Text="Week" Value="week"></asp:ListItem>
                                                            <asp:ListItem Text="Month" Value="month"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                 <br /> 
                                                                 <div style="text-align: center;">
                                                                    <asp:Label ID="lblOrder" Font-Bold="true" CssClass="text-center align-items-center"  Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                               </div>
                                                                 <asp:Label ID="lblMessage" runat="server" Width="364px"></asp:Label>
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
                                                       <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                       <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                       <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                       <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                       <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                       <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                       <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                       <SortedDescendingHeaderStyle BackColor="#242121" />
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
                                                       <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                       <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                       <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                       <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                       <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                       <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                       <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                       <SortedDescendingHeaderStyle BackColor="#242121" />
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
                 </div>
        </div>
    </div>
</asp:Content>
