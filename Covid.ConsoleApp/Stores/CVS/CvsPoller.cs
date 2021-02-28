using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Covid.ConsoleApp.Models;

namespace Covid.ConsoleApp.Stores.CVS
{
    internal class CvsPoller
    {
        internal event PollerManager.dgAvailabilityFound OnAvailabilityFound;
        private readonly List<string> _cities = Settings.Locations.SelectMany(l => l.CvsLocations).Distinct().ToList();

        internal void PollCvsLoop()
        {
            while (true)
            {
                try
                {
                    PollCvs();
                    Console.WriteLine($"CVS polled {DateTime.Now} (PA)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"CVS Poller Error: {ex.GetType()} ");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }

                Thread.Sleep(5 * 60 * 1000);
            }
        }

        private void PollCvs()
        {
            using var cvsClient = new CvsHttpClient();
            var result = cvsClient.GetAvailabilityResponse();

            foreach (var cityName in _cities)
            {
                if (result.ResponsePayloadData.Data.State.Single(d => d.City == cityName).TotalAvailable > 0)
                {
                    var msgBody = $"CVS has vaccine appts available in {cityName}. Go to this URL now! ";
                    msgBody += "https://www.cvs.com/immunizations/covid-19-vaccine?icid=cvs-home-hero1-link2-coronavirus-vaccine";
                    OnAvailabilityFound?.Invoke("CVS", new Location() {LocationName = "Pennsylvania"}, msgBody);
                }
            }
        }
    }
}