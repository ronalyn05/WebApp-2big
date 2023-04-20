<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="WRS2big_Web.superAdmin.Notifications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                                            <h5 class="m-b-10">NOTIFICATIONS</h5>
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
                                            
                                                <div class="card-block tab-icon">
                                                    <div class="row">
                                                                <div class="col-xl-6 col-md-12">
                                                                    <!-- Tab variant tab card start -->
                                                                    <div class="card">
                                                                        <div class="card-header">
                                                                             <h7>NOTIFICATIONS</h7>
                                                                                <div class="card-header-right">
                                                                                    <ul class="list-unstyled card-option">
                                                                                        <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                                                        <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                                        <li><i class="fa fa-minus minimize-card"></i></li>
                                                                                        <li><i class="fa fa-refresh reload-card"></i></li>
                                                               
                                                                                    </ul>
                                                                                </div>
                                                                        </div>
                                                                        <div class="card-block tab-icon">
                                                                            <!-- Row start -->
                                                                            <div class="row">
                                                                              <div class="col-xl-6 col-md-12">
                                                                              <div class="card-block">
                                                                                <ul>
                                                                                    <li class="waves-effect waves-light" style="border:solid;border-color:black;width:600px">
                                                                                        <div >
                                                                                            <h5>Body of notification</h5>
                                                                                            <span class="notification-time"> time</span>
                                                                                        </div>
                                                                                    </li>
                                                                                </ul>

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
