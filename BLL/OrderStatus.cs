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
    public class OrderStatus
    {
        public DataTable SelectAllByOrder(long OrderId)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@OrderId", Value = OrderId } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetStatusByOrder", param);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {

            }

            return dt;
        }

        public void UpdateStatus(HttpContext context)
        {
            try
            {
                long OrderId = DataHelper.longParse(context.Request.Form["order_id"]);
                long AccountId = DataHelper.longParse(context.Request.Form["account_id"]);
                long StatusId = DataHelper.longParse(context.Request.Form["status_id"]);
                string Remarks = DataHelper.stringParse(context.Request.Form["remarks"]);

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@OrderId", Value = OrderId },
                                           new SqlParameter() { ParameterName = "@AccountId", Value = AccountId },
                                           new SqlParameter() { ParameterName = "@StatusId", Value = StatusId },
                                           new SqlParameter() { ParameterName = "@Remarks", Value = Remarks },
                                           new SqlParameter() { ParameterName = "@LoginId", Value = clsSession.LoginId }
                                       };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("WB_UpdateStatus", param, ref response);
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
                else if (dtbls[0].Rows[0]["ErrorCode"].ToString() == "002")
                {
                    context.Response.Clear();
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("002");
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
                Logs.WriteError("OrderStatus.cs", "UpdateStatus()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }
    }
}
