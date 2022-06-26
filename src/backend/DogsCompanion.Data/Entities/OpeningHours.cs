using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsCompanion.Data.Entities
{
    public class OpeningHours
    {
        public Day Day { get; set; }
        public List<Period>? Periods { get; set; }
    }

    public class Period
    {
        public DateTime Open { get; set; }
        public DateTime Close { get; set; }
    }

    public enum Day
    {
        Mon = 1,
        Tue,
        Wed,
        Thu,
        Fri,
        Sat,
        Sun
    }
}
