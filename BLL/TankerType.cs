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
    public class TankerType
    {

        public long TankerTypeId { get; set; }
        public string TankerTypeName { get; set; }

        public DataTable SelectAll(long Id)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllTankerType", param);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {
                Logs.WriteError("TankerType.cs", "SelectAll(" + Id + ")", "General", ae.Message);
            }

            return dt;
        }

        public DataTable SelectAllArchive()
        {
            DataTable dt = new DataTable();

            try
            {
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllTankerTypeArchive", null);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {
                Logs.WriteError("TankerType.cs", "SelectAllArchive()", "General", ae.Message);
            }

            return dt;
        }

        public void Delete(HttpContext context)
        {
            try
            {
                long Id = DataHelper.longParse(context.Request.Form["id"]);
                string Status = DataHelper.stringParse(context.Request.Form["status"]);

                ErrorResponse response = new ErrorResponse();
                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@Id", Value = Id },
                                           new SqlParameter() { ParameterName = "@Status", Value = Status },
                                       };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_DeleteTankerType", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write(dr["ErrorCode"].ToString());
            }
            catch (Exception ae)
            {
                Logs.WriteError("TankerType.cs", "Delete()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

        public bool Save(long Id, string TankerTypeName, bool IsActive, long LoginId, InsertMode Mode)
        {
            try
            {
                SqlParameter[] param = {
                                           new SqlParameter() { ParameterName="@Id", Value=Id },
                                           new SqlParameter() { ParameterName="@TankerTypeName", Value=TankerTypeName },
                                           new SqlParameter() { ParameterName="@IsActive", Value=IsActive },
                                           new SqlParameter() { ParameterName="@LoginId", Value=LoginId },
                                           new SqlParameter() { ParameterName="@Mode", Value=(int)Mode }
                                       };

                ErrorResponse response = new ErrorResponse();
                return DBService.ExecuteSP("WB_CreateTankerType", param, ref response);
            }
            catch (Exception ae)
            {
                Logs.WriteError("TankerType.cs", "Save(" + Id + ",'" + TankerTypeName + "'," + IsActive + "," + LoginId + ")", "General", ae.Message);
                return false;
            }
        }

        public DataTable GetItemForDropDown()
        {
            return DBService.FetchTable("SELECT 0 TankerTypeId, 'Select Tanker Type' AS TankerTypeName UNION ALL SELECT TankerTypeId, TankerTypeName FROM TankerType WHERE ISNULL(IsActive,0) = 1 AND Status = 'A'");
        }

        public void GetTankerType(HttpContext context)
        {
            try
            {
                DataTable dt = DBService.FetchTable("SELECT TankerTypeId, TankerTypeName FROM TankerType WHERE ISNULL(IsActive,0) = 1");
                List<TankerType> list = new List<TankerType>();

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new TankerType()
                    {
                        TankerTypeId = DataHelper.longParse(row["TankerTypeId"].ToString()),
                        TankerTypeName = DataHelper.stringParse(row["TankerTypeName"].ToString())
                    });
                }

                context.Response.Clear();
                context.Response.ContentType = "application/json; charset=utf-8";

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    data = list
                });

                context.Response.Write(json);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
