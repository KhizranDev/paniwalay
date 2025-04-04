<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="tanker_type.aspx.cs" Inherits="tanker_type" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

    <div class="noty_bar noty_theme_default noty_layout_top noty_success NotificationDiv"
        id="notify_success_delete" style="cursor: pointer; display: none;">
        <div class="noty_message">
            <span class="noty_text">Record Archive Successfully</span>
        </div>
    </div>
    <div class="noty_bar noty_theme_default noty_layout_top noty_error NotificationDiv"
        id="notify_error_delete" style="cursor: pointer; display: none;">
        <div class="noty_message">
            <span class="noty_text">Error in Archive, Please try again!</span>
        </div>
    </div>
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
                            Tanker Type List</h2>
                        <ul class="nav navbar-right panel_toolbox">
                             <li>
                                <button class="btn btn-warning" onclick="window.location='tanker_type_archive.aspx';">
                                    Archive Tanker Type</button></li>

                            <li>
                                <button class="btn btn-primary" onclick="window.location='add_tanker_type.aspx';">
                                    Add Tanker Type</button></li>
                        </ul>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="x_content" id="divTable" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>

  <form role="form" class="form-inline" id="form1" runat="server">
    <input type="hidden" id="txtHiddenValue" runat="server" />
  </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFooter" Runat="Server">

<script>

    $().ready(function () {

        var value = $('#<%= txtHiddenValue.ClientID %>').val();

        if (value == "delete_success")
            $('#notify_success_delete').show();

        if (value == "delete_error")
            $('#notify_error_delete').show();


        if (value == "insert_success") {
            $('#notify_success_insert').show();
        }

        if (value == "insert_error")
            $('#notify_error_insert').show();


        setTimeout(function () {
            $('.NotificationDiv').fadeOut('fast');
        }, 5000); // <-- time in milliseconds


    });


</script>


 <script>

    $('.btnArchive').on('click', function (e) {

        var id = this.id;

        $.confirm({
            text: "Are you sure you want to Archive this Record?",
            title: "Confirmation required",
            confirm: function (button) {
                Archive(id);
            },
            cancel: function (button) {
                // nothing to do
            },
            confirmButton: "Yes I am",
            cancelButton: "No",
            post: true,
            confirmButtonClass: "btn-warning",
            cancelButtonClass: "btn-default",
            dialogClass: "modal-dialog delete_confirmation" // Bootstrap classes for large modal
        });

        return false;

    });

    function Archive(id) {

        jQuery.ajax({

            type: "POST",
            url: "RequestHandler.ashx?action=delete_tanker_type",
            cache: false,
            dataType: 'text',
            data: {
                "id"       : id,
                "status"   : "R",
            },
            async: false,
            success: function (data) {

                //console.log(is_verified);
                //alert(data);

                if (data == "000") {
                    $('#notify_success_delete').show();
                } else {
                    $('#notify_error_delete').show();
                }

                setTimeout(function () {
                    window.location.href = "tanker_type.aspx";
                }, 2000);

            }
        });

    }

</script>

</asp:Content>

