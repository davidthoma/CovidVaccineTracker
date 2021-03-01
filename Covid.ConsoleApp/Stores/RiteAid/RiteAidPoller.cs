using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Covid.ConsoleApp.Models;

namespace Covid.ConsoleApp.Stores.RiteAid
{
    internal class RiteAidPoller
    {
        private readonly List<Location> locations;
        int pollCount = 0;

        internal event PollerManager.dgAvailabilityFound OnAvailabilityFound;

        internal RiteAidPoller(List<Location> locations)
        {
            this.locations = locations;
        }

        internal void PollRiteAidLoop()
        {
            while (true)
            {
                try
                {
                    foreach (var location in locations.Where(l => !string.IsNullOrEmpty(l.ZipCode)))
                    {
                        PollRiteAid(location);
                        Console.WriteLine($"RiteAid polled {DateTime.Now} ({location.LocationName}, {pollCount} stores)");
                        Thread.Sleep(2000);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"RiteAid Poller Error: {ex.GetType()} ");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }

                Thread.Sleep(1 * 60 * 1000);
            }
        }

        private void PollRiteAid(Location location)
        {
            using var client = new RiteAidHttpClient();
            pollCount = 0;
            var stores = client.GetStores(location);

            foreach (var store in stores)
            {
                var storeHasAvailability = client.GetAvailabilityResponse(store);
                pollCount++;

                if (storeHasAvailability)
                {
                    var msgBody = $"RiteAid has vaccine appts at store #{store} near zip code {location.ZipCode} ";
                    msgBody += "Go to this URL now! https://www.riteaid.com/pharmacy/apt-scheduler";
                    OnAvailabilityFound?.Invoke("RiteAid", location, msgBody);
                    return;
                }

                Thread.Sleep(1000);
            }
        }
    }
}