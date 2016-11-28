using System.Collections.Generic;

namespace CheckAppCore.Models
{
    public class Professional
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public User User { get; set; }
        public string NumeroCRM { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }

        public ICollection<ProfessionalAppointmentType> ProfessionalAppointmentTypes { get; set; }
    }
}
