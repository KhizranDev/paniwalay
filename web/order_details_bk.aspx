<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="order_details_bk.aspx.cs" Inherits="order_details_bk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <div class="noty_bar noty_theme_default noty_layout_top noty_success NotificationDiv"
        id="notify_success_insert" style="cursor: pointer; display: none;">
        <div class="noty_message">
            <span class="noty_text">Status Update Successfully</span>
        </div>
    </div>
    <div class="noty_bar noty_theme_default noty_layout_top noty_error NotificationDiv"
        id="notify_error_insert" style="cursor: pointer; display: none;">
        <div class="noty_message">
            <span class="noty_text">Error while updating status, Please try again!</span>
        </div>
    </div>

    <div class="clearfix">
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Order Details</h2>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="x_content">
                    <section class="content invoice">
                     
                      <!-- info row -->
                      <div class="row invoice-info">

                      <form id="form1" runat="server" class="form-horizontal form-label-left" autocomplete="off">
                        <div class="col-sm-6">
                            
                            <input type="hidden" id="txtOrderId" runat="server" clientidmode="Static" value="" />

                            <p><strong>Order No: </strong> <span id="txtOrder" runat="server">PW-001</span> </p>
                            <p><strong>Order Date: </strong> <span id="txtOrderDate" runat="server">12-Sep-2018</span> </p>
                            <p><strong>Customer: </strong> <span id="txtCustomer" runat="server">Noman Khan</span> </p>
                            <p class="order-detail-para"> <span id="txtEmail" runat="server">noman.khan330@gmail.com</span> </p>
                            <p class="order-detail-para"> <span id="txtContactNo" runat="server">03102338276</span> </p>
                            <p id="txtVerified" class="order-detail-para" runat="server" visible="false"><button type='button' class='btn btn-success btn-xs'>Verified</button></p>
                            <p id="txtUnVerified" class="order-detail-para" runat="server" visible="false"><button type='button' class='btn btn-success btn-xs'>Un-Verified</button></p>

                            <br />

                            <p><strong>Order Type: </strong> <span id="txtOrderType" runat="server">Scheduled 12-Oct-2018</span></p>
                            <p><strong>Address: </strong> <span id="txtLocation" runat="server">D.H.A</span> </p>
                            <p class="order-detail-para"> <span id="txtAddress" runat="server">A-32 Street 21, DHA Phase 5, Karachi</span> </p>
                            <p><strong>Amount: </strong> <span id="txtAmount" runat="server">2,300 PKR</span> </p>
                            <p><strong>Quantity: </strong> <span id="txtQuantity" runat="server">2,300 PKR</span> </p>
                            <p><strong>Total Amount: </strong> <span id="txtTotalAmount" runat="server">2,300 PKR</span> </p>
                            <p class="order-detail-para">
                               <button type='button' class='btn btn-success btn-xs order-detail-button'> <span id="txtStatus" runat="server">New Order</span> </button>
                            </p>
                            
                            <p class="order-detail-para">

                               <%-- <select class="form-control" style="width:50%;">
                                    <option value="0">In-Progress</option>
                                    <option value="0">In-Progress</option>
                                    <option value="0">In-Progress</option>
                                </select>--%>

                                <asp:DropDownList ID="ddlStatus" style="width:50%;" CssClass="form-control" runat="server" DataTextField="OrderStatus" DataValueField="OrderStatusId" ClientIDMode="Static">
                                </asp:DropDownList>

                            </p>
                            
                            <p>
                                <strong>Remarks: </strong>
                                <textarea rows="5" style="width: 44%; margin-top: -15px;" class="form-control order-detail-para" id="txtRemarks" runat="server" ClientIDMode="Static"></textarea>
                            </p>

                            
                            <div class="form-group">
                                <div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-4">
                                    <button type="button" class="btn btn-success" id="btnSave" data-loading-text="<i class='fa fa-spinner fa-spin '></i> Process...">Save</button>
                                    <a class="btn btn-danger" href="orders.aspx">Back</a>
                                </div>
                            </div>
                           

                        </div>
                      </form>
                      </div>
                    
                    </section>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFooter" runat="Server">

<script>

    $(document).ready(function () {

        $('#btnSave').on('click', function (e) {

            var order_id = $("#txtOrderId").val();
            var status_id = $("#ddlStatus").val();
            var remarks = $("#txtRemarks").val();

            $("#btnSave").button('loading');

            $.ajax({
                type: "POST",
                url: "RequestHandler.ashx?action=status_update",
                data: {
                    'order_id'  : order_id,
                    'status_id' : status_id,
                    'remarks'   : remarks
                }
            }).done(function (data) {

                $("#btnSave").button('reset');
                //alert(data);
                console.log(data);

                if (data == "000") {
                    $('#notify_success_insert').show();
                } else {
                    $('#notify_error_insert').show();
                }

                setTimeout(function () {
                    window.location.href = "orders.aspx";
                }, 5000);

            });
        });

    });

</script>

</asp:Content>
