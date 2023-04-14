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
                                            <div class="card-header"><h4>2BIG LOYALTY PROMO</h4>
                                            </div> 
                                            <div class="card-body">
                                                <div class="row">
                                                   <div class="col-md-12 col-sm-12 ">
                                            <h5>Promo Name:</h5>
                                            <asp:TextBox ID="txtrewardname" runat="server" ToolTip="Note: must enter promo in percentage" class="form-control" placeholder="Enter reward promo offered (Ex: 10% discount coupon)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqlname" runat="server" ErrorMessage="***required***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtrewardname" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                                     <div class="col-md-12 col-sm-12 ">
                                            <h5>Description:</h5>
                                            <asp:TextBox ID="txtdescription" runat="server" ToolTip="eg: Get 10% off on your next purchase " class="form-control" placeholder="Enter promo description"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="***required***" ForeColor="Red" Font-Bold="true" ControlToValidate="txtdescription" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                             <div class="col-md-12 col-sm-12 ">
                                            <h5>Points Required:</h5>
                                            <asp:TextBox ID="txtpointsrequired" runat="server" TextMode="Number" class="form-control" placeholder="Enter promo points required "></asp:TextBox>
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
                                               <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" OnClientClick="confirmAdd();" AutoPostback="False" Text="Submit" ValidationGroup="a" OnClick="btnAdd_Click" />
                                                     <input type="hidden" id="confirm_add" name="confirm_add" value="" />
                                               </div>
                                               <script type="text/javascript">
                                                        function confirmAdd() {
                                                            var confirmValue = confirm("Are you sure you want to add the data? Make sure your promo is final and no further changes as you can only set your promo once until the expiration");
                                                            document.getElementById('confirm_add').value = confirmValue ? 'Yes' : 'No';
                                                        }
                                               </script>

                                                   <%-- <div class="col-md-12 text-center">
                                                        <h1 class="text-warning mt-4 mb-4">
                                                            <b><span id="average_rating">0.0</span> / 5</b>
                                                        </h1>
                                                        <div class="mb-3">
                                                            <i class="fas fa-star star-light mr-1 main_star"></i>
                                                            <i class="fas fa-star star-light mr-1 main_star"></i>
                                                            <i class="fas fa-star star-light mr-1 main_star"></i>
                                                            <i class="fas fa-star star-light mr-1 main_star"></i>
                                                            <i class="fas fa-star star-light mr-1 main_star"></i>
                                                        </div>
                                                        <h3><span id="total_review">0</span> Review</h3>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <p>
                                                            <div class="progress-label-left"><b>5</b> <i class="fas fa-star text-warning"></i></div>

                                                            <div class="progress-label-right">(<span id="total_five_star_review">0</span>)</div>
                                                            <div class="progress">
                                                                <div class="progress-bar bg-warning" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" id="five_star_progress"></div>
                                                            </div>
                                                        </p>
                                                        <p>
                                                            <div class="progress-label-left"><b>4</b> <i class="fas fa-star text-warning"></i></div>

                                                            <div class="progress-label-right">(<span id="total_four_star_review">0</span>)</div>
                                                            <div class="progress">
                                                                <div class="progress-bar bg-warning" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" id="four_star_progress"></div>
                                                            </div>
                                                        </p>
                                                        <p>
                                                            <div class="progress-label-left"><b>3</b> <i class="fas fa-star text-warning"></i></div>

                                                            <div class="progress-label-right">(<span id="total_three_star_review">0</span>)</div>
                                                            <div class="progress">
                                                                <div class="progress-bar bg-warning" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" id="three_star_progress"></div>
                                                            </div>
                                                        </p>
                                                        <p>
                                                            <div class="progress-label-left"><b>2</b> <i class="fas fa-star text-warning"></i></div>

                                                            <div class="progress-label-right">(<span id="total_two_star_review">0</span>)</div>
                                                            <div class="progress">
                                                                <div class="progress-bar bg-warning" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" id="two_star_progress"></div>
                                                            </div>
                                                        </p>
                                                        <p>
                                                            <div class="progress-label-left"><b>1</b> <i class="fas fa-star text-warning"></i></div>

                                                            <div class="progress-label-right">(<span id="total_one_star_review">0</span>)</div>
                                                            <div class="progress">
                                                                <div class="progress-bar bg-warning" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" id="one_star_progress"></div>
                                                            </div>
                                                        </p>
                                                    </div>--%>

                                                </div>
                                            </div>
                                        </div>
                                       <%-- <div class="mt-5"></div>--%>
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
