<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="ManageCustomers.aspx.cs" Inherits="WRS2big_Web.superAdmin.ManageCustomers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
        .texts{
            font-size:16px;
            color:black;
        
        }
        .scrollable-listbox {
        height: 200px;
        overflow-y: auto;
         }
    </style>
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
                                            <h5 class="m-b-10"> REFILLING STATION CLIENTS</h5>
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
                                        <div class="col-sm-12">
                                            <!-- Tab variant tab card start -->
                                            <div class="card">
                                                <div class="card-header">
                                                    <h5>CLIENT APPROVAL</h5>
                                                </div>
                                                <div class="card-block tab-icon">
                                                    <!-- Row start -->
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xl-16">
                                                            <!-- <h6 class="sub-title">Tab With Icon</h6> -->
                                                            
                                                            <!-- Nav tabs -->
                                                            <ul class="nav nav-tabs md-tabs " role="tablist">
                                                                <li class="nav-item">
                                                                    <a class="nav-link active" data-toggle="tab" href="#AllCustomers" role="tab">All Customers</a>
                                                                    <div class="slide"></div>
                                                                </li>
                                                                <li class="nav-item">
                                                                    <a class="nav-link" data-toggle="tab" href="#pendingCustomers" role="tab">Pending Customers</a>
                                                                    <div class="slide"></div>
                                                                </li>
                                                                <li class="nav-item">
                                                                    <a class="nav-link" data-toggle="tab" href="#approvedCustomers" role="tab">Approved Customers</a>
                                                                    <div class="slide"></div>
                                                                </li>
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content card-block">
                                                                <div class="tab-pane active" ID="AllCustomers" role="tabpanel">
                                                                        <div class="col-lg-16 col-xl-18">
                                                                            <div class="card table-card">
                                                                                <div class="card-block">
                                                                                    <div class="table-responsive"> <br />
                                                                                      <asp:GridView runat="server" ID="AllGridview" class="texts table-responsive table-hover"  style=" text-align:center;overflow-y: auto;max-height: 500px; margin-left: 14px;"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                         <Columns>
                                                                                             <asp:TemplateField >
                                                                                                <ItemTemplate>
                                                                                                      <asp:Button runat="server" OnClick="detailsButton_Click" Text="View" style="background-color:transparent;font-size:16px;"  class="active btn waves-effect text-center"/> 
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
                                                                <div class="tab-pane" id="pendingCustomers" role="tabpanel">
                                                                        <div class="col-lg-12 col-xl-12">
                                                                            <div class="card table-card">
                                                                                <div class="card-block">
                                                                                    <div class="table-responsive"> <br />
                                                                                      <asp:GridView runat="server" ID="pendingGridView" class="texts table-responsive table-hover"  style=" text-align:center;overflow-y: auto;max-height: 500px; margin-left: 14px;"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                         <Columns>
                                                                                             <asp:TemplateField >
                                                                                                <ItemTemplate>
                                                                                                      <asp:Button runat="server"  OnClick="detailsButton_Click" Text="View" style="background-color:transparent;font-size:16px;"  class="active btn waves-effect text-center"/> 
                                                                                                   
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
                                                                <div class="tab-pane" id="approvedCustomers" role="tabpanel">
                                                                          <div class="col-lg-12 col-xl-12">
                                                                            <div class="card table-card">
                                                                                <div class="card-block">
                                                                                    <div class="table-responsive"> <br />
                                                                                      <asp:GridView runat="server" ID="approvedGridView" class="texts table-responsive table-hover"  style=" text-align:center;overflow-y: auto;max-height: 500px; margin-left: 14px;"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                         <Columns>
                                                                                             <asp:TemplateField >
                                                                                                <ItemTemplate>
                                                                                                      <asp:Button runat="server" ID="detailsButton" OnClick="detailsButton_Click" Text="View" style="background-color:transparent;font-size:16px;"  class="active btn waves-effect text-center"/> 
                                                                                                  
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
</form>
</asp:Content>