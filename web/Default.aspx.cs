using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTech.Common;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Login
            if (Request.Form["txtUserId"] != null)
            {
                string error_message = string.Empty;
                BLL.Authentication authentication = new BLL.Authentication();
                if (authentication.Login(Request.Form["txtUserId"].ToString(), Request.Form["txtPassword"].ToString(), ref error_message))
                {
                    WTSession.CreateSession(Sessions.LoginId, authentication.LoginId);
                    WTSession.CreateSession(Sessions.UserId, authentication.UserId);
                    WTSession.CreateSession(Sessions.UserName, authentication.UserName);

                    Response.Redirect("Dashboard.aspx", false);
                    return;
                }
                else
                {
                    divError.Visible = true;
                }
            }
            #endregion Login
        }
    }
}