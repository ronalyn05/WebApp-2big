<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="ManageCustomers.aspx.cs" Inherits="WRS2big_Web.superAdmin.ManageCustomers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .texts {
            font-size: 16px;
            color: black;
        }

        .scrollable-listbox {
            height: 200px;
            overflow-y: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <form runat="server">--%>
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
                                            <h5 class="m-b-10">REGISTERED CUSTOMERS</h5>
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

                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <!-- Tab variant tab card start -->
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <h5>CUSTOMER APPROVAL</h5>
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
                                                                    <asp:TextBox runat="server" ID="search" Style="margin-left: 50px" Height="40" PlaceHolder="Search by firstname"> </asp:TextBox>
                                                                    <asp:Button runat="server" ID="searchButton" class="btn btn-primary" Height="40" Text="search" OnClick="searchButton_Click" />
                                                                     <asp:Button runat="server" ID="closeButton" Text="X" OnClick="closeButton_Click" BorderColor="Transparent" BackColor="Transparent"/> 
                                                                   <%-- <asp:DropDownList runat="server" ID="sortDropdown" Font-Size="18px" Height="40" Width="200px" Style="margin-left: 50px" Placeholder="Sort the data by:">
                                                                        <asp:ListItem Value="All"> All </asp:ListItem>
                                                                        <asp:ListItem Value="Pending"> Pending Customers </asp:ListItem>
                                                                        <asp:ListItem Value="Approved"> Approved Customers </asp:ListItem>
                                                                        <asp:ListItem Value="Declined"> Declined Customers </asp:ListItem>

                                                                    </asp:DropDownList>
                                                                    <asp:Button runat="server" ID="viewSorted" class="btn btn-primary" Height="40" Text="view" OnClick="viewSorted_Click" />--%>
                                                                </div>


                                                            </div>

                                                        </div>
                                                        <div class="card-block tab-icon">
                                                            <!-- Row start -->
                                                            <div class="row">
                                                                <div class="col-lg-12 col-xl-16">
                                                                            <div class="col-lg-16 col-xl-18">
                                                                                <div class="card table-card">
                                                                                    <div class="card-block">

                                                                                        <div class="table-responsive">
                                                                                            <div>
                                                                                              <asp:Label runat="server" ID="labelResponse" Font-Size="18px"></asp:Label>
                                                                                            <asp:GridView runat="server" ID="pendingGridView" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox runat="server" Style="font-size: 18px" ID="select" />
                                                                                                            <asp:Button runat="server" OnClick="detailsButton_Click" Text="View" Style="background-color: transparent; font-size: 16px;" class="active btn waves-effect text-center" />

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
                                                                                       
                                                                                      <asp:CheckBox runat="server" ID="selectAll" Text=" Select All" Style="font-size: 18px;margin-left:50px" OnCheckedChanged="selectAll_CheckedChanged" AutoPostBack="true"/>
                                                                                <br />
                                                                                <br />

                                                                                <asp:Button runat="server" ID="approveButton" class="btn btn-primary btn-sm text" Style="font-size: 18px;margin-left:60px" Text="APPROVE" OnClick="approveButton_Click" />
                                                                                <asp:Button runat="server" ID="declineButton" class="btn btn-primary btn-sm text" Style="font-size: 18px;" Text="DECLINE" OnClick="declineButton_Click" />

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                </div>
                                                            </div>
                                                            <!-- Row end -->
                                                              <br /><br />
                                                            <asp:Label   class="form-control-round" runat="server" id="allLabel" style="font-size:16px;color:dimgray;"> Visit All Registered Customers</asp:Label> <br />
                                                             <asp:LinkButton runat="server" Text="Registered Customers" class="btn btn-primary" style="font-size:18px;margin-left:40px" href="CustomerReports.aspx"></asp:LinkButton> 
                                                             
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
