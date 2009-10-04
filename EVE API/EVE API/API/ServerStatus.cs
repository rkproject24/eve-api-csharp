using System;
using System.Xml;
using System.Xml.Serialization;

namespace EVE_API
{
    public class ServerStatus
    {
        public DateTime currentTime { get; set; }
        public DateTime cachedUntil { get; set; }
        public bool serverOpen { get; set; }
        public string onlinePlayers { get; set; }

        public ServerStatus(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            currentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            cachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);
            serverOpen = Boolean.Parse(doc.SelectSingleNode("/eveapi/result/serverOpen").InnerText);
            onlinePlayers = doc.SelectSingleNode("/eveapi/result/onlinePlayers").InnerText;
        }
    }
}
