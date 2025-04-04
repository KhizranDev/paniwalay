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
    public class Faqs
    {
        public long FaqId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }

        public DataTable SelectAll(long Id)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllFaqs", param);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {

            }

            return dt;
        }

        public bool Save(long Id, long FaqCatId, string Title, string Description, bool IsActive, long LoginId, InsertMode Mode)
        {
            try
            {
                SqlParameter[] param = {
                                           new SqlParameter() { ParameterName="@Id", Value=Id },
                                           new SqlParameter() { ParameterName="@FaqCatId", Value=FaqCatId },
                                           new SqlParameter() { ParameterName="@Title", Value=Title },
                                           new SqlParameter() { ParameterName="@Description", Value=Description },
                                           new SqlParameter() { ParameterName="@IsActive", Value=IsActive },
                                           new SqlParameter() { ParameterName="@LoginId", Value=LoginId },
                                           new SqlParameter() { ParameterName="@Mode", Value=(int)Mode }
                                       };

                ErrorResponse response = new ErrorResponse();
                return DBService.ExecuteSP("WB_CreateFaqs", param, ref response);
            }
            catch (Exception ae)
            {
                Logs.WriteError("Faqs.cs", "Save(" + Id + ",'" + FaqCatId + "', " + Title + "," + Description + ")", "General", ae.Message);
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
                bool result = DBService.ExecuteSP("WB_DeleteFaqs", param, ref response);

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
                Logs.WriteError("Faqs.cs", "Delete()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

    }
}
