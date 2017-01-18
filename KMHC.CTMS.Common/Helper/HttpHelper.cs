using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.Helper
{
    public class HttpHelper
    {
        public static string Post(string url,Dictionary<string,string> headers,string formData)
        {
            try {
                byte[] postData = Encoding.UTF8.GetBytes(formData);

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.AllowAutoRedirect = false;
                request.KeepAlive = true;
                request.ContentType = "text/xml";
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.7.6)";
                foreach (KeyValuePair<string, string> item in headers)
                {
                    request.Headers.Add(item.Key, item.Value);
                }

                request.ContentLength = postData.Length;
                Stream outputStream = request.GetRequestStream();
                outputStream.Write(postData, 0, postData.Length);
                outputStream.Close();

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                string result = reader.ReadToEnd();
                reader.Close();
                return result;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
