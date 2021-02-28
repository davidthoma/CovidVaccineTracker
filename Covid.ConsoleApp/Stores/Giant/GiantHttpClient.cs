using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace Covid.ConsoleApp.Stores.Giant
{
    public class GiantHttpClient : IDisposable
    {
        private HttpClient client { get; set; }
        HttpClientHandler handler;

        public GiantHttpClient()
        {
            handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            client = new HttpClient(handler);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Origin", "https://giantsched.rxtouch.com");
            client.DefaultRequestHeaders.Add("Referer", "https://giantsched.rxtouch.com/rbssched/program/Covid19/Patient/Advisory");
        }


        public string GetAvailabilityResponse(string zip)
        {
            var uri = $"https://giantsched.rxtouch.com/rbssched/program/Covid19/Patient/CheckZipCode";

            var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("zip", zip),
                    new KeyValuePair<string, string>("appointmentType", "5958"),
                    new KeyValuePair<string, string>("PatientInterfaceMode", " 0")
                });

            var response = client.PostAsync(uri, formContent).Result;

            Thread.Sleep(100);

            //Call a second time with cookies intact
            response = client.PostAsync(uri, formContent).Result;

            var result = response.Content.ReadAsStringAsync().Result;
            var results = result; //Looks like just a string

            return results;
        }



        public void Dispose()
        {
            client = null;
        }
    }
}