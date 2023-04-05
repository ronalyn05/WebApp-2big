<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerReports.aspx.cs" Inherits="WRS2big_Web.superAdmin.CustomerReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <form runat="server">
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
                                            <h5 class="m-b-10"> 2BIG CUSTOMERS</h5>
                                            <h6 class="m-b-0">2BiG: Water Refilling Station Management System</h6>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="breadcrumb-title">
                                            <li class="breadcrumb-item">
                                                <a href="/superAdmin/SAdminIndex"> <i class="fa fa-home"></i> </a>
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
                                                <div class="col-md-12 col-sm-12 ">
                                                    <div class="x_panel">
                                                        <div class="x_title">
                                                        <div class="x_content">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="card-box table-responsive">

                                                                          <!--PAGE CONTENTS-->
                                                                              <div class="texts table-responsive" style=" text-align:center;overflow-y: auto;max-height: 500px; margin-left: 14px;"  BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" HtmlEncode="false" Width="1626px" CssClass="m-r-0">
                                                                                        <asp:GridView runat="server" ID="customerReports" Width="1552px">
                                                                                            <Columns>
                                                                                         </Columns>
                                                                                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                                                                            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                                                                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                                                                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                                                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                                                                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                                                                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                                                                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                                                                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                                                                                        </asp:GridView>
                                                                               </div>
                                                                              <br /><br />
                                                                      <!--PAGE CONTENTS END-->

                                              
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
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
</form>
</asp:Content>
