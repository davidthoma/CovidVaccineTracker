using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.ConsoleApp.Models;
using Covid.ConsoleApp.Stores.CVS;
using Covid.ConsoleApp.Stores.Giant;
using Covid.ConsoleApp.Stores.RiteAid;
using Covid.ConsoleApp.Stores.Walgreens;
using PushoverClient;

namespace Covid.ConsoleApp
{
    internal class PollerManager
    {
        private readonly Pushover notificationEngine;

        internal delegate void dgAvailabilityFound(string storeName, Location location, string notes);

        private static Dictionary<string, DateTime> mostRecentAvailability;

        internal PollerManager(Pushover notificationEngine)
        {
            this.notificationEngine = notificationEngine;

            mostRecentAvailability ??= new Dictionary<string, DateTime>();
        }

        internal void BeginPolling()
        {
            Console.WriteLine("Beginning Covid availability poller.");

            var riteAidPoller = new RiteAidPoller(Settings.Locations);
            riteAidPoller.OnAvailabilityFound += OnAvailabilityFound;
            var tskRiteAid = new Task(riteAidPoller.PollRiteAidLoop);
            tskRiteAid.Start();

            var giantPoller = new GiantPoller(Settings.Locations);
            giantPoller.OnAvailabilityFound += OnAvailabilityFound;
            var tskGiant = new Task(giantPoller.PollGiantLoop);
            tskGiant.Start();

            var walgreensPoller = new WalgreensPoller(Settings.Locations);
            walgreensPoller.OnAvailabilityFound += OnAvailabilityFound;
            var tskWalgreens = new Task(walgreensPoller.PollWalgreensLoop);
            tskWalgreens.Start();

            var cvsPoller = new CvsPoller();
            cvsPoller.OnAvailabilityFound += OnAvailabilityFound;
            var tskCvs = new Task(cvsPoller.PollCvsLoop);
            tskCvs.Start();
        }

        private void OnAvailabilityFound(string storeName, Location location, string notes)
        {
            if (IsDuplicateAppointment(storeName, location.LocationName)) return;

            var msgBody = $"*** {storeName} has vaccine appts available in {location.LocationName} {notes}";
            Console.WriteLine(msgBody);
            notificationEngine.Push("VACCINE CHECKER", msgBody);
        }

        private static bool IsDuplicateAppointment(string storeName, string locationName)
        {
            var key = storeName + "-" + locationName;
            if (mostRecentAvailability.ContainsKey(key) && mostRecentAvailability[key].AddMinutes(10) >= DateTime.Now)
            {
                Console.WriteLine($"Skipping duplicate appointment at {storeName} {locationName}");
                return true;
            }
            mostRecentAvailability[key] = DateTime.Now;
            return false;
        }
    }
}