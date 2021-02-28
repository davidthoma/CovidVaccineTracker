using Newtonsoft.Json;

namespace Covid.ConsoleApp.Stores.Walgreens
{
    public class WalgreensResponse
    {
        [JsonProperty("appointmentsAvailable")]
        public bool AppointmentsAvailable { get; set; }

        [JsonProperty("stateName")]
        public string StateName { get; set; }

        [JsonProperty("stateCode")]
        public string StateCode { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("radius")]
        public long Radius { get; set; }

        [JsonProperty("days")]
        public long Days { get; set; }
    }
}