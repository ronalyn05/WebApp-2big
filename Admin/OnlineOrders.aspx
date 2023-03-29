<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OnlineOrders.aspx.cs" Inherits="WRS2big_Web.Admin.ProductGallon" %>
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
                        <!-- Page-header start -->
                        <div class="page-header">
                            <div class="page-block">
                                <div class="row align-items-center">
                                    <div class="col-md-8">
                                        <div class="page-header-title">
                                            <h5 class="m-b-10">ORDERS - ONLINE </h5>
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
                                    <!-- page content -->
                                      <div class="col-xl-12 col-xl-12 h-100">
                                                <div class="card">
                                                    <div class="card-header">
                                                         <asp:Label ID="Label2" runat="server" Text="ORDERS FROM CUSTOMER" ForeColor="Black" Font-Size="Large" Font-Bold="True"></asp:Label>
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
                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                      <div style="overflow: auto; height: 600px; text-align:center;" class="texts" >
                                               <asp:GridView runat="server" ID="GridView1" CellPadding="3" Width="975px" CssClass="auto-style1" SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                      <Columns>
                                                        <asp:TemplateField>
                                                          <ItemTemplate>
                                                              <asp:Button ID="btnAccept" runat="server" Text="Accept" OnClick="btnAccept_Click" Font-Bold="true" BorderStyle="None" ForeColor="White" BackColor="Green"/>
                                                              <asp:Button ID="btnDecline" runat="server" Text="Decline" OnClick="btnDecline_Click" Font-Bold="true" BorderStyle="None" ForeColor="White" BackColor="Red"/>
                                                         </ItemTemplate>
                                                        </asp:TemplateField>
                                                      </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" VerticalAlign="Middle" Height="50px" Width="400px" ForeColor="White" HorizontalAlign="Center" Font-Size="20px"/>
                                                        <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                                                        <RowStyle Width="200px" Font-Size="16px" ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                     <%-- <FooterStyle BackColor="White" ForeColor="#000066" />
                                                      <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                      <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                                                      <RowStyle Width="200px" ForeColor="#000066" />
                                                      <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                      <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                      <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                      <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                      <SortedDescendingHeaderStyle BackColor="#00547E" />--%>
                                                    </asp:GridView>
                                                 </div>
                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
                                                            <%--TAB end --%>
                                                        </div>
                                                    </div>
                                                    <div class="card-footer">
                                                                    <%-- <asp:Button ID="EditBtn" style="font-size:14px;" class="btn btn-primary btn-sm"  runat="server" Text="Update Records" OnClick="btnEdit_Click"/>
                                                                     --%> 
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


</asp:Content>

