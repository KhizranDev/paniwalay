using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebTech.Common;
using BLL;
using System.Text;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for clsSession
/// </summary>
public static class clsSession
{
    public static bool useSession = true;

    public static bool HasLogin
    {
        get
        {
            if (useSession)
                return WTSession.SessionExists(Sessions.LoginId);
            else
                return (clsCookie.LoginId > 0);
        }
    }

    public static long LoginId
    {
        get
        {
            if (useSession)
                return DataHelper.longParse(WTSession.GetSessionValue(Sessions.LoginId));
            else
                return clsCookie.LoginId;
        }
    }

    public static string UserId
    {
        get
        {
            if (useSession)
                return DataHelper.stringParse(WTSession.GetSessionValue(Sessions.UserId));
            else
                return clsCookie.UserId;
        }
    }

    public static string UserName
    {
        get
        {
            if (useSession)
                return DataHelper.stringParse(WTSession.GetSessionValue(Sessions.UserName));
            else
                return clsCookie.UserName;
        }
    }

    public static long AdminLoginId
    {
        get
        {
            if (useSession)
                return DataHelper.longParse(WTSession.GetSessionValue(Sessions.AdminLoginId));
            else
                return clsCookie.AdminLoginId;
        }
    }

    public static string AdminUserId
    {
        get
        {
            if (useSession)
                return DataHelper.stringParse(WTSession.GetSessionValue(Sessions.AdminUserId));
            else
                return clsCookie.AdminUserId;
        }
    }

    public static string AdminUserName
    {
        get
        {
            if (useSession)
                return DataHelper.stringParse(WTSession.GetSessionValue(Sessions.AdminUserName));
            else
                return clsCookie.AdminUserName;
        }
    }

    public static DateTime AdminLoginTime
    {
        get
        {
            if (useSession)
                return DataHelper.dateParse(WTSession.GetSessionValue(Sessions.AdminLoginTime));
            else
                return clsCookie.AdminLoginTime;
        }
    }

    public static int AdminMemberType
    {
        get
        {
            if (useSession)
                return DataHelper.intParse(WTSession.GetSessionValue(Sessions.AdminMemberType));
            else
                return clsCookie.AdminMemberType;
        }
    }

    public static DateTime LoginTime
    {
        get
        {
            if (useSession)
                return DataHelper.dateParse(WTSession.GetSessionValue(Sessions.LoginTime));
            else
                return clsCookie.LoginTime;
        }

    }

    public static int MemberType
    {
        get
        {
            if (useSession)
                return DataHelper.intParse(WTSession.GetSessionValue(Sessions.MemberType));
            else
                return clsCookie.MemberType;
        }
    }

    public static long CustomerId
    {
        get
        {
            return DataHelper.longParse(WTSession.GetSessionValue(Sessions.CustomerId));
        }
    }

    public static string ErrorCode
    {
        get
        {
            if (WTSession.SessionExists(Sessions.ErrorCode))
            {
                return WTSession.GetSessionValue(Sessions.ErrorCode).ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            WTSession.CreateSession(Sessions.ErrorCode, value);
        }
    }

    public static void Logout()
    {
        if (useSession)
            WTSession.Logout();
        else
            clsCookie.Logout();
    }

    public static decimal GetMinimumOrder()
    {
        ErrorResponse response = new ErrorResponse();
        DataTable dt = DBService.FetchTable("SELECT CAST(ParameterValue as decimal(18,2)) MinimumOrder FROM SystemConfiguration WHERE OptionID=0 AND ParameterName='OS_MinimumOrder'", ref response);
        DataRow row = dt.Rows[0];
        decimal minimumOrder = 0;
        return minimumOrder = DataHelper.decimalParse(row["MinimumOrder"]);
    }

    public static decimal GetShippingCharges()
    {
        ErrorResponse response = new ErrorResponse();
        DataTable dt = DBService.FetchTable("SELECT CAST(ParameterValue as decimal(18,0)) Charges FROM SystemConfiguration WHERE OptionID=0 AND ParameterName='Charges'", ref response);
        DataRow row = dt.Rows[0];

        decimal shippingCharges;
        return shippingCharges = DataHelper.decimalParse(row["Charges"]);
    }
}

public class ShippingChargesList
{
    public long CountryId { get; set; }
    public long StateId { get; set; }
    public long CityId { get; set; }
    public decimal Minimmum { get; set; }
    public decimal Maximum { get; set; }
    public decimal Charges { get; set; }
}

public class Sessions
{
    public const string LoginId = "LoginId";
    public const string UserId = "UserId";
    public const string CustomerId = "CustomerId";
    public const string UserName = "UserName";
    public const string AdminLoginId = "AdminLoginId";
    public const string AdminUserId = "AdminUserId";
    public const string AdminUserName = "AdminUserName";
    public const string AdminLoginTime = "AdminLoginTime";
    public const string AdminMemberType = "AdminMemberType";
    public const string LoginTime = "LoginTime";
    public const string MemberType = "MemberType";
    public const string PackageDetail = "PackageDetail";
    public const string TrialDetail = "TrialDetail";
    public const string ErrorCode = "ErrorCode";
    public const string Basket = "Basket";
    public const string Package = "Package";
    public const string TotalShippingCharges = "TotalShippingCharges";
    public const string TotalAmount = "TotalAmount";
}