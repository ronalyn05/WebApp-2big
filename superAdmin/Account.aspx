<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="WRS2big_Web.superAdmin.Account" %>


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

        <!-- Include the Leaflet CSS file -->
    <link rel="stylesheet" href="/assets/leaflet/leaflet.css"/>

    <!-- Include the Leaflet JavaScript file -->
    <script src="/assets/leaflet/leaflet.js"></script>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
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
        <!-- Container-fluid starts -->
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <form class="md-float-material form-material">
                        <div class="text-center">
                            <img src="/images/2ndLogo.png" style="width:500px" alt="logo.png">
                        </div>
                        <div class="col-xl-6 col-md-8" style="align-content:center;margin-left:480px">
                        <div class="card">
                            <div class="card-block"> <br />
                                <br><br>

                                <%--LOG IN FIELD--%>
                                    <div id="login">
                                        <div class="row">
<%--                                            @*username*@--%>
                                            <div class="col">
                                                <div class="">
                                                    <%--<label>Email:</label>--%>
                                                    <label>Email:</label>
                                                    <div class="input-group">
                                           <%-- <asp:TextBox runat="server"  type="usrname" class="form-control" id="txt_email" ValidationGroup="a"></asp:TextBox> --%>
                                                        <asp:TextBox runat="server" type="idno" class="form-control" ID="txt_idno" ValidationGroup="a" > </asp:TextBox>
                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationExpression="^[\w.\-]{2,18}$" ControlToValidate="txt_username"  ErrorMessage="Invalid Username"></asp:RegularExpressionValidator>--%>
                                                    </div>
                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  ForeColor="Red" ControlToValidate="txt_idno" ValidationGroup="a"></asp:RegularExpressionValidator>--%>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
<%--                                            @*password*@--%>
                                            <div class="col">
                                                <div class="form-group">
                                                    <label>Password:</label>
                                                    <div class="input-group">
                                            <asp:TextBox runat="server" type="password" class="form-control" TextMode="Password" id="txt_password" ValidationGroup="a"></asp:TextBox> 

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
<%--                                        @*Show Password checkbox*@--%>
                                        <div class="row m-t-25 text-left">
                                            <div class="col-md-12">
                                                <div class="checkbox-fade fade-in-primary">
                                                    <label>
                                                        <input type="checkbox" value="" id="togglePassword">
                                                        <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                        <span class="text-inverse">Show Password</span>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>

                                        

                                    <%-- @*Show Password script*@--%>
                                       <script>
                                           const togglePassword = document.querySelector('#togglePassword');
                                           const password = document.querySelector('#txt_password');

                                           togglePassword.addEventListener('click', function (e) {
                                               // toggle the type attribute
                                               const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
                                               password.setAttribute('type', type);
                                               // toggle the eye slash icon
                                               this.classList.toggle('fa-eye-slash');
                                           });
                                       </script>
                                    <div class="d-flex justify-content-center">
                                        <%--LOGIN BUTTON--%>
                                        <asp:Button ID="Login" runat="server" Text="Login"  class="btn" style="background: linear-gradient(to right, #5bc0de, #9dd9eb);"  OnClick="Login_Click" /> 
                                    </div>
                                  </div>
                                 </div>
                                </div>
                            </div>
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



