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
    public class VendorsOrder
    {
        public void GetOrders(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            long VendorId = DataHelper.longParse(context.Request.Form["vendor_id"]);

            try
            {

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@VendorOrderId", Value = 0 },
                                           new SqlParameter() { ParameterName = "@OrderId", Value = 0 },
                                           new SqlParameter() { ParameterName = "@VendorId", Value = VendorId }
                                       };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllVendorsOrders", param);
                dt = dtbls[0];

                sb.AppendLine(" <table id='data-table' class='table table-striped table-bordered'>");
                sb.AppendLine("     <thead>");
                sb.AppendLine("         <tr>");
                sb.AppendLine("             <th style='display:none;'>Id</th>");
                sb.AppendLine("             <th>Transaction No.</th>");
                sb.AppendLine("             <th>Transaction Date</th>");
                sb.AppendLine("             <th>Order No.</th>");
                sb.AppendLine("             <th>Tanker Type</th>");
                sb.AppendLine("             <th>Location</th>");
                sb.AppendLine("             <th>Vendor</th>");
                sb.AppendLine("             <th>Rate</th>");
                sb.AppendLine("             <th>Quantity</th>");
                sb.AppendLine("             <th>Amount</th>");
                sb.AppendLine("             <th>Action</th>");
                sb.AppendLine("         </tr>");
                sb.AppendLine("     </thead>");
                sb.AppendLine("     <tbody>");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine("         <tr>");
                        sb.AppendLine("             <td style='display:none;'>" + DataHelper.longParse(row["VendorOrderId"]).ToString() + "</td>");
                        sb.AppendLine("             <td>" + "V-" + DataHelper.longParse(row["VendorOrderId"]).ToString("0000") + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.dateParse(row["VendorOrderDate"]).ToString("dd-MMM-yyyy") + " " + DataHelper.dateParse(row["VendorOrderTime"]).ToString("hh:mm tt") + "</td>");
                        sb.AppendLine("             <td>" + "PW-" + DataHelper.longParse(row["OrderId"]).ToString("0000") + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["TankerTypeName"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["LocationName"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["VendorName"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.decimalParse(row["VendorRate"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.intParse(row["Quantity"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.decimalParse(row["VendorAmount"]) + "</td>");
                        sb.AppendLine("             <td style='width: 100px;'><a href='#' title='Click here to see details' data-id='" + DataHelper.longParse(row["OrderId"]) + "' class='btn btn-sm btn-primary btnView'><i class='glyphicon glyphicon-file icon-white'></i> Details</a></td>");
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
                Logs.WriteError("VendorsOrder.cs", "GetOrders(" + VendorId + ")", "General", ae.Message);
            }

        }

        public void SaveVendorOrder(HttpContext context)
        {
            try
            {
                long OrderId = DataHelper.longParse(context.Request.Form["order_id"]);
                long TankerTypeId = DataHelper.longParse(context.Request.Form["tanker_type_id"]);
                long LocationId = DataHelper.longParse(context.Request.Form["location_id"]);
                long VendorId = DataHelper.longParse(context.Request.Form["vendor_id"]);
                decimal VendorRate = DataHelper.decimalParse(context.Request.Form["vendor_rate"]);
                int Quantity = DataHelper.intParse(context.Request.Form["quantity"]);
                decimal VendorAmount = DataHelper.decimalParse(context.Request.Form["vendor_amount"]);

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@OrderId", Value = OrderId },
                                           new SqlParameter() { ParameterName = "@TankerTypeId", Value = TankerTypeId },
                                           new SqlParameter() { ParameterName = "@LocationId", Value = LocationId },
                                           new SqlParameter() { ParameterName = "@VendorId", Value = VendorId },
                                           new SqlParameter() { ParameterName = "@VendorRate", Value = VendorRate },
                                           new SqlParameter() { ParameterName = "@Quantity", Value = Quantity },
                                           new SqlParameter() { ParameterName = "@VendorAmount", Value = VendorAmount }
                                       };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("WB_CreateVendorOrder", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write(dtbls[0].Rows[0]["ErrorCode"].ToString());


            }
            catch (Exception ae)
            {
                Logs.WriteError("VendorsOrder.cs", "SaveVendorOrder()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

        public void GetVendorPayment(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();

            DateTime DateFrom = DataHelper.dateParse(context.Request.Form["date_from"]);
            DateTime DateTo = DataHelper.dateParse(context.Request.Form["date_to"]);
            string Vendor = DataHelper.stringParse(context.Request.Form["vendor"]);

            decimal grand_total = 0;

            try
            {

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@DateFrom", Value = DateFrom },
                                           new SqlParameter() { ParameterName = "@DateTo", Value = DateTo },
                                           new SqlParameter() { ParameterName = "@Vendor", Value = Vendor },
                                       };

                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetVendorsPayment", param);
                dt = dtbls[0];

                sb.AppendLine(" <table id='tblPayments' class='table table-striped table-bordered'>");
                sb.AppendLine("     <thead>");
                sb.AppendLine("         <tr>");
                //sb.AppendLine("             <th>Transaction No.</th>");
                //sb.AppendLine("             <th>Transaction Date</th>");
                //sb.AppendLine("             <th>Order No.</th>");
                sb.AppendLine("             <th>Vendor Name</th>");
                //sb.AppendLine("             <th>Tanker Type</th>");
                //sb.AppendLine("             <th>Location</th>");
                //sb.AppendLine("             <th>Amount</th>");
                //sb.AppendLine("             <th>Quantity</th>");
                //sb.AppendLine("             <th>Total Amount</th>");
                sb.AppendLine("             <th>Paid Amount</th>");
                sb.AppendLine("             <th>Payment Method</th>");
                sb.AppendLine("             <th>Payment From</th>");
                sb.AppendLine("             <th>Remarks</th>");
                sb.AppendLine("             <th>Payment Date</th>");
                sb.AppendLine("         </tr>");
                sb.AppendLine("     </thead>");
                sb.AppendLine("     <tbody>");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        grand_total += DataHelper.decimalParse(row["PaidAmount"]);

                        sb.AppendLine("         <tr>");
                        //sb.AppendLine("             <td>" + "V-" + DataHelper.longParse(row["VendorOrderId"]).ToString("0000") + "</td>");
                        //sb.AppendLine("             <td>" + DataHelper.dateParse(row["OrderDate"]).ToString("dd-MMM-yyyy") + " " + DataHelper.dateParse(row["OrderTime"]).ToString("hh:mm tt") + "</td>");
                        //sb.AppendLine("             <td>" + "PW-" + DataHelper.longParse(row["OrderId"]).ToString("0000") + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["VendorName"]) + " </td>");
                        //sb.AppendLine("             <td>" + DataHelper.stringParse(row["TankerTypeName"]) + "</td>");
                        //sb.AppendLine("             <td>" + DataHelper.stringParse(row["LocationName"]) + "</td>");
                        //sb.AppendLine("             <td>" + DataHelper.decimalParse(row["VendorRate"]).ToString("#,#0") + " PKR" + "</td>");
                        //sb.AppendLine("             <td>" + DataHelper.intParse(row["Quantity"]).ToString() + "</td>");
                        //sb.AppendLine("             <td>" + DataHelper.decimalParse(row["VendorAmount"]).ToString("#,#0") + " PKR" + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.decimalParse(row["PaidAmount"]).ToString("#,#0") + " PKR" + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["PaymentMethodName"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["Memo"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["PaymentRemarks"]).ToString() + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.dateParse(row["PaymentDate"]).ToString("dd-MMM-yyyy") + " " + DataHelper.dateParse(row["PaymentTime"]).ToString("hh:mm tt") + "</td>");
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
                Logs.WriteError("VendorsOrder.cs", "GetVendorPayment()", "General", ae.Message);
            }

        }

        public void SavePayment(HttpContext context)
        {
            try
            {
                long VendorId = DataHelper.longParse(context.Request.Form["vendor_id"]);
                int PaymentMethodId = DataHelper.intParse(context.Request.Form["method_id"]);
                int MemoId = DataHelper.intParse(context.Request.Form["memo_id"]);
                decimal PaidAmount = DataHelper.intParse(context.Request.Form["paid_amount"]);
                string Remarks = DataHelper.stringParse(context.Request.Form["remarks"]);

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@VendorId", Value = VendorId },
                                           new SqlParameter() { ParameterName = "@PaymentMethodId", Value = PaymentMethodId },
                                           new SqlParameter() { ParameterName = "@MemoId", Value = MemoId },
                                           new SqlParameter() { ParameterName = "@PaidAmount", Value = PaidAmount },
                                           new SqlParameter() { ParameterName = "@Remarks", Value = Remarks },
                                           new SqlParameter() { ParameterName = "@LoginId", Value = clsSession.LoginId }
                                       };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("WB_CreateVendorPayment", param, ref response);
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
                Logs.WriteError("VendorsOrder.cs", "SavePayment()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }
    }
}
