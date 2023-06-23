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
                                    <!-- Page-body start -->
                                    <button type="button" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".addreward"><i class="fa fa-plus"></i>Set reward system </button>
                                    <button type="button" style="font-size: 14px;" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".setpromo"><i class="fa fa-plus"></i>Set discount coupon offered </button>
                                    <br />
                                    <%-- MODAL FOR REWARD SYSTEM--%>
                                    <div class="modal fade addreward" tabindex="-1" role="dialog" aria-hidden="true">
                                        <div class="modal-dialog modal-dialog-centered modal-lg">
                                            <div class="modal-content">
                                                <form id="demo-form" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                    <div class="modal-header">
                                                        <h4 class="modal-title" id="myModalLabel">REWARD SYSTEM</h4>
                                                        <%--exit button--%>
                                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="col-md-12 col-sm-12 ">
                                                            <div class="x_content">
                                                                <h4 style="color: black; font-family: Bahnschrift">Set your reward system here:</h4>
                                                                <hr />
                                                                <div class="col-md-12 col-sm-12">
                                                                    <h5>How would you like your customer to earn their reward points?</h5>
                                                                    <asp:RadioButtonList ID="radioWaysToEarnPoints" runat="server" RepeatDirection="Horizontal" onclick="disableMinMaxRange()">
                                                                        <asp:ListItem Text="Per transaction" Value="per transaction" />
                                                                        <asp:ListItem Text="Per amount" Value="per amount" />
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div id="pointsInput" class="col-md-12 col-sm-12">
                                                                    <h5>Points to earn:</h5>
                                                                    <%--<label for="points">Points:</labe>l>--%>
                                                                    <asp:TextBox ID="txtrewardspointsPerTxnOrAmount" runat="server" CssClass="form-control" TextMode="Number" step="any" placeholder="Enter the reward points to earn by the customer per transaction or per amount"></asp:TextBox>
                                                                    <asp:RangeValidator ID="rangeValidator4" runat="server" ControlToValidate="txtrewardspointsPerTxnOrAmount" Type="Double" MinimumValue="0" ErrorMessage="input a number value only" ForeColor="Red"></asp:RangeValidator>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12">
                                                                    <h5>Minimum range per amount (applies for per amount only):</h5>
                                                                    <asp:TextBox ID="txtminRange_perAmount" runat="server" CssClass="form-control" TextMode="Number" step="any" placeholder="Enter the minimum range amount"></asp:TextBox>
                                                                    <asp:RangeValidator ID="rangeValidator2" runat="server" ControlToValidate="txtminRange_perAmount" Type="Double" MinimumValue="0" ErrorMessage="input a number value only" ForeColor="Red"></asp:RangeValidator>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12">
                                                                    <h5>Maximum range per amount (applies for per amount only):</h5>
                                                                    <%--<label for="points">Points:</labe>l>--%>
                                                                    <asp:TextBox ID="txtmaxRange_perAmount" runat="server" CssClass="form-control" TextMode="Number" step="any" placeholder="Enter the maximum range amount"></asp:TextBox>
                                                                    <asp:RangeValidator ID="rangeValidator3" runat="server" ControlToValidate="txtmaxRange_perAmount" Type="Double" MinimumValue="0" ErrorMessage="input a number value only" ForeColor="Red"></asp:RangeValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="modal-footer">
                                                            <%-- set data button--%>
                                                            <asp:Button ID="btnSet" runat="server" Text="Add Reward" class="btn btn-primary btn-sm" OnClick="btnAddReward_Click" />
                                                            <br />
                                                        </div>
                                                    </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- end set REWARD SYSTEM --%>
                                <script>
                                     function disableMinMaxRange() {
                                         
                                         var pointsCheckbox = document.querySelector("#<%= radioWaysToEarnPoints.ClientID %> input:checked");
                                                                    var minrangeAmount = document.querySelector("#<%= txtminRange_perAmount.ClientID %>");
                                                                    var maxRangeAmount = document.querySelector("#<%= txtmaxRange_perAmount.ClientID %>");
                                         if (pointsCheckbox && pointsCheckbox.value === "per transaction") {
                                             minrangeAmount.disabled = true;
                                             maxRangeAmount.disabled = true;
                                         } else if (pointsCheckbox && pointsCheckbox.value === "per amount") {
                                             minrangeAmount.disabled = false;
                                             maxRangeAmount.disabled = false;
                                         }
                                     }
                                </script>
                                <%-- MODAL FOR PROMO OFFERED--%>
                                <div class="modal fade setpromo" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog modal-dialog-centered modal-lg">
                                        <div class="modal-content">
                                            <form id="demo-form1" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                <div class="modal-header">
                                                    <h4 class="modal-title" id="myModalLabel1">DISCOUNT COUPON</h4>
                                                    <%--exit button--%>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="col-md-12 col-sm-12 ">
                                                        <div class="x_content">
                                                            <h4 style="color: black; font-family: Bahnschrift">Set your discount coupon offered here:</h4>
                                                            <hr />
                                                            <div class="col-md-12 col-sm-12 ">
                                                                <h5>Coupon Name:</h5>
                                                                <asp:TextBox ID="txtpromoname" runat="server" ToolTip="eg: 10% discount coupon" class="form-control" placeholder="Enter discount coupon offered (Ex:10% discount coupon )"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*** required ***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtpromoname" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 ">
                                                                <h5>Coupon Discount Value :</h5>
                                                                <asp:TextBox ID="txtpromoDiscountValue" runat="server" TextMode="Number" class="form-control" placeholder="Enter discount percentage in number base on the coupon you offered"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*** required ***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtpromoDiscountValue" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                <asp:RangeValidator ID="rangeValidator" runat="server" ControlToValidate="txtpromoDiscountValue" Type="Double" MinimumValue="0" ErrorMessage="input a number value only" ForeColor="Red" ValidationGroup="a"></asp:RangeValidator>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 ">
                                                                <h5>Coupon Description:</h5>
                                                                <asp:TextBox ID="txtpromodescription" runat="server" ToolTip="eg: Get 10% off on your next purchase " class="form-control" placeholder="Enter coupon description"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*** required ***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtpromodescription" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <br />
                                                            <div class="col-md-12 col-sm-12 ">
                                                                <h5>Choose any product offers that applies to the coupon you offered:</h5>
                                                                <asp:CheckBoxList ID="checkPromo_productOffered" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                                                    <asp:ListItem Text="Product Refill" Value="Product Refill" ID="pro_refillRadio"></asp:ListItem>
                                                                    <asp:ListItem Text="other Product (in-store)" Value="other Product" ID="otherproductRadio"></asp:ListItem>
                                                                    <asp:ListItem Text="Third Party Product" Value="Third Party Product" ID="thirdPartyproductRadio"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <br />
                                                            <div class="col-md-12 col-sm-12 ">
                                                                <h5>Choose any unit and sizes of product refill offers that applies to the coupon you offered:</h5>
                                                                <asp:CheckBoxList ID="chUnitSizes_proRefill" runat="server" RepeatDirection="Horizontal">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <br />
                                                            <div class="col-md-12 col-sm-12 ">
                                                                <h5>Choose any unit and sizes of other product offers that applies to the coupon you offered:</h5>
                                                                <asp:CheckBoxList ID="chUnitSizes_otherProduct" runat="server" RepeatDirection="Horizontal">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 ">
                                                                <h5>Choose any unit and sizes of third party product offers that applies to the coupon you offered:</h5>
                                                                <asp:CheckBoxList ID="chUnitSizes_thirdparty" runat="server" RepeatDirection="Horizontal">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 ">
                                                                <h5>Points required to claim the discount coupon:</h5>
                                                                <asp:TextBox ID="txtpromo_pointsToClaimReward" runat="server" TextMode="Number" class="form-control" placeholder="Enter points required to claim the discount coupon"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="***required***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtpromo_pointsToClaimReward" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                <asp:RangeValidator ID="rangeValidator1" runat="server" ControlToValidate="txtpromo_pointsToClaimReward" Type="Double" MinimumValue="0" ErrorMessage="input a number value only" ForeColor="Red" ValidationGroup="a"></asp:RangeValidator>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 ">
                                                                <h5>Discount coupon expiration:</h5>
                                                                <br />
                                                                <strong>From:</strong>
                                                                <asp:TextBox ID="txtpromoExpirationFrom" TextMode="Date" runat="server"></asp:TextBox>
                                                                <strong>To:</strong>
                                                                <asp:TextBox ID="txtpromoExpirationTo" TextMode="Date" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="***required***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtpromoExpirationTo" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <br />
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <%-- add data button--%>
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add Promo Offered" class="btn btn-primary btn-sm" ValidationGroup="a" OnClick="btnAddPromoOffered_Click" />
                                                    </div>
                                                </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- end set  for promo offered --%>
                                <!--PAGE CONTENTS-->
                                <div class="col-xl-12 col-xl-12 h-100">
                                    <div class="card">
                                        <%--<div class="card" style="background-color:#f2e2ff">--%>
                                        <div class="card-header">
                                            <asp:Label ID="Label1" runat="server" Text="DISCOUNT COUPON & REWARD REPORTS" ForeColor="Black" Font-Size="Large" Font-Bold="True"></asp:Label>
                                            <hr />
                                            <div style="float: right;">
                                                <asp:TextBox ID="txtSearch" Width="364px" Placeholder="search by coupon name " ToolTip="enter promo name to view record" Height="40px" runat="server"></asp:TextBox>
                                                <asp:Button ID="btnSearchReports" runat="server" Text="Search Coupon" OnClick="btnSearchReports_Click" CssClass="btn-primary" Height="40px" />
                                            </div>
                                            <div style="float: left;">
                                                <asp:TextBox ID="txtSearchReward" Width="450px" Placeholder="search using per transaction or per amount (small letters only) " ToolTip="enter per transaction or per amount to view record " Height="40px" runat="server"></asp:TextBox>
                                                <asp:Button ID="btnSearchRewards" runat="server" Text="Search Reward" OnClick="btnSearchReward_Click" CssClass="btn-primary" Height="40px" />
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
                                                            <h4 class="modal-title" id="myModalLabel3">REWARD SYSTEM REPORTS: 
                                
                                                            </h4>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="col-xl-12 col-xl-12 ">
                                                                <div class="x_content">
                                                                    <div class="card-block">
                                                                        <div class="table-responsive">
                                                                            <div class="tab-content">
                                                                                <div class="tab-pane active">
                                                                                    <%--the gridview starts here--%>
                                                                                    <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                                        <asp:Label ID="lblMessageError" runat="server" Font-Underline="true" ForeColor="red" />
                                                                                        <br />
                                                                                        <%--  GRID REWARD SYSTEM OFFERED--%>
                                                                                        <asp:GridView runat="server" ID="gridReward" CellPadding="3" Width="975px" CssClass="auto-style1" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                            SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">

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
                                                                                    <!--/tab-pane-->
                                                                                </div>
                                                                                <!--/tab-content-->
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
                                        <%-- MODAL TO VIEW PROMO OFFERED CERTAIN RECORDS --%>
                                        <div class="modal fade" id="viewPromo" tabindex="-1" role="dialog" aria-hidden="true">
                                            <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
                                                <div class="modal-content">
                                                    <form id="demo-form4" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                        <div class="modal-header">
                                                            <h4 class="modal-title" id="myModalLabel4">DISCOUNT COUPON REPORTS: </h4>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="col-xl-12 col-xl-12 ">
                                                                <div class="x_content">
                                                                    <div class="card-block">
                                                                        <div class="table-responsive">
                                                                            <div class="tab-content">
                                                                                <div class="tab-pane active">
                                                                                    <%--the gridview starts here--%>
                                                                                    <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                                                        <asp:Label ID="Label2" runat="server" Font-Underline="true" ForeColor="red" />
                                                                                        <br />
                                                                                        <%--  GRID PROMO OFFERED--%>
                                                                                        <asp:GridView runat="server" ID="gridPromoOffered" CellPadding="3" Width="975px" CssClass="auto-style1" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                                                            SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">

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
                                                                                </div>
                                                                            </div>
                                                                            <!--/tab-pane-->
                                                                        </div>
                                                                        <!--/tab-content-->
                                                                        <%--TAB end --%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-block">
                                        <asp:DropDownList ID="ddlSearchOptions" runat="server" CssClass="text-center" Height="40px" Width="364px">
                                            <asp:ListItem Text="---View All Reports -----" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Reward System" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Discount Coupon Offered" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Button ID="btnDisplayReports" runat="server" Text="Search" OnClick="btnDisplayReports_Click" CssClass="btn-primary" Height="40px" />
                                        <br />
                                        <div class="table-responsive">
                                            <div class="tab-content">
                                                <div class="tab-pane active">
                                                    <%--the gridview starts here--%>
                                                    <div style="overflow: auto; height: 600px; text-align: center;" class="texts">
                                                        <asp:Label ID="lblMessage" runat="server" Font-Underline="true" ForeColor="red" />
                                                        <br />
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblreports" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                        </div>
                                                        <br />
                                                        <asp:GridView runat="server" ID="gridPromoReports" CellPadding="3" Width="975px" CssClass="auto-style1" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                            SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">

                                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="Black" />
                                                            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
                                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                                        </asp:GridView>
                                                        <br />
                                                        <asp:Label ID="lblReward" Font-Bold="true" Font-Size="20px" runat="server" Width="364px"></asp:Label>
                                                        <asp:GridView runat="server" ID="gridRewardReport" CellPadding="3" Width="975px" CssClass="auto-style1" Style="text-align: center; overflow-y: auto; max-height: 500px; margin-left: 14px;"
                                                            SelectionMode="FullRow" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">

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
                                                </div>
                                                <!--/tab-pane-->
                                            </div>
                                            <!--/tab-content-->
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
    <%-- </div>
    </div>--%>
</asp:Content>

