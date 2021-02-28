using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Covid.ConsoleApp.Models;
using Newtonsoft.Json;

namespace Covid.ConsoleApp
{
    public class Settings
    {
        public static PushoverConfig PushoverConfig { get; private set; }

        public static List<Location> Locations { get; private set; }

        public static string State { get; private set; }

        private readonly string _pushoverConfigFileName;
        private readonly string _locationsFileName;

        public Settings(
            string pushoverConfigFileName,
            string locationsFileName)
        {
            _pushoverConfigFileName =
                pushoverConfigFileName ?? throw new ArgumentNullException(nameof(pushoverConfigFileName));
            _locationsFileName = locationsFileName ?? throw new ArgumentNullException(nameof(locationsFileName));
        }

        public void LoadSettings()
        {
            var locationsJson = File.ReadAllText(_locationsFileName);
            Locations = JsonConvert.DeserializeObject<List<Location>>(locationsJson);

            State = Locations.First().State;

            var pushoverConfigJson = File.ReadAllText(_pushoverConfigFileName);
            PushoverConfig = JsonConvert.DeserializeObject<PushoverConfig>(pushoverConfigJson);
        }
    }
}