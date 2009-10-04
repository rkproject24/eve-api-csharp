using System;
using System.Xml;
using System.Xml.Serialization;

namespace EVE_API
{
    public class Error
    {
        public DateTime currentTime { get; set; }
        public DateTime cachedUntil { get; set; }
        public int Code { get; set; }
        public string CodeDescription { get; set; }

        public Error(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            currentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            cachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);
            Code = Convert.ToInt32(doc.SelectSingleNode("/eveapi/error[@='code']").Attributes["code"].Value);
            CodeDescription = doc.SelectSingleNode("/eveapi/error").InnerText;
        }

        public Error(int code, string description, DateTime current, DateTime cached)
        {
            Code = code;
            CodeDescription = description;
            currentTime = current;
            cachedUntil = cached;
        }
    }
}
