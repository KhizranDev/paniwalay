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
    public class FaqsCategory
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }

        public DataTable SelectAll(long Id)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllFaqsCategory", param);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {

            }

            return dt;
        }

        public bool Save(long Id, string CategoryName, bool IsActive, long LoginId, InsertMode Mode)
        {
            try
            {
                SqlParameter[] param = {
                                           new SqlParameter() { ParameterName="@Id", Value=Id },
                                           new SqlParameter() { ParameterName="@CategoryName", Value=CategoryName },
                                           new SqlParameter() { ParameterName="@IsActive", Value=IsActive },
                                           new SqlParameter() { ParameterName="@LoginId", Value=LoginId },
                                           new SqlParameter() { ParameterName="@Mode", Value=(int)Mode }
                                       };

                ErrorResponse response = new ErrorResponse();
                return DBService.ExecuteSP("WB_CreateFaqsCategory", param, ref response);
            }
            catch (Exception ae)
            {
                Logs.WriteError("FaqsCategory.cs", "Save(" + Id + ",'" + CategoryName + "', " + IsActive + "," + LoginId + ")", "General", ae.Message);
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
                bool result = DBService.ExecuteSP("WB_DeleteFaqsCategory", param, ref response);

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
                Logs.WriteError("FaqsCategory.cs", "Delete()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

        public DataTable GetItemForDropDown()
        {
            return DBService.FetchTable("SELECT 0 CategoryId, 'Select Faq Category' AS CategoryName UNION ALL SELECT CategoryId, CategoryName FROM FaqsCategory WHERE ISNULL(IsActive,0) = 1");
        }
    }
}
