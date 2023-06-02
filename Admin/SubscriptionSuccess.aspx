<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionSuccess.aspx.cs" Inherits="WRS2big_Web.Admin.SubscriptionSuccess" %>


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
</head>

<body themebg-pattern="theme1">
    <form id="form1" runat="server">
        <section>
            <div class="text-center">
                <img src="/images/2ndLogo.png" style="width: 700px" alt="logo.png">
            </div>
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <div class="col-lg-7 mb-10 mb-lg-0" style="margin-left: 250px; background-color: lightblue">
                        <br />
                        <h6 class="h6-mktg" runat="server" id="subSuccessTitle">Subscription Confirmation</h6>
                                 
                        <div class="d-lg-flex flex-items-stretch  text-center">
                            <div class="height-full position-relative rounded-3 px-2 pt-5 pb-2 js-pricing-plan">
                                <div class="d-md-flex flex-column flex-justify-between height-full rounded-3 color-shadow-extra-large color-bg-default">
                                    <br />
                                    <br />
                                    
                                    <br />
                                    <div class="px-1 pt-1 pb-3">

                                        <asp:Label class="mb-2 h5-mktg" ID="packageName" runat="server"> </asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label class=" lh-condensed mb-2" ID="packagedescription" runat="server" Style="font-size: 18px; color: black">  </asp:Label>
                                        <br />
                                        <br />
                                        <div hidden class="js-monthly-cost tooltipped-n tooltipped-multiline tooltipped-no-delay"
                                            data-plan="free">
                                            <h3 class="mb-0">
                                                <span class="d-flex flex-justify-center flex-items-center">
                                                    <span class="d-flex flex-items-center f0-mktg text-normal mr-2">
                                                        <sup class="f3 color-fg-muted v-align-middle mr-1">₱</sup>
                                                        <asp:Label runat="server" class="js-computed-value" ID="packagePrice"></asp:Label>
                                                        <br />
                                                    </span>
                                                </span>
                                                <asp:Label runat="server" ID="packageDuration" class="text-normal f4 color-fg-muted js-pricing-cost-suffix js-monthly-suffix"></asp:Label>
                                                <br />
                                            </h3>
                                        </div>
                                        <br />
                                        <br />
                                        <center>
                                            <div class="col text-center">
                                                <asp:Button ID="btnContinue" class="btn-mktg d-block btn-muted-mktg" OnClick="btnContinue_Click" runat="server" Text="Confirm" />
                                                
                                            </div>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />



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



