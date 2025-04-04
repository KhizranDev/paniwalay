<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="add_notification.aspx.cs" Inherits="add_notification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">

<style>
    .text-area
    {
        height: 121px !important;
        margin: 0px !important;
        width: 377px !important;
    }
    
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

<div class="">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>
                            Notification <small>Add or Edit Notification</small></h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </ul>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="x_content">
                        <br />
                        <form id="form1" runat="server" class="form-horizontal form-label-left">
                            
                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtId">ID</label>
                                <div class="col-md-2 col-sm-2 col-xs-12">
                                    <asp:TextBox ID="txtId" runat="server" CssClass="form-control col-md-7 col-xs-12" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtTitle">Title <span class="required">*</span></label>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                                    <div class="input-error form-control-input" style="color: Red; display: none;">Title is required</div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtDescription">Description</label>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control col-md-7 col-xs-12 text-area" TextMode="MultiLine"></asp:TextBox>
                                    <div class="input-error form-control-input" style="color: Red; display: none;">Description is required</div>
                                </div>
                            </div>

                            <div class="form-group">
						        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtStartDate">Start Date <span class="required">*</span></label>
						        <div class="col-md-4 col-sm-4 col-xs-12">
							        <input type="text" class="form-control datepicker-autoClose" id="txtStartDate" name="txtStartDate" value="" runat="server" clientidmode="Static" />
						        </div>
					        </div>
                            
                            <div class="form-group">
						        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtEndDate">End Date <span class="required">*</span></label>
						        <div class="col-md-4 col-sm-4 col-xs-12">
							        <input type="text" class="form-control datepicker-autoClose" id="txtEndDate" name="txtStartDate" value="" runat="server" clientidmode="Static" />
						        </div>
					        </div>


                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtAddress">Image</label>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control form-control-input" ClientIDMode="Static" />
                                    <input type="hidden" id="txtImageURL" runat="server" clientidmode="Static" />
                                </div>
                            </div>

                             <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12">Is Active</label>
                                <div class="col-md-9 col-sm-9 col-xs-12">
                                    <div class="">
                                    <label>
                                        <input type="checkbox" class="js-switch" id="chkIsActive"  runat="server" />
                                    </label>
                                    </div>
                                </div>
                            </div>

                            <div class="ln_solid"></div>
                            
                            <div class="form-group">
                                <div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-3">
                                <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="btn btn-success" OnClick="btnSave_Click" />
                                 <a class="btn btn-danger" href="notification.aspx">Back</a>
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

<script>

    $(document).ready(function () {

        $(".datepicker-autoClose").datepicker({
            format: 'dd-M-yyyy',
            todayHighlight: !0,
            autoclose: !0
        });

        var id = $('#<%= txtId.ClientID %>').val();

        if (id == "") {
            $("#txtStartDate").val(firstDate(new Date()));
            $("#txtEndDate").val(lastDate(new Date()));
        }

        $('#<%= btnSave.ClientID %>').click(function () {

            var hasFocus = false;
            var errCount = 0;

            if ($('#<%= txtTitle.ClientID %>').val() == '') {

                $('#<%= txtTitle.ClientID %>').parent().addClass('has-error');
                $('#<%= txtTitle.ClientID %>').parent().find('.input-error').show().css('display', 'inline-block');

                if (!hasFocus) {
                    $('#<%= txtTitle.ClientID %>').focus();
                    hasFocus = true;
                }
                errCount++;
            }
            else {
                $('#<%= txtTitle.ClientID %>').parent().removeClass('has-error');
                $('#<%= txtTitle.ClientID %>').parent().find('.input-error').hide();
            }

            if ($('#<%= txtDescription.ClientID %>').val() == '') {

                $('#<%= txtDescription.ClientID %>').parent().addClass('has-error');
                $('#<%= txtDescription.ClientID %>').parent().find('.input-error').show().css('display', 'inline-block');

                if (!hasFocus) {
                    $('#<%= txtDescription.ClientID %>').focus();
                    hasFocus = true;
                }
                errCount++;
            }
            else {
                $('#<%= txtDescription.ClientID %>').parent().removeClass('has-error');
                $('#<%= txtDescription.ClientID %>').parent().find('.input-error').hide();
            }

            if (errCount > 0)
                return false;


        });

    });


    function firstDate(date) {

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

