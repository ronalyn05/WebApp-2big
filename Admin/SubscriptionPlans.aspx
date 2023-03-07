﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionPlans.aspx.cs" Inherits="WRS2big_Web.Admin.SubscriptionPlans" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>2BiG: WRS Management System </title>
    <!-- HTML5 Shim and Respond.js IE10 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 10]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
      <![endif]-->
    <!-- Meta -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="Mega Able Bootstrap admin template made using Bootstrap 4 and it has huge amount of ready made feature, UI components, pages which completely fulfills any dashboard needs." />
    <meta name="keywords" content="bootstrap, bootstrap admin template, admin theme, admin dashboard, dashboard template, admin template, responsive" />
    <meta name="author" content="codedthemes" />
    <!-- Favicon icon -->
    <link rel="icon" src="/images/FinalLogo.png" type="image/x-icon">

    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,500" rel="stylesheet">
    <!-- Required Fremwork -->
    <link rel="stylesheet" type="text/css" href="/assets/css/bootstrap/css/bootstrap.min.css">
    <!-- waves.css -->
    <link rel="stylesheet" href="/assets/pages/waves/css/waves.min.css" type="text/css" media="all">
    <!-- themify-icons line icon -->
    <link rel="stylesheet" type="text/css" href="/assets/icon/themify-icons/themify-icons.css">
    <!-- ico font -->
    <link rel="stylesheet" type="text/css" href="/assets/icon/icofont/css/icofont.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" type="text/css" href="/assets/icon/font-awesome/css/font-awesome.min.css">
    <!-- Style.css -->
    <link rel="stylesheet" type="text/css" href="/assets/css/style.css">
</head>

<body themebg-pattern="theme1">
    <form id="form1" runat="server">
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
    <section class="login-block">
               <div class="text-center">
                     <img src="/images/2ndLogo.png" style="width:500px" alt="logo.png">
                </div>
        <!-- Container-fluid starts -->
        <div class="container-fluid ">
            <div class="row">
                <div class="col-md-12">
                    <form class="md-float-material form-material">
                        <!-- SUBSCRIPTION-->
                        <section class="cell medium-8 padding-vertical-4">
                            <a name="subscription"> </a>
                            <div>
                                <h1 class="h3 text-center">
                                    <br> SUBSCRIPTION PLANS <br>
                                </h1>
                                <h2 class="texts text-center " style="font-size:20px;">Pick the best plan and start growing your business NOW !</h2><br />

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
                  </form>
              </div>
     <!-- end of col-sm-12 -->
            </div>
   <!-- end of row -->
    </div>
<!-- end of container-fluid -->
    </section>

    <!-- Required Jquery -->
    <script type="text/javascript" src="/assets/js/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="/assets/js/jquery-ui/jquery-ui.min.js "></script>
    <script type="text/javascript" src="/assets/js/popper.js/popper.min.js"></script>
    <script type="text/javascript" src="/assets/js/bootstrap/js/bootstrap.min.js "></script>
    <!-- waves js -->
    <script src="/assets/pages/waves/js/waves.min.js"></script>
    <!-- jquery slimscroll js -->
    <script type="text/javascript" src="/assets/js/jquery-slimscroll/jquery.slimscroll.js "></script>
    <!-- modernizr js -->
    <script type="text/javascript" src="/assets/js/SmoothScroll.js"></script>
    <script src="/assets/js/jquery.mCustomScrollbar.concat.min.js "></script>
    <!-- i18next.min.js -->
    <script type="text/javascript" src="~/bower_components/i18next/js/i18next.min.js"></script>
    <script type="text/javascript" src="~/bower_components/i18next-xhr-backend/js/i18nextXHRBackend.min.js"></script>
    <script type="text/javascript" src="~/bower_components/i18next-browser-languagedetector/js/i18nextBrowserLanguageDetector.min.js"></script>
    <script type="text/javascript" src="~/bower_components/jquery-i18next/js/jquery-i18next.min.js"></script>
    <script type="text/javascript" src="/assets/js/common-pages.js"></script>
    <script src="/Scripts/MyScript/Index.js"></script>
</form>
</body>
</html>

