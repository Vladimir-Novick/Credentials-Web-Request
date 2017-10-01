using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace SGcombo.WebUtils
{


////////////////////////////////////////////////////////////////////////////
//	Copyright 2017 : Vladimir Novick    https://www.linkedin.com/in/vladimirnovick/  
//        
//             https://github.com/Vladimir-Novick/NET-CORE-2-Credentional-Web-Reguest
//
//    NO WARRANTIES ARE EXTENDED. USE AT YOUR OWN RISK. 
//
// To contact the author with suggestions or comments, use  :vlad.novick@gmail.com
//
////////////////////////////////////////////////////////////////////////////


    /// <summary>
    ///   Soap Reguests with save cookies
    /// </summary>
    public class SoapWebReguest
    {


        public string SendHttpReq(string sUrl, string sQuery)
        {
            string sRes = string.Empty;
            // http request
            string lcUrl = sUrl;
            string lcHtml = string.Empty;
        


            //
            string url = sUrl;
            //url = sUrl;
            WebRequest req = WebRequest.Create(url);
            req.Timeout = 560000;
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";

            string sParams = string.Empty;

            sParams = sQuery;
            byte[] bytes = Encoding.UTF8.GetBytes(sParams);
            Stream os = null;
            try
            { // send the Post
                req.ContentLength = bytes.Length;   //Count bytes to send
                os = req.GetRequestStream();
                os.Write(bytes, 0, bytes.Length);         //Send it
            }
            catch (WebException ex)
            {

            }
            finally
            {
                if (os != null)
                {
                    os.Close();
                }
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
