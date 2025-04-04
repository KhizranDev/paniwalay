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
    public class Rate
    {
        public long LocationId { get; set; }
        public string LocationName { get; set; }
        public decimal CurrentRate { get; set; }

        public DataTable SelectAll(long Id)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllRates", param);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {

            }

            return dt;
        }

        public void Delete(HttpContext context)
        {
            try
            {
                long Id = DataHelper.longParse(context.Request.Form["rate_id"]);

                ErrorResponse response = new ErrorResponse();
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
                bool result = DBService.ExecuteSP("WB_DeleteRates", param, ref response);

                if (result)
                {
                    context.Response.Clear();
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("000");
                }
                else
                {
                    context.Response.Clear();
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("001");
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Rate.cs", "Delete()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

        public void GetRates(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            long TankerTypeId = DataHelper.longParse(context.Request.Form["tanker_type_id"]);

            try
            {

                SqlParameter[] param = { new SqlParameter() { ParameterName = "@TankerTypeId", Value = TankerTypeId } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllRatesByTanker", param);
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
			            sb.AppendLine("	    <td>"+ row["LocationName"].ToString() +"</td>");
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
                Logs.WriteError("Rate.cs", "GetRates(" + TankerTypeId + ")", "General", ae.Message);
            }

        }

        public void SaveRates(HttpContext context)
        {
            try
            {
                long RateId = DataHelper.longParse(context.Request.Form["rate_id"]);
                long LocationId = DataHelper.longParse(context.Request.Form["location_id"]);
                long TankerTypeId = DataHelper.longParse(context.Request.Form["tanker_type_id"]);
                DateTime Date = DataHelper.dateParse(context.Request.Form["date"]);
                decimal Rate = DataHelper.decimalParse(context.Request.Form["rate"]);

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@RateId", Value = RateId },
                                           new SqlParameter() { ParameterName = "@LocationId", Value = LocationId },
                                           new SqlParameter() { ParameterName = "@TankerTypeId", Value = TankerTypeId },
                                           new SqlParameter() { ParameterName = "@Date", Value = Date },
                                           new SqlParameter() { ParameterName = "@Rate", Value = Rate },
                                           new SqlParameter() { ParameterName = "@LoginId", Value = clsSession.LoginId }
                                       };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("WB_CreateRates", param, ref response);
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
                Logs.WriteError("Rate.cs", "SaveRates()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

        public void GetRatesHistory(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            long RateId = DataHelper.longParse(context.Request.Form["rate_id"]);

            try
            {

                SqlParameter[] param = { new SqlParameter() { ParameterName = "@RateId", Value = RateId } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllRatesHistory", param);
                dt = dtbls[0];

                sb.AppendLine(" <table id='tblHistory' class='table table-striped table-bordered'>");
                sb.AppendLine("     <thead>");
                sb.AppendLine("         <tr>");
                sb.AppendLine("             <th>Rate</th>");
                sb.AppendLine("             <th>Effective Date</th>");
                sb.AppendLine("         </tr>");
                sb.AppendLine("     </thead>");
                sb.AppendLine("     <tbody>");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine("	<tr>");
                        sb.AppendLine("	    <td>" + DataHelper.decimalParse(row["Rate"]) + "</td>");
                        sb.AppendLine("	    <td>" + DataHelper.dateParse(row["EffectiveDate"]).ToString("dd-MMM-yyyy") + "</td>");
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
                Logs.WriteError("Rate.cs", "GetRatesHistory(" + RateId + ")", "General", ae.Message);
            }

        }

        public void SaveRatesOld(HttpContext context)
        {
            try
            {
                long RateId = DataHelper.longParse(context.Request.Form["rate_id"]);
                long LocationId = DataHelper.longParse(context.Request.Form["location_id"]);
                DateTime StartDate = DataHelper.dateParse(context.Request.Form["start_date"]);
                DateTime EndDate = DataHelper.dateParse(context.Request.Form["end_date"]);
                decimal Rates = DataHelper.decimalParse(context.Request.Form["rate"]);

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@RateId", Value = RateId },
                                           new SqlParameter() { ParameterName = "@LocationId", Value = LocationId },
                                           new SqlParameter() { ParameterName = "@StartDate", Value = StartDate },
                                           new SqlParameter() { ParameterName = "@EndDate", Value = EndDate },
                                           new SqlParameter() { ParameterName = "@Rates", Value = Rates },
                                           new SqlParameter() { ParameterName = "@LoginId", Value = clsSession.LoginId }
                                       };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("WB_CreateRates", param, ref response);
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
                    context.Response.Write("001");
                }


            }
            catch (Exception ae)
            {
                Logs.WriteError("Rate.cs", "SaveRates()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }
    }
}
