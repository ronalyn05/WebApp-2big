﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Reviews.aspx.cs" Inherits="WRS2big_Web.Admin.Reviews" %>

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
                                            <h5 class="m-b-10">REVIEWS AND RATINGS</h5>
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
                                            <div class="col-xl-12 col-xl-12 h-100">
                                                <div class="card">
                                                    <%--<div class="card" style="background-color:#f2e2ff">--%>
                                                    <div class="card-header">
                                                        <asp:Label ID="Label1" runat="server" Text="RATINGS AND FEEDBACK FROM CUSTOMER" ForeColor="Black" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                        
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
                                                    <br />
                                                    <div class="table-responsive">
                                                        <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                <%--the gridview starts here--%>
                                                                <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                     <div style="overflow: auto; text-align: center; font-size: x-large" >
                                                                        <asp:Label ID="lblAverageRating" runat="server" style = "text-align: center; justify-content: center; align-items: center;" ForeColor="Blue"></asp:Label>
                                                                         <br />
                                                                         <asp:Label ID="lblStarIcon" runat="server" style = "text-align: center; justify-content: center; align-items: center;"></asp:Label>
                                                                    </div>
                                                                    <hr />
                                                                 <%--   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="975px" CssClass="auto-style1"
                                                                        SelectionMode="FullRow" BorderColor="#CCCCCC" HorizontalAlign="Center">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="orderId" HeaderText="ORDER ID" HeaderStyle-HorizontalAlign="Center">
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="adminID" HeaderText="STATION ID" HeaderStyle-HorizontalAlign="Center">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="cusId" HeaderText="CUSTOMER ID" HeaderStyle-HorizontalAlign="Center">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="feedback" HeaderText="FEEDBACK / REVIEWS" HeaderStyle-HorizontalAlign="Center">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ratings" HeaderText="RATINGS" HeaderStyle-HorizontalAlign="Center">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <HeaderStyle VerticalAlign="Middle" BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" Height="50px" Width="400px" HorizontalAlign="Center" Font-Size="20px" />
                                                                        <RowStyle Font-Size="16px" />

                                                                    </asp:GridView>--%>
                                                                 <%--   <div>
                                                                        <asp:Label ID="lblCustomerName" runat="server" CssClass="review-label"></asp:Label>
                                                                    </div>
                                                                    <div>
                                                                        <asp:Label ID="lblstarRating" runat="server" CssClass="review-label"></asp:Label>
                                                                    </div>
                                                                    <div>
                                                                        <asp:Label ID="lblDate" runat="server" CssClass="review-label"></asp:Label>
                                                                    </div>--%>
                                                                    <div>
                                                                        <asp:Label ID="lblReview" runat="server" style = "text-align: center; justify-content: center; align-items: center;"></asp:Label>
                                                                    </div>

                                                            </div>

                                                                <%--Gridview ends here--%>
                                                            </div>
                                                            <!--/tab-pane-->
                                                        </div>
                                                        <!--/tab-content-->
                                                        <%--TAB end --%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <!-- Page-body end -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <%-- </div>--%>
</asp:Content>
