using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace UralSibSite.APIConnection
{
    public class HttpConnector
    {
        public static string BaseUrlString = "";
        private static string login;
        private static string password;
        private static NetworkCredential Credentials = new NetworkCredential(login, password);
        private static string encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(login + ":" + password));
        public HttpConnector()
        {

        }
        /// <summary>
        /// Создает запрос 
        /// </summary>
        /// <param name="method">GET обычно</param>
        /// <param name="url">url</param>
        /// <returns></returns>
        private WebRequest CreateRequest(string url, string method="GET")
        {
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Method = method;
            // optional
            webRequest.PreAuthenticate = true;
            webRequest.Credentials = Credentials;
            webRequest.Headers.Add("Authorization", "Basic " + encoded);
            return webRequest;
        } 
        /// <summary>
        /// Возвращает результат в виде строки с данными.
        /// Выдает Exception с инфой по статусу ошибки.
        /// </summary>
        /// <param name="request">уже созданный запрос</param>
        /// <returns></returns>
        private async Task<string> GetResponseResult(WebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                StreamReader responseReader = new StreamReader(responseStream);
                string result = await responseReader.ReadToEndAsync();
                return result;
            }
            else throw new HttpException(response.StatusDescription);          
        }
    }
}