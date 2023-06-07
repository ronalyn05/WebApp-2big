<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsersLog.aspx.cs" Inherits="WRS2big_Web.Admin.UsersLog" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="UsersLog.aspx.cs" Inherits="WRS2big_Web.Admin.UsersLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 29px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <form id="form1" runat="server">--%>
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
                                            <h5 class="m-b-10">HISTORY </h5>
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
                                    <div class="right_col" role="main">
                                        <div class="">
                                            <div class="clearfix">


                                                <!--PAGE CONTENTS-->
                                                <div class="col-xl-12 col-xl-12 h-100">
                                                    <div class="card">
                                                        <%--<div class="card" style="background-color:#f2e2ff">--%>
                                                        <div class="card-header">
                                                            <asp:Label ID="Label1" runat="server" Text="USER ACTIVITY LOG" ForeColor="Black" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                            <hr />
                                                            <%--   SORTING CODES HERE --%>
                                                            <%-- <h6 style="color: black; font-family: Bahnschrift;margin-left:20px"> Sorting:</h6>--%>
                                                            <%-- SORT BY DATE--%>

                                                            <h7 style="margin-left: 20px">Start Date:</h7>
                                                            <asp:TextBox ID="sortStart" Style="margin-left: 20px" TextMode="Date" Width="150px" Height="40px" runat="server"></asp:TextBox>
                                                            <h7 style="margin-left: 20px">End Date:</h7>
                                                            <asp:TextBox ID="sortEnd" Style="margin-left: 20px" TextMode="Date" Width="150px" Height="40px" runat="server"></asp:TextBox>

                                                            <asp:Button runat="server" ID="generateSortedData" class="btn btn-primary" OnClick="generateSortedData_Click" Height="40" Text="Sort Date" />
                                                            <asp:LinkButton runat="server" ID="clearSort" OnClick="clearSort_Click" Text="Clear"></asp:LinkButton>
                                                            <%-- <br />
                                                            <br />--%>
                                                            <hr />
                                                            <div style="display: flex; justify-content: space-between;">
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="drdRole" CssClass="text-center" runat="server" Height="40px" Width="364px">
                                                                        <%--<asp:ListItem Text="View orders by delivery type" Selected="False"></asp:ListItem>--%>
                                                                        <asp:ListItem Text="View All" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Owner/Admin" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Cashier" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Driver" Value="3"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Button ID="btnViewRole" runat="server" Text="View" OnClick="btnViewRole_Click" CssClass="btn-primary" Height="40px" />
                                                                </div>
                                                                <div style="display: flex; justify-content: space-between;">
                                                                    <div style="float: right;">
                                                                        <asp:TextBox ID="txtSearch" Width="350px" Placeholder="search by role, firstname or fullname" ToolTip="enter role, firstname or fullname to search" Height="40px" runat="server"></asp:TextBox>
                                                                        <asp:Button ID="btnSearchLogs" runat="server" Text="Search" OnClick="btnSearchLogs_Click" CssClass="btn-primary" Height="40px" />
                                                                        </div>
                                                                </div>
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
                                                                            <h4 class="modal-title" id="myModalLabel3">ACTIVITY DETAILS
                                                                               <%-- <asp:Label ID="lbluserId" runat="server" Font-Underline="true" ForeColor="#0066ff" />--%>
                                                                            </h4>
                                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                        </div>
                                                                        <div class="modal-body">
                                                                            <div class="col-xl-12 col-xl-12 ">
                                                                                <div class="x_content">
                                                                                    <%-- USERS LOG REPORTS--%>
                                                                                    <div class="card-block">
                                                                                        <div class="table-responsive">
                                                                                            <div class="tab-content">
                                                                                                <div class="tab-pane active">
                                                                                                    <%--the gridview starts here--%>
                                                                                                    <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                                                        <asp:Label ID="lblSearchError" runat="server" CssClass="text-c-red" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                                                                         <br />
                                                                                                        <asp:Label ID="lblSearchResult" runat="server" CssClass="text-c-blue" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                                                                        <br />
                                                                                                        <hr />
                                                                                                        <asp:GridView runat="server" ID="GridLogs" CellPadding="3" Width="975px" CssClass="auto-style1" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
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
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </form>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-block">
                                                            <div class="table-responsive">
                                                                <div class="tab-content">
                                                                    <div class="tab-pane active">
                                                                        <%--the gridview starts here--%>
                                                                        <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                            <asp:Label ID="lblErrorActivity" runat="server" CssClass="text-c-red" Font-Bold="true" Font-Size="Large"></asp:Label>
                                                                            <br />
                                                                            <asp:GridView runat="server" ID="gridUserLog" CellPadding="3" Width="975px" CssClass="auto-style1" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
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
