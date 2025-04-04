using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Xml;
using System.IO;
using WebTech.Common;

/// <summary>
/// Summary description for JsonHelper
/// </summary>
public static class JsonHelper
{
    public static void WriteJson(object obj, HttpContext context)
    {
        WriteJson(obj, context, "json");
    }

    public static void WriteJson(object obj, HttpContext context, string serviceType)
    {
        try
        {
            string stype = serviceType.Trim();
            if (stype != "xml")
                stype = "json";

            string json = JsonConvert.SerializeObject(obj);
            context.Response.Clear();

            if (stype == "json")
            {
                context.Response.ContentType = "application/json";
                context.Response.Write(json);
            }
            else if (stype == "xml")
            {
                XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(json, "Object");
                string xmlValue = GetXmlString(doc);

                context.Response.ContentType = "text/xml";
                context.Response.Write(xmlValue);
            }
        }
        catch (Exception ae)
        {
            WebTech.Common.Logs.WriteError("JsonHelper.cs", "WriteJson()", "Info", "Error Found " + ae.Message);
            context.Response.Write(ae.Message);
        }
    }

    public static void WriteXml(object obj, HttpContext context)
    {
        try
        {
            //context.Response.Clear();
            //var xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            //context.Response.ContentType = "text/xml";

            //xs.Serialize(context.Response.Output, obj);

            string json = JsonConvert.SerializeObject(obj);

            XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(json, "Object");
            string xmlValue = GetXmlString(doc);

            context.Response.Clear();
            context.Response.ContentType = "text/xml";
            context.Response.Write(xmlValue);
        }
        catch (Exception ae)
        {
            WebTech.Common.Logs.WriteError("JsonHelper.cs", "WriteXml()", "Info", "Error Found " + ae.Message);

            context.Response.Write(ae.Message);
        }
    }

    private static string GetXmlString(XmlDocument xmlDoc)
    {
        StringWriter sw = new StringWriter();
        XmlTextWriter xw = new XmlTextWriter(sw);
        xw.Formatting = System.Xml.Formatting.Indented;
        xmlDoc.WriteTo(xw);
        return sw.ToString();

    }

}
