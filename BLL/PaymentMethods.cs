using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BLL;
using WebTech.Common;
using System.Web;

namespace BLL
{
    public class PaymentMethods
    {

        public long PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }

        public DataTable GetItemForDropDown()
        {
            return DBService.FetchTable("SELECT 0 PaymentMethodId, 'Select Payment Method' AS PaymentMethodName UNION ALL SELECT PaymentMethodId, PaymentMethodName FROM PaymentMethods WHERE ISNULL(IsActive,0) = 1");
        }

        public void GetPaymentMethod(HttpContext context)
        {
            try
            {
                DataTable dt = DBService.FetchTable("SELECT PaymentMethodId, PaymentMethodName FROM PaymentMethods WHERE ISNULL(IsActive,0) = 1");
                List<PaymentMethods> list = new List<PaymentMethods>();

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new PaymentMethods()
                    {
                        PaymentMethodId = DataHelper.longParse(row["PaymentMethodId"].ToString()),
                        PaymentMethodName = DataHelper.stringParse(row["PaymentMethodName"].ToString())
                    });
                }

                context.Response.Clear();
                context.Response.ContentType = "application/json; charset=utf-8";

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    data = list
                });

                context.Response.Write(json);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
