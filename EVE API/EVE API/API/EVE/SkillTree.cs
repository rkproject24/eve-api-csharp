using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace EVE_API
{
    public class SkillTree
    {
        public DateTime CurrentTime { get; set; }
        public DateTime CachedUntil { get; set; }
        public Skill[] skills { get; set; }

        public SkillTree(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            CurrentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            CachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);

            List<Skill> parsedSkills = new List<Skill>();
            foreach (XmlNode group in doc.SelectNodes("//rowset[@name='skillGroups']/row"))
            {
                Skill skill = new Skill();
                
                foreach (XmlNode row in doc.SelectNodes("//rowset[@name='skills']/row"))
                {
                    skill.Name = row.Attributes["typeName"].InnerText;
                    skill.GroupID = Convert.ToInt32(row.Attributes["groupID"].InnerText);
                    skill.SkillID = Convert.ToInt32(row.Attributes["typeID"].InnerText);
                    skill.Description = row.SelectSingleNode("description").InnerText;
                    skill.Rank = Convert.ToInt32(row.SelectSingleNode("rank").InnerText);

                    skill.PrimaryAttribute = row.SelectSingleNode("requiredAttributes/primaryAttribute").InnerText;
                    skill.SecondaryAttribute = row.SelectSingleNode("requiredAttributes/secondaryAttribute").InnerText;

                    // Required Skills
                    List<Skill> parsedRequiredSkills = new List<Skill>();
                    foreach (XmlNode requiredNode in row.SelectNodes("rowset[@name='requiredSkills']/row"))
                    {
                        Skill requiredSkill = new Skill();
                        requiredSkill.SkillID = Convert.ToInt32(requiredNode.Attributes["typeID"].InnerText);
                        requiredSkill.SkillLevel = Convert.ToInt32(requiredNode.Attributes["skillLevel"].InnerText);
                        parsedRequiredSkills.Add(requiredSkill);
                    }
                    skill.RequiredSkills = parsedRequiredSkills.ToArray();

                    // Skill Bonuses
                    List<SkillBonus> parsedSkillBonuses = new List<SkillBonus>();
                    foreach (XmlNode bonusNode in row.SelectNodes("rowset[@name='skillBonusCollection']/row"))
                    {
                        SkillBonus skillBonus = new SkillBonus();
                        skillBonus.Name = bonusNode.Attributes["bonusType"].InnerText;
                        skillBonus.Bonus = Convert.ToDouble(bonusNode.Attributes["bonusValue"].InnerText);
                        parsedSkillBonuses.Add(skillBonus);
                    }
                    skill.SkillBonusCollection = parsedSkillBonuses.ToArray();
                }
                skill.GroupName = group.Attributes["groupName"].InnerText;

                parsedSkills.Add(skill);
            }
            skills = parsedSkills.ToArray();
        }

        public class Skill
        {
            public string Name { get; set; }
            public int SkillID { get; set; }
            public string GroupName { get; set; }
            public int GroupID { get; set; }
            public string Description { get; set; }
            public int Rank { get; set; }
            public Skill[] RequiredSkills { get; set; }
            public string PrimaryAttribute { get; set; }
            public string SecondaryAttribute { get; set; }
            public SkillBonus[] SkillBonusCollection { get; set; }
            public int SkillLevel { get; set; }

            public Skill()
            {
                SkillLevel = -1;
            }
        }

        public struct SkillBonus
        {
            public string Name { get; set; }
            public double Bonus { get; set; }
        }
    }
}
