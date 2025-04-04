<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_details.aspx.cs" Inherits="order_details" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>::.Pani Walay.::</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <link rel="stylesheet" type="text/css" href="bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="font-awesome/css/font-awesome.min.css" />
    
</head>
<body>

<div class="container">

<div class="page-header">
   <div class="print-btn"> 
    	<a href="#" onclick="myFunction()" class="btn" style="width: 200px; text-transform: uppercase;font-size: 14px;font-weight: normal;	border-radius: 1px; background:rgba(146, 40, 123, 1); color: #fff;">Print</a>
    </div>
</div>

<!-- Simple Invoice - START -->
<div class="container">

    <div id="divData" runat="server">
        
        <div class="row">
        
            <div class="col-xs-6">
                <div style="text-align:left;">
                    <img src="images/invoice-logo.png" alt="logo" style="text-align:left;">
                </div>
            </div>

            <div class="col-xs-6">
                <div style="text-align:right;">
                    <h1>INVOICE</h1>
                    <h2>Order # 33221</h2>
                    <h4>Order Date: 10-Oct-2018</h4>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-xs-12">
            
                <hr>
                <div class="row">
                    <div class="col-xs-12 col-md-3 col-lg-3 pull-left">
                        <div class="panel panel-default height">
                            <div class="panel-heading">Customer Information</div>
                            <div class="panel-body">
                                <strong>David Peere:</strong><br>
                                1111 Army Navy Drive<br>
                                Arlington<br>
                                VA<br>
                                <strong>22 203</strong><br>
                            </div>
                        </div>
                    </div>
                
                    <div class="col-xs-12 col-md-3 col-lg-3 pull-right">
                        <div class="panel panel-default height">
                            <div class="panel-heading">Delivered To</div>
                            <div class="panel-body">
                                <strong>David Peere:</strong><br>
                                1111 Army Navy Drive<br>
                                Arlington<br>
                                VA<br>
                                <strong>22 203</strong><br>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="text-center"><strong>Order summary</strong></h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table class="table table-condensed">
                                <thead>
                                    <tr>
                                        <td><strong>Item Name</strong></td>
                                        <td class="text-center"><strong>Item Price</strong></td>
                                        <td class="text-center"><strong>Item Quantity</strong></td>
                                        <td class="text-right"><strong>Total</strong></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Samsung Galaxy S5</td>
                                        <td class="text-center">$900</td>
                                        <td class="text-center">1</td>
                                        <td class="text-right">$900</td>
                                    </tr>
                                    <tr>
                                        <td class="highrow"></td>
                                        <td class="highrow"></td>
                                        <td class="highrow text-center"><strong>Subtotal</strong></td>
                                        <td class="highrow text-right">$958.00</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</div>

<style>
    
.print-btn {
	text-align: center;
	margin: 30px 0;
}
		
.height {
    min-height: 200px;
}

.icon {
    font-size: 47px;
    color: #5CB85C;
}

.iconbig {
    font-size: 77px;
    color: #5CB85C;
}

.table > tbody > tr > .emptyrow {
    border-top: none;
}

.table > thead > tr > .emptyrow {
    border-bottom: none;
}

.table > tbody > tr > .highrow {
    border-top: 3px solid;
}
</style>

<!-- Simple Invoice - END -->

</div>

<script>
    function myFunction() {
        window.print();
    }
</script>

</body>
</html>
