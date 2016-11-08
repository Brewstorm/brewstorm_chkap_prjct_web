using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAppCore.Models
{
    public class Professional
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NumeroCRM { get; set; }
        public ICollection<ProfessionalAppointmentType> ProfessionalAppointmentTypes { get; set; }
    }
}
