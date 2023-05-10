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
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="page-wrapper">
                                    <!-- Page-body start -->
                                    <div class="page-body">
                                        <section class="login-block">
                                            <div class="text-center">
                                                <img src="/images/2ndLogo.png" style="width: 400px" alt="logo.png">
                                            </div>
                                            <div class="row justify-content-center">
                                                <div class="col-lg-5">
                                                    <div class="card shadow-lg border-0 rounded-lg mb-4 bgColor">
                                                        <div class="card-header">
                                                            <div class="form-group d-flex justify-content-between">
                                                                <h3 class="text-left font-weight-light my-2">Waiting for Approval</h3>

                                                            </div>
                                                            <h2 class="texts text-center " style="font-size: 20px;">Your account is under review. 
                                                                <br />
                                                                You will receive a notification once your account is approved.
                                                                <br />
                                                                Please check your notification from time to time. </h2>
                                                        </div>
                                                        <div class="card" id="reminderStatus">
                                                            <br />
                                                            <h7 class="text-center " style="font-size: 18px;">Reminder:</h7>
                                                            <br>
                                                            <h7 class="text-center " style="font-size: 16px;">" You can't proceed with the subscription yet since your account is under review.
                                                                <br />
                                                                Please wait for your account to be approved before subscribing" </h7>
                                                        </div>
                                                        <!-- cancel button -->
                                                        <div class="container pt-4 px-0 text-center">
                                                            <asp:LinkButton ID="LinkButton1" href="SubscriptionPackages.aspx" runat="server" Style="font-size: 18px;"> View Subscription Plans </asp:LinkButton>
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
