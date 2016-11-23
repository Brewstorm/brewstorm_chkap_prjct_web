using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAppCore.Models
{
    public class AgendamentoModel
    {
        public int AgendaScheduleID { get; set; }
        public string DateSchedule { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
    }
}
