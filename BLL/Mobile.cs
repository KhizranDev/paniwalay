using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using WebTech.Common;
using BLL;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data.SqlTypes;
using System.Xml;
using System.Net;
using System.Text.RegularExpressions;

namespace BLL
{
    public class Mobile
    {
        public HttpContext context { get; set; }
        SMS objSMS = new SMS();

        public void Proceed()
        {
            if (context.Request.QueryString.Count > 0)
            {
                switch (context.Request.QueryString[0].ToString())
                {
                    case "request_code":
                        RequestCode();
                    break;

                    case "verified_code":
                        VerifiedCode();
                    break;

                    case "upload_profile_pic":
                        UploadProfilePicture();
                    break;

                    case "sign_up":
                        SignUpAccount();
                    break;

                    case "update_profile":
                        UpdateProfile();
                    break;

                    case "sign_in":
                        SignInAccount();
                    break;

                    case "request_password_code":
                        RequestPasswordCode();
                    break;

                    case "reset_password":
                        ResetPassword();
                    break;

                    case "set_first_login":
                        SetFirstLogin();
                    break;

                    case "set_order_feedback":
                        SetOrderFeedback();
                    break;

                    case "set_order":
                        SetOrder();
                    break;

                    case "get_order_history":
                        GetOrdersHistory();
                    break;

                    case "get_tanker_types":
                        GetTankerTypes();
                    break;

                    case "get_location_rates":
                        GetLocationRates();
                    break;

                    case "save_accounts_address":
                        SaveAccountsAddress();
                    break;

                    case "update_accounts_address":
                        UpdateAccountsAddress();
                    break;

                    case "get_accounts_address":
                        GetAccountsAddress();
                    break;

                    case "delete_accounts_address":
                        DeleteAccountsAddress();
                    break;

                    case "get_faq_category":
                        GetFAQCategory();
                    break;

                    case "get_faqs":
                        GetFAQs();
                    break;

                    case "get_notifications":
                        GetNotifications();
                    break;

                    case "set_notification_status":
                        SetNotificationReadStatus();
                    break;

                    case "set_notification_enable_status":
                        SetNotificationEnableStatus();
                    break;

                    case "set_cancel_order":
                        SetCancelOrder();
                    break;

                    case "logout":
                        LogoutAccount();
                    break;

                    case "test_email":
                        TestEmail();
                    break;

                    case "delete_temp_customer":
                        DeleteTempCustomer();
                    break;

                    
                }
            }
        }

        /// <summary>Request Code
        /// Parameters 
        ///     * mobile_num                       (User Mobile Number)                [Required]
        /// </summary>
        private void RequestCode()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Number Already Registered
            // 003 Invalid Mobile Number
            // 999 Exception / unknown error

            try
            {

                if (context.Request.Form["mobile_num"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                if (context.Request.Form["mobile_num"].Length > 12)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Invalid Mobile Number" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                string mobile_num = context.Request.Form["mobile_num"].ToString();

                if (Regex.IsMatch(mobile_num, "^3"))
                {
                    mobile_num = "92" + mobile_num;
                }
                else if (Regex.IsMatch(mobile_num, "^03"))
                {
                    mobile_num = mobile_num.Substring(1, 10);
                    mobile_num = "92" + mobile_num;
                }

                SqlParameter[] param = { new SqlParameter("@MobileNo", mobile_num) };
                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_RequestCode", param, ref response);

                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    string MessageText = "Dear Customer use code " + DataHelper.stringParse(dr["Code"]) + " to signup into PaniWalay App";

                    objSMS.SaveSMS(mobile_num, MessageText);

                    var json = new
                    {
                        ErrorCode = "000",
                        ErrorDescription = "OK",
                        Code = DataHelper.stringParse(dr["Code"])
                    };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Number Already Registered" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "RequestCode()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Verified Code
        /// Parameters 
        ///     * mobile_num                        (Mobile No)                        [Required]
        ///     * code                              (Code)                             [Required]
        /// </summary>
        private void VerifiedCode()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Code Expired
            // 003 Mobile/Code Not Found
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["mobile_num"] == null || context.Request.Form["code"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                string mobile_num = context.Request.Form["mobile_num"].ToString();
                string code = context.Request.Form["code"].ToString();


                if (Regex.IsMatch(mobile_num, "^3"))
                {
                    mobile_num = "92" + mobile_num;
                }
                else if (Regex.IsMatch(mobile_num, "^03"))
                {
                    mobile_num = mobile_num.Substring(1, 10);
                    mobile_num = "92" + mobile_num;
                }

                SqlParameter[] param = { 
                                           new SqlParameter("@MobileNo", mobile_num),
                                           new SqlParameter("@Code", code)
                                       };
                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_VerifiedCode", param, ref response);

                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new
                    {
                        ErrorCode = "000",
                        ErrorDescription = "OK",
                        MobileNo = mobile_num
                    };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = dr["ErrorDescription"].ToString() };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "VerifiedCode()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Sign Up Account
        /// Parameters 
        ///     * user_id                       (User Id)                                       [Required]
        ///     * full_name                     (Full Name)                                     [Required]
        ///     * email_address                 (Email Address)                                 [Required]
        ///     * password                      (Password)                                      [Required]
        ///     * contact_no                    (Contact No.)                                   [Required]
        ///     * user_type_id                  (User Type Id 1=Normal/2=Facebook/3=Google)     [Optional]
        ///     * device_id                     (Device Id)                                     [Required]
        /// </summary>
        private void SignUpAccount()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Email Already exists 
            // 004 Contact No. Already exists 
            // 999 Exception / unknown error 

            try
            {
                string profile_picture = "";
                int user_type_id = 1;

                if (context.Request.Form["full_name"] == null || context.Request.Form["email_address"] == null || context.Request.Form["password"] == null ||
                    context.Request.Form["contact_no"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                if (context.Request.Form["contact_no"].Length > 12)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Invalid Mobile Number" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                string userid = "";
                string full_name = context.Request.Form["full_name"].ToString();
                string email_address = context.Request.Form["email_address"].ToString();
                string password = context.Request.Form["password"].ToString();
                string contact_no = context.Request.Form["contact_no"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                if (Regex.IsMatch(contact_no, "^3"))
                {
                    contact_no = "92" + contact_no;
                }
                else if (Regex.IsMatch(contact_no, "^03"))
                {
                    contact_no = contact_no.Substring(1, 10);
                    contact_no = "92" + contact_no;
                }

                if (context.Request.Form["user_type_id"] != null)
                    user_type_id = DataHelper.intParse(context.Request.Form["user_type_id"].ToString());


                if (context.Request.Form["profile_picture"] != null)
                    profile_picture = context.Request.Form["profile_picture"].ToString();


                password = WTEncryption.getMD5Password(email_address, password);

                SqlParameter[] param = {
                                       new SqlParameter("@UserId", userid),
                                       new SqlParameter("@FullName", full_name),
                                       new SqlParameter("@EmailAddress", email_address),
                                       new SqlParameter("@Password", password),
                                       new SqlParameter("@ContactNo", contact_no),
                                       new SqlParameter("@ProfilePicture", profile_picture),
                                       new SqlParameter("@UserTypeId", user_type_id),
                                       new SqlParameter("@DeviceId", device_id)
                                   };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_SignUpAccount", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    //SendSignUpEmail(DataHelper.longParse(dr["AccountId"]), dr["UserId"].ToString(), "");
                    //EmailTemplate.SignUpEmail(dr["UserId"].ToString(), email_address, dr["ActivationCode"].ToString());

                    var json = new
                    {
                        ErrorCode = "000",
                        ErrorDescription = "OK",
                        AccountId = DataHelper.longParse(dr["AccountId"]),
                        Token = dr["Token"].ToString(),
                        FullName = dr["FullName"].ToString(),
                        Email = dr["Email"].ToString(),
                        ContactNo = dr["ContactNo"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        DOB = DataHelper.dateParse(dr["DOB"]).ToString("yyyy-M-d"),
                        FirstLogin = DataHelper.boolParse(dr["FirstLogin"].ToString()),
                        IsNotificationEnable = DataHelper.boolParse(dr["IsNotificationEnable"].ToString()),
                        ProfilePictureUrl = DataHelper.getProfilePicture(dr["ProfilePictureUrl"].ToString())
                    };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = dr["ErrorDescription"].ToString() };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "SignUpAccount()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Upload Profile Picture
        /// Parameters 
        /// *   File            File                (Required)
        /// </summary>
        private void UploadProfilePicture()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 999 Exception / unknown error

            try
            {
                
                bool fileFound = false;
                if (context.Request.Files != null)
                    if (context.Request.Files.Count > 0)
                        fileFound = true;


                if (!fileFound)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "File Not Found" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                if (fileFound)
                {
                    int fileCount = context.Request.Files.Count;
                    if (context.Request.Files.Count > 0)
                    {
                        for (int i = 0; i < fileCount; i++)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(context.Request.Files[i].FileName) + DateTime.Now.Ticks;
                            string extension = Path.GetExtension(context.Request.Files[i].FileName);

                            string fullFileName = fileName + extension;
                            context.Request.Files[0].SaveAs(HttpContext.Current.Server.MapPath("~/content/users/" + fileName + extension));

                            var json = new
                            {
                                ErrorCode = "000",
                                ErrorDescription = "OK",
                                ProfilePicture = fullFileName,
                            };

                            JsonHelper.WriteJson(json, context);
                        }
                    }

                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "UploadProfilePicture()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Update Profile
        /// Parameters 
        ///     * email_address            (Email Address)                      [Required]
        ///     * contact_no               (Contact No.)                        [Required]
        ///     * profile_picture          (Profile Picture Name)               [Required]
        ///     * gender                   (Gender M=Male/F=Female)             [Required]
        ///     * dob                      (Date of Birth Format=yyyy-mm-dd)    [Required]
        ///     * old_password             (Old Password)                       [Optional]
        ///     * new_password             (New Password)                       [Optional]
        ///     * account_id               (Account Id)                         [Required]
        ///     * token                    (Token)                              [Required]
        ///     * device_id                (Device Id)                          [Required]
        /// </summary>
        private void UpdateProfile()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 Invalid Old Password
            // 999 Exception / unknown error 

            try
            {
                string old_password = "";
                string new_password = "";

                if (context.Request.Form["email_address"] == null || context.Request.Form["contact_no"] == null || context.Request.Form["gender"] == null ||
                    context.Request.Form["dob"] == null || context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                string email_address = context.Request.Form["email_address"].ToString();
                string contact_no = context.Request.Form["contact_no"].ToString();
                string profile_picture = context.Request.Form["profile_picture"].ToString();
                string gender = context.Request.Form["gender"].ToString();
                DateTime dob = DataHelper.dateParse(context.Request.Form["dob"].ToString());

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                SqlParameter[] param = { new SqlParameter() { ParameterName = "@EmailAddress", Value = email_address } };
                string userid = DBService.DLookupDB("SP_GetUserIdByEmail", param);

                if (context.Request.Form["old_password"] != null && context.Request.Form["old_password"] != "")
                {
                    old_password = context.Request.Form["old_password"].ToString();
                    old_password = WTEncryption.getMD5Password(userid, old_password);
                }

                if (context.Request.Form["new_password"] != null && context.Request.Form["old_password"] != "")
                {
                    new_password = context.Request.Form["new_password"].ToString();
                    new_password = WTEncryption.getMD5Password(userid, new_password);
                }
                    

                SqlParameter[] param2 = {
                                            new SqlParameter() { ParameterName = "@UserId", Value = userid },
                                            new SqlParameter() { ParameterName = "@EmailAddress", Value = email_address },
                                            new SqlParameter() { ParameterName = "@ContactNo", Value = contact_no },
                                            new SqlParameter() { ParameterName = "@ProfilePicture", Value = profile_picture },
                                            new SqlParameter() { ParameterName = "@Gender", Value = gender },
                                            new SqlParameter() { ParameterName = "@DOB", Value = dob },
                                            new SqlParameter() { ParameterName = "@OldPassword", Value =  old_password },
                                            new SqlParameter() { ParameterName = "@NewPassword", Value =  new_password },
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_UpdateProfile", param2, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];

                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new { ErrorCode = "000", ErrorDescription = "OK" };
                    JsonHelper.WriteJson(json, context);
                }
                else if (dr["ErrorCode"].ToString() == "003")
                {
                    var json = new { ErrorCode = "003", ErrorDescription = "Invalid Old Password" };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "UpdateProfile()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }
        
        /// <summary>Sign In Account
        /// Parameters 
        ///     * user_id                       (User Id)                    [Required]
        ///     * password                      (Password)                   [Required]
        ///     * device_id                     (Device Id)                  [Required]
        /// </summary>
        private void SignInAccount()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid User Id / Password
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["user_id"] == null || context.Request.Form["password"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                string userid = context.Request.Form["user_id"].ToString();
                string password = context.Request.Form["password"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                password = WTEncryption.getMD5Password(userid, password);

                SqlParameter[] param = {
                                       new SqlParameter("@UserId", userid),
                                       new SqlParameter("@Password", password),
                                       new SqlParameter("@DeviceId", device_id)
                                   };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_SignInAccount", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new
                    {
                        ErrorCode = "000",
                        ErrorDescription = "OK",
                        AccountId = DataHelper.longParse(dr["AccountId"]),
                        Token = dr["Token"].ToString(),
                        FullName = dr["FullName"].ToString(),
                        Email = dr["Email"].ToString(),
                        ContactNo = dr["ContactNo"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        DOB = DataHelper.dateParse(dr["DOB"]).ToString("yyyy-M-d"),
                        FirstLogin = DataHelper.boolParse(dr["FirstLogin"].ToString()),
                        IsNotificationEnable = DataHelper.boolParse(dr["IsNotificationEnable"].ToString()),
                        ProfilePictureUrl = DataHelper.getProfilePicture(dr["ProfilePictureUrl"].ToString())
                    };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Invalid User Id / Password" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "SignInAccount()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Request Password Code
        /// Parameters 
        ///     * email_address                       (User Email Address)                [Required]
        /// </summary>
        private void RequestPasswordCode()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 003 Invalid Email Address
            // 999 Exception / unknown error

            try
            {

                if (context.Request.Form["email_address"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                string email_address = context.Request.Form["email_address"].ToString();

                SqlParameter[] param = { new SqlParameter("@EmailAddress", email_address) };
                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_RequestPassCode", param, ref response);

                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    string Code = DataHelper.stringParse(dr["Code"]);
                    string FullName = DataHelper.stringParse(dr["FullName"]);

                    EmailTemplate.ResetPasswordEmail(FullName, email_address, Code);

                    var json = new
                    {
                        ErrorCode = "000",
                        ErrorDescription = "OK",
                        Code = Code
                    };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = dr["ErrorDescription"].ToString() };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "RequestPasswordCode()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Reset Password
        /// Parameters 
        ///     * email_address                 (Email Address)                               [Required]
        ///     * new_password                  (New Password)                                [Required]
        ///     * request_type                  (Request Type 1=change/2=forget)              [Required] 
        /// </summary>
        private void ResetPassword()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Email Address
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["email_address"] == null || context.Request.Form["code"] == null || context.Request.Form["new_password"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }
                
                string email_address = context.Request.Form["email_address"].ToString();
                string code = context.Request.Form["code"].ToString();
                string new_password = context.Request.Form["new_password"].ToString();

                new_password = WTEncryption.getMD5Password(email_address, new_password);

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@EmailAddress", Value = email_address },
                                            new SqlParameter() { ParameterName = "@Code", Value = code },
                                            new SqlParameter() { ParameterName = "@NewPassword", Value = new_password }
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_RequestPassword", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new { ErrorCode = "000", ErrorDescription = "OK" };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = dr["ErrorDescription"].ToString() };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "ResetPassword()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Set First Login Flag
        /// Parameters 
        ///     * account_id                (Account Id)          [Required]
        ///     * token                     (Token)               [Required]
        ///     * device_id                 (Device Id)           [Required]
        /// </summary>
        private void SetFirstLogin()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_SetFirstLogin", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new { ErrorCode = "000", ErrorDescription = "OK" };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "SetFirstLogin()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Order Feedback
        /// Parameters 
        ///     * order_id                 (Order Id)                    [Required]
        ///     * ranking                  (Ranking)                     [Required]
        ///     * feedback                 (Order Feedback)              [Required]
        ///     * account_id               (Account Id)                  [Required]
        ///     * token                    (Token)                       [Required]
        ///     * device_id                (Device Id)                   [Required]
        /// </summary>
        private void SetOrderFeedback()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["order_id"] == null || context.Request.Form["ranking"] == null || context.Request.Form["feedback"] == null ||
                    context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long order_id = DataHelper.longParse(context.Request.Form["order_id"].ToString());
                int ranking = DataHelper.intParse(context.Request.Form["ranking"].ToString());
                string feedback = context.Request.Form["feedback"].ToString();
                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@OrderId", Value = order_id },
                                            new SqlParameter() { ParameterName = "@Ranking", Value = ranking },
                                            new SqlParameter() { ParameterName = "@Feedback", Value = feedback },
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_SetOrderFeedback", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new { ErrorCode = "000", ErrorDescription = "OK" };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "SetOrderFeedback()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Order
        /// Parameters 
        ///     * account_id               (Account Id)                            [Required]
        ///     * order_type_id            (Order Type Id 1=Normal/2=Schedule)     [Required]
        ///     * schedule_date            (Schedule Date)                         [Optional]
        ///     * promo_code               (Promo Code)                            [Optional]
        ///     * location_id              (Location Id)                           [Required]
        ///     * address                  (Address)                               [Required]
        ///     * latitude                 (Latitude)                              [Required]
        ///     * longitude                (Longitude)                             [Required]
        ///     * google_address           (GoogleAddress)                         [Required]
        ///     * amount                   (Amount)                                [Required]
        ///     * quantity                 (Quantity)                              [Required]
        ///     * remarks                  (Remarks)                               [Optional]
        ///     * vendor                   (Vendor)                                [Optional]
        ///     * near_by_location         (Near By Location)                      [Optional]
        ///     * tanker_type_id           (Tanker Type Id)                        [Required]
        ///     * contact_person           (Contact Person)                        [Required]
        ///     * token                    (Token)                                 [Required]
        ///     * device_id                (Device Id)                             [Required]
        /// </summary>
        private void SetOrder()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 Schedule Date not supplied
            // 999 Exception / unknown error 

            try
            {

                string schedule_date = "";
                string promo_code = "";
                string remarks = "";
                string vendor = "";
                string near_by_location = "";
                string preferred_time = "";

                if (context.Request.Form["account_id"] == null || context.Request.Form["order_type_id"] == null || context.Request.Form["location_id"] == null ||
                    context.Request.Form["address"] == null || context.Request.Form["latitude"] == null || context.Request.Form["longitude"] == null
                    || context.Request.Form["google_address"] == null || context.Request.Form["amount"] == null || context.Request.Form["token"] == null
                    || context.Request.Form["device_id"] == null || context.Request.Form["tanker_type_id"] == null || context.Request.Form["quantity"] == null
                    || context.Request.Form["contact_person"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                int order_type_id = DataHelper.intParse(context.Request.Form["order_type_id"].ToString());
                long location_id = DataHelper.longParse(context.Request.Form["location_id"].ToString());
                string address = context.Request.Form["address"].ToString();
                float latitude = DataHelper.floatParse(context.Request.Form["latitude"].ToString());
                float longitude = DataHelper.floatParse(context.Request.Form["longitude"].ToString());
                string google_address = context.Request.Form["google_address"].ToString();
                decimal amount = DataHelper.longParse(context.Request.Form["amount"].ToString());
                int quantity = DataHelper.intParse(context.Request.Form["quantity"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();
                long tanker_type_id = DataHelper.longParse(context.Request.Form["tanker_type_id"].ToString());
                string contact_person = context.Request.Form["contact_person"].ToString();
                

                if (order_type_id == 2)
                {
                    if (context.Request.Form["schedule_date"] != null && context.Request.Form["schedule_date"] != "")
                    {
                        schedule_date = context.Request.Form["schedule_date"].ToString();
                    }
                    else
                    {
                        var json = new { ErrorCode = "003", ErrorDescription = "Schedule Date not supplied" };
                        JsonHelper.WriteJson(json, context);
                        return;
                    }
                }

                if (context.Request.Form["promo_code"] != null)
                    promo_code = context.Request.Form["promo_code"].ToString();

                if (context.Request.Form["remarks"] != null)
                    remarks = context.Request.Form["remarks"].ToString();

                if (context.Request.Form["vendor"] != null)
                    vendor = context.Request.Form["vendor"].ToString();

                if (context.Request.Form["near_by_location"] != null)
                    near_by_location = context.Request.Form["near_by_location"].ToString();

                if (context.Request.Form["preferred_time"] != null)
                    preferred_time = context.Request.Form["preferred_time"].ToString();


                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@OrderTypeId", Value = order_type_id },
                                            new SqlParameter() { ParameterName = "@LocationId", Value = location_id },
                                            new SqlParameter() { ParameterName = "@Address", Value = address },
                                            new SqlParameter() { ParameterName = "@Latitude", Value = latitude },
                                            new SqlParameter() { ParameterName = "@Longitude", Value = longitude },
                                            new SqlParameter() { ParameterName = "@GoogleAddress", Value = google_address },
                                            new SqlParameter() { ParameterName = "@Amount", Value = amount },
                                            new SqlParameter() { ParameterName = "@Quantity", Value = quantity },

                                            new SqlParameter() { ParameterName = "@ScheduleDate", Value = schedule_date },
                                            new SqlParameter() { ParameterName = "@PromoCode", Value = promo_code },
                                            new SqlParameter() { ParameterName = "@Remarks", Value = remarks },
                                            new SqlParameter() { ParameterName = "@Vendor", Value = vendor },
                                            new SqlParameter() { ParameterName = "@NearByLocation", Value = near_by_location },
                                            new SqlParameter() { ParameterName = "@TankerTypeId", Value = tanker_type_id },
                                            new SqlParameter() { ParameterName = "@ContactPerson", Value = contact_person },
                                            new SqlParameter() { ParameterName = "@PreferredTime", Value = preferred_time },

                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_SetOrder", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new { ErrorCode = "000", ErrorDescription = "OK", OrderId = DataHelper.longParse(dr["OrderId"]) };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "SetOrder()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Order History
        /// Parameters 
        ///     * order_id                 (Order Id)                    [Required]
        ///     * account_id               (Account Id)                  [Required]
        ///     * token                    (Token)                       [Required]
        ///     * device_id                (Device Id)                   [Required]
        /// </summary>
        private void GetOrdersHistory()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 No Record Found
            // 999 Exception / unknown error 

            try
            {
                
                if (context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_GetOrderHistory", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                if (dtbls[0].Rows[0]["ErrorCode"].ToString() == "000")
                {

                    if (dtbls[1].Rows.Count > 0)
                    {
                        List<Order> list = new List<Order>();

                        foreach (DataRow row in dtbls[1].Rows)
                        {

                            list.Add(new Order()
                            {
                                OrderId = DataHelper.longParse(row["OrderId"]),
                                OrderNumber = "PW-" + DataHelper.longParse(row["OrderId"]).ToString("0000"),
                                OrderDate = DataHelper.dateParse(row["OrderDate"]).ToString("dd-MMM-yyyy"),
                                OrderTime = DataHelper.dateParse(row["OrderTime"]).ToString("hh:mm tt"),
                                AccountId = DataHelper.longParse(row["AccountId"].ToString()),
                                OrderType = DataHelper.intParse(row["OrderTypeId"]) == 1 ? "On Time Delivery" : "Scheduled Delivery",
                                ScheduleDate = DataHelper.dateParse(row["ScheduleDate"]).ToString("dd-MMM-yyyy"),
                                PromoCode = DataHelper.stringParse(row["PromoCode"].ToString()),
                                TankerTypeName = DataHelper.stringParse(row["TankerTypeName"].ToString()),
                                LocationName = DataHelper.stringParse(row["LocationName"].ToString()),
                                Address = DataHelper.stringParse(row["Address"].ToString()),
                                Latitude = DataHelper.floatParse(row["Latitude"].ToString()),
                                Longitude = DataHelper.floatParse(row["Longitude"].ToString()),
                                GoogleAddress = DataHelper.stringParse(row["GoogleAddress"].ToString()),
                                Amount = DataHelper.decimalParse(row["Amount"].ToString()),
                                Quantity = DataHelper.intParse(row["Quantity"].ToString()),
                                TotalAmount = DataHelper.decimalParse(row["TotalAmount"].ToString()),
                                ReceivedAmount = DataHelper.decimalParse(row["ReceivedAmount"].ToString()),
                                RemainingAmount = DataHelper.decimalParse(row["RemainingAmount"].ToString()),
                                OrderStatusId = DataHelper.intParse(row["OrderStatusId"].ToString()),
                                OrderStatus = DataHelper.stringParse(row["OrderStatus"].ToString()),
                                PaidStatus = DataHelper.intParse(row["OrderStatusId"].ToString()) == 5 ? "Paid" : "Unpaid",
                                Remarks = DataHelper.stringParse(row["Remarks"].ToString()),
                                Vendor = DataHelper.stringParse(row["Vendor"].ToString()),
                                NearByLocation = DataHelper.stringParse(row["NearByLocation"].ToString()),
                                OrderRating = DataHelper.intParse(row["OrderRating"].ToString()),
                                OrderFeedback = DataHelper.stringParse(row["OrderFeedback"].ToString()),
                                ContactPerson = DataHelper.stringParse(row["ContactPerson"].ToString()),
                                DeliveryDate = DataHelper.dateParse(row["DeliveryDate"]).ToString("dd-MMM-yyyy"),
                                PreferredTime = DataHelper.stringParse(row["PreferredTime"].ToString())
                            });
                        }

                        var json = new { ErrorCode = "000", ErrorDescription = "OK", Orders = list };
                        JsonHelper.WriteJson(json, context);
                    }
                    else
                    {
                        var json = new { ErrorCode = "003", ErrorDescription = "No Record Found" };
                        JsonHelper.WriteJson(json, context);
                    }

                }
                else
                {
                    var json = new { ErrorCode = "002", ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "GetOrdersHistory()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Tanker Types
        /// Parameters
        ///     * account_id               (Account Id)                  [Required]
        ///     * token                    (Token)                       [Required]
        ///     * device_id                (Device Id)                   [Required]
        /// </summary>
        private void GetTankerTypes()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 No Record Found
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_GetTankerTypes", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                if (dtbls[0].Rows[0]["ErrorCode"].ToString() == "000")
                {

                    if (dtbls[1].Rows.Count > 0)
                    {
                        List<TankerType> list = new List<TankerType>();

                        foreach (DataRow row in dtbls[1].Rows)
                        {
                            list.Add(new TankerType()
                            {
                                TankerTypeId = DataHelper.longParse(row["TankerTypeId"].ToString()),
                                TankerTypeName = DataHelper.stringParse(row["TankerTypeName"].ToString())
                            });
                        }

                        var json = new { ErrorCode = "000", ErrorDescription = "OK", TankerTypes = list };
                        JsonHelper.WriteJson(json, context);
                    }
                    else
                    {
                        var json = new { ErrorCode = "003", ErrorDescription = "No Record Found" };
                        JsonHelper.WriteJson(json, context);
                    }

                }
                else
                {
                    var json = new { ErrorCode = "002", ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "GetTankerTypes()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Location Rates
        /// Parameters
        ///     * tanker_type_id           (Tanker Type Id)              [Required]
        ///     * account_id               (Account Id)                  [Required]
        ///     * token                    (Token)                       [Required]
        ///     * device_id                (Device Id)                   [Required]
        /// </summary>
        private void GetLocationRates()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 No Record Found
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["tanker_type_id"] == null || context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long tanker_type_id = DataHelper.longParse(context.Request.Form["tanker_type_id"].ToString());
                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@TankerTypeId", Value = tanker_type_id },
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_GetLocationRates", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                if (dtbls[0].Rows[0]["ErrorCode"].ToString() == "000")
                {

                    if (dtbls[1].Rows.Count > 0)
                    {
                        List<Rate> list = new List<Rate>();

                        foreach (DataRow row in dtbls[1].Rows)
                        {
                            list.Add(new Rate()
                            {
                                LocationId = DataHelper.longParse(row["LocationId"].ToString()),
                                LocationName = DataHelper.stringParse(row["LocationName"].ToString()),
                                CurrentRate = DataHelper.decimalParse(row["CurrentRate"].ToString())
                            });
                        }

                        var json = new { ErrorCode = "000", ErrorDescription = "OK", LocationRates = list };
                        JsonHelper.WriteJson(json, context);
                    }
                    else
                    {
                        var json = new { ErrorCode = "003", ErrorDescription = "No Record Found" };
                        JsonHelper.WriteJson(json, context);
                    }

                }
                else
                {
                    var json = new { ErrorCode = "002", ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "GetLocationRates()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Accounts Address
        /// Parameters 
        ///     * title                    (Title)                       [Required]
        ///     * address                  (Address)                     [Required]
        ///     * near_by_location         (Near By Location)            [Optional]
        ///     * account_id               (Account Id)                  [Required]
        ///     * token                    (Token)                       [Required]
        ///     * device_id                (Device Id)                   [Required]
        /// </summary>
        private void SaveAccountsAddress()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 Title or Address already exist
            // 999 Exception / unknown error 

            try
            {

                string near_by_location = "";

                if (context.Request.Form["title"] == null || context.Request.Form["address"] == null || context.Request.Form["account_id"] == null || 
                    context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                string title = context.Request.Form["title"].ToString();
                string address = context.Request.Form["address"].ToString();
                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();


                if (context.Request.Form["near_by_location"] != null)
                    near_by_location = context.Request.Form["near_by_location"].ToString();


                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@Title", Value = title },
                                            new SqlParameter() { ParameterName = "@Address", Value = address },
                                            new SqlParameter() { ParameterName = "@NearByLocation", Value = near_by_location },
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_SaveAccountsAddress", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new { AddressId = DataHelper.longParse(dr["AddressId"].ToString()), ErrorCode = "000", ErrorDescription = "OK" };
                    JsonHelper.WriteJson(json, context);
                }
                else if (dr["ErrorCode"].ToString() == "003")
                {
                    var json = new { ErrorCode = "003", ErrorDescription = "Title or Address already exist" };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "SaveAccountsAddress()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Get Accounts Address
        /// Parameters
        ///     * account_id               (Account Id)                  [Required]
        ///     * token                    (Token)                       [Required]
        ///     * device_id                (Device Id)                   [Required]
        /// </summary>
        private void GetAccountsAddress()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 No Record Found
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_GetAccountsAddress", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                if (dtbls[0].Rows[0]["ErrorCode"].ToString() == "000")
                {

                    if (dtbls[1].Rows.Count > 0)
                    {
                        List<AccountsAddress> list = new List<AccountsAddress>();

                        foreach (DataRow row in dtbls[1].Rows)
                        {
                            list.Add(new AccountsAddress()
                            {
                                AddressId = DataHelper.longParse(row["AddressId"].ToString()),
                                Title = DataHelper.stringParse(row["Title"].ToString()),
                                Address = DataHelper.stringParse(row["Address"].ToString()),
                                NearByLocation = DataHelper.stringParse(row["NearByLocation"].ToString())
                            });
                        }

                        var json = new { ErrorCode = "000", ErrorDescription = "OK", AccountsAddress = list };
                        JsonHelper.WriteJson(json, context);
                    }
                    else
                    {
                        var json = new { ErrorCode = "003", ErrorDescription = "No Record Found" };
                        JsonHelper.WriteJson(json, context);
                    }

                }
                else
                {
                    var json = new { ErrorCode = "002", ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "GetAccountsAddress()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Update Accounts Address
        /// Parameters 
        ///     * address_id                (Address Id)                        [Required]
        ///     * title                     (Title)                             [Required]
        ///     * address                   (Address)                           [Required]
        ///     * near_by_location          (Near By Location)                  [Optional]
        ///     * account_id                (Account Id)                        [Required]
        ///     * token                     (Token)                             [Required]
        ///     * device_id                 (Device Id)                         [Required]
        /// </summary>
        private void UpdateAccountsAddress()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 999 Exception / unknown error 

            try
            {

                string near_by_location = "";

                if (context.Request.Form["address_id"] == null || context.Request.Form["title"] == null || context.Request.Form["address"] == null ||
                    context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long address_id = DataHelper.longParse(context.Request.Form["address_id"].ToString());
                string title = context.Request.Form["title"].ToString();
                string address = context.Request.Form["address"].ToString();
                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();


                if (context.Request.Form["near_by_location"] != null)
                    near_by_location = context.Request.Form["near_by_location"].ToString();


                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AddressId", Value = address_id },
                                            new SqlParameter() { ParameterName = "@Title", Value = title },
                                            new SqlParameter() { ParameterName = "@Address", Value = address },
                                            new SqlParameter() { ParameterName = "@NearByLocation", Value = near_by_location },
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_UpdateAccountsAddress", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];

                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new { ErrorCode = "000", ErrorDescription = "OK" };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "UpdateAccountsAddress()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Delete Accounts Address
        /// Parameters 
        ///     * address_id                (Address Id)                        [Required]
        ///     * account_id                (Account Id)                        [Required]
        ///     * token                     (Token)                             [Required]
        ///     * device_id                 (Device Id)                         [Required]
        /// </summary>
        private void DeleteAccountsAddress()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 No Record Found
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["address_id"] == null || context.Request.Form["account_id"] == null 
                    || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long address_id = DataHelper.longParse(context.Request.Form["address_id"].ToString());
                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AddressId", Value = address_id },
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_DeleteAccountsAddress", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];

                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new { ErrorCode = "000", ErrorDescription = "OK" };
                    JsonHelper.WriteJson(json, context);
                } 
                else if (dr["ErrorCode"].ToString() == "003")
                {
                    var json = new { ErrorCode = "003", ErrorDescription = "No Record Found" };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "DeleteAccountsAddress()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>FAQs Category
        /// Parameters
        ///     * account_id               (Account Id)                  [Required]
        ///     * token                    (Token)                       [Required]
        ///     * device_id                (Device Id)                   [Required]
        /// </summary>
        private void GetFAQCategory()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 No Record Found
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_GetFaqsCategory", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                if (dtbls[0].Rows[0]["ErrorCode"].ToString() == "000")
                {

                    if (dtbls[1].Rows.Count > 0)
                    {
                        List<FaqsCategory> list = new List<FaqsCategory>();

                        foreach (DataRow row in dtbls[1].Rows)
                        {
                            list.Add(new FaqsCategory()
                            {
                                CategoryId = DataHelper.longParse(row["CategoryId"].ToString()),
                                CategoryName = DataHelper.stringParse(row["CategoryName"].ToString())
                            });
                        }

                        var json = new { ErrorCode = "000", ErrorDescription = "OK", FAQCategory = list };
                        JsonHelper.WriteJson(json, context);
                    }
                    else
                    {
                        var json = new { ErrorCode = "003", ErrorDescription = "No Record Found" };
                        JsonHelper.WriteJson(json, context);
                    }

                }
                else
                {
                    var json = new { ErrorCode = "002", ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "GetFAQCategory()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>FAQs
        /// Parameters
        ///     * account_id               (Account Id)                  [Required]
        ///     * token                    (Token)                       [Required]
        ///     * device_id                (Device Id)                   [Required]
        ///     * category_id              (Faq Category Id)             [Required]
        /// </summary>
        private void GetFAQs()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 No Record Found
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null ||
                    context.Request.Form["category_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();
                long category_id = DataHelper.longParse(context.Request.Form["category_id"].ToString());

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                            new SqlParameter() { ParameterName = "@CategoryId", Value = category_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_GetFaqs", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                if (dtbls[0].Rows[0]["ErrorCode"].ToString() == "000")
                {

                    if (dtbls[1].Rows.Count > 0)
                    {
                        List<Faqs> list = new List<Faqs>();

                        foreach (DataRow row in dtbls[1].Rows)
                        {
                            list.Add(new Faqs()
                            {
                                FaqId = DataHelper.longParse(row["FaqId"].ToString()),
                                Title = DataHelper.stringParse(row["Title"].ToString()),
                                Description = DataHelper.stringParse(row["Description"].ToString()),
                                CategoryId = DataHelper.longParse(row["CategoryId"].ToString()),
                                CategoryName = DataHelper.stringParse(row["CategoryName"].ToString())
                            });
                        }

                        var json = new { ErrorCode = "000", ErrorDescription = "OK", FAQs = list };
                        JsonHelper.WriteJson(json, context);
                    }
                    else
                    {
                        var json = new { ErrorCode = "003", ErrorDescription = "No Record Found" };
                        JsonHelper.WriteJson(json, context);
                    }

                }
                else
                {
                    var json = new { ErrorCode = "002", ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "GetFAQs()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Notifications
        /// Parameters
        ///     * account_id               (Account Id)                  [Required]
        ///     * token                    (Token)                       [Required]
        ///     * device_id                (Device Id)                   [Required]
        /// </summary>
        private void GetNotifications()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 No Record Found
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_GetNotifications", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                if (dtbls[0].Rows[0]["ErrorCode"].ToString() == "000")
                {

                    if (dtbls[1].Rows.Count > 0)
                    {
                        List<Notifications> list = new List<Notifications>();

                        foreach (DataRow row in dtbls[1].Rows)
                        {
                            list.Add(new Notifications()
                            {
                                Id = DataHelper.longParse(row["Id"].ToString()),
                                Title = DataHelper.stringParse(row["Title"].ToString()),
                                Description = DataHelper.stringParse(row["Description"].ToString()),
                                StartDate = DataHelper.dateParse(row["StartDate"]).ToString("dd-MMM-yyyy"),
                                EndDate = DataHelper.dateParse(row["EndDate"]).ToString("dd-MMM-yyyy"),
                                ImageURL = DataHelper.getNotificationsPicture(row["ImageURL"].ToString()),
                                IsRead = DataHelper.intParse(row["IsRead"]) == 1 ? true : false
                            });
                        }

                        var json = new { ErrorCode = "000", ErrorDescription = "OK", Notifications = list };
                        JsonHelper.WriteJson(json, context);
                    }
                    else
                    {
                        var json = new { ErrorCode = "003", ErrorDescription = "No Record Found" };
                        JsonHelper.WriteJson(json, context);
                    }

                }
                else
                {
                    var json = new { ErrorCode = "002", ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "GetNotifications()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Set Notification Read Status
        /// Parameters 
        ///     * account_id                (Account Id)          [Required]
        ///     * token                     (Token)               [Required]
        ///     * device_id                 (Device Id)           [Required]
        ///     * notification_id           (Notification Id)     [Required]
        /// </summary>
        private void SetNotificationReadStatus()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 Notification Already Read
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null ||
                    context.Request.Form["notification_id"] == null || context.Request.Form["notification_id"] == "0")
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();
                long notification_id = DataHelper.longParse(context.Request.Form["notification_id"].ToString());

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                            new SqlParameter() { ParameterName = "@NotificationId", Value = notification_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_SetNotificationReadStatus", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new { ErrorCode = "000", ErrorDescription = "OK" };
                    JsonHelper.WriteJson(json, context);
                }
                else if (dr["ErrorCode"].ToString() == "003")
                {
                    var json = new { ErrorCode = "003", ErrorDescription = "Notification Already Read" };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "SetNotificationReadStatus()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Set Notification Enable/Disable Flag
        /// Parameters 
        ///     * account_id                (Account Id)          [Required]
        ///     * token                     (Token)               [Required]
        ///     * device_id                 (Device Id)           [Required]
        ///     * status                    (Status)              [Required]
        /// </summary>
        private void SetNotificationEnableStatus()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null ||
                    context.Request.Form["status"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();
                bool status = DataHelper.boolParse(context.Request.Form["status"].ToString());

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                            new SqlParameter() { ParameterName = "@Status", Value = status },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_SetNotificationAllowStatus", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new { ErrorCode = "000", ErrorDescription = "OK" };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "SetNotificationAllowStatus()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Set Cancel Order
        /// Parameters 
        ///     * account_id                (Account Id)                [Required]
        ///     * token                     (Token)                     [Required]
        ///     * device_id                 (Device Id)                 [Required]
        ///     * order_id                  (Order Id)                  [Required]
        ///     * reason                    (Reason for cancel order)   [Optional]
        /// </summary>
        private void SetCancelOrder()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 Order Cannot cancel because Order In-Progress or Delivered or already cancelled
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null ||
                    context.Request.Form["order_id"] == null || context.Request.Form["order_id"] == "0" )
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();
                long order_id = DataHelper.longParse(context.Request.Form["order_id"].ToString());
                string remarks = "";

                if (context.Request.Form["remarks"] != null)
                    remarks = context.Request.Form["remarks"].ToString();

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                            new SqlParameter() { ParameterName = "@OrderId", Value = order_id },
                                            new SqlParameter() { ParameterName = "@Remarks", Value = remarks },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_CancelOrder", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new { ErrorCode = "000", ErrorDescription = "OK" };
                    JsonHelper.WriteJson(json, context);
                }
                else if (dr["ErrorCode"].ToString() == "003")
                {
                    var json = new { ErrorCode = "003", ErrorDescription = "Order Cannot be cancel because Order In-Progress or Delivered or already cancelled" };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "SetCancelOrder()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        /// <summary>Logout
        /// Parameters 
        ///     * account_id               (Account Id)                  [Required]
        ///     * token                    (Token)                       [Required]
        ///     * device_id                (Device Id)                   [Required]
        /// </summary>
        private void LogoutAccount()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["account_id"] == null || context.Request.Form["token"] == null || context.Request.Form["device_id"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                long account_id = DataHelper.longParse(context.Request.Form["account_id"].ToString());
                string token = context.Request.Form["token"].ToString();
                string device_id = context.Request.Form["device_id"].ToString();

                SqlParameter[] param = {
                                            new SqlParameter() { ParameterName = "@AccountId", Value = account_id },
                                            new SqlParameter() { ParameterName = "@Token", Value = token },
                                            new SqlParameter() { ParameterName = "@DeviceId", Value = device_id },
                                    };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("SP_LogoutAccount", param, ref response);
                if (response.Error)
                {
                    throw new Exception(response.ErrorList[0].Message);
                }

                DataRow dr = dtbls[0].Rows[0];
                if (dr["ErrorCode"].ToString() == "000")
                {
                    var json = new { ErrorCode = "000", ErrorDescription = "OK" };
                    JsonHelper.WriteJson(json, context);
                }
                else
                {
                    var json = new { ErrorCode = dr["ErrorCode"].ToString(), ErrorDescription = "Invalid Token" };
                    JsonHelper.WriteJson(json, context);
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "LogoutAccount()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        private void TestEmail()
        {
            // 000 Succeeds
            // 001 Required Parameters not supplied
            // 002 Invalid Token
            // 003 No Record Found
            // 999 Exception / unknown error 

            try
            {

                if (context.Request.Form["user_id"] == null || context.Request.Form["email"] == null || context.Request.Form["activation_code"] == null)
                {
                    var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                    JsonHelper.WriteJson(json, context);
                    return;
                }

                string user_id = context.Request.Form["user_id"].ToString();
                string email = context.Request.Form["email"].ToString();
                string activation_code = context.Request.Form["activation_code"].ToString();

                EmailTemplate.SignUpEmail(user_id, email, activation_code);
                
            }
            catch (Exception ae)
            {
                Logs.WriteError("Mobile.cs", "TestEmail()", "General", ae.Message);
                var json = new { ErrorCode = "999", ErrorDescription = "Unknown Error" };
                JsonHelper.WriteJson(json, context);
            }
        }

        private void DeleteTempCustomer()
        {

            if (context.Request.Form["email"] == null)
            {
                var json = new { ErrorCode = "001", ErrorDescription = "Required parameters not supplied" };
                JsonHelper.WriteJson(json, context);
                return;
            }

            ErrorResponse response = new ErrorResponse();
            string email = context.Request.Form["email"].ToString();

            string sql = "DELETE FROM Accounts WHERE EmailAddress = '" + email + "'";
            DBService.ExecuteQuery(sql, ref response);

            var json2 = new { ErrorCode = "000", ErrorDescription = "OK" };
            JsonHelper.WriteJson(json2, context);
        }
    }

}
