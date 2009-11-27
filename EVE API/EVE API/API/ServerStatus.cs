using System;
using System.Xml;
using System.Xml.Serialization;

namespace EVE_API
{
    public class ServerStatus
    {
        public DateTime CurrentTime { get; set; }
        public DateTime CachedUntil { get; set; }
        public bool serverOpen { get; set; }
        public string onlinePlayers { get; set; }

        public ServerStatus(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            CurrentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            CachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);

            serverOpen = Boolean.Parse(doc.SelectSingleNode("/eveapi/result/serverOpen").InnerText);
            onlinePlayers = doc.SelectSingleNode("/eveapi/result/onlinePlayers").InnerText;
        }
    }
}
