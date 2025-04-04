using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebTech.Common;

namespace BLL
{
    public static class DateHandler
    {
        public static DateTime GetDate(string value)
        {
            try
            {
                int year = DataHelper.intParse(value.Substring(6));
                int month = DataHelper.intParse(value.Substring(3, 2));
                int day = DataHelper.intParse(value.Substring(0, 2));

                return new DateTime(year, month, day);
            }
            catch (Exception ae)
            {
                return DateTime.Now;
            }
        }
    }
}
