using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Windows.Forms;

using Newtonsoft.Json;

using TheMinx.Tatts.Data;
using TheMinx.Tatts.Data.Requests;
using TheMinx.Tatts.Data.Responses;
using TheMinx.Tatts.Data.Data.Racing;

namespace TheMinx
{
    class TattsUtils
    {
        public static string CallAPI(string uri, string json, Boolean secure, string reqtype)
        {
            string responseJSON = string.Empty;

            try
            {
                //-- Build Web Request

                HttpWebRequest webRequest = CreateAPIWebRequest(uri, secure, reqtype);

                if (!string.IsNullOrEmpty(json))
                {
                    // Write JSON to Request

                    byte[] webRequestContent = System.Text.UTF8Encoding.ASCII.GetBytes(json);

                    webRequest.ContentType = "application/json; charset=utf-8";
                    webRequest.ContentLength = webRequestContent.Length;

                    var webRequestStream = webRequest.GetRequestStream();

                    webRequestStream.Write(webRequestContent, 0, webRequestContent.Length);
                    webRequestStream.Close();
                }
                else
                {
                    webRequest.ContentLength = (webRequest.ContentLength <= 0) ? 0 : webRequest.ContentLength;
                }


                //-- Send Request to Server & Get Response

                HttpWebResponse webResponse = null;

                // Create SSL Delegate (Required for SSL connections)
                if (SSLDelegateLock.TryEnterWriteLock(1000))
                {
                    ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
                }

                SSLDelegateLock.ExitWriteLock();

                webResponse = (HttpWebResponse)webRequest.GetResponse();


                // Decode Response

                if (webResponse != null)
                {
                    if (webResponse.ContentLength > 0)
                    {
                        StreamReader webResponseStream = new StreamReader(webResponse.GetResponseStream(), System.Text.UTF8Encoding.ASCII);

                        responseJSON = webResponseStream.ReadToEnd();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Format("Unexpected error while performing API call: {0}", exception.Message));

                responseJSON = string.Empty;
            }

            return responseJSON;
        }

        public static string CallAPI2(string uri, string json, Boolean secure, string reqtype)
        {
            string responseJSON = string.Empty;

            try
            {
                //-- Build Web Request

                HttpWebRequest webRequest = CreateAPIWebRequest2(uri, secure, reqtype);

                if (!string.IsNullOrEmpty(json))
                {
                    // Write JSON to Request

                    byte[] webRequestContent = System.Text.UTF8Encoding.ASCII.GetBytes(json);

                    webRequest.ContentType = "application/json; charset=utf-8";
                    webRequest.ContentLength = webRequestContent.Length;

                    var webRequestStream = webRequest.GetRequestStream();

                    webRequestStream.Write(webRequestContent, 0, webRequestContent.Length);
                    webRequestStream.Close();
                }
                else
                {
                    webRequest.ContentLength = (webRequest.ContentLength <= 0) ? 0 : webRequest.ContentLength;
                }


                //-- Send Request to Server & Get Response

                HttpWebResponse webResponse = null;

                // Create SSL Delegate (Required for SSL connections)
                if (SSLDelegateLock.TryEnterWriteLock(1000))
                {
                    ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
                }

                SSLDelegateLock.ExitWriteLock();

                webResponse = (HttpWebResponse)webRequest.GetResponse();


                // Decode Response

                if (webResponse != null)
                {
                    if (webResponse.ContentLength > 0)
                    {
                        StreamReader webResponseStream = new StreamReader(webResponse.GetResponseStream(), System.Text.UTF8Encoding.ASCII);

                        responseJSON = webResponseStream.ReadToEnd();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Format("Unexpected error while performing API call: {0}", exception.Message));

                responseJSON = string.Empty;
            }

            return responseJSON;
        }

        
        public static HttpWebRequest CreateAPIWebRequest(string uri, Boolean secure, string reqtype)
        {
            HttpWebRequest request;
            if (secure)
            {
                request = (HttpWebRequest)WebRequest.Create("https://api.tatts.com/tbg/vmax/web/" + uri);
//                request = (HttpWebRequest)WebRequest.Create("https://api.tatts.com/sales/vmax/web/" + uri);
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create("http://api.tatts.com/svc/public/tbg/vmax/web/" + uri);
            }

            ServicePointManager.Expect100Continue = false;

            request.UserAgent = "The Minx";
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = true;
            request.Method = reqtype;

            return request;
        }

        public static HttpWebRequest CreateAPIWebRequest2(string uri, Boolean secure, string reqtype)
        {
            HttpWebRequest request;
            if (secure)
            {
                //                request = (HttpWebRequest)WebRequest.Create("https://api.tatts.com/tbg/vmax/web/" + uri);
                request = (HttpWebRequest)WebRequest.Create("https://api.tatts.com/sales/vmax/web/" + uri);
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create("http://api.tatts.com/svc/public/sales/vmax/web/" + uri);
            }

            ServicePointManager.Expect100Continue = false;

            request.UserAgent = "The Minx";
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = true;
            request.Method = reqtype;

            return request;
        }

        private static ReaderWriterLockSlim SSLDelegateLock = new ReaderWriterLockSlim();

        private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }
    }
}
