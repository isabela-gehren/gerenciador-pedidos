using GEPED.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;

namespace GEPED.ServicePayment
{
    public class ApiGateway
    {
        private HttpMethod GetHttpMethod(string method)
        {
            return new HttpMethod(method);
        }

        public Dictionary<string, string> Headers { get; set; }

        public Response<TResult> InvokeApi<TParam, TResult>(string url, TParam json, string method)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                PreAuthenticate = true,
                UseDefaultCredentials = true
            };
            string reasonPhrase = "";
            using (var client = new HttpClient(handler))
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var httpRequestMessage = new HttpRequestMessage(GetHttpMethod(method), new Uri(url));
                httpRequestMessage.Version = HttpVersion.Version10;
                httpRequestMessage.Headers.Add("MerchantId", ConfigurationManager.AppSettings["MerchantId"]);
                httpRequestMessage.Headers.Add("MerchantKey", ConfigurationManager.AppSettings["MerchantKey"]);

                if (json != null)
                {
                    var request = Newtonsoft.Json.JsonConvert.SerializeObject(json);
                    httpRequestMessage.Content = new StringContent(request, Encoding.UTF8, "application/json");  // "Content-Type" Header.
                }
                var response = client.SendAsync(httpRequestMessage).Result;

                if (response.IsSuccessStatusCode)
                {
                    return new Response<TResult>(response.Content.ReadAsAsync<TResult>().Result);
                }
                else
                {
                    reasonPhrase = response.ReasonPhrase;
                    Response<TResult> result = new Response<TResult>();
                    result.StatusCode = StatusCode.InternalServerError;
                    result.Messages.Add(reasonPhrase);
                    // TO DO: Colocar o status code 
                    return result;

                }
            }
        }
    }
}
