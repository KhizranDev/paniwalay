using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using HtmlAgilityPack;
using System.Threading;
using System.Drawing;


namespace WebTech.Common
{

    // ----------------------------------------------------------------------------------------
    /// <summary>
    /// Http
    /// 
    /// Common library of methods for HTTP communications.
    /// </summary>
    // ----------------------------------------------------------------------------------------
    public static class HTTPHandler
    {
        //static string userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.7 (KHTML, like Gecko) Chrome/16.0.912.63 Safari/535.7";
        static string userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.125 Safari/537.36";
        //static string userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.56 Safari/537.17";
        // private data members
        private static bool isCancellingPostFile = false;
        private static bool postFileWasCancelled = false;
        private static bool isCancellingDownloadFile = false;
        private static bool downloadFileWasCancelled = false;
        // public class members
        public class CancelPostFileException : Exception { };
        public const int timeOut = 300000;


        // ----------------------------------------------------------------------------------------
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="keyValues"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        // ----------------------------------------------------------------------------------------
        public static string Get(string url,
                                    CookieContainer cookieContainer,
                                    out HttpWebResponse response,
                                    out HttpWebRequest request,
                                    string referer,
                                    DataRow proxyRow,
                                    bool autoReDirect = true,
                                    WebHeaderCollection headers = null,
                                    int proxytimeout = timeOut)
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            // is the cookie container instanced? no, create one
            if (cookieContainer == null)
                cookieContainer = new CookieContainer();

            // encode the parameters
            string parameters = "";

            // create the http web request
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            httpWebRequest.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.3");
            httpWebRequest.Headers.Add("Accept-Language", "en-US,en;q=0.8");
            httpWebRequest.ProtocolVersion = HttpVersion.Version11;

            httpWebRequest.UserAgent = userAgent;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "GET";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.AllowAutoRedirect = autoReDirect;
            httpWebRequest.Timeout = proxytimeout;


            if (proxytimeout == 0)
                httpWebRequest.Timeout = timeOut;

            if (headers != null)
            {
                httpWebRequest.Headers.Add(headers);
            }

            if (referer != string.Empty)
                httpWebRequest.Referer = referer;

            httpWebRequest.Proxy = GetProxy(proxyRow);
            httpWebRequest.CookieContainer = cookieContainer;


            System.Net.WebResponse webResponse = null;
            // read the response
            int counter = 0;
            while (counter <= 3)
            {
                try
                {
                    webResponse = httpWebRequest.GetResponse();
                    counter = 4;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(3000);
                    counter++;

                    if (counter == 3)
                        throw ex;
                }
            }

            System.IO.StreamReader streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
            string result = streamReader.ReadToEnd().Trim();
            HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
            response = httpWebResponse;
            request = httpWebRequest;
            // return the result
            try
            {
                webResponse.Close();
            }
            catch (Exception ex)
            {
                //ProgramManager.LogException(ex);
            }
            return result;
        }

        public static string GetJSON(string url,
                                    CookieContainer cookieContainer,
                                    out HttpWebResponse response,
                                    out HttpWebRequest request,
                                    string referer,
                                    DataRow proxyRow,
                                    bool autoReDirect = true,
                                    WebHeaderCollection headers = null)
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            // is the cookie container instanced? no, create one
            if (cookieContainer == null)
                cookieContainer = new CookieContainer();

            // encode the parameters
            string parameters = "";

            // create the http web request
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            httpWebRequest.ProtocolVersion = HttpVersion.Version11;
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.UserAgent = userAgent;
            //httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "GET";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.AllowAutoRedirect = autoReDirect;
            //if (Global.ProxyTimeout > 0)
            //{
            //    httpWebRequest.Timeout = Global.ProxyTimeout;
            //}
            //else
            //{
            //    httpWebRequest.Timeout = timeOut;
            //}

            if (headers != null)
                httpWebRequest.Headers = headers;

            if (referer != string.Empty)
                httpWebRequest.Referer = referer;

            httpWebRequest.Proxy = GetProxy(proxyRow);

            System.Net.WebResponse webResponse = null;
            // read the response
            int counter = 0;
            while (counter <= 3)
            {
                try
                {
                    webResponse = httpWebRequest.GetResponse();
                    counter = 4;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(3000);
                    counter++;

                    if (counter == 3)
                        throw ex;
                }
            }

            System.IO.StreamReader streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
            string result = streamReader.ReadToEnd().Trim();
            HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
            response = httpWebResponse;
            request = httpWebRequest;
            // return the result
            try
            {
                webResponse.Close();
            }
            catch (Exception ex)
            {
                //ProgramManager.LogException(ex);
            }
            return result;
        }



        // ----------------------------------------------------------------------------------------
        /// <summary>
        /// PostUrlEncoded
        /// </summary>
        /// <param name="url"></param>
        /// <param name="keyValues"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        // ----------------------------------------------------------------------------------------
        public static string PostUrlEncoded(string url,
            NameValueCollection keyValues,
            CookieContainer cookieContainer,
            string paramParameters,
            out HttpWebResponse response,
            out HttpWebRequest request,
            string referer,
            DataRow proxyRow,
            bool redirect = false,
            string host = "",
            string headers = "",
            WebHeaderCollection headerCollection = null,
            string accept = "")
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            // is the cookie container instanced? no, create one
            if (cookieContainer == null)
                cookieContainer = new CookieContainer();
            // create the http web request
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));

            if (cookieContainer != null)
                httpWebRequest.CookieContainer = cookieContainer;

            httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
            httpWebRequest.ProtocolVersion = HttpVersion.Version11;
            httpWebRequest.UserAgent = userAgent;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.AllowAutoRedirect = redirect;
            httpWebRequest.Referer = referer;
            httpWebRequest.Timeout = timeOut;
            httpWebRequest.Headers.Set("Accept-Encoding", "gzip,deflate,sdch");
            httpWebRequest.Headers.Set("Accept-Language", "en-US,en;q=0.8");
            httpWebRequest.Accept = "*/*";

            if (accept != string.Empty)
                httpWebRequest.Accept = accept;

            if (headers != string.Empty)
                httpWebRequest.Headers.Add(headers);

            if (host != string.Empty)
                httpWebRequest.Host = host;

            if (headerCollection != null)
            {
                foreach (string header in headerCollection)
                {
                    switch (header)
                    {
                        case "Content-Type":
                            //httpWebRequest.ContentType = headerCollection[header];
                            break;

                        case "Accept":
                            //httpWebRequest.Accept = headerCollection[header];
                            break;

                        default:
                            httpWebRequest.Headers.Add(header, headerCollection[header]);
                            break;
                    }
                }
            }

            // encode the parameters
            string parameters = "";
            foreach (string key in keyValues.AllKeys)
            {
                parameters += (parameters.Length > 0 ? "&" : "") + key + "=" + System.Web.HttpUtility.UrlEncode(keyValues[key]);

                parameters.Trim(new char[] { '&' });
            }
            paramParameters = paramParameters.Trim(new char[] { '&' }) + "&" + parameters;
            // convert the parameters to a byte array
            byte[] parameterBytes = System.Text.Encoding.UTF8.GetBytes(paramParameters);
            httpWebRequest.ContentLength = parameterBytes.Length;

            httpWebRequest.Proxy = GetProxy(proxyRow);

            System.Net.WebResponse webResponse = null;
            // read the response
            try
            {
                // sent the request, write the parameters
                System.IO.Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(parameterBytes, 0, parameterBytes.Length);
                requestStream.Close();
                int counter = 0;
                while (counter <= 3)
                {
                    try
                    {
                        webResponse = httpWebRequest.GetResponse();
                        counter = 4;
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(3000);
                        counter++;

                        if (counter == 3)
                            throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                //ProgramManager.LogException(ex);
            }


            StreamReader streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
            string result = streamReader.ReadToEnd().Trim();
            HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
            response = httpWebResponse;
            request = httpWebRequest;
            // return the result
            try
            {
                webResponse.Close();
            }
            catch (Exception ex)
            {
                //ProgramManager.LogException(ex);
            }
            return result;
        }

        public static string PostandGetResponse(string url, string poststring)
        {
            string returnValue = "";

            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(url);

                System.Net.ServicePointManager.Expect100Continue = false;

                // Set the Method property of the request to POST.
                request.Method = "POST";
                // Create POST data and convert it to a byte array.
                string postData = poststring;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                returnValue = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(returnValue);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ae)
            {

            }

            return returnValue;
        }



        public static string PostJSON(string url,
           NameValueCollection keyValues,
           CookieContainer cookieContainer,
           string paramParameters,
           out HttpWebResponse response,
           out HttpWebRequest request,
           string referer,
           DataRow proxyRow,
           bool redirect = false,
           string host = "",
           string headers = "",
           WebHeaderCollection headerCollection = null,
           string accept = "")
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            // is the cookie container instanced? no, create one
            if (cookieContainer == null)
                cookieContainer = new CookieContainer();
            // create the http web request
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));

            if (cookieContainer != null)
                httpWebRequest.CookieContainer = cookieContainer;

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.ProtocolVersion = HttpVersion.Version11;
            httpWebRequest.UserAgent = userAgent;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.AllowAutoRedirect = redirect;
            httpWebRequest.Referer = referer;
            //if (Global.ProxyTimeout > 0)
            //{
            //    httpWebRequest.Timeout = Global.ProxyTimeout;
            //}
            //else
            //{
            //    httpWebRequest.Timeout = timeOut;
            //}
            httpWebRequest.Accept = "application/json, text/javascript, */*; q=0.01";

            if (accept != string.Empty)
                httpWebRequest.Accept = accept;

            if (headers != string.Empty)
                httpWebRequest.Headers.Add(headers);

            if (host != string.Empty)
                httpWebRequest.Host = host;

            if (headerCollection != null)
            {
                foreach (string header in headerCollection)
                    httpWebRequest.Headers.Add(header, headerCollection[header]);
            }

            // encode the parameters
            string parameters = "";
            foreach (string key in keyValues.AllKeys)
                parameters += (parameters.Length > 0 ? "&" : "") + key + "=" + System.Web.HttpUtility.UrlEncode(keyValues[key]);

            // convert the parameters to a byte array
            byte[] parameterBytes = System.Text.Encoding.UTF8.GetBytes(paramParameters);
            httpWebRequest.ContentLength = parameterBytes.Length;
            httpWebRequest.Proxy = GetProxy(proxyRow);

            System.Net.WebResponse webResponse = null;
            // read the response
            try
            {
                // sent the request, write the parameters
                System.IO.Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(parameterBytes, 0, parameterBytes.Length);
                requestStream.Close();
                int counter = 0;
                while (counter <= 3)
                {
                    try
                    {
                        webResponse = httpWebRequest.GetResponse();
                        counter = 4;
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(3000);
                        counter++;

                        if (counter == 3)
                            throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            StreamReader streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
            string result = streamReader.ReadToEnd().Trim();
            HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
            response = httpWebResponse;
            request = httpWebRequest;
            // return the result
            try
            {
                webResponse.Close();
            }
            catch (Exception ex)
            {
                //ProgramManager.LogException(ex);
            }
            return result;
        }

        /// <summary>
        /// Function to download Image from website
        /// </summary>
        /// <param name="_URL">URL address to download image</param>
        /// <returns>Image</returns>
        public static Image DownloadImage(string _URL,
                                             DataRow proxyRow,
                                            WebHeaderCollection header = null,
                                            CookieContainer cookieContainer = null,
                                            string _Referer = "http://www.google.com/")
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.HttpWebRequest _HttpWebRequest;
            System.Net.WebResponse _WebResponse = null;
            Image _tmpImage = null;

            try
            {
                // Open a connection
                _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);

                _HttpWebRequest.AllowWriteStreamBuffering = true;
                _HttpWebRequest.CookieContainer = cookieContainer;
                // You can also specify additional header values like the user agent or the referer: (Optional)
                _HttpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.7 (KHTML, like Gecko) Chrome/16.0.912.63 Safari/535.7";
                _HttpWebRequest.Referer = _Referer;

                // set timeout for 20 seconds (Optional)
                _HttpWebRequest.Timeout = timeOut;

                _HttpWebRequest.Headers.Add("Cache-Control", "max-age=0");
                _HttpWebRequest.Accept = "image/webp,*/*;q=0.8";
                _HttpWebRequest.Headers.Add("Accept-Language: en-US,en;q=0.8");
                _HttpWebRequest.Headers.Add("Accept-Encoding: gzip,deflate,sdch");

                if (header != null)
                {
                    foreach (string headerInfo in header)
                        _HttpWebRequest.Headers.Add(headerInfo, header[headerInfo]);
                }

                _HttpWebRequest.KeepAlive = true;
                // Request response:

                _HttpWebRequest.Proxy = GetProxy(proxyRow);

                try
                {
                    int counter = 0;
                    while (counter <= 3)
                    {
                        try
                        {
                            _WebResponse = _HttpWebRequest.GetResponse();
                            counter = 4;
                        }
                        catch (Exception ex)
                        {
                            Thread.Sleep(3000);
                            counter++;

                            if (counter == 3)
                                throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //try
                    //{
                    //    //Record IP
                    //    if (Global.RecordIPHits)
                    //    {
                    //        Global.InsertIPHit(proxyRow["IP"].ToString() + ":" + proxyRow["Port"].ToString(), _URL, "Failure");
                    //    }
                    //}
                    //catch (Exception e)
                    //{
                    //    ProgramManager.LogException(e);
                    //}
                    //throw ex;
                }

                // Open data stream:
                System.IO.Stream _WebStream = _WebResponse.GetResponseStream();

                // convert webstream to image
                _tmpImage = Image.FromStream(_WebStream);

                // Cleanup
                _WebResponse.Close();
                _WebResponse.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
                return null;
            }

            //try
            //{
            //    //Record IP
            //    if (Global.RecordIPHits)
            //    {
            //        Global.InsertIPHit(proxyRow["IP"].ToString() + ":" + proxyRow["Port"].ToString(), _URL, "Success");
            //    }
            //}
            //catch (Exception e)
            //{
            //    ProgramManager.LogException(e);
            //}

            try
            {
                _WebResponse.Close();
            }
            catch (Exception ex)
            {
                //ProgramManager.LogException(ex);
            }
            // >>> abdul @ 7th August 2013 : Dispose response object
            try
            {
                if (_WebResponse != null)
                {
                    IDisposable disposableResponse = _WebResponse as IDisposable;
                    disposableResponse.Dispose();
                }
            }
            catch (Exception ex)
            {
                //ProgramManager.LogException(ex);
            }
            // >>> abdul @ 7th August 2013 : Dispose response object
            return _tmpImage;
        }


        public static string GetAllCookies(HttpWebResponse response)
        {
            string cookieString = string.Empty;
            try
            {
                foreach (Cookie cookie in response.Cookies)
                    cookieString = cookieString + cookie.Name + "=" + cookie.Value + ";";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cookieString;
        }

        public static string GetAllCookies(HttpWebRequest request, CookieContainer cookieJar)
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            string cookieString = string.Empty;
            try
            {
                foreach (Cookie cookie in cookieJar.GetCookies(request.RequestUri))
                {
                    cookieString = cookieString + cookie.Name + "=" + cookie.Value + ";";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cookieString;
        }

        public static string GetAllHiddenInputPostString(string html, string formName)
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            string postString = string.Empty;
            var doc = new HtmlDocument();
            HtmlAgilityPack.HtmlNodeCollection nodes;
            doc.LoadHtml(html);

            if (formName != string.Empty)
            {
                nodes = doc.DocumentNode.SelectNodes(string.Format("//form[@name='{0}' or @id='{0}']", formName));
                nodes = nodes[0].SelectNodes("//input[@type='hidden' and @name and @value]");
            }
            else
            {
                nodes = doc.DocumentNode.SelectNodes("//input[@type='hidden']");
            }

            foreach (var node in nodes)
            {
                var inputName = node.Attributes["name"].Value;
                var inputValue = node.Attributes["value"].Value;
                if (!postString.Contains(inputName + "=" + inputValue))
                {
                    postString += string.Format("{0}={1}", inputName, inputValue) + "&";
                }
            }
            char[] trimChar = { '&' };
            return postString.Trim(trimChar);
        }

        public static string PostUrlEncoded(string url,
            NameValueCollection keyValues,
            CookieContainer cookieContainer,
            string paramParameters, DataRow proxyRow)
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            // is the cookie container instanced? no, create one
            if (cookieContainer == null)
                cookieContainer = new CookieContainer();

            // create the http web request
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));

            if (cookieContainer != null)
                httpWebRequest.CookieContainer = cookieContainer;

            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
            httpWebRequest.ProtocolVersion = HttpVersion.Version11;
            httpWebRequest.UserAgent = userAgent;

            // encode the parameters
            string parameters = "";
            foreach (string key in keyValues.AllKeys)
                parameters += (parameters.Length > 0 ? "&" : "") + key + "=" + System.Web.HttpUtility.UrlEncode(keyValues[key].Replace(",", ""));

            // convert the parameters to a byte array
            byte[] parameterBytes = System.Text.Encoding.UTF8.GetBytes(parameters);
            httpWebRequest.ContentLength = parameterBytes.Length;

            httpWebRequest.Proxy = GetProxy(proxyRow);

            // read the response


            int maxTry = 10;
            Boolean retry = true;
            int tryCount = 0;
            System.Net.WebResponse webResponse = null;
            System.IO.Stream requestStream = null;
            // read the response
            while (retry && tryCount < maxTry)
            {
                try
                {
                    // sent the request, write the parameters
                    requestStream = httpWebRequest.GetRequestStream();
                    requestStream.Write(parameterBytes, 0, parameterBytes.Length);
                    requestStream.Close();
                    int counter = 0;
                    while (counter <= 3)
                    {
                        try
                        {
                            webResponse = httpWebRequest.GetResponse();
                            counter = 4;
                        }
                        catch (Exception ex)
                        {
                            Thread.Sleep(3000);
                            counter++;

                            if (counter == 3)
                                throw ex;
                        }
                    }
                    retry = false;
                }
                catch (Exception ex)
                {
                    //ProxyRow = proxiesData.Rows[random.Next(0, proxiesData.Rows.Count - 1)];
                    //myProxy.Address = new Uri("http://" + ProxyRow["IP"].ToString() + ":" + ProxyRow["Port"].ToString());
                    ////myProxy.Credentials = new NetworkCredential(ProxyRow["Username"].ToString(), ProxyRow["Password"].ToString());
                    //httpWebRequest.Proxy = myProxy;
                    retry = true;
                    tryCount = tryCount + 1;
                }
            }
            StreamReader streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
            string result = streamReader.ReadToEnd().Trim();
            HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
            try
            {
                httpWebResponse.Close();
            }
            catch (Exception ex)
            {
                //ProgramManager.LogException(ex);
            }
            // return the result
            return result;
        }

        private static WebProxy GetProxy(DataRow proxyRow)
        {
            WebProxy myProxy = new WebProxy();
            try
            {
                if (proxyRow != null)
                {
                    myProxy.Address = new Uri("http://" + proxyRow["IP"].ToString() + ":" + proxyRow["Port"].ToString());
                    myProxy.Credentials = new NetworkCredential(proxyRow["Username"].ToString(), proxyRow["Password"].ToString());
                }
                else
                {
                    myProxy.Address = new Uri("http://127.0.0.1:8888");
                }
            }
            catch (Exception ae)
            {

            }

            return myProxy;
        }

    }
}
