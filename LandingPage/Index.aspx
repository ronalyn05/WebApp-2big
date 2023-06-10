<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WRS2big_Web.LandingPage.Index" %>
<%--<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WRS2big_Web.About" %>--%>

<!DOCTYPE html>
<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>2BiG: WRS Management System</title>
    <link href="/assets/css/animate.css/css/animate.css" rel="stylesheet" />
    <link href="/assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/assets/css/chartist/chartist.css" rel="stylesheet" />
    <link href="/assets/css/morris.js/css/morris.css" rel="stylesheet" />
    <link href="/assets/css/ionicons.css" rel="stylesheet" />
    <link href="/assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="/assets/css/jquery.mCustomScrollbar.css" rel="stylesheet" />
    <link href="/assets/css/style.css" rel="stylesheet" />
    <link href="/assets/icon/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="/assets/icon/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <a href="/assets/icon/font-awesome/fonts/fontawesome-webfont.eot"></a>
    <a href="/assets/icon/font-awesome/fonts/fontawesome-webfont.ttf"></a>
    <a href="/assets/icon/font-awesome/fonts/fontawesome-webfont.woff"></a>
    <a href="/assets/icon/font-awesome/fonts/fontawesome-webfont.woff2"></a>
    <a href="/assets/icon/font-awesome/fonts/FontAwesome.otf"></a>
    <link href="/assets/icon/icofont/css/icofont.css" rel="stylesheet" />
    <a href="/assets/icon/icofont/fonts/icofont.eot"></a>
    <a href="/assets/icon/icofont/fonts/icofont.ttf"></a>
    <a href="/assets/icon/icofont/fonts/icofont.woff"></a>
    <a href="/assets/icon/themify-icons/fonts/themify.eot"></a>
    <a href="/assets/icon/themify-icons/fonts/themify.ttf"></a>
    <a href="/assets/icon/themify-icons/fonts/themify.woff"></a>
    <link href="/assets/icon/themify-icons/themify-icons.css" rel="stylesheet" />
    <script src="/assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="/assets/js/chart.js/Chart.js"></script>
    <script src="/assets/js/jquery/jquery.min.js"></script>
    <script src="/assets/js/jquery-slimscroll/jquery.slimscroll.js"></script>
    <script src="/assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="/assets/js/modernizr/css-scrollbars.js"></script>
    <script src="/assets/js/modernizr/modernizr.js"></script>
    <script src="/assets/js/morris.js/morris.js"></script>
    <script src="/assets/js/popper.js/popper.min.js"></script>
    <script src="/assets/js/raphael/raphael.min.js"></script>
    <script src="/assets/js/bootstrap-growl.min.js"></script>
    <script src="/assets/js/common-pages.js"></script>
    <script src="/assets/js/jquery.mCustomScrollbar.concat.min.js"></script>
    <script src="/assets/js/jquery.mousewheel.min.js"></script>
    <script src="/assets/js/morris-custom-chart.js"></script>
    <script src="/assets/js/pcoded.min.js"></script>
    <script src="/assets/js/script.js"></script>
    <script src="/assets/js/script.min.js"></script>
    <script src="/assets/js/SmoothScroll.js"></script>
    <script src="/assets/js/vertical-layout.min.js"></script>
    <script src="/assets/pages/accordion/accordion.js"></script>
    <script src="/assets/pages/accordion/accordion.min.js"></script>
    <link href="/assets/pages/dashboard/amchart/css/amchart.css" rel="stylesheet" />
    <link href="/assets/pages/dashboard/amchart/css/export.css" rel="stylesheet" />
    <script src="/assets/pages/dashboard/amchart/js/amcharts.js"></script>
    <script src="/assets/pages/dashboard/amchart/js/custom-amchart.js"></script>
    <script src="/assets/pages/dashboard/amchart/js/export.min.js"></script>
    <script src="/assets/pages/dashboard/amchart/js/light.js"></script>
    <script src="/assets/pages/dashboard/amchart/js/serial.js"></script>
    <script src="/assets/pages/dashboard/custom-dashboard.js"></script>
    <script src="/assets/pages/google-maps/gmaps.js"></script>
    <script src="/assets/pages/google-maps/google-maps.js"></script>
    <link href="/assets/pages/notification/notification.css" rel="stylesheet" />
    <script src="/assets/pages/todo/todo.js"></script>
    <link href="/assets/pages/waves/css/waves.min.css" rel="stylesheet" />
    <script src="/assets/pages/waves/js/waves.min.js"></script>
        <!-- Favicon icon -->
    <link rel="icon" src="/images/FinalLogo.png" type="image/x-icon">
        <meta charset="utf-8">
    <link rel="dns-prefetch" href="https://github-cloud.s3.amazonaws.com">
    <link rel="dns-prefetch" href="https://user-images.githubusercontent.com/">
    <link rel="preconnect" href="https://github.githubassets.com">
    <link rel="preconnect" href="https://avatars.githubusercontent.com">
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
        href="https://github.githubassets.com/assets/global-243d3a393d7d.css" />
    <link crossorigin="anonymous" media="all" rel="stylesheet"
        href="https://github.githubassets.com/assets/github-b717d68e0146.css" />
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
        src="https://github.githubassets.com/assets/vendors-node_modules_lit-html_lit-html_js-9d9fe1859ce5.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_github_mini-throttle_dist_index_js-node_modules_github_alive-client_dist-bf5aa2-424aa982deef.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_github_turbo_dist_turbo_es2017-esm_js-ba0e4d5b3207.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_primer_behaviors_dist_esm_dimensions_js-node_modules_github_hotkey_dist_-269f6d-5d145c7cc849.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_github_remote-form_dist_index_js-node_modules_scroll-anchoring_dist_scro-5881a7-44d01ee9e782.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_color-convert_index_js-35b3ae68c408.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_github_paste-markdown_dist_index_esm_js-node_modules_github_quote-select-973149-7c1c1618332f.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/app_assets_modules_github_updatable-content_ts-dadb69f79923.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/app_assets_modules_github_behaviors_keyboard-shortcuts-helper_ts-app_assets_modules_github_be-f5afdb-8cfe1dd0ad56.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/app_assets_modules_github_blob-anchor_ts-app_assets_modules_github_code-editor_ts-app_assets_-d384d0-eae4affc5787.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/app_assets_modules_github_sticky-scroll-into-view_ts-1d145b63ed56.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/app_assets_modules_github_behaviors_ajax-error_ts-app_assets_modules_github_behaviors_include-2e2258-dae7d38e0248.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/app_assets_modules_github_behaviors_commenting_edit_ts-app_assets_modules_github_behaviors_ht-83c235-80a9915bf75c.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/behaviors-86791d034ef8.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_delegated-events_dist_index_js-node_modules_github_catalyst_lib_index_js-623425af41e1.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/notifications-global-4dc6f295cc92.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/vendors-node_modules_delegated-events_dist_index_js-node_modules_github_catalyst_lib_index_js-b4a243-6b0c4317c3ae.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/marketing-2db4382316fc.js"></script>
    <script crossorigin="anonymous" defer="defer" type="application/javascript"
        src="https://github.githubassets.com/assets/pricing-c6cefaa0d0c4.js"></script>
    <link rel="icon" href="/images/FinalLogo.PNG" type="image/x-icon">

    <style>
        .divider {
            background: linear-gradient(100deg, white 0%, rgba(66, 135, 245) 100%, rgba(66, 135, 245) 80%);
            width: 100%;
            height: 6px;
        }

        .background {
            background-color: white;
        }

        .Lightblue-background {
            background-color: #9CE6F7;
        }

        .bodytext {
            font-family: "Nunito Sans", "Arial", sans-serif;
            font-weight: normal;
            line-height: 1.5;
            color: #00053e;
        }

        .grid-container {
            padding-right: 0.9375rem;
            padding-left: 0.9375rem;
            max-width: 75rem;
        }

        .videosize {
            width: 100%;
        }

        .texts {
            font-size: 200%;
            line-height: 1.6;
            font-weight: 500;
            color: #001242;
            font-family: "Nunito Sans", "Arial", sans-serif;
        }

        .h2 {
            font-family: "Nunito Sans", "Arial", sans-serif;
            text-transform: uppercase;
            line-height: normal;
            font-weight: 100;
            color: #001242;
            font-size: 400%;
        }

        .h3 {
            font-family: "Nunito Sans", "Arial", sans-serif;
            text-transform: uppercase;
            line-height: normal;
            font-weight: 200;
            color: #001242;
            font-size: 280%;
        }

        .h4 {
            font-family: "Nunito Sans", "Arial", sans-serif;
            text-transform: uppercase;
            line-height: normal;
            font-weight: 200;
            color: #001242;
            font-size: 150%;
        }

        .h5 {
            font-family: "Nunito Sans", "Arial", sans-serif;
            text-transform: uppercase;
            line-height: normal;
            font-weight: 200;
            color: #001242;
            font-size: 100%;
        }

        .h6 {
            font-family: "Nunito Sans", "Arial", sans-serif;
            line-height: normal;
            font-weight: 200;
            color: #001242;
            font-size: 100%;
        }

        .h7 {
            font-family: "Nunito Sans", "Arial", sans-serif;
            line-height: normal;
            font-weight: 100;
            color: #001242;
            font-size: 50%;
        }

        .h1 {
            font-family: "Nunito Sans", "Arial", sans-serif;
            text-transform: uppercase;
            line-height: normal;
            font-weight: 200;
            color: #001242;
            font-size: 650%;
        }

        .verticalLine {
            height: 200px;
            border-right: 7px solid #001242;
            position: absolute;
            right: 50%;
        }

        .secbackground {
            background-image: url('images/FinalLogo.png');
            background-repeat: no-repeat;
            background-size: cover;
        }

        .teal-gradient {
            background: #00A8E4;
            background: linear-gradient(180deg, #00a8e4 0%, #9ce6f7 70%, #e7f6fd 100%);
        }

        .orange-gradient {
            background: #7ADBF0;
            background: linear-gradient(180deg, #7ADBF0 0%, #9AE8F9 70%, #D0F1F9 100%);
        }

        .button {
            text-transform: uppercase;
            text-decoration: none;
            font-weight: 700;
            border-radius: 50px;
        }

        .margin-0 {
            margin-top: 0rem !important;
            margin-right: 0rem !important;
            margin-bottom: 0rem !important;
            margin-left: 0rem !important;
        }
    </style>
</head>


<body>
    <form runat="server">
    <div id="pcoded" class="pcoded">
        <div class="pcoded-overlay-box"></div>
        <div class="pcoded-container navbar-wrapper">
            <nav class="navbar header-navbar pcoded-header">
                <div class="navbar-wrapper">

                    <div class="navbar-container container-fluid">
                        <ul class="nav-left">
                            <li>
                                <div class="text-center">
                                    <img src="/images/secLogo.png" style="width:170px" alt="logo.png">
                                </div>
                            </li>
                        </ul>
                        <ul class="nav-right">
                            <li>
                                <a href="#why2big" class="waves-effect waves-light">2BiG </a>
                            </li>
                            <li>
                                <a href="#services" class="waves-effect waves-light"> Services </a>
                            </li>
                            <li>
                                <a href="#theteam" class="waves-effect waves-light"> The Team </a>
                            </li>
                            <li>
                                <a href="#subscription" class="waves-effect waves-light"> Subscribe </a>
                            </li>
                            <li class="user-profile header-notification">
                                <a href="/LandingPage/Account.aspx" class="waves-effect waves-light">
                                    <span> Sign Up / Login </span>
                                </a>

                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
            <div class="pcoded-main-container background">
                <div class="pcoded-wrapper">
                    <!-- Page-header start -->
                    <div class="page-header">
                        <div class="">
                            <div class="row align-items-center">
                                <div class="col-md-8">
                                    <div class="page-header-title">
                                        <h3 class="m-b-0 text-center"></h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Page-header end -->
                    <!-- MAIN CONTENT -->
                    <section>
                        <div class="h1">
                            <h1 class="text-center">
                                <br>
                                2BiG: Water Refilling Station Management System
                            </h1>
                        </div>
                        <br>
                        <div class="row">

                            <div class="col-sm-4 text-right">
                                <img src="/images/innovation.png" style="width:70%">
                            </div>
                            <div class="col-sm-4">
                                <img src="/images/FinalLogo.PNG" style="width:110%">
                            </div>
                            <div class="col-sm-4">
                                <img src="/images/Growth.PNG" style="width:70%">
                            </div>
                        </div>

                        <div class="h1">
                            <h1 class=" text-center">
                                Grow your business with 2BiG!
                            </h1>
                        </div>
                    </section>
                    <section>
                        <!-- <img src="images/FinalLogo.PNG" style="width:40%"> -->

                        <div class="form-group form-primary">
                            <!-- <h1 class="h1">
                               <a name="why2big"> </a>
                               2BiG
                             </h1> -->
                        </div>
                        <div class="text-center">
                            <h2 class="texts">
                                Having innovations that could grow your business should be a no-brainer. <br> That's when 2BiG comes in. <br> Join our community now and let's start growing your business.<br>
                                <!--
                                A platform that provides efficient business management. <br>This platform aims to increase productivity to the business <br>and provide quality services for both business owners and customers.  -->
                            </h2>
                        </div>
                        <div class="text-center">
                            <div class="align-items-center ">
                                <video autoplay loop muted controls>
                                    <source src="/images/Updated_ExplainerVideo.mp4" type="video/mp4">
                                </video>
                            </div>
                        </div>
                        <a name="why2big"> </a>
                    </section>   <br><br>
                    <!-- WHY 2BIG?-->
                    <section class="cell medium-8 orange-gradient padding-vertical-4">

                        <div class="h2">
                            <h1 class=" text-center">
                                <br> Why choose 2BiG ? <br>
                            </h1>
                        </div><br>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group form-primary">
                                    <div class="h1">
                                        <h1 class="text-right">
                                            ACCURATE
                                        </h1>
                                    </div>
                                    <div class="h2">
                                        <h2 class=" text-right">
                                            SALES AND <br> REPORTS
                                        </h2>
                                    </div>

                                </div>
                            </div>
                            <div class="verticalLine" style="height:120px">
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group form-primary">
                                    <div class="h1">
                                    <h1 class="text-left">
                                        EFFICIENT
                                    </h1>
                                    </div>
                                    <div class="h2">
                                    <h2 class="text-left">
                                        BUSINESS <br> OPERATIONS <br><br>
                                    </h2>
                                    </div>

                                </div>
                            </div>
                        </div> <br />
                        <div class="text-center">
                            <h2 class="texts">
                                A platform that provides efficient business management. <br>This platform aims to increase productivity to the business <br>and provide quality services for both business owners and customers.
                            </h2>
                        </div>
                        <div cell small-24 class="text-center h4">
                            <br>
                            <a href="#subscription" class="button" style="color:darkblue;font-size: 25px;background-color: white;padding: 10px;">
                                SUBSCRIBE
                                <!--
                                <img src="images/playstore.PNG" style="width:80px"> <br>
                                Download <br> 2BiG Mobile ! <br> <br> -->
                            </a>
                        </div>
                        <br>
                    </section>
                    <!-- SERVICES-->
                    <section class="cell medium-8 padding-vertical-4">
                        <a name="services"> </a>
                        <div>
                            <h1 class="h1 text-center">
                                <br> SERVICES <br>
                            </h1>
                        </div>
                        <br>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-sm-3" style="background-color: white;">
                                    <div class="">
                                        <div class="text-center">
                                            <img src="/images/delivery.png" style="width:50%">
                                        </div>
                                        <p class="h2 text-center">DELIVERY</p>
                                        <h2 class="texts text-center " style="font-size:25px;">Provides reliable and efficient service to residential and commercial customers</h2> <br>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="service-item second-service">
                                        <div class="text-center">
                                            <img src="/images/orders.PNG" style="width:50%">
                                        </div>
                                        <p class="h2 text-center">ONLINE ORDER</p>
                                        <h2 class="texts text-center" style="font-size:25px;">
                                            Online ordering is critical for your business’ success, By using 2BiG, you are building an online presence that would attract more potential customers for your water refilling business.
                                        </h2>
                                    </div>
                                </div>
                                <div class="col-sm-3" style="background-color: white;">
                                    <div class="service-item second-service">
                                        <div class="text-center">
                                            <img src="/images/monitor.PNG" style="width:50%">
                                        </div>
                                        <p class="h2 text-center">MONITORING</p>
                                        <h2 class="texts text-center" style="font-size:25px;">
                                            2BiG allows you to track all orders in real-time. You can also monitor your delivery locations and your sales & reports.
                                        </h2>
                                    </div>
                                </div>
                                <div class="col-sm-3" style="background-color: white;">
                                    <div class="service-item second-service">
                                        <div class="text-center">
                                            <img src="/images/reports.PNG" style="width:50%">
                                        </div>
                                        <p class="h2 text-center">REPORTS</p>
                                        <h2 class="texts text-center" style="font-size:25px;">
                                            2BiG gives you accurate and real-time reports on your sales, deliveries, customers and etc.
                                        </h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                    <br /><br />

                    <div class="text-center page-block ">
                        <section class="cell medium-8 teal-gradient padding-vertical-4">
                            <div class="row">
                                <div class="col-sm-4">
                                    <ul class="nav-left">
                                        <li>
                                            <br>
                                            <div class="text-right">
                                                <img src="/images/water.cartoon.png" style="width:320px" alt="logo.png">
                                            </div>

                                        </li>
                                    </ul>
                                </div>

                                <div class="col-sm-4">
                                    <br><br><br><br> <br /><br />
                                    <div cell small-24 class="text-center">
                                        <a href="#subscription" class="button h2" style="color:darkblue;font-size: 25px;padding: 10px;">
                                            SUBSCRIBE TO 2BIG NOW!
                                            <!--
                                            <img src="images/playstore.PNG" style="width:80px"> <br>
                                            Download <br> 2BiG Mobile ! <br> <br> -->
                                        </a>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <ul class="nav-left">
                                        <li>
                                            <div class="text-left">
                                                <h2 style="color:black;font-size: 100%;" class="">
                                                    <br><br> <br><br><br>
                                                    <a class="h1">
                                                        Boost
                                                    </a>
                                                    <a class="h2">
                                                        your
                                                    </a>
                                                    <a class="h1">
                                                        Water Refilling Business
                                                    </a>
                                                    <a class="h2">
                                                        <br>
                                                        now,  and attract new <br>
                                                    </a>
                                                    <a class="h1">
                                                        Loyal Customers!
                                                    </a>
                                                </h2>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </section>
 
<!--SUBSCRIPTION--->
              <a name="subscription"> </a>          
        <main class="font-mktg">
            <div class="position-relative">
                <div class="d-none d-md-block position-absolute width-full left-0 right-0 z-n1" style="top: 40%">
                    <img class="width-full height-auto"
                        src="https://github.githubassets.com/images/modules/site/features/launchpad/backgrounds/bg-whats-new.svg"
                        aria-hidden="true" alt="" width="1676" height="1040">
                </div>

                <div class="p-responsive container-xl text-center">
                    <br>
                    <br>
                    <div class="text-center">
                        <img src="/images/2ndLogo.png" style="width: 500px" alt="logo.png">
                    </div>
                    <div class="p-responsive container-xl text-center mt-7 mt-md-8 mt-lg-9 mb-5 mb-lg-9">
                        <h6 class="h4-mktg">Take your desired package <br /> and start growing your business now!</h6>
                    </div>
                    <div class="d-lg-flex flex-items-stretch gutter-lg-condensed text-center">
                        <asp:Repeater ID="packagesRepeater" runat="server" OnItemDataBound="packagesRepeater_ItemDataBound">
                            <ItemTemplate>
                                <!-- Package Container -->
                                <div class="col-lg-5 mb-3 mb-lg-0" id='<%# "packagesPanel" + Eval("packageID") %>'>
                                    
                                    <div class="height-full position-relative rounded-3 px-2 pt-5 pb-2 js-pricing-plan" data-min-seats="1" data-max-seats="4">
                                        <div class="d-md-flex flex-column flex-justify-between height-full rounded-3 color-shadow-extra-large color-bg-default">
                                            <div class="px-3 pt-4 pb-3">
                                                <!-- Package Name -->
                                                <asp:Label class="mb-2 h5-mktg" ID="packageName" runat="server"></asp:Label>
                                                <br />
                                                <br />
                                                <!-- Package Description -->
                                                <asp:Label class="lh-condensed mb-2" ID="packageDescription" runat="server" Style="font-size: 16px; color: black"></asp:Label>
                                                <br />
                                                <br />
                                                <div hidden class="js-monthly-cost tooltipped-n tooltipped-multiline tooltipped-no-delay" data-plan="free">
                                                    <h3 class="mb-0">
                                                        <span class="d-flex flex-justify-center flex-items-center">
                                                            <span class="d-flex flex-items-center f0-mktg text-normal mr-2">
                                                                <sup class="f3 color-fg-muted v-align-middle mr-1">₱</sup>
                                                                <!-- Package Price -->
                                                                <asp:Label runat="server" class="js-computed-value" ID="packagePrice"></asp:Label>
                                                                <br />
                                                            </span>
                                                        </span>
                                                        <!-- Package Duration -->
                                                        <asp:Label runat="server" ID="packageDuration" class="text-normal f4 color-fg-muted js-pricing-cost-suffix js-monthly-suffix"></asp:Label>
                                                        <br />
                                                    </h3>
                                                </div>
                                            </div>
                                            <div class="d-lg-block flex-auto text-left rounded-bottom-3 px-3 js-compare-features-item">
                                                <br />
                                                <h4>
                                                    PACKAGE EXCLUSIVE :
                                                </h4>
                                                <br />
                                                 <asp:Image ID="Image2" src="https://img.icons8.com/?size=512&id=21319&format=png" runat="server" Width="20" Height="20" />
                                                 <asp:Label class="lh-condensed mb-2" ID="renewable" runat="server" Style="font-size: 16px; color: black"></asp:Label> <br />
                                                <br />
                                                 <asp:Image ID="Image4" src="https://img.icons8.com/?size=512&id=21319&format=png" runat="server" Width="20" Height="20" />
                                                 <asp:Label class="lh-condensed mb-2" ID="orderLimit" runat="server" Style="font-size: 16px; color: black"></asp:Label> <br />
                                                <br />
                                                 <asp:Image ID="Image3" src="https://img.icons8.com/?size=512&id=21319&format=png" runat="server" Width="20" Height="20" /> 
                                                 <asp:Label class="lh-condensed mb-2" ID="unliProducts" runat="server" Style="font-size: 16px; color: black"></asp:Label><br />
                                            </div>
                                            <div class="d-lg-block flex-auto text-left rounded-bottom-3 px-3 js-compare-features-item">
                                                <br />
                                                <h4>FEATURES:</h4>
                                                <br />
                                                <!-- Features Repeater -->
                                                <asp:Repeater ID='featuresRepeater' runat="server">
                                                    <ItemTemplate>
                                                        <div class="col-10 d-none d-md-block text-left" style="font-size: 16px; height: 50px">
                                                            <div class="height-full">
                                                                <asp:Image ID="Image1" src="https://img.icons8.com/?size=512&id=21319&format=png" runat="server" Width="20" Height="20" />
                                                                <%# Container.DataItem %>
                                                            </div>
                                                        </div>
                                                        <div class="col-10 d-md-none text-left" style="font-size: 16px; height: 50px">
                                                            <div class="height-full">
                                                                <asp:Image ID="Image2" src="https://img.icons8.com/?size=512&id=21319&format=png" runat="server" Width="20" Height="20" />
                                                                <%# Container.DataItem %>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                     <!--PAYPAL BUTTON-->
                                            
                                        <div class="mt-2">
                                            <div>
                                                
                                            </div>
                                            <div class="btn-mktg d-block btn-muted-mktg" id="paypalContainer" runat="server">
                                               <asp:LinkButton runat="server" ID="subsribeNow" OnClick="subsribeNow_Click"> Subscribe now !</asp:LinkButton>
                                            </div>
                                        </div> <br />
                                        </div>
                                    </div>
                                </div>

                            </ItemTemplate>
                        </asp:Repeater>
   

                    </div>
                </div>
            </div>



        </main>


                        <a name="theteam" > </a>
                        <!-- WHY 2BIG?-->
                        <section class="cell medium-8 orange-gradient padding-vertical-4">
                            <div>
                                <h1 class="h3 text-center">
                                    <br> THE TEAM BEHIND 2BIG <br>
                                </h1>
                            </div><br>
                            <div class="container-fluid ">
                                <div class="row">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="">
                                            <div class="text-center">
                                                <br>
                                                <img src="/images/rhea.PNG" style="width:50%">
                                            </div> <br>
                                            <p class="h2 text-center">RHEA MAE TRINIDAD</p>
                                            <h2 class="texts text-center " style="font-size:20px;">PROJECT MANAGER / WEB DEVELOPER</h2>

                                            <a href="mailto:rheamae.trinidad@ctu.edu.ph" class="button h5">
                                                <i class="ti-email"></i>
                                                Contact
                                            </a>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="">
                                            <div class="text-center">
                                                <br>
                                                <img src="/images/jeahael.jpg" style="width:50%">
                                            </div> <br>
                                            <p class="h2 text-center">Jeahael Suhot</p>
                                            <h2 class="texts text-center " style="font-size:20px;">MOBILE DEVELOPER </h2>

                                            <a href="mailto:jeahael.suhot@ctu.edu.ph" class="button h5">
                                                <i class="ti-email"></i>
                                                Contact
                                            </a>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="">
                                            <div class="text-center">
                                                <br>
                                                <img src="/images/ronalyn.jpg" style="width:50%">
                                            </div> <br>
                                            <p class="h2 text-center">RONALYN GIDUCOS</p>
                                            <h2 class="texts text-center " style="font-size:20px;">WEB DEVELOPER </h2>
                                            <a href="mailto:ronalyn.giducos@ctu.edu.ph" class="button h5">
                                                <i class="ti-email"></i>
                                                Contact
                                            </a>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="">
                                            <div class="text-center">
                                                <br>
                                                <img src="/images/daisy.jpg" style="width:50%">
                                            </div> <br>
                                            <p class="h2 text-center">DAISY LIMATO</p>
                                            <h2 class="texts text-center " style="font-size:20px;">MOBILE DEVELOPER</h2>
                                            <a href="mailto:daisy.limato@ctu.edu.ph" class="button h5">
                                                <i class="ti-email"></i>
                                                Contact
                                            </a>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                            </div> <br>
                        </section>


                        <br />
                        <section class="cell medium-8 teal-gradient padding-vertical-4">
                            <div class="row">
                                <div class="col-sm-4">
                                    <ul class="nav-left">
                                        <li>
                                            <br>
                                            <div class="text-right">
                                                <img src="/images/water.cartoon.png" style="width:320px" alt="logo.png">
                                            </div>

                                        </li>
                                    </ul>
                                </div>

                                <div class="col-sm-4">
                                    <br><br><br><br>
                                    <div cell small-24 class="text-center">
                                        <a href="https://play.google.com/store/games" class="button" style="color:darkblue;font-size: 25px;padding: 6px;">
                                            <!-- SUBSCRIBE TO 2BIG NOW!-->

                                            <img src="/images/playstore.PNG" style="width:80px"> <br>
                                            Download <br> 2BiG Driver ! <br> <br>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <ul class="nav-left">
                                        <li>
                                            <div class="text-left">
                                                <h2 style="color:black;font-size: 100%;" class="">
                                                    <br><br> <br><br><br>
                                                    <a class="h1">
                                                        Boost
                                                    </a>
                                                    <a class="h2">
                                                        your
                                                    </a>
                                                    <a class="h1">
                                                        Water Refilling Business
                                                    </a>
                                                    <a class="h2">
                                                        <br>
                                                        now,  and attract new <br>
                                                    </a>
                                                    <a class="h1">
                                                        Loyal Customers!
                                                    </a>
                                                </h2>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>

</body>
</html>

