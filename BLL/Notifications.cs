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
    public class Notifications
    {

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ImageURL { get; set; }
        public bool IsRead { get; set; }

        public DataTable SelectAll(long Id)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllNotifications", param);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {

            }

            return dt;
        }

        public bool Save(long Id, string Title, string Description, DateTime StartDate, DateTime EndDate, string ImageURL, bool IsActive, long LoginId, InsertMode Mode)
        {
            try
            {
                SqlParameter[] param = {
                                           new SqlParameter() { ParameterName="@Id", Value=Id },
                                           new SqlParameter() { ParameterName="@Title", Value=Title },
                                           new SqlParameter() { ParameterName="@Description", Value=Description },
                                           new SqlParameter() { ParameterName="@StartDate", Value=StartDate },
                                           new SqlParameter() { ParameterName="@EndDate", Value=EndDate },
                                           new SqlParameter() { ParameterName="@ImageURL", Value=ImageURL },
                                           new SqlParameter() { ParameterName="@IsActive", Value=IsActive },
                                           new SqlParameter() { ParameterName="@LoginId", Value=LoginId },
                                           new SqlParameter() { ParameterName="@Mode", Value=(int)Mode }
                                       };

                ErrorResponse response = new ErrorResponse();
                return DBService.ExecuteSP("WB_CreateNotification", param, ref response);
            }
            catch (Exception ae)
            {
                Logs.WriteError("Notifications.cs", "Save(" + Id + ",'" + Title + "', '" + Description + "', '" + StartDate + "', " + IsActive + "," + LoginId + ")", "General", ae.Message);
                return false;
            }
        }

        public void Delete(HttpContext context)
        {
            try
            {
                long Id = DataHelper.longParse(context.Request.Form["id"]);

                ErrorResponse response = new ErrorResponse();
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
                bool result = DBService.ExecuteSP("WB_DeleteNotifications", param, ref response);

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
                Logs.WriteError("Notifications.cs", "Delete()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }
    }
}
