<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="accounts.aspx.cs" Inherits="accounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

    <div class="">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>
                            Accounts List</h2>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="x_content" id="divTable" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFooter" Runat="Server">

<script>


    $('.btnVerified').on('click', function (e) {
        
        var account_id = this.id;
        var is_verified = 1;

        $.confirm({
            text: "Are you sure you want to Verify this Account?",
            title: "Confirmation required",
            confirm: function (button) {
                
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

    });

    $('.js-switch').on('change.bootstrapSwitch', function (e) {
        
        var account_id = this.id;
        var is_verified = e.target.checked;
        Verify(account_id, is_verified);
        //NotifySuccess('User has been Verified successfully');
        
    });

    function Verify(account_id,is_verified) {
        
        jQuery.ajax({

		    type: "POST",
		    url: "RequestHandler.ashx?action=verify",
		    cache: false,
		    dataType: 'text',
		    data: {
			    "account_id"  : account_id,
			    "is_verified" : is_verified,
		    },
		    async: false,
		    success: function(data) {
                
			    //console.log(is_verified);
			    //alert(data);
			
			    if(data == "000"){
				    if(is_verified == true){
					    NotifySuccess('User has been Verified successfully');
				    }else{
					    NotifySuccess('User has been Un-Verify successfully');
				    }
			    }
			
		    }
	    });
    }

    function NotifySuccess(text){
	    new PNotify({
		      title: 'Success',
		      text: text,
		      type: 'success',
		      hide: true,
		      delay: 2500,
		      styling: 'bootstrap3'
	      });
	}

</script>
</asp:Content>

