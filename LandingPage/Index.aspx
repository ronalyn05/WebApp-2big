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
    <!-- Pre-loader start -->
    <div class="theme-loader">
        <div class="loader-track">
            <div class="preloader-wrapper">
                <div class="spinner-layer spinner-blue">
                    <div class="circle-clipper left">
                        <div class="circle"></div>
                    </div>
                    <div class="gap-patch">
                        <div class="circle"></div>
                    </div>
                    <div class="circle-clipper right">
                        <div class="circle"></div>
                    </div>
                </div>
                <div class="spinner-layer spinner-red">
                    <div class="circle-clipper left">
                        <div class="circle"></div>
                    </div>
                    <div class="gap-patch">
                        <div class="circle"></div>
                    </div>
                    <div class="circle-clipper right">
                        <div class="circle"></div>
                    </div>
                </div>

                <div class="spinner-layer spinner-yellow">
                    <div class="circle-clipper left">
                        <div class="circle"></div>
                    </div>
                    <div class="gap-patch">
                        <div class="circle"></div>
                    </div>
                    <div class="circle-clipper right">
                        <div class="circle"></div>
                    </div>
                </div>

                <div class="spinner-layer spinner-green">
                    <div class="circle-clipper left">
                        <div class="circle"></div>
                    </div>
                    <div class="gap-patch">
                        <div class="circle"></div>
                    </div>
                    <div class="circle-clipper right">
                        <div class="circle"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Pre-loader end -->
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
                                    <span> Account </span>
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

                        <div>
                            <h1 class="h3 text-center">
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
                                    <source src="/images/EXPLAINER_VIDEO.mp4" type="video/mp4">
                                </video>
                            </div>
                        </div>
                        <a name="why2big"> </a>
                    </section>   <br><br>
                    <!-- WHY 2BIG?-->
                    <section class="cell medium-8 orange-gradient padding-vertical-4">

                        <div>
                            <h1 class="h3 text-center">
                                <br> Why choose 2BiG ? <br>
                            </h1>
                        </div><br>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group form-primary">
                                    <p class="h2 text-right">
                                        ACCURATE
                                    </p>
                                    <h1 class="h3 text-right">
                                        SALES AND <br> REPORTS
                                    </h1>
                                </div>
                            </div>
                            <div class="verticalLine">
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group form-primary">
                                    <p class="h2 text-left">
                                        EFFICIENT
                                    </p>
                                    <p class="h3 text-left">
                                        BUSINESS <br> OPERATIONS <br><br>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="text-center">
                            <h2 class="texts">
                                A platform that provides efficient business management. <br>This platform aims to increase productivity to the business <br>and provide quality services for both business owners and customers.
                            </h2>
                        </div>
                        <div cell small-24 class="text-center h4">
                            <br>
                            <a href="#subscription" class="button" style="color:darkblue;font-size: 25px;background-color: pink;padding: 6px;">
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
                            <h1 class="h3 text-center">
                                <br> SERVICES <br>
                            </h1>
                        </div>
                        <br>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-sm-3" style="background-color: pink;">
                                    <div class="">
                                        <div class="text-center">
                                            <img src="/images/delivery.png" style="width:50%">
                                        </div>
                                        <p class="h3 text-center">DELIVERY</p>
                                        <h2 class="texts text-center " style="font-size:20px;">Provides reliable and efficient service to residential and commercial customers</h2> <br>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="service-item second-service">
                                        <div class="text-center">
                                            <img src="/images/orders.PNG" style="width:50%">
                                        </div>
                                        <p class="h3 text-center">ONLINE ORDER</p>
                                        <h2 class="texts text-center" style="font-size:20px;">
                                            Online ordering is critical for your business’ success, By using 2BiG, you are building an online presence that would attract more potential customers for your water refilling business.
                                        </h2>
                                    </div>
                                </div>
                                <div class="col-sm-3" style="background-color: pink;">
                                    <div class="service-item second-service">
                                        <div class="text-center">
                                            <img src="/images/monitor.PNG" style="width:50%">
                                        </div>
                                        <p class="h3 text-center">MONITORING</p>
                                        <h2 class="texts text-center" style="font-size:20px;">
                                            2BiG allows you to track all orders in real-time. You can also monitor your delivery locations and your sales & reports.
                                        </h2>
                                    </div>
                                </div>
                                <div class="col-sm-3" style="background-color: white;">
                                    <div class="service-item second-service">
                                        <div class="text-center">
                                            <img src="/images/reports.PNG" style="width:50%">
                                        </div>
                                        <p class="h3 text-center">REPORTS</p>
                                        <h2 class="texts text-center" style="font-size:20px;">
                                            2BiG gives you accurate and real-time reports on your sales, deliveries, customers and etc.
                                        </h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>


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
                                    <br><br><br><br>
                                    <div cell small-24 class="text-center">
                                        <a href="#subscription" class="button" style="color:darkblue;font-size: 25px;background-color: pink;padding: 6px;">
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
                                                <h6 style="color:black;font-size: 100%;" class="">
                                                    <br><br> <br><br><br>
                                                    <a class="h3">
                                                        Boost
                                                    </a>
                                                    <a class="h4">
                                                        your
                                                    </a>
                                                    <a class="h3">
                                                        Water Refilling Business
                                                    </a>
                                                    <a class="h4">
                                                        <br>
                                                        now,  and attract new <br>
                                                    </a>
                                                    <a class="h3">
                                                        Loyal Customers!
                                                    </a>
                                                </h6>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </section>
                        <!-- SUBSCRIPTION-->
                        <section class="cell medium-8 padding-vertical-4">
                            <a name="subscription"> </a>
                            <div>
                                <h1 class="h3 text-center">
                                    <br> SUBSCRIPTION <br>
                                </h1>
                                <h4 style="color:black"> Pick the Best Plan</h4>
                                <h2 class="texts text-center " style="font-size:20px;">Take your desired plan to get access to our service easily</h2><br />

                            </div>
                            <br>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-sm-3" style="background-color: white;">
                                    </div>

                                    <div class="col-sm-3" style="background-color: pink;">
                                        <div class="">
                                            <div class="text-center">
                                                <img src="/images/BasicPlan.PNG" style="width:100%">
                                            </div>
                                            <p class="h3 text-center">BASIC PLAN</p>
                                            <h2 class="texts text-center " style="font-size:20px;"> Grow your Water Refilling Business for only ₱3000 good for 6 Months ! Enjoy a hassle-free business process and attract new and loyal customers</h2> <br>
                                            <div cell small-24 class="text-center">
                                                <a href="BasicPlanSub.aspx" class="button" style="color:darkblue;font-size: 18px;background-color: lightskyblue;padding: 10px;">
                                                    SUBSCRIBE
                                                </a>
                                            </div> <br>
                                        </div>
                                    </div>


                                    <div class="col-sm-3" style="background-color: lightskyblue;">
                                        <div class="service-item second-service">
                                            <div class="text-center">
                                                <img src="/images/PremiumPlan.PNG" style="width:100%">
                                            </div>
                                            <p class="h3 text-center">PREMIUM PLAN</p>
                                            <h2 class="texts text-center" style="font-size:20px;">
                                               Grow your Water Refilling Business for only ₱5500 good for 1 year ! Enjoy a hassle-free business process and attract new and loyal customers
                                            </h2> <br>
                                            <div cell small-24 class="text-center">
                                                <a href="PremiumPlanSub" class="button" style="color:darkblue;font-size: 18px;background-color: pink;padding: 10px;">
                                                    SUBSCRIBE
                                                </a>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3" style="background-color: white;">
                                        <div class="service-item second-service">
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </section>
                        <a name="theteam" > </a>
                        </section class="login-block">
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
                                            <p class="h4 text-center">RHEA MAE TRINIDAD</p>
                                            <h2 class="texts text-center " style="font-size:16px;">PROJECT MANAGER </h2>

                                            <a href="" class="button h6">
                                                <i class="ti-email"></i>
                                                Contact
                                            </a>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="">
                                            <div class="text-center">
                                                <br>
                                                <img src="/images/ja.PNG" style="width:50%">
                                            </div> <br>
                                            <p class="h4 text-center">Jeahael Suhot</p>
                                            <h2 class="texts text-center " style="font-size:16px;">SOFTWARE ENGINEER </h2>

                                            <a href="" class="button h6">
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
                                                <img src="/images/rona.jpg" style="width:50%">
                                            </div> <br>
                                            <p class="h4 text-center">RONALYN GIDUCOS</p>
                                            <h2 class="texts text-center " style="font-size:16px;">UI DESIGNER </h2>
                                            <a href="" class="button h6">
                                                <i class="ti-email"></i>
                                                Contact
                                            </a>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="">
                                            <div class="text-center">
                                                <br>
                                                <img src="/images/DC.jpg" style="width:50%">
                                            </div> <br>
                                            <p class="h4 text-center">DAISY LIMATO</p>
                                            <h2 class="texts text-center " style="font-size:16px;">UI DESIGNER</h2>
                                            <a href="" class="button h6">
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
                                            Download <br> 2BiG Mobile ! <br> <br>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <ul class="nav-left">
                                        <li>
                                            <div class="text-left">
                                                <h6 style="color:black;font-size: 100%;" class="">
                                                    <br><br> <br><br><br>
                                                    <a class="h3">
                                                        Boost
                                                    </a>
                                                    <a class="h4">
                                                        your
                                                    </a>
                                                    <a class="h3">
                                                        Water Refilling Business
                                                    </a>
                                                    <a class="h4">
                                                        <br>
                                                        now,  and attract new <br>
                                                    </a>
                                                    <a class="h3">
                                                        Loyal Customers!
                                                    </a>
                                                </h6>
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
</body>
</html>

