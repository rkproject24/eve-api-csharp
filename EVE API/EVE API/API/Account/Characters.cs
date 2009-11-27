using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EVE_API
{
    public class Characters
    {
        public DateTime CurrentTime { get; set; }
        public DateTime CachedUntil { get; set; }
        public Character [] CharacterList { get; set; }

        public Characters(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            CurrentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            CachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);

            List<Character> parsedCharacters = new List<Character>();
            foreach (XmlNode row in doc.SelectNodes("//rowset[@name='characters']/row"))
            {
                Character character = new Character();
                character.Name = row.Attributes["name"].InnerText;
                character.CharacterID = Convert.ToInt32(row.Attributes["characterID"].InnerText);
                character.CorpName = row.Attributes["corporationName"].InnerText;
                character.CorpID = Convert.ToInt32(row.Attributes["corporationID"].InnerText);
                parsedCharacters.Add(character);
            }
            CharacterList = parsedCharacters.ToArray();
        }
    }
}
