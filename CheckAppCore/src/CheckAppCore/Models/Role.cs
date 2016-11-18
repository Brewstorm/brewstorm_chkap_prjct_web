using System.Collections.Generic;
using CheckAppCore.Enumerators;

namespace CheckAppCore.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
