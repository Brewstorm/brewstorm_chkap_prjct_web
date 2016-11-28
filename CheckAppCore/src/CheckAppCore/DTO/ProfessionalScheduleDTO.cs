using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAppCore.DTO
{
    public class ProfessionalScheduleDTO
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Patient { get; set; }
        public string AppointmentType { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public int Status { get; set; }
        public string PhotoUrl { get; set; }
    }
}
