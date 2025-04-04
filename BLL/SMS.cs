using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using WebTech.Common;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace BLL
{
    public class SMS
    {
        private String getSessionId()
        {
            String MSISDN = "923458246556";
            String PASSWORD = "Bailssoft*123456";

            String url = "https://telenorcsms.com.pk:27677/corporate_sms2/api/auth.jsp?msisdn=" + MSISDN + "&password=" + PASSWORD;
            return SendRequestSession(url);
        }

        public String SendSMS(String messageText, String to, String mask)
        {

            String sessionId = getSessionId();

            if (sessionId != null)
            {

                String url = "https://telenorcsms.com.pk:27677/corporate_sms2/api/sendsms.jsp?session_id=" + sessionId + "&text=" + messageText + "&to=" + to;

                if (mask != null)
                {
                    url = url += "&mask=" + mask;
                }

                return SendRequest(url);

            }
            else
            {
                return "999";
            }
            
        }

        private String SendRequest(String url)
        {
            String response = null;

            try
            {

                var client = new WebClient();
                response = client.DownloadString(url);

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(response);
                //string jsonText = JsonConvert.SerializeXmlNode(xmldoc);

                XmlNodeList responseType = xmldoc.GetElementsByTagName("response"); 
                XmlNodeList data = xmldoc.GetElementsByTagName("data");

                if (responseType.Equals("Error"))
                {
                    return null;
                }
                response = responseType[0].InnerText;
                return response;

            }
            catch (Exception ae)
            {
                Logs.WriteError("SMS.cs", "SendRequest()", "General", ae.Message);
            }

            return null;
        }

        private String SendRequestSession(String url)
        {
            String response = null;

            try
            {

                var client = new WebClient();
                response = client.DownloadString(url);

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(response);

                XmlNodeList responseType = xmldoc.GetElementsByTagName("response");
                XmlNodeList data = xmldoc.GetElementsByTagName("data");

                if (responseType.Equals("Error"))
                {
                    return null;
                }
                response = data[0].InnerText;
                return response;

            }
            catch (Exception ae)
            {
                Logs.WriteError("SMS.cs", "SendRequest()", "General", ae.Message);
            }

            return null;
        }

        public bool SaveSMS(string MobileNo, string Message)
        {
            try
            {
                SqlParameter[] param = {
                                           new SqlParameter() { ParameterName="@MobileNo", Value=MobileNo },
                                           new SqlParameter() { ParameterName="@Message", Value=Message },
                                       };

                ErrorResponse response = new ErrorResponse();
                return DBService.ExecuteSP("WB_SaveSMS", param, ref response);
            }
            catch (Exception ae)
            {
                Logs.WriteError("SMS.cs", "Save('" + MobileNo + "', " + Message + ")", "General", ae.Message);
                return false;
            }
        }
    }
}
