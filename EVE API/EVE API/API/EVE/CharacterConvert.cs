using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EVE_API
{
    public class CharacterConvert
    {
        public Character [] theCharacterList { get; set; }
        public DateTime CurrentTime { get; set; }
        public DateTime CachedUntil { get; set; }

        public CharacterConvert(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            CurrentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            CachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);

            List<Character> parsedCharacters = new List<Character>();
            foreach (XmlNode row in doc.SelectNodes("//rowset[@name='characters']/row"))
            {
                parsedCharacters.Add(new Character(row.Attributes["characters"].InnerText, Convert.ToInt32(row.Attributes["characterID"].InnerText)));
            }
            theCharacterList = parsedCharacters.ToArray();
        }
    }
}
