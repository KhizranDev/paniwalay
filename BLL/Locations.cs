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
    public class Locations
    {
        public long LocationId { get; set; }
        public string LocationName { get; set; }

        public DataTable SelectAll(long Id)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllLocations", param);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {

            }

            return dt;
        }

        public DataTable SelectAllArchive()
        {
            DataTable dt = new DataTable();

            try
            {
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllLocationsArchive", null);
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
                long Id = DataHelper.longParse(context.Request.Form["id"]);
                string Status = DataHelper.stringParse(context.Request.Form["status"]);

                ErrorResponse response = new ErrorResponse();
                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@Id", Value = Id },
                                           new SqlParameter() { ParameterName = "@Status", Value = Status },
                                       };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_DeleteLocations", param, ref response);
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
                Logs.WriteError("Locations.cs", "Delete()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

        public bool Save(long Id, string LocationName, bool IsActive, long LoginId, InsertMode Mode)
        {
            try
            {
                SqlParameter[] param = {
                                           new SqlParameter() { ParameterName="@Id", Value=Id },
                                           new SqlParameter() { ParameterName="@LocationName", Value=LocationName },
                                           new SqlParameter() { ParameterName="@IsActive", Value=IsActive },
                                           new SqlParameter() { ParameterName="@LoginId", Value=LoginId },
                                           new SqlParameter() { ParameterName="@Mode", Value=(int)Mode }
                                       };

                ErrorResponse response = new ErrorResponse();
                return DBService.ExecuteSP("WB_CreateLocation", param, ref response);
            }
            catch (Exception ae)
            {
                Logs.WriteError("Locations.cs", "Save(" + Id + ",'" + LocationName + "'," + IsActive + "," + LoginId + ")", "General", ae.Message);
                return false;
            }
        }

        public DataTable GetItemForDropDown()
        {
            return DBService.FetchTable("SELECT 0 LocationId, 'Select Location' AS LocationName UNION ALL SELECT LocationId, LocationName FROM Location WHERE ISNULL(IsActive,0) = 1 AND Status = 'A'");
        }

        public void GetLocation(HttpContext context)
        {
            try
            {
                DataTable dt = DBService.FetchTable("SELECT LocationId, LocationName FROM Location WHERE ISNULL(IsActive,0) = 1");
                List<Locations> list = new List<Locations>();

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Locations()
                    {
                        LocationId = DataHelper.longParse(row["LocationId"].ToString()),
                        LocationName = DataHelper.stringParse(row["LocationName"].ToString())
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
