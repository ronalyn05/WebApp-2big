<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminProfile.aspx.cs" Inherits="WRS2big_Web.Admin.AdminProfile" Async="true"%>
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
                                                <a href="/Admin/AdminIndex"> <i class="fa fa-home"></i> </a>
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
                                         <%--SUBSCRIPTION DETAILS--%>
                                         <div class="modal fade subscription" tabindex="-1" role="dialog" aria-hidden="true">
                                       <div class="modal-dialog modal-dialog-centered modal-md">
                                        <div class="modal-content">
                                        <form id="demo-form1" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                        <div class="modal-header">
                                        <h4 class="modal-title" id="myModalLabel1">Edit Subscription Details</h4>
                                            <%--exit button--%>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                        </div>
                                        <div class="modal-body">
                                        <div class="col-md-12 col-sm-12 ">
                                        <div class="x_content">
                                             <h6>Subscription details</h6>
                                            <hr>          
                                        </div>
                                             <div class="col-md-12 col-sm-12">
                                                   <%-- basic plan--%>
                                                <asp:Label ID="Label1" runat="server" style="color:black; font-size:20px"> Subscription Plan: </asp:Label>
                                                 <asp:Label ID="LblSubPlan" runat="server"  Width="364px" class="active btn waves-effect text-center" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift">
                                                  <%--<i class=" ti-credit-card "></i>--%>
                                                 </asp:Label>
                                                 </div>
                                         <div class="col-md-12 col-sm-12">
                                             <%--subscription start--%>
                                                  <asp:Label ID="Label5" runat="server" style="color:black; font-size:20px"> Date Subscribed: </asp:Label>
                                                  <asp:Label ID="LblDateStarted" runat="server"  Width="364px" class="active btn waves-effect text-center" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift">
                                                     <%--<i class=" ti-credit-card "></i>--%>
                                                 </asp:Label>
                                          </div>
                                         <div class="col-md-12 col-sm-12">
                                              <%--subscription end--%>
                                                 <asp:Label ID="Label6" runat="server" style="color:black; font-size:20px">Subscription End:</asp:Label>
                                                 <asp:Label ID="LblSubEnd" runat="server" class="active btn waves-effect text-center"  Width="364px" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift">
                                                    <%-- <i class=" ti-credit-card "></i>--%>
                                                 </asp:Label>                                          

                                         </div>
                                        </div>
                                        <div class="modal-footer">
                                           <%-- renew subscription button--%>
                                            <i class=" ti-credit-card ">
                                           <asp:Button ID="btnSubscription" class="active btn btn-primary waves-effect text-right" runat="server" Text="Renew Subscription" OnClick="btnSubscription_Click"/>
                                            </i>
                                        </div>
                                           <%--</form>--%>
                                         </div>
                                        </div>
                                      </div>
                                          </div>
                                        
                                         <%--REFILLING STATION MANAGEMENT --%>
                                          <div class="modal fade management" tabindex="-1" role="dialog" aria-hidden="true">
                                       <div class="modal-dialog modal-dialog-centered modal-md">
                                        <div class="modal-content">
                                        <form id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                        <div class="modal-header">
                                        <h4 class="modal-title" id="myModalLabel">Manage Refilling Station

                                        </h4>
                                            <%--exit button--%>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                        </div>
                                        <div class="modal-body">
                                        <div class="col-md-12 col-sm-12 ">
                                            <div class="col-md-12 col-sm-12">
                                                <asp:Label ID="Label4" runat="server" style="color:black; font-size:20px">Operating hours:</asp:Label>
                                                <br />
                                                <strong> From:</strong>
                                             <asp:TextBox ID="txtOperatingHrsFrom" TextMode="Time" placeholder="Set operating hours" runat="server"></asp:TextBox>
                                                <strong> To:</strong>
                                                 <asp:TextBox ID="txtOperatingHrsTo" TextMode="Time" placeholder="Set operating hours" runat="server"></asp:TextBox>
                                             
                                               <%-- <asp:TextBox ID="txtOperatingHrs" TextMode="Time" placeholder="Set operating hours" runat="server" Width="364px"></asp:TextBox>--%>
                                                 </div>
                                           
                                            <br />
                                         <div class="col-md-12 col-sm-12">
                                                 <asp:Label ID="Label3" runat="server" style="color:black; font-size:20px">Business Days:</asp:Label>
                                             <br /> 
                                             <%--<asp:Label ID="Label15" runat="server" style="color:black; font-size:20px">From:</asp:Label>--%>
                                             <strong> From:</strong>
                                              <br />
                                                   <asp:DropDownList ID="drdBusinessDaysFrom" Height="40px" runat="server" Width="364px">
                                                   <asp:ListItem Text="Monday" Value="Monday"></asp:ListItem>
                                                   <asp:ListItem Text="Tuesday" Value="Tuesday" ></asp:ListItem>
                                                       <asp:ListItem Text="Wednesday" Value="Wednesday"></asp:ListItem>
                                                   <asp:ListItem Text="Thursday" Value="Thursday" ></asp:ListItem>
                                                       <asp:ListItem Text="Friday" Value="Friday"></asp:ListItem>
                                                   <asp:ListItem Text="Saturday" Value="Saturday" ></asp:ListItem>
                                                    <asp:ListItem Text="Sunday" Value="Sunday" ></asp:ListItem>
                                                       </asp:DropDownList>
                                             <br />
                                              <br />
                                           <%-- <asp:Label ID="Label16" runat="server" style="color:black; font-size:20px">To:</asp:Label>--%>
                                              <strong> To:</strong>
                                              <br />
                                                   <asp:DropDownList ID="drdBusinessDaysTo" Height="40px" runat="server" Width="364px">
                                                  <asp:ListItem Text="Monday" Value="Monday"></asp:ListItem>
                                                   <asp:ListItem Text="Tuesday" Value="Tuesday" ></asp:ListItem>
                                                       <asp:ListItem Text="Wednesday" Value="Wednesday"></asp:ListItem>
                                                   <asp:ListItem Text="Thursday" Value="Thursday" ></asp:ListItem>
                                                       <asp:ListItem Text="Friday" Value="Friday" ></asp:ListItem>
                                                   <asp:ListItem Text="Saturday" Value="Saturday" ></asp:ListItem>
                                                    <asp:ListItem Text="Sunday" Value="Sunday" ></asp:ListItem>
                                                       </asp:DropDownList>
                                               <%--  <asp:TextBox ID="txtBusinessDaysTo" TextMode="Week" placeholder="Set business days" runat="server"></asp:TextBox>--%>
                                                                   
                                          </div>
                                        </div>
                                        </div>
                                        <div class="modal-footer">
                                           <%-- manage station button--%>
                                           <asp:Button ID="btnManageStation" class="active btn btn-primary btn-sm waves-effect text-right" runat="server" Text="SAVE" OnClick="btnManageStation_Click"/>
                                            <%--<asp:Button ID="btnEditStation" class="active btn btn-success btn-sm waves-effect text-right" runat="server" Text="Update" OnClick="btnEditStationDetails_Click"/>--%>
                                        </div>
                                           <%--</form>--%>
                                         </div>
                                        </div>
                                      </div>
                                          <%--EDIT PROFILE INFORMATION HERE --%>
                                          <div class="modal fade editprofile" tabindex="-1" role="dialog" aria-hidden="true">
                                       <div class="modal-dialog modal-dialog-centered modal-md">
                                        <div class="modal-content">
                                        <form id="demo-form2" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
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
                                                <asp:Label ID="Label8" runat="server" style="color:black; font-size:20px; display: inline-block;">Firstname:</asp:Label>
                                                <asp:TextBox ID="firstname" placeholder=" enter firstname " runat="server" Width="364px" style="display: inline-block; margin-right: 100px;"></asp:TextBox>
                                                 </div>
                                         <div class="col-md-12 col-sm-12">
                                                  <asp:Label ID="Label9" runat="server" style="color:black; font-size:20px; display: inline-block;">Middlename:</asp:Label>
                                                  <asp:TextBox ID="middlename" placeholder=" enter middlename " runat="server" Width="364px" style="display: inline-block; margin-right: 100px;"></asp:TextBox>
                                          </div>
                                         <div class="col-md-12 col-sm-12">
                                                 <asp:Label ID="Label10" runat="server" style="color:black; font-size:20px; display: inline-block;">Lastname:</asp:Label>
                                                 <asp:TextBox ID="lastname" placeholder="enter lastname" runat="server" Width="364px" style="display: inline-block; margin-right: 100px;"></asp:TextBox>                       
                                          </div>
                                             <div class="col-md-12 col-sm-12">
                                                 <asp:Label ID="Label11" runat="server" style="color:black; font-size:20px; display: inline-block;">Birthdate:</asp:Label>
                                                 <asp:TextBox ID="birthdate" placeholder="enter date of birth" TextMode="Date" runat="server" Width="364px" style=" display: inline-block; margin-right: 100px;"></asp:TextBox>                       
                                          </div>
                                             <div class="col-md-12 col-sm-12">
                                                 <asp:Label ID="Label12" runat="server" style="color:black; font-size:20px; display: inline-block;">Phone number:</asp:Label>
                                                 <asp:TextBox ID="contactnum" placeholder="enter contact number" runat="server" Width="364px" style="display: inline-block; margin-right: 100px;"></asp:TextBox>                       
                                          </div>
                                             <div class="col-md-12 col-sm-12">
                                                 <asp:Label ID="Label13" runat="server" style="color:black; font-size:20px; display: inline-block;">Email address:</asp:Label>
                                                 <asp:TextBox ID="email" placeholder="enter email" runat="server" Width="364px" style="display: inline-block; margin-right: 100px;"></asp:TextBox>                       
                                          </div>

                                        </div>
                                        </div>
                                        <div class="modal-footer">
                                           <%-- manage personal information button--%>
                                           <asp:Button ID="btnEdit" class="active btn btn-primary btn-sm waves-effect text-right" runat="server" Text="SAVE" OnClick="btnEditProfile_Click"/>
                                           
                                        </div>
                                           <%--</form>--%>
                                         </div>
                                        </div>
                                      </div> <%--Edit profile information ends here--%>
                                    <!-- Page-body start -->
                                            <div class="row">
                                                <div class="col-xl-4 col-md-12 " >
                                                <div class="card " style="background-color:	#deffea">
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
                                                     <asp:ImageButton ID="ImageButton_new" class="img-100 img-radius" style="width:200px" runat="server" /> <%--src="/images/rhea.png"--%>
                                                          <br />
                                                          <asp:Label ID="Lbl_user" runat="server" style="color:black; font-size:16px"></asp:Label>
                                                          <br />
                                                           <br />
                                                      </center>
                                                          <center> 
                                                              <%--file upload--%>
                                                              <asp:FileUpload ID="imgProfile" runat="server" Font-Size="Medium" Height="38px" Width="301px"  />
                                                               <br />
                                                              <asp:Button ID="profileBtn" runat="server" Text="Update Profile" style="background-color:paleturquoise; color:black" OnClick="profileBtn_Click" />
                                                          </center>
                                                  </div>
                                                    <hr />

                                                    <div class="nav-tabs col-xl-8 ">
                                                            <div class="nav nav-tabs" style="font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;color:black">
                                                                 <%-- Update button--%>
                                                            <%--<button type="button" class="active btn waves-effect text-left" data-toggle="modal" data-target=".edit" style="background-color:transparent;font-size:20px;"><i class="fa fa-edit"></i> Update Profile</button>--%>
                                                                 <%-- Subscritption button--%>
                                                            <button type="button" class="active btn waves-effect text-center" data-toggle="modal" data-target=".subscription" style="background-color:transparent;font-size:20px;"><i class=" ti-settings"></i>Subscription</button> 
                                                                <br />
                                                               <%-- REFILLING STATION MANAGEMENT MODAL --%>
                                                                 <button type="button" class="active btn waves-effect text-center" data-toggle="modal" data-target=".management" style="background-color:transparent;font-size:20px;"><i class="ti-marker"></i>Manage Refilling Station</button> 
                                                                <br />
                                                                <%-- EDIT PROFILE INFORMATION MODAL --%>
                                                                <button type="button" class="active btn waves-effect text-center" data-toggle="modal" data-target=".editprofile" style="background-color:transparent;font-size:20px;"><i class="ti-marker"></i>Edit Profile Information</button> 
                                                               
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
                                                                  <form class="form" action="##" method="POST" id="ProfilePage">
                                                                      <div class="form-group">                
                                                                        <div class="col-xs-12" style="font-size:16px">
                                                                          <%--  DISPLAY DATA STARTS HERE--%>
                                                                            <br /> 
                                                                            <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Id Number:</h5>
                                                                            <asp:Label ID="Lbl_Idno" class="btn waves-effect text-center" style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                            <br />
                                                                           <%-- <asp:Label ID="Lbl_Idno" class=" btn waves-effect text-center" 
                                                                                         style="width:700px" runat="server"></asp:Label>--%>
                                                                          <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Firstname:</h5>
                                                                            <asp:Label ID="lblfname" class="btn waves-effect text-center" style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>

                                                                             <%--<asp:TextBox ID="txtfname" class="btn waves-effect text-center" style="width:364px; display: inline-block; margin-right: 100px;" runat="server">
                                                                             </asp:TextBox> --%>
                                                                            <br />
                                                                            <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Middlename:</h5> 
                                                                             <asp:Label ID="lblmname" class="btn waves-effect text-center" style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                            <%-- <asp:TextBox ID="txtmname" class="btn waves-effect text-center" style="width:364px; display: inline-block; margin-right: 100px;" runat="server">
                                                                             </asp:TextBox> --%>
                                                                            <br />
                                                                            <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Lastname:</h5> 
                                                                          <asp:Label ID="lblLname" class="btn waves-effect text-center" style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                           <%--  <asp:TextBox ID="txtlname" class="btn waves-effect text-center" style="width:364px; display: inline-block; margin-right: 100px;" runat="server">
                                                                             </asp:TextBox> --%>
                                                                            <br />
                                                                            <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Birthdate: </h5>
                                                                             <asp:Label ID="lbldob" class="btn waves-effect text-center" style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                            <%--<asp:TextBox ID="txtdob" TextMode="Date" class="btn waves-effect text-center" style="width:364px; display: inline-block; margin-right: 100px;" runat="server">
                                                                             </asp:TextBox> --%>
                                                                            <br />
                                                                             <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Contact number:  </h5>
                                                                             <asp:Label ID="lblcontactnum" class="btn waves-effect text-center" style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                           <%-- <asp:TextBox ID="txtcontact" TextMode="Phone" class="btn waves-effect text-center" style="width:364px; display: inline-block; margin-right: 100px;" runat="server">
                                                                             </asp:TextBox> --%>
                                                                            <br />
                                                                              <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Email Address: </h5>
                                                                             <asp:Label ID="lblemail" class="btn waves-effect text-center" style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                            <%--<asp:TextBox ID="txtemail" TextMode="Email" class="btn waves-effect text-center" style="width:364px; display: inline-block; margin-right: 100px;" runat="server">
                                                                             </asp:TextBox> --%>
                                                                            <br />
                                                                            <hr />
                                                                            <%--<asp:Button ID="btnEdit" class="btn btn-primary" style="background-color:#bae1ff; font-size:18px; color:black; font-family:Bahnschrift; margin-right: 800px;"
                                                                                runat="server" Text="Update Information" OnClick="btnUpdate_Click"/>--%>
                                                                            <%--<asp:Button ID="btnEdit" class="active btn btn-primary btn-sm waves-effect text-right"  style="margin-right: 800px;" runat="server" Text="Edit Info" OnClick="btnEditProfile_Click"/>
                                           --%>
                                                                               <asp:Label ID="Label14" runat="server" Font-Bold="true" Font-Size="Large" Text="STATION DETAILS"></asp:Label>
                                                                             <hr />
                                                                            <br /> 
                                                                            <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Station Name: </h5>
                                                                            <asp:Label ID="lblStationName" class=" btn btn-round waves-effect text-center" 
                                                                                         style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                            <br />
                                                                               <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Address:  </h5>
                                                                            <asp:Label ID="lblAddress" class=" btn btn-round waves-effect text-center" 
                                                                                        style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                            <br />
                                                                          <%-- <asp:TextBox ID="txtaddress" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server">
                                                                             </asp:TextBox> 
                                                                            <br />--%>
                                                                            <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Operating Hours: </h5>
                                                                             <asp:Label ID="lblOperatingHours" class="btn waves-effect text-center" style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                            <%--<asp:TextBox ID="txtOperatngHours" class=" btn btn-round waves-effect text-center" 
                                                                                        style="width:364px; display: inline-block; margin-right: 100px;" runat="server">
                                                                             </asp:TextBox> --%>
                                                                            <br />
                                                                            <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Business Days: </h5>
                                                                             <asp:Label ID="lblBusinessday" class="btn waves-effect text-center" style="display: inline-block; margin-right: 100px;font-size: 20px; vertical-align: middle;" runat="server"></asp:Label>
                                                                           <%-- <asp:TextBox ID="txtBssnessDay" class=" btn btn-round waves-effect text-center" 
                                                                                        style="width:364px; display: inline-block; margin-right: 100px;" runat="server">
                                                                             </asp:TextBox> --%>
                                                                            <br />
                                                                            <h5 style="display: inline-block; font-size: 20px; vertical-align: middle;">Status: </h5>
                                                                             <asp:Label ID="lblstatus" class="btn waves-effect text-center" style="display: inline-block; margin-right: 100px; font-size: 20px; vertical-align: middle; " runat="server"></asp:Label>
                                                                           <%-- <asp:TextBox ID="txt_Status" class=" btn btn-round waves-effect text-center" 
                                                                                        style="width:364px; display: inline-block; margin-right: 100px;" runat="server">
                                                                             </asp:TextBox>--%>
                                                                            <br />
                                                                            <br />
                                                                            <hr />
                                                                          </div>
                                                                      </div>
                                                                </form>
                                                             </div><!--/SETTINGS TAB-pane-->
                                                             <div class="tab-pane" id="Settings">
               
                                                               <h6>Profile Settings and Privacy</h6>
               
                                                               <hr> <center>
                                                                   <button class="active btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:20px;"><a data-toggle="tab" href="#ManageProfile"><i class=" ti-key "></i> Change Password</a></button> 
                                                                    </center>
                                                                     

               
                                                             </div><!--/ SETTINGS tab-pane-->

         </div><!--/tab-content-->
                                                           
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
