﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="customerDetails.aspx.cs" Inherits="WRS2big_Web.superAdmin.customerDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <style>
        .texts{
            font-size:16px;
            color:black;
        
        }
        .text{
            font-size:20px;
            color:black;
        }
        .scrollable-listbox {
        height: 200px;
        overflow-y: auto;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--   <form runat="server">--%>
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
                                                <a href="/superAdmin/ManageCustomers">Customer List</a>
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
                                                <div class="modal fade declineCustomer" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md">
                                                        <div class="modal-content">
                                                            <div id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="declineModal"></h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">
                                                                            <%-- <div class="item form-group">--%>
                                                                            <h4 style="color: black; font-family: Bahnschrift">DECLINE CUSTOMER</h4>

                                                                            <div class="col-md-12 col-sm-12" style="font-size: 20px"> <br />
                                                                                <center>
                                                                                    <h4 style="font-size: 16px; color: black">Why are you declining this customer?</h4>
                                                                                    <p style="font-size: 16px; color: black;">"Provide more information on why do you want to decline this customer. Declined customer will be notified about this action."</p>
                                                                                </center>

                                                                                <hr />
                                                                                <asp:TextBox runat="server" class="form-control" TextMode="MultiLine" Style="font-size: 18px" Width="400px" ID="reasonDecline" Placeholder="Reason for declining"></asp:TextBox>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <%--  BUTTON CONFIRM DECLINE--%>
                                                                        <asp:Button ID="confirmDecline" runat="server" Text="Confirm" class="btn btn-primary btn-sm" OnClick="declineButton_Click"/>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                     <!--MAIN CONTENT-->
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <!-- Tab variant tab card start -->
                                            <div class="card">
                                                <div class="card-header">
                                                    <h5>CUSTOMER DETAILS</h5>
                                                </div>
                                                <div class="card-block tab-icon">
                                                    <!-- Row start -->
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xl-12">
                                                            <center>
                                                          <asp:Image runat="server" ID="clientImage" class="img-100" style="width:300px" Height="300px"/> <br /> <br />
                                                             <asp:Label  class="form-control-round" style="font-size:20px;color:black;" id="clientFullName" runat="server"></asp:Label> <br />
                                                            <asp:Label   class="form-control-round" runat="server" id="clientEmail" style="font-size:16px;color:dimgray;"></asp:Label><br /> <br />
                                                            <asp:Label   class="form-control-round" runat="server" id="Label23" style="font-size:16px;color:dimgray;">Status:</asp:Label> <br />
                                                            <asp:Label   class="form-control-round" runat="server" id="customerStatus" style="font-size:20px;color:black;"></asp:Label>
                                                            </center>

                                                               <br />  <asp:Label   class="form-control-round" runat="server" id="allLabel" style="font-size:16px;color:dimgray;"> Visit All Registered Customers</asp:Label> <br />
                                                              <asp:LinkButton runat="server" Text="View All Customers" class="btn btn-primary" style="font-size:18px" href="CustomerReports.aspx"></asp:LinkButton> <br /> <br />
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <!-- Tab variant tab card start -->
                                                                    <div class="card">
                                                                        <div class="card-header">
                                                                           
                                                                        </div>
                                                                        <div class="card-block tab-icon">
                                                                            <!-- Row start -->
                                                                            <div class="row">
                                                                              <div class="col-xl-6 col-md-12">
                                                                              <div class="card-block">
                                                                                          <div style="background-color:#018cff;color:white" class="card card-block">
	                                                                                           <h7>PERSONAL INFORMATION</h7>
                                                                                           </div>
                                                                                            <div class="form-material" style="margin-left:100px">
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">Firstname:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                            <asp:Label class="form-control-round" id="cusFirstName" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">Middlename:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                            <asp:Label class="form-control-round" id="cusmiddleName" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                   <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">Lastname:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                            <asp:Label class="form-control-round" id="cuslastName" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">Birthdate:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                            <asp:Label class="form-control-round" id="clientBirthDate" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">Contact number:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px">  
                                                                                                          <asp:Label  class="form-control-round" id="clientPhone" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">Home Address:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px">  
                                                                                                          <asp:Label  class="form-control-round" id="clientAddress" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>

                                                                                            </div>

                                                                            </div>
                                                                         </div>
                                                                              <div class="col-xl-6 col-md-12">
                                                                              <div class="card-block">
                                                                                  <br /><br /><br />
                                                                                            <div class="form-material" style="margin-left:100px">
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">Government-issued ID:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px">  
                                                                                                            <asp:Image runat="server" ID="clientValidID" class="img-500  img-container" style="width:600px;height:600px" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                            </div>

                                                                            </div>
                                                                         </div>
                                                                        </div>

                                                                        </div>
                                                                    </div>
                                                                    <!-- Tab variant tab card start -->
                                                                </div>
                                                            </div>
                                                            <center>
                                                            <asp:Button runat="server" ID="approveButton" class="btn btn-primary btn-sm text" style="font-size:18px" OnClick="approveButton_Click" Text="APPROVE"/>
                                                            <button type="button" runat="server" id="declineButton" style="font-size: 18px;" class="btn btn-primary btn-sm text" data-toggle="modal" data-target=".declineCustomer">DECLINE</button>
                                                            </center>
                                                              <asp:LinkButton runat="server" Text="Pending Customer List" class="btn btn-primary" style="font-size:18px" href="ManageCustomers.aspx"></asp:LinkButton>

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
<%--</form>--%>
</asp:Content>
