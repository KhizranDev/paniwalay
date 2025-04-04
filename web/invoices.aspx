<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="invoices.aspx.cs" Inherits="invoices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

<!-- page content -->
    
<div class="">
    
   <div class="x_panel" style="">
    <div class="x_title">
        <h2>Invoices</h2>
        <h2 style="float:right;" id="lblGrandTotal">Total Outstanding</h2>
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
                    Customer Name
                    <div class="form-group">
                        <input type='text' class="form-control" id="txtCustomer" />
                    </div>
                </div>

                <div class='col-sm-4'>
                    Tanker Type
                    <div class="form-group">
                        <select id="ddlTankerType" name="ddlTankerType" class="form-control col-md-7 col-xs-12 default-select2" data-live-search="true" data-placeholder="Select Tanker Type">
                        </select>
                    </div>
                </div>

                <div class='col-sm-4'>
                    Location
                    <div class="form-group">
                        <select id="ddlLocation" name="ddlLocation" class="form-control col-md-7 col-xs-12 default-select2" data-live-search="true" data-placeholder="Select Location">
                        </select>
                    </div>
                </div>

                <div class='col-sm-4'>
                    <br />
                    <div class="form-group">
                        <button type="button" class="col-md-5 col-xs-12 btn btn-md btn-info" id="btnSearch" data-loading-text="<i class='fa fa-spinner fa-spin '></i> Process...">Search</button>
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
				<h4 class="modal-title" id="myModalLabel">Add Payment</h4>
			</div>

			<div class="modal-body">
            
                    <div class="row">
                        
                        <div class="col-md-2">
                            <label>Order No.</label>
                        </div>
                        <div class="col-md-3">
                            <label style="font-weight:lighter;" id="lblOrderNo">PW-001</label>
                        </div>

                         <div class="col-md-2">
                            <label>Order Date: </label>
                        </div>
                        <div class="col-md-4">
                            <label style="font-weight:lighter;" id="lblOrderDate">2018-01-01</label>
                        </div>

                    </div>

                    <div class="row">
                        
                        <div class="col-md-2">
                            <label>Customer: </label>
                        </div>
                        <div class="col-md-3">
                            <label style="font-weight:lighter;" id="lblCustomerName">Noman Matin Khan</label>
                        </div>


                        <div class="col-md-2">
                            <label>Amount: </label>
                        </div>
                        <div class="col-md-3">
                            <label style="font-weight:lighter;" id="lblTotalAmount">10,000 PKR</label>
                        </div>

                    </div>

                    <div class="row">
                        
                        <form class="form-horizontal form-label-left" role="form" id="frmPayment" name="frmPayment" runat="server">
                            
                            <div class="form-group">
                                <label class="control-label col-md-2" for="ddlMethod">Method <span class="required">*</span></label>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="ddlMethod" CssClass="form-control col-md-7 col-xs-12" runat="server" DataTextField="PaymentMethodName" DataValueField="PaymentMethodId" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group" style="display:none;" id="divBank">
                                <label class="control-label col-md-2" for="ddlBank">Bank <span class="required">*</span></label>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="ddlBank" CssClass="form-control col-md-7 col-xs-12" runat="server" DataTextField="BankName" DataValueField="BankId" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group" style="display:none;" id="divRider">
                                <label class="control-label col-md-2" for="ddlRider">Rider <span class="required">*</span></label>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="ddlRider" CssClass="form-control col-md-7 col-xs-12" runat="server" DataTextField="RiderName" DataValueField="RiderId" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-2" for="txtRemarks">Amount <span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="text" id="txtPayAmount" class="form-control" onkeypress="return validateNumbers(event)" />
                                </div>
                                <div class="col-md-5">
                                    <label>Remaining Bal:</label> <label id="lblRemain">17200.00</label><br />
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

    var status_id = 4;
    var order_id = 0;
    var date_from = "";
    var date_to = "";
    var customer = "";

    var $ddlTankerType = $("#ddlTankerType");
    var $ddlLocation = $("#ddlLocation");

    $(document).ready(function () {

        $('#myDatepicker1').datetimepicker({ format: 'DD-MMM-YYYY' });
        $('#myDatepicker2').datetimepicker({ format: 'DD-MMM-YYYY' });
        $(".default-select2").select2();

        //console.log(lastDate(new Date()));

        $("#txtFromDate").val(firstDate(new Date()));
        $("#txtToDate").val(lastDate(new Date()));

        GetInvoices();
        LoadTankerType();
        LoadLocation();

        $("#divTable").on('click', '.btnPayment', function () {

            order_id = this.id;

            //var orderdate = $(this).data('orderdate');
            //alert(total_amount_check);

            $("#lblOrderNo").html($(this).data('order'));
            $("#lblOrderDate").html($(this).data('orderdate'));
            $("#lblCustomerName").html($(this).data('customer'));
            $("#lblTotalAmount").html($(this).data('totalamount'));
            $("#lblRemain").html($(this).data('remainamount'));
            $("#hremainamount").val($(this).data('remainamount'));

            $('#ModalPayment').modal({ backdrop: 'static', keyboard: false });
            $('#ModalPayment').modal('show');
        });

        $('#btnSave').click(function () {

            var remarks = $("#txtRemarks").val();
            var method_id = $("#ddlMethod").val();
            var paid_amount = $("#txtPayAmount").val();
            var memo_id = 0;

            if (method_id == 1) {
                memo_id = $("#ddlBank").val();
            }
            else if (method_id == 2) {
                memo_id = $("#ddlRider").val();
            }


            if (method_id == 3) {
                SaveInvoice(method_id, memo_id, paid_amount, remarks);
                return false;
            }

            if (method_id != 0 && memo_id != 0 && paid_amount != "") {
                SaveInvoice(method_id, memo_id, paid_amount, remarks);
            }
            else {
                $("#divErrorValidation").show();
            }

        });

        $("#ddlMethod").change(function () {

            var id = $(this).val();

            if (id == 1) {
                $("#divBank").show();
                $("#divRider").hide();
            }
            else if (id == 2) {
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

        $('#btnSearch').click(function () {
            GetInvoices();
        });

    });

    function SaveInvoice(method_id, memo_id, paid_amount, remarks) {

        $("#btnSave").button('loading');

        $("#divErrorAmount").hide();
        $("#divErrorValidation").hide();

        $.ajax({
            type: "POST",
            url: "RequestHandler.ashx?action=payment_save",
            data: {
                'order_id': order_id,
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
                GetInvoices();
                NotifySuccess('Payment Paid Successfully..!');
            }
            else {
                NotifyError('Error Occured..!');
            }

        });
        
    }

    function GetInvoices() {

        $('#divTable').hide();
        $('#divLoader').show();

        date_from = $("#txtFromDate").val();
        date_to = $("#txtToDate").val();
        customer = $("#txtCustomer").val();

        jQuery.ajax({

            type: "POST",
            url: "RequestHandler.ashx?action=get_invoices",
            cache: false,
            dataType: 'text',
            data: {
                "date_from"    : date_from,
                "date_to"      : date_to,
                "customer"     : customer,
                "tanker_type"  : $ddlTankerType.val(),
                "location"     : $ddlLocation.val()
            },
            //async: false,
            success: function (data) {

                var myJSON = JSON.parse(data);
                //console.log(myJSON['data']);

                $("#divTable").html(myJSON['data']);
                $("#lblGrandTotal").html("Total Outstanding " + myJSON['GrandTotal'] + " PKR");
                $('#divTable').show();
                $('#divLoader').hide();

                $('#tblInvoices').dataTable({
                    destroy: true,
                    searching: true,
                    pageLength: 10,
                    order: [[1, "desc"]]
                });
            }
        });

    }

    function LoadTankerType() {

        $ddlTankerType.html('<option value="0">Loading....</option>');
        $ddlTankerType.attr('disabled', 'disabled');

        $.get("RequestHandler.ashx?action=get_tanker_type", function (response) {
            
            var result = response['data'];

            $ddlTankerType.empty();

            $ddlTankerType.append($("<option />").val(0).text("Select Tanker Type"));

            $.each(result, function (key, value) {
                $ddlTankerType.append($("<option />").val(value.TankerTypeId).text(value.TankerTypeName));
            });

            $ddlTankerType.removeAttr('disabled');
        });
    }

    function LoadLocation() {

        $ddlLocation.html('<option value="0">Loading....</option>');
        $ddlLocation.attr('disabled', 'disabled');

        $.get("RequestHandler.ashx?action=get_location", function (response) {

            var result = response['data'];

            $ddlLocation.empty();

            $ddlLocation.append($("<option />").val(0).text("Select Location"));

            $.each(result, function (key, value) {
                $ddlLocation.append($("<option />").val(value.LocationId).text(value.LocationName));
            });

            $ddlLocation.removeAttr('disabled');
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

    function validateNumbers(key) {
        //getting key code of pressed key
        var keycode = (key.which) ? key.which : key.keyCode;
        //comparing pressed keycodes
        if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
            return false;
        }
        else {
            return true;
        }
    }

    function clear_values() {
        $("#divErrorAmount").hide();
        $("#divErrorValidation").hide();
        $('#txtPayAmount').val('');
        $('#ddlMethod').val(0);
        $('#divBank').hide();
        $('#divRider').hide();
    }

</script>

</asp:Content>

