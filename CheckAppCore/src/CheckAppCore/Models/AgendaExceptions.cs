using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAppCore.Models
{
    public class AgendaException
    {
        public int ID { get; set; }
        public int AgendaScheduleID { get; set; }
        public AgendaSchedule AgendaSchedule { get; set; }
        public int BeginTime { get; set; }
        public int EndTime { get; set; }
    }
}
