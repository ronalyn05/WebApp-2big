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
                                    <div class="container">
                                      <%--  <h1 class="mt-5 mb-5">Review & Rating </h1>--%>
                                        <div class="card">
                                            <div class="card-header"><h4>SET YOUR PROMO OFFERED HERE</h4>
                                            </div> 
                                            <div class="card-body">
                                                <div class="row">
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
                                                    <div class="col-md-12 col-sm-12">
                                              <h5>How would you like your customer to earn points?</h5>
                                              <asp:RadioButtonList ID="radioCusEarnPoints" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Per transaction" Value="per_transaction" />
                                                <asp:ListItem Text="Per amount" Value="per_amount" />
                                              </asp:RadioButtonList>
                                              <div id="pointsInput">
                                                  <h5>Points to earn:</h5>
                                                <%--<label for="points">Points:</labe>l>--%>
                                                <asp:TextBox ID="txtpointsPerTxnOrAmount" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Enter points to earn by the customer per transaction or per amount"></asp:TextBox>
                                              </div>
                                                  <div>
                                                  <h5>Minimum range amount (applies for per amount only):</h5>
                                                <%--<label for="points">Points:</labe>l>--%>
                                                <asp:TextBox ID="txtrange_perAmount" runat="server" CssClass="form-control" TextMode="Number" placeholder="Enter the minimum range amount"></asp:TextBox>
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
                                                     </div> 
                                                 <br />
                                                 <div class="modal-footer">
                                               <%-- add data button--%>
                                                     <script>
                                                         function disableButton() {
                                                             document.getElementById("btnAdd").disabled = true;
                                                         }
                                                     </script>

                                                    <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" Text="Submit" ValidationGroup="a" OnClick="btnAddReward_Click" OnClientClick="disableButton()" />

                                                    <%-- <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" Text="Submit" ValidationGroup="a" OnClick="btnAddReward_Click" />--%>
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
