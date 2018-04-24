using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Common
{
    // <summary>
    /// 网审平台HTTP请求
    /// </summary>

    public class HttpWSPTUtils
    {
        private static readonly string DefaultUserAgent =
        "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET";
        /// <summary>
        ///  HTTP POST请求：application/json;charset=utf-8
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public static string HttpPost(string postUrl, string paramData)
        {
            string ret = string.Empty;
            try
            {

                var request = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                request.Method = "POST";
                request.ContentType = "application/json;charset=utf-8";
                byte[] byteArray = Encoding.UTF8.GetBytes(paramData); //转化
                request.ContentLength = byteArray.Length;

                Stream newStream = request.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
                return ret;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// HTTP POST请求  ContentType：application/x-www-form-urlencoded
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public static string HttpPost2(string postUrl, string paramData)
        {
            string ret = string.Empty;
            try
            {

                var request = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] byteArray = Encoding.UTF8.GetBytes(paramData); //转化
                request.ContentLength = byteArray.Length;

                Stream newStream = request.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
                return ret;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
            catch (Exception ex)
            {
                throw ex;
            }
            return html;
        }
    }

}
