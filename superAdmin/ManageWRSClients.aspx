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


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 ">
                                                    <div class="x_panel">
                                                        <div class="x_title">
                                                        <div class="x_content">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="card-box table-responsive">

                                                                          <!--PAGE CONTENTS-->
                                                                          <asp:GridView runat="server" ID="GridView1" class="texts table-responsive"  style=" text-align:center;overflow-y: auto;max-height: 500px; margin-left: 14px;"  BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" HtmlEncode="false" Width="1690px" CssClass="m-r-0" GridLines="Vertical">
                                                                              <AlternatingRowStyle BackColor="#DCDCDC" />
                                                                             <Columns>
                                                                                 <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Button ID="btnAccept" runat="server" Text="Accept" OnClick="btnAccept_Click" BorderStyle="Solid" ForeColor="Black" BackColor="transparent"/>
                                                                                          <asp:Button ID="btnDecline" runat="server" Text="Decline" OnClick="btnDecline_Click"  BorderStyle="Solid" ForeColor="Black" BackColor="transparent"/> <%--  <asp:LinkButton ID="approve" runat="server" Text="Approve" OnClick="approve_Click"/>--%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                             </Columns>
                                                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                                <PagerStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
                                                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                                <SortedDescendingHeaderStyle BackColor="#000065" />
                                                                            </asp:GridView>
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
