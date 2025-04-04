<%@ WebHandler Language="C#" Class="RequestHandler" %>

using System;
using System.Web;

public class RequestHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {

        if (context.Request.QueryString["action"] != null)
        {
            switch (WebTech.Common.DataHelper.stringParse(context.Request.QueryString["action"].ToString()))
            {
                case "verify":
                    new BLL.Accounts().VerifyAccount(context);
                    break;
                case "save_rate":
                    new BLL.Rate().SaveRates(context);
                    break;
                case "delete_rate":
                    new BLL.Rate().Delete(context);
                    break;
                case "get_orders":
                    new BLL.Order().GetOrders(context);
                    break;
                case "get_orders_detail":
                    new BLL.Order().GetOrdersDetails(context);
                    break;
                case "get_rates":
                    new BLL.Rate().GetRates(context);
                    break;
                case "get_rates_history":
                    new BLL.Rate().GetRatesHistory(context);
                    break;
                case "status_update":
                    new BLL.OrderStatus().UpdateStatus(context);
                    break;
                case "get_invoices":
                    new BLL.Order().GetInvoices(context);
                    break;
                case "payment_save":
                    new BLL.Order().SavePayment(context);
                    break;
                case "get_payments":
                    new BLL.Order().GetPayment(context);
                    break;
                case "get_dashboard":
                    new BLL.Dashboard().GetDashboard(context);
                    break;
                case "delete_bank":
                    new BLL.Banks().Delete(context);
                    break;
                case "delete_rider":
                    new BLL.Riders().Delete(context);
                    break;
                case "get_tanker_type":
                    new BLL.TankerType().GetTankerType(context);
                    break;
                case "get_location":
                    new BLL.Locations().GetLocation(context);
                    break;
                case "get_payment_method":
                    new BLL.PaymentMethods().GetPaymentMethod(context);
                    break;
                case "get_bank":
                    new BLL.Banks().GetBank(context);
                    break;
                case "get_rider":
                    new BLL.Riders().GetRider(context);
                    break;
                case "delete_faqs_category":
                    new BLL.FaqsCategory().Delete(context);
                    break;
                case "delete_faqs":
                    new BLL.Faqs().Delete(context);
                    break;
                case "delete_notification":
                    new BLL.Notifications().Delete(context);
                    break;
                case "reset_password":
                    new BLL.Accounts().ResetPassword(context);
                    break;
                case "delete_location":
                    new BLL.Locations().Delete(context);
                    break;
                case "delete_tanker_type":
                    new BLL.TankerType().Delete(context);
                    break;
               
            }
        }
        
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}