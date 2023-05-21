<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="packageDetails.aspx.cs" Inherits="WRS2big_Web.superAdmin.packageDetails" %>
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
<%--       <form runat="server">--%>
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
                                                <a href="/superAdmin/ManagePackagePlans">Subscription Packages</a>
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
                                     <!--MAIN CONTENT-->
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <!-- Tab variant tab card start -->
                                            <div class="card">
                                                <div class="card-header">
                                                    
                                                </div>
                                                <div class="card-block tab-icon">
                                                    <!-- Row start -->
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xl-12">
                                                               <br />
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
	                                                                                           <h7>PACKAGE DETAILS</h7>
                                                                                           </div>
                                                                                            <div class="form-material" style="margin-left:100px">
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE ID:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:90px">  
                                                                                                            <asp:Label class="form-control-round" id="packageID" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE NAME:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:90px">  
                                                                                                            <asp:Label class="form-control-round" id="packageName" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE DESCRIPTION:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:90px">  
                                                                                                            <asp:Label class="form-control-round" id="packageDescription" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                   <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE PRICE:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:90px">  
                                                                                                            <asp:Label class="form-control-round" id="packagePrice" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE DURATION:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:90px">  
                                                                                                            <asp:Label class="form-control-round" id="packageDuration" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">ORDER LIMIT:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:90px">  
                                                                                                          <asp:Label  class="form-control-round" id="packageLimit" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                            </div>

                                                                            </div>
                                                                         </div>
                                                                              <div class="col-xl-6 col-md-12">
                                                                              <div class="card-block">
                                                                                           <div style="background-color:#018cff;color:white" class="card card-block">
	                                                                                           <h7>PACKAGE DETAILS</h7>
                                                                                           </div>
                                                                                            <div class="form-material" style="margin-left:100px">

                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">FEATURES:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px"> 
                                                                                                          <asp:ListBox runat="server" ID="featuresList" style="text-align:center; font-size:18px" Height="248px" Width="421px"> </asp:ListBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                            </div>
                                                                                            <div class="form-material" style="margin-left:100px">

                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">number of Manageable Stations:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px"> 
                                                                                                          <asp:Label  class="form-control-round" id="numofStations" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                            </div>
                                                                                            <div class="form-material" style="margin-left:100px">

                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">Renewable:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px"> 
                                                                                                          <asp:Label  class="form-control-round" id="renewable" runat="server"></asp:Label>
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
                                                              <asp:LinkButton runat="server" Text="View Packages List" style="font-size:18px" href="ManagePackagePlans.aspx"></asp:LinkButton>

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
