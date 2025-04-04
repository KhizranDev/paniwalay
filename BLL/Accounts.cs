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
    public class Accounts
    {
        public DataTable SelectAll(long Id)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllAccounts", param);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {

            }

            return dt;
        }

        public void VerifyAccount(HttpContext context)
        {
            try
            {
                long Id = DataHelper.longParse(context.Request.Form["account_id"]);
                bool IsVerified = DataHelper.boolParse(context.Request.Form["is_verified"]);

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@Id", Value = Id },
                                           new SqlParameter() { ParameterName = "@IsVerified", Value = IsVerified }
                                       };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("WB_VerifyAccount", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("000");
            }
            catch (Exception ae)
            {
                Logs.WriteError("Accounts.cs", "VerifyAccount()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

        public void ResetPassword(HttpContext context)
        {
            try
            {
                string UserId = DataHelper.stringParse(context.Request.Form["user_id"]);
                long AccountId = DataHelper.longParse(context.Request.Form["account_id"]);
                string NewPassword = DataHelper.stringParse(context.Request.Form["new_password"]);
                string Token = DataHelper.stringParse(context.Request.Form["token"]);

                NewPassword = WTEncryption.getMD5Password(UserId, NewPassword);

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@AccountId", Value = AccountId },
                                           new SqlParameter() { ParameterName = "@RequestType", Value = 1 },
                                           new SqlParameter() { ParameterName = "@NewPassword", Value = NewPassword },
                                           new SqlParameter() { ParameterName = "@Token", Value = Token },
                                       };
                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_ResetPassword", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
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
                Logs.WriteError("Accounts.cs", "ResetPassword()", "General", ae.Message);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write("999");
            }
        }

    }

    public class AccountsAddress
    {
        public long AddressId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string NearByLocation { get; set; }
    }
}
