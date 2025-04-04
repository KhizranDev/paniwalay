using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebTech.Common;

namespace BLL
{
    public class UserInfoCookie : System.Web.UI.Page
    {
        public const string scookiename = "opusrb__inf";
        public const string sItemId = "tmid";
        public const string sadmincookiename = "cmdt__inf";
        public const string sLoginId = "dlid";
        public const string sUserId = "duid";
        public const string sUserName = "dun";
        public const string sAdminLoginId = "dalid";
        public const string sAdminUserId = "dauid";
        public const string sAdminUserName = "daun";
        public const string sAdminLoginTime = "dalt";
        public const string sAdminMemberType = "damt";
        public const string sLoginTime = "dlt";
        public const string sMemberType = "dmt";
        public const string sLocationId = "slcId";
        public const string sZoneId = "slzncd";
        public const string sLocationName = "slcname";

        public string RecentlyViewedProducts
        {
            get
            {
                try
                {
                    HttpCookie userCookie = HttpContext.Current.Request.Cookies[scookiename];
                    if (userCookie != null)
                    {
                        return WTEncryption.Decrypt(userCookie[sItemId]);
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception ae)
                {
                    return "";
                }
            }
        }

        public bool Exists
        {
            get
            {
                try
                {
                    HttpCookie userCookie = HttpContext.Current.Request.Cookies[scookiename];
                    if (userCookie != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ae)
                {
                    Logs.WriteError("UserInfoCookie.cs", "Exists", "General", ae.Message);
                    return false;
                }
            }
        }

        public bool AdminExists
        {
            get
            {
                try
                {
                    HttpCookie userCookie = HttpContext.Current.Request.Cookies[sadmincookiename];
                    if (userCookie != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ae)
                {
                    Logs.WriteError("UserInfoCookie.cs", "AdminExists", "General", ae.Message);
                    return false;
                }
            }
        }

        public long LoginId
        {
            get
            {
                try
                {
                    if (Exists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[scookiename];
                        return DataHelper.longParse(Encryption.Decrypt(userCookie[sLoginId], null));
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ae)
                {
                    return 0;
                }
            }
        }

        public string UserId
        {
            get
            {
                try
                {
                    if (Exists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[scookiename];
                        return Encryption.Decrypt(userCookie[sUserId], null);
                    }
                    else
                    {
                        return "";
                    }

                }
                catch (Exception ae)
                {
                    return "";
                }
            }
        }

        public string UserName
        {
            get
            {
                try
                {
                    if (Exists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[scookiename];
                        return Encryption.Decrypt(userCookie[sUserName], null);
                    }
                    else
                    {
                        return "";
                    }

                }
                catch (Exception ae)
                {
                    return "";
                }
            }
        }

        public long AdminLoginId
        {
            get
            {
                try
                {
                    if (AdminExists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[sadmincookiename];
                        return DataHelper.longParse(Encryption.Decrypt(userCookie[sAdminLoginId], null));
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ae)
                {
                    return 0;
                }
            }
        }

        public string AdminUserId
        {
            get
            {
                try
                {
                    if (AdminExists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[sadmincookiename];
                        return Encryption.Decrypt(userCookie[sAdminUserId], null);
                    }
                    else
                    {
                        return "";
                    }

                }
                catch (Exception ae)
                {
                    return "";
                }
            }
        }

        public string AdminUserName
        {
            get
            {
                try
                {
                    if (AdminExists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[sadmincookiename];
                        return Encryption.Decrypt(userCookie[sAdminUserName], null);
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception ae)
                {
                    return "";
                }
            }
        }

        public DateTime AdminLoginTime
        {
            get
            {
                try
                {
                    if (AdminExists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[sadmincookiename];
                        return DataHelper.dateParse(Encryption.Decrypt(userCookie[sAdminLoginTime], null));
                    }
                    else
                    {
                        return DateTime.Now;
                    }

                }
                catch (Exception ae)
                {
                    return DateTime.Now;
                }
            }
        }

        public int AdminMemberType
        {
            get
            {
                try
                {
                    if (AdminExists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[sadmincookiename];
                        return DataHelper.intParse(Encryption.Decrypt(userCookie[sAdminMemberType], null));
                    }
                    else
                    {
                        return 0;
                    }

                }
                catch (Exception ae)
                {
                    return 0;
                }
            }
        }

        public DateTime LoginTime
        {
            get
            {
                try
                {
                    if (Exists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[scookiename];
                        return DataHelper.dateParse(Encryption.Decrypt(userCookie[sLoginTime], null));
                    }
                    else
                    {
                        return DateTime.Now;
                    }

                }
                catch (Exception ae)
                {
                    return DateTime.Now;
                }
            }
        }

        public int MemberType
        {
            get
            {
                try
                {
                    if (Exists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[scookiename];
                        return DataHelper.intParse(Encryption.Decrypt(userCookie[sMemberType], null));
                    }
                    else
                    {
                        return 0;
                    }

                }
                catch (Exception ae)
                {
                    return 0;
                }
            }
        }

        public long LocationId
        {
            get
            {
                try
                {
                    if (Exists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[scookiename];
                        return DataHelper.intParse(Encryption.Decrypt(userCookie[sLocationId], null));
                    }
                    else
                    {
                        return 0;
                    }

                }
                catch (Exception ae)
                {
                    return 0;
                }
            }
        }



        public long ZoneId
        {
            get
            {
                try
                {
                    if (Exists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[scookiename];
                        return DataHelper.intParse(Encryption.Decrypt(userCookie[sZoneId], null));
                    }
                    else
                    {
                        return 0;
                    }

                }
                catch (Exception ae)
                {
                    return 0;
                }
            }
        }

        public string LocationName
        {
            get
            {
                try
                {
                    if (Exists)
                    {
                        HttpCookie userCookie = HttpContext.Current.Request.Cookies[scookiename];
                        return DataHelper.stringParse(Encryption.Decrypt(userCookie[sLocationName], null));
                    }
                    else
                    {
                        return "";
                    }

                }
                catch (Exception ae)
                {
                    return "";
                }
            }
        }

        public void Logout()
        {
            //Response.Cookies[scookiename].Expires = DateTime.Now.AddDays(-1);
        }
    }
}
