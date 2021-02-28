using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Covid.ConsoleApp.Stores.CVS
{
    public class CvsResponse
    {
        [JsonProperty("responsePayloadData")]
        public ResponsePayloadData ResponsePayloadData { get; set; }

        [JsonProperty("responseMetaData")]
        public ResponseMetaData ResponseMetaData { get; set; }
    }

    public class ResponseMetaData
    {
        [JsonProperty("statusDesc")]
        public string StatusDesc { get; set; }

        [JsonProperty("conversationId")]
        public string ConversationId { get; set; }

        [JsonProperty("refId")]
        public string RefId { get; set; }

        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }
    }

    public class ResponsePayloadData
    {
        [JsonProperty("currentTime")]
        public DateTimeOffset CurrentTime { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("isBookingCompleted")]
        public bool IsBookingCompleted { get; set; }
    }

    public class Data
    {
        [JsonProperty("PA")]
        public List<CityResult> Pa { get; set; }

        [JsonProperty("MD")]
        public List<CityResult> Md { get; set; }

        [JsonProperty("NJ")]
        public List<CityResult> Nj { get; set; }

        public List<CityResult> State
        {
            get
            {
                switch (Settings.State)
                {
                    case "PA": return Pa;
                    case "NJ": return Nj;
                    case "MD": return Md;
                    default: throw new Exception($"No configuration for state {Settings.State}");
                }
            }
        }
    }

    public class CityResult
    {
        [JsonProperty("totalAvailable")]
        public long TotalAvailable { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("pctAvailable")]
        public string PctAvailable { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}