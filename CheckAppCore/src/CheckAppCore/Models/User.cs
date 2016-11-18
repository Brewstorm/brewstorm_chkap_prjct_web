using System.Collections.Generic;
using CheckAppCore.Enumerators;

namespace CheckAppCore.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhotoUrl { get; set; }
        public string Password { get; set; }
        public string FacebookID { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
