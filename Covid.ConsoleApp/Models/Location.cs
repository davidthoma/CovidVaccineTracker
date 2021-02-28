namespace Covid.ConsoleApp.Models
{
    public struct Location
    {
        public int Id { get; set; }
        public string ZipCode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string LocationName { get; set; }
        public string State { get; set; }
        public string[] CvsLocations { get; set; }
    }
}