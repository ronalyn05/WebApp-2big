<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WalkIns.aspx.cs" Inherits="WRS2big_Web.Admin.POS" %>

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
                                            <h5 class="m-b-10">WALK - IN ORDERS </h5>
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
                        <!-- Page-header end -->
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="page-wrapper">
                                    <center>
                                    <asp:Label ID="warning" runat="server" style="font-size:18px;color:red;" class="danger"></asp:Label> <br /><br /><br />
                                    </center>

                                    <!-- page content -->
                                    <div class="right_col" role="main">
                                        <div class="">

                                            <div class="col-xl-9 col-md-12">
                                                <div class="card">
                                                    <div class="card-header">
                                                        <asp:Label ID="Label2" runat="server" Text="CREATE NEW ORDERS FOR WALK IN" ForeColor="Black" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                        <%-- <h5>CREATE NEW ORDERS FOR WALK IN</h5>--%>
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
                                                                        <div class="col-xs-12" style="font-size: 16px">
                                                                            <h5>Order Type: </h5>
                                                                            <asp:DropDownList ID="drdOrderType" runat="server" class="text-center" Height="40px" Width="364px" Font-Size="18px">
                                                                                <%--  <asp:ListItem Selected="True" Text="Select"></asp:ListItem>--%>
                                                                                <asp:ListItem Text="Refill" Value="Refill"></asp:ListItem>
                                                                                <asp:ListItem Text="Other Product" Value="other Product"></asp:ListItem>
                                                                                <asp:ListItem Text="Third Party Products" Value="Third Party Products"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:Button ID="btnSearch" runat="server" class="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                                                            <br />
                                                                            <asp:Label ID="lblDescription" runat="server" Height="40px" Width="564px" Font-Size="18px" Font-Bold="true"></asp:Label>
                                                                            <h5>Product Name: </h5>
                                                                            <asp:DropDownList ID="drdProdName" runat="server" class="text-center" Height="40px" Width="364px">
                                                                                <asp:ListItem Selected="True" Text="Select product"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <br />
                                                                            <h5>Product Unit and Number of Volume: </h5>
                                                                            <asp:DropDownList ID="drdUnit_Size" runat="server" class="text-center" Height="40px" Width="364px">
                                                                                <asp:ListItem Selected="True" Text="Select Size and Number of Volume"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <%-- <asp:TextBox  ID="txtprodSize" class="text-center" runat="server" Height="40px" Width="564px" Font-Size="14px"></asp:TextBox>--%>
                                                                            <%-- <asp:Label ID="lblUnit" runat="server" BorderColor="Black" BorderWidth="1" class="text-center" Height="40px" Width="564px" Font-Size="18px"></asp:Label>--%>
                                                                            <%--<br />
                                                                            <h5>Product Volume/Size: </h5>
                                                                            <asp:DropDownList ID="drdSize" runat="server" class="text-center" Height="40px" Width="364px" >
                                                                                <asp:ListItem Selected="True" Text="Select Size"></asp:ListItem>
                                                                       </asp:DropDownList>--%>
                                                                            <br />
                                                                            <%-- <asp:TextBox  ID="txtprodSize" class="text-center" runat="server" Height="40px" Width="564px" Font-Size="14px"></asp:TextBox>--%>
                                                                            <%--<asp:Label ID="lblSize" runat="server" BorderColor="Black" BorderWidth="1" class="text-center" Height="40px" Width="564px" Font-Size="18px"></asp:Label>--%>

                                                                            <%-- <asp:TextBox  ID="txtprodName" runat="server"  class="btn btn-round waves-effect text-center" style="background-color:#bae1ff;font-size:16px;color:black;font-family:Bahnschrift;width:700px"></asp:TextBox>--%>



                                                                            <h5>Product Price: </h5>
                                                                            <%-- <asp:TextBox  ID="txtprice" type="number" step="0.01" min="0" pattern="\d+(\.\d{2})?" runat="server" Height="40px" Width="564px" Font-Size="14px"></asp:TextBox>--%>
                                                                            <div style="display: flex; align-items: center;">
                                                                                <asp:Label ID="lblprice" runat="server" BorderColor="Black" Placeholder="product price" BorderWidth="1" class="text-center" Height="40px" Width="200px" Font-Size="18px"></asp:Label>
                                                                                <asp:Button ID="btnSearchPrice" runat="server" class="btn btn-success" Text="Search Price" OnClick="btnSearchPrice_Click" Style="margin-left: 10px;" />
                                                                            </div>
                                                                            <br />
                                                                            <h5>Product Discount: </h5>
                                                                            <asp:Label ID="lblDiscount" runat="server" Placeholder="product discount" BorderColor="Black" BorderWidth="1" class="text-center" Height="40px" Width="364px" Font-Size="18px"></asp:Label>
                                                                            <%--<asp:TextBox  ID="txtDiscount" runat="server" class="text-center" Height="40px" Width="564px" Font-Size="14px"></asp:TextBox>--%>
                                                                            <br />
                                                                            <h5>Product Quantity: </h5>
                                                                            <asp:TextBox ID="txtQty" Placeholder="Enter quantity" TextMode="Number" ForeColor="Black" runat="server" class="text-center" Height="40px" Width="364px" Font-Size="18px"></asp:TextBox>
                                                                            <br />
                                                                            <h5>Total Amount: </h5>
                                                                            <%--  <asp:TextBox  ID="txtTotalAmount" type="number" step="0.01" min="0" pattern="\d+(\.\d{2})?" runat="server"  Height="40px" Width="364px"></asp:TextBox>--%>
                                                                            <asp:Label ID="lblAmount" Placeholder="Total amount of order" runat="server" BorderColor="Black" BorderWidth="1" class="text-center" Height="40px" Width="364px" ForeColor="Green" Font-Size="18px" Font-Bold="True"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <!--/tab-pane-->
                                                                <asp:Button ID="PaymentBtn" Style="font-size: 14px;" class="btn btn-success btn-sm" runat="server" Text="PROCESS PAYMENT" OnClick="btnPayment_Click" />
                                                                <asp:Button ID="btnClear" Style="font-size: 14px;" class="btn btn-danger btn-sm" runat="server" Text="Clear All" OnClick="btnClear_Click" />
                                                            </div>
                                                            <!--/tab-content-->

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <!-- /page content -->
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
