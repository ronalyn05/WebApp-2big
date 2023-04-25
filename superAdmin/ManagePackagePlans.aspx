<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="ManagePackagePlans.aspx.cs" Inherits="WRS2big_Web.superAdmin.ManagePackagePlans" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%-- <form runat="server">--%>
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
                                            <h5 class="m-b-10"> SUBSCRIPTION PACKAGES</h5>
                                            <h6 class="m-b-0">2BiG: Water Refilling Station Management System</h6>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="breadcrumb-title">
                                            <li class="breadcrumb-item">
                                                <a href="/superAdmin/SAdminIndex"> <i class="fa fa-home"></i> </a>
                                            </li>
                                            <li class="breadcrumb-item">
                                                <a href="/superAdmin/SAdminIndex">Dashboard</a>
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
                                            <%-- <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>--%>
                                            <div class="clearfix">
                                            </div>
                                   <!--PACKAGES -->
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <!-- Tab variant tab card start -->
                                            <div class="card">
                                                <div class="card-header">
                                                    <h5>PACKAGES</h5>
                                                </div>
                                                <div class="card-block tab-icon">
                                                    <!-- Row start -->
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xl-16">
                                                                          <div class="col-lg-12 col-xl-12">
                                                                            <div class="card table-card">
                                                                                <div class="card-block">
                                                                                    <div class="table-responsive"> <br />
                                                                                      <asp:GridView runat="server" ID="packagesGridview" class="texts table-responsive table-hover"  style=" text-align:center;overflow-y: auto;max-height: 500px; margin-left: 14px;"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="20" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" ForeColor="Black" CellSpacing="20" Font-Size="14px">
                                                                                         <Columns>
                                                                                             <asp:TemplateField >
                                                                                                <ItemTemplate> 
                                                                                                     <%-- <asp:Button runat="server" Text="Open" ID="viewPackageDetails" style="background-color:transparent;font-size:16px;"  class="active btn waves-effect text-center"/> --%>
                                                                                                   <asp:Button runat="server" Text="Open" ID="viewPackageDetails" style="background-color:transparent;font-size:16px;" OnClick="viewPackageDetails_Click" class="active btn waves-effect text-center"/> 
                                                                                                    <%--OnClick="viewPackageDetails_Click"--%>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         </Columns>
                                                                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                                                <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                                                                <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                           
                                                        </div>
                                                    </div>
                                                    <!-- Row end -->
                                                </div>
                                            </div>
                                            <!-- Tab variant tab card start -->
                                        </div>
                                    </div>
                                             <!-- ADDING OF NEW PACKAGE -->
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <!-- Tab variant tab card start -->
                                            <div class="card">
                                                <div class="card-header">
                                                    <h5>CREATE PACKAGE</h5>
                                                        <div class="card-header-right">
                                                            <ul class="list-unstyled card-option">
                                                                <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                                <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                <li><i class="fa fa-minus minimize-card"></i></li>
                                                                <li><i class="fa fa-refresh reload-card"></i></li>
                                                               
                                                            </ul>
                                                        </div>
                                                </div>
                                                <div class="card-block tab-icon">
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
                                                                                            <div class="form-material" style="margin-left:50px">
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE NAME:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                             <asp:TextBox type="text" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;" id="packageName" runat="server"> </asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE DESCRIPTION:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                            
                                                                                                            <asp:TextBox type="text" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;" id="packageDescription" runat="server"> </asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                   <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE PRICE:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                            <asp:TextBox type="number" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;" id="packagePrice"  runat="server"> </asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                   <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">DURATION TYPE:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                           <asp:DropDownList class="form-control" runat="server" style="border-style: solid; border-width: thin; border-color: darkgrey;" ID="durationTypeSelected">
                                                                                                                <asp:ListItem></asp:ListItem>
                                                                                                               <asp:ListItem style="color:black;font-size:18px" Value="Month">Monthly</asp:ListItem>
                                                                                                               <asp:ListItem style="color:black;font-size:18px" Value="Year">Yearly</asp:ListItem>
                                                                                                           </asp:DropDownList>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text" >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE DURATION:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left:70px">  
                                                                                                             <asp:TextBox type="number" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;" id="packageDuration" runat="server"></asp:TextBox>
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
                                                                                            <div class="form-material" style="margin-left:50px">

                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size:18px;color:black">PACKAGE FEATURES:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:40px;font-size:18px;color:black">
                                                                                                            <%--<asp:CheckBox runat="server" Text="Select All" ID="selectAll"/>--%>
                                                                                                        </div>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px;font-size:18px;color:black"> 
                                                                                                          <asp:CheckBoxList runat="server" ID="featuresCheckbox" Height="318px" Width="327px" >
                                                                                                                <asp:ListItem> &nbsp; Account Management </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Employee Management </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Product Management </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Refilling Station Management </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Online Orders </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Reports </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Loyalty Program </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Customer Reviews </asp:ListItem>
                                                                                                                <asp:ListItem> &nbsp; Online Subscription </asp:ListItem>
                                                                                                          </asp:CheckBoxList>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label " style="font-size:18px;color:black"">ORDER TRANSACTION LIMIT:</label>
                                                                                                        <div class="col-sm-10 form-control-round"  style="margin-left:70px"> 
                                                                                                          <asp:TextBox type="number" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;" id="packageOrderLimit" runat="server"> </asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>

                                                                                            </div>

                                                                            </div>
                                                                         </div>
                                                                           <div class="col-xl-6 col-md-12">
                                                                              <div class="card-block">
                                                                                           <div style="background-color:#018cff;color:white" class="card card-block">
	                                                                                           <h7>PACKAGE SETTINGS</h7>
                                                                                           </div>
                                                                                            <div class="form-material" style="margin-left:50px">
                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label " style="font-size:18px;color:black"">Client to Super-Admin Messaging:</label>
                                                                                                        <div class="col-sm-10 form-control-round"   style="margin-left:70px;font-size:18px;color:black"> 
                                                                                                         <asp:CheckBoxList runat="server" ID="messagingOption" Width="327px" >
                                                                                                             <asp:ListItem Value="Allowed"> &nbsp; Allow</asp:ListItem>
                                                                                                             <asp:ListItem  Value="Not Allowed"> &nbsp; Don't Allow</asp:ListItem>
                                                                                                         </asp:CheckBoxList>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group row text"  >
                                                                                                        <label class="col-sm-5 col-form-label " style="font-size:18px;color:black"">Number of Refilling Stations to manage:</label>
                                                                                                        <div class="col-sm-10 form-control-round"   style="margin-left:70px;font-size:18px;color:black"> 
                                                                                                          <asp:TextBox type="number" class="form-control" style="border-style: solid; border-width: thin; border-color: darkgrey;" id="numofStations" runat="server"> </asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                            </div>

                                                                            </div>
                                                                         </div>
                                                    </div>
                                                    <!-- Row end -->
                                                </div>
                                            </div>
                                            <!-- Tab variant tab card start -->
                                        </div>
                                    </div>
                                                    <center>
                                                            <asp:Button runat="server" ID="createPackage" class="btn btn-primary btn-sm text" style="font-size:20px" OnClick="createPackage_Click" Text="CREATE PACKAGE"/>
                                                         
                                                     </center>
                                    </div>

                                    <!-- page content -->
                                    <!-- Page-body end -->

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
<%--</form>--%>
</asp:Content>
