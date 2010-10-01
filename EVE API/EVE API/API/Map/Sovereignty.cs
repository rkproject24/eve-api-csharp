using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EVE_API
{
    public class Sovereignty
    {
        public DateTime CurrentTime { get; set; }
        public DateTime CachedUntil { get; set; }
        public DateTime DataTime { get; set; }
        public SovereigntyList[] theList { get; set; }

        public Sovereignty(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            CurrentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            CachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);
            DataTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("eveapi/result/dataTime").InnerText), DateTimeKind.Utc);

            List<SovereigntyList> parsedList = new List<SovereigntyList>();
            foreach (XmlNode row in doc.SelectNodes("//rowset[@name='solarSystems']/row"))
            {
                parsedList.Add(new SovereigntyList(Convert.ToInt32(row.Attributes["solarSystemID"].InnerText), row.Attributes["solarSystemName"].InnerText,
                                            Convert.ToInt32(row.Attributes["allianceID"].InnerText), Convert.ToInt32(row.Attributes["corporationID"].InnerText),
                                            Convert.ToInt32(row.Attributes["factionID"].InnerText)));
            }
            theList = parsedList.ToArray();
        }

        private class SovereigntyList
        {
            public int SolarSystemID { get; set; }
            public string SolarSystemName { get; set; }
            public int AllianceID { get; set; }
            public int CorporationID { get; set; }
            public int OccupyingFactionID { get; set; }

            public SovereigntyList(int systemID, string systemName, int allianceID, int corpID, int factionID)
            {
                SolarSystemID = systemID;
                SolarSystemName = systemName;
                AllianceID = allianceID;
                CorporationID = corpID;
                OccupyingFactionID = factionID;
            }
        }
    }
}
