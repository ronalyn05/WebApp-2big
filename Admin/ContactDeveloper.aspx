<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ContactDeveloper.aspx.cs" Inherits="WRS2big_Web.Admin.ContactDeveloper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link crossorigin="anonymous" media="all" rel="stylesheet"
        href="https://github.githubassets.com/assets/light-0946cdc16f15.css" />
    <link crossorigin="anonymous" media="all" rel="stylesheet"
        href="https://github.githubassets.com/assets/dark-3946c959759a.css" />
    <link data-color-theme="light" crossorigin="anonymous" media="all" rel="stylesheet"
        data-href="https://github.githubassets.com/assets/light-0946cdc16f15.css" />
    <link data-color-theme="dark" crossorigin="anonymous" media="all" rel="stylesheet"
        data-href="https://github.githubassets.com/assets/dark-3946c959759a.css" />
    <link data-color-theme="dark_dimmed" crossorigin="anonymous" media="all" rel="stylesheet"
        data-href="https://github.githubassets.com/assets/dark_dimmed-9b9a8c91acc5.css" />
    <link data-color-theme="dark_high_contrast" crossorigin="anonymous" media="all" rel="stylesheet"
        data-href="https://github.githubassets.com/assets/dark_high_contrast-11302a585e33.css" />
    <link data-color-theme="dark_colorblind" crossorigin="anonymous" media="all" rel="stylesheet"
        data-href="https://github.githubassets.com/assets/dark_colorblind-1a4564ab0fbf.css" />
    <link data-color-theme="light_colorblind" crossorigin="anonymous" media="all" rel="stylesheet"
        data-href="https://github.githubassets.com/assets/light_colorblind-12a8b2aa9101.css" />
    <link data-color-theme="light_high_contrast" crossorigin="anonymous" media="all" rel="stylesheet"
        data-href="https://github.githubassets.com/assets/light_high_contrast-5924a648f3e7.css" />
    <link data-color-theme="light_tritanopia" crossorigin="anonymous" media="all" rel="stylesheet"
        data-href="https://github.githubassets.com/assets/light_tritanopia-05358496cb79.css" />
    <link data-color-theme="dark_tritanopia" crossorigin="anonymous" media="all" rel="stylesheet"
        data-href="https://github.githubassets.com/assets/dark_tritanopia-aad6b801a158.css" />
    <link crossorigin="anonymous" media="all" rel="stylesheet"
        href="https://github.githubassets.com/assets/primer-4d8f37cc9d91.css" />
    <link crossorigin="anonymous" media="all" rel="stylesheet"
        href="https://github.githubassets.com/assets/site-2e14bc28cc0a.css" />
    <link crossorigin="anonymous" media="all" rel="stylesheet"
        href="https://github.githubassets.com/assets/pricing-38d9c74b2012.css" />



    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/wp-runtime-bb6465119c89.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_stacktrace-parser_dist_stack-trace-parser_esm_js-node_modules_github_bro-a4c183-ae93d3fba59c.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/ui_packages_failbot_failbot_ts-e38c93eab86e.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/environment-de3997b81651.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_github_selector-observer_dist_index_esm_js-2646a2c533e3.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_github_relative-time-element_dist_index_js-99e288659d4f.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_fzy_js_index_js-node_modules_github_markdown-toolbar-element_dist_index_js-e3de700a4c9d.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_delegated-events_dist_index_js-node_modules_github_auto-complete-element-5b3870-ff38694180c6.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_github_file-attachment-element_dist_index_js-node_modules_github_text-ex-3415a8-7ecc10fb88d0.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_github_filter-input-element_dist_index_js-node_modules_github_remote-inp-8873b7-5771678648e0.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_primer_view-components_app_components_primer_primer_js-node_modules_gith-3af896-2189f4f604ee.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/github-elements-7b037525f59f.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/element-registry-265f231a8769.js"></script>

    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/marketing-2db4382316fc.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/pricing-c6cefaa0d0c4.js"></script>
    <link rel="icon" href="/images/FinalLogo.PNG" type="image/x-icon">
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
                                            <h5 class="m-b-10">MESSAGES</h5>
                                            <h6 class="m-b-0">2BiG: Water Refilling Station Management System</h6>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="breadcrumb-title">
                                            <li class="breadcrumb-item">
                                                <a href="/Admin/AdminIndex"><i class="fa fa-home"></i></a>
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
                        <!-- Page-header end -->
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="page-wrapper">

                                    <!-- page content -->
                                    <div class="right_col" role="main">
                                        <div class="col-lg-7 mb-10 mb-lg-0" style="margin-left: 250px; background-color: lightblue">
                                            <div class="d-lg-flex flex-items-stretch">
                                                <div class="height-full position-relative rounded-3 px-2 pt-5 pb-2 js-pricing-plan">
                                                    <div
                                                        class="d-md-flex flex-column flex-justify-between height-full rounded-3 color-shadow-extra-large color-bg-default">
                                                        <div class="px-3 pt-4 pb-3" style="height: 599px; width: 1100px">
                                                            <asp:Repeater runat="server" ID="messagesrepeater">

                                                                <HeaderTemplate>
                                                                    <ul>
                                                                        <h5>Messages</h5>
                                                                        <br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <li style='width: 650px;<%# Eval("sender").ToString() == "Admin" ? "background-color: whitesmoke;" : "text-align: left;" %>'>
                                                                        <div class="media">
                                                                            <div class="media-body">

                                                                                <asp:Label runat="server" Style="font-size: 18px; margin-left: 10px" ID="notifMsg" Text='<%# Eval("body") %>' class="notification-msg"></asp:Label>
                                                                                <br />
                                                                                <asp:Label runat="server" ID="notifArrived" Text='<%# Eval("sent") %>' class="notification-time" Style="margin-left: 15px; font-size: 16px"></asp:Label>
                                                                            </div>

                                                                        </div>
                                                                    </li>
                                                                    <br />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </ul>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </div>



                                                    </div>
                                                </div>
                                            </div>

                                        </div>


                                    </div> <br /><br />
                                    <center>
                                            
                                                <asp:TextBox runat="server" ID="enterMessage" MultipleLine="true" Style="background-color: whitesmoke" TextMode="MultiLine" Rows="5" Columns="50" Font-Size="16px" Height="132px" Width="571px"></asp:TextBox>

                                                <asp:Button runat="server" ID="sendMessage" class="btn-primary" Text="Send" Height="55px" Width="98px" Style="margin-left: 10px; margin-top: 40px" OnClick="sendMessage_Click" />

                                            
                                    </center>

                                </div>
                            </div>


                            <!-- /page content -->
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
