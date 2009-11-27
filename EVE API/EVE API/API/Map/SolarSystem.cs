using System;

namespace EVE_API
{
    public class SolarSystem
    {
        public int SolarSystemID { get; set; }
        public string SolarSystemName { get; set; }
        public int OccupyingFactionID { get; set; }
        public string OccupyingFactionName { get; set; }
        public bool Contested { get; set; }
        public int ShipKills { get; set; }
        public int FactionKills { get; set; }
        public int PodKills { get; set; }
        public int ShipJumps { get; set; }

        public SolarSystem(int solarSystemID, int shipKills, int factionKills, int podKills)
        {
            SolarSystemID = solarSystemID;
            ShipKills = shipKills;
            FactionKills = factionKills;
            PodKills = podKills;
        }

        public SolarSystem(int solarSystemID, int shipJumps)
        {
            SolarSystemID = solarSystemID;
            ShipJumps = shipJumps;
        }

        public SolarSystem(int solarSystemID, string solarSystemName, int occupyingFactionID, string occupyingFactionName, bool contested)
        {
            SolarSystemID = solarSystemID;
            SolarSystemName = solarSystemName;
            OccupyingFactionID = occupyingFactionID;
            OccupyingFactionName = occupyingFactionName;
            Contested = contested;
        }
    }
}
