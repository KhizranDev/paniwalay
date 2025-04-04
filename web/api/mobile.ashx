<%@ WebHandler Language="C#" Class="mobile" %>

using System;
using System.Web;
using BLL;

public class mobile : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        BLL.Mobile mobile = new BLL.Mobile() { context = context };
        mobile.Proceed();
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}