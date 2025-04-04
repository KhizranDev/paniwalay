<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rates_new.aspx.cs" Inherits="rates_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

    <div class="noty_bar noty_theme_default noty_layout_top noty_success NotificationDiv"
        id="notify_success_insert" style="cursor: pointer; display: none;">
        <div class="noty_message">
            <span class="noty_text">Record Saved Successfully</span>
        </div>
    </div>
    <div class="noty_bar noty_theme_default noty_layout_top noty_error NotificationDiv"
        id="notify_error_insert" style="cursor: pointer; display: none;">
        <div class="noty_message">
            <span class="noty_text">Error while saving record, Please try again!</span>
        </div>
    </div>

    <div class="">
        
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>
                            Rates <small>Add or Edit Rates</small></h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </ul>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="x_content">
                        <br />
                        <form id="form1" runat="server" class="form-horizontal form-label-left" autocomplete="off">
                          
                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtDescription">Tanker Type <span class="required">*</span></label>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <asp:DropDownList ID="ddlTankerType" data-live-search="true" CssClass="form-control col-md-7 col-xs-12 default-select2" runat="server" DataTextField="TankerTypeName" DataValueField="TankerTypeId" ClientIDMode="Static">
                                    </asp:DropDownList>
                                    <div class="input-error form-control-input" style="color: Red; display: none;">Tanker Type Name is required</div>
                                </div>
                            </div>
                            
                            <div class="ln_solid"></div>

                        </form>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
			<div class="col-md-12">
			  <div class="x_panel">
			  
				<div class="panel panel-primary">
					<div class="panel-heading">
						<h3 class="panel-title">Rates</h3>
					</div>
					<div class="panel-body">

						<div id="divLoader" style="width: 100%; text-align: center; display: none;">
							<img src='images/ajax-loader.gif' width="50px">
							<p>Please wait while loading.....</p>
						</div>

						<div id="divData">
							
						</div>
                        
					</div>
				</div>
			</div>	
				
		</div>
	</div>

    </div>

<!-- Start Rate Modal -->
<div class="modal fade" id="ModalRate" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" id="btnCloseUp" onclick="clear_values();" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
				<h4 class="modal-title" id="myModalLabel">Add Rate</h4>
			</div>

			<div class="modal-body">

				<form class="form-horizontal form-label-left" role="form" id="frmRate" name="frmRate">

					<div class="form-group">
						<label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtDate">Effective Date <span class="required">*</span></label>
						<div class="col-md-4 col-sm-4 col-xs-12">
							<input type="text" class="form-control datepicker-autoClose" id="txtDate" name="txtDate" value="" runat="server" clientidmode="Static" />
						</div>
					</div>

					<div class="form-group">
						<label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtRate">Rate <span class="required">*</span></label>
						<div class="col-md-4 col-sm-4 col-xs-12">
							<input type="text" id="txtRate" name="txtRate" class="form-control col-md-7 col-xs-12" placeholder="Rate" value="" onkeypress="return validateNumbers(event)" />
						</div>
					</div>
				        
					<div class="form-group">
						<div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-3">
							<button type="button" class="btn btn-sm btn-primary" id="btnSave" data-loading-text="<i class='fa fa-spinner fa-spin '></i> Process...">Save</button>
						</div>
					</div>
					
					<div class="form-group" style="display: none; color: red;" id="divError">
						<div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-3">
							<b>Please enter all data</b>
						</div>
					</div>
											
				</form>


			</div>

			<div class="modal-footer">
				<button type="button" id="btnCloseDown" onclick="clear_values();" class="btn btn-warning" data-dismiss="modal">Close</button>
			</div>
		</div>
	</div>
</div>
<!-- End Rate Modal -->

<!-- Start Rate Modal History -->
<div class="modal fade" id="ModalRateHistory" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
				<h4 class="modal-title" id="H1">Rates History</h4>
			</div>

			<div class="modal-body">
				<div class="x_panel">
					<div id="divLoaderHistory" style="width: 100%; text-align: center; display: none;">
						<img src='images/ajax-loader.gif' width="50px">
						<p>Please wait while loading.....</p>
					</div>

					<div id="divDataHistory" style="display:none;">
					   
					</div>
				</div>
			</div>

			<div class="modal-footer">
				<button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>
			</div>
		</div>
	</div>
</div>
<!-- End Rate Modal History -->


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFooter" Runat="Server">


<script src="vendors/moment/min/moment.min.js"></script> 
<script src="vendors/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js"></script>

<script>

    var $ddlTankerType = $("#ddlTankerType");
    var rate_id = 0;
    var location_id = 0;

    $(document).ready(function () {
        
        //console.log(formatDate(new Date()));

        $(".default-select2").select2();

        $("#txtDate").datepicker({
            format: 'dd-M-yyyy',
            todayHighlight: !0,
            autoclose: !0
        });

        $ddlTankerType.on('change', function () {
            LoadRates($(this).val());
        });

        //Add Region Rate start
        $(document).on('click', '.btnChangeRate', function () {

            rate_id = $(this).data('id');
            location_id = $(this).data('locationid');

            //console.log(location_id);

            $("#txtDate").val(formatDate(new Date()));
            $('#ModalRate').modal({ backdrop: 'static', keyboard: false });
            $('#ModalRate').modal('show');


        });

        $(document).on('click', '.btnHistoryRate', function () {

            rate_id = $(this).data('id');
            LoadRatesHistory();
            $('#ModalRateHistory').modal({ backdrop: 'static', keyboard: false });
            $('#ModalRateHistory').modal('show');
        });

        $(document).on('click', '#btnSave', function () {

            var date = $("#txtDate").val();
            var rate = $("#txtRate").val();
            //alert(rate);
            //return false;

            $("#btnSave").button('loading');

            $("#divError").hide();

            if (date != '' && rate != '') {

                $.ajax({
                    type: "POST",
                    url: "RequestHandler.ashx?action=save_rate",
                    data: {
                        'rate_id': rate_id,
                        'location_id': location_id,
                        'tanker_type_id': $ddlTankerType.val(),
                        'date': date,
                        'rate': rate
                    }
                }).done(function (data) {

                    $("#btnSave").button('reset');
                    //alert(data);
                    console.log(data);

                    if (data == '000') {
                        $('#ModalRate').modal('hide');
                        LoadRates($ddlTankerType.val());
                        clear_values();
                        NotifySuccess('Rate Update Successfully..!');
                    } else {
                        NotifyError('Error Occured..!');
                    }
                });
            } else {
                $("#btnSave").button('reset');
                $("#divError").show();
            }

            return false;
        });

    });

    function LoadRates(tanker_type_id) {

        $('#divLoader').show();
        $('#divData').hide();

        $.ajax({
            type: "POST",
            url: "RequestHandler.ashx?action=get_rates",
            data: {
                "tanker_type_id": tanker_type_id
            }
        }).done(function (data) {
            //console.log(data);

            $('#divData').html(data);
            $('#divData').show();
            $('#divLoader').hide();

        });
    }

    function LoadRatesHistory() {

        $('#divLoaderHistory').show();
        $('#divDataHistory').hide();

        $.ajax({
            type: "POST",
            url: "RequestHandler.ashx?action=get_rates_history",
            data: {
                'rate_id': rate_id
            }
        }).done(function (data) {

            //console.log(data);

            $('#divDataHistory').html(data);
            $('#divDataHistory').show();
            $('#divLoaderHistory').hide();

            //$("#tblHistory").dataTable();

            $('#tblHistory').dataTable({
                destroy: true,
                searching: false,
                pageLength: 10,
                order: [[1, "desc"]]
            });

        });
    }

    function clear_values() {
        $("#divError").hide();
        $('#txtRate').val('');
        $('#txtDate').val('');
    }

    function validateNumbers(key) {
        //getting key code of pressed key
        var keycode = (key.which) ? key.which : key.keyCode;
        var phn = document.getElementById('txtPhn');
        //comparing pressed keycodes
        if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
            return false;
        }
        else {
            //Condition to check textbox contains ten numbers or not
            return true;
        }
    }

    function formatDate(date) {
        var monthNames = [
            "Jan", "Feb", "Mar",
            "Apr", "May", "Jun", "Jul",
            "Aug", "Sep", "Oct",
            "Nov", "Dec"
        ];

        var day = date.getDate();
        var monthIndex = date.getMonth();
        var year = date.getFullYear();

        return day + '-' + monthNames[monthIndex] + '-' + year;
    }

</script>


</asp:Content>

