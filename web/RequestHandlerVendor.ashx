<%@ WebHandler Language="C#" Class="RequestHandlerVendor" %>

using System;
using System.Web;

public class RequestHandlerVendor : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {

        if (context.Request.QueryString["action"] != null)
        {
            switch (WebTech.Common.DataHelper.stringParse(context.Request.QueryString["action"].ToString()))
            {
                case "get_rates":
                    new BLL.VendorsRate().GetRates(context);
                    break;
                case "save_rate":
                    new BLL.VendorsRate().SaveRates(context);
                    break;
                case "get_vendor_rate":
                    new BLL.VendorsRate().GetRatesOrder(context);
                    break;
                case "save_vendor_order":
                    new BLL.VendorsOrder().SaveVendorOrder(context);
                    break;
                case "get_orders":
                    new BLL.VendorsOrder().GetOrders(context);
                    break;
                case "get_payments":
                    new BLL.VendorsOrder().GetVendorPayment(context);
                    break;
                case "get_vendor":
                    new BLL.Vendors().GetVendor(context);
                    break;
                case "get_remaining_amount":
                    new BLL.Vendors().GetVendorRemainingAmount(context);
                    break;
                case "payment_save":
                    new BLL.VendorsOrder().SavePayment(context);
                    break;
                case "get_statements":
                    new BLL.Vendors().GetVendorStatement(context);
                    break;
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}