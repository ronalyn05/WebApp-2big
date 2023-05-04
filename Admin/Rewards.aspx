<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Rewards.aspx.cs" Inherits="WRS2big_Web.Admin.Rewards" %>
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
                                            <h5 class="m-b-10">LOYALTY PROGRAM </h5>
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
                                   <!-- Page-body start -->

                                    <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".addreward"><i class="fa fa-plus"></i> Set reward system </button>
                                       <button type="button" style="font-size:14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".setpromo"><i class="fa fa-plus"></i> Set promo offered </button>
                                    <br />
                                      <%-- MODAL FOR REWARD SYSTEM--%>
                                      <div class="modal fade addreward" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-lg">
                                            <div class="modal-content">
                                            <form id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel"> REWARD SYSTEM</h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-12 col-sm-12 ">
                                            <div class="x_content">
                                                <h4 style="color:black;font-family:Bahnschrift"> Set your reward system here:</h4>
                                                <hr />
                                                <div class="col-md-12 col-sm-12">
                                              <h5>How would you like your customer to earn points?</h5>
                                              <asp:RadioButtonList ID="radioCusEarnPoints" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Per transaction" Value="per_transaction" />
                                                <asp:ListItem Text="Per amount" Value="per_amount" />
                                              </asp:RadioButtonList>
                                                    </div>
                                              <div id="pointsInput" class="col-md-12 col-sm-12">
                                                  <h5>Points to earn:</h5>
                                                <%--<label for="points">Points:</labe>l>--%>
                                                <asp:TextBox ID="txtpointsPerTxnOrAmount" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Enter points to earn by the customer per transaction or per amount"></asp:TextBox>
                                              </div>
                                                  <div class="col-md-12 col-sm-12">
                                                  <h5>Minimum range amount (applies for per amount only):</h5>
                                                <%--<label for="points">Points:</labe>l>--%>
                                                <asp:TextBox ID="txtrange_perAmount" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Enter the minimum range amount"></asp:TextBox>
                                              </div>
                                            </div>

                                            <script>
                                                // Get the relevant elements from the DOM
                                                var radioCusEarnPoints = document.getElementById("radioCusEarnPoints");
                                                var pointsInput = document.getElementById("pointsInput");
                                                var pointsInputField = document.getElementById("txtpointsPerTxnOrAmount");
                                                var rangeAmountField = document.getElementById("txtrange_perAmount");

                                                // Add a change event listener to the RadioButtonList
                                                radioCusEarnPoints.addEventListener("change", function () {
                                                    if (radioCusEarnPoints.value === "per_transaction") {
                                                        // If per transaction is selected, show the points input and make it required
                                                        pointsInput.style.display = "block";
                                                        pointsInputField.required = true;
                                                        rangeAmountField.setAttribute("disabled", "disabled");
                                                        // rangeAmountField.disabled = true; // Disable the range amount field
                                                    } else {
                                                        // If per amount is selected, hide the points input and make it not required
                                                        pointsInput.style.display = "block";
                                                        pointsInputField.required = false;
                                                        rangeAmountField.disabled = false; // Enable the range amount field
                                                    }
                                                });
                                            </script>

                                            
                                                     </div> 
                                                 <br />
                                                 <div class="modal-footer">
                                               <%-- add data button--%>
                                                     <script>
                                                         function disableButton() {
                                                             document.getElementById("btnSet").disabled = true;
                                                         }
                                                     </script>
                                                      <%-- set data button--%>
                                               <asp:Button ID="btnSet" runat="server" Text="Add Reward" OnClientClick="disableButton()" class="btn btn-primary btn-sm" OnClick="btnAddReward_Click"/>
                                                <br />
                                                  </div>
                                                </div>
                                               <%-- <div class="modal-footer">
                                              
                                                </div>--%>
                                              </div>
                                            </div>
                                             <%-- </form>--%>
                                             </div>
                                            </div>
                                        <%-- end set REWARD SYSTEM --%>
                                      <%-- MODAL FOR PROMO OFFERED--%>
                                      <div class="modal fade setpromo" tabindex="-1" role="dialog" aria-hidden="true">
                                           <div class="modal-dialog modal-dialog-centered modal-lg">
                                            <div class="modal-content">
                                            <form id="demo-form1" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                            <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel1"> PROMO OFFERED</h4>
                                                <%--exit button--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                            </div>
                                            <div class="modal-body">
                                            <div class="col-md-12 col-sm-12 ">
                                            <div class="x_content">
                                                <h4 style="color:black;font-family:Bahnschrift"> Set your promo offered here:</h4>
                                                <hr />
                                                 <div class="col-md-12 col-sm-12 ">
                                            <h5>Promo Name:</h5>
                                            <asp:TextBox ID="txtrewardname" runat="server" ToolTip="eg: 10% discount coupon" class="form-control" placeholder="Enter reward promo offered (Ex:10% discount coupon )"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*** required ***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtrewardname" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                                   <div class="col-md-12 col-sm-12 ">
                                            <h5>Promo Discount Value :</h5>
                                            <asp:TextBox ID="txtrewardValue" runat="server" TextMode="Number" class="form-control" placeholder="Enter promo percentage in number base on the promo type you offered"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqlname" runat="server" ErrorMessage="*** required ***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtrewardValue" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                                     <div class="col-md-12 col-sm-12 ">
                                            <h5>Description:</h5>
                                            <asp:TextBox ID="txtdescription" runat="server" ToolTip="eg: Get 10% off on your next purchase " class="form-control" placeholder="Enter promo description"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*** required ***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtdescription" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                                     <div class="col-md-12 col-sm-12 ">
                                                  <h5>Choose any product offers that applies to the promo you offered:</h5>
                                                    <asp:CheckBoxList ID="checkPromoOffered" runat="server" AutoPostBack="false">
                                                        <asp:ListItem Text="Product Refill" Value="Product Refill" ID="pro_refillRadio" ></asp:ListItem>
                                                        <asp:ListItem Text="other Product" Value="other Product" ID="otherproductRadio" ></asp:ListItem>
                                                    </asp:CheckBoxList>
                                            </div>
                                                    <div class="col-md-12 col-sm-12 ">
                                                  <h5>Choose any unit and sizes offers that applies to the promo you offered:</h5>
                                                    <asp:CheckBoxList ID="chUnitSizes" runat="server">
                                                    </asp:CheckBoxList>
                                            </div>
                                                 <div class="col-md-12 col-sm-12 ">
                                            <h5>Points required to claim the reward:</h5>
                                            <asp:TextBox ID="txtpointsrequired" runat="server" TextMode="Number" class="form-control" placeholder="Enter points required to claim the reward"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="***required***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtpointsrequired" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                                     <div class="col-md-12 col-sm-12 ">
                                            <h5>Promo Expiration:</h5>
                                                         <br />
                                                          <strong> From:</strong>
                                             <asp:TextBox ID="txtpromoExpirationFrom" TextMode="Date" runat="server"></asp:TextBox>
                                                <strong> To:</strong>
                                                 <asp:TextBox ID="txtpromoExpirationTo" TextMode="Date" runat="server"></asp:TextBox>
                                          <%--  <asp:TextBox ID="TextBox1" runat="server" TextMode="Number" class="form-control" placeholder="Enter promo points required "></asp:TextBox>--%>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="***required***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtpointsrequired" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                                <br />
                                                  </div>
                                                </div>
                                                <div class="modal-footer">
                                               <%-- set data button--%>
                                               <asp:Button ID="btnAdd" runat="server" Text="Add Promo Offered" class="btn btn-primary btn-sm" OnClick="btnAddPromoOffered_Click" AutoPostBack="false"/>
                                                </div>
                                              </div>
                                            </div>
                                             <%-- </form>--%>
                                             </div>
                                            </div>
                                        <%-- end set  for promo offered --%>

                                    
                                      <!--PAGE CONTENTS-->
                                         <div class="col-xl-12 col-xl-12 h-100">
                                             <div class="card">
                                                <%--<div class="card" style="background-color:#f2e2ff">--%>
                                                    <div class="card-header">
                                                         <asp:Label ID="Label1" runat="server" Text="PROMO REPORTS" ForeColor="Black" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                        <div style="float:right;"> 
                                                            <asp:TextBox ID="txtSearch" Width="364px" Placeholder="search by promo name " ToolTip="enter promo name to view record" Height="40px" runat="server"></asp:TextBox>
                                                            <asp:Button ID="btnSearchReports" runat="server" Text="Search" OnClick="btnSearchReports_Click" CssClass="btn-primary" Height="40px"/>
                                                        
                                                        </div>
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
                                                   <%-- MODAL TO VIEW CERTAIN RECORDS --%>
                                                    <div class="modal fade" id="view" tabindex="-1" role="dialog" aria-hidden="true">
                      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
                        <div class="modal-content">
                          <form id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                            <div class="modal-header">
                              <h4 class="modal-title" id="myModalLabel3"> REWARD / PROMO REPORTS: 
                                
                              </h4>
                              <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                            </div>
                            <div class="modal-body">
                              <div class="col-xl-12 col-xl-12 ">
                                <div class="x_content">
                                   <%-- USERS LOG REPORTS--%>
                                     <div class="card-block">
                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                <%--the gridview starts here--%>
                                                               <div style="overflow: auto; height: 600px; text-align:center;" class="texts" >
                                                                   <asp:Label ID="lblMessageError" runat="server" Font-Underline="true" ForeColor="red"/>
                                                                   <br />
                                                                    <asp:GridView runat="server" ID="gridReward" CellPadding="3" Width="975px" CssClass="auto-style1" style=" text-align:center; overflow-y: auto;max-height: 500px; margin-left: 14px;" 
                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                                  
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

                                                               <%--Gridview ends here--%>

                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
                                                            <%--TAB end --%>
                                                        </div>
                                                    </div>
                                </div>
                              </div>
                            </div>
                          </form>
                        </div>
                      </div>
                    </div>
                                                    <div class="card-block">
                                                         <asp:DropDownList ID="ddlSearchOptions" runat="server" CssClass="text-center" Height="40px" Width="364px">
                                                            <asp:ListItem Selected="False" Text="---View Reports -----"></asp:ListItem>
                                                            <asp:ListItem Text="Reward System" Value="0"></asp:ListItem>
                                                           <asp:ListItem Text="Promo Offered" Value="1"></asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:Button ID="btnDisplayReports" runat="server" Text="Search" OnClick="btnDisplayReports_Click" CssClass="btn-primary" Height="40px"/>
                                                        <br />
                                                   <%-- </div>
                                                    <div class="card-footer">--%>
                                                                    <%-- <asp:Button ID="EditBtn" style="font-size:14px;" class="btn btn-primary btn-sm"  runat="server" Text="Update Records" OnClick="btnEdit_Click"/>
                                                                     --%> 
                                                        <div class="table-responsive">
                                                            <div class="tab-content">
                                                            <div class="tab-pane active">
                                                                <%--the gridview starts here--%>
                                                               <div style="overflow: auto; height: 600px; text-align:center;" class="texts" >
                                                                   <br />
                                                                   <asp:Label ID="lblreports" runat="server"></asp:Label>
                                                                   <br />
                                                                    <asp:GridView runat="server" ID="gridPromoReports" CellPadding="3" Width="975px" CssClass="auto-style1" style=" text-align:center; overflow-y: auto;max-height: 500px; margin-left: 14px;" 
                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                                  <%-- <asp:GridView runat="server" ID="gridUserLog" class="texts table-responsive table-hover" style=" text-align:center; overflow-y: auto; max-height: 500px; margin-left: 14px;" 
                                                   BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="5" HtmlEncode="false" Width="1850px" CssClass="m-r-0" GridLines="Horizontal" 
                                                   ForeColor="Black" CellSpacing="5" Font-Size="14px">--%>
                                                                 <%-- <asp:GridView runat="server" OnRowDataBound="gridUserLog_RowDataBound" ID="gridUserLog" CellPadding="3" Width="975px" CssClass="auto-style1" 
                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >--%>
                                                                        
                                                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                        </asp:GridView>
                                                                   <asp:GridView runat="server" ID="gridRewardReport" CellPadding="3" Width="975px" CssClass="auto-style1" style=" text-align:center; overflow-y: auto;max-height: 500px; margin-left: 14px;" 
                                                                        SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                                                                 
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


                                                               <%--Gridview ends here--%>

                                                              </div><!--/tab-pane-->
                                                          </div><!--/tab-content-->
                                                            <%--TAB end --%>
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
