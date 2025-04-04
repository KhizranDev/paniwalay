<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="orders.aspx.cs" Inherits="orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">

<style>
    
    .ulclass
    {
        margin:0px 0px 0px -30px;
    }
    
    .liclass 
    {
        position:relative; 
        float:left; 
        display:block; 
        list-style :none;
    }
    
    
    
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
        
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Order List</h2>

                     <div class="clearfix">
                    </div>

                </div>

                <div class="row">
                    <ul class="ulclass">
                        <li class="liclass"><button class="btn btn-primary btnFilter" id="1">New Orders</button></li>
                        <li class="liclass"><button class="btn btn-success btnFilter" id="2">In-Progress</button></li>
                        <li class="liclass"><button class="btn btn-info btnFilter" id="3">All Pending</button></li>
                    </ul>    
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

    <div class="modal fade" id="ModalOrderDetails" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h3>Order Detail</h3>
                </div>
                <div class="modal-body" style="max-height: 450px; overflow-y: auto; overflow-x: hidden;" id="divOrderDetail">
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <th>Tanker Type</th>
                                <th>Order Type</th>
                                <th>Amount</th>
                                <th>Qty</th>
                                <th>Total Amount</th>
                            </tr>
                        </thead>
                        <tbody>

                            <tr>
                                <td style="padding: 2px;">deal of 50 items <br><br></td>
                                <td style="padding: 2px;"> Schedule <br /> Date: 2018-09-15 </td>
                                <td style="padding: 2px; text-align: right;">PKR 500</td>
                                <td style="padding: 2px; text-align: right;">2</td>
                                <td style="padding: 2px; text-align: right;">PKR 1,000</td>
                            </tr>
                            
                            <tr>
                                <td style="padding: 2px; text-align: right; font-weight: bold;" colspan="4">Bill Amount</td>
                                <td style="padding: 2px; text-align: right; font-weight: bold;">PKR 3,155</td>
                            </tr>

                        </tbody>
                    </table>
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <th>Customer Information</th>
                                <th>Delivery Information</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <strong>Noman Khan</strong>
                                    <br>
                                    noman.khan330@gmail.com
                                    <br>
                                    Contact No.03102338276
                                    <br>
                                </td>
                                <td>
                                    <strong>DHA</strong>
                                    <br>
                                    gurunank-pura, street no.2, 1st chowk, Sohail Sharif, Baba jani girls hostel, faisalabad
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <strong>Payment Details :</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <strong>Payment Details :</strong>
                                </td>
                            </tr>
                        </tbody>
                    </table>
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

    <!-- Start Vendor Order Modal -->
    <div class="modal fade" id="ModalVendorOrder" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
	    <div class="modal-dialog">
		    <div class="modal-content">
			    <div class="modal-header">
				    <button type="button" id="btnCloseUp" onclick="clearValuesVendor();" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
				    <h4 class="modal-title" id="myModalLabel">Vendor Assign</h4>
			    </div>

			    <div class="modal-body">
            
                        <div class="row" style="display:none;">
                        
                            <div class="col-md-2">
                                <label>Order No.</label>
                            </div>
                            <div class="col-md-3">
                                <label style="font-weight:lighter;" id="lblOrderNo">PW-001</label>
                            </div>

                             <div class="col-md-3">
                                <label>Order Date: </label>
                            </div>
                            <div class="col-md-4">
                                <label style="font-weight:lighter;" id="lblOrderDate">2018-01-01</label>
                            </div>

                        </div>

                        <div class="row" style="display:none;">
                        
                            <div class="col-md-2">
                                <label>Customer: </label>
                            </div>
                            <div class="col-md-3">
                                <label style="font-weight:lighter;" id="lblCustomerName">Noman Matin Khan</label>
                            </div>


                            <div class="col-md-3">
                                <label>Customer Amount: </label>
                            </div>
                            <div class="col-md-3">
                                <label style="font-weight:lighter;" id="lblTotalAmount">10,000 PKR</label>
                            </div>

                        </div>

                        <div class="row">
                        
                            <form class="form-horizontal form-label-left" role="form" id="frmVendorOrder" name="frmVendorOrder" runat="server">
                            
                                <div class="form-group">
                                    <label class="control-label col-md-2" for="ddlVendors">Vendor <span class="required">*</span></label>
                                    <div class="col-md-10">
                                        <asp:DropDownList ID="ddlVendors" CssClass="form-control col-md-7 col-xs-12" runat="server" DataTextField="VendorName" DataValueField="VendorId" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label class="control-label col-md-2" for="txtRate">Rate <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <input type="text" id="txtRate" class="form-control" onkeypress="return validateNumbers(event)" />
                                    </div>

                                    <label class="control-label col-md-2" for="txtRate">Quantity</label>
                                    <div class="col-md-2">
                                        <input type="text" id="txtQuantity" class="form-control" onkeypress="return validateNumbers(event)" readonly="readonly" />
                                    </div>

                                    <label class="control-label col-md-2" for="txtAmount">Amount</label>
                                    <div class="col-md-2">
                                        <input type="text" id="txtAmount" class="form-control" onkeypress="return validateNumbers(event)" readonly="readonly" />
                                    </div>
                                </div>

                                <label style="color:Red; display:none;margin-left: 102px;" id="divErrorValidation">Select Required Data</label>

                            </form>

                        </div>
					
			    </div>

			    <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="btnVendorOrder" data-loading-text="<i class='fa fa-spinner fa-spin '></i> Process...">Assign</button>
				    <button type="button" id="btnCloseDown" onclick="clearValuesVendor();" class="btn btn-warning" data-dismiss="modal">Close</button>
			    </div>

		    </div>
	    </div>
    </div>
    <!-- End Vendor Order Modal -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFooter" Runat="Server">

<script>

    var status_id = 0;
    var order_id = 0;
    var account_id = 0;
    var tanker_type_id = 0;
    var location_id = 0;
    var vendor_id = 0;
    var quantity = 0;

    $(document).ready(function () {

        GetOrders();

        $('.btnFilter').on('click', function (e) {
            status_id = this.id;
            GetOrders();
        });

        $(document).on('click', '.btnView', function () {

            order_id = $(this).data('id');

            $.ajax({
                type: "POST",
                url: "RequestHandler.ashx?action=get_orders_detail",
                data: {
                    'order_id': order_id
                }
            }).done(function (data) {

                console.log(data);
                $('#divOrderDetail').html(data['data']);
                $('#ModalOrderDetails').modal();
                $("#divBillAmount").html("Bill Amount : " + data['TotalAmount'] + "PKR");

            }).fail(function (msg) {
                console.log(msg);
            }).always(function () {

            });

            return false;

        });

        $(document).on('click', '.btnChangeStatus', function () {

            order_id = $(this).data('id');
            account_id = $(this).data('accountid');

            tanker_type_id = $(this).data('tankertypeid');
            location_id = $(this).data('locationid');
            quantity = $(this).data('quantity');

            var order_status = $(this).html();
            var next_order_status_id = $(this).data('nextstatus');
            var next_order_status_name = $(this).data('nextstatusname');
            var is_vendor_order = $(this).data('isvendororder');

            //alert(quantity);
            //return false;

            if (is_vendor_order == 0) {
                
                $('#ModalVendorOrder').modal({ backdrop: 'static', keyboard: false });
                $('#ModalVendorOrder').modal('show');

            } else {

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
                   
            }

            return false;
        });

    });


    $(document).on('change', '#ddlVendors', function () {

        vendor_id = $(this).val();

        jQuery.ajax({
            type: "POST",
            url: "RequestHandlerVendor.ashx?action=get_vendor_rate",
            cache: false,
            dataType: 'text',
            data: {
                "vendor_id": vendor_id,
                "tanker_type_id": tanker_type_id,
                "location_id": location_id
            },
            success: function (data) {
                $("#txtQuantity").val(quantity);
                $("#txtRate").val(data);
                $("#txtAmount").val(data * quantity);
                //alert(data);

            }
        });

    });

    $(document).on('click', '#btnVendorOrder', function () {

        var vendor_rate = $("#txtRate").val();
        var vendor_amount = $("#txtAmount").val();

        if (vendor_id != 0 && vendor_rate != '') {

            $("#divErrorValidation").hide();

            $("#btnVendorOrder").button('loading');

            jQuery.ajax({

                type: "POST",
                url: "RequestHandlerVendor.ashx?action=save_vendor_order",
                cache: false,
                dataType: 'text',
                data: {
                    "order_id": order_id,
                    "tanker_type_id": tanker_type_id,
                    "location_id": location_id,
                    "vendor_id": vendor_id,
                    "vendor_rate": vendor_rate,
                    "quantity": quantity,
                    "vendor_amount": vendor_amount
                },
                success: function (data) {

                    $("#btnVendorOrder").button('reset');

                    if (data == "000") {
                        UpdateOrderStatus(2, '');
                        $('#ModalVendorOrder').modal('hide');
                    } else {
                        NotifyError('Error Occured..!');
                    }

                }
            });

        } else {
            $("#divErrorValidation").show();
        }


    });

    $("#txtRate").keyup(function () {
        
        var amount = $(this).val() * quantity;
        $("#txtAmount").val(amount);

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
                    aaSorting : [[ 1, "desc" ]]
                });
            }
        });

    }

    function UpdateOrderStatus(next_order_status_id,remarks) {
        
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
            } else if (data == '002') {
                NotifyError('Account not verified..!');
            } else {
                NotifyError('Error Occured..!');
            }

            GetOrders();

        })
        .fail(function (msg) {
            console.log(msg);
        })
        .always(function () {

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


    function clearValuesVendor() {
        $('#txtRate').val('');
        $('#txtAmount').val('');
        $("#divErrorValidation").hide();
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

</script>

</asp:Content>

