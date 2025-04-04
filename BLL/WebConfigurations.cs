using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace BLL
{
    class WebConfigurations
    {
        public const bool ApplyGlobalTax = false;

        public static string URL
        {
            get
            {
                if (ConfigurationManager.AppSettings["URL"] != null)
                {
                    return ConfigurationManager.AppSettings["URL"].ToString();
                }
                else
                {
                    return "http://loto.pk/";
                }
            }
        }

        public static string WebURL
        {
            get
            {
                if (ConfigurationManager.AppSettings["WebURL"] != null)
                {
                    return ConfigurationManager.AppSettings["WebURL"].ToString();
                }
                else
                {
                    return "http://loto.pk";
                }
            }
        }
    }

}
