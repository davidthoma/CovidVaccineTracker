using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Covid.ConsoleApp.Models;

namespace Covid.ConsoleApp.Stores.Walgreens
{
    internal class WalgreensPoller
    {
        private readonly List<Location> locations;
        internal event PollerManager.dgAvailabilityFound OnAvailabilityFound;

        internal WalgreensPoller(List<Location> locations)
        {
            this.locations = locations;
        }

        internal void PollWalgreensLoop()
        {
            while (true)
            {
                try
                {
                    foreach (var location in locations.Where(l=> l.Longitude != 0))
                    {
                        PollWalgreens(location);
                        Console.WriteLine($"Walgreens polled {DateTime.Now} ({location.LocationName})");
                        Thread.Sleep(2000);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Walgreens Poller Error: {ex.GetType()} ");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }

                Thread.Sleep(5 * 60 * 1000);
            }
        }

        private void PollWalgreens(Location location)
        {
            using var walgreensClient = new WalgreensHttpClient();
            var request = new WalgreensRequest()
            {
                ServiceId = "99",
                Position = new Position() { Latitude = location.Latitude, Longitude = location.Longitude },
                AppointmentAvailability = new AppointmentAvailability() { StartDateTime = DateTime.Now.ToString("yyyy-MM-dd") },
                Radius = 10
            };

            var result = walgreensClient.GetAvailabilityResponse(request);

            if (result.AppointmentsAvailable)
            {
                var msgBody = $"Go to this URL now and enter {result.ZipCode} in the search. ";
                msgBody += "https://www.walgreens.com/findcare/vaccination/covid-19/location-screening";
                OnAvailabilityFound?.Invoke("Walgreens", location, msgBody);
            }
        }
    }



}