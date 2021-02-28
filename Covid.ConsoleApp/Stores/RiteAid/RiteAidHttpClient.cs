using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Covid.ConsoleApp.Models;
using Newtonsoft.Json;

namespace Covid.ConsoleApp.Stores.RiteAid
{
    public class RiteAidHttpClient : IDisposable
    {
        private HttpClient client { get; set; }

        public RiteAidHttpClient()
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
            client.DefaultRequestHeaders.Add("Referer", "https://www.riteaid.com/pharmacy/apt-scheduler");
        }

        public List<string> GetStores(Location location)
        {
            var uri = $"https://www.riteaid.com/services/ext/v2/stores/getStores?address={location.ZipCode}&attrFilter=PREF-112&fetchMechanismVersion=2&radius=10";
            var response = client.GetAsync(uri).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var results = JsonConvert.DeserializeObject<RiteAidStoreResponse>(result);

            return results.Data.Stores.ToList().Select(item => item.StoreNumber.ToString()).ToList();
        }

        public bool GetAvailabilityResponse(string storeNumber)
        {
            var uri = $"https://www.riteaid.com/services/ext/v2/vaccine/checkSlots?storeNumber={storeNumber}";
            var response = client.GetAsync(uri).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var results = JsonConvert.DeserializeObject<RiteAidAvailabilityResponse>(result);

            return results.Data.Slots["1"];
        }

        public void Dispose()
        {
            client = null;
        }
    }
}