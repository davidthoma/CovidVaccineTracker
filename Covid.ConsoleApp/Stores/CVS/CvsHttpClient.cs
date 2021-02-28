using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Covid.ConsoleApp.Stores.CVS
{
    public class CvsHttpClient : IDisposable
    {
        private HttpClient client { get; set; }

        public CvsHttpClient()
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            client = new HttpClient(handler);

            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Referer", "https://www.cvs.com/immunizations/covid-19-vaccine?icid=cvs-home-hero1-link2-coronavirus-vaccine");
        }


        public CvsResponse GetAvailabilityResponse()
        {
            var uri = $"https://www.cvs.com/immunizations/covid-19-vaccine.vaccine-status.{Settings.State}.json?vaccineinf";
            var response = client.GetAsync(uri).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var results = JsonConvert.DeserializeObject<CvsResponse>(result);

            return results;
        }



        public void Dispose()
        {
            client = null;
        }
    }
}