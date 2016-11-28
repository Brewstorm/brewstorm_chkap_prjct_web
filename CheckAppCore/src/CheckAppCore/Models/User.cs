using System.Collections.Generic;
using CheckAppCore.Enumerators;

namespace CheckAppCore.Models
{
    public class User
    {
        public int ID { get; set; }
        public int? ProfessionalID { get; set; }
        public Professional Professional { get; set; }
        public int PersonalInfoID { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FacebookID { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<AgendaAppointment> AgendaAppointments { get; set; }
    }
}
