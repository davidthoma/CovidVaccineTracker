using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Covid.ConsoleApp.Stores.Walgreens
{
    public class WalgreensHttpClient : IDisposable
    {
        private HttpClient client { get; set; }

        public WalgreensHttpClient()
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
            client.DefaultRequestHeaders.Add("Origin", "https://www.walgreens.com");
            client.DefaultRequestHeaders.Add("Referer", "https://www.walgreens.com/findcare/vaccination/covid-19/location-screening");
        }


        public WalgreensResponse GetAvailabilityResponse(WalgreensRequest request)
        {
            var uri = $"https://www.walgreens.com/hcschedulersvc/svc/v1/immunizationLocations/availability";
            var body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = client.PostAsync(uri, body).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var results = JsonConvert.DeserializeObject<WalgreensResponse>(result);

            return results;
        }



        public void Dispose()
        {
            client = null;
        }
    }
}