using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVE_API
{
    public class Alliance
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int ID { get; set; }
        public int ExecutorCorpID { get; set; }
        public int MemberCount { get; set; }
        public DateTime StartDate { get; set; }
        public Corporation[] CorpList { get; set; }

        public Alliance(string name, string shortName, int id, int executorID, int memberCount, DateTime startDate)
        {
            Name = name;
            ShortName = shortName;
            ID = id;
            ExecutorCorpID = executorID;
            MemberCount = memberCount;
            StartDate = startDate; 
        }
    }
}
