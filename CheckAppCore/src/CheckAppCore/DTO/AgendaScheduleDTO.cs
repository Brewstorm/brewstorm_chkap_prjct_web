using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAppCore.DTO
{
    public class AgendaScheduleDTO
    {
        public int AgendaScheduleID { get; set; }
        public string BeginTime { get; set; }
        public DateTime Date { get; internal set; }
        public string EndTime { get; set; }
    }
}
