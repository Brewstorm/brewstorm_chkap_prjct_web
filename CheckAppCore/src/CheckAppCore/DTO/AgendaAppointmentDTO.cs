using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAppCore.DTO
{
    public class AgendaAppointmentDTO
    {
        public string AppointmentType { get; internal set; }
        public string BeginTime { get; internal set; }
        public DateTime Date { get; internal set; }
        public string EndTime { get; internal set; }
        public int ID { get; internal set; }
        public string Professional { get; internal set; }
        public int Status { get; set; }
    }
}
