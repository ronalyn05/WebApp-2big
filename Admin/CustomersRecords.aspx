<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WaitingPage.aspx.cs" Inherits="WRS2big_Web.Admin.WaitingPage" %>
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
                                            <h5 class="m-b-10"> </h5>
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
                                    <div class="page-body">
                                                    <section class="login-block">
                                                               <div class="text-center">
                                                                     <img src="/images/2ndLogo.png" style="width:400px" alt="logo.png">
                                                                </div>
                                                                   <div class="row justify-content-center" >
                                                                    <div class="col-lg-5" >
                                                                        <div class="card shadow-lg border-0 rounded-lg mb-4 bgColor">
                                                                        <div class="card-header">
                                                                            <div class="form-group d-flex justify-content-between">
                                                                                <h3 class="text-left font-weight-light my-2">Waiting for Approval</h3>   
                               
                                                                            </div>
                                                                             <h2 class="texts text-center " style="font-size:20px;"> "Please wait for your account to be approved. You will receive a notification once your account is approved" </h2> 
                                                                        </div>               
                                                                         <!-- cancel button -->
                                                                           <div class="container pt-4 px-0">
                                                                           <a href="SubscriptionPlans.aspx" class="button btn btn-prinary">
                                                                            View Subscription Plans
                                                                            </a>
                                                                        </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                        <!-- Container-fluid starts -->
                                                        <div class="container-fluid ">
                                                            <div class="row">

                                                     <!-- end of col-sm-12 -->
                                                            </div>
                                                   <!-- end of row -->
                                                    </div>
                                                <!-- end of container-fluid -->
                                                    </section>

                                        <div class="row">
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card">

                                                </div>
                                            </div>

                                        </div>
                                    </div>
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
