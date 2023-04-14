<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Reviews.aspx.cs" Inherits="WRS2big_Web.Admin.Reviews" %>
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
                                                <a href="/Admin/AdminIndex"> <i class="fa fa-home"></i> </a>
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
                                                    <div class="card-block">
                                                        <%-- <asp:DropDownList ID="ddlSearchOptions" runat="server" CssClass="text-center" Height="40px" Width="364px">
                                                            <asp:ListItem Selected="False" Text="---Select Role-----"></asp:ListItem>
                                                            <asp:ListItem Text="View All Logs" Value="0"></asp:ListItem>
                                                           <asp:ListItem Text="Owner/Cashier" Value="1"></asp:ListItem>
                                                           <asp:ListItem Text="Driver" Value="2"></asp:ListItem>
                                                           <asp:ListItem Text="Customer" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn-primary" Height="40px"/>--%>
                                                        <br />
                                                   <%-- </div>
                                                    <div class="card-footer">--%>
                                                                    <%-- <asp:Button ID="EditBtn" style="font-size:14px;" class="btn btn-primary btn-sm"  runat="server" Text="Update Records" OnClick="btnEdit_Click"/>
                                                                     --%> 
                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                <%--the gridview starts here--%>
                                                               <div style="overflow: auto; height: 600px; text-align:center;" class="texts" >
                                                                   <br />
                                                                 <%--  <asp:GridView runat="server" OnRowDataBound="gridUserLog_RowDataBound" ID="gridUserLog" CellPadding="3" Width="975px" CssClass="auto-style1" 
                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >--%>
                                                                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3" Width="975px" CssClass="auto-style1" 
                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                                                <Columns>
                                                                    <asp:BoundField DataField="orderId" HeaderText="ORDER ID" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="adminID" HeaderText="STATION ID" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="cusId" HeaderText="CUSTOMER ID" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="feedback" HeaderText="FEEDBACK / REVIEWS" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ratings" HeaderText="RATINGS" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                             <HeaderStyle BackColor="#006699" Font-Bold="True" VerticalAlign="Middle" Height="50px" Width="400px" ForeColor="White" HorizontalAlign="Center" Font-Size="20px"/>
                                                                            <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                                                                            <RowStyle Font-Size="16px" ForeColor="#000066" />
                                                                            <%--<RowStyle Width="200px" Font-Size="16px" ForeColor="#000066" />--%>
                                                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                        </asp:GridView>
                                                                   
                                                                    </div>

                                                               <%--Gridview ends here--%>

                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
                                                            <%--TAB end --%>
                                                        </div>
                                                    </div>
                                                </div>
                                             </div> 
                                            <%--<div class="col-xl-3 col-md-2">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-10">
                                                                <h6 class="text-c-blue">Daisy Limato</h6>
                                                                <i class="ti-star"></i>
                                                                <i class="ti-star"></i>
                                                            </div>
                                                            <div class="col-10">
                                                                <h6 class="text-c-black">"Had some problems while ordering"</h6>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>
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
    </div>
</asp:Content>
