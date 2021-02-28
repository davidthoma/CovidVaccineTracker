using System.Collections.Generic;
using Newtonsoft.Json;

namespace Covid.ConsoleApp.Stores.RiteAid
{
    public class RiteAidAvailabilityResponse
    {
        [JsonProperty("Data")]
        public Data Data { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("ErrCde")]
        public object ErrCde { get; set; }

        [JsonProperty("ErrMsg")]
        public object ErrMsg { get; set; }

        [JsonProperty("ErrMsgDtl")]
        public object ErrMsgDtl { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("slots")]
        public Dictionary<string, bool> Slots { get; set; }
    }
}
