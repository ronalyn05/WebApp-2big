<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OnlineOrders.aspx.cs" Inherits="WRS2big_Web.Admin.OnlineOrders" %>

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
                                    <center>
                                        <asp:Label ID="warning" runat="server" Style="font-size: 18px; color: red;" class="danger"> </asp:Label>
                                        <br />
                                        <br />
                                        <br />
                                    </center>
                                    <!-- Page-body start -->
                                    <!-- page content -->
                                    <%--  DISPLAYING ORDER DETAILS HERE--%>
                                    <div class="card">
                                        <div class="card-header">
                                            <asp:Label ID="Label2" runat="server" Text="ORDERS FROM CUSTOMER" ForeColor="Black" Font-Size="Large" Font-Bold="True"></asp:Label>
                                            <%-- dropdown in category for payment mode--%>
                                            <br>
                                            <div style="display: flex; justify-content: space-between;">
                                                <div>
                                                    <asp:DropDownList ID="drdViewOrders" CssClass="text-center" runat="server" Height="40px" Width="364px">
                                                        <asp:ListItem Text="View All" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Express" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Standard" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Reservation" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnViewOrders" runat="server" Text="View" OnClick="btnViewOrders_Click" CssClass="btn-primary" Height="40px" />
                                                </div>
                                                <div style="float: right;">
                                                    <h6>Note:  (decline order if unforeseen emergency arises) 
                                                    </h6>
                                                </div>
                                            </div>
                                            <br />
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
                                                        <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                            <asp:Label Style="text-align: center;" ID="lblViewOrders" runat="server" ForeColor="Black" Font-Size="16"></asp:Label>
                                                            <br />
                                                            <asp:Label Style="text-align: center;" ID="lblError" runat="server" ForeColor="red" Font-Size="16"></asp:Label>
                                                            <asp:GridView runat="server" ID="gridView_order" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal"
                                                                ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ACTION">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnAccept" runat="server" Text="Accept" OnClick="btnAccept_Click" Style="background-color: transparent; font-size: 18px; border-color: green; border-style: solid" />
                                                                            <asp:Button ID="btnDecline" runat="server" Text="Decline" OnClick="btnDecline_Click" Style="background-color: transparent; font-size: 18px; border-color: red; border-style: solid" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ASSIGN DRIVER">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnAssign" runat="server" Text="Assign Driver" OnClick="btnAssignDriverClick" Style="background-color: transparent; font-size: 18px; border-color: blue; border-style: solid" />

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
                                    </div>
                                </div>
                                <!-- MODAL FOR ASSIGNING DRIVER -->
                                <div class="modal fade" id="assignDriver" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog modal-dialog-centered">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title">Assign Driver</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">X</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="form-group">
                                                    <div class="col-md-12 col-sm-12">
                                                        <div style="display: flex;">
                                                            <h6>Which driver would you like to assign to deliver the customer's order?</h6>
                                                        </div>
                                                        <asp:DropDownList ID="drdAssignDriver" runat="server" class="text-center" Height="40px" Width="364px">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary btn-sm" OnClick="btnSubmit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:HiddenField runat="server" ID="hfAssignDriver" />
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
                                                    <label for="reasonInput">Reason for Declining:</label>
                                                    <textarea class="form-control" id="reasonInput" runat="server" rows="3"></textarea>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ID="btnSub_decline" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmitDecline_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:HiddenField runat="server" ID="hfDeclineOrderID" />
                                </div>
                                <%-- end for modal DECLINING ORDER--%>
                                <!-- page content -->
                                <!-- Page-body end -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

