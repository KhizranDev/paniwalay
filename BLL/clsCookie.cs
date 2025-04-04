using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebTech.Common;

namespace BLL
{
    public static class clsCookie
    {
        public static long ExcludedRecentlyViewedProductId { get; set; }

        public static List<long> RecentlyViewedProducts
        {
            get
            {
                UserInfoCookie info = new UserInfoCookie();
                string[] val = info.RecentlyViewedProducts.Split(',');

                List<long> list = new List<long>();
                foreach (string item in val)
                {
                    if (DataHelper.longParse(item) > 0)
                        list.Add(DataHelper.longParse(item));
                }

                return list;
            }
        }


        public static string cookieName
        {
            get
            {
                return UserInfoCookie.scookiename;
            }
        }

        public static bool Exists
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.Exists;
            }
        }

        public static long LoginId
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.LoginId;
            }
        }

        public static string UserId
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.UserId;
            }
        }

        public static string UserName
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.UserName;
            }
        }

        public static long AdminLoginId
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.AdminLoginId;
            }
        }

        public static string AdminUserId
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.AdminUserId;
            }
        }

        public static string AdminUserName
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.AdminUserName;
            }
        }

        public static DateTime AdminLoginTime
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.AdminLoginTime;
            }
        }

        public static int AdminMemberType
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.AdminMemberType;
            }
        }

        public static DateTime LoginTime
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.LoginTime;
            }
        }

        public static int MemberType
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.MemberType;
            }
        }


        public static long ZoneId
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.ZoneId;
            }
        }

        public static long LocationId
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.LocationId;
            }
        }

        public static string LocationName
        {
            get
            {
                UserInfoCookie cookie = new UserInfoCookie();
                return cookie.LocationName;
            }
        }

        public static void Logout()
        {
            clsCookie.Logout();
        }
    }
}
