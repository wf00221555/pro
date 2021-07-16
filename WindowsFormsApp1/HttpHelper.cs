using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class HttpHelper
    {
        public static string httpGet(string url)
        {
            HttpWebRequest request = null;
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myStreamReader = null;
            myResponseStream.Close();
            myResponseStream.Dispose();
            myResponseStream = null;
            request.Abort();
            request = null;

            return retString;
        }

        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="dic">请求参数定义</param>
        /// <returns></returns>
        public static string httpGet(string url, Dictionary<string, string> dic)
        {
            if (url.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            string result = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(url);
            if (dic.Count > 0)
            {
               //builder.Append("?");
                int i = 0;
                foreach (var item in dic)
                {
                    
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
            }

            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);//验证服务器证书回调自动验证
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(builder.ToString());
            //添加参数
            NetworkCredential d = new NetworkCredential("hzadmin", "19820412");//添加此代码

            req.Credentials = d;

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            try
            {
                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                stream.Close();
            }
            return result;
            }

            private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受     
        }

        /// <summary>
        /// Post提交
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public static string httpPost(string Url, string postDataStr)
        {
            return httpPost(Url, postDataStr, "txt", "", "utf-8");
        }
        public static string httpPost(string Url)
        {
            return httpPost(Url, "", "txt", "", "utf-8");
        }

        public static string httpPost1(string Url)
        {
            return httpPost(Url, "", "html", "", "utf-8");
        }

        public static string httpPost2(string Url, string postDataStr)
        {
            return httpPost(Url, postDataStr, "html", "", "utf-8");
        }

        public static string httpPost3(string Url, string postDataStr)
        {
            return httpPost(Url, postDataStr, "textjson", "", "utf-8");
        }

        public static string httpPostByJson(string Url, string postDataStr)
        {
            return httpPost(Url, postDataStr, "json", "", "utf-8");
        }



        public static string httpPost(string Url, string postDataStr, string postType, string cacert, string chartset)  //post读取
        {
            //发送
            System.GC.Collect();//系统回收垃圾
            if (Url.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Timeout = 1000 * 20;
            request.Method = "POST";

            //以下加速请求
            //request.KeepAlive = false;//请求完关闭
            //request.ServicePoint.Expect100Continue = false;
            //request.ServicePoint.UseNagleAlgorithm = false;
            //request.ServicePoint.ConnectionLimit = 65500;
            //request.AllowWriteStreamBuffering = false; 
            request.Proxy = null;
            request.AllowAutoRedirect = true;
            //以上加速请求
            if (postType == "txt")
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            else if (postType == "json")
            {
                request.ContentType = "application/json";
            }
            else if (postType == "html")
            {
                request.ContentType = "text/html";
            }
            else if (postType == "textjson")
            {
                request.ContentType = "text/json";
            }

            if (cacert != "")
            {
                X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate(cacert, "");
                request.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;//设定验证回调(总是同意)
                request.ClientCertificates.Add(cert);//把证书添加进http请求中
            }

            try
            {
                byte[] payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);
                request.ContentLength = payload.Length;
                request.ServicePoint.Expect100Continue = false;
                request.GetRequestStream().Write(payload, 0, payload.Length);

                //响应接收
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                HttpWebResponse response;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(chartset));
                string retString = myStreamReader.ReadToEnd();
                response.Close();
                myStreamReader.Close();
                myResponseStream.Close();
                myResponseStream.Dispose();
                response = null;
                myStreamReader = null;
                myResponseStream = null;
                request.Abort();
                request = null;

                return retString;
            }
            catch (Exception ex)
            {
                request.Abort();
                request = null;
                Console.WriteLine(ex.Message);
                //Message ms = new Message("网络超时!");
                return "";
            }
        }

        /// <summary>
        /// Post方式请求接口
        /// </summary>
        /// <param name="action">请求的方法名</param>
        /// <param name="dic">请求发送的数据</param>
        /// <returns></returns>
        public static string HttpPost(string action, Dictionary<string, string> dic)
        {
            //此处换为自己的请求url
            string url = action;
            string result = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        public static string PostDataNew(string url, string infor)
        {
            string result = "";
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.KeepAlive = true;
                request.AllowAutoRedirect = false;
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] postdatabtyes = Encoding.UTF8.GetBytes(infor);
                request.ContentLength = postdatabtyes.Length;
                request.ServicePoint.Expect100Continue = false;//这个在Post的时候，一定要加上，如果服务器返回错误，他还会继续再去请求，不会使用之前的错误数据，做返回数据
                Stream requeststream = request.GetRequestStream();
                requeststream.Write(postdatabtyes, 0, postdatabtyes.Length);
                requeststream.Close();
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader sr2 = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    string respsr = sr2.ReadToEnd();
                    result = respsr;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public static string RequestData(string POSTURL, string PostData)
        {
            //发送请求的数据
            WebRequest myHttpWebRequest = WebRequest.Create(POSTURL);
            myHttpWebRequest.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(PostData);
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = byte1.Length;

            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            //发送成功后接收返回的XML信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            string lcHtml = string.Empty;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, enc);
            lcHtml = streamReader.ReadToEnd();
            return lcHtml;
        }

        #region 同步通过POST方式发送数据
        /// <summary>
        /// 通过POST方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">Post数据</param>
        /// <param name="cookie">Cookie容器</param>
        /// <returns></returns>
        public static string SendDataByPost(string Url, string postDataStr, ref CookieContainer cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            if (cookie.Count == 0)
            {
                request.CookieContainer = new CookieContainer();
                cookie = request.CookieContainer;
            }
            else
            {
                request.CookieContainer = cookie;
            }

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        #endregion

        public static string PostCookie(string url,string postData, Dictionary<string,string> headdic,out string cookiehead)
        {
            try
            {
                //data
                //string cookieStr = "51fd9f14fa7561b5";
                //string postData = string.Format("userid={0}&password={1}", "guest", "123456");
                byte[] data = Encoding.UTF8.GetBytes(postData);

                // Prepare web request...
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                //request.Referer = "https://www.xxx.com";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
                //request.Host = "www.xxx.com";
                foreach(KeyValuePair<string,string> kv in headdic)
                {
                    request.Headers.Add(kv.Key,kv.Value);
                }
                
                request.ContentLength = data.Length;
                Stream newStream = request.GetRequestStream();

                // Send the data.
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                // Get response
                HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
                cookiehead = myResponse.Headers.Get("Set-Cookie");
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                return content;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public static string GetCookie(string url,Dictionary<string, string> headdic)
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);    //创建一个请求示例 
            foreach (KeyValuePair<string, string> kv in headdic)
            {
                request.Headers[kv.Key] = kv.Value;
            }
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string str = streamReader.ReadToEnd();
            return str;
        }

    }
}
