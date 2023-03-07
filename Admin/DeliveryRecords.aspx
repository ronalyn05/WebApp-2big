<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DeliveryRecords.aspx.cs" Inherits="WRS2big_Web.Admin.DeliveryRecords" %>
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
                                            <h5 class="m-b-10">DELIVERY RECORDS </h5>
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
                                    <!-- page content -->
                                    <div class="right_col" role="main">
                                        <div class="">

                                            <div class="clearfix">
                                               <%-- <button type="button" class="btn btn-success btn-sm" data-toggle="modal" data-target=".add"><i class="fa fa-plus"></i> Add</button>--%>
 <%--BUTTON to ADD--%>
                                        <button type="button" style="font-size:14px;" class="btn btn-success btn-sm" data-toggle="modal" data-target=".add"><i class="fa fa-plus"></i> Add Delivery Type</button>
                                       
                                            </div>
                                            <div class="modal fade add" tabindex="-1" role="dialog" aria-hidden="true">
                                                <div class="modal-dialog modal-sm">
                                                    <div class="modal-content">
                                                        <form id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                            <div class="modal-header">
                                                                <h4 class="modal-title" id="myModalLabel">Add Delivery</h4>
                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                    <span aria-hidden="true">X</span>
                                                                </button>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="col-md-12 col-sm-12 ">
                                                                    <div class="x_content">
                                                                        <div class="item form-group">
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <asp:Label ID="Lbltype" runat="server" Text="Delivery Type: "></asp:Label><br />
                                              <%-- <asp:DropDownList ID="DrdDeliveryType" runat="server" >
                                                   <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                   <asp:ListItem Text="Standard" Value="Standard"></asp:ListItem>
                                                   <asp:ListItem Text="Express" Value="Express" ></asp:ListItem>
                                                   <asp:ListItem Text="Reservation" Value="Reservation" ></asp:ListItem>
                                               </asp:DropDownList>--%>
                                                        <asp:TextBox ID="DeliveryType" runat="server"></asp:TextBox>
                                                     </div>
                                                     </div>
                                                      <div class="item form-group">
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <asp:Label ID="Label3" runat="server" Text="Delivery Fee: "></asp:Label><br />
                                                        <asp:TextBox ID="DeliveryFee" TextMode="Number" runat="server"></asp:TextBox>
                                                     </div>
                                                       </div>
                                                      <div class="item form-group">
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <asp:Label ID="Label2" runat="server" Text="Estimated Time: "></asp:Label><br />
                                                         <asp:TextBox ID="EstimatedTime" runat="server"></asp:TextBox>
                                                     </div>
                                                       </div>
                                                     <div class="item form-group">
                                                      <div class="col-md-12 col-sm-12 ">
                                                          <asp:Label ID="LabelStatus" runat="server" Text="Status:"></asp:Label> <br />
                                                           <asp:DropDownList ID="Drd_Status" runat="server" Width="180px">
                                                               <asp:ListItem Text="Available" Value="Available" Selected="True"></asp:ListItem>
                                                            </asp:DropDownList>
                                                          </div>
                                                        </div>
                                                     </div>
                                                    </div>
                                                    </div>
                                                     <div class="modal-footer">
                                                        <%--Button Add ni diri--%>
                                                         <asp:Button ID="btnAdd" runat="server" Text="Add Delivery Type" class="btn btn-primary btn-sm" OnClick="btnAdd_Click"/>
                                                </div>
                                                        </form>
                                                    </div>
                                                </div>
                                            </div>
                                            <br /> <br />
                                       <%--  end of add delivery details--%>
                                            <!-- DELIVERY PAGE CONTENTS-->
                                               <div class="row">
                                                  <%-- LISTBOX FOR AVAILABLE DELIVERY DETAILS--%>
                                                   <div class="col-xl-3 col-md-12">
                                                    <div class="card ">
                                                        <div class="card-header">
                                                            <div class="card-header-right">
                                                                <ul class="list-unstyled card-option">
                                                                    <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                                    <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                    <li><i class="fa fa-minus minimize-card"></i></li>
                                                                    <li><i class="fa fa-refresh reload-card"></i></li>
                                                                    <li><i class="fa fa-trash close-card"></i></li>
                                                                </ul>
                                                            </div>
                                                            <h5>Available Delivery ID:</h5>
                                                        </div>
                                                        <div class="card-block">        
                                                           <asp:ListBox ID="ListBox1" runat="server" style="border:transparent; font-size:20px;padding:4px 7px 2px 4px;" Width="273px" Height="179px">
                                                           </asp:ListBox> 
                                                            <asp:Button ID="Btndisplay" onclick="btnDisplayDelivery_Click" type="button" style="font-size:14px;" class="btn btn-primary btn-sm" runat="server" Text="View Complete Details" />
                                                        </div>
                                                    </div>
                                                  </div>
                                                   <%--END OF LISTBOX FOR AVAILABLE DELIVERY DETAILS--%>
                                                  <%-- LISTBOX FOR UNAVAILABLE DELIVERY DETAILS--%>
                                                   <div class="col-xl-3 col-md-12">
                                                    <div class="card ">
                                                        <div class="card-header">
                                                            <div class="card-header-right">
                                                                <ul class="list-unstyled card-option">
                                                                    <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                                    <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                    <li><i class="fa fa-minus minimize-card"></i></li>
                                                                    <li><i class="fa fa-refresh reload-card"></i></li>
                                                                    <li><i class="fa fa-trash close-card"></i></li>
                                                                </ul>
                                                            </div>
                                                            <h5>Unavailable Delivery ID:</h5>
                                                        </div>
                                                        <div class="card-block">        
                                                           <asp:ListBox ID="ListBox2" runat="server" style="border:transparent; font-size:20px;padding:4px 7px 2px 4px;" Width="273px" Height="179px">
                                                           </asp:ListBox> 
                                                            <asp:Button ID="Display" onclick="btnDisplayDelivery2_Click" type="button" style="font-size:14px;" class="btn btn-primary btn-sm" runat="server" Text="View Complete Details" />
                                                        </div>
                                                    </div>
                                                  </div>
                                                   <%--END OF LISTBOX FOR UNAVAILABLE DELIVERY DETAILS--%>
                                            <div class="col-xl-9 col-md-12">
                                                <div class="card" style="background-color:#f2e2ff">
                                                    <div class="card-header">
                                                        <h5>DELIVERY DETAILS</h5>
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
                                                                      <div class="form-group">                
                                                                        <div class="col-xs-12" style="font-size:16px">
                                                                           <h5>Delivery ID:</h5> 
                                                                            <asp:Label ID="LblID" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff; font-size:16px; color:black; font-family:Bahnschrift; width:700px" ></asp:Label><br />
                                                                            <br>
                                                                            <h5>Delivery Type: </h5>
                                                                             <asp:TextBox ID="txtdeliveryType" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff; font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                            <%--<asp:Label ID="LblBorrowGal" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px" ></asp:Label>--%>
                                                                          <br>
                                                                            <h5>Delivery Fee: </h5>
                                                                            <asp:TextBox ID="txtdeliveryFee" TextMode="Number" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff; font-size:16px; color:black; font-family:Bahnschrift; width:700px"></asp:TextBox>
                                                                              
                                                                            <br>
                                                                             <h5>Estimated Time:  </h5>
                                                                           <asp:TextBox ID="txtEstimatedTime" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff; font-size:16px; color:black; font-family:Bahnschrift; width:700px"></asp:TextBox>
                                                                            <%--<asp:Label ID="Lbl_ProdType" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px" ></asp:Label>--%>
                                                                             <br />
                                                                             <h5>Status:  </h5>
                                                                               <asp:DropDownList ID="drdstatus" runat="server" CssClass="btn btn-round waves-effect text-center" style="background-color:#bae1ff; font-size:16px; color:black; font-family:Bahnschrift; width:700px; height: 40px">
                                                                                   <%--<asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>--%>
                                                                                   <asp:ListItem Text="Available" Value="Available" Selected="True"></asp:ListItem>
                                                                                   <asp:ListItem Text="Unavailable" Value="Unavailable"></asp:ListItem>
                                                                               </asp:DropDownList>
                                                                            <%--<h5>Date Added:  </h5>
                                                                            <asp:Label ID="Label4" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px" ></asp:Label>
                                                                            <br>--%><%--
                                                                            <h5>Reservation Date:  </h5>
                                                                            <asp:Label ID="Lbl_ReservDate" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px" ></asp:Label>
                                                                            <br>
                                                                            <h5>Order Type:  </h5>
                                                                            <asp:Label ID="Lbl_OrderType" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px" ></asp:Label>
                                                                            <br>--%>
                                                                          </div>
                                                                      </div>
                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
                                                           
                                                        </div>
                                                    </div>
                                                    <div class="card-footer">
                                                                   <%--  <asp:Button ID="Button6" style="font-size:14px;" class="btn btn-primary btn-sm"  runat="server" Text="ACCEPT"/>
                                                                       <asp:Button ID="Button7" style="font-size:14px;" class="btn btn-danger btn-sm" runat="server"  Text="DECLINE" OnClick="DeclineBtn_Click" /> --%>

                                                        <asp:Button ID="Button5" style="font-size:14px;" class="btn btn-primary btn-sm"  runat="server" Text="Edit Delivery Details" OnClick="btnEditDeliveryType_Click"/>
                                                                   <br />
                                                        <br />
                                                       <%-- <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="16px">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <Columns> --%>
                                                                    <%--<asp:TemplateField HeaderText="Delivery ID">
                                                                    <ItemTemplate>
                                                                        <%# Eval("deliveryId") %>
                                                                    </ItemTemplate>
                                                                     </asp:TemplateField>--%>
                                                                    <%--<asp:TemplateField HeaderText="Delivery Type">
                                                                    <ItemTemplate>
                                                                        <%# Eval("deliveryType") %>
                                                                    </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delivery Fee">
                                                                    <ItemTemplate>
                                                                        <%# Eval("deliveryFee") %>
                                                                    </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Estimated Time">
                                                                    <ItemTemplate>
                                                                        <%# Eval("estimatedTime") %>
                                                                    </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <%# Eval("status") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                         
                                                                <%-- <asp:BoundField DataField="DID" HeaderText="Delivery ID" ItemStyle-Width="10%"
                                                                ItemStyle-HorizontalAlign="Center"/>
                                                                 <asp:BoundField DataField="DTYPE" HeaderText="Delivery Type" ItemStyle-Width="20%"
                                                                ItemStyle-HorizontalAlign="Center" />
                                                                 <asp:BoundField DataField="DFEE" HeaderText="Delivery Fee" ItemStyle-Width="10%"
                                                                ItemStyle-HorizontalAlign="Center"/>
                                                                 <asp:BoundField DataField="ESTYM" HeaderText="Estimated Time" ItemStyle-Width="10%" 
                                                                ItemStyle-HorizontalAlign="Center"/> 
                                                                 <asp:BoundField DataField="STAT" HeaderText="Status" ItemStyle-Width="10%"
                                                                ItemStyle-HorizontalAlign="Center"/>--%>
                                                          <%--  </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                        </asp:GridView>--%>

                                                        <table cellpadding="4" cellspacing="0" style="color:#333333;border-collapse:collapse;width:16px;">
                        <thead>
                            <tr style="background-color:#5D7B9D;color:white;">
                                <th style="font-weight:bold;">Delivery Type</th>
                                <th style="font-weight:bold;">Delivery Fee</th>
                                <th style="font-weight:bold;">Estimated Time</th>
                                <th style="font-weight:bold;">Status</th>
                            </tr>
                        </thead>
                        <tbody style="background-color:#F7F6F3;">
       
                                 <% foreach (var deliveryDetail in deliveryDetails) { %>
                              <tr>
                                <td><%= deliveryDetail.DeliveryType %></td>
                                <td><%= deliveryDetail.DeliveryFee %></td>
                                <td><%= deliveryDetail.EstimatedTime %></td>
                                <td><%= deliveryDetail.Status %></td>
                              </tr>
                            <% } %>
                        </tbody>
                    </table>





                                                    </div>
                                                </div>
                                               </div> 
                                             </div>
                                      <!--PAGE CONTENTS END DELIVERY-->

                                            
                                        </div>
                                    </div>


                                    <!-- /page content -->
                                </div>
                            </div>
                        </div>
                        

                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
