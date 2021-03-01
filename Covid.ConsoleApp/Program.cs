using System;
using PushoverClient;

namespace Covid.ConsoleApp
{
    internal static class Program
    {
        private static string _pushoverConfigFileName = "configs/pushover.config.json";
        private static string _locationsFileName = "configs/locations.json";
        private static int _storeRadius = 50;

        private static void Main(string[] args)
        {
            ParseArgs(args);

            new Settings(_pushoverConfigFileName, _locationsFileName, _storeRadius).LoadSettings();

            var notificationEngine = new Pushover(Settings.PushoverConfig.AppKey)
                {DefaultUserGroupSendKey = Settings.PushoverConfig.UserGroupKey};

            var pollerManager = new PollerManager(notificationEngine);
            pollerManager.BeginPolling();

            Console.ReadLine();
        }

        private static void ParseArgs(string[] args)
        {
            if (args.Length == 0) return;

            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                switch (arg)
                {
                    case "--help":
                        PrintUsage();
                        Environment.Exit(0);
                        break;
                    case "--locations":
                        if (i + 1 == args.Length)
                        {
                            Console.WriteLine("Invalid arguments provided ");
                            PrintUsage();
                            Environment.Exit(1);
                        }
                        else
                        {
                            _locationsFileName = args[++i];
                        }
                        break;

                    case "--pushoverConfig":
                        if (i + 1 == args.Length)
                        {
                            Console.WriteLine("Invalid arguments provided ");
                            PrintUsage();
                            Environment.Exit(1);
                        }
                        else
                        {
                            _pushoverConfigFileName = args[++i];
                        }
                        break;

                    case "--radius":
                        if (i + 1 == args.Length || !int.TryParse(args[++i], out _storeRadius))
                        {
                            Console.WriteLine("Invalid arguments provided ");
                            PrintUsage();
                            Environment.Exit(1);
                        }
                        break;
                }
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: ");
            Console.WriteLine("--locations <file> : name and location of the locations config");
            Console.WriteLine("--pushoverConfig <file> : name and location of the Pushover config");
            Console.WriteLine("--radius <number> : store search radius in miles. Default is 50");
        }
    }
}