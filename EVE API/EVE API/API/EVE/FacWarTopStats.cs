using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace EVE_API
{
    public class FacWarTopStats
    {
        public Stats[] CorporationStats { get; set; }
        public Stats[] CharacterStats { get; set; }
        public Stats[] FactionStats { get; set; }
        public DateTime CurrentTime { get; set; }
        public DateTime CachedUntil { get; set; }

        public FacWarTopStats(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            CurrentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            CachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);

            List<Stats> parsedStats = new List<Stats>();
            foreach (XmlNode group in doc.SelectNodes("//rowset[@name='skillGroups']/row"))
            {
                parsedStats.Add(new Stats(group.Attributes["groupName"].InnerText, Convert.ToInt32(group.Attributes["groupID"].InnerText)));
            }
            CorporationStats = parsedStats.ToArray();
        }


        public struct Stats
        {
            public KillStat KillsYesterday { get; set; }
            public KillStat KillsLastWeek { get; set; }
            public KillStat KillsTotal { get; set; }
            public VictoryStat VictoryPointsYesterday { get; set; }
            public VictoryStat VictoryPointsLastWeek { get; set; }
            public VictoryStat VictoryPointsTotal { get; set; }

            public struct VictoryStat
            {
                public int ID { get; set; }
                public string Name { get; set; }
                public int VictoryPoints { get; set; }
            }

            public struct KillStat
            {
                public int ID { get; set; }
                public string Name { get; set; }
                public int Kills { get; set; }
            }
        }
    }
}
