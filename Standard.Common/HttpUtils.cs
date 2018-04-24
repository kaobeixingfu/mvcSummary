using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Common
{

    /// <summary>
    /// HttpUtils 的摘要说明
    /// </summary>
    public class HttpUtils
    {
        public HttpUtils()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private static readonly string DefaultUserAgent =
            "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET";

        public static String DoGet(String url)
        {
            String html = "";
            StreamReader reader = null;
            HttpWebRequest webReqst = (HttpWebRequest)WebRequest.Create(url);
            webReqst.Method = "GET";
            webReqst.UserAgent = DefaultUserAgent;
            webReqst.KeepAlive = true;
            webReqst.Timeout = 30000;
            webReqst.ReadWriteTimeout = 30000;
            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)webReqst.GetResponse();
                if (webResponse.StatusCode == HttpStatusCode.OK && webResponse.ContentLength < 1024 * 1024)
                {
                    Stream stream = webResponse.GetResponseStream();
                    if (stream != null)
                    {
                        stream.ReadTimeout = 30000;
                        reader = new StreamReader(stream, Encoding.UTF8);
                        html = reader.ReadToEnd();
                    }
                }
                Console.WriteLine(html);
            }
            catch
            {

            }
            return html;
        }


        public static String DoPost(String url, String Content)
        {
            string html = "";
            StreamReader reader = null;
            HttpWebRequest webReqst = null;

            webReqst = WebRequest.Create(url) as HttpWebRequest;
            if (webReqst != null)
            {
                webReqst.Method = "POST";
                webReqst.UserAgent = DefaultUserAgent;
                webReqst.ContentType = "application/x-www-form-urlencoded";
                webReqst.ContentLength = Content.Length;
                webReqst.Timeout = 30000;
                webReqst.ReadWriteTimeout = 30000;

                try
                {
                    byte[] data = Encoding.Default.GetBytes(Content);
                    Stream stream = webReqst.GetRequestStream();
                    stream.Write(data, 0, data.Length);

                    HttpWebResponse webResponse = (HttpWebResponse)webReqst.GetResponse();
                    if (webResponse.StatusCode == HttpStatusCode.OK && webResponse.ContentLength < 1024 * 1024)
                    {
                        stream = webResponse.GetResponseStream();
                        if (stream != null)
                        {
                            stream.ReadTimeout = 30000;
                            reader = new StreamReader(stream, Encoding.UTF8);
                            html = reader.ReadToEnd();
                        }
                    }
                    Console.WriteLine(html);
                }

                catch (Exception exception)
                {

                }
            }
            return html;
        }
    }
}
