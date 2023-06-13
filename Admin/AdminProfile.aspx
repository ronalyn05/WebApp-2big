<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminProfile.aspx.cs" Inherits="WRS2big_Web.Admin.AdminProfile" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                                            <h5 class="m-b-10">ADMIN PROFILE </h5>
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
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="page-wrapper">
                                    <div class="right_col" role="main">
                                        <div class="">
                                            <div class="clearfix">
                                                <%-- MODAL FOR CHANGE PACKAGE--%>
                                                <div class="modal fade changePackage" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md">
                                                        <div class="modal-content">
                                                            <div id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="changeModal"></h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">
                                                                            <%-- <div class="item form-group">--%>
                                                                            <h4 style="color: black; font-family: Bahnschrift" runat="server" id="changeModalTitle">CHANGE PACKAGE</h4>
                                                                            <br />

                                                                            <div class="col-md-12 col-sm-12" style="font-size: 20px">
                                                                                <h4 style="font-size: 16px; color: black">Current Subscription: </h4>
                                                                                <br />
                                                                                <center>
                                                                                    <h4 style="font-size: 16px; color: black;" id="currentSubscription" runat="server"></h4>
                                                                                    <br />
                                                                                </center>
                                                                                <h4 style="font-size: 16px; color: black">Expiration: </h4>
                                                                                <br />
                                                                                <center>
                                                                                    <h4 style="font-size: 16px; color: black;" id="currentExpiration" runat="server"></h4>
                                                                                    <br />
                                                                                </center>
                                                                                <hr />
                                                                                <br />
                                                                                <p style="font-size: 16px; color: black;">
                                                                                    Are you sure you want to change your current package?
                                                                                    <br />
                                                                                    Important Note: If you change your package, the amount you paid for you current subscription will not be refunded.
                                                                                </p>

                                                                            </div>
                                                                            <br />

                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <asp:Button ID="cancelButton" runat="server" Text="Cancel" class="btn btn-primary btn-sm" data-dismiss="modal" />
                                                                        <%-- CONFIRM  BUTTON--%>
                                                                        <asp:Button ID="confirmChangePackage" runat="server" Text="Confirm" class="btn btn-primary btn-sm" OnClick="confirmChangePackage_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--SUBSCRIPTION DETAILS--%>
                                                <div class="modal fade subscription" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md">
                                                        <div class="modal-content">
                                                            <div id="subscriptionDetails" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="myModalLabel1">Edit Subscription Details</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">

                                                                            <hr>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="subscriptionLabel" Style="color: red; font-size: 22px"></asp:Label>
                                                                        <div class="col-md-12 col-sm-12">
                                                                            <%-- basic plan--%>
                                                                            <asp:Label ID="Label1" runat="server" Style="color: black; font-size: 20px"> Subscription Plan: </asp:Label>
                                                                            <asp:Label ID="LblSubPlan" runat="server" Width="364px" class="active btn waves-effect text-center" Style="background-color: #bae1ff; font-size: 20px; color: black; font-family: Bahnschrift">
                                                  <%--<i class=" ti-credit-card "></i>--%>
                                                                            </asp:Label>
                                                                        </div>
                                                                        <div class="col-md-12 col-sm-12">
                                                                            <%--subscription start--%>
                                                                            <asp:Label ID="Label5" runat="server" Style="color: black; font-size: 20px"> Date Subscribed: </asp:Label>
                                                                            <asp:Label ID="LblDateStarted" runat="server" Width="364px" class="active btn waves-effect text-center" Style="background-color: #bae1ff; font-size: 20px; color: black; font-family: Bahnschrift">
                                                     <%--<i class=" ti-credit-card "></i>--%>
                                                                            </asp:Label>
                                                                        </div>
                                                                        <div class="col-md-12 col-sm-12">
                                                                            <%--subscription end--%>
                                                                            <asp:Label ID="Label6" runat="server" Style="color: black; font-size: 20px">Subscription End:</asp:Label>
                                                                            <asp:Label ID="LblSubEnd" runat="server" class="active btn waves-effect text-center" Width="364px" Style="background-color: #bae1ff; font-size: 20px; color: black; font-family: Bahnschrift">
                                                    <%-- <i class=" ti-credit-card "></i>--%>
                                                                            </asp:Label>

                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    <div class="modal-footer">
                                                                        <%-- renew subscription button--%>

                                                                        <asp:LinkButton ID="subscribeBTN" href="SubscriptionPackages.aspx" class="active btn btn-primary waves-effect text-right" runat="server" Style="font-size: 18px;"> Subscribe now </asp:LinkButton>
                                                                        <asp:LinkButton ID="renewBTN" OnClick="renewBTN_Click" class="active btn btn-primary waves-effect text-right" runat="server" Style="font-size: 18px;">Renew </asp:LinkButton>
                                                                        <button id="changePackage" class="active btn btn-primary waves-effect text-right" runat="server" data-toggle="modal" data-target=".changePackage" data-dismiss="modal" style="font-size: 18px;">Change Package </button>
                                                                    </div>

                                                                    <%--</form>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--REFILLING STATION MANAGEMENT --%>
                                                <div class="modal fade management" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md">
                                                        <div class="modal-content">
                                                            <div id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="myModalLabel">Manage Refilling Station

                                                                    </h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="col-md-12 col-sm-12">
                                                                            <asp:Label ID="Label4" runat="server" Style="color: black; font-size: 20px">Operating hours:</asp:Label>
                                                                            <br />
                                                                            <strong>From:</strong>
                                                                            <asp:TextBox ID="txtOperatingHrsFrom" TextMode="Time" placeholder="Set operating hours" runat="server"></asp:TextBox>
                                                                            <strong>To:</strong>
                                                                            <asp:TextBox ID="txtOperatingHrsTo" TextMode="Time" placeholder="Set operating hours" runat="server"></asp:TextBox>

                                                                            <%-- <asp:TextBox ID="txtOperatingHrs" TextMode="Time" placeholder="Set operating hours" runat="server" Width="364px"></asp:TextBox>--%>
                                                                        </div>

                                                                        <br />
                                                                        <div class="col-md-12 col-sm-12">
                                                                            <asp:Label ID="Label3" runat="server" Style="color: black; font-size: 20px">Business Days:</asp:Label>
                                                                            <br />
                                                                            <%--<asp:Label ID="Label15" runat="server" style="color:black; font-size:20px">From:</asp:Label>--%>
                                                                            <strong>From:</strong>
                                                                            <br />
                                                                            <asp:DropDownList ID="drdBusinessDaysFrom" Height="40px" runat="server" placeholder="Business Days" Width="364px">
                                                                                <asp:ListItem Text="Monday" Value="Monday"></asp:ListItem>
                                                                                <asp:ListItem Text="Tuesday" Value="Tuesday"></asp:ListItem>
                                                                                <asp:ListItem Text="Wednesday" Value="Wednesday"></asp:ListItem>
                                                                                <asp:ListItem Text="Thursday" Value="Thursday"></asp:ListItem>
                                                                                <asp:ListItem Text="Friday" Value="Friday"></asp:ListItem>
                                                                                <asp:ListItem Text="Saturday" Value="Saturday"></asp:ListItem>
                                                                                <asp:ListItem Text="Sunday" Value="Sunday"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <br />
                                                                            <br />
                                                                            <%-- <asp:Label ID="Label16" runat="server" style="color:black; font-size:20px">To:</asp:Label>--%>
                                                                            <strong>To:</strong>
                                                                            <br />
                                                                            <asp:DropDownList ID="drdBusinessDaysTo" Height="40px" runat="server" placeholder="Business Days" Width="364px">
                                                                                <asp:ListItem Text="Monday" Value="Monday"></asp:ListItem>
                                                                                <asp:ListItem Text="Tuesday" Value="Tuesday"></asp:ListItem>
                                                                                <asp:ListItem Text="Wednesday" Value="Wednesday"></asp:ListItem>
                                                                                <asp:ListItem Text="Thursday" Value="Thursday"></asp:ListItem>
                                                                                <asp:ListItem Text="Friday" Value="Friday"></asp:ListItem>
                                                                                <asp:ListItem Text="Saturday" Value="Saturday"></asp:ListItem>
                                                                                <asp:ListItem Text="Sunday" Value="Sunday"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <%--  <asp:TextBox ID="txtBusinessDaysTo" TextMode="Week" placeholder="Set business days" runat="server"></asp:TextBox>--%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <%-- manage station button--%>
                                                                    <asp:Button ID="btnManageStation" class="active btn btn-primary btn-sm waves-effect text-right" runat="server" Text="SAVE" OnClick="btnManageStation_Click" />
                                                                    <%--<asp:Button ID="btnEditStation" class="active btn btn-success btn-sm waves-effect text-right" runat="server" Text="Update" OnClick="btnEditStationDetails_Click"/>--%>
                                                                </div>
                                                                <%--</form>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--MODAL FOR UPLOADED DOCUMENTS-->
                                                <div class="modal fade uploadedDocuments col-md-12 col-md-12" id="subscribedModal" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md col-md-10 col-md-15">
                                                        <div class="modal-content col-xl-10 col-md-10" style="margin-left: 370px">
                                                            <div id="demo-form4" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title">Uploaded Business Proof and Valid ID</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" id="closeuploadedModal"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">
                                                                            
                        
                                                                                <asp:Label runat="server" Style="font-size: 16px; color: red; margin-left: 700px" ID="noUploaded"> </asp:Label>
                                                                                <asp:Repeater ID="uploadedImages" runat="server">

                                                                                    <ItemTemplate>
                                                                                        <a href='<%# Container.DataItem %>'>
                                                                                            

                                                                                                <img id="ImageItem" runat="server" src='<%# Container.DataItem %>' width="350" height="350" style="border-color: black" />
                                                                                          

                                                                                        </a>

                                                                                    </ItemTemplate>

                                                                                </asp:Repeater>
                                                                                <hr />
                                                                                
                                                                          
                                                                             <button type="button" id="newUploadBtn" runat="server" class="active btn waves-effect text-center"  data-dismiss="modal" data-toggle="modal" data-target=".uploadNew" style="background-color: transparent; font-size: 20px;"><i class="ti-clip"></i>Upload new documents</button>
                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <%--  BUTTON ADD PAYMENT METHOD--%>
                                                                        <%--                                                                        <asp:Button ID="paymentButton" runat="server" Text="Confirm" class="btn btn-primary btn-sm" />--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            <!--MODAL FOR UPLOADED DOCUMENTS-->
                                                <%--SUBSCRIPTION DETAILS--%>
                                                <div class="modal fade uploadNew" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md">
                                                        <div class="modal-content">
                                                            <div data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="myModalLabel6">Upload new Business Proofs and Valid ID</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content"> 
                                                                    <center>
                                                                    <%--file upload--%>
                                                                         <asp:Label ID="label" runat="server" ForeColor="Black">Upload Business Proofs:</asp:Label>
                                                                    <asp:FileUpload ID="uploadBussProofs" runat="server" Font-Size="Medium" Height="38px" Width="301px" AllowMultiple="true" Accept="image/*" />
                                                                    <br /> <br />
                                                                  <asp:Label ID="label2" runat="server" ForeColor="Black">Upload Valid ID:</asp:Label> <br />
                                                                    <asp:FileUpload ID="uploadnewValid" runat="server" Font-Size="Medium" Height="38px" Width="301px" AllowMultiple="true" Accept="image/*" />
                                                                    <br />
                                                                </center>
                                                                        </div>

                                                                    </div>
                                                                    <br />
                                                                    <div class="modal-footer">
                                                                      <asp:Button ID="uploadbtnNewproofs" runat="server" Text="Upload Now" class="btn btn-primary btn-sm" OnClick="uploadbtnNewproofs_Click"/>
                                                                    </div>

                                                                   
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--EDIT PROFILE INFORMATION HERE --%>
                                                <div class="modal fade editprofile" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md">
                                                        <div class="modal-content">
                                                            <div id="demo-form2" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="myModalLabel2">Profile Information</h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">
                                                                            <%-- edit profile information starts here--%>
                                                                            <h6>Edit Profile Information</h6>
                                                                            <hr>
                                                                        </div>
                                                                        <div class="col-md-12 col-sm-12">
                                                                            <asp:Label ID="Label8" runat="server" Style="color: black; font-size: 20px; display: inline-block;">Firstname:</asp:Label>
                                                                            <asp:TextBox ID="firstname" placeholder=" enter firstname " runat="server" Width="364px" Style="display: inline-block; margin-right: 100px;"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-12 col-sm-12">
                                                                            <asp:Label ID="Label9" runat="server" Style="color: black; font-size: 20px; display: inline-block;">Middlename:</asp:Label>
                                                                            <asp:TextBox ID="middlename" placeholder=" enter middlename " runat="server" Width="364px" Style="display: inline-block; margin-right: 100px;"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-12 col-sm-12">
                                                                            <asp:Label ID="Label10" runat="server" Style="color: black; font-size: 20px; display: inline-block;">Lastname:</asp:Label>
                                                                            <asp:TextBox ID="lastname" placeholder="enter lastname" runat="server" Width="364px" Style="display: inline-block; margin-right: 100px;"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-12 col-sm-12">
                                                                            <asp:Label ID="Label11" runat="server" Style="color: black; font-size: 20px; display: inline-block;">Birthdate:</asp:Label>
                                                                            <asp:TextBox ID="birthdate" placeholder="enter date of birth" TextMode="Date" runat="server" Width="364px" Style="display: inline-block; margin-right: 100px;"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-12 col-sm-12">
                                                                            <asp:Label ID="Label12" runat="server" Style="color: black; font-size: 20px; display: inline-block;">Phone number:</asp:Label>
                                                                            <asp:TextBox ID="contactnum" placeholder="enter contact number" runat="server" Width="364px" Style="display: inline-block; margin-right: 100px;"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-12 col-sm-12">
                                                                            <asp:Label ID="Label13" runat="server" Style="color: black; font-size: 20px; display: inline-block;">Email address:</asp:Label>
                                                                            <asp:TextBox ID="email" placeholder="enter email" runat="server" Width="364px" Style="display: inline-block; margin-right: 100px;"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <%-- manage personal information button--%>
                                                                    <asp:Button ID="btnEdit" class="active btn btn-primary btn-sm waves-effect text-right" runat="server" Text="SAVE" OnClick="btnEditProfile_Click" />

                                                                </div>
                                                                <%--</form>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--Edit profile information ends here--%>
                                                <!-- Page-body start -->
                                                <asp:Label runat="server" ID="warningMsg" Style="font-size: 18px; color: black"></asp:Label><br />
                                                <center>
                                                    <asp:Label runat="server" ID="declinereason" Style="font-size: 18px; color: red"></asp:Label><br />
                                                </center>
                                                <br />
                                                <div class="row">

                                                    <div class="col-xl-4 col-md-12 ">
                                                        <div class="card " style="background-color: white">
                                                            <div class="card-header">
                                                                <h5>My Profile</h5>
                                                                <div class="card-header-right">
                                                                    <ul class="list-unstyled card-option">
                                                                        <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                                        <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                        <li><i class="fa fa-minus minimize-card"></i></li>
                                                                        <li><i class="fa fa-refresh reload-card"></i></li>
                                                                        <li><i class="fa fa-trash close-card"></i></li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                            <div class="main-menu-header">
                                                                <center>
                                                                    <%--image container--%>
                                                                    <br />
                                                                    <br />
                                                                    <asp:Label runat="server" ID="uploadinstruction" Style="font-size: 16px; color: black"></asp:Label>
                                                                    <asp:ImageButton ID="ImageButton_new" class="img-100 img-radius" Style="width: 200px" runat="server" />
                                                                    <%--src="/images/rhea.png"--%>
                                                                    <br />
                                                                    <asp:Label ID="Lbl_user" runat="server" Style="color: black; font-size: 16px"></asp:Label>
                                                                    <br />
                                                                    <br />
                                                                </center>
                                                                <center>
                                                                    <%--file upload--%>
                                                                    <asp:FileUpload ID="imgProfile" runat="server" Font-Size="Medium" Height="38px" Width="301px" />
                                                                    <br />
                                                                    <asp:Button ID="profileBtn" runat="server" Text="Update Profile" Style="background-color: paleturquoise; color: black" OnClick="profileBtn_Click" />
                                                                </center>
                                                            </div>
                                                            <hr />

                                                            <div class="nav-tabs col-xl-8 ">
                                                                <div class="nav nav-tabs" style="font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; color: black">
                                                                    <%-- Update button--%>
                                                                    <%--<button type="button" class="active btn waves-effect text-left" data-toggle="modal" data-target=".edit" style="background-color:transparent;font-size:20px;"><i class="fa fa-edit"></i> Update Profile</button>--%>
                                                                    <%-- Subscritption button--%>
                                                                    <button type="button" class="active btn waves-effect text-center" data-toggle="modal" data-target=".subscription" style="background-color: transparent; font-size: 20px;"><i class=" ti-settings"></i>Subscription</button>
                                                                    <br />
                                                                    <%-- REFILLING STATION MANAGEMENT MODAL --%>
                                                                    <button type="button" class="active btn waves-effect text-center" data-toggle="modal" data-target=".management" style="background-color: transparent; font-size: 20px;"><i class="ti-marker"></i>Manage Refilling Station</button>
                                                                    <br />
                                                                    <%-- EDIT PROFILE INFORMATION MODAL --%>
                                                                    <button type="button" class="active btn waves-effect text-center" data-toggle="modal" data-target=".editprofile" style="background-color: transparent; font-size: 20px;"><i class="ti-marker"></i>Edit Profile Information</button>
                                                                    <br />
                                                                    <button type="button" class="active btn waves-effect text-center" data-toggle="modal" data-target=".uploadedDocuments" style="background-color: transparent; font-size: 20px;"><i class="ti-clip"></i>Uploaded Documents</button>
                                                                    <br />
                                                                    <br />
                                                                    <%-- REFILLING STATION MANAGEMENT --%>
                                                                    <%-- <button type="button" class="active btn waves-effect text-center" data-toggle="modal" data-target=".editInfo" style="background-color:transparent;font-size:20px;"><i class="ti-marker"></i>Edit Personal Info</button> --%>
                                                                    <%--logout button--%>
                                                                    <%-- <i class="fa fa-undo">
                                                           <asp:Button ID="btnLogout" class="active btn waves-effect text-right" style="background-color:transparent;font-size:20px;" runat="server" Text="Logout" OnClick="btnLogout_Click"/>
                                                            </i>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <%--PROFILE MANAGEMENT--%>
                                                    <div class="col-xl-8 col-md-12">
                                                        <div class="card">
                                                            <div class="card-header">
                                                                <br />
                                                                <%-- <h5>PROFILE MANAGEMENT</h5>--%>
                                                                <asp:Label ID="Label7" runat="server" Font-Bold="true" Font-Size="Large" Text="PERSONAL INFORMATION"></asp:Label>
                                                                <div class="card-header-right">
                                                                    <ul class="list-unstyled card-option">
                                                                        <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                                        <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                        <li><i class="fa fa-minus minimize-card"></i></li>
                                                                        <li><i class="fa fa-refresh reload-card"></i></li>
                                                                        <li><i class="fa fa-trash close-card"></i></li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                            <div class="card-block">
                                                                <div class="table-responsive">
                                                                    <div class="tab-content">
                                                                        <div class="tab-pane active" id="Profile">
                                                                            <%-- <h5>Personal Information</h5>
                                                                <hr>--%>
                                                                            <div class="form" action="##" method="POST" id="ProfilePage">
                                                                                <div class="form-group">
                                                                                    <div class="col-xs-12" style="font-size: 16px">
                                                                                        <%--  DISPLAY DATA STARTS HERE--%>
                                                                                        <br />
                                                                                        <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Id Number:</h5>
                                                                                        <asp:Label ID="Lbl_Idno" class="btn waves-effect text-center" Style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                                        <br />
                                                                                        <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Firstname:</h5>
                                                                                        <asp:Label ID="lblfname" class="btn waves-effect text-center" Style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                                        <br />
                                                                                        <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Middlename:</h5>
                                                                                        <asp:Label ID="lblmname" class="btn waves-effect text-center" Style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                                        <br />
                                                                                        <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Lastname:</h5>
                                                                                        <asp:Label ID="lblLname" class="btn waves-effect text-center" Style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                                        <br />
                                                                                        <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Birthdate: </h5>
                                                                                        <asp:Label ID="lbldob" class="btn waves-effect text-center" Style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                                        <br />
                                                                                        <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Contact number:  </h5>
                                                                                        <asp:Label ID="lblcontactnum" class="btn waves-effect text-center" Style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                                        <br />
                                                                                        <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Email Address: </h5>
                                                                                        <asp:Label ID="lblemail" class="btn waves-effect text-center" Style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                                        <br />
                                                                                        <hr />
                                                                                        <asp:Label ID="Label14" runat="server" Font-Bold="true" Font-Size="Large" Text="STATION DETAILS"></asp:Label>
                                                                                        <hr />
                                                                                        <br />
                                                                                        <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Station Name: </h5>
                                                                                        <asp:Label ID="lblStationName" class=" btn btn-round waves-effect text-center"
                                                                                            Style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                                        <br />
                                                                                        <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Address:  </h5>
                                                                                        <asp:Label ID="lblAddress" class=" btn btn-round waves-effect text-center"
                                                                                            Style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                                        <br />
                                                                                        <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Operating Hours: </h5>
                                                                                        <asp:Label ID="lblOperatingHours" class="btn waves-effect text-center" Style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                                        <br />
                                                                                        <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Business Days: </h5>
                                                                                        <asp:Label ID="lblBusinessday" class="btn waves-effect text-center" Style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                                        <br />
                                                                                        <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Status: </h5>
                                                                                        <asp:Label ID="lblstatus" class="btn waves-effect text-center" Style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                                        <br />
                                                                                        <br />
                                                                                        <hr />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                    </div>
                                                                    <!--/tab-content-->

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
