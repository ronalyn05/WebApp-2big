﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="WRS2big_Web.superAdmin.Reports" %>
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
                                            <h5 class="m-b-10">SUPER ADMIN LOGS</h5>
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
                                            <%-- <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>--%>
                                            <div class="clearfix">
                                            </div>

                                            <!--PACKAGES -->
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <!-- Tab variant tab card start -->
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <h5>HISTORY</h5>
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
                                                                    <asp:TextBox runat="server" ID="search" Style="margin-left: 50px" Height="40" PlaceHolder="Search by SuperAdmin Name"> </asp:TextBox>
                                                                    <asp:Button runat="server" ID="searchButton" class="btn btn-primary" Height="40" Text="search" />
                                                                    <asp:Button runat="server" ID="clearSearch"  Height="40" Text="X" OnClick="clearSearch_Click" />
                                                                  
                                                                   <asp:DropDownList runat="server" ID="activityDropdown" Font-Size="18px" Height="40" Width="200px" Style="margin-left: 50px" Placeholder="Sort the data by:">
                                                                                <asp:ListItem Value="Login"> Logged-in </asp:ListItem>
                                                                                <asp:ListItem Value="Customer Evaluation"> Customer Evaluation</asp:ListItem>
                                                                                <asp:ListItem Value="Admin Evaluation"> Admin Evaluation </asp:ListItem>
                                                                                <asp:ListItem Value="Package"> Package</asp:ListItem>
                                                                                <asp:ListItem Value="Logout"> Logged-out </asp:ListItem> 
                                                                                

                                                                            </asp:DropDownList>
                                                                   <asp:Button runat="server" ID="viewActDropdown" class="btn btn-primary" Height="40" OnClick="activityDropdown_Click" Text="view" />
                                                                    
                                                                </div>
                                                                <%--   SORTING CODES HERE --%>
                                                                <h6 style="color: black; font-family: Bahnschrift;margin-left:20px"> Sort by Date of Activity:</h6>
                                                               <%-- SORT BY DATE--%>
                                                                  
                                                                                    <h7 style="margin-left:20px">Start Date:</h7>
                                                                                    <asp:TextBox ID="sortStart" style="margin-left:20px" TextMode="Date" Width="150px" runat="server"></asp:TextBox> 
                                                                                    <h7 style="margin-left:20px">End Date:</h7> 
                                                                                    <asp:TextBox ID="sortEnd" style="margin-left:20px" TextMode="Date" Width="150px" runat="server"></asp:TextBox>

                                                                                     <asp:Button runat="server" ID="generateSortedData" class="btn btn-primary" OnClick="generateSortedData_Click" Height="40" Text="Generate" />
                                                                                    <asp:LinkButton runat="server" ID="clearSort" OnClick="clearSort_Click" Text="Clear"></asp:LinkButton> 


                                                            </div>

                                                        </div>
                                                        <div class="card-block tab-icon">
                                                            <!-- Row start -->
                                                            <div class="row">
                                                                <div class="col-lg-12 col-xl-16">
                                                                    <div class="col-lg-12 col-xl-12">
                                                                        <div class="card table-card">
                                                                            <div class="card-block">
                                                                                <div class="table-responsive">
                                                                                    <br /> <asp:Label runat="server" style="margin-left:10px" id="customersLabel">SUPER ADMIN LOGS</asp:Label>
                                                                                      <asp:Label runat="server" Style="margin-left: 700px;color:red" ID="subSalesLabel"></asp:Label>
                                                                                    <asp:GridView runat="server" ID="superAdminLogs" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                        
                                                                                        <Columns>
                                                                                            <asp:TemplateField>

                                                                                                <ItemTemplate>
                                                                                                    
                                                                                                   
                                                                                                </ItemTemplate>
                                                                                                
                                                                                            </asp:TemplateField>

                                                                                        </Columns>
                                                                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                                        <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black"/>
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
