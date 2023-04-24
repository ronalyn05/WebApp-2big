<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="ManageSubscription.aspx.cs" Inherits="WRS2big_Web.superAdmin.Subscriptions1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .texts{
            font-size:14px;
            color:black;

        }
        .scrollable-listbox {
        height: 200px;
        overflow-y: auto;
         }
    </style>
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/2.9.3/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--     <form runat="server">--%>
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
                                            <h5 class="m-b-10">SUBSCRIPTION PLANS</h5>
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
                              <!-- page content -->
                               <div class="right_col" role="main">
                                <div class="">
                                  <div class="clearfix">
                                        <%--BUTTON to ADD--%>
                                        <asp:Button runat="server" ID="createpackagePlan" style="font-size:14px; background-color: #3399FF;" class="btn btn-success btn-sm" Text="Create Plan Package" Onclick="createpackagePlan_Click" />
                                            <!--PAGE CONTENTS-->
                                      <div id="packageContent" runat="server"  style="display:none">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <!-- Tab variant tab card start -->
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <h5>PLAN PACKAGE</h5>
                                                        </div>
                                                        <div class="card-block tab-icon">
                                                            <!-- Row start -->
                                                            <div class="row">
                                                                <div class="col-lg-12 col-xl-16">
                                                                    <!-- <h6 class="sub-title">Tab With Icon</h6> -->
                                                            
                                                                    <!-- Nav tabs -->
                                                                    <ul class="nav nav-tabs md-tabs " role="tablist">
                                                                        <li class="nav-item">
                                                                            <a class="nav-link active" data-toggle="tab" href="#AllCustomers" role="tab">All Customers</a>
                                                                            <div class="slide"></div>
                                                                        </li>
                                                                        <li class="nav-item">
                                                                            <a class="nav-link" data-toggle="tab" href="#pendingCustomers" role="tab">Pending Customers</a>
                                                                            <div class="slide"></div>
                                                                        </li>
                                                                        <li class="nav-item">
                                                                            <a class="nav-link" data-toggle="tab" href="#approvedCustomers" role="tab">Approved Customers</a>
                                                                            <div class="slide"></div>
                                                                        </li>
                                                                    </ul>
                                                                    <!-- Tab panes -->
                                                                    </div>
                                                        
                                                            </div>
                                                            <!-- Row end -->
                                                        </div>
                                                    </div>
                                                    <!-- Tab variant tab card start -->
                                                </div>
                                            </div>
                                     </div>
                                      <%-- <button type="button" id="createpackagePlan" class="togglebtn btn btn-primary waves-effect text-center" style="font-size:14px; background-color: #3399FF;" onclick="packageContent()">Create Plan Package</button>   --%>

                                                <!--MODAL FOR ADD NEW PLAN-->
                                           <div class="modal fade add texts" tabindex="-1" role="dialog" aria-hidden="true">
                                                <div class="modal-dialog  modal-dialog-centered modal-md">
                                                    <div class="modal-content">
                                                        <form id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                       <%--header ni diri--%>
                                                          <div class="modal-header">
                                                               <%--title ni diri--%>
                                                                <h4 class="modal-title" id="myModalPlan"> New Plan</h4>
                                                              
                                                              <%--button close ni diri--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                        <span aria-hidden="true">X</span>
                                                                    </button>
                                                          </div> <%--closing tag ni header--%> 

                                                           <%--body ni diri ni modal--%>
                                                            <div class="modal-body">
                                                                 <div class="col-md-12 col-sm-12 ">
                                                                        <div class="card-header" style="background-color:antiquewhite">
                                                                            <h8>Plan Details:</h8>
                                                                        </div>
                                                                     <div class="x_content">

                                                                          <div class="item form-group">
                                                                              <div class="col-md-12 col-sm-12 ">
                                                                                <asp:Label ID="Label1" runat="server" Text="Plan Name: "></asp:Label><br />
                                                                                <asp:TextBox ID="subPlan" runat="server" Width="281px"></asp:TextBox>
                                                                              </div>
                                                                             <div class="col-md-12 col-sm-12 ">
                                                                                <asp:Label ID="Label2" runat="server" Text="Plan Description:"></asp:Label> <br />
                                                                                 <asp:TextBox ID="PlanDescription" runat="server" Width="281px"></asp:TextBox>
                                                                            </div>
                                                                     
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <asp:Label ID="LabelDuration" runat="server" Text="Plan Duration(months):"></asp:Label> <br />
                                                                                 <asp:TextBox ID="duration" runat="server" Width="281px"></asp:TextBox>
                                                                            </div>
                                                                     
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <asp:Label ID="Amount" runat="server" Text="Amount/Price:"></asp:Label> <br />
                                                                                 <asp:TextBox ID="txtamount" runat="server" Width="281px"></asp:TextBox>
                                                                            </div>
                                                                         </div>
                                                                        <div class="card-header" style="background-color:antiquewhite">
                                                                            <h8 >Plan Features:</h8>
                                                                        </div>
                                                                         <div class="item form-group">
                                                                             <div class="col-md-12 col-sm-12 ">
                                                                            <asp:CheckBoxList ID="featuresList" class="scrollable-listbox" runat="server" >
                                                                                <asp:ListItem> Account Management </asp:ListItem>
                                                                                <asp:ListItem> Employee Management </asp:ListItem>
                                                                                <asp:ListItem> Product Management </asp:ListItem>
                                                                                <asp:ListItem> Refilling Station Management </asp:ListItem>
                                                                                <asp:ListItem> Online Orders </asp:ListItem>
                                                                                <asp:ListItem> Reports </asp:ListItem>
                                                                                <asp:ListItem> Loyalty Program </asp:ListItem>
                                                                                <asp:ListItem> Customer Reviews </asp:ListItem>
                                                                                 <asp:ListItem>Online Subscription </asp:ListItem>
                                                                            </asp:CheckBoxList>                                                                              
                                                                             </div>
                                                                         </div>


                                                                    </div>
                                                               </div>
                                                           </div>
                                                             <div class="modal-footer">
                                                                <%--Button Add ni diri--%>
                                                                 <asp:Button ID="btnAdd" runat="server" Text="Create Subscription Plan" class="btn btn-primary btn-sm texts" OnClick="btnAdd_Click"/>
                                                                 <%-- <button type="button" ID="btnAdd" OnClick="btnAdd_Click" style="font-size:14px;" class="btn btn-success btn-sm"><i class="fa fa-plus"></i> Create Plan</button>--%>
                                                             </div>
                                                       </form>
                                                    </div>
                                                  </div>
                                                </div>
                                      <!--MODAL FOR SETTINGS-->
                                           <div class="modal fade settings texts" tabindex="-1" role="dialog" aria-hidden="true">
                                                <div class="modal-dialog  modal-dialog-centered modal-md">
                                                    <div class="modal-content">
                                                        <form id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                       <%--header ni diri--%>
                                                          <div class="modal-header">
                                                               <%--title ni diri--%>
                                                                <h4 class="modal-title" id="myModalLabel"> Settings</h4>
                                                              
                                                              <%--button close ni diri--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                        <span aria-hidden="true">X</span>
                                                                    </button>
                                                          </div> <%--closing tag ni header--%> 

                                                           <%--body ni diri ni modal--%>
                                                            <div class="modal-body">
                                                                 <div class="col-md-12 col-sm-12 ">

<%--                                                                     <div class="x_content"> <br />
                                                                          <button type="button" ID="btnLock" style="font-size:14px;margin-left:20px"class="btn btn-success btn-sm"><i class="ti-lock"></i> Features Locking </button>
                                                                           <button type="button" ID="btnReminder" style="font-size:14px;margin-left:20px"class="btn btn-success btn-sm"><i class="ti-pin-alt"></i> Reminders </button>
                                                                    </div>--%>
                                                               </div>
                                                           </div>
                                                             <div class="modal-footer">
                                                                <%--Button Add ni diri--%>
<%--                                                                 <asp:Button ID="btnAdd" runat="server" Text="Create Subscription Plan" class="btn btn-primary btn-sm texts" OnClick="btnAdd_Click"/>--%>
                                                                  <button type="button" ID="btnSave" style="font-size:14px;margin-left:20px" class="btn btn-success btn-sm"><i class="ti-save"></i>Save </button>
                                                             </div>
                                                       </form>
                                                    </div>
                                                  </div>
                                                </div>
                                            <%--//</div>--%>
                                                <br /><br />


                                              <asp:GridView runat="server" ID="GridView1" class="texts table-responsive" RowStyle-CssClass="grid-row"  style=" text-align:center;overflow-y: auto;max-height: 500px;"  BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" HtmlEncode="false" Width="1728px" GridLines="Vertical">
                                                  <AlternatingRowStyle BackColor="#DCDCDC" />
                                                <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="updateButton" runat="server" CssClass="update-button" Text="View" OnClick="updateButton_Click1" CommandArgument='<%# Container.DataItemIndex %>'  BorderStyle="None" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
                                                    <RowStyle ForeColor="Black" BackColor="#EEEEEE" />
                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                                </asp:GridView> <br /> <br /><br />
                                           <div class="col-md-6" id="fullDetails">
                                                <div class="card">
                                                    <div class="card-header">
                                                        <h5>Plan Full details</h5>
                                                        <!--<span>Add class of <code>.form-control</code> with <code>&lt;input&gt;</code> tag</span>-->
                                                    </div>
                                                    <div class="card-block" >
                                                        <div class="form-material">
                                                            <div class="form-group form-default">
                                                                <label class="form-control">Plan ID:</label>
                                                                <asp:Label runat="server" style="text-align:center" class="form-control" id="planID"> </asp:Label>
                                                            </div>
                                                            <div class="form-group form-default">
                                                                <label class="form-control">Plan Name:</label>
                                                                <asp:TextBox runat="server" style="text-align:center" class="form-control" id="planName"> </asp:TextBox>
                                                                
                                                            </div>
                                                            <div class="form-group form-default">
                                                                <label class="form-control">Plan Description:</label>
                                                                <asp:TextBox runat="server" style="text-align:center" class="form-control" id="planDes"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group form-default">
                                                                <label class="form-control">Plan Price:</label>
                                                                <asp:TextBox runat="server" style="text-align:center" class="form-control" id="planAmount"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group form-default">
                                                                <label class="form-control">Plan Duration(month):</label>
                                                                <asp:TextBox runat="server" style="text-align:center" class="form-control" id="planDuration"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group form-default">
                                                                <label class="form-control">Plan Features:</label>
                                                                <asp:ListBox runat="server" style="text-align:center; font-size:18px" SelectionMode="Multiple" class="form-control" id="planFeatures" Height="208px" Width="889px"></asp:ListBox>
                                                            </div>
                                                            <asp:Button id="updateBtn" runat="server" Text="UPDATE" class="btn btn-primary btn-sm texts" OnClick="updateBtn_Click"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                    <%--<script>
                                          $(document).ready(function () {
                                              $('#GridView1 tr').click(function () {
                                                  // Get the PLAN ID value for the clicked row
                                                  var planID = $(this).find('#planIDLabel').text();

                                                  // Retrieve the selected subscription plan from Firebase
                                                  var ref = firebase.database().ref('SUPERADMIN/SUBSCRIPTION_PLANS/' + planID);
                                                  ref.once('value', function (snapshot) {
                                                      var selectedPlan = snapshot.val();
                                                      if (selectedPlan != null) {
                                                          // Update the textboxes with the selected plan details
                                                          $('#updatePlan').val(selectedPlan.planName);
                                                          $('#updateDesc').val(selectedPlan.planDes);
                                                          $('#updateDuration').val(selectedPlan.planDuration);
                                                          $('#updateAmount').val(selectedPlan.planPrice);
                                                          //$('#PlanFeaturesTextBox').val(selectedPlan.features.join(', ')); // Uncomment this line if you want to display the features in a textbox
                                                          $('#updatePlanModal').modal();
                                                      }
                                                  });
                                              });
                                          });

                                      </script>--%>


                                      <br /><br />
                                                <!--MODAL FOR UPDATE PLAN-->
                                           <div class="modal fade updatePlan texts" tabindex="-1" role="dialog" aria-hidden="true">
                                                <div class="modal-dialog  modal-dialog-centered modal-md">
                                                    <div class="modal-content">
                                                        <form id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                       <%--header ni diri--%>
                                                          <div class="modal-header">
                                                               <%--title ni diri--%>
                                                                <h4 class="modal-title" id="updatePlanModal"> Update Plan</h4>
                                                              
                                                              <%--button close ni diri--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                        <span aria-hidden="true">X</span>
                                                                    </button>
                                                          </div> <%--closing tag ni header--%> 

                                                           <%--body ni diri ni modal--%>
                                                            <div class="modal-body">
                                                                 <div class="col-md-12 col-sm-12 ">
                                                                        <div class="card-header" style="background-color:antiquewhite">
                                                                            <h8>Plan Details:</h8>
                                                                        </div>
                                                                     <div class="x_content">

                                                                          <div class="item form-group">
                                                                              <div class="col-md-12 col-sm-12 ">
                                                                                <asp:Label ID="Label3" runat="server" Text="Plan Name: "></asp:Label><br />
                                                                                <asp:TextBox id="updatePlan" runat="server" Width="281px"></asp:TextBox>
                                                                              </div>
                                                                             <div class="col-md-12 col-sm-12 ">
                                                                                <asp:Label ID="Label4" runat="server" Text="Plan Description:"></asp:Label> <br />
                                                                                 <asp:TextBox id="updateDesc" runat="server" Width="281px"></asp:TextBox>
                                                                            </div>
                                                                     
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <asp:Label ID="Label5" runat="server" Text="Plan Duration(months):"></asp:Label> <br />
                                                                                 <asp:TextBox id="updateDuration" runat="server" Width="281px"></asp:TextBox>
                                                                            </div>
                                                                     
                                                                            <div class="col-md-12 col-sm-12 ">
                                                                                <asp:Label ID="Label6" runat="server" Text="Amount/Price:"></asp:Label> <br />
                                                                                 <asp:TextBox id="updateAmount" runat="server" Width="281px"></asp:TextBox>
                                                                            </div>
                                                                         </div>
                                                                        <div class="card-header" style="background-color:antiquewhite">
                                                                            <h8 >Plan Features:</h8>
                                                                        </div>
                                                                         <div class="item form-group">
                                                                             <div class="col-md-12 col-sm-12 ">
                                                                    <asp:CheckBoxList ID="updateFeatures" class="scrollable-listbox" runat="server" >
                                                                        <asp:ListItem> Account Management </asp:ListItem>
                                                                        <asp:ListItem> Employee Management </asp:ListItem>
                                                                        <asp:ListItem> Product Management </asp:ListItem>
                                                                        <asp:ListItem> Refilling Station Management </asp:ListItem>
                                                                        <asp:ListItem> Online Orders </asp:ListItem>
                                                                        <asp:ListItem> Reports </asp:ListItem>
                                                                        <asp:ListItem> Loyalty Program </asp:ListItem>
                                                                        <asp:ListItem> Customer Reviews </asp:ListItem>
                                                                         <asp:ListItem> Online Subscription </asp:ListItem>
                                                                    </asp:CheckBoxList>                                                                              
                                                                             </div>
                                                                         </div>


                                                                    </div>
                                                               </div>
                                                           </div>
                                                             <div class="modal-footer">
                                                                <%--Button Add ni diri--%>
                                                                 <asp:Button ID="Button1" runat="server" Text="Update" class="btn btn-primary btn-sm texts"/>
                                                                 <%-- <button type="button" ID="btnAdd" OnClick="btnAdd_Click" style="font-size:14px;" class="btn btn-success btn-sm"><i class="fa fa-plus"></i> Create Plan</button>--%>
                                                             </div>
                                                       </form>
                                                    </div>
                                                  </div>
                                                </div>

                                                       <%--<div class="row">
                                                           <div class="col-xl-3 col-md-12">
                                                            <div class="card ">
                                                                <div class="card-header">
                                                                    <div class="card-header-right">
                                                                        <ul class="list-unstyled card-option">
                                                                            <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                                                            <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                            <li><i class="fa fa-minus minimize-card"></i></li>
                                                                            <li><i class="fa fa-refresh reload-card"></i></li>
                                                                            <li><i class="fa fa-trash close-card"></i></li>
                                                                        </ul>
                                                                    </div>
                                                                    <h5>Subscription Plans</h5>
                                                                </div>
                                                                <div class="card-block">        
                                                                   <asp:ListBox ID="PlansListbox" runat="server" style="border:transparent; font-size:20px;padding:4px 7px 2px 4px;" Width="273px" Height="179px">
                                                                   </asp:ListBox> 
                                                                </div>
                                                            <div class="card-footer">
                                                                    <asp:Button ID="PlanDetails" OnClick="PlanDetails_Click" type="button" style="font-size:14px;" class="btn btn-primary btn-sm" runat="server" Text="View Plan Details" />
                                                            </div>
                                                         </div>
                                                       </div>
                                                   
                                                    <div class="col-xl-9 col-md-12">
                                                        <div class="card" style="background-color:#f2e2ff">
                                                            <div class="card-header">
                                                                <h5>SUBSCRIPTION PLAN INFORMATION</h5>
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
                                                                    <div class="tab-pane active">
                                                                              <div class="form-group">                
                                                                                <div class="col-xs-12" style="font-size:16px">
                                                                                   <h5>Plan ID:</h5> 
                                                                                    <asp:Label ID="planID" runat="server" class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px" ></asp:Label>
                                                                                    <br>
                                                                                    <h5>Plan Name: </h5>
                                                                                    <asp:TextBox  ID="planName" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                          
                                                                                    <h5>Plan Description: </h5>
                                                                                      <asp:TextBox  ID="planDes" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px;height:70px"></asp:TextBox>
                                                                           
                                                                                     <h5>Plan Features:  </h5>
                                                                                    
                                                                                    <asp:ListBox ID="planFeatures" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px;height:170px"> </asp:ListBox>

                                                                                    <h5>Plan Duration(months): </h5>
                                                                                      <asp:TextBox  ID="planDue" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                                   <h5>Plan Price: </h5>
                                                                                      <asp:TextBox  ID="planPrice" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>
                                                                           
                                                                                  </div>
                                                                              </div>
                                                                      </div><!--/tab-pane-->
                                                                  </div><!--/tab-content-->
                                                           
                                                                </div>
                                                            </div>
                                                            <div class="card-footer">
                                                                             <asp:Button ID="updateBtn" OnClick="updateBtn_Click" style="font-size:14px;" class="btn btn-primary btn-sm"  runat="server" Text="Update details" />
                                                                           
                                                            </div>
                                                        </div>
                                                       </div> 
                                                     </div>--%>
                                      <!--PAGE CONTENTS END-->
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
