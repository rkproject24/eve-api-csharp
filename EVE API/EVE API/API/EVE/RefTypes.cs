using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace EVE_API
{
    public class RefTypes
    {
        public DateTime currentTime { get; set; }
        public DateTime cachedUntil { get; set; }
        public RefType[] RefTypeList { get; set; }

        public RefTypes(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            currentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            cachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);

            List<RefType> parsedRefTypes = new List<RefType>();
            foreach (XmlNode row in doc.SelectNodes("//rowset[@name='refTypes']/row"))
            {
                parsedRefTypes.Add(new RefType(Convert.ToInt32(row.Attributes["refTypeID"].InnerText), row.Attributes["refTypeName"].InnerText));
            }
            RefTypeList = parsedRefTypes.ToArray();
        }
    }
}
