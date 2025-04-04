using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebTech.Common
{
    public static class WTURL
    {
        public static string getValidURL(string url)
        {
            if (WTString.getString(url).Contains("http://"))
            {
                return WTString.getString(url);
            }
            else if (WTString.getString(url).Length > 0)
            {
                return "http://" + WTString.getString(url);
            }
            else
            {
                return "";
            }
        }
    }
}
