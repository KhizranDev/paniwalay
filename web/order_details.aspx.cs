using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using WebTech.Common;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class order_details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        long Id = 0;
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                Id = DataHelper.longParse(Encryption.Decrypt(Request.QueryString["id"].ToString(), Response));
            }
            else
            {
                Response.Redirect("Dashboard.aspx", true);
            }

            DisplayData(Id);
        }
    }

    private void DisplayData(long OrderId)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlParameter[] param = { 
                                        new SqlParameter() { ParameterName = "@StatusId", Value = 0 },
                                        new SqlParameter() { ParameterName = "@OrderId", Value = OrderId }
                                   };
            DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllOrders", param);
            DataRow row = dtbls[0].Rows[0];


            sb.AppendLine("   <div class='row'> ");
            sb.AppendLine("   	<div class='col-xs-6'> ");
            sb.AppendLine("   		<div style='text-align:left;'> ");
            sb.AppendLine("   			<img src='images/invoice-logo.png' alt='logo' style='text-align:left;'> ");
            sb.AppendLine("   		</div> ");
            sb.AppendLine("   	</div> ");
            sb.AppendLine("   	<div class='col-xs-6'> ");
            sb.AppendLine("   		<div style='text-align:right;'> ");
            sb.AppendLine("   			<h1>INVOICE</h1> ");
            sb.AppendLine("   			<h2>Order # " + "PW-" + DataHelper.longParse(row["OrderId"]).ToString("0000") + "</h2> ");
            sb.AppendLine("   			<h4>Order Date " + DataHelper.dateParse(row["OrderDate"]).ToString("dd-MMM-yyyy") + " " + DataHelper.dateParse(row["OrderTime"]).ToString("hh:mm tt") + "</h4> ");
            sb.AppendLine("   		</div> ");
            sb.AppendLine("   	</div> ");
            sb.AppendLine("   </div> ");


            sb.AppendLine(" <div class='row'> ");
            sb.AppendLine("	<div class='col-xs-12'> ");
            sb.AppendLine("		<hr>  ");
            sb.AppendLine("		<div class='row'> ");
            sb.AppendLine("			<div class='col-xs-6 col-md-3 col-lg-3 pull-left'> ");
            sb.AppendLine("				<div class='panel panel-default height'> ");
            sb.AppendLine("					<div class='panel-heading'>Customer Information</div> ");
            sb.AppendLine("					    <div class='panel-body'> ");
            sb.AppendLine("						    <strong>" + row["CustomerName"].ToString() + "</strong><br> ");
            sb.AppendLine("						    "+ row["EmailAddress"].ToString() +"<br> ");
            sb.AppendLine("						    " + row["ContactNo"].ToString() + "<br> ");
            sb.AppendLine("					    </div> ");
            sb.AppendLine("				    </div> ");
            sb.AppendLine("			</div> ");

            sb.AppendLine("			<div class='col-xs-6 col-md-3 col-lg-3 pull-right'> ");
            sb.AppendLine("				<div class='panel panel-default height'> ");
            sb.AppendLine("					<div class='panel-heading'>Delivered To</div> ");
            sb.AppendLine("					    <div class='panel-body'> ");
            sb.AppendLine("						    <strong>" + row["LocationName"].ToString() + "</strong><br> ");
            sb.AppendLine("						   " + row["Address"].ToString() + "<br> ");
            sb.AppendLine("						    " + row["NearByLocation"].ToString() + "<br> ");
            sb.AppendLine("						    VA<br> ");
            sb.AppendLine("					    </div> ");
            sb.AppendLine("				    </div> ");
            sb.AppendLine("			    </div> ");
            sb.AppendLine("		    </div> ");
            sb.AppendLine("	    </div> ");
            sb.AppendLine(" </div> ");


            sb.AppendLine("<div class='row'> ");
            sb.AppendLine("	<div class='col-md-12'> ");
            sb.AppendLine("		<div class='panel panel-default'> ");
            sb.AppendLine("			<div class='panel-heading'> ");
            sb.AppendLine("				<h3 class='text-center'><strong>Order summary</strong></h3> ");
            sb.AppendLine("			</div> ");
            sb.AppendLine("			<div class='panel-body'> ");
            sb.AppendLine("				<div class='table-responsive'> ");
            sb.AppendLine("					<table class='table table-condensed'> ");
            sb.AppendLine("						<thead> ");
            sb.AppendLine("							<tr> ");
            sb.AppendLine("								<td><strong>Tanker Type</strong></td> ");
            sb.AppendLine("								<td><strong>Location</strong></td> ");
            sb.AppendLine("								<td class='text-center'><strong>Amount</strong></td> ");
            sb.AppendLine("								<td class='text-center'><strong>Quantity</strong></td> ");
            sb.AppendLine("								<td class='text-center'><strong>Total Amount</strong></td> ");
            sb.AppendLine("							</tr> ");
            sb.AppendLine("						</thead> ");
            sb.AppendLine("						<tbody> ");
            sb.AppendLine("							<tr> ");
            sb.AppendLine("								<td>"+ row["TankerTypeName"].ToString() +"</td> ");
            sb.AppendLine("								<td>"+ row["LocationName"].ToString() +"</td> ");
            sb.AppendLine("								<td class='text-center'>" + DataHelper.decimalParse(row["Amount"]).ToString("#,#0") + " PKR" + "</td> ");
            sb.AppendLine("								<td class='text-center'>" + DataHelper.intParse(row["Quantity"]).ToString() + "</td> ");
            sb.AppendLine("								<td class='text-center'>" + DataHelper.decimalParse(row["TotalAmount"]).ToString("#,#0") + " PKR" + "</td> ");
            sb.AppendLine("							</tr> ");
            sb.AppendLine("							<tr> ");
            sb.AppendLine("								<td class='highrow'></td> ");
            sb.AppendLine("								<td class='highrow'></td> ");
            sb.AppendLine("								<td class='highrow'></td> ");
            sb.AppendLine("								<td class='highrow text-center'><strong>Grand Total</strong></td> ");
            sb.AppendLine("								<td class='highrow text-center'>" + DataHelper.decimalParse(row["TotalAmount"]).ToString("#,#0") + " PKR" + "</td> ");
            sb.AppendLine("							</tr> ");
            sb.AppendLine("						</tbody> ");
            sb.AppendLine("					</table> ");
            sb.AppendLine("				</div> ");
            sb.AppendLine("			</div> ");
            sb.AppendLine("		</div> ");


            divData.InnerHtml = sb.ToString();

        }
        catch (Exception ae)
        {
            Logs.WriteError("order_details.aspx.cs", "DisplayData(" + OrderId + ")", "General", ae.Message);
        }
    }
}