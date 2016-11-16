using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAppCore.Models
{
    public class Agenda
    {
        public int ID { get; set; }
        public int ProfessionalID { get; set; }
        public Professional Professional { get; set; }
        public int AppointmentTypeID { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public bool Active { get; set; } = true;
        public int AgendaScheduleID { get; set; }
        public AgendaSchedule AgendaSchedule { get;  set; }
    }
}
