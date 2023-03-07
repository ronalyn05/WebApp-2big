<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="WRS2big_Web.Admin.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script>
window.onload = function() {

var chart = new CanvasJS.Chart("chartContainer", {
  animationEnabled: true,
  title: {
    text: "Sales Category Pie Graph - 2022"
  },
  data: [{
    type: "pie",
    startAngle: 240,
    yValueFormatString: "00",
    indexLabel: "{label} {y}",
    dataPoints: [
      {y: 20, label: "Container"},
      {y: 20, label: "Per Bottles"},
      {y: 70, label: "Accessories"},
    ]
  }]
});
chart.render();

}
        </script>
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
                                            <h5 class="m-b-10">REPORTS </h5>
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
                            <!-- Main-body start -->
                        <div class="row" >
                                  <div class="col-md-10 col-sm-12 " style="margin-left: 7%" >
                                    <div class="x_panel" style="background-color: #eae8e8">
                                      <div class="x_title">
                                        <h2>Sales Report<small></small></h2>
                                        <div class="clearfix"></div>
                                      </div>
                                        <hr />
                                      <div class="x_content">
                                          <div class="row">
                                          <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <h4 class="text-c-purple">₱30,200</h4>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-bar-chart f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card-footer bg-c-purple">
                                                        <div class="row align-items-center">
                                                            <div class="col-9">
                                                                <p class="text-white m-b-0 text-center"> SALES</p>
                                                            </div>
                                                            <div class="col-3 text-right">
                                                                <i class="fa fa-line-chart text-white f-16"></i>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <h4 class="text-c-green">290+</h4>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-file-text-o f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card-footer bg-c-green">
                                                        <div class="row align-items-center">
                                                            <div class="col-9">
                                                                <p class="text-white m-b-0 text-center">DELIVERIES</p>
                                                            </div>
                                                            <div class="col-3 text-right">
                                                                <i class="fa fa-line-chart text-white f-16"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <h4 class="text-c-red">145</h4>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-shopping-cart f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card-footer bg-c-red">
                                                        <div class="row align-items-center">
                                                            <div class="col-9">
                                                                <p class="text-white m-b-0 text-center">ORDERS</p>
                                                            </div>
                                                            <div class="col-3 text-right">
                                                                <i class="fa fa-line-chart text-white f-16"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <h4 class="text-c-blue">500</h4>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-calendar-check-o f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card-footer bg-c-blue">
                                                        <div class="row align-items-center">
                                                            <div class="col-9">
                                                                <p class="text-white m-b-0 text-center">RESERVATIONS</p>
                                                            </div>
                                                            <div class="col-3 text-right">
                                                                <i class="fa fa-line-chart text-white f-16"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                          </div>
                                    <%--<div class="row">
                                  <div class="col-sm-6">
                                      <div class="card-box table-responsive">
                                        <table id="datatable" class="table table-striped table-bordered" style="width:100%">
                                          <thead>
                                            <tr>
                    
                                              <th>Month</th>
                                              <th>Sales</th>
                 
                                            </tr>
                                          </thead>
                                          <tbody>
                                            <tr>
                                              <td>January</td>
                                              <td>25,000</td>
                                            </tr>
                                            <tr>
                                              <td>February</td>
                                              <td>1,000</td>
                                            </tr>
                                           <tr>
                                              <td>March</td>
                                              <td>11,000</td>
                                            </tr>
                                            <tr>
                                              <td>April</td>
                                              <td>1,000</td>
                                            </tr>
                                            <tr>
                                              <td>May</td>
                                              <td>1,000</td>
                                            </tr>
                                            </tbody>
                                            <tbody>
                                            <tr>
                                             <th>Month</th>
                                              <th>Sales</th>
                                            </tr>
                                          </tbody>
                                        </table>
              
                                      </div>
                                    </div> </br>
                                      <div class="col-sm-6">
                                      <div class="card-box table-responsive">
                      <div id="myfirstchart" style="height: 470px;"></div>

                                      </div>
                                    </div>
                                  </div>--%>
                                </div>
                              </div>
                            </div>
                          </div>                        
                        

<%--                          <script src="../jquery.min.js"></script>
                    <script src="../raphael-min.js"></script>
                    <script src="../morris.min.js"></script>
                          <script type="text/javascript">
                              new Morris.Bar({
                                  // ID of the element in which to draw the chart.
                                  element: 'myfirstchart',
                                  // Chart data records -- each entry in this array corresponds to a point on
                                  // the chart.
                                  data: [
                                      { year: 'January', value: 15000 },
                                      { year: 'February', value: 30000 },
                                      { year: 'March', value: 40000 },
                                      { year: 'April', value: 50000 },
                                  ],
                                  // The name of the data record attribute that contains x-values.
                                  xkey: 'year',
                                  // A list of names of data record attributes that contain y-values.
                                  ykeys: ['value'],
                                  // Labels for the ykeys -- will be displayed when you hover over the
                                  // chart.
                                  labels: ['Value']
                              });
                          </script>
                    <hr>--%>

                    <%--<h1 style="background-color: #007bff; height: 50px; color: white" align="center">Sales Category</h1>

                           </br>
                           </br>
                           </br>
                           </br>     
                    <div id="chartContainer" style="height:450px; width: 100%;"></div>--%>
                            </div>
                        </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
