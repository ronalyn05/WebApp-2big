<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="SubscriptionReports.aspx.cs" Inherits="WRS2big_Web.superAdmin.SubscriptionReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <form runat="server">--%>
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
                                            <h5 class="m-b-10">SUBSCRIPTION REPORT</h5>
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
                                            <li class="breadcrumb-item">
                                                <a>Subscription Reports</a>
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

                                            <div class="clearfix">
                                                <%--MODAL FOR SUBSCRIBED CLIENTS--%>
                                                <div class="modal fade subscribedClients  col-md-12 col-md-12" id="subscribedModal" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md col-md-10 col-md-15">
                                                        <div class="modal-content col-xl-10 col-md-10" style="margin-left: 370px">
                                                            <div id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" ></h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" id="closeModal" onclick="closeModal_Click"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">
                                                                            <%-- <div class="item form-group">--%>
                                                                            <h4 style="color: black; font-family: Bahnschrift">SUBSCRIBED CLIENTS</h4>
                                                                            <br />
                                                                            <br />
                                                                            <asp:TextBox runat="server" ID="searchClient" Style="margin-left: 50px" Height="40" PlaceHolder="Search by Client name"> </asp:TextBox>
                                                                            <asp:Button runat="server" ID="modalSearch" class="btn btn-primary" Height="40" Text="search" OnClick="modalSearch_Click" />
                                                                             <asp:Button runat="server" ID="closeButton" Text="X" OnClick="closeButton_Click" BorderColor="Transparent" BackColor="Transparent"/> 

                                                                              <asp:DropDownList runat="server" ID="modalDropdown" Font-Size="18px" Height="40" Width="200px" Style="margin-left: 50px" Placeholder="Sort the data by:">
                                                                                <asp:ListItem Value="All"> All </asp:ListItem>
                                                                                <asp:ListItem Value="Active"> Active Subscriptions </asp:ListItem>
                                                                                <asp:ListItem Value="Inactive"> Inactive Subscriptions </asp:ListItem>
                                                                              

                                                                            </asp:DropDownList>
                                                                            <asp:Button runat="server" ID="viewSorted" class="btn btn-primary" Height="40" Text="view" />
                                                                            <br /><br />
                                                                            <div>
                                                                                <asp:GridView runat="server" ID="subscriptionReport" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1300px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">

                                                                                    <Columns>
                                                                                        <asp:TemplateField>

                                                                                            <ItemTemplate>
                                                                                               <%-- <asp:Button runat="server" Text="History" ID="viewSubscriptionHistory" Style="font-size: 16px; margin-left: 10px" class="btn btn-primary btn-sm text-center" OnClick="viewSubscriptionHistory_Click" />--%>
                                                                                               <%--  <button type="button" style="font-size: 14px; margin-left: 50px" height="40" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".clientSubHistory"><i class="fa fa-book" ></i>Subscription History</button>--%>
                                                                                                <asp:CheckBox runat="server" Style="font-size: 18px" ID="selectedClient"  OnCheckedChanged="selectedClient_CheckedChanged" AutoPostBack="true"/>
                                                                                                 
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
                                                                                </asp:GridView> <br /><br />
                                                                                <asp:Button runat="server" Text="Subscription History" ID="clientSubHistoryButton" Style="font-size: 16px; margin-left: 10px" class="btn btn-primary btn-sm text-center" OnClick="clientSubHistory_Click"/>
                                                                                    
                                                                                    <%-- <button type="button" style="font-size: 14px; margin-left: 50px" height="40" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".clientSubHistory" id="clientHistory"  runat="server">Subscription History</button>--%>
                                                                            </div>
                                                                            

                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <%--  BUTTON ADD PAYMENT METHOD--%>
<%--                                                                        <asp:Button ID="paymentButton" runat="server" Text="Confirm" class="btn btn-primary btn-sm" />--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <script>
                                                    $(document).ready(function () {
                                                        // Check the value of the session variable
                                                        var modalOpen = '<%= Session["ModalOpen"] %>';

                                                        if (modalOpen === 'True') {
                                                            //Trigger the opening of the modal
                                                            $('#subscribedModal').modal('show'); // Assuming your modal has an id of 'myModal'
                                                        }
                                                    });   

                                                </script>
                                            </div>

                                            <!--PACKAGES -->
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <!-- Tab variant tab card start -->
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <h5>REPORTS</h5>
                                                            <div class="card-header-right">
                                                                <ul class="list-unstyled card-option">
                                                                    <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                                    <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                    <li><i class="fa fa-minus minimize-card"></i></li>
                                                                    <li><i class="fa fa-refresh reload-card"></i></li>

                                                                </ul>

                                                            </div>
                                                            <div class="header-search">
                                                                <div class="input-group"> <br />
                                                                    <button type="button" style="font-size: 14px; margin-left: 50px" height="40" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".subscribedClients"><i class="fa fa-book"></i>View Subscribed Clients List</button>
                                                                   <%-- <asp:TextBox runat="server" ID="search" Style="margin-left: 70px" Height="40" PlaceHolder="Search by Activity"> </asp:TextBox>
                                                                    <asp:Button runat="server" ID="searchButton" class="btn btn-primary" Height="40" Text="search" />
                                                                   <asp:DropDownList runat="server" ID="DropDownList1" Font-Size="18px" Height="40" Width="200px" Style="margin-left: 50px" Placeholder="Sort the data by:">
                                                                                <asp:ListItem Value="All"> All </asp:ListItem>
                                                                                <asp:ListItem Value="Pending"> Pending Customers </asp:ListItem>
                                                                                <asp:ListItem Value="Approved"> Approved Customers </asp:ListItem>
                                                                                <asp:ListItem Value="Declined"> Declined Customers </asp:ListItem>

                                                                            </asp:DropDownList>
                                                                   <asp:Button runat="server" ID="Button1" class="btn btn-primary" Height="40" Text="view" />--%>
                                                            
                                                                    <br />

                                                                </div> <br />

                                                              <%--   SORTING CODES HERE --%>
                                                                <h6 style="color: black; font-family: Bahnschrift;margin-left:20px"> Sort by Subscription Date:</h6>
                                                               <%-- SORT BY DATE--%>
                                                                  
                                                                                    <h7 style="margin-left:20px">Start Date:</h7>
                                                                                    <asp:TextBox ID="sortStart" style="margin-left:20px" TextMode="Date" Width="150px" runat="server"></asp:TextBox> 
                                                                                    <h7 style="margin-left:20px">End Date:</h7> 
                                                                                    <asp:TextBox ID="sortEnd" style="margin-left:20px" TextMode="Date" Width="150px" runat="server"></asp:TextBox>

                                                                                     <asp:Button runat="server" ID="generateSortedData" class="btn btn-primary" OnClick="generateSortedData_Click" Height="40" Text="Generate" />
                                                                                    <asp:LinkButton runat="server" ID="clearSort" OnClick="clearSort_Click" Text="Clear"></asp:LinkButton> 

                                                                <%--SORT BY MONTH--%>
<%--                                                                <h7 style="margin-left:50px">Start Month:</h7>
                                                                   <asp:DropDownList runat="server" ID="sortbyMonthStart" Font-Size="18px" Height="40" Width="100px" Style="margin-left: 20px" Placeholder="Sort the data by:">
                                                                                <asp:ListItem Value="">  </asp:ListItem>
                                                                                <asp:ListItem Value="January"> January</asp:ListItem>
                                                                                <asp:ListItem Value="February"> February </asp:ListItem>
                                                                                <asp:ListItem Value="March"> March</asp:ListItem>
                                                                                <asp:ListItem Value="April">April</asp:ListItem>
                                                                                <asp:ListItem Value="May">May  </asp:ListItem>
                                                                                <asp:ListItem Value="June"> June </asp:ListItem>
                                                                                <asp:ListItem Value="July">July  </asp:ListItem>
                                                                                <asp:ListItem Value="August">August </asp:ListItem>
                                                                                <asp:ListItem Value="September"> September </asp:ListItem>
                                                                                <asp:ListItem Value="October">October </asp:ListItem>
                                                                                <asp:ListItem Value="November"> November</asp:ListItem>
                                                                                <asp:ListItem Value="December">December </asp:ListItem>
                                                                            </asp:DropDownList>

                                                                <h7 style="margin-left:20px">End Month:</h7>
                                                                   <asp:DropDownList runat="server" ID="sortbyMonthEnd" Font-Size="18px" Height="40" Width="100px" Style="margin-left: 20px" Placeholder="Sort the data by:">
                                                                                <asp:ListItem Value="">  </asp:ListItem>
                                                                                <asp:ListItem Value="January"> January</asp:ListItem>
                                                                                <asp:ListItem Value="February"> February </asp:ListItem>
                                                                                <asp:ListItem Value="March"> March</asp:ListItem>
                                                                                <asp:ListItem Value="April">April</asp:ListItem>
                                                                                <asp:ListItem Value="May">May  </asp:ListItem>
                                                                                <asp:ListItem Value="June"> June </asp:ListItem>
                                                                                <asp:ListItem Value="July">July  </asp:ListItem>
                                                                                <asp:ListItem Value="August">August </asp:ListItem>
                                                                                <asp:ListItem Value="September"> September </asp:ListItem>
                                                                                <asp:ListItem Value="October">October </asp:ListItem>
                                                                                <asp:ListItem Value="November"> November</asp:ListItem>
                                                                                <asp:ListItem Value="December">December </asp:ListItem>
                                                                            </asp:DropDownList>
                                                                     <asp:Button runat="server" ID="generateMonthSort" class="btn btn-primary"  Height="40" OnClick="generateMonthSort_Click" Text="Generate" />
                                                                  <asp:LinkButton runat="server" ID="clearMonthSort" OnClick="clearSort_Click" Text="Clear"></asp:LinkButton> --%>
                                                            </div>
                                                            <br />
                                                           <hr />
                                                                                    <h4 style="color: dodgerblue; font-family: Bahnschrift;margin-left:600px" id="subscriptionRevenue" runat="server">SUBSCRIPTION REVENUE:</h4>
                                                                                    <h4 style="color: crimson; font-family: Bahnschrift;margin-left:690px" id="overallRevenue" runat="server"></h4>
                                                        </div>

                                                        <div class="card-block tab-icon">
                                                            <!-- Row start -->
                                                            <div class="row">
                                                                <div class="col-lg-12 col-xl-16">
                                                                    <div class="col-lg-12 col-xl-12">
                                                                        <div class="card table-card">
                                                                            <div class="card-block">
                                                                                <div class="table-responsive">
                                                                                    <br />
                                                                                    <h4 style="color: black; font-family: Bahnschrift;margin-left:20px">SUBSCRIPTION SALES</h4>
                                                                                    <asp:Label runat="server" Style="margin-left: 700px;color:red" ID="subSalesLabel"></asp:Label>

                                                                                    <asp:GridView runat="server" ID="subscriptionSales" class="texts table-responsive table-hover" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1300px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px"  
                                                                                        AllowPaging="true" 
                                                                                        PageSize="10" 
                                                                                        OnPageIndexChanging="subscriptionSales_PageIndexChanging"
                                                                                       OnPreRender="subscriptionSales_PreRender"
                                                                                      
                                                                                        > 

                                                                                        <Columns>
                                                                                            <asp:TemplateField>

                                                                                                <ItemTemplate>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>

                                                                                        </Columns>
                                                                                        <PagerTemplate>
                                                                                            <div class="pager-container">
                                                                                                <asp:LinkButton ID="lnkFirst" runat="server" CommandName="Page" CommandArgument="First" Text="First" CssClass="pager-link"></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkPrev" runat="server" CommandName="Page" CommandArgument="Prev" Text="Prev" CssClass="pager-link"></asp:LinkButton>
                                                                                                <asp:Repeater runat="server" ID="pagerRepeater" OnItemCommand="pagerRepeater_ItemCommand">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton runat="server" CommandName="Page" CommandArgument='<%# Container.DataItem %>' Text='<%# Container.DataItem %>' CssClass="pager-link"></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:Repeater>
                                                                                                <asp:LinkButton ID="lnkNext" runat="server" CommandName="Page" CommandArgument="Next" Text="Next" CssClass="pager-link"></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkLast" runat="server" CommandName="Page" CommandArgument="Last" Text="Last" CssClass="pager-link"></asp:LinkButton>
                                                                                            </div>
                                                                                        </PagerTemplate>
                                                                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                                        <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                                        <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                                        <SortedDescendingHeaderStyle BackColor="#242121" />

                                                                                    </asp:GridView>
                                                                                    <br /><br />

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
    <%--</form>--%>
</asp:Content>
