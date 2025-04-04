using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using WebTech.Common;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class Order
    {

        public long OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string OrderTime { get; set; }
        public long AccountId { get; set; }
        public string OrderType { get; set; }
        public string ScheduleDate { get; set; }
        public string PromoCode { get; set; }
        public string TankerTypeName { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string GoogleAddress { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatus { get; set; }
        public string PaidStatus { get; set; }
        public string Remarks { get; set; }
        public string Vendor { get; set; }
        public string NearByLocation { get; set; }
        public int OrderRating { get; set; }
        public string OrderFeedback { get; set; }
        public string ContactPerson { get; set; }
        public string DeliveryDate { get; set; }
        public string PreferredTime { get; set; }

        public DataTable OrderDetails(long StatusId, long OrderId)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@StatusId", Value = StatusId },
                                           new SqlParameter() { ParameterName = "@OrderId", Value = OrderId }
                                       };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllOrders", param);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {

            }

            return dt;
        }

        public void GetOrders(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            long StatusId = DataHelper.longParse(context.Request.Form["status_id"]);

            try
            {

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@StatusId", Value = StatusId },
                                           new SqlParameter() { ParameterName = "@OrderId", Value = 0 }
                                       };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllOrders", param);
                dt = dtbls[0];

                sb.AppendLine(" <table id='test-table' class='table table-striped table-bordered'>");
                sb.AppendLine("     <thead>");
                sb.AppendLine("         <tr>");
                sb.AppendLine("             <th style='display:none;'>Id</th>");
                sb.AppendLine("             <th>Order No.</th>");
                sb.AppendLine("             <th>Order Date</th>");
                sb.AppendLine("             <th>Customer Name</th>");
                sb.AppendLine("             <th>Tanker Type</th>");
                sb.AppendLine("             <th>Location</th>");
                sb.AppendLine("             <th>Vendor</th>");
                sb.AppendLine("             <th>Status</th>");
                sb.AppendLine("             <th>Action</th>");
                sb.AppendLine("         </tr>");
                sb.AppendLine("     </thead>");
                sb.AppendLine("     <tbody>");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        long OrderStatusId = DataHelper.longParse(row["OrderStatusId"]);
                        string OrderStatusColor = "";
                        string ButtonDisabled = "";
                        string ButtonClicked = "btnChangeStatus";
                        string ButtonClickedCancel = "btnCancel";

                        switch (OrderStatusId)
                        {
                            case 1:
                                OrderStatusColor = "primary";
                                break;

                            case 2:
                                OrderStatusColor = "warning";
                                break;

                            case 3:
                                OrderStatusColor = "success";
                                break;

                            case 4:
                                OrderStatusColor = "success";
                                ButtonDisabled = "disabled";
                                ButtonClicked = "";
                                ButtonClickedCancel = "";
                                break;

                            case 5:
                                OrderStatusColor = "info";
                                ButtonDisabled = "disabled";
                                ButtonClicked = "";
                                ButtonClickedCancel = "";
                                break;

                            case 6:
                                OrderStatusColor = "danger";
                                ButtonDisabled = "disabled";
                                ButtonClicked = "";
                                ButtonClickedCancel = "";
                                break;

                            default:
                                break;
                        }

                        //if (OrderStatusId == 1 || OrderStatusId == 2)
                        //{
                        //    OrderStatusColor = "primary";
                        //}
                        //else if (OrderStatusId == 3 || OrderStatusId == 4)
                        //{
                        //    OrderStatusColor = "success";
                        //}
                        //else if (OrderStatusId == 5)
                        //{
                        //    OrderStatusColor = "info";
                        //    ButtonDisabled = "disabled";
                        //    ButtonClicked = "";
                        //    ButtonClickedCancel = "";
                        //}
                        //else if (OrderStatusId == 6)
                        //{
                        //    OrderStatusColor = "danger";
                        //    ButtonDisabled = "disabled";
                        //    ButtonClicked = "";
                        //    ButtonClickedCancel = "";
                        //}

                        //if (OrderStatusId == 4)
                        //{
                        //    ButtonDisabled = "disabled";
                        //    ButtonClicked = "";
                        //    ButtonClickedCancel = "";
                        //}

                        sb.AppendLine("         <tr>");
                        sb.AppendLine("             <td style='display:none;'>" + DataHelper.longParse(row["OrderId"]).ToString() + "</td>");
                        sb.AppendLine("             <td>" + "PW-" + DataHelper.longParse(row["OrderId"]).ToString("0000") + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.dateParse(row["OrderDate"]).ToString("dd-MMM-yyyy") + " " + DataHelper.dateParse(row["OrderTime"]).ToString("hh:mm tt") + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["CustomerName"]) + " " + (DataHelper.boolParse(row["IsVerified"]) ? "<i class='glyphicon glyphicon-ok green'></i>" : "<i class='glyphicon glyphicon-remove red'></i>") + " </td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["TankerTypeName"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["LocationName"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["VendorName"]) + "</td>");
                        sb.AppendLine("             <td><a href='javascript:;' " + ButtonDisabled + " data-tankertypeid='" + DataHelper.longParse(row["TankerTypeId"]) + "' data-locationid='" + DataHelper.longParse(row["LocationId"]) + "' data-quantity='" + DataHelper.intParse(row["Quantity"]) + "' data-isvendororder='" + DataHelper.intParse(row["IsVendorOrder"]) + "' data-nextstatus='" + DataHelper.longParse(row["NextOrderStatusId"]) + "' data-nextstatusname='" + DataHelper.stringParse(row["NextOrderStatus"]) + "' data-id='" + DataHelper.longParse(row["OrderId"]) + "' data-accountid='" + DataHelper.longParse(row["AccountId"]) + "' " +
                                                    "title='Click here to change status' class='btn btn-" + OrderStatusColor + " btn-xs button-status " + ButtonClicked + "'>" + DataHelper.stringParse(row["OrderStatus"]) + "</a></td>");

                        //sb.AppendLine("           <td><a href='order_details.aspx?id=" + Encryption.Encrypt(DataHelper.stringParse(row["OrderId"])) + "' title='Click here to see details'>View</a></td>");
                        sb.AppendLine("             <td style='width: 100px;'><a href='#' title='Click here to see details' data-id='" + DataHelper.longParse(row["OrderId"]) + "' class='btn btn-sm btn-primary btnView'><i class='glyphicon glyphicon-file icon-white'></i></a> <a href='javascript:;' title='Cancel Order' data-id='" + DataHelper.longParse(row["OrderId"]) + "' class='btn btn-sm btn-danger "+ ButtonClickedCancel +"' " + ButtonDisabled + "><i class='glyphicon glyphicon-remove icon-white'></i></a></td>");
                        sb.AppendLine("         </tr>");
                    }
                }

                sb.AppendLine("     </tbody>");
                sb.AppendLine(" </table>");

                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
            }
            catch (Exception ae)
            {
                Logs.WriteError("Order.cs", "GetOrders(" + StatusId + ")", "General", ae.Message);
            }

        }

        public void GetOrdersDetails(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            long OrderId = DataHelper.longParse(context.Request.Form["order_id"]);

            try
            {

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@StatusId", Value = 0 },
                                           new SqlParameter() { ParameterName = "@OrderId", Value = OrderId }
                                       };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllOrders", param);
                dt = dtbls[0];
                DataRow row = dt.Rows[0];

                string OrderType = DataHelper.intParse(row["OrderTypeId"]) == 1 ? "On Time Delivery" : "Scheduled Delivery";

                sb.AppendLine("     <table class='table table-striped table-bordered'> ");
                sb.AppendLine("        <thead> ");
                sb.AppendLine("             <tr> ");
                sb.AppendLine("                 <th>Tanker Type</th> ");
                sb.AppendLine("                 <th>Amount</th> ");
                sb.AppendLine("                 <th>Qty</th> ");
                sb.AppendLine("                 <th>Total Amount</th> ");
                sb.AppendLine("             </tr> ");
                sb.AppendLine("         </thead> ");
                sb.AppendLine("             <tbody> ");

                sb.AppendLine("                 <tr> ");
                sb.AppendLine("                     <td style='padding: 2px;'>"+ DataHelper.stringParse(row["TankerTypeName"]) +" <br></td> ");
                sb.AppendLine("                     <td style='padding: 2px; text-align: right;'>" + DataHelper.decimalParse(row["Amount"]).ToString("#,#0") + " PKR" + "</td> ");
                sb.AppendLine("                     <td style='padding: 2px; text-align: right;'>" + DataHelper.intParse(row["Quantity"]).ToString() + "</td> ");
                sb.AppendLine("                     <td style='padding: 2px; text-align: right;'>" + DataHelper.decimalParse(row["TotalAmount"]).ToString("#,#0") + " PKR" + "</td> ");
                sb.AppendLine("                 </tr> ");
                            
                sb.AppendLine("                 <tr> ");
                sb.AppendLine("                     <td style='padding: 2px; text-align: right; font-weight: bold;' colspan='3'>Bill Amount</td> ");
                sb.AppendLine("                     <td style='padding: 2px; text-align: right; font-weight: bold;'>" + DataHelper.decimalParse(row["TotalAmount"]).ToString("#,#0") + " PKR" + "</td> ");
                sb.AppendLine("                 </tr> ");

                sb.AppendLine("             </tbody> ");
                sb.AppendLine("     </table> ");


                sb.AppendLine("     <table class='table table-striped table-bordered'> ");
                sb.AppendLine("         <thead> ");
                sb.AppendLine("             <tr> ");
                sb.AppendLine("                 <th>Customer Information</th> ");
                sb.AppendLine("                 <th>Delivery Information</th> ");
                sb.AppendLine("             </tr> ");
                sb.AppendLine("         </thead> ");
                sb.AppendLine("         <tbody> ");
                sb.AppendLine("             <tr> ");
                sb.AppendLine("                 <td> ");
                sb.AppendLine("                     <strong> Name: </strong>" + DataHelper.stringParse(row["CustomerName"]) + " ");
                sb.AppendLine("                     <br> ");
                sb.AppendLine("                     <strong> Email: </strong> " + DataHelper.stringParse(row["EmailAddress"]) + " ");
                sb.AppendLine("                     <br> ");
                sb.AppendLine("                     <strong> Contact No: </strong>" + DataHelper.stringParse(row["ContactNo"]) + " ");
                sb.AppendLine("                     <br> ");
                sb.AppendLine("                 </td> ");
                sb.AppendLine("                 <td> ");
                sb.AppendLine("                     <strong> Location: </strong>" + DataHelper.stringParse(row["LocationName"]) + " ");
                sb.AppendLine("                     <br> ");
                sb.AppendLine("                     <strong> Address: </strong>" + DataHelper.stringParse(row["Address"]) + " ");
                sb.AppendLine("                     <br> ");
                sb.AppendLine("                     <strong> Near By: </strong>" + DataHelper.stringParse(row["NearByLocation"]) + " ");
                sb.AppendLine("                     <br> ");
                sb.AppendLine("                     <strong> Contact Person: </strong>" + DataHelper.stringParse(row["ContactPerson"]) + " ");
                sb.AppendLine("                     <br> ");
                sb.AppendLine("                     <strong> Order Date: </strong>" + DataHelper.dateParse(row["OrderDate"]).ToString("dd-MMM-yyyy") + "");
                sb.AppendLine("                     <br> ");
                sb.AppendLine("                     <strong> Order Type: </strong>  " + OrderType + " ");
                sb.AppendLine("                     <br> ");
                sb.AppendLine("                     <strong> Delivery Date: </strong>" + DataHelper.dateParse(row["DeliveryDate"]).ToString("dd-MMM-yyyy") + "");
                sb.AppendLine("                 </td> ");
                sb.AppendLine("             </tr> ");
                sb.AppendLine("             <tr> ");
                sb.AppendLine("                 <td colspan='2'> ");
                sb.AppendLine("                     <strong>Payment Details : Cash On Delivery</strong> ");
                sb.AppendLine("                 </td> ");
                sb.AppendLine("             </tr> ");

                if (DataHelper.intParse(row["OrderStatusId"]) == 6)
                {
                    sb.AppendLine("             <tr> ");
                    sb.AppendLine("                 <td colspan='2'> ");
                    sb.AppendLine("                     <strong>Cancel Order Reason : " + DataHelper.stringParse(row["Remarks"]) + "</strong> ");
                    sb.AppendLine("                 </td> ");
                    sb.AppendLine("             </tr> ");
                }


                if (DataHelper.intParse(row["OrderRating"]) != 0)
                {
                    sb.AppendLine("             <tr> ");
                    sb.AppendLine("                 <td colspan='2'> ");
                    sb.AppendLine("                     <strong>Order Rating : </strong>" + DataHelper.stringParse(row["OrderRating"]) + " <br>");
                    sb.AppendLine("                     <strong>Order Feedback : </strong>" + DataHelper.stringParse(row["OrderFeedback"]) + " ");
                    sb.AppendLine("                 </td> ");
                    sb.AppendLine("             </tr> ");
                }
                
                sb.AppendLine("         </tbody> ");
                sb.AppendLine("     </table> ");


                context.Response.Clear();
                context.Response.ContentType = "application/json; charset=utf-8";

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    data = sb.ToString(),
                    TotalAmount = DataHelper.decimalParse(row["TotalAmount"]).ToString("#,#0"),
                });

                context.Response.Write(json);
            }
            catch (Exception ae)
            {
                Logs.WriteError("Order.cs", "GetOrdersDetails(" + OrderId + ")", "General", ae.Message);
            }

        }

        public void GetInvoices(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();

            DateTime DateFrom = DataHelper.dateParse(context.Request.Form["date_from"]);
            DateTime DateTo = DataHelper.dateParse(context.Request.Form["date_to"]);
            long TankerType = DataHelper.longParse(context.Request.Form["tanker_type"]);
            long Location = DataHelper.longParse(context.Request.Form["location"]);
            string Customer = DataHelper.stringParse(context.Request.Form["customer"]);
            
            decimal grand_total = 0;

            try
            {

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@DateFrom", Value = DateFrom },
                                           new SqlParameter() { ParameterName = "@DateTo", Value = DateTo },
                                           new SqlParameter() { ParameterName = "@TankerType", Value = TankerType },
                                           new SqlParameter() { ParameterName = "@Location", Value = Location },
                                           new SqlParameter() { ParameterName = "@Customer", Value = Customer },
                                       };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetInvoices", param);
                dt = dtbls[0];

                sb.AppendLine(" <table id='tblInvoices' class='table table-striped table-bordered'>");
                sb.AppendLine("     <thead>");
                sb.AppendLine("         <tr>");
                sb.AppendLine("             <th>Order No.</th>");
                sb.AppendLine("             <th>Order Date</th>");
                sb.AppendLine("             <th>Customer Name</th>");
                sb.AppendLine("             <th>Tanker Type</th>");
                sb.AppendLine("             <th>Location</th>");
                sb.AppendLine("             <th>Amount</th>");
                sb.AppendLine("             <th>Quantity</th>");
                sb.AppendLine("             <th>Total Amount</th>");
                sb.AppendLine("             <th>Remaining Amount</th>");
                sb.AppendLine("             <th>Action</th>");
                sb.AppendLine("         </tr>");
                sb.AppendLine("     </thead>");
                sb.AppendLine("     <tbody>");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        grand_total += DataHelper.decimalParse(row["RemainingAmount"]);

                        sb.AppendLine("         <tr>");
                        sb.AppendLine("             <td>" + "PW-" + DataHelper.longParse(row["OrderId"]).ToString("0000") + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.dateParse(row["OrderDate"]).ToString("dd-MMM-yyyy") + " " + DataHelper.dateParse(row["OrderTime"]).ToString("hh:mm tt") + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["CustomerName"]) + " " + (DataHelper.boolParse(row["IsVerified"]) ? "<i class='glyphicon glyphicon-ok green'></i>" : "<i class='glyphicon glyphicon-remove red'></i>") + " </td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["TankerTypeName"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["LocationName"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.decimalParse(row["Amount"]).ToString("#,#0") + " PKR" + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.intParse(row["Quantity"]).ToString() + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.decimalParse(row["TotalAmount"]).ToString("#,#0") + " PKR" + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.decimalParse(row["RemainingAmount"]).ToString("#,#0") + " PKR" + "</td>");
                        sb.AppendLine("             <td> ");
                        sb.AppendLine("                 <button type='button' id=" + DataHelper.longParse(row["OrderId"]) + " data-order='" + "PW-" + DataHelper.longParse(row["OrderId"]).ToString("0000") + "' " +
                                                        "data-orderdate='" + DataHelper.dateParse(row["OrderDate"]).ToString("dd-MMM-yyyy") + " " + DataHelper.dateParse(row["OrderTime"]).ToString("hh:mm tt") + "' " +
                                                        " data-customer='" + DataHelper.stringParse(row["CustomerName"]) + "' data-remainamount='" + DataHelper.decimalParse(row["RemainingAmount"]) + "' data-totalamount='" + DataHelper.decimalParse(row["TotalAmount"]).ToString("#,#0") + " PKR" + "'" +
                                                        "class='btn btn-success btn-xs btnPayment'>Payment</button> " +

                                                        "<a href='order_details.aspx?id=" + Encryption.Encrypt(DataHelper.stringParse(row["OrderId"])) + "' target='_blank' class='btn btn-primary btn-xs' title='Click here to see details'>View Invoice</a></td>");
                        sb.AppendLine("         </tr>");

                        //order_details.aspx?id=" + Encryption.Encrypt(DataHelper.stringParse(row["OrderId"])) + "
                    }
                }

                sb.AppendLine("     </tbody>");
                sb.AppendLine(" </table>");

                context.Response.Clear();
                context.Response.ContentType = "application/json; charset=utf-8";

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    data = sb.ToString(),
                    GrandTotal = DataHelper.decimalParse(grand_total).ToString("#,#0"),
                });

                context.Response.Write(json);
            }
            catch (Exception ae)
            {
                Logs.WriteError("Order.cs", "GetInvoices()", "General", ae.Message);
            }

        }

        public void SavePayment(HttpContext context)
        {
            try
            {
                long OrderId = DataHelper.longParse(context.Request.Form["order_id"]);
                int PaymentMethodId = DataHelper.intParse(context.Request.Form["method_id"]);
                int MemoId = DataHelper.intParse(context.Request.Form["memo_id"]);
                decimal PaidAmount = DataHelper.intParse(context.Request.Form["paid_amount"]);
                string Remarks = DataHelper.stringParse(context.Request.Form["remarks"]);

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@OrderId", Value = OrderId },
                                           new SqlParameter() { ParameterName = "@PaymentMethodId", Value = PaymentMethodId },
                                           new SqlParameter() { ParameterName = "@MemoId", Value = MemoId },
                                           new SqlParameter() { ParameterName = "@PaidAmount", Value = PaidAmount },
                                           new SqlParameter() { ParameterName = "@Remarks", Value = Remarks },
                                           new SqlParameter() { ParameterName = "@LoginId", Value = clsSession.LoginId }
                                       };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("WB_CreatePayment", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                if (dtbls[0].Rows[0]["ErrorCode"].ToString() == "000")
                {
                    context.Response.Clear();
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("000");
                }
                else
                {
                    context.Response.Clear();
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(dtbls[0].Rows[0]["ErrorCode"].ToString());
                }


            }
            catch (Exception ae)
            {
                Logs.WriteError("Order.cs", "SavePayment()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

        public void GetPayment(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();

            DateTime DateFrom = DataHelper.dateParse(context.Request.Form["date_from"]);
            DateTime DateTo = DataHelper.dateParse(context.Request.Form["date_to"]);
            long TankerType = DataHelper.longParse(context.Request.Form["tanker_type"]);
            long Location = DataHelper.longParse(context.Request.Form["location"]);
            string Customer = DataHelper.stringParse(context.Request.Form["customer"]);

            decimal grand_total = 0;

            try
            {

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@DateFrom", Value = DateFrom },
                                           new SqlParameter() { ParameterName = "@DateTo", Value = DateTo },
                                           new SqlParameter() { ParameterName = "@TankerType", Value = TankerType },
                                           new SqlParameter() { ParameterName = "@Location", Value = Location },
                                           new SqlParameter() { ParameterName = "@Customer", Value = Customer },
                                       };

                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetPayment", param);
                dt = dtbls[0];

                sb.AppendLine(" <table id='tblPayments' class='table table-striped table-bordered'>");
                sb.AppendLine("     <thead>");
                sb.AppendLine("         <tr>");
                sb.AppendLine("             <th>Order No.</th>");
                sb.AppendLine("             <th>Order Date</th>");
                sb.AppendLine("             <th>Customer Name</th>");
                sb.AppendLine("             <th>Tanker Type</th>");
                sb.AppendLine("             <th>Location</th>");
                sb.AppendLine("             <th>Amount</th>");
                sb.AppendLine("             <th>Quantity</th>");
                sb.AppendLine("             <th>Total Amount</th>");
                sb.AppendLine("             <th>Paid Amount</th>");
                sb.AppendLine("             <th>Payment Method</th>");
                sb.AppendLine("             <th>Payment From</th>");
                sb.AppendLine("             <th>Remarks</th>");
                sb.AppendLine("         </tr>");
                sb.AppendLine("     </thead>");
                sb.AppendLine("     <tbody>");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        grand_total += DataHelper.decimalParse(row["PaidAmount"]);

                        sb.AppendLine("         <tr>");
                        sb.AppendLine("             <td>" + "PW-" + DataHelper.longParse(row["OrderId"]).ToString("0000") + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.dateParse(row["OrderDate"]).ToString("dd-MMM-yyyy") + " " + DataHelper.dateParse(row["OrderTime"]).ToString("hh:mm tt") + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["CustomerName"]) + " " + (DataHelper.boolParse(row["IsVerified"]) ? "<i class='glyphicon glyphicon-ok green'></i>" : "<i class='glyphicon glyphicon-remove red'></i>") + " </td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["TankerTypeName"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["LocationName"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.decimalParse(row["Amount"]).ToString("#,#0") + " PKR" + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.intParse(row["Quantity"]).ToString() + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.decimalParse(row["TotalAmount"]).ToString("#,#0") + " PKR" + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.decimalParse(row["PaidAmount"]).ToString("#,#0") + " PKR" + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["PaymentMethodName"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["Memo"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["PaymentRemarks"]).ToString() + "</td>");
                        sb.AppendLine("         </tr>");
                    }
                }

                sb.AppendLine("     </tbody>");
                sb.AppendLine(" </table>");

                context.Response.Clear();
                context.Response.ContentType = "application/json; charset=utf-8";

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    data = sb.ToString(),
                    GrandTotal = DataHelper.decimalParse(grand_total).ToString("#,#0"),
                });

                context.Response.Write(json);
            }
            catch (Exception ae)
            {
                Logs.WriteError("Order.cs", "GetPayment()", "General", ae.Message);
            }

        }
    }
}
