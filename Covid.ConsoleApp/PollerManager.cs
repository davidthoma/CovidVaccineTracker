using System;
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

        internal PollerManager(Pushover notificationEngine)
        {
            this.notificationEngine = notificationEngine;
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
            var msgBody = $"*** {storeName} has vaccine appts available in {location.LocationName} {notes}";
            Console.WriteLine(msgBody);
            notificationEngine.Push("VACCINE CHECKER", msgBody);
        }
    }
}