using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using BLL;
using WebTech.Common;
using System.Net.Mail;
using System.Web.Configuration;

namespace BLL
{
    public static class EmailTemplate
    {
        public static void SignUpEmail(string user_id, string email, string activation_code)
        {
            string BaseURL = WebConfigurationManager.AppSettings["BaseURL"];

            activation_code = Encryption.Encrypt(activation_code);
            string activation_url = BaseURL + "email_activation.aspx?activation_code=" + activation_code;

            StringBuilder sbHtml = new StringBuilder();
            sbHtml.AppendLine(System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/email_templates/signup.html")));
            sbHtml = sbHtml.Replace("{UserId}", user_id);
            sbHtml = sbHtml.Replace("{ActivationUrl}", activation_url);

            string subject = "Signup - Activation Code";
            MailAddress fromAddress = new MailAddress("noman.khan330@gmail.com", "Paaniwalay.com");
            MailAddress toAddress = new MailAddress(email);

            Email.SendMail(fromAddress, toAddress, subject, sbHtml.ToString(), true);
            
        }

        public static void ResetPasswordEmail(string full_name, string email, string reset_code)
        {

            StringBuilder sbHtml = new StringBuilder();
            sbHtml.AppendLine(System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/email_templates/reset_password.html")));
            sbHtml = sbHtml.Replace("{FULLNAME}", full_name);
            sbHtml = sbHtml.Replace("{CODE}", reset_code);
            sbHtml = sbHtml.Replace("{YEAR}", DataHelper.stringParse(DateTime.Now.Year));

            string subject = "Paani Walay (Reset - Password Code)";
            Email.SaveEmail(email, subject, sbHtml.ToString());

        }

        public static void ResetPasswordEmail2(string user_id, string email, string reset_code)
        {

            string BaseURL = WebConfigurationManager.AppSettings["BaseURL"];

            //reset_code = Encryption.Encrypt(reset_code);
            string ResetURL = BaseURL + "email_reset_password.aspx?id=" + reset_code;

            StringBuilder sbHtml = new StringBuilder();
            sbHtml.AppendLine(System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/email_templates/reset_password.html")));
            sbHtml = sbHtml.Replace("{UserId}", user_id);
            sbHtml = sbHtml.Replace("{ActivationUrl}", ResetURL);

            string subject = "Reset - Password";
            MailAddress fromAddress = new MailAddress("noman.khan330@gmail.com", "Paaniwalay.com");
            MailAddress toAddress = new MailAddress(email);

            Email.SendMail(fromAddress, toAddress, subject, sbHtml.ToString(), true);

        }
    }
    
}
