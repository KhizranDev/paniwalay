using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using WebTech.Common;
using System.Web;

namespace BLL
{
    public class Authentication
    {
        public long LoginId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        

        public bool Login(string userid, string password, ref string error_msg)
        {
            bool returnValue = false;

            try
            {
                SqlParameter[] param = {
                                           new SqlParameter() { ParameterName="@UserId", Value=userid },
                                           new SqlParameter() { ParameterName="@Password", Value=WTEncryption.getMD5Password(userid, password) }
                                       };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("WB_ValidateLogin", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    LoginId = DataHelper.longParse(dtbls[0].Rows[0]["LoginId"]);
                    UserId = userid;
                    UserName = DataHelper.stringParse(dtbls[0].Rows[0]["UserName"]);
                    
                    returnValue = true;
                }
                
            }
            catch (Exception ae)
            {
               Logs.WriteError("Authentication.cs", "Login(" + userid + ")", "General", ae.Message);
            }
            
            return returnValue;
        }
    }
}
