using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;

namespace SGcombo.WebUtils
{


////////////////////////////////////////////////////////////////////////////
//	Copyright 2017 : Vladimir Novick    https://www.linkedin.com/in/vladimirnovick/  
//        
//             https://github.com/Vladimir-Novick/NET-CORE-2-Credentional-Web-Request
//
//    NO WARRANTIES ARE EXTENDED. USE AT YOUR OWN RISK. 
//
// To contact the author with suggestions or comments, use  :vlad.novick@gmail.com
//
////////////////////////////////////////////////////////////////////////////


    /// <summary>
    ///   Soap Reguests with save cookies
    /// </summary>
    public class SoapWebRequest
    {
        /// <summary>
        ///    Send request with waiting correct time
        /// </summary>
        /// <param name="sUrl">string - URL</param>
        /// <param name="sQuery"></param>
        /// <param name="wait"></param>
        /// <param name="WaitCount"></param>
        /// <param name="WaitTime"></param>
        /// <returns></returns>
        public string SendHttpReq(string sUrl, string sQuery,bool wait = true, int WaitCount = 20,int WaitTime = 10)
        {
            String response = SendHttpReq_(sUrl, sQuery);
            if (!wait) return response;
            int currentCount = 0;
            while (response.Length == 0 && currentCount < WaitCount)
            {
                Thread.Sleep(WaitTime * 1000);
                Console.WriteLine($"Wait {currentCount} -> {sUrl}");
                currentCount++;
                response = SendHttpReq_(sUrl, sQuery);
            }
            return response;
        }
        private string SendHttpReq_(string sUrl, string sQuery)
        {
            string sRes = string.Empty;
            // http request
            string lcUrl = sUrl;
            string lcHtml = string.Empty;

            try
            {

                //
                string url = sUrl;
                //url = sUrl;
                var req = (HttpWebRequest)WebRequest.Create(url);
                req.Timeout = 560000;
                req.ContentType = "application/x-www-form-urlencoded";
                req.Method = "POST";
                req.CookieContainer = CookieContainer;
                string sParams = string.Empty;

                sParams = sQuery;
                byte[] bytes = Encoding.UTF8.GetBytes(sParams);

                try
                { // send the Post
                    req.ContentLength = bytes.Length;   //Count bytes to send
                    using (Stream os = req.GetRequestStream())
                    {
                        os.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (WebException ex)
                {

                }


                try
                { // get the response

                    using (WebResponse webResponse = req.GetResponse())
                    {
                        if (webResponse == null)
                        {
                            return null;
                        }
                        StreamReader sr = new StreamReader(webResponse.GetResponseStream());
                        lcHtml = sr.ReadToEnd().Trim();
                    }
                }
                catch (WebException ex)
                {

                }
            } catch (Exception) { }
            return lcHtml;
        }


        private CookieContainer CookieContainer = new CookieContainer();

        private HttpWebRequest webRequest = null;

        /// <summary>
        ///   Send soap request with soap string 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="soapEnvelopeString"></param>
        /// <returns></returns>
        public string Execute(String url, string soapEnvelopeString)
        {
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(soapEnvelopeString);
            return Execute(url, soapEnvelopeXml);
        }

        /// <summary>
        ///   Send soap request with XmlDocument
        /// </summary>
        /// <param name="url"></param>
        /// <param name="soapEnvelopeXml"></param>
        /// <returns></returns>
        public String Execute(String url, XmlDocument soapEnvelopeXml)
        {

            String soapResult = null;

            CreateWebRequest(url);


            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = webRequest.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
            }
            return soapResult;
        }

        /// <summary>
        ///   Create a soap webrequest to [Url] with CookieContainer
        /// </summary>
        /// <param name="Url"></param>
        private void CreateWebRequest(string Url)
        {
            webRequest = (HttpWebRequest)WebRequest.Create(Url);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.CookieContainer = CookieContainer;
        }


    }
}
