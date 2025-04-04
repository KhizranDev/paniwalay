<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="add_rate.aspx.cs" Inherits="add_rate" %>

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
                            Location <small>Add or Edit Location</small></h2>
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
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtId">ID</label>
                                <div class="col-md-2 col-sm-2 col-xs-12">
                                    <asp:TextBox ID="txtId" runat="server" CssClass="form-control col-md-7 col-xs-12" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtDescription">Location Name <span class="required">*</span></label>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <asp:DropDownList ID="ddlLocation" data-live-search="true" CssClass="form-control col-md-7 col-xs-12 default-select2" runat="server" DataTextField="LocationName" DataValueField="LocationId" ClientIDMode="Static">
                                    </asp:DropDownList>
                                    <div class="input-error form-control-input" style="color: Red; display: none;">Location Name is required</div>
                                </div>
                            </div>

                           <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtStartDate">Start Date <span class="required">*</span></label>
                                <div class=' col-md-4 col-sm-4 col-xs-12 date'>
                                    <input id="txtStartDate" name="txtStartDate" data-format="dd/MM/yyyy" placeholder="dd/MM/yyyy" class="form-control" runat="server" clientidmode="Static" />
                                    <div class="input-error form-control-input" style="color: Red; display: none;">Start Date is required</div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtEndDate">End Date <span class="required">*</span></label>
                                <div class=' col-md-4 col-sm-4 col-xs-12 date'>
                                    <input id="txtEndDate" name="txtEndDate" data-format="dd/MM/yyyy" placeholder="dd/MM/yyyy" class="form-control" runat="server" clientidmode="Static" />
                                    <div class="input-error form-control-input" style="color: Red; display: none;">End Date is required</div>
                                </div>
                            </div>
                          
                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtRate">Rate <span class="required">*</span></label>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <input type="text" id="txtRate" name="txtRate" class="form-control" runat="server" clientidmode="Static" />
                                    <div class="input-error form-control-input" style="color: Red; display: none;">Rate is required</div>
                                </div>
                            </div>

                            <div class="ln_solid"></div>
                            
                            <div class="form-group">
                                <div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-3">
                                    <%--<input type="button" id="btnSave" value="Save" class="btn btn-success" />--%>
                                    <button type="button" class="btn btn-success" id="btnSave" data-loading-text="<i class='fa fa-spinner fa-spin '></i> Process...">Save</button>
                                    <a class="btn btn-danger" href="rates.aspx">Back</a>
                                </div>
                            </div>

                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFooter" Runat="Server">

<script src="vendors/moment/min/moment.min.js"></script> 
<script src="vendors/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js"></script>

<script>

    $(document).ready(function () {

        $(".default-select2").select2();

        $("#txtStartDate").datepicker({
            format: 'dd-MM-yyyy',
            autoclose: !0
        });

        $("#txtEndDate").datepicker({
            format: 'dd-MM-yyyy',
            autoclose: !0
        });

        $('#btnSave').on('click', function (e) {

            var rate_id = $("#txtId").val();
            var location_id = $("#ddlLocation").val();
            var start_date = $("#txtStartDate").val();
            var end_date = $("#txtEndDate").val();
            var rate = $("#txtRate").val();


            if (validation()) {

                $("#btnSave").button('loading');

                jQuery.ajax({

                    type: "POST",
                    url: "RequestHandler.ashx?action=save_rate",
                    cache: false,
                    dataType: 'text',
                    data: {
                        "rate_id": rate_id,
                        "location_id": location_id,
                        "start_date": start_date,
                        "end_date": end_date,
                        "rate": rate
                    },
                    async: false,
                    success: function (data) {
                        //alert(data);
                        console.log(data);

                        $("#btnSave").button('reset');

                        if (data == "000") {
                            $('#notify_success_insert').show();
                            clearValues();
                        } else {
                            $('#notify_error_insert').show();
                        }

                        setTimeout(function () {
                            window.location.href = "rates.aspx";
                        }, 5000);
                    }
                });
            }

        });

    });

    function validation() {

        var hasFocus = false;
        var errCount = 0;

        if ($('#ddlLocation').val() == 0) {

            $('#ddlLocation').parent().addClass('has-error');
            $('#ddlLocation').parent().find('.input-error').show().css('display', 'inline-block');

            if (!hasFocus) {
                $('#ddlLocation').focus();
                hasFocus = true;
            }
            errCount++;
        }
        else {
            $('#ddlLocation').parent().removeClass('has-error');
            $('#ddlLocation').parent().find('.input-error').hide();
        }

        if ($('#txtStartDate').val() == '') {

            $('#txtStartDate').parent().addClass('has-error');
            $('#txtStartDate').parent().find('.input-error').show().css('display', 'inline-block');

            if (!hasFocus) {
                $('#txtStartDate').focus();
                hasFocus = true;
            }
            errCount++;
        }
        else {
            $('#txtStartDate').parent().removeClass('has-error');
            $('#txtStartDate').parent().find('.input-error').hide();
        }

        if ($('#txtEndDate').val() == '') {

            $('#txtEndDate').parent().addClass('has-error');
            $('#txtEndDate').parent().find('.input-error').show().css('display', 'inline-block');

            if (!hasFocus) {
                $('#txtEndDate').focus();
                hasFocus = true;
            }
            errCount++;
        }
        else {
            $('#txtEndDate').parent().removeClass('has-error');
            $('#txtEndDate').parent().find('.input-error').hide();
        }

        if ($('#txtRate').val() == '') {

            $('#txtRate').parent().addClass('has-error');
            $('#txtRate').parent().find('.input-error').show().css('display', 'inline-block');

            if (!hasFocus) {
                $('#txtRate').focus();
                hasFocus = true;
            }
            errCount++;
        }
        else {
            $('#txtRate').parent().removeClass('has-error');
            $('#txtRate').parent().find('.input-error').hide();
        }

        if (errCount > 0) {
            return false;
        } else {
            return true;
        }

    }

    function clearValues() {

        $('#ddlLocation').prop('selectedIndex', 0);
        $('#select2-ddlLocation-container').html('Select Location');

        $("#txtStartDate").val('');
        $("#txtEndDate").val('');
        $("#txtRate").val('');
    }
</script>

</asp:Content>

