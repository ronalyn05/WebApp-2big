<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="ManageWRSClients.aspx.cs" Inherits="WRS2big_Web.superAdmin.ManageWRSClients" %>
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
                                         <div class="modal fade ViewDetails col-xl-8 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                            <div class="modal-dialog modal-dialog-centered modal-md col-xl-10 col-md-10">
                                            <div class="modal-content col-xl-10 col-md-10" style="/*background-color:red;*/ margin-left:370px">
                                               <div id="demo-form1" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                   <div class="modal-header">
                                                    <h4 class="modal-title" id="myModalLabel1">Client Full Details</h4>
                                                        <%--exit button--%>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                   </div>
                                                <div class="modal-body">
                                                     <div class="col-md-18 col-sm-18 ">
                                                        <div class="x_content">
                                                             
                                                            <hr>          
                                                        </div>
                                                         <div class="col-md-12 col-sm-12">
                                                             <asp:ImageButton ID="ClientImage" class="img-100 img-radius" style="width:200px" runat="server" />
                                                         </div>
                                                    </div> <br /><br /><br />
                                                    <div class="modal-footer">
                                                        <asp:LinkButton ID="updateButton" href="SubscriptionPlans.aspx" class="active btn btn-primary waves-effect text-right" runat="server" style="font-size:18px;"> UPDATE </asp:LinkButton>
                                                    </div>
                                                 </div>
                                               </div>
                                          </div>
                                          </div>
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
                                                                    <a class="nav-link active" data-toggle="tab" href="#AllClients" role="tab">All Clients</a>
                                                                    <div class="slide"></div>
                                                                </li>
                                                                <li class="nav-item">
                                                                    <a class="nav-link" data-toggle="tab" href="#pendingClients" role="tab">Pending Clients</a>
                                                                    <div class="slide"></div>
                                                                </li>
                                                                <li class="nav-item">
                                                                    <a class="nav-link" data-toggle="tab" href="#approvedClients" role="tab">Approved Clients</a>
                                                                    <div class="slide"></div>
                                                                </li>
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content card-block">
                                                                <div class="tab-pane active" ID="AllClients" role="tabpanel">
                                                                        <div class="col-lg-16 col-xl-18">
                                                                            <div class="card table-card">
                                                                                <div class="card-block">
                                                                                    <div class="table-responsive"> <br />
                                                                                      <asp:GridView runat="server" ID="AllGridview" class="texts table-responsive table-hover"  style=" text-align:center;overflow-y: auto;max-height: 500px; margin-left: 14px;"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                         <Columns>
                                                                                             <asp:TemplateField >
                                                                                                <ItemTemplate>
                                                                                                   <button type="button" class="active btn waves-effect text-center" data-toggle="modal" data-target=".ViewDetails" OnClick="viewButton_Clicked" style="background-color:transparent;font-size:16px;"><i class="ti-marker"></i>View</button> 
                                                                                                  
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
                                                                <div class="tab-pane" id="pendingClients" role="tabpanel">
                                                                        <div class="col-lg-12 col-xl-12">
                                                                            <div class="card table-card">
                                                                                <div class="card-block">
                                                                                    <div class="table-responsive"> <br />
                                                                                      <asp:GridView runat="server" ID="pendingGridView" class="texts table-responsive table-hover"  style=" text-align:center;overflow-y: auto;max-height: 500px; margin-left: 14px;"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                         <Columns>
                                                                                             <asp:TemplateField >
                                                                                                <ItemTemplate>
                                                                                                    <%--<asp:Button ID="ViewButton" runat="server" Text="Open" ForeColor="Black" BackColor="transparent" BorderStyle="Solid" BorderColor="White"/>--%>
                                                                                                    <button type="button" class="active btn waves-effect text-center" data-toggle="modal" data-target=".ViewDetails" style="background-color:transparent;font-size:16px;"><i class="ti-marker"></i>View</button> 
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
                                                                <div class="tab-pane" id="approvedClients" role="tabpanel">
                                                                          <div class="col-lg-12 col-xl-12">
                                                                            <div class="card table-card">
                                                                                <div class="card-block">
                                                                                    <div class="table-responsive"> <br />
                                                                                      <asp:GridView runat="server" ID="approvedGridView" class="texts table-responsive table-hover"  style=" text-align:center;overflow-y: auto;max-height: 500px; margin-left: 14px;"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                         <%--<asp:ImageButton ID="ImageButton_new" class="img-100 img-radius" style="width:200px" runat="server" /> --%>
                                                                                          <Columns>
                                                                                             <asp:TemplateField>
                                                                                                <ItemTemplate>  
                                                                                                          <asp:ImageButton ID="ImageClient" class="img-100 img-radius"  data-toggle="modal" data-target=".ViewDetails" style="width:200px" runat="server" /> 
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
