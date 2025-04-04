using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data;

public partial class send_email : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Write(SendEmail());
        }
    }

    private string SendEmail()
    {
        string ToEmail = "";
        string Subject = "";
        string Message = "";
        long EmailId = 0;
        bool IsSend = false;

        try
        {
            ErrorResponse response = new ErrorResponse();
            DataTable dt = DBService.FetchTable("SELECT Id, ToEmail, Subject, Message FROM SendEmail", ref response);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EmailId = DataHelper.longParse(row["Id"]);
                    ToEmail = DataHelper.stringParse(row["ToEmail"]);
                    Subject = DataHelper.stringParse(row["Subject"]);
                    Message = DataHelper.stringParse(row["Message"]);

                    IsSend = Email.SendMail("admin@paniwalay.com", ToEmail, Subject, Message, true);

                    if (IsSend)
                    {
                        DBService.ExecuteQuery("DELETE FROM SendEmail WHERE Id = '" + EmailId + "'", ref response);
                    }
                    else
                    {
                        Logs.WriteError("send_email.aspx.cs", "SendEmail(" + ToEmail + ")", "EMAIL", DataHelper.stringParse(IsSend));
                    }
                }
            }
            else
            {
                return "No Email Found";
            }
            
            return "Process Complete";
        }
        catch (Exception ae)
        {
            Logs.WriteError("send_email.aspx.cs", "SendEmail(" + ToEmail + ")", "EMAIL", DataHelper.stringParse(IsSend));
            return ae.Message.ToString();
        }
    }
}