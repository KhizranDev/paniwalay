using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BLL;
using WebTech.Common;
using System.Web;

namespace BLL
{
    public class VendorsRate
    {
        public void GetRates(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            long VendorId = DataHelper.longParse(context.Request.Form["vendor_id"]);
            long TankerTypeId = DataHelper.longParse(context.Request.Form["tanker_type_id"]);

            try
            {

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@VendorId", Value = VendorId },
                                           new SqlParameter() { ParameterName = "@TankerTypeId", Value = TankerTypeId } 
                                       };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllRatesByVendor", param);
                dt = dtbls[0];

                sb.AppendLine("     <table class='table table-striped responsive'>");
                sb.AppendLine("         <thead>");
                sb.AppendLine("             <tr>");
                sb.AppendLine("                 <th style='width:20%;'></th>");
                sb.AppendLine("                 <th style='width:20%;'>Rate</th>");
                sb.AppendLine("                 <th style='width:25%;'>Effective Date</th>");
                sb.AppendLine("                 <th>Actions</th>");
                sb.AppendLine("             </tr>");
                sb.AppendLine("         </thead>");
                sb.AppendLine("     <tbody>");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine("	<tr>");
                        sb.AppendLine("	    <td>" + row["LocationName"].ToString() + "</td>");
                        sb.AppendLine("	    <td>" + DataHelper.decimalParse(row["CurrentRate"]) + "</td>");
                        sb.AppendLine("	    <td>" + DataHelper.dateParse(row["CurrentEffectiveDate"]).ToString("dd-MMM-yyyy") + "</td>");
                        sb.AppendLine("		<td>");
                        sb.AppendLine("			<a href='javascript:;' data-id='" + DataHelper.longParse(row["RateId"]) + "' data-locationid='" + DataHelper.longParse(row["LocationId"]) + "' class='btn btn-xs btn-primary btnChangeRate'><i class='glyphicon glyphicon-edit icon-white'></i> Change</a>");
                        sb.AppendLine("			<a href='javascript:;' data-id='" + DataHelper.longParse(row["RateId"]) + "' class='btn btn-xs btn-info btnHistoryRate'><i class='glyphicon glyphicon-edit icon-white'></i> History</a>");
                        sb.AppendLine("		</td>");
                        sb.AppendLine("	 </tr>");
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
                Logs.WriteError("VendorsRate.cs", "GetRates(" + VendorId + ", " + TankerTypeId + ")", "General", ae.Message);
            }

        }

        public void SaveRates(HttpContext context)
        {
            try
            {
                long VendorId = DataHelper.longParse(context.Request.Form["vendor_id"]);
                long RateId = DataHelper.longParse(context.Request.Form["rate_id"]);
                long LocationId = DataHelper.longParse(context.Request.Form["location_id"]);
                long TankerTypeId = DataHelper.longParse(context.Request.Form["tanker_type_id"]);
                DateTime Date = DataHelper.dateParse(context.Request.Form["date"]);
                decimal Rate = DataHelper.decimalParse(context.Request.Form["rate"]);

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@VendorId", Value = VendorId },
                                           new SqlParameter() { ParameterName = "@RateId", Value = RateId },
                                           new SqlParameter() { ParameterName = "@LocationId", Value = LocationId },
                                           new SqlParameter() { ParameterName = "@TankerTypeId", Value = TankerTypeId },
                                           new SqlParameter() { ParameterName = "@Date", Value = Date },
                                           new SqlParameter() { ParameterName = "@Rate", Value = Rate },
                                           new SqlParameter() { ParameterName = "@LoginId", Value = clsSession.LoginId }
                                       };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("WB_CreateRatesVendor", param, ref response);
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
                Logs.WriteError("VendorsRate.cs", "SaveRates()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

        public void GetRatesOrder(HttpContext context)
        {
            long VendorId = DataHelper.longParse(context.Request.Form["vendor_id"]);
            long TankerTypeId = DataHelper.longParse(context.Request.Form["tanker_type_id"]);
            long LocationId = DataHelper.longParse(context.Request.Form["location_id"]);

            try
            {

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@VendorId", Value = VendorId },
                                           new SqlParameter() { ParameterName = "@TankerTypeId", Value = TankerTypeId },
                                           new SqlParameter() { ParameterName = "@LocationId", Value = LocationId }
                                       };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetVendorRatesOrder", param);

                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write(dtbls[0].Rows[0]["VendorRate"].ToString());
            }
            catch (Exception ae)
            {
                Logs.WriteError("VendorsRate.cs", "GetRatesOrder(" + VendorId + ", " + TankerTypeId + ", " + LocationId + ")", "General", ae.Message);
            }

        }
    }
}
