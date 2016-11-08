using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAppCore.Models
{
    public class ProfessionalAppointmentType
    {
        public int ProfessionalId { get; set; }
        public Professional Professional { get; set; }

        public int AppointmentTypeId { get; set; }
        public AppointmentType AppointmentType { get; set; }
    }
}
