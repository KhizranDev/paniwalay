<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="payments.aspx.cs" Inherits="payments" %>

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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFooter" Runat="Server">

<script>

    var status_id = 5;
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

        GetPayments();
        LoadTankerType();
        LoadLocation();

        $('#btnSearch').click(function () {
            GetPayments();
        });

    });

    function GetPayments() {

        $('#divTable').hide();
        $('#divLoader').show();

        date_from = $("#txtFromDate").val();
        date_to = $("#txtToDate").val();
        customer = $("#txtCustomer").val();

        jQuery.ajax({

            type: "POST",
            url: "RequestHandler.ashx?action=get_payments",
            cache: false,
            dataType: 'text',
            data: {
                "date_from": date_from,
                "date_to": date_to,
                "customer": customer,
                "tanker_type": $ddlTankerType.val(),
                "location": $ddlLocation.val()
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

</script>

</asp:Content>

