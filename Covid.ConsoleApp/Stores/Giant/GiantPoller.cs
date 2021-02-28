using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Covid.ConsoleApp.Models;

namespace Covid.ConsoleApp.Stores.Giant
{
    class GiantPoller
    {
        private readonly List<Location> locations;
        internal event PollerManager.dgAvailabilityFound OnAvailabilityFound;

        internal GiantPoller(List<Location> locations)
        {
            this.locations = locations;
        }

        internal void PollGiantLoop()
        {
            while (true)
            {
                try
                {
                    foreach (var location in locations.Where(l=> !string.IsNullOrEmpty(l.ZipCode)))
                    {
                        PollGiant(location);
                        Console.WriteLine($"Giant polled {DateTime.Now} ({location.LocationName})");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Giant Poller Error: {ex.GetType()} ");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }

                Thread.Sleep(5 * 60 * 1000);
            }
        }

        private void PollGiant(Location location)
        {
            using var giantClient = new GiantHttpClient();
            var result = giantClient.GetAvailabilityResponse(location.ZipCode);

            if (result.IndexOf("There are no locations with available appointments") == -1
                && result.IndexOf("\r\n<!-- PharmScheduler.master -->\r\n<!DOCTYPE html>") == -1
                && result.IndexOf("There are currently no COVID-19 vaccine appointments available") == -1)
            {
                var msgBody = $"Giant has vaccine appts available in {location.ZipCode}. Go to this URL now!";
                msgBody += "https://giantsched.rxtouch.com/rbssched/program/covid19";
                OnAvailabilityFound?.Invoke("Giant", location, msgBody);
            }
        }
    }
}