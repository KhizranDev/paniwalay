<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vendors_order.aspx.cs" Inherits="vendors_order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">


<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
                <h2>Vendors Order List</h2>
                <div class="clearfix"></div>
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

 <form id="form1" runat="server">
    <input type="hidden" id="txtVendorId" runat="server" clientidmode="Static" />
 </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFooter" Runat="Server">

<script>

    var order_id = 0;

    $(document).ready(function () {

        GetOrders();

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
    });

    function GetOrders() {

        var vendor_id = $("#txtVendorId").val();

        $('#divTable').hide();
        $('#divLoader').show();

        jQuery.ajax({

            type: "POST",
            url: "RequestHandlerVendor.ashx?action=get_orders",
            cache: false,
            dataType: 'text',
            data: {
                "vendor_id": vendor_id
            },
            //async: false,
            success: function (data) {

                $("#divTable").html(data);

                $('#divTable').show();
                $('#divLoader').hide();

                $('#data-table').DataTable({
                    responsive: false,
                    aaSorting: [[1, "desc"]]
                });
            }
        });

    }

</script>

</asp:Content>

