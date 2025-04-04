<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="add_vendor.aspx.cs" Inherits="add_vendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

<div class="">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>
                            Vendor <small>Add or Edit Vendor</small></h2>
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
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtVendorName">Vendor Name <span class="required">*</span></label>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                                    <div class="input-error form-control-input" style="color: Red; display: none;">Vendor Name is required</div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtEmail">Email <span class="required">*</span></label>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                                    <div class="input-error form-control-input" style="color: Red; display: none;">Email is required</div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtAddress">Address</label>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control col-md-7 col-xs-12" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtCNIC">CNIC <span class="required">*</span></label>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <asp:TextBox ID="txtCNIC" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                                    <div class="input-error form-control-input" style="color: Red; display: none;">CNIC Number is required</div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3 col-xs-12" for="txtMobileNo">Mobile No <span class="required">*</span></label>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                                    <div class="input-error form-control-input" style="color: Red; display: none;">Mobile Number is required</div>
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
                                 <a class="btn btn-danger" href="vendors.aspx">Back</a>
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

    $().ready(function () {

        $('#<%= btnSave.ClientID %>').click(function () {

            var hasFocus = false;
            var errCount = 0;

            if ($('#<%= txtVendorName.ClientID %>').val() == '') {

                $('#<%= txtVendorName.ClientID %>').parent().addClass('has-error');
                $('#<%= txtVendorName.ClientID %>').parent().find('.input-error').show().css('display', 'inline-block');

                if (!hasFocus) {
                    $('#<%= txtVendorName.ClientID %>').focus();
                    hasFocus = true;
                }
                errCount++;
            }
            else {
                $('#<%= txtVendorName.ClientID %>').parent().removeClass('has-error');
                $('#<%= txtVendorName.ClientID %>').parent().find('.input-error').hide();
            }


            if ($('#<%= txtEmail.ClientID %>').val() == '') {

                $('#<%= txtEmail.ClientID %>').parent().addClass('has-error');
                $('#<%= txtEmail.ClientID %>').parent().find('.input-error').show().css('display', 'inline-block');

                if (!hasFocus) {
                    $('#<%= txtEmail.ClientID %>').focus();
                    hasFocus = true;
                }
                errCount++;
            }
            else {
                $('#<%= txtEmail.ClientID %>').parent().removeClass('has-error');
                $('#<%= txtEmail.ClientID %>').parent().find('.input-error').hide();
            }


            if ($('#<%= txtMobileNo.ClientID %>').val() == '') {

                $('#<%= txtMobileNo.ClientID %>').parent().addClass('has-error');
                $('#<%= txtMobileNo.ClientID %>').parent().find('.input-error').show().css('display', 'inline-block');

                if (!hasFocus) {
                    $('#<%= txtCNIC.ClientID %>').focus();
                    hasFocus = true;
                }
                errCount++;
            }
            else {
                $('#<%= txtMobileNo.ClientID %>').parent().removeClass('has-error');
                $('#<%= txtMobileNo.ClientID %>').parent().find('.input-error').hide();
            }

            if (errCount > 0)
                return false;


        });
    });

</script>

</asp:Content>

