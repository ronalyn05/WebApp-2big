<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionPackages.aspx.cs" Inherits="WRS2big_Web.Admin.SubscriptionPackages" %>

<!DOCTYPE html>
<html lang="en" data-a11y-animated-images="system">

<head>
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

    <title>2BiG: WRS Management System
    </title>


</head>

<body class="logged-in env-production page-responsive header-white" style="word-wrap: break-word;">
    <div class="logged-in env-production page-responsive header-white" style="word-wrap: break-word;">
        <div class="position-relative js-header-wrapper ">
            <a href="#start-of-content"
                class="p-3 color-bg-accent-emphasis color-fg-on-emphasis show-on-focus js-skip-to-content">Skip to content
            </a>
            <span data-view-component="true" class="progress-pjax-loader Progress position-fixed width-full">
                <span style="width: 0%;" data-view-component="true"
                    class="Progress-item progress-pjax-loader-bar left-0 top-0 color-bg-accent-emphasis"></span>
            </span>

            <script crossorigin="anonymous" defer="defer" type="application/javascript"
                src="https://github.githubassets.com/assets/vendors-node_modules_allex_crc32_lib_crc32_esm_js-node_modules_github_mini-throttle_dist_deco-26fa0f-02e7ed68dae1.js">
            </script>
            <script crossorigin="anonymous" defer="defer" type="application/javascript"
                src="https://github.githubassets.com/assets/vendors-node_modules_github_clipboard-copy-element_dist_index_esm_js-node_modules_delegated-e-b37f7d-a9177ba414f2.js">
            </script>
            <script crossorigin="anonymous" defer="defer" type="application/javascript"
                src="https://github.githubassets.com/assets/app_assets_modules_github_command-palette_items_help-item_ts-app_assets_modules_github_comman-48ad9d-beffe41c24a7.js">
            </script>
            <script crossorigin="anonymous" defer="defer" type="application/javascript"
                src="https://github.githubassets.com/assets/command-palette-c2a5f7e7eb12.js">
            </script>
        </div>

        <div id="start-of-content" class="show-on-focus"></div>

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

                        <!--PACKAGE A HERE-->
                        <div class="col-lg-4 mb-3 mb-lg-0">
                            <div class="height-full position-relative rounded-3 px-2 pt-5 pb-2 js-pricing-plan" data-min-seats="1"
                                data-max-seats="4">
                                <div
                                    class="d-md-flex flex-column flex-justify-between height-full rounded-3 color-shadow-extra-large color-bg-default">
                                    <div class="px-3 pt-4 pb-3">
                                        <asp:Label class="mb-2 h5-mktg" ID="packageAName" runat="server"> </asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label class=" lh-condensed mb-2" ID="packageAdescription" runat="server" Style="font-size: 16px; color: black">  </asp:Label>
                                        <br />
                                        <br />
                                        <div hidden class="js-monthly-cost tooltipped-n tooltipped-multiline tooltipped-no-delay"
                                            data-plan="free">
                                            <h3 class="mb-0">
                                                <span class="d-flex flex-justify-center flex-items-center">
                                                    <span class="d-flex flex-items-center f0-mktg text-normal mr-2">
                                                        <sup class="f3 color-fg-muted v-align-middle mr-1">₱</sup>
                                                        <asp:Label runat="server" class="js-computed-value" ID="packageAPrice"></asp:Label>
                                                        <br />
                                                    </span>
                                                </span>
                                                <asp:Label runat="server" ID="durationA" class="text-normal f4 color-fg-muted js-pricing-cost-suffix js-monthly-suffix"></asp:Label>
                                                <br />
                                            </h3>
                                        </div>
                                    </div>

                                    <div
                                        class="d-lg-block flex-auto text-left rounded-bottom-3 px-3 js-compare-features-item">
                                        <br />
                                        <h4>Features:</h4>
                                        <br />
                                        <!--FEATURES AREA HERE-->
                                        <asp:Repeater ID="featuresPackageA" runat="server">
                                            <ItemTemplate>

                                                <div class="col-10 d-none d-md-block text-left" style="font-size: 16px; height: 50px">
                                                    <div class="height-full">
                                                        <asp:Image ID="Image1" src="https://img.icons8.com/?size=512&id=21319&format=png" runat="server" Width="20" Height="20" />
                                                        <%# Container.DataItem%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="mt-2">
                                            <a class="btn-mktg d-block btn-muted-mktg" href="PackageAPage.aspx">View Package details
                                            </a>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>



                        <!--PACKAGE B HERE-->
                        <div class="col-lg-4 mb-3 mb-lg-0">
                            <div class="height-full position-relative rounded-3 px-2 pt-5 pb-2 js-pricing-plan" data-min-seats="5" data-max-seats="10">
                                <p class="position-absolute left-0 right-0 h5 text-center text-uppercase color-text-white js-recommended-plan-caption" hidden style="top: 6px;">Most popular</p>
                                <div class="d-md-flex flex-column flex-justify-between height-full rounded-3 color-shadow-extra-large color-bg-default">
                                    <div class="px-3 pt-4 pb-3">
                                        <asp:Label runat="server" class="mb-2 h5-mktg" ID="packageBName"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label runat="server" class="lh-condensed" ID="packageBdescription" Style="font-size: 16px; color: black"></asp:Label>
                                        <br />
                                        <br />
                                        <div hidden class="js-monthly-cost tooltipped-n tooltipped-multiline tooltipped-no-delay"
                                            data-plan="business">
                                            <h3 class="mb-0">
                                                <span class="d-flex flex-justify-center flex-items-center">
                                                    <span class="d-flex flex-items-center f0-mktg text-normal mr-2">
                                                        <sup class="f3 color-fg-muted v-align-middle mr-1">₱</sup>
                                                        <asp:Label runat="server" class="js-computed-value" ID="packageBPrice"></asp:Label>
                                                        <br />

                                                    </span>
                                                </span>
                                                <asp:Label runat="server" ID="durationB" class="text-normal f4 color-fg-muted js-pricing-cost-suffix js-monthly-suffix"></asp:Label>
                                                <br />
                                            </h3>
                                        </div>

                                    </div>
                                    <div
                                        class="d-lg-block flex-auto text-left rounded-bottom-3  px-3 py-2 js-compare-features-item">
                                        <br />
                                        <h4>Features:</h4>
                                        <br />
                                        <asp:Repeater ID="featuresPackageB" runat="server">
                                            <ItemTemplate>

                                                <div class="col-10 d-none d-md-block text-left" style="font-size: 16px; height: 50px">
                                                    <div class="height-full">
                                                        <asp:Image ID="Image1" src="https://img.icons8.com/?size=512&id=21319&format=png" runat="server" Width="20" Height="20" />
                                                        <%# Container.DataItem%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        
                                        <div class="mt-2">
                                            <a class="btn-mktg d-block btn-muted-mktg" href="PackageBPage.aspx">View Package details
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--PACKAGE C HERE-->
                        <div class="col-lg-4 mb-3 mb-lg-0">
                            <div class="height-full position-relative rounded-3 px-2 pt-5 pb-2 js-pricing-plan" data-min-seats="11"
                                data-max-seats="Infinity">
                                <div
                                    class="d-md-flex flex-column flex-justify-between height-full rounded-3 color-shadow-extra-large color-bg-default">
                                    <div class="px-3 pt-4 pb-3">
                                        <asp:Label runat="server" class="mb-2 h5-mktg" ID="packageCName"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label runat="server" class=" lh-condensed" ID="packageCdescription" style="font-size: 16px; color: black"></asp:Label>
                                        <br />
                                        <br />
                                        <div hidden class="js-monthly-cost tooltipped-n tooltipped-multiline tooltipped-no-delay"
                                            data-plan="business">
                                            <h3 class="mb-0">
                                                <span class="d-flex flex-justify-center flex-items-center">
                                                    <span class="d-flex flex-items-center f0-mktg text-normal mr-2">
                                                        <sup class="f3 color-fg-muted v-align-middle mr-1">₱</sup>
                                                        <asp:Label runat="server" class="js-computed-value" ID="packageCPrice"></asp:Label>
                                                    </span>
                                                </span>
                                                <asp:Label runat="server" ID="durationC" class="text-normal f4 color-fg-muted js-pricing-cost-suffix js-monthly-suffix"> </asp:Label>
                                                <br />
                                            </h3>
                                        </div>

                                    </div>
                                    <div
                                        class="d-lg-block flex-auto text-left rounded-bottom-3  px-3 py-2 js-compare-features-item">
                                        <br />
                                        <h4>Features:</h4>
                                        <br />
                                        <asp:Repeater ID="featuresPackageC" runat="server">
                                            <ItemTemplate>

                                                <div class="col-10 d-none d-md-block text-left" style="font-size: 16px; height: 50px">
                                                    <div class="height-full">
                                                        <asp:Image ID="Image1" src="https://img.icons8.com/?size=512&id=21319&format=png" runat="server" Width="20" Height="20" />
                                                        <%# Container.DataItem %>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <!--BUTTON-->
                                        <div class="mt-2">
                                            <a class="btn-mktg d-block btn-muted-mktg" href="PackageCPage.aspx">View Package details
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>




                </div>
            </div>



        </main>

    </div>
    <style>
        .user-mention[href$="/RheaMaeRMT"] {
            color: var(--color-user-mention-fg);
            background-color: var(--color-user-mention-bg);
            border-radius: 2px;
            margin-left: -2px;
            margin-right: -2px;
            padding: 0 2px;
        }
    </style>

    <div id="js-global-screen-reader-notice" class="sr-only" aria-live="polite"></div>
</body>

</html>

