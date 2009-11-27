using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EVE_API
{
    public class FacWarSystems
    {
        public DateTime CurrentTime { get; set; }
        public DateTime CachedUntil { get; set; }
        public SolarSystem[] SolarSystemsList { get; set; }
        // Returns a list of contestable solarsystems and the NPC faction currently occupying them.
        // It should be noted that this file only returns a non-zero ID if the occupying faction is not the sovereign faction. 
        public FacWarSystems(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            CurrentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            CachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);

            List<SolarSystem> parsedSolarSystems = new List<SolarSystem>();
            foreach (XmlNode row in doc.SelectNodes("//rowset[@name='solarSystems']/row"))
            {
                parsedSolarSystems.Add(new SolarSystem(Convert.ToInt32(row.Attributes["solarSystemID"].InnerText), row.Attributes["solarSystemName"].InnerText,
                                            Convert.ToInt32(row.Attributes["occupyingFactionID"].InnerText), row.Attributes["occupyingFactionName"].InnerText,
                                            Convert.ToBoolean(row.Attributes["contested"].InnerText)));
            }
            SolarSystemsList = parsedSolarSystems.ToArray();
        }

    }
}
