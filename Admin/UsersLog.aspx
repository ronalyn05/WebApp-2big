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
                                  <div class="right_col" role="main">
                                    <div class="">
                                     <div class="clearfix">
                                       

                                            <!--PAGE CONTENTS-->
                                         <div class="col-xl-12 col-xl-12 h-100">
                                             <div class="card">
                                                <%--<div class="card" style="background-color:#f2e2ff">--%>
                                                    <div class="card-header">
                                                         <asp:Label ID="Label1" runat="server" Text="USER ACTIVITY LOG" ForeColor="Black" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                        <div style="float:right;"> 
                                                            <asp:TextBox ID="txtSearch" Width="545px" Placeholder="search by activity (must be in capital letters and spacing in between words)...." ToolTip="enter activity to search in capital letters and with space in between each word" Height="40px" runat="server"></asp:TextBox>
                                                            <asp:Button ID="btnSearchLogs" runat="server" Text="Search" OnClick="btnSearchLogs_Click" CssClass="btn-primary" Height="40px"/>
                                                            <%--<asp:TextBox ID="txtSearch" Placeholder="search activity (must be in capital letters)...." ToolTip="enter activity to search in capital letters and with space in between each word" Width ="364px" runat="server" style="background-color:transparent; border-color:blue; border-style:solid"></asp:TextBox> 
                                                         <asp:Button ID="btnSearchLogs" runat="server" Text="Search" style="background-color:transparent; font-size:18px; border-color:green; border-style:solid" OnClick="btnSearchLogs_Click"/>--%>
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
                              <h4 class="modal-title" id="myModalLabel3"> ACTIVITY DETAILS: 
                                <asp:Label ID="lbluserId" runat="server" Font-Underline="true" ForeColor="#0066ff"/>
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
                                                               <div style="overflow: auto; height: 600px; text-align:center;" class="texts" >
                                                                   <br />
                                                                    <asp:GridView runat="server" ID="GridLogs" CellPadding="3" Width="975px" CssClass="auto-style1" style=" text-align:center; overflow-y: auto;max-height: 500px; margin-left: 14px;" 
                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                                  
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

                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
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
                                                        <%-- <asp:DropDownList ID="ddlSearchOptions" runat="server" CssClass="text-center" Height="40px" Width="364px">
                                                            <asp:ListItem Selected="False" Text="---Sort-----"></asp:ListItem>
                                                            <asp:ListItem Text="Activity (alphabetically, A-Z)" Value="0"></asp:ListItem>
                                                           <asp:ListItem Text="Activity (alphabetically, Z-A)" Value="1"></asp:ListItem>
                                                           <asp:ListItem Text="Date (New to Old)" Value="2"></asp:ListItem>
                                                           <asp:ListItem Text="Date (Old to New)" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn-primary" Height="40px"/>
                                                        <br />--%>
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
                                                                    <asp:GridView runat="server" ID="gridUserLog" CellPadding="3" Width="975px" CssClass="auto-style1" style=" text-align:center; overflow-y: auto;max-height: 500px; margin-left: 14px;" 
                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                                  <%-- <asp:GridView runat="server" ID="gridUserLog" class="texts table-responsive table-hover" style=" text-align:center; overflow-y: auto; max-height: 500px; margin-left: 14px;" 
                                                   BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="5" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" 
                                                   ForeColor="Black" CellSpacing="5" Font-Size="14px">--%>
                                                                 <%-- <asp:GridView runat="server" OnRowDataBound="gridUserLog_RowDataBound" ID="gridUserLog" CellPadding="3" Width="975px" CssClass="auto-style1" 
                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >--%>
                                                                         
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

                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
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
