using System;

namespace EVE_API
{
    public class Character
    {
        public string Name { get; set; }
        public int CharacterID { get; set; }
        public string CorpName { get; set; }
        public int CorpID { get; set; }

        public Character()
        {

        }

        public Character(string name, int id)
        {
            Name = name;
            CharacterID = id;
        }
    }
}
