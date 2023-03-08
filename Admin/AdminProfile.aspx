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
                                           <%-- basic plan--%>
                                        <h4 style="color:black;font-family:Bahnschrift"> Subscription Plan:</h4>
                                             <center>
                                                 <asp:Label ID="LblSubPlan" runat="server" class="active btn waves-effect text-center" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift">
                                                     <%--<i class=" ti-credit-card "></i>--%>
                                                 </asp:Label>
                                               <%-- <p class="active btn waves-effect text-center" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift"> BASIC PLAN</p> --%>
                                             </center>
                                        </div>
                                         <div class="col-md-12 col-sm-12">
                                             <%--date subscribed--%>
                                        <h4 style="color:black;font-family:Bahnschrift"> Date Subscribed:</h4>
                                              <center>
                                                  <asp:Label ID="LblDateStarted" runat="server" class="active btn waves-effect text-center" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift">
                                                     <i class=" ti-credit-card "></i>
                                                 </asp:Label>
                                                 <%--<p class="active btn waves-effect text-center" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift"><i class=" ti-credit-card "></i> December 10, 2022</p> --%>
                                              </center> 
                                          </div>
                                         <div class="col-md-12 col-sm-12">
                                             <%--subscription end--%>
                                          <h4 style="color:black;font-family:Bahnschrift">Subscription End:</h4>
                                                                 <center>
                                                                     <asp:Label ID="LblSubEnd" runat="server" class="active btn waves-effect text-center" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift">
                                                     <i class=" ti-credit-card "></i>
                                                 </asp:Label>
                                                                          <%-- <p class="active btn waves-effect text-center" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift"><i class=" ti-credit-card "></i> March 10, 2022</p> --%>
                                                                 </center>
                                          </div>
                                        </div>
                                         <%-- </div>--%>
                                        </div>
                                        <div class="modal-footer">
                                           <%-- renew subscription button--%>
                                            <i class=" ti-credit-card ">
                                           <asp:Button ID="btnSubscription" class="active btn waves-effect text-right" style="background-color:#ffffe3;font-size:20px;color:black;font-family:Bahnschrift" runat="server" Text="Renew Subscription" ValidationGroup="a" OnClick="btnSubscription_Click"/>
                                            </i>
                                        </div>
                                           <%--</form>--%>
                                         </div>
                                        </div>
                                      </div>
                                         <%--REFILLING STATION MANAGEMENT --%>
                                          <div class="modal fade management" tabindex="-1" role="dialog" aria-hidden="true">
                                       <div class="modal-dialog modal-dialog-centered modal-md">
                                        <div class="modal-content">
                                        <form id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                        <div class="modal-header">
                                        <h4 class="modal-title" id="myModalLabel">Refilling Station</h4>
                                            <%--exit button--%>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                        </div>
                                        <div class="modal-body">
                                        <div class="col-md-12 col-sm-12 ">
                                        <div class="x_content">
                                             <h6>Refilling Station details</h6>
                                            <hr>                        
                                           <%-- basic plan--%>
                                        <h4 style="color:black;font-family:Bahnschrift"> Operating hours:</h4>
                                             <center>
                                                <asp:TextBox ID="txtOperatingHrs" placeholder="Set operating hours" runat="server"></asp:TextBox>
                                               </center>
                                        </div>
                                         <div class="col-md-12 col-sm-12">
                                             <%--<%--date subscribed--%>
                                        <h4 style="color:black;font-family:bahnschrift">Status:</h4>
                                              <center>
                                                 <%-- <asp:Label ID="Label2" runat="server" class="active btn waves-effect text-center" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift">
                                                 Status:
                                                  </asp:Label>--%>
                                                   <asp:DropDownList ID="operatingHrsStatus" runat="server" >
                                                   <asp:ListItem Text="Open" Value="open" Selected="True"></asp:ListItem>
                                                   <asp:ListItem Text="Close" Value="close" ></asp:ListItem>
                                               </asp:DropDownList>
                                                 <%--<p class="active btn waves-effect text-center" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift"><i class=" ti-credit-card "></i> December 10, 2022</p> --%>
                                              </center> 
                                          </div>
                                         <div class="col-md-12 col-sm-12">
                                             <%--subscription end--%>
                                          <h4 style="color:black;font-family:Bahnschrift">Business Days:</h4>
                                                                 <center>
                                                             <%--        <asp:Label ID="Label3" runat="server" class="active btn waves-effect text-center" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift">
                                                      Business Days:
                                                 </asp:Label>--%>
                                                                     <asp:TextBox ID="txtBusinessDays" placeholder="Set business days" runat="server"></asp:TextBox>
                                                                          <%-- <p class="active btn waves-effect text-center" style="background-color:#bae1ff;font-size:20px;color:black;font-family:Bahnschrift"><i class=" ti-credit-card "></i> March 10, 2022</p> --%>
                                                                 </center>
                                          </div>
                                        </div>
                                         <%-- </div>--%>
                                        </div>
                                        <div class="modal-footer">
                                           <%-- manage station button--%>
                                           <asp:Button ID="btnManageStation" class="active btn waves-effect text-right" style="background-color:#ffffe3;font-size:20px;color:black;font-family:Bahnschrift" runat="server" Text="Submit" ValidationGroup="a" OnClick="btnManageStation_Click"/>
                                           
                                        </div>
                                           <%--</form>--%>
                                         </div>
                                        </div>
                                      </div>
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
                                                          <asp:Label ID="Lbl_user" runat="server" style="color:black;font-size:16px"></asp:Label>
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
                                                               <%-- REFILLING STATION MANAGEMENT --%>
                                                                 <button type="button" class="active btn waves-effect text-center" data-toggle="modal" data-target=".management" style="background-color:transparent;font-size:20px;"><i class="ti-marker"></i>Manage Refilling Station</button> 
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
                                                <div class="card" style="background-color:#f2e2ff">
                                                    <div class="card-header">
                                                        <br /> 
                                                        <h5>PROFILE MANAGEMENT</h5>
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
                                                                <h6>Personal Information</h6>
                                                                <hr>
                                                                  <form class="form" action="##" method="POST" id="ProfilePage">
                                                                      <div class="form-group">                
                                                                        <div class="col-xs-12" style="font-size:16px">
                                                                          <%--  DISPLAY DATA STARTS HERE--%>
                                                                            <br /> 
                                                                            <h5>Id Number: </h5>
                                                                            <asp:Label ID="Lbl_Idno" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server"></asp:Label>
                                                                           <h5>Firstname:</h5> 
                                                                             <asp:TextBox ID="txtfname" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server">
                                                                             </asp:TextBox> 
                                                                            <br />
                                                                            <h5>Middlename:</h5> 
                                                                             <asp:TextBox ID="txtmname" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server">
                                                                             </asp:TextBox> 
                                                                            <br />
                                                                            <h5>Lastname:</h5> 
                                                                             <asp:TextBox ID="txtlname" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server">
                                                                             </asp:TextBox> 
                                                                            <br />
                                                                            <h5>Birthdate: </h5>
                                                                            <asp:TextBox ID="txtdob" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server">
                                                                             </asp:TextBox> 
                                                                            <br />
                                                                             <h5>Contact number:  </h5>
                                                                            <asp:TextBox ID="txtcontact" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server">
                                                                             </asp:TextBox> 
                                                                            <br />
                                                                              <h5>Email Address: </h5>
                                                                            <asp:TextBox ID="txtemail" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server">
                                                                             </asp:TextBox> 
                                                                            <br />
                                                                               <h5>Address:  </h5>
                                                                           <asp:TextBox ID="txtaddress" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server">
                                                                             </asp:TextBox> 
                                                                            <br />
                                                                            <hr />
                                                                               <h5>STATION DETAILS:  </h5>
                                                                            <br /> 
                                                                            <h5>Station Name: </h5>
                                                                            <asp:Label ID="lblStationName" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server"></asp:Label>
                                                                            <br />
                                                                            <h5>Operating Hours: </h5>
                                                                            <asp:TextBox ID="txtOperatngHrs" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server">
                                                                             </asp:TextBox> 
                                                                            <br />
                                                                            <h5>Business Days: </h5>
                                                                            <asp:TextBox ID="txtBssnessDay" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server">
                                                                             </asp:TextBox> 
                                                                            <br />
                                                                            <h5>Status: </h5>
                                                                            <asp:TextBox ID="txt_Status" class=" btn btn-round waves-effect text-center" 
                                                                                         style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift;width:700px" runat="server">
                                                                             </asp:TextBox>
                                                                            <br />
                                                                            <br />
                                                                            <hr />
                                                                            <asp:Button ID="btnEdit" class="btn btn-primary" style="background-color:#bae1ff;font-size:18px;color:black;font-family:Bahnschrift" runat="server" Text="Update Information" OnClick="btnUpdate_Click"/>
                                           
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
