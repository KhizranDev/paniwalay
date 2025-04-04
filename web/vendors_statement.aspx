<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vendors_statement.aspx.cs" Inherits="vendors_statement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

<!-- page content -->
    
<div class="">

    <div class="x_panel" style="">
        <div class="x_title">
            <h2>Accounts Statement</h2>
            <h2 style="float:right;" id="lblGrandTotal"></h2>
            <div class="clearfix"></div>
        </div>
        <div class="x_content">
            <div class="container">
                <div class="row">

                <form id="form1" autocomplete="off">

                    <div class='col-sm-3'>
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

                    <div class='col-sm-3'>
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
                
                    <div class='col-sm-3'>
                        Vendor
                        <div class="form-group">
                            <select id="ddlVendor" name="ddlVendor" class="form-control col-md-7 col-xs-12 default-select2" data-live-search="true" data-placeholder="Select Vendor">
                            </select>
                        </div>
                    </div>


                    <div class='col-sm-3'>
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

    var date_from = "";
    var date_to = "";
    var vendor_id = 0;

    var $ddlVendor = $("#ddlVendor");

    $(document).ready(function () {

        $('#myDatepicker1').datetimepicker({ format: 'DD-MMM-YYYY' });
        $('#myDatepicker2').datetimepicker({ format: 'DD-MMM-YYYY' });
        $(".default-select2").select2();

        LoadVendors();

        //console.log(lastDate(new Date()));

        $("#txtFromDate").val(firstDate(new Date()));
        $("#txtToDate").val(lastDate(new Date()));

        $('#btnSearch').click(function () {
            GetStatements();
        });

    });

    function GetStatements() {

        $('#divTable').hide();
        $('#divLoader').show();

        date_from = $("#txtFromDate").val();
        date_to = $("#txtToDate").val();
        vendor_id = $ddlVendor.val();

        jQuery.ajax({

            type: "POST",
            url: "RequestHandlerVendor.ashx?action=get_statements",
            cache: false,
            dataType: 'text',
            data: {
                "date_from": date_from,
                "date_to": date_to,
                "vendor_id": vendor_id
            },
            success: function (data) {
                //console.log(data);
                $("#divTable").html(data);
                $('#divTable').show();
                $('#divLoader').hide();

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

