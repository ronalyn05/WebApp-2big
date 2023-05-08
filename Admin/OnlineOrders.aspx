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

                                   <%-- update records here--%>
                                    <button type="button" style="font-size:14px;" class="btn btn-primary btn-md" data-toggle="modal" data-target="#assign"><i class="fa fa-plus"></i> Assign Driver</button>
                                      <div class="col-xl-12 col-xl-12 h-100">
                                           <%-- MODAL FOR UPDATE RECORDS --%>
                                                    <div class="modal fade" id="assign" tabindex="-1" role="dialog" aria-hidden="true">
                                                      <div class="modal-dialog modal-dialog-centered modal-lg">
                                                          <div class="modal-content">
                                                              <div id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                  <div class="modal-header">
                                                                      <h5 class="modal-title" id="myModalLabel3">ASSIGN DRIVER: </h5>
                                                                      <%--exit button--%>
                                                                      <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                  </div>
                                                                  <div class="modal-body">
                                                                      <div class="col-md-12 col-sm-12 ">
                                                                          <div class="x_content">
                                                                               <h5 style="color:black;font-family:Bahnschrift"> You can assign the driver to deliver the certain order to customer's order here:</h5>
                                                <hr />
                                                                              <div class="col-md-12 col-sm-12">
                                                                                  <h6>Which order would you like to assign to the driver to deliver the customer's order?</h6>
                                                                                 <div style="display: flex;">
                                                                                  <asp:TextBox ID="txtOrderId" runat="server" ToolTip="Enter the order ID you want to assign to the driver" class="form-control" placeholder="Enter the order ID you want to assign to the driver" TextMode="Number" Height="40px"></asp:TextBox>
                                                                                 <%-- <asp:Button ID="btnSearchDetails" runat="server" Text="Search Details" OnClick="btnSearchEmpDetails_Click" CssClass="btn-primary" Height="40px" />--%>
                                                                                </div>

                                                                                  <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="a" ErrorMessage="*** employee id required ***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtempId"></asp:RequiredFieldValidator>--%>
                                                                               </div>
                                                                              <br />
                                                                               <div class="col-md-12 col-sm-12">
                                                                                   <div style="display: flex;">
                                                                                 <h6>Which driver would you like to assign to deliver the customer's order?</h6>
                                                                                        </div> 
                                                                                  <asp:TextBox ID="txtDriverId" runat="server" Height="40px" ToolTip="Enter the id number of the driver you want to assign the order of the customer" TextMode="Number" placeholder="Enter the id number of the driver you want to assign the order of the customer" class="form-control"></asp:TextBox>
                                                                                  <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="a" ErrorMessage="*** employee id required ***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtempId"></asp:RequiredFieldValidator>--%>
                                                                               </div> 
                                                                                  
                                                                                <br />
                                                                          </div>
                                                                      </div>
                                                                      <div class="modal-footer">
                                                                      <%--  BUTTON SUBMIT RECORD HERE--%>
                                                                       <%--   <asp:Button ID="btnUpdateRecord" runat="server" Text="Edit Record" class="btn btn-primary btn-sm" OnClick="btnUpdateEmpRecord_Click"/>--%>
                                                                      <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary btn-sm" OnClick="btnSubmit_Click"/>
                                                                      </div>
                                                                  </div>
                                                               </div>
                                                          </div>
                                                      </div>
                                                   </div><%-- end for modal assigning drivers--%>
                                          <br />
                                        <%--  DISPLAYING ORDER DETAILS HERE--%>
                                                <div class="card">
                                                    <div class="card-header">
                                                         <asp:Label ID="Label2" runat="server" Text="ORDERS FROM CUSTOMER" ForeColor="Black" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                        
                                             <%-- dropdown in category for payment mode--%>
                                                         <asp:Label style="text-align: center;" ID="Label1" runat="server" Text="View order base on customer's payment mode " ForeColor="Black" Font-Size="12" ></asp:Label>
                                                    <%--<h5 style="text-align: center;">View order base on customer's payment mode to pay the order:</h5>--%>
                                                    <br>
                                                    <div style="display: flex; justify-content: center;">
                                                      <asp:DropDownList ID="drdPaymentMode" CssClass="text-center" runat="server"  Height="40px" Width="364px">
                                                          <asp:ListItem Text="All Orders" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="COD" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Gcash" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Reward Points" Value="3" ></asp:ListItem>
                                                      </asp:DropDownList>
                                                      <asp:Button ID="btnView" runat="server" Text="View Orders" OnClick="btnViewOrders_Click" CssClass="btn-primary" Height="40px" />
                                                    </div>
                                                    <br />

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
                                                                          <asp:Label style="text-align: center;" ID="lblViewOrders" runat="server" ForeColor="Black" Font-Size="16" ></asp:Label>
                                                                           <br />
                                                                           <asp:Label style="text-align: center;" ID="lblCodError" runat="server" ForeColor="Black" Font-Size="16" ></asp:Label>
                                               <asp:GridView runat="server" ID="gridCOD_order" class="texts table-responsive table-hover" style=" text-align:center; overflow-y: auto; max-height: 500px; margin-left: 14px;" 
                                                   BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" 
                                                   ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                      <Columns>
                                                        <asp:TemplateField HeaderText="ACTION">
                                                          <ItemTemplate>
                                                              <asp:Button ID="btnAccept" runat="server" Text="Accept" OnClick="btnAcceptCOD_Click" style="background-color:transparent;font-size:18px;border-color:green;border-style:solid"/>
                                                              <asp:Button ID="btnDecline" runat="server" Text="Decline" OnClick="btnDeclineCOD_Click" style="background-color:transparent;font-size:18px;border-color:red;border-style:solid"/>
                                                             <%-- <asp:Button runat="server" Text="View" style="background-color:transparent;font-size:16px;"  class="active btn waves-effect text-center"/> --%>
                                                         </ItemTemplate>
                                                        </asp:TemplateField>
                                                      </Columns>
                                                <%--   <Columns>
                                                        <asp:TemplateField HeaderText="PAYMENT">
                                                          <ItemTemplate>
                                                              <asp:Button ID="btnPaymentAccept" runat="server" Text="Payment Received" OnClick="btnPaymentAccept_Click"  style="background-color:transparent;font-size:18px;border-color:darkblue;border-style:solid"/>
                                                             
                                                         </ItemTemplate>
                                                        </asp:TemplateField>
                                                    
                                            </Columns>--%>
                                                 
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
                                                                          <asp:Label style="text-align: center;" ID="lblViewGcashOrder" runat="server" ForeColor="Black" Font-Size="16" ></asp:Label>
                                                                           <br />
                                                                           <asp:Label style="text-align: center;" ID="lblGcashError" runat="server" ForeColor="Red" Font-Size="16" ></asp:Label>
                                                                           <asp:GridView runat="server" ID="gridGcash_order" class="texts table-responsive table-hover" style=" text-align:center; overflow-y: auto; max-height: 500px; margin-left: 14px;" 
                                                   BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" 
                                                   ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                      <Columns>
                                                        <asp:TemplateField HeaderText="ACTION">
                                                          <ItemTemplate>
                                                              <asp:Button ID="btnAcceptGcash" runat="server" Text="Accept" OnClick="btnAcceptGcash_Click" style="background-color:transparent;font-size:18px;border-color:green;border-style:solid"/>
                                                              <asp:Button ID="btnDeclineGcash" runat="server" Text="Decline" OnClick="btnDeclineGcash_Click" style="background-color:transparent;font-size:18px;border-color:red;border-style:solid"/>
                                                             <%-- <asp:Button runat="server" Text="View" style="background-color:transparent;font-size:16px;"  class="active btn waves-effect text-center"/> --%>
                                                         </ItemTemplate>
                                                        </asp:TemplateField>
                                                      </Columns>
                                                   <Columns>
                                                        <asp:TemplateField HeaderText="PAYMENT">
                                                          <ItemTemplate>
                                                              <asp:Button ID="btnPaymentAccept" runat="server" Text="Payment Received" OnClick="btnPaymentAcceptGcash_Click"  style="background-color:transparent;font-size:18px;border-color:darkblue;border-style:solid"/>
                                                             
                                                         </ItemTemplate>
                                                        </asp:TemplateField>
                                                    
                                            </Columns>
                                                             <%-- <Columns>
                                                              <asp:TemplateField HeaderText="GCASH PROOF PAYMENT">
                                                                 <ItemTemplate>
                                                                    <asp:Image ID="imgGcash" runat="server" DataImageUrlField="GCASH PROOF PAYMENT" />
                                                                 </ItemTemplate>
                                                              </asp:TemplateField>
                                                           </Columns>--%>
                                                 
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
                                                                          <asp:Label style="text-align: center;" ID="lblViewRewardPoints" runat="server" ForeColor="Black" Font-Size="16" ></asp:Label>
                                                                           <br />
                                                                          <asp:Label style="text-align: center;" ID="lblPointsError" runat="server" ForeColor="Red" Font-Size="16" ></asp:Label>
                                                                           <asp:GridView runat="server" ID="gridRewardPoints_order" class="texts table-responsive table-hover" style=" text-align:center; overflow-y: auto; max-height: 500px; margin-left: 14px;" 
                                                   BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" 
                                                   ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                      <Columns>
                                                        <asp:TemplateField HeaderText="ACTION">
                                                          <ItemTemplate>
                                                              <asp:Button ID="btnAcceptPoints" runat="server" Text="Accept" OnClick="btnAcceptPoints_Click" style="background-color:transparent;font-size:18px;border-color:green;border-style:solid"/>
                                                              <asp:Button ID="btnDeclinePoints" runat="server" Text="Decline" OnClick="btnDeclinePoints_Click" style="background-color:transparent;font-size:18px;border-color:red;border-style:solid"/>
                                                             <%-- <asp:Button runat="server" Text="View" style="background-color:transparent;font-size:16px;"  class="active btn waves-effect text-center"/> --%>
                                                         </ItemTemplate>
                                                        </asp:TemplateField>
                                                      </Columns>
                                                   <Columns>
                                                        <asp:TemplateField HeaderText="PAYMENT">
                                                          <ItemTemplate>
                                                              <asp:Button ID="btnPaymentAcceptPoints" runat="server" Text="Payment Received" OnClick="btnPaymentAcceptPoints_Click"  style="background-color:transparent;font-size:18px;border-color:darkblue;border-style:solid"/>
                                                             
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

