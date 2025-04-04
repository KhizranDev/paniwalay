<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vendors_payments.aspx.cs" Inherits="vendors_payments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

<!-- page content -->
    
<div class="">

    <div class="x_panel" style="">
        <div class="x_title">
            <h2>Payments</h2>
            <h2 style="float:right;" id="lblGrandTotal">Total Payments 25,000 PKR</h2>
            <div class="clearfix"></div>
        </div>
        <div class="x_content">
            <div class="container">
                <div class="row">

                <form id="form1" autocomplete="off">

                    <div class='col-sm-4'>
                        From Date
                        <div class="form-group">
                            <div class='input-group date' id='myDatepicker1'>
                                <input type='text' class="form-control" id="txtFromDate" name="txtFromDate" />
                                <span class="input-group-addon">
                                   <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class='col-sm-4'>
                        To Date
                        <div class="form-group">
                            <div class='input-group date' id='myDatepicker2'>
                                <input type='text' class="form-control" id="txtToDate" name="txtToDate" />
                                <span class="input-group-addon">
                                   <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                
                    <div class='col-sm-4'>
                        Vendor
                        <div class="form-group">
                            <input type='text' class="form-control" id="txtVendor" Placeholder="Name/Email/CNIC" />
                        </div>
                    </div>


                    <div class='col-sm-4'>
                        <br />
                        <div class="form-group">
                            <button type="button" class="col-md-5 col-xs-12 btn btn-md btn-info" id="btnSearch" data-loading-text="<i class='fa fa-spinner fa-spin '></i> Process...">Search</button>
                            <button type="button" class="col-md-5 col-xs-12 btn btn-md btn-primary" id="btnMakePayment" data-loading-text="<i class='fa fa-spinner fa-spin '></i> Process...">Make Payment</button>
                        </div>
                    </div>

                </form>

                </div>
            </div>
        </div>
   </div>

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <section class="content invoice">
                        <div class="row">

                            <div id="divLoader" style="width: 100%; text-align: center; margin-top:50px; display: none;">
                                <img src='images/ajax-loader.gif' width="50px">
                                <p>Please wait while loading.....</p>
                            </div>

                            <div class="col-xs-12 table" id="divTable">
                                   
                            </div>

                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
</div>
   
<!-- /page content -->


<!-- Start Payment Modal -->
<div class="modal fade" id="ModalPayment" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" id="btnCloseUp" onclick="clear_values();" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
				<h4 class="modal-title" id="myModalLabel">Make Payment</h4>
			</div>

			<div class="modal-body">
            
                <div class="row">
                        
                    <form class="form-horizontal form-label-left" role="form" id="frmPayment" name="frmPayment" runat="server">
                           
                        <div class="form-group">
                            <label class="control-label col-md-2" for="ddlMethod">Vendor <span class="required">*</span></label>
                            <div class="col-md-10">
                                <select id="ddlVendor" name="ddlVendor" class="form-control col-md-7 col-xs-12 default-select2" data-live-search="true" data-placeholder="Select Vendor">
                                </select>
                            </div>
                        </div>
                         
                        <div class="form-group">
                            <label class="control-label col-md-2" for="ddlMethod">Method <span class="required">*</span></label>
                            <div class="col-md-10">
                                <select id="ddlPaymentMethod" name="ddlPaymentMethod" class="form-control col-md-7 col-xs-12 default-select2" data-live-search="true" data-placeholder="Select Payment Method">
                                </select>
                            </div>
                        </div>

                        <div class="form-group" style="display:none;" id="divBank">
                            <label class="control-label col-md-2" for="ddlBank">Bank <span class="required">*</span></label>
                            <div class="col-md-10">
                                <select id="ddlBank" name="ddlBank" class="form-control col-md-7 col-xs-12 default-select2" data-live-search="true" data-placeholder="Select Bank">
                                </select>
                            </div>
                        </div>

                        <div class="form-group" style="display:none;" id="divRider">
                            <label class="control-label col-md-2" for="ddlRider">Rider <span class="required">*</span></label>
                            <div class="col-md-10">
                                <select id="ddlRider" name="ddlRider" class="form-control col-md-7 col-xs-12 default-select2" data-live-search="true" data-placeholder="Select Rider">
                                </select>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-md-2" for="txtRemarks">Amount <span class="required">*</span></label>
                            <div class="col-md-5">
                                <input type="text" id="txtPayAmount" class="form-control" onkeypress="return validateNumbers(event)" />
                            </div>
                            <div class="col-md-5">
                                <label>Remaining Bal:</label> <label id="lblRemain"></label><br />
                                <small style="color:Red; display:none;" id="divErrorAmount">Amount is greater than Remaining</small>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-md-2" for="txtRemarks">Remarks</label>
                            <div class="col-md-10">
                                <textarea id="txtRemarks" cols="6" class="form-control"></textarea>
                            </div>
                        </div>

                        <label style="color:Red; display:none;margin-left: 102px;" id="divErrorValidation">Select Required Data</label>

                    </form>

                </div>
					
			</div>

			<div class="modal-footer">
                <button type="button" class="btn btn-primary" id="btnSave" data-loading-text="<i class='fa fa-spinner fa-spin '></i> Process...">Paid</button>
				<button type="button" id="btnCloseDown" onclick="clear_values();" class="btn btn-warning" data-dismiss="modal">Close</button>
			</div>

		</div>
	</div>
</div>
<!-- End Payment Modal -->

<input type="hidden" id="hremainamount" value="" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFooter" Runat="Server">

<script>

    var date_from = "";
    var date_to = "";
    var vendor = "";
    var vendor_id = 0;

    var $ddlVendor = $("#ddlVendor");
    var $ddlPaymentMethod = $("#ddlPaymentMethod");
    var $ddlBank = $("#ddlBank");
    var $ddlRider = $("#ddlRider");

    $(document).ready(function () {

        $('#myDatepicker1').datetimepicker({ format: 'DD-MMM-YYYY' });
        $('#myDatepicker2').datetimepicker({ format: 'DD-MMM-YYYY' });
        $(".default-select2").select2();

        //console.log(lastDate(new Date()));

        $("#txtFromDate").val(firstDate(new Date()));
        $("#txtToDate").val(lastDate(new Date()));

        GetPayments();

        $("#btnMakePayment").on('click', function () {

            //order_id = this.id;
            LoadVendors();
            LoadPaymentMethods();

            $('#ModalPayment').modal({ backdrop: 'static', keyboard: false });
            $('#ModalPayment').modal('show');
        });

        $ddlVendor.change(function () {

            vendor_id = $(this).val();

            jQuery.ajax({

                type: "POST",
                url: "RequestHandlerVendor.ashx?action=get_remaining_amount",
                cache: false,
                dataType: 'text',
                data: {
                    "vendor_id": vendor_id
                },
                success: function (data) {

                    //var myJSON = JSON.parse(data);
                    $("#lblRemain").text(data);
                    $("#hremainamount").val(data);
                }
            });

        });

        $ddlPaymentMethod.change(function () {

            var id = $(this).val();

            if (id == 1) {
                LoadBanks();
                $("#divBank").show();
                $("#divRider").hide();
            }
            else if (id == 2) {
                LoadRiders();
                $("#divBank").hide();
                $("#divRider").show();
            }
            else {
                $("#divBank").hide();
                $("#divRider").hide();
            }

        });

        $("#txtPayAmount").keyup(function () {

            var remain = $("#hremainamount").val();
            var paid = $(this).val() != '' ? $(this).val() : 0;

            var updated = parseInt(remain) - parseInt(paid);

            if (Number(paid) > Number(remain)) {
                //console.log('amount is greater');
                $("#divErrorAmount").show();
                $("#lblRemain").text(remain);
            }
            else {
                $("#divErrorAmount").hide();
                $("#lblRemain").text(updated);
            }

        });

        $('#btnSave').click(function () {

            var remarks = $("#txtRemarks").val();
            var method_id = $("#ddlPaymentMethod").val();
            var paid_amount = $("#txtPayAmount").val();
            var memo_id = 0;

            if (method_id == 1) {
                memo_id = $("#ddlBank").val();
            }
            else if (method_id == 2) {
                memo_id = $("#ddlRider").val();
            }


            var remain = $("#hremainamount").val();
            var paid = paid_amount != '' ? paid_amount : 0;
            
            var updated = parseInt(remain) - parseInt(paid);

            if (Number(paid) > Number(remain)) {
                //console.log('amount is greater');
                $("#divErrorAmount").show();
                $("#lblRemain").text(remain);
            }
            else {
                
                $("#divErrorAmount").hide();

                if (method_id == 3) {

                    if (paid != "" && paid != 0) {
                        SavePayment(method_id, memo_id, paid_amount, remarks);
                    }
                    else {
                        $("#divErrorValidation").show();
                    }

                    return false;
                }

                if (method_id != 0 && memo_id != 0 && (paid != "" && paid != 0)) {
                    SavePayment(method_id, memo_id, paid_amount, remarks);
                }
                else {
                    $("#divErrorValidation").show();
                }

            }

        });


        $('#btnSearch').click(function () {
            GetPayments();
        });

    });

    function SavePayment(method_id, memo_id, paid_amount, remarks) {

        $("#btnSave").button('loading');

        $("#divErrorAmount").hide();
        $("#divErrorValidation").hide();

        $.ajax({
            type: "POST",
            url: "RequestHandlerVendor.ashx?action=payment_save",
            data: {
                'vendor_id': vendor_id,
                'method_id': method_id,
                'memo_id': memo_id,
                'paid_amount': paid_amount,
                'remarks': remarks
            }
        }).done(function (data) {

            $("#btnSave").button('reset');
            console.log(data);

            if (data == '000') {
                $('#ModalPayment').modal('hide');
                clear_values();
                GetPayments();
                NotifySuccess('Payment Paid Successfully..!');
            }
            else {
                NotifyError('Amount is greater than Remaining');
            }

        });

    }

    function GetPayments() {

        $('#divTable').hide();
        $('#divLoader').show();

        date_from = $("#txtFromDate").val();
        date_to = $("#txtToDate").val();
        vendor = $("#txtVendor").val();

        jQuery.ajax({

            type: "POST",
            url: "RequestHandlerVendor.ashx?action=get_payments",
            cache: false,
            dataType: 'text',
            data: {
                "date_from": date_from,
                "date_to": date_to,
                "vendor": vendor
            },
            //async: false,
            success: function (data) {

                var myJSON = JSON.parse(data);
                //console.log(myJSON['data']);

                $("#divTable").html(myJSON['data']);
                $("#lblGrandTotal").html("Total Payments " + myJSON['GrandTotal'] + " PKR");
                $('#divTable').show();
                $('#divLoader').hide();

                $('#tblPayments').dataTable({
                    destroy: true,
                    searching: true,
                    pageLength: 10,
                    order: [[5, "desc"]]
                });
            }
        });

    }

    function LoadVendors() {

        $ddlVendor.html('<option value="0">Loading....</option>');
        $ddlVendor.attr('disabled', 'disabled');

        $.get("RequestHandlerVendor.ashx?action=get_vendor", function (response) {

            var result = response['data'];

            $ddlVendor.empty();

            $ddlVendor.append($("<option />").val(0).text("Select Vendor"));

            $.each(result, function (key, value) {
                $ddlVendor.append($("<option />").val(value.VendorId).text(value.VendorName));
            });

            $ddlVendor.removeAttr('disabled');
        });
    }

    function LoadPaymentMethods() {

        $ddlPaymentMethod.html('<option value="0">Loading....</option>');
        $ddlPaymentMethod.attr('disabled', 'disabled');

        $.get("RequestHandler.ashx?action=get_payment_method", function (response) {

            var result = response['data'];

            $ddlPaymentMethod.empty();

            $ddlPaymentMethod.append($("<option />").val(0).text("Select Payment Method"));

            $.each(result, function (key, value) {
                $ddlPaymentMethod.append($("<option />").val(value.PaymentMethodId).text(value.PaymentMethodName));
            });

            $ddlPaymentMethod.removeAttr('disabled');
        });
    }

    function LoadBanks() {

        $ddlBank.html('<option value="0">Loading....</option>');
        $ddlBank.attr('disabled', 'disabled');

        $.get("RequestHandler.ashx?action=get_bank", function (response) {

            var result = response['data'];

            $ddlBank.empty();

            $ddlBank.append($("<option />").val(0).text("Select Bank"));

            $.each(result, function (key, value) {
                $ddlBank.append($("<option />").val(value.BankId).text(value.BankName));
            });

            $ddlBank.removeAttr('disabled');
        });
    }

    function LoadRiders() {

        $ddlRider.html('<option value="0">Loading....</option>');
        $ddlRider.attr('disabled', 'disabled');

        $.get("RequestHandler.ashx?action=get_rider", function (response) {

            var result = response['data'];

            $ddlRider.empty();

            $ddlRider.append($("<option />").val(0).text("Select Rider"));

            $.each(result, function (key, value) {
                $ddlRider.append($("<option />").val(value.RiderId).text(value.RiderName));
            });

            $ddlRider.removeAttr('disabled');
        });
    }

    function firstDate(date) {

        var monthNames = [
            "Jan", "Feb", "Mar",
            "Apr", "May", "Jun", "Jul",
            "Aug", "Sep", "Oct",
            "Nov", "Dec"
        ];

        var day = 1; //date.getDate();
        var monthIndex = date.getMonth();
        var year = date.getFullYear();

        return day + '-' + monthNames[monthIndex] + '-' + year;
    }

    function lastDate(date) {

        var monthNames = [
            "Jan", "Feb", "Mar",
            "Apr", "May", "Jun", "Jul",
            "Aug", "Sep", "Oct",
            "Nov", "Dec"
        ];

        var day = new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
        var monthIndex = date.getMonth();
        var year = date.getFullYear();

        return day + '-' + monthNames[monthIndex] + '-' + year;
    }

    function clear_values() {
        $("#divErrorValidation").hide();
        $("#divErrorAmount").hide();
        $("#lblRemain").text('');
        $("#hremainamount").val('');
        $("#txtRemarks").val('');
        $("#txtPayAmount").val('');
        $("#divBank").hide();
        $("#divRider").hide();
    }

</script>

</asp:Content>

