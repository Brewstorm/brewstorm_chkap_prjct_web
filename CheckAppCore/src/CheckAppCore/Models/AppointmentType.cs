using System;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CheckAppCore.Models
{
    public class AppointmentType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ProfessionalName { get; set; }
        public ICollection<ProfessionalAppointmentType> ProfessionalAppointmentTypes { get; set; }
    }
}
