using Newtonsoft.Json;

namespace Covid.ConsoleApp.Stores.Walgreens
{
    public class WalgreensRequest
    {
        [JsonProperty("serviceId")]
        public string ServiceId { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("appointmentAvailability")]
        public AppointmentAvailability AppointmentAvailability { get; set; }

        [JsonProperty("radius")]
        public long Radius { get; set; }
    }

    public class AppointmentAvailability
    {
        [JsonProperty("startDateTime")]
        public string StartDateTime { get; set; }
    }

    public class Position
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}