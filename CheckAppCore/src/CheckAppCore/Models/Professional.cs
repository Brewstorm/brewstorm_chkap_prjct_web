using System.Collections.Generic;

namespace CheckAppCore.Models
{
    public class Professional
    {
        public int ID { get; set; }
        public string NumeroCRM { get; set; }
        public ICollection<ProfessionalAppointmentType> ProfessionalAppointmentTypes { get; set; }
        public int PersonalInfoID { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public PersonalInfo PersonalInfo { get; internal set; }
    }
}
