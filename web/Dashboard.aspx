<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="Server">
    <!-- NProgress -->
    <link href="vendors/nprogress/nprogress.css" rel="stylesheet">
    <!-- iCheck -->
    <link href="vendors/iCheck/skins/flat/green.css" rel="stylesheet">
    <!-- bootstrap-progressbar -->
    <link href="vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css"
        rel="stylesheet">
    <!-- JQVMap -->
    <link href="vendors/jqvmap/dist/jqvmap.min.css" rel="stylesheet" />
    <!-- bootstrap-daterangepicker -->
    <link href="vendors/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet">
    <style>
        .loader
        {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('images/pageLoader.gif') 50% 50% no-repeat rgb(249,249,249);
            opacity: .8;
        }
        
        .graph-main {
            direction: ltr;
            /*position: absolute;
            left: 0px;
            top: 0px;*/
            width: 1209px;
            height: 329px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    
    <div class="loader" style="display: none;"></div>
    
    <!-- top tiles -->
    <div class="row tile_count" id="divStatusCounts">

        <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
            <span class="count_top"><i class="fa fa-user"></i>Total New Orders</span>
            <div class="count green"></div>
        </div>
        <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
            <span class="count_top"><i class="fa fa-clock-o"></i>Total Received Orders</span>
            <div class="count"></div>
        </div>
        <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
            <span class="count_top"><i class="fa fa-user"></i>Total In-Progress Orders</span>
            <div class="count"></div>
        </div>
        <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
            <span class="count_top"><i class="fa fa-user"></i>Total Delivered Orders</span>
            <div class="count"></div>
        </div>
        <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
            <span class="count_top"><i class="fa fa-user"></i>Total Finished Orders</span>
            <div class="count"></div>
        </div>
        <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
            <span class="count_top"><i class="fa fa-user"></i>Total Cancel Orders</span>
            <div class="count red"></div>
        </div>

    </div>
    <!-- /top tiles -->

    <!-- top graph -->
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="dashboard_graph">
                <div class="row x_title">
                    <div class="col-md-6">
                        <h3>Order Counts <small>Counts Datewise</small></h3>
                    </div>
                    <div class="col-md-6">
                        <div id="reportrange1" class="pull-right" style="background: #fff; cursor: pointer;
                            padding: 5px 10px; border: 1px solid #ccc">
                            <i class="glyphicon glyphicon-calendar fa fa-calendar"></i><span>December 30, 2014 -
                                January 28, 2015</span> <b class="caret"></b>
                        </div>
                    </div>
                </div>


                 <div class="col-md-12 col-sm-12 col-xs-12">
                    <%--<canvas id="lineChart1" class="graph-main"></canvas>--%>
                    <div id="chart_plot_01" class="demo-placeholder"></div>
                    <%--<div id="lineChart1" class="demo-placeholder"></div>--%>
                </div>

                
                <div class="clearfix">
                </div>
            </div>
        </div>
    </div>
    <!-- /top graph -->

    <br />

    <div class="row">
        <div class="col-md-6 col-sm-6 col-xs-12">
            <div class="x_panel tile fixed_height_320">
                <div class="x_title">
                    <h2>Locations</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                    </ul>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="x_content" id="divLocations">
                    <h4>Location across percentage</h4>
                    <div class="widget_summary">
                        <div class="w_left w_25">
                            <span>DHA</span>
                        </div>
                        <div class="w_center w_55">
                            <div class="progress">
                                <div class="progress-bar bg-green" role="progressbar" aria-valuenow="60" aria-valuemin="0"
                                    aria-valuemax="100" style="width: 50%;">
                                    <span class="sr-only">60% Complete</span>
                                </div>
                            </div>
                        </div>
                        <div class="w_right w_20">
                            <span>123k</span>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="widget_summary">
                        <div class="w_left w_25">
                            <span>0.1.5.3</span>
                        </div>
                        <div class="w_center w_55">
                            <div class="progress">
                                <div class="progress-bar bg-green" role="progressbar" aria-valuenow="60" aria-valuemin="0"
                                    aria-valuemax="100" style="width: 45%;">
                                    <span class="sr-only">60% Complete</span>
                                </div>
                            </div>
                        </div>
                        <div class="w_right w_20">
                            <span>53k</span>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="widget_summary">
                        <div class="w_left w_25">
                            <span>0.1.5.4</span>
                        </div>
                        <div class="w_center w_55">
                            <div class="progress">
                                <div class="progress-bar bg-green" role="progressbar" aria-valuenow="60" aria-valuemin="0"
                                    aria-valuemax="100" style="width: 25%;">
                                    <span class="sr-only">60% Complete</span>
                                </div>
                            </div>
                        </div>
                        <div class="w_right w_20">
                            <span>23k</span>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="widget_summary">
                        <div class="w_left w_25">
                            <span>0.1.5.5</span>
                        </div>
                        <div class="w_center w_55">
                            <div class="progress">
                                <div class="progress-bar bg-green" role="progressbar" aria-valuenow="60" aria-valuemin="0"
                                    aria-valuemax="100" style="width: 5%;">
                                    <span class="sr-only">60% Complete</span>
                                </div>
                            </div>
                        </div>
                        <div class="w_right w_20">
                            <span>3k</span>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="widget_summary">
                        <div class="w_left w_25">
                            <span>0.1.5.6</span>
                        </div>
                        <div class="w_center w_55">
                            <div class="progress">
                                <div class="progress-bar bg-green" role="progressbar" aria-valuenow="60" aria-valuemin="0"
                                    aria-valuemax="100" style="width: 2%;">
                                    <span class="sr-only">60% Complete</span>
                                </div>
                            </div>
                        </div>
                        <div class="w_right w_20">
                            <span>1k</span>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-12">
            <div class="x_panel tile fixed_height_320 overflow_hidden">
                <div class="x_title">
                    <h2>Tanker Types</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                    </ul>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="x_content">
                    <table class="" style="width: 100%">
                        <tr>
                            <th style="width: 37%;">
                                <p>Top 5</p>
                            </th>
                            <th>
                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7">
                                    <p class="">Types</p>
                                </div>
                                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5">
                                    <p class="">Progress</p>
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <canvas id="canvasDoughnut1" class="canvasDoughnut1" height="140" width="140" style="margin: 15px 10px 10px 0"></canvas>
                            </td>
                            <td>
                                <table id="Doughnut1Legend" class="tile_info">
                                    <tr>
                                        <td>
                                            <p>
                                                <i class="fa fa-square blue"></i>IOS
                                            </p>
                                        </td>
                                        <td>
                                            30%
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <i class="fa fa-square green"></i>Android
                                            </p>
                                        </td>
                                        <td>
                                            10%
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <i class="fa fa-square purple"></i>Blackberry
                                            </p>
                                        </td>
                                        <td>
                                            20%
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <i class="fa fa-square aero"></i>Symbian
                                            </p>
                                        </td>
                                        <td>
                                            15%
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <i class="fa fa-square red"></i>Others
                                            </p>
                                        </td>
                                        <td>
                                            30%
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Recent Orders</h2>

                     <div class="clearfix">
                    </div>

                </div>

                <div id="divLoader" style="width: 100%; text-align: center; margin-top:50px; display: none;">
                    <img src='images/ajax-loader.gif' width="50px">
                    <p>Please wait while loading.....</p>
                </div>

                <div class="x_content" id="divTable" runat="server" clientidmode="Static">
                </div>

            </div>
        </div>
    </div>

    <!-- Start Order Detail Modal -->
    <div class="modal fade" id="ModalOrderDetails" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h3>Order Detail</h3>
                </div>
                <div class="modal-body" style="max-height: 450px; overflow-y: auto; overflow-x: hidden;" id="divOrderDetail">
                    
                </div>
                <div class="modal-footer">
                    <span class="label label-important" style="font-size: 12px; padding: 5px; display:none;" id="divTaxAmount">Tax Amount : 0</span>
                    <span class="label label-warning" style="font-size: 12px; padding: 5px; display:none;" id="divDeliveryCharges">Delivery Charges : 195</span>
                    <span class="label label-success" style="font-size: 12px; padding: 5px; display:none;" id="divDiscount">Discount : 0</span>
                    <span class="label label-success" style="font-size: 12px; padding: 5px;" id="divBillAmount">Bill Amount : 3,155</span>
                </div>
            </div>
        </div>
    </div>
    <!-- End Order Detail Modal -->

    <!-- Start Cancel Order Modal -->
    <div class="modal fade" id="ModalCancelReason" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
		        <div class="modal-header">
                    
			    <button type="button" id="btnCancelOrderClose" class="close" data-dismiss="modal">×</button>
			    <h3>Cancel Order Reason</h3>
                    
		        </div>
		        <div class="modal-body" style="max-height: 480px; overflow-y:auto; overflow-x:hidden;" id="div2">
                    <div class="form-group">
                        <textarea class="form-control" style="margin-bottom:-13px;" rows="4" id="txtReason" placeholder="Enter Cancel Order Reason"></textarea><br />
                        <div class="input-error form-control-input" style="color: Red; display: none;">Please enter Reason</div>
                    </div>

                    <div class="form-group">
                        <button type='button' id='btnCancelled' class='btn btn-sm btn-danger' style="margin-right: 451px;">Cancel Order</button>
                    </div>  
		        </div>
		        <div class="modal-footer">
		        </div>
	        </div>
        </div>
    </div>
    <!-- End Cancel Order Modal -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFooter" runat="Server">
    <!-- FastClick -->
    <script src="vendors/fastclick/lib/fastclick.js"></script>
    <!-- NProgress -->
    <script src="vendors/nprogress/nprogress.js"></script>
    <!-- Chart.js -->
    <script src="vendors/Chart.js/dist/Chart.min.js"></script>
    <!-- gauge.js -->
    <script src="vendors/gauge.js/dist/gauge.min.js"></script>
    <!-- bootstrap-progressbar -->
    <script src="vendors/bootstrap-progressbar/bootstrap-progressbar.min.js"></script>
    <!-- iCheck -->
    <script src="vendors/iCheck/icheck.min.js"></script>
    <!-- Skycons -->
    <script src="vendors/skycons/skycons.js"></script>
    <!-- Flot -->
    <script src="vendors/Flot/jquery.flot.js"></script>
    <script src="vendors/Flot/jquery.flot.pie.js"></script>
    <script src="vendors/Flot/jquery.flot.time.js"></script>
    <script src="vendors/Flot/jquery.flot.stack.js"></script>
    <script src="vendors/Flot/jquery.flot.resize.js"></script>
    <!-- Flot plugins -->
    <script src="vendors/flot.orderbars/js/jquery.flot.orderBars.js"></script>
    <script src="vendors/flot-spline/js/jquery.flot.spline.min.js"></script>
    <script src="vendors/flot.curvedlines/curvedLines.js"></script>
    <!-- DateJS -->
    <script src="vendors/DateJS/build/date.js"></script>
    <!-- JQVMap -->
    <script src="vendors/jqvmap/dist/jquery.vmap.js"></script>
    <script src="vendors/jqvmap/dist/maps/jquery.vmap.world.js"></script>
    <script src="vendors/jqvmap/examples/js/jquery.vmap.sampledata.js"></script>
    <!-- bootstrap-daterangepicker -->
    <script src="vendors/moment/min/moment.min.js"></script>
    <script src="vendors/bootstrap-daterangepicker/daterangepicker.js"></script>
    
    <script>
        
        var start = moment().subtract(30, 'days');
        var end = moment();

        var status_id = 0;
        var order_id = 0;
        var account_id = 0;

        $(document).ready(function () {
            
            $('#reportrange1').daterangepicker({
                startDate: start,
                endDate: end,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);

            cb(start, end);

            $(document).on('click', '.btnView', function () {

                order_id = $(this).data('id');

                $.ajax({
                    type: "POST",
                    url: "RequestHandler.ashx?action=get_orders_detail",
                    data: {
                        'order_id': order_id
                    }
                }).done(function (data) {

                    //console.log(data);
                    $('#divOrderDetail').html(data['data']);
                    $('#ModalOrderDetails').modal();
                    $("#divBillAmount").html("Bill Amount : " + data['TotalAmount'] + "PKR");

                }).fail(function (msg) {
                    console.log(msg);
                }).always(function (msg) {
                    console.log(msg);
                });

                return false;

            });

            $(document).on('click', '.btnChangeStatus', function () {

                order_id = $(this).data('id');
                account_id = $(this).data('accountid');
                var order_status = $(this).html();
                var next_order_status_id = $(this).data('nextstatus');
                var next_order_status_name = $(this).data('nextstatusname');
                //alert(next_order_status_name);
                //return false;

                $.confirm({
                    text: "Are you sure you want to Proceed this Order from <b>" + order_status + "</b> to <b>" + next_order_status_name + "</b>",
                    title: "Confirmation required",
                    confirm: function (button) {
                        UpdateOrderStatus(next_order_status_id, '');
                    },
                    cancel: function (button) {
                        // nothing to do
                    },
                    confirmButton: "Yes I am",
                    cancelButton: "No",
                    post: true,
                    confirmButtonClass: "btn-danger",
                    cancelButtonClass: "btn-default",
                    dialogClass: "modal-dialog delete_confirmation" // Bootstrap classes for large modal
                });

            });

            $(document).on('click', '.btnCancel', function () {

                order_id = $(this).data('id');

                $('#ModalCancelReason').modal({ backdrop: 'static', keyboard: false });
                $('#ModalCancelReason').modal('show');

            });

            $(document).on('click', '#btnCancelled', function () {

                var remarks = $('#txtReason').val();

                if (validation()) {
                    $('#ModalCancelReason').modal('hide');
                    UpdateOrderStatus(6, remarks);
                }

            });

            $(document).on('click', '#btnCancelOrderClose', function () {
                clearValues();
            });

        });

        function cb(start, end) {

            StartDate = start.format('YYYY-MM-D');
            EndDate = end.format('YYYY-MM-D');
            $('#reportrange1 span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));

            console.log("Start Date " + StartDate + ", End Date " + EndDate);
            //DisplayGraphs2();
            GetData();
            GetOrders();
            
        }

        function GetData() {

            $(".loader").show();

            $.ajax({
                url: "RequestHandler.ashx?action=get_dashboard",
                type: "POST",
                data: {
                    'StartDate': StartDate,
                    'EndDate': EndDate
                },
                success: function (data) {

                    console.log("Dashboard Response");
                    console.log(data);

                    $('#divStatusCounts').html(data.StatusCounts);
                    $('#divLocations').html(data.Locations);
                    DisplayTankerTypeChart(data.TankerType);
                    $(".loader").hide();
                },
                error: function (data) {
                    console.log(data);
                }
            });
        }

        function DisplayTankerTypeChart(TankerType) {

            var chart1 = null;
            var filter_label = "Tanker Type";

            var DonutColors = ["#BDC3C7", "#9B59B6", "#E74C3C", "#26B99A", "#3498DB", "#34495E", "#ACADAC", "#F08080", "#808000", "#800080"];

            var filter = [];
            var percent = [];
            var _html = "";
            var index = 0;

            var obj = jQuery.parseJSON(TankerType);

            $.each(obj, function (key, value) {

                //console.log("loop: " + value.TankerTypeName);

                filter.push(value.TankerTypeName);
                percent.push(value.Percentage);

                _html += "	<tr>";
                _html += " 		<td>";
                _html += " 			<p><i class='fa fa-square' style='font-size: 10px; color: " + DonutColors[index] + ";'></i>" + value.TankerTypeName + " </p>";
                _html += "		</td>";
                _html += " 		<td>" + value.Percentage + "%</td>";
                _html += "	</tr>";
                //console.log(index);
                index++;

            });

            var ctx1 = $("#canvasDoughnut1");
            $("#Doughnut1Legend").html(_html);

            var data1 = {
                labels: filter,
                datasets: [
						{
						    label: filter_label,
						    data: percent,
						    backgroundColor: DonutColors
						    //hoverBackgroundColor: ["#CFD4D8", "#B370CF", "#E95E4F", "#36CAAB", "#49A9EA"]
						}
					]
            };

            var options = {
                title: {
                    display: true,
                    position: "top",
                    text: filter_label,
                    fontSize: 18,
                    fontColor: "#111",
                    responsive: true,
                    maintainAspectRatio: false
                },

                legend: {
                    labels: {
                        fontSize: 20
                    }
                }

            };

            chart1 = new Chart(ctx1, {
                type: "doughnut",
                data: data1,
                options: options
            });
            chart1.destroy();
        }

        function GetOrders() {

            $('#divTable').hide();
            $('#divLoader').show();

            jQuery.ajax({

                type: "POST",
                url: "RequestHandler.ashx?action=get_orders",
                cache: false,
                dataType: 'text',
                data: {
                    "status_id": status_id
                },
                //async: false,
                success: function (data) {

                    $("#divTable").html(data);

                    $('#divTable').show();
                    $('#divLoader').hide();

                    $('#test-table').DataTable({
                        responsive: false,
                        aaSorting: [[1, "desc"]]
                    });
                }
            });

        }

        function UpdateOrderStatus(next_order_status_id, remarks) {
            
            $(".loader").show();

            $.ajax({
                type: "POST",
                url: "RequestHandler.ashx?action=status_update",
                data: {
                    'order_id': order_id,
                    'account_id': account_id,
                    'status_id': next_order_status_id,
                    'remarks': remarks
                }
            }).done(function (data) {

                console.log(data);

                if (data == '000') {
                    NotifySuccess('Status Changed Successfully..!');
                    GetOrders();
                } else if (data == '002') {
                    NotifyError('Account not verified..!');
                } else {
                    NotifyError('Error Occured..!');
                }

                
                $(".loader").hide();

            }).fail(function (msg) {
                console.log(msg);
            }).always(function (msg) {
                console.log(msg);
            });
        }

        function DisplayKeywordTrends(){
		
		    //alert(StartDate);
		    //alert(EndDate);
		    //alert(ClientId);
		    //alert($ddlChannel.val());
		
		    $.ajax({
			     type: "POST",
			     url: "RequestHandler.ashx?action=get_dashboard",
			     data: {
                    'StartDate': '',
                    'EndDate': ''
                }
		     }).done(function (data) {
			
			    //console.log(data);
			 
			    var objKeyword = jQuery.parseJSON(data.OrderCounts);
			    
                //var data_sets = [];
			    var i = 0;
			    var date = [];
			    var duration = [];
			    
			    $.each(objKeyword, function(key,value) {
                    //console.log(value.Date);
                    date.push(new Date(value.Date));
                    duration.push(parseInt(value.TotalRecords));
			    });
			    
                var data_sets = [{
                            label: "My First dataset",
					        backgroundColor: "rgba(38, 185, 154, 0.3)",
					        borderColor: "rgba(22, 160, 133, 1)",
					        pointBorderColor: "rgba(22, 160, 133, 1)",
					        pointBackgroundColor: "rgba(38, 185, 154, 0.7)",
					        pointHoverBackgroundColor: "#fff",
					        pointHoverBorderColor: "rgba(220,220,220,1)",
					        pointBorderWidth: 1,
					        data: duration		// duration;
				    }]

                console.log(data_sets);

                var f = document.getElementById("lineChart1");

			    new Chart(f, {
				    type: "line",
				    data: {
					    labels: date,
					    datasets: data_sets
				    },
				    options: {
					    legend: {
						    display: false
					    },
					    scales: {
						    xAxes: [{
							        type: 'time',
							        time: {
								        displayFormats: {
									        'millisecond': 'DD MMM',
									        'second': 'DD MMM',
									        'minute': 'DD MMM',
									        'hour': 'DD MMM',
									        'day': 'DD MMM',
									        'week': 'DD MMM',
									        'month': 'DD MMM',
									        'quarter': 'DD MMM',
									        'year': 'DD MMM',
								        },
								        tooltipFormat: "d MMM"
							        }
						        }
					        ]
					    }
					
				    }
			    })
		    });
	    }

        function DisplayGraphs() {

            var f = document.getElementById("lineChart1");
            new Chart(f, {
                type: "line",
                data: {
                    labels: ["January", "February", "March", "April", "May", "June", "July"],
                    datasets: [{
                        label: "My First dataset",
                        backgroundColor: "rgba(38, 185, 154, 0.3)",
                        borderColor: "rgba(22, 160, 133, 1)",
                        pointBorderColor: "rgba(22, 160, 133, 1)",
                        pointBackgroundColor: "rgba(38, 185, 154, 0.7)",
                        pointHoverBackgroundColor: "#fff",
                        pointHoverBorderColor: "rgba(220,220,220,1)",
                        pointBorderWidth: 1,
                        data: [31, 74, 6, 39, 20, 85, 7]
                    }, {
                        label: "My Second dataset",
                        backgroundColor: "rgba(3, 88, 106, 0.3)",
                        borderColor: "rgba(22, 160, 133, 0.70)",
                        pointBorderColor: "rgba(22, 160, 133, 0.70)",
                        pointBackgroundColor: "rgba(3, 88, 106, 0.70)",
                        pointHoverBackgroundColor: "#fff",
                        pointHoverBorderColor: "rgba(151,187,205,1)",
                        pointBorderWidth: 1,
                        data: [82, 23, 66, 9, 99, 4, 2]
                    }, {
                        label: "My Third dataset",
                        backgroundColor: "rgba(22, 160, 133, 0.3)",
                        borderColor: "rgba(22, 160, 133, 0.70)",
                        pointBorderColor: "rgba(3, 88, 106, 0.70)",
                        pointBackgroundColor: "rgba(3, 88, 106, 0.70)",
                        pointHoverBackgroundColor: "#fff",
                        pointHoverBorderColor: "rgba(151,187,205,1)",
                        pointBorderWidth: 1,
                        data: [10, 30, 50, 5, 66, 2, 1]
                    }]
                }
            })

        }

        function DisplayGraphs2() {

            $.ajax({
			         type: "POST",
			         url: "RequestHandler.ashx?action=get_dashboard",
			         data: {
                        'StartDate': StartDate,
                        'EndDate': EndDate
                    }
		         }).done(function (data) {
			
			        //console.log(data);
			 
			        var objKeyword = jQuery.parseJSON(data.OrderCounts);
			    
			        var date = [];
			        var duration = [];
			    
			        $.each(objKeyword, function(key,value) {
                        //console.log(value.Date);
                        date.push(value.Date);
                        duration.push(parseInt(value.TotalRecords));
			        });
			    

                    var f = document.getElementById("lineChart1");
                    new Chart(f, {
                        type: "line",
                        data: {
                            labels: date,
                            datasets: [{
                                label: "Order Counts",
                                backgroundColor: "rgba(38, 185, 154, 0.3)",
                                borderColor: "rgba(22, 160, 133, 1)",
                                pointBorderColor: "rgba(22, 160, 133, 1)",
                                pointBackgroundColor: "rgba(38, 185, 154, 0.7)",
                                pointHoverBackgroundColor: "#fff",
                                pointHoverBorderColor: "rgba(220,220,220,1)",
                                pointBorderWidth: 1,
                                data: duration
                            }]
                        },
				        
                    })
		        });
        }

        function validation() {

            var hasFocus = false;
            var errCount = 0;

            if ($('#txtReason').val() == '') {
                $('#txtReason').parent().addClass('has-error');
                $('#txtReason').parent().find('.input-error').show().css('display', 'inline-block');

                if (!hasFocus) {
                    $('#txtReason').val().focus();
                    hasFocus = true;
                }

                errCount++;
            }
            else {
                $('#txtReason').parent().removeClass('has-error');
                $('#txtReason').parent().find('.input-error').hide();
            }


            if (errCount > 0)
                return false;
            else
                return true;
        }

        function clearValues() {
            $('#txtReason').val('');
            $('#txtReason').parent().removeClass('has-error');
            $('#txtReason').parent().find('.input-error').hide();
        }

    </script>

</asp:Content>