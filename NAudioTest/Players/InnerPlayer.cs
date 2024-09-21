using NAudioTest.TimeThings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.Players
{

    public class Orchestra
    {
        public List<APlayer> Players { get; set; } = new List<APlayer> { };
        public TimeQueue TimeQueue { get; set; } = new TimeQueue(null, 100);

    }
}
