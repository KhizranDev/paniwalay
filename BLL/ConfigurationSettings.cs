using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebTech.Common;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Data.SqlTypes;
using System.Web;
using System.IO;

namespace BLL
{
    public static class ConfigurationSettings
    {
        public static int ItemDaysCheck
        {
            get
            {
                int ItemDaysCheck = 15;

                if (System.Web.Configuration.WebConfigurationManager.AppSettings["ItemDaysCheck"] != null)
                    ItemDaysCheck = DataHelper.intParse(System.Web.Configuration.WebConfigurationManager.AppSettings["ItemDaysCheck"].ToString());
                else
                    Logs.WriteError("Global", "Configuration", "General", "Item days check not defined in web.config");

                return ItemDaysCheck;
            }
            
        }

        public static int TransDaysCheck
        {
            get
            {
                int TransDaysCheck = 4;

                if (System.Web.Configuration.WebConfigurationManager.AppSettings["TransDaysCheck"] != null)
                    TransDaysCheck = DataHelper.intParse(System.Web.Configuration.WebConfigurationManager.AppSettings["TransDaysCheck"].ToString());
                else
                    Logs.WriteError("Global", "Configuration", "General", "Transaction days check not defined in web.config");

                return TransDaysCheck;
            }

        }


        public static string Format
        {
            get
            {
                string CurrencyFormat = "0,0";

                if (System.Web.Configuration.WebConfigurationManager.AppSettings["CurrencyFormat"] != null)
                    CurrencyFormat = DataHelper.stringParse(System.Web.Configuration.WebConfigurationManager.AppSettings["CurrencyFormat"].ToString());
                else
                    Logs.WriteError("Global", "Configuration", "General", "Currency Format not defined in web.config");

                return CurrencyFormat;
            }

        }

        public static string FormatPercentage
        {
            get
            {
                string CurrencyFormat = "#0";

                if (System.Web.Configuration.WebConfigurationManager.AppSettings["CurrencyPercentageFormat"] != null)
                    CurrencyFormat = DataHelper.stringParse(System.Web.Configuration.WebConfigurationManager.AppSettings["CurrencyPercentageFormat"].ToString());
                else
                    Logs.WriteError("Global", "Configuration", "General", "Currency Percentage Format not defined in web.config");

                return CurrencyFormat;
            }

        }

        public static int CheckWeight
        {
            get
            {
                int Weight = 1;

                if (System.Web.Configuration.WebConfigurationManager.AppSettings["CheckWeight"] != null)
                    Weight = DataHelper.intParse(System.Web.Configuration.WebConfigurationManager.AppSettings["CheckWeight"].ToString());
                else
                    Logs.WriteError("Global", "Configuration", "General", "Check Weight not defined in web.config");

                return Weight;
            }

        }
    }
}
