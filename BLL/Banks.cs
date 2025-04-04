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
    public class Banks
    {
        public long BankId { get; set; }
        public string BankName { get; set; }
        
        public DataTable SelectAll(long Id)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllBanks", param);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {

            }

            return dt;
        }

        public bool Save(long Id, string BankName, string AccountTitle, string AccountNo, string BankAddress, bool IsActive, long LoginId, InsertMode Mode)
        {
            try
            {
                SqlParameter[] param = {
                                           new SqlParameter() { ParameterName="@Id", Value=Id },
                                           new SqlParameter() { ParameterName="@BankName", Value=BankName },
                                           new SqlParameter() { ParameterName="@AccountTitle", Value=AccountTitle },
                                           new SqlParameter() { ParameterName="@AccountNo", Value=AccountNo },
                                           new SqlParameter() { ParameterName="@BankAddress", Value=BankAddress },
                                           new SqlParameter() { ParameterName="@IsActive", Value=IsActive },
                                           new SqlParameter() { ParameterName="@LoginId", Value=LoginId },
                                           new SqlParameter() { ParameterName="@Mode", Value=(int)Mode }
                                       };

                ErrorResponse response = new ErrorResponse();
                return DBService.ExecuteSP("WB_CreateBank", param, ref response);
            }
            catch (Exception ae)
            {
                Logs.WriteError("Banks.cs", "Save(" + Id + ",'" + BankName + "', '" + AccountTitle + "', '" + AccountNo + "', " + IsActive + "," + LoginId + ")", "General", ae.Message);
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
                bool result = DBService.ExecuteSP("WB_DeleteBank", param, ref response);

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
                Logs.WriteError("Banks.cs", "Delete()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

        public DataTable GetItemForDropDown()
        {
            return DBService.FetchTable("SELECT 0 BankId, 'Select Bank' AS BankName UNION ALL SELECT BankId, BankName FROM Banks WHERE ISNULL(IsActive,0) = 1");
        }

        public void GetBank(HttpContext context)
        {
            try
            {
                DataTable dt = DBService.FetchTable("SELECT BankId, BankName FROM Banks WHERE ISNULL(IsActive,0) = 1");
                List<Banks> list = new List<Banks>();

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Banks()
                    {
                        BankId = DataHelper.longParse(row["BankId"].ToString()),
                        BankName = DataHelper.stringParse(row["BankName"].ToString())
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
