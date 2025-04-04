using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data;

public partial class send_message : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Write(SendSMS());
        }
    }

    private string SendSMS()
    {
        SMS objSMS = new SMS();

        string MobileNo = "";
        string Message = "";
        long MessageId = 0;
        string ServiceResponse = "";

        try
        {

            ErrorResponse response = new ErrorResponse();

            DataTable dt = DBService.FetchTable("SELECT Id, MobileNo, Message FROM SendSMS", ref response);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    MessageId = DataHelper.longParse(row["Id"]);
                    MobileNo = DataHelper.stringParse(row["MobileNo"]);
                    Message = DataHelper.stringParse(row["Message"]);

                    ServiceResponse = objSMS.SendSMS(Message, MobileNo, "PaniWalay");

                    if (ServiceResponse == "OK")
                    {
                        DBService.ExecuteQuery("DELETE FROM SendSMS WHERE Id = '" + MessageId + "'", ref response);
                    }
                    else
                    {
                        Logs.WriteError("send_sms.aspx.cs", "SendSMS(" + MobileNo + ")", "SMSAPI", ServiceResponse);
                    }
                }
                return "Process Complete";
                
            }
            else
            {
                return "No SMS Found";
            }

        }
        catch (Exception ae)
        {
            Logs.WriteError("send_sms.aspx.cs", "SendSMS()", "SMSAPI", ServiceResponse);
            return ae.Message.ToString();
        }
    }
}