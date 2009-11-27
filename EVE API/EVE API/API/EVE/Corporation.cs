using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVE_API
{
    public class Corporation
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }

        public Corporation(int id, DateTime startDate)
        {
            ID = id;
            StartDate = startDate;
        }
    }
}
