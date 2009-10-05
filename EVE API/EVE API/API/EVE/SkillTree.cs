using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace EVE_API
{
    public class SkillTree
    {
        public DateTime currentTime { get; set; }
        public DateTime cachedUntil { get; set; }
        public SkillGroup[] skillGroups { get; set; }
        public Skill[] skills { get; set; }

        public SkillTree(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            currentTime = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/currentTime").InnerText), DateTimeKind.Utc);
            cachedUntil = DateTime.SpecifyKind(DateTime.Parse(doc.SelectSingleNode("/eveapi/cachedUntil").InnerText), DateTimeKind.Utc);

            List<SkillGroup> parsedGroups = new List<SkillGroup>();
            List<Skill> parsedSkills = new List<Skill>();
            foreach (XmlNode group in doc.SelectNodes("//rowset[@name='skillGroups']/row"))
            {
                parsedGroups.Add(new SkillGroup(group.Attributes["groupName"].InnerText, Convert.ToInt32(group.Attributes["groupID"].InnerText)));
            }
            skillGroups = parsedGroups.ToArray();

            foreach (XmlNode group in doc.SelectNodes("//rowset[@name='skills']/row"))
            {
                Skill skill = new Skill();
                skill.Name = group.Attributes["typeName"].InnerText;
                skill.GroupID = Convert.ToInt32(group.Attributes["groupID"].InnerText);
                skill.SkillID = Convert.ToInt32(group.Attributes["typeID"].InnerText);
                skill.Description = group.SelectSingleNode("description").InnerText;
                skill.Rank = Convert.ToInt32(group.SelectSingleNode("rank").InnerText);

                skill.PrimaryAttribute = group.SelectSingleNode("requiredAttributes/primaryAttribute").InnerText;
                skill.SecondaryAttribute = group.SelectSingleNode("requiredAttributes/secondaryAttribute").InnerText;

                // Required Skills
                List<Skill> parsedRequiredSkills = new List<Skill>();
                foreach (XmlNode requiredNode in group.SelectNodes("rowset[@name='requiredSkills']/row"))
                {
                    Skill requiredSkill = new Skill();
                    requiredSkill.SkillID = Convert.ToInt32(requiredNode.Attributes["typeID"].InnerText);
                    requiredSkill.SkillLevel = Convert.ToInt32(requiredNode.Attributes["skillLevel"].InnerText);
                    parsedRequiredSkills.Add(requiredSkill);
                }
                skill.RequiredSkills = parsedRequiredSkills.ToArray();

                // Skill Bonuses
                List<SkillBonus> parsedSkillBonuses = new List<SkillBonus>();
                foreach (XmlNode bonusNode in group.SelectNodes("rowset[@name='skillBonusCollection']/row"))
                {
                    SkillBonus skillBonus = new SkillBonus();
                    skillBonus.Name = bonusNode.Attributes["bonusType"].InnerText;
                    skillBonus.Bonus = Convert.ToDouble(bonusNode.Attributes["bonusValue"].InnerText);
                    parsedSkillBonuses.Add(skillBonus);
                }
                skill.SkillBonusCollection = parsedSkillBonuses.ToArray();

                parsedSkills.Add(skill);
            }
            skills = parsedSkills.ToArray();
        }

        public class SkillGroup
        {
            public string Name { get; set; }
            public int GroupID { get; set; }

            public SkillGroup(string name, int id)
            {
                Name = name;
                GroupID = id;
            }
        }

        public class Skill
        {
            public string Name { get; set; }
            public int SkillID { get; set; }
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

        public class SkillBonus
        {
            public string Name { get; set; }
            public double Bonus { get; set; }
        }
    }
}
