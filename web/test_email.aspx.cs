using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using BLL;
using WebTech.Common;

public partial class test_email : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SendSignUpEmail(1,"noman","token");
    }

    private void SendSignUpEmail(long account_id, string userid, string activation_code)
    {
        try
        {
            #region Email Message

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" <table width='100%' height='100%' style='min-width:348px' border='0' cellspacing='0' cellpadding='0'>");
            sb.AppendLine("     <tbody>");
            sb.AppendLine("         <tr height='32px'></tr>");
            sb.AppendLine("         <tr align='center'>");
            sb.AppendLine("             <td width='32px'></td>");
            sb.AppendLine("             <td>");
            sb.AppendLine("                 <table border='0' cellspacing='0' cellpadding='0' style='max-width:600px'>");
            sb.AppendLine("                     <tbody>");
            sb.AppendLine("                         <tr>");
            sb.AppendLine("                             <td>");
            sb.AppendLine("                                 <table width='100%' border='0' cellspacing='0' cellpadding='0' style='display:none;'>");
            sb.AppendLine("                                     <tbody>");
            sb.AppendLine("                                         <tr>");
            sb.AppendLine("                                             <td align='left'><img src='http://www.myshiftworkapp.com/images/logo.png' style='display:block; width: 180px;' class='CToWUd'></td>");
            sb.AppendLine("                                             <td align='right'></td>");
            sb.AppendLine("                                         </tr>");
            sb.AppendLine("                                     </tbody>");
            sb.AppendLine("                                 </table>");
            sb.AppendLine("                             </td>");
            sb.AppendLine("                         </tr>");
            sb.AppendLine("                         <tr height='16'></tr>");
            sb.AppendLine("                         <tr>");
            sb.AppendLine("                             <td>");
            sb.AppendLine("                                 <table bgcolor='#2C97DE' width='100%' border='0' cellspacing='0' cellpadding='0' ");
            sb.AppendLine("                                     style='min-width:332px;max-width:600px;border:1px solid #e0e0e0;border-bottom:0;border-top-left-radius:3px;border-top-right-radius:3px'>");
            sb.AppendLine("                                     <tbody>");
            sb.AppendLine("                                         <tr>");
            sb.AppendLine("                                             <td height='25px' colspan='3'></td>");
            sb.AppendLine("                                         </tr>");
            sb.AppendLine("                                         <tr>");
            sb.AppendLine("                                             <td width='32px'></td>");
            sb.AppendLine("                                             <td style='font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:24px;color:#ffffff;line-height:1.25'>Forgot <span class='il'>Password</span></td>");
            sb.AppendLine("                                             <td width='32px'></td>");
            sb.AppendLine("                                         </tr>");
            sb.AppendLine("                                         <tr>");
            sb.AppendLine("                                             <td height='18px' colspan='3'></td>");
            sb.AppendLine("                                         </tr>");
            sb.AppendLine("                                     </tbody>");
            sb.AppendLine("                                 </table>");
            sb.AppendLine("                             </td>");
            sb.AppendLine("                         </tr>");
            sb.AppendLine("                         <tr>");
            sb.AppendLine("                             <td>");
            sb.AppendLine("                                 <table bgcolor='#FAFAFA' width='100%' border='0' cellspacing='0' cellpadding='0' style='min-width:332px;max-width:600px;border:1px solid #f0f0f0;border-bottom:1px solid #c0c0c0;border-top:0;border-bottom-left-radius:3px;border-bottom-right-radius:3px'>");
            sb.AppendLine("                                     <tbody>");
            sb.AppendLine("                                         <tr height='16px'>");
            sb.AppendLine("                                             <td width='32px' rowspan='3'></td>");
            sb.AppendLine("                                             <td></td>");
            sb.AppendLine("                                             <td width='32px' rowspan='3'></td>");
            sb.AppendLine("                                         </tr>");
            sb.AppendLine("                                         <tr>");
            sb.AppendLine("                                             <td>");
            sb.AppendLine("                                                 <table style='min-width:300px' border='0' cellspacing='0' cellpadding='0'>");
            sb.AppendLine("                                                     <tbody>");
            sb.AppendLine("                                                         <tr>");
            sb.AppendLine("                                                             <td style='font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5'>Hi,</td>");
            sb.AppendLine("                                                         </tr>");
            sb.AppendLine("                                                         <tr>");
            sb.AppendLine("                                                             <td style='font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5'>Request has been received for reset your MyShiftWorkApp account <a href='mailto:" + userid + "' target='_blank'>" + userid + "</a>  <span class='il'>password</span>, please use below code for reset your password.<br/><br/>Access Code : <b>token</b><br><br><b>Don't recognize this activity?</b><br>ignore this email we will expire this token after 1 hour.</td>");
            sb.AppendLine("                                                         </tr>");
            sb.AppendLine("                                                         <tr height='32px'></tr>");
            sb.AppendLine("                                                         <tr>");
            sb.AppendLine("                                                             <td style='font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5'>Best,<br>The My Shift Work App Accounts team</td>");
            sb.AppendLine("                                                         </tr>");
            sb.AppendLine("                                                         <tr height='16px'></tr>");
            sb.AppendLine("                                                         <tr>");
            sb.AppendLine("                                                             <td>");
            sb.AppendLine("                                                                 <table style='font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:12px;color:#b9b9b9;line-height:1.5'>");
            sb.AppendLine("                                                                     <tbody>");
            sb.AppendLine("                                                                         <tr>");
            sb.AppendLine("                                                                             <td>This email can't receive replies.</td>");
            sb.AppendLine("                                                                         </tr>");
            sb.AppendLine("                                                                     </tbody>");
            sb.AppendLine("                                                                 </table>");
            sb.AppendLine("                                                             </td>");
            sb.AppendLine("                                                         </tr>");
            sb.AppendLine("                                                     </tbody>");
            sb.AppendLine("                                                 </table>");
            sb.AppendLine("                                             </td>");
            sb.AppendLine("                                         </tr>");
            sb.AppendLine("                                         <tr height='32px'></tr>");
            sb.AppendLine("                                     </tbody>");
            sb.AppendLine("                                 </table>");
            sb.AppendLine("                             </td>");
            sb.AppendLine("                             </tr>");
            sb.AppendLine("                         <tr height='16'></tr>");
            sb.AppendLine("                         <tr>");
            sb.AppendLine("                             <td style='max-width:600px;font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:10px;color:#bcbcbc;line-height:1.5'></td>");
            sb.AppendLine("                         </tr>");
            sb.AppendLine("                         <tr>");
            sb.AppendLine("                             <td>");
            sb.AppendLine("                                 <table style='font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:10px;color:#666666;line-height:18px;padding-bottom:10px'>");
            sb.AppendLine("                                     <tbody>");
            sb.AppendLine("                                         <tr>");
            sb.AppendLine("                                             <td>You received this mandatory email service announcement to update you about important changes to your myShiftWorkApp account.</td>");
            sb.AppendLine("                                         </tr>");
            sb.AppendLine("                                         <tr>");
            sb.AppendLine("                                             <td><div style='text-align:left'>© " + DateTime.Now.Year + " myShiftWorkApp</div></td>");
            sb.AppendLine("                                         </tr>");
            sb.AppendLine("                                     </tbody>");
            sb.AppendLine("                                 </table>");
            sb.AppendLine("                             </td>");
            sb.AppendLine("                         </tr>");
            sb.AppendLine("                     </tbody>");
            sb.AppendLine("                 </table>");
            sb.AppendLine("             </td>");
            sb.AppendLine("             <td width='32px'></td>");
            sb.AppendLine("         </tr>");
            sb.AppendLine("         <tr height='32px'></tr>");
            sb.AppendLine("     </tbody>");
            sb.AppendLine(" </table>");

            #endregion Email Message

            //WTMail.SendMail("accounts@myshiftworkapp.com", userid, "Password Reset Request", sb.ToString(), true);

            Response.Write(sb.ToString());
        }
        catch (Exception ae)
        {
            //Logs.WriteError("General", ae.Message);
        }
    }
}