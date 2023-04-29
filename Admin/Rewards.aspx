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
                                            <div class="card-header"><h4>SET YOUR REWARD OFFERED HERE</h4>
                                            </div> 
                                            <div class="card-body">
                                                <div class="row">
                                                     <div class="col-md-12 col-sm-12 ">
                                            <h5>Promo Type:</h5>
                                            <asp:TextBox ID="txtrewardname" runat="server" ToolTip="eg: discount coupon or buy 1 take 1" class="form-control" placeholder="Enter reward promo offered (Ex: 10% discount coupon)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="***required***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtrewardname" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                                   <div class="col-md-12 col-sm-12 ">
                                            <h5>Promo Value:</h5>
                                            <asp:TextBox ID="txtrewardValue" runat="server" TextMode="Number" class="form-control" placeholder="Enter promo value in percentage or in number"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqlname" runat="server" ErrorMessage="***required***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtrewardValue" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                                     <div class="col-md-12 col-sm-12 ">
                                            <h5>Description:</h5>
                                            <asp:TextBox ID="txtdescription" runat="server" ToolTip="eg: Get 10% off on your next purchase " class="form-control" placeholder="Enter promo description"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="***required***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtdescription" ValidationGroup="a"></asp:RequiredFieldValidator>
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
                                                        <%--<asp:ListItem Text=""  ID="proUnitSizes" ></asp:ListItem>--%>
                                                    </asp:CheckBoxList>
                                            </div>
                                                    <div class="col-md-12 col-sm-12 ">
                                                  <h5>How would you like your customer to earn points?</h5>
                                                    <asp:CheckBoxList ID="check_cusEarnPoints" runat="server" AutoPostBack="false">
                                                        <asp:ListItem Text="per transaction" Value="per transaction" ></asp:ListItem>
                                                        <asp:ListItem Text="per amount" Value="other Product" ></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                         <div id="pointsInput" style="display: none;">
                                                        <label for="points">Points:</label>
                                                         <asp:TextBox ID="txtpoints" runat="server" class="form-control" placeholder="Enter points"></asp:TextBox>
                                                      </div>
                                            </div>
                                                    <script>
                                                        var checkBoxList = document.getElementById("check_cusEarnPoints");
                                                        var pointsInput = document.getElementById("pointsInput");
                                                        var pointsInputField = document.getElementById("points");

                                                        checkBoxList.addEventListener("change", function () {
                                                            if (checkBoxList.value === "per transaction" || checkBoxList.value === "per amount") {
                                                                pointsInput.style.display = "block";
                                                                pointsInputField.required = true;
                                                            } else {
                                                                pointsInput.style.display = "none";
                                                                pointsInputField.required = false;
                                                            }
                                                        });
                                                    </script>

                                             <div class="col-md-12 col-sm-12 ">
                                            <h5>Points Required:</h5>
                                            <asp:TextBox ID="txtpointsrequired" runat="server" TextMode="Number" class="form-control" placeholder="Enter promo points required per transaction or per amount"></asp:TextBox>
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
                                               <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" OnClientClick="confirmAdd();" Text="Submit" ValidationGroup="a" OnClick="btnAddReward_Click" />
                                                     <input type="hidden" id="confirm_add" name="confirm_add" value="" />
                                               </div>
                                               <script type="text/javascript">
                                                        function confirmAdd() {
                                                            var confirmValue = confirm("Are you sure you want to add the data? Make sure your promo is final and no further changes as you can only set your promo once until the expiration");
                                                            document.getElementById('confirm_add').value = confirmValue ? 'Yes' : 'No';
                                                        }
                                               </script>

                                                  

                                                </div>
                                            </div>
                                        </div>
                                       <%-- <div class="mt-5"></div>--%>
                                  <%--  sa pag set sa discount ni here --%>
                                   <%-- <div class="container">
                                     <div class="card">
                                            <div class="card-header"><h4>SET DISCOUNT OFFERED HERE</h4>
                                            </div> 
                                            <div class="card-body">
                                                <div class="row">
                                                      <div class="col-md-12 col-sm-12 ">
                                                  <h5>Choose any product offers that applies to the discount you offered:</h5>
                                                    <asp:CheckBoxList ID="chckdiscountOffered" runat="server" AutoPostBack="false">
                                                        <asp:ListItem Text="Product Refill" Value="Product Refill" ></asp:ListItem>
                                                        <asp:ListItem Text="other Product" Value="other Product" ></asp:ListItem>
                                                    </asp:CheckBoxList>
                                            </div>
                                                     <div class="col-md-12 col-sm-12 ">
                                                  <h5>Choose any unit below that applies to the discount you offered:</h5>
                                                    <asp:CheckBoxList ID="chckunitOffered" runat="server" AutoPostBack="false">
                                                        <asp:ListItem Text="gallon" Value="gallon" ></asp:ListItem>
                                                        <asp:ListItem Text="liter" Value="liter" ></asp:ListItem>
                                                        <asp:ListItem Text="mL" Value="mL" ></asp:ListItem>
                                                    </asp:CheckBoxList>
                                            </div>
                                                   <div class="col-md-12 col-sm-12 ">
                                            <h5>Discount:</h5>
                                            <asp:TextBox ID="txtdiscount" runat="server" TextMode="Number" ToolTip="Note: must enter discount in percentage (%)" class="form-control" placeholder="Enter discount offered"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="***required***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtrewardname" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                                    
                                                     </div> 
                                                 <br />
                                                 <div class="modal-footer">
                                               <asp:Button ID="Button1" class="btn btn-primary" runat="server" OnClientClick="confirmAdd();" AutoPostback="False" Text="Submit" ValidationGroup="a" OnClick="btnAddDiscount_Click" />
                                                     <input type="hidden" id="confirm_add2" name="confirm_add2" value="" />
                                               </div>
                                                </div>
                                            </div>
                                         </div>--%>
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
