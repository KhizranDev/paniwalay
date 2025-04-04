using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Web;
using System.IO;

namespace WebTech.Common
{
    public static class DataHelper
    {
        public static string stringParse(object value)
        {
            try
            {
                if (value != null)
                {
                    return value.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static int intParse(object value)
        {
            try
            {
                if (value != null)
                {
                    int i = 0;
                    string[] values = value.ToString().Split('.');


                    int.TryParse(values[0].ToString().Replace(",", "").Replace("_", "").Replace("(", "-").Replace(")", ""), out i);
                    return i;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int[] intParseArray(string[] array)
        {
            int[] int_array = new int[array.Count()];
            int i = 0;
            foreach (string item in array)
            {
                int_array[i] = intParse(item);
                i++;
            }

            return int_array;
        }

        public static long longParse(object value)
        {
            try
            {
                if (value != null)
                {
                    long i = 0;
                    long.TryParse(value.ToString().Replace(",", "").Replace("_", "").Replace("(", "-").Replace(")", ""), out i);
                    return i;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static bool boolParse(object value)
        {
            try
            {
                if (value != null)
                {
                    bool i = false;
                    bool.TryParse(value.ToString().Replace(",", "").Replace("_", ""), out i);
                    return i;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static double doubleParse(object value)
        {
            try
            {
                if (value != null)
                {
                    double i = 0;
                    double.TryParse(value.ToString().Replace(",", "").Replace("_", "").Replace("(", "-").Replace(")", ""), out i);
                    return i;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static float floatParse(object value)
        {
            try
            {
                if (value != null)
                {
                    float i = 0;
                    float.TryParse(value.ToString().Replace(",", "").Replace("_", "").Replace("(", "-").Replace(")", ""), out i);
                    return i;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static decimal decimalParse(object value)
        {
            try
            {
                if (value != null)
                {
                    decimal i = 0;
                    decimal.TryParse(value.ToString().Replace(",", "").Replace("_", "").Replace("(", "-").Replace(")", ""), out i);
                    return i;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static decimal decimalParse(object value, decimal defaultValue)
        {
            try
            {
                if (value != null)
                {
                    decimal i = defaultValue;
                    decimal.TryParse(value.ToString().Replace(",", "").Replace("_", "").Replace("(", "-").Replace(")", ""), out i);
                    return i;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static string dateOnly(object value)
        {
            try
            {
                if (value != null)
                {
                    DateTime i;
                    if (DateTime.TryParse(value.ToString().Replace(",", "").Replace("_", ""), out i))
                        return i.ToString("dd-MMM-yyyy");

                    return "";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static DateTime dateParse(object value)
        {
            try
            {
                if (value != null)
                {
                    DateTime i;
                    if (DateTime.TryParse(value.ToString(), out i))
                        return i;

                    return DateTime.Now;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public static int arrayCount(object value)
        {
            try
            {
                if (value != null)
                {
                    object[] arr = (object[])value;
                    return arr.Count();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int arrayItemCount(string[] array)
        {
            int itemcount = 0;
            foreach (var item in array)
            {
                if (item.Length > 0)
                    itemcount++;
            }

            return itemcount;
        }

        public static int RowCount(System.Data.DataTable dt)
        {
            int row = 0;
            if (dt != null)
            {
                row = dt.Rows.Count;
            }
            return row;
        }

        public static bool HasRows(System.Data.DataTable dt)
        {
            bool has = false;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                    has = true;
            }
            return has;
        }

        public static bool ValueExists(string[] array, string matchValue)
        {
            bool returnValue = false;

            foreach (string item in array)
            {
                if (item == matchValue)
                {
                    returnValue = true;
                    break;
                }
            }

            return returnValue;
        }

        public static SqlParameter SqlParameter(string ParameterName, object Value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = ParameterName;
            parameter.Value = Value;

            return parameter;
        }

        public static string FriendlyUrlParse(string value)
        {
            return value.ToLower().Replace("-", "_").Replace("/", " ").Replace(" ", "-").Replace("'", "").Replace("\"", "").Replace("&","and");
        }

        public static long FindCategoryIdByName(string category_name)
        {
            long category_id = DataHelper.longParse(DBService.DLookupDB("SELECT Id FROM OS_Category WHERE isnull(ParentId,0) = 0 AND replace(replace(replace(replace(replace(replace(FullName, '-', '_'), '/', ' '), ' ', '-'), '''', ''), '\"',''),'&','and') = '" + category_name + "'"));
            return category_id;
        }

        public static long FindCategoryIdByName(string category_name, long parent_category_id)
        {
            long category_id = DataHelper.longParse(DBService.DLookupDB("SELECT Id FROM OS_Category WHERE ParentId=" + parent_category_id + " AND replace(replace(replace(replace(replace(replace(FullName, '-', '_'), '/', ' '), ' ', '-'), '''', ''), '\"',''),'&','and') = '" + category_name + "'"));
            return category_id;
        }

        public static long FindProductIdByName(string product_name)
        {
            long product_id = DataHelper.longParse(DBService.DLookupDB("SELECT Id FROM OS_Items WHERE replace(replace(replace(replace(replace(replace(FullName, '-', '_'), '/', ' '), ' ', '-'), '''', ''), '\"',''),'&','and') = '" + product_name + "'"));
            return product_id;
        }

        public static string FindCategoryNameById(long category_id)
        {
            string category_name = DataHelper.stringParse(DBService.DLookupDB("SELECT replace(replace(replace(replace(replace(replace(FullName, '-', '_'), '/', ' '), ' ', '-'), '''', ''), '\"',''),'&','and') FullName FROM OS_Category WHERE Id='" + category_id + "'"));
            return category_name.ToLower();
        }

        public static bool hasLastLevelCategory(long categoryId)
        {
            int count = DataHelper.intParse(DBService.DLookupDB("SELECT COUNT(*) FROM OS_Category WHERE isnull(ParentId,0) = " + categoryId + ""));
            return count == 0;
        }

        public static string GetIPAddress()
        {
            return "192.168.0.1";
        }

        public static string FormatNumber(object num)
        {
            long this_num = longParse(num);

            if (this_num >= 100000)
                return FormatNumber(this_num / 1000) + "K";
            if (this_num >= 10000)
            {
                return (this_num / 1000D).ToString("0.#") + "K";
            }
            return this_num.ToString("#,0");
        }

        public static string getPlainTextfromHTML(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            html = doc.DocumentNode.InnerText;

            return Regex.Replace(html, "<.*?>", String.Empty);
        }

        public static string getShortDescription(string description, int length = 150)
        {
            description = getPlainTextfromHTML(description);

            description = (description.Length > length ? description.Substring(0, length - 2) + "..." : description);

            return description;
        }

        public static string getStoreImageURL_api(string url, DataHelper.ImgSize imageSize = ImgSize.Thumbnail)
        {
            string folder = "item_thumb";

            switch (imageSize)
            {
                case ImgSize.Thumbnail:
                    folder = "item_thumb";
                    break;
                case ImgSize.Large:
                    folder = "large";
                    break;
                case ImgSize.Slider:
                    folder = "item_feature_slider";
                    break;
                default:
                    break;
            }

            string imageURL = HttpContext.Current.Server.MapPath("~/content/items/" + folder + "/" + url);
            if (File.Exists(imageURL))
                imageURL = "content/items/" + folder + "/" + url;
            else
                imageURL = "";

            return imageURL;
        }

        public static string getStoreImageURL(string url, DataHelper.ImgSize imageSize = ImgSize.Thumbnail)
        {
            string folder = "item_thumb";

            switch (imageSize)
            {
                case ImgSize.Thumbnail:
                    folder = "item_thumb";
                    break;
                case ImgSize.Large:
                    folder = "large";
                    break;
                case ImgSize.Slider:
                    folder = "item_feature_slider";
                    break;
                default:
                    break;
            }

            string imageURL = HttpContext.Current.Server.MapPath("~/content/items/" + folder + "/" + url);
            if (File.Exists(imageURL))
                imageURL = "content/items/" + folder + "/" + url;
            else
                imageURL = "content/items/" + folder + "/notavailable.jpg";

            return imageURL;
        }

        public static string getStoreCategoryImageURL(string url, DataHelper.ImgSize imageSize = ImgSize.Thumbnail)
        {
            string folder = "thumb";

            switch (imageSize)
            {
                case ImgSize.Thumbnail:
                    folder = "thumb";
                    break;
                case ImgSize.Large:
                    folder = "large";
                    break;
                case ImgSize.Slider:
                    folder = "large";
                    break;
                default:
                    break;
            }

            string imageURL = HttpContext.Current.Server.MapPath("~/content/items/category/" + folder + "/" + url);
            if (File.Exists(imageURL))
                imageURL = "content/items/category/" + folder + "/" + url;
            else
                imageURL = "content/items/category/" + folder + "/notavailable.jpg";

            return imageURL;
        }

        public static string getNotificationsPicture(string url)
        {
            string imageURL = HttpContext.Current.Server.MapPath("~/content/notifications/" + url);
            if (File.Exists(imageURL))
                imageURL = "content/notifications/" + url;
            else
                imageURL = "content/notifications/not_found.png";

            return imageURL;
        }

        public static string getProfilePicture(string url)
        {
            string imageURL = HttpContext.Current.Server.MapPath("~/content/users/" + url);
            if (File.Exists(imageURL))
                imageURL = "content/users/" + url;
            else
                imageURL = "content/users/user.jpg";

            return imageURL;
        }
        
        public static string StripHtml(string Txt)
        {
            return Regex.Replace(Txt, "<(.|\\n)*?>", string.Empty);
        }

        // Resize a Bitmap  
        public static System.Drawing.Bitmap ResizeImage(System.Drawing.Bitmap image, int width, int height)
        {
            System.Drawing.Bitmap resizedImage = new System.Drawing.Bitmap(width, height);
            using (System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(resizedImage))
            {
                gfx.DrawImage(image, new System.Drawing.Rectangle(0, 0, width, height),
                    new System.Drawing.Rectangle(0, 0, image.Width, image.Height), System.Drawing.GraphicsUnit.Pixel);
            }
            return resizedImage;
        } 

        public enum ImgSize
        {
            Thumbnail,
            Large,
            Slider
        }
    }
}
