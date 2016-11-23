using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAppCore.Models
{
    public class AgendaSchedule
    {
        public int ID { get; set; }
        public int AgendaID { get; set; }
        public Agenda Agenda { get; set; }
        public DateTime Date { get; set; }
        public int IntervalTime { get; set; }
        public int BeginTime { get; set; }
        public int EndTime { get; set; }

        public ICollection<AgendaException> AgendaExceptions { get; set; }
        public ICollection<AgendaAppointment> AgendaAppointments { get; set; }
    }
}
