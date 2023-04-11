<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EmployeeRecord.aspx.cs" Inherits="WRS2big_Web.Admin.Employees" %>
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
                                            <h5 class="m-b-10">EMPLOYEES </h5>
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
                                        <%-- add employee button--%>
                                         <button type="button" class="btn btn-primary btn-md" data-toggle="modal" data-target=".add"><i class="fa fa-plus"></i> Add Employee</button>
                                         <%--VIEW BUTTON --%>
                                          &nbsp;
                                          <%--<asp:Button ID="ViewID" runat="server"  style="font-size:14px;" class="btn btn-success btn-sm " Text="View List of Employee IDs" Height="41px" />--%>
                                       <%-- MODAL FOR ADD EMPLOYEE--%>
                                       <div class="modal fade add" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-md">
                                            <div class="modal-content">
                                            <form id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel">Add Employee Records</h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-12 col-sm-12 ">
                                            <div class="x_content">
                                           <%-- <div class="item form-group">--%>
                                            <div class="col-md-12 col-sm-12 ">
                                               <%-- input lastname--%>
                                            <strong>Lastname:</strong>
                                            <asp:TextBox ID="txtlastname" runat="server" class="form-control" placeholder="Employees' Lastname"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqlname" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtlastname" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                            <%--</div>--%>
                                            <%--<div class="item form-group">--%>
                                             <div class="col-md-12 col-sm-12">
                                                 <%--firstname--%>
                                             <strong>Firstname:</strong>
                                              <asp:TextBox ID="txtfirstname" runat="server" class="form-control" placeholder="Employees' Firstname"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="reqfname" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtfirstname" ValidationGroup="a"></asp:RequiredFieldValidator>
                                              </div>
                                              <%--</div>--%>
                                                <%--<div class="item form-group">--%>
                                             <div class="col-md-12 col-sm-12">
                                                 <%--middlename--%>
                                             <strong>Middlename (Optional):</strong>
                                              <asp:TextBox ID="txtmidname" runat="server" class="form-control" placeholder="Employees' Middlename"></asp:TextBox>   
                                              </div>
                                              <%--</div>--%>
                                              <%--<div class="item form-group">--%>
                                              <div class="col-md-12 col-sm-12">
                                                  <%--date of birth--%>
                                              <strong>Date of Birth:</strong>
                                                <asp:TextBox ID="BirthDate" runat="server" class="form-control" TextMode="Date" onchange="validateBirthDate()"> </asp:TextBox>
                                                  <%--<asp:RangeValidator ID="RangeValidator1" runat="server"  ErrorMessage="Age must be 18 years and above!" ControlToValidate="txtbirthdate" MaximumValue="01/01/2004"  Display="Dynamic" ForeColor="Red" ValidationGroup="a"></asp:RangeValidator>--%>
                                                  <asp:RequiredFieldValidator ID="reqdob" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="BirthDate" ValidationGroup="a"></asp:RequiredFieldValidator>    
                                                                                                  
                                               </div>
                                               <%--</div>--%>
                                              <%-- <div class="item form-group">--%>
                                               <div class="col-md-12 col-sm-12">
                                                   <%--gender--%>
                                               <strong>Gender:</strong>
                                                   <asp:DropDownList ID="drdgender" runat="server" Height="40px" Width="364px">
                                                       <%--<asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>--%>
                                                       <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                       <asp:ListItem Text="Female" Value="Female" ></asp:ListItem>
                                                   </asp:DropDownList>
                                                   <%--<script> 
                                                       $(function () {
                                                           $("#drdgender option:first").prop("disabled", true);
                                                           //alert("Invalid gender selection!");
                                                       });
                                                   </script>--%>

                                               <%--</div>--%>
                                               </div>
                                              <%-- <div class="item form-group">--%> 
                                               <div class="col-md-12 col-sm-12">
                                                   <%--address--%>
                                               <strong>Address:</strong>
                                                   <asp:TextBox ID="txtaddress" runat="server" class="form-control" placeholder="Employees' Address"></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="reqaddress" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtaddress" ValidationGroup="a"></asp:RequiredFieldValidator> 
                                              <%-- </div>--%>
                                               </div>
                                             <%-- <div class="item form-group">--%>
                                               <div class="col-md-12 col-sm-12">
                                                   <%--contact number--%>
                                               <strong>Contact Number:</strong>
                                                    <asp:TextBox ID="txtcontactnum" runat="server" class="form-control" placeholder="Employees' Contact number" TextMode="Phone" Value=""></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="reqcontact" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtcontactnum" ValidationGroup="a"></asp:RequiredFieldValidator> 
                                               <%--</div>--%>
                                               </div>
                                               <%--<div class="item form-group">--%>
                                               <div class="col-md-12 col-sm-12">
                                                  <%-- email--%>
                                               <strong>Email Address:</strong>
                                                   <asp:TextBox ID="txtemail"  runat="server" class="form-control" placeholder="Employees' email@example.com"></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="reqemail" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtemail" ValidationGroup="a"></asp:RequiredFieldValidator>
                                              <%-- </div>--%>
                                               </div>
                                                  <div class="col-md-12 col-sm-12">
                                                  <%-- email--%>
                                               <strong>Password:</strong>
                                                   <asp:TextBox ID="txtpass"  runat="server" class="form-control" placeholder="set employee password"></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtpass" ValidationGroup="a"></asp:RequiredFieldValidator>
                                              <%-- </div>--%>
                                               </div>
                                               <%--<div class="item form-group">--%>
                                               <div class="col-md-12 col-sm-12">
                                                  <%-- date hired--%>
                                               <strong>Date Hired:</strong>
                                                   <asp:TextBox ID="txtdateHired" TextMode="Date" runat="server" class="form-control" placeholder="Employees' Date Hired"></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="reqdoh" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtdateHired" ValidationGroup="a"></asp:RequiredFieldValidator>
                                             <%--   </div>--%>
                                                </div>
                                               <%--<div class="item form-group">--%>
                                               <div class="col-md-12 col-sm-12">
                                                   <%--emergency contact--%>
                                               <strong>In case of emergency, Contact:</strong>
                                                   <asp:TextBox ID="txtemergencycontact" runat="server" class="form-control" placeholder="Employees' emergencey contact" TextMode="Phone" Value=""></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="reqemercontact" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtemergencycontact" ValidationGroup="a"></asp:RequiredFieldValidator>
                                               <%-- </div>--%>
                                                </div>
                                               <%-- <div class="item form-group">--%>
                                                <div class="col-md-12 col-sm-12">
                                                    <%--role--%>
                                                    <strong>Position:</strong>
                                                   <asp:DropDownList ID="drdrole" runat="server" Height="40px" Width="364px">
                                                       <%--<asp:ListItem Selected="True">-----Choose One-----</asp:ListItem>--%>
                                                       <asp:ListItem Text="Cashier" Value="Cashier" Selected="True"></asp:ListItem>
                                                       <asp:ListItem Text="Driver" Value="Driver" ></asp:ListItem>
                                                       <asp:ListItem Text="Water Refiller" Value="Water Refiller" ></asp:ListItem>
                                                       <asp:ListItem Text="WRS Helper" Value="WRS Helper" ></asp:ListItem>
                                                   </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="drdrole" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                <%--  </div>--%>
                                                    <strong>Status:</strong>
                                                   <asp:DropDownList ID="Drd_status" runat="server" Height="40px" Width="364px">
                                                         <%--<asp:ListItem Selected="True" > ------ Select Employee Status ------ </asp:ListItem>--%>
                                                         <asp:ListItem Text="Active" Value="Active" Selected="True"></asp:ListItem>
                                                         <%--<asp:ListItem Text="Inactive" Value="Inactive" ></asp:ListItem>--%>
                                                         </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="Drd_status" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                  </div>
                                                </div>
                                              </div>
                                            </div>
                                            <div class="modal-footer">
                                               <%-- add data button--%>
                                               <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" Text="Add Data" ValidationGroup="a" OnClick="btnAdd_Click" />
                                               </div>
                                               </form>
                                             </div>
                                            </div>
                                      </div>
                                        <%-- end add employee--%>
                                           <br />
                                         <br />

                                            <!--PAGE CONTENTS-->
                                         <div class="col-xl-12 col-xl-12 h-100">
                                             <div class="card">
                                                <%--<div class="card" style="background-color:#f2e2ff">--%>
                                                    <div class="card-header">
                                                         <asp:Label ID="Label1" runat="server" Text="LIST OF EMPLOYEE RECORDS" ForeColor="Black" Font-Size="Large" Font-Bold="True"></asp:Label>
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
                                                    </div>
                                                    <div class="card-footer">
                                                                    <%-- <asp:Button ID="EditBtn" style="font-size:14px;" class="btn btn-primary btn-sm"  runat="server" Text="Update Records" OnClick="btnEdit_Click"/>
                                                                     --%> 
                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                <%--the gridview starts here--%>
                                                               <div style="overflow: auto; height: 600px; text-align:center;" class="texts" >
                                                                  <asp:GridView runat="server" ID="GridView1" CellPadding="3" Width="975px" CssClass="auto-style1" 
                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                                          <Columns>
                                                                            <asp:TemplateField>
                                                                              <ItemTemplate>
                                                                                 <asp:Button ID="btnUpdate" runat="server" Text="Edit" OnClick="btnupdateData_Click" Font-Bold="true" class="btn btn-primary btn-md" data-toggle="modal" 
                                                                                        data-target="#editModal" BorderStyle="None" ForeColor="White"/>
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

                                           <%-- MODAL FOR UPDATE EMPLOYEE--%>
                                      <div class="modal fade" id="editModal" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-md">
                                            <div class="modal-content">
                                            <form id="demo-form1" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel1">Edit Employee Records</h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-12 col-sm-12 ">
                                            <div class="x_content">
                                                <div class="col-md-12 col-sm-12">
                                                    <%--role--%>
                                                    <strong>Position:</strong>
                                                   <asp:DropDownList ID="drd_empPosition" runat="server" Height="40px" Width="364px">
                                                       <%--<asp:ListItem Selected="True">-----Choose One-----</asp:ListItem>--%>
                                                       <asp:ListItem Text="Cashier" Value="Cashier" Selected="True"></asp:ListItem>
                                                       <asp:ListItem Text="Driver" Value="Driver" ></asp:ListItem>
                                                       <asp:ListItem Text="Water Refiller" Value="Water Refiller" ></asp:ListItem>
                                                       <asp:ListItem Text="WRS Helper" Value="WRS Helper" ></asp:ListItem>
                                                   </asp:DropDownList>
                                                    
                                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="drdrole" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
                                                  </div>
                                                <div class="col-md-12 col-sm-12">
                                                    <strong>Status:</strong>
                                                   <asp:DropDownList ID="drd_empStatus" runat="server" Height="40px" Width="364px">
                                                         <%--<asp:ListItem Selected="True" > ------ Select Employee Status ------ </asp:ListItem>--%>
                                                         <asp:ListItem Text="Active" Value="Active" Selected="True"></asp:ListItem>
                                                         <asp:ListItem Text="Inactive" Value="Inactive" ></asp:ListItem>
                                                         </asp:DropDownList>
                                                     <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="***" ForeColor="Red" Font-Bold="true" ControlToValidate="Drd_status" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
                                                  </div>
                                                </div>
                                                </div>
                                              </div>
                                                 <div class="modal-footer">
                                               <%-- update data button--%>
                                                     <%-- <asp:Button ID="btnUpdate" class="btn btn-primary" runat="server" CommandArgument='<%# Eval("EMPLOYEE ID") %>'
                                                                Text="Update Data" OnClick="btnupdate_Click"/>--%>
                                                      <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnupdateData_Click" Font-Bold="true" class="btn btn-primary btn-md" BorderStyle="None" ForeColor="White"/>
                                                    <%-- <asp:Button ID="btnUpdateData" class="btn btn-primary" runat="server" Text="Update" CommandArgument='<%# Eval("EMPLOYEE ID") %>' OnClick="btnupdateData_Click" />--%>

                                                </div>
                                            </div>
                                            
                                               </form>
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

