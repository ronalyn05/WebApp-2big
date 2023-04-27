<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionPlans.aspx.cs" Inherits="WRS2big_Web.Admin.SubscriptionPlans" %>


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


    <style>
        .texts{
            font-size:22px;
            color:black;
        }
        .auto-style1 {
            margin-left: 68;
        }
    </style>
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
                                <h1 class="h3 text-center" style="color:black">
                                    <br>Pick the Best plan<br>
                                </h1>
                                <h2 class="texts text-center " style="font-size:20px;">
                                    Take your desired plan to be able to use our platform, and start growing your business now!
                                </h2><br />

                            </div>
                            <br>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-sm-3" style="background-color: transparent;">
                                    </div>
                                    <asp:Repeater ID="rtpPackages" runat="server">
                                        
                                    </asp:Repeater>
                                    <div class="col-sm-3 card" style="background-color: white;">
                                        <div class="">
                                            <br /> <br /> <br />
                                            <p class="texts text-center">
                                                <asp:Label runat="server" ID="basinplanLabel">

                                                </asp:Label>
                                              </p>
                                            <p class="texts text-center">
                                                <asp:Label runat="server" ID="basicPriceLabel">

                                                </asp:Label> for
                                                <asp:Label runat="server" ID="basicDurationLabel">
                                                    
                                                </asp:Label> 
                                              </p>
                                            <p class="texts text-center " style="font-size:20px;" > 
                                               <%-- Grow your Water Refilling Business for only ₱3000 good for 6 Months ! Enjoy a hassle-free business process and attract new and loyal customers--%>
                                                <asp:Label ID="descriptionBasic" runat="server"> 

                                                </asp:Label>
                                            </p> <br /> <br>
                                                <h7 runat="server" class="texts"> Features: </h7> <br />
                                                <asp:ListBox ID="BasicfeaturesList" runat="server" Font-Size="14pt"  style="background-color:transparent" Width="389px" Height="213px">

                                                </asp:ListBox> <br /> <br />
<%--                                           <div cell small-24 class="text-center">
                                                <a href="BasicPlanSub.aspx" class="button" style="color:darkblue;font-size: 18px;background-color: lightskyblue;padding: 10px;">
                                                    SUBSCRIBE
                                                </a>
                                            </div> --%>
                                                <div class="container pt-4 px-0">
                                                    <div id="paypal-button"></div>
                                                </div>
                                                  <div class="container pt-4 px-0">
                                                   <a href="WaitingPage.aspx" class="button btn btn-danger">
                                                    CANCEL
                                                    </a>
                                                </div> <br />

                                        </div>
                                    </div> 



                                    <div class="col-sm-3 card" style="background-color: white;">
                                        <div class="service-item second-service">
                                            <br /> <br /> <br />
                                            <p class="texts text-center">
                                                <asp:Label runat="server" ID="premiumplanLabel">

                                                </asp:Label>
                                            </p>
                                            <p class="texts text-center">
                                                <asp:Label runat="server" ID="premiumPriceLabel">

                                                </asp:Label> /
                                                <asp:Label runat="server" ID="premiumDurationLabel">
                                                    
                                                </asp:Label> 
                                              </p>
                                            <p class="texts text-center" style="font-size:20px;">
                                                <asp:Label ID="descriptionPremium" runat="server"> 

                                                </asp:Label>
                                            </p> <br>
                                                <asp:Label runat="server" class="texts" > Features: </asp:Label> <br />
                                                <asp:ListBox ID="premiumFeaturesList" Font-Size="14pt" runat="server" style="background-color:transparent" CssClass="auto-style1" Width="389px" Height="207px">

                                                </asp:ListBox> <br /> <br />
<%--                                            <div cell small-24 class="text-center">
                                                <a href="PremiumPlanSub" class="button" style="color:darkblue;font-size: 18px;background-color: lightskyblue;padding: 10px;">
                                                    SUBSCRIBE
                                                </a>
                                            </div>--%>
                                            <!--PAYPAL BUTTON-->
                                                <div class="container pt-4 px-0">
                                                    <div id="paypal-button-container-premium"></div>
                                                </div>
                                                 <div class="container pt-4 px-0">
                                                   <a href="WaitingPage.aspx" class="button btn btn-danger">
                                                    CANCEL
                                                    </a>
                                                </div> <br />
                                        </div>
                                    </div>

                                    <div class="col-sm-3" style="background-color: transparent;">
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
<script type="text/javascript" src="https://www.paypal.com/sdk/js?client-id=AYAFhn0XmpP6EiQM_GVzmj2KIUjfOPS_jgzExbE8Ssmim0KMrdhdzESAlmgYUmSVLfCia0vrDaPPvJBZ&currency=PHP">
    </script>
            <!--BASIC PLAN-->
            <script>
                var name = '<%= Session["Lname"] %>';
                var lname = '<%= Session["Fname"] %>';

                paypal.Buttons({
                    createOrder: function (data, actions) {
                        return actions.order.create({
                            payee: {
                                name: {
                                    given_name: '<%= Session["Fname"] %>',
                                    surname: '<%= Session["Lname"] %>'
                                },
                                phone: {
                                    phone_type: "MOBILE",
                                    phone_num: '<%= Session["Phone"] %>'
                                },
                                email: '<%= Session["Email"] %>',
                            },
                            purchase_units: [{
                                amount: {
                                    value: '.1'

                                }
                            }]
                        });
                    },
                    onApprove: function (data, actions) {
                        return actions.order.capture().then(function (details) {
                            console.log(details)
                            sessionStorage.setItem("plan", "basic");
                            window.location.replace("BasicSubSuccess.aspx");
                        });
                    },
                    onCancel: function (data) {
                       window.location.replace("SubscriptionPlans.aspx");
                      
                    }
                }).render("#paypal-button");
            </script>

        <%--<script type="text/javascript" src="https://www.paypal.com/sdk/js?client-id=AYAFhn0XmpP6EiQM_GVzmj2KIUjfOPS_jgzExbE8Ssmim0KMrdhdzESAlmgYUmSVLfCia0vrDaPPvJBZ&currency=PHP">
    </script>--%>

     <!--PREMIUM PLAN-->
        <script>
            var name = '<%= Session["Lname"] %>';
            var lname = '<%= Session["Fname"] %>';
            

            paypal.Buttons({
                createOrder: function (data, actions) {
                    return actions.order.create({
                        payee: {
                            name: {
                                given_name: '<%= Session["Fname"] %>',
                            surname: '<%= Session["Lname"] %>'
                        },
                        phone: {
                            phone_type: "MOBILE",
                            phone_num: '<%= Session["Phone"] %>'
                        },
                        email: '<%= Session["Email"] %>',
                        },
                        purchase_units: [{
                            amount: {
                                value: '3000'

                            }
                        }]
                    });
                },
                onApprove: function (data, actions) {
                    return actions.order.capture().then(function (details) {
                        console.log(details)
                        sessionStorage.setItem("plan", "premium");
                        window.location.replace("PremiumSubSuccess.aspx");
                    });
                },
                onCancel: function (data) {
                    window.location.replace("SubscriptionPlans.aspx");
                }
            }).render("#paypal-button-container-premium");
        </script>


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


