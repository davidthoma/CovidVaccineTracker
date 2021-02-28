using System.Collections.Generic;
using Newtonsoft.Json;

namespace Covid.ConsoleApp.Stores.RiteAid
{
    public class RiteAidStoreResponse
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
        [JsonProperty("stores")]
        public List<Store> Stores { get; set; }

        [JsonProperty("globalZipCode")]
        public object GlobalZipCode { get; set; }

        [JsonProperty("resolvedAddress")]
        public ResolvedAddress ResolvedAddress { get; set; }

        [JsonProperty("ambiguousAddresses")]
        public object AmbiguousAddresses { get; set; }

        [JsonProperty("warnings")]
        public List<object> Warnings { get; set; }
    }

    public class ResolvedAddress
    {
        [JsonProperty("addressLine")]
        public object AddressLine { get; set; }

        [JsonProperty("adminDistrict")]
        public string AdminDistrict { get; set; }

        [JsonProperty("altitude")]
        public long Altitude { get; set; }

        [JsonProperty("confidence")]
        public string Confidence { get; set; }

        [JsonProperty("calculationMethod")]
        public string CalculationMethod { get; set; }

        [JsonProperty("countryRegion")]
        public string CountryRegion { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("formattedAddress")]
        public string FormattedAddress { get; set; }

        [JsonProperty("geocodeBestView")]
        public GeocodeBestView GeocodeBestView { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("postalCode")]
        public long PostalCode { get; set; }

        [JsonProperty("postalTown")]
        public string PostalTown { get; set; }
    }

    public class GeocodeBestView
    {
        [JsonProperty("northEastElements")]
        public StElements NorthEastElements { get; set; }

        [JsonProperty("southWestElements")]
        public StElements SouthWestElements { get; set; }
    }

    public class StElements
    {
        [JsonProperty("altitude")]
        public long Altitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    public class Store
    {
        [JsonProperty("storeNumber")]
        public long StoreNumber { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }

        [JsonProperty("fullZipCode")]
        public string FullZipCode { get; set; }

        [JsonProperty("fullPhone")]
        public string FullPhone { get; set; }

        [JsonProperty("locationDescription")]
        public string LocationDescription { get; set; }

        [JsonProperty("storeHoursMonday")]
        public string StoreHoursMonday { get; set; }

        [JsonProperty("storeHoursTuesday")]
        public string StoreHoursTuesday { get; set; }

        [JsonProperty("storeHoursWednesday")]
        public string StoreHoursWednesday { get; set; }

        [JsonProperty("storeHoursThursday")]
        public string StoreHoursThursday { get; set; }

        [JsonProperty("storeHoursFriday")]
        public string StoreHoursFriday { get; set; }

        [JsonProperty("storeHoursSaturday")]
        public string StoreHoursSaturday { get; set; }

        [JsonProperty("storeHoursSunday")]
        public string StoreHoursSunday { get; set; }

        [JsonProperty("rxHrsMon")]
        public string RxHrsMon { get; set; }

        [JsonProperty("rxHrsTue")]
        public string RxHrsTue { get; set; }

        [JsonProperty("rxHrsWed")]
        public string RxHrsWed { get; set; }

        [JsonProperty("rxHrsThu")]
        public string RxHrsThu { get; set; }

        [JsonProperty("rxHrsFri")]
        public string RxHrsFri { get; set; }

        [JsonProperty("rxHrsSat")]
        public string RxHrsSat { get; set; }

        [JsonProperty("rxHrsSun")]
        public string RxHrsSun { get; set; }

        [JsonProperty("storeType")]
        public string StoreType { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("milesFromCenter")]
        public double MilesFromCenter { get; set; }

        [JsonProperty("specialServiceKeys")]
        public List<string> SpecialServiceKeys { get; set; }

        [JsonProperty("event")]
        public object Event { get; set; }

        [JsonProperty("holidayHours")]
        public List<object> HolidayHours { get; set; }

        [JsonProperty("pickupDateAndTimes")]
        public PickupDateAndTimes PickupDateAndTimes { get; set; }
    }

    public class PickupDateAndTimes
    {
        [JsonProperty("regularHours")]
        public List<string> RegularHours { get; set; }

        [JsonProperty("specialHours")]
        public Dictionary<string, string> SpecialHours { get; set; }

        [JsonProperty("defaultTime")]
        public string DefaultTime { get; set; }

        [JsonProperty("earliest")]
        public string Earliest { get; set; }
    }
}


