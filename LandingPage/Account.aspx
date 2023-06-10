<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="WRS2big_Web.LandingPage.Account" Async="true" %>

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
    <link rel="stylesheet" href="/assets/leaflet/leaflet.css" />

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
                        <div class="md-float-material form-material">
                            <div class="text-center">
                                <img src="/images/2ndLogo.png" style="width: 500px" alt="logo.png">
                            </div>
                            <%-- MODAL FOR TERMS AND CONDITIONS--%>
                            <div class="modal fade termsCondition col-xl-10 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-md col-xl-10 col-md-15">
                                    <div class="modal-content col-xl-10 col-md-10" style="margin-left: 370px">
                                        <div id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                                <h4 class="modal-title" id="myModalLabel2">Terms and Conditions</h4>
                                                <%--exit button--%>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="col-md-18 col-sm-18 ">
                                                    <div class="x_content"  style="font-size:16px;color:black">
                                                        <center>
                                                        <asp:Label runat="server" style="font-size:16px">
                                                            This End-User License Agreement <strong>("Agreement")</strong> is a legal agreement between you (referred to as "Licensee" or "you") and <strong>[Technique Services] </strong> (referred to as "Licensor" or "we" or "us"), governing the use of
                                                            <strong> [2BiG: WRS Management System]</strong> (referred to as the "Software").
                                                        </asp:Label>
                                                        </center> <br /><br />
                                                        <asp:Label runat="server" style="">
                                                            <strong>
                                                                ACCEPTANCE OF TERMS <br />
                                                            </strong>
                                                            By installing, copying, or using the Software, you agree to be bound by the terms of this Agreement. If you do not agree to these terms, you may not use the Software <br /><br />
                                                            <strong>
                                                                GRANT OF LICENSE <br />

                                                            </strong>
                                                            Subject to the terms and conditions of this Agreement, Licensor grants you a non-exclusive, non-transferable, limited license to install and use the Software for your personal or internal business purposes, 
                                                            in accordance with the documentation and any additional usage guidelines provided by Licensor. <br /> <br />
                                                            <strong>
                                                                INTELLECTUAL PROPERTY RIGHTS <br />
                                                            </strong>
                                                            The Software is protected by copyright and other intellectual property laws. Licensor retains all rights, title, and interest in and to the Software, including any and all intellectual property rights.
                                                            This Agreement does not transfer any ownership rights to you. <br /> <br />

                                                            <strong> TERMINATION </strong> <br />
                                                            This Agreement is effective until terminated. Licensor may terminate this Agreement at any time if you fail to comply with its terms and conditions. Upon termination, you must cease all use of the Software and destroy all copies in your possession. <br /><br />
                                                            </asp:Label>

                                                            <strong>
                                                                SUBSCRIPTION TERMS <br />
                                                            </strong>
                                                            a. To fully use the Software, you must subscribe to the package offered. All packages are on a monthly basis and require payment of the corresponding fees. The available package is described on our website or within the Software. <br />
b. Some subscription package is not renewable. At the end of each monthly period, your subscription will automatically expire, and you will need to resubscribe to continue using the Software. <br />
c. If you subscribed to the free package and wish to change to a non-free package, you may do so within 5 days after the initial subscription. After this 5-day period, you may not change your package until the next subscription cycle. <br />
d. To change the package, you must follow the instructions provided within the Software or on our website. Additional fees may apply for the new package, and any unused portion of the previously subscribed package will not be refunded. <br /><br />
                                                            <strong>
                                                                UPLOAD OF VALID IDs AND BUSINESS PROOFS
                                                            </strong> <br />
a. As part of the subscription process or in connection with the use of certain features or services within the Software, you may be required to upload and provide valid identification documents (Valid IDs) and business proofs, such as a Business Permit or equivalent documentation. <br />
b. You agree that any Valid IDs and business proofs you upload must be legitimate and accurate. Falsifying or providing fraudulent documents is strictly prohibited and may result in legal consequences. <br />
c. Licensor reserves the right to verify the authenticity and validity of the uploaded documents. If any document is found to be fraudulent, Licensor may terminate your subscription and take appropriate legal action. <br /><br />
                                                            <strong>
                                                                REVENUE OWNERSHIP <br />
                                                            </strong>
a. Licensor clarifies that the super admin or the developers of the Software do not earn from the earnings of your business through the online platform. The earnings generated by your business using the Software belong solely to you, the Licensee. <br />
b. Licensor is not responsible for any financial transactions, revenue sharing, or profit distribution between you and any third parties using the Software. It is your sole responsibility to manage and distribute your business earnings. <br /><br />
                                                            <strong> 
                                                                EMPLOYEE MANAGEMENT <br />
                                                            </strong>
                                                            a. The Employee Management feature in the Software provides functionality for creating user accounts for cashiers and drivers. It does not include managing or covering the pay or salary of the employees. The Software does not handle or facilitate any salary-related transactions or calculations for your employees. <br />
b. You, as the Licensee, are solely responsible for managing the payment and salary of your employees. <br /><br />
                                                            <strong>
                                                                PRODUCT PRICING LIABILITY <br />
                                                                  </strong>
a. The prices of the products you offer through the Software are solely determined by you, the Licensee. Licensor does not control or influence the pricing of your products. <br />
b. You acknowledge and agree that you are solely responsible for setting up and maintaining the prices of your products. Any complaints or issues related to the prices of your products are your sole liability, and Licensor shall not be held responsible for any such complaints or issues. <br />

                                                          

                                                        </asp:Label>


                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-6 col-md-12" style="align-content: center; margin-left: 480px; top: 113px; left: 171px;">
                            <div class="card">
                                <div class="card-block">
                                    <div class="button-box">
                                        <center>
                                            <div id="bttn">
                                                <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                <br />
                                                <br />
                                            </div>

                                            <script>
                                                function showLogin() {
                                                    document.getElementById('login').style.display = 'block';
                                                    document.getElementById('register').style.display = 'none';
                                                }

                                                function showRegister() {
                                                    document.getElementById('login').style.display = 'none';
                                                    document.getElementById('register').style.display = 'block';
                                                }
                                            </script>

                                            <button type="button" id="btnlogin" class="togglebtn btn btn-primary waves-effect text-center active" onclick="showLogin()">Login</button>
                                            <button type="button" id="btnregister" class="togglebtn btn btn-primary waves-effect text-center active" onclick="showRegister()">Sign Up</button>

                                        </center>
                                    </div>
                                    <br />
                                    <br>
                                    <br>
                                    <%--LOG IN FIELD--%>
                                    <div id="login">
                                        <%--<div id="login" style="display:none;">--%>
                                        <div class="row">
                                            <%--                                            @*username*@--%>
                                            <div class="col">
                                                <div class="">

                                                    <label>Plese choose your Role:</label><br />
                                                    <asp:DropDownList runat="server" ID="roleType" Style="font-size: 18px" Width="150px" OnSelectedIndexChanged="roleType_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Enabled="false"></asp:ListItem>
                                                        <asp:ListItem Value="Admin"> Admin</asp:ListItem>
                                                        <asp:ListItem Value="Cashier">Cashier</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <%--<label>Email:</label>--%>
                                                    <label>ID Number:</label>
                                                    <div class="input-group">
                                                        <%-- <asp:TextBox runat="server"  type="usrname" class="form-control" id="txt_email" ValidationGroup="a"></asp:TextBox> --%>
                                                        <asp:TextBox runat="server" type="idno" TextMode="Number" class="form-control" ID="txt_idno" ValidationGroup="a"> </asp:TextBox>
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
                                                        <asp:TextBox runat="server" type="password" class="form-control" TextMode="Password" ID="txt_password" ValidationGroup="a"></asp:TextBox>

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
                                        <div class="row m-t-25 text-left">
                                            <div class="col-md-12">
                                                <asp:LinkButton runat="server" Text="Forgot Password? Reset your password." href="ResetPassword.aspx"></asp:LinkButton>

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
                                            <asp:Button ID="Login" runat="server" Text="Login" class="btn" Style="background: linear-gradient(to right, #5bc0de, #9dd9eb);" OnClick="btnLogin_Click" ValidationGroup="a" />
                                        </div>
                                    </div>
                                    <%--end of login here--%>
                                    <%--                                @*      SIGN UP     *@--%>
                                    <div id="register" style="display: none;">
                                        <%-- <div class="active" id="register" style="display:none;">--%>
                                        <%--  <div id="register">--%>
                                        <div style="background-color: #018cff; color: white" class="card card-block">
                                            <h5>PERSONAL INFORMATION</h5>
                                        </div>
                                        <div class="row ">

                                            <%--                                    @*last name*@--%>
                                            <div class="col">
                                                <div class="form-group">
                                                    <label>Last Name</label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtlname" ForeColor="Red" ErrorMessage="*" Font-Bold="true"></asp:RequiredFieldValidator>
                                                    <div class="input-group-sm">
                                                        <asp:TextBox ID="txtlname" placeholder=" enter last name" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--                                    @*first name*@--%>
                                            <div class="col">
                                                <div class="form-group">
                                                    <label>First Name</label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfname" ForeColor="Red" ErrorMessage="*" Font-Bold="true"></asp:RequiredFieldValidator>
                                                    <div class="input-group-sm">
                                                        <asp:TextBox ID="txtfname" placeholder=" enter first name" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--                                    @*middle name*@--%>
                                            <div class="col">
                                                <div class="form-group">
                                                    <label>Middle Name</label>
                                                    <div class="input-group-sm">
                                                        <asp:TextBox ID="txtmname" placeholder=" enter middle name (optional)" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <%--                                    @*birthdate*@--%>
                                            <div class="col">
                                                <div class="form-group">
                                                    <label>Birthdate</label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtbirthdate" ForeColor="Red" ErrorMessage="*" Font-Bold="true"></asp:RequiredFieldValidator>
                                                    <div class="input-group-sm">
                                                        <asp:TextBox runat="server" TextMode="Date" class="form-control" ID="txtbirthdate" Placeholder="YYYY/MM/D"></asp:TextBox>
                                                        <script>
                                                            function validateDate() {
                                                                var date = new Date(document.getElementById("txtbirthdate").value);
                                                                if ((date.getFullYear() >= 2004) || (date.getFullYear() <= 1922)) {
                                                                    alert("Please select a date in between 1922 and 2004. You must be 18 years and above!");
                                                                    document.getElementById("txtbirthdate").value = "";
                                                                }
                                                            }
                                                        </script>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <%--                                    @*Phone Number*@--%>
                                            <div class="col">
                                                <div class="form-group">
                                                    <label>Phone Number</label>
                                                    <div class="input-group-sm">
                                                        <asp:TextBox runat="server" TextMode="Number" class="form-control" Placeholder="(Format: 09XXXXXXXXX) (must be 11 digit)" ID="txtphoneNum"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegexValidator" ValidationGroup="a" runat="server" ControlToValidate="txtphoneNum" ForeColor="Red" ErrorMessage="Invalid phone number format (must be 11 digit)" ValidationExpression="^09\d{9}$"></asp:RegularExpressionValidator>

                                                        <%--                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid phone number." ControlToValidate="txtphoneNum"  ForeColor="Red" ValidationExpression="^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$" ></asp:RegularExpressionValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <%-- @*username*@--%>
                                            <%--<div class="col">
                                                        <div class="form-group">
                                                            <label>Username</label> 
                                                            <div class="input-group-sm">
                                                            <asp:TextBox runat="server" placeholder="enter username" class="form-control" ID="txtusername">
                                                            </asp:TextBox> <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^[\w.\-]{2,18}$" ControlToValidate="txtusername" ErrorMessage="Invalid Username"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                            <%-- @*email*@--%>
                                            <div class="col">
                                                <div class="form-group">
                                                    <label>Email</label>
                                                    <div class="input-group-sm">
                                                        <asp:TextBox runat="server" TextMode="Email" placeholder="example@gmail.com" class="form-control" ID="txtEmail"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <%--                                    @*password*@--%>
                                            <div class="col">
                                                <div class="form-group">
                                                    <label>Password</label>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="id_passwordreg"
                                                        ErrorMessage="Password is required." ForeColor="Red" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                    <div class="input-group-sm">
                                                        <asp:TextBox runat="server" class="form-control" TextMode="Password" placeholder=" enter password (must be 8-20 characters long and contain at least 1 letter, 1 number, and 1 special character except underscore(_)" ID="id_passwordreg"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*_#?&]{8,20}$"
                                                            runat="server" ErrorMessage="Password must be 8-20 characters long and contain at least 1 letter, 1 number, and 1 special character except underscore(_) " ForeColor="Red" ControlToValidate="id_passwordreg"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <%--                                    @*Confirm password*@--%>
                                            <div class="col">
                                                <div class="form-group">
                                                    <label>Confirm Password</label>
                                                    <asp:CompareValidator runat="server" ControlToCompare="id_passwordreg" ControlToValidate="conpass"
                                                        ErrorMessage="Passwords do not match." ForeColor="Red" Display="Dynamic">
                                                    </asp:CompareValidator>
                                                    <div class="input-group-sm">
                                                        <asp:TextBox runat="server" placeholder="confirm your password" class="form-control" TextMode="Password" ID="conpass"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--                                @*Show Password checkbox*@--%>
                                        <div class="row m-t-25 text-left">
                                            <div class="col-md-12">
                                                <div class="checkbox-fade fade-in-primary">
                                                    <label>
                                                        <input type="checkbox" value="" id="togglePasswordreg">
                                                        <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                        <span class="text-inverse">Show Password</span>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>

                                        <br />
                                        <!--REFILLING STATION-->
                                        <div style="background-color: #018cff; color: white" class="card card-block">
                                            <h5>REFILLING STATION INFORMATION</h5>
                                        </div>
                                        <div class="row">
                                            <%--                                    @*WRS NAME*@--%>
                                            <div class="col">
                                                <div class="form-group">
                                                    <label>Water Station Name</label>
                                                    <div class="input-group-sm">
                                                        <asp:TextBox runat="server" placeholder="Station Name" class="form-control" ID="txtStationName"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <%-- WRS Location--%>
                                            <div class="col">
                                                <div class="form-group">
                                                    <label>Refilling Station Address:</label>
                                                    <div class="input-group-sm">
                                                        <asp:TextBox runat="server" ID="address" placeholder="Station Address" ReadOnly="true" class="form-control"> </asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="col" id="map" style="height: 250px;">
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <asp:Label runat="server" ID="latitude"> Latitude</asp:Label>
                                                <asp:TextBox runat="server" ID="lat" ReadOnly="true">
                                                </asp:TextBox>
                                                <asp:Label runat="server" ID="longitude"> Longitude </asp:Label>
                                                <asp:TextBox runat="server" ID="long" ReadOnly="true">
                                                </asp:TextBox>
                                            </div>
                                        </div>

                                        <br />


                                        <!-- SCRIPT FOR GEOLOCATION-->

                                        <script>

                                            // Reverse geocoding function using Nominatim API
                                            function reverseGeocode(lat, long) {
                                                const url = `https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${long}`;
                                                return $.ajax({
                                                    url: url,
                                                    dataType: 'json',
                                                    type: 'GET',
                                                    success: function (data) {
                                                        const Address = data.display_name;
                                                        document.getElementById("address").value = Address;


                                                    }
                                                });
                                            }
                                            navigator.geolocation.getCurrentPosition(
                                                function (pos) {
                                                    // Save the latitude and longitude values to variables
                                                    const lat = pos.coords.latitude;
                                                    const long = pos.coords.longitude;


                                                    // Send a POST request to the server with the device registration data
                                                    const xhr = new XMLHttpRequest();
                                                    xhr.onreadystatechange = function () {
                                                        if (xhr.readyState === 4 && xhr.status === 200) {

                                                            console.log("Location Detected");

                                                        }
                                                    };
                                                    xhr.open("POST", "Account.aspx", true);
                                                    xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
                                                    xhr.send(JSON.stringify({
                                                        "lat": lat,
                                                        "long": long
                                                    }));

                                                    // Set the value of the latitude and longitude text boxes
                                                    document.getElementById("lat").value = lat;
                                                    document.getElementById("long").value = long;

                                                    // Get the address using reverse geocoding
                                                    reverseGeocode(lat, long);
                                                    // Create the map and add a tile layer
                                                    const map = L.map('map').setView([lat, long], 50); //50 is the ZOOM value
                                                    const layer = L.tileLayer('https://{s}.basemaps.cartocdn.com/light_all/{z}/{x}/{y}.png', {
                                                        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, &copy; <a href="https://cartodb.com/attributions">CartoDB</a>'
                                                    });
                                                    map.addLayer(layer);

                                                    // Add a marker to the map at your location
                                                    const marker = L.marker([lat, long]).addTo(map);
                                                    marker.bindPopup("Your location is here").openPopup();

                                                },

                                                function (err) {
                                                    // Show an error message if the user denies the location request
                                                    document.getElementById("err").innerHTML = `ERROR(${err.code}): ${err.message}`;
                                                },
                                                { enableHighAccuracy: true }
                                            );
                                        </script>
                                        <br />
                                        <%-- Upload File--%>
                                        <div style="background-color: #018cff; color: white" class="card card-block">
                                            <h7>UPLOAD THE NECESSARY DOCUMENTS</h7>
                                        </div>
                                        <div class="row" style="margin-left: 20px">
                                            <div class="col">
                                                <div class="form-group">
                                                    <br />
                                                    <h5>Valid Business Documents</h5>
                                                    <div class="input-group-sm">
                                                        <label>
                                                            Please choose from the options below.
                                                            <br />
                                                            Upload a picture of your choosen business document.
                                                            <br />
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col">
                                                <div class="form-group">
                                                    <br />
                                                    <h5>Valid ID</h5>
                                                    <div class="input-group-sm">
                                                        <label>
                                                            Please choose from the list of government issued ID below.
                                                            <br />
                                                            Please upload a clear and high quality picture of your Valid ID
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-left: 30px">
                                            <div class="col">
                                                <div class="input-group-sm">
                                                    <asp:DropDownList ID="documentDropDown" runat="server" Font-Size="12pt" Height="30px" Width="250px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value="Business Permit">Business Permit</asp:ListItem>
                                                        <asp:ListItem Value="Mayors Business Permit">Mayors Business Permit</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                            </div>
                                            <div class="col">
                                                <div class="input-group-sm">
                                                    <asp:DropDownList ID="validIDList" runat="server" Font-Size="12pt" Height="30px" Width="250px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value="Drivers License">Drivers License</asp:ListItem>
                                                        <asp:ListItem Value="Passport">Passport</asp:ListItem>
                                                        <asp:ListItem Value="Postal ID">Postal ID</asp:ListItem>
                                                        <asp:ListItem Value="Voters ID">Voters ID</asp:ListItem>
                                                        <asp:ListItem Value="National ID">National ID</asp:ListItem>
                                                        <asp:ListItem Value="SSS ID">SSS ID</asp:ListItem>
                                                        <asp:ListItem Value="GSIS ID">GSIS ID</asp:ListItem>
                                                        <asp:ListItem Value="PAGIBIG ID">PAGIBIG ID</asp:ListItem>
                                                        <asp:ListItem Value="PHILHEALTH ID">PHILHEALTH ID</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                            </div>
                                            <div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-left: 30px">
                                            <div class="col">
                                                <div class="form-group">
                                                    <div class="input-group-sm">
                                                        <asp:FileUpload ID="businessProof" runat="server" Height="90" Width="300px" AllowMultiple="true" Accept="image/*" />

                                                        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label><br />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col">
                                                <div class="form-group">
                                                    <div class="input-group-sm">
                                                        <asp:FileUpload ID="validIDUpload" runat="server" Height="90" Width="300px" AllowMultiple="true" Accept="image/*" />

                                                        <asp:Label ID="lblErrorUpload" runat="server" ForeColor="Red"></asp:Label><br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>



                                        <%--                                @*Show Password script*@--%>
                                        <script>
                                            const toggleregPassword = document.querySelector('#togglePasswordreg');
                                            const regpassword = document.querySelector('#id_passwordreg');
                                            const con_pass = document.querySelector('#conpass');
                                            // SHOW PASSWORD
                                            toggleregPassword.addEventListener('click', function (e) {
                                                // toggle the type attribute
                                                const type = regpassword.getAttribute('type') === 'password' ? 'text' : 'password';
                                                regpassword.setAttribute('type', type);
                                                // toggle the eye slash icon
                                                this.classList.toggle('fa-eye-slash');
                                            });
                                            // SHOW CONFIRM PASSWORD
                                            toggleregPassword.addEventListener('click', function (e) {
                                                // toggle the type attribute
                                                const type = con_pass.getAttribute('type') === 'password' ? 'text' : 'password';
                                                con_pass.setAttribute('type', type);
                                                // toggle the eye slash icon
                                                this.classList.toggle('fa-eye-slash');
                                            });
                                        </script>
                                        <!-- Show Password END-->
                                        <%--<div class="row m-t-25 text-left">
                                            <div class="col-md-12">
                                                <div>

                                                    <hr />
                                                    <label>
                                                        <span class="text-inverse">By signing up, you agree to 2BiG's <a href="#" data-toggle="modal" data-target=".termsCondition">Terms and Conditions </a>
                                                        </span>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>--%>

                                        <asp:LinkButton runat="server" ID="terms" data-toggle="modal" data-target=".termsCondition"> By Signing-up, you agree to our <strong> Terms and Conditions </strong></asp:LinkButton>
                                        <!-- SignUp  -->
                                        <div class="row m-t-30">
                                            <div class="col-md-12">
                                                <%-- @*buttons*@--%>
                                                <div class="d-flex justify-content-center">
                                                    <%--SIGN UP BUTTON--%>

                                                    <asp:Button ID="btnSignup" runat="server" Text="Sign Up" class="btn" Style="background-color: #018cff; color: white" OnClick="btnSignup_Click" />
                                                    <%--  <button id="btnCreateAcc" class="btn" style="background: linear-gradient(to right, #5bc0de, #9dd9eb);" >
                                        
                                                                Sign up
                                                            </button>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--End ofsignup here--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end of col-sm-12 -->
            </div>
            <!-- end of row -->
            <%-- </div>--%>
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
