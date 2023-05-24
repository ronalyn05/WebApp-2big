<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="packageDetails.aspx.cs" Inherits="WRS2big_Web.superAdmin.packageDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style>
        .texts{
            font-size:16px;
            color:black;
        
        }
        .text{
            font-size:20px;
            color:black;
        }
        .scrollable-listbox {
        height: 200px;
        overflow-y: auto;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--       <form runat="server">--%>
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
                                            <h5 class="m-b-10"> REFILLING STATION CLIENTS</h5>
                                            <h6 class="m-b-0">2BiG: Water Refilling Station Management System</h6>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="breadcrumb-title">
                                            <li class="breadcrumb-item">
                                                <a href="/superAdmin/SAdminIndex"> <i class="fa fa-home"></i> </a>
                                            </li>
                                            <li class="breadcrumb-item">
                                                <a href="/superAdmin/ManagePackagePlans">Subscription Packages</a>
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
                                    <!-- Page-body start -->
                                    <!-- page content -->
                                    <div class="right_col" role="main">
                                        <div class="">
                                        
                                            <div class="clearfix">
                                             <div class="modal fade updatePackage  col-md-12 col-md-12" tabindex="-1" role="dialog" aria-hidden="true">
 <div class="modal-dialog modal-dialog-centered modal-md col-md-10 col-md-15">
                                                        <div class="modal-content col-xl-10 col-md-10" style="margin-left: 370px">
                                                            <div id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="update"></h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="row">
                                                                   <div class="col-xl-5 col-md-8">
                                                                        <div class="x_content">
                                                                            <%-- <div class="item form-group">--%>
                                                                            <h4 style="color: black; font-family: Bahnschrift">PACKAGE DETAILS</h4>

                                                                                            <div class="form-material" style="margin-left:20px">
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE NAME:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                             <asp:TextBox type="text" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;font-size:18px;color:black" id="updatepackageName" runat="server"> </asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE DESCRIPTION:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                            
                                                                                                            <asp:TextBox type="text" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;font-size:18px;color:black" id="updatePackageDes" runat="server"> </asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                   <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE PRICE:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                            <asp:TextBox type="number" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;font-size:18px;color:black" id="updatePackagePrice"  runat="server"> </asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                   <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">DURATION TYPE:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                           <asp:DropDownList class="form-control" runat="server" style="border-style: solid; border-width: thin; border-color: darkgrey;font-size:18px;color:black" ID="updateDurationType">
                                                                                                                <asp:ListItem></asp:ListItem>
                                                                                                               <asp:ListItem style="color:black;font-size:18px" Value="Month">Monthly</asp:ListItem>
                                                                                                               <asp:ListItem style="color:black;font-size:18px" Value="Year">Yearly</asp:ListItem>
                                                                                                           </asp:DropDownList>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE DURATION:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                             <asp:TextBox type="number" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;font-size:18px;color:black" id="updateDuration" runat="server"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label " style="font-size:18px;color:black"">ORDER TRANSACTION LIMIT:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px"> 
                                                                                                          <asp:TextBox type="number" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;font-size:18px;color:black" id="updateOrderLimit" runat="server"> </asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label " style="font-size:18px;color:black"">PRODUCT LIMIT:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px"> 
                                                                                                          <asp:TextBox type="number" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;font-size:18px;color:black" id="updateProductLimit" runat="server"> </asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label " style="font-size:18px;color:black"">MANAGEABLE STATIONS:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px"> 
                                                                                                          <asp:TextBox type="number" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;font-size:18px;color:black" id="updateManagebleStation" runat="server"> </asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                           <div class="col-xl-5 col-md-8">
                                                                              <div class="card-block">
                                                                                            <div class="form-material" style="margin-left:50px"> <br /><br />
                                                                                                 <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE FEATURES:</label> 
                                                                                                    <div class="form-group row text"  >
                                                                                                       
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:14px;color:black;margin-left:70px;">Existing Features:
                                                                                                        </label>
                                                                                                         <asp:ListBox runat="server" ID="existingFeatures" style="text-align:center; font-size:18px;margin-left:150px;" Height="230px" Width="300px"> </asp:ListBox> <br /><br />
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:40px;font-size:18px;color:black">
                                                                                                            <%--<asp:CheckBox runat="server" Text="Select All" ID="selectAll"/>--%>
                                                                                                        </div>
                                                                                                         <div class="col-sm-10 form-control-round"  style="margin-left:40px;font-size:18px;color:black"> 
                                                                                                        
                                                                                                        </div>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px;font-size:18px;color:black"> 
                                                                                                          <asp:CheckBoxList runat="server" ID="updatefeaturesCheckbox" Height="318px" Width="327px" >
                                                                                                                <asp:ListItem> &nbsp; Account Management </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Employee Management </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Product Management </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Refilling Station Management </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Online Orders </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Reports </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Loyalty Program </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Customer Reviews </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Online Subscription </asp:ListItem>
                                                                                                               <asp:ListItem> &nbsp; Walk-in Orders </asp:ListItem>
                                                                                                          </asp:CheckBoxList>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label " style="font-size:18px;color:black"">RENEWABLE:</label>
                                                                                                           <asp:DropDownList class="form-control" runat="server" style="border-style: solid; border-width: thin; border-color: darkgrey;font-size:18px;color:black" ID="updateRenewable">
                                                                                                                <asp:ListItem></asp:ListItem>
                                                                                                               <asp:ListItem style="color:black;font-size:18px" Value="Yes">YES</asp:ListItem>
                                                                                                               <asp:ListItem style="color:black;font-size:18px" Value="No">NO</asp:ListItem>
                                                                                                           </asp:DropDownList>
                                                                                                    </div>

                                                                                            </div>

                                                                            </div>
                                                                         </div>
                                                                    </div>

                                                                    <div class="modal-footer">
                                                                        <%--  BUTTON ADD PAYMENT METHOD--%>
                                                                        <asp:Button ID="updatePackagebtn" runat="server" Text="Update" OnClick="updatePackagebtn_Click" class="btn btn-primary btn-sm"  />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                     <!--MAIN CONTENT-->
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <!-- Tab variant tab card start -->
                                            <div class="card">
                                                <div class="card-header">
                                                    
                                                </div>
                                                <div class="card-block tab-icon">
                                                    <!-- Row start -->
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xl-12">
                                                               <br />
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <!-- Tab variant tab card start -->
                                                                    <div class="card">
                                                                        <div class="card-header">
                                                                           
                                                                        </div>
                                                                        <div class="card-block tab-icon">
                                                                            <!-- Row start -->
                                                                            <div class="row">
                                                                              <div class="col-xl-6 col-md-12">
                                                                              <div class="card-block">
                                                                                          <div style="background-color:#018cff;color:white" class="card card-block">
	                                                                                           <h7>PACKAGE DETAILS</h7>
                                                                                           </div>
                                                                                            <div class="form-material" style="margin-left:100px">
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE ID:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:90px">  
                                                                                                            <asp:Label class="form-control-round" id="packageID" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE NAME:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:90px">  
                                                                                                            <asp:Label class="form-control-round" id="packageName" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE DESCRIPTION:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:90px">  
                                                                                                            <asp:Label class="form-control-round" id="packageDescription" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                   <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE PRICE:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:90px">  
                                                                                                            <asp:Label class="form-control-round" id="packagePrice" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE DURATION:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:90px">  
                                                                                                            <asp:Label class="form-control-round" id="packageDuration" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">ORDER LIMIT:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:90px">  
                                                                                                          <asp:Label  class="form-control-round" id="packageLimit" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">MANAGEABLE STATIONS:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:90px">  
                                                                                                          <asp:Label  class="form-control-round" id="manageableStation" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                            </div>

                                                                            </div>
                                                                         </div>
                                                                              <div class="col-xl-6 col-md-12">
                                                                              <div class="card-block">
                                                                                           <div style="background-color:#018cff;color:white" class="card card-block">
	                                                                                           <h7>PACKAGE DETAILS</h7>
                                                                                           </div>
                                                                                            <div class="form-material" style="margin-left:100px">

                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">FEATURES:</label>

                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px"> 
                                                                                                          <asp:ListBox runat="server" ID="featuresList" style="text-align:center; font-size:18px" Height="248px" Width="421px"> </asp:ListBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                            </div>
                                                                                            <div class="form-material" style="margin-left:100px">

                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">PRODUCT LIMIT:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px"> 
                                                                                                          <asp:Label  class="form-control-round" id="productLimit" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                            </div>
                                                                                            <div class="form-material" style="margin-left:100px">

                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">RENEWABLE:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px"> 
                                                                                                          <asp:Label  class="form-control-round" id="renewable" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                            </div>
                                                                                            <div class="form-material" style="margin-left:100px">

                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px">STATUS:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px"> 
                                                                                                          <asp:Label  class="form-control-round" id="status" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                            </div>
                                                                            </div>
                                                                         </div>
                                                                        </div>
                                                                            <asp:Button runat="server" Text="Archive" ID="archivePackage" style="font-size:16px;margin-left:10px" OnClick="archivePackage_Click" class="btn btn-primary btn-sm text-center"/>
                                                                             <asp:Button runat="server" Text="Restore" ID="restorePackage" style="font-size:16px;margin-left:10px" OnClick="restorePackage_Click" class="btn btn-primary btn-sm text-center"/>
                                                                           
                                                                         <button type="button" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".updatePackage">Update</button>
                                                                        </div>
                                                                    </div>
                                                                    <!-- Tab variant tab card start -->
                                                                </div>
                                                            </div>
                                                              <asp:LinkButton runat="server" Text="View Packages List" style="font-size:18px" href="ManagePackagePlans.aspx"></asp:LinkButton>

                                                        </div>

                                                    </div>
                                                    <!-- Row end -->
                                                </div>
                                            </div>
                                            <!-- Tab variant tab card start -->
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
<%--</form>--%>
</asp:Content>
