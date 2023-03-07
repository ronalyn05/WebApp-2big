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
                                            <h5 class="m-b-10">POINT OF SALE </h5>
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
                        <!-- Page-header end -->
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="page-wrapper">

        <!-- page content -->
        <div class="right_col" role="main">
          <div class="">

            <div class="clearfix"></div>
            <div class="row">
          <div class="col-md-7 col-sm-7  " style="color: black"><button style="width: 100%; background-color: red; height: 1.5%"></button>
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Point of Sale </h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                    <form>
                  <input required="required" class="form-control " type="text" placeholder="SEARCH ITEM NAME OR ITEM ID HERE..." >
                    </form>
                    <table class="table">
                      <thead>
                        <tr>
                          <th>#</th>
                          <th>Id</th>
                          <th>Item</th>
                          <th>Price</th>
                          <th>Qty</th>
                          <th>Disc %</th>
                          <th>Total</th>
                          <th> </th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <th scope="row">1</th>
                          <td>00001</td>
                          <td><i>Distilled Drinking Water</i></td>
                          <td style="color: blue">182</td>
                          <td style="color: blue">2 PC</td>
                          <td style="color: blue">0.00</td>
                          <td><strong><u>364</u></strong></td>
                          <td><button class="btn btn-primary btn-sm"><i class="fa fa-times"></i></button></td>
                        </tr>
                        <tr>
                          <th scope="row">2</th>
                          <td>00002</td>
                          <td><i>ph9 Drinking Water</i></td>
                          <td style="color: blue">222</td>
                          <td style="color: blue">1 PC</td>
                          <td style="color: blue">0.00</td>
                          <td><strong><u>222</u></strong></td>
                          <td><button class="btn btn-primary btn-sm"><i class="fa fa-times"></i></button></td>
                        </tr>
                      </tbody>
                    </table>

                  </div>
                </div>
              </div>
            <div class="col-md-5 col-sm-12 "><button style="width: 100%; background-color: #257774; height: 1.5%"></button>
                <div class="x_panel">
                    <button class="btn btn-sm" style="background-color:#141866;; color: #f9f7f7"><i class="fa fa-times"></i> Reset</button>
                    <div class="clearfix"></div>
                  <br>
                  <div class="x_content">

                    <form class="form-horizontal form-label-left">

                      <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Id Trans.</label>

                        <div class="col-sm-9">
                          <div class="input-group">
                            <input class="form-control" type="text" disabled="" value="ZZJKE11133">
                            <span class="input-group-btn">
                            <button type="button" class="btn btn-primary"><i class="fa fa-search"></i></button>
                            </span>
                          </div>
                      </div>
                      </div>
                      <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Date Trans.</label>

                        <div class="col-sm-9">
                          <div class="input-group">
                            <input class="form-control" type="text" disabled value="10-18-2022">
                            <!-- /btn-group -->
                          </div>
                        </div>
                      </div>
                      <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Cashier</label>

                        <div class="col-sm-9">
                          <div class="input-group">
                            <input class="form-control" type="text" disabled="" value="Ronalyn Giducos">
                          </div>
                      </div>
                      </div>
                      <div class="form-group row">
                        <label class="col-sm-3 col-form-label" style="color: #2727e7"><strong>Disc %</strong></label>

                        <div class="col-sm-9">
                          <div class="input-group">
                            <input class="form-control" type="text"  value="0">
                            <span class="input-group-btn">
                            <button type="button" class="btn btn-default" style="background-color: #dbdbdb; color: black">%</button>
                            </span>
                          </div>
                      </div>
                      </div>
                      <div class="form-group row">
                        <label class="col-sm-3 col-form-label"style="color: #2727e7"><strong>Disc php</strong></label>

                        <div class="col-sm-9">
                          <div class="input-group">
                            <span class="input-group-btn">
                              <button type="button" class="btn go-class" style="background-color: #dbdbdb; color: black">Php</button>
                               </span>
                            <input class="form-control" type="text" disabled="" value="0">
                          </div>
                        </div>
                      </div>
                      <div class="form-group row">
                        <label class="col-sm-3 col-form-label" style="color: black"><strong>Sub Total</strong></label>

                        <div class="col-sm-9">
                          <div class="input-group">
                            <span class="input-group-btn">
                              <button type="button" class="btn go-class" style="background-color: #dbdbdb; color: black">Php</button>
                               </span>
                            <input class="form-control" type="text" disabled="" value="364.00">
                          </div>
                        </div>
                        <button style="height: 90%; width: 27.8%; background-color: #ffb400; color: white; border-radius: 7%"><h1 style="font-size: 65px">Php.</h1></button><h1 style="margin-left: 40%; margin-top: 30px"><strong>Php 364.00</strong></h1>
                        <button class="btn btn-success" style="width: 128%;margin-top: 10px"><i class="fa fa-shopping-cart"></i>[f9]Process Payment</button>
                      </div>
                    </form>
                  </div>
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
