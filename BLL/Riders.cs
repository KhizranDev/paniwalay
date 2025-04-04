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
    public class Riders
    {
        public long RiderId { get; set; }
        public string RiderName { get; set; }

        public DataTable SelectAll(long Id)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllRiders", param);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {

            }

            return dt;
        }

        public bool Save(long Id, string RiderName, string ContactNo, string CNIC, string RiderAddress, bool IsActive, long LoginId, InsertMode Mode)
        {
            try
            {
                SqlParameter[] param = {
                                           new SqlParameter() { ParameterName="@Id", Value=Id },
                                           new SqlParameter() { ParameterName="@RiderName", Value=RiderName },
                                           new SqlParameter() { ParameterName="@ContactNo", Value=ContactNo },
                                           new SqlParameter() { ParameterName="@CNIC", Value=CNIC },
                                           new SqlParameter() { ParameterName="@RiderAddress", Value=RiderAddress },
                                           new SqlParameter() { ParameterName="@IsActive", Value=IsActive },
                                           new SqlParameter() { ParameterName="@LoginId", Value=LoginId },
                                           new SqlParameter() { ParameterName="@Mode", Value=(int)Mode }
                                       };

                ErrorResponse response = new ErrorResponse();
                return DBService.ExecuteSP("WB_CreateRider", param, ref response);
            }
            catch (Exception ae)
            {
                Logs.WriteError("Riders.cs", "Save(" + Id + ",'" + RiderName + "', '" + ContactNo + "', '" + CNIC + "', " + IsActive + "," + LoginId + ")", "General", ae.Message);
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
                bool result = DBService.ExecuteSP("WB_DeleteRider", param, ref response);

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
                Logs.WriteError("Riders.cs", "Delete()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

        public DataTable GetItemForDropDown()
        {
            return DBService.FetchTable("SELECT 0 RiderId, 'Select Rider' AS RiderName UNION ALL SELECT RiderId, RiderName FROM Riders WHERE ISNULL(IsActive,0) = 1");
        }

        public void GetRider(HttpContext context)
        {
            try
            {
                DataTable dt = DBService.FetchTable("SELECT RiderId, RiderName FROM Riders WHERE ISNULL(IsActive,0) = 1");
                List<Riders> list = new List<Riders>();

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Riders()
                    {
                        RiderId = DataHelper.longParse(row["RiderId"].ToString()),
                        RiderName = DataHelper.stringParse(row["RiderName"].ToString())
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
