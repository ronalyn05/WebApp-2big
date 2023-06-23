<%@ Page Title="" Language="C#" MasterPageFile="~/WRSsuperAdmin.Master" AutoEventWireup="true" CodeBehind="clientDetails.aspx.cs" Inherits="WRS2big_Web.superAdmin.clientDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .texts {
            font-size: 16px;
            color: black;
        }
        .text {
            font-size: 20px;
            color: black;
        }

        .scrollable-listbox {
            height: 200px;
            overflow-y: auto;
        }

        .image-grid {
            display: flex;
            flex-wrap: wrap;
            justify-content: flex-start;
        }

        .image-item {
            flex-basis: 50%;
            padding: 2px;
            box-sizing: border-box;
        }

        .image-container {
            display: flex;
            justify-content: flex-start;
            align-items: center;
            gap: 10px;
            flex-wrap: wrap;
        }

            .image-container img {
                width: 350px;
                height: 350px;
                border-color: black;
            }
    </style>


    <script src="https://mozilla.github.io/pdf.js/build/pdf.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--   <form runat="server">--%>
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
                                            <h5 class="m-b-10">REFILLING STATION CLIENTS</h5>
                                            <h6 class="m-b-0">2BiG: Water Refilling Station Management System</h6>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="breadcrumb-title">
                                            <li class="breadcrumb-item">
                                                <a href="/superAdmin/SAdminIndex"><i class="fa fa-home"></i></a>
                                            </li>
                                            <li class="breadcrumb-item">
                                                <a href="/superAdmin/ManageWRSClients">Client List</a>
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
                                                <div class="modal fade declineClient" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-dialog-centered modal-md">
                                                        <div class="modal-content">
                                                            <div id="demo-form3" data-parsley-validate="" class="form-horizontal form-label-left" novalidate="">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title" id="declineModal"></h4>
                                                                    <%--exit button--%>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span> </button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-12 col-sm-12 ">
                                                                        <div class="x_content">
                                                                            <%-- <div class="item form-group">--%>
                                                                            <h4 style="color: black; font-family: Bahnschrift">DECLINE CLIENT</h4>

                                                                            <div class="col-md-12 col-sm-12" style="font-size: 20px">
                                                                                <br />
                                                                                <center>
                                                                                    <h4 style="font-size: 16px; color: black">Why are you declining this client?</h4>
                                                                                    <p style="font-size: 16px; color: black;">"Provide more information on why do you want to decline this client. Your client will be notified about this action."</p>
                                                                                </center>

                                                                                <hr />
                                                                                <asp:TextBox runat="server" class="form-control"  ValidateRequest="false" TextMode="MultiLine" Style="font-size: 18px" Width="400px" ID="reasonDecline" Placeholder="Reason for declining"></asp:TextBox>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <%--  BUTTON CONFIRM DECLINE--%>
                                                                        <asp:Button ID="confirmDecline" runat="server" Text="Confirm" class="btn btn-primary btn-sm" OnClick="declineButton_Click" />
                                                                    </div>
                                                                </div>
<%--                                                                <script>
                                                                    // Check for the error query string parameter
                                                                    var errorParam = '<%= Request.QueryString["error"] %>';

                                                                    // Display an alert if the error parameter is present
                                                                    if (errorParam === '1') {
                                                                        alert('Invalid Input!');
                                                                    }
                                                                </script>--%>

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
                                                            <h5>CLIENT DETAILS</h5>
                                                        </div>
                                                        <div class="card-block tab-icon">
                                                            <!-- Row start -->
                                                            <div class="row">
                                                                <div class="col-lg-12 col-xl-12">
                                                                    <center>
                                                                        <asp:Image runat="server" ID="clientImage" class="img-200 img-radius" Style="width: 350px" />
                                                                        <br />
                                                                        <br />
                                                                        <asp:Label class="form-control-round" Style="font-size: 20px; color: black;" ID="clientFullName" runat="server"></asp:Label>
                                                                        <br />
                                                                        <asp:Label class="form-control-round" runat="server" ID="clientEmail" Style="font-size: 16px; color: dimgray;"></asp:Label>
                                                                        <br />
                                                                        <br />
                                                                        <asp:Label class="form-control-round" runat="server" ID="Label23" Style="font-size: 16px; color: dimgray;">Status:</asp:Label>
                                                                        <br />
                                                                        <asp:Label class="form-control-round" runat="server" ID="clientStatus" Style="font-size: 20px; color: black;"></asp:Label>
                                                                    </center>

                                                                    <asp:LinkButton runat="server" Text="All Clients" class="btn btn-primary" Style="font-size: 18px" href="RefillingStationReports.aspx"></asp:LinkButton>


                                                                    <asp:LinkButton runat="server" Text="Pending Clients" class="btn btn-primary" Style="font-size: 18px; margin-left: 40px" href="ManageWRSClients.aspx"></asp:LinkButton>
                                                                    <br />
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
                                                                                                <div style="background-color: #018cff; color: white" class="card card-block">
                                                                                                    <h7>PERSONAL INFORMATION</h7>
                                                                                                </div>
                                                                                                <div class="form-material" style="margin-left: 100px">
                                                                                                    <div class="form-group row text">
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size: 18px; color: black">Firstname:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left: 70px">
                                                                                                            <asp:Label class="form-control-round" ID="firstName" runat="server"></asp:Label>
                                                                                                        </div>

                                                                                                        <label class="col-sm-5 col-form-label" style="font-size: 18px; color: black">Middlename:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left: 70px">
                                                                                                            <asp:Label class="form-control-round" ID="middleName" runat="server"></asp:Label>
                                                                                                        </div>

                                                                                                        <label class="col-sm-5 col-form-label" style="font-size: 18px; color: black">Lastname:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left: 70px">
                                                                                                            <asp:Label class="form-control-round" ID="lastName" runat="server"></asp:Label>
                                                                                                        </div>

                                                                                                        <label class="col-sm-5 col-form-label" style="font-size: 18px; color: black">Birthdate:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left: 70px">
                                                                                                            <asp:Label class="form-control-round" ID="clientBirthDate" runat="server"></asp:Label>
                                                                                                        </div>

                                                                                                        <label class="col-sm-5 col-form-label" style="font-size: 18px">Contact number:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left: 70px">
                                                                                                            <asp:Label class="form-control-round" ID="clientPhone" runat="server"></asp:Label>
                                                                                                        </div>


                                                                                                    </div>
                                                                                                </div>

                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-xl-6 col-md-12">
                                                                                            <div class="card-block">
                                                                                                <div style="background-color: #018cff; color: white" class="card card-block">
                                                                                                    <h7>REFILLING STATION INFORMATION</h7>
                                                                                                </div>
                                                                                                <div class="form-material" style="margin-left: 100px">

                                                                                                    <div class="form-group row text">
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size: 18px">Refilling Station Name:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left: 70px">
                                                                                                            <asp:Label class="form-control-round" ID="clientStationName" runat="server"></asp:Label>
                                                                                                        </div>

                                                                                                        <label class="col-sm-5 col-form-label " style="font-size: 18px">Refilling Station Address:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left: 70px">
                                                                                                            <asp:Label class="form-control-round" ID="clientStationAdd" runat="server"></asp:Label>
                                                                                                        </div>

                                                                                                        <label class="col-sm-5 col-form-label " style="font-size: 18px">Proof of Business:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left: 70px">
                                                                                                            <asp:Label class="form-control-round" ID="chosenProof" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                        <label class="col-sm-5 col-form-label" style="font-size: 18px">Valid ID:</label>
                                                                                                        <div class="col-sm-10 form-control-round" style="margin-left: 70px">
                                                                                                            <asp:Label class="form-control-round" ID="chosenValidID" runat="server"></asp:Label>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                </div>

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <!-- Tab variant tab card start -->
                                                                        </div>
                                                                    </div>
                                                                    <label class="col-sm-5 col-form-label" style="font-size: 18px; color: black">Uploaded Business Proof and Valid ID:</label>
                                                                    <div class="row">
                                                                        <asp:Label runat="server" Style="font-size: 16px; color: red; margin-left: 700px" ID="noUploaded"> </asp:Label>
                                                                        <div class="image-container">
                                                                            <asp:Repeater ID="uploadedImages" runat="server">
                                                                                <ItemTemplate>
                                                                                    <a href='<%# Container.DataItem %>' target="_blank">
                                                                                        <img id="ImageItem" runat="server" src='<%# Container.DataItem %>' />
                                                                                    </a>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </div>

                                                                    <center>
                                                                        <br />
                                                                        <br />
                                                                        <asp:Button runat="server" ID="approveButton" class="btn btn-primary btn-sm text" Style="font-size: 18px" OnClick="approveButton_Click" Text="APPROVE" />
                                                                        <button type="button" runat="server" id="declineButton" style="font-size: 18px;" class="btn btn-primary btn-sm text" data-toggle="modal" data-target=".declineClient">DECLINE</button>
                                                                        <br />
                                                                        <br />
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
    <%--</form>--%>
</asp:Content>
