using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAppCore.DTO
{
    public class UserDataDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public int? UserType { get; set; }
    }
}
