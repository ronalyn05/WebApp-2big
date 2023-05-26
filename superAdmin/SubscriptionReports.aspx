﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="SubscriptionReports.aspx.cs" Inherits="WRS2big_Web.superAdmin.SubscriptionReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <form runat="server">--%>
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
                                            <h5 class="m-b-10">SUBSCRIPTION REPORT</h5>
                                            <h6 class="m-b-0">2BiG: Water Refilling Station Management System</h6>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="breadcrumb-title">
                                            <li class="breadcrumb-item">
                                                <a href="/superAdmin/SAdminIndex"><i class="fa fa-home"></i></a>
                                            </li>
                                            <li class="breadcrumb-item">
                                                <a href="/superAdmin/SAdminIndex">Dashboard</a>
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
                                    <!-- page content -->
                                    <div class="right_col" role="main">
                                        <div class="">

                                            <div class="clearfix">
                                                <div class="modal fade subscribedClients  col-md-12 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md col-md-10 col-md-15">
                                                        <div class="modal-content col-xl-10 col-md-10" style="margin-left: 370px">
                                                            <div id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="paymentModal"></h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">
                                                                            <%-- <div class="item form-group">--%>
                                                                            <h4 style="color: black; font-family: Bahnschrift">SUBSCRIBED CLIENTS</h4>
                                                                            <br />
                                                                            <br />
                                                                            <asp:TextBox runat="server" ID="TextBox1" Style="margin-left: 50px" Height="40" PlaceHolder="Search by Client name"> </asp:TextBox>
                                                                            <asp:Button runat="server" ID="modalSearch" class="btn btn-primary" Height="40" Text="search" OnClick="modalSearch_Click" />
                                                                            <br />
                                                                            <br />

                                                                            <div>
                                                                                <asp:GridView runat="server" ID="subscriptionReport" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1300px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">

                                                                                    <Columns>
                                                                                        <asp:TemplateField>

                                                                                            <ItemTemplate>
                                                                                                <asp:Button runat="server" Text="History" ID="viewSubscriptionHistory" Style="font-size: 16px; margin-left: 10px" class="btn btn-primary btn-sm text-center" OnClick="viewSubscriptionHistory_Click" />

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
                                                                            </div>


                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <%--  BUTTON ADD PAYMENT METHOD--%>
                                                                        <asp:Button ID="paymentButton" runat="server" Text="Confirm" class="btn btn-primary btn-sm" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!--PACKAGES -->
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <!-- Tab variant tab card start -->
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <h5>REPORTS</h5>
                                                            <div class="card-header-right">
                                                                <ul class="list-unstyled card-option">
                                                                    <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                                    <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                    <li><i class="fa fa-minus minimize-card"></i></li>
                                                                    <li><i class="fa fa-refresh reload-card"></i></li>

                                                                </ul>

                                                            </div>
                                                            <div class="header-search">
                                                                <div class="input-group">
                                                                    <asp:TextBox runat="server" ID="search" Style="margin-left: 50px" Height="40" PlaceHolder="Search by Activity"> </asp:TextBox>
                                                                    <asp:Button runat="server" ID="searchButton" class="btn btn-primary" Height="40" Text="search" />
                                                                    <%--OnClick="searchButton_Click"--%>
                                                                </div>


                                                            </div>
                                                            <br />
                                                           
                                                            <button type="button" style="font-size: 14px; margin-left: 50px" height="40" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".subscribedClients"><i class="fa fa-book"></i>View Subscribed Clients List</button>

                                                        </div>

                                                        <div class="card-block tab-icon">
                                                            <!-- Row start -->
                                                            <div class="row">
                                                                <div class="col-lg-12 col-xl-16">
                                                                    <div class="col-lg-12 col-xl-12">
                                                                        <div class="card table-card">
                                                                            <div class="card-block">
                                                                                <div class="table-responsive">
                                                                                    <br />
                                                                                    <asp:Label runat="server" Style="margin-left: 10px" ID="customersLabel"></asp:Label>

                                                                                    <asp:GridView runat="server" ID="subscriptionSales" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1300px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">

                                                                                        <Columns>
                                                                                            <asp:TemplateField>

                                                                                                <ItemTemplate>
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
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <!-- Row end -->
                                                        </div>
                                                    </div>
                                                    <!-- Tab variant tab card start -->
                                                </div>
                                            </div>
                                        </div>


                                        <!-- page content -->
                                        <!-- Page-body end -->

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--</form>--%>
</asp:Content>
