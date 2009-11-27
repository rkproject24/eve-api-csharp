using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace EVE_API
{
    public class ErrorList
    {
        public DateTime CurrentTime { get; set; }
        public DateTime CachedUntil { get; set; }
        public Error [] theErrorList { get; set; }

        public ErrorList(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            CurrentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            CachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);

            List<Error> parsedErrors = new List<Error>();
            foreach (XmlNode row in doc.SelectNodes("//rowset[@name='errors']/row"))
            {
                parsedErrors.Add(new Error(Convert.ToInt32(row.Attributes["errorCode"].InnerText), row.Attributes["errorText"].InnerText, currentTime, cachedUntil));
            }
            theErrorList = parsedErrors.ToArray();
        }
    }
}
