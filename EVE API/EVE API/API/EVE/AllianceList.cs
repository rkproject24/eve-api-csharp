using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EVE_API
{
    public class AllianceList
    {
        public DateTime currentTime { get; set; }
        public DateTime cachedUntil { get; set; }
        public Alliance[] theAllianceList { get; set; }

        public AllianceList(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            currentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            cachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);

            List<Alliance> parsedAlliances = new List<Alliance>();
            foreach (XmlNode row in doc.SelectNodes("//rowset[@name='alliances']/row"))
            {
                string name = row.Attributes["name"].InnerText;
                string shortName = row.Attributes["shortName"].InnerText;
                int id = Convert.ToInt32(row.Attributes["allianceID"].InnerText);
                int executorID = Convert.ToInt32(row.Attributes["executorCorpID"].InnerText);
                DateTime startDate = DateTime.Parse(row.Attributes["startDate"].InnerText);
                int memberCount = Convert.ToInt32(row.Attributes["memberCount"].InnerText);

                Alliance parsedAlliance = new Alliance(name, shortName, id, executorID, memberCount, startDate);

                List<Corporation> parsedCorps = new List<Corporation>();
                foreach (XmlNode row2 in doc.SelectNodes("//rowset[@name='memberCorporations']/row"))
                {
                    parsedCorps.Add(new Corporation(Convert.ToInt32(row.Attributes["corporationID"].InnerText), DateTime.Parse(row.Attributes["startDate"].InnerText)));
                }

                parsedAlliance.CorpList = parsedCorps.ToArray();
                parsedAlliances.Add(parsedAlliance);
            }
            theAllianceList = parsedAlliances.ToArray();
        }
    }
}
