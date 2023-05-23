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
                                            <h5 class="m-b-10">ORDER REPORTS</h5>
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
                                                          <%--  <h2>Order List</h2>--%>
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
                                                                                <asp:Label ID="Label2" runat="server" Text="ORDER REPORTS" ForeColor="Black" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                                            </div>
                                                                             <!-- MODAL FOR DECLINING ORDER -->
                                                                        <div class="modal fade" id="declineModal" tabindex="-1" role="dialog" aria-hidden="true">
                                                                            <div class="modal-dialog modal-dialog-centered">
                                                                                <div class="modal-content">
                                                                                    <div class="modal-header">
                                                                                        <h5 class="modal-title">Decline Order</h5>
                                                                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                                            <span aria-hidden="true">X</span>
                                                                                        </button>
                                                                                    </div>
                                                                                    <div class="modal-body">
                                                                                        <div class="form-group">
                                                                                            <label for="reasonInput">Reason for Declining Order:</label>
                                                                                            <textarea class="form-control" id="reasonInput" runat="server" rows="3"></textarea>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="modal-footer">
                                                                                        <%--<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>--%>
                                                                                         <asp:Button ID="btnSub_decline" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmitDecline_Click" />
                                                                                       <%-- <button type="button" class="btn btn-primary" runat="server" id="btnSubmitDecline" OnClick="btnSubmitDecline_Click">Submit</button>--%>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <asp:HiddenField runat="server" ID="hfDeclineOrderID" />
                                                                        </div>

                                        <%-- end for modal DECLINING ORDER--%>
                                                                            <%-- MODAL TO VIEW CERTAIN RECORDS --%>
                                                                            <div class="modal fade" id="view" tabindex="-1" role="dialog" aria-hidden="true">
                                                                                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
                                                                                    <div class="modal-content">
                                                                                        <form id="demo-form5" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                                            <div class="modal-header">
                                                                                                <h4 class="modal-title" id="myModalLabel5">ORDER DETAILS
                                
                                                                                                </h4>
                                                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                                            </div>
                                                                                            <div class="modal-body">
                                                                                                <div class="col-xl-12 col-xl-12 ">
                                                                                                    <div class="x_content">
                                                                                                        <%-- ORDER DETAILS REPORTS--%>
                                                                                                        <div class="card-block">
                                                                                                            <div class="table-responsive">
                                                                                                                <div class="tab-content">
                                                                                                                    <div class="tab-pane active">
                                                                                                                        <%--the gridview starts here--%>
                                                                                                                        <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                                                                            <asp:Label ID="lblError_Message" runat="server" Font-Underline="true" ForeColor="red" />
                                                                                                                            <br />
                                                                                                                            <asp:GridView runat="server" ID="gridOnline_Order" CellPadding="3" Width="975px" CssClass="auto-style1" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
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
                                                                                                                            <asp:GridView runat="server" ID="gridWalkin_Order" CellPadding="3" Width="975px" CssClass="auto-style1" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
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

                                                                                                                        <%--Gridview ends here--%>
                                                                                                                    </div>
                                                                                                                    <!--/tab-pane-->
                                                                                                                </div>
                                                                                                                <!--/tab-content-->
                                                                                                                <%--TAB end --%>
                                                                                                            </div> <%--end for category--%>

                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </form>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <%-- end for modal view--%>
                                                                              <%-- MODAL TO DISPLAY THE GCASH PAYMENT PROOF --%>
                                        <div class="modal fade" id="viewPaymentProof" tabindex="-1" role="dialog" aria-hidden="true">
                                            <div class="modal-dialog modal-dialog-centered modal-lg">
                                                <div class="modal-content">
                                                    <div id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="myModalLabel3">GCASH PAYMENT PROOF </h5>
                                                            <%--exit button--%>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="col-md-12 col-sm-12 ">
                                                                <div class="x_content">
                                                                    <asp:Image ID="imgGcashProof" runat="server" CssClass="img-responsive" Height="60%" Width="100%" />

                                                                    <br />
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                                                            <%-- SEARCH, DISPLAY --%>
                                                                            <%--  DISPLAYING COD ORDER DETAILS HERE--%>
                                                                            <div class="card">
                                                                               <div class="card-header">
                                                                                   
                                                                                <div style="display: flex; justify-content: space-between;">
                                                                                    <div style="float: left;"> 
                                                                                        <asp:DropDownList ID="ddlSearchOptions" CssClass="text-center" runat="server" Height="40px" Width="364px">
                                                                                            <asp:ListItem Text="View orders by: " Selected="False"></asp:ListItem>
                                                                                             <asp:ListItem Text="All COD orders  " Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="Status - Accepted " Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="Status - Declined " Value="2"></asp:ListItem>
                                                                                            <asp:ListItem Text="Status - Delivered " Value="3"></asp:ListItem>
                                                                                            <asp:ListItem Text="Status - Payment Received " Value="4"></asp:ListItem>
                                                                                          <%--  <asp:ListItem Text="Delivery Types - Express" Value="5"></asp:ListItem>
                                                                                            <asp:ListItem Text="Delivery Types - Standard" Value="6"></asp:ListItem>
                                                                                            <asp:ListItem Text="Delivery Types - Reservation" Value="7"></asp:ListItem>--%>
                                                                                        </asp:DropDownList>
                                                                                        <asp:Button ID="btnView" runat="server" Text="View Report" OnClick="btnView_Click" CssClass="btn-primary" Height="40px" />
                                                                                    </div>
                                                                                    <div style="float: right;">
                                                                                        <asp:TextBox ID="txtSearch" Width="364px" Placeholder="enter order id...." ToolTip="enter order id number to search and view record" Height="40px" runat="server"></asp:TextBox>
                                                                                        <asp:Button ID="btnSearchOrder" runat="server" Text="Search" OnClick="btnSearchOrder_Click" CssClass="btn-primary" Height="40px" />
                                                                                    </div>
                                                                                </div>
                                                                                   <br />
                                                                               </div>

                                                                                <div class="card-block">
                                                                                    <div class="table-responsive">
                                                                                        <div class="tab-content">
                                                                                            <div class="tab-pane active">
                                                                                                <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                                                    <asp:Label Style="text-align: center;" ID="lblOrder" runat="server" ForeColor="Black" Font-Size="16"></asp:Label>
                                                                                                    <br />
                                                                                                    <asp:Label Style="text-align: center;" ID="lblMessage" runat="server" ForeColor="Red" Font-Size="16"></asp:Label>
                                                                                                    <asp:GridView runat="server" ID="gridOrder" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal"
                                                                                                        ForeColor="Black" CellSpacing="20" Font-Size="14px">


                                                                                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                                                        <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                                                        <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                                                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                                                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                                                        <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                                                    </asp:GridView>
                                                                                                    <asp:GridView runat="server" ID="gridStatusAccepted" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal"
                                                                                                        ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="ACTION">
                                                                                                                <ItemTemplate>
                                                                                                                     <asp:Button ID="btnDecline" runat="server" Text="Decline" OnClick="btnDecline_Click" Style="background-color: transparent; font-size: 18px; border-color: red; border-style: solid" />
                                                                          
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
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
                                                                                                      
                                                                                                    <br />

                                                                                                </div>
                                                                                                    <br />

                                                                                                </div>
                                                                                            </div>
                                                                                            <!--/tab-pane-->
                                                                                        </div>
                                                                                        <!--/tab-content-->

                                                                                        <%--TAB end --%>
                                                                                    </div>
                                                                                </div>
                                                                            </div> <%--card end --%>
                                                                           <%-- GCASH--%>
                                                                            <div class="card">
                                                                               <div class="card-header">
                                                                                   
                                                                                <div style="display: flex; justify-content: space-between;">
                                                                                    <div style="float: left;"> 
                                                                                        <asp:DropDownList ID="drdGcash" CssClass="text-center" runat="server" Height="40px" Width="364px">
                                                                                            <asp:ListItem Text="View orders by: " Selected="False"></asp:ListItem>
                                                                                            <asp:ListItem Text=" All Gcash Order " Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="Status - Accepted " Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="Status - Declined " Value="2"></asp:ListItem>
                                                                                            <asp:ListItem Text="Status - Delivered " Value="3"></asp:ListItem>
                                                                                            <asp:ListItem Text="Status - Payment Received " Value="4"></asp:ListItem>
                                                                                          <%--  <asp:ListItem Text="Delivery Types - Express" Value="5"></asp:ListItem>
                                                                                            <asp:ListItem Text="Delivery Types - Standard" Value="6"></asp:ListItem>
                                                                                            <asp:ListItem Text="Delivery Types - Reservation" Value="7"></asp:ListItem>--%>
                                                                                        </asp:DropDownList>
                                                                                        <asp:Button ID="btnGcash" runat="server" Text="View Report" OnClick="btnViewGcash_Click" CssClass="btn-primary" Height="40px" />
                                                                                    </div>
                                                                                   <%-- <div style="float: right;">
                                                                                        <asp:TextBox ID="TextBox1" Width="364px" Placeholder="enter order id...." ToolTip="enter order id number to search and view record" Height="40px" runat="server"></asp:TextBox>
                                                                                        <asp:Button ID="Button2" runat="server" Text="Search" OnClick="btnSearchOrder_Click" CssClass="btn-primary" Height="40px" />
                                                                                    </div>--%>
                                                                                </div>
                                                                                   <br />
                                                                               </div>

                                                                                <div class="card-block">
                                                                                    <div class="table-responsive">
                                                                                        <div class="tab-content">
                                                                                            <div class="tab-pane active">
                                                                                                <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                                                    <asp:Label Style="text-align: center;" ID="lblGcashError" runat="server" ForeColor="Red" Font-Size="16"></asp:Label>
                                                                                                    <asp:GridView runat="server" ID="gridGcash_order" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal"
                                                                    ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                   <%-- <Columns>
                                                                        <asp:TemplateField HeaderText="ACTION">
                                                                            <ItemTemplate>
                                                                             
                                                                                <asp:Button ID="btnDeclineGcash" runat="server" Text="Decline" OnClick="btnDeclineGcash_Click" Style="background-color: transparent; font-size: 18px; border-color: red; border-style: solid" />
                                                                              
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>--%>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="PAYMENT">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnPaymentAccept" runat="server" Text="Payment Received" OnClick="btnPaymentAcceptGcash_Click" Style="background-color: transparent; font-size: 18px; border-color: darkblue; border-style: solid" />

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="GCASH PROOF PAYMENT">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnPaymentProof" runat="server" Text="View Payment Proof" OnClick="btnPaymentProof_Click" Style="background-color: transparent; font-size: 18px; border-color: green; border-style: solid" data-toggle="modal" data-target="#viewPaymentProof" />

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
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
                                                                                                    <asp:GridView runat="server" ID="gridGcashAccepted_order" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal"
                                                                                                        ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                                         <Columns>
                                                                                                            <asp:TemplateField HeaderText="ACTION">
                                                                                                                <ItemTemplate>
                                                                                                                     <asp:Button ID="btnDeclineGcash" runat="server" Text="Decline" OnClick="btnDeclineGcash_Click" Style="background-color: transparent; font-size: 18px; border-color: red; border-style: solid" />
                                                                          
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
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
                                                                                                    <br />

                                                                                                </div>
                                                                                            </div>
                                                                                            <!--/tab-pane-->
                                                                                        </div>
                                                                                        <!--/tab-content-->

                                                                                        <%--TAB end --%>
                                                                                    </div>
                                                                                </div>
                                                                            </div> <%--card end --%>
                                                                             <%-- POINTS--%>
                                                                            <div class="card">
                                                                               <div class="card-header">
                                                                                <div style="display: flex; justify-content: space-between;">
                                                                                    <div style="float: left;"> 
                                                                                        <asp:DropDownList ID="drdPoints" CssClass="text-center" runat="server" Height="40px" Width="364px">
                                                                                            <asp:ListItem Text="View orders by: " Selected="False"></asp:ListItem>
                                                                                            <asp:ListItem Text=" All Points Order " Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="Status - Accepted " Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="Status - Declined " Value="2"></asp:ListItem>
                                                                                            <asp:ListItem Text="Status - Delivered " Value="3"></asp:ListItem>
                                                                                            <asp:ListItem Text="Status - Payment Received " Value="4"></asp:ListItem>
                                                                                           <%-- <asp:ListItem Text="Delivery Types - Express" Value="5"></asp:ListItem>
                                                                                            <asp:ListItem Text="Delivery Types - Standard" Value="6"></asp:ListItem>
                                                                                            <asp:ListItem Text="Delivery Types - Reservation" Value="7"></asp:ListItem>--%>
                                                                                        </asp:DropDownList>
                                                                                        <asp:Button ID="btnPoints" runat="server" Text="View Report" OnClick="btnViewPoints_Click" CssClass="btn-primary" Height="40px" />
                                                                                    </div>
                                                                                  <%--  <div style="float: right;">
                                                                                        <asp:TextBox ID="TextBox2" Width="364px" Placeholder="enter order id...." ToolTip="enter order id number to search and view record" Height="40px" runat="server"></asp:TextBox>
                                                                                        <asp:Button ID="Button3" runat="server" Text="Search" OnClick="btnSearchOrder_Click" CssClass="btn-primary" Height="40px" />
                                                                                    </div>--%>
                                                                                </div>
                                                                                   <br />
                                                                               </div>

                                                                                <div class="card-block">
                                                                                    <div class="table-responsive">
                                                                                        <div class="tab-content">
                                                                                            <div class="tab-pane active">
                                                                                                <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                                                    <asp:Label Style="text-align: center;" ID="Label6" runat="server" ForeColor="Black" Font-Size="16"></asp:Label>
                                                                                                    <br />
                                                                                                    <asp:Label Style="text-align: center;" ID="Label7" runat="server" ForeColor="Red" Font-Size="16"></asp:Label>
                                                                                                     <asp:GridView runat="server" ID="gridRewardPoints_order" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal"
                                                                    ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ACTION">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnPaymentAcceptPoints" runat="server" Text="Payment Received" OnClick="btnPaymentAcceptPoints_Click" Style="background-color: transparent; font-size: 18px; border-color: darkblue; border-style: solid" />

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
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
                                                                                                     <asp:GridView runat="server" ID="gridorder_accepted" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal"
                                                                                                        ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="ACTION">
                                                                                                                <ItemTemplate>
                                                                                                                     <asp:Button ID="btnDecline" runat="server" Text="Decline" OnClick="btnDeclinePoints_Click" Style="background-color: transparent; font-size: 18px; border-color: red; border-style: solid" />
                                                                          
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
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
                                                                                                    <br />

                                                                                                </div>
                                                                                            </div>
                                                                                            <!--/tab-pane-->
                                                                                        </div>
                                                                                        <!--/tab-content-->

                                                                                        <%--TAB end --%>
                                                                                    </div>
                                                                                </div>
                                                                            </div> <%--card end --%>
                                                                            <%-- WALKIN--%>
                                                                            <div class="card">
                                                                               <div class="card-header">
                                                                                   <h4>WALKIN ORDER</h4>
                                                                                   <hr />
                                                                                <div class="card-block">
                                                                                    <div class="table-responsive">
                                                                                        <div class="tab-content">
                                                                                            <div class="tab-pane active">
                                                                                                <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                                                   <asp:Label Style="text-align: center;" ID="Label10" runat="server" ForeColor="Red" Font-Size="16"></asp:Label>
                                                                                                    <asp:GridView runat="server" ID="gridWalkIn" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal"
                                                                                                        ForeColor="Black" CellSpacing="20" Font-Size="14px">

                                                                                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                                                        <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                                                        <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                                                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                                                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                                                        <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                                                    </asp:GridView>
                                                                                                    <br />
                                                                                                </div>
                                                                                            </div>
                                                                                            <!--/tab-pane-->
                                                                                        </div>
                                                                                        <!--/tab-content-->

                                                                                        <%--TAB end --%>
                                                                                    </div>
                                                                                </div>
                                                                            </div> <%--card end --%>
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
        </div>
</asp:Content>
