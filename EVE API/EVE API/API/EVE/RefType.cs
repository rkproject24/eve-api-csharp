using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVE_API
{
    public class RefType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public RefType(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
