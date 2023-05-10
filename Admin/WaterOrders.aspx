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

                                                                                <asp:Label ID="Label2" runat="server" Text="ORDER REPORTS" ForeColor="Black" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                                               

                                                                                <%--<div style="display: flex; justify-content: left;">
                                                                                    <asp:DropDownList ID="ddlSearchOptions" CssClass="text-center" runat="server" Height="40px" Width="364px">
                                                                                        <asp:ListItem Text="All Orders Report" Value="0"></asp:ListItem>
                                                                                        <asp:ListItem Text="Online Order Report" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="Walkin Order Report" Value="2"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:Button ID="btnView" runat="server" Text="View Report" OnClick="btnView_Click" CssClass="btn-primary" Height="40px" />
                                                                                </div>
                                                                                <div style="float: right;">
                                                                                    <asp:TextBox ID="txtSearch" Width="364px" Placeholder="enter order id...." ToolTip="enter order id number to search and view record" Height="40px" runat="server"></asp:TextBox>
                                                                                    <asp:Button ID="btnSearchOrder" runat="server" Text="Search" OnClick="btnSearchOrder_Click" CssClass="btn-primary" Height="40px" />
                                                                                </div>--%>


                                                                                <%--<div class="card-header-right">
                                                                                    <ul class="list-unstyled card-option">
                                                                                        <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                                                        <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                                        <li><i class="fa fa-minus minimize-card"></i></li>
                                                                                        <li><i class="fa fa-refresh reload-card"></i></li>
                                                                                        <li><i class="fa fa-trash close-card"></i></li>
                                                                                    </ul>
                                                                                </div>--%>
                                                                            </div>

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
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </form>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <%-- end for modal view--%>

                                                                            <%-- SEARCH, DISPLAY --%>
                                                                            <%--  DISPLAYING ORDER DETAILS HERE--%>
                                                                            <div class="card">
                                                                               <div class="card-header">
                                                                                   
                                                                                <div style="display: flex; justify-content: space-between;">
                                                                                    <div style="float: left;"> 
                                                                                        <asp:DropDownList ID="ddlSearchOptions" CssClass="text-center" runat="server" Height="40px" Width="364px">
                                                                                            <asp:ListItem Text="All Orders Report" Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="Online Order Report" Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="Walkin Order Report" Value="2"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <asp:Button ID="btnView" runat="server" Text="View Report" OnClick="btnView_Click" CssClass="btn-primary" Height="40px" />
                                                                                    </div>
                                                                                    <div style="float: right;">
                                                                                        <asp:TextBox ID="txtSearch" Width="364px" Placeholder="enter order id...." ToolTip="enter order id number to search and view record" Height="40px" runat="server"></asp:TextBox>
                                                                                        <asp:Button ID="btnSearchOrder" runat="server" Text="Search" OnClick="btnSearchOrder_Click" CssClass="btn-primary" Height="40px" />
                                                                                    </div>
                                                                                </div>
                                                                                   <br />
                                                     <%-- 
                                                <div class="card-header-right">
                                                    <ul class="list-unstyled card-option">
                                                        <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                        <li><i class="fa fa-window-maximize full-card"></i></li>
                                                        <li><i class="fa fa-minus minimize-card"></i></li>
                                                        <li><i class="fa fa-refresh reload-card"></i></li>
                                                        <li><i class="fa fa-trash close-card"></i></li>
                                                    </ul>
                                                </div>--%>
                                            </div>
                                                                                <div class="card-block">
                                                                                    <div class="table-responsive">
                                                                                        <div class="tab-content">
                                                                                            <div class="tab-pane active">
                                                                                                <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                                                    <asp:Label Style="text-align: center;" ID="lblOrder" runat="server" ForeColor="Black" Font-Size="16"></asp:Label>
                                                                                                    <br />
                                                                                                    <asp:Label Style="text-align: center;" ID="lblMessage" runat="server" ForeColor="Black" Font-Size="16"></asp:Label>
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
                                                                                                    <br />
                                                                                                    <asp:Label Style="text-align: center;" ID="lblWalkIn" runat="server" ForeColor="Black" Font-Size="16"></asp:Label>
                                                                                                    <br />
                                                                                                    <asp:Label Style="text-align: center;" ID="lblMessageError" runat="server" ForeColor="Red" Font-Size="16"></asp:Label>
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
                                                                                <div class="card-footer">
                                                                                    <%-- <asp:Button ID="EditBtn" style="font-size:14px;" class="btn btn-primary btn-sm"  runat="server" Text="Update Records" OnClick="btnEdit_Click"/>
                                                                                    --%>
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
            </div>
        </div>
</asp:Content>
