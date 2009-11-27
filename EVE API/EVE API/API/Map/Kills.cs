using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EVE_API
{
    public class Kills
    {
        public DateTime CurrentTime { get; set; }
        public DateTime CachedUntil { get; set; }
        public SolarSystem[] SolarSystemsList { get; set; }

        // Only systems with data are shown, assume 0s for the rest
        public Kills(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            CurrentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            CachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);

            List<SolarSystem> parsedSolarSystems = new List<SolarSystem>();
            foreach (XmlNode row in doc.SelectNodes("//rowset[@name='solarSystems']/row"))
            {
                parsedSolarSystems.Add(new SolarSystem(Convert.ToInt32(row.Attributes["solarSystemID"].InnerText), Convert.ToInt32(row.Attributes["shipKills"].InnerText),
                                            Convert.ToInt32(row.Attributes["factionKills"].InnerText), Convert.ToInt32(row.Attributes["podKills"].InnerText)));
            }
            SolarSystemsList = parsedSolarSystems.ToArray();
        }
    }
}
